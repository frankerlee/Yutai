namespace JLK.Catalog
{
    using ESRI.ArcGIS.Carto;
    using System;

    public interface IGxLayer
    {
        ILayer Layer { get; set; }

        JLK.Catalog.LayerType LayerType { get; }
    }
}

