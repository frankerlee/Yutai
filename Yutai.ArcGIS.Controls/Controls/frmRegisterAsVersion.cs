using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmRegisterAsVersion : Form
    {
        private bool edit_to_base = false;

        public frmRegisterAsVersion()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.edit_to_base = this.checkEdit1.Checked;
        }

        private void frmRegisterAsVersion_Load(object sender, EventArgs e)
        {
        }

        public bool EditToBase
        {
            get { return this.edit_to_base; }
        }
    }
}