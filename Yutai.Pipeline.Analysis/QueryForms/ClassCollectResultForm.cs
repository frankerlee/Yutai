using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class ClassCollectResultForm : Form
	{
		public DataTable ResultTable;
		public int HbCount;
	    public int nType;

	    public ClassCollectResultForm()
		{
			this.InitializeComponent();
		}

		private void ClassCollectResultForm_Load(object sender, EventArgs e)
		{
			this._dataGridView.DataSource=(this.ResultTable);
		}
        
		private void PrintButton_Click(object sender, EventArgs e)
		{
			if (this.ResultTable.Rows.Count < 1)
			{
				MessageBox.Show(@"空数据,不进行打印！");
			}
			else
			{
				try
				{
					this._dataGridView.PrintPreview();
				}
				catch (Exception ex)
				{
					MessageBox.Show(@"Error occured while printing.\n" + ex.Message, @"Error printing", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void ToExcel_Click(object sender, EventArgs e)
		{
			if (this.ResultTable.Rows.Count < 1)
			{
				MessageBox.Show(@"空数据,不转出EXCEL文件！");
			}
			else
            {
                this._dataGridView.ToExcel(true);
			}
		}

		private void CloseBut_Click(object sender, EventArgs e)
		{
			base.Close();
		}
    }
}