using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmGDBInfo
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.domainControl_0.Dispose();
                this.gdbgeneralCtrl_0.Dispose();
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGDBInfo));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(376, 480);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Location = new Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(368, 455);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage2.Location = new Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new Size(368, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "域";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(144, 504);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(72, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(232, 504);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnApply.Location = new Point(312, 504);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(64, 24);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(392, 530);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            
            base.Name = "frmGDBInfo";
            this.Text = "数据库属性";
            base.Load += new EventHandler(this.frmGDBInfo_Load);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}