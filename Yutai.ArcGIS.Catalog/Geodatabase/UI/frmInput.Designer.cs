using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInput));
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 0;
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(8, 40);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(232, 21);
            this.textEdit1.TabIndex = 1;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Location = new Point(88, 80);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(64, 24);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(160, 80);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(64, 24);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(266, 127);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmInput";
            base.Load += new EventHandler(this.frmInput_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label label1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;
    }
}