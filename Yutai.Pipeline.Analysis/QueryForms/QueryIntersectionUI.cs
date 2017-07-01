using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class QueryIntersectionUI : Form
    {
        public partial class ItemInfo
        {
            private int m_iOID = -1;

            private string m_strName = "";

            public int OID
            {
                get { return this.m_iOID; }
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

        public IAppContext m_context;
        public IMapControl3 m_MapControl;
        public IPipelineConfig m_pPipeCfg;
        public int m_nTimerCount;

        public QueryIntersectionUI()
        {
            this.InitializeComponent();
        }

        private void AddLayer(ILayer ipLay)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer) ipLay);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer) ipLay);
            }
        }

        private void AddGroupLayer(IGroupLayer iGLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer) iGLayer;
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
                if (m_pPipeCfg.IsFunctionLayer(iFLayer.FeatureClass.AliasName, enumFunctionLayerType.RoadCenterLine))
                    this.m_pFtLayer = iFLayer;
            }
        }

        private void UpdateView()
        {
            int layerCount = this.m_MapControl.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = this.m_MapControl.Layer[i];
                this.AddLayer(ipLay);
            }
            if (this.m_pFtLayer == null)
            {
                MessageBox.Show(@"没有加载道路中心线图层!");
                base.Close();
            }
            else
            {
                IFeatureClass featureClass = this.m_pFtLayer.FeatureClass;
                IFunctionLayer layer = m_pPipeCfg.GetFunctionLayer(featureClass);
                IFeatureCursor featureCursor = featureClass.Search(null, false);
                IFeature feature = featureCursor.NextFeature();
                int num = feature.Fields.FindField(layer.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.DLMC));
                if (num >= 0)
                {
                    List<string> values = new List<string>();
                    CommonHelper.GetUniqueValues((ITable) featureClass,
                        layer.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.DLMC), values);
                    this.comboRoad1.Items.AddRange(values.ToArray());
                    this.comboRoad2.Items.AddRange(values.ToArray());
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
                    string text = $"道路名称[{this.comboRoad1.Text}]错误！";
                    MessageBox.Show(text);
                }
                else
                {
                    QueryIntersectionUI.ItemInfo itemInfo = this.comboRoad1.Items[num] as QueryIntersectionUI.ItemInfo;
                    int oID = itemInfo.OID;
                    num = this.comboRoad1.FindString(this.comboRoad2.Text);
                    if (num < 0)
                    {
                        string text2 = $"道路名称[{this.comboRoad2.Text}]错误！";
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
                                this.m_MapControl.Extent = (envelope);
                                this.m_MapControl.Refresh((esriViewDrawPhase) 32, null, envelope);
                                this.m_nTimerCount = 0;
                                QueryIntersectionUI.NewBasePointElement(this.m_MapControl, point);
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"选择的道路没有交叉口!");
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
            IGraphicsContainer graphicsContainer = (IGraphicsContainer) pMapCtrl.ActiveView;

            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
            IRgbColor rgbColor = new RgbColor();
            rgbColor.Red = 255;
            rgbColor.Green = 0;
            rgbColor.Blue = 0;
            IRgbColor rgbColor2 = new RgbColor();
            rgbColor2.Red = 0;
            rgbColor2.Green = 0;
            rgbColor2.Blue = 0;
            simpleMarkerSymbol.Style = (esriSimpleMarkerStyle) (3);
            simpleMarkerSymbol.Color = (rgbColor);
            simpleMarkerSymbol.Outline = (true);
            simpleMarkerSymbol.OutlineSize = (1.0);
            simpleMarkerSymbol.OutlineColor = (rgbColor2);
            simpleMarkerSymbol.Size = (12.0);
            IElement element = new MarkerElement();
            IMarkerElement markerElement = element as IMarkerElement;
            markerElement.Symbol = (simpleMarkerSymbol);
            element.Geometry = (pPoint);
            graphicsContainer.AddElement(element, 0);
        }

        public static void DeleteAllElements(IMapControl3 pMapCtrl)
        {
            IGraphicsContainer graphicsContainer = (IGraphicsContainer) pMapCtrl.Map;
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