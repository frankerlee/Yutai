using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class ExportChangesHelper : CheckOutInHelper
    {
        private IWorkspaceName iworkspaceName_0 = null;
        public static ExportChangesHelper m_pHelper;
        private string string_0 = "";

        static ExportChangesHelper()
        {
            old_acctor_mc();
        }

        public override void Do()
        {
            this.method_11();
        }

        private void method_11()
        {
            esriExportDataChangesOption esriExportToAccess = esriExportDataChangesOption.esriExportToAccess;
            if (Path.GetExtension(this.string_0).ToLower() == ".xml")
            {
                esriExportToAccess = esriExportDataChangesOption.esriExportToXML;
            }
            IExportDataChanges changes = new DataChangesExporterClass();
            IName name = this.iworkspaceName_0 as IName;
            IWorkspace workspace = name.Open() as IWorkspace;
            IWorkspaceReplicas replicas = workspace as IWorkspaceReplicas;
            IEnumReplica replica = replicas.Replicas;
            replica.Reset();
            if (replica != null)
            {
                for (IReplica replica2 = replica.Next(); replica2 != null; replica2 = replica.Next())
                {
                    if (replica2.ReplicaRole == esriReplicaType.esriCheckOutTypeChild)
                    {
                        IReplicaDataChangesInit init = new CheckOutDataChangesClass();
                        init.Init(replica2, this.iworkspaceName_0);
                        IDataChanges dataChanges = init as IDataChanges;
                        changes.ExportDataChanges(this.string_0, esriExportToAccess, dataChanges, true);
                    }
                }
            }
        }

        private static void old_acctor_mc()
        {
            m_pHelper = null;
        }

        public IWorkspaceName CheckoutWorkspaceName
        {
            get
            {
                return this.iworkspaceName_0;
            }
            set
            {
                this.iworkspaceName_0 = value;
            }
        }

        public string DeltaFileName
        {
            set
            {
                this.string_0 = value;
            }
        }
    }
}

