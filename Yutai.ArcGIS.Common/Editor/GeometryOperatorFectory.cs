using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor
{
	public class GeometryOperatorFectory
	{
		public GeometryOperatorFectory()
		{
		}

		public static GeometryOperator CreateGeometryOperator(IGeometry igeometry_0)
		{
			GeometryOperator pointOperator;
			switch (igeometry_0.GeometryType)
			{
				case esriGeometryType.esriGeometryPoint:
				case esriGeometryType.esriGeometryMultipoint:
				{
					pointOperator = new PointOperator(igeometry_0);
					break;
				}
				case esriGeometryType.esriGeometryPolyline:
				{
					pointOperator = new PolylineOperator(igeometry_0);
					break;
				}
				case esriGeometryType.esriGeometryPolygon:
				{
					pointOperator = new PolygonOperator(igeometry_0);
					break;
				}
				default:
				{
					pointOperator = null;
					break;
				}
			}
			return pointOperator;
		}
	}
}