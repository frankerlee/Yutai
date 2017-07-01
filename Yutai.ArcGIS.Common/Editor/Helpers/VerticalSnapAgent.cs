using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class VerticalSnapAgent : IFeatureSnapAgent, IEngineSnapAgent, IVerticalSnapAgent
    {
        private IFeatureClass ifeatureClass_0 = null;

        private IFeatureCache ifeatureCache_0 = new FeatureCache();

        public IFeatureCache FeatureCache
        {
            get { return this.ifeatureCache_0; }
            set { this.ifeatureCache_0 = value; }
        }

        public IFeatureClass FeatureClass
        {
            get { return this.ifeatureClass_0; }
            set { this.ifeatureClass_0 = value; }
        }

        public int GeometryHitType
        {
            get { return 0; }
            set { }
        }

        public esriGeometryHitPartType HitType
        {
            get { return esriGeometryHitPartType.esriGeometryPartNone; }
            set { }
        }

        public string Name
        {
            get { return "垂直捕捉"; }
        }

        public VerticalSnapAgent()
        {
        }

        public bool Snap(IGeometry igeometry_0, IPoint ipoint_0, double double_0)
        {
            bool flag;
            double num = 0;
            int num1 = 0;
            int num2 = 0;
            bool flag1 = true;
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            bool flag2 = false;
            IPoint ipoint0 = ipoint_0;
            IPoint igeometry0 = null;
            if (igeometry_0 is IPoint)
            {
                igeometry0 = (IPoint) igeometry_0;
            }
            else if (igeometry_0 != null)
            {
                igeometry0 = ((IPointCollection) igeometry_0).Point[0];
            }
            int num3 = 0;
            while (true)
            {
                if (num3 < this.ifeatureCache_0.Count)
                {
                    IHitTest shape = (IHitTest) this.ifeatureCache_0.Feature[num3].Shape;
                    if (shape.HitTest(ipoint0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, pointClass,
                        ref num, ref num1, ref num2, ref flag1))
                    {
                        ILine segment =
                            ((ISegmentCollection) ((IGeometryCollection) shape).Geometry[num1]).Segment[num2] as ILine;
                        if (segment != null)
                        {
                            ILine lineClass = new Line();
                            lineClass.PutCoords(segment.FromPoint, igeometry0);
                            double angle = lineClass.Angle;
                            if (angle < 0)
                            {
                                angle = angle + 6.28318530717959;
                            }
                            double angle1 = segment.Angle;
                            if (angle1 < 0)
                            {
                                angle1 = angle1 + 6.28318530717959;
                            }
                            double num4 = angle1 - angle;
                            if (num4 < 0)
                            {
                                num4 = num4 + 6.28318530717959;
                            }
                            double num5 = Math.Cos(num4)*lineClass.Length;
                            if (num5 <= segment.Length)
                            {
                                IPoint point = new Point()
                                {
                                    X = segment.FromPoint.X + num5*Math.Cos(segment.Angle),
                                    Y = segment.FromPoint.Y + num5*Math.Sin(segment.Angle),
                                    Z = pointClass.Z
                                };
                                ipoint_0.PutCoords(point.X, point.Y);
                                flag = true;
                                break;
                            }
                        }
                    }
                    num3++;
                }
                else
                {
                    flag = flag2;
                    break;
                }
            }
            return flag;
        }
    }
}