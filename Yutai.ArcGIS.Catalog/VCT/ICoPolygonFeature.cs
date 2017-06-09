using System.Collections.Generic;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public interface ICoPolygonFeature
    {
        List<CoPointCollection> Points { get; }
    }
}

