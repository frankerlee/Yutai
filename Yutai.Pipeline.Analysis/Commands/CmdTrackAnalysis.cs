using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.PipeConfig;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdTrackAnalysis : YutaiTool
    {

        private IPipeConfig ipipeConfig_0;

        private IMapControl3 imapControl3_0;

        private TrackingAnalyForm trackingAnalyForm_0;

        private object object_0;


        public CmdTrackAnalysis(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);
            if (this.trackingAnalyForm_0 == null || this.trackingAnalyForm_0.IsDisposed)
            {
                this.trackingAnalyForm_0 = new TrackingAnalyForm();
                this.trackingAnalyForm_0.MapControl = this.imapControl3_0;
                this.trackingAnalyForm_0.pPipeCfg = this.ipipeConfig_0;
                this.trackingAnalyForm_0.m_iApp = _context;
                this.trackingAnalyForm_0.Show((Form)this.object_0);
            }
            else if (!this.trackingAnalyForm_0.Visible)
            {
                this.trackingAnalyForm_0.Show();
                if (this.trackingAnalyForm_0.WindowState == FormWindowState.Minimized)
                {
                    this.trackingAnalyForm_0.WindowState = FormWindowState.Normal;
                }
            }
            this.trackingAnalyForm_0.MapControl = this.imapControl3_0;
            this.trackingAnalyForm_0.pPipeCfg = this.ipipeConfig_0;
            this.trackingAnalyForm_0.Show();

        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "流向追踪";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_FlowAnalysis";
            base._key = "PipeAnalysis_FlowAnalysis";
            base.m_toolTip = "流向追踪";
            base.m_checked = false;
            base.m_message = "流向追踪";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        private void method_0(int num, IGeometry geometry)
        {
            if (num == 0)
            {
                ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                IRgbColor rgbColor = new RgbColor();
                simpleMarkerSymbol.Style=(esriSimpleMarkerStyle) (1);
                rgbColor.RGB=(Color.FromArgb(0, 255, 0).ToArgb());
                simpleMarkerSymbol.Color=(rgbColor);
                ISymbol symbol = (ISymbol)simpleMarkerSymbol;
                object obj = symbol;
                this.imapControl3_0.DrawShape(geometry, ref obj);
            }
            else if (num == 1)
            {
                ISimpleMarkerSymbol simpleMarkerSymbol2 = new SimpleMarkerSymbol();
                IRgbColor rgbColor2 = new RgbColor();
                simpleMarkerSymbol2.Style=(esriSimpleMarkerStyle) (2);
                rgbColor2.RGB=(Color.FromArgb(0, 0, 255).ToArgb());
                simpleMarkerSymbol2.Color=(rgbColor2);
                simpleMarkerSymbol2.Size=(12.0);
                simpleMarkerSymbol2.Angle=(45.0);
                ISymbol symbol2 = (ISymbol)simpleMarkerSymbol2;
                object obj2 = symbol2;
                this.imapControl3_0.DrawShape(geometry, ref obj2);
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            IActiveView activeView = _context.ActiveView;
            if (activeView != null)
            {
                activeView.ScreenDisplay.StartDrawing(0, -1);
                IActiveView activeView2 = _context.ActiveView;
               _context.FocusMap.ClearSelection();
                activeView2.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IPoint point = this.imapControl3_0.ToMapPoint(X, Y);
                IEnvelope envelope = new Envelope() as IEnvelope;
                double num = activeView.Extent.Width / 200.0;
                envelope.XMax = (point.X + num);
                envelope.XMin = (point.X - num);
                envelope.YMax = (point.Y + num);
                envelope.YMin = (point.Y - num);
                switch (this.trackingAnalyForm_0.DrawType)
                {
                    case 1:
                        {
                            IFeatureClass pSelectPointLayer = this.trackingAnalyForm_0.pSelectPointLayer;
                            IFeatureClass arg_103_0 = pSelectPointLayer;
                            ISpatialFilter spatialFilterClass = new SpatialFilter();
                            spatialFilterClass.Geometry=(envelope);
                            spatialFilterClass.SpatialRel=(esriSpatialRelEnum) (8);
                            IFeatureCursor featureCursor = arg_103_0.Search(spatialFilterClass, false);
                            IFeature feature = featureCursor.NextFeature();
                            if (feature != null)
                            {
                                this.trackingAnalyForm_0.AddJunctionFlag(feature);
                                this.method_0(0, point);
                            }
                            break;
                        }
                    case 2:
                        {
                            ISpatialFilter spatialFilter = new SpatialFilter();
                            IFeatureClass pSelectLineLayer = this.trackingAnalyForm_0.pSelectLineLayer;
                            spatialFilter.Geometry=(envelope);
                            spatialFilter.SpatialRel=(esriSpatialRelEnum) (6);
                            IFeatureCursor featureCursor2 = pSelectLineLayer.Search(spatialFilter, false);
                            IFeature feature2 = featureCursor2.NextFeature();
                            if (feature2 != null)
                            {
                                this.trackingAnalyForm_0.AddEdgeFlag(feature2);
                                this.method_0(0, point);
                            }
                            break;
                        }
                    case 3:
                        {
                            IFeatureClass pSelectPointLayer2 = this.trackingAnalyForm_0.pSelectPointLayer;
                            IFeatureClass arg_1BD_0 = pSelectPointLayer2;
                            ISpatialFilter spatialFilterClass2 = new SpatialFilter();
                            spatialFilterClass2.Geometry=(envelope);
                            spatialFilterClass2.SpatialRel=(esriSpatialRelEnum) (8);
                            IFeatureCursor featureCursor3 = arg_1BD_0.Search(spatialFilterClass2, false);
                            IFeature feature3 = featureCursor3.NextFeature();
                            if (feature3 != null)
                            {
                                this.trackingAnalyForm_0.AddJunctionBarrierFlag(feature3);
                                this.method_0(1, point);
                            }
                            break;
                        }
                    case 4:
                        {
                            IFeatureClass pSelectLineLayer2 = this.trackingAnalyForm_0.pSelectLineLayer;
                            IFeatureClass arg_214_0 = pSelectLineLayer2;
                            ISpatialFilter spatialFilterClass3 = new SpatialFilter();
                            spatialFilterClass3.Geometry = (envelope);
                            spatialFilterClass3.SpatialRel = (esriSpatialRelEnum)(6);
                            IFeatureCursor featureCursor4 = arg_214_0.Search(spatialFilterClass3, false);
                            IFeature feature4 = featureCursor4.NextFeature();
                            if (feature4 != null)
                            {
                                this.trackingAnalyForm_0.AddEdgeBarrierFlag(feature4);
                                this.method_0(1, point);
                            }
                            break;
                        }
                }
                activeView.ScreenDisplay.FinishDrawing();
            }
        }
    }
}