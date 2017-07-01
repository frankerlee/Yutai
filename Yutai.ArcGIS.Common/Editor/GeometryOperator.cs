using System;
using System.Reflection;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor
{
    public abstract class GeometryOperator
    {
        protected IGeometry m_pGeometry = null;

        public GeometryOperator()
        {
        }

        public static void CreateVerticalLOSPatches(bool bool_0, IPoint ipoint_0, IPoint ipoint_1, IPolyline ipolyline_0,
            IPolyline ipolyline_1, IGeometryCollection igeometryCollection_0, IGeometryCollection igeometryCollection_1,
            out double double_0)
        {
            int i;
            IPointCollection geometry;
            IClone point;
            IPointCollection triangleFanClass;
            IVector3D vector3DClass;
            IPoint point1;
            IPoint point2;
            object value = Missing.Value;
            IGeometryCollection ipolyline0 = ipolyline_0 as IGeometryCollection;
            IMultiPatch igeometryCollection1 = igeometryCollection_1 as IMultiPatch;
            double_0 = ipoint_1.Z;
            double magnitude = 0;
            IPoint point3 = null;
            for (i = 0; i < ipolyline0.GeometryCount; i++)
            {
                geometry = ipolyline0.Geometry[i] as IPointCollection;
                if (i == 0)
                {
                    point = geometry.Point[0] as IClone;
                    IPoint point4 = point.Clone() as IPoint;
                }
                point = geometry as IClone;
                triangleFanClass = new TriangleFan();
                point = ipoint_0 as IClone;
                triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                point = geometry as IClone;
                triangleFanClass.AddPointCollection(point.Clone() as IPointCollection);
                if (i == ipolyline0.GeometryCount - 1)
                {
                    vector3DClass = new Vector3D() as IVector3D;
                    point = ipoint_0 as IClone;
                    point1 = point.Clone() as IPoint;
                    point1.Z = 0;
                    point = geometry.Point[geometry.PointCount - 1] as IClone;
                    point2 = point.Clone() as IPoint;
                    point2.Z = 0;
                    vector3DClass.ConstructDifference(point1, point2);
                    magnitude = vector3DClass.Magnitude;
                    point3 = point.Clone() as IPoint;
                    if (ipolyline_1 == null && ipoint_1.Z > geometry.Point[geometry.PointCount - 1].Z)
                    {
                        point = ipoint_1 as IClone;
                        triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                    }
                }
                igeometryCollection_0.AddGeometry(triangleFanClass as IGeometry, ref value, ref value);
            }
            if (ipolyline_1 != null)
            {
                ipolyline0 = ipolyline_1 as IGeometryCollection;
                for (i = 0; i < ipolyline0.GeometryCount; i++)
                {
                    geometry = ipolyline0.Geometry[i] as IPointCollection;
                    point = geometry as IClone;
                    IPointCollection ringClass = new Ring();
                    point = geometry as IClone;
                    ringClass.AddPointCollection(point.Clone() as IPointCollection);
                    if (i == ipolyline0.GeometryCount - 1)
                    {
                        vector3DClass = new Vector3D() as IVector3D;
                        point = ipoint_0 as IClone;
                        point1 = point.Clone() as IPoint;
                        point1.Z = 0;
                        point = geometry.Point[geometry.PointCount - 1] as IClone;
                        point2 = point.Clone() as IPoint;
                        point2.Z = 0;
                        vector3DClass.ConstructDifference(point1, point2);
                        if (magnitude < vector3DClass.Magnitude)
                        {
                            point = ipoint_0 as IClone;
                            point1 = point.Clone() as IPoint;
                            point1.Z = 0;
                            point = geometry.Point[0] as IClone;
                            point2 = point.Clone() as IPoint;
                            point2.Z = 0;
                            vector3DClass.ConstructDifference(point1, point2);
                            double num = vector3DClass.Magnitude;
                            double z = (ipoint_0.Z - geometry.Point[0].Z)/num;
                            point = geometry.Point[geometry.PointCount - 1] as IClone;
                            IPoint z1 = point.Clone() as IPoint;
                            point2 = point.Clone() as IPoint;
                            point2.Z = 0;
                            vector3DClass.ConstructDifference(point1, point2);
                            double magnitude1 = vector3DClass.Magnitude;
                            z1.Z = ipoint_0.Z - magnitude1*z;
                            point = z1 as IClone;
                            ringClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                            if (!bool_0)
                            {
                                double_0 = z1.Z;
                            }
                            else
                            {
                                triangleFanClass = new TriangleFan();
                                point = ipoint_0 as IClone;
                                triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                                point = ipoint_1 as IClone;
                                triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                                point = z1 as IClone;
                                triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                                igeometryCollection_0.AddGeometry(triangleFanClass as IGeometry, ref value, ref value);
                            }
                        }
                        else if (bool_0 && ipoint_1.Z > point3.Z)
                        {
                            triangleFanClass = new TriangleFan();
                            point = ipoint_0 as IClone;
                            triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                            point = ipoint_1 as IClone;
                            triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                            point = point3 as IClone;
                            triangleFanClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                            igeometryCollection_0.AddGeometry(triangleFanClass as IGeometry, ref value, ref value);
                        }
                    }
                    point = ringClass.Point[0] as IClone;
                    ringClass.AddPoint(point.Clone() as IPoint, ref value, ref value);
                    igeometryCollection_1.AddGeometry(ringClass as IGeometry, ref value, ref value);
                    igeometryCollection1.PutRingType(ringClass as IRing, esriMultiPatchRingType.esriMultiPatchRing);
                }
            }
        }

        public abstract bool DeCompose(out IArray iarray_0);

        public abstract bool HiTest(double double_0, IPoint ipoint_0, out IPoint ipoint_1, ref double double_1,
            ref int int_0, ref int int_1, out bool bool_0);

        public abstract IGeometry HorizontalMirror(IPoint ipoint_0);

        public static void InsertPoint(IPoint ipoint_0, IGeometry igeometry_0, int int_0, int int_1)
        {
            object value = Missing.Value;
            IPath geometry = (IPath) ((IGeometryCollection) igeometry_0).Geometry[int_0];
            if ((!double.IsNaN(ipoint_0.Z) ? false : !double.IsNaN(geometry.FromPoint.Z)))
            {
                ipoint_0.Z = geometry.FromPoint.Z;
            }
            object int1 = int_1;
            ((IPointCollection) geometry).AddPoint(ipoint_0, ref value, ref int1);
        }

        public static void MakeConstantZ(IGeometry igeometry_0, double double_0)
        {
            if (igeometry_0 is IZ)
            {
                GeometryOperator.MakeZMAware(igeometry_0, true);
                IZ igeometry0 = igeometry_0 as IZ;
                igeometry0.CalculateNonSimpleZs();
                igeometry0.SetConstantZ(double_0);
            }
        }

        public static void MakeOffsetZ(IGeometry igeometry_0, double double_0)
        {
            if (igeometry_0 is IZ)
            {
                GeometryOperator.MakeZMAware(igeometry_0, true);
                IZ igeometry0 = igeometry_0 as IZ;
                igeometry0.CalculateNonSimpleZs();
                igeometry0.OffsetZs(double_0);
            }
        }

        public static void MakeZMAware(IGeometry igeometry_0, bool bool_0)
        {
            if (igeometry_0 is IZAware)
            {
                IZAware igeometry0 = igeometry_0 as IZAware;
                if ((!igeometry0.ZAware ? false : !bool_0))
                {
                    igeometry0.DropZs();
                }
                igeometry0.ZAware = bool_0;
            }
            if (igeometry_0 is IMAware)
            {
                IMAware bool0 = igeometry_0 as IMAware;
                if ((!bool0.MAware ? false : !bool_0))
                {
                    bool0.DropMs();
                }
                bool0.MAware = bool_0;
            }
        }

        private IGeometry method_0(esriGeometryType esriGeometryType_0)
        {
            IGeometry pointClass = null;
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                {
                    pointClass = new ESRI.ArcGIS.Geometry.Point();
                    break;
                }
                case esriGeometryType.esriGeometryMultipoint:
                {
                    pointClass = new Multipoint() as IGeometry;
                    break;
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    pointClass = new Polyline() as IGeometry;
                    break;
                }
                case esriGeometryType.esriGeometryPolygon:
                {
                    pointClass = new Polygon() as IGeometry;
                    break;
                }
            }
            return pointClass;
        }

        public static IGeometry Mirror(IGeometry igeometry_0, ILine iline_0)
        {
            IGeometry geometry;
            IPointCollection igeometry0;
            object value;
            int i;
            IPointCollection polylineClass = null;
            if (igeometry_0 is IPolyline)
            {
                polylineClass = new Polyline();
            }
            else if (!(igeometry_0 is IPolygon))
            {
                if (igeometry_0 is IMultipoint)
                {
                    polylineClass = new Multipoint();
                    igeometry0 = igeometry_0 as IPointCollection;
                    value = Missing.Value;
                    for (i = 0; i < igeometry0.PointCount; i++)
                    {
                        polylineClass.AddPoint(GeometryOperator.Mirror(igeometry0.Point[i], iline_0), ref value,
                            ref value);
                    }
                    geometry = polylineClass as IGeometry;
                    return geometry;
                }
                if (!(igeometry_0 is IPoint))
                {
                    geometry = null;
                    return geometry;
                }
                else
                {
                    geometry = GeometryOperator.Mirror(igeometry_0 as IPoint, iline_0);
                    return geometry;
                }
            }
            else
            {
                polylineClass = new Polygon();
            }
            igeometry0 = igeometry_0 as IPointCollection;
            value = Missing.Value;
            for (i = 0; i < igeometry0.PointCount; i++)
            {
                polylineClass.AddPoint(GeometryOperator.Mirror(igeometry0.Point[i], iline_0), ref value, ref value);
            }
            geometry = polylineClass as IGeometry;
            return geometry;
        }

        public static IGeometry Mirror(IGeometry igeometry_0, IPolyline ipolyline_0)
        {
            ILine segment = (ipolyline_0 as ISegmentCollection).Segment[0] as ILine;
            return GeometryOperator.Mirror(igeometry_0, segment);
        }

        public static IPoint Mirror(IPoint ipoint_0, ILine iline_0)
        {
            ILine lineClass = new Line();
            lineClass.PutCoords(iline_0.FromPoint, ipoint_0);
            double length = lineClass.Length;
            double angle = lineClass.Angle - iline_0.Angle;
            angle = iline_0.Angle - angle;
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            double x = iline_0.FromPoint.X + length*Math.Cos(angle);
            double y = iline_0.FromPoint.Y + length*Math.Sin(angle);
            pointClass.PutCoords(x, y);
            return pointClass;
        }

        public static void RelpacePoint(IPoint ipoint_0, IGeometry igeometry_0, int int_0, int int_1)
        {
            IPointCollection multipointClass = new Multipoint();
            object value = Missing.Value;
            multipointClass.AddPoint(ipoint_0, ref value, ref value);
            IPath geometry = (IPath) ((IGeometryCollection) igeometry_0).Geometry[int_0];
            bool isClosed = geometry.IsClosed;
            ((IPointCollection) geometry).ReplacePointCollection(int_1, 1, multipointClass);
            if (isClosed && int_1 == 0)
            {
                ((IPointCollection) geometry).ReplacePointCollection((geometry as IPointCollection).PointCount - 1, 1,
                    multipointClass);
            }
        }

        public bool Split(IPolyline ipolyline_0, out IGeometryBag igeometryBag_0)
        {
            bool flag;
            IGeometry geometry;
            IGeometry geometry1;
            igeometryBag_0 = null;
            if (this.m_pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
            {
                try
                {
                    ITopologicalOperator mPGeometry = this.m_pGeometry as ITopologicalOperator;
                    if (mPGeometry != null)
                    {
                        if (!mPGeometry.IsSimple)
                        {
                            mPGeometry.Simplify();
                        }
                        mPGeometry.Cut(ipolyline_0, out geometry, out geometry1);
                        object value = Missing.Value;
                        igeometryBag_0 = new GeometryBag() as IGeometryBag;
                        (igeometryBag_0 as IGeometryCollection).AddGeometry(geometry, ref value, ref value);
                        (igeometryBag_0 as IGeometryCollection).AddGeometry(geometry1, ref value, ref value);
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }
                }
                catch
                {
                }
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public abstract IArray Split(IGeometry igeometry_0);

        public abstract bool SplitAtPoint(IPoint ipoint_0, out IGeometryBag igeometryBag_0);

        public static bool TestGeometryHit(double double_0, IPoint ipoint_0, IGeometry igeometry_0, out IPoint ipoint_1,
            ref double double_1, ref int int_0, ref int int_1, out bool bool_0)
        {
            bool flag;
            bool flag1 = false;
            IHitTest igeometry0 = igeometry_0 as IHitTest;
            ipoint_1 = new ESRI.ArcGIS.Geometry.Point();
            bool_0 = false;
            bool flag2 = false;
            if (
                !(igeometry_0.GeometryType == esriGeometryType.esriGeometryPoint
                    ? false
                    : igeometry_0.GeometryType != esriGeometryType.esriGeometryMultipoint))
            {
                if (
                    !igeometry0.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_1,
                        ref double_1, ref int_0, ref int_1, ref flag2))
                {
                    bool_0 = false;
                }
                else
                {
                    flag1 = true;
                    bool_0 = true;
                }
                flag = flag1;
            }
            else if (igeometry0.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_1,
                ref double_1, ref int_0, ref int_1, ref flag2))
            {
                bool_0 = true;
                flag = true;
            }
            else if (igeometry0.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, ipoint_1,
                ref double_1, ref int_0, ref int_1, ref flag2))
            {
                bool_0 = false;
                flag = true;
            }
            else if ((igeometry_0.GeometryType == esriGeometryType.esriGeometryEnvelope
                         ? false
                         : igeometry_0.GeometryType != esriGeometryType.esriGeometryPolygon) ||
                     !((IRelationalOperator) igeometry_0).Contains(ipoint_0))
            {
                flag = false;
            }
            else
            {
                int_0 = -1;
                bool_0 = false;
                flag = true;
            }
            return flag;
        }

        public abstract IGeometry VerticalMirror(IPoint ipoint_0);
    }
}