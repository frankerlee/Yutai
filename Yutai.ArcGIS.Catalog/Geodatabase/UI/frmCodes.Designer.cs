using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmCodes
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.txtCode = new TextEdit();
            this.label1 = new Label();
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.txtCode.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.Location = new Point(82, 59);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(152, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.txtCode.EditValue = "";
            this.txtCode.Location = new Point(47, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(165, 21);
            this.txtCode.TabIndex = 9;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "代码";
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(47, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(165, 21);
            this.txtName.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "描述";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(236, 98);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtCode);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCodes";
            base.Load += new EventHandler(this.frmCodes_Load);
            this.txtCode.Properties.EndInit();
            this.txtName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Label label1;
        private Label label2;
        private TextEdit txtCode;
        private TextEdit txtName;
    }
}