using System.Collections.Generic;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public interface ICoLayer
    {
        void AppendFeature(ICoFeature icoFeature_0);
        ICoFeature GetFeature(int int_0);
        ICoFeature GetFeatureByIndex(int int_0);
        ICoField GetField(int int_0);
        ICoField GetField(string string_0);
        int GetFieldIndex(ICoField icoField_0);
        void RemoveAllFeature();
        void RemoveFeature(ICoFeature icoFeature_0);
        void RemoveFeature(int int_0);

        string AliasName { get; set; }

        string Categorie { get; set; }

        bool Enable { get; set; }

        int FeatureCount { get; }

        List<ICoField> Fields { get; }

        string ID { get; set; }

        CoLayerType LayerType { get; set; }

        string Name { get; set; }

        CoLayerHead Parameter { get; set; }

        object Tag { get; set; }
    }
}

