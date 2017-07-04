using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateRasterDataset : YutaiCommand
    {
        public CmdCreateRasterDataset(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "新建栅格数据集";
            this.m_category = "Catalog";
            this.m_message = "新建栅格数据集";
            this.m_name = "Catalog_NewRasterDataset";
            this._key = "Catalog_NewRasterDataset";
            this.m_toolTip = "新建栅格数据集";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (_context.GxSelection != null ? ((IGxSelection) _context.GxSelection).FirstObject != null : false);
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace != null)
                {
                    frmCreateGDBRasterDataset _frmCreateGDBRasterDataset = new frmCreateGDBRasterDataset()
                    {
                        OutLocation = ((IGxSelection) _context.GxSelection).FirstObject
                    };
                    try
                    {
                        if (_frmCreateGDBRasterDataset.ShowDialog() == DialogResult.OK)
                        {
                            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                        }
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                }
            }
        }
    }
}