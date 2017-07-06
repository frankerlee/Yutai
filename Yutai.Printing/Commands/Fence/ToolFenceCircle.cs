using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Printing.Commands.Fence
{

    public class CmdlFenceClear : YutaiCommand
    {
        
        private PrintingPlugin _plugin;
      

        public override bool Enabled
        {
            get { return true; }
        }

        public CmdlFenceClear(IAppContext context, PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "删除范围设置";
            base.m_toolTip = "删除范围设置";
            base.m_message = "删除范围设置";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_map_cleae;
            base.m_name = "Printing_Fence_Clear";
            _key = "Printing_Fence_Clear";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
           
        }

        public override void OnClick()
        {
            _plugin.ClearFence();
            IActiveView activeView = this._context.FocusMap as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
    public class ToolFenceCircle : YutaiTool
    {
        private INewCircleFeedback _circleFeedback = null;
        private PrintingPlugin _plugin;
        private IFillSymbol _symbol;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public ToolFenceCircle(IAppContext context,PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "圆";
            base.m_toolTip = "新建圆形绘图范围";
            base.m_message = "新建圆形绘图范围";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_circle;
            base.m_name = "Printing_Fence_NewCircle";
            _key = "Printing_Fence_NewCircle";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            m_cursor =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
          
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
       
        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
            }
            IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x,
                y);
          
            if (this._circleFeedback == null)
            {
                IActiveView activeView = (IActiveView)this._context.FocusMap;
                this._circleFeedback = new NewCircleFeedback();
                this._circleFeedback.Display = activeView.ScreenDisplay;
                this._circleFeedback.Start(point);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._circleFeedback != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this._circleFeedback.MoveTo(point);
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (this._circleFeedback != null)
            {
                try
                {
                    ICircularArc circularArc = this._circleFeedback.Stop();
                    if (circularArc == null || circularArc.IsEmpty)
                    {
                        this._circleFeedback = null;
                        return;
                    }
                    this._circleFeedback = null;
                    IPolygon polygon = new Polygon() as IPolygon;
                    object value = Missing.Value;
                    (polygon as ISegmentCollection).AddSegment(circularArc as ISegment, ref value, ref value);
                    _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)polygon));
                    IActiveView activeView = this._context.FocusMap as IActiveView;
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography,null,polygon.Envelope);

                }
                catch (Exception exception_)
                {
                    //CErrorLog.writeErrorLog(this, exception_, "");
                }
            }
        }
    }


    public class ToolFenceCircleRadius : YutaiTool
    {
       
        private PrintingPlugin _plugin;
        private IFillSymbol _symbol;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public ToolFenceCircleRadius(IAppContext context, PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "圆";
            base.m_toolTip = "新建圆形绘图范围";
            base.m_message = "新建圆形绘图范围";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_circle;
            base.m_name = "Printing_Fence_NewCircleRadius";
            _key = "Printing_Fence_NewCircleRadius";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            m_cursor =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);

        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 1) return;
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
            }
            IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x,
                y);

           frmInputText frm=new frmInputText();
            frm.txtText.Properties.EditFormat.FormatType = FormatType.Numeric;
            frm.Label = "输入半径";
            if (frm.ShowDialog() != DialogResult.OK) return;
            try
            {
                double radius = Convert.ToDouble(frm.txtText.EditValue);
                IConstructCircularArc2 arc2 = new CircularArc() as IConstructCircularArc2;
                arc2.ConstructCircle(point, radius,false);
                IPolygon polygon = new Polygon() as IPolygon;
                object value = Missing.Value;
                (polygon as ISegmentCollection).AddSegment(arc2 as ISegment, ref value, ref value);
                _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)polygon));
                IActiveView activeView = this._context.FocusMap as IActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, polygon.Envelope);
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("半径必须为数字!");
            }
            
        }

    
    }


    public class ToolFenceRectangle : YutaiTool
    {
        private INewEnvelopeFeedback pEnvelopeFeedback = null;
        private PrintingPlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "矩形";
            this.m_message = "新建矩形绘图范围";
            this.m_toolTip = "新建矩形绘图范围";
            this.m_category = "制图";
            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
            base.m_bitmap = Properties.Resources.icon_rectangle;
            base.m_name = "Printing_Fence_Rectangle";
            _key = "Printing_Fence_Rectangle";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolFenceRectangle(IAppContext context,PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button != 1) return;
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
            }
            IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x,
                y);
            if (pEnvelopeFeedback == null)
            {
                pEnvelopeFeedback=new NewEnvelopeFeedback();
                pEnvelopeFeedback.Display = ((IActiveView) this._context.FocusMap).ScreenDisplay;
                pEnvelopeFeedback.Start(point);
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this.pEnvelopeFeedback != null)
            {
                IActiveView activeView = this._context.FocusMap as IActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.pEnvelopeFeedback.MoveTo(point);
            }
        }

        private IActiveView GetActiveView()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);
            else if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                result = _context.MapControl.ActiveView;
            }

            return result;
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (this.pEnvelopeFeedback != null)
            {
                IEnvelope geometry = this.pEnvelopeFeedback.Stop();
                if (geometry == null || geometry.IsEmpty)
                {
                    pEnvelopeFeedback = null;
                    return;
                }
                IGeometry polygon = GeometryUtility.ConvertEnvelopeToPolygon(geometry);
             
                _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)polygon));
                IActiveView activeView = this._context.FocusMap as IActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, geometry);

                pEnvelopeFeedback = null;
            }
        }
    }

    public class ToolFencePolygon : YutaiTool
    {
        private INewPolygonFeedback inewPolygonFeedback_0 = null;
        private PrintingPlugin _plugin;
        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "新建多边形绘图范围";
            this.m_toolTip = "新建多边形绘图范围";
            this.m_category = "Printing";
            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
            base.m_bitmap = Properties.Resources.icon_polygon;
            base.m_name = "Printing_Fence_olygon";
            _key = "Printing_Fence_olygon";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolFencePolygon(IAppContext context,PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        private IActiveView GetActiveView()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);
            else if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                result = _context.MapControl.ActiveView;
            }

            return result;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button != 1) return;
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
            }
            IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
            if (inewPolygonFeedback_0 == null)
            {
                inewPolygonFeedback_0 = new NewPolygonFeedback();
                inewPolygonFeedback_0.Display = ((IActiveView)this._context.FocusMap).ScreenDisplay;
                inewPolygonFeedback_0.Start(point);
                return;
            }
            inewPolygonFeedback_0.AddPoint(point);
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this.inewPolygonFeedback_0 != null)
            {
                IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
                this.inewPolygonFeedback_0.MoveTo(point);
            }
        }

        public override void OnKeyDown(int button, int shift)
        {
            if (button == 27)
            {
                this.inewPolygonFeedback_0 = null;
                ((IActiveView)this._context.FocusMap).Refresh();
            }
        }

        public override void OnDblClick()
        {
            if (this.inewPolygonFeedback_0 != null)
            {
                IGeometry geometry = this.inewPolygonFeedback_0.Stop();
                if (geometry == null || geometry.IsEmpty)
                {
                    inewPolygonFeedback_0 = null;
                    return;
                }
                _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)geometry));
                IActiveView activeView = this._context.FocusMap as IActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, geometry.Envelope);

                inewPolygonFeedback_0 = null;
            }
        }
    }



    public class ToolFenceLineBuffer : YutaiTool
    {
        private INewLineFeedback _lineFeedback = null;
        private PrintingPlugin _plugin;
        private IPoint _Point = null;

        private esriLineConstraints lineConstraints = esriLineConstraints.esriLineConstraintsNone;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public ToolFenceLineBuffer(IAppContext context,PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "";
            base.m_toolTip = "新建线缓冲绘图范围";
            base.m_message = "新建线缓冲绘图范围";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_line;
            base.m_name = "Printing_Fence_LineBuffer";
            _key = "Printing_Fence_LineBuffer";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            m_cursor =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        private IActiveView GetActiveView()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);
            else if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                result = _context.MapControl.ActiveView;
            }

            return result;
        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 1) return;
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
            }
            IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            
            if (this._lineFeedback != null)
            {
                if (this._Point != null)
                {
                    if (this.lineConstraints == esriLineConstraints.esriLineConstraintsVertical)
                    {
                        point.X = this._Point.X;
                    }
                    else if (this.lineConstraints == esriLineConstraints.esriLineConstraintsHorizontal)
                    {
                        point.Y = this._Point.Y;
                    }
                }
                this._lineFeedback.AddPoint(point);
                this._Point = point;
            }
            else
            {
                this._lineFeedback = new NewLineFeedback()
                {
                    Constraint = this.lineConstraints,
                    Display = ((IActiveView)this._context.FocusMap).ScreenDisplay
                };
                this._lineFeedback.Start(point);
                this._Point = point;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._lineFeedback != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint mapPoint = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                if (this._Point != null)
                {
                    if (this.lineConstraints == esriLineConstraints.esriLineConstraintsVertical)
                    {
                        mapPoint.X = this._Point.X;
                    }
                    else if (this.lineConstraints == esriLineConstraints.esriLineConstraintsHorizontal)
                    {
                        mapPoint.Y = this._Point.Y;
                    }
                }
                this._lineFeedback.MoveTo(mapPoint);
            }
        }


        public override void OnDblClick()
        {
            if (this._lineFeedback != null)
            {
                this._Point = null;
                IPolyline polyline = this._lineFeedback.Stop();
                this._lineFeedback = null;
                if (!polyline.IsEmpty)
                {
                    frmInputText frm=new frmInputText();
                    frm.Title = "输入缓冲距离";
                    frm.Label = "缓冲距离";
                    frm.txtText.EditValue = 50;
                    if (frm.ShowDialog() != DialogResult.OK) return;
                    double bufferDist = 50;
                    try
                    {
                         bufferDist = Convert.ToDouble(frm.txtText.EditValue);
                    }
                    catch (Exception)
                    {
                        MessageService.Current.Warn("缓冲距离必须为数字!");
                        return;
                    }
                    ITopologicalOperator topo=polyline as ITopologicalOperator;
                    IGeometry buffer=topo.Buffer(bufferDist);
                    _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)buffer));
                    IActiveView activeView = this._context.FocusMap as IActiveView;
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, buffer.Envelope);
                }

            }
        }

        public override void OnKeyDown(int button, int shift)
        {
            if (button == 27)
            {
                this._lineFeedback = null;
                this._context.ActiveView.Refresh();
                this.lineConstraints = esriLineConstraints.esriLineConstraintsNone;
                this._Point = null;
            }
            else if (button == 16)
            {
                this.lineConstraints = esriLineConstraints.esriLineConstraintsHorizontal;
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsHorizontal;
                }
            }
            else if (button == 17)
            {
                this.lineConstraints = esriLineConstraints.esriLineConstraintsVertical;
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsHorizontal;
                }
            }
        }

        public override void OnKeyUp(int button, int shift)
        {
            this.lineConstraints = esriLineConstraints.esriLineConstraintsNone;
            if (this._lineFeedback != null)
            {
                this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsNone;
            }
        }
    }



    public class ToolFenceLine : YutaiTool
    {
        private INewLineFeedback _lineFeedback = null;
        private PrintingPlugin _plugin;
        private IPoint _Point = null;

        private esriLineConstraints lineConstraints = esriLineConstraints.esriLineConstraintsNone;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public ToolFenceLine(IAppContext context, PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "";
            base.m_toolTip = "新建带状绘图范围";
            base.m_message = "新建带状绘图范围";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_line;
            base.m_name = "Printing_Fence_Line";
            _key = "Printing_Fence_Line";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            m_cursor =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
        }

        public override void OnClick()
        {
            if(_plugin.Fences!=null && _plugin.Fences.Count>0)
            if (MessageService.Current.Ask("带状图边界不能与其他边界共存，继续执行将清空现有绘图范围，继续吗?") != true) return;
            _plugin.ClearFence();
            _context.SetCurrentTool(this);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        private IActiveView GetActiveView()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);
            else if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                result = _context.MapControl.ActiveView;
            }

            return result;
        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 1) return;
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
            }
            IPoint point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);


            if (this._lineFeedback != null)
            {
                if (this._Point != null)
                {
                    if (this.lineConstraints == esriLineConstraints.esriLineConstraintsVertical)
                    {
                        point.X = this._Point.X;
                    }
                    else if (this.lineConstraints == esriLineConstraints.esriLineConstraintsHorizontal)
                    {
                        point.Y = this._Point.Y;
                    }
                }
                this._lineFeedback.AddPoint(point);
                this._Point = point;
            }
            else
            {
                this._lineFeedback = new NewLineFeedback()
                {
                    Constraint = this.lineConstraints,
                    Display = ((IActiveView)this._context.FocusMap).ScreenDisplay
                };
                this._lineFeedback.Start(point);
                this._Point = point;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._lineFeedback != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint mapPoint = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                if (this._Point != null)
                {
                    if (this.lineConstraints == esriLineConstraints.esriLineConstraintsVertical)
                    {
                        mapPoint.X = this._Point.X;
                    }
                    else if (this.lineConstraints == esriLineConstraints.esriLineConstraintsHorizontal)
                    {
                        mapPoint.Y = this._Point.Y;
                    }
                }
                this._lineFeedback.MoveTo(mapPoint);
            }
        }


        public override void OnDblClick()
        {
            if (this._lineFeedback != null)
            {
                this._Point = null;
                IPolyline polyline = this._lineFeedback.Stop();
                this._lineFeedback = null;
                if (!polyline.IsEmpty)
                {
                  
                    _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)polyline));
                    IActiveView activeView = this._context.FocusMap as IActiveView;
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, polyline.Envelope);
                }
            }
        }

        public override void OnKeyDown(int button, int shift)
        {
            if (button == 27)
            {
                this._lineFeedback = null;
                this._context.ActiveView.Refresh();
                this.lineConstraints = esriLineConstraints.esriLineConstraintsNone;
                this._Point = null;
            }
            else if (button == 16)
            {
                this.lineConstraints = esriLineConstraints.esriLineConstraintsHorizontal;
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsHorizontal;
                }
            }
            else if (button == 17)
            {
                this.lineConstraints = esriLineConstraints.esriLineConstraintsVertical;
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsHorizontal;
                }
            }
        }

        public override void OnKeyUp(int button, int shift)
        {
            this.lineConstraints = esriLineConstraints.esriLineConstraintsNone;
            if (this._lineFeedback != null)
            {
                this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsNone;
            }
        }
    }


    public class CmdlFenceExtent : YutaiCommand
    {

        private PrintingPlugin _plugin;


        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return _context.FocusMap != null;
            }
        }

        public CmdlFenceExtent(IAppContext context, PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "新建视图绘图范围";
            base.m_toolTip = "新建视图绘图范围";
            base.m_message = "新建视图绘图范围";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_fence_extent;
            base.m_name = "Printing_Fence_Extent";
            _key = "Printing_Fence_Extent";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

        }

        public override void OnClick()
        {
            IActiveView activeView = this._context.FocusMap as IActiveView;
            IEnvelope pEnvelope = activeView.Extent;
            IGeometry polygon = GeometryUtility.ConvertEnvelopeToPolygon(pEnvelope);
            _plugin.FireFenceAdded(new FenceAddedArgs((IGeometry)polygon));
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pEnvelope);
           
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}
