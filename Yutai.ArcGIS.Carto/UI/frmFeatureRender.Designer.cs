using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmFeatureRender
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
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.panel1.Location = new Point(16, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(264, 32);
            this.panel1.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(368, 293);
            base.Controls.Add(this.panel1);
            base.Name = "frmFeatureRender";
            this.Text = "frmFeatureRender";
            base.ResumeLayout(false);
        }
    
        private Panel panel1;
    }
}