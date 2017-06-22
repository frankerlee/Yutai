using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmNewJLKCodeDomain
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.txtCode = new TextEdit();
            this.label1 = new Label();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.label3 = new Label();
            this.cboNameField = new System.Windows.Forms.ComboBox();
            this.cboValueField = new System.Windows.Forms.ComboBox();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtName.Properties.BeginInit();
            this.txtCode.Properties.BeginInit();
            base.SuspendLayout();
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(71, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(165, 21);
            this.txtName.TabIndex = 17;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "描  述";
            this.txtCode.EditValue = "";
            this.txtCode.Location = new Point(71, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(165, 21);
            this.txtCode.TabIndex = 15;
            this.txtCode.EditValueChanged += new EventHandler(this.txtCode_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "代  码";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(165, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(95, 147);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "代码表";
            this.cboNameField.FormattingEnabled = true;
            this.cboNameField.Location = new Point(71, 90);
            this.cboNameField.Name = "cboNameField";
            this.cboNameField.Size = new Size(165, 20);
            this.cboNameField.TabIndex = 20;
            this.cboValueField.FormattingEnabled = true;
            this.cboValueField.Location = new Point(71, 121);
            this.cboValueField.Name = "cboValueField";
            this.cboValueField.Size = new Size(165, 20);
            this.cboValueField.TabIndex = 21;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new Point(71, 64);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new Size(165, 20);
            this.cboTable.TabIndex = 22;
            this.cboTable.SelectedIndexChanged += new EventHandler(this.cboTable_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new Size(53, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "名称字段";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 124);
            this.label5.Name = "label5";
            this.label5.Size = new Size(41, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "值字段";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(257, 195);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboTable);
            base.Controls.Add(this.cboValueField);
            base.Controls.Add(this.cboNameField);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtCode);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "frmNewJLKCodeDomain";
            this.Text = "扩展域值";
            base.Load += new EventHandler(this.frmNewJLKCodeDomain_Load);
            this.txtName.Properties.EndInit();
            this.txtCode.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private System.Windows.Forms.ComboBox cboNameField;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.ComboBox cboValueField;
        private IWorkspace iworkspace_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextEdit txtCode;
        private TextEdit txtName;
    }
}