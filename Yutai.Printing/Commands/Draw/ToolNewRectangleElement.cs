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
    public class ToolNewRectangleElement : YutaiTool
    {
        private INewEnvelopeFeedback inewEnvelopeFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.ActiveView != null; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "新建矩形";
            this.m_toolTip = "新建矩形";
            this.m_category = "制图";
            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
            base.m_bitmap = Properties.Resources.icon_rectangle;
            base.m_name = "Printing_NewRectangle";
            _key = "Printing_NewRectangle";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolNewRectangleElement(IAppContext context)
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

        public override void OnMouseDown(int button, int shift, int x, int y)
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

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this.inewEnvelopeFeedback_0 != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.inewEnvelopeFeedback_0.MoveTo(point);
            }
        }

        private IActiveView method_0()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);

            return result;
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (this.inewEnvelopeFeedback_0 != null)
            {
                IEnvelope geometry = this.inewEnvelopeFeedback_0.Stop();
                this.inewEnvelopeFeedback_0 = null;
                IElement element = new RectangleElement
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