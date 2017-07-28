using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Classes
{
    interface ILineFeatureCalculate : IFeatureCalculate
    {
        IFeature LineFeature { get; set; }
        IPoint Point { get; set; }

        int IdxDMGCField { get; set; }
        int IdxQDMSField { get; set; }
        int IdxZDMSField { get; set; }

        double GetQDGCValue();
        double GetZDGCValue();
        double GetHeightByPoint();
        double GetGroundHeightByPoint();
        double GetDepthByPoint();
    }
}
