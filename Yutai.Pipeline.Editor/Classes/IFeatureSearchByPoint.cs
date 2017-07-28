using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Classes
{
    interface IFeatureSearchByPoint : IFeatureSearch
    {
        IFeatureLayer LineFeatureLayer { get; set; }
        IPoint Point { get; set; }

        IFeature GetNearFeature();
    }
}
