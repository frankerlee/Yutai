using System.ComponentModel;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Historical
{
    internal class frmChangeVersions : Form
    {
        private IContainer components = null;

        public frmChangeVersions()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "frmChangeVersions";
        }
    }
}