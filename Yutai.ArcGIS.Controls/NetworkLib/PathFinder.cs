using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class PathFinder
    {
        public INetWeight FromToEdgeWeight = null;
        public INetWeight JunctionWeight = null;
        private double m_dblPathCost;
        private IEnumNetEID m_ipEnumNetEID_Edges;
        private IEnumNetEID m_ipEnumNetEID_Junctions;
        private IGeometricNetwork m_ipGeometricNetwork;
        private IMap m_ipMap;
        private IPointCollection m_ipPoints;
        private IPointToEID m_ipPointToEID;
        private IPolyline m_ipPolyline;
        public INetWeight ToFromEdgeWeight = null;

        private void CloseWorkspace()
        {
            this.m_ipGeometricNetwork = null;
            this.m_ipPoints = null;
            this.m_ipPointToEID = null;
            this.m_ipEnumNetEID_Junctions = null;
            this.m_ipEnumNetEID_Edges = null;
            this.m_ipPolyline = null;
        }

        private bool InitializeNetworkAndMap(IFeatureDataset FeatureDataset)
        {
            bool flag = false;
            try
            {
                IFeatureLayer layer;
                int num;
                this.m_ipGeometricNetwork = ((INetworkCollection) FeatureDataset).get_GeometricNetwork(0);
                INetwork network = this.m_ipGeometricNetwork.Network;
                if (this.m_ipMap == null)
                {
                    this.m_ipMap = new MapClass();
                    IFeatureClassContainer ipGeometricNetwork = (IFeatureClassContainer) this.m_ipGeometricNetwork;
                    for (num = 0; num < ipGeometricNetwork.ClassCount; num++)
                    {
                        IFeatureClass class2 = ipGeometricNetwork.get_Class(num);
                        layer = new FeatureLayerClass
                        {
                            FeatureClass = class2
                        };
                        this.m_ipMap.AddLayer(layer);
                    }
                }
                IEnvelope envelope = new EnvelopeClass();
                for (num = 0; num < this.m_ipMap.LayerCount; num++)
                {
                    layer = (IFeatureLayer) this.m_ipMap.get_Layer(num);
                    IGeoDataset dataset = (IGeoDataset) layer;
                    IEnvelope extent = dataset.Extent;
                    envelope.Union(extent);
                }
                this.m_ipPointToEID = new PointToEIDClass();
                this.m_ipPointToEID.SourceMap = this.m_ipMap;
                this.m_ipPointToEID.GeometricNetwork = this.m_ipGeometricNetwork;
                double width = envelope.Width;
                double height = envelope.Height;
                if (width > height)
                {
                    this.m_ipPointToEID.SnapTolerance = width/100.0;
                }
                else
                {
                    this.m_ipPointToEID.SnapTolerance = height/100.0;
                }
                flag = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return flag;
        }

        public void OpenAccessNetwork(string AccessFileName, string FeatureDatasetName)
        {
            this.CloseWorkspace();
            IFeatureDataset featureDataset =
                ((IFeatureWorkspace) new AccessWorkspaceFactoryClass().OpenFromFile(AccessFileName, 0))
                    .OpenFeatureDataset(FeatureDatasetName);
            if (!this.InitializeNetworkAndMap(featureDataset))
            {
                MessageBox.Show("Error initializing Network and Map", "OpenAccessNetwork");
            }
        }

        public void OpenFeatureDatasetNetwork(IFeatureDataset FeatureDataset)
        {
            this.CloseWorkspace();
            if (!this.InitializeNetworkAndMap(FeatureDataset))
            {
                MessageBox.Show("Error initializing Network and Map", "OpenFeatureDatasetNetwork");
            }
        }

        public void SolvePath(string WeightName)
        {
            int num;
            ITraceFlowSolver solver = (ITraceFlowSolver) new TraceFlowSolverClass();
            INetSolver solver2 = (INetSolver) solver;
            INetwork network = this.m_ipGeometricNetwork.Network;
            solver2.SourceNetwork = network;
            INetElements elements = (INetElements) network;
            IEdgeFlag[] edgeOrigins = new IEdgeFlag[this.m_ipPoints.PointCount + 1];
            for (num = 0; num < this.m_ipPoints.PointCount; num++)
            {
                IPoint point;
                int num2;
                double num3;
                int num4;
                int num5;
                int num6;
                INetFlag flag = new EdgeFlagClass();
                IPoint inputPoint = this.m_ipPoints.get_Point(num);
                this.m_ipPointToEID.GetNearestEdge(inputPoint, out num2, out point, out num3);
                elements.QueryIDs(num2, esriElementType.esriETEdge, out num4, out num5, out num6);
                flag.UserClassID = num4;
                flag.UserID = num5;
                flag.UserSubID = num6;
                edgeOrigins[num] = (IEdgeFlag) flag;
            }
            (solver as ITraceFlowSolverGEN).PutEdgeOrigins(ref edgeOrigins);
            try
            {
                INetSolverWeights weights = (INetSolverWeights) solver;
                weights.JunctionWeight = this.JunctionWeight;
                weights.FromToEdgeWeight = this.FromToEdgeWeight;
                weights.ToFromEdgeWeight = this.ToFromEdgeWeight;
            }
            catch
            {
            }
            object[] segmentCosts = new object[this.m_ipPoints.PointCount];
            (solver as ITraceFlowSolverGEN).FindPath(esriFlowMethod.esriFMConnected,
                esriShortestPathObjFn.esriSPObjFnMinSum, out this.m_ipEnumNetEID_Junctions,
                out this.m_ipEnumNetEID_Edges, this.m_ipPoints.PointCount - 1, ref segmentCosts);
            this.m_dblPathCost = 0.0;
            for (num = 0; num < segmentCosts.Length; num++)
            {
                this.m_dblPathCost += (double) segmentCosts[num];
            }
            this.m_ipPolyline = null;
        }

        public IMap Map
        {
            get { return this.m_ipMap; }
            set { this.m_ipMap = value; }
        }

        public double PathCost
        {
            get { return this.m_dblPathCost; }
        }

        public IPolyline PathPolyLine
        {
            get
            {
                if (this.m_ipPolyline == null)
                {
                    this.m_ipPolyline = new PolylineClass();
                    IGeometryCollection ipPolyline = (IGeometryCollection) this.m_ipPolyline;
                    IEIDHelper helper = new EIDHelperClass
                    {
                        GeometricNetwork = this.m_ipGeometricNetwork
                    };
                    ISpatialReference spatialReference = this.m_ipMap.SpatialReference;
                    helper.OutputSpatialReference = spatialReference;
                    helper.ReturnGeometries = true;
                    IEnumEIDInfo info = helper.CreateEnumEIDInfo(this.m_ipEnumNetEID_Edges);
                    info.Reset();
                    for (int i = 1; i <= info.Count; i++)
                    {
                        IGeometry geometry = info.Next().Geometry;
                        ipPolyline.AddGeometryCollection((IGeometryCollection) geometry);
                    }
                }
                return this.m_ipPolyline;
            }
        }

        public IPointCollection StopPoints
        {
            get { return this.m_ipPoints; }
            set { this.m_ipPoints = value; }
        }
    }
}