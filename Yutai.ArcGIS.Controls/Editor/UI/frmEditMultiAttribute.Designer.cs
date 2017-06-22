using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmEditMultiAttribute
    {
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
            base.ClientSize = new Size(292, 273);
            
            base.Name = "frmEditMultiAttribute";
            this.Text = "frmEditMultiAttribute";
            base.ResumeLayout(false);
        }
    
        private Container components = null;
    }
}