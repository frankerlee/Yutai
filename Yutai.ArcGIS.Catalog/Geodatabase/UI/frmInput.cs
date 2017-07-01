using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmInput : Form
    {
        private Container container_0 = null;
        private string string_0 = "";
        private string string_1 = "";

        public frmInput(string string_2, string string_3)
        {
            this.InitializeComponent();
            this.string_1 = string_3;
            this.string_0 = string_2;
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            this.textEdit1.Text = this.string_1;
            this.label1.Text = this.string_0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.string_1 = this.textEdit1.Text;
        }

        public string InputValue
        {
            get { return this.string_1; }
        }
    }
}