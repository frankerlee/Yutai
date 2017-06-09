using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxGPGPTool
    {
        IAGSServerConnection AGSServerConnection { get; set; }

        IAGSServerObjectName AGSServerObjectName { get; set; }

        IGPToolInfo GPToolInfo { get; set; }
    }
}

