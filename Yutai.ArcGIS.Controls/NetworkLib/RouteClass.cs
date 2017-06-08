using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class RouteClass
    {
        private string INPUT_STOPS_FC = "BayAreaLocations";
        private IFeatureClass m_pInputStopsFClass;
        private INetworkDataset m_pNetworkDataset;
        private IWorkspace m_pWorkspace;
        private string NETWORK_DATASET = "Streets_ND";
        private string SHAPE_INPUT_NAME_FIELD = "Name";
        private string SHAPE_WORKSPACE = @"..\..\..\..\Data\NetworkAnalyst";

        private INALayer CreateRouteAnalysisLayer(string sName, INetworkDataset pNetworkDataset)
        {
            INARouteSolver solver = new NARouteSolverClass();
            INASolverSettings settings = solver as INASolverSettings;
            INASolver solver2 = solver as INASolver;
            solver.FindBestSequence = true;
            solver.PreserveFirstStop = true;
            solver.PreserveLastStop = false;
            solver.UseTimeWindows = false;
            solver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;
            IStringArray restrictionAttributeNames = settings.RestrictionAttributeNames;
            restrictionAttributeNames.Add("Oneway");
            settings.RestrictionAttributeNames = restrictionAttributeNames;
            IDatasetComponent component = pNetworkDataset as IDatasetComponent;
            IDENetworkDataset dataElement = component.DataElement as IDENetworkDataset;
            INAContext context = solver2.CreateContext(dataElement, sName);
            (context as INAContextEdit).Bind(pNetworkDataset, new GPMessagesClass());
            INALayer layer = solver2.CreateLayer(context);
            (layer as ILayer).Name = sName;
            return layer;
        }

        private void SaveLayerToDisk(ILayer pLayer, string sPath)
        {
        }

        private void SetMemberVariables()
        {
            IFeatureWorkspace pWorkspace = this.m_pWorkspace as IFeatureWorkspace;
            IWorkspaceExtensionManager manager = this.m_pWorkspace as IWorkspaceExtensionManager;
            UID gUID = new UIDClass {
                Value = "esriGeoDatabase.NetworkDatasetWorkspaceExtension"
            };
            IDatasetContainer2 container = manager.FindExtension(gUID) as IDatasetContainer2;
            this.m_pNetworkDataset = container.get_DatasetByName(esriDatasetType.esriDTNetworkDataset, this.NETWORK_DATASET) as INetworkDataset;
            this.m_pInputStopsFClass = pWorkspace.OpenFeatureClass(this.INPUT_STOPS_FC);
        }

        private void Solve()
        {
            INALayer layer = this.CreateRouteAnalysisLayer("Route", this.m_pNetworkDataset);
            INAContext nAContext = layer.Context;
            INAClass class2 = nAContext.NAClasses.get_ItemByName("Stops") as INAClass;
            IFeatureClass class3 = nAContext.NAClasses.get_ItemByName("Routes") as IFeatureClass;
            INAClassFieldMap map = new NAClassFieldMapClass();
            map.set_MappedField("Name", this.SHAPE_INPUT_NAME_FIELD);
            INAClassLoader loader = new NAClassLoaderClass {
                Locator = nAContext.Locator,
                NAClass = class2,
                FieldMap = map
            };
            int rowsInCursor = 0;
            int rowsLocated = 0;
            loader.Load(this.m_pInputStopsFClass.Search(new QueryFilterClass(), false) as ICursor, new CancelTrackerClass(), ref rowsInCursor, ref rowsLocated);
            nAContext.Solver.Solve(nAContext, new GPMessagesClass(), new CancelTrackerClass());
            this.SaveLayerToDisk(layer as ILayer, Environment.CurrentDirectory + @"\Route.lyr");
        }

        public void SolveRoute()
        {
            IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
            this.m_pWorkspace = factory.OpenFromFile(this.SHAPE_WORKSPACE, 0);
            this.SetMemberVariables();
            this.Solve();
            this.m_pWorkspace = null;
            this.m_pNetworkDataset = null;
            this.m_pInputStopsFClass = null;
        }
    }
}

