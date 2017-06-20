using System;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdImportSingleFeatureClass : YutaiCommand
    {
        public CmdImportSingleFeatureClass(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "导入单个要素类";
            this.m_category = "Catalog";
            this.m_message = "导入单个要素类";
            this.m_name = "Catalog_ImportSingleFeatureClass";
            this._key = "Catalog_ImportSingleFeatureClass";
            this.m_toolTip = "导入单个要素类";
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
                OutGxObject = ((IGxSelection) _context.GxSelection).FirstObject
            };
            _frmDataConvert.ShowDialog();
        }
    }
}