using ApplicationData;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using PipeConfig;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class StatReportformsUI : Form
	{
		private IContainer components = null;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(292, 273);
			base.Name = "StatReportformsUI";
			this.Text = "统计报表";
			base.ResumeLayout(false);
		}

		public StatReportformsUI()
		{
			this.InitializeComponent();
		}
	}
}
