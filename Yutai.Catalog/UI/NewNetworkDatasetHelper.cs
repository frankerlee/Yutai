namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class NewNetworkDatasetHelper
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private bool bool_3 = false;
        private bool bool_4 = false;
        private IArray iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
        private IFeatureDataset ifeatureDataset_0 = null;
        private INetworkDirections inetworkDirections_0 = null;
        private int int_0 = 1;
        private List<FeatureClassWrap> list_0 = new List<FeatureClassWrap>();
        private static NewNetworkDatasetHelper m_helper;
        private string string_0 = "";

        static NewNetworkDatasetHelper()
        {
            old_acctor_mc();
        }

        public void CreateDirection(string string_1)
        {
            this.inetworkDirections_0.DefaultOutputLengthUnits = esriNetworkAttributeUnits.esriNAUMiles;
            this.inetworkDirections_0.LengthAttributeName = string_1;
            this.inetworkDirections_0.TimeAttributeName = "Minutes";
            this.inetworkDirections_0.RoadClassAttributeName = "RoadClass";
        }

        public INetworkDataset CreateNetworkDataset()
        {
            try
            {
                return this.method_1(this.ifeatureDataset_0, this.string_0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return null;
        }

        public bool HasLineFeatureClass()
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                if (this.list_0[i].IsUse && (this.list_0[i].FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Init()
        {
            m_helper = new NewNetworkDatasetHelper();
            NewNetworkDataset.list_0.Clear();
        }

        private string method_0(IDataset idataset_0)
        {
            string[] strArray = idataset_0.BrowseName.Split(new char[] { '.' });
            return strArray[strArray.Length - 1];
        }

        private INetworkDataset method_1(IFeatureDataset ifeatureDataset_1, string string_1)
        {
            int num;
            IGeoDataset dataset = (IGeoDataset) ifeatureDataset_1;
            ISpatialReference spatialReference = dataset.SpatialReference;
            IDENetworkDataset dataset2 = new DENetworkDataset() as IDENetworkDataset;
            IDataElement element = (IDataElement) dataset2;
            IDEGeoDataset dataset3 = (IDEGeoDataset) dataset2;
            element.Name = string_1;
            dataset2.Buildable = true;
            dataset3.SpatialReference = spatialReference;
            IArray array = new ESRI.ArcGIS.esriSystem.Array()  as IArray;
            for (num = 0; num < this.FeatureClassWraps.Count; num++)
            {
                if (this.FeatureClassWraps[num].IsUse)
                {
                    INetworkSource source;
                    IFeatureClass featureClass = this.FeatureClassWraps[num].FeatureClass;
                    if (featureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        source = new JunctionFeatureSource {
                            Name = this.FeatureClassWraps[num].ToString()
                        };
                        IJunctionFeatureSource source2 = (IJunctionFeatureSource) source;
                        source2.ClassConnectivityPolicy = (esriNetworkJunctionConnectivityPolicy) this.FeatureClassWraps[num].NetworkConnectivityPolicy;
                        if (this.ModifyConnectivity)
                        {
                            source2.ElevationFieldName = this.FeatureClassWraps[num].ElevationFieldName;
                        }
                        array.Add(source);
                    }
                    else if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        source = new EdgeFeatureSource();
                        IEdgeFeatureSource source3 = (IEdgeFeatureSource) source;
                        source.Name = this.FeatureClassWraps[num].ToString();
                        source3.ClassConnectivityGroup = 1;
                        source3.ClassConnectivityPolicy = (esriNetworkEdgeConnectivityPolicy) this.FeatureClassWraps[num].NetworkConnectivityPolicy;
                        if (this.ModifyConnectivity)
                        {
                            source3.FromElevationFieldName = this.FeatureClassWraps[num].FromElevationFieldName;
                            source3.ToElevationFieldName = this.FeatureClassWraps[num].ToElevationFieldName;
                        }
                        array.Add(source);
                    }
                }
            }
            dataset2.Sources = array;
            for (num = 0; num < this.iarray_0.Count; num++)
            {
                dataset2.Attributes.Add(this.iarray_0.get_Element(num));
            }
            dataset2.SupportsTurns = this.bool_2;
            IFeatureDatasetExtensionContainer container = (IFeatureDatasetExtensionContainer) ifeatureDataset_1;
            IDatasetContainer2 container2 = (IDatasetContainer2) container.FindExtension(esriDatasetType.esriDTNetworkDataset);
            return (INetworkDataset) container2.CreateDataset((IDEDataset) dataset2);
        }

        private static void old_acctor_mc()
        {
            m_helper = null;
        }

        public IArray Attributes
        {
            get
            {
                return this.iarray_0;
            }
            set
            {
                this.iarray_0 = value;
            }
        }

        public int ClassConnectivityGroups
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public List<FeatureClassWrap> FeatureClassWraps
        {
            get
            {
                return this.list_0;
            }
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
                if (this.string_0.Length == 0)
                {
                    this.string_0 = this.method_0(this.ifeatureDataset_0) + "_ND";
                }
            }
        }

        public bool IsEstablishDirection
        {
            get
            {
                return this.bool_4;
            }
            set
            {
                this.bool_4 = value;
            }
        }

        public bool ModifyConnectivity
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

        public string NetworkDatasetName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public INetworkDirections NetworkDirections
        {
            get
            {
                return this.inetworkDirections_0;
            }
            set
            {
                this.inetworkDirections_0 = value;
            }
        }

        public static NewNetworkDatasetHelper NewNetworkDataset
        {
            get
            {
                return m_helper;
            }
        }

        public bool SupportsTurns
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool UseElevationFieldName
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        public class FeatureClassWrap
        {
            private bool bool_0 = false;
            private IFeatureClass ifeatureClass_0 = null;
            private INetworkSourceDirections inetworkSourceDirections_0 = null;
            private int int_0 = 0;
            private string string_0 = "";
            private string string_1 = "";
            private string string_2 = "";

            public FeatureClassWrap(IFeatureClass ifeatureClass_1)
            {
                this.ifeatureClass_0 = ifeatureClass_1;
                if (this.ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    this.int_0 = 0;
                }
                else if (this.ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    this.int_0 = 1;
                }
            }

            public override string ToString()
            {
                return this.ifeatureClass_0.AliasName;
            }

            public string ElevationFieldName
            {
                get
                {
                    return this.string_0;
                }
                set
                {
                    this.string_0 = value;
                }
            }

            public IFeatureClass FeatureClass
            {
                get
                {
                    return this.ifeatureClass_0;
                }
            }

            public string FromElevationFieldName
            {
                get
                {
                    return this.string_1;
                }
                set
                {
                    this.string_1 = value;
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

            public int NetworkConnectivityPolicy
            {
                get
                {
                    return this.int_0;
                }
                set
                {
                    this.int_0 = 0;
                }
            }

            public INetworkSourceDirections NetworkSourceDirections
            {
                get
                {
                    if (this.inetworkSourceDirections_0 == null)
                    {
                        return null;
                    }
                    if (this.inetworkSourceDirections_0.StreetNameFields.Count == 0)
                    {
                        return null;
                    }
                    return this.inetworkSourceDirections_0;
                }
                set
                {
                    this.inetworkSourceDirections_0 = value;
                }
            }

            public string ToElevationFieldName
            {
                get
                {
                    return this.string_2;
                }
                set
                {
                    this.string_2 = value;
                }
            }
        }
    }
}

