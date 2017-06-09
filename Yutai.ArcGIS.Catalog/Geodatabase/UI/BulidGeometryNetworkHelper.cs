using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class BulidGeometryNetworkHelper
    {
        private bool bool_0 = false;
        internal bool buildNormalizedTables;
        public static BulidGeometryNetworkHelper BulidGNHelper;
        internal string ConfigKey = "";
        public string ConfigurationKeyword = "";
        internal IList FeatureClassWraps = new ArrayList();
        internal string GUID_COMPLEXEDGE_CLSID = "esriGeodatabase.ComplexEdgeFeature";
        internal string GUID_SIMPLEEDGE_CLSID = "esriGeodatabase.SimpleEdgeFeature";
        internal string GUID_SIMPLEJUNCTION_CLSID = "esriGeodatabase.SimpleJunctionFeature";
        internal bool HasWeight = false;
        private IFeatureDataset ifeatureDataset_0 = null;
        public bool IsEmpty = false;
        internal bool IsSnap = false;
        internal bool mGNImportPreserveEnabledValue = false;
        internal double MinSnapTolerance = 0.01;
        public string Name = "";
        internal IList NetworkClassAncillaryRoles = new ArrayList();
        internal INetworkLoader NetworkLoader = new NetworkLoaderClass();
        internal esriNetworkType NetworkType = esriNetworkType.esriNTUtilityNetwork;
        internal bool PreserveEnabledValues = false;
        internal double SnapTolerance = 0.01;
        internal bool UseDefaultConfigKey = true;
        internal IList WeightAssociations = new ArrayList();
        public IList Weights = new ArrayList();

        static BulidGeometryNetworkHelper()
        {
            old_acctor_mc();
        }

        public IGeometricNetwork CreateGeometricNetwork(IFeatureDataset ifeatureDataset_1)
        {
            int i;
            IGeometricNetwork geometricNetworkByName;
            try
            {
                INetworkCollection ifeatureDataset1 = ifeatureDataset_1 as INetworkCollection;
                INetworkLoader networkLoader = this.NetworkLoader;
                INetworkLoader2 preserveEnabledValues = networkLoader as INetworkLoader2;
                INetworkLoaderProps networkLoaderProp = networkLoader as INetworkLoaderProps;
                networkLoader.FeatureDatasetName = ifeatureDataset_1.FullName as IFeatureDatasetName as IDatasetName;
                networkLoader.NetworkName = this.Name;
                UID uIDClass = new UIDClass();
                UID gUIDSIMPLEEDGECLSID = new UIDClass();
                UID gUIDSIMPLEJUNCTIONCLSID = new UIDClass();
                uIDClass.Value = this.GUID_COMPLEXEDGE_CLSID;
                gUIDSIMPLEEDGECLSID.Value = this.GUID_SIMPLEEDGE_CLSID;
                gUIDSIMPLEJUNCTIONCLSID.Value = this.GUID_SIMPLEJUNCTION_CLSID;
                if (!this.IsSnap)
                {
                    networkLoader.SnapTolerance = preserveEnabledValues.MinSnapTolerance;
                }
                else
                {
                    networkLoader.SnapTolerance = this.SnapTolerance;
                }
                for (i = 0; i < this.Weights.Count; i++)
                {
                    BulidGeometryNetworkHelper.Weight item = this.Weights[i] as BulidGeometryNetworkHelper.Weight;
                    networkLoader.AddWeight(item.networkWeightName, item.weightType, item.bitGateSize);
                }
                bool flag = true;
                bool flag1 = false;
                for (i = 0; i < this.FeatureClassWraps.Count; i++)
                {
                    BulidGeometryNetworkHelper.FeatureClassWrap featureClassWrap = this.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                    if (featureClassWrap.IsUse)
                    {
                        flag = true;
                        IDataset featureClass = featureClassWrap.FeatureClass as IDataset;
                        esriNetworkLoaderFeatureClassCheck _esriNetworkLoaderFeatureClassCheck = preserveEnabledValues.CanUseFeatureClass(featureClass.Name);
                        esriNetworkLoaderFeatureClassCheck _esriNetworkLoaderFeatureClassCheck1 = _esriNetworkLoaderFeatureClassCheck;
                        if (_esriNetworkLoaderFeatureClassCheck1 == esriNetworkLoaderFeatureClassCheck.esriNLFCCUnknownError)
                        {
                            MessageBox.Show(string.Concat(featureClass.Name, " 未知错误"));
                            flag = false;
                        }
                        else
                        {
                            switch (_esriNetworkLoaderFeatureClassCheck1)
                            {
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCInTerrain:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "已用在三角网中"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCIsCompressedReadOnly:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "是压缩只读要素类"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCInTopology:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "已用在拓扑中"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCRegisteredAsVersioned:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "已注册版本"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCInvalidShapeType:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "不是点或线几何要素"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCInvalidFeatureType:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "是无效要素类型"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCInAnotherNetwork:
                                    {
                                        MessageBox.Show(string.Concat(featureClass.Name, "已在其它网络中使用"));
                                        flag = false;
                                        break;
                                    }
                                case esriNetworkLoaderFeatureClassCheck.esriNLFCCCannotOpen:
                                    {
                                        MessageBox.Show(string.Concat("无法打开", featureClass.Name));
                                        flag = false;
                                        break;
                                    }
                            }
                        }
                        if (flag && _esriNetworkLoaderFeatureClassCheck == esriNetworkLoaderFeatureClassCheck.esriNLFCCValid)
                        {
                            preserveEnabledValues.PreserveEnabledValues = this.PreserveEnabledValues;
                            esriNetworkLoaderFieldCheck _esriNetworkLoaderFieldCheck = preserveEnabledValues.CheckEnabledDisabledField(featureClass.Name, networkLoaderProp.DefaultEnabledField);
                            if (_esriNetworkLoaderFieldCheck == esriNetworkLoaderFieldCheck.esriNLFCUnknownError)
                            {
                                MessageBox.Show(string.Concat(featureClass.Name, ": (ENABLED 字段)- 产生未知错误."));
                                flag = false;
                            }
                            else
                            {
                                switch (_esriNetworkLoaderFieldCheck)
                                {
                                    case esriNetworkLoaderFieldCheck.esriNLFCInvalidDomain:
                                        {
                                            MessageBox.Show(string.Concat(featureClass.Name, ": ENABLED字段有无效域值."));
                                            flag = false;
                                            break;
                                        }
                                    case esriNetworkLoaderFieldCheck.esriNLFCInvalidType:
                                        {
                                            MessageBox.Show(string.Concat(featureClass.Name, ": ENABLED字段有无效类型"));
                                            flag = false;
                                            break;
                                        }
                                    case esriNetworkLoaderFieldCheck.esriNLFCNotFound:
                                        {
                                            try
                                            {
                                                networkLoader.PutEnabledDisabledFieldName(featureClass.Name, networkLoaderProp.DefaultEnabledField);
                                                break;
                                            }
                                            catch (Exception exception)
                                            {
                                                Logger.Current.Error("",exception, "");
                                                break;
                                            }
                                            break;
                                        }
                                }
                            }
                            if (flag)
                            {
                                esriNetworkClassAncillaryRole _esriNetworkClassAncillaryRole = esriNetworkClassAncillaryRole.esriNCARNone;
                                if (featureClassWrap.GeometryType == esriGeometryType.esriGeometryPoint && featureClassWrap.IsUse)
                                {
                                    _esriNetworkClassAncillaryRole = esriNetworkClassAncillaryRole.esriNCARSourceSink;
                                }
                                esriFeatureType featureType = featureClassWrap.FeatureType;
                                if (featureType == esriFeatureType.esriFTSimpleJunction)
                                {
                                    switch (_esriNetworkClassAncillaryRole)
                                    {
                                        case esriNetworkClassAncillaryRole.esriNCARNone:
                                            {
                                               
                                                break;
                                            }
                                        case esriNetworkClassAncillaryRole.esriNCARSourceSink:
                                            {
                                                _esriNetworkLoaderFieldCheck = preserveEnabledValues.CheckAncillaryRoleField(featureClass.Name, networkLoaderProp.DefaultAncillaryRoleField);
                                                if (_esriNetworkLoaderFieldCheck == esriNetworkLoaderFieldCheck.esriNLFCUnknownError)
                                                {
                                                    MessageBox.Show(string.Concat(featureClass.Name, ": (ROLE Field)- An unknown error was encountered."));
                                                    geometricNetworkByName = null;
                                                    return geometricNetworkByName;
                                                }
                                                else
                                                {
                                                    switch (_esriNetworkLoaderFieldCheck)
                                                    {
                                                        case esriNetworkLoaderFieldCheck.esriNLFCInvalidDomain:
                                                            {
                                                                MessageBox.Show(string.Concat(featureClass.Name, ": ROLE字段有无效域值."));
                                                                geometricNetworkByName = null;
                                                                return geometricNetworkByName;
                                                            }
                                                        case esriNetworkLoaderFieldCheck.esriNLFCInvalidType:
                                                            {
                                                                MessageBox.Show(string.Concat(featureClass.Name, ": ROLE字段有无效类型."));
                                                                geometricNetworkByName = null;
                                                                return geometricNetworkByName;
                                                            }
                                                        case esriNetworkLoaderFieldCheck.esriNLFCNotFound:
                                                            {
                                                                try
                                                                {
                                                                    networkLoader.PutAncillaryRole(featureClass.Name, esriNetworkClassAncillaryRole.esriNCARSourceSink, networkLoaderProp.DefaultAncillaryRoleField);
                                                                    break;
                                                                }
                                                                catch
                                                                {
                                                                    break;
                                                                }
                                                                break;
                                                            }
                                                        default:
                                                            {
                                                                break;
                                                            }
                                                    }
                                                }
                                                break;
                                            }
                                        default:
                                            {
                                                goto case esriNetworkClassAncillaryRole.esriNCARNone;
                                            }
                                    }
                                }
                                try
                                {
                                    switch (featureType)
                                    {
                                        case esriFeatureType.esriFTSimpleJunction:
                                            {
                                                networkLoader.AddFeatureClass(featureClass.Name, esriFeatureType.esriFTSimpleJunction, gUIDSIMPLEJUNCTIONCLSID, (this.IsSnap ? featureClassWrap.canChangeGeometry : false));
                                                goto case esriFeatureType.esriFTComplexJunction;
                                            }
                                        case esriFeatureType.esriFTSimpleEdge:
                                            {
                                                networkLoader.AddFeatureClass(featureClass.Name, esriFeatureType.esriFTSimpleEdge, gUIDSIMPLEEDGECLSID, (this.IsSnap ? featureClassWrap.canChangeGeometry : false));
                                                goto case esriFeatureType.esriFTComplexJunction;
                                            }
                                        case esriFeatureType.esriFTComplexJunction:
                                            {
                                                flag1 = true;
                                                break;
                                            }
                                        case esriFeatureType.esriFTComplexEdge:
                                            {
                                                networkLoader.AddFeatureClass(featureClass.Name, esriFeatureType.esriFTComplexEdge, uIDClass, (this.IsSnap ? featureClassWrap.canChangeGeometry : false));
                                                goto case esriFeatureType.esriFTComplexJunction;
                                            }
                                        default:
                                            {
                                                goto case esriFeatureType.esriFTComplexJunction;
                                            }
                                    }
                                }
                                catch (COMException cOMException1)
                                {
                                    COMException cOMException = cOMException1;
                                    if (cOMException.ErrorCode != -2147220462)
                                    {
                                        MessageBox.Show(cOMException.Message);
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Concat("要素类[", featureClass.Name, "]无法添加到几何网络中!"));
                                    }
                                }
                                catch (Exception exception1)
                                {
                                    MessageBox.Show(exception1.Message);
                                }
                            }
                        }
                    }
                }
                if (flag1)
                {
                    for (i = 0; i < this.WeightAssociations.Count; i++)
                    {
                        BulidGeometryNetworkHelper.WeightAssociation weightAssociation = this.WeightAssociations[i] as BulidGeometryNetworkHelper.WeightAssociation;
                        preserveEnabledValues.AddWeightAssociation(weightAssociation.networkWeightName, weightAssociation.featureClassName, weightAssociation.fieldName);
                    }
                    preserveEnabledValues.ConfigurationKeyword = this.ConfigurationKeyword;
                    preserveEnabledValues.PreserveEnabledValues = this.PreserveEnabledValues;
                    networkLoader.LoadNetwork();
                    geometricNetworkByName = ifeatureDataset1.GeometricNetworkByName[this.Name];
                }
                else
                {
                    geometricNetworkByName = null;
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
                geometricNetworkByName = null;
                return geometricNetworkByName;
            }
            return geometricNetworkByName;
        }

        public int GetSelectFefatureClassCount()
        {
            int num = 0;
            for (int i = 0; i < this.FeatureClassWraps.Count; i++)
            {
                FeatureClassWrap wrap = this.FeatureClassWraps[i] as FeatureClassWrap;
                if (wrap.IsUse)
                {
                    num++;
                }
            }
            return num;
        }

        public bool HasEnabeldField()
        {
            for (int i = 0; i < this.FeatureClassWraps.Count; i++)
            {
                FeatureClassWrap wrap = this.FeatureClassWraps[i] as FeatureClassWrap;
                if (wrap.IsUse)
                {
                    int index = wrap.FeatureClass.Fields.FindField("Enabled");
                    if (index != -1)
                    {
                        wrap.FeatureClass.Fields.get_Field(index);
                        this.bool_0 = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasLineFeatureClass()
        {
            for (int i = 0; i < this.FeatureClassWraps.Count; i++)
            {
                FeatureClassWrap wrap = this.FeatureClassWraps[i] as FeatureClassWrap;
                if (wrap.IsUse && (wrap.GeometryType == esriGeometryType.esriGeometryPolyline))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPointFeatureClass()
        {
            for (int i = 0; i < this.FeatureClassWraps.Count; i++)
            {
                FeatureClassWrap wrap = this.FeatureClassWraps[i] as FeatureClassWrap;
                if (wrap.IsUse && (wrap.GeometryType == esriGeometryType.esriGeometryPoint))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Init()
        {
            BulidGNHelper = new BulidGeometryNetworkHelper();
        }

        private static void old_acctor_mc()
        {
            BulidGNHelper = null;
        }

        public string Summary()
        {
            if (this.IsEmpty)
            {
                return (("网络名:" + this.Name + "\r\n") + "空几何网络");
            }
            return ("网络名:" + this.Name + "\r\n");
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return this.ifeatureDataset_0;
            }
            set
            {
                this.ifeatureDataset_0 = value;
                this.NetworkLoader.FeatureDatasetName = this.ifeatureDataset_0.FullName as IDatasetName;
                this.SnapTolerance = (this.NetworkLoader as INetworkLoader2).MinSnapTolerance;
                this.MinSnapTolerance = (this.NetworkLoader as INetworkLoader2).MinSnapTolerance;
            }
        }

        internal class FeatureClassWrap
        {
            private bool bool_0 = false;
            private bool bool_1 = true;
            private bool bool_2 = true;
            private esriFeatureType esriFeatureType_0;
            private esriNetworkClassAncillaryRole esriNetworkClassAncillaryRole_0 = esriNetworkClassAncillaryRole.esriNCARNone;
            private IFeatureClass ifeatureClass_0 = null;

            public FeatureClassWrap(IFeatureClass ifeatureClass_1)
            {
                this.ifeatureClass_0 = ifeatureClass_1;
                if (this.ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    this.esriFeatureType_0 = esriFeatureType.esriFTSimpleJunction;
                }
                else
                {
                    this.esriFeatureType_0 = esriFeatureType.esriFTSimpleEdge;
                }
            }

            public override string ToString()
            {
                if (this.ifeatureClass_0 != null)
                {
                    return (this.ifeatureClass_0 as IDataset).Name;
                }
                return "";
            }

            public bool canChangeGeometry
            {
                get
                {
                    return this.bool_1;
                }
                set
                {
                    this.bool_1 = value;
                }
            }

            public IFeatureClass FeatureClass
            {
                get
                {
                    return this.ifeatureClass_0;
                }
            }

            public esriFeatureType FeatureType
            {
                get
                {
                    return this.esriFeatureType_0;
                }
                set
                {
                    this.esriFeatureType_0 = value;
                }
            }

            public esriGeometryType GeometryType
            {
                get
                {
                    if (this.ifeatureClass_0 != null)
                    {
                        return this.ifeatureClass_0.ShapeType;
                    }
                    return esriGeometryType.esriGeometryAny;
                }
            }

            public bool IsEdge
            {
                get
                {
                    return this.bool_2;
                }
                set
                {
                    this.bool_2 = true;
                }
            }

            public bool IsUse
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }

            public esriNetworkClassAncillaryRole NetworkClassAncillaryRole
            {
                get
                {
                    return this.esriNetworkClassAncillaryRole_0;
                }
                set
                {
                    this.esriNetworkClassAncillaryRole_0 = value;
                }
            }
        }

        internal class Weight
        {
            internal int bitGateSize;
            internal string networkWeightName;
            internal esriWeightType weightType;

            public override string ToString()
            {
                return this.networkWeightName;
            }
        }

        internal class WeightAssociation
        {
            internal string featureClassName;
            internal string fieldName;
            internal string networkWeightName;
        }
    }
}

