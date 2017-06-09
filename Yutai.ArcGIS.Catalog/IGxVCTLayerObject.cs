namespace Yutai.ArcGIS.Catalog
{
    public interface IGxVCTLayerObject
    {
        string LayerTypeName { get; }

        object VCTLayer { get; set; }
    }
}

