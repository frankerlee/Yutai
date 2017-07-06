using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Printing.Forms
{
    internal partial class frmInputText : System.Windows.Forms.Form
    {
        public TextEdit txtText;

        public string Label { set { this.label1.Text = value; } }
        public string Title { set { this.Text = value; } }

        private Container container_0 = null;

        public frmInputText()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }
    }
}