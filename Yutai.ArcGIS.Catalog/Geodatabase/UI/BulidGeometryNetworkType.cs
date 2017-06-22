using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGeometryNetworkType : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;

        public BulidGeometryNetworkType()
        {
            this.InitializeComponent();
        }

        private void BulidGeometryNetworkType_Load(object sender, EventArgs e)
        {
            if (BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty)
            {
                this.rdoCreateType.SelectedIndex = 1;
            }
            else
            {
                this.rdoCreateType.SelectedIndex = 0;
            }
            this.bool_0 = true;
        }

 private void rdoCreateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoCreateType.SelectedIndex == 1)
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty = true;
                }
                else
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty = false;
                }
            }
        }
    }
}

