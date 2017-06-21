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
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class PPAlarmResult : Form
	{
		private IContainer icontainer_0 = null;

		private DataGridView dataGridView3;

		private Timer timer_0;

		public string m_strBuildDate = "建设时间";

		public IAppContext m_iApp;

		public IMap Map;

		public IPipeConfig pPipeCfg;

		public string m_strLayerName = "";

		public int m_nExpireTime;

		public int m_nTimerCounter;

		public int m_nCurRowIndex;

		private IGeometry igeometry_0;

		public IFeatureLayer m_pCurLayer;

		public IAppContext  App
		{
			set
			{
				this.m_iApp = value;
				this.Map = this.m_iApp.FocusMap;
				this.pPipeCfg = this.m_iApp.PipeConfig;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.icontainer_0 = new Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPAlarmResult));
			this.dataGridView3 = new DataGridView();
			this.timer_0 = new Timer(this.icontainer_0);
			((ISupportInitialize)this.dataGridView3).BeginInit();
			base.SuspendLayout();
			this.dataGridView3.AllowUserToAddRows = false;
			this.dataGridView3.AllowUserToDeleteRows = false;
			this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Dock = DockStyle.Fill;
			this.dataGridView3.Location = new System.Drawing.Point(0, 0);
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.ReadOnly = true;
			this.dataGridView3.RowHeadersVisible = false;
			this.dataGridView3.RowTemplate.Height = 18;
			this.dataGridView3.RowTemplate.Resizable = DataGridViewTriState.False;
			this.dataGridView3.Size = new Size(635, 266);
			this.dataGridView3.TabIndex = 1;
			this.dataGridView3.CellClick += new DataGridViewCellEventHandler(this.dataGridView3_CellClick);
			this.timer_0.Interval = 500;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(635, 266);
			base.Controls.Add(this.dataGridView3);
			base.Icon = (Icon)resources.GetObject("$Icon");
			base.Name = "PPAlarmResult";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "多次爆点列表";
			base.TopMost = true;
			base.Load += new EventHandler(this.PPAlarmResult_Load);
			base.FormClosing += new FormClosingEventHandler(this.PPAlarmResult_FormClosing);
			((ISupportInitialize)this.dataGridView3).EndInit();
			base.ResumeLayout(false);
		}

		public PPAlarmResult()
		{
			this.InitializeComponent();
		}

		private void method_0(ILayer layer)
		{
			if (layer is IFeatureLayer && layer.Visible)
			{
				IFeatureLayer featureLayer = layer as IFeatureLayer;
				IFeatureClass featureClass = featureLayer.FeatureClass;
				IFields fields = featureClass.Fields;
				if (featureClass.ShapeType == (esriGeometryType) 1)
				{
					this.m_strBuildDate = this.pPipeCfg.GetPointTableFieldName("爆管次数");
					this.pPipeCfg.GetPointTableFieldName("点性");
				}
				else
				{
					this.m_strBuildDate = this.pPipeCfg.GetLineTableFieldName("爆管次数");
				}
				int num = fields.FindField(this.m_strBuildDate);
				if (num == -1)
				{
					MessageBox.Show("爆管次数字段不存在！返回");
				}
				else
				{
					IField field = fields.get_Field(num);
					if (this.pPipeCfg.IsPipePoint(featureClass.AliasName) || this.pPipeCfg.IsPipeLine(featureClass.AliasName))
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
