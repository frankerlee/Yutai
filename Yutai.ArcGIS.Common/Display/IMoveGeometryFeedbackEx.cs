using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
	public interface IMoveGeometryFeedbackEx : IDisplayFeedback
	{
		void Add(IFeature ifeature_0);

		void Start(IPoint ipoint_0);
	}
}