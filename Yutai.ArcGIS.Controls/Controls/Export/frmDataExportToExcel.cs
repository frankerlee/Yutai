using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public partial class frmDataExportToExcel : Form
    {
        private System.Data.DataTable m_dt = null;

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

 private void frmDataExportToExcel_Load(object sender, EventArgs e)
        {
            if (ExportToExcelHelper.ExcelHelper == null)
            {
                ExportToExcelHelper.Init();
            }
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

