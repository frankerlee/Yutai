using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;


namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class BaseQueryUI : Form
	{
		private partial class LayerboxItem
		{
			public IFeatureLayer m_pPipeLayer;

			public override string ToString()
			{
				return this.m_pPipeLayer.Name;
			}
		}

		public IGeometry m_ipGeo;
		public IAppContext m_context;
		public IMapControl3 MapControl;
		public IPipelineConfig pPipeCfg;
		public IGeometry SelectBound;
	    private PipelineAnalysisPlugin _plugin;
        public PipelineAnalysisPlugin Plugin
	    {
            set
            {
                _plugin = value;
            }
	    }
        public ushort DrawType;

		public ushort SelectType;



		private bool bSave = true;

		public object mainform;

        


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

		public string WindowText
		{
			set
			{
				this.Text = value;
			}
		}

		public BaseQueryUI()
		{
			this.InitializeComponent();
		}

		~BaseQueryUI()
		{
		}

		private void AddLayer(ILayer ipLay)
		{
			if (ipLay is IFeatureLayer)
			{
				this.AddFeatureLayer((IFeatureLayer)ipLay);
			}
			else if (ipLay is IGroupLayer)
			{
				this.AddGroupLayer((IGroupLayer)ipLay);
			}
		}

		private void AddGroupLayer(IGroupLayer iGLayer)
		{
			ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
			if (compositeLayer != null)
			{
				int count = compositeLayer.Count;
				for (int i = 0; i < count; i++)
				{
					ILayer ipLay = compositeLayer.get_Layer(i);
					this.AddLayer(ipLay);
				}
			}
		}

		private void AddFeatureLayer(IFeatureLayer iFLayer)
		{
			if (iFLayer != null)
			{
				string aliasName = iFLayer.FeatureClass.AliasName;
				if (this.Text == @"管点查询")
				{

					if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name,enumPipelineDataType.Point))
					{
						BaseQueryUI.LayerboxItem layerboxItem = new BaseQueryUI.LayerboxItem();
						layerboxItem.m_pPipeLayer = iFLayer;
						this.Layerbox.Items.Add(layerboxItem);
					}
				}
				else if (this.Text == "管线查询" && this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
				{
					BaseQueryUI.LayerboxItem layerboxItem2 = new BaseQueryUI.LayerboxItem();
					layerboxItem2.m_pPipeLayer = iFLayer;
					this.Layerbox.Items.Add(layerboxItem2);
				}
			}
		}

		private bool Fill()
		{
			this.SelectLayer = null;
			bool result;
			if (this.MapControl == null)
			{
				result = false;
			}
			else
			{
				this.SelectLayer = ((BaseQueryUI.LayerboxItem)this.Layerbox.SelectedItem).m_pPipeLayer;
				if (this.SelectLayer == null)
				{
					result = false;
				}
				else
				{
					this.myfields = this.SelectLayer.FeatureClass.Fields;
					this.FieldsBox.Items.Clear();
					for (int i = 0; i < this.myfields.FieldCount; i++)
					{
						IField field = this.myfields.get_Field(i);
						string name = field.Name;
						if (field.Type != (esriFieldType) 6 && field.Type != (esriFieldType) 7 && !(name.ToUpper() == "ENABLED") && !(name.ToUpper() == "SHAPE.LEN"))
						{
							this.FieldsBox.Items.Add(name);
						}
					}
					if (this.FieldsBox.Items.Count > 0)
					{
						this.FieldsBox.SelectedIndex = 0;
					}
					result = true;
				}
			}
			return result;
		}

		private void FieldValue()
		{
			if (this.myfields != null)
			{
				int num = this.myfields.FindField(this.FieldsBox.SelectedItem.ToString());
				IField field = this.myfields.get_Field(num);
				if (field.Type == (esriFieldType) 4)
				{
					this.BigeRaido.Enabled = false;
					this.Bigradio.Enabled = false;
					this.SmalelRadio.Enabled = false;
					this.SmallRadio.Enabled = false;
					this.LikeRadio.Enabled = true;
				}
				else
				{
					this.BigeRaido.Enabled = true;
					this.Bigradio.Enabled = true;
					this.SmalelRadio.Enabled = true;
					this.SmallRadio.Enabled = true;
					this.LikeRadio.Enabled = false;
				}
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.ValueBox.Items.Clear();
				this.ValueEdit.Text = "";
				while (feature != null)
				{
					object obj = feature.get_Value(num);
					string text;
					if (obj is DBNull)
					{
						text = "NULL";
					}
					else if (field.Type == (esriFieldType) 5)
					{
						text = Convert.ToDateTime(obj).ToShortDateString();
					}
					else
					{
						text = feature.get_Value(num).ToString();
						if (text.Length == 0)
						{
							text = "空字段值";
						}
					}
					if (!this.ValueBox.Items.Contains(text))
					{
						this.ValueBox.Items.Add(text);
					}
					if (this.ValueBox.Items.Count > 100)
					{
						break;
					}
					feature = featureCursor.NextFeature();
				}
			}
		}

		private void BaseQueryUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.Layerbox.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				this.AddLayer(ipLay);
			}
			if (this.Layerbox.Items.Count > 0)
			{
				this.Layerbox.SelectedIndex = 0;
				if (this.Fill())
				{
					this.FieldValue();
				}
			}
		}

		private void Layerbox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FieldsBox.Items.Clear();
			if (this.Fill())
			{
				this.FieldValue();
			}
		}

		private void FieldsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FieldValue();
		}

		private void QueryButton_Click(object sender, EventArgs e)
		{
			IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			ISpatialFilter spatialFilter = new SpatialFilter();
			IFeatureCursor pCursor = null;
			string text = this.ValueEdit.Text;
			if (text != "")
			{
				if (this.myfields == null)
				{
					return;
				}
				int num = this.myfields.FindField(this.FieldsBox.SelectedItem.ToString());
				IField field = this.myfields.get_Field(num);
				string text2 = this.FieldsBox.SelectedItem.ToString();
				if (text == "NULL")
				{
					if (this.Equalradio.Checked)
					{
						text2 += " IS NULL";
					}
					if (this.NoEqualRadio.Checked)
					{
						text2 = "NOT(" + text2 + " IS NULL)";
					}
					if (this.LikeRadio.Checked)
					{
						text2 += " LIKE NULL";
					}
				}
				else
				{
					if (this.Equalradio.Checked)
					{
						text2 += "=";
					}
					if (this.NoEqualRadio.Checked)
					{
						text2 += "<>";
					}
					if (this.SmallRadio.Checked)
					{
						text2 += "<";
					}
					if (this.SmalelRadio.Checked)
					{
						text2 += "<=";
					}
					if (this.Bigradio.Checked)
					{
						text2 += ">";
					}
					if (this.BigeRaido.Checked)
					{
						text2 += ">=";
					}
					if (this.LikeRadio.Checked)
					{
						text2 += " like ";
					}
					if (text == "空字段值")
					{
						text = "";
					}
					if (field.Type == (esriFieldType) 4)
					{
						if (this.LikeRadio.Checked)
						{
							IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
							if (dataset.Workspace.Type == (esriWorkspaceType) 2)
							{
								text2 += "'%";
								text2 += text;
								text2 += "%'";
							}
							else
							{
								text2 += "'*";
								text2 += text;
								text2 += "*'";
							}
						}
						else
						{
							text2 += "'";
							text2 += text;
							text2 += "'";
						}
					}
					else if (field.Type == (esriFieldType) 5)
					{
						if (this.SelectLayer.DataSourceType == "SDE Feature Class")
						{
							text2 += "TO_DATE('";
							text2 += text;
							text2 += "','YYYY-MM-DD')";
						}
						if (this.SelectLayer.DataSourceType == "Personal Geodatabase Feature Class")
						{
							text2 += "#";
							text2 += text;
							text2 += "#";
						}
					}
					else
					{
						text2 += text;
					}
				}
				spatialFilter.WhereClause=(text2);
			}
			else if (MessageBox.Show("末指定属性条件,是否查询?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			if (this.GeometrySet.Checked && this.m_ipGeo != null)
			{
				spatialFilter.Geometry=(this.m_ipGeo);
			}
			if (this.SelectType == 0)
			{
				spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
			}
			if (this.SelectType == 1)
			{
				spatialFilter.SpatialRel=(esriSpatialRelEnum) (7);
			}
			try
			{
				pCursor = featureClass.Search(spatialFilter, false);
			}
			catch (Exception)
			{
				MessageBox.Show("查询值有误,请检查!");
				return;
			}
            //修改为插件事件，因为结果显示窗体为插件拥有。
            _plugin.FireQueryResultChanged(new QueryResultArgs(pCursor, (IFeatureSelection)this.SelectLayer));
			
		}

		private void ByEnvelope_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = false;
			this.ByCircle.Checked = false;
			this.ByEnvelope.Checked = true;
			this.DrawType = 0;
			IActiveView activeView = m_context.ActiveView;
			this.MapControl.Refresh((esriViewDrawPhase) 32, Type.Missing, Type.Missing);
		}

		private void ByPolygon_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = true;
			this.ByCircle.Checked = false;
			this.ByEnvelope.Checked = false;
			this.DrawType = 1;
		}

		private void ByCircle_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = false;
			this.ByCircle.Checked = true;
			this.ByEnvelope.Checked = false;
			this.DrawType = 2;
		}

		private void CrossesSelect_Click(object sender, EventArgs e)
		{
			this.CrossesSelect.Checked = true;
			this.WithinSelect.Checked = false;
			this.SelectType = 0;
		}

		private void WithinSelect_Click(object sender, EventArgs e)
		{
			this.CrossesSelect.Checked = false;
			this.WithinSelect.Checked = true;
			this.SelectType = 1;
		}

		private void Clearbut_Click(object sender, EventArgs e)
		{
			this.m_ipGeo = null;
			this.MapControl.Refresh((esriViewDrawPhase) 32, null, null);
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			if (this.SelectLayer != null)
			{
				IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
				featureSelection.Clear();
				featureSelection.SelectionSet.Refresh();
				IActiveView activeView = m_context.ActiveView;
				activeView.Refresh();
			}
			this.GeometrySet.Checked = false;
			base.Close();
		}

		private void BaseQueryUI_VisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
				axMapControl.OnAfterDraw+= AxMapControlOnOnAfterDraw;
			}
			else
			{

                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw-= AxMapControlOnOnAfterDraw;
            }
		}

	    private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
	    {
           
            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }

	   

		protected override void OnClosed(EventArgs e)
		{
            IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            base.OnClosed(e);
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

		private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValueEdit.Text = this.ValueBox.SelectedItem.ToString();
		}

		private void BaseQueryUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "基本查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}


    }
}