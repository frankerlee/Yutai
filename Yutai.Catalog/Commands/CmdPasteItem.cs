using System;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdPasteItem : YutaiCommand
    {
        public CmdPasteItem(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_paste;
            this.m_caption = "粘贴";
            this.m_category = "Catalog";
            this.m_message = "粘贴";
            this.m_name = "Catalog_Paste";
            this._key = "Catalog_Paste";
            this.m_toolTip = "粘贴";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return CmdCopyItem.m_GxObjectContainer != null && _context.GxSelection != null; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IGxPasteTarget gxPasteTarget = ((IGxSelection) _context.GxSelection).FirstObject as IGxPasteTarget;
            if (gxPasteTarget != null)
            {
                bool flag = false;
                IEnumGxObject enumGxObject = CmdCopyItem.m_GxObjectContainer as IEnumGxObject;
                enumGxObject.Reset();
                IGxObject gxObject = enumGxObject.Next();
                IEnumNameEdit enumNameEdit = new NamesEnumerator() as IEnumNameEdit;
                while (gxObject != null)
                {
                    enumNameEdit.Add(gxObject.InternalObjectName);
                    gxObject = enumGxObject.Next();
                }
                if (gxPasteTarget.CanPaste(enumNameEdit as IEnumName, ref flag))
                {
                    flag = false;
                    gxPasteTarget.Paste(enumNameEdit as IEnumName, ref flag);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("无法粘贴打指定位置！");
                }
            }
        }
    }
}