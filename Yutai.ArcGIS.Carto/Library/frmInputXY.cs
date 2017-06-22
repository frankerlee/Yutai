using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmInputXY : Form
    {
        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private IContainer icontainer_0 = null;

        public frmInputXY()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.double_0 = double.Parse(this.txtX.Text);
                this.double_1 = double.Parse(this.txtY.Text);
                base.DialogResult = DialogResult.OK;
            }
            catch
            {
            }
        }

 private void frmInputXY_Load(object sender, EventArgs e)
        {
            this.txtX.Text = this.double_0.ToString();
            this.txtY.Text = this.double_1.ToString();
        }

 public double X
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        public double Y
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }
    }
}

