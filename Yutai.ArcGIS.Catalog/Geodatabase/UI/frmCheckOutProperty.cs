using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCheckOutProperty : Form
    {
        private Container container_0 = null;
        private IReplica ireplica_0 = null;
        private IWorkspace iworkspace_0 = null;

        public frmCheckOutProperty(IReplica ireplica_1, IWorkspace iworkspace_1)
        {
            this.InitializeComponent();
            this.ireplica_0 = ireplica_1;
            this.iworkspace_0 = iworkspace_1;
        }

        private void frmCheckOutProperty_Load(object sender, EventArgs e)
        {
            this.lblName.Text = this.ireplica_0.Name;
            this.lblOwner.Text = this.ireplica_0.Owner;
            this.lblCreateTime.Text = new DateTime((long) this.ireplica_0.ReplicaDate).ToLongDateString();
            this.lblParentVersion.Text = this.method_0(this.ireplica_0.Version);
            this.CheckOutDatasetlist.Items.Clear();
            IEnumReplicaDataset replicaDatasets = this.ireplica_0.ReplicaDatasets;
            replicaDatasets.Reset();
            IReplicaDataset dataset2 = replicaDatasets.Next();
            string item = "";
            while (dataset2 != null)
            {
                if (dataset2.Type == esriDatasetType.esriDTTable)
                {
                    item = "表:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                {
                    item = "要素类:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTGeometricNetwork)
                {
                    item = "网络:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTRelationshipClass)
                {
                    item = "关系:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTTopology)
                {
                    item = "拓扑:" + dataset2.Name;
                }
                else
                {
                    item = dataset2.Name;
                }
                this.CheckOutDatasetlist.Items.Add(item);
                dataset2 = replicaDatasets.Next();
            }
        }

        private string method_0(string string_0)
        {
            if (this.iworkspace_0 is IVersionedWorkspace)
            {
                IVersionedWorkspace workspace = this.iworkspace_0 as IVersionedWorkspace;
                try
                {
                    IVersion version = workspace.FindVersion(string_0);
                    if (version.HasParent())
                    {
                        return version.VersionInfo.Parent.VersionName;
                    }
                }
                catch
                {
                }
            }
            return "";
        }

        public IReplica Replica
        {
            set { this.ireplica_0 = value; }
        }
    }
}