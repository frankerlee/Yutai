using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmHost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHost));
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtHost = new TextEdit();
            this.txtDescription = new MemoEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtHost.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器名:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "描述";
            this.txtHost.EditValue = "";
            this.txtHost.Location = new Point(72, 16);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new Size(144, 21);
            this.txtHost.TabIndex = 2;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(16, 80);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(208, 112);
            this.txtDescription.TabIndex = 3;
            this.btnOK.Location = new Point(88, 216);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(160, 216);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(234, 252);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtHost);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHost";
            this.Text = "添加机器";
            base.Load += new EventHandler(this.frmHost_Load);
            this.txtHost.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private Label label1;
        private Label label2;
        private SimpleButton simpleButton2;
        private MemoEdit txtDescription;
        private TextEdit txtHost;
    }
}