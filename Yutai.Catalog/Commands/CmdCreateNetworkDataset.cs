using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.NetworkAnalystTools;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateNetworkDataset : YutaiCommand
    {
        public CmdCreateNetworkDataset(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "新建网络要素集";
            this.m_category = "Catalog";
            this.m_message = "新建网络要素集";
            this.m_name = "Catalog_NewNetworkDataset";
            this._key = "Catalog_NewNetworkDataset";
            this.m_toolTip = "新建网络要素集";
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
                    flag = (!(((IGxSelection) _context.GxSelection).FirstObject is IGxDataset) || (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Type != esriDatasetType.esriDTFeatureDataset ? false : true);
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
            frmNewNetworkDatasetWizard _frmNewNetworkDatasetWizard = new frmNewNetworkDatasetWizard();
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
            {
                _frmNewNetworkDatasetWizard.FeatureDataset = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IFeatureDataset;
            }
            if (_frmNewNetworkDatasetWizard.ShowDialog() == DialogResult.OK)
            {
                if (_frmNewNetworkDatasetWizard.NetworkDataset != null && MessageBox.Show("是否构建网络要素集?", "网络", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        Geoprocessor geoprocessor = new Geoprocessor();
                        BuildNetwork buildNetwork = new BuildNetwork()
                        {
                            in_network_dataset = (_frmNewNetworkDatasetWizard.NetworkDataset as IDataset).FullName
                        };
                        Common.RunTool(geoprocessor, buildNetwork, null);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                }
                ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
            }
        }
    }
}