using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class PreAlarmDlg : Form
	{
		private IContainer icontainer_0 = null;


	    public IPipelineConfig _config;


        public IAppContext m_iApp;

		public IMapControl3 MapControl;

		public IPipelineConfig pPipeCfg;


		public IAppContext App
		{
			set
			{
				this.m_iApp = value;
				this.MapControl = this.m_iApp.MapControl as IMapControl3;
				
			}
		}

	    public PreAlarmDlg(IAppContext context,IPipelineConfig config)
		{
			this.InitializeComponent();
            m_iApp = context;
		    _config = config;
		}

		private void btnAnalyse_Click(object obj, EventArgs eventArgs)
		{
            CheckListFeatureLayerItem pclass = this.LayerBox.SelectedItem as CheckListFeatureLayerItem;
			this.DeleteAllElements(this.m_iApp.ActiveView);
			if (this.preAlarmResult_0 == null)
			{
				this.preAlarmResult_0 = new PreAlarmResult(m_iApp,_config);
				this.preAlarmResult_0.App = this.m_iApp;
				this.preAlarmResult_0.m_pCurLayer = pclass.m_pFeatureLayer;
				this.preAlarmResult_0.m_strLayerName = this.LayerBox.Text;
				this.preAlarmResult_0.m_nExpireTime = (int)Convert.ToSingle(this.txBoxExpireTime.Text.Trim());
				this.preAlarmResult_0.Show();
			}
			else if (this.preAlarmResult_0.Visible)
			{
				this.preAlarmResult_0.m_strLayerName = this.LayerBox.Text;
				this.preAlarmResult_0.m_pCurLayer = pclass.m_pFeatureLayer;
				this.preAlarmResult_0.m_nExpireTime = (int)Convert.ToSingle(this.txBoxExpireTime.Text.Trim());
				this.preAlarmResult_0.ThrougAllLayer();
			}
			else
			{
				this.preAlarmResult_0.m_strLayerName = this.LayerBox.Text;
				this.preAlarmResult_0.m_pCurLayer = pclass.m_pFeatureLayer;
				this.preAlarmResult_0.m_nExpireTime = (int)Convert.ToSingle(this.txBoxExpireTime.Text.Trim());
				this.preAlarmResult_0.ThrougAllLayer();
				this.preAlarmResult_0.Visible = true;
			}
		}

		private void btnClose_Click(object obj, EventArgs eventArgs)
		{
			base.Visible = false;
		}

		private void PreAlarmDlg_Load(object obj, EventArgs eventArgs)
		{
			this.FillLayers();
		}

		public void AddName(ILayer pLayer)
		{
			if (pLayer is IFeatureLayer)
			{
				IFeatureLayer featureLayer = pLayer as IFeatureLayer;
				if (this.pPipeCfg.IsPipelineLayer(featureLayer.FeatureClass))
				{
                    CheckListFeatureLayerItem pclass = new CheckListFeatureLayerItem();
					pclass.m_pFeatureLayer = featureLayer;
					this.LayerBox.Items.Add(pclass);
				}
			}
		}

		private void FillLayers()
		{
			this.LayerBox.Items.Clear();
			CommonUtils.ThrougAllLayer(this.m_iApp.FocusMap, new CommonUtils.DealLayer(this.AddName));
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
			}
		
		}

		private void LayerBox_SelectedIndexChanged(object obj, EventArgs eventArgs)
		{
			
		}


		//private void method_2(IMap map)
		//{
		//	int layerCount = map.LayerCount;
		//	for (int i = 0; i < layerCount; i++)
		//	{
		//		ILayer layer = map.get_Layer(i);
		//		if (layer is ICompositeLayer)
		//		{
		//			this.VerifyLayer(layer);
		//		}
		//		else
		//		{
		//			this.ValidatePipeLayer((IFeatureLayer)layer);
		//		}
		//	}
		//}

		public ILayer VerifyLayer(ILayer pLayVal)
		{
			ICompositeLayer compositeLayer = pLayVal as ICompositeLayer;
			int count = compositeLayer.Count;
			for (int i = 0; i < count; i++)
			{
				ILayer layer = compositeLayer.get_Layer(i);
				if (layer is ICompositeLayer)
				{
					this.VerifyLayer(layer);
				}
				else
				{
					this.ValidatePipeLayer((IFeatureLayer)layer);
				}
			}
			return null;
		}

		private void ValidatePipeLayer(IFeatureLayer featureLayer)
		{
		}

		public static void NewPointElement(IActiveView pView, IPoint pPoint)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) (IActiveView) pView;
			
			ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
			ISimpleMarkerSymbol arg_3D_0 = simpleMarkerSymbol;
			IRgbColor rgbColorClass = new RgbColor();
			rgbColorClass.Red=(0);
			rgbColorClass.Green=(255);
			rgbColorClass.Blue=(255);
			arg_3D_0.Color=(rgbColorClass);
			simpleMarkerSymbol.Size=(8.0);
			simpleMarkerSymbol.Style=(0);
			IGraphicsContainer arg_68_0 = graphicsContainer;
			ILineElement lineElementClass = new LineElement() as ILineElement;
			((IElement)lineElementClass).Geometry=(pPoint);
			arg_68_0.AddElement(lineElementClass as IElement, 0);
            ((IActiveView)pView).Refresh();
		}

		//public void NewLineElement(IActiveView pView, IPolyline pPolyLine)
		//{
		//    IGraphicsContainer graphicsContainer = (IGraphicsContainer) pView;
			
		//	ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
		//	ISimpleLineSymbol arg_3D_0 = simpleLineSymbol;
		//	IRgbColor rgbColorClass = new RgbColor();
		//	rgbColorClass.Red=(0);
		//	rgbColorClass.Green=(255);
		//	rgbColorClass.Blue=(255);
		//	arg_3D_0.Color=(rgbColorClass);
		//	simpleLineSymbol.Width=(8.0);
		//	simpleLineSymbol.Style=(esriSimpleLineStyle) (2);
		//	IGraphicsContainer arg_68_0 = graphicsContainer;
		//	ILineElement lineElementClass = new LineElement() as ILineElement;
  //          ((IElement)lineElementClass).Geometry=(pPolyLine);
		//	arg_68_0.AddElement(lineElementClass as IElement, 0);
		//	pView.Refresh();
		//}

		public void DeleteAllElements(IActiveView pView)
		{
			IGraphicsContainer graphicsContainer = (IGraphicsContainer)pView;
			graphicsContainer.DeleteAllElements();
		    IActiveView activeView = pView;
            activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			activeView.Refresh();
		}

		//private bool aelhLaYb1(IFeatureLayer featureLayer)
		//{
		//	ILayerFields layerFields = (ILayerFields)featureLayer;
		//	int num = layerFields.FindField("建设年代");
		//	return num > 0;
		//}

		private void PreAlarmDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArgs)
		{
			base.Visible = false;
			formClosingEventArgs.Cancel = true;
		}

		private void txBoxExpireTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			CommonUtils.NumberText_KeyPress(sender, e);
		}

		private void PreAlarmDlg_HelpRequested(object obj, HelpEventArgs helpEventArgs)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "预警分析";
			Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
		}
	}
}
