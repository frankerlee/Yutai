using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmNewVersion : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private NewVersionControl newVersionControl_0 = new NewVersionControl();
        private Panel panel1;
        private Panel panel2;

        public frmNewVersion()
        {
            this.InitializeComponent();
            this.panel1.Controls.Add(this.newVersionControl_0);
            this.newVersionControl_0.Dock = DockStyle.Fill;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.newVersionControl_0.CreateVersion();
            base.Close();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmNewVersion_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(240, 0x13b);
            this.panel1.TabIndex = 2;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x13f);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(240, 0x20);
            this.panel2.TabIndex = 3;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xa8, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x58, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(240, 0x15f);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewVersion";
            this.Text = "新建版本";
            base.Load += new EventHandler(this.frmNewVersion_Load);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IArray ParentVersions
        {
            set
            {
                this.newVersionControl_0.ParentVersions = value;
            }
        }
    }
}

