using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common
{
    public interface IFeatureSnapAgent : IEngineSnapAgent
    {
        IFeatureCache FeatureCache { get; set; }

        IFeatureClass FeatureClass { get; set; }

        int GeometryHitType { get; set; }
    }
}