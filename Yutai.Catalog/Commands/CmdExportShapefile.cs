using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdExportShapefile : YutaiCommand
    {
        public CmdExportShapefile(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "导出到Shapefile";
            this.m_category = "Catalog";
            this.m_message = "导出到Shapefile";
            this.m_name = "Catalog_ExportShapefile";
            this._key = "Catalog_ExportShapefile";
            this.m_toolTip = "导出到Shapefile";
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
            frmDataConvert _frmDataConvert = new frmDataConvert()
            {
                Text = "要素类 To 要素类"
            };
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset && (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureClass)
            {
                _frmDataConvert.InGxObject = ((IGxSelection) _context.GxSelection).FirstObject;
            }
            _frmDataConvert.ShowDialog();
        }
    }
}