using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

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


        public CmdShortCutAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);
            this.ifeature_0 = null;
            this.ifeature_1 = null;
            this.ifeature_2 = null;

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
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_ShortestAnalysis";
            base._key = "PipeAnalysis_ShortestAnalysis";
            base.m_toolTip = "最短路径分析";
            base.m_checked = false;
            base.m_message = "最短路径分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IActiveView activeView = _context.ActiveView;
                _context.FocusMap.ClearSelection();
                activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IPoint point =_context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IEnvelope envelope = new Envelope() as IEnvelope;
                double num = activeView.Extent.Width / 200.0;
                envelope.XMax=(point.X + num);
                envelope.XMin=(point.X - num);
                envelope.YMax=(point.Y + num);
                envelope.YMin=(point.Y - num);
                _context.FocusMap.SelectByShape(envelope, null, true);
                activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
                IEnumFeature enumFeature = (MapSelection)_context.FocusMap.FeatureSelection;
                if (enumFeature != null)
                {
                    IFeature feature = enumFeature.Next();
                    if (feature != null)
                    {
                        this.StartAnalysis(feature);
                    }
                }
            }
        }

       public override  void Refresh(int hdc)
        {
            if (this.ifeature_0 != null)
            {
               _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, -1);
                ISimpleTextSymbol simpleTextSymbol = new TextSymbol() as ISimpleTextSymbol;
                ISimpleTextSymbol arg_72_0 = simpleTextSymbol;
                IRgbColor rgbColorClass = new RgbColor() {Blue = 255,Green=0,Red=0,Transparency = 30};
              
                arg_72_0.Color=(rgbColorClass);
                simpleTextSymbol.Size=(15.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol)simpleTextSymbol);
                _context.ActiveView.ScreenDisplay.DrawText(this.ifeature_1.Shape, "起点");
                _context.ActiveView.ScreenDisplay.FinishDrawing();
            }
            else if (this.ipolyline_0 != null)
            {
                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, -1);
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                IRgbColor rgbColor = new RgbColor() {Blue = 0,Red = 255,Green = 0,Transparency = 30};
            
                simpleLineSymbol.Color=(rgbColor);
                simpleLineSymbol.Width=(2.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol)simpleLineSymbol);
                _context.ActiveView.ScreenDisplay.DrawPolyline(this.ipolyline_0);
                ISimpleTextSymbol simpleTextSymbol2 = new TextSymbol() as ISimpleTextSymbol;
                rgbColor.Blue=(255);
                rgbColor.Green=(0);
                rgbColor.Red=(0);
                rgbColor.Transparency=(30);
                simpleTextSymbol2.Color=(rgbColor);
                simpleTextSymbol2.Size=(15.0);
                _context.ActiveView.ScreenDisplay.SetSymbol((ISymbol)simpleTextSymbol2);
                _context.ActiveView.ScreenDisplay.DrawText(this.ifeature_1.Shape, "起点");
                _context.ActiveView.ScreenDisplay.DrawText(this.ifeature_2.Shape, "终点");
                _context.ActiveView.ScreenDisplay.FinishDrawing();
            }
        }

        private void StartAnalysis(IFeature feature)
        {
            if (feature != null)
            {
                if (feature.FeatureType != (esriFeatureType) 7)
                {
                    MessageBox.Show("请选择管线点");
                    return;
                }
                IBasicLayerInfo lineConfig =
                    _plugin.PipeConfig.GetBasicLayerInfo(feature.Class.AliasName) as IBasicLayerInfo;

                if (lineConfig == null)
                {
                    MessageBox.Show("请选择管线点");
                    return;
                }
                 if (this.ifeature_0 == null)
                {
                    this.ifeature_0 = feature;
                    this.ifeature_1 = feature;
                }
                else
                 {
                     string text = lineConfig.GetFieldName(PipeConfigWordHelper.PointWords.TZW);
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
                    this.string_0 = arg;
                    if (this.ifeature_0.Class != feature.Class)
                    {
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
                                    ShowShortObjectForm showShortObjectForm = new ShowShortObjectForm(_context);
                                    showShortObjectForm.pApp = _context;
                                    ISpatialReference spatialReference = _context.FocusMap.SpatialReference;
                                    IEIDHelper eIDHelperClass = new EIDHelper();
                                    eIDHelperClass.GeometricNetwork=(networkClass.GeometricNetwork);
                                    eIDHelperClass.OutputSpatialReference=(spatialReference);
                                    eIDHelperClass.ReturnGeometries=(true);
                                    eIDHelperClass.ReturnFeatures=(true);
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
                                    IEnvelope envelope = new Envelope() as IEnvelope;
                                    envelope.XMax=(this.ipolyline_0.Envelope.XMax + 20.0);
                                    envelope.XMin=(this.ipolyline_0.Envelope.XMin - 20.0);
                                    envelope.YMax=(this.ipolyline_0.Envelope.YMax + 20.0);
                                    envelope.YMin=(this.ipolyline_0.Envelope.YMin - 20.0);
                                    _context.ActiveView.Extent=(envelope);
                                    CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, this.ipolyline_0);
                                    this.ifeature_0 = null;
                                    showShortObjectForm.Show();
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
        }
    }
}