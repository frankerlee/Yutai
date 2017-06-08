namespace JLK.Catalog
{
    using ESRI.ArcGIS.GISClient;
    using System;

    public interface IGxServersFolder
    {
        IAGSServerConnection2 AGSServerConnection { get; set; }

        string FolderName { get; set; }
    }
}

