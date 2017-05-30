using ESRI.ArcGIS.Geometry;

namespace Yutai.Catalog
{
	public interface IGxPrjFile
	{
		ISpatialReference SpatialReference
		{
			get;
		}
	}
}