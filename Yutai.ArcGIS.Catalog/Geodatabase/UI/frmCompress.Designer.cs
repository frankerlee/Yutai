using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmCompress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompress));
            this.textEdit1 = new TextEdit();
            this.label1 = new Label();
            this.btnOpen = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(8, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.BackColor = SystemColors.Control;
            this.textEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(232, 21);
            this.textEdit1.TabIndex = 0;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(160, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入空间数据库连接";
            this.btnOpen.Image = (Image) resources.GetObject("btnOpen.Image");
            this.btnOpen.Location = new Point(253, 32);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(28, 28);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnOK.Location = new Point(184, 88);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(48, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new Point(240, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(48, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 130);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textEdit1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCompress";
            this.Text = "压缩数据库";
            base.Load += new EventHandler(this.frmCompress_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnOpen;
        private Label label1;
        private TextEdit textEdit1;
    }
}