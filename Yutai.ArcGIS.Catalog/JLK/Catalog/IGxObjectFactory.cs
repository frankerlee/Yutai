namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;

    public interface IGxObjectFactory
    {
        IEnumGxObject GetChildren(string string_0, IFileNames ifileNames_0);
        bool HasChildren(string string_0, IFileNames ifileNames_0);

        IGxCatalog Catalog { set; }

        string Name { get; }
    }
}

