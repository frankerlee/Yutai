namespace JLK.Catalog
{
    using ESRI.ArcGIS.Geometry;

    public interface IGxPrjFile
    {
        ISpatialReference SpatialReference { get; }
    }
}

