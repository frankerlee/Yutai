
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class RelateQueryUI : Form
	{
		private partial class LayerboxItem
		{
			public IFeatureLayer m_pPipeLayer;

			public override string ToString()
			{
				return this.m_pPipeLayer.Name;
			}
		}


	    private PipelineAnalysisPlugin _plugin;

	    public PipelineAnalysisPlugin Plugin
	    {
            set { _plugin = value; }
	    }

        


		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipelineConfig pPipeCfg;


		private ArrayList m_alPipeLine = new ArrayList();

		private ArrayList m_alPipePoint = new ArrayList();

		public bool SelectGeometry
		{
			get
			{
				return this.GeometrySet.Checked;
			}
			set
			{
				this.GeometrySet.Checked = value;
			}
		}

	public RelateQueryUI()
		{
			this.InitializeComponent();
		}

		public void AutoFlash()
		{
			this.FillLayers();
		}

		public void AddName(ILayer pLayer)
		{
			if (pLayer is IFeatureLayer)
			{
				IFeatureLayer featureLayer = pLayer as IFeatureLayer;
				if (this.pPipeCfg.IsPipelineLayer(featureLayer.FeatureClass.AliasName))
				{
					RelateQueryUI.LayerboxItem layerboxItem = new RelateQueryUI.LayerboxItem();
					layerboxItem.m_pPipeLayer = featureLayer;
					this.cmbPipeLine.Items.Add(layerboxItem);
				}
				if (this.pPipeCfg.IsPipelineLayer(featureLayer.FeatureClass.AliasName))
				{
					RelateQueryUI.LayerboxItem layerboxItem2 = new RelateQueryUI.LayerboxItem();
					layerboxItem2.m_pPipeLayer = featureLayer;
					this.cmbPipePoint.Items.Add(layerboxItem2);
				}
			}
		}

		private void FillLayers()
		{
			this.cmbPipeLine.Items.Clear();
			this.cmbPipePoint.Items.Clear();
			this.cmbPipeLineFields.Items.Clear();
			this.cmbPipePointFields.Items.Clear();
			CommonUtils.ThrougAllLayer(m_context.FocusMap, new CommonUtils.DealLayer(this.AddName));
			if (this.cmbPipeLine.Items.Count > 0)
			{
				this.cmbPipeLine.SelectedIndex = 0;
			}
			if (this.cmbPipePoint.Items.Count > 0)
			{
				this.cmbPipePoint.SelectedIndex = 0;
			}
		}

		private void RelateQueryUI_Load(object sender, EventArgs e)
		{
			this.FillLayers();
		}

		private void cmbPipeLine_SelectedIndexChanged(object sender, EventArgs e)
		{
			RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
			IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
			this.FillLayerFieldsToCmb(pPipeLayer, this.cmbPipeLineFields);
			if (this.cmbPipeLine.Focused)
			{
				string text = this.cmbPipeLine.Text.Trim();
				if (text.Length >= 2)
				{
					int length = text.LastIndexOf("线");
					text = text.Substring(0, length);
					int count = this.cmbPipePoint.Items.Count;
					for (int i = 0; i < count; i++)
					{
						string text2 = this.cmbPipePoint.Items[i].ToString();
						int length2 = text2.LastIndexOf("点");
						text2 = text2.Substring(0, length2);
						if (text2.Equals(text))
						{
							this.cmbPipePoint.SelectedIndex = i;
						}
					}
				}
			}
		}

		private void cmbPipePoint_SelectedIndexChanged(object sender, EventArgs e)
		{
			RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipePoint.SelectedItem as RelateQueryUI.LayerboxItem;
			IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
			this.FillLayerFieldsToCmb(pPipeLayer, this.cmbPipePointFields);
			if (this.cmbPipePoint.Focused)
			{
				string text = this.cmbPipePoint.Text.Trim();
				if (text.Length >= 2)
				{
					int length = text.LastIndexOf("点");
					text = text.Substring(0, length);
					int count = this.cmbPipeLine.Items.Count;
					for (int i = 0; i < count; i++)
					{
						string text2 = this.cmbPipeLine.Items[i].ToString();
						int length2 = text2.LastIndexOf("线");
						text2 = text2.Substring(0, length2);
						if (text2.Equals(text))
						{
							this.cmbPipeLine.SelectedIndex = i;
						}
					}
				}
			}
		}

		private void FillLayerFieldsToCmb(IFeatureLayer pFeaLay, ComboBox cmbVal)
		{
			IFeatureClass featureClass = pFeaLay.FeatureClass;
			IFields fields = featureClass.Fields;
			int fieldCount = fields.FieldCount;
			Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
			cmbVal.Items.Clear();
			int num = fields.FindField(featureClass.ShapeFieldName);
			for (int i = 0; i < fieldCount; i++)
			{
				if (num != i)
				{
					string name = fields.get_Field(i).Name;
					if (regex.IsMatch(name))
					{
						cmbVal.Items.Add(name);
					}
				}
			}
			if (cmbVal.Items.Count > 0)
			{
				cmbVal.SelectedIndex = 0;
			}
		}

		private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, string strFieldName, ListBox lbVal)
		{
			IFeatureClass featureClass = pFeaLay.FeatureClass;
			IQueryFilter queryFilter = new QueryFilter();
			IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
			IFeature feature = featureCursor.NextFeature();
			int num = featureClass.Fields.FindField(strFieldName);
			if (num != -1)
			{
				lbVal.Items.Clear();
				while (feature != null)
				{
					object obj = feature.get_Value(num).ToString();
					if (obj != null && !Convert.IsDBNull(obj))
					{
						string value = obj.ToString();
						if (!lbVal.Items.Contains(value))
						{
							lbVal.Items.Add(obj);
						}
						feature = featureCursor.NextFeature();
					}
				}
			}
		}

		private void cmbPipeLineFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cmbPipeLine.Items.Count >= 1)
			{
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				if (pPipeLayer != null)
				{
					this.FillFieldValuesToListBox(pPipeLayer, this.cmbPipeLineFields.Text.Trim(), this.lstBoxPipeLineValues);
				}
			}
		}

		private void cmbPipePointFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cmbPipePoint.Items.Count >= 1)
			{
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipePoint.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				if (pPipeLayer != null)
				{
					this.FillFieldValuesToListBox(pPipeLayer, this.cmbPipePointFields.Text.Trim(), this.lstBoxPipePointValues);
				}
			}
		}

		private void btnPipeLineQuery_Click(object sender, EventArgs e)
		{
			if (this.cmbPipeLineFields.Text == "")
			{
				MessageBox.Show("请选择管线层查询字段！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管线层查询值！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管点层查询值！");
			}
			else
			{
				Splash.Show();
				Splash.Status = "状态:关联查询中,请稍候...";
				this.Walk();
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				IFeatureSelection featureSelection = (IFeatureSelection)pPipeLayer;
				featureSelection.Clear();
				ISelectionSet selectionSet = featureSelection.SelectionSet;
				selectionSet.IDs.Reset();
				int count = this.m_alPipeLine.Count;
				for (int i = 0; i < count; i++)
				{
					IFeature feature = this.m_alPipeLine[i] as IFeature;
					selectionSet.Add(feature.OID);
				}
				IQueryFilter queryFilter = new QueryFilter();
				ICursor cursor;
				selectionSet.Search(queryFilter, false, out cursor);
				IFeatureCursor pFeatureCursor = cursor as IFeatureCursor;
				Splash.Close();
				_plugin.FireQueryResultChanged(new QueryResultArgs(pFeatureCursor, featureSelection));
			}
		}

		private void btnPipePointQuery_Click(object sender, EventArgs e)
		{
			if (this.cmbPipeLineFields.Text == "")
			{
				MessageBox.Show("请选择管点层查询字段！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管线层查询值！");
			}
			else if (this.lstBoxPipeLineValues.Text == "")
			{
				MessageBox.Show("请指定管点层查询值！");
			}
			else
			{
				this.Walk();
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipePoint.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				IFeatureSelection featureSelection = (IFeatureSelection)pPipeLayer;
				featureSelection.Clear();
				ISelectionSet selectionSet = featureSelection.SelectionSet;
				selectionSet.IDs.Reset();
				int count = this.m_alPipePoint.Count;
				for (int i = 0; i < count; i++)
				{
					IFeature feature = this.m_alPipePoint[i] as IFeature;
					selectionSet.Add(feature.OID);
				}
				IQueryFilter queryFilter = new QueryFilter();
				ICursor cursor;
				selectionSet.Search(queryFilter, false, out cursor);
				IFeatureCursor pFeatureCursor = cursor as IFeatureCursor;
                _plugin.FireQueryResultChanged(new QueryResultArgs(pFeatureCursor, featureSelection));
            }
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.GeometrySet.Checked = false;
			base.Close();
		}

		private void Walk()
		{
			if (this.cmbPipeLine.Items.Count >= 1)
			{
				RelateQueryUI.LayerboxItem layerboxItem = this.cmbPipeLine.SelectedItem as RelateQueryUI.LayerboxItem;
				IFeatureLayer pPipeLayer = layerboxItem.m_pPipeLayer;
				if (pPipeLayer != null)
				{
					IFeatureClass featureClass = pPipeLayer.FeatureClass;
					ISpatialFilter spatialFilter = new SpatialFilter();
					int num = featureClass.Fields.FindField(this.cmbPipeLineFields.Text.Trim());
					if (featureClass.Fields.get_Field(num).Type == (esriFieldType) 4)
					{
						spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + " = '" + this.lstBoxPipeLineValues.Text.Trim() + "'";
					}
					else if (featureClass.Fields.get_Field(num).Type == (esriFieldType) 5)
					{
						if (pPipeLayer.DataSourceType == "Personal Geodatabase Feature Class")
						{
							spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + " = #" + this.lstBoxPipeLineValues.Text.Trim() + "#";
						}
						else
						{
							spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + "= TO_DATE('" + this.lstBoxPipeLineValues.Text.Trim() + "','YYYY-MM-DD')";
						}
					}
					else
					{
						spatialFilter.WhereClause=this.cmbPipeLineFields.Text.Trim() + " = " + this.lstBoxPipeLineValues.Text.Trim();
					}
					if (this.GeometrySet.Checked)
					{
						if (this.m_ipGeo != null)
						{
							spatialFilter.Geometry=(this.m_ipGeo);
						}
						spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
					}
					IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
					IFeature feature = featureCursor.NextFeature();
					int num2 = featureClass.Fields.FindField(this.cmbPipeLineFields.Text.Trim());
					if (num2 != -1)
					{
						this.m_alPipeLine.Clear();
						this.m_alPipePoint.Clear();
						while (feature != null)
						{
							if (this.JustifyPipeLine(feature))
							{
								this.m_alPipeLine.Add(feature);
							}
							feature = featureCursor.NextFeature();
						}
					}
				}
			}
		}

		private void QueryPipePoint()
		{
		}

		private bool JustifyPipeLine(IFeature pFeatureLine)
		{
			IEdgeFeature edgeFeature = null;
			if (pFeatureLine is IEdgeFeature)
			{
				edgeFeature = (pFeatureLine as IEdgeFeature);
			}
			IFeature feature = edgeFeature.FromJunctionFeature as IFeature;
			IFeature feature2 = edgeFeature.ToJunctionFeature as IFeature;
			IFields fields = feature.Fields;
			string text = this.cmbPipePointFields.Text.Trim();
			int num = fields.FindField(text);
			object obj = feature.get_Value(num);
			string a;
			if (obj == null || Convert.IsDBNull(obj))
			{
				a = "";
			}
			else
			{
				a = obj.ToString();
			}
			object obj2 = feature2.get_Value(num);
			string a2;
			if (obj2 == null || Convert.IsDBNull(obj2))
			{
				a2 = "";
			}
			else
			{
				a2 = obj2.ToString();
			}
			bool flag = a == this.lstBoxPipePointValues.Text.Trim();
			bool flag2 = a2 == this.lstBoxPipePointValues.Text.Trim();
			if (flag)
			{
				this.m_alPipePoint.Add(feature);
			}
			if (flag2)
			{
				this.m_alPipePoint.Add(feature2);
			}
			return flag || flag2;
		}

		private void lstBoxPipeLineValues_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void lstBoxPipePointValues_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void axMap_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
		{
			int viewDrawPhase = e.viewDrawPhase;
			if (viewDrawPhase == 32)
			{
				this.DrawSelGeometry();
			}
		}

		private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
		{
		    IMapControlEvents2_Event axMapControl = m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw-= AxMapControlOnOnAfterDraw;

            
			base.OnClosed(e);
		}

	    private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
	    {
            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }

	    public void DrawSelGeometry()
		{
			if (this.m_ipGeo != null)
			{
                IRgbColor rgbColor = new RgbColor();
                IColor selectionCorlor = this.m_context.Config.SelectionEnvironment.DefaultColor;
                rgbColor.RGB = selectionCorlor.RGB;
                rgbColor.Transparency = selectionCorlor.Transparency;

                object obj = null;
                int selectionBufferInPixels = this.m_context.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
				switch ((int)this.m_ipGeo.GeometryType)
				{
				case 1:
				{
					ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
					symbol = (ISymbol)simpleMarkerSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
					simpleMarkerSymbol.Color=(rgbColor);
					simpleMarkerSymbol.Size=((double)(selectionBufferInPixels + selectionBufferInPixels + selectionBufferInPixels));
					break;
				}
				case 3:
				{
					ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
					symbol = (ISymbol)simpleLineSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
					simpleLineSymbol.Color=(rgbColor);
					simpleLineSymbol.Color.Transparency=(1);
					simpleLineSymbol.Width=((double)selectionBufferInPixels);
					break;
				}
				case 4:
				case 5:
				{
					ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
					symbol = (ISymbol)simpleFillSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
					simpleFillSymbol.Color=(rgbColor);
					simpleFillSymbol.Color.Transparency=(1);
					break;
				}
				}
				obj = symbol;
				this.MapControl.DrawShape(this.m_ipGeo, ref obj);
			}
		}

		private void GeometrySet_Click_1(object sender, EventArgs e)
		{
			this.m_ipGeo = null;
			m_context.ActiveView.Refresh();
		}

		private void RelateQueryUI_VisibleChanged(object sender, EventArgs e)
		{
            if (base.Visible)
            {
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw += AxMapControlOnOnAfterDraw;
            }
            else
            {

                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            }
        }

		private void RelateQueryUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "关联查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}
	}
}
