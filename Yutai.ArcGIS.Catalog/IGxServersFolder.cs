using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxServersFolder
    {
        IAGSServerConnection2 AGSServerConnection { get; set; }

        string FolderName { get; set; }
    }
}

