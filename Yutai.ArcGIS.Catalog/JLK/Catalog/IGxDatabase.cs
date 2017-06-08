namespace JLK.Catalog
{
    using ESRI.ArcGIS.Geodatabase;
    using System;

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

