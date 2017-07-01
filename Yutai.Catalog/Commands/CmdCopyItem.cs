using System;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCopyItem : YutaiCommand
    {
        public static IGxObjectArray m_GxObjectContainer;

        static CmdCopyItem()
        {
            // 注意: 此类型已标记为 'beforefieldinit'.
            CmdCopyItem.old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            CmdCopyItem.m_GxObjectContainer = null;
        }

        public CmdCopyItem(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_copy;
            this.m_caption = "复制";
            this.m_category = "Catalog";
            this.m_message = "复制";
            this.m_name = "Catalog_Copy";
            this._key = "Catalog_Copy";
            this.m_toolTip = "复制";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return _context.GxSelection != null && ((IGxSelection) _context.GxSelection).Count != 0; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            CmdCopyItem.m_GxObjectContainer = new GxObjectArray();
            IEnumGxObject selectedObjects = ((IGxSelection) _context.GxSelection).SelectedObjects;
            selectedObjects.Reset();
            for (IGxObject gxObject = selectedObjects.Next(); gxObject != null; gxObject = selectedObjects.Next())
            {
                CmdCopyItem.m_GxObjectContainer.Insert(-1, gxObject);
            }
        }
    }
}