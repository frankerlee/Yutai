using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Yutai.UI.Dialogs
{
    partial class frmExportData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboExportData = new System.Windows.Forms.ComboBox();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.txtOutName = new System.Windows.Forms.TextBox();
            this.btnOutName = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoSRType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "导出";
            // 
            // cboExportData
            // 
            this.cboExportData.Items.AddRange(new object[] {
            "所有要素",
            "选中要素",
            "在视图范围内的要素"});
            this.cboExportData.Location = new System.Drawing.Point(57, 4);
            this.cboExportData.Name = "cboExportData";
            this.cboExportData.Size = new System.Drawing.Size(175, 20);
            this.cboExportData.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(128, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "输出shape文件或要素类";
            // 
            // txtOutName
            // 
            this.txtOutName.Location = new System.Drawing.Point(12, 72);
            this.txtOutName.Name = "txtOutName";
            this.txtOutName.Size = new System.Drawing.Size(238, 21);
            this.txtOutName.TabIndex = 5;
            // 
            // btnOutName
            // 
            this.btnOutName.Location = new System.Drawing.Point(256, 69);
            this.btnOutName.Name = "btnOutName";
            this.btnOutName.Size = new System.Drawing.Size(24, 24);
            this.btnOutName.TabIndex = 7;
            this.btnOutName.Text = "...";
            this.btnOutName.Click += new System.EventHandler(this.btnOutName_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(115, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(208, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "数据坐标系:";
            // 
            // rdoSRType
            // 
            this.rdoSRType.Items.AddRange(new object[] {
            "图层数据源",
            "数据框"});
            this.rdoSRType.Location = new System.Drawing.Point(77, 30);
            this.rdoSRType.Name = "rdoSRType";
            this.rdoSRType.Size = new System.Drawing.Size(155, 20);
            this.rdoSRType.TabIndex = 11;
            // 
            // frmExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 156);
            this.Controls.Add(this.rdoSRType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOutName);
            this.Controls.Add(this.txtOutName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboExportData);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportData";
            this.Text = "导出数据";
            this.Load += new System.EventHandler(this.frmExportData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnCancel;
        private Button btnOK;
        private Button btnOutName;
        private ComboBox cboExportData;
        private Label label1;
        private Label labelControl1;
        private TextBox txtOutName;
        private Label label2;
        private ComboBox rdoSRType;
    }
}