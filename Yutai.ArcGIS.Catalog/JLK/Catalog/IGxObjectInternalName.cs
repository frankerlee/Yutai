namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;

    public interface IGxObjectInternalName
    {
        IName InternalObjectName { get; set; }
    }
}

