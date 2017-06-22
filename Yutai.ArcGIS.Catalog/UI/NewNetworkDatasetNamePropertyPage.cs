using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class NewNetworkDatasetNamePropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public NewNetworkDatasetNamePropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.txtNetworkDatasetName.Text.Trim().Length == 0)
            {
                return false;
            }
            if (NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset == null)
            {
                return false;
            }
            if (NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset.Subsets == null)
            {
                return false;
            }
            IEnumDataset subsets = NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset.Subsets;
            subsets.Reset();
            if (subsets.Next() == null)
            {
                return false;
            }
            NewNetworkDatasetHelper.NewNetworkDataset.NetworkDatasetName = this.txtNetworkDatasetName.Text.Trim();
            return true;
        }

 private void NewNetworkDatasetNamePropertyPage_Load(object sender, EventArgs e)
        {
            this.txtNetworkDatasetName.Text = NewNetworkDatasetHelper.NewNetworkDataset.NetworkDatasetName;
        }
    }
}

