namespace JLK.Catalog
{
    using System;

    public interface IGxVCTLayerObject
    {
        string LayerTypeName { get; }

        object VCTLayer { get; set; }
    }
}

