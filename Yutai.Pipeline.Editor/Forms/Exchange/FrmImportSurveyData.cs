using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Emuns;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    public partial class FrmImportSurveyData : Form
    {
        public FrmImportSurveyData()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtBaseName.Text.Trim()) || string.IsNullOrWhiteSpace(ucSelectGDBFile.FileName) || string.IsNullOrWhiteSpace(this.txtSurveyer.Text.Trim()) || string.IsNullOrWhiteSpace(ucSelectExcelFile.FileName) || string.IsNullOrWhiteSpace(ucSelectSurveyFile.FileName))
            {
                MessageBox.Show(@"请将参数设置完整，不能缺项！");
            }
            if (!(new eFieldFileHelper(ucSelectGDBFile.Workspace, this.txtBaseName.Text.Trim(), ucSelectExcelFile.FileName, ucSelectSurveyFile.FileName, (SurveyCoordType)(this.cmbCoordType.SelectedIndex + 1), this.txtSurveyer.Text.Trim(), this.dateSurvey.Value)).StartImport())
            {
                MessageBox.Show(@"转入和挂接数据发生错误！");
            }
            else
            {
                MessageBox.Show($@"转入并挂接完成，你可以直接打开表查看！表位置在{ucSelectGDBFile.FileName}\\XYZ_{this.txtBaseName.Text}");
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
