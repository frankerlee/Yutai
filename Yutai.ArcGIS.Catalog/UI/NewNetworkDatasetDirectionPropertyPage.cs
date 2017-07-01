using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class NewNetworkDatasetDirectionPropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public NewNetworkDatasetDirectionPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
        }

        private void btnDirections_Click(object sender, EventArgs e)
        {
        }

        private void rdoTrue_CheckedChanged(object sender, EventArgs e)
        {
            this.btnDirections.Enabled = this.rdoTrue.Checked;
        }
    }
}