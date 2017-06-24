
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;

using Yutai.Pipeline.Analysis.Classes;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class ResultDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultDialog));
			this.dataGridView3 = new DataGridView();
			this.timer_0 = new Timer(this.icontainer_0);
			this.OutBut = new Button();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.saveFileDialog_0 = new SaveFileDialog();
			((ISupportInitialize)this.dataGridView3).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.dataGridView3.AllowUserToAddRows = false;
			this.dataGridView3.AllowUserToDeleteRows = false;
			this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Dock = DockStyle.Fill;
			this.dataGridView3.Location = new System.Drawing.Point(3, 33);
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.ReadOnly = true;
			this.dataGridView3.RowHeadersVisible = false;
			this.dataGridView3.RowTemplate.Height = 18;
			this.dataGridView3.RowTemplate.Resizable = DataGridViewTriState.False;
			this.dataGridView3.Size = new System.Drawing.Size(629, 230);
			this.dataGridView3.TabIndex = 1;
			this.dataGridView3.CellClick += new DataGridViewCellEventHandler(this.dataGridView3_CellClick);
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			this.OutBut.Location = new System.Drawing.Point(3, 3);
			this.OutBut.Name = "OutBut";
			this.OutBut.Size = new System.Drawing.Size(75, 23);
			this.OutBut.TabIndex = 2;
			this.OutBut.Text = "输出EXCEL";
			this.OutBut.UseVisualStyleBackColor = true;
			this.OutBut.Click += new EventHandler(this.OutBut_Click);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Controls.Add(this.OutBut, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView3, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(635, 266);
			this.tableLayoutPanel1.TabIndex = 3;
			this.saveFileDialog_0.DefaultExt = "xls";
			this.saveFileDialog_0.FileName = "hitanalyse1";
			this.saveFileDialog_0.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
			this.saveFileDialog_0.OverwritePrompt = false;
			this.saveFileDialog_0.RestoreDirectory = true;
			this.saveFileDialog_0.Title = "保存";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(635, 266);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$Icon");
			base.Name = "ResultDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "缓冲分析明细";
			base.TopMost = true;
			base.Load += new EventHandler(this.ResultDialog_Load);
			base.FormClosing += new FormClosingEventHandler(this.ResultDialog_FormClosing);
			((ISupportInitialize)this.dataGridView3).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

	
		private DataGridView dataGridView3;
		private Timer timer_0;
		private Button OutBut;
		private TableLayoutPanel tableLayoutPanel1;
		private SaveFileDialog saveFileDialog_0;
		private IGeometry igeometry_0;
    }
}