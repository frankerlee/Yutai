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

using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class PPAlarmResult : Form
	{
		private IContainer icontainer_0 = null;



		public string m_strBuildDate = "建设时间";

		public IAppContext m_iApp;

		public IMap Map;

		public IPipelineConfig pPipeCfg;

		public string m_strLayerName = "";

		public int m_nExpireTime;

		public int m_nTimerCounter;

		public int m_nCurRowIndex;


		public IFeatureLayer m_pCurLayer;

		public IAppContext  App
		{
			set
			{
				this.m_iApp = value;
				this.Map = this.m_iApp.FocusMap;
				
			}
		}

	    public IPipelineConfig _config;

	    public PPAlarmResult(IAppContext context, IPipelineConfig config)
		{
			this.InitializeComponent();
		    m_iApp = context;
		    _config = config;

		}

		private void method_0(ILayer layer)
		{
			if (layer is IFeatureLayer && layer.Visible)
			{
				IFeatureLayer featureLayer = layer as IFeatureLayer;
				IFeatureClass featureClass = featureLayer.FeatureClass;
				IFields fields = featureClass.Fields;
			    IBasicLayerInfo layerInfo = _config.GetBasicLayerInfo(featureClass);
				if (featureClass.ShapeType == (esriGeometryType) 1)
				{
                    //this.m_strBuildDate = this.pPipeCfg.GetPointTableFieldName("爆管次数");
				    this.m_strBuildDate = layerInfo.GetFieldName(PipeConfigWordHelper.PointWords.SGYHDJ);
                   
				}
				else
				{
                    this.m_strBuildDate = layerInfo.GetFieldName(PipeConfigWordHelper.PointWords.SGYHDJ);
                   // this.m_strBuildDate = this.pPipeCfg.GetLineTableFieldName("爆管次数");
				}
				int num = fields.FindField(this.m_strBuildDate);
				if (num == -1)
				{
					MessageBox.Show("爆管次数字段不存在！返回");
				}
				else
				{
					IField field = fields.get_Field(num);
					if (layerInfo!=null && (layerInfo.DataType == enumPipelineDataType.Point || layerInfo.DataType == enumPipelineDataType.Line))
					{
						DateTime now = DateTime.Now;
						now.ToShortDateString();
						now.AddYears(-1 * this.m_nExpireTime).ToShortDateString();
						string whereClause;
						if (field.Type == (esriFieldType) 4)
						{
							whereClause = this.m_strBuildDate + "> '" + this.m_nExpireTime.ToString() + "'";
						}
						else
						{
							whereClause = this.m_strBuildDate + "> " + this.m_nExpireTime.ToString();
						}
						IFeatureClass arg_162_0 = featureClass;
						IQueryFilter queryFilterClass = new QueryFilter();
						queryFilterClass.WhereClause=(whereClause);
						IFeatureCursor featureCursor = arg_162_0.Search(queryFilterClass, false);
						ILayerFields layerFields = (ILayerFields)featureLayer;
						int fieldCount = featureLayer.FeatureClass.Fields.FieldCount;
						this.dataGridView3.Rows.Clear();
						this.dataGridView3.Columns.Clear();
						DataGridViewCellStyle columnHeadersDefaultCellStyle = new DataGridViewCellStyle();
						this.dataGridView3.ColumnHeadersDefaultCellStyle = columnHeadersDefaultCellStyle;
						this.dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName("Control");
						this.dataGridView3.Columns.Clear();
						this.dataGridView3.ColumnCount = fieldCount;
						for (int i = 0; i < fieldCount; i++)
						{
							IField field2 = layerFields.get_Field(i);
							string aliasName = field2.AliasName;
							this.dataGridView3.Columns[i].Name = aliasName;
							this.dataGridView3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
						}
						IFeature feature = featureCursor.NextFeature();
						int num2 = 0;
						while (feature != null)
						{
							if (!feature.HasOID || feature == null)
							{
								feature = featureCursor.NextFeature();
							}
							else
							{
								string text = "esriGeometry";
								int length = text.Length;
								int num3 = layerFields.FindField(featureLayer.FeatureClass.ShapeFieldName);
								string text2 = featureLayer.FeatureClass.ShapeType.ToString();
								string value = text2.Remove(0, length);
								this.dataGridView3.Rows.Add(new object[]
								{
									""
								});
								int num4 = 1;
								for (int j = 0; j < fieldCount; j++)
								{
									if (num3 == j)
									{
										this.dataGridView3[j, num2].Value = value;
									}
									else
									{
										this.dataGridView3[j, num2].Value = feature.get_Value(j).ToString();
									}
									num4++;
								}
								num2++;
								feature = featureCursor.NextFeature();
							}
						}
						this.Text = "多次爆点列表: 记录条数－-" + num2.ToString();
					}
				}
			}
		}

		private void PPAlarmResult_Load(object obj, EventArgs eventArgs)
		{
			this.ThrougAllLayer();
		}

		public void ThrougAllLayer()
		{
			this.method_0(this.m_pCurLayer);
		}

		private void PPAlarmResult_FormClosing(object obj, FormClosingEventArgs formClosingEventArgs)
		{
			base.Visible = false;
			formClosingEventArgs.Cancel = true;
		}

		private void dataGridView3_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArgs)
		{
			if (dataGridViewCellEventArgs.RowIndex >= 0)
			{
				IFeatureClass featureClass = this.m_pCurLayer.FeatureClass;
				string str = this.dataGridView3[0, dataGridViewCellEventArgs.RowIndex].Value.ToString();
				string whereClause = "OID = " + str;
				IFeatureClass arg_56_0 = featureClass;
				IQueryFilter queryFilterClass = new QueryFilter();
				queryFilterClass.WhereClause=(whereClause);
				IFeatureCursor featureCursor = arg_56_0.Search(queryFilterClass, false);
				IFeature feature = featureCursor.NextFeature();
				IGeometry shape = feature.Shape;
				IClone clone = (IClone)shape;
				IClone clone2 = clone.Clone();
				this.igeometry_0 = (IGeometry)clone2;
				this.ScaleToGeo(m_iApp.FocusMap, this.igeometry_0);
				this.m_nCurRowIndex = dataGridViewCellEventArgs.RowIndex;
				this.timer_0.Start();
				this.m_nTimerCounter = 0;
				this.m_iApp.ActiveView.Refresh();
			}
		}

		public void FlashDstItem()
		{
			IMapControl3 mapControl = this.m_iApp.MapControl as IMapControl3;
			CRandomColor cRandomColor = new CRandomColor();
			Color randColor = cRandomColor.GetRandColor();
			ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=((int)randColor.R);
			rgbColor.Green=((int)randColor.G);
			rgbColor.Blue=((int)randColor.B);
			simpleLineSymbol.Color=(rgbColor);
			simpleLineSymbol.Width=(5.0);
			object obj = simpleLineSymbol;
			ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
			simpleMarkerSymbolClass.Color=(rgbColor);
			simpleMarkerSymbolClass.Size=(10.0);
			simpleMarkerSymbolClass.Style=(0);
			object obj2 = simpleMarkerSymbolClass;
			try
			{
				if (this.igeometry_0.GeometryType == (esriGeometryType) 1)
				{
					mapControl.DrawShape(this.igeometry_0, ref obj2);
				}
				if (this.igeometry_0.GeometryType == (esriGeometryType) 3)
				{
					mapControl.DrawShape(this.igeometry_0, ref obj);
				}
			}
			catch
			{
			}
		}

		private void timer_0_Tick(object obj, EventArgs eventArgs)
		{
			this.m_nTimerCounter++;
			if (this.m_nTimerCounter > 20)
			{
				this.timer_0.Stop();
				this.m_nTimerCounter = 0;
			}
			else
			{
				this.FlashDstItem();
			}
		}

		public void ScaleToGeo(IMap pMap, IGeometry pGeo)
		{
			if (pGeo.GeometryType == (esriGeometryType) 1)
			{
				IEnvelope envelope = pGeo.Envelope;
				IEnvelope extent = ((IActiveView)pMap).Extent;
			    double width = extent.Width;
			    double height = extent.Height;
				envelope.Expand(width / 2.0, height / 2.0, false);
                ((IActiveView)pMap).Extent=(envelope);
			}
			else
			{
				IEnvelope envelope2 = pGeo.Envelope;
				envelope2.Expand(3.0, 3.0, true);
                ((IActiveView)pMap).Extent=(envelope2);
			}
		}
	}
}
