using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolPagePan : YutaiTool
    {
        private bool bool_0 = false;

        private Cursor cursor_0;

        private Cursor cursor_1;

        public override bool Enabled
        {
            get { return (!(this._context.ActiveView is IPageLayout) ? false : true); }
        }

        public override void OnCreate(object hook)
        {
            this.cursor_0 =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.PageHand.cur"));
            this.cursor_1 =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.PageMoveHand.cur"));
            this.m_cursor = this.cursor_0;
            this.m_bitmap = Properties.Resources.PageHand;
            this.m_name = "PagePan";
            this.m_caption = "";
            this.m_toolTip = "页面移动";
            this.m_category = "页面操作";
            base.m_name = "Printing_PagePan";
            _key = "Printing_PagePan";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolPagePan(IAppContext context)
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

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 != 2)
            {
                this.bool_0 = true;
                this.m_cursor = this.cursor_1;
                IPoint mapPoint = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this._context.ActiveView.ScreenDisplay.PanStart(mapPoint);
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_0)
            {
                IPoint mapPoint = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this._context.ActiveView.ScreenDisplay.PanMoveTo(mapPoint);
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.m_cursor = this.cursor_0;
                IActiveView activeView = this._context.ActiveView;
                activeView.Extent = activeView.ScreenDisplay.PanStop();
                activeView.Refresh();
            }
        }
    }
}