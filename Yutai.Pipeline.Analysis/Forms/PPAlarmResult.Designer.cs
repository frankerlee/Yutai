using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class PPAlarmResult
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
			this.icontainer_0 = new Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPAlarmResult));
			this.dataGridView3 = new DataGridView();
			this.timer_0 = new Timer(this.icontainer_0);
			((ISupportInitialize)this.dataGridView3).BeginInit();
			base.SuspendLayout();
			this.dataGridView3.AllowUserToAddRows = false;
			this.dataGridView3.AllowUserToDeleteRows = false;
			this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Dock = DockStyle.Fill;
			this.dataGridView3.Location = new System.Drawing.Point(0, 0);
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.ReadOnly = true;
			this.dataGridView3.RowHeadersVisible = false;
			this.dataGridView3.RowTemplate.Height = 18;
			this.dataGridView3.RowTemplate.Resizable = DataGridViewTriState.False;
			this.dataGridView3.Size = new Size(635, 266);
			this.dataGridView3.TabIndex = 1;
			this.dataGridView3.CellClick += new DataGridViewCellEventHandler(this.dataGridView3_CellClick);
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(635, 266);
			base.Controls.Add(this.dataGridView3);
			base.Icon = (Icon)resources.GetObject("$Icon");
			base.Name = "PPAlarmResult";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "多次爆点列表";
			base.TopMost = true;
			base.Load += new EventHandler(this.PPAlarmResult_Load);
			base.FormClosing += new FormClosingEventHandler(this.PPAlarmResult_FormClosing);
			((ISupportInitialize)this.dataGridView3).EndInit();
			base.ResumeLayout(false);
		}

	
		private DataGridView dataGridView3;
		private Timer timer_0;
		private IGeometry igeometry_0;
    }
}