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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaiShenAnalysis));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.chkRegionAnalysis = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelctAll = new System.Windows.Forms.Button();
            this.btnSelectReverse = new System.Windows.Forms.Button();
            this.btnSelectNon = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveFileDialog_0 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer_0 = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(143, 176);
            this.checkedListBox1.TabIndex = 1;
            // 
            // chkRegionAnalysis
            // 
            this.chkRegionAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRegionAnalysis.AutoSize = true;
            this.chkRegionAnalysis.Location = new System.Drawing.Point(244, 13);
            this.chkRegionAnalysis.Name = "chkRegionAnalysis";
            this.chkRegionAnalysis.Size = new System.Drawing.Size(108, 16);
            this.chkRegionAnalysis.TabIndex = 2;
            this.chkRegionAnalysis.Text = "区域分析(隐藏)";
            this.chkRegionAnalysis.UseVisualStyleBackColor = true;
            this.chkRegionAnalysis.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(359, 176);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkedListBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(506, 176);
            this.splitContainer1.SplitterDistance = 143;
            this.splitContainer1.TabIndex = 4;
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalysis.Location = new System.Drawing.Point(345, 226);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(75, 23);
            this.btnAnalysis.TabIndex = 5;
            this.btnAnalysis.Text = "分析";
            this.btnAnalysis.UseVisualStyleBackColor = true;
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(443, 226);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "选择要分析管线层";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "分析结果";
            // 
            // btnSelctAll
            // 
            this.btnSelctAll.AutoSize = true;
            this.btnSelctAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelctAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelctAll.Image")));
            this.btnSelctAll.Location = new System.Drawing.Point(119, 12);
            this.btnSelctAll.Name = "btnSelctAll";
            this.btnSelctAll.Size = new System.Drawing.Size(26, 23);
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
            this.btnSelectReverse.Location = new System.Drawing.Point(147, 12);
            this.btnSelectReverse.Name = "btnSelectReverse";
            this.btnSelectReverse.Size = new System.Drawing.Size(26, 23);
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
            this.btnSelectNon.Location = new System.Drawing.Point(177, 12);
            this.btnSelectNon.Name = "btnSelectNon";
            this.btnSelectNon.Size = new System.Drawing.Size(26, 23);
            this.btnSelectNon.TabIndex = 11;
            this.btnSelectNon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectNon.UseVisualStyleBackColor = true;
            this.btnSelectNon.Click += new System.EventHandler(this.btnSelectNon_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(384, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 226);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(327, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Visible = false;
            // 
            // timer_0
            // 
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
            // 
            // FormMaiShenAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 261);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSelectNon);
            this.Controls.Add(this.btnSelectReverse);
            this.Controls.Add(this.btnSelctAll);
            this.Controls.Add(this.label2);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion
       
		private CheckedListBox checkedListBox1;
		private CheckBox chkRegionAnalysis;
		private DataGridView dataGridView1;
		private Button btnAnalysis;
		private Button btnExit;
		private Label label1;
		private Label label2;
		private Button btnSelctAll;
		private Button btnSelectReverse;
		private Button btnSelectNon;
		private Button btnExport;
		private SaveFileDialog saveFileDialog_0;
		private SplitContainer splitContainer1;
		private ProgressBar progressBar1;
		private Timer timer_0;
		private IFeature ifeature_0;
	    private IAppContext _context;
    }
}