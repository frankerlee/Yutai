using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmVersionInfo
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersionInfo));
            base.SuspendLayout();
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(408, 245);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmVersionInfo";
            this.Text = "版本管理器";
            base.ResumeLayout(false);
        }

       
    }
}