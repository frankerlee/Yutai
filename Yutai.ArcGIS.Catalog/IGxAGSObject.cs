using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog
{
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

