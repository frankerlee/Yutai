using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmAGSProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAGSProperty));
            this.panel1 = new Panel();
            this.xtraTabControl1 = new XtraTabControl();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 282);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(480, 48);
            this.panel1.TabIndex = 0;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(480, 282);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(480, 330);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmAGSProperty";
            this.Text = "frmAGSProperty";
            base.Load += new EventHandler(this.frmAGSProperty_Load);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }

       
        private Panel panel1;
        private XtraTabControl xtraTabControl1;
    }
}