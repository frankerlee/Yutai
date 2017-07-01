using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class SetFlowToolForm : Form
    {
        private partial class Class2
        {
            public IFeatureLayer ifeatureLayer_0;

            public override string ToString()
            {
                return this.ifeatureLayer_0.Name;
            }
        }

        public IAppContext m_Context;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;

        private IContainer icontainer_0 = null;


        public SetFlowToolForm(IAppContext context)
        {
            this.InitializeComponent();
            m_Context = context;
        }

        private void SetFlowToolForm_Load(object obj, EventArgs eventArgs)
        {
            this.WayCombo.SelectedIndex = 0;
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
            if (featureLayer != null)
            {
                string aliasName = featureLayer.FeatureClass.AliasName;
                if (this.pPipeCfg.IsPipelineLayer(featureLayer.Name, enumPipelineDataType.Line))
                {
                    SetFlowToolForm.Class2 pclass = new SetFlowToolForm.Class2();
                    pclass.ifeatureLayer_0 = featureLayer;
                    this.NetWorkCombo.Items.Add(pclass);
                }
            }
        }

        private bool method_3()
        {
            int layerCount = this.m_Context.FocusMap.LayerCount;
            bool result;
            if (this.MapControl == null)
            {
                result = false;
            }
            else
            {
                this.NetWorkCombo.Items.Clear();
                for (int i = 0; i < layerCount; i++)
                {
                    ILayer layer = this.m_Context.FocusMap.Layer[i];
                    this.method_0(layer);
                }
                if (this.NetWorkCombo.Items.Count > 0)
                {
                    this.NetWorkCombo.SelectedIndex = 0;
                }
                result = true;
            }
            return result;
        }

        private void SetBut_Click(object obj, EventArgs eventArgs)
        {
            int selectedIndex = this.NetWorkCombo.SelectedIndex;
            if (selectedIndex >= 0 && this.MapControl != null)
            {
                IFeatureLayer ifeatureLayer_ = ((SetFlowToolForm.Class2) this.NetWorkCombo.SelectedItem).ifeatureLayer_0;
                INetworkClass networkClass = ifeatureLayer_.FeatureClass as INetworkClass;
                if (networkClass != null)
                {
                    IGeometricNetwork geometricNetwork = networkClass.GeometricNetwork;
                    INetwork network = geometricNetwork.Network;
                    IDataset dataset = (IDataset) ifeatureLayer_.FeatureClass;
                    IWorkspaceEdit workspaceEdit = (IWorkspaceEdit) dataset.Workspace;
                    if (!workspaceEdit.IsBeingEdited())
                    {
                        MessageBox.Show("数据不可编辑");
                    }
                    else
                    {
                        workspaceEdit.StartEditOperation();
                        geometricNetwork.EstablishFlowDirection();
                        if (this.WayCombo.SelectedIndex == 0)
                        {
                            this.method_4(network);
                        }
                        else if (this.WayCombo.SelectedIndex == 1)
                        {
                            this.method_6(geometricNetwork);
                        }
                        else if (this.WayCombo.SelectedIndex == 2)
                        {
                            this.method_5(geometricNetwork);
                        }
                        workspaceEdit.StopEditOperation();
                        MessageBox.Show("操作完成！");
                        base.Close();
                    }
                }
            }
        }

        private void method_4(INetwork network)
        {
            IUtilityNetwork utilityNetwork = (IUtilityNetwork) network;
            IEnumNetEID enumNetEID = network.CreateNetBrowser((esriElementType) 2);
            enumNetEID.Reset();
            long num = (long) enumNetEID.Count;
            int num2 = 0;
            while ((long) num2 < num)
            {
                int num3 = enumNetEID.Next();
                utilityNetwork.SetFlowDirection(num3, (esriFlowDirection) 1);
                num2++;
            }
        }

        private void method_5(IGeometricNetwork geometricNetwork)
        {
            INetwork network = geometricNetwork.Network;
            IUtilityNetwork utilityNetwork = (IUtilityNetwork) network;
            IEnumNetEID enumNetEID = network.CreateNetBrowser((esriElementType) 2);
            IEIDHelper eIDHelperClass = new EIDHelper();
            eIDHelperClass.GeometricNetwork = (geometricNetwork);
            eIDHelperClass.DisplayEnvelope = (null);
            eIDHelperClass.OutputSpatialReference = (null);
            eIDHelperClass.ReturnFeatures = (true);
            eIDHelperClass.ReturnGeometries = (true);
            eIDHelperClass.PartialComplexEdgeGeometry = (true);
            IEnumEIDInfo enumEIDInfo = eIDHelperClass.CreateEnumEIDInfo(enumNetEID);
            enumEIDInfo.Reset();
            long num = (long) enumEIDInfo.Count;
            int num2 = 0;
            while ((long) num2 < num)
            {
                IEIDInfo iEIDInfo = enumEIDInfo.Next();
                int eID = iEIDInfo.EID;
                IFeature feature = iEIDInfo.Feature;
                int num3 = feature.Fields.FindField("流向");
                if (num3 < 0)
                {
                    utilityNetwork.SetFlowDirection(eID, (esriFlowDirection) 1);
                }
                if (Convert.ToBoolean(feature.get_Value(num3)))
                {
                    utilityNetwork.SetFlowDirection(eID, (esriFlowDirection) 2);
                }
                else
                {
                    utilityNetwork.SetFlowDirection(eID, (esriFlowDirection) 1);
                }
                num2++;
            }
        }

        private void method_6(IGeometricNetwork geometricNetwork)
        {
            INetwork network = geometricNetwork.Network;
            IUtilityNetwork utilityNetwork = (IUtilityNetwork) network;
            IEnumNetEID enumNetEID = network.CreateNetBrowser((esriElementType) 2);
            IEIDHelper eIDHelperClass = new EIDHelper();
            eIDHelperClass.GeometricNetwork = (geometricNetwork);
            eIDHelperClass.DisplayEnvelope = (null);
            eIDHelperClass.OutputSpatialReference = (null);
            eIDHelperClass.ReturnFeatures = (false);
            eIDHelperClass.ReturnGeometries = (true);
            eIDHelperClass.PartialComplexEdgeGeometry = (true);
            IEnumEIDInfo enumEIDInfo = eIDHelperClass.CreateEnumEIDInfo(enumNetEID);
            enumEIDInfo.Reset();
            long num = (long) enumEIDInfo.Count;
            int num2 = 0;
            while ((long) num2 < num)
            {
                IEIDInfo iEIDInfo = enumEIDInfo.Next();
                int eID = iEIDInfo.EID;
                IGeometry geometry = iEIDInfo.Geometry;
                esriFlowDirection esriFlowDirection = this.method_7(geometry);
                utilityNetwork.SetFlowDirection(eID, esriFlowDirection);
                num2++;
            }
        }

        private esriFlowDirection method_7(IGeometry geometry)
        {
            ISegmentCollection segmentCollection = (ISegmentCollection) geometry;
            ISegmentZ segmentZ = (ISegmentZ) segmentCollection.get_Segment(0);
            ISegmentZ segmentZ2 = (ISegmentZ) segmentCollection.get_Segment(-1);
            double num;
            double num2;
            segmentZ.GetZs(out num, out num2);
            double num3;
            segmentZ2.GetZs(out num2, out num3);
            esriFlowDirection result;
            if (num > num3)
            {
                result = (esriFlowDirection) 1;
            }
            else if (num < num3)
            {
                result = (esriFlowDirection) 2;
            }
            else
            {
                result = (esriFlowDirection) 3;
            }
            return result;
        }

        private void CloseBut_Click(object obj, EventArgs eventArgs)
        {
            base.Close();
        }

        private void SetFlowToolForm_HelpRequested(object obj, HelpEventArgs helpEventArgs)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "追踪分析";
            Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
        }
    }
}