using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;

namespace Yutai.ArcGIS.Controls.ClosestFacilitySolver
{
    internal partial class frmClosesFacilitySolver : Form
    {
        private IMap m_pFocusMap = null;

        public frmClosesFacilitySolver()
        {
            this.InitializeComponent();
        }

        public void CheckInNetworkAnalystExtension()
        {
        }

        public string CheckOutNetworkAnalystExtension()
        {
            return "OK";
        }

        private void cmdSolve_Click(object sender, EventArgs e)
        {
            try
            {
                this.lstOutput.Items.Clear();
                this.lstOutput.Items.Add("Solving...");
                this.SetSolverSettings();
                IGPMessages messages = new GPMessagesClass();
                if (!this.m_pNAContext.Solver.Solve(this.m_pNAContext, messages, null))
                {
                    this.GetCFOutput("CFRoutes");
                }
                else
                {
                    this.lstOutput.Items.Add("Partial Result");
                }
                if (messages != null)
                {
                    for (int i = 0; i < messages.Count; i++)
                    {
                        esriGPMessageType type = messages.GetMessage(i).Type;
                        if (type != esriGPMessageType.esriGPMessageTypeWarning)
                        {
                            if (type != esriGPMessageType.esriGPMessageTypeError)
                            {
                                goto Label_0116;
                            }
                            this.lstOutput.Items.Add("Error " + messages.GetMessage(i).ErrorCode.ToString() + " " + messages.GetMessage(i).Description);
                        }
                        else
                        {
                            this.lstOutput.Items.Add("Warning " + messages.GetMessage(i).Description);
                        }
                        continue;
                    Label_0116:
                        this.lstOutput.Items.Add("Information " + messages.GetMessage(i).Description);
                    }
                }
                IGeoDataset dataset = this.m_pNAContext.NAClasses.get_ItemByName("CFRoutes") as IGeoDataset;
                IEnvelope extent = dataset.Extent;
                if (!extent.IsEmpty)
                {
                    extent.Expand(1.1, 1.1, true);
                }
                (this.m_pFocusMap as IActiveView).Extent = extent;
                (this.m_pFocusMap as IActiveView).Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                this.cmdSolve.Text = "Find Closest Faclities";
            }
        }

        public INAContext CreateSolverContext(INetworkDataset pNetDataset)
        {
            IDENetworkDataset dENetworkDataset = this.GetDENetworkDataset(pNetDataset);
            INASolver solver = new NAClosestFacilitySolverClass();
            INAContextEdit edit = solver.CreateContext(dENetworkDataset, solver.Name) as INAContextEdit;
            edit.Bind(pNetDataset, new GPMessagesClass());
            return (edit as INAContext);
        }

 private void frmClosesFacilitySolver_Load(object sender, EventArgs e)
        {
        }

        public void GetCFOutput(string strNAClass)
        {
            ITable table = this.m_pNAContext.NAClasses.get_ItemByName(strNAClass) as ITable;
            if (table == null)
            {
                this.lstOutput.Items.Add("Impssible to get the " + strNAClass + " table");
            }
            this.lstOutput.Items.Add("Number facility(ies) found " + table.RowCount(null).ToString());
            this.lstOutput.Items.Add("");
            if (table.RowCount(null) > 0)
            {
                this.lstOutput.Items.Add("IncidentID, FacilityID,FacilityRank,Total_" + this.cboCostAttribute.Text);
                ICursor cursor = table.Search(null, false);
                for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
                {
                    long num2 = long.Parse(row.get_Value(table.FindField("IncidentID")).ToString());
                    long num3 = long.Parse(row.get_Value(table.FindField("FacilityID")).ToString());
                    long num4 = long.Parse(row.get_Value(table.FindField("FacilityRank")).ToString());
                    double num = double.Parse(row.get_Value(table.FindField("Total_" + this.cboCostAttribute.Text)).ToString());
                    this.lstOutput.Items.Add(num2.ToString() + ",\t" + num3.ToString() + ",\t" + num4.ToString() + ",\t" + num.ToString("F2"));
                }
            }
            this.lstOutput.Refresh();
        }

        public IDENetworkDataset GetDENetworkDataset(INetworkDataset pNetDataset)
        {
            IDatasetComponent component = pNetDataset as IDatasetComponent;
            return (component.DataElement as IDENetworkDataset);
        }

