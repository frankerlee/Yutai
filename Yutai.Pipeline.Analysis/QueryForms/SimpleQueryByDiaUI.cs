
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class SimpleQueryByDiaUI : Form
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

		public IPipeConfig pPipeCfg;


        private PipelineAnalysisPlugin _plugin;
        public PipelineAnalysisPlugin Plugin
        {
            set
            {
                _plugin = value;
            }
        }





        public object mainform;

		public ushort DrawType;

		public ushort SelectType;


























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

		public SimpleQueryByDiaUI()
		{
			this.InitializeComponent();
		}

		private void SimpleQueryByDiaUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			this.OperateBox.SelectedIndex = 0;
			this.OperateBox2.SelectedIndex = 0;
			this.FillLayerBox();
			this.ValidateField();
			this.FillValueBox();
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
				if (this.pPipeCfg.IsPipeLine(aliasName))
				{
					SimpleQueryByDiaUI.LayerboxItem layerboxItem = new SimpleQueryByDiaUI.LayerboxItem();
					layerboxItem.m_pPipeLayer = iFLayer;
					this.LayerBox.Items.Add(layerboxItem);
				}
			}
		}

		private void FillLayerBox()
		{
			this.LayerBox.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer layer = m_context.FocusMap.get_Layer(i);
				if (layer.Valid)
				{
				}
				this.AddLayer(layer);
			}
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
			}
		}

		private void FillValueBox()
		{
			if (this.myfields != null)
			{
				int num = -1;
				if (this.radioButton1.Enabled)
				{
					num = this.myfields.FindField(this.strGJ);
				}
				int num2 = -1;
				if (this.radioButton2.Enabled)
				{
					num2 = this.myfields.FindField(this.strKG);
				}
				if (num >= 0)
				{
					this.myfieldGJ = this.myfields.get_Field(num);
				}
				else
				{
					this.radioButton1.Checked = true;
				}
				if (num2 >= 0)
				{
					this.myfieldKG = this.myfields.get_Field(num2);
					if (num < 0)
					{
						this.radioButton2.Checked = true;
					}
				}
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.ValueBox1.Items.Clear();
				this.ValueBox2.Items.Clear();
				this.ValueBox3.Items.Clear();
				if (num2 >= 0 || num >= 0)
				{
					while (feature != null)
					{
						if (num >= 0)
						{
							object obj = feature.get_Value(num);
							string text;
							if (obj is DBNull)
							{
								text = "0";
							}
							else
							{
								text = obj.ToString();
								if (text.Length == 0)
								{
									text = "0";
								}
							}
							if (!this.ValueBox1.Items.Contains(text))
							{
								this.ValueBox1.Items.Add(text);
								this.ValueBox2.Items.Add(text);
							}
						}
						if (num2 >= 0)
						{
							object obj = feature.get_Value(num2);
							string text;
							if (obj is DBNull)
							{
								text = "NULL";
							}
							else
							{
								text = obj.ToString();
								if (text.Length == 0)
								{
									text = "空字段值";
								}
							}
							if (!this.ValueBox3.Items.Contains(text))
							{
								this.ValueBox3.Items.Add(text);
							}
						}
						feature = featureCursor.NextFeature();
					}
					if (this.ValueBox1.Items.Count > 0)
					{
						this.ValueBox1.SelectedIndex = 0;
					}
					if (this.ValueBox2.Items.Count > 0)
					{
						this.ValueBox2.SelectedIndex = 0;
					}
					if (this.ValueBox3.Items.Count > 0)
					{
						this.ValueBox3.SelectedIndex = 0;
					}
				}
			}
		}

		private void ValidateField()
		{
			int selectedIndex = this.LayerBox.SelectedIndex;
			if (selectedIndex >= 0)
			{
				this.SelectLayer = null;
				if (this.MapControl != null)
				{
					this.SelectLayer = ((SimpleQueryByDiaUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
					if (this.SelectLayer != null)
					{
						this.myfields = this.SelectLayer.FeatureClass.Fields;
						this.strGJ = this.pPipeCfg.GetLineTableFieldName("管径");
						this.strKG = this.pPipeCfg.GetLineTableFieldName("断面尺寸");
						if (this.myfields.FindField(this.strGJ) < 0)
						{
							this.radioButton1.Enabled = false;
							this.OperateBox.Enabled = false;
							this.ValueBox1.Enabled = false;
							this.ValueBox2.Enabled = false;
							this.radioButton1.Checked = false;
						}
						else
						{
							this.radioButton1.Enabled = true;
							this.OperateBox.Enabled = true;
							this.ValueBox1.Enabled = true;
							this.ValueBox2.Enabled = true;
						}
						if (this.myfields.FindField(this.strKG) < 0)
						{
							this.radioButton2.Enabled = false;
							this.radioButton2.Checked = false;
							this.OperateBox2.Enabled = false;
							this.ValueBox3.Enabled = false;
						}
						else
						{
							this.radioButton2.Enabled = true;
							this.OperateBox2.Enabled = true;
							this.ValueBox3.Enabled = true;
						}
					}
				}
			}
		}

		private void OperateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.OperateBox.SelectedItem.ToString() == "介于")
			{
				this.label2.Visible = true;
				this.ValueBox2.Visible = true;
			}
			else
			{
				this.label2.Visible = false;
				this.ValueBox2.Visible = false;
			}
		}

		private void CloseBut_Click(object sender, EventArgs e)
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

		private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidateField();
			this.FillValueBox();
		}

		private void QueryBut_Click(object sender, EventArgs e)
		{
			if (!this.radioButton1.Checked && !this.radioButton2.Checked)
			{
				MessageBox.Show("系统设置有误/当前层不含有管径信息，请检查配置文件/数据！");
			}
			else if (!this.radioButton1.Enabled && !this.radioButton2.Enabled)
			{
				MessageBox.Show("系统设置有误/当前层不含有管径信息，请检查配置文件/数据！");
			}
			else
			{
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				ISpatialFilter spatialFilter = new SpatialFilter();
				IFeatureCursor pCursor = null;
				string text = "";
				if (this.radioButton1.Checked)
				{
					if (this.OperateBox.SelectedItem.ToString() == "介于")
					{
						double num = Convert.ToDouble(this.ValueBox1.Text);
						double num2 = Convert.ToDouble(this.ValueBox2.Text);
						string text2;
						string text3;
						if (num > num2)
						{
							text2 = this.ValueBox1.Text;
							text3 = this.ValueBox2.Text;
						}
						else
						{
							text3 = this.ValueBox1.Text;
							text2 = this.ValueBox2.Text;
						}
						if (text3 == text2)
						{
							if (this.myfieldGJ.Type == (esriFieldType) 4)
							{
								text = this.strGJ;
								text += " = '";
								text += text3;
								text += "'";
							}
							else
							{
								text = this.strGJ;
								text += text3;
							}
						}
						else if (this.myfieldGJ.Type == (esriFieldType) 4)
						{
							text = this.strGJ;
							text += ">= '";
							text += text3;
							text += "' and ";
							text += this.strGJ;
							text += "<= '";
							text += text2;
							text += "'";
						}
						else
						{
							text = this.strGJ;
							text += ">=";
							text += text3;
							text += " and ";
							text += this.strGJ;
							text += "<=";
							text += text2;
						}
					}
					else
					{
						text += this.strGJ;
						switch (this.OperateBox.SelectedIndex)
						{
						case 0:
							text += ">";
							break;
						case 1:
							text += ">=";
							break;
						case 2:
							text += "=";
							break;
						case 3:
							text += "<>";
							break;
						case 4:
							text += "<";
							break;
						case 5:
							text += "<=";
							break;
						}
						if (this.myfieldGJ.Type == (esriFieldType) 4)
						{
							text += "'";
							text += this.ValueBox1.Text;
							text += "'";
						}
						else
						{
							text += this.ValueBox1.Text;
						}
					}
				}
				else if (this.radioButton2.Checked)
				{
					text = this.strKG;
					switch (this.OperateBox2.SelectedIndex)
					{
					case 0:
						text += "=";
						break;
					case 1:
						text += "<>";
						break;
					}
					if (this.ValueBox3.Text == "空字段值")
					{
						text += "''";
					}
					else
					{
						text += "'";
						text += this.ValueBox3.Text;
						text += "'";
					}
				}
				spatialFilter.WhereClause=text;
				try
				{
					if (this.GeometrySet.Checked && this.m_ipGeo != null)
					{
						spatialFilter.Geometry=(this.m_ipGeo);
					}
					if (this.SelectType == 0)
					{
						spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
					}
					else if (this.SelectType == 1)
					{
						spatialFilter.SpatialRel=(esriSpatialRelEnum) (7);
					}
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
		}

		private void lable_Click(object sender, EventArgs e)
		{
		}

		private void ValueBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void ByEnvelope_Click(object sender, EventArgs e)
		{
			this.ByPolygon.Checked = false;
			this.ByCircle.Checked = false;
			this.ByEnvelope.Checked = true;
			this.DrawType = 0;
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

		private void SimpleQueryByDiaUI_VisibleChanged(object sender, EventArgs e)
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

        private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
        {

            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }



   
        private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
		{
            IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
		    axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;

            this.OnClosed(e);
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

		private void SimpleQueryByDiaUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "快速查询";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}


    }
}