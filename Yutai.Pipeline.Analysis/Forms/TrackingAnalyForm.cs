using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class TrackingAnalyForm : Form
    {
        private partial class LayerInfo
        {
            public IFeatureLayer _layer;

            public override string ToString()
            {
                return this._layer.Name;
            }
        }

        private IContainer icontainer_0 = null;


        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;

        public IAppContext m_iApp;

        private IList<IFeature> listJunctionFlag = new List<IFeature>();

        private IList<IFeature> listEdgeFlag = new List<IFeature>();

        private IList<IFeature> listJunctionBarrier = new List<IFeature>();

        private IList<IFeature> listEdgeBarrier = new List<IFeature>();

        private IList<IFeature> ilist_4 = new List<IFeature>();

        public int DrawType
        {
            get
            {
                int result;
                if (this.radioButton1.Checked)
                {
                    result = 1;
                }
                else if (this.radioButton2.Checked)
                {
                    result = 2;
                }
                else if (this.radioButton3.Checked)
                {
                    result = 3;
                }
                else if (this.radioButton4.Checked)
                {
                    result = 4;
                }
                else
                {
                    result = 0;
                }
                return result;
            }
        }

        public IFeatureClass pSelectPointLayer
        {
            get
            {
                int selectedIndex = this.LayerCom.SelectedIndex;
                IFeatureClass result;
                if (selectedIndex < 0)
                {
                    result = null;
                }
                else if (this.MapControl == null)
                {
                    result = null;
                }
                else
                {
                    IFeatureClass featureClass =
                        ((TrackingAnalyForm.LayerInfo) this.LayerCom.SelectedItem)._layer.FeatureClass;
                    if (featureClass == null)
                    {
                        result = null;
                    }
                    else
                    {
                        INetworkClass networkClass = featureClass as INetworkClass;
                        if (networkClass == null)
                        {
                            result = null;
                        }
                        else
                        {
                            IGeometricNetwork geometricNetwork = networkClass.GeometricNetwork;
                            IEnumFeatureClass enumFeatureClass =
                                geometricNetwork.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
                            enumFeatureClass.Reset();
                            featureClass = enumFeatureClass.Next();
                            if (featureClass.AliasName.Contains("Junctions"))
                            {
                                featureClass = enumFeatureClass.Next();
                            }
                            result = featureClass;
                        }
                    }
                }
                return result;
            }
        }

        public IFeatureClass pSelectLineLayer
        {
            get
            {
                int selectedIndex = this.LayerCom.SelectedIndex;
                IFeatureClass result;
                if (selectedIndex < 0)
                {
                    result = null;
                }
                else if (this.MapControl == null)
                {
                    result = null;
                }
                else
                {
                    IFeatureClass featureClass =
                        ((TrackingAnalyForm.LayerInfo) this.LayerCom.SelectedItem)._layer.FeatureClass;
                    if (featureClass == null)
                    {
                        result = null;
                    }
                    else
                    {
                        INetworkClass networkClass = featureClass as INetworkClass;
                        if (networkClass == null)
                        {
                            result = null;
                        }
                        else
                        {
                            IGeometricNetwork geometricNetwork = networkClass.GeometricNetwork;
                            IEnumFeatureClass enumFeatureClass =
                                geometricNetwork.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
                            enumFeatureClass.Reset();
                            result = enumFeatureClass.Next();
                        }
                    }
                }
                return result;
            }
        }

        public TrackingAnalyForm()
        {
            this.InitializeComponent();
        }

        public void AddJunctionFlag(IFeature PointFeature)
        {
            this.listJunctionFlag.Add(PointFeature);
        }

        public void AddEdgeFlag(IFeature PointFeature)
        {
            this.listEdgeFlag.Add(PointFeature);
        }

        public void AddJunctionBarrierFlag(IFeature PointFeature)
        {
            this.listJunctionBarrier.Add(PointFeature);
        }

        public void AddEdgeBarrierFlag(IFeature PointFeature)
        {
            this.listEdgeBarrier.Add(PointFeature);
        }

        private void TrackingAnalyForm_Load(object obj, EventArgs eventArgs)
        {
            this.WayCom.SelectedIndex = 0;
            this.method_3();
        }

        private void method_0(ILayer layer)
        {
            if (layer is IFeatureLayer)
            {
                this.method_2((IFeatureLayer) layer);
            }
            else if (layer is IGroupLayer)
            {
                this.method_1((IGroupLayer) layer);
            }
        }

        private void method_1(IGroupLayer groupLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer) groupLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    ILayer layer = compositeLayer.get_Layer(i);
                    this.method_0(layer);
                }
            }
        }

        private void method_2(IFeatureLayer featureLayer)
        {
            if (featureLayer != null && featureLayer.FeatureClass != null)
            {
                string aliasName = featureLayer.FeatureClass.AliasName;
                if (this.pPipeCfg.IsPipelineLayer(featureLayer.Name, enumPipelineDataType.Line))
                {
                    TrackingAnalyForm.LayerInfo pclass = new TrackingAnalyForm.LayerInfo();
                    pclass._layer = featureLayer;
                    this.LayerCom.Items.Add(pclass);
                }
            }
        }

        private void method_3()
        {
            int layerCount = this.m_iApp.FocusMap.LayerCount;
            if (this.MapControl != null)
            {
                this.LayerCom.Items.Clear();
                for (int i = 0; i < layerCount; i++)
                {
                    ILayer layer = this.m_iApp.FocusMap.Layer[i];
                    this.method_0(layer);
                }
                if (this.LayerCom.Items.Count > 0)
                {
                    this.LayerCom.SelectedIndex = 0;
                }
            }
        }

        private void LayerCom_SelectedIndexChanged(object obj, EventArgs eventArgs)
        {
            this.listJunctionFlag.Clear();
            this.listEdgeFlag.Clear();
            this.listJunctionBarrier.Clear();
            this.listEdgeBarrier.Clear();
            this.ilist_4.Clear();
        }

        private void SetButton_Click(object obj, EventArgs eventArgs)
        {
            int count = this.listJunctionFlag.Count;
            int count2 = this.listEdgeFlag.Count;
            int count3 = this.listJunctionBarrier.Count;
            int count4 = this.listEdgeBarrier.Count;
            if (count < 1 && count2 < 1)
            {
                MessageBox.Show("请用户选择要分析的点或线!");
            }
            else
            {
                IEdgeFlag[] array = new EdgeFlag[count2];
                IJunctionFlag[] array2 = new JunctionFlag[count];
                INetwork network = null;
                INetworkClass networkClass = null;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        IFeature feature = this.listJunctionFlag[i];
                        networkClass = (feature.Class as INetworkClass);
                        network = networkClass.GeometricNetwork.Network;
                        INetElements netElements = network as INetElements;
                        INetFlag netFlag = new JunctionFlag() as INetFlag;
                        ISimpleJunctionFeature simpleJunctionFeature = feature as ISimpleJunctionFeature;
                        int userClassID;
                        int userID;
                        int userSubID;
                        netElements.QueryIDs(simpleJunctionFeature.EID, (esriElementType) 1, out userClassID, out userID,
                            out userSubID);
                        netFlag.UserClassID = (userClassID);
                        netFlag.UserID = (userID);
                        netFlag.UserSubID = (userSubID);
                        IJunctionFlag junctionFlag = netFlag as IJunctionFlag;
                        array2[i] = junctionFlag;
                    }
                }
                if (count2 > 0)
                {
                    for (int j = 0; j < count2; j++)
                    {
                        IFeature feature2 = this.listEdgeFlag[j];
                        networkClass = (feature2.Class as INetworkClass);
                        network = networkClass.GeometricNetwork.Network;
                        INetElements netElements2 = network as INetElements;
                        INetFlag netFlag2 = new EdgeFlag() as INetFlag;
                        ISimpleEdgeFeature simpleEdgeFeature = feature2 as ISimpleEdgeFeature;
                        int userClassID2;
                        int userID2;
                        int userSubID2;
                        netElements2.QueryIDs(simpleEdgeFeature.EID, (esriElementType) 2, out userClassID2, out userID2,
                            out userSubID2);
                        netFlag2.UserClassID = (userClassID2);
                        netFlag2.UserID = (userID2);
                        netFlag2.UserSubID = (userSubID2);
                        IEdgeFlag edgeFlag = netFlag2 as IEdgeFlag;
                        array[j] = edgeFlag;
                    }
                }
                ITraceFlowSolverGEN traceFlowSolverGEN = new TraceFlowSolver() as ITraceFlowSolverGEN;
                INetSolver netSolver = traceFlowSolverGEN as INetSolver;
                INetElementBarriersGEN netElementBarriersGEN = new NetElementBarriers();
                netElementBarriersGEN.Network = (network);
                netElementBarriersGEN.ElementType = (esriElementType) (1);
                int[] array3 = new int[count3];
                int num = 0;
                if (count3 > 0)
                {
                    for (int k = 0; k < count3; k++)
                    {
                        IFeature feature3 = this.listJunctionBarrier[k];
                        networkClass = (feature3.Class as INetworkClass);
                        network = networkClass.GeometricNetwork.Network;
                        INetElements netElements3 = network as INetElements;
                        new EdgeFlag();
                        ISimpleJunctionFeature simpleJunctionFeature2 = feature3 as ISimpleJunctionFeature;
                        int num2;
                        int num3;
                        netElements3.QueryIDs(simpleJunctionFeature2.EID, (esriElementType) 1, out num, out num2,
                            out num3);
                        array3[k] = num2;
                    }
                    netElementBarriersGEN.SetBarriers(num, ref array3);
                    netSolver.set_ElementBarriers((esriElementType) 1, (INetElementBarriers) netElementBarriersGEN);
                }
                INetElementBarriersGEN netElementBarriersGEN2 = new NetElementBarriers();
                netElementBarriersGEN2.Network = (network);
                netElementBarriersGEN2.ElementType = (esriElementType) (2);
                int[] array4 = new int[count4];
                if (count4 > 0)
                {
                    for (int l = 0; l < count4; l++)
                    {
                        IFeature feature4 = this.listEdgeBarrier[l];
                        networkClass = (feature4.Class as INetworkClass);
                        network = networkClass.GeometricNetwork.Network;
                        INetElements netElements4 = network as INetElements;
                        new EdgeFlag();
                        ISimpleEdgeFeature simpleEdgeFeature2 = feature4 as ISimpleEdgeFeature;
                        int num4;
                        int num5;
                        netElements4.QueryIDs(simpleEdgeFeature2.EID, (esriElementType) 2, out num, out num4, out num5);
                        array4[l] = num4;
                    }
                    netElementBarriersGEN2.SetBarriers(num, ref array4);
                    netSolver.set_ElementBarriers((esriElementType) 2, (INetElementBarriers) netElementBarriersGEN2);
                }
                netSolver.SourceNetwork = (network);
                if (count > 0)
                {
                    traceFlowSolverGEN.PutJunctionOrigins(ref array2);
                }
                if (count2 > 0)
                {
                    traceFlowSolverGEN.PutEdgeOrigins(ref array);
                }
                IEnumNetEID enumNetEID = null;
                IEnumNetEID enumNetEID2 = null;
                object[] array5 = new object[1];
                if (this.WayCom.SelectedIndex == 0)
                {
                    traceFlowSolverGEN.FindSource(0, (esriShortestPathObjFn) 1, out enumNetEID, out enumNetEID2,
                        count + count2, ref array5);
                }
                if (this.WayCom.SelectedIndex == 1)
                {
                    traceFlowSolverGEN.FindSource((esriFlowMethod) 1, (esriShortestPathObjFn) 1, out enumNetEID,
                        out enumNetEID2, count + count2, ref array5);
                }
                IPolyline polyline = new Polyline() as IPolyline;
                IGeometryCollection geometryCollection = polyline as IGeometryCollection;
                ISpatialReference spatialReference = this.m_iApp.FocusMap.SpatialReference;
                IEIDHelper iEIDHelper = new EIDHelper();
                iEIDHelper.GeometricNetwork = (networkClass.GeometricNetwork);
                iEIDHelper.OutputSpatialReference = (spatialReference);
                iEIDHelper.ReturnGeometries = (true);
                iEIDHelper.ReturnFeatures = (true);
                int selectedIndex = this.LayerCom.SelectedIndex;
                if (selectedIndex >= 0 && this.MapControl != null)
                {
                    this.LayerCom.SelectedItem.ToString();
                    IFeatureLayer ifeatureLayer_ = ((TrackingAnalyForm.LayerInfo) this.LayerCom.SelectedItem)._layer;
                    if (ifeatureLayer_ != null)
                    {
                        IFeatureSelection featureSelection = (IFeatureSelection) ifeatureLayer_;
                        featureSelection.Clear();
                        if (enumNetEID2 != null)
                        {
                            IEnumEIDInfo enumEIDInfo = iEIDHelper.CreateEnumEIDInfo(enumNetEID2);
                            int count5 = enumEIDInfo.Count;
                            enumEIDInfo.Reset();
                            for (int m = 0; m < count5; m++)
                            {
                                IEIDInfo iEIDInfo = enumEIDInfo.Next();
                                featureSelection.Add(iEIDInfo.Feature);
                                IGeometry geometry = iEIDInfo.Geometry;
                                geometryCollection.AddGeometryCollection(geometry as IGeometryCollection);
                            }
                        }
                        featureSelection.SelectionSet.Refresh();
                        IActiveView activeView = this.m_iApp.ActiveView;
                        activeView.Refresh();
                        CMapOperator.ShowFeatureWithWink(this.m_iApp.ActiveView.ScreenDisplay, polyline);
                    }
                }
            }
        }

        private void ClearBut_Click(object obj, EventArgs eventArgs)
        {
            this.listJunctionFlag.Clear();
            this.listEdgeFlag.Clear();
            this.listJunctionBarrier.Clear();
            this.listEdgeBarrier.Clear();
            this.ilist_4.Clear();
            IActiveView activeView = this.m_iApp.ActiveView;

            activeView.Refresh();
        }

        private void TrackingAnalyForm_HelpRequested(object obj, HelpEventArgs helpEventArgs)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "追踪分析";
            Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
        }
    }
}