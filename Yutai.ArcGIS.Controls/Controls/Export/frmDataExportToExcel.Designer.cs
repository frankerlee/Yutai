using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class frmDataExportToExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataExportToExcel));
            this.groupBox1 = new GroupBox();
            this.txtRowCount = new TextEdit();
            this.label4 = new Label();
            this.txtRowStartIndex = new TextEdit();
            this.label3 = new Label();
            this.btnOpenFile = new SimpleButton();
            this.txtFileName = new TextEdit();
            this.label2 = new Label();
            this.txtTitle = new TextEdit();
            this.chkUseTemplate = new CheckEdit();
            this.label1 = new Label();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtRowCount.Properties.BeginInit();
            this.txtRowStartIndex.Properties.BeginInit();
            this.txtFileName.Properties.BeginInit();
            this.txtTitle.Properties.BeginInit();
            this.chkUseTemplate.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtRowCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRowStartIndex);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new Point(16, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(288, 128);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Excel模板设置";
            this.txtRowCount.EditValue = "1";
            this.txtRowCount.Location = new Point(72, 88);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new Size(168, 21);
            this.txtRowCount.TabIndex = 9;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 91);
            this.label4.Name = "label4";
            this.label4.Size = new Size(59, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "行    数:";
            this.txtRowStartIndex.EditValue = "0";
            this.txtRowStartIndex.Location = new Point(72, 56);
            this.txtRowStartIndex.Name = "txtRowStartIndex";
            this.txtRowStartIndex.Size = new Size(168, 21);
            this.txtRowStartIndex.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "起始行号:";
            this.btnOpenFile.Location = new Point(248, 24);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new Size(24, 24);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.Click += new EventHandler(this.btnOpenFile_Click);
            this.txtFileName.EditValue = "";
            this.txtFileName.Location = new Point(72, 24);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.ReadOnly = true;
            this.txtFileName.Size = new Size(168, 21);
            this.txtFileName.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 26);
            this.label2.Name = "label2";
            this.label2.Size = new Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "模板名称:";
            this.txtTitle.EditValue = "";
            this.txtTitle.Location = new Point(56, 8);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(216, 21);
            this.txtTitle.TabIndex = 6;
            this.chkUseTemplate.Location = new Point(8, 40);
            this.chkUseTemplate.Name = "chkUseTemplate";
            this.chkUseTemplate.Properties.Caption = "使用Excel模板";
            this.chkUseTemplate.Size = new Size(112, 19);
            this.chkUseTemplate.TabIndex = 5;
            this.chkUseTemplate.CheckedChanged += new EventHandler(this.chkUseTemplate_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "标题:";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(232, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(160, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(328, 229);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtTitle);
            base.Controls.Add(this.chkUseTemplate);
            base.Controls.Add(this.label1);
          
            base.Name = "frmDataExportToExcel";
            this.Text = "导出到Excel";
            base.Load += new EventHandler(this.frmDataExportToExcel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtRowCount.Properties.EndInit();
            this.txtRowStartIndex.Properties.EndInit();
            this.txtFileName.Properties.EndInit();
            this.txtTitle.Properties.EndInit();
            this.chkUseTemplate.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnOpenFile;
        private CheckEdit chkUseTemplate;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtFileName;
        private TextEdit txtRowCount;
        private TextEdit txtRowStartIndex;
        private TextEdit txtTitle;
    }
}