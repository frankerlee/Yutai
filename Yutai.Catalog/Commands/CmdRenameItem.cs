using System;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdRenameItem : YutaiCommand
    {
        public CmdRenameItem(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "重命名";
            this.m_category = "Catalog";
            this.m_message = "重命名";
            this.m_name = "Catalog_Rename";
            this._key = "Catalog_Rename";
            this.m_toolTip = "重命名";
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
                    result = (firstObject != null && firstObject is IGxObjectEdit &&
                              (firstObject as IGxObjectEdit).CanRename());
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
            IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
        }
    }
}