using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmSimpleDataLoader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimpleDataLoader));
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.btnOpen = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择数据";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(16, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(224, 21);
            this.textEdit1.TabIndex = 1;
            this.btnOpen.Image = (Image) resources.GetObject("btnOpen.Image");
            this.btnOpen.Location = new Point(256, 32);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(24, 24);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnOK.Location = new Point(160, 72);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(224, 72);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Text = "取消";
            this.progressBar1.Location = new Point(16, 72);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(136, 16);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 122);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSimpleDataLoader";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "数据装载";
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private SimpleButton btnOpen;
        private Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;
    }
}