using Infragistics.Win;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class ClassCollectResultForm : Form
	{
		public DataTable ResultTable;

		public int nType;

		public int HbCount;








		public ClassCollectResultForm()
		{
			this.InitializeComponent();
		}

		private void ClassCollectResultForm_Load(object sender, EventArgs e)
		{
			this.ultraGrid1.DataSource=(this.ResultTable);
			this.ultraGrid1.DataBind();
		}

		private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
			e.Layout.Bands[0].Override.RowAlternateAppearance.BackColor = Color.LightCyan;
			e.Layout.Override.MergedCellStyle=(MergedCellStyle) (2);
			e.Layout.Override.MergedCellContentArea=(MergedCellContentArea) (2);
			e.Layout.Override.MergedCellAppearance.BackColor = Color.LightYellow;
			e.Layout.Override.MergedCellAppearance.ForeColor = Color.Blue;
			e.Layout.Bands[0].Columns[0].MergedCellStyle=(MergedCellStyle) (1);
			e.Layout.Bands[0].Columns[0].MergedCellContentArea=(MergedCellContentArea) (1);
			e.Layout.Bands[0].Columns[0].MergedCellEvaluationType=(MergedCellEvaluationType) (2);
			if (this.nType == 1)
			{
				e.Layout.Bands[0].Columns[1].MergedCellStyle=(MergedCellStyle) (1);
				e.Layout.Bands[0].Columns[1].MergedCellContentArea=(MergedCellContentArea) (1);
				e.Layout.Bands[0].Columns[1].MergedCellEvaluationType=(MergedCellEvaluationType) (2);
			}
			if (this.nType == 2)
			{
				for (int i = 0; i < this.HbCount; i++)
				{
					e.Layout.Bands[0].Columns[i+1].MergedCellStyle=(MergedCellStyle) (1);
					e.Layout.Bands[0].Columns[i+1].MergedCellContentArea=(MergedCellContentArea) (1);
					e.Layout.Bands[0].Columns[i+1].MergedCellEvaluationType=(MergedCellEvaluationType) (2);
				}
			}
		}

		private void ultraGrid1_InitializeGroupByRow(object sender, InitializeGroupByRowEventArgs e)
		{
			e.Row.Description=(string.Concat(new object[]
			{
				e.Row.Value.ToString(),
				", ",
				e.Row.Rows.Count,
				" 条记录."
			}));
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			if (this.ResultTable.Rows.Count < 1)
			{
				MessageBox.Show("空数据,不进行打印！");
			}
			else
			{
				try
				{
					this.ultraGrid1.PrintPreview();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error occured while printing.\n" + ex.Message, "Error printing", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void ToExcel_Click(object sender, EventArgs e)
		{
			if (this.ResultTable.Rows.Count < 1)
			{
				MessageBox.Show("空数据,不转出EXCEL文件！");
			}
			else
			{
				DialogResult dialogResult = this.SaveExcelDlg.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					string fileName = this.SaveExcelDlg.FileName;
					this.ultraGridExcelExporter1.Export(this.ultraGrid1, fileName);
				}
			}
		}

		private void CloseBut_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void ultraGrid1_InitializePrintPreview(object sender, CancelablePrintPreviewEventArgs e)
		{
			e.PrintPreviewSettings.Zoom=(1.5);
			e.PrintPreviewSettings.DialogLeft=(SystemInformation.WorkingArea.X);
			e.PrintPreviewSettings.DialogTop=(SystemInformation.WorkingArea.Y);
			e.PrintPreviewSettings.DialogWidth=(SystemInformation.WorkingArea.Width);
			e.PrintPreviewSettings.DialogHeight=(SystemInformation.WorkingArea.Height);
			e.DefaultLogicalPageLayoutInfo.FitWidthToPages=(1);
		}


    }
}