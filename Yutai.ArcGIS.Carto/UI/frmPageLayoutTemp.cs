using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmPageLayoutTemp : Form
    {
        internal AxPageLayoutControl axPageLayoutControl1;
        private IContainer icontainer_0 = null;

        public frmPageLayoutTemp()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageLayoutTemp));
            this.axPageLayoutControl1 = new AxPageLayoutControl();
            this.axPageLayoutControl1.BeginInit();
            base.SuspendLayout();
            this.axPageLayoutControl1.Dock = DockStyle.Fill;
            this.axPageLayoutControl1.Location = new Point(0, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl1.OcxState") ;
            this.axPageLayoutControl1.Size = new Size(0x11c, 0x106);
            this.axPageLayoutControl1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.axPageLayoutControl1);
            base.Name = "frmPageLayoutTemp";
            this.Text = "frmPageLayoutTemp";
            this.axPageLayoutControl1.EndInit();
            base.ResumeLayout(false);
        }
    }
}

