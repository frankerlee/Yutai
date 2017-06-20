using System;
using Yutai.Plugins.Catalog.Menu;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdShowCatalogView : YutaiCommand
    {
        private CatalogViewService _dockService;
        public CmdShowCatalogView(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog;
            this.m_caption = "目录管理";
            this.m_category = "Catalog";
            this.m_message = "目录管理";
            this.m_name = "Catalog_OpenView";
            this._key = "Catalog_OpenView";
            this.m_toolTip = "目录管理";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (_dockService == null)
            {
                _dockService = _context.Container.GetInstance<CatalogViewService>();

            }
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                return;
            }
        }
    }
}