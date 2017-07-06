using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolNewPolygonElement : YutaiTool
    {
        private INewPolygonFeedback inewPolygonFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.ActiveView != null; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "新建多边形";
            this.m_toolTip = "新建多边形";
            this.m_category = "Printing";
            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
            base.m_bitmap = Properties.Resources.icon_polygon;
            base.m_name = "Printing_NewPolygon";
            _key = "Printing_NewPolygon";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolNewPolygonElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        private IActiveView method_0()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);

            return result;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            IActiveView activeView = this._context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (this.inewPolygonFeedback_0 == null)
            {
                this.inewPolygonFeedback_0 = new NewPolygonFeedback();
                this.inewPolygonFeedback_0.Display = activeView.ScreenDisplay;
                this.inewPolygonFeedback_0.Start(point);
            }
            else
            {
                this.inewPolygonFeedback_0.AddPoint(point);
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this.inewPolygonFeedback_0 != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.inewPolygonFeedback_0.MoveTo(point);
            }
        }

        public override void OnKeyDown(int button, int shift)
        {
            if (button == 27)
            {
                this.inewPolygonFeedback_0 = null;
                this._context.ActiveView.Refresh();
            }
        }

        public override void OnDblClick()
        {
            if (this.inewPolygonFeedback_0 != null)
            {
                IPolygon geometry = this.inewPolygonFeedback_0.Stop();
                this.inewPolygonFeedback_0 = null;
                IElement element = new PolygonElement
                {
                    Geometry = geometry
                };
                INewElementOperation operation = new NewElementOperation
                {
                    ActiveView = this._context.ActiveView,
                    Element = element,
                    ContainHook = this.method_0()
                };
                this._context.OperationStack.Do(operation);
                //if (this._context.Hook is IApplication)
                //{
                //	if ((this._context.Hook as IApplication).ContainerHook != null)
                //	{
                //		DocumentManager.DocumentChanged((this._context.Hook as IApplication).ContainerHook);
                //	}
                //	else
                //	{
                //		DocumentManager.DocumentChanged((this._context.Hook as IApplication).Hook);
                //	}
                //}
                //else
                //{
                //	DocumentManager.DocumentChanged(this._context.Hook);
                //}
            }
        }
    }
}