using ESRI.ArcGIS.Carto;
using System;
using System.Drawing;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdPageZoomToPercent100 : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_name = "PageZoomToPercent100";
            this.m_caption = "100%";
            this.m_toolTip = "缩放到100%";
            this.m_category = "页面操作";
            base.m_bitmap = Properties.Resources.PageZoomToPercent100;
            base.m_name = "Printing_FullPage";
            _key = "Printing_FullPage";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdPageZoomToPercent100(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            ((IPageLayout) this._context.ActiveView).ZoomToPercent(100);
        }
    }
}