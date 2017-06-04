using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
	public interface IScaleGeometryFeedback : IDisplayFeedback
	{
		IPoint AnchorPoint
		{
			set;
		}

		double Scale
		{
			get;
		}

		void Start(IPoint ipoint_0, IGeometry igeometry_0);

		IGeometry Stop();
	}
}