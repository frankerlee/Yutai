using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmExportToCAD : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private ExportToCADControl exportToCADControl1;
        private Panel panel1;
        private Panel panel2;

        public frmExportToCAD()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.exportToCADControl1.CanDo())
            {
                this.exportToCADControl1.Do();
                base.Close();
            }
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportToCAD));
            this.panel2 = new Panel();
            this.exportToCADControl1 = new ExportToCADControl();
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.exportToCADControl1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x132, 0xfb);
            this.panel2.TabIndex = 3;
            this.exportToCADControl1.Dock = DockStyle.Fill;
            this.exportToCADControl1.Location = new Point(0, 0);
            this.exportToCADControl1.Name = "exportToCADControl1";
            this.exportToCADControl1.Size = new Size(0x132, 0xfb);
            this.exportToCADControl1.TabIndex = 0;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0xfb);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x132, 0x1c);
            this.panel1.TabIndex = 2;
            this.btnOK.Location = new Point(0x98, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe8, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x132, 0x117);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExportToCAD";
            this.Text = "导出到CAD";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}

