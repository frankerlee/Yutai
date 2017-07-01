using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateGeometryNetwork : YutaiCommand
    {
        public CmdCreateGeometryNetwork(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "新建网络";
            this.m_category = "Catalog";
            this.m_message = "新建网络";
            this.m_name = "Catalog_NewNetwork";
            this._key = "Catalog_NewNetwork";
            this.m_toolTip = "新建网络";
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
                bool flag;
                if (_context.GxSelection != null)
                {
                    flag = (!(((IGxSelection) _context.GxSelection).FirstObject is IGxDataset) ||
                            (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Type !=
                            esriDatasetType.esriDTFeatureDataset
                        ? false
                        : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmNewGN _frmNewGN = new frmNewGN();
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
            {
                _frmNewGN.FeatureDataset =
                    (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IFeatureDataset;
            }
            if (_frmNewGN.ShowDialog() == DialogResult.OK)
            {
                ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
            }
        }
    }
}