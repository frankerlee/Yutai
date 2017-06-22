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
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class HrzDistDlg
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
			DataGridViewCellStyle window = new DataGridViewCellStyle();
			DataGridViewCellStyle control = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle font = new DataGridViewCellStyle();
			this.dataGridView1 = new DataGridView();
			this.序号 = new DataGridViewTextBoxColumn();
			this.PipeDataset = new DataGridViewTextBoxColumn();
			this.FID = new DataGridViewTextBoxColumn();
			this.hrzDist = new DataGridViewTextBoxColumn();
			this.stanDist = new DataGridViewTextBoxColumn();
			this.label1 = new Label();
			this.tbBufferRadius = new TextBox();
			this.btAnalyse = new Button();
			this.timer_0 = new Timer(this.icontainer_0);
			this.btClose = new Button();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
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
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.序号, this.PipeDataset, this.FID, this.hrzDist, this.stanDist });
			window.Alignment = DataGridViewContentAlignment.MiddleCenter;
			window.BackColor = SystemColors.Window;
			window.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			window.ForeColor = SystemColors.ControlText;
			window.SelectionBackColor = SystemColors.Highlight;
			window.SelectionForeColor = SystemColors.HighlightText;
			window.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = window;
			this.dataGridView1.Location = new System.Drawing.Point(17, 44);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			control.Alignment = DataGridViewContentAlignment.MiddleCenter;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			control.ForeColor = SystemColors.WindowText;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = control;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.RowTemplate.Height = 20;
			this.dataGridView1.Size = new System.Drawing.Size(359, 157);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			this.序号.HeaderText = "序号";
			this.序号.Name = "序号";
			this.序号.ReadOnly = true;
			this.序号.Width = 40;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.PipeDataset.DefaultCellStyle = dataGridViewCellStyle1;
			this.PipeDataset.HeaderText = "管线性质";
			this.PipeDataset.Name = "PipeDataset";
			this.PipeDataset.ReadOnly = true;
			this.PipeDataset.Width = 80;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.FID.DefaultCellStyle = dataGridViewCellStyle2;
			this.FID.HeaderText = "编号";
			this.FID.Name = "FID";
			this.FID.ReadOnly = true;
			this.FID.Width = 35;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.hrzDist.DefaultCellStyle = dataGridViewCellStyle3;
			this.hrzDist.HeaderText = "水平净距(米)";
			this.hrzDist.Name = "hrzDist";
			this.hrzDist.ReadOnly = true;
			font.Alignment = DataGridViewContentAlignment.MiddleCenter;
			font.BackColor = SystemColors.Window;
			font.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			font.ForeColor = SystemColors.ControlText;
			font.SelectionBackColor = SystemColors.Highlight;
			font.SelectionForeColor = SystemColors.HighlightText;
			font.WrapMode = DataGridViewTriState.False;
			this.stanDist.DefaultCellStyle = font;
			this.stanDist.HeaderText = "标准";
			this.stanDist.Name = "stanDist";
			this.stanDist.ReadOnly = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "分析半径(米)：";
			this.tbBufferRadius.Location = new System.Drawing.Point(120, 10);
			this.tbBufferRadius.Name = "tbBufferRadius";
			this.tbBufferRadius.Size = new System.Drawing.Size(99, 21);
			this.tbBufferRadius.TabIndex = 2;
			this.tbBufferRadius.Text = "5";
			this.tbBufferRadius.KeyPress += new KeyPressEventHandler(this.tbBufferRadius_KeyPress);
			this.btAnalyse.Enabled = false;
			this.btAnalyse.Location = new System.Drawing.Point(71, 211);
			this.btAnalyse.Name = "btAnalyse";
			this.btAnalyse.Size = new System.Drawing.Size(75, 23);
			this.btAnalyse.TabIndex = 3;
			this.btAnalyse.Text = "分析(&A)";
			this.btAnalyse.UseVisualStyleBackColor = true;
			this.btAnalyse.Click += new EventHandler(this.btAnalyse_Click);
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			this.btClose.Location = new System.Drawing.Point(200, 211);
			this.btClose.Name = "btClose";
			this.btClose.Size = new System.Drawing.Size(75, 23);
			this.btClose.TabIndex = 21;
			this.btClose.Text = "关闭(&C)";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new EventHandler(this.btClose_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(390, 242);
			base.Controls.Add(this.btClose);
			base.Controls.Add(this.btAnalyse);
			base.Controls.Add(this.tbBufferRadius);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dataGridView1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HrzDistDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "水平净距分析";
			base.TopMost = true;
			base.FormClosed += new FormClosedEventHandler(this.HrzDistDlg_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.HrzDistDlg_FormClosing);
			base.Load += new EventHandler(this.HrzDistDlg_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private IPolyline ipolyline_0;
		private DataGridView dataGridView1;
		private Label label1;
		private TextBox tbBufferRadius;
		private Button btAnalyse;
		private Timer timer_0;
		private Button btClose;
		private DataGridViewTextBoxColumn 序号;
		private DataGridViewTextBoxColumn PipeDataset;
		private DataGridViewTextBoxColumn FID;
		private DataGridViewTextBoxColumn hrzDist;
		private DataGridViewTextBoxColumn stanDist;
    }
}