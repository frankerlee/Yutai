using System;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdExportSingleToGDB : YutaiCommand
    {
        public CmdExportSingleToGDB(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "到Geodatabase";
            this.m_category = "Catalog";
            this.m_message = "导出数据到Geodatabase";
            this.m_name = "Catalog_ExportSingleToGDB";
            this._key = "Catalog_ExportSingleToGDB";
            this.m_toolTip = "导出数据到Geodatabase";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Text;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            new frmDataConvert
            {
                Text = "要素类 To 要素类",
                InGxObject = ((IGxSelection) _context.GxSelection).FirstObject
            }.ShowDialog();
        }
    }
}