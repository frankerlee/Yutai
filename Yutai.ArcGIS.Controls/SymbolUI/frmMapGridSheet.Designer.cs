using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class frmMapGridSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapGridSheet));
            this.panel1 = new Panel();
            this.xtraTabControl1 = new XtraTabControl();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 261);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(400, 48);
            this.panel1.TabIndex = 0;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(400, 261);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(400, 309);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmMapGridSheet";
            this.Text = "参考系";
            base.Load += new EventHandler(this.frmMapGridSheet_Load);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }
    
        private Container components = null;
        private Panel panel1;
        private XtraTabControl xtraTabControl1;
    }
}