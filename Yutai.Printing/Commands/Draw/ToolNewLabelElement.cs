using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolNewLabelElement : YutaiTool
    {
        private INewLineFeedback inewLineFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.ActiveView != null; }
        }


        public override void OnCreate(object hook)
        {
            this.m_caption = "标签";
            this.m_message = "新建Label";
            this.m_toolTip = "新建Label";
            this.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_label;
            base.m_name = "Printing_NewLableElement";
            _key = "Printing_NewLableElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolNewLabelElement(IAppContext context)
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
            if (this.inewLineFeedback_0 == null)
            {
                this.inewLineFeedback_0 = new NewLineFeedback();
                this.inewLineFeedback_0.Display = activeView.ScreenDisplay;
                this.inewLineFeedback_0.Start(point);
            }
            else
            {
                this.inewLineFeedback_0.AddPoint(point);
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this.inewLineFeedback_0 != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.inewLineFeedback_0.MoveTo(point);
            }
        }

        private IActiveView GetActiveView()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);

            return result;
        }

        public override void OnDblClick()
        {
            if (this.inewLineFeedback_0 != null)
            {
                IPolyline polyline = this.inewLineFeedback_0.Stop();
                this.inewLineFeedback_0 = null;
                if (!polyline.IsEmpty)
                {
                    IElement element = new LineElement();
                    element.Geometry = polyline;
                    INewElementOperation newElementOperation = new NewElementOperation();
                    newElementOperation.ActiveView = this._context.ActiveView;
                    newElementOperation.ContainHook = this.GetActiveView();
                    newElementOperation.Element = element;
                    this._context.OperationStack.Do(newElementOperation);
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

        public override void OnKeyDown(int button, int shift)
        {
            if (button == 27)
            {
                this.inewLineFeedback_0 = null;
                this._context.ActiveView.Refresh();
            }
        }
    }
}