        private void Initialize()
        {
            this.CheckOutNetworkAnalystExtension();
            IFeatureWorkspace workspace = this.OpenWorkspace(Application.StartupPath + @"\..\..\..\..\Data\NetworkAnalyst") as IFeatureWorkspace;
            INetworkDataset pNetDataset = this.OpenNetworkDataset(workspace as IWorkspace, "Streets_nd");
            this.m_pNAContext = this.CreateSolverContext(pNetDataset);
            for (int i = 0; i < (pNetDataset.AttributeCount - 1); i++)
            {
                INetworkAttribute attribute = pNetDataset.get_Attribute(i);
                if (attribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
                {
                    this.cboCostAttribute.Items.Add(attribute.Name);
                    this.cboCostAttribute.SelectedIndex = 0;
                }
            }
            this.txtTargetFacility.Text = "1";
            this.txtCutOff.Text = "";
            IFeatureClass pInputFC = workspace.OpenFeatureClass("BayAreaIncident");
            this.LoadNANetworkLocations("Incidents", pInputFC, 100.0);
            pInputFC = workspace.OpenFeatureClass("BayAreaLocations");
            this.LoadNANetworkLocations("Facilities", pInputFC, 100.0);
            INetworkLayer layer2 = new NetworkLayerClass {
                NetworkDataset = pNetDataset
            };
            ILayer layer = layer2 as ILayer;
            layer.Name = "Network Dataset";
            layer = this.m_pNAContext.Solver.CreateLayer(this.m_pNAContext) as ILayer;
            layer.Name = this.m_pNAContext.Solver.DisplayName;
        }

 private bool IsNumeric(string str)
        {
            try
            {
                double.Parse(str.Trim());
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void LoadNANetworkLocations(string strNAClassName, IFeatureClass pInputFC, double SnapTolerance)
        {
            INAClass class2 = this.m_pNAContext.NAClasses.get_ItemByName(strNAClassName) as INAClass;
            class2.DeleteAllRows();
            INAClassLoader loader = new NAClassLoaderClass {
                Locator = this.m_pNAContext.Locator
            };
            if (SnapTolerance > 0.0)
            {
                loader.Locator.SnapTolerance = SnapTolerance;
            }
            loader.NAClass = class2;
            INAClassFieldMap map = new NAClassFieldMapClass();
            map.CreateMapping(class2.ClassDefinition, pInputFC.Fields);
            loader.FieldMap = map;
            int rowsInCursor = 0;
            int rowsLocated = 0;
            IFeatureCursor cursor = pInputFC.Search(null, true);
            loader.Load((ICursor) cursor, null, ref rowsInCursor, ref rowsLocated);
        }

        public INetworkDataset OpenNetworkDataset(IWorkspace pWorkspace, string sNDSName)
        {
            IWorkspaceExtensionManager manager = pWorkspace as IWorkspaceExtensionManager;
            int extensionCount = manager.ExtensionCount;
            for (int i = 0; i < extensionCount; i++)
            {
                IWorkspaceExtension extension = manager.get_Extension(i);
                if (extension.Name.Equals("Network Dataset"))
                {
                    IDatasetContainer2 container = extension as IDatasetContainer2;
                    return (container.get_DatasetByName(esriDatasetType.esriDTNetworkDataset, sNDSName) as INetworkDataset);
                }
            }
            return null;
        }

        public IWorkspace OpenWorkspace(string strGDBName)
        {
            IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
            return factory.OpenFromFile(strGDBName, 0);
        }

        public void SetSolverSettings()
        {
            INASolver solver = this.m_pNAContext.Solver;
            INAClosestFacilitySolver solver2 = solver as INAClosestFacilitySolver;
            if ((this.txtCutOff.Text.Length > 0) && this.IsNumeric(this.txtCutOff.Text.Trim()))
            {
                solver2.DefaultCutoff = this.txtCutOff.Text;
            }
            else
            {
                solver2.DefaultCutoff = null;
            }
            if ((this.txtTargetFacility.Text.Length > 0) && this.IsNumeric(this.txtTargetFacility.Text))
            {
                solver2.DefaultTargetFacilityCount = int.Parse(this.txtTargetFacility.Text);
            }
            else
            {
                solver2.DefaultTargetFacilityCount = 1;
            }
            solver2.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;
            solver2.TravelDirection = esriNATravelDirection.esriNATravelDirectionToFacility;
            INASolverSettings settings = solver as INASolverSettings;
            settings.ImpedanceAttributeName = this.cboCostAttribute.Text;
            IStringArray restrictionAttributeNames = settings.RestrictionAttributeNames;
            restrictionAttributeNames.RemoveAll();
            if (this.chkUseRestriction.Checked)
            {
                restrictionAttributeNames.Add("oneway");
            }
            settings.RestrictionAttributeNames = restrictionAttributeNames;
            settings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack;
            settings.IgnoreInvalidLocations = true;
            settings.UseHierarchy = this.chkUseHierarchy.Checked;
            if (settings.UseHierarchy)
            {
                settings.HierarchyAttributeName = "hierarchy";
                settings.HierarchyLevelCount = 3;
                settings.set_MaxValueForHierarchy(1, 1);
                settings.set_NumTransitionToHierarchy(1, 9);
                settings.set_MaxValueForHierarchy(2, 2);
                settings.set_NumTransitionToHierarchy(2, 9);
            }
            solver.UpdateContext(this.m_pNAContext, this.GetDENetworkDataset(this.m_pNAContext.NetworkDataset), new GPMessagesClass());
        }

        public string Solve(INAContext pNAContext, IGPMessages pGPMessages)
        {
            string str = "";
            try
            {
                str = "Error when solving";
                if (!pNAContext.Solver.Solve(pNAContext, pGPMessages, null))
                {
                    return "OK";
                }
                return "Partial Solution";
            }
            catch (Exception exception)
            {
                return (str + " Error Description " + exception.Message);
            }
        }

        private IMap FocusMap
        {
            set
            {
                this.m_pFocusMap = value;
            }
        }
    }
}

