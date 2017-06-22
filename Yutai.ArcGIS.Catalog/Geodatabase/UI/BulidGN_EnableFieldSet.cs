using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_EnableFieldSet : UserControl
    {
        private Container container_0 = null;

        public BulidGN_EnableFieldSet()
        {
            this.InitializeComponent();
        }

        private void BulidGN_EnableFieldSet_Load(object sender, EventArgs e)
        {
        }

 private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BulidGeometryNetworkHelper.BulidGNHelper.PreserveEnabledValues = this.radioGroup1.SelectedIndex == 1;
        }
    }
}

