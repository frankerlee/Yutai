using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    partial class QueryResult
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.SaveExcelDlg = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ColchooserBut = new System.Windows.Forms.Button();
            this.SetPosBut = new System.Windows.Forms.Button();
            this.RayObjectBut = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.CountBox = new System.Windows.Forms.TextBox();
            this.CalField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StatWay = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StatField = new System.Windows.Forms.ComboBox();
            this.Stat_but = new System.Windows.Forms.Button();
            this.ToExcel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveExcelDlg
            // 
            this.SaveExcelDlg.DefaultExt = "xls";
            this.SaveExcelDlg.FileName = "Result.xls";
            this.SaveExcelDlg.Filter = "Excel文件|*.xls";
            this.SaveExcelDlg.OverwritePrompt = false;
            this.SaveExcelDlg.Title = "保存";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ColchooserBut);
            this.panel1.Controls.Add(this.SetPosBut);
            this.panel1.Controls.Add(this.RayObjectBut);
            this.panel1.Controls.Add(this.PrintButton);
            this.panel1.Controls.Add(this.CountBox);
            this.panel1.Controls.Add(this.CalField);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.StatWay);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.StatField);
            this.panel1.Controls.Add(this.Stat_but);
            this.panel1.Controls.Add(this.ToExcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 60);
            this.panel1.TabIndex = 14;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ColchooserBut
            // 
            this.ColchooserBut.Location = new System.Drawing.Point(667, 30);
            this.ColchooserBut.Name = "ColchooserBut";
            this.ColchooserBut.Size = new System.Drawing.Size(41, 23);
            this.ColchooserBut.TabIndex = 26;
            this.ColchooserBut.Text = "设置";
            this.ColchooserBut.UseVisualStyleBackColor = true;
            // 
            // SetPosBut
            // 
            this.SetPosBut.Location = new System.Drawing.Point(404, 29);
            this.SetPosBut.Name = "SetPosBut";
            this.SetPosBut.Size = new System.Drawing.Size(128, 23);
            this.SetPosBut.TabIndex = 25;
            this.SetPosBut.Text = "实体闪烁";
            this.SetPosBut.UseVisualStyleBackColor = true;
            // 
            // RayObjectBut
            // 
            this.RayObjectBut.Location = new System.Drawing.Point(214, 29);
            this.RayObjectBut.Name = "RayObjectBut";
            this.RayObjectBut.Size = new System.Drawing.Size(128, 23);
            this.RayObjectBut.TabIndex = 24;
            this.RayObjectBut.Text = "实体定位";
            this.RayObjectBut.UseVisualStyleBackColor = true;
            // 
            // PrintButton
            // 
            this.PrintButton.Location = new System.Drawing.Point(624, 30);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(42, 23);
            this.PrintButton.TabIndex = 23;
            this.PrintButton.Text = "打印";
            this.PrintButton.UseVisualStyleBackColor = true;
            // 
            // CountBox
            // 
            this.CountBox.Location = new System.Drawing.Point(10, 31);
            this.CountBox.Name = "CountBox";
            this.CountBox.ReadOnly = true;
            this.CountBox.Size = new System.Drawing.Size(140, 21);
            this.CountBox.TabIndex = 22;
            // 
            // CalField
            // 
            this.CalField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CalField.Enabled = false;
            this.CalField.FormattingEnabled = true;
            this.CalField.Location = new System.Drawing.Point(257, 7);
            this.CalField.Name = "CalField";
            this.CalField.Size = new System.Drawing.Size(85, 20);
            this.CalField.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "计算项";
            // 
            // StatWay
            // 
            this.StatWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StatWay.FormattingEnabled = true;
            this.StatWay.Items.AddRange(new object[] {
            "计数",
            "求和",
            "平均值"});
            this.StatWay.Location = new System.Drawing.Point(64, 7);
            this.StatWay.Name = "StatWay";
            this.StatWay.Size = new System.Drawing.Size(86, 20);
            this.StatWay.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "统计方式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "分类项";
            // 
            // StatField
            // 
            this.StatField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StatField.FormattingEnabled = true;
            this.StatField.Location = new System.Drawing.Point(446, 7);
            this.StatField.Name = "StatField";
            this.StatField.Size = new System.Drawing.Size(86, 20);
            this.StatField.TabIndex = 16;
            // 
            // Stat_but
            // 
            this.Stat_but.Location = new System.Drawing.Point(624, 6);
            this.Stat_but.Name = "Stat_but";
            this.Stat_but.Size = new System.Drawing.Size(153, 22);
            this.Stat_but.TabIndex = 15;
            this.Stat_but.Text = "统计";
            this.Stat_but.UseVisualStyleBackColor = true;
            // 
            // ToExcel
            // 
            this.ToExcel.Location = new System.Drawing.Point(709, 29);
            this.ToExcel.Name = "ToExcel";
            this.ToExcel.Size = new System.Drawing.Size(68, 24);
            this.ToExcel.TabIndex = 14;
            this.ToExcel.Text = "转出EXCEL";
            this.ToExcel.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraGrid1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(892, 311);
            this.panel2.TabIndex = 15;
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.GroupByBox.Hidden = true;
            this.ultraGrid1.DisplayLayout.GroupByBox.Prompt = "分组项，点击修改排序";
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Rectangular;
            this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance1;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(892, 311);
            this.ultraGrid1.TabIndex = 1;
            this.ultraGrid1.Text = "ultraGrid1";
            // 
            // QueryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 371);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "QueryResult";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "查询结果:Shift+A减少透明,Shift+S增加透明";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QueryResult_FormClosed);
            this.Load += new System.EventHandler(this.QueryResult_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QueryResult_KeyDown);
            this.Resize += new System.EventHandler(this.QueryResult_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.ResumeLayout(false);

        }


        private IContainer components = null;
        private UltraGridExcelExporter ultraGridExcelExporter1;
        private SaveFileDialog SaveExcelDlg;
        private Button ColchooserBut;
        private Button SetPosBut;
        private Button RayObjectBut;
        private Button PrintButton;
        private TextBox CountBox;
        private ComboBox CalField;
        private Label label3;
        private ComboBox StatWay;
        private Label label2;
        private Label label1;
        private ComboBox StatField;
        private Button Stat_but;
        private Button ToExcel;
        private UltraGrid ultraGrid1;
        private Panel panel1;
        private Panel panel2;
        private IFeatureCursor m_FeatureCursor;
        private IGeometry AllGeo;
        private bool bControlEvent;
        private int OidField;
        private CustomColumnChooser customColumnChooserDialog;
        private string TopBandText;
        private FontData TopBandFont;
        private Color TopBandColor;
        private bool bHaveTop;
    }
}