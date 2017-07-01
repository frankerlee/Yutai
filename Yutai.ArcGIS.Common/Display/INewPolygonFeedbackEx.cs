using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
    public interface INewPolygonFeedbackEx : INewPolygonFeedback, IDisplayFeedback
    {
        bool CanSquareAndFinish { get; }

        void AddPart(IGeometry igeometry_0);

        void ChangeLineType(enumLineType enumLineType_0);

        void CompletePart();

        IPolygon SquareAndFinish(ISpatialReference ispatialReference_0);
    }
}