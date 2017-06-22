
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class VerDistDlg
    {
		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
		private void InitializeComponent()
		{
			this.icontainer_0 = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle white = new DataGridViewCellStyle();
			this.btAnalyse = new Button();
			this.tbBufferRadius = new TextBox();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.序号 = new DataGridViewTextBoxColumn();
			this.PipeDataset = new DataGridViewTextBoxColumn();
			this.FID = new DataGridViewTextBoxColumn();
			this.verDist = new DataGridViewTextBoxColumn();
			this.stanDist = new DataGridViewTextBoxColumn();
			this.timer_0 = new Timer(this.icontainer_0);
			this.btClose = new Button();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.btAnalyse.Enabled = false;
			this.btAnalyse.Location = new System.Drawing.Point(215, 188);
			this.btAnalyse.Name = "btAnalyse";
			this.btAnalyse.Size = new System.Drawing.Size(75, 23);
			this.btAnalyse.TabIndex = 7;
			this.btAnalyse.Text = "分析(&A)";
			this.btAnalyse.UseVisualStyleBackColor = true;
			this.btAnalyse.Click += new EventHandler(this.btAnalyse_Click);
			this.tbBufferRadius.Location = new System.Drawing.Point(104, 185);
			this.tbBufferRadius.Name = "tbBufferRadius";
			this.tbBufferRadius.Size = new System.Drawing.Size(99, 21);
			this.tbBufferRadius.TabIndex = 6;
			this.tbBufferRadius.Text = "20";
			this.tbBufferRadius.KeyPress += new KeyPressEventHandler(this.tbBufferRadius_KeyPress);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 188);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "分析半径(米)：";
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeight = 22;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.序号, this.PipeDataset, this.FID, this.verDist, this.stanDist });
			
			this.dataGridView1.Location = new System.Drawing.Point(12, 16);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.RowTemplate.Height = 20;
			this.dataGridView1.Size = new System.Drawing.Size(359, 163);
			this.dataGridView1.TabIndex = 4;
			this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			this.序号.HeaderText = "序号";
			this.序号.Name = "序号";
			this.序号.ReadOnly = true;
			this.序号.Width = 40;
			this.PipeDataset.HeaderText = "管线性质";
			this.PipeDataset.Name = "PipeDataset";
			this.PipeDataset.ReadOnly = true;
			this.PipeDataset.Width = 80;
			this.FID.HeaderText = "编号";
			this.FID.Name = "FID";
			this.FID.ReadOnly = true;
			this.FID.Width = 35;
			this.verDist.HeaderText = "垂直净距(米)";
			this.verDist.Name = "verDist";
			this.verDist.ReadOnly = true;
			white.Alignment = DataGridViewContentAlignment.MiddleCenter;
			white.BackColor = Color.White;
			white.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			white.ForeColor = SystemColors.WindowText;
			white.SelectionBackColor = SystemColors.Highlight;
			white.SelectionForeColor = SystemColors.HighlightText;
			white.WrapMode = DataGridViewTriState.True;
			this.stanDist.DefaultCellStyle = white;
			this.stanDist.HeaderText = "标准";
			this.stanDist.Name = "stanDist";
			this.stanDist.ReadOnly = true;
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			this.btClose.Location = new System.Drawing.Point(296, 188);
			this.btClose.Name = "btClose";
			this.btClose.Size = new System.Drawing.Size(75, 23);
			this.btClose.TabIndex = 22;
			this.btClose.Text = "关闭(&C)";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new EventHandler(this.btClose_Click);
			base.AcceptButton = this.btAnalyse;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(373, 224);
			base.Controls.Add(this.btClose);
			base.Controls.Add(this.btAnalyse);
			base.Controls.Add(this.tbBufferRadius);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dataGridView1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "VerDistDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "垂直净距分析";
			base.TopMost = true;
			base.FormClosing += new FormClosingEventHandler(this.VerDistDlg_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.VerDistDlg_FormClosed);
			base.Load += new EventHandler(this.VerDistDlg_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private Button btAnalyse;
		private TextBox tbBufferRadius;
		private Label label1;
		private DataGridView dataGridView1;
		private Timer timer_0;
		private Button btClose;
		private DataGridViewTextBoxColumn 序号;
		private DataGridViewTextBoxColumn PipeDataset;
		private DataGridViewTextBoxColumn FID;
		private DataGridViewTextBoxColumn verDist;
		private DataGridViewTextBoxColumn stanDist;
		private IPolyline ipolyline_0;
    }
}