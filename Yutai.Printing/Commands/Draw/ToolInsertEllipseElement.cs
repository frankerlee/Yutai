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
    public class ToolInsertEllipseElement : YutaiTool
    {
        private INewEnvelopeFeedback inewEnvelopeFeedback_0 = null;


        public override bool Enabled
        {
            get { return _context.ActiveView != null; }
        }

        public ToolInsertEllipseElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "椭圆";
            base.m_toolTip = "新建椭圆";
            base.m_message = "新建椭圆";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_ellipse;
            base.m_name = "Printing_NewEllipse";
            _key = "Printing_NewEllipse";
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
            if (this.inewEnvelopeFeedback_0 == null)
            {
                this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback();
                this.inewEnvelopeFeedback_0.Display = activeView.ScreenDisplay;
                this.inewEnvelopeFeedback_0.Start(point);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this.inewEnvelopeFeedback_0 != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.inewEnvelopeFeedback_0.MoveTo(point);
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (this.inewEnvelopeFeedback_0 != null)
            {
                IEnvelope boundingEnvelope = this.inewEnvelopeFeedback_0.Stop();
                IEllipticArc ellipticArc = new EllipticArc();
                (ellipticArc as IConstructEllipticArc).ConstructEnvelope(boundingEnvelope);
                this.inewEnvelopeFeedback_0 = null;
                IPolygon polygon = new Polygon() as IPolygon;
                object value = Missing.Value;
                (polygon as ISegmentCollection).AddSegment(ellipticArc as ISegment, ref value, ref value);
                IElement element = new EllipseElement
                {
                    Geometry = polygon
                };
                INewElementOperation operation = new NewElementOperation
                {
                    ActiveView = this._context.ActiveView,
                    Element = element,
                    ContainHook = this.GetActiveView()
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
        }
    }
}