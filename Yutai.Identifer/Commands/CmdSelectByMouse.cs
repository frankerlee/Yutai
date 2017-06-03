using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Identifer.Helpers;
using Yutai.Plugins.Interfaces;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Plugins.Identifer.Commands
{
    public class CmdSelectByMouse:YutaiTool, ICommandSubType
    {
      
        public static esriSelectionResultEnum m_SelectionResultEnum;

        private bool _isFree = false;

        private IPoint _mPoint = null;

        private IDisplayFeedback _displayFeedback = null;

        private int _subType = 0;

        private int int_1 = -1;

        private int int_2 = 0;

        private string _basicName;

     

        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (this._context.MapControl.Map == null || this._context.MapControl.Map.LayerCount<= 0 ? false : true);
                return flag;
            }
        }

        public CmdSelectByMouse(IAppContext context)
        {
            _context = context as IAppContext;
            m_SelectionResultEnum = esriSelectionResultEnum.esriSelectionResultNew;
        }
        public override void OnClick(object sender, EventArgs args)
        {
            
            ToolStripItem strip= sender as ToolStripItem;
            if (strip.Name == _basicName) return;
           
            CmdSelectByMouse tag = strip.Tag as CmdSelectByMouse;
            _context.SetCurrentTool(tag);
        }

        public override void OnCreate(object hook)
        {
            _context=hook as IAppContext;
            m_SelectionResultEnum = esriSelectionResultEnum.esriSelectionResultNew;
        }

        public int GetCount()
        {
            return 2;
        }

        private IMap GetMap()
        {
            return this._context.MapControl.Map;
        }

        

        public override void OnDblClick()
        {
            ISelectionEnvironment selectionEnvironment;
            if (this._isFree && this._subType == 1)
            {
                IGeometry pPoint = (this._displayFeedback as INewPolygonFeedback).Stop();
                if (pPoint.IsEmpty)
                {
                    pPoint = this._mPoint;
                }
                pPoint.SpatialReference =GetMap().SpatialReference;
                IMap focusMap =GetMap();
                IActiveView activeView = focusMap as IActiveView;

                selectionEnvironment = _context.Config.SelectionEnvironment;
                
                if (this.int_2 <= 0)
                {
                    selectionEnvironment.CombinationMethod = m_SelectionResultEnum;
                }
                else
                {
                    selectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultXOR;
                }
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                try
                {
                    focusMap.SelectByShape(pPoint, selectionEnvironment, false);
                    //this.m_HookHelper.UpdateUI();
                }
                catch (COMException cOMException1)
                {
                    COMException cOMException = cOMException1;
                    if (cOMException.ErrorCode != -2147467259)
                    {
                        //CErrorLog.writeErrorLog(this, cOMException, "");
                    }
                    else
                    {
                        MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                    }
                }
                catch (Exception exception)
                {
                   // CErrorLog.writeErrorLog(this, exception, "");
                }
                selectionEnvironment.CombinationMethod = m_SelectionResultEnum;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                this._displayFeedback = null;
                this._isFree = false;
            }
        }

        public override void OnKeyDown(int keyCode, int shift)
        {
            if (this._isFree && keyCode == 27)
            {
                this._displayFeedback = null;
                this._isFree = false;
                _context.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button != 2)
            {
                this.int_2 = shift;
                if (_context.MapControl.ActiveView is IPageLayout)
                {
                    IPoint mapPoint = _context.MapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                    IMap map = _context.MapControl.ActiveView.HitTestMap(mapPoint);
                    if (map == null)
                    {
                        return;
                    }
                    _context.MapControl.ActiveView.FocusMap = map;
                    _context.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
                this.int_1 = x;
                IActiveView focusMap =GetMap() as IActiveView;
                this._mPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this._isFree = true;
                if (this._subType == 1)
                {
                    if (this._displayFeedback != null)
                    {
                        (this._displayFeedback as INewPolygonFeedback).AddPoint(this._mPoint);
                    }
                    else
                    {
                        this._displayFeedback = new NewPolygonFeedback()
                        {
                            Display = focusMap.ScreenDisplay
                        };
                        (this._displayFeedback as INewPolygonFeedback).Start(this._mPoint);
                    }
                }
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this._isFree)
            {
                IActiveView focusMap =GetMap() as IActiveView;
                if (this._displayFeedback == null)
                {
                    switch (this._subType)
                    {
                        case 0:
                            {
                                this._displayFeedback = new NewEnvelopeFeedback()
                                {
                                    Display = focusMap.ScreenDisplay
                                };
                                (this._displayFeedback as INewEnvelopeFeedback).Start(this._mPoint);
                                break;
                            }
                        case 2:
                            {
                                this._displayFeedback = new NewCircleFeedback()
                                {
                                    Display = focusMap.ScreenDisplay
                                };
                                (this._displayFeedback as INewCircleFeedback).Start(this._mPoint);
                                break;
                            }
                    }
                }
                this._displayFeedback.MoveTo(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            bool flag;
            ISelectionEnvironment selectionEnvironmentClass;
            if (this._isFree && this._subType != 1)
            {
                IGeometry ipoint0 = null;
                if (this._displayFeedback != null)
                {
                    if (this._displayFeedback is INewEnvelopeFeedback)
                    {
                        ipoint0 = (this._displayFeedback as INewEnvelopeFeedback).Stop();
                    }
                    else if (this._displayFeedback is INewCircleFeedback)
                    {
                        ipoint0 = (this._displayFeedback as INewCircleFeedback).Stop();
                    }
                    if (ipoint0.IsEmpty)
                    {
                        ipoint0 = this._mPoint;
                    }
                    flag = false;
                }
                else
                {
                    IActiveView focusMap =GetMap() as IActiveView;
                    IEnvelope envelope = this._mPoint.Envelope;
                    double searchTolerance = 8;
                   
                     searchTolerance = (double)_context.Config.SelectionEnvironment.SearchTolerance;
                    
                    searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)_context.MapControl.ActiveView, searchTolerance);
                    envelope.Height = searchTolerance;
                    envelope.Width = searchTolerance;
                    envelope.CenterAt(this._mPoint);
                    ipoint0 = envelope;
                    flag = true;
                }
                ipoint0.SpatialReference =GetMap().SpatialReference;
                IMap map =GetMap();
                IActiveView activeView = map as IActiveView;
                
                    selectionEnvironmentClass =_context.Config.SelectionEnvironment;
              
                if (shift <= 0)
                {
                    selectionEnvironmentClass.CombinationMethod = m_SelectionResultEnum;
                }
                else
                {
                    selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultXOR;
                }
                esriSpatialRelEnum linearSelectionMethod = selectionEnvironmentClass.LinearSelectionMethod;
                esriSpatialRelEnum areaSelectionMethod = selectionEnvironmentClass.AreaSelectionMethod;
                if (ipoint0 is IEnvelope)
                {
                    if (this.int_1 == -1)
                    {
                        this.int_1 = -1;
                    }
                    else if (this.int_1 <= x)
                    {
                        selectionEnvironmentClass.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        selectionEnvironmentClass.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                try
                {
                    map.SelectByShape(ipoint0, selectionEnvironmentClass, flag);
                   // this.m_HookHelper.UpdateUI();
                }
                catch (COMException cOMException1)
                {
                    COMException cOMException = cOMException1;
                    if (cOMException.ErrorCode != -2147467259)
                    {
                       // CErrorLog.writeErrorLog(this, cOMException, "");
                    }
                    else
                    {
                        MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                    }
                }
                catch (Exception exception)
                {
                   // CErrorLog.writeErrorLog(this, exception, "");
                }
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                selectionEnvironmentClass.CombinationMethod = m_SelectionResultEnum;
                selectionEnvironmentClass.LinearSelectionMethod = linearSelectionMethod;
                selectionEnvironmentClass.AreaSelectionMethod = areaSelectionMethod;
                this._displayFeedback = null;
                this._isFree = false;
            }
        }

        public   int SubType
            {
            get { return _subType; }
            set
            {
                SetSubType(value);
            }
        }

        public void SetSubType(int subType)
        {
            this._subType = subType;
            switch (subType)
            {
                case -1:
                    {
                        base.m_bitmap = Properties.Resources.icon_select_mouse;
                        base.m_toolTip = "图形选择";
                        base.m_name = "Query.SelectionTools.Selector";
                        _basicName = this.m_name;
                        base.m_message = "图形选择";
                        base.m_caption = "图形选择";
                        base.m_category = "Query";
                        base._key = "Query.SelectionTools.Selector";
                        base._itemType = RibbonItemType.DropDown;
                        base.ToolStripItemImageScalingYT= ToolStripItemImageScalingYT.None;
                        base.DisplayStyleYT= DisplayStyleYT.ImageAndText;
                        base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
                        break;
                    }
                case 0:
                {
                        base.m_bitmap = Properties.Resources.SelectFeatures;
                        base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Identifer.Resources.Cursor.MoveSelectFeatures.cur"));
                        base.m_toolTip = "矩形选择";
                        base.m_name = "Query.SelectionTools.Mouse.RectangleSelector";
                        base.m_message = "矩形选择";
                        base.m_caption = "矩形选择";
                        base.m_category = "Query";
                        base._key = "Query.SelectionTools.Mouse.RectangleSelector";
                        base._itemType = RibbonItemType.Tool;
                        base.DisplayStyleYT= DisplayStyleYT.ImageAndText;
                        base.TextImageRelationYT= TextImageRelationYT.ImageBeforeText;
                        break;
                    }
                case 1:
                {
                        base.m_bitmap = Properties.Resources.PolygonSelectFeatures;
                        base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Identifer.Resources.Cursor.PolygonMoveSelectFeatures.cur"));
                        base.m_toolTip = "多边形选择";
                        base.m_name = "Query.SelectionTools.Mouse.PolygonSelector";
                        base.m_message = "多边形选择";
                        base.m_caption = "多边形选择";
                        base.m_category = "Query";
                        base._key = "Query.SelectionTools.Mouse.PolygonSelector";
                        base._itemType = RibbonItemType.Tool;
                        base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
                        base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
                        break;
                    }
            }
        }

        //因为在YTTool中，ItemType暂时写死为Tool，但是DropDown需要变化，所以进行了改变
        //public override RibbonItemType ItemType
        //{
        //    get { return _itemType; }
        //    set { _itemType = value; }
        //}
    }
}

