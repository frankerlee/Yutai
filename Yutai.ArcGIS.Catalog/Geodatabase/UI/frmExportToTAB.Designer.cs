using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmExportToTAB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportToTAB));
            this.panel2 = new Panel();
            this.exportToTABControl1 = new ExportToTABControl();
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.exportToTABControl1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(306, 251);
            this.panel2.TabIndex = 3;
            this.exportToTABControl1.Dock = DockStyle.Fill;
            this.exportToTABControl1.Location = new Point(0, 0);
            this.exportToTABControl1.Name = "exportToTABControl1";
            this.exportToTABControl1.Size = new Size(306, 251);
            this.exportToTABControl1.TabIndex = 0;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(306, 28);
            this.panel1.TabIndex = 2;
            this.btnOK.Location = new Point(152, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(232, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(306, 279);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExportToTAB";
            this.Text = "导出到Mapinfo";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ExportToTABControl exportToTABControl1;
        private Panel panel1;
        private Panel panel2;
    }
}