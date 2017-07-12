using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Helper
{
    public class GeometryConvert
    {
        public static IPoint ToPoint(IMultipoint multipoint)
        {
            IPointCollection pointCollection = multipoint as IPointCollection;
            if (pointCollection != null && pointCollection.PointCount >= 0)
                return pointCollection.Point[0];
            return null;
        }

        public static List<IPolyline> ToPolylineList(IPolyline polyline)
        {
            List<IPolyline> polylines = new List<IPolyline>();
            IZAware pZAware = polyline as IZAware;
            IMAware pMAware = polyline as IMAware;
            if (pZAware == null || pMAware == null)
                return polylines;
            IPointCollection pointCollection = polyline as IPointCollection;
            if (pointCollection == null)
                return polylines;
            for (int i = 1; i < pointCollection.PointCount; i++)
            {
                IPolyline tempPolyline = new PolylineClass
                {
                    FromPoint = new PointClass
                    {
                        X = pointCollection.Point[i - 1].X,
                        Y = pointCollection.Point[i - 1].Y
                    },
                    ToPoint = new PointClass
                    {
                        X = pointCollection.Point[i].X,
                        Y = pointCollection.Point[i].Y
                    }
                };
                IZAware pTempZAware = tempPolyline as IZAware;
                pTempZAware.ZAware = pZAware.ZAware;
                IMAware pTempMAware = tempPolyline as IMAware;
                pTempMAware.MAware = pMAware.MAware;
                polylines.Add(tempPolyline);
            }
            return polylines;
        }

        public static IGeometry GeometryConvertM(IFeature feature, bool hasZ, bool hasM)
        {
            IGeometry geometry = feature.ShapeCopy;
            IZAware zAware = geometry as IZAware;
            if (zAware != null)
                zAware.ZAware = hasZ;
            IMAware mAware = geometry as IMAware;
            if (mAware != null)
                mAware.MAware = hasM;
            return geometry;
        }

        public static void PointAddZ(IFeature feature, double value)
        {
            IPoint point = feature.Shape as IPoint;
            if (point == null)
                return;
            PointAddZ(point, value);
            feature.Shape = point;
            feature.Store();
        }

        public static void PointAddZ(IPoint point, double value)
        {
            IZAware pZAware = point as IZAware;
            if (pZAware == null)
                return;
            pZAware.ZAware = true;
            point.Z = value;
        }

        public static void PointAddM(IFeature feature, double value)
        {
            IPoint point = feature.Shape as IPoint;
            if (point == null)
                return;
            PointAddM(point, value);
            feature.Shape = point;
            feature.Store();
        }

        public static void PointAddM(IPoint point, double value)
        {
            IMAware pMAware = point as IMAware;
            if (pMAware == null)
                return;
            pMAware.MAware = true;
            point.M = value;
        }

        public static void PolylineAddZ(IFeature feature, double sValue, double eValue)
        {
            IPolyline polyline = feature.Shape as IPolyline;
            if (polyline == null)
                return;
            PolylineAddZ(polyline, sValue, eValue);
            feature.Shape = polyline;
            feature.Store();
        }

        public static void PolylineAddZ(IPolyline polyline, double sValue, double eValue)
        {
            IZAware pZAware = polyline as IZAware;
            if (pZAware == null)
                return;
            pZAware.ZAware = true;
            IPoint fromPoint = polyline.FromPoint;
            PointAddZ(fromPoint, sValue);
            polyline.FromPoint = fromPoint;
            IPoint toPoint = polyline.ToPoint;
            PointAddZ(toPoint, eValue);
            polyline.ToPoint = toPoint;
        }

        public static void PolylineAddM(IFeature feature, double sValue, double eValue)
        {
            IPolyline polyline = feature.Shape as IPolyline;
            if (polyline == null)
                return;
            PolylineAddM(polyline, sValue, eValue);
            feature.Shape = polyline;
            feature.Store();
        }

        public static void PolylineAddM(IPolyline polyline, double sValue, double eValue)
        {
            IMAware pMAware = polyline as IMAware;
            if (pMAware == null)
                return;
            pMAware.MAware = true;
            IPoint fromPoint = polyline.FromPoint;
            PointAddM(fromPoint, sValue);
            polyline.FromPoint = fromPoint;
            IPoint toPoint = polyline.ToPoint;
            PointAddM(toPoint, eValue);
            polyline.ToPoint = toPoint;
        }
    }
}
