using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_Summary : UserControl
    {
        private Container container_0 = null;

        public BulidGN_Summary()
        {
            this.InitializeComponent();
        }

        private void BulidGN_Summary_Load(object sender, EventArgs e)
        {
            this.memoEdit1.Text = BulidGeometryNetworkHelper.BulidGNHelper.Summary();
        }
    }
}