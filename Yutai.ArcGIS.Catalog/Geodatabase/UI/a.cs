using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class a
    {
        public void List_CO_Versions(IWorkspace iworkspace_0)
        {
            if (iworkspace_0 is IVersionedWorkspace)
            {
                IVersionedWorkspace workspace = iworkspace_0 as IVersionedWorkspace;
                IEnumVersionInfo versions = workspace.Versions;
                versions.Reset();
                for (IVersionInfo info2 = versions.Next(); info2 != null; info2 = versions.Next())
                {
                    if (!this.method_0(info2.VersionName, iworkspace_0 as IWorkspaceReplicas))
                    {
                    }
                }
            }
        }

        private bool method_0(string string_0, IWorkspaceReplicas iworkspaceReplicas_0)
        {
            ISQLSyntax syntax = iworkspaceReplicas_0 as ISQLSyntax;
            IEnumReplica replicas = iworkspaceReplicas_0.Replicas;
            replicas.Reset();
            for (IReplica replica2 = replicas.Next(); replica2 != null; replica2 = replicas.Next())
            {
                if (replica2.ReplicaRole == esriReplicaType.esriCheckOutTypeParent)
                {
                    string str = syntax.QualifyTableName("", replica2.Owner, replica2.Version);
                    if (string_0.ToLower() == str.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}