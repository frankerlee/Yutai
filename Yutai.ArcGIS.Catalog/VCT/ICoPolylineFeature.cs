using System.Collections.Generic;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public interface ICoPolylineFeature
    {
        List<CoPointCollection> Points { get; }
    }
}

