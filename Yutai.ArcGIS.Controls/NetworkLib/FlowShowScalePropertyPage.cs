using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal partial class FlowShowScalePropertyPage : UserControl
    {
        public FlowShowScalePropertyPage()
        {
            this.InitializeComponent();
        }

        private void rdoNoScale_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMaxScale.Enabled = false;
            this.txtMinScale.Enabled = false;
        }

        private void rdoScaleSet_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMaxScale.Enabled = true;
            this.txtMinScale.Enabled = true;
        }

        private void txtMaxScale_TextChanged(object sender, EventArgs e)
        {
        }
    }
}