using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmInputText : Form
    {
        private IContainer icontainer_0 = null;
        private string string_0 = "";

        public frmInputText()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.string_0 = this.textBox1.Text;
        }

        private void frmInputText_Load(object sender, EventArgs e)
        {
            this.label1.Text = this.Text;
            this.textBox1.Text = this.string_0;
        }

        public string InputText
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}