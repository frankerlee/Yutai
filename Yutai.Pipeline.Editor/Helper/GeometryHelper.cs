using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Helper
{
    public class GeometryHelper
    {
        public static List<IPointCollection> CreateGgbxGeometryCollection(IGeometry geometry, double width, bool hasZ = true, bool hasM = true)
        {
            List<IPointCollection> polylineList = new List<IPointCollection>();
            try
            {
                IPolyline polyline = geometry as IPolyline;
                if (polyline == null)
                    return polylineList;
                IPoint fromPoint = CreatePoint(polyline.FromPoint.X, polyline.FromPoint.Y, 0, 0, hasZ, hasM);
                IPoint toPoint = CreatePoint(polyline.ToPoint.X, polyline.ToPoint.Y, 0, 0, hasZ, hasM);
                List<IPoint> list1 = GetCornerPoints(new PolylineClass
                {
                    FromPoint = fromPoint,
                    ToPoint = toPoint
                }, width);
                List<IPoint> list2 = GetCornerPoints(new PolylineClass
                {
                    FromPoint = toPoint,
                    ToPoint = fromPoint
                }, width);
                polylineList.Add(CreatePointCollection(list1[0], list2[1], hasZ, hasM));
                polylineList.Add(CreatePointCollection(list1[1], list2[0], hasZ, hasM));
                return polylineList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IPoint CreatePoint(double x, double y, double z = 0, double m = 0, bool hasZ = true, bool hasM = true)
        {
            IPoint point = new PointClass
            {
                X = x,
                Y = y
            };
            IZAware pZAware = point as IZAware;
            pZAware.ZAware = hasZ;
            if (hasZ)
                point.Z = z;
            IMAware pMAware = point as IMAware;
            pMAware.MAware = hasM;
            if (hasM)
                point.M = m;
            return point;
        }

        public static IPointCollection CreatePointCollection(IPoint point1, IPoint point2, bool hasZ = true, bool hasM = true)
        {
            IPointCollection pointCollection = new PolylineClass();
            IZAware pZAware = pointCollection as IZAware;
            pZAware.ZAware = hasZ;
            IMAware pMAware = pointCollection as IMAware;
            pMAware.MAware = hasM;
            pointCollection.AddPoint(CreatePoint(point1.X, point1.Y, point1.Z, point1.M, hasZ, hasM));
            pointCollection.AddPoint(CreatePoint(point2.X, point2.Y, point2.Z, point2.M, hasZ, hasM));
            return pointCollection;
        }

        public static IPointCollection CreatePointCollection(List<IPoint> list, bool hasZ = true, bool hasM = true)
        {
            IPointCollection pointCollection = new PolylineClass();
            IZAware pZAware = pointCollection as IZAware;
            pZAware.ZAware = hasZ;
            IMAware pMAware = pointCollection as IMAware;
            pMAware.MAware = hasM;
            for (int i = 0; i < list.Count; i++)
            {
                pointCollection.AddPoint(list[i]);
            }
            return pointCollection;
        }

        public static IPointCollection CreateSmallRoomGeometryCollection(IPoint point, IPolyline polyline, List<double> distanceList, List<double> lengthList, bool hasZ = true, bool hasM = true)
        {
            try
            {
                List<IPoint> list = new List<IPoint>();
                IPoint firstPoint = GetPoint(polyline, point, distanceList[0]);
                list.Add(firstPoint);
                for (int i = 0; i < distanceList.Count; i++)
                {
                    if (i == distanceList.Count - 1)
                        list.Add(CalcurateThirdPoint(point, list[i], distanceList[i], distanceList[0], lengthList[i]));
                    else
                        list.Add(CalcurateThirdPoint(point, list[i], distanceList[i], distanceList[i + 1], lengthList[i]));
                }
                return CreatePointCollection(list, hasZ, hasM);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IPoint CalcurateThirdPoint(IPoint aPoint, IPoint bPoint, double m, double n, double h)
        {
            IPoint dPoint = new PointClass();

            double b = Math.Acos((m * m + n * n - h * h) / (m * n * 2));

            if (aPoint.X <= bPoint.X && aPoint.Y <= bPoint.Y)       // 第一象限
            {
                double a = Math.Atan((bPoint.Y - aPoint.Y) / (bPoint.X - aPoint.X));
                if (b <= a)
                {
                    double v = a - b;
                    dPoint.X = aPoint.X + n * Math.Cos(v);
                    dPoint.Y = aPoint.Y + n * Math.Sin(v);
                    dPoint.Z = 0;
                }
                else
                {
                    double v = b - a;
                    dPoint.X = aPoint.X + n * Math.Cos(v);
                    dPoint.Y = aPoint.Y - n * Math.Sin(v);
                    dPoint.Z = 0;
                }
            }
            else if (aPoint.X <= bPoint.X && aPoint.Y > bPoint.Y)       // 第二象限
            {
                double a = Math.Atan((aPoint.Y - bPoint.Y) / (bPoint.X - aPoint.X));
                if ((a + b) <= Math.PI / 2)
                {
                    double v = a + b;
                    dPoint.X = aPoint.X + n * Math.Cos(v);
                    dPoint.Y = aPoint.Y - n * Math.Sin(v);
                    dPoint.Z = 0;
                }
                else
                {
                    double v = Math.PI - a - b;
                    dPoint.X = aPoint.X - n * Math.Cos(v);
                    dPoint.Y = aPoint.Y - n * Math.Sin(v);
                    dPoint.Z = 0;
                }
            }
            else if (aPoint.X > bPoint.X && aPoint.Y > bPoint.Y)        // 第三象限
            {
                double a = Math.Atan((aPoint.Y - bPoint.Y) / (aPoint.X - bPoint.X));
                if (b <= a)
                {
                    double v = a - b;
                    dPoint.X = aPoint.X - n * Math.Cos(v);
                    dPoint.Y = aPoint.Y - n * Math.Sin(v);
                    dPoint.Z = 0;
                }
                else
                {
                    double v = b - a;
                    dPoint.X = aPoint.X - n * Math.Cos(v);
                    dPoint.Y = aPoint.Y + n * Math.Sin(v);
                    dPoint.Z = 0;
                }
            }
            else if (aPoint.X > bPoint.X && aPoint.Y < bPoint.Y)
            {
                double a = Math.Atan((bPoint.Y - aPoint.Y) / (aPoint.X - bPoint.X));
                if ((a + b) <= Math.PI)
                {
                    double v = a + b;
                    dPoint.X = aPoint.X - n * Math.Cos(v);
                    dPoint.Y = aPoint.Y + n * Math.Sin(v);
                    dPoint.Z = 0;
                }
                else
                {
                    double v = Math.PI - a - b;
                    dPoint.X = aPoint.X + n * Math.Cos(v);
                    dPoint.Y = aPoint.Y + n * Math.Sin(v);
                    dPoint.Z = 0;
                }
            }

            return CreatePoint(dPoint.X, dPoint.Y, dPoint.Z);
        }

        private static IPoint GetPoint(IPolyline polyline, IPoint point, double distance)
        {
            if (IsInLine(point, polyline))  // 测量点在管线上
            {
                return GetPointByInLine(point, polyline, distance);
            }
            else                         // 测量点在管线外
            {
                return GetPointByOutLine(point, polyline, distance);
            }
        }

        private static IPoint GetPointByOutLine(IPoint point, IPolyline polyline, double distance)
        {
            IPoint projPoint = GetProjectionPoint(point, polyline);
            double a = GetMinDistance(polyline.FromPoint, polyline.ToPoint, point);
            double b = Math.Sqrt(distance * distance - a * a);

            IPolyline tempPolyline = new PolylineClass
            {
                FromPoint = projPoint,
                ToPoint = polyline.FromPoint
            };

            return GetPointInLineByLength(tempPolyline, b); ;
        }

        public static IPoint GetProjectionPoint(IPoint point, IPolyline polyline)
        {
            double distanceFrom = GetDistance(point, polyline.FromPoint);
            double distanceTo = GetDistance(point, polyline.ToPoint);

            double h = (distanceFrom * distanceFrom - distanceTo * distanceTo + polyline.Length * polyline.Length) /
                       (2 * polyline.Length);
            IPoint projPoint = GetPointInLineByLength(polyline, h);
            return projPoint;
        }

        private static IPoint GetPointByInLine(IPoint point, IPolyline polyline, double distance)
        {
            IPoint tempPoint = new PointClass();
            IPolyline tempPolyline = CreatePointCollection(point, polyline.FromPoint) as IPolyline;
            tempPoint.X = distance * ((tempPolyline.ToPoint.X - tempPolyline.FromPoint.X) / tempPolyline.Length) +
                        tempPolyline.FromPoint.X;
            tempPoint.Y = distance * ((tempPolyline.ToPoint.Y - tempPolyline.FromPoint.Y) / tempPolyline.Length) +
                          tempPolyline.FromPoint.Y;
            tempPoint.Z = 0;
            return tempPoint;
        }

        public static IPoint GetPointInLineByLength(IPolyline polyline, double length)
        {
            IPoint point = new PointClass();

            point.X = length * ((polyline.ToPoint.X - polyline.FromPoint.X) / polyline.Length) + polyline.FromPoint.X;
            point.Y = length * ((polyline.ToPoint.Y - polyline.FromPoint.Y) / polyline.Length) + polyline.FromPoint.Y;
            point.Z = 0;

            return point;
        }

        public static List<IPoint> GetCornerPoints(IPolyline polyline, double length)
        {
            List<IPoint> list = new List<IPoint>();
            IPoint point = new PointClass();

            point.X = length * ((polyline.ToPoint.X - polyline.FromPoint.X) / polyline.Length) + polyline.FromPoint.X;
            point.Y = length * ((polyline.ToPoint.Y - polyline.FromPoint.Y) / polyline.Length) + polyline.FromPoint.Y;
            point.Z = 0;

            IPoint pointsl = new PointClass
            {
                X = polyline.FromPoint.X - (point.Y - polyline.FromPoint.Y),
                Y = polyline.FromPoint.Y + (point.X - polyline.FromPoint.X),
                Z = 0
            };
            list.Add(pointsl);
            IPoint pointsr = new PointClass
            {
                X = polyline.FromPoint.X + (point.Y - polyline.FromPoint.Y),
                Y = polyline.FromPoint.Y - (point.X - polyline.FromPoint.X),
                Z = 0
            };
            list.Add(pointsr);
            return list;
        }

        public static bool IsInLine(IPoint point, IPolyline polyline)
        {
            double dis = GetMinDistance(polyline.FromPoint, polyline.ToPoint, point);
            if (dis < 0.0001)
                return true;
            return false;
        }

        public static bool IsFromOrToPoint(IPoint point, IPolyline polyline)
        {
            double dis = GetDistance(point, polyline.FromPoint);
            if (dis < 0.00001)
                return true;
            dis = GetDistance(point, polyline.ToPoint);
            if (dis < 0.00001)
                return true;
            return false;
        }

        public static int IsFromPoint(IPoint point, IPolyline polyline)
        {
            double dis = GetDistance(point, polyline.FromPoint);
            if (dis < 0.001)
                return 1;
            dis = GetDistance(point, polyline.ToPoint);
            if (dis < 0.001)
                return -1;
            return 0;
        }

        public static bool IsFromPoint(IPolyline polyline, IPoint point)
        {
            IPoint outPoint = new PointClass();
            double distanceAlongCurve = 0.0, distanceFromCurve = 0.0;
            bool bRightSide = false;
            polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, point, false, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
            if (distanceFromCurve > 0.001)
                return false;
            if (distanceAlongCurve > 0.001)
                return false;
            return true;
        }

        public static IPoint GetNearPoint(IPolyline polyline, IPoint point)
        {
            IPoint outPoint = new PointClass();
            double distanceAlongCurve = 0.0, distanceFromCurve = 0.0;
            bool bRightSide = false;
            polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, point, false, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
            return outPoint;
        }

        public static double GetMinDistance(IPoint pt1, IPoint pt2, IPoint pt3)
        {
            double dis = 0;
            if (Math.Abs(pt1.X - pt2.X) <= 0)
            {
                dis = Math.Abs(pt3.X - pt1.X);
                return dis;
            }
            double lineK = (pt2.Y - pt1.Y) / (pt2.X - pt1.X);
            double lineC = (pt2.X * pt1.Y - pt1.X * pt2.Y) / (pt2.X - pt1.X);
            dis = Math.Abs(lineK * pt3.X - pt3.Y + lineC) / (Math.Sqrt(lineK * lineK + 1));
            return dis;
        }

        public static double GetDistance(IPoint point1, IPoint point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        public static IPoint GetIntersectPoint(IPolyline polyline1, IPolyline polyline2)
        {
            ITopologicalOperator topo = polyline1 as ITopologicalOperator;
            IGeometry pGeometry = topo.Intersect(polyline2, esriGeometryDimension.esriGeometry0Dimension);
            if (!pGeometry.IsEmpty)
            {
                IPointCollection pointCollection = pGeometry as IPointCollection;
                return pointCollection.Point[0];
            }
            return null;
        }

        public static IPoint GetAnotherPoint(IPolyline polyline, IPoint point)
        {
            IPoint outPoint = new PointClass();
            double distAlongCurveFrom = 0;
            double disFromCurve = 0;
            bool isRightSide = false;
            polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, point, false, outPoint, ref distAlongCurveFrom, ref disFromCurve, ref isRightSide);
            if (distAlongCurveFrom < 0.0001)
                return polyline.ToPoint;
            else
                return polyline.FromPoint;
        }

        public static IPoint GetVerticalPoint(IPoint firstPoint, IPoint secondPoint, IPoint otherPoint)
        {
            IPolyline polyline = new PolylineClass
            {
                FromPoint = firstPoint,
                ToPoint = secondPoint
            };
            IPoint outPoint = new PointClass();
            double distAlongCurveFrom = 0;
            double disFromCurve = 0;
            bool isRightSide = false;
            polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, otherPoint, false, outPoint, ref distAlongCurveFrom, ref disFromCurve, ref isRightSide);
            return outPoint;
        }

        public static IPoint GetVerticalPoint(IFeature polylineFeature, IFeature pointFeature)
        {
            IPolyline polyline = polylineFeature.Shape as IPolyline;
            IPoint point = pointFeature.Shape as IPoint;
            if (polyline == null || point == null)
                return null;
            return GetVerticalPoint(polyline.FromPoint, polyline.ToPoint, point);
        }

        public static double GetDistance(IFeature polylineFeature, IFeature pointFeature)
        {
            IPolyline polyline = polylineFeature.Shape as IPolyline;
            IPoint point = pointFeature.Shape as IPoint;
            if (polyline == null || point == null)
                return -1;
            return GetDistance(polyline.FromPoint, polyline.ToPoint, point);
        }

        public static double GetDistance(IPoint firstPoint, IPoint secondPoint, IPoint otherPoint)
        {
            IPolyline polyline = new PolylineClass
            {
                FromPoint = firstPoint,
                ToPoint = secondPoint
            };
            IPoint outPoint = new PointClass();
            double distAlongCurveFrom = 0;
            double disFromCurve = 0;
            bool isRightSide = false;
            polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, otherPoint, false, outPoint, ref distAlongCurveFrom, ref disFromCurve, ref isRightSide);
            return disFromCurve;
        }

        public static double GetAngleDegreeByPolyline(IPolyline polyline)
        {
            ILine pLine = new LineClass();
            pLine.PutCoords(polyline.FromPoint, polyline.ToPoint);
            return (180 * pLine.Angle) / Math.PI;
        }

        public static double GetAngleRadianByPolyline(IPolyline polyline)
        {
            ILine pLine = new LineClass();
            pLine.PutCoords(polyline.FromPoint, polyline.ToPoint);
            return pLine.Angle;
        }

        public static double GetZValue(IPoint fromPoint, double fromZ, IPoint toPoint, double toZ, IPoint centerPoint)
        {
            double df = GetDistance(toPoint, centerPoint);
            double bc = fromZ;
            double bd = GetDistance(fromPoint, toPoint);
            double de = toZ;

            return (df * (bc - de)) / bd + de;
        }
    }
}
