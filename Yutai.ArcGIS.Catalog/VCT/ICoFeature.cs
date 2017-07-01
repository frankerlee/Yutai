namespace Yutai.ArcGIS.Catalog.VCT
{
    public interface ICoFeature
    {
        void AppendValue(object object_0);
        object GetValue(ICoField icoField_0);
        object GetValue(int int_0);
        object GetValue(string string_0);
        void SetValue(ICoField icoField_0, object object_0);
        void SetValue(int int_0, object object_0);
        void SetValue(string string_0, object object_0);

        ICoLayer Layer { get; }

        int OID { get; set; }

        CoFeatureType Type { get; }

        object[] Values { get; }
    }
}