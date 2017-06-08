namespace JLK.Catalog
{
    using ESRI.ArcGIS.Geodatabase;
    using System;

    public interface IGxNewDatabase
    {
        IWorkspaceFactory WorkspaceFactory { set; }
    }
}

