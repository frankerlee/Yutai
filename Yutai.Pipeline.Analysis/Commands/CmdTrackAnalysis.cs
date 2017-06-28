using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;

using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdTrackAnalysis : YutaiTool
    {

        private IPipelineConfig ipipeConfig_0;

        private IMapControl3 mapControl;

        private TrackingAnalyForm trackingAnalyForm;
        private object object_0;
        private PipelineAnalysisPlugin _plugin;

        public CmdTrackAnalysis(IAppContext context,PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
            mapControl = _context.MapControl as IMapControl3;
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);
            if (this.trackingAnalyForm == null || this.trackingAnalyForm.IsDisposed)
            {
                this.trackingAnalyForm = new TrackingAnalyForm();
                this.trackingAnalyForm.MapControl = this._context.MapControl as IMapControl3;
                this.trackingAnalyForm.pPipeCfg = this._plugin.PipeConfig;
                this.trackingAnalyForm.m_iApp = _context;
                this.trackingAnalyForm.Show((Form)this.object_0);
            }
            else if (!this.trackingAnalyForm.Visible)
            {
                this.trackingAnalyForm.Show();
                if (this.trackingAnalyForm.WindowState == FormWindowState.Minimized)
                {
                    this.trackingAnalyForm.WindowState = FormWindowState.Normal;
                }
            }
            this.trackingAnalyForm.MapControl = this._context.MapControl as IMapControl3;
            this.trackingAnalyForm.pPipeCfg = this._plugin.PipeConfig; ;
            this.trackingAnalyForm.Show();

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
            base.m_bitmap = Properties.Resources.icon_track;
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
                this.mapControl.DrawShape(geometry, ref obj);
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
                this.mapControl.DrawShape(geometry, ref obj2);
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
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                IEnvelope envelope = new Envelope() as IEnvelope;
                double num = activeView.Extent.Width / 200.0;
                envelope.XMax = (point.X + num);
                envelope.XMin = (point.X - num);
                envelope.YMax = (point.Y + num);
                envelope.YMin = (point.Y - num);
                switch (this.trackingAnalyForm.DrawType)
                {
                    case 1:
                        {
                            IFeatureClass pSelectPointLayer = this.trackingAnalyForm.pSelectPointLayer;
                           
                            ISpatialFilter spatialFilterClass = new SpatialFilter();
                            spatialFilterClass.Geometry=(envelope);
                            spatialFilterClass.SpatialRel=(esriSpatialRelEnum) (8);
                            IFeatureCursor featureCursor = pSelectPointLayer.Search(spatialFilterClass, false);
                            IFeature feature = featureCursor.NextFeature();
                            if (feature != null)
                            {
                                this.trackingAnalyForm.AddJunctionFlag(feature);
                                this.method_0(0, point);
                            }
                            break;
                        }
                    case 2:
                        {
                            ISpatialFilter spatialFilter = new SpatialFilter();
                            IFeatureClass pSelectLineLayer = this.trackingAnalyForm.pSelectLineLayer;
                            spatialFilter.Geometry=(envelope);
                            spatialFilter.SpatialRel=(esriSpatialRelEnum) (6);
                            IFeatureCursor featureCursor2 = pSelectLineLayer.Search(spatialFilter, false);
                            IFeature feature2 = featureCursor2.NextFeature();
                            if (feature2 != null)
                            {
                                this.trackingAnalyForm.AddEdgeFlag(feature2);
                                this.method_0(0, point);
                            }
                            break;
                        }
                    case 3:
                        {
                            IFeatureClass pSelectPointLayer2 = this.trackingAnalyForm.pSelectPointLayer;
                          
                            ISpatialFilter spatialFilterClass2 = new SpatialFilter();
                            spatialFilterClass2.Geometry=(envelope);
                            spatialFilterClass2.SpatialRel=(esriSpatialRelEnum) (8);
                            IFeatureCursor featureCursor3 = pSelectPointLayer2.Search(spatialFilterClass2, false);
                            IFeature feature3 = featureCursor3.NextFeature();
                            if (feature3 != null)
                            {
                                this.trackingAnalyForm.AddJunctionBarrierFlag(feature3);
                                this.method_0(1, point);
                            }
                            break;
                        }
                    case 4:
                        {
                            IFeatureClass pSelectLineLayer2 = this.trackingAnalyForm.pSelectLineLayer;
                          
                            ISpatialFilter spatialFilterClass3 = new SpatialFilter();
                            spatialFilterClass3.Geometry = (envelope);
                            spatialFilterClass3.SpatialRel = (esriSpatialRelEnum)(6);
                            IFeatureCursor featureCursor4 = pSelectLineLayer2.Search(spatialFilterClass3, false);
                            IFeature feature4 = featureCursor4.NextFeature();
                            if (feature4 != null)
                            {
                                this.trackingAnalyForm.AddEdgeBarrierFlag(feature4);
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