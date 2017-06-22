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
	public class ClassCollectResultForm : Form
	{
		public DataTable ResultTable;

		public int nType;

		public int HbCount;

		private IContainer components = null;

		private UltraGrid ultraGrid1;

		private Button PrintButton;

		private Button ToExcel;

		private UltraGridExcelExporter ultraGridExcelExporter1;

		private Button CloseBut;

		private SaveFileDialog SaveExcelDlg;

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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			this.ultraGrid1 = new UltraGrid();
			this.PrintButton = new Button();
			this.ToExcel = new Button();
			this.ultraGridExcelExporter1 = new UltraGridExcelExporter(this.components);
			this.CloseBut = new Button();
			this.SaveExcelDlg = new SaveFileDialog();
			((ISupportInitialize) this.ultraGrid1).BeginInit();
			base.SuspendLayout();
			this.ultraGrid1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			appearance.BackColor = SystemColors.Window;
			appearance.BorderColor = SystemColors.InactiveCaption;
			this.ultraGrid1.DisplayLayout.Appearance=(appearance);
			this.ultraGrid1.DisplayLayout.BorderStyle=(UIElementBorderStyle.Solid);
			this.ultraGrid1.DisplayLayout.CaptionVisible=(DefaultableBoolean.False);
			appearance2.BackColor = SystemColors.ActiveBorder;
			appearance2.BackColor2 = SystemColors.ControlDark;
			appearance2.BackGradientStyle = GradientStyle.Vertical;
			appearance2.BorderColor = SystemColors.Window;
			this.ultraGrid1.DisplayLayout.GroupByBox.Appearance=(appearance2);
			appearance3.ForeColor = SystemColors.GrayText;
			this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance=(appearance3);
			this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle=(UIElementBorderStyle.Solid);
			this.ultraGrid1.DisplayLayout.GroupByBox.Prompt=("拖一列到此，进行分组");
			appearance4.BackColor = SystemColors.ControlLightLight;
			appearance4.BackColor2 = SystemColors.Control;
			appearance4.BackGradientStyle = GradientStyle.Horizontal;
			appearance4.ForeColor = SystemColors.GrayText;
			this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance=(appearance4);
			this.ultraGrid1.DisplayLayout.MaxBandDepth=(2);
			this.ultraGrid1.DisplayLayout.MaxColScrollRegions=(1);
			this.ultraGrid1.DisplayLayout.MaxRowScrollRegions=(1);
			appearance5.BackColor = SystemColors.Window;
			appearance5.ForeColor = SystemColors.ControlText;
			this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance=(appearance5);
			appearance6.BackColor = SystemColors.Highlight;
			appearance6.ForeColor = SystemColors.HighlightText;
			this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance=(appearance6);
			this.ultraGrid1.DisplayLayout.Override.AllowUpdate=(DefaultableBoolean.False);
			this.ultraGrid1.DisplayLayout.Override.BorderStyleCell=(UIElementBorderStyle.Dotted);
			this.ultraGrid1.DisplayLayout.Override.BorderStyleRow=(UIElementBorderStyle.Dotted);
			appearance7.BackColor = SystemColors.Window;
			this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance=(appearance7);
			appearance8.BorderColor = Color.Silver;
			appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
			this.ultraGrid1.DisplayLayout.Override.CellAppearance=(appearance8);
			this.ultraGrid1.DisplayLayout.Override.CellClickAction=CellClickAction.Default;
			this.ultraGrid1.DisplayLayout.Override.CellPadding=(0);
			appearance9.BackColor = SystemColors.Control;
			appearance9.BackColor2 = SystemColors.ControlDark;
			appearance9.BackGradientAlignment = GradientAlignment.Element;
			appearance9.BackGradientStyle = GradientStyle.Horizontal;
			appearance9.BorderColor = SystemColors.Window;
			this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance=(appearance9);
			appearance10.TextHAlignAsString = "Left";
			this.ultraGrid1.DisplayLayout.Override.HeaderAppearance=(appearance10);
			this.ultraGrid1.DisplayLayout.Override.HeaderClickAction=(HeaderClickAction) (3);
			this.ultraGrid1.DisplayLayout.Override.HeaderStyle=(HeaderStyle.WindowsXPCommand);
			appearance11.BackColor = SystemColors.Window;
			appearance11.BorderColor = Color.Silver;
			this.ultraGrid1.DisplayLayout.Override.RowAppearance=(appearance11);
			this.ultraGrid1.DisplayLayout.Override.RowSelectors=(DefaultableBoolean.False);
			appearance12.BackColor = SystemColors.ControlLight;
			this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance=(appearance12);
			this.ultraGrid1.DisplayLayout.ScrollBounds=(0);
			this.ultraGrid1.DisplayLayout.ScrollStyle=(ScrollStyle) (1);
			this.ultraGrid1.DisplayLayout.ViewStyleBand=(ViewStyleBand) (2);
			this.ultraGrid1.Location = new System.Drawing.Point(0, 43);
			this.ultraGrid1.Name = "ultraGrid1";
			this.ultraGrid1.Size = new Size(493, 261);
			this.ultraGrid1.TabIndex = 0;
			this.ultraGrid1.Text = "ultraGrid1";
			this.ultraGrid1.InitializePrintPreview+=(new InitializePrintPreviewEventHandler(this.ultraGrid1_InitializePrintPreview));
			this.ultraGrid1.InitializeLayout+=(new InitializeLayoutEventHandler(this.ultraGrid1_InitializeLayout));
			this.ultraGrid1.InitializeGroupByRow+=(new InitializeGroupByRowEventHandler(this.ultraGrid1_InitializeGroupByRow));
			this.PrintButton.Location = new System.Drawing.Point(4, 7);
			this.PrintButton.Name = "PrintButton";
			this.PrintButton.Size = new Size(75, 23);
			this.PrintButton.TabIndex = 12;
			this.PrintButton.Text = "打印";
			this.PrintButton.UseVisualStyleBackColor = true;
			this.PrintButton.Click += new EventHandler(this.PrintButton_Click);
			this.ToExcel.Location = new System.Drawing.Point(81, 6);
			this.ToExcel.Name = "ToExcel";
			this.ToExcel.Size = new Size(75, 23);
			this.ToExcel.TabIndex = 11;
			this.ToExcel.Text = "转出EXCEL";
			this.ToExcel.UseVisualStyleBackColor = true;
			this.ToExcel.Click += new EventHandler(this.ToExcel_Click);
			this.CloseBut.DialogResult = DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(409, 7);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 13;
			this.CloseBut.Text = "关闭";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.SaveExcelDlg.DefaultExt = "xls";
			this.SaveExcelDlg.FileName = "Result.xls";
			this.SaveExcelDlg.Filter = "Excel文件|*.xls";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(493, 304);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.PrintButton);
			base.Controls.Add(this.ToExcel);
			base.Controls.Add(this.ultraGrid1);
			base.Name = "ClassCollectResultForm";
			base.ShowIcon = false;
			this.Text = "汇总结果";
			base.Load += new EventHandler(this.ClassCollectResultForm_Load);
			((ISupportInitialize) this.ultraGrid1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
