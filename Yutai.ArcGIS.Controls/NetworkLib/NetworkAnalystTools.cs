using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class NetworkAnalystTools
    {
        public static ContextMenu contextMenu1;
        public static INetworkDataset m_AnalystNetworkDataset = null;
        public static IMap m_FocusMap = null;
        public static EngineNetworkAnalystEnvironmentClass m_naEnv = new EngineNetworkAnalystEnvironmentClass();
        private static MenuItem miClearLocations;
        private static MenuItem miLoadLocations;

        public static INALayer CreateClosestFacilityLayer(string layerName, INetworkDataset networkDataset, INAClosestFacilitySolver naClosesFacilitySolver)
        {
            INASolverSettings settings = naClosesFacilitySolver as INASolverSettings;
            INASolver solver = naClosesFacilitySolver as INASolver;
            IDatasetComponent component = networkDataset as IDatasetComponent;
            IDENetworkDataset dataElement = component.DataElement as IDENetworkDataset;
            INAContext context = solver.CreateContext(dataElement, layerName);
            (context as INAContextEdit).Bind(networkDataset, new GPMessagesClass());
            INALayer layer = solver.CreateLayer(context);
            (layer as ILayer).Name = layerName;
            naClosesFacilitySolver.CreateTraversalResult = true;
            naClosesFacilitySolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionFromFacility;
            naClosesFacilitySolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;
            IStringArray restrictionAttributeNames = settings.RestrictionAttributeNames;
            restrictionAttributeNames.Add("Oneway");
            settings.RestrictionAttributeNames = restrictionAttributeNames;
            solver.UpdateContext(context, dataElement, new GPMessagesClass());
            return layer;
        }

        public static INALayer CreateNetworkAnalysisLayer(string name, INetworkDataset networkDataset, INASolver naSolver)
        {
            if (naSolver is INARouteSolver)
            {
                return CreateRouteAnalysisLayer(name, networkDataset, naSolver as INARouteSolver);
            }
            if (naSolver is INAClosestFacilitySolver)
            {
                return CreateClosestFacilityLayer(name, networkDataset, naSolver as INAClosestFacilitySolver);
            }
            IDatasetComponent component = (IDatasetComponent) networkDataset;
            IDENetworkDataset dataElement = (IDENetworkDataset) component.DataElement;
            INAContext context = naSolver.CreateContext(dataElement, name);
            ((INAContextEdit) context).Bind(networkDataset, null);
            INALayer layer = naSolver.CreateLayer(context);
            ((ILayer) layer).Name = name;
            return layer;
        }

        public static INALayer CreateRouteAnalysisLayer(string layerName, INetworkDataset networkDataset, INARouteSolver naRouteSolver)
        {
            INASolverSettings settings = naRouteSolver as INASolverSettings;
            INASolver solver = naRouteSolver as INASolver;
            IDatasetComponent component = networkDataset as IDatasetComponent;
            IDENetworkDataset dataElement = component.DataElement as IDENetworkDataset;
            INAContext context = solver.CreateContext(dataElement, layerName);
            (context as INAContextEdit).Bind(networkDataset, new GPMessagesClass());
            INALayer layer = solver.CreateLayer(context);
            (layer as ILayer).Name = layerName;
            naRouteSolver.FindBestSequence = true;
            naRouteSolver.PreserveFirstStop = true;
            naRouteSolver.PreserveLastStop = false;
            naRouteSolver.UseTimeWindows = false;
            naRouteSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;
            IStringArray restrictionAttributeNames = settings.RestrictionAttributeNames;
            restrictionAttributeNames.Add("Oneway");
            settings.RestrictionAttributeNames = restrictionAttributeNames;
            solver.UpdateContext(context, dataElement, new GPMessagesClass());
            return layer;
        }

        public static void Init()
        {
            m_naEnv.ZoomToResultAfterSolve = false;
            m_naEnv.ShowAnalysisMessagesAfterSolve = 3;
            contextMenu1 = new ContextMenu();
            miLoadLocations = new MenuItem();
            miClearLocations = new MenuItem();
            contextMenu1.MenuItems.AddRange(new MenuItem[] { miLoadLocations, miClearLocations });
            miLoadLocations.Index = 0;
            miLoadLocations.Text = "Load Locations...";
            miLoadLocations.Click += new EventHandler(NetworkAnalystTools.miLoadLocations_Click);
            miClearLocations.Index = 1;
            miClearLocations.Text = "Clear Locations";
            miClearLocations.Click += new EventHandler(NetworkAnalystTools.miClearLocations_Click);
        }

        private static void miClearLocations_Click(object sender, EventArgs e)
        {
        }

        private static void miLoadLocations_Click(object sender, EventArgs e)
        {
        }

        public bool OnContextMenu(int x, int y)
        {
            return true;
        }
    }
}

