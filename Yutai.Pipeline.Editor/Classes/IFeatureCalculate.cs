using ESRI.ArcGIS.Carto;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface IFeatureCalculate
    {
        IFeatureLayer LineFeatureLayer { get; set; }
        IFeatureLayer PointFeatureLayer { get; set; }
    }
}