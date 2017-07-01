using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class FeatureSnapAgent : IFeatureSnapAgent, IEngineSnapAgent
    {
        private esriGeometryHitPartType esriGeometryHitPartType_0;

        private IFeatureClass ifeatureClass_0;

        private IFeatureCache ifeatureCache_0;

        private int int_0 = 0;

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
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        public string Name
        {
            get { return "要素捕捉"; }
        }

        public FeatureSnapAgent()
        {
            this.ifeatureCache_0 = new FeatureCache();
        }

        public bool Snap(IGeometry igeometry_0, IPoint ipoint_0, double double_0)
        {
            bool result;
            if (!EditorLicenseProviderCheck.Check())
            {
                result = false;
            }
            else if (this.int_0 == 0)
            {
                result = false;
            }
            else if (this.ifeatureCache_0.Count == 0)
            {
                result = false;
            }
            else
            {
                double num = 0.0;
                int num2 = 0;
                int num3 = 0;
                bool flag = true;
                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                double num4 = double_0*3.0;
                bool flag2 = false;
                for (int i = 0; i < this.ifeatureCache_0.Count; i++)
                {
                    IHitTest hitTest = (IHitTest) this.ifeatureCache_0.get_Feature(i).Shape;
                    if ((this.int_0 & 16) != 0 &&
                        (hitTest as IGeometry).GeometryType == esriGeometryType.esriGeometryPolyline &&
                        hitTest.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartEndpoint, point,
                            ref num, ref num2, ref num3, ref flag) && num4 > num)
                    {
                        num4 = num;
                        ipoint_0.PutCoords(point.X, point.Y);
                        flag2 = true;
                        if (double_0 > num)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if ((this.int_0 & 1) != 0 &&
                             hitTest.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartVertex, point,
                                 ref num, ref num2, ref num3, ref flag) && num4 > num)
                    {
                        num4 = num;
                        ipoint_0.PutCoords(point.X, point.Y);
                        flag2 = true;
                        if (double_0 > num)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if ((this.int_0 & 8) != 0 &&
                             ((hitTest as IGeometry).GeometryType == esriGeometryType.esriGeometryPolyline ||
                              (hitTest as IGeometry).GeometryType == esriGeometryType.esriGeometryPolygon) &&
                             hitTest.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartMidpoint, point,
                                 ref num, ref num2, ref num3, ref flag) && num4 > num)
                    {
                        num4 = num;
                        ipoint_0.PutCoords(point.X, point.Y);
                        flag2 = true;
                        if (double_0 > num)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if ((this.int_0 & 4) != 0 &&
                             ((hitTest as IGeometry).GeometryType == esriGeometryType.esriGeometryPolyline ||
                              (hitTest as IGeometry).GeometryType == esriGeometryType.esriGeometryPolygon) &&
                             hitTest.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, point,
                                 ref num, ref num2, ref num3, ref flag) && num4 > num)
                    {
                        num4 = num;
                        ipoint_0.PutCoords(point.X, point.Y);
                        flag2 = true;
                        if (double_0 > num)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if ((this.int_0 & 32) != 0 &&
                             (hitTest as IGeometry).GeometryType == esriGeometryType.esriGeometryPolygon &&
                             hitTest.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartCentroid, point,
                                 ref num, ref num2, ref num3, ref flag) && num4 > num)
                    {
                        num4 = num;
                        ipoint_0.PutCoords(point.X, point.Y);
                        flag2 = true;
                        if (double_0 > num)
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                result = flag2;
            }
            return result;
        }
    }
}