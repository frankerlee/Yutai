using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmAttributeEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeEdit));
            base.SuspendLayout();
            base.ClientSize = new Size(292, 273);
            
            base.Name = "frmAttributeEdit";
            base.ResumeLayout(false);
        }

       
        private Container components = null;
    }
}