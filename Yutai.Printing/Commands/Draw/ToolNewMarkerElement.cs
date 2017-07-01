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
    public class ToolNewMarkerElement : YutaiTool
    {
        private INewLineFeedback inewLineFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.ActiveView != null; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "点";
            this.m_message = "新建点";
            this.m_toolTip = "新建点";
            this.m_category = "制图";

            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
            base.m_bitmap = Properties.Resources.icon_marker;
            base.m_name = "Printing_NewMarkerElement";
            _key = "Printing_NewMarkerElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public ToolNewMarkerElement(IAppContext context)
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
            IPoint geometry = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IElement element = new MarkerElement();
            element.Geometry = geometry;
            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbol();
            (element as IMarkerElement).Symbol = symbol;
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

        private IActiveView GetActiveView()
        {
            IActiveView result = null;
            if (_context.MainView.ControlType == GISControlType.PageLayout)
                result = ((_context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView);

            return result;
        }
    }
}