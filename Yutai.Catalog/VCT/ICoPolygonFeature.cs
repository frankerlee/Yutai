namespace Yutai.Catalog.VCT
{
    using System.Collections.Generic;

    public interface ICoPolygonFeature
    {
        List<CoPointCollection> Points { get; }
    }
}

