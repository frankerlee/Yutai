using System;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Catalog.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdRasterToGDB : YutaiCommand
    {
        public CmdRasterToGDB(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "栅格";
            this.m_category = "Catalog";
            this.m_message = "导入栅格数据集";
            this.m_name = "Catalog_RasterToGDB";
            this._key = "Catalog_RasterToGDB";
            this.m_toolTip = "导入栅格数据集";
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
            frmRasterLoad _frmRasterLoad = new frmRasterLoad()
            {
                OutGxObject = ((IGxSelection) _context.GxSelection).FirstObject
            };
            _frmRasterLoad.ShowDialog();
            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
        }
    }
}