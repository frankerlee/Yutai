using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
	public interface INewCircularArcFeedback : IDisplayFeedback
	{
		void AddPoint(IPoint ipoint_0);

		void Start(IPoint ipoint_0);

		ICircularArc Stop();
	}
}