namespace JLK.Catalog
{
    using ESRI.ArcGIS.Geoprocessing;
    using ESRI.ArcGIS.GISClient;
    using System;

    public interface IGxGPGPTool
    {
        IAGSServerConnection AGSServerConnection { get; set; }

        IAGSServerObjectName AGSServerObjectName { get; set; }

        IGPToolInfo GPToolInfo { get; set; }
    }
}

