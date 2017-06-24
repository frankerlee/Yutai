
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class StatReportformsUI : Form
	{

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipelineConfig pPipeCfg;

	public StatReportformsUI()
		{
			this.InitializeComponent();
		}
	}
}
