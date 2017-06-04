using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	internal class CheckOutCheckIn
	{
		public CheckOutCheckIn()
		{
		}

        public static void SimpleCheckOut(IWorkspaceName iworkspaceName_0, IWorkspaceName iworkspaceName_1, IEnumName ienumName_0, bool bool_0, bool bool_1, string string_0)
        {
            IName name = iworkspaceName_1 as IName;
            IWorkspace arg_12_0 = name.Open() as IWorkspace;
            IWorkspace arg_23_0 = (iworkspaceName_0 as IName).Open() as IWorkspace;
            IReplicaDescription replicaDescription = new ReplicaDescription() as IReplicaDescription;
            replicaDescription.Init(ienumName_0, iworkspaceName_1, bool_0, esriDataExtractionType.esriDataCheckOut);
            replicaDescription.ReplicaModelType = esriReplicaModelType.esriModelTypeFullGeodatabase;
            ICheckOut checkOut = new CheckOut();
            if (bool_1)
            {
                checkOut.CheckOutData(replicaDescription, true, string_0);
            }
            else
            {
                checkOut.CheckOutSchema(replicaDescription, string_0);
            }
        }

        public static void SimpleCheckIn(IWorkspaceName iworkspaceName_0, IWorkspaceName iworkspaceName_1, IEnumName ienumName_0, bool bool_0, bool bool_1, string string_0)
        {
            IName name = iworkspaceName_1 as IName;
            IWorkspace workspace = name.Open() as IWorkspace;
            IWorkspaceReplicas workspaceReplicas = workspace as IWorkspaceReplicas;
            IReplica replica = workspaceReplicas.Replicas.Next();
            IWorkspace arg_36_0 = (iworkspaceName_0 as IName).Open() as IWorkspace;
            ICheckIn checkIn = new CheckIn();
            if (bool_1)
            {
                checkIn.CheckInFromGDB(iworkspaceName_0, replica.Name, iworkspaceName_1, false, false);
            }
        }
    }
}