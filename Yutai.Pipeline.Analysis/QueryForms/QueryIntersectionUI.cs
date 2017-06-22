
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class QueryIntersectionUI : Form
	{
		public class ItemInfo
		{
			private int m_iOID = -1;

			private string m_strName = "";

			public int OID
			{
				get
				{
					return this.m_iOID;
				}
			}

			public ItemInfo(int iOID, string strName)
			{
				this.m_iOID = iOID;
				this.m_strName = strName;
			}

			public override string ToString()
			{
				return this.m_strName;
			}
		}

		private IContainer components = null;

		private Button btnQuery;

		private Button btnCancel;

		private Timer timer1;

		private ComboBox comboRoad1;

		private Label label3;

		private Label label1;

		private ComboBox comboRoad2;

		public IAppContext m_context;

		public IMapControl3 m_MapControl;

		public IPipeConfig m_pPipeCfg;

		private IFeatureLayer m_pFtLayer;

		public int m_nTimerCount;

		private IGeometry m_pGeoFlash;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			this.btnQuery = new Button();
			this.btnCancel = new Button();
			this.timer1 = new Timer(this.components);
			this.comboRoad1 = new ComboBox();
			this.label3 = new Label();
			this.label1 = new Label();
			this.comboRoad2 = new ComboBox();
			base.SuspendLayout();
			this.btnQuery.Location = new System.Drawing.Point(136, 106);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new Size(75, 23);
			this.btnQuery.TabIndex = 0;
			this.btnQuery.Text = "查询";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.btnCancel.Location = new System.Drawing.Point(217, 106);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "关闭";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.comboRoad1.FormattingEnabled = true;
			this.comboRoad1.Location = new System.Drawing.Point(92, 37);
			this.comboRoad1.Name = "comboRoad1";
			this.comboRoad1.Size = new Size(200, 20);
			this.comboRoad1.TabIndex = 5;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 40);
			this.label3.Name = "label3";
			this.label3.Size = new Size(65, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "所选道路：";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 66);
			this.label1.Name = "label1";
			this.label1.Size = new Size(65, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "所选道路：";
			this.comboRoad2.FormattingEnabled = true;
			this.comboRoad2.Location = new System.Drawing.Point(92, 63);
			this.comboRoad2.Name = "comboRoad2";
			this.comboRoad2.Size = new Size(200, 20);
			this.comboRoad2.TabIndex = 5;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(314, 169);
			base.Controls.Add(this.comboRoad2);
			base.Controls.Add(this.comboRoad1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnQuery);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "QueryIntersectionUI";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "道路交叉口查询";
			base.Load += new EventHandler(this.QueryIntersectionUI_Load);
			base.Activated += new EventHandler(this.QueryIntersectionUI_Activated);
			base.Enter += new EventHandler(this.QueryIntersectionUI_Enter);
			base.FormClosed += new FormClosedEventHandler(this.QueryIntersectionUI_FormClosed);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public QueryIntersectionUI()
		{
			this.InitializeComponent();
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
				string a = iFLayer.Name.ToString();
				if (a == "道路中心线" || a == "SY_ZX_L")
				{
					this.m_pFtLayer = iFLayer;
				}
			}
		}

		private void UpdateView()
		{
			int layerCount = this.m_MapControl.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = this.m_MapControl.get_Layer(i);
				this.AddLayer(ipLay);
			}
			if (this.m_pFtLayer == null)
			{
				MessageBox.Show("没有加载道路中心线图层!");
				base.Close();
			}
			else
			{
				IFeatureClass featureClass = this.m_pFtLayer.FeatureClass;
				IFeatureCursor featureCursor = featureClass.Search(null, false);
				IFeature feature = featureCursor.NextFeature();
				int num = feature.Fields.FindField("名称");
				while (feature != null)
				{
					object obj = feature.get_Value(num);
					string text = obj as string;
					int oID = feature.OID;
					text.Replace(" ", "");
					if (text != null && "" != text && " " != text)
					{
						QueryIntersectionUI.ItemInfo item = new QueryIntersectionUI.ItemInfo(oID, text);
						this.comboRoad1.Items.Add(item);
						this.comboRoad2.Items.Add(item);
					}
					feature = featureCursor.NextFeature();
				}
			}
		}

		private void QueryIntersectionUI_Load(object sender, EventArgs e)
		{
			if (this.m_MapControl != null)
			{
				this.UpdateView();
			}
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			if (this.m_pFtLayer != null)
			{
				int num = this.comboRoad1.FindString(this.comboRoad1.Text);
				if (num < 0)
				{
					string text = string.Format("道路名称[{0}]错误！", this.comboRoad1.Text);
					MessageBox.Show(text);
				}
				else
				{
					QueryIntersectionUI.ItemInfo itemInfo = this.comboRoad1.Items[num] as QueryIntersectionUI.ItemInfo;
					int oID = itemInfo.OID;
					num = this.comboRoad1.FindString(this.comboRoad2.Text);
					if (num < 0)
					{
						string text2 = string.Format("道路名称[{0}]错误！", this.comboRoad2.Text);
						MessageBox.Show(text2);
					}
					else
					{
						itemInfo = (this.comboRoad1.Items[num] as QueryIntersectionUI.ItemInfo);
						int oID2 = itemInfo.OID;
						IFeatureClass featureClass = this.m_pFtLayer.FeatureClass;
						IFeature feature = featureClass.GetFeature(oID);
						IFeature feature2 = featureClass.GetFeature(oID2);
						IPolyline polyline = feature.Shape as IPolyline;
						ITopologicalOperator topologicalOperator = polyline as ITopologicalOperator;
						IGeometry geometry = null;
						if (topologicalOperator != null)
						{
							geometry = topologicalOperator.Intersect(feature2.Shape, (esriGeometryDimension) 1);
						}
						if (!geometry.IsEmpty)
						{
							IMultipoint multipoint = geometry as IMultipoint;
							IPointCollection pointCollection = multipoint as IPointCollection;
							IPoint point = pointCollection.get_Point(0);
							if (point != null)
							{
								this.m_pGeoFlash = point;
								this.timer1.Start();
								this.timer1.Interval = 100;
								IEnvelope envelope = new Envelope() as IEnvelope;
								envelope = this.m_MapControl.Extent;
								envelope.CenterAt(point);
								this.m_MapControl.Extent=(envelope);
								this.m_MapControl.Refresh((esriViewDrawPhase) 32, null, envelope);
								this.m_nTimerCount = 0;
								QueryIntersectionUI.NewBasePointElement(this.m_MapControl, point);
							}
						}
						else
						{
							MessageBox.Show("选择的道路没有交叉口!");
						}
					}
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			QueryIntersectionUI.DeleteAllElements(this.m_MapControl);
			base.Close();
		}

		private void QueryIntersectionUI_Activated(object sender, EventArgs e)
		{
		}

		private void QueryIntersectionUI_Enter(object sender, EventArgs e)
		{
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (!base.Visible || this.m_nTimerCount > 1)
			{
				this.m_nTimerCount = 0;
				this.timer1.Stop();
			    IActiveView activeView = m_context.ActiveView;
				activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
				this.m_pGeoFlash = null;
			}
			else
			{
				CMapOperator.ShowFeatureWithWink(m_context.ActiveView.ScreenDisplay, this.m_pGeoFlash);
				this.m_nTimerCount++;
			}
		}

		public static void NewBasePointElement(IMapControl3 pMapCtrl, IPoint pPoint)
		{
			IGraphicsContainer graphicsContainer = (IGraphicsContainer)pMapCtrl.ActiveView;
			
			ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=(255);
			rgbColor.Green=(0);
			rgbColor.Blue=(0);
			IRgbColor rgbColor2 = new RgbColor();
			rgbColor2.Red=(0);
			rgbColor2.Green=(0);
			rgbColor2.Blue=(0);
			simpleMarkerSymbol.Style=(esriSimpleMarkerStyle) (3);
			simpleMarkerSymbol.Color=(rgbColor);
			simpleMarkerSymbol.Outline=(true);
			simpleMarkerSymbol.OutlineSize=(1.0);
			simpleMarkerSymbol.OutlineColor=(rgbColor2);
			simpleMarkerSymbol.Size=(12.0);
			IElement element = new MarkerElement();
			IMarkerElement markerElement = element as IMarkerElement;
			markerElement.Symbol=(simpleMarkerSymbol);
			element.Geometry=(pPoint);
			graphicsContainer.AddElement(element, 0);
		}

		public static void DeleteAllElements(IMapControl3 pMapCtrl)
		{
			IGraphicsContainer graphicsContainer = (IGraphicsContainer)pMapCtrl.Map;
			graphicsContainer.DeleteAllElements();
			IActiveView activeView = pMapCtrl.ActiveView;
			activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			activeView.Refresh();
		}

		private void QueryIntersectionUI_FormClosed(object sender, FormClosedEventArgs e)
		{
			QueryIntersectionUI.DeleteAllElements(this.m_MapControl);
		}
	}
}
