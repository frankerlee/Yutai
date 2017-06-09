using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxDatabase
    {
        void Connect();
        void Disconnect();

        bool IsConnected { get; }

        bool IsEnterpriseGeodatabase { get; }

        bool IsRemoteDatabase { get; }

        IWorkspace Workspace { get; }

        IWorkspaceName WorkspaceName { get; set; }
    }
}

