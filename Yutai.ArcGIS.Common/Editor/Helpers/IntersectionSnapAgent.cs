using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class IntersectionSnapAgent : IFeatureSnapAgent, IEngineSnapAgent
    {
        private IFeatureClass ifeatureClass_0;

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
            get { return esriGeometryHitPartType.esriGeometryPartBoundary; }
            set { }
        }

        public string Name
        {
            get { return null; }
        }

        public IntersectionSnapAgent()
        {
        }

        public bool Snap(IGeometry igeometry_0, IPoint ipoint_0, double double_0)
        {
            bool flag;
            int i;
            IHitTest shape;
            IGeometry geometry;
            if (!EditorLicenseProviderCheck.Check())
            {
                flag = false;
            }
            else if (this.ifeatureCache_0.Count != 0)
            {
                bool flag1 = false;
                double num = 0;
                int num1 = 0;
                int num2 = 0;
                bool flag2 = true;
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                object value = Missing.Value;
                IArray arrayClass = new Array();
                for (i = 0; i < this.ifeatureCache_0.Count; i++)
                {
                    shape = (IHitTest) this.ifeatureCache_0.Feature[i].Shape;
                    if ((shape is IPolyline ? true : shape is IPolygon) &&
                        shape.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, pointClass,
                            ref num, ref num1, ref num2, ref flag2))
                    {
                        arrayClass.Add(shape);
                    }
                }
                IPointCollection multipointClass = new Multipoint();
                IArray arrayClass1 = new Array() as IArray;
                for (i = 0; i < arrayClass.Count; i++)
                {
                    ITopologicalOperator2 element = (ITopologicalOperator2) arrayClass.Element[i];
                    for (int j = 0; j < arrayClass.Count; j++)
                    {
                        if (i != j)
                        {
                            if (((IGeometry) arrayClass.Element[i]).GeometryType !=
                                ((IGeometry) arrayClass.Element[j]).GeometryType)
                            {
                                geometry = element.IntersectMultidimension((IGeometry) arrayClass.Element[j]);
                                if (geometry != null)
                                {
                                    IGeometryCollection geometryCollection = geometry as IGeometryCollection;
                                    if (geometryCollection != null)
                                    {
                                        for (int k = 0; k < geometryCollection.GeometryCount; k++)
                                        {
                                            geometry = geometryCollection.Geometry[k];
                                            if (geometry is IPointCollection)
                                            {
                                                multipointClass.AddPointCollection((IPointCollection) geometry);
                                            }
                                            else if (geometry is IPointCollection)
                                            {
                                                multipointClass.AddPoint((IPoint) geometry, ref value, ref value);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                geometry = element.Intersect((IGeometry) arrayClass.Element[j],
                                    esriGeometryDimension.esriGeometry0Dimension);
                                if (geometry != null)
                                {
                                    if (geometry is IPointCollection)
                                    {
                                        multipointClass.AddPointCollection((IPointCollection) geometry);
                                    }
                                    else if (geometry is IPointCollection)
                                    {
                                        multipointClass.AddPoint((IPoint) geometry, ref value, ref value);
                                    }
                                }
                            }
                        }
                    }
                }
                shape = (IHitTest) multipointClass;
                if (shape.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartVertex, pointClass,
                    ref num, ref num1, ref num2, ref flag2))
                {
                    ipoint_0.PutCoords(pointClass.X, pointClass.Y);
                    flag1 = true;
                }
                flag = flag1;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }
}