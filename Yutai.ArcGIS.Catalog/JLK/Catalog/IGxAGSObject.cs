namespace JLK.Catalog
{
    using ESRI.ArcGIS.GISClient;
    using System;

    public interface IGxAGSObject
    {
        void EditServerObjectProperties(int int_0);

        IAGSServerObjectName AGSServerObjectName { get; set; }

        string DefaultMapName { get; }

        int NumInstancesInUse { get; }

        int NumInstancesRunning { get; }

        string Status { get; }
    }
}

