using System;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class GeometryHelper
    {
        public GeometryHelper()
        {
        }

        public static IEnvelope GetIdentifyEnvelope(IEnvelope inGeom, double dist )
        {
            if (inGeom.Width == 0 || inGeom.Height == 0)
            {
                inGeom.XMin = inGeom.XMin - dist;
                inGeom.YMin = inGeom.YMin - dist;
                inGeom.XMax = inGeom.XMax + dist;
                inGeom.YMax = inGeom.YMax + dist;
            }
            return inGeom;
        }
        public static void QueryGeometryLocation(IGeometry feaGeom, out double px, out double py)
        {
            px = double.NaN;
            py = double.NaN;
            try
            {
                if (!feaGeom.IsEmpty)
                {
                    IPoint fromPoint = null;
                    if (feaGeom is IPoint)
                    {
                        fromPoint = feaGeom as IPoint;
                    }
                    else if (feaGeom is IPolyline)
                    {
                        fromPoint = (feaGeom as IPolyline).FromPoint;
                    }
                    else if (feaGeom is IPolygon)
                    {
                        fromPoint = (feaGeom as IArea).Centroid;
                    }
                    if (fromPoint != null)
                    {
                        px = fromPoint.X;
                        py = fromPoint.Y;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public static string ShapeTypeName(esriGeometryType paramGT)
        {
            return "";
        }
    }
}