using System;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Helpers
{
    /// <summary>
    /// ArcGIS的几何形状的公用工具类，主要为系统提供几何形状的合法性判断，高级几何形状判断等方法。
    /// </summary>
    public class GeometryUtility
    {
        /// <summary>
        /// 判断是否合法的几何形状
        /// 只是判断接口是否为空和几何形状是否为空
        /// geometry == null 和 geometry.IsEmpty
        /// </summary>
        /// <param name="geometry">ESRI几何形状接</param>
        /// <returns>是否</returns>
        public static bool IsValidGeometry(IGeometry geometry)
        {
            return (geometry != null) && !geometry.IsEmpty;
        }

        /// <summary>
        /// 判断几何对象是否高级几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>是否</returns>
        public static bool IsHighLevelGeometry(IGeometry geometry)
        {
            return IsValidGeometry(geometry) && (geometry is IRelationalOperator);
        }

        /// <summary>
        /// 判断是否合法的橡皮筋几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>是否</returns>
        public static bool IsValidRubberGeometry(IGeometry geometry)
        {
            if (IsValidGeometry(geometry) && IsValidGeometry(geometry.Envelope))
            {
                return !double.IsNaN(geometry.Envelope.Height) &&
                       !double.IsNaN(geometry.Envelope.Width) &&
                       ((geometry.Envelope.Height > 0) || (geometry.Envelope.Width > 0));
            }
            else
                return false;
        }

        /// <summary>
        /// 判断几何形状是否可成为合法元素的几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>是否</returns>
        public static bool IsValidElementGeometry(IGeometry geometry)
        {
            if (IsValidGeometry(geometry))
            {
                if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                    return true;
                else
                    return IsValidRubberGeometry(geometry);
            }
            else
                return false;

        }

        /// <summary>
        /// 通过坐标创建点
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>ESRI点几何形状接口</returns>
        public static IPoint CreatePointByCoord(double x, double y)
        {
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords(x, y);
            return point;
        }

        /// <summary>
        /// 通过坐标创建包络
        /// </summary>
        /// <param name="xMin">最小x坐标</param>
        /// <param name="yMin">最小y坐标</param>
        /// <param name="xMax">最大x坐标</param>
        /// <param name="yMax">最大y坐标</param>
        /// <returns>ESRI包络几何形状接口</returns>
        public static IEnvelope CreateEnvelopeByCoord(double xMin, double yMin, double xMax, double yMax)
        {
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords(xMin, yMin, xMax, yMax);
            return envelope;
        }

        /// <summary>
        /// 通过起点和终点创建多义线
        /// </summary>
        /// <param name="fromPoint">起点的ESRI点几何形状接口</param>
        /// <param name="toPoint">终点的ESRI点几何形状接口</param>
        /// <returns>ESRI多义线几何形状接口</returns>
        public static IPolyline CreatePolylineByTwoPoints(IPoint fromPoint, IPoint toPoint)
        {
            IPolyline polyline = new Polyline() as IPolyline;
            polyline.FromPoint = fromPoint;
            polyline.ToPoint = toPoint;
            return polyline;
        }

        /// <summary>
        /// 通过起点和终点创建直线
        /// </summary>
        /// <param name="fromPoint">起点的ESRI点几何形状接口</param>
        /// <param name="toPoint">终点的ESRI点几何形状接口</param>
        /// <returns>ESRI直线几何形状接口</returns>
        public static ILine CreateLineByTwoPoints(IPoint fromPoint, IPoint toPoint)
        {
            ILine line = new Line();
            line.FromPoint = fromPoint;
            line.ToPoint = toPoint;
            return line;
        }

        /// <summary>
        /// 通过开始点和结束点创建圆弧片段，圆心为与开始点、结束点构成等边三角形的点
        /// </summary>
        /// <param name="fromPoint">开始点</param>
        /// <param name="toPoint">结束点</param>
        /// <returns>圆弧片段</returns>
        public static ISegment CreateCircularArcByTwoPoints(IPoint fromPoint, IPoint toPoint)
        {
            ILine line = CreateLineByTwoPoints(fromPoint, toPoint);
            ILine normal = new Line();
            line.QueryNormal(esriSegmentExtension.esriNoExtension, 0.5, true, (double)(line.Length / 3.0), normal);
            IConstructCircularArc Constructor = new CircularArc() as IConstructCircularArc;
            Constructor.ConstructThreePoints(fromPoint, normal.ToPoint, toPoint, true);
            return (ISegment)Constructor;
        }

        /// <summary>
        /// 通过开始点和结束点创建半圆弧片段
        /// </summary>
        /// <param name="fromPoint">开始点</param>
        /// <param name="toPoint">结束点</param>
        /// <returns>半圆弧片段</returns>
        public static ISegment CreateHalfCircularArcByTwoPoints(IPoint fromPoint, IPoint toPoint)
        {
            ILine line = CreateLineByTwoPoints(fromPoint, toPoint);
            ILine normal = new Line();
            line.QueryNormal(esriSegmentExtension.esriNoExtension, 0.5, true, (double)(line.Length / 2.0), normal);
            IConstructCircularArc Constructor = new CircularArc() as IConstructCircularArc;
            Constructor.ConstructThreePoints(fromPoint, normal.ToPoint, toPoint, true);
            return (ISegment)Constructor;
        }

        /// <summary>
        /// 通过开始点和结束点创建贝塞尔曲线片段
        /// </summary>
        /// <param name="fromPoint">开始点</param>
        /// <param name="toPoint">结束点</param>
        /// <returns>贝塞尔曲线片段</returns>
        public static ISegment CreateBezierCurveByTwoPoints(IPoint fromPoint, IPoint toPoint)
        {
            IBezierCurveGEN CurveGEN = new BezierCurve();
            ILine line = CreateLineByTwoPoints(fromPoint, toPoint);
            ILine normal = new Line();
            line.QueryNormal(esriSegmentExtension.esriNoExtension, 0.5, true, (double)(line.Length / 2.0), normal);
            IPoint fromTangent = (IPoint)normal.ToPoint;
            line = CreateLineByTwoPoints(toPoint, fromPoint);
            line.QueryNormal(esriSegmentExtension.esriNoExtension, 0.5, true, (double)(line.Length / 2.0), normal);
            IPoint toTangent = (IPoint)normal.ToPoint;
            IPoint[] points = { fromPoint, fromTangent, toTangent, toPoint };
            CurveGEN.PutCoords(ref points);
            return (ISegment)CurveGEN;
        }

        /// <summary>
        /// 获取包络的水平平分线
        /// </summary>
        /// <param name="envelope">ESRI包络几何形状接口</param>
        /// <returns>ESRI多义线几何形状接口</returns>
        public static IPolyline GetEnvelopeHorizontalBisector(IEnvelope envelope)
        {
            IPolyline polyline = null;
            if (envelope != null)
            {
                double dx0, dy0, dx1, dy1;
                envelope.QueryCoords(out dx0, out dy0, out dx1, out dy1);
                polyline = CreatePolylineByTwoPoints(CreatePointByCoord(dx0, (double)(dy0 + dy1) / 2.0),
                    CreatePointByCoord(dx1, (double)(dy0 + dy1) / 2.0));
            }
            return polyline;
        }

        /// <summary>
        /// 获取包络的垂直平分线
        /// </summary>
        /// <param name="envelope">ESRI包络几何形状接口</param>
        /// <returns>ESRI多义线几何形状接口</returns>
        public static IPolyline GetEnvelopeVerticalBisector(IEnvelope envelope)
        {
            IPolyline polyline = null;
            if (envelope != null)
            {
                double dx0, dy0, dx1, dy1;
                envelope.QueryCoords(out dx0, out dy0, out dx1, out dy1);
                polyline = CreatePolylineByTwoPoints(CreatePointByCoord((double)(dx0 + dx1) / 2.0, dy1),
                    CreatePointByCoord((double)(dx0 + dx1) / 2.0, dy0));
            }
            return polyline;
        }

        /// <summary>
        /// 转换直线段为高级几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形状</returns>
        public static IGeometry ConvertLineToHigh(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is ILine) && (geometry is ISegment))
            {
                ISegmentCollection segmentCollecttion = new Polyline() as ISegmentCollection;
                object Missing = Type.Missing;
                segmentCollecttion.AddSegment((ISegment)geometry, ref Missing, ref Missing);
                return (IGeometry)segmentCollecttion;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换曲线段为高级几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形状</returns>
        public static IGeometry ConvertCurveToHigh(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is ICurve) && (geometry is ISegment))
            {
                ISegmentCollection segmentCollecttion = new Polyline() as ISegmentCollection;
                object Missing = Type.Missing;
                segmentCollecttion.AddSegment((ISegment)geometry, ref Missing, ref Missing);
                return (IGeometry)segmentCollecttion;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换圆弧线为高级几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形状</returns>
        public static IGeometry ConvertCircularArcToHigh(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is ICircularArc))
            {
                ISegmentCollection segmentCollecttion = new Polyline() as ISegmentCollection;
                object Missing = Type.Missing;
                segmentCollecttion.AddSegment((ISegment)geometry, ref Missing, ref Missing);
                return (IGeometry)segmentCollecttion;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换圆弧线为面几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>面几何形状的ESRI几何形状接口</returns>
        public static IGeometry ConvertCircularArcToPolygon(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is ICircularArc))
            {
                ISegmentCollection segmentCollecttion = new Polygon() as ISegmentCollection;
                segmentCollecttion.SetCircle((geometry as ICircularArc).CenterPoint, (geometry as ICircularArc).Radius);
                return (IGeometry)segmentCollecttion;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换包络为椭圆线
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口的椭圆线</returns>
        public static IGeometry ConvertEnvelopeToEllipticArc(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is IEnvelope))
            {
                IEnvelope envelope = (IEnvelope)geometry;
                IPoint point = CreatePointByCoord((envelope.XMin + envelope.XMax) / 2, envelope.YMin);
                IEllipticArc ellipticArc = new EllipticArc();
                (ellipticArc as IConstructEllipticArc).ConstructTwoPointsEnvelope(point, point,
                    envelope, esriArcOrientation.esriArcClockwise);
                return ellipticArc;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换椭圆线为高级椭圆几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形状</returns>
        public static IGeometry ConvertEllipticArcToHigh(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is ISegment))
            {
                ISegmentCollection segmentCollecttion = new Polyline() as ISegmentCollection;
                object Missing = Type.Missing;
                segmentCollecttion.AddSegment((ISegment)geometry, ref Missing, ref Missing);
                return (IGeometry)segmentCollecttion;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换椭圆线为椭圆面几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>椭圆面几何形状的ESRI几何形状接口</returns>
        public static IGeometry ConvertEllipticArcToPolygon(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is ISegment))
            {
                ISegmentCollection segmentCollecttion = new Polygon() as ISegmentCollection;
                object Missing = Type.Missing;
                segmentCollecttion.AddSegment((ISegment)geometry, ref Missing, ref Missing);
                return (IGeometry)segmentCollecttion;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换多线段为高级几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形</returns>
        public static IGeometry ConvertPathToHigh(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is IPath) && (geometry is ISegmentCollection))
            {
                IGeometry polyline = new Polyline() as IGeometry;
                (polyline as ISegmentCollection).AddSegmentCollection((ISegmentCollection)geometry);
                return polyline;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换闭合线段为高级几何形状（面）
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形</returns>
        public static IGeometry ConvertRingToHigh(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is IRing) && (geometry is ISegmentCollection))
            {
                IGeometry polygon = new Polygon() as IGeometry;
                (polygon as ISegmentCollection).AddSegmentCollection((ISegmentCollection)geometry);
                return polygon;
            }
            else
                return null;
        }

        /// <summary>
        /// 转换几何形状为高级几何形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状接口高级几何形</returns>
        public static IGeometry ConvertGeometryToHigh(IGeometry geometry)
        {
            if (!IsHighLevelGeometry(geometry))
            {
                switch (geometry.GeometryType)
                {
                    case esriGeometryType.esriGeometryLine:
                        return ConvertLineToHigh(geometry);
                    case esriGeometryType.esriGeometryCircularArc:
                        return ConvertCircularArcToHigh(geometry);
                    case esriGeometryType.esriGeometryEllipticArc:
                        return ConvertEllipticArcToHigh(geometry);
                    case esriGeometryType.esriGeometryBezier3Curve:
                        return ConvertCurveToHigh(geometry);
                    case esriGeometryType.esriGeometryPath:
                        return ConvertPathToHigh(geometry);
                    case esriGeometryType.esriGeometryRing:
                        return ConvertRingToHigh(geometry);
                    default:
                        return geometry;
                }
            }
            else
                return geometry;
        }

        /// <summary>
        /// 转化包络为几何面形状
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>ESRI几何形状（面）接口</returns>
        public static IGeometry ConvertEnvelopeToPolygon(IGeometry geometry)
        {
            if ((IsValidGeometry(geometry)) && (geometry is IEnvelope))
            {
                IEnvelope envelope = (IEnvelope)geometry;
                IGeometry polygon = new Polygon() as IGeometry;
                IPointCollection pointCollection = (IPointCollection)polygon;
                IPoint point = envelope.LowerLeft;
                pointCollection.AddPoints(1, ref point);
                point = envelope.LowerRight;
                pointCollection.AddPoints(1, ref point);
                point = envelope.UpperRight;
                pointCollection.AddPoints(1, ref point);
                point = envelope.UpperLeft;
                pointCollection.AddPoints(1, ref point);
                ITopologicalOperator topoOp = (ITopologicalOperator)polygon;
                topoOp.Simplify();
                return polygon;
            }
            else
                return null;
        }

        /// <summary>
        /// 判断两个包络是否系统兼容一致
        /// 主要用于地图缩放到指定的包络时不要频繁刷新
        /// </summary>
        /// <param name="envelope1">ESRI包络几何形状接口1</param>
        /// <param name="envelope2">ESRI包络几何形状接口2</param>
        /// <returns>是否</returns>
        public static bool IsCompatibleEnvlope(IEnvelope envelope1, IEnvelope envelope2)
        {
            bool isCompatible = false;
            if (IsHighLevelGeometry(envelope1) && IsHighLevelGeometry(envelope2))
            {
                isCompatible = (envelope1 as IRelationalOperator).Disjoint(envelope2);
                if (!isCompatible)
                {
                    if ((envelope1 as IArea).Area >= (envelope2 as IArea).Area)
                        isCompatible = (envelope1 as IRelationalOperator).Contains(envelope2);
                    else
                        isCompatible = (envelope2 as IRelationalOperator).Contains(envelope1);
                    if (isCompatible)
                    {
                        object lpEnvelope1 = new Envelope();
                        SystemUtility.ObjectCopy(envelope1, ref lpEnvelope1);
                        object lpEnvelope2 = new Envelope();
                        SystemUtility.ObjectCopy(envelope2, ref lpEnvelope2);
                        if ((envelope1 as IArea).Area >= (envelope2 as IArea).Area)
                        {
                            (lpEnvelope2 as IEnvelope).Expand(1.001, 1.001, true);
                            isCompatible = (lpEnvelope1 as IRelationalOperator).Contains((IEnvelope)lpEnvelope2);
                            return !isCompatible;
                        }
                        else
                        {
                            (lpEnvelope1 as IEnvelope).Expand(1.001, 1.001, true);
                            isCompatible = (lpEnvelope2 as IRelationalOperator).Contains((IEnvelope)lpEnvelope1);
                            return !isCompatible;
                        }
                    }
                }
            }
            return isCompatible;
        }

        /// <summary>
        /// 屏幕上绘画几何形状
        /// </summary>
        /// <param name="screenDisplay">ESRI屏幕显示接口</param>
        /// <param name="geomType">几何形状类型的枚举类</param>
        /// <returns>ESRI几何形状接口</returns>
        public static IGeometry ScreenTrackGeometry(IScreenDisplay screenDisplay, DsGeometryType geomType)
        {
            IGeometry trackGeom = null;
            if (screenDisplay != null)
            {
                IRubberBand rubberBand = null;
                switch (geomType)
                {
                    case DsGeometryType.dsGTPoint:
                        rubberBand = new RubberPoint();
                        break;
                    case DsGeometryType.dsGTLine:
                        rubberBand = new RubberLine();
                        break;
                    case DsGeometryType.dsGTPolyline:
                        rubberBand = new RubberLine();
                        break;
                    case DsGeometryType.dsGTRectangle:
                        rubberBand = new RubberEnvelope();
                        break;
                    case DsGeometryType.dsGTCircle:
                        rubberBand = new RubberCircle();
                        break;
                    case DsGeometryType.dsGTEllipse:
                        rubberBand = new RubberEnvelope();
                        break;
                    case DsGeometryType.dsGTPolygon:
                        rubberBand = new RubberPolygon();
                        break;
                    case DsGeometryType.dsGTRectangularPolygon:
                        rubberBand = new RubberRectangularPolygon();
                        break;
                    default:
                        break;
                }
                if (rubberBand != null)
                    trackGeom = rubberBand.TrackNew(screenDisplay, null);
                if (geomType == DsGeometryType.dsGTEllipse)
                    trackGeom = ConvertEnvelopeToEllipticArc(trackGeom);
                else if ((geomType == DsGeometryType.dsGTLine) ||
                         (geomType == DsGeometryType.dsGTPolyline) ||
                         (geomType == DsGeometryType.dsGTPolygon))
                {
                    if (trackGeom is ITopologicalOperator)
                        (trackGeom as ITopologicalOperator).Simplify();
                }
            }
            return trackGeom;
        }

        /// <summary>
        /// 获取几何形状的中心（重力）点
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <returns>中心（重力）点的ESRI几何形状接口</returns>
        public static IPoint GeometryCentroid(IGeometry geometry)
        {
            IPoint centroid = null;
            if (IsValidGeometry(geometry))
            {
                if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                    centroid = (IPoint)geometry;
                else
                {
                    if (geometry.Envelope is IArea)
                        centroid = (geometry.Envelope as IArea).Centroid;
                }
            }
            return centroid;
        }

        /// <summary>
        /// 几何形状点击查询测试
        /// </summary>
        /// <param name="geometry">ESRI几何形状接口</param>
        /// <param name="queryPoint">查询点的ESRI几何形状接口</param>
        /// <param name="searchRadius">搜索半径</param>
        /// <param name="geometryPart">搜索命中的几何形状部位</param>
        /// <param name="hitPoint">搜索命中点的ESRI点接口</param>
        /// <param name="hitDistance">搜索命中的距离</param>
        /// <param name="hitPartIndex">搜索命中的几何形状部位索引</param>
        /// <param name="hitSegmentIndex">搜索命中的几何形状部位片段索引</param>
        /// <param name="bRightSide">是否命中几何形状的右方</param>
        /// <param name="hit">是否命中</param>
        public static void GeometryHitTest(IGeometry geometry, IPoint queryPoint, double searchRadius,
            out esriGeometryHitPartType geometryPart, out IPoint hitPoint, out double hitDistance,
            out int hitPartIndex, out int hitSegmentIndex, out bool bRightSide, out bool hit)
        {
            geometryPart = esriGeometryHitPartType.esriGeometryPartNone;
            hitPoint = new ESRI.ArcGIS.Geometry.Point();
            hitDistance = -1;
            hitPartIndex = -1;
            hitSegmentIndex = -1;
            bRightSide = false;
            hit = false;
            if (IsValidGeometry(geometry) && (geometry is IHitTest))
            {
                IHitTest hitTest = (IHitTest)geometry;
                geometryPart = esriGeometryHitPartType.esriGeometryPartVertex;
                hit = hitTest.HitTest(queryPoint, searchRadius, geometryPart, hitPoint,
                    ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref bRightSide);
                if (!hit)
                {
                    geometryPart = esriGeometryHitPartType.esriGeometryPartBoundary;
                    hit = hitTest.HitTest(queryPoint, searchRadius, geometryPart, hitPoint,
                        ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref bRightSide);

                }
                if (!hit)
                {
                    geometryPart = esriGeometryHitPartType.esriGeometryPartMidpoint;
                    hit = hitTest.HitTest(queryPoint, searchRadius, geometryPart, hitPoint,
                        ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref bRightSide);

                }
                if (!hit)
                {
                    geometryPart = esriGeometryHitPartType.esriGeometryPartEndpoint;
                    hit = hitTest.HitTest(queryPoint, searchRadius, geometryPart, hitPoint,
                        ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref bRightSide);

                }
                if (!hit)
                {
                    geometryPart = esriGeometryHitPartType.esriGeometryPartCentroid;
                    hit = hitTest.HitTest(queryPoint, searchRadius, geometryPart, hitPoint,
                        ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref bRightSide);

                }
                if (!hit)
                {
                    geometryPart = esriGeometryHitPartType.esriGeometryPartSurface;
                    hit = hitTest.HitTest(queryPoint, searchRadius, geometryPart, hitPoint,
                        ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref bRightSide);

                }
            }
        }

        /// <summary>
        /// 判断多义线是否是伯泽尔曲线
        /// </summary>
        /// <param name="polyline">ESRI多义线几何形状接口</param>
        /// <returns>是否</returns>
        public static bool PolylineIsBezierCurve(IPolyline polyline)
        {
            bool result = false;
            if ((polyline != null) && (polyline is ISegmentCollection))
            {
                IEnumCurve curves = (polyline as ISegmentCollection).EnumCurve;
                if (curves != null)
                {
                    curves.Reset();
                    result = curves.Segment is IBezierCurve;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断两个点是否XY相同
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns>是否XY相同</returns>
        public static bool IsSamePointByXY(IPoint p1, IPoint p2)
        {
            if (IsValidGeometry(p1) && IsValidGeometry(p2))
            {
                return p1.X == p2.X && p1.Y == p2.Y;
            }
            else
                return false;
        }

        /// <summary>
        /// 判断两个点是否XYZ相同
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns>是否XYZ相同</returns>
        public static bool IsSamePointByXYZ(IPoint p1, IPoint p2)
        {
            if (IsValidGeometry(p1) && IsValidGeometry(p2))
            {
                return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
            }
            else
                return false;
        }

        /// <summary>
        /// 点集合转换为多点
        /// </summary>
        /// <param name="pointCollection">点集合</param>
        /// <returns>多点</returns>
        public static IMultipoint PointCollectionToMultiPoint(IPointCollection pointCollection)
        {
            if (pointCollection != null)
            {
                IMultipoint points = new Multipoint() as IMultipoint;
                if (pointCollection.PointCount > 0)
                    (points as IPointCollection).AddPointCollection(pointCollection);
                return points;
            }
            else
                return null;
        }

        /// <summary>
        /// 删除几何形状指定的顶点。
        /// 顶点的会与几何形状的顶点进行IPoint::Compare比较，
        /// 因此顶点必须是通过几何形状的顶点获取，才能成功删除。
        /// 如果删除的结果导致几何形状为空，则几何形状接口不为空，只是setEmpty。
        /// </summary>
        /// <param name="geometry">需要删除顶点的ESRI几何形状接口</param>
        /// <param name="point">指定要删除的顶点ESRI点接口</param>
        /// <returns>是否成功删除</returns>
        public static bool GeometryRemoveVertex(IGeometry geometry, IPoint point)
        {
            bool deleted = false;
            if (IsValidGeometry(geometry) && IsValidGeometry(point))
            {
                switch (geometry.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        #region 点几何形状
                        if (geometry is IPoint)
                        {
                            if ((geometry as IPoint).Compare(point) == 0)
                            {
                                geometry.SetEmpty();
                                deleted = true;
                            }
                        }
                        #endregion
                        break;
                    case esriGeometryType.esriGeometryMultipoint:
                        #region 多点几何形状
                        if (geometry is IPointCollection)
                        {
                            for (int i = 0; i <= (geometry as IPointCollection).PointCount - 1; i++)
                            {
                                if ((geometry as IPointCollection).get_Point(i).Compare(point) == 0)
                                {
                                    (geometry as IPointCollection).RemovePoints(i, 1);
                                    deleted = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        #region 多义线几何形状
                        if (geometry is IPointCollection)
                        {
                            for (int i = 0; i <= (geometry as IPointCollection).PointCount - 1; i++)
                            {
                                if ((geometry as IPointCollection).get_Point(i).Compare(point) == 0)
                                {
                                    (geometry as IPointCollection).RemovePoints(i, 1);
                                    deleted = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        #region 面几何形状
                        if (geometry is IGeometryCollection)
                        {
                            bool removed = false;
                            for (int i = 0; i <= (geometry as IGeometryCollection).GeometryCount - 1; i++)
                            {
                                IGeometry partGeom = (geometry as IGeometryCollection).get_Geometry(i);
                                if (IsValidGeometry(partGeom) && partGeom is IRing && partGeom is IPointCollection)
                                {
                                    if ((partGeom as IRing).IsClosed)
                                    {
                                        #region 闭合的环（在编辑草图中存在）
                                        if ((partGeom as IRing).FromPoint.Compare(point) == 0 ||
                                            (partGeom as IRing).ToPoint.Compare(point) == 0)
                                        {
                                            if ((partGeom as IPointCollection).PointCount <= 2)
                                            {
                                                partGeom = null;
                                                removed = true;
                                            }
                                            else
                                            {
                                                IPoint startPoint = (partGeom as IPointCollection).get_Point(1);
                                                (partGeom as IPointCollection).RemovePoints((partGeom as IPointCollection).PointCount - 1, 1);
                                                (partGeom as IPointCollection).RemovePoints(0, 1);
                                                //(partGeom as IRing).FromPoint = startPoint;
                                                (partGeom as IRing).Close();
                                                removed = true;
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j <= (partGeom as IPointCollection).PointCount - 1; j++)
                                            {
                                                if ((partGeom as IPointCollection).get_Point(j).Compare(point) == 0)
                                                {
                                                    (partGeom as IPointCollection).RemovePoints(j, 1);
                                                    removed = true;
                                                    break;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 非闭合的环（在编辑草图中应该不存在）
                                        for (int j = 0; j <= (partGeom as IPointCollection).PointCount - 1; j++)
                                        {
                                            if ((partGeom as IPointCollection).get_Point(j).Compare(point) == 0)
                                            {
                                                (partGeom as IPointCollection).RemovePoints(j, 1);
                                                removed = true;
                                                break;
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                if (removed)
                                {
                                    (geometry as IGeometryCollection).RemoveGeometries(i, 1);
                                    if (IsValidGeometry(partGeom))
                                    {
                                        IGeometryCollection geometryCollection = new Multipoint() as IGeometryCollection;
                                        IGeometryBridge geometryBridge = new GeometryEnvironment() as IGeometryBridge;
                                        IGeometry[] geometryArray = { partGeom };
                                        geometryBridge.InsertGeometries((IGeometryCollection)geometry, i, ref geometryArray);
                                    }
                                    deleted = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            return deleted;
        }

        /// <summary>
        /// 获取几何形状的边线，对于面几何形状就是外型边线
        /// </summary>
        /// <param name="geometry">查询的几何形状</param>
        /// <returns>几何形状的边线</returns>
        public static IPolycurve GeometryLineShape(IGeometry geometry)
        {
            if (IsValidGeometry(geometry) && geometry is IPolycurve)
            {
                try
                {
                    IPolycurve lineShape = new Polyline() as IPolycurve;
                    object shapeLineObj = (object)lineShape;
                    SystemUtility.ObjectCopy(geometry, ref shapeLineObj);
                    lineShape = (IPolycurve)shapeLineObj;
                    return lineShape;
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }

        /// <summary>
        /// 获取几何形状的中间点，只有连续的线才具有
        /// </summary>
        /// <param name="geometry">几何形状</param>
        /// <returns>中间点</returns>
        public static IPoint GetMiddlePoint(IGeometry geometry)
        {
            IPoint point = null;
            if (IsValidGeometry(geometry))
            {
                if (geometry is ICurve)
                {
                    point = new ESRI.ArcGIS.Geometry.Point();
                    (geometry as ICurve).QueryPoint(esriSegmentExtension.esriNoExtension, 0.5, true, point);
                }
                else if (geometry is IPolycurve)
                {
                    point = new ESRI.ArcGIS.Geometry.Point();
                    (geometry as IPolycurve).QueryPoint(esriSegmentExtension.esriNoExtension, 0.5, true, point);
                }
            }
            return point;
        }
    }
}