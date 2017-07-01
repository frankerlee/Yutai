using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmNewHatchClassName : Form
    {
        private Container container_0 = null;
        private string string_0 = "";

        public frmNewHatchClassName()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.string_0 = this.txtHatchClassName.Text;
        }

        public string HatchClassName
        {
            get { return this.string_0; }
        }
    }
}