using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmHost : Form
    {
        private Container container_0 = null;
        private string string_0 = "";
        private string string_1 = "";

        public frmHost()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtHost.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入主机名!");
            }
            else
            {
                this.string_0 = this.txtHost.Text.Trim();
                this.string_1 = this.txtDescription.Text;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void frmHost_Load(object sender, EventArgs e)
        {
            if (!(this.string_0 == ""))
            {
                this.txtHost.Text = this.string_0;
                this.txtHost.Enabled = false;
            }
            this.txtDescription.Text = this.string_1;
        }

        public string Description
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public string HostName
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}