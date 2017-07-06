using System;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolInsertCircleElement : YutaiTool
    {
        private INewCircleFeedback _circleFeedback = null;


        public override bool Enabled
        {
            get { return _context.ActiveView != null; }
        }

        public ToolInsertCircleElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "";
            base.m_toolTip = "新建圆";
            base.m_message = "新建圆";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_circle;
            base.m_name = "Printing_NewCircle";
            _key = "Printing_NewCircle";
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
            IActiveView focusMap = null;


            if (this._context.MainView.ControlType == GISControlType.PageLayout)
            {
                focusMap =
                    (this._context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView;
            }

            return focusMap;
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            IActiveView activeView = this._context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (this._circleFeedback == null)
            {
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
                    this._circleFeedback = null;
                    IPolygon polygon = new Polygon() as IPolygon;
                    object value = Missing.Value;
                    (polygon as ISegmentCollection).AddSegment(circularArc as ISegment, ref value, ref value);
                    IElement element = new CircleElement
                    {
                        Geometry = polygon
                    };
                    INewElementOperation operation = new NewElementOperation
                    {
                        ActiveView = this._context.ActiveView,
                        ContainHook = this.GetActiveView(),
                        Element = element
                    };
                    this._context.OperationStack.Do(operation);
                    //if (this._context.Hook is IApplication)
                    //{
                    //    if ((this._context.Hook as IApplication).ContainerHook != null)
                    //    {
                    //        DocumentManager.DocumentChanged((this._context.Hook as IApplication).ContainerHook);
                    //    }
                    //    else
                    //    {
                    //        DocumentManager.DocumentChanged((this._context.Hook as IApplication).Hook);
                    //    }
                    //}
                    //else
                    //{
                    //    DocumentManager.DocumentChanged(this._context.Hook);
                    //}
                }
                catch (Exception exception_)
                {
                    //CErrorLog.writeErrorLog(this, exception_, "");
                }
            }
        }
    }
}