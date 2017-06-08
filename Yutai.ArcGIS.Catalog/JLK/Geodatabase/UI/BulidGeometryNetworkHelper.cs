namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using ESRI.ArcGIS.NetworkAnalysis;
    using JLK.Utility;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

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
            Exception exception;
            try
            {
                int num;
                INetworkCollection networks = ifeatureDataset_1 as INetworkCollection;
                INetworkLoader networkLoader = this.NetworkLoader;
                INetworkLoader2 loader2 = networkLoader as INetworkLoader2;
                INetworkLoaderProps props = networkLoader as INetworkLoaderProps;
                IFeatureDatasetName fullName = ifeatureDataset_1.FullName as IFeatureDatasetName;
                networkLoader.FeatureDatasetName = fullName as IDatasetName;
                networkLoader.NetworkName = this.Name;
                UID newClsID = new UIDClass();
                UID uid2 = new UIDClass();
                UID uid3 = new UIDClass();
                newClsID.Value = this.GUID_COMPLEXEDGE_CLSID;
                uid2.Value = this.GUID_SIMPLEEDGE_CLSID;
                uid3.Value = this.GUID_SIMPLEJUNCTION_CLSID;
                if (this.IsSnap)
                {
                    networkLoader.SnapTolerance = this.SnapTolerance;
                }
                else
                {
                    networkLoader.SnapTolerance = loader2.MinSnapTolerance;
                }
                for (num = 0; num < this.Weights.Count; num++)
                {
                    Weight weight = this.Weights[num] as Weight;
                    networkLoader.AddWeight(weight.networkWeightName, weight.weightType, weight.bitGateSize);
                }
                bool flag = true;
                bool flag2 = false;
                num = 0;
            Label_00F8:
                if (num >= this.FeatureClassWraps.Count)
                {
                    goto Label_04FD;
                }
                FeatureClassWrap wrap = this.FeatureClassWraps[num] as FeatureClassWrap;
                if (!wrap.IsUse)
                {
                    goto Label_04F2;
                }
                flag = true;
                IDataset featureClass = wrap.FeatureClass as IDataset;
                esriNetworkLoaderFeatureClassCheck check = loader2.CanUseFeatureClass(featureClass.Name);
                switch (check)
                {
                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCInTerrain:
                        MessageBox.Show(featureClass.Name + "已用在三角网中");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCIsCompressedReadOnly:
                        MessageBox.Show(featureClass.Name + "是压缩只读要素类");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCInTopology:
                        MessageBox.Show(featureClass.Name + "已用在拓扑中");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCRegisteredAsVersioned:
                        MessageBox.Show(featureClass.Name + "已注册版本");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCInvalidShapeType:
                        MessageBox.Show(featureClass.Name + "不是点或线几何要素");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCInvalidFeatureType:
                        MessageBox.Show(featureClass.Name + "是无效要素类型");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCInAnotherNetwork:
                        MessageBox.Show(featureClass.Name + "已在其它网络中使用");
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCCannotOpen:
                        MessageBox.Show("无法打开" + featureClass.Name);
                        flag = false;
                        break;

                    case esriNetworkLoaderFeatureClassCheck.esriNLFCCUnknownError:
                        MessageBox.Show(featureClass.Name + " 未知错误");
                        flag = false;
                        break;
                }
                if (!flag || (check != esriNetworkLoaderFeatureClassCheck.esriNLFCCValid))
                {
                    goto Label_04F2;
                }
                loader2.PreserveEnabledValues = this.PreserveEnabledValues;
                switch (loader2.CheckEnabledDisabledField(featureClass.Name, props.DefaultEnabledField))
                {
                    case esriNetworkLoaderFieldCheck.esriNLFCInvalidDomain:
                        MessageBox.Show(featureClass.Name + ": ENABLED字段有无效域值.");
                        flag = false;
                        break;

                    case esriNetworkLoaderFieldCheck.esriNLFCInvalidType:
                        MessageBox.Show(featureClass.Name + ": ENABLED字段有无效类型");
                        flag = false;
                        break;

                    case esriNetworkLoaderFieldCheck.esriNLFCNotFound:
                        try
                        {
                            networkLoader.PutEnabledDisabledFieldName(featureClass.Name, props.DefaultEnabledField);
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            CErrorLog.writeErrorLog(null, exception, "");
                        }
                        break;

                    case esriNetworkLoaderFieldCheck.esriNLFCUnknownError:
                        MessageBox.Show(featureClass.Name + ": (ENABLED 字段)- 产生未知错误.");
                        flag = false;
                        break;
                }
                if (!flag)
                {
                    goto Label_04F2;
                }
                esriNetworkClassAncillaryRole esriNCARNone = esriNetworkClassAncillaryRole.esriNCARNone;
                if ((wrap.GeometryType == esriGeometryType.esriGeometryPoint) && wrap.IsUse)
                {
                    esriNCARNone = esriNetworkClassAncillaryRole.esriNCARSourceSink;
                }
                esriFeatureType featureType = wrap.FeatureType;
                switch (featureType)
                {
                    case esriFeatureType.esriFTSimpleJunction:
                    {
                        switch (esriNCARNone)
                        {
                            case esriNetworkClassAncillaryRole.esriNCARSourceSink:
                                switch (loader2.CheckAncillaryRoleField(featureClass.Name, props.DefaultAncillaryRoleField))
                                {
                                    case esriNetworkLoaderFieldCheck.esriNLFCInvalidDomain:
                                        goto Label_0582;

                                    case esriNetworkLoaderFieldCheck.esriNLFCInvalidType:
                                        goto Label_059E;

                                    case esriNetworkLoaderFieldCheck.esriNLFCNotFound:
                                        goto Label_03F6;

                                    case esriNetworkLoaderFieldCheck.esriNLFCUnknownError:
                                        goto Label_05BA;
                                }
                                break;
                        }
                    }
                }
                goto Label_0410;
            Label_03F6:
                try
                {
                    networkLoader.PutAncillaryRole(featureClass.Name, esriNetworkClassAncillaryRole.esriNCARSourceSink, props.DefaultAncillaryRoleField);
                }
                catch
                {
                }
            Label_0410:
                try
                {
                    switch (featureType)
                    {
                        case esriFeatureType.esriFTSimpleJunction:
                            networkLoader.AddFeatureClass(featureClass.Name, esriFeatureType.esriFTSimpleJunction, uid3, this.IsSnap ? wrap.canChangeGeometry : false);
                            break;

                        case esriFeatureType.esriFTSimpleEdge:
                            networkLoader.AddFeatureClass(featureClass.Name, esriFeatureType.esriFTSimpleEdge, uid2, this.IsSnap ? wrap.canChangeGeometry : false);
                            break;

                        case esriFeatureType.esriFTComplexEdge:
                            networkLoader.AddFeatureClass(featureClass.Name, esriFeatureType.esriFTComplexEdge, newClsID, this.IsSnap ? wrap.canChangeGeometry : false);
                            break;
                    }
                    flag2 = true;
                }
                catch (COMException exception2)
                {
                    if (exception2.ErrorCode == -2147220462)
                    {
                        MessageBox.Show("要素类[" + featureClass.Name + "]无法添加到几何网络中!");
                    }
                    else
                    {
                        MessageBox.Show(exception2.Message);
                    }
                }
                catch (Exception exception4)
                {
                    exception = exception4;
                    MessageBox.Show(exception.Message);
                }
            Label_04F2:
                num++;
                goto Label_00F8;
            Label_04FD:
                if (!flag2)
                {
                    return null;
                }
                for (num = 0; num < this.WeightAssociations.Count; num++)
                {
                    WeightAssociation association = this.WeightAssociations[num] as WeightAssociation;
                    loader2.AddWeightAssociation(association.networkWeightName, association.featureClassName, association.fieldName);
                }
                loader2.ConfigurationKeyword = this.ConfigurationKeyword;
                loader2.PreserveEnabledValues = this.PreserveEnabledValues;
                networkLoader.LoadNetwork();
                return networks.get_GeometricNetworkByName(this.Name);
            Label_0582:
                MessageBox.Show(featureClass.Name + ": ROLE字段有无效域值.");
                return null;
            Label_059E:
                MessageBox.Show(featureClass.Name + ": ROLE字段有无效类型.");
                return null;
            Label_05BA:
                MessageBox.Show(featureClass.Name + ": (ROLE Field)- An unknown error was encountered.");
                return null;
            }
            catch (Exception exception5)
            {
                exception = exception5;
                MessageBox.Show(exception.Message);
            }
            return null;
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

