using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    partial class StatForm
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ChartKind = new System.Windows.Forms.ComboBox();
            this.DoIt = new System.Windows.Forms.Button();
            this.ultraChart1 = new DevExpress.XtraCharts.ChartControl();
            this.CountRadio = new System.Windows.Forms.RadioButton();
            this.sumRadio = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图表类型";
            // 
            // ChartKind
            // 
            this.ChartKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChartKind.FormattingEnabled = true;
            this.ChartKind.Items.AddRange(new object[] {
            "柱状图",
            "饼图"});
            this.ChartKind.Location = new System.Drawing.Point(71, 10);
            this.ChartKind.Name = "ChartKind";
            this.ChartKind.Size = new System.Drawing.Size(115, 20);
            this.ChartKind.TabIndex = 1;
            this.ChartKind.SelectedIndexChanged += new System.EventHandler(this.ChartType_SelectedIndexChanged);
            // 
            // DoIt
            // 
            this.DoIt.Location = new System.Drawing.Point(192, 9);
            this.DoIt.Name = "DoIt";
            this.DoIt.Size = new System.Drawing.Size(69, 23);
            this.DoIt.TabIndex = 2;
            this.DoIt.Text = "打印预览";
            this.DoIt.UseVisualStyleBackColor = true;
            this.DoIt.Click += new System.EventHandler(this.DoIt_Click);
            // 
            // ultraChart1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.ultraChart1.Diagram = xyDiagram1;
            this.ultraChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraChart1.Legend.Name = "Default Legend";
            this.ultraChart1.Location = new System.Drawing.Point(0, 0);
            this.ultraChart1.Name = "ultraChart1";
            series1.Name = "Series 1";
            this.ultraChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.ultraChart1.Size = new System.Drawing.Size(694, 307);
            this.ultraChart1.TabIndex = 3;
            this.ultraChart1.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            // 
            // CountRadio
            // 
            this.CountRadio.AutoSize = true;
            this.CountRadio.Checked = true;
            this.CountRadio.Location = new System.Drawing.Point(276, 13);
            this.CountRadio.Name = "CountRadio";
            this.CountRadio.Size = new System.Drawing.Size(83, 16);
            this.CountRadio.TabIndex = 7;
            this.CountRadio.TabStop = true;
            this.CountRadio.Text = "个数分布图";
            this.CountRadio.UseVisualStyleBackColor = true;
            this.CountRadio.CheckedChanged += new System.EventHandler(this.CountRadio_CheckedChanged);
            // 
            // sumRadio
            // 
            this.sumRadio.AutoSize = true;
            this.sumRadio.Location = new System.Drawing.Point(365, 13);
            this.sumRadio.Name = "sumRadio";
            this.sumRadio.Size = new System.Drawing.Size(83, 16);
            this.sumRadio.TabIndex = 8;
            this.sumRadio.Text = "数量分布图";
            this.sumRadio.UseVisualStyleBackColor = true;
            this.sumRadio.CheckedChanged += new System.EventHandler(this.sumRadio_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "btnPicSetting.Image.png");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sumRadio);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ChartKind);
            this.panel1.Controls.Add(this.CountRadio);
            this.panel1.Controls.Add(this.DoIt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(694, 37);
            this.panel1.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 37);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ultraChart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Size = new System.Drawing.Size(694, 463);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.TabIndex = 13;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(694, 152);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // StatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 500);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "StatForm";
            this.ShowInTaskbar = false;
            this.Text = "统计图表";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatForm_FormClosed);
            this.Load += new System.EventHandler(this.StatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }


        private IContainer components = null;
        private Label label1;
        private ComboBox ChartKind;
        private Button DoIt;
        private RadioButton CountRadio;
        private RadioButton sumRadio;
        private ImageList imageList1;
        private DataTable dt;
        private DataTable AllTable;
        private DataTable CalTable;
        private string statWay;
        private string calField;
        private string statField;
        private bool bExtent;
        private DevExpress.XtraCharts.ChartControl ultraChart1;
        private PrintDialog printDialog1;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}