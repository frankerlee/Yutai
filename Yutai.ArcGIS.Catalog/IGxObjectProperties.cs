namespace Yutai.ArcGIS.Catalog
{
    public interface IGxObjectProperties
    {
        void GetPropByIndex(int int_0, ref string string_0, ref object object_0);
        object GetProperty(string string_0);
        void SetProperty(string string_0, object object_0);

        int PropertyCount { get; }
    }
}