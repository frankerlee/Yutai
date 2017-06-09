using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmVersionProperty : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private Panel panel1;
        private Panel panel2;
        private VersionPropertyCtrl versionPropertyCtrl_0 = new VersionPropertyCtrl();

        public frmVersionProperty()
        {
            this.InitializeComponent();
            base.StartPosition = FormStartPosition.CenterParent;
            this.panel1.Controls.Add(this.versionPropertyCtrl_0);
            this.versionPropertyCtrl_0.Dock = DockStyle.Fill;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.versionPropertyCtrl_0.Apply();
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
            this.panel1.Size = new Size(240, 0x177);
            this.panel1.TabIndex = 2;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x177);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(240, 0x1b);
            this.panel2.TabIndex = 3;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xa8, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x60, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(240, 0x192);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmVersionProperty";
            this.Text = "版本属性";
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IVersion Version
        {
            set
            {
                this.versionPropertyCtrl_0.Version = value;
            }
        }
    }
}

