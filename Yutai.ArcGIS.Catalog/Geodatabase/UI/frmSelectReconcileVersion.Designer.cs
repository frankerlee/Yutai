using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmSelectReconcileVersion
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
            this.VersionListBox = new ListBoxControl();
            this.btnOK = new SimpleButton();
            this.btnCancle = new SimpleButton();
            ((ISupportInitialize) this.VersionListBox).BeginInit();
            base.SuspendLayout();
            this.VersionListBox.ItemHeight = 15;
            this.VersionListBox.Location = new Point(8, 8);
            this.VersionListBox.Name = "VersionListBox";
            this.VersionListBox.Size = new Size(184, 144);
            this.VersionListBox.TabIndex = 0;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(64, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new Point(136, 160);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(56, 24);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(200, 189);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.VersionListBox);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectReconcileVersion";
            this.Text = "选择协调版本";
            base.Load += new EventHandler(this.frmSelectReconcileVersion_Load);
            ((ISupportInitialize) this.VersionListBox).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancle;
        private SimpleButton btnOK;
        private IVersion iversion_0;
        private ListBoxControl VersionListBox;
    }
}