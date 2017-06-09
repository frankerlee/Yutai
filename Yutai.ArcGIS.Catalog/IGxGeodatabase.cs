namespace Yutai.ArcGIS.Catalog
{
    public interface IGxGeodatabase
    {
        void Backup(string string_0, string string_1, string string_2, out bool bool_0);
        void DetachGeodatabase();
        void GeodatabaseName(ref string string_0);
        void GetProperties(out string string_0, out object object_0, out int int_0, out string string_1, out object object_1);
        void Upgrade();

        object DataServerManager { get; }
    }
}

