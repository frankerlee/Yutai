using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdShortCutAnalysis : YutaiTool
    {
        private IFeature ifeature_0;
        private IPolyline ipolyline_0;
        private string string_0 = "";
        private IFeature ifeature_1;
        private IFeature ifeature_2;
        private PipelineAnalysisPlugin _plugin;

        private IPipelineConfig _pipelineConfig;
        private IGeometricNetwork _geometricNetwork;
        private int _startEid;
        private IPoint _startPoint;
        private int _endEid;
        private IPoint _endPoint;


        public CmdShortCutAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
            _pipelineConfig = _plugin.PipeConfig;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "最短路径分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_short_path;
            base.m_name = "PipeAnalysis_ShortestAnalysis";
            base._key = "PipeAnalysis_ShortestAnalysis";
            base.m_toolTip = "最短路径分析";
            base.m_checked = false;
            base.m_message = "最短路径分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IActiveView activeView = _context.ActiveView;
                _context.FocusMap.ClearSelection();
                activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                StartQueryJunction(point);
                //IEnvelope envelope = new Envelope() as IEnvelope;
                //double num = activeView.Extent.Width / 200.0;
                //envelope.XMax=(point.X + num);
                //envelope.XMin=(point.X - num);
                //envelope.YMax=(point.Y + num);
                //envelope.YMin=(point.Y - num);
                //_context.FocusMap.SelectByShape(envelope, null, true);
                //activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                //IEnumFeature enumFeature = (MapSelection)_context.FocusMap.FeatureSelection;
                //if (enumFeature != null)
                //{
                //    IFeature feature = enumFeature.Next();
                //    if (feature != null)
                //    {
                //        this.StartAnalysis(feature);
                //    }
                //}
            }
            else
            {
                this._startPoint = null;
                this._geometricNetwork = null;
                this._startEid = 0;
            }
        }

        private void StartQueryJunction(IPoint point)
        {
            double dist = CommonUtils.ConvertPixelsToMapUnits(_context.ActiveView, _context.Config.SnapTolerance);
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.XMax = (point.X + dist);
            envelope.XMin = (point.X - dist);
            envelope.YMax = (point.Y + dist);
            envelope.YMin = (point.Y - dist);
            _context.FocusMap.SelectByShape(envelope, null, true);
            _context.ActiveView.PartialRefresh((esriViewDrawPhase) 4, null, null);
            IEnumFeature enumFeature =
                (MapSelection) _context.FocusMap.FeatureSelection;
            if (enumFeature != null)
            {
                IFeature feature = enumFeature.Next();
                if (feature != null)
                {
                    this.StartAnalysis(feature);
                }
            }
        }

        public override void Refresh(int hdc)
        {
            if (this.ifeature_0 != null)
            {
                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, -1);
                ISimpleTextSymbol simpleTextSymbol = new TextSymbol() as ISimpleTextSymbol;
                ISimpleTextSymbol arg_72_0 = simpleTextSymbol;
                IRgbColor rgbColorClass = new RgbColor() {Blue = 255, Green = 0, Red = 0, Transparency = 30};

                arg_72_0.Color = (rgbColorClass);
                simpleTextSymbol.Size = (15.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol) simpleTextSymbol);
                _context.ActiveView.ScreenDisplay.DrawText(this.ifeature_1.Shape, "起点");
                _context.ActiveView.ScreenDisplay.FinishDrawing();
            }
            else if (this.ipolyline_0 != null)
            {
                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, -1);
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                IRgbColor rgbColor = new RgbColor() {Blue = 0, Red = 255, Green = 0, Transparency = 30};

                simpleLineSymbol.Color = (rgbColor);
                simpleLineSymbol.Width = (2.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol) simpleLineSymbol);
                _context.ActiveView.ScreenDisplay.DrawPolyline(this.ipolyline_0);
                ISimpleTextSymbol simpleTextSymbol2 = new TextSymbol() as ISimpleTextSymbol;
                rgbColor.Blue = (255);
                rgbColor.Green = (0);
                rgbColor.Red = (0);
                rgbColor.Transparency = (30);
                simpleTextSymbol2.Color = (rgbColor);
                simpleTextSymbol2.Size = (15.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol) simpleTextSymbol2);
                _context.ActiveView.ScreenDisplay.DrawText(this.ifeature_1.Shape, "起点");
                _context.ActiveView.ScreenDisplay.DrawText(this.ifeature_2.Shape, "终点");
                _context.ActiveView.ScreenDisplay.FinishDrawing();
            }
        }

        private void StartAnalysis(IFeature feature)
        {
            if (feature.FeatureType != esriFeatureType.esriFTSimpleJunction)
            {
                MessageService.Current.Warn("请选择管线点");
                return;
            }
            if (!_pipelineConfig.IsPipelineLayer(feature.Class.AliasName, enumPipelineDataType.Point))
            {
                MessageService.Current.Warn("请选择管线点");
                return;
            }
            double snapDist = CommonUtils.ConvertPixelsToMapUnits(_context.ActiveView,
                _context.Config.SnapTolerance);
            IBasicLayerInfo lineConfig =
                _plugin.PipeConfig.GetBasicLayerInfo(feature.Class as IFeatureClass) as IBasicLayerInfo;

            if (this._startPoint == null && _startEid == 0)
            {
                //开始记录起始点
                IPipelineLayer oldLayer = _pipelineConfig.GetPipelineLayer(feature.Class.AliasName,
                    enumPipelineDataType.Point);
                if (oldLayer == null)
                {
                    MessageService.Current.Warn("你选择的图层不是合法的管线图层!");
                    return;
                }
                List<IBasicLayerInfo> basicInfos = oldLayer.GetLayers(enumPipelineDataType.Junction);

                IFeatureClass featureClass = basicInfos.Count > 0 ? basicInfos[0].FeatureClass : null;
                if (featureClass == null)
                {
                    MessageService.Current.Warn("管线图层没有构建网络图层!");
                    return;
                }
                INetworkClass networkClass = featureClass as INetworkClass;
                _geometricNetwork = networkClass.GeometricNetwork;
                IPointToEID pnToEid = new PointToEIDClass();
                pnToEid.GeometricNetwork = _geometricNetwork;
                pnToEid.SnapTolerance = snapDist;
                pnToEid.SourceMap = _context.FocusMap;
                pnToEid.GetNearestJunction(feature.Shape as IPoint, out _startEid, out _startPoint);
                return;
            }
            IPipelineLayer newLayer = _pipelineConfig.GetPipelineLayer(feature.Class.AliasName,
                enumPipelineDataType.Point);
            if (newLayer == null)
            {
                MessageService.Current.Warn("你选择的图层不是合法的管线图层!");
                return;
            }
            List<IBasicLayerInfo> basicInfos1 = newLayer.GetLayers(enumPipelineDataType.Junction);

            IFeatureClass featureClass2 = basicInfos1.Count > 0 ? basicInfos1[0].FeatureClass : null;
            if (featureClass2 == null)
            {
                MessageService.Current.Warn("第二个管线图层没有构建网络图层!");
                return;
            }
            INetworkClass networkClass2 = featureClass2 as INetworkClass;
            if (networkClass2.GeometricNetwork != _geometricNetwork)
            {
                if (MessageService.Current.Ask("两个点位属于不同的网络图层，使用第二个网络图层作为分析图层吗?") == false)
                {
                    return;
                }
                _geometricNetwork = networkClass2.GeometricNetwork;
                IPointToEID pnToEid = new PointToEIDClass();
                pnToEid.GeometricNetwork = _geometricNetwork;
                pnToEid.SnapTolerance = snapDist;
                pnToEid.SourceMap = _context.FocusMap;
                pnToEid.GetNearestJunction(feature.Shape as IPoint, out _startEid, out _startPoint);
                return;
            }

            try
            {
                IPointToEID pntEid = new PointToEIDClass();
                pntEid.GeometricNetwork = _geometricNetwork;
                pntEid.SourceMap = _context.FocusMap;
                pntEid.SnapTolerance = snapDist;

                pntEid.GetNearestJunction(feature.Shape as IPoint, out _endEid, out _endPoint);
                if (_endEid < 1)
                {
                    MessageService.Current.Warn("未能找到第二个分析点!");
                    return;
                }
                if (_startEid == _endEid)
                {
                    MessageService.Current.Warn("起点终点为同一个点!");
                    return;
                }

                INetElements netElements = _geometricNetwork.Network as INetElements;
                INetworkClass networkClass = feature.Class as INetworkClass;

                IJunctionFlag[] array = new JunctionFlag[2];
                INetFlag netFlag = new JunctionFlag() as INetFlag;

                int userClassID;
                int userID;
                int userSubID;
                netElements.QueryIDs(_endEid, esriElementType.esriETJunction, out userClassID, out userID, out userSubID);
                netFlag.UserClassID = (userClassID);
                netFlag.UserID = (userID);
                netFlag.UserSubID = (userSubID);
                IJunctionFlag value = netFlag as IJunctionFlag;
                array.SetValue(value, 0);
                INetFlag netFlag2 = new JunctionFlag() as INetFlag;
                netElements.QueryIDs(_startEid, esriElementType.esriETJunction, out userClassID,
                    out userID, out userSubID);
                netFlag2.UserClassID = (userClassID);
                netFlag2.UserID = (userID);
                netFlag2.UserSubID = (userSubID);
                value = (netFlag2 as IJunctionFlag);
                array.SetValue(value, 1);
                ITraceFlowSolverGEN traceFlowSolverGEN = new TraceFlowSolver() as ITraceFlowSolverGEN;
                INetSolver netSolver = traceFlowSolverGEN as INetSolver;
                netSolver.SourceNetwork = _geometricNetwork.Network;
                traceFlowSolverGEN.PutJunctionOrigins(ref array);
                object[] array2 = new object[1];
                IEnumNetEID enumNetEID;
                IEnumNetEID enumNetEID2;
                traceFlowSolverGEN.FindPath((esriFlowMethod) 2, (esriShortestPathObjFn) 1, out enumNetEID,
                    out enumNetEID2, 1, ref array2);
                if (this.ipolyline_0 == null)
                {
                    this.ipolyline_0 = new Polyline() as IPolyline;
                }
                IGeometryCollection geometryCollection = this.ipolyline_0 as IGeometryCollection;
                geometryCollection.RemoveGeometries(0, geometryCollection.GeometryCount);
                if (enumNetEID2.Count <= 0)
                {
                    this.ifeature_0 = null;
                    MessageBox.Show("两点之间不存在路径可以连通！");
                }
                else
                {
                    ShowShortObjectForm showShortObjectForm = new ShowShortObjectForm(_context);
                    showShortObjectForm.pApp = _context;
                    ISpatialReference spatialReference = _context.FocusMap.SpatialReference;
                    IEIDHelper eIDHelperClass = new EIDHelper();
                    eIDHelperClass.GeometricNetwork = (networkClass.GeometricNetwork);
                    eIDHelperClass.OutputSpatialReference = (spatialReference);
                    eIDHelperClass.ReturnGeometries = (true);
                    eIDHelperClass.ReturnFeatures = (true);
                    IEnumEIDInfo enumEIDInfo = eIDHelperClass.CreateEnumEIDInfo(enumNetEID2);
                    int count = enumEIDInfo.Count;
                    enumEIDInfo.Reset();
                    for (int i = 0; i < count; i++)
                    {
                        IEIDInfo iEIDInfo = enumEIDInfo.Next();
                        IGeometry geometry = iEIDInfo.Geometry;
                        if (i == 0)
                        {
                            showShortObjectForm.AddPipeName(this.string_0);
                        }
                        showShortObjectForm.AddFeature(iEIDInfo.Feature);
                        geometryCollection.AddGeometryCollection(geometry as IGeometryCollection);
                    }
                    showShortObjectForm.AddShortPath(this.ipolyline_0);
                    showShortObjectForm.AddLenght(this.ipolyline_0.Length);
                    this.ifeature_2 = feature;
                    EsriUtils.ZoomToGeometry(this.ipolyline_0, _context.MapControl.Map, 1.3);
                    FlashUtility.FlashGeometry(this.ipolyline_0, _context.MapControl);
                    this.ifeature_0 = null;
                    _startEid = 0;
                    _startPoint = null;
                    _geometricNetwork = null;
                    showShortObjectForm.Show();
                }
            }
            catch (Exception ex)
            {
                this.ifeature_0 = null;
                MessageBox.Show(ex.Message);
            }
        }
    }
}