using System;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdRefreshItem : YutaiCommand
    {
        public CmdRefreshItem(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "刷新";
            this.m_category = "Catalog";
            this.m_message = "刷新";
            this.m_name = "Catalog_Refresh";
            this._key = "Catalog_Refresh";
            this.m_toolTip = "刷新";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Text;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.GxSelection == null)
                {
                    result = false;
                }
                else
                {
                    IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
                    result = (firstObject != null);
                }
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {

            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
        }
    }
}