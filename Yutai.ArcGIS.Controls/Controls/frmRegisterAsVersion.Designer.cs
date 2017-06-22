using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmRegisterAsVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegisterAsVersion));
            this.checkEdit1 = new CheckEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.checkEdit1.Location = new Point(12, 12);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "是否注册为移动编辑的基版本";
            this.checkEdit1.Size = new Size(226, 19);
            this.checkEdit1.TabIndex = 0;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(41, 37);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(145, 37);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 74);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.checkEdit1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRegisterAsVersion";
            this.Text = "注册为版本";
            base.Load += new EventHandler(this.frmRegisterAsVersion_Load);
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private SimpleButton btnOK;
        private CheckEdit checkEdit1;
        private SimpleButton simpleButton2;
    }
}