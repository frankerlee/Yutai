using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
    public interface INewPolylineFeedback : INewLineFeedback, IDisplayFeedback
    {
        bool CanSquareAndFinish { get; }

        void AddPart(IGeometry igeometry_0);

        void ChangeLineType(enumLineType enumLineType_0);

        void Close();

        void CompletePart();

        IPoint ReverseOrientation();

        IPolyline SquareAndFinish(ISpatialReference ispatialReference_0);
    }
}