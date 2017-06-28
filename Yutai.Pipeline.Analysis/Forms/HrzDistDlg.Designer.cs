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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PipeDataset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hrzDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBufferRadius = new System.Windows.Forms.TextBox();
            this.btAnalyse = new System.Windows.Forms.Button();
            this.timer_0 = new System.Windows.Forms.Timer();
            this.btClose = new System.Windows.Forms.Button();
            this.cmbDepthType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 22;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.PipeDataset,
            this.FID,
            this.hrzDist,
            this.stanDist});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Location = new System.Drawing.Point(20, 51);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(419, 183);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // 序号
            // 
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            this.序号.Width = 40;
            // 
            // PipeDataset
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PipeDataset.DefaultCellStyle = dataGridViewCellStyle2;
            this.PipeDataset.HeaderText = "管线性质";
            this.PipeDataset.Name = "PipeDataset";
            this.PipeDataset.ReadOnly = true;
            this.PipeDataset.Width = 80;
            // 
            // FID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FID.DefaultCellStyle = dataGridViewCellStyle3;
            this.FID.HeaderText = "编号";
            this.FID.Name = "FID";
            this.FID.ReadOnly = true;
            this.FID.Width = 35;
            // 
            // hrzDist
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.hrzDist.DefaultCellStyle = dataGridViewCellStyle4;
            this.hrzDist.HeaderText = "水平净距(米)";
            this.hrzDist.Name = "hrzDist";
            this.hrzDist.ReadOnly = true;
            // 
            // stanDist
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.stanDist.DefaultCellStyle = dataGridViewCellStyle5;
            this.stanDist.HeaderText = "标准";
            this.stanDist.Name = "stanDist";
            this.stanDist.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "分析半径(米)：";
            // 
            // tbBufferRadius
            // 
            this.tbBufferRadius.Location = new System.Drawing.Point(112, 12);
            this.tbBufferRadius.Name = "tbBufferRadius";
            this.tbBufferRadius.Size = new System.Drawing.Size(115, 22);
            this.tbBufferRadius.TabIndex = 2;
            this.tbBufferRadius.Text = "5";
            this.tbBufferRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBufferRadius_KeyPress);
            // 
            // btAnalyse
            // 
            this.btAnalyse.Enabled = false;
            this.btAnalyse.Location = new System.Drawing.Point(83, 246);
            this.btAnalyse.Name = "btAnalyse";
            this.btAnalyse.Size = new System.Drawing.Size(87, 27);
            this.btAnalyse.TabIndex = 3;
            this.btAnalyse.Text = "分析(&A)";
            this.btAnalyse.UseVisualStyleBackColor = true;
            this.btAnalyse.Click += new System.EventHandler(this.btAnalyse_Click);
            // 
            // timer_0
            // 
            this.timer_0.Interval = 500;
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(233, 246);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(87, 27);
            this.btClose.TabIndex = 21;
            this.btClose.Text = "关闭(&C)";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // cmbDepthType
            // 
            this.cmbDepthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepthType.FormattingEnabled = true;
            this.cmbDepthType.Items.AddRange(new object[] {
            "保存在属性中",
            "保存在Z，M值中"});
            this.cmbDepthType.Location = new System.Drawing.Point(322, 12);
            this.cmbDepthType.Name = "cmbDepthType";
            this.cmbDepthType.Size = new System.Drawing.Size(121, 22);
            this.cmbDepthType.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 14);
            this.label4.TabIndex = 23;
            this.label4.Text = "埋深数据类型:";
            // 
            // HrzDistDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 282);
            this.Controls.Add(this.cmbDepthType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btAnalyse);
            this.Controls.Add(this.tbBufferRadius);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HrzDistDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "水平净距分析";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HrzDistDlg_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HrzDistDlg_FormClosed);
            this.Load += new System.EventHandler(this.HrzDistDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ComboBox cmbDepthType;
        private Label label4;
    }
}