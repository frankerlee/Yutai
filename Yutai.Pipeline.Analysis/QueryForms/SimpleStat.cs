
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class SimpleStat : Form
	{
		private partial class LayerboxItem
		{
			public IFeatureLayer m_pPipeLayer;

			public override string ToString()
			{
				return this.m_pPipeLayer.Name;
			}
		}
        

		public ColumnHeader columnHeader4;

		public IGeometry m_ipGeo;

		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipelineConfig pPipeCfg;

		private DataTable Sumtable = new DataTable();
        
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

		public SimpleStat()
		{
			this.InitializeComponent();
		}

		public void AutoFlash()
		{
			this.checkedListBox1.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.get_Layer(i);
				this.AddLayer(ipLay);
			}
			if (this.checkedListBox1.Items.Count > 0)
			{
				this.checkedListBox1.SelectedIndex = 0;
			}
		}

		private void CloseBut_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private IFeatureLayer GetLayer(string layername)
		{
			int layerCount = m_context.FocusMap.LayerCount;
			IFeatureLayer featureLayer = null;
			IFeatureLayer result;
			if (this.MapControl == null)
			{
				result = null;
			}
			else
			{
				for (int i = 0; i < layerCount; i++)
				{
					ILayer layer = m_context.FocusMap.get_Layer(i);
					if (layer is IFeatureLayer)
					{
						string a = layer.Name.ToString();
						if (a == layername)
						{
							featureLayer = (IFeatureLayer)m_context.FocusMap.get_Layer(i);
							break;
						}
					}
					else if (layer is IGroupLayer)
					{
						ICompositeLayer compositeLayer = (ICompositeLayer)layer;
						if (compositeLayer != null)
						{
							int count = compositeLayer.Count;
							for (int j = 0; j < count; j++)
							{
								ILayer layer2 = compositeLayer.get_Layer(j);
								string a = layer2.Name.ToString();
								if (a == layername)
								{
									featureLayer = (IFeatureLayer)layer2;
									break;
								}
							}
						}
					}
				}
				result = featureLayer;
			}
			return result;
		}

		private void CalButton_Click(object sender, EventArgs e)
		{
		}

		private bool ColumnEqual(object A, object B)
		{
			return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
		}

		private void AllBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, true);
			}
			this.listBox1.Items.Clear();
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count != 0)
			{
				for (int j = 0; j < count; j++)
				{
					IFeatureLayer pPipeLayer = ((SimpleStat.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
					this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
				}
			}
		}

		private void NoneBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, false);
			}
			this.listBox1.Items.Clear();
		}

		private void RevBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				if (this.checkedListBox1.GetItemChecked(i))
				{
					this.checkedListBox1.SetItemChecked(i, false);
				}
				else
				{
					this.checkedListBox1.SetItemChecked(i, true);
				}
			}
			this.listBox1.Items.Clear();
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count != 0)
			{
				for (int j = 0; j < count; j++)
				{
					IFeatureLayer pPipeLayer = ((SimpleStat.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
					this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
				}
			}
		}

		private void SimpleStat_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void FillValue()
		{
			if (this.myfields != null)
			{
                IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(this.SelectLayer.FeatureClass);
                
			    string lineTableFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);//m_context.PipeConfig.GetLineTableFieldName("管径");
				int num = this.myfields.FindField(lineTableFieldName);
				this.myfield = this.myfields.get_Field(num);
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
				IQueryFilter queryFilter = new QueryFilter();
				IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
				IFeature feature = featureCursor.NextFeature();
				this.listBox1.Items.Clear();
				while (feature != null)
				{
					object obj = feature.get_Value(num);
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
					if (!this.listBox1.Items.Contains(text))
					{
						this.listBox1.Items.Add(text);
					}
					feature = featureCursor.NextFeature();
				}
			}
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

				if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name,enumPipelineDataType.Line))
				{
					SimpleStat.LayerboxItem layerboxItem = new SimpleStat.LayerboxItem();
					layerboxItem.m_pPipeLayer = iFLayer;
					this.checkedListBox1.Items.Add(layerboxItem);
				}
			}
		}

		private void CalButton_Click_1(object sender, EventArgs e)
		{
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count == 0)
			{
				MessageBox.Show("请选定需要统计的管线");
			}
			else
			{
				int rowCount = this.dataGridView1.RowCount;
				if (rowCount <= 0)
				{
					MessageBox.Show("请确定上下限的值，其值不能为空");
				}
				else if (this.dataGridView1[0, 0].Value == null && this.dataGridView1[1, 0].Value == null)
				{
					MessageBox.Show("没有确定管径的范围");
				}
				else
				{
					int num = 0;
					int num2 = -1;
					DataTable dataTable = new DataTable();
					dataTable.Columns.Clear();
					if (!dataTable.Columns.Contains("层名"))
					{
						dataTable.Columns.Add("层名", typeof(string));
					}
					if (!dataTable.Columns.Contains("统计范围"))
					{
						dataTable.Columns.Add("统计范围", typeof(string));
					}
					if (!dataTable.Columns.Contains("条数"))
					{
						dataTable.Columns.Add("条数", typeof(int));
					}
					if (!dataTable.Columns.Contains("长度"))
					{
						dataTable.Columns.Add("长度", typeof(double));
					}
					int count2 = this.dataGridView1.Rows.Count;
					for (int i = 0; i < count; i++)
					{
						for (int j = 0; j < count2; j++)
						{
							string text = (string)this.dataGridView1[0, j].Value;
							string text2 = (string)this.dataGridView1[1, j].Value;
							if (text == null || text2 == null)
							{
								MessageBox.Show("请确定上下限的值,其值不能为空");
								return;
							}
							double num3 = 0.0;
							double num4 = 0.0;
							try
							{
								num3 = Convert.ToDouble(text);
								num4 = Convert.ToDouble(text2);
							}
							catch (Exception)
							{
								MessageBox.Show("请确定上下限的值");
								return;
							}
							IFeatureLayer pPipeLayer = ((SimpleStat.LayerboxItem)this.checkedListBox1.CheckedItems[i]).m_pPipeLayer;
							IFields fields = pPipeLayer.FeatureClass.Fields;
                            IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(this.SelectLayer.FeatureClass);
                            string lineTableFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);// this.pPipeCfg.GetLineTableFieldName("管径");
							num = fields.FindField(lineTableFieldName);
							for (int k = 0; k < fields.FieldCount; k++)
							{
								IField field = fields.get_Field(k);
								if (field.Type == (esriFieldType) 7)
								{
									num2 = k;
									break;
								}
							}
							if (num >= 0)
							{
								string name = pPipeLayer.Name;
								ISpatialFilter spatialFilter = new SpatialFilter();
								IFeatureClass featureClass = pPipeLayer.FeatureClass;
								if (this.GeometrySet.Checked)
								{
									if (this.m_ipGeo != null)
									{
										spatialFilter.Geometry=(this.m_ipGeo);
									}
									spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
								}
								ISelectionSet selectionSet = featureClass.Select(spatialFilter, (esriSelectionType) 3, (esriSelectionOption) 1, null);
								ITableSort tableSort = new TableSort();
								tableSort.Fields=(lineTableFieldName);
								tableSort.SelectionSet=(selectionSet);
								tableSort.Sort(null);
								ICursor rows = tableSort.Rows;
								int num5 = 0;
								IRow row = rows.NextRow();
								double num6 = 0.0;
								while (row != null)
								{
									object obj = row.get_Value(num);
									double num7 = 0.0;
									if (obj is DBNull)
									{
										row = rows.NextRow();
									}
									else
									{
										try
										{
											num7 = Convert.ToDouble(obj);
										}
										catch (Exception)
										{
										}
										if (num7 >= num3 && num7 < num4)
										{
											IPolyline polyline = (IPolyline)row.get_Value(num2);
											IPointCollection pointCollection = (IPointCollection)polyline;
											double num8 = 0.0;
											for (int l = 0; l < pointCollection.PointCount - 1; l++)
											{
												IPoint point = pointCollection.get_Point(l);
												IPoint point2 = pointCollection.get_Point(l + 1);
												num8 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0) + Math.Pow(point.Z - point2.Z, 2.0));
											}
											num6 += num8;
											num5++;
										}
										row = rows.NextRow();
									}
								}
								object obj2 = text + "-" + text2;
								num6 = Math.Round(num6, 3);
								dataTable.Rows.Add(new object[]
								{
									name,
									obj2,
									num5,
									num6
								});
							}
						}
					}
					new ClassCollectResultForm
					{
						nType = 1,
						ResultTable = dataTable
					}.ShowDialog();
				}
			}
		}

		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.listBox1.Items.Clear();
			int count = this.checkedListBox1.CheckedItems.Count;
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					IFeatureLayer pPipeLayer = ((SimpleStat.LayerboxItem)this.checkedListBox1.CheckedItems[i]).m_pPipeLayer;
					this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
					int count2 = this.listBox1.Items.Count;
					for (int j = 0; j < count2; j++)
					{
						string a = this.listBox1.Items[j].ToString();
						if (a == "")
						{
							this.listBox1.Items.RemoveAt(j);
						}
					}
				}
			}
		}

		private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, ListBox lbVal)
		{
			IFeatureClass featureClass = pFeaLay.FeatureClass;
			IQueryFilter queryFilter = new QueryFilter();
			IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
			IFeature feature = featureCursor.NextFeature();
			int num = featureClass.Fields.FindField("管径");
			if (num != -1)
			{
				while (feature != null)
				{
					object obj = feature.get_Value(num).ToString();
					if (obj == null || Convert.IsDBNull(obj))
					{
						feature = featureCursor.NextFeature();
					}
					else
					{
						string text = obj.ToString();
						text = text.Trim();
						if (text == string.Empty)
						{
							feature = featureCursor.NextFeature();
						}
						else
						{
							if (!lbVal.Items.Contains(text))
							{
								lbVal.Items.Add(obj);
							}
							feature = featureCursor.NextFeature();
						}
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.checkedListBox1.Items.Clear();
			this.listBox1.Items.Clear();
			this.dataGridView1.Rows.Clear();
			this.GeometrySet.Checked = false;
			base.Close();
		}

		private void FillValueBox()
		{
			int num = this.myfields.FindField("管径");
			int num2 = -1;
			this.myfieldGJ = this.myfields.get_Field(num);
			IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			IQueryFilter queryFilter = new QueryFilter();
			IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
			IFeature feature = featureCursor.NextFeature();
			if (num2 >= 0 || num >= 0)
			{
				while (feature != null)
				{
					if (num >= 0)
					{
						object obj = feature.get_Value(num);
						if (!(obj is DBNull))
						{
							string text = obj.ToString();
							if (text.Length == 0)
							{
							}
						}
					}
					if (num2 >= 0)
					{
						object obj = feature.get_Value(num2);
						if (!(obj is DBNull))
						{
							string text = obj.ToString();
							if (text.Length == 0)
							{
							}
						}
						feature = featureCursor.NextFeature();
					}
				}
			}
		}

		private void ValidateField()
		{
			int selectedIndex = this.checkedListBox1.SelectedIndex;
			if (selectedIndex >= 0)
			{
				this.SelectLayer = null;
				if (this.MapControl != null)
				{
					this.SelectLayer = ((SimpleStat.LayerboxItem)this.checkedListBox1.SelectedItem).m_pPipeLayer;
					if (this.SelectLayer != null)
					{
                        IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(this.SelectLayer.FeatureClass);
                        this.myfields = this.SelectLayer.FeatureClass.Fields;
					    this.strGJ = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);// this.pPipeCfg.GetLineTableFieldName("管径");
					}
				}
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.listBox1.SelectedItem.ToString();
			if (this.dataGridView1.Rows.Count == 0)
			{
				this.dataGridView1.Rows.Add();
			}
		}

		private void dataGridView()
		{
			this.dataGridView1.DataSource = null;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.listBox1.Items.Count == 0)
			{
				MessageBox.Show("请选定要统计的管线");
			}
			else if (this.listBox1.SelectedItems.Count == 0)
			{
				MessageBox.Show("请选定需要的管径");
			}
			else
			{
				int count = this.dataGridView1.Rows.Count;
				if (count < 1)
				{
					MessageBox.Show("请用户添加行");
				}
				else
				{
					string value = this.listBox1.SelectedItem.ToString();
					double num = Convert.ToDouble(value);
					int index = this.dataGridView1.CurrentRow.Index;
					double num2 = Convert.ToDouble(this.dataGridView1[1, index].Value);
					if (num2 < num && num2 != 0.0)
					{
						MessageBox.Show("下限值不应大于上限值");
					}
					else
					{
						this.dataGridView1.CurrentRow.Cells[0].Value = value;
					}
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (this.listBox1.Items.Count == 0)
			{
				MessageBox.Show("请选定要统计的管线");
			}
			else if (this.listBox1.SelectedItems.Count == 0)
			{
				MessageBox.Show("请选定需要的管径");
			}
			else
			{
				int count = this.dataGridView1.Rows.Count;
				if (count < 1)
				{
					MessageBox.Show("请用户添加行");
				}
				else
				{
					string value = this.listBox1.SelectedItem.ToString();
					double num = Convert.ToDouble(value);
					int index = this.dataGridView1.CurrentRow.Index;
					double num2 = Convert.ToDouble(this.dataGridView1[0, index].Value);
					if (num2 > num)
					{
						MessageBox.Show("下限值不应大于上限值");
					}
					else
					{
						this.dataGridView1.CurrentRow.Cells[1].Value = value;
					}
				}
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("行数已为空");
			}
			else
			{
				this.dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			this.dataGridView1.Rows.Add();
		}

		private void InsertBut_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count == 0)
			{
				this.dataGridView1.Rows.Add();
			}
			else
			{
				this.dataGridView1.Rows.Insert(this.dataGridView1.CurrentRow.Index, new object[0]);
			}
		}

		private void SimpleStat_VisibleChanged(object sender, EventArgs e)
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

		private void GeometrySet_Click(object sender, EventArgs e)
		{
			if (!this.GeometrySet.Checked)
			{
				this.m_ipGeo = null;
				m_context.ActiveView.Refresh();
			}
		}

		private void SimpleStat_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "分段统计";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}


    }
}