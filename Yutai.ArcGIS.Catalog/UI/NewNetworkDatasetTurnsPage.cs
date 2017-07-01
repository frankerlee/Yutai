using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class NewNetworkDatasetTurnsPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public NewNetworkDatasetTurnsPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            NewNetworkDatasetHelper.NewNetworkDataset.SupportsTurns = this.rdoTrue.Checked;
            return true;
        }

        private void NewNetworkDatasetTurnsPage_Load(object sender, EventArgs e)
        {
            this.checkedListBox1.SetItemChecked(0, true);
        }

        private void rdoFalse_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}