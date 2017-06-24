using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdConnectivityAnalysis : YutaiTool
    {

        private IFeature ifeature_0;

        private IPolyline ipolyline_0;

        private IPipelineConfig _pipelineConfig;


        public CmdConnectivityAnalysis(IAppContext context,PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _pipelineConfig = plugin.PipeConfig;
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);

            this.ifeature_0 = null;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption ="解析加点(&A)";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_ConnectivityAnalysis";
            base._key = "PipeAnalysis_ConnectivityAnalysis";
            base.m_toolTip = "解析加点";
            base.m_checked = false;
            base.m_message = "解析加点";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {

        }


        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IActiveView activeView = _context.ActiveView;
                this._context.FocusMap.ClearSelection();
                activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IEnvelope envelope = new Envelope() as IEnvelope;
                double num = activeView.Extent.Width/200.0;
                envelope.XMax=(point.X + num);
                envelope.XMin=(point.X - num);
                envelope.YMax=(point.Y + num);
                envelope.YMin=(point.Y - num);
                _context.FocusMap.SelectByShape(envelope, null, true);
                activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IEnumFeature enumFeature =
                    (MapSelection) _context.FocusMap.FeatureSelection;
                if (enumFeature != null)
                {
                    IFeature feature = enumFeature.Next();
                    if (feature != null)
                    {
                        this.method_0(feature);
                    }
                }
            }
        }

        private void method_0(IFeature feature)
        {
            if (feature != null)
            {
                if (feature.FeatureType != esriFeatureType.esriFTSimpleJunction)
                {
                    MessageBox.Show("请选择管线点");
                }
                else if (!_pipelineConfig.IsPipelineLayer(feature.Class.AliasName,enumPipelineDataType.Point))
                {
                    MessageBox.Show("请选择管线点");
                }
                else if (this.ifeature_0 == null)
                {
                    this.ifeature_0 = feature;
                    //this._context.ActiveView.axMapControl.Invalidate(false);
                }
                else if (this.ifeature_0.Class != feature.Class)
                {
                    string text = "管线性质";
                    string arg = "";
                    int num = this.ifeature_0.Fields.FindField(text);
                    if (num != -1)
                    {
                        object obj = this.ifeature_0.get_Value(num);
                        if (obj == null || Convert.IsDBNull(obj))
                        {
                            arg = "";
                        }
                        else
                        {
                            arg = obj.ToString();
                        }
                    }
                    string text2 = string.Format("需要选择的管线点层【{0}】", arg);
                    MessageBox.Show(text2);
                }
                else
                {
                    try
                    {
                        INetworkClass networkClass = feature.Class as INetworkClass;
                        INetwork network = networkClass.GeometricNetwork.Network;
                        INetElements netElements = network as INetElements;
                        IJunctionFlag[] array = new JunctionFlag[2];
                        INetFlag netFlag = new JunctionFlag() as INetFlag;
                        ISimpleJunctionFeature simpleJunctionFeature = feature as ISimpleJunctionFeature;
                        ISimpleJunctionFeature simpleJunctionFeature2 = this.ifeature_0 as ISimpleJunctionFeature;
                        if (simpleJunctionFeature2.EID == simpleJunctionFeature.EID)
                        {
                            MessageBox.Show("起点终点为同一个点!");
                        }
                        else
                        {
                            int userClassID;
                            int userID;
                            int userSubID;
                            netElements.QueryIDs(simpleJunctionFeature2.EID, (esriElementType) 1, out userClassID, out userID, out userSubID);
                            netFlag.UserClassID=(userClassID);
                            netFlag.UserID=(userID);
                            netFlag.UserSubID=(userSubID);
                            IJunctionFlag value = netFlag as IJunctionFlag;
                            array.SetValue(value, 0);
                            INetFlag netFlag2 = new JunctionFlag() as INetFlag;
                            netElements.QueryIDs(simpleJunctionFeature.EID, (esriElementType) 1, out userClassID, out userID, out userSubID);
                            netFlag2.UserClassID=(userClassID);
                            netFlag2.UserID=(userID);
                            netFlag2.UserSubID=(userSubID);
                            value = (netFlag2 as IJunctionFlag);
                            array.SetValue(value, 1);
                            ITraceFlowSolverGEN traceFlowSolverGEN = new TraceFlowSolver() as ITraceFlowSolverGEN;
                            INetSolver netSolver = traceFlowSolverGEN as INetSolver;
                            netSolver.SourceNetwork=(network);
                            traceFlowSolverGEN.PutJunctionOrigins(ref array);
                            object[] array2 = new object[1];
                            IEnumNetEID enumNetEID;
                            IEnumNetEID enumNetEID2;
                            traceFlowSolverGEN.FindPath((esriFlowMethod) 2, (esriShortestPathObjFn) 1, out enumNetEID, out enumNetEID2, 1, ref array2);
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
                                ISpatialReference spatialReference = _context.FocusMap.SpatialReference;
                                IEIDHelper eIDHelperClass = new EIDHelper();
                                eIDHelperClass.GeometricNetwork=(networkClass.GeometricNetwork);
                                eIDHelperClass.OutputSpatialReference=(spatialReference);
                                eIDHelperClass.ReturnGeometries=(true);
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
                                CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, this.ipolyline_0);
                                this.ifeature_0 = null;
                                IEnvelope envelope = new Envelope() as IEnvelope;
                                envelope.XMax=(this.ipolyline_0.Envelope.XMax + 20.0);
                                envelope.XMin=(this.ipolyline_0.Envelope.XMin - 20.0);
                                envelope.YMax=(this.ipolyline_0.Envelope.YMax + 20.0);
                                envelope.YMin=(this.ipolyline_0.Envelope.YMin - 20.0);
                                _context.ActiveView.FullExtent=(envelope);
                                _context.ActiveView.Extent=(envelope);
                                string text3 = string.Format("两个管线点之间连通,最短路径为{0}米", num2.ToString("f2"));
                                MessageBox.Show(text3);
                            }
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

        public override void Refresh(int hdc)
        {
            if (this.ipolyline_0 != null)
            {
                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, -1);
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                ISimpleLineSymbol arg_72_0 = simpleLineSymbol;
                IRgbColor rgbColorClass = new RgbColor();
                rgbColorClass.Blue=(0);
                rgbColorClass.Green=(0);
                rgbColorClass.Red=(255);
                rgbColorClass.Transparency=(30);
                arg_72_0.Color=(rgbColorClass);
                simpleLineSymbol.Width=(2.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol)simpleLineSymbol);
                _context.ActiveView.ScreenDisplay.DrawPolyline(this.ipolyline_0);
                _context.ActiveView.ScreenDisplay.FinishDrawing();
            }
        }
    }
}