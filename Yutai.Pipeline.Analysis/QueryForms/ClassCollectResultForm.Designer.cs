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
	    partial class ClassCollectResultForm
    {
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
			this.ultraGrid1.DisplayLayout.Override.HeaderClickAction=(HeaderClickAction.SortMulti);
			this.ultraGrid1.DisplayLayout.Override.HeaderStyle=(HeaderStyle.WindowsXPCommand);
			appearance11.BackColor = SystemColors.Window;
			appearance11.BorderColor = Color.Silver;
			this.ultraGrid1.DisplayLayout.Override.RowAppearance=(appearance11);
			this.ultraGrid1.DisplayLayout.Override.RowSelectors=(DefaultableBoolean.False);
			appearance12.BackColor = SystemColors.ControlLight;
			this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance=(appearance12);
			this.ultraGrid1.DisplayLayout.ScrollBounds=(0);
			this.ultraGrid1.DisplayLayout.ScrollStyle=(ScrollStyle.Immediate);
			this.ultraGrid1.DisplayLayout.ViewStyleBand=(ViewStyleBand.OutlookGroupBy);
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
			this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
	
		private IContainer components = null;
		private UltraGrid ultraGrid1;
		private Button PrintButton;
		private Button ToExcel;
		private UltraGridExcelExporter ultraGridExcelExporter1;
		private Button CloseBut;
		private SaveFileDialog SaveExcelDlg;
    }
}