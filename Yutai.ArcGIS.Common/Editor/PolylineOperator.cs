using System;
using System.Diagnostics;
using System.Reflection;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor
{
    public class PolylineOperator : GeometryOperator
    {
        public PolylineOperator(IGeometry igeometry_0)
        {
            this.m_pGeometry = igeometry_0;
        }

        public IPolyline Cut(IPoint ipoint_0, IPoint ipoint_1, double double_0)
        {
            IPoint point;
            bool flag;
            int num;
            int num1;
            IPoint point1;
            int num2;
            IPolyline polyline;
            IPolyline mPGeometry = this.m_pGeometry as IPolyline;
            bool flag1 = false;
            int num3 = -1;
            int num4 = -1;
            double num5 = 0;
            int num6 = -1;
            int num7 = -1;
            bool flag2 = GeometryOperator.TestGeometryHit(double_0, ipoint_0, mPGeometry, out point, ref num5, ref num6,
                ref num3, out flag1);
            bool flag3 = flag2;
            if ((!flag2 ? false : !flag1))
            {
                mPGeometry.SplitAtPoint(point, true, false, out flag, out num, out num1);
                if (flag)
                {
                    GeometryOperator.TestGeometryHit(double_0, ipoint_0, mPGeometry, out point, ref num5, ref num6,
                        ref num3, out flag1);
                }
            }
            bool flag4 = GeometryOperator.TestGeometryHit(double_0, ipoint_1, mPGeometry, out point1, ref num5, ref num7,
                ref num4, out flag1);
            bool flag5 = flag4;
            if ((!flag4 ? false : !flag1))
            {
                mPGeometry.SplitAtPoint(point, true, false, out flag, out num, out num1);
                if (flag)
                {
                    if (num3 >= num4)
                    {
                        num3++;
                    }
                    GeometryOperator.TestGeometryHit(double_0, ipoint_1, mPGeometry, out point1, ref num5, ref num7,
                        ref num4, out flag1);
                }
            }
            if ((!flag3 ? true : !flag5))
            {
                polyline = null;
            }
            else
            {
                if (num6 > num7)
                {
                    num2 = num6;
                    num6 = num7;
                    num7 = num2;
                    num2 = num3;
                    num3 = num4;
                    num4 = num2;
                }
                else if ((num6 != num7 ? false : num3 > num4))
                {
                    num2 = num3;
                    num3 = num4;
                    num4 = num2;
                }
                polyline = this.method_1(mPGeometry, num6, num3, num7, num4) as IPolyline;
            }
            return polyline;
        }

        public override bool DeCompose(out IArray iarray_0)
        {
            bool flag;
            iarray_0 = null;
            IGeometryCollection mPGeometry = this.m_pGeometry as IGeometryCollection;
            if (mPGeometry.GeometryCount <= 1)
            {
                flag = false;
            }
            else
            {
                iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
                object value = Missing.Value;
                bool zAware = false;
                bool mAware = false;
                double zMin = 0;
                try
                {
                    zAware = (mPGeometry as IZAware).ZAware;
                    zMin = (mPGeometry as IZ).ZMin;
                }
                catch
                {
                }
                try
                {
                    mAware = (mPGeometry as IMAware).MAware;
                }
                catch
                {
                }
                for (int i = 0; i < mPGeometry.GeometryCount; i++)
                {
                    IGeometry geometry = mPGeometry.Geometry[i];
                    IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                    (polylineClass as IZAware).ZAware = zAware;
                    (polylineClass as IMAware).MAware = mAware;
                    polylineClass.AddGeometry(geometry, ref value, ref value);
                    if (zAware)
                    {
                        (polylineClass as IZ).SetConstantZ(zMin);
                    }
                    (polylineClass as ITopologicalOperator).Simplify();
                    iarray_0.Add(polylineClass);
                }
                flag = true;
            }
            return flag;
        }

        public override bool HiTest(double double_0, IPoint ipoint_0, out IPoint ipoint_1, ref double double_1,
            ref int int_0, ref int int_1, out bool bool_0)
        {
            bool flag;
            IHitTest mPGeometry = this.m_pGeometry as IHitTest;
            ipoint_1 = new ESRI.ArcGIS.Geometry.Point();
            bool_0 = false;
            bool flag1 = false;
            if (mPGeometry.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_1,
                ref double_1, ref int_0, ref int_1, ref flag1))
            {
                bool_0 = true;
                flag = true;
            }
            else if (
                !mPGeometry.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, ipoint_1,
                    ref double_1, ref int_0, ref int_1, ref flag1))
            {
                flag = false;
            }
            else
            {
                bool_0 = false;
                flag = true;
            }
            return flag;
        }

        public override IGeometry HorizontalMirror(IPoint ipoint_0)
        {
            object value = Missing.Value;
            IGeometryCollection mPGeometry = this.m_pGeometry as IGeometryCollection;
            IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
            for (int i = 0; i < mPGeometry.GeometryCount; i++)
            {
                ISegmentCollection geometry = mPGeometry.Geometry[i] as ISegmentCollection;
                ISegmentCollection pathClass = new ESRI.ArcGIS.Geometry.Path() as ISegmentCollection;
                int num = 0;
                while (num < geometry.SegmentCount)
                {
                    ISegment segment = this.method_3(geometry.Segment[num], ipoint_0);
                    pathClass.AddSegment(segment, ref value, ref value);
                    i++;
                }
                polylineClass.AddGeometry(geometry as IGeometry, ref value, ref value);
            }
            return polylineClass as IGeometry;
        }

        private IGeometry method_1(IPolyline ipolyline_0, int int_0, int int_1, int int_2, int int_3)
        {
            IPointCollection geometry;
            int i;
            IGeometryCollection ipolyline0 = ipolyline_0 as IGeometryCollection;
            IGeometryCollection polylineClass = null;
            object value = Missing.Value;
            if (int_2 != int_0)
            {
                if (int_0 != ipolyline0.GeometryCount - 1)
                {
                    ipolyline0.RemoveGeometries(int_0 + 1, ipolyline0.GeometryCount - int_0 - 1);
                }
                if (int_2 != 0)
                {
                    ipolyline0.RemoveGeometries(0, int_2);
                }
                geometry = ipolyline0.Geometry[0] as IPointCollection;
                if (int_3 != 0)
                {
                    geometry.RemovePoints(0, int_3);
                }
                ipolyline0.GeometriesChanged();
                geometry = ipolyline0.Geometry[int_0 - int_2] as IPointCollection;
                if (int_1 != geometry.PointCount - 1)
                {
                    geometry.RemovePoints(int_1 + 1, geometry.PointCount - int_1 - 1);
                }
                ipolyline0.GeometriesChanged();
            }
            else
            {
                geometry = ipolyline0.Geometry[int_0] as IPointCollection;
                if (int_3 - int_1 + 1 != geometry.PointCount)
                {
                    polylineClass = new Polyline() as IGeometryCollection;
                    for (i = 0; i < int_0 - 1; i++)
                    {
                        polylineClass.AddGeometry((ipolyline0.Geometry[i] as IClone).Clone() as IGeometry, ref value,
                            ref value);
                    }
                    IPointCollection pathClass = new ESRI.ArcGIS.Geometry.Path();
                    for (i = int_1; i < int_3 + 1; i++)
                    {
                        pathClass.AddPoint(geometry.Point[i], ref value, ref value);
                    }
                    if (pathClass is IRing)
                    {
                        (pathClass as IRing).Close();
                    }
                    polylineClass.AddGeometry(pathClass as IGeometry, ref value, ref value);
                    for (i = int_0 + 1; i < ipolyline0.GeometryCount; i++)
                    {
                        polylineClass.AddGeometry((ipolyline0.Geometry[i] as IClone).Clone() as IGeometry, ref value,
                            ref value);
                    }
                }
                else
                {
                    ipolyline0.RemoveGeometries(int_0, 1);
                    polylineClass = ipolyline0;
                }
            }
            return polylineClass as IGeometry;
        }

        private IArray method_2(IGeometry igeometry_0, IGeometry igeometry_1)
        {
            IArray array;
            if (!(igeometry_0 == null ? false : igeometry_1 != null))
            {
                array = null;
            }
            else if (igeometry_0.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                IGeometry geometry = ((ITopologicalOperator) igeometry_0).Intersect(igeometry_1,
                    esriGeometryDimension.esriGeometry0Dimension);
                if (geometry != null)
                {
                    ((ITopologicalOperator) geometry).Simplify();
                    IEnumVertex enumVertices = ((IPointCollection) geometry).EnumVertices;
                    if (enumVertices != null)
                    {
                        IPolycurve2 igeometry0 = (IPolycurve2) igeometry_0;
                        if (igeometry0.SplitAtPoints(enumVertices, true, true, -1).SplitHappened)
                        {
                            IGeometryCollection geometryCollection = (IGeometryCollection) igeometry0;
                            IArray arrayClass = new ESRI.ArcGIS.esriSystem.Array();
                            try
                            {
                                bool zAware = false;
                                bool mAware = false;
                                double zMin = 0;
                                try
                                {
                                    zAware = (igeometry_0 as IZAware).ZAware;
                                    zMin = (igeometry_0 as IZ).ZMin;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    mAware = (igeometry_0 as IMAware).MAware;
                                }
                                catch
                                {
                                }
                                for (int i = 0; i < geometryCollection.GeometryCount; i++)
                                {
                                    IGeometry geometry1 = geometryCollection.Geometry[i];
                                    IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                                    (polylineClass as IZAware).ZAware = zAware;
                                    (polylineClass as IMAware).MAware = mAware;
                                    polylineClass.AddGeometries(1, ref geometry1);
                                    if (zAware)
                                    {
                                        (polylineClass as IZ).SetConstantZ(zMin);
                                    }
                                    (polylineClass as ITopologicalOperator).Simplify();
                                    arrayClass.Add(polylineClass);
                                }
                            }
                            catch (Exception exception)
                            {
                                Trace.WriteLine(exception);
                            }
                            array = arrayClass;
                        }
                        else
                        {
                            array = null;
                        }
                    }
                    else
                    {
                        array = null;
                    }
                }
                else
                {
                    array = null;
                }
            }
            else
            {
                array = null;
            }
            return array;
        }

        private ISegment method_3(ISegment isegment_0, IPoint ipoint_0)
        {
            ISegment lineClass;
            IPoint pointClass;
            IPoint point;
            ISegment segment;
            IPoint x;
            double num;
            double num1;
            double x1;
            double num2;
            IPoint fromPoint = isegment_0.FromPoint;
            IPoint toPoint = isegment_0.ToPoint;
            if (isegment_0.GeometryType == esriGeometryType.esriGeometryLine)
            {
                lineClass = new Line() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(2*ipoint_0.X - fromPoint.X, fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(2*ipoint_0.X - toPoint.X, toPoint.Y);
                lineClass.FromPoint = pointClass;
                lineClass.ToPoint = point;
                segment = lineClass;
            }
            else if (isegment_0.GeometryType == esriGeometryType.esriGeometryCircularArc)
            {
                lineClass = new CircularArc() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(2*ipoint_0.X - fromPoint.X, fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(2*ipoint_0.X - toPoint.X, toPoint.Y);
                x = new ESRI.ArcGIS.Geometry.Point();
                (isegment_0 as ICircularArc).QueryCenterPoint(x);
                x.X = 2*ipoint_0.X - x.X;
                (lineClass as IConstructCircularArc).ConstructThreePoints(pointClass, x, point, false);
                segment = lineClass;
            }
            else if (isegment_0.GeometryType == esriGeometryType.esriGeometryEllipticArc)
            {
                lineClass = new EllipticArc() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(2*ipoint_0.X - fromPoint.X, fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(2*ipoint_0.X - toPoint.X, toPoint.Y);
                x = new ESRI.ArcGIS.Geometry.Point();
                (isegment_0 as IEllipticArc).QueryCenterPoint(x);
                x.X = 2*ipoint_0.X - x.X;
                isegment_0.Envelope.QueryCoords(out num, out num1, out x1, out num2);
                num = 2*ipoint_0.X - num;
                x1 = 2*ipoint_0.X - x1;
                IEnvelope envelopeClass = new Envelope() as IEnvelope;
                envelopeClass.PutCoords(num, num1, x1, num2);
                (lineClass as IConstructEllipticArc).ConstructTwoPointsEnvelope(pointClass, point, envelopeClass,
                ((isegment_0 as IEllipticArc).IsCounterClockwise
                    ? esriArcOrientation.esriArcCounterClockwise
                    : esriArcOrientation.esriArcClockwise));
                segment = lineClass;
            }
            else if (isegment_0.GeometryType != esriGeometryType.esriGeometryBezier3Curve)
            {
                segment = null;
            }
            else
            {
                lineClass = new Line() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(2*ipoint_0.X - fromPoint.X, fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(2*ipoint_0.X - toPoint.X, toPoint.Y);
                lineClass.FromPoint = pointClass;
                lineClass.ToPoint = point;
                segment = lineClass;
            }
            return segment;
        }

        private ISegment method_4(ISegment isegment_0, IPoint ipoint_0)
        {
            ISegment lineClass;
            IPoint pointClass;
            IPoint point;
            ISegment segment;
            double num;
            double y;
            double num1;
            double y1;
            IPoint fromPoint = isegment_0.FromPoint;
            IPoint toPoint = isegment_0.ToPoint;
            if (isegment_0.GeometryType == esriGeometryType.esriGeometryLine)
            {
                lineClass = new Line() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(fromPoint.X, 2*ipoint_0.Y - fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(toPoint.X, 2*ipoint_0.Y - toPoint.Y);
                lineClass.FromPoint = pointClass;
                lineClass.ToPoint = point;
                segment = lineClass;
            }
            else if (isegment_0.GeometryType == esriGeometryType.esriGeometryCircularArc)
            {
                lineClass = new CircularArc() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(fromPoint.X, 2*ipoint_0.Y - fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(toPoint.X, 2*ipoint_0.Y - toPoint.Y);
                IPoint pointClass1 = new ESRI.ArcGIS.Geometry.Point();
                (isegment_0 as ICircularArc).QueryCenterPoint(pointClass1);
                pointClass1.Y = 2*ipoint_0.Y - pointClass1.Y;
                (lineClass as IConstructCircularArc).ConstructThreePoints(pointClass, pointClass1, point, false);
                segment = lineClass;
            }
            else if (isegment_0.GeometryType == esriGeometryType.esriGeometryEllipticArc)
            {
                lineClass = new EllipticArc() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(fromPoint.X, 2*ipoint_0.Y - fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(toPoint.X, 2*ipoint_0.Y - toPoint.Y);
                isegment_0.Envelope.QueryCoords(out num, out y, out num1, out y1);
                y = 2*ipoint_0.Y - y;
                y1 = 2*ipoint_0.Y - y1;
                IEnvelope envelopeClass = new Envelope() as IEnvelope;
                envelopeClass.PutCoords(num, y, num1, y1);
                (lineClass as IConstructEllipticArc).ConstructTwoPointsEnvelope(pointClass, point, envelopeClass,
                ((isegment_0 as IEllipticArc).IsCounterClockwise
                    ? esriArcOrientation.esriArcCounterClockwise
                    : esriArcOrientation.esriArcClockwise));
                segment = lineClass;
            }
            else if (isegment_0.GeometryType != esriGeometryType.esriGeometryBezier3Curve)
            {
                segment = null;
            }
            else
            {
                lineClass = new Line() as ISegment;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(fromPoint.X, 2*ipoint_0.Y - fromPoint.Y);
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(toPoint.X, 2*ipoint_0.Y - toPoint.Y);
                lineClass.FromPoint = pointClass;
                lineClass.ToPoint = point;
                segment = lineClass;
            }
            return segment;
        }

        public override IArray Split(IGeometry igeometry_0)
        {
            return this.method_2(this.m_pGeometry, igeometry_0);
        }

        public override bool SplitAtPoint(IPoint ipoint_0, out IGeometryBag igeometryBag_0)
        {
            bool flag;
            bool flag1;
            int num;
            int num1;
            int i;
            igeometryBag_0 = null;
            IPolycurve mPGeometry = this.m_pGeometry as IPolycurve;
            if (mPGeometry != null)
            {
                mPGeometry.SplitAtPoint(ipoint_0, true, true, out flag1, out num, out num1);
                if (flag1)
                {
                    igeometryBag_0 = new GeometryBag() as IGeometryBag;
                    IGeometryCollection geometryCollection = mPGeometry as IGeometryCollection;
                    IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                    object value = Missing.Value;
                    for (i = 0; i < num; i++)
                    {
                        polylineClass.AddGeometry(geometryCollection.Geometry[i], ref value, ref value);
                    }
                    (igeometryBag_0 as IGeometryCollection).AddGeometry(polylineClass as IGeometry, ref value, ref value);
                    polylineClass = new Polyline() as IGeometryCollection;
                    for (i = num; i < geometryCollection.GeometryCount; i++)
                    {
                        polylineClass.AddGeometry(geometryCollection.Geometry[i], ref value, ref value);
                    }
                    (igeometryBag_0 as IGeometryCollection).AddGeometry(polylineClass as IGeometry, ref value, ref value);
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public override IGeometry VerticalMirror(IPoint ipoint_0)
        {
            object value = Missing.Value;
            IGeometryCollection mPGeometry = this.m_pGeometry as IGeometryCollection;
            IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
            for (int i = 0; i < mPGeometry.GeometryCount; i++)
            {
                ISegmentCollection geometry = mPGeometry.Geometry[i] as ISegmentCollection;
                ISegmentCollection pathClass = new ESRI.ArcGIS.Geometry.Path() as ISegmentCollection;
                int num = 0;
                while (num < geometry.SegmentCount)
                {
                    ISegment segment = this.method_4(geometry.Segment[num], ipoint_0);
                    pathClass.AddSegment(segment, ref value, ref value);
                    i++;
                }
                polylineClass.AddGeometry(geometry as IGeometry, ref value, ref value);
            }
            return polylineClass as IGeometry;
        }
    }
}