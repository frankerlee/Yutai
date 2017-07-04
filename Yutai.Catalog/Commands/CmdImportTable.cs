using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdImportTable : YutaiCommand
    {
        public CmdImportTable(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "导入单个表";
            this.m_category = "Catalog";
            this.m_message = "导入单个表";
            this.m_name = "Catalog_ImportSingleTable";
            this._key = "Catalog_ImportSingleTable";
            this.m_toolTip = "导入单个表";
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
            frmDataConvert _frmDataConvert = new frmDataConvert()
            {
                OutGxObject = ((IGxSelection) _context.GxSelection).FirstObject,
                ImportDatasetType = esriDatasetType.esriDTTable
            };
            _frmDataConvert.ShowDialog();
        }
    }
}