using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxAGSConnection
    {
        void Connect();
        void Disconnect();
        void EditServerProperties(int int_0, short short_0);
        void LoadFromFile(string string_0);
        void SaveToFile(string string_0);

        IAGSServerConnectionName AGSServerConnectionName { get; set; }

        int ConnectionMode { get; set; }

        string FileName { get; }

        bool IsConnected { get; }

        object SelectedServerObjects { get; set; }
    }
}