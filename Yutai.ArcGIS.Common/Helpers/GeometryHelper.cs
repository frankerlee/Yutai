using System;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class GeometryHelper
    {
        public GeometryHelper()
        {
        }

        public static IEnvelope GetIdentifyEnvelope(IEnvelope inGeom, double dist)
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

        public static esriGeometryType ConvertFromString(string geometryTypeName)
        {
            string typeName = geometryTypeName.Trim().ToUpper();
           if(typeName.Equals("POINT")) return esriGeometryType.esriGeometryPoint;
            if (typeName.Equals("PATH")) return esriGeometryType.esriGeometryPath;
            if (typeName.Equals("POLYLINE")) return esriGeometryType.esriGeometryPolyline;
            if (typeName.Equals("POLYGON")) return esriGeometryType.esriGeometryPolygon;
            if (typeName.Equals("MULTIPOINT")) return esriGeometryType.esriGeometryMultipoint;
            if (typeName.Equals("MULTIPATCH")) return esriGeometryType.esriGeometryMultiPatch;
            if (typeName.Equals("ANY")) return esriGeometryType.esriGeometryAny;
            if (typeName.Equals("BAG")) return esriGeometryType.esriGeometryBag;
            if (typeName.Equals("BEZIER3CURVE")) return esriGeometryType.esriGeometryBezier3Curve;
            if (typeName.Equals("CIRCULARARC")) return esriGeometryType.esriGeometryCircularArc;
            if (typeName.Equals("ELLIPTICARC")) return esriGeometryType.esriGeometryEllipticArc;
            if (typeName.Equals("ENVELOPE")) return esriGeometryType.esriGeometryEnvelope;
            if (typeName.Equals("LINE")) return esriGeometryType.esriGeometryLine;
            if (typeName.Equals("RAY")) return esriGeometryType.esriGeometryRay;
            if (typeName.Equals("NULL")) return esriGeometryType.esriGeometryNull;
            if (typeName.Equals("RING")) return esriGeometryType.esriGeometryRing;
            if (typeName.Equals("SPHERE")) return esriGeometryType.esriGeometrySphere;
            if (typeName.Equals("TRIANGLEFAN")) return esriGeometryType.esriGeometryTriangleFan;
            if (typeName.Equals("TRIANGLESTRIP")) return esriGeometryType.esriGeometryTriangleStrip;
            if (typeName.Equals("TRIANGLES")) return esriGeometryType.esriGeometryTriangles;
            return esriGeometryType.esriGeometryPoint;
        }

        public static string ConvertToString(esriGeometryType geometryType)
        {
           
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryNull:
                    return "Null";
                    break;
                case esriGeometryType.esriGeometryPoint:
                    return "Point";
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    return "Multipoint";
                    break;
                case esriGeometryType.esriGeometryLine:
                    return "Line";
                    break;
                case esriGeometryType.esriGeometryCircularArc:
                    return "CircularArc";
                    break;
                case esriGeometryType.esriGeometryEllipticArc:
                    return "EllipticArc";
                    break;
                case esriGeometryType.esriGeometryBezier3Curve:
                    return "Bezier3Curve";
                    break;
                case esriGeometryType.esriGeometryPath:
                    return "Path";
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    return "Polyline";
                    break;
                case esriGeometryType.esriGeometryRing:
                    return "Ring";
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    return "Polygon";
                    break;
                case esriGeometryType.esriGeometryEnvelope:
                    return "Envelope";
                    break;
                case esriGeometryType.esriGeometryAny:
                    return "Any";
                    break;
                case esriGeometryType.esriGeometryBag:
                    return "Bag";
                    break;
                case esriGeometryType.esriGeometryMultiPatch:
                    return "MultiPatch";
                    break;
                case esriGeometryType.esriGeometryTriangleStrip:
                    return "TriangleStrip";
                    break;
                case esriGeometryType.esriGeometryTriangleFan:
                    return "TriangleFan";
                    break;
                case esriGeometryType.esriGeometryRay:
                    return "Ray";
                    break;
                case esriGeometryType.esriGeometrySphere:
                    return "Sphere";
                    break;
                case esriGeometryType.esriGeometryTriangles:
                    return "Triangles";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(geometryType), geometryType, null);
            }
       
        }
    }
}