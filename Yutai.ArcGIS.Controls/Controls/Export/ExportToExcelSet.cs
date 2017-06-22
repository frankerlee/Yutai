using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal partial class ExportToExcelSet : UserControl
    {

        public ExportToExcelSet()
        {
            this.InitializeComponent();
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

 public bool Do()
        {
            if (this.chkUseTemplate.Checked && (this.txtFileName.Text.Length == 0))
            {
                MessageBox.Show("请选择模板文件!");
                return false;
            }
            ExportToExcelHelper.ExcelHelper.Export();
            return true;
        }

        private void Excute()
        {
        }

        private void ExportToExcelSet_Load(object sender, EventArgs e)
        {
        }

 private void Start1()
        {
        }

        private void txtRowCount_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ExportToExcelHelper.ExcelHelper.TempleteRowCount = int.Parse(this.txtRowCount.Text);
            }
            catch
            {
            }
        }

        private void txtRowStartIndex_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ExportToExcelHelper.ExcelHelper.TempleteStartRowIndex = int.Parse(this.txtRowStartIndex.Text);
            }
            catch
            {
            }
        }

        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            ExportToExcelHelper.ExcelHelper.Title = this.txtTitle.Text;
        }
    }
}

