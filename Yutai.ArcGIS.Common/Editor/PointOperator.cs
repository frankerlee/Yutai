using System.Reflection;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor
{
    public class PointOperator : GeometryOperator
    {
        public PointOperator(IGeometry igeometry_0)
        {
            this.m_pGeometry = igeometry_0;
        }

        public override bool DeCompose(out IArray iarray_0)
        {
            bool flag;
            iarray_0 = null;
            if (this.m_pGeometry.GeometryType != esriGeometryType.esriGeometryMultipoint)
            {
                flag = false;
            }
            else
            {
                object value = Missing.Value;
                iarray_0 = new Array();
                IPointCollection mPGeometry = this.m_pGeometry as IPointCollection;
                IPoint point = null;
                for (int i = 0; i < mPGeometry.PointCount; i++)
                {
                    point = mPGeometry.Point[i];
                    IPointCollection multipointClass = new Multipoint();
                    multipointClass.AddPoint(point, ref value, ref value);
                    iarray_0.Add(multipointClass);
                }
                flag = true;
            }
            return flag;
        }

        public override bool HiTest(double double_0, IPoint ipoint_0, out IPoint ipoint_1, ref double double_1,
            ref int int_0, ref int int_1, out bool bool_0)
        {
            ipoint_1 = null;
            bool_0 = false;
            bool flag = false;
            IHitTest mPGeometry = this.m_pGeometry as IHitTest;
            ipoint_1 = new ESRI.ArcGIS.Geometry.Point();
            bool_0 = false;
            bool flag1 = false;
            if (
                !mPGeometry.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_1,
                    ref double_1, ref int_0, ref int_1, ref flag1))
            {
                bool_0 = false;
            }
            else
            {
                flag = true;
                bool_0 = true;
            }
            return flag;
        }

        public override IGeometry HorizontalMirror(IPoint ipoint_0)
        {
            IPoint pointClass;
            IGeometry geometry;
            if (this.m_pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
            {
                object value = Missing.Value;
                IPointCollection mPGeometry = this.m_pGeometry as IPointCollection;
                IPointCollection multipointClass = new Multipoint();
                for (int i = 0; i < mPGeometry.PointCount; i++)
                {
                    IPoint point = mPGeometry.Point[i];
                    pointClass = new ESRI.ArcGIS.Geometry.Point();
                    pointClass.PutCoords(2*ipoint_0.X - pointClass.X, pointClass.Y);
                    multipointClass.AddPoint(pointClass, ref value, ref value);
                }
                geometry = multipointClass as IGeometry;
            }
            else
            {
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(2*ipoint_0.X - (this.m_pGeometry as IPoint).X, (this.m_pGeometry as IPoint).Y);
                geometry = pointClass;
            }
            return geometry;
        }

        public override IArray Split(IGeometry igeometry_0)
        {
            return null;
        }

        public override bool SplitAtPoint(IPoint ipoint_0, out IGeometryBag igeometryBag_0)
        {
            igeometryBag_0 = null;
            return false;
        }

        public override IGeometry VerticalMirror(IPoint ipoint_0)
        {
            IPoint pointClass;
            IGeometry geometry;
            if (this.m_pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
            {
                object value = Missing.Value;
                IPointCollection mPGeometry = this.m_pGeometry as IPointCollection;
                IPointCollection multipointClass = new Multipoint();
                for (int i = 0; i < mPGeometry.PointCount; i++)
                {
                    IPoint point = mPGeometry.Point[i];
                    pointClass = new ESRI.ArcGIS.Geometry.Point();
                    pointClass.PutCoords(pointClass.X, 2*ipoint_0.Y - pointClass.Y);
                    multipointClass.AddPoint(pointClass, ref value, ref value);
                }
                geometry = multipointClass as IGeometry;
            }
            else
            {
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords((this.m_pGeometry as IPoint).X, 2*ipoint_0.Y - (this.m_pGeometry as IPoint).Y);
                geometry = pointClass;
            }
            return geometry;
        }
    }
}