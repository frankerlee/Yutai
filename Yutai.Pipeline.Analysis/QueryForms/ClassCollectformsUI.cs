using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class ClassCollectformsUI : Form
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

		public ClassCollectformsUI()
		{
			this.InitializeComponent();
		}

		private void ClassCollectformsUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
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
					ILayer ipLay = compositeLayer.Layer[i];
					this.AddLayer(ipLay);
				}
			}
		}

		private void AddFeatureLayer(IFeatureLayer iFLayer)
		{
			if (iFLayer != null)
			{
				string aliasName = iFLayer.FeatureClass.AliasName;
				if (this.PointRadio.Checked)
				{
					if (this.pPipeCfg.IsPipelineLayer(aliasName))
					{
						ClassCollectformsUI.LayerboxItem layerboxItem = new ClassCollectformsUI.LayerboxItem();
						layerboxItem.m_pPipeLayer = iFLayer;
						this.Layerbox.Items.Add(layerboxItem);
					}
				}
				else if (this.pPipeCfg.IsPipelineLayer(aliasName))
				{
					ClassCollectformsUI.LayerboxItem layerboxItem2 = new ClassCollectformsUI.LayerboxItem();
					layerboxItem2.m_pPipeLayer = iFLayer;
					this.Layerbox.Items.Add(layerboxItem2);
				}
			}
		}

		public void AutoFlash()
		{
			this.Layerbox.Items.Clear();
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.Layer[i];
				this.AddLayer(ipLay);
			}
			if (this.Layerbox.Items.Count > 0)
			{
				this.Layerbox.SelectedIndex = 0;
				this.listBox1.Items.Clear();
				this.UpBut.Enabled = false;
				this.DownBut.Enabled = false;
				IFeatureLayer pPipeLayer = ((ClassCollectformsUI.LayerboxItem)this.Layerbox.SelectedItem).m_pPipeLayer;
				IFields fields = pPipeLayer.FeatureClass.Fields;
				this.FieldsBox.Items.Clear();
				for (int j = 0; j < fields.FieldCount; j++)
				{
					IField field = fields.Field[j];
					string name = field.Name;
					if (field.Type != (esriFieldType) 6 && field.Type != (esriFieldType) 7 && name != "Enabled")
					{
						Regex regex = new Regex("^[\\u4e00-\\u9fa5A-Za-z0-9]+$");
						if (regex.IsMatch(name))
						{
							this.FieldsBox.Items.Add(name);
						}
					}
				}
				if (this.FieldsBox.Items.Count > 0)
				{
					this.FieldsBox.SelectedIndex = 0;
				}
			}
		}

		private void PointRadio_CheckedChanged(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		private void CloseBut_Click(object sender, EventArgs e)
		{
			this.PointRadio.Checked = true;
			this.GeometrySet.Checked = false;
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
					ILayer layer = m_context.FocusMap.Layer[i];
					if (layer is IFeatureLayer)
					{
						string a = layer.Name;
						if (a == layername)
						{
							featureLayer = (IFeatureLayer)m_context.FocusMap.Layer[i];
							break;
						}
					}
					else if (layer is IGroupLayer)
				    {
				        ICompositeLayer compositeLayer = (ICompositeLayer) layer;
				        int count = compositeLayer.Count;
				        for (int j = 0; j < count; j++)
				        {
				            ILayer layer2 = compositeLayer.Layer[j];
				            string a = layer2.Name;
				            if (a == layername)
				            {
				                featureLayer = (IFeatureLayer) layer2;
				                break;
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
			Splash.Show();
			int count = this.Layerbox.CheckedItems.Count;
			int num = -1;
			DataTable dataTable = new DataTable();
			dataTable.Columns.Clear();
			Splash.Status = "状态: 正在汇总,请稍候...";
			if (this.PointRadio.Checked)
			{
				if (!dataTable.Columns.Contains("层名"))
				{
					dataTable.Columns.Add("层名", typeof(string));
				}
				if (!dataTable.Columns.Contains("点性"))
				{
					dataTable.Columns.Add("点性", typeof(string));
				}
				if (!dataTable.Columns.Contains("个数"))
				{
					dataTable.Columns.Add("个数", typeof(int));
				}
				if (!dataTable.Columns.Contains("总数"))
				{
					dataTable.Columns.Add("总数", typeof(int));
				}
				int num2 = 0;
				for (int i = 0; i < count; i++)
				{
					int num3 = 0;
					Splash.Status = "状态: 正在汇总" + this.Layerbox.CheckedItems[i] + ",请稍候...";
					IFeatureLayer pPipeLayer = ((ClassCollectformsUI.LayerboxItem)this.Layerbox.CheckedItems[i]).m_pPipeLayer;
					IFields fields = pPipeLayer.FeatureClass.Fields;
				    IYTField fieldInfo = pPipeCfg.GetSpecialField(pPipeLayer.FeatureClass.AliasName,
				        PipeConfigWordHelper.PointWords.TZW);
					string pointTableFieldName = fieldInfo.Name;
					int num4 = fields.FindField(pointTableFieldName);
					if (num4 >= 0)
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
						tableSort.Fields=(pointTableFieldName);
						tableSort.SelectionSet=(selectionSet);
						tableSort.Sort(null);
						ICursor rows = tableSort.Rows;
						object obj = null;
						int num5 = 1;
						for (IRow row = rows.NextRow(); row != null; row = rows.NextRow())
						{
							object obj2 = row.Value[num4];
							if (obj == null || !this.ColumnEqual(obj, obj2))
							{
								if (obj == null)
								{
									obj = obj2;
								}
								else
								{
									dataTable.Rows.Add(new object[]
									{
										name,
										obj,
										num5
									});
									num3 += num5;
									obj = obj2;
									num5 = 1;
								}
							}
							else
							{
								num5++;
							}
						}
						num3 += num5;
						num2 += num3;
						dataTable.Rows.Add(new object[]
						{
							name,
							obj,
							num5,
							num3
						});
					}
				}
				object[] array = new object[4];
				array.SetValue("合计", 1);
				array.SetValue(num2, 3);
				dataTable.Rows.Add(array);
			}
			else
			{
				if (!dataTable.Columns.Contains("层名"))
				{
					dataTable.Columns.Add("层名", typeof(string));
				}
				if (!dataTable.Columns.Contains("规格"))
				{
					dataTable.Columns.Add("规格", typeof(string));
				}
				if (!dataTable.Columns.Contains("材质"))
				{
					dataTable.Columns.Add("材质", typeof(string));
				}
				if (!dataTable.Columns.Contains("条数"))
				{
					dataTable.Columns.Add("条数", typeof(int));
				}
				if (!dataTable.Columns.Contains("长度"))
				{
					dataTable.Columns.Add("长度", typeof(double));
				}
				if (!dataTable.Columns.Contains("总条数"))
				{
					dataTable.Columns.Add("总条数", typeof(int));
				}
				if (!dataTable.Columns.Contains("总长度"))
				{
					dataTable.Columns.Add("总长度", typeof(double));
				}
				int num6 = 0;
				double num7 = 0.0;
				for (int j = 0; j < count; j++)
				{
					int num8 = 0;
					double num9 = 0.0;
					Splash.Status = "状态: 正在汇总" + this.Layerbox.CheckedItems[j] + ",请稍候...";
					IFeatureLayer pPipeLayer = ((ClassCollectformsUI.LayerboxItem)this.Layerbox.CheckedItems[j]).m_pPipeLayer;
					IFields fields2 = pPipeLayer.FeatureClass.Fields;
                    IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(pPipeLayer.FeatureClass);
				    string lineTableFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);

                    int num4 = fields2.FindField(lineTableFieldName);
					string lineTableFieldName2 = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.DMCC);// this.pPipeCfg.GetLineTableFieldName("断面尺寸");
					int num10 = fields2.FindField(lineTableFieldName2);
					string lineTableFieldName3 = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GXCZ);// this.pPipeCfg.GetLineTableFieldName("材质");
					int num11 = fields2.FindField(lineTableFieldName3);
					for (int k = 0; k < fields2.FieldCount; k++)
					{
						IField field = fields2.Field[k];
						if (field.Type == (esriFieldType) 7)
						{
							num = k;
							break;
						}
					}
					if (num4 >= 0 || num10 >= 0)
					{
						string name = pPipeLayer.Name;
						ISpatialFilter spatialFilter2 = new SpatialFilter();
						IFeatureClass featureClass2 = pPipeLayer.FeatureClass;
						if (this.GeometrySet.Checked)
						{
							if (this.m_ipGeo != null)
							{
								spatialFilter2.Geometry=(this.m_ipGeo);
							}
							spatialFilter2.SpatialRel=(esriSpatialRelEnum) (1);
						}
						ISelectionSet selectionSet2 = featureClass2.Select(spatialFilter2, (esriSelectionType) 3, (esriSelectionOption) 1, null);
						ITableSort tableSort2 = new TableSort();
						tableSort2.Fields=(string.Concat(new string[]
						{
							lineTableFieldName,
							",",
							lineTableFieldName2,
							",",
							lineTableFieldName3
						}));
						tableSort2.SelectionSet=(selectionSet2);
						tableSort2.Sort(null);
						ICursor rows2 = tableSort2.Rows;
						object obj3 = null;
						object obj4 = null;
						int num12 = 1;
						IRow row2 = rows2.NextRow();
						double num13 = 0.0;
						while (row2 != null)
						{
							object obj5 = row2.Value[num4];
							if (obj5 is DBNull || obj5.ToString() == "0")
							{
								obj5 = row2.Value[num10];
							}
							object obj6 = row2.Value[num11];
							if (row2.Value[num] is DBNull)
							{
								row2 = rows2.NextRow();
							}
							else
							{
								IPolyline polyline = (IPolyline)row2.Value[num];
								IPointCollection pointCollection = (IPointCollection)polyline;
								double num14 = 0.0;
								for (int l = 0; l < pointCollection.PointCount - 1; l++)
								{
									IPoint point = pointCollection.Point[l];
									IPoint point2 = pointCollection.Point[l + 1];
									num14 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0) + Math.Pow(point.Z - point.M - point2.Z + point2.M, 2.0));
								}
								if (obj3 == null || !this.ColumnEqual(obj3, obj5))
								{
									if (obj3 == null)
									{
										obj3 = obj5;
										num13 += num14;
									}
									else
									{
										num13 = Math.Round(num13, 3);
										dataTable.Rows.Add(new object[]
										{
											name,
											obj3,
											obj4,
											num12,
											num13
										});
										num8 += num12;
										num9 += num13;
										obj3 = obj5;
										obj4 = null;
										num13 = num14;
										num12 = 1;
									}
								}
								else if (obj4 == null || !this.ColumnEqual(obj4, obj6))
								{
									if (obj4 == null)
									{
										obj4 = obj6;
										num13 += num14;
										num12++;
									}
									else
									{
										num13 = Math.Round(num13, 3);
										dataTable.Rows.Add(new object[]
										{
											name,
											obj3,
											obj4,
											num12,
											num13
										});
										num8 += num12;
										num9 += num13;
										num13 = num14;
										obj4 = obj6;
										num12 = 1;
									}
								}
								else
								{
									num13 += num14;
									num12++;
								}
								row2 = rows2.NextRow();
							}
						}
						num8 += num12;
						num9 += num13;
						num6 += num8;
						num7 += num9;
						num13 = Math.Round(num13, 3);
						num9 = Math.Round(num9, 3);
						dataTable.Rows.Add(new object[]
						{
							name,
							obj3,
							obj4,
							num12,
							num13,
							num8,
							num9
						});
					}
				}
				num7 = Math.Round(num7, 3);
				object[] array2 = new object[7];
				array2.SetValue("合计", 1);
				array2.SetValue(num6, 5);
				array2.SetValue(num7, 6);
				dataTable.Rows.Add(array2);
			}
			Splash.Close();
			ClassCollectResultForm classCollectResultForm = new ClassCollectResultForm();
			if (this.PointRadio.Checked)
			{
				classCollectResultForm.nType = 0;
			}
			else
			{
				classCollectResultForm.nType = 1;
			}
			classCollectResultForm.ResultTable = dataTable;
			classCollectResultForm.ShowDialog();
		}

		private bool ColumnEqual(object A, object B)
		{
			return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
		}

		private void AllBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.Layerbox.Items.Count; i++)
			{
				this.Layerbox.SetItemChecked(i, true);
			}
		}

		private void NoneBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.Layerbox.Items.Count; i++)
			{
				this.Layerbox.SetItemChecked(i, false);
			}
		}

		private void RevBut_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.Layerbox.Items.Count; i++)
			{
				if (this.Layerbox.GetItemChecked(i))
				{
					this.Layerbox.SetItemChecked(i, false);
				}
				else
				{
					this.Layerbox.SetItemChecked(i, true);
				}
			}
		}

		private void Layerbox_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void AddBut_Click(object sender, EventArgs e)
		{
			if (!this.listBox1.Items.Contains(this.FieldsBox.Text))
			{
				this.listBox1.Items.Add(this.FieldsBox.Text);
			}
		}

		private void UpBut_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			string value = this.listBox1.Items[selectedIndex - 1].ToString();
			this.listBox1.Items[selectedIndex - 1] = this.listBox1.Items[selectedIndex].ToString();
			this.listBox1.Items[selectedIndex] = value;
			this.listBox1.SelectedIndex = selectedIndex - 1;
		}

		private void DownBut_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			string value = this.listBox1.Items[selectedIndex + 1].ToString();
			this.listBox1.Items[selectedIndex + 1] = this.listBox1.Items[selectedIndex].ToString();
			this.listBox1.Items[selectedIndex] = value;
			this.listBox1.SelectedIndex = selectedIndex + 1;
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpBut.Enabled = true;
			this.DownBut.Enabled = true;
			if (this.listBox1.Items.Count == 0)
			{
				this.UpBut.Enabled = false;
				this.DownBut.Enabled = false;
			}
			if (this.listBox1.SelectedIndex == 0)
			{
				this.UpBut.Enabled = false;
			}
			if (this.listBox1.SelectedIndex == this.listBox1.Items.Count - 1)
			{
				this.DownBut.Enabled = false;
			}
		}

		private void Calbut2_Click(object sender, EventArgs e)
		{
			if (this.listBox1.Items.Count < 1)
			{
				MessageBox.Show(@"请添加分类项!");
			}
			else
			{
				Splash.Show();
				int count = this.Layerbox.CheckedItems.Count;
				int num = -1;
				DataTable dataTable = new DataTable();
				dataTable.Columns.Clear();
				Splash.Status = @"状态: 正在汇总,请稍候...";
				string text = "";
				if (this.PointRadio.Checked)
				{
					if (!dataTable.Columns.Contains("层名"))
					{
						dataTable.Columns.Add("层名", typeof(string));
					}
					for (int i = 0; i < this.listBox1.Items.Count; i++)
					{
						if (!dataTable.Columns.Contains(this.listBox1.Items[i].ToString()))
						{
							dataTable.Columns.Add(this.listBox1.Items[i].ToString(), typeof(string));
							text += this.listBox1.Items[i].ToString();
							if (i < this.listBox1.Items.Count - 1)
							{
								text += ",";
							}
						}
					}
					if (!dataTable.Columns.Contains("个数"))
					{
						dataTable.Columns.Add("个数", typeof(int));
					}
					if (!dataTable.Columns.Contains("总数"))
					{
						dataTable.Columns.Add("总数", typeof(int));
					}
					int num2 = 0;
					int count2 = this.listBox1.Items.Count;
					for (int j = 0; j < count; j++)
					{
						int num3 = 0;
						Splash.Status = "状态: 正在汇总" + this.Layerbox.CheckedItems[j] + ",请稍候...";
						IFeatureLayer pPipeLayer = ((ClassCollectformsUI.LayerboxItem)this.Layerbox.CheckedItems[j]).m_pPipeLayer;
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
						string name = pPipeLayer.Name;
						ITableSort tableSort = new TableSort();
						tableSort.Fields=(text);
						tableSort.SelectionSet=(selectionSet);
						tableSort.Sort(null);
						ICursor rows = tableSort.Rows;
						object[] array = new object[count2];
						object[] array2 = new object[count2];
						int num4 = 1;
						IRow row = rows.NextRow();
						if (row != null)
						{
							for (int k = 0; k < count2; k++)
							{
								int num5 = row.Fields.FindField(this.listBox1.Items[k].ToString());
								array2[k] = row.Value[num5];
							}
							for (row = rows.NextRow(); row != null; row = rows.NextRow())
							{
								for (int l = 0; l < count2; l++)
								{
									int num6 = row.Fields.FindField(this.listBox1.Items[l].ToString());
									array[l] = row.Value[num6];
								}
								bool flag = false;
								for (int m = 0; m < count2; m++)
								{
									if (!this.ColumnEqual(array2[m], array[m]))
									{
										object[] array3 = new object[count2 + 2];
										array3.SetValue(name, 0);
										for (int n = 0; n < count2; n++)
										{
											array3.SetValue(array2[n], n + 1);
										}
										array3.SetValue(num4, count2 + 1);
										dataTable.Rows.Add(array3);
										num3 += num4;
										for (int num7 = m; num7 < count2; num7++)
										{
											array2[num7] = array[num7];
										}
										num4 = 1;
										flag = false;
										break;
									}
									flag = true;
								}
								if (flag)
								{
									num4++;
								}
							}
							num3 += num4;
							num2 += num3;
							object[] array4 = new object[count2 + 3];
							array4.SetValue(name, 0);
							for (int num8 = 0; num8 < count2; num8++)
							{
								array4.SetValue(array2[num8], num8 + 1);
							}
							array4.SetValue(num4, count2 + 1);
							array4.SetValue(num3, count2 + 2);
							dataTable.Rows.Add(array4);
						}
					}
					object[] array5 = new object[count2 + 3];
					array5.SetValue("合计", 1);
					array5.SetValue(num2, count2 + 2);
					dataTable.Rows.Add(array5);
				}
				else
				{
					if (!dataTable.Columns.Contains("层名"))
					{
						dataTable.Columns.Add("层名", typeof(string));
					}
					for (int num9 = 0; num9 < this.listBox1.Items.Count; num9++)
					{
						if (!dataTable.Columns.Contains(this.listBox1.Items[num9].ToString()))
						{
							dataTable.Columns.Add(this.listBox1.Items[num9].ToString(), typeof(string));
							text += this.listBox1.Items[num9].ToString();
							if (num9 < this.listBox1.Items.Count - 1)
							{
								text += ",";
							}
						}
					}
					if (!dataTable.Columns.Contains("条数"))
					{
						dataTable.Columns.Add("条数", typeof(int));
					}
					if (!dataTable.Columns.Contains("长度"))
					{
						dataTable.Columns.Add("长度", typeof(double));
					}
					if (!dataTable.Columns.Contains("总条数"))
					{
						dataTable.Columns.Add("总条数", typeof(int));
					}
					if (!dataTable.Columns.Contains("总长度"))
					{
						dataTable.Columns.Add("总长度", typeof(double));
					}
					int num10 = 0;
					double num11 = 0.0;
					int count3 = this.listBox1.Items.Count;
					for (int num12 = 0; num12 < count; num12++)
					{
						int num13 = 0;
						double num14 = 0.0;
						Splash.Status = "状态: 正在汇总" + this.Layerbox.CheckedItems[num12] + ",请稍候...";
						IFeatureLayer pPipeLayer = ((ClassCollectformsUI.LayerboxItem)this.Layerbox.CheckedItems[num12]).m_pPipeLayer;
						IFields fields = pPipeLayer.FeatureClass.Fields;
						for (int num15 = 0; num15 < fields.FieldCount; num15++)
						{
							IField field = fields.Field[num15];
							if (field.Type == (esriFieldType) 7)
							{
								num = num15;
								break;
							}
						}
						ISpatialFilter spatialFilter2 = new SpatialFilter();
						IFeatureClass featureClass2 = pPipeLayer.FeatureClass;
						if (this.GeometrySet.Checked)
						{
							if (this.m_ipGeo != null)
							{
								spatialFilter2.Geometry=(this.m_ipGeo);
							}
							spatialFilter2.SpatialRel=(esriSpatialRelEnum) (1);
						}
						ISelectionSet selectionSet2 = featureClass2.Select(spatialFilter2, (esriSelectionType) 3, (esriSelectionOption) 1, null);
						string name = pPipeLayer.Name;
						ITableSort tableSort2 = new TableSort();
						tableSort2.Fields=(text);
						tableSort2.SelectionSet=(selectionSet2);
						tableSort2.Sort(null);
						ICursor rows2 = tableSort2.Rows;
						object[] array6 = new object[count3];
						object[] array7 = new object[count3];
						int num16 = 1;
						double num17 = 0.0;
						double num18 = 0.0;
						IRow row2 = rows2.NextRow();
						if (row2 != null)
						{
							IPolyline polyline = (IPolyline)row2.Value[num];
							IPointCollection pointCollection = (IPointCollection)polyline;
							for (int num19 = 0; num19 < pointCollection.PointCount - 1; num19++)
							{
								IPoint point = pointCollection.Point[num19];
								IPoint point2 = pointCollection.Point[num19 + 1];
								num18 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0) + Math.Pow(point.Z - point.M - point2.Z + point2.M, 2.0));
							}
							num17 += num18;
							for (int num20 = 0; num20 < count3; num20++)
							{
								int num21 = row2.Fields.FindField(this.listBox1.Items[num20].ToString());
								array7[num20] = row2.Value[num21];
							}
							row2 = rows2.NextRow();
							while (row2 != null)
							{
								if (row2.Value[num] is DBNull)
								{
									row2 = rows2.NextRow();
								}
								else
								{
									IPolyline polyline2 = (IPolyline)row2.Value[num];
									IPointCollection pointCollection2 = (IPointCollection)polyline2;
									num18 = 0.0;
									for (int num22 = 0; num22 < pointCollection2.PointCount - 1; num22++)
									{
										IPoint point3 = pointCollection2.Point[num22];
										IPoint point4 = pointCollection2.Point[num22 + 1];
										num18 += Math.Sqrt(Math.Pow(point3.X - point4.X, 2.0) + Math.Pow(point3.Y - point4.Y, 2.0) + Math.Pow(point3.Z - point3.M - point4.Z + point4.M, 2.0));
									}
									for (int num23 = 0; num23 < count3; num23++)
									{
										int num24 = row2.Fields.FindField(this.listBox1.Items[num23].ToString());
										array6[num23] = row2.Value[num24];
									}
									bool flag2 = false;
									for (int num25 = 0; num25 < count3; num25++)
									{
										if (!this.ColumnEqual(array7[num25], array6[num25]))
										{
											object[] array8 = new object[count3 + 3];
											array8.SetValue(name, 0);
											for (int num26 = 0; num26 < count3; num26++)
											{
												array8.SetValue(array7[num26], num26 + 1);
											}
											array8.SetValue(num16, count3 + 1);
											num17 = Math.Round(num17, 3);
											array8.SetValue(num17, count3 + 2);
											dataTable.Rows.Add(array8);
											for (int num27 = num25; num27 < count3; num27++)
											{
												array7[num27] = array6[num27];
											}
											num13 += num16;
											num14 += num17;
											num16 = 1;
											num17 = num18;
											flag2 = false;
											break;
										}
										flag2 = true;
									}
									if (flag2)
									{
										num16++;
										num17 += num18;
									}
									row2 = rows2.NextRow();
								}
							}
							num13 += num16;
							num14 += num17;
							num10 += num13;
							num11 += num14;
							num17 = Math.Round(num17, 3);
							num14 = Math.Round(num14, 3);
							object[] array9 = new object[count3 + 5];
							array9.SetValue(name, 0);
							for (int num28 = 0; num28 < count3; num28++)
							{
								array9.SetValue(array7[num28], num28 + 1);
							}
							array9.SetValue(num16, count3 + 1);
							array9.SetValue(num17, count3 + 2);
							array9.SetValue(num13, count3 + 3);
							array9.SetValue(num14, count3 + 4);
							dataTable.Rows.Add(array9);
						}
					}
					num11 = Math.Round(num11, 3);
					object[] array10 = new object[count3 + 5];
					array10.SetValue("合计", 1);
					array10.SetValue(num10, count3 + 3);
					array10.SetValue(num11, count3 + 4);
					dataTable.Rows.Add(array10);
				}
				Splash.Close();
				new ClassCollectResultForm
				{
					nType = 2,
					HbCount = this.listBox1.Items.Count,
					ResultTable = dataTable
				}.ShowDialog();
			}
		}

		private void DelBut_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			if (selectedIndex == -1)
			{
				MessageBox.Show(@"请用户选择项!");
			}
			else
			{
				this.listBox1.Items.RemoveAt(selectedIndex);
			}
		}

		private void ClassCollectformsUI_VisibleChanged(object sender, EventArgs e)
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

		private void GeometrySet_Click(object sender, EventArgs e)
		{
			this.m_ipGeo = null;
			m_context.ActiveView.Refresh();
		}

		private void ClassCollectformsUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "分类汇总";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}


    }
}