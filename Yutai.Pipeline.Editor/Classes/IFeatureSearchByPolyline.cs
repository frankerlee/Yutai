using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface IFeatureSearchByPolyline : IFeatureSearch
    {
        double Tolerance { get; set; }
        IFeatureLayer PointFeatureLayer { get; set; }
        IPolyline Polyline { get; set; }

        IFeature GetNearFromFeature();
        IFeature GetNearToFeature();
    }
}