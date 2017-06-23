using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class QueryResult : Form
	{


		public IMapControl3 MapControl;

		public IFeatureSelection pFeatureSelection;

		private DataSet pDataSet = new DataSet("总表");

		private DataTable stable = new DataTable();

		private DataTable Geotable = new DataTable();

		private int nGeoType = -1;

        

		private bool bFitWidth = true;


		private bool bShowGeo = true;

		public IFeatureCursor pFeatureCursor
		{
			get
			{
				return this.m_FeatureCursor;
			}
			set
			{
				this.m_FeatureCursor = value;
			}
		}

	public QueryResult()
		{
			this.InitializeComponent();
		}

		private void QueryResult_Load(object sender, EventArgs e)
		{
			this.StatWay.SelectedIndex = 0;
			this.bControlEvent = true;
			this.UpdateGrid();
			this.bControlEvent = false;
		}

		public void UpdateGrid()
		{
			this.ultraGrid1.DataSource=(null);
			Splash.Show();
			Splash.Status = "状态:正在查询,请稍候...";
			try
			{
				if (this.MakeData())
				{
					this.pDataSet.Tables.Add(this.stable);
					this.pDataSet.Tables.Add(this.Geotable);
					DataRelation relation = new DataRelation("空间表", this.stable.Columns[0], this.Geotable.Columns[0]);
					this.pDataSet.Relations.Add(relation);
					this.ultraGrid1.DataSource=(this.pDataSet);
					for (int i = 0; i < this.ultraGrid1.DisplayLayout.Bands[0].Columns.Count; i++)
					{
						this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].PerformAutoResize();
						this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Width=((int)((double)this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Width * 1.4));
						string key = this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Key;
						if (key == "管径" || key == "沟截面宽高")
						{
							this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Header.Caption=(key + "[毫米]");
						}
						else if (key.ToUpper() == "X" || key.ToUpper() == "Y" || key == "地面高程" || key == "起点高程" || key == "终点高程" || key == "起点埋深" || key == "终点埋深")
						{
							this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Header.Caption=(key + "[米]");
						}
						else if (key == "电压")
						{
							this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Header.Caption=("电压[千伏]");
						}
						else if (key == "压力")
						{
							this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Header.Caption=("压力[兆帕]");
						}
						Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
						if (!regex.IsMatch(key))
						{
							this.ultraGrid1.DisplayLayout.Bands[0].Columns[i].Hidden=(true);
						}
					}
					this.FormatCurrencyColumns();
				}
				Splash.Close();
			}
			catch
			{
				Splash.Close();
			}
		}

		private void FormatCurrencyColumns()
		{
			BandEnumerator enumerator = this.ultraGrid1.DisplayLayout.Bands.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					UltraGridBand current = enumerator.Current;
					ColumnEnumerator enumerator2 = current.Columns.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							UltraGridColumn current2 = enumerator2.Current;
							if (current2.DataType.ToString() == "System.Decimal" || current2.DataType.ToString() == "System.Single" || current2.DataType.ToString() == "System.Double" || current2.DataType.ToString() == "System.Int32")
							{
								current2.CellAppearance.TextHAlign = HAlign.Right;
							}
						}
					}
					finally
					{
						IDisposable disposable = enumerator2 as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		private bool MakeData()
		{
			Splash.Status = "状态:创建临时表,请稍候...";
			this.pDataSet = new DataSet("总表");
			this.stable = new DataTable("属性表");
			this.Geotable = new DataTable("空间");
			this.StatField.Items.Clear();
			bool result;
			if (this.m_FeatureCursor == null)
			{
				result = false;
			}
			else
			{
				IFields fields = this.m_FeatureCursor.Fields;
				int num = 0;
				this.OidField = 0;
				this.stable.Columns.Add("OID", typeof(string));
				for (int i = 0; i < fields.FieldCount; i++)
				{
					IField field = fields.get_Field(i);
					string name = field.Name;
					if (field.Type == (esriFieldType) 7)
					{
						num = i;
					}
					if (field.Type == (esriFieldType)6)
					{
						this.OidField = i;
					}
					else
					{
						Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
						if (regex.IsMatch(field.Name))
						{
							if (field.Type == (esriFieldType)3 || field.Type == (esriFieldType)1 || field.Type == (esriFieldType)2 || field.Type == 0)
							{
								this.CalField.Items.Add(field.Name);
							}
							this.StatField.Items.Add(fields.get_Field(i).Name);
						}
						if (!this.stable.Columns.Contains(fields.get_Field(i).Name))
						{
							switch ((int)field.Type)
							{
							case 1:
								this.stable.Columns.Add(name, typeof(int));
								goto IL_262;
							case 2:
								this.stable.Columns.Add(name, typeof(float));
								goto IL_262;
							case 3:
								this.stable.Columns.Add(name, typeof(double));
								goto IL_262;
							case 6:
								this.stable.Columns.Add(name, typeof(string));
								goto IL_262;
							}
							this.stable.Columns.Add(name, typeof(string));
						}
					}
					IL_262:;
				}
				if (this.CalField.Items.Count > 0)
				{
					this.CalField.SelectedIndex = 0;
				}
				IFeature feature = this.pFeatureCursor.NextFeature();
				bool flag = false;
				if (feature == null)
				{
					result = false;
				}
				else
				{
					if (feature.FeatureType == (esriFeatureType) 10 || feature.FeatureType == (esriFeatureType) 8)
					{
						this.StatWay.Items.Add("统计长度");
						if (!this.stable.Columns.Contains("GD管线长度"))
						{
							this.stable.Columns.Add("GD管线长度", typeof(double));
						}
						flag = true;
						this.nGeoType = 1;
					}
					else if (feature.FeatureType == (esriFeatureType) 7 || feature.FeatureType == (esriFeatureType) 9)
					{
						this.nGeoType = 0;
					}
					else if (feature.FeatureType == (esriFeatureType) 1)
					{
						if (feature.Shape.GeometryType == (esriGeometryType) 1)
						{
							this.nGeoType = 0;
						}
						if (feature.Shape.GeometryType == (esriGeometryType) 6 || feature.Shape.GeometryType == (esriGeometryType) 3)
						{
							this.nGeoType = 3;
						}
						if (feature.Shape.GeometryType == (esriGeometryType) 4)
						{
							this.nGeoType = 2;
						}
						if (feature.Shape.GeometryType == 0)
						{
							this.nGeoType = -1;
						}
					}
					if (feature.FeatureType == (esriFeatureType) 11)
					{
						this.nGeoType = 4;
					}
					if (this.nGeoType == -1)
					{
						result = false;
					}
					else
					{
						if (this.nGeoType > 0)
						{
							if (!this.Geotable.Columns.Contains("LINECODE"))
							{
								this.Geotable.Columns.Add("LINECODE", typeof(string));
							}
							if (!this.Geotable.Columns.Contains("序号"))
							{
								this.Geotable.Columns.Add("序号", typeof(int));
							}
							if (!this.Geotable.Columns.Contains("X"))
							{
								this.Geotable.Columns.Add("X", typeof(double));
							}
							if (!this.Geotable.Columns.Contains("Y"))
							{
								this.Geotable.Columns.Add("Y", typeof(double));
							}
							if (!this.Geotable.Columns.Contains("Z"))
							{
								this.Geotable.Columns.Add("Z", typeof(double));
							}
							if (!this.Geotable.Columns.Contains("M"))
							{
								this.Geotable.Columns.Add("M", typeof(double));
							}
						}
						else
						{
							if (!this.Geotable.Columns.Contains("PONTCODE"))
							{
								this.Geotable.Columns.Add("PONTCODE", typeof(string));
							}
							if (!this.Geotable.Columns.Contains("X"))
							{
								this.Geotable.Columns.Add("X", typeof(double));
							}
							if (!this.Geotable.Columns.Contains("Y"))
							{
								this.Geotable.Columns.Add("Y", typeof(double));
							}
							if (!this.Geotable.Columns.Contains("Z"))
							{
								this.Geotable.Columns.Add("Z", typeof(double));
							}
						}
						object[] array;
						object[] array2;
						if (this.nGeoType > 0)
						{
							if (this.nGeoType == 1)
							{
								array = new object[fields.FieldCount + 1];
							}
							else
							{
								array = new object[fields.FieldCount];
							}
							array2 = new object[6];
						}
						else
						{
							array = new object[fields.FieldCount];
							array2 = new object[4];
						}
						string text = feature.FeatureType.ToString();
						this.pFeatureSelection.Clear();
						Splash.Status = "状态:填充表单,请稍候...";
						while (feature != null)
						{
							double num2 = 0.0;
							string text2 = feature.get_Value(this.OidField).ToString();
							if (feature.Shape== null || feature.Shape.IsEmpty)
							{
								feature = this.pFeatureCursor.NextFeature();
							}
							else
							{
								int j;
								for (j = 0; j < fields.FieldCount; j++)
								{
									if (j == this.OidField)
									{
										array[0] = text2;
									}
									if (j == num)
									{
										if (num > this.OidField)
										{
											array[j] = text;
										}
										else
										{
											array[j + 1] = text;
										}
										if (this.nGeoType == 1 || this.nGeoType == 3)
										{
											IPolyline polyline = (IPolyline)feature.Shape;
											IPointCollection pointCollection = (IPointCollection)polyline;
											IPoint point = null;
											IPoint point2 = null;
											for (int k = 0; k < pointCollection.PointCount - 1; k++)
											{
												point = pointCollection.get_Point(k);
												array2[0] = text2;
												array2[1] = k + 1;
												array2[2] = point.X;
												array2[3] = point.Y;
												if (this.nGeoType == 3)
												{
													array2[4] = point.Z;
													array2[5] = 0;
												}
												else
												{
													array2[4] = ((float)(point.Z - point.M)).ToString("f3");
													array2[5] = point.M.ToString("f3");
												}
												this.Geotable.Rows.Add(array2);
												point2 = pointCollection.get_Point(k + 1);
												num2 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0) + Math.Pow(point.Z - point.M - point2.Z + point2.M, 2.0));
											}
											array2[0] = text2;
											array2[1] = pointCollection.PointCount;
											array2[2] = point2.X;
											array2[3] = point2.Y;
											if (this.nGeoType == 3)
											{
												array2[4] = point2.Z;
												array2[5] = 0;
											}
											else
											{
												array2[4] = ((float)(point.Z - point.M)).ToString("f3");
												array2[5] = point2.M.ToString("f3");
											}
											this.Geotable.Rows.Add(array2);
										}
										else if (this.nGeoType == 0)
										{
											IPoint point3 = (IPoint)feature.Shape;
											array2[0] = text2;
											array2[1] = point3.X;
											array2[2] = point3.Y;
											array2[3] = point3.Z;
											this.Geotable.Rows.Add(array2);
										}
										else if (this.nGeoType == 2 || this.nGeoType == 4)
										{
											IPolygon polygon = (IPolygon)feature.Shape;
											IPointCollection pointCollection2 = (IPointCollection)polygon;
											IPoint point4 = null;
											for (int l = 0; l < pointCollection2.PointCount - 1; l++)
											{
												IPoint point5 = pointCollection2.get_Point(l);
												array2[0] = text2;
												array2[1] = l + 1;
												array2[2] = point5.X;
												array2[3] = point5.Y;
												array2[4] = point5.Z;
												array2[5] = point5.M;
												this.Geotable.Rows.Add(array2);
												point4 = pointCollection2.get_Point(l + 1);
												num2 += Math.Sqrt(Math.Pow(point5.X - point4.X, 2.0) + Math.Pow(point5.Y - point4.Y, 2.0) + Math.Pow(point5.Z - point5.M - point4.Z + point4.M, 2.0));
											}
											array2[0] = text2;
											array2[1] = pointCollection2.PointCount;
											array2[2] = point4.X;
											array2[3] = point4.Y;
											array2[4] = point4.Z;
											array2[5] = point4.M;
											this.Geotable.Rows.Add(array2);
										}
									}
									else
									{
										IField field2 = feature.Fields.get_Field(j);
										object obj = feature.get_Value(j);
										if (j < this.OidField)
										{
											if (field2.Type == (esriFieldType) 3 || field2.Type == (esriFieldType) 2)
											{
												if (obj != DBNull.Value)
												{
													array[j + 1] = Math.Round(Convert.ToDouble(obj), 3);
												}
												else
												{
													array[j + 1] = obj;
												}
												feature.get_Value(j).ToString();
											}
											else if (field2.Type == (esriFieldType) 5)
											{
												if (obj != DBNull.Value)
												{
													DateTime dateTime = Convert.ToDateTime(obj);
													array[j + 1] = dateTime.ToShortDateString();
												}
												else
												{
													array[j + 1] = "";
												}
											}
											else
											{
												array[j + 1] = obj;
											}
										}
										else if (field2.Type == (esriFieldType) 3 || field2.Type == (esriFieldType) 2)
										{
											if (obj != DBNull.Value)
											{
												array[j] = Math.Round(Convert.ToDouble(obj), 3);
											}
											else
											{
												array[j] = obj;
											}
										}
										else if (field2.Type == (esriFieldType) 5)
										{
											if (obj != DBNull.Value)
											{
												array[j] = Convert.ToDateTime(obj).ToShortDateString();
											}
											else
											{
												array[j] = "";
											}
										}
										else
										{
											array[j] = obj;
										}
									}
								}
								if (flag)
								{
									array[j] = num2.ToString();
								}
								this.stable.Rows.Add(array);
								this.pFeatureSelection.Add(feature);
								feature = this.pFeatureCursor.NextFeature();
							}
						}
						int count = this.pFeatureSelection.SelectionSet.Count;
						this.CountBox.Text = "查询的记录数为:" + count.ToString();
						this.pFeatureSelection.SelectionSet.Refresh();
						IActiveView activeView = m_context.ActiveView;
						activeView.Refresh();
						result = true;
					}
				}
			}
			return result;
		}

		private void ToExcel_Click(object sender, EventArgs e)
		{
			if (this.stable.Rows.Count < 1)
			{
				MessageBox.Show("空数据,不转出EXCEL文件！");
			}
			else
			{
				DialogResult dialogResult = this.SaveExcelDlg.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					string fileName = this.SaveExcelDlg.FileName;
					if (!File.Exists(fileName))
					{
						this.ultraGridExcelExporter1.Export(this.ultraGrid1, fileName);
					}
					else
					{
						MessageBox.Show("该文件已存在,返回!");
					}
				}
			}
		}

		private void Stat_but_Click(object sender, EventArgs e)
		{
			if (this.stable.Rows.Count < 1)
			{
				MessageBox.Show("空数据,不进行统计！");
			}
			else if (this.StatField.SelectedIndex < 0)
			{
				MessageBox.Show("请用户先指定分类项！");
			}
			else
			{
				new StatForm
				{
					Owner = this,
					Form_StatField = this.StatField.SelectedItem.ToString(),
					Form_StatWay = this.StatWay.SelectedItem.ToString(),
					Form_CalField = this.CalField.SelectedItem.ToString(),
					resultTable = this.stable
				}.ShowDialog();
			}
		}

		private void StatWay_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.bControlEvent)
			{
				if (this.StatWay.SelectedItem.ToString() == "计数" || this.StatWay.SelectedItem.ToString() == "统计长度" || this.StatWay.SelectedItem.ToString() == "统计面积")
				{
					this.CalField.Enabled = false;
				}
				else
				{
					this.CalField.Enabled = true;
				}
			}
		}

		private void ultraGrid1_StyleChanged(object sender, EventArgs e)
		{
			MessageBox.Show("sdd");
		}

		private void ultraGrid1_AfterSortChange(object sender, BandEventArgs e)
		{
			for (int i = 0; i < e.Band.SortedColumns.Count; i++)
			{
				UltraGridColumn ultraGridColumn = e.Band.SortedColumns[i];
				if (ultraGridColumn.IsGroupByColumn)
				{
					this.StatField.Text = ultraGridColumn.Key;
					break;
				}
			}
		}

		private void StatField_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.bControlEvent && this.stable.Rows.Count > 0)
			{
				this.ultraGrid1.DisplayLayout.Bands[0].SortedColumns.Clear();
				this.ultraGrid1.DisplayLayout.Bands[0].SortedColumns.Add(this.StatField.SelectedItem.ToString(), true, true);
			}
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			if (this.stable.Rows.Count < 1)
			{
				MessageBox.Show("空数据,不进行打印！");
			}
			else
			{
				try
				{
					this.ultraGrid1.PrintPreview();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error occured while printing.\n" + ex.Message, "Error printing", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void QueryResult_Resize(object sender, EventArgs e)
		{
			Size size = this.ultraGrid1.Size;
			size.Height = (int)((double)base.Size.Height - (double)this.CountBox.Bottom * 1.6);
			this.ultraGrid1.Size = size;
		}

		private void ultraGrid1_MouseUp(object sender, MouseEventArgs e)
		{
			UIElement uIElement = this.ultraGrid1.DisplayLayout.UIElement.ElementFromPoint(new System.Drawing.Point(e.X, e.Y));
			if (uIElement != null)
			{
				UltraGridRow ultraGridRow = uIElement.GetContext(typeof(UltraGridRow)) as UltraGridRow;
			}
		}

		private void RayObjectBut_Click(object sender, EventArgs e)
		{
			SelectedRowsCollection rows = this.ultraGrid1.Selected.Rows;
			if (rows.Count >= 1)
			{
				if (this.nGeoType == 0)
				{
					UltraGridColumn ultraGridColumn = this.ultraGrid1.DisplayLayout.Bands[1].Columns[1];
					UltraGridColumn ultraGridColumn2 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[2];
					for (int i = 0; i < rows.Count; i++)
					{
						UltraGridRow ultraGridRow = rows[i];
						if (ultraGridRow is UltraGridGroupByRow)
						{
							UltraGridGroupByRow ultraGridGroupByRow = (UltraGridGroupByRow)ultraGridRow;
							IPointCollection pointCollection = new MultipointClass();
							RowEnumerator enumerator = ultraGridGroupByRow.Rows.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									UltraGridRow current = enumerator.Current;
									ChildBandsCollection childBands = current.ChildBands;
									UltraGridChildBand ultraGridChildBand = childBands[0];
									UltraGridRow ultraGridRow2 = ultraGridChildBand.Rows[0];
									object cellValue = ultraGridRow2.GetCellValue(ultraGridColumn);
									object cellValue2 = ultraGridRow2.GetCellValue(ultraGridColumn2);
									IPoint point = new PointClass();
									point.X=(Convert.ToDouble(cellValue));
									point.Y=(Convert.ToDouble(cellValue2));
									object missing = Type.Missing;
									pointCollection.AddPoint(point, ref missing, ref missing);
								}
							}
							finally
							{
								IDisposable disposable = enumerator as IDisposable;
								if (disposable != null)
								{
									disposable.Dispose();
								}
							}
							this.AllGeo = (IGeometry)pointCollection;
							IEnvelope envelope = this.MapControl.Extent;
							if (pointCollection.PointCount < 2)
							{
								envelope.CenterAt(pointCollection.get_Point(0));
							}
							else
							{
								envelope = this.AllGeo.Envelope;
							}
							this.MapControl.Extent=(envelope);
						}
						else if (ultraGridRow != null)
						{
							if (ultraGridRow.Band.Index == 0)
							{
								ChildBandsCollection childBands2 = ultraGridRow.ChildBands;
								if (!childBands2.HasChildRows)
								{
									MessageBox.Show("BAND1出现问题,请检查!");
									break;
								}
								IPointCollection pointCollection2 = new MultipointClass();
								foreach (UltraGridChildBand ultraGridChildBand2 in childBands2)
								{
									RowEnumerator enumerator = ultraGridChildBand2.Rows.GetEnumerator();
									try
									{
										while (enumerator.MoveNext())
										{
											UltraGridRow current2 = enumerator.Current;
											if (current2 is UltraGridGroupByRow)
											{
												MessageBox.Show("记录状态");
											}
											else
											{
												object cellValue3 = current2.GetCellValue(ultraGridColumn);
												object cellValue4 = current2.GetCellValue(ultraGridColumn2);
												IPoint point2 = new PointClass();
												point2.X=(Convert.ToDouble(cellValue3));
												point2.Y=(Convert.ToDouble(cellValue4));
												object missing2 = Type.Missing;
												pointCollection2.AddPoint(point2, ref missing2, ref missing2);
											}
										}
									}
									finally
									{
										IDisposable disposable = enumerator as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
								}
								this.AllGeo = (IGeometry)pointCollection2;
								IEnvelope envelope2 = this.MapControl.Extent;
								if (pointCollection2.PointCount < 2)
								{
									envelope2.CenterAt(pointCollection2.get_Point(0));
								}
								else
								{
									envelope2 = this.AllGeo.Envelope;
								}
								this.MapControl.Extent=(envelope2);
							}
							else if (ultraGridRow.Band.Index == 1)
							{
								object cellValue5 = ultraGridRow.GetCellValue(ultraGridColumn);
								object cellValue6 = ultraGridRow.GetCellValue(ultraGridColumn2);
								IEnvelope extent = this.MapControl.Extent;
								IPoint point3 = new PointClass();
								point3.X=(Convert.ToDouble(cellValue5));
								point3.Y=(Convert.ToDouble(cellValue6));
								extent.CenterAt(point3);
								this.MapControl.Extent=(extent);
								this.AllGeo = point3;
							}
						}
					}
				}
				else if (this.nGeoType == 1 || this.nGeoType == 3)
				{
					UltraGridColumn ultraGridColumn3 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[2];
					UltraGridColumn ultraGridColumn4 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[3];
					object missing3 = Type.Missing;
					RowEnumerator enumerator = rows.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							UltraGridRow current3 = enumerator.Current;
							if (current3 is UltraGridGroupByRow)
							{
								IGeometryCollection geometryCollection = new PolylineClass();
								UltraGridGroupByRow ultraGridGroupByRow2 = (UltraGridGroupByRow)current3;
								RowEnumerator enumerator3 = ultraGridGroupByRow2.Rows.GetEnumerator();
								try
								{
									while (enumerator3.MoveNext())
									{
										UltraGridRow current4 = enumerator3.Current;
										ChildBandsCollection childBands3 = current4.ChildBands;
										UltraGridChildBand ultraGridChildBand3 = childBands3[0];
										IPointCollection pointCollection3 = new PathClass();
										RowEnumerator enumerator4 = ultraGridChildBand3.Rows.GetEnumerator();
										try
										{
											while (enumerator4.MoveNext())
											{
												UltraGridRow current5 = enumerator4.Current;
												object cellValue7 = current5.GetCellValue(ultraGridColumn3);
												object cellValue8 = current5.GetCellValue(ultraGridColumn4);
												IPointCollection arg_58B_0 = pointCollection3;
												PointClass pointClass = new PointClass();
												pointClass.X=(Convert.ToDouble(cellValue7));
												pointClass.Y=(Convert.ToDouble(cellValue8));
												arg_58B_0.AddPoint(pointClass, ref missing3, ref missing3);
											}
										}
										finally
										{
											IDisposable disposable = enumerator4 as IDisposable;
											if (disposable != null)
											{
												disposable.Dispose();
											}
										}
										geometryCollection.AddGeometry((IGeometry)pointCollection3, ref missing3, ref missing3);
									}
								}
								finally
								{
									IDisposable disposable = enumerator3 as IDisposable;
									if (disposable != null)
									{
										disposable.Dispose();
									}
								}
								this.AllGeo = (IGeometry)geometryCollection;
							}
							else
							{
								if (current3.Band.Index == 0)
								{
									ChildBandsCollection childBands4 = current3.ChildBands;
									if (!childBands4.HasChildRows)
									{
										MessageBox.Show("BAND1出现问题,请检查!");
										return;
									}
									IPointCollection pointCollection4 = new PolylineClass();
								    IEnumerator enumerator5 = childBands4.Enumerator;
									try
									{
										while (enumerator5.MoveNext())
										{
											UltraGridChildBand ultraGridChildBand4 = (UltraGridChildBand)enumerator5.Current;
											RowEnumerator enumerator3 = ultraGridChildBand4.Rows.GetEnumerator();
											try
											{
												while (enumerator3.MoveNext())
												{
													UltraGridRow current6 = enumerator3.Current;
													object cellValue9 = current6.GetCellValue(ultraGridColumn3);
													object cellValue10 = current6.GetCellValue(ultraGridColumn4);
													IPointCollection arg_6DD_0 = pointCollection4;
													PointClass pointClass2 = new PointClass();
													pointClass2.X=(Convert.ToDouble(cellValue9));
													pointClass2.Y=(Convert.ToDouble(cellValue10));
													arg_6DD_0.AddPoint(pointClass2, ref missing3, ref missing3);
												}
											}
											finally
											{
												IDisposable disposable = enumerator3 as IDisposable;
												if (disposable != null)
												{
													disposable.Dispose();
												}
											}
											this.AllGeo = (IGeometry)pointCollection4;
										}
										continue;
									}
									finally
									{
										IDisposable disposable2 = enumerator5 as IDisposable;
										if (disposable2 != null)
										{
											disposable2.Dispose();
										}
									}
								}
								if (current3.Band.Index == 1)
								{
									IGeometryCollection geometryCollection2 = new PolylineClass();
									IPointCollection pointCollection5 = new PathClass();
									UltraGridRow parentRow = current3.ParentRow;
									ChildBandsCollection childBands5 = parentRow.ChildBands;
									if (!childBands5.HasChildRows)
									{
										MessageBox.Show("BAND1出现问题,请检查!");
										return;
									}
									UltraGridChildBand ultraGridChildBand5 = childBands5[0];
									RowEnumerator enumerator3 = ultraGridChildBand5.Rows.GetEnumerator();
									try
									{
										while (enumerator3.MoveNext())
										{
											UltraGridRow current7 = enumerator3.Current;
											object cellValue11 = current7.GetCellValue(ultraGridColumn3);
											object cellValue12 = current7.GetCellValue(ultraGridColumn4);
											IPointCollection arg_817_0 = pointCollection5;
											PointClass pointClass3 = new PointClass();
											pointClass3.X=(Convert.ToDouble(cellValue11));
											pointClass3.Y=(Convert.ToDouble(cellValue12));
											arg_817_0.AddPoint(pointClass3, ref missing3, ref missing3);
										}
									}
									finally
									{
										IDisposable disposable = enumerator3 as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
									IEnvelope envelope3 = ((IGeometry)pointCollection5).Envelope;
									double num = (envelope3.Width > envelope3.Height) ? (envelope3.Width / 20.0) : (envelope3.Height / 20.0);
									geometryCollection2.AddGeometry((IGeometry)pointCollection5, ref missing3, ref missing3);
									object cellValue13 = current3.GetCellValue(ultraGridColumn3);
									object cellValue14 = current3.GetCellValue(ultraGridColumn4);
									IPoint point4 = new PointClass();
									point4.X=(Convert.ToDouble(cellValue13) - num);
									point4.Y=(Convert.ToDouble(cellValue14) - num);
									IPoint point5 = new PointClass();
									point5.X=(Convert.ToDouble(cellValue13) + num);
									point5.Y=(Convert.ToDouble(cellValue14) + num);
									IPointCollection pointCollection6 = new PathClass();
									pointCollection6.AddPoint(point4, ref missing3, ref missing3);
									pointCollection6.AddPoint(point5, ref missing3, ref missing3);
									geometryCollection2.AddGeometry((IGeometry)pointCollection6, ref missing3, ref missing3);
									IPoint point6 = new PointClass();
									point6.X=(Convert.ToDouble(cellValue13) - num);
									point6.Y=(Convert.ToDouble(cellValue14) + num);
									IPoint point7 = new PointClass();
									point7.X=(Convert.ToDouble(cellValue13) + num);
									point7.Y=(Convert.ToDouble(cellValue14) - num);
									IPointCollection pointCollection7 = new PathClass();
									pointCollection7.AddPoint(point6, ref missing3, ref missing3);
									pointCollection7.AddPoint(point7, ref missing3, ref missing3);
									geometryCollection2.AddGeometry((IGeometry)pointCollection7, ref missing3, ref missing3);
									this.AllGeo = (IGeometry)geometryCollection2;
								}
							}
						}
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					if (this.AllGeo != null)
					{
						IEnvelope envelope4 = this.MapControl.Extent;
						envelope4 = this.AllGeo.Envelope;
						envelope4.Expand(1.5, 1.5, true);
						this.MapControl.Extent=(envelope4);
					}
				}
				else if (this.nGeoType == 2 || this.nGeoType == 4)
				{
					UltraGridColumn ultraGridColumn5 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[2];
					UltraGridColumn ultraGridColumn6 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[3];
					object missing4 = Type.Missing;
					RowEnumerator enumerator = rows.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							UltraGridRow current8 = enumerator.Current;
							if (current8 is UltraGridGroupByRow)
							{
								IGeometryCollection geometryCollection3 = new PolygonClass();
								UltraGridGroupByRow ultraGridGroupByRow3 = (UltraGridGroupByRow)current8;
								RowEnumerator enumerator3 = ultraGridGroupByRow3.Rows.GetEnumerator();
								try
								{
									while (enumerator3.MoveNext())
									{
										UltraGridRow current9 = enumerator3.Current;
										ChildBandsCollection childBands6 = current9.ChildBands;
										UltraGridChildBand ultraGridChildBand6 = childBands6[0];
										IPointCollection pointCollection8 = new RingClass();
										RowEnumerator enumerator4 = ultraGridChildBand6.Rows.GetEnumerator();
										try
										{
											while (enumerator4.MoveNext())
											{
												UltraGridRow current10 = enumerator4.Current;
												object cellValue15 = current10.GetCellValue(ultraGridColumn5);
												object cellValue16 = current10.GetCellValue(ultraGridColumn6);
												IPointCollection arg_BB4_0 = pointCollection8;
												PointClass pointClass4 = new PointClass();
												pointClass4.X=(Convert.ToDouble(cellValue15));
												pointClass4.Y=(Convert.ToDouble(cellValue16));
												arg_BB4_0.AddPoint(pointClass4, ref missing4, ref missing4);
											}
										}
										finally
										{
											IDisposable disposable = enumerator4 as IDisposable;
											if (disposable != null)
											{
												disposable.Dispose();
											}
										}
										geometryCollection3.AddGeometry((IGeometry)pointCollection8, ref missing4, ref missing4);
									}
								}
								finally
								{
									IDisposable disposable = enumerator3 as IDisposable;
									if (disposable != null)
									{
										disposable.Dispose();
									}
								}
								this.AllGeo = (IGeometry)geometryCollection3;
							}
							else
							{
								if (current8.Band.Index == 0)
								{
									ChildBandsCollection childBands7 = current8.ChildBands;
									if (!childBands7.HasChildRows)
									{
										MessageBox.Show("BAND1出现问题,请检查!");
										return;
									}
									IPointCollection pointCollection9 = new PolygonClass();
									IEnumerator enumerator5 = childBands7.Enumerator;
									try
									{
										while (enumerator5.MoveNext())
										{
											UltraGridChildBand ultraGridChildBand7 = (UltraGridChildBand)enumerator5.Current;
											RowEnumerator enumerator3 = ultraGridChildBand7.Rows.GetEnumerator();
											try
											{
												while (enumerator3.MoveNext())
												{
													UltraGridRow current11 = enumerator3.Current;
													object cellValue17 = current11.GetCellValue(ultraGridColumn5);
													object cellValue18 = current11.GetCellValue(ultraGridColumn6);
													IPointCollection arg_D06_0 = pointCollection9;
													PointClass pointClass5 = new PointClass();
													pointClass5.X=(Convert.ToDouble(cellValue17));
													pointClass5.Y=(Convert.ToDouble(cellValue18));
													arg_D06_0.AddPoint(pointClass5, ref missing4, ref missing4);
												}
											}
											finally
											{
												IDisposable disposable = enumerator3 as IDisposable;
												if (disposable != null)
												{
													disposable.Dispose();
												}
											}
											this.AllGeo = (IGeometry)pointCollection9;
										}
										continue;
									}
									finally
									{
										IDisposable disposable2 = enumerator5 as IDisposable;
										if (disposable2 != null)
										{
											disposable2.Dispose();
										}
									}
								}
								if (current8.Band.Index == 1)
								{
									IGeometryCollection geometryCollection4 = new PolylineClass();
									IPointCollection pointCollection10 = new PathClass();
									UltraGridRow parentRow2 = current8.ParentRow;
									ChildBandsCollection childBands8 = parentRow2.ChildBands;
									if (!childBands8.HasChildRows)
									{
										MessageBox.Show("BAND1出现问题,请检查!");
										return;
									}
									UltraGridChildBand ultraGridChildBand8 = childBands8[0];
									RowEnumerator enumerator3 = ultraGridChildBand8.Rows.GetEnumerator();
									try
									{
										while (enumerator3.MoveNext())
										{
											UltraGridRow current12 = enumerator3.Current;
											object cellValue19 = current12.GetCellValue(ultraGridColumn5);
											object cellValue20 = current12.GetCellValue(ultraGridColumn6);
											IPointCollection arg_E40_0 = pointCollection10;
											PointClass pointClass6 = new PointClass();
											pointClass6.X=(Convert.ToDouble(cellValue19));
											pointClass6.Y=(Convert.ToDouble(cellValue20));
											arg_E40_0.AddPoint(pointClass6, ref missing4, ref missing4);
										}
									}
									finally
									{
										IDisposable disposable = enumerator3 as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
									IEnvelope envelope5 = ((IGeometry)pointCollection10).Envelope;
									double num2 = (envelope5.Width > envelope5.Height) ? (envelope5.Width / 20.0) : (envelope5.Height / 20.0);
									geometryCollection4.AddGeometry((IGeometry)pointCollection10, ref missing4, ref missing4);
									object cellValue21 = current8.GetCellValue(ultraGridColumn5);
									object cellValue22 = current8.GetCellValue(ultraGridColumn6);
									IPoint point8 = new PointClass();
									point8.X=(Convert.ToDouble(cellValue21) - num2);
									point8.Y=(Convert.ToDouble(cellValue22) - num2);
									IPoint point9 = new PointClass();
									point9.X=(Convert.ToDouble(cellValue21) + num2);
									point9.Y=(Convert.ToDouble(cellValue22) + num2);
									IPointCollection pointCollection11 = new PathClass();
									pointCollection11.AddPoint(point8, ref missing4, ref missing4);
									pointCollection11.AddPoint(point9, ref missing4, ref missing4);
									geometryCollection4.AddGeometry((IGeometry)pointCollection11, ref missing4, ref missing4);
									IPoint point10 = new PointClass();
									point10.X=(Convert.ToDouble(cellValue21) - num2);
									point10.Y=(Convert.ToDouble(cellValue22) + num2);
									IPoint point11 = new PointClass();
									point11.X=(Convert.ToDouble(cellValue21) + num2);
									point11.Y=(Convert.ToDouble(cellValue22) - num2);
									IPointCollection pointCollection12 = new PathClass();
									pointCollection12.AddPoint(point10, ref missing4, ref missing4);
									pointCollection12.AddPoint(point11, ref missing4, ref missing4);
									geometryCollection4.AddGeometry((IGeometry)pointCollection12, ref missing4, ref missing4);
									this.AllGeo = (IGeometry)geometryCollection4;
								}
							}
						}
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					if (this.AllGeo != null)
					{
						IEnvelope envelope6 = this.MapControl.Extent;
						envelope6 = this.AllGeo.Envelope;
						envelope6.Expand(1.5, 1.5, true);
						this.MapControl.Extent=(envelope6);
					}
				}
			}
		}

		private void FlashPolygon(IScreenDisplay pDisplay, IGeometry pGeometry, int nTimer, int time)
		{
			ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
			IMarkerSymbol markerSymbol = new SimpleMarkerSymbol();
			ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Green=(128);
			string a;
			if ((a = pGeometry.GeometryType.ToString()) != null)
			{
				if (a == "esriGeometryPoint")
				{
					markerSymbol.Size=(10.0);
					markerSymbol.Color=(rgbColor);
					ISymbol symbol = (ISymbol)markerSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
					pDisplay.StartDrawing(0, -1);
					pDisplay.SetSymbol(symbol);
					for (int i = 0; i <= nTimer; i++)
					{
						pDisplay.DrawPoint(pGeometry);
						Thread.Sleep(time);
					}
				}
				else if (a == "esriGeometryPolyline")
				{
					simpleLineSymbol.Width=(10.0);
					simpleLineSymbol.Color=(rgbColor);
					ISymbol symbol = (ISymbol)simpleLineSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
					pDisplay.StartDrawing(0, -1);
					pDisplay.SetSymbol(symbol);
					for (int j = 0; j <= nTimer; j++)
					{
						pDisplay.DrawPolyline(pGeometry);
						Thread.Sleep(time);
					}
				}
				else if (a == "esriGeometryPolygon")
				{
					simpleFillSymbol.Outline=(null);
					simpleFillSymbol.Color=(rgbColor);
					ISymbol symbol = (ISymbol)simpleFillSymbol;
					symbol.ROP2=(esriRasterOpCode) (10);
					pDisplay.StartDrawing(0, -1);
					pDisplay.SetSymbol(symbol);
					for (int k = 0; k <= nTimer; k++)
					{
						pDisplay.DrawPolygon(pGeometry);
						Thread.Sleep(time);
					}
				}
			}
		}

		private void SetPosBut_Click(object sender, EventArgs e)
		{
			SelectedRowsCollection rows = this.ultraGrid1.Selected.Rows;
			if (rows.Count >= 1)
			{
				if (this.nGeoType == 0)
				{
					UltraGridColumn ultraGridColumn = this.ultraGrid1.DisplayLayout.Bands[1].Columns[1];
					UltraGridColumn ultraGridColumn2 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[2];
					for (int i = 0; i < rows.Count; i++)
					{
						UltraGridRow ultraGridRow = rows[i];
						if (ultraGridRow is UltraGridGroupByRow)
						{
							UltraGridGroupByRow ultraGridGroupByRow = (UltraGridGroupByRow)ultraGridRow;
							IPointCollection pointCollection = new MultipointClass();
							RowEnumerator enumerator = ultraGridGroupByRow.Rows.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									UltraGridRow current = enumerator.Current;
									ChildBandsCollection childBands = current.ChildBands;
									UltraGridChildBand ultraGridChildBand = childBands[0];
									UltraGridRow ultraGridRow2 = ultraGridChildBand.Rows[0];
									object cellValue = ultraGridRow2.GetCellValue(ultraGridColumn);
									object cellValue2 = ultraGridRow2.GetCellValue(ultraGridColumn2);
									IPoint point = new PointClass();
									point.X=(Convert.ToDouble(cellValue));
									point.Y=(Convert.ToDouble(cellValue2));
									object missing = Type.Missing;
									pointCollection.AddPoint(point, ref missing, ref missing);
								}
							}
							finally
							{
								IDisposable disposable = enumerator as IDisposable;
								if (disposable != null)
								{
									disposable.Dispose();
								}
							}
							this.AllGeo = (IGeometry)pointCollection;
						}
						else if (ultraGridRow != null)
						{
							if (ultraGridRow.Band.Index == 0)
							{
								ChildBandsCollection childBands2 = ultraGridRow.ChildBands;
								if (!childBands2.HasChildRows)
								{
									MessageBox.Show("BAND1出现问题,请检查!");
									return;
								}
								IPointCollection pointCollection2 = new MultipointClass();
								foreach (UltraGridChildBand ultraGridChildBand2 in childBands2)
								{
									RowEnumerator enumerator = ultraGridChildBand2.Rows.GetEnumerator();
									try
									{
										while (enumerator.MoveNext())
										{
											UltraGridRow current2 = enumerator.Current;
											if (current2 is UltraGridGroupByRow)
											{
												MessageBox.Show("记录状态");
											}
											else
											{
												object cellValue3 = current2.GetCellValue(ultraGridColumn);
												object cellValue4 = current2.GetCellValue(ultraGridColumn2);
												IPoint point2 = new PointClass();
												point2.X=(Convert.ToDouble(cellValue3));
												point2.Y=(Convert.ToDouble(cellValue4));
												object missing2 = Type.Missing;
												pointCollection2.AddPoint(point2, ref missing2, ref missing2);
											}
										}
									}
									finally
									{
										IDisposable disposable = enumerator as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
								}
								this.AllGeo = (IGeometry)pointCollection2;
							}
							else if (ultraGridRow.Band.Index == 1)
							{
								object cellValue5 = ultraGridRow.GetCellValue(ultraGridColumn);
								object cellValue6 = ultraGridRow.GetCellValue(ultraGridColumn2);
								IEnvelope extent = this.MapControl.Extent;
								IPoint point3 = new PointClass();
								point3.X=(Convert.ToDouble(cellValue5));
								point3.Y=(Convert.ToDouble(cellValue6));
								extent.CenterAt(point3);
								this.MapControl.Extent=(extent);
								this.AllGeo = point3;
							}
						}
					}
				}
				else
				{
					if (this.nGeoType == 1)
					{
						UltraGridColumn ultraGridColumn3 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[2];
						UltraGridColumn ultraGridColumn4 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[3];
						object missing3 = Type.Missing;
						RowEnumerator enumerator3 = rows.GetEnumerator();
						try
						{
							while (enumerator3.MoveNext())
							{
								UltraGridRow current3 = enumerator3.Current;
								if (current3 is UltraGridGroupByRow)
								{
									IGeometryCollection geometryCollection = new PolylineClass();
									UltraGridGroupByRow ultraGridGroupByRow2 = (UltraGridGroupByRow)current3;
									RowEnumerator enumerator = ultraGridGroupByRow2.Rows.GetEnumerator();
									try
									{
										while (enumerator.MoveNext())
										{
											UltraGridRow current4 = enumerator.Current;
											ChildBandsCollection childBands3 = current4.ChildBands;
											UltraGridChildBand ultraGridChildBand3 = childBands3[0];
											IPointCollection pointCollection3 = new PathClass();
											RowEnumerator enumerator4 = ultraGridChildBand3.Rows.GetEnumerator();
											try
											{
												while (enumerator4.MoveNext())
												{
													UltraGridRow current5 = enumerator4.Current;
													object cellValue7 = current5.GetCellValue(ultraGridColumn3);
													object cellValue8 = current5.GetCellValue(ultraGridColumn4);
													IPointCollection arg_4DF_0 = pointCollection3;
													PointClass pointClass = new PointClass();
													pointClass.X=(Convert.ToDouble(cellValue7));
													pointClass.Y=(Convert.ToDouble(cellValue8));
													arg_4DF_0.AddPoint(pointClass, ref missing3, ref missing3);
												}
											}
											finally
											{
												IDisposable disposable = enumerator4 as IDisposable;
												if (disposable != null)
												{
													disposable.Dispose();
												}
											}
											geometryCollection.AddGeometry((IGeometry)pointCollection3, ref missing3, ref missing3);
										}
									}
									finally
									{
										IDisposable disposable = enumerator as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
									this.AllGeo = (IGeometry)geometryCollection;
								}
								else
								{
									if (current3.Band.Index == 0)
									{
										ChildBandsCollection childBands4 = current3.ChildBands;
										if (!childBands4.HasChildRows)
										{
											MessageBox.Show("BAND1出现问题,请检查!");
											return;
										}
										IPointCollection pointCollection4 = new PolylineClass();
										IEnumerator enumerator5 = childBands4.Enumerator;
										try
										{
											while (enumerator5.MoveNext())
											{
												UltraGridChildBand ultraGridChildBand4 = (UltraGridChildBand)enumerator5.Current;
												RowEnumerator enumerator = ultraGridChildBand4.Rows.GetEnumerator();
												try
												{
													while (enumerator.MoveNext())
													{
														UltraGridRow current6 = enumerator.Current;
														object cellValue9 = current6.GetCellValue(ultraGridColumn3);
														object cellValue10 = current6.GetCellValue(ultraGridColumn4);
														IPointCollection arg_631_0 = pointCollection4;
														PointClass pointClass2 = new PointClass();
														pointClass2.X=(Convert.ToDouble(cellValue9));
														pointClass2.Y=(Convert.ToDouble(cellValue10));
														arg_631_0.AddPoint(pointClass2, ref missing3, ref missing3);
													}
												}
												finally
												{
													IDisposable disposable = enumerator as IDisposable;
													if (disposable != null)
													{
														disposable.Dispose();
													}
												}
												this.AllGeo = (IGeometry)pointCollection4;
											}
											continue;
										}
										finally
										{
											IDisposable disposable2 = enumerator5 as IDisposable;
											if (disposable2 != null)
											{
												disposable2.Dispose();
											}
										}
									}
									if (current3.Band.Index == 1)
									{
										IGeometryCollection geometryCollection2 = new PolylineClass();
										IPointCollection pointCollection5 = new PathClass();
										UltraGridRow parentRow = current3.ParentRow;
										ChildBandsCollection childBands5 = parentRow.ChildBands;
										if (!childBands5.HasChildRows)
										{
											MessageBox.Show("BAND1出现问题,请检查!");
											return;
										}
										UltraGridChildBand ultraGridChildBand5 = childBands5[0];
										RowEnumerator enumerator = ultraGridChildBand5.Rows.GetEnumerator();
										try
										{
											while (enumerator.MoveNext())
											{
												UltraGridRow current7 = enumerator.Current;
												object cellValue11 = current7.GetCellValue(ultraGridColumn3);
												object cellValue12 = current7.GetCellValue(ultraGridColumn4);
												IPointCollection arg_76B_0 = pointCollection5;
												PointClass pointClass3 = new PointClass();
												pointClass3.X=(Convert.ToDouble(cellValue11));
												pointClass3.Y=(Convert.ToDouble(cellValue12));
												arg_76B_0.AddPoint(pointClass3, ref missing3, ref missing3);
											}
										}
										finally
										{
											IDisposable disposable = enumerator as IDisposable;
											if (disposable != null)
											{
												disposable.Dispose();
											}
										}
										IEnvelope envelope = ((IGeometry)pointCollection5).Envelope;
										double num = (envelope.Width > envelope.Height) ? (envelope.Width / 20.0) : (envelope.Height / 20.0);
										geometryCollection2.AddGeometry((IGeometry)pointCollection5, ref missing3, ref missing3);
										object cellValue13 = current3.GetCellValue(ultraGridColumn3);
										object cellValue14 = current3.GetCellValue(ultraGridColumn4);
										IPoint point4 = new PointClass();
										point4.X=(Convert.ToDouble(cellValue13) - num);
										point4.Y=(Convert.ToDouble(cellValue14) - num);
										IPoint point5 = new PointClass();
										point5.X=(Convert.ToDouble(cellValue13) + num);
										point5.Y=(Convert.ToDouble(cellValue14) + num);
										IPointCollection pointCollection6 = new PathClass();
										pointCollection6.AddPoint(point4, ref missing3, ref missing3);
										pointCollection6.AddPoint(point5, ref missing3, ref missing3);
										geometryCollection2.AddGeometry((IGeometry)pointCollection6, ref missing3, ref missing3);
										IPoint point6 = new PointClass();
										point6.X=(Convert.ToDouble(cellValue13) - num);
										point6.Y=(Convert.ToDouble(cellValue14) + num);
										IPoint point7 = new PointClass();
										point7.X=(Convert.ToDouble(cellValue13) + num);
										point7.Y=(Convert.ToDouble(cellValue14) - num);
										IPointCollection pointCollection7 = new PathClass();
										pointCollection7.AddPoint(point6, ref missing3, ref missing3);
										pointCollection7.AddPoint(point7, ref missing3, ref missing3);
										geometryCollection2.AddGeometry((IGeometry)pointCollection7, ref missing3, ref missing3);
										this.AllGeo = (IGeometry)geometryCollection2;
									}
								}
							}
							goto IL_F37;
						}
						finally
						{
							IDisposable disposable2 = enumerator3 as IDisposable;
							if (disposable2 != null)
							{
								disposable2.Dispose();
							}
						}
					}
					if (this.nGeoType == 2 || this.nGeoType == 4)
					{
						UltraGridColumn ultraGridColumn5 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[2];
						UltraGridColumn ultraGridColumn6 = this.ultraGrid1.DisplayLayout.Bands[1].Columns[3];
						object missing4 = Type.Missing;
						RowEnumerator enumerator = rows.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								UltraGridRow current8 = enumerator.Current;
								if (current8 is UltraGridGroupByRow)
								{
									IGeometryCollection geometryCollection3 = new PolygonClass();
									UltraGridGroupByRow ultraGridGroupByRow3 = (UltraGridGroupByRow)current8;
									RowEnumerator enumerator4 = ultraGridGroupByRow3.Rows.GetEnumerator();
									try
									{
										while (enumerator4.MoveNext())
										{
											UltraGridRow current9 = enumerator4.Current;
											ChildBandsCollection childBands6 = current9.ChildBands;
											UltraGridChildBand ultraGridChildBand6 = childBands6[0];
											IPointCollection pointCollection8 = new RingClass();
											RowEnumerator enumerator6 = ultraGridChildBand6.Rows.GetEnumerator();
											try
											{
												while (enumerator6.MoveNext())
												{
													UltraGridRow current10 = enumerator6.Current;
													object cellValue15 = current10.GetCellValue(ultraGridColumn5);
													object cellValue16 = current10.GetCellValue(ultraGridColumn6);
													IPointCollection arg_AAF_0 = pointCollection8;
													PointClass pointClass4 = new PointClass();
													pointClass4.X=(Convert.ToDouble(cellValue15));
													pointClass4.Y=(Convert.ToDouble(cellValue16));
													arg_AAF_0.AddPoint(pointClass4, ref missing4, ref missing4);
												}
											}
											finally
											{
												IDisposable disposable = enumerator6 as IDisposable;
												if (disposable != null)
												{
													disposable.Dispose();
												}
											}
											geometryCollection3.AddGeometry((IGeometry)pointCollection8, ref missing4, ref missing4);
										}
									}
									finally
									{
										IDisposable disposable = enumerator4 as IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
									this.AllGeo = (IGeometry)geometryCollection3;
								}
								else
								{
									if (current8.Band.Index == 0)
									{
										ChildBandsCollection childBands7 = current8.ChildBands;
										if (!childBands7.HasChildRows)
										{
											MessageBox.Show("BAND1出现问题,请检查!");
											return;
										}
										IPointCollection pointCollection9 = new PolygonClass();
										IEnumerator enumerator5 = childBands7.Enumerator;
										try
										{
											while (enumerator5.MoveNext())
											{
												UltraGridChildBand ultraGridChildBand7 = (UltraGridChildBand)enumerator5.Current;
												RowEnumerator enumerator4 = ultraGridChildBand7.Rows.GetEnumerator();
												try
												{
													while (enumerator4.MoveNext())
													{
														UltraGridRow current11 = enumerator4.Current;
														object cellValue17 = current11.GetCellValue(ultraGridColumn5);
														object cellValue18 = current11.GetCellValue(ultraGridColumn6);
														IPointCollection arg_C01_0 = pointCollection9;
														PointClass pointClass5 = new PointClass();
														pointClass5.X=(Convert.ToDouble(cellValue17));
														pointClass5.Y=(Convert.ToDouble(cellValue18));
														arg_C01_0.AddPoint(pointClass5, ref missing4, ref missing4);
													}
												}
												finally
												{
													IDisposable disposable = enumerator4 as IDisposable;
													if (disposable != null)
													{
														disposable.Dispose();
													}
												}
												this.AllGeo = (IGeometry)pointCollection9;
											}
											continue;
										}
										finally
										{
											IDisposable disposable2 = enumerator5 as IDisposable;
											if (disposable2 != null)
											{
												disposable2.Dispose();
											}
										}
									}
									if (current8.Band.Index == 1)
									{
										IGeometryCollection geometryCollection4 = new PolylineClass();
										IPointCollection pointCollection10 = new PathClass();
										UltraGridRow parentRow2 = current8.ParentRow;
										ChildBandsCollection childBands8 = parentRow2.ChildBands;
										if (!childBands8.HasChildRows)
										{
											MessageBox.Show("BAND1出现问题,请检查!");
											return;
										}
										UltraGridChildBand ultraGridChildBand8 = childBands8[0];
										RowEnumerator enumerator4 = ultraGridChildBand8.Rows.GetEnumerator();
										try
										{
											while (enumerator4.MoveNext())
											{
												UltraGridRow current12 = enumerator4.Current;
												object cellValue19 = current12.GetCellValue(ultraGridColumn5);
												object cellValue20 = current12.GetCellValue(ultraGridColumn6);
												IPointCollection arg_D3B_0 = pointCollection10;
												PointClass pointClass6 = new PointClass();
												pointClass6.X=(Convert.ToDouble(cellValue19));
												pointClass6.Y=(Convert.ToDouble(cellValue20));
												arg_D3B_0.AddPoint(pointClass6, ref missing4, ref missing4);
											}
										}
										finally
										{
											IDisposable disposable = enumerator4 as IDisposable;
											if (disposable != null)
											{
												disposable.Dispose();
											}
										}
										IEnvelope envelope2 = ((IGeometry)pointCollection10).Envelope;
										double num2 = (envelope2.Width > envelope2.Height) ? (envelope2.Width / 20.0) : (envelope2.Height / 20.0);
										geometryCollection4.AddGeometry((IGeometry)pointCollection10, ref missing4, ref missing4);
										object cellValue21 = current8.GetCellValue(ultraGridColumn5);
										object cellValue22 = current8.GetCellValue(ultraGridColumn6);
										IPoint point8 = new PointClass();
										point8.X=(Convert.ToDouble(cellValue21) - num2);
										point8.Y=(Convert.ToDouble(cellValue22) - num2);
										IPoint point9 = new PointClass();
										point9.X=(Convert.ToDouble(cellValue21) + num2);
										point9.Y=(Convert.ToDouble(cellValue22) + num2);
										IPointCollection pointCollection11 = new PathClass();
										pointCollection11.AddPoint(point8, ref missing4, ref missing4);
										pointCollection11.AddPoint(point9, ref missing4, ref missing4);
										geometryCollection4.AddGeometry((IGeometry)pointCollection11, ref missing4, ref missing4);
										IPoint point10 = new PointClass();
										point10.X=(Convert.ToDouble(cellValue21) - num2);
										point10.Y=(Convert.ToDouble(cellValue22) + num2);
										IPoint point11 = new PointClass();
										point11.X=(Convert.ToDouble(cellValue21) + num2);
										point11.Y=(Convert.ToDouble(cellValue22) - num2);
										IPointCollection pointCollection12 = new PathClass();
										pointCollection12.AddPoint(point10, ref missing4, ref missing4);
										pointCollection12.AddPoint(point11, ref missing4, ref missing4);
										geometryCollection4.AddGeometry((IGeometry)pointCollection12, ref missing4, ref missing4);
										this.AllGeo = (IGeometry)geometryCollection4;
									}
								}
							}
						}
						finally
						{
							IDisposable disposable = enumerator as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
				IL_F37:
				if (this.AllGeo != null)
				{
					ISymbol symbol = null;
					IRgbColor rgbColor = new RgbColor();
					rgbColor.Red=(0);
					rgbColor.Green=(255);
					rgbColor.Blue=(0);
					switch ((int)this.AllGeo.GeometryType)
					{
					case 1:
					case 2:
					{
						ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
						simpleMarkerSymbolClass.Color=(rgbColor);
						symbol = simpleMarkerSymbolClass as ISymbol;
						break;
					}
					case 3:
					case 6:
					{
						ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
						simpleLineSymbolClass.Color=(rgbColor);
						simpleLineSymbolClass.Width=(6.0);
						symbol = simpleLineSymbolClass as ISymbol;
						break;
					}
					case 4:
					{
						ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
						simpleFillSymbolClass.Style=(0);
						simpleFillSymbolClass.Outline.Width=(6.0);
						simpleFillSymbolClass.Color=(rgbColor);
						symbol = simpleFillSymbolClass as ISymbol;
						break;
					}
					}
					symbol.ROP2=(esriRasterOpCode) (10);
					this.MapControl.FlashShape(this.AllGeo, 6, 150, symbol);
				}
			}
		}

		private void ultraGrid1_BeforeSortChange(object sender, BeforeSortChangeEventArgs e)
		{
		}

		private void ColchooserBut_Click(object sender, EventArgs e)
		{
			if (this.customColumnChooserDialog == null || this.customColumnChooserDialog.IsDisposed)
			{
				this.customColumnChooserDialog = new CustomColumnChooser();
				this.customColumnChooserDialog.Owner = this;
				this.customColumnChooserDialog.Grid = this.ultraGrid1;
			}
			if (this.customColumnChooserDialog.ShowDialog() == DialogResult.OK)
			{
				this.TopBandText = this.customColumnChooserDialog.TopBand;
				this.TopBandFont = this.customColumnChooserDialog.mFont;
				this.TopBandColor = this.customColumnChooserDialog.mColor;
				this.bFitWidth = this.customColumnChooserDialog.bFitWidth;
				this.bHaveTop = this.customColumnChooserDialog.HaveTopBand;
				this.bShowGeo = this.customColumnChooserDialog.ShowGeo;
			}
		}

		private void ultraGrid1_InitializePrintPreview(object sender, CancelablePrintPreviewEventArgs e)
		{
			e.PrintPreviewSettings.Zoom=(1.0);
			e.PrintLayout.Bands[1].Hidden=(!this.bShowGeo);
			e.PrintPreviewSettings.DialogLeft=(SystemInformation.WorkingArea.X);
			e.PrintPreviewSettings.DialogTop=(SystemInformation.WorkingArea.Y);
			e.PrintPreviewSettings.DialogWidth=(SystemInformation.WorkingArea.Width);
			e.PrintPreviewSettings.DialogHeight=(SystemInformation.WorkingArea.Height);
			if (this.bFitWidth)
			{
				e.DefaultLogicalPageLayoutInfo.FitWidthToPages=(1);
			}
			else
			{
                e.DefaultLogicalPageLayoutInfo.FitWidthToPages=(0);
			}
			if (this.bHaveTop)
			{
				e.DefaultLogicalPageLayoutInfo.PageHeader=(this.TopBandText);
				e.DefaultLogicalPageLayoutInfo.PageHeaderHeight=((int)((double)this.TopBandFont.SizeInPoints * 1.5));
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.FontData.Bold = this.TopBandFont.Bold;
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.FontData.Italic = this.TopBandFont.Italic;
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.FontData.Underline = this.TopBandFont.Underline;
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.FontData.SizeInPoints = this.TopBandFont.SizeInPoints;
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.ForeColor = this.TopBandColor;
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.FontData.Name = this.TopBandFont.Name;
				e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance.TextHAlign = HAlign.Center;
				e.DefaultLogicalPageLayoutInfo.PageHeaderBorderStyle=(UIElementBorderStyle.Solid);
			}
			e.DefaultLogicalPageLayoutInfo.PageFooter=("<#>.页");
			e.DefaultLogicalPageLayoutInfo.PageFooterHeight=(40);
			e.DefaultLogicalPageLayoutInfo.PageFooterAppearance.TextHAlign = HAlign.Center;
			e.DefaultLogicalPageLayoutInfo.PageFooterAppearance.FontData.Italic = DefaultableBoolean.True;
			e.DefaultLogicalPageLayoutInfo.PageFooterBorderStyle=(UIElementBorderStyle.Solid);
			e.DefaultLogicalPageLayoutInfo.ClippingOverride=ClippingOverride.Yes;
			e.PrintDocument.DocumentName = "Document Name";
		}

		private void ultraGrid1_InitializeGroupByRow(object sender, InitializeGroupByRowEventArgs e)
		{
			e.Row.Description=(string.Concat(new object[]
			{
				e.Row.Value.ToString(),
				", ",
				e.Row.Rows.Count,
				" 条记录."
			}));
		}

		private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
			e.Layout.Bands[0].Override.RowAlternateAppearance.BackColor = Color.LightCyan;
			e.Layout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.LightYellow;
		}

		private void QueryResult_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A && (Control.ModifierKeys & Keys.Shift) == Keys.Shift && base.Opacity < 1.0)
			{
				base.Opacity += 0.05;
			}
			if (e.KeyCode == Keys.S && (Control.ModifierKeys & Keys.Shift) == Keys.Shift && base.Opacity > 0.5)
			{
				base.Opacity -= 0.05;
			}
		}

		private void QueryResult_FormClosed(object sender, FormClosedEventArgs e)
		{
			Splash.Close();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}
	}
}
