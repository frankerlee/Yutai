using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public partial class frmNASheet : Form
    {
        public frmNASheet()
        {
            this.InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ((this.naGeneralPropertyPage1.Apply() && this.naWeightsPropertyPage1.Apply()) &&
                this.naWeightFilterPropertyPage1.Apply())
            {
                base.Close();
            }
        }

        private void frmNASheet_Load(object sender, EventArgs e)
        {
        }
    }
}