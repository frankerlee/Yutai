using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
    public interface IRotateTrackerFeedback : IDisplayFeedback
    {
        double Angle { get; }

        IPoint Origin { get; set; }

        void AddGeometry(IGeometry igeometry_0);

        void ClearGeometry();

        void Start(IPoint ipoint_0);

        double Stop();
    }
}