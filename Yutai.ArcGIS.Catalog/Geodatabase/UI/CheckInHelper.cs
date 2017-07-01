using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class CheckInHelper : CheckOutInHelper
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private IWorkspaceName iworkspaceName_0 = null;
        public static CheckInHelper m_pHelper;
        private string string_0 = "";
        private string string_1 = "";

        static CheckInHelper()
        {
            old_acctor_mc();
        }

        public override void Do()
        {
            this.method_11();
        }

        private void method_11()
        {
            ICheckIn @in = new CheckInClass();
            ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event event2 =
                @in as ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event;
            base.InitEvent(event2);
            if (this.bool_0)
            {
                IName name = this.iworkspaceName_0 as IName;
                IWorkspace workspace = name.Open() as IWorkspace;
                IWorkspaceReplicas replicas = workspace as IWorkspaceReplicas;
                IReplica replica = replicas.Replicas.Next();
                try
                {
                    @in.CheckInFromGDB(base.m_MasterWorkspaceName, replica.Name, this.iworkspaceName_0, this.bool_1,
                        false);
                }
                catch
                {
                }
            }
            else
            {
                esriExportDataChangesOption esriExportToAccess = esriExportDataChangesOption.esriExportToAccess;
                if (Path.GetExtension(this.string_0).ToLower() == ".xml")
                {
                    esriExportToAccess = esriExportDataChangesOption.esriExportToXML;
                }
                try
                {
                    @in.CheckInFromDeltaFile(base.m_MasterWorkspaceName, this.string_1, this.string_0,
                        esriExportToAccess, this.bool_1, false);
                }
                catch
                {
                }
            }
            base.ReleaseEvent(event2);
        }

        private static void old_acctor_mc()
        {
            m_pHelper = null;
        }

        public bool CheckInfromGDB
        {
            set { this.bool_0 = value; }
        }

        public string CheckInName
        {
            set { this.string_1 = value; }
        }

        public IWorkspaceName CheckoutWorkspaceName
        {
            get { return this.iworkspaceName_0; }
            set { this.iworkspaceName_0 = value; }
        }

        public string DeltaFileName
        {
            set { this.string_0 = value; }
        }

        public bool ReconcileCheckout
        {
            set { this.bool_1 = value; }
        }
    }
}