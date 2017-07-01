using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxLayer
    {
        ILayer Layer { get; set; }

        LayerType LayerType { get; }
    }
}