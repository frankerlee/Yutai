using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class frmEditMultiAttribute : Form
    {
        private Container components = null;

        public frmEditMultiAttribute()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditMultiAttribute));
            base.SuspendLayout();
            base.ClientSize = new Size(0x124, 0x111);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmEditMultiAttribute";
            this.Text = "frmEditMultiAttribute";
            base.ResumeLayout(false);
        }
    }
}

