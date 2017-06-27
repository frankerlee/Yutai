using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class FormMaiShenAnalysis
    {
        protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaiShenAnalysis));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.chkRegionAnalysis = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelctAll = new System.Windows.Forms.Button();
            this.btnSelectReverse = new System.Windows.Forms.Button();
            this.btnSelectNon = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveFileDialog_0 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer_0 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDepthType = new System.Windows.Forms.ComboBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(166, 205);
            this.checkedListBox1.TabIndex = 1;
            // 
            // chkRegionAnalysis
            // 
            this.chkRegionAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRegionAnalysis.AutoSize = true;
            this.chkRegionAnalysis.Location = new System.Drawing.Point(402, 19);
            this.chkRegionAnalysis.Name = "chkRegionAnalysis";
            this.chkRegionAnalysis.Size = new System.Drawing.Size(98, 18);
            this.chkRegionAnalysis.TabIndex = 2;
            this.chkRegionAnalysis.Text = "当前视图范围";
            this.chkRegionAnalysis.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(14, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkedListBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Size = new System.Drawing.Size(590, 205);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 4;
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalysis.Location = new System.Drawing.Point(402, 264);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(87, 27);
            this.btnAnalysis.TabIndex = 5;
            this.btnAnalysis.Text = "分析";
            this.btnAnalysis.UseVisualStyleBackColor = true;
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(517, 264);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(87, 27);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "管线层";
            // 
            // btnSelctAll
            // 
            this.btnSelctAll.AutoSize = true;
            this.btnSelctAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelctAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelctAll.Image")));
            this.btnSelctAll.Location = new System.Drawing.Point(61, 16);
            this.btnSelctAll.Name = "btnSelctAll";
            this.btnSelctAll.Size = new System.Drawing.Size(30, 27);
            this.btnSelctAll.TabIndex = 9;
            this.btnSelctAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelctAll.UseVisualStyleBackColor = true;
            this.btnSelctAll.Click += new System.EventHandler(this.btnSelctAll_Click);
            // 
            // btnSelectReverse
            // 
            this.btnSelectReverse.AutoSize = true;
            this.btnSelectReverse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectReverse.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectReverse.Image")));
            this.btnSelectReverse.Location = new System.Drawing.Point(93, 16);
            this.btnSelectReverse.Name = "btnSelectReverse";
            this.btnSelectReverse.Size = new System.Drawing.Size(30, 27);
            this.btnSelectReverse.TabIndex = 10;
            this.btnSelectReverse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectReverse.UseVisualStyleBackColor = true;
            this.btnSelectReverse.Click += new System.EventHandler(this.btnSelectReverse_Click);
            // 
            // btnSelectNon
            // 
            this.btnSelectNon.AutoSize = true;
            this.btnSelectNon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectNon.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectNon.Image")));
            this.btnSelectNon.Location = new System.Drawing.Point(128, 16);
            this.btnSelectNon.Name = "btnSelectNon";
            this.btnSelectNon.Size = new System.Drawing.Size(30, 27);
            this.btnSelectNon.TabIndex = 11;
            this.btnSelectNon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectNon.UseVisualStyleBackColor = true;
            this.btnSelectNon.Click += new System.EventHandler(this.btnSelectNon_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(517, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(87, 27);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(14, 264);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(381, 27);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Visible = false;
            // 
            // timer_0
            // 
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "埋深数据类型:";
            // 
            // cmbDepthType
            // 
            this.cmbDepthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepthType.FormattingEnabled = true;
            this.cmbDepthType.Items.AddRange(new object[] {
            "保存在属性中",
            "保存在M值中"});
            this.cmbDepthType.Location = new System.Drawing.Point(271, 16);
            this.cmbDepthType.Name = "cmbDepthType";
            this.cmbDepthType.Size = new System.Drawing.Size(121, 22);
            this.cmbDepthType.TabIndex = 15;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(419, 205);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsLayout.Columns.AddNewColumns = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // FormMaiShenAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 304);
            this.Controls.Add(this.cmbDepthType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSelectNon);
            this.Controls.Add(this.btnSelectReverse);
            this.Controls.Add(this.btnSelctAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAnalysis);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.chkRegionAnalysis);
            this.Name = "FormMaiShenAnalysis";
            this.ShowIcon = false;
            this.Text = "覆土分析";
            this.Load += new System.EventHandler(this.FormMaiShenAnalysis_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FormMaiShenAnalysis_HelpRequested);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

      
       
		private CheckedListBox checkedListBox1;
		private CheckBox chkRegionAnalysis;
		private Button btnAnalysis;
		private Button btnExit;
		private Label label1;
		private Button btnSelctAll;
		private Button btnSelectReverse;
		private Button btnSelectNon;
		private Button btnExport;
		private SaveFileDialog saveFileDialog_0;
		private SplitContainer splitContainer1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private Timer timer_0;
		private IFeature ifeature_0;
	    private IAppContext _context;
        private IContainer components;
        private Label label2;
        private System.Windows.Forms.ComboBox cmbDepthType;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}