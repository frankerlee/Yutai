using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxNewDatabase
    {
        IWorkspaceFactory WorkspaceFactory { set; }
    }
}