using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public class frmDataExportToExcel : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnOpenFile;
        private CheckEdit chkUseTemplate;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private System.Data.DataTable m_dt = null;
        private TextEdit txtFileName;
        private TextEdit txtRowCount;
        private TextEdit txtRowStartIndex;
        private TextEdit txtTitle;

        public frmDataExportToExcel()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.chkUseTemplate.Checked && (this.txtFileName.Text.Length == 0))
            {
                MessageBox.Show("请选择模板文件!");
            }
            else
            {
                if (this.m_dt != null)
                {
                    ExportToExcelHelper.ExcelHelper.Export(this.m_dt);
                }
                base.Close();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Excel工作簿|*.xls|模板|*.xlt",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = dialog.FileName;
                ExportToExcelHelper.ExcelHelper.TempleteFile = dialog.FileName;
            }
        }

        private void chkUseTemplate_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = this.chkUseTemplate.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmDataExportToExcel_Load(object sender, EventArgs e)
        {
            if (ExportToExcelHelper.ExcelHelper == null)
            {
                ExportToExcelHelper.Init();
            }
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
            this.groupBox1.Location = new Point(0x10, 0x40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x120, 0x80);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Excel模板设置";
            this.txtRowCount.EditValue = "1";
            this.txtRowCount.Location = new Point(0x48, 0x58);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new Size(0xa8, 0x15);
            this.txtRowCount.TabIndex = 9;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x5b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "行    数:";
            this.txtRowStartIndex.EditValue = "0";
            this.txtRowStartIndex.Location = new Point(0x48, 0x38);
            this.txtRowStartIndex.Name = "txtRowStartIndex";
            this.txtRowStartIndex.Size = new Size(0xa8, 0x15);
            this.txtRowStartIndex.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "起始行号:";
            this.btnOpenFile.Location = new Point(0xf8, 0x18);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new Size(0x18, 0x18);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.Click += new EventHandler(this.btnOpenFile_Click);
            this.txtFileName.EditValue = "";
            this.txtFileName.Location = new Point(0x48, 0x18);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.ReadOnly = true;
            this.txtFileName.Size = new Size(0xa8, 0x15);
            this.txtFileName.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x1a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "模板名称:";
            this.txtTitle.EditValue = "";
            this.txtTitle.Location = new Point(0x38, 8);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0xd8, 0x15);
            this.txtTitle.TabIndex = 6;
            this.chkUseTemplate.Location = new Point(8, 40);
            this.chkUseTemplate.Name = "chkUseTemplate";
            this.chkUseTemplate.Properties.Caption = "使用Excel模板";
            this.chkUseTemplate.Size = new Size(0x70, 0x13);
            this.chkUseTemplate.TabIndex = 5;
            this.chkUseTemplate.CheckedChanged += new EventHandler(this.chkUseTemplate_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "标题:";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe8, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(160, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x148, 0xe5);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtTitle);
            base.Controls.Add(this.chkUseTemplate);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
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

        public System.Data.DataTable DataTable
        {
            set
            {
                this.m_dt = value;
            }
        }
    }
}

