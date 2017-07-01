using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxPrjFile
    {
        ISpatialReference SpatialReference { get; }
    }
}