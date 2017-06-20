using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdDataLoader : YutaiCommand
    {
        public CmdDataLoader(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "装载数据";
            this.m_category = "Catalog";
            this.m_message = "装载数据";
            this.m_name = "Catalog_DataLoader";
            this._key = "Catalog_DataLoader";
            this.m_toolTip = "装载数据";
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
                else if (((IGxSelection) _context.GxSelection).Count == 0)
                {
                    result = false;
                }
                else
                {
                    IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
                    if (firstObject == null)
                    {
                        result = false;
                    }
                    else
                    {
                        if (firstObject is IGxDataset)
                        {
                            esriDatasetType type = (firstObject as IGxDataset).Type;
                            if (type == esriDatasetType.esriDTFeatureClass || type == esriDatasetType.esriDTTable)
                            {
                                result = true;
                                return result;
                            }
                        }
                        result = false;
                    }
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
            new frmDataLoader
            {
                Table = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset
            }.ShowDialog();
        }
    }
}