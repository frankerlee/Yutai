using System.Reflection;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor
{
    public class PolygonOperator : GeometryOperator
    {
        public PolygonOperator(IGeometry igeometry_0)
        {
            this.m_pGeometry = igeometry_0;
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
                iarray_0 = new Array();
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
                    IGeometryCollection polygonClass = new Polygon() as IGeometryCollection;
                    (polygonClass as IZAware).ZAware = zAware;
                    (polygonClass as IMAware).MAware = mAware;
                    polygonClass.AddGeometry(geometry, ref value, ref value);
                    if (zAware)
                    {
                        (polygonClass as IZ).SetConstantZ(zMin);
                    }
                    (polygonClass as ITopologicalOperator).Simplify();
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
            else if (mPGeometry.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, ipoint_1,
                ref double_1, ref int_0, ref int_1, ref flag1))
            {
                bool_0 = false;
                flag = true;
            }
            else if (!((IRelationalOperator) this.m_pGeometry).Contains(ipoint_0))
            {
                flag = false;
            }
            else
            {
                int_0 = -1;
                bool_0 = false;
                ipoint_1.PutCoords(ipoint_0.X, ipoint_0.Y);
                double_1 = 0;
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
                ISegmentCollection ringClass = new Ring() as ISegmentCollection;
                int num = 0;
                while (num < geometry.SegmentCount)
                {
                    ISegment segment = this.method_2(geometry.Segment[num], ipoint_0);
                    ringClass.AddSegment(segment, ref value, ref value);
                    i++;
                }
                polylineClass.AddGeometry(geometry as IGeometry, ref value, ref value);
            }
            return polylineClass as IGeometry;
        }

        private IArray method_1(IGeometry igeometry_0, IGeometry igeometry_1)
        {
            IArray array;
            IPolyline polyline = null;
            ITopologicalOperator4 igeometry0 = null;
            if (!(igeometry_0 == null ? false : igeometry_1 != null))
            {
                array = null;
            }
            else if (igeometry_0.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                polyline = (igeometry_1.GeometryType == esriGeometryType.esriGeometryPolyline
                    ? (IPolyline) igeometry_1
                    : (IPolyline) ((ITopologicalOperator4) igeometry_1).Boundary);
                igeometry0 = (ITopologicalOperator4) igeometry_0;
                IArray arrayClass = new Array();
                IGeometryCollection geometryCollection = igeometry0.Cut2(polyline);
                for (int i = 0; i < geometryCollection.GeometryCount; i++)
                {
                    arrayClass.Add(geometryCollection.Geometry[i]);
                }
                array = arrayClass;
            }
            else
            {
                array = null;
            }
            return array;
        }

        private ISegment method_2(ISegment isegment_0, IPoint ipoint_0)
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

        private ISegment method_3(ISegment isegment_0, IPoint ipoint_0)
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
            return this.method_1(this.m_pGeometry, igeometry_0);
        }

        public override bool SplitAtPoint(IPoint ipoint_0, out IGeometryBag igeometryBag_0)
        {
            bool flag;
            bool flag1;
            int num;
            int num1;
            igeometryBag_0 = null;
            IPolycurve mPGeometry = this.m_pGeometry as IPolycurve;
            if (mPGeometry != null)
            {
                mPGeometry.SplitAtPoint(ipoint_0, true, true, out flag1, out num, out num1);
                object value = Missing.Value;
                igeometryBag_0 = new GeometryBag() as IGeometryBag;
                (igeometryBag_0 as IGeometryCollection).AddGeometry(mPGeometry, ref value, ref value);
                flag = true;
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
                ISegmentCollection ringClass = new Ring() as ISegmentCollection;
                int num = 0;
                while (num < geometry.SegmentCount)
                {
                    ISegment segment = this.method_3(geometry.Segment[num], ipoint_0);
                    ringClass.AddSegment(segment, ref value, ref value);
                    i++;
                }
                polylineClass.AddGeometry(geometry as IGeometry, ref value, ref value);
            }
            return polylineClass as IGeometry;
        }
    }
}