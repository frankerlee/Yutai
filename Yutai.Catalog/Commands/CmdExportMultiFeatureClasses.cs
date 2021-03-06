﻿using System;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdExportMultiFeatureClasses : YutaiCommand
    {
        public CmdExportMultiFeatureClasses(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "导出多个要素类";
            this.m_category = "Catalog";
            this.m_message = "导出多个要素类";
            this.m_name = "Catalog_ExportMultiFeatureClasses";
            this._key = "Catalog_ExportMultiFeatureClasses";
            this.m_toolTip = "导出多个要素类";
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
            frmMultiDataConvert _frmMultiDataConvert = new frmMultiDataConvert()
            {
                InGxObject = ((IGxSelection) _context.GxSelection).FirstObject
            };
            _frmMultiDataConvert.ShowDialog();
        }
    }
}