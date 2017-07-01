using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars.Commands.Ribbon;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Pipeline.Analysis.Commands
{
    //!
    class CmdConnectivityAnalysis : YutaiTool
    {
        private IFeature _iFeature;
        private IPolyline ipolyline_0;
        private IPipelineConfig _pipelineConfig;
        private PipelineAnalysisPlugin _plugin;
        private IGeometricNetwork _geometricNetwork;
        private int _startEid;
        private IPoint _startPoint;
        private int _endEid;
        private IPoint _endPoint;

        public CmdConnectivityAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);

            //this._iFeature = null;
            _pipelineConfig = _plugin.PipeConfig;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "连通性分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_connection;
            base.m_name = "PipeAnalysis_ConnectivityAnalysis";
            base._key = "PipeAnalysis_ConnectivityAnalysis";
            base.m_toolTip = "连通性分析";
            base.m_checked = false;
            base.m_message = "连通性分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {
        }


        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IActiveView activeView = _context.ActiveView;
                this._context.FocusMap.ClearSelection();
                activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                StartQueryJunction(point);
                //IEnvelope envelope = new Envelope() as IEnvelope;
                //double num = activeView.Extent.Width/200.0;
                //envelope.XMax = (point.X + num);
                //envelope.XMin = (point.X - num);
                //envelope.YMax = (point.Y + num);
                //envelope.YMin = (point.Y - num);
                //_context.FocusMap.SelectByShape(envelope, null, true);
                //activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                //IEnumFeature enumFeature =
                //    (MapSelection) _context.FocusMap.FeatureSelection;
                //if (enumFeature != null)
                //{
                //    IFeature feature = enumFeature.Next();
                //    if (feature != null)
                //    {
                //        this.ValidateFeature(feature);
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
                    this.ValidateFeature(feature);
                }
            }
        }

        private void ValidateFeature(IFeature feature)
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
                IJunctionFlag[] array = new JunctionFlag[2];
                INetFlag netFlag = new JunctionFlag() as INetFlag;

                int userClassID;
                int userID;
                int userSubID;
                netElements.QueryIDs(_endEid, esriElementType.esriETJunction, out userClassID,
                    out userID, out userSubID);
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
                traceFlowSolverGEN.FindPath(esriFlowMethod.esriFMConnected,
                    (esriShortestPathObjFn.esriSPObjFnMinMax), out enumNetEID, out enumNetEID2, 1, ref array2);
                if (this.ipolyline_0 == null)
                {
                    this.ipolyline_0 = new Polyline() as IPolyline;
                }
                IGeometryCollection geometryCollection = this.ipolyline_0 as IGeometryCollection;
                geometryCollection.RemoveGeometries(0, geometryCollection.GeometryCount);
                if (enumNetEID2.Count <= 0)
                {
                    this._iFeature = null;
                    MessageService.Current.Warn("两点之间不存在路径可以连通！");
                    return;
                }
                else
                {
                    ISpatialReference spatialReference = _context.FocusMap.SpatialReference;
                    IEIDHelper eIDHelperClass = new EIDHelper();
                    eIDHelperClass.GeometricNetwork = _geometricNetwork;
                    eIDHelperClass.OutputSpatialReference = (spatialReference);
                    eIDHelperClass.ReturnGeometries = (true);
                    IEnumEIDInfo enumEIDInfo = eIDHelperClass.CreateEnumEIDInfo(enumNetEID2);
                    int count = enumEIDInfo.Count;
                    enumEIDInfo.Reset();
                    double num2 = 0.0;
                    for (int i = 0; i < count; i++)
                    {
                        IEIDInfo iEIDInfo = enumEIDInfo.Next();
                        IGeometry geometry = iEIDInfo.Geometry;
                        IPolyline polyline = geometry as IPolyline;
                        num2 += polyline.Length;
                        geometryCollection.AddGeometryCollection(geometry as IGeometryCollection);
                    }

                    EsriUtils.ZoomToGeometry(this.ipolyline_0, _context.MapControl.Map, 1.3);
                    FlashUtility.FlashGeometry(this.ipolyline_0, _context.MapControl);

                    //_context.ActiveView.Refresh();
                    //CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, this.ipolyline_0);
                    this._iFeature = null;
                    _startEid = 0;
                    _startPoint = null;
                    _geometricNetwork = null;

                    string text3 = string.Format("两个管线点之间连通,最短路径为{0}米", num2.ToString("f2"));
                    MessageService.Current.Info(text3);
                }
            }
            catch (Exception ex)
            {
                this._iFeature = null;
                _startEid = 0;
                _startPoint = null;
                _geometricNetwork = null;
                MessageService.Current.Warn(ex.Message);
            }
        }

        public override void Refresh(int hdc)
        {
            if (this.ipolyline_0 != null)
            {
                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, -1);
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();

                IRgbColor rgbColorClass = new RgbColor();
                rgbColorClass.Blue = (0);
                rgbColorClass.Green = (0);
                rgbColorClass.Red = (255);
                rgbColorClass.Transparency = (30);
                simpleLineSymbol.Color = (rgbColorClass);
                simpleLineSymbol.Width = (2.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol) simpleLineSymbol);
                _context.ActiveView.ScreenDisplay.DrawPolyline(this.ipolyline_0);
                _context.ActiveView.ScreenDisplay.FinishDrawing();
            }
        }
    }
}