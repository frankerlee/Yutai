using System.Collections;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
    public class MoveGeometryFeedbackEx : IDisplayFeedback, IMoveGeometryFeedbackEx
    {
        private IPoint ipoint_0 = null;

        private IMoveGeometryFeedback imoveGeometryFeedback_0 = null;

        private IList ilist_0 = new ArrayList();

        private IList ilist_1 = new ArrayList();

        private IScreenDisplay iscreenDisplay_0 = null;

        private ISet iset_0 = new Set();

        private ISet iset_1 = null;

        public IScreenDisplay Display
        {
            set { this.iscreenDisplay_0 = value; }
        }

        public ISymbol Symbol
        {
            get { return null; }
            set { }
        }

        public MoveGeometryFeedbackEx()
        {
        }

        public void Add(IFeature ifeature_0)
        {
            if (!(ifeature_0 is INetworkFeature))
            {
                if (this.imoveGeometryFeedback_0 == null)
                {
                    this.imoveGeometryFeedback_0 = new MoveGeometryFeedback()
                    {
                        Display = this.iscreenDisplay_0
                    };
                }
                this.imoveGeometryFeedback_0.AddGeometry(ifeature_0.Shape);
            }
            else
            {
                this.iset_0.Add(ifeature_0);
            }
        }

        private void method_0()
        {
            this.iset_1 = new Set();
            this.iset_0.Reset();
            for (IFeature i = this.iset_0.Next() as IFeature; i != null; i = this.iset_0.Next() as IFeature)
            {
                if (i is IEdgeFeature)
                {
                    this.method_1(i as IEdgeFeature);
                }
                else if (i is IJunctionFeature)
                {
                    this.method_3(i as IJunctionFeature);
                }
            }
        }

        private void method_1(IEdgeFeature iedgeFeature_0)
        {
            int edgeFeatureCount;
            int i;
            IEdgeFeature edgeFeature;
            IPoint shape;
            ILineMovePointFeedback lineMovePointFeedbackClass;
            IHitTest hitTest;
            IPoint pointClass;
            double num;
            int num1;
            int num2;
            bool flag;
            int pointCount;
            int j;
            IPath geometry;
            if (!(iedgeFeature_0 is IComplexEdgeFeature))
            {
                int oID = (iedgeFeature_0 as IFeature).OID;
                IGeometry shapeCopy = (iedgeFeature_0 as IFeature).ShapeCopy;
                if (this.imoveGeometryFeedback_0 == null)
                {
                    this.imoveGeometryFeedback_0 = new MoveGeometryFeedback()
                    {
                        Display = this.iscreenDisplay_0
                    };
                }
                this.imoveGeometryFeedback_0.AddGeometry(shapeCopy);
                int fromJunctionEID = iedgeFeature_0.FromJunctionEID;
                IJunctionFeature fromJunctionFeature = iedgeFeature_0.FromJunctionFeature;
                if (fromJunctionFeature is ISimpleJunctionFeature)
                {
                    edgeFeatureCount = (fromJunctionFeature as ISimpleJunctionFeature).EdgeFeatureCount;
                    for (i = 0; i < edgeFeatureCount; i++)
                    {
                        edgeFeature = (fromJunctionFeature as ISimpleJunctionFeature).EdgeFeature[i];
                        if ((edgeFeature as IFeature).OID != oID && !this.iset_0.Find(edgeFeature) &&
                            !this.iset_1.Find(edgeFeature))
                        {
                            this.iset_1.Add(edgeFeature);
                            shapeCopy = (edgeFeature as IFeature).Shape;
                            if (!(edgeFeature is ISimpleEdgeFeature))
                            {
                                shape = (fromJunctionFeature as IFeature).Shape as IPoint;
                                hitTest = shapeCopy as IHitTest;
                                pointClass = new ESRI.ArcGIS.Geometry.Point();
                                num = 0;
                                num1 = -1;
                                num2 = -1;
                                flag = false;
                                if (hitTest.HitTest(shape, 0, esriGeometryHitPartType.esriGeometryPartVertex, pointClass,
                                    ref num, ref num1, ref num2, ref flag))
                                {
                                    lineMovePointFeedbackClass = new LineMovePointFeedback()
                                    {
                                        Display = this.iscreenDisplay_0
                                    };
                                    pointCount = 0;
                                    for (j = 0; j < num1; j++)
                                    {
                                        geometry = (IPath) ((IGeometryCollection) hitTest).Geometry[j];
                                        pointCount = pointCount + (geometry as IPointCollection).PointCount;
                                    }
                                    num2 = num2 + pointCount;
                                    lineMovePointFeedbackClass.Start(hitTest as IPolyline, num2, shape);
                                    this.ilist_0.Add(lineMovePointFeedbackClass);
                                    this.ilist_1.Add(shape);
                                }
                            }
                            else
                            {
                                shape = null;
                                lineMovePointFeedbackClass = new LineMovePointFeedback()
                                {
                                    Display = this.iscreenDisplay_0
                                };
                                if (edgeFeature.FromJunctionEID != fromJunctionEID)
                                {
                                    shape =
                                        (shapeCopy as IPointCollection).Point[
                                            (shapeCopy as IPointCollection).PointCount - 1];
                                    lineMovePointFeedbackClass.Start(shapeCopy as IPolyline,
                                        (shapeCopy as IPointCollection).PointCount - 1, shape);
                                }
                                else
                                {
                                    shape = (shapeCopy as IPointCollection).Point[0];
                                    lineMovePointFeedbackClass.Start(shapeCopy as IPolyline, 0, shape);
                                }
                                this.ilist_0.Add(lineMovePointFeedbackClass);
                                this.ilist_1.Add(shape);
                            }
                        }
                    }
                }
                fromJunctionEID = iedgeFeature_0.ToJunctionEID;
                IJunctionFeature toJunctionFeature = iedgeFeature_0.ToJunctionFeature;
                if (toJunctionFeature is ISimpleJunctionFeature)
                {
                    edgeFeatureCount = (toJunctionFeature as ISimpleJunctionFeature).EdgeFeatureCount;
                    for (i = 0; i < edgeFeatureCount; i++)
                    {
                        edgeFeature = (toJunctionFeature as ISimpleJunctionFeature).EdgeFeature[i];
                        if ((edgeFeature as IFeature).OID != oID && !this.iset_0.Find(edgeFeature) &&
                            !this.iset_1.Find(edgeFeature))
                        {
                            this.iset_1.Add(edgeFeature);
                            shape = null;
                            lineMovePointFeedbackClass = new LineMovePointFeedback()
                            {
                                Display = this.iscreenDisplay_0
                            };
                            shapeCopy = (edgeFeature as IFeature).Shape;
                            if (!(edgeFeature is ISimpleEdgeFeature))
                            {
                                IPoint point = (toJunctionFeature as IFeature).Shape as IPoint;
                                hitTest = shapeCopy as IHitTest;
                                pointClass = new ESRI.ArcGIS.Geometry.Point();
                                num = 0;
                                num1 = -1;
                                num2 = -1;
                                flag = false;
                                if (hitTest.HitTest(point, 0, esriGeometryHitPartType.esriGeometryPartVertex, pointClass,
                                    ref num, ref num1, ref num2, ref flag))
                                {
                                    lineMovePointFeedbackClass = new LineMovePointFeedback()
                                    {
                                        Display = this.iscreenDisplay_0
                                    };
                                    pointCount = 0;
                                    for (j = 0; j < num1; j++)
                                    {
                                        geometry = (IPath) ((IGeometryCollection) hitTest).Geometry[j];
                                        pointCount = pointCount + (geometry as IPointCollection).PointCount;
                                    }
                                    num2 = num2 + pointCount;
                                    lineMovePointFeedbackClass.Start(hitTest as IPolyline, num2, point);
                                    this.ilist_0.Add(lineMovePointFeedbackClass);
                                    this.ilist_1.Add(point);
                                }
                            }
                            else
                            {
                                if (edgeFeature.FromJunctionEID != fromJunctionEID)
                                {
                                    shape =
                                        (shapeCopy as IPointCollection).Point[
                                            (shapeCopy as IPointCollection).PointCount - 1];
                                    lineMovePointFeedbackClass.Start(shapeCopy as IPolyline,
                                        (shapeCopy as IPointCollection).PointCount - 1, shape);
                                }
                                else
                                {
                                    shape = (shapeCopy as IPointCollection).Point[0];
                                    lineMovePointFeedbackClass.Start(shapeCopy as IPolyline, 0, shape);
                                }
                                this.ilist_0.Add(lineMovePointFeedbackClass);
                                this.ilist_1.Add(shape);
                            }
                        }
                    }
                }
            }
            else
            {
                this.method_2(iedgeFeature_0 as IComplexEdgeFeature);
            }
        }

        private void method_2(IComplexEdgeFeature icomplexEdgeFeature_0)
        {
            ILineMovePointFeedback lineMovePointFeedbackClass;
            int oID = (icomplexEdgeFeature_0 as IFeature).OID;
            IGeometry shapeCopy = (icomplexEdgeFeature_0 as IFeature).ShapeCopy;
            if (this.imoveGeometryFeedback_0 == null)
            {
                this.imoveGeometryFeedback_0 = new MoveGeometryFeedback()
                {
                    Display = this.iscreenDisplay_0
                };
            }
            this.imoveGeometryFeedback_0.AddGeometry(shapeCopy);
            for (int i = 0; i < icomplexEdgeFeature_0.JunctionFeatureCount; i++)
            {
                IJunctionFeature junctionFeature = icomplexEdgeFeature_0.JunctionFeature[i];
                if (junctionFeature is ISimpleJunctionFeature)
                {
                    int eID = (junctionFeature as ISimpleJunctionFeature).EID;
                    IPoint point = (junctionFeature as IFeature).ShapeCopy as IPoint;
                    int edgeFeatureCount = (junctionFeature as ISimpleJunctionFeature).EdgeFeatureCount;
                    for (int j = 0; j < edgeFeatureCount; j++)
                    {
                        IEdgeFeature edgeFeature = (junctionFeature as ISimpleJunctionFeature).EdgeFeature[j];
                        if ((edgeFeature as IFeature).OID != oID && !this.iset_0.Find(edgeFeature) &&
                            !this.iset_1.Find(edgeFeature))
                        {
                            this.iset_1.Add(edgeFeature);
                            shapeCopy = (edgeFeature as IFeature).Shape;
                            IPoint point1 = null;
                            if (!(edgeFeature is ISimpleEdgeFeature))
                            {
                                IHitTest shape = (edgeFeature as IFeature).Shape as IHitTest;
                                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                                double num = 0;
                                int num1 = -1;
                                int num2 = -1;
                                bool flag = false;
                                if (shape.HitTest(point, 0, esriGeometryHitPartType.esriGeometryPartVertex, pointClass,
                                    ref num, ref num1, ref num2, ref flag))
                                {
                                    lineMovePointFeedbackClass = new LineMovePointFeedback()
                                    {
                                        Display = this.iscreenDisplay_0
                                    };
                                    int pointCount = 0;
                                    for (int k = 0; k < num1; k++)
                                    {
                                        IPath geometry = (IPath) ((IGeometryCollection) shape).Geometry[k];
                                        pointCount = pointCount + (geometry as IPointCollection).PointCount;
                                    }
                                    num2 = num2 + pointCount;
                                    lineMovePointFeedbackClass.Start(shape as IPolyline, num2, point);
                                    this.ilist_0.Add(lineMovePointFeedbackClass);
                                    this.ilist_1.Add(point);
                                }
                            }
                            else
                            {
                                lineMovePointFeedbackClass = new LineMovePointFeedback()
                                {
                                    Display = this.iscreenDisplay_0
                                };
                                if (edgeFeature.FromJunctionEID != eID)
                                {
                                    point1 =
                                        (shapeCopy as IPointCollection).Point[
                                            (shapeCopy as IPointCollection).PointCount - 1];
                                    lineMovePointFeedbackClass.Start(shapeCopy as IPolyline,
                                        (shapeCopy as IPointCollection).PointCount - 1, point1);
                                }
                                else
                                {
                                    point1 = (shapeCopy as IPointCollection).Point[0];
                                    lineMovePointFeedbackClass.Start(shapeCopy as IPolyline, 0, point1);
                                }
                                this.ilist_0.Add(lineMovePointFeedbackClass);
                                this.ilist_1.Add(point1);
                            }
                        }
                    }
                }
            }
        }

        private void method_3(IJunctionFeature ijunctionFeature_0)
        {
            if (ijunctionFeature_0 is ISimpleJunctionFeature)
            {
                int oID = (ijunctionFeature_0 as IFeature).OID;
                IGeometry shapeCopy = (ijunctionFeature_0 as IFeature).ShapeCopy;
                if (this.imoveGeometryFeedback_0 == null)
                {
                    this.imoveGeometryFeedback_0 = new MoveGeometryFeedback()
                    {
                        Display = this.iscreenDisplay_0
                    };
                }
                this.imoveGeometryFeedback_0.AddGeometry(shapeCopy);
                int eID = (ijunctionFeature_0 as ISimpleJunctionFeature).EID;
                int edgeFeatureCount = (ijunctionFeature_0 as ISimpleJunctionFeature).EdgeFeatureCount;
                IPoint point = null;
                for (int i = 0; i < edgeFeatureCount; i++)
                {
                    IEdgeFeature edgeFeature = (ijunctionFeature_0 as ISimpleJunctionFeature).EdgeFeature[i];
                    if (!this.iset_0.Find(edgeFeature) && !this.iset_1.Find(edgeFeature))
                    {
                        this.iset_1.Add(edgeFeature);
                        ILineMovePointFeedback lineMovePointFeedbackClass = new LineMovePointFeedback()
                        {
                            Display = this.iscreenDisplay_0
                        };
                        shapeCopy = (edgeFeature as IFeature).Shape;
                        if (!(edgeFeature is ISimpleEdgeFeature))
                        {
                            IPoint shape = (ijunctionFeature_0 as IFeature).Shape as IPoint;
                            IHitTest hitTest = shapeCopy as IHitTest;
                            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                            double num = 0;
                            int num1 = -1;
                            int num2 = -1;
                            bool flag = false;
                            if (hitTest.HitTest(shape, 0, esriGeometryHitPartType.esriGeometryPartVertex, pointClass,
                                ref num, ref num1, ref num2, ref flag))
                            {
                                lineMovePointFeedbackClass = new LineMovePointFeedback()
                                {
                                    Display = this.iscreenDisplay_0
                                };
                                int pointCount = 0;
                                for (int j = 0; j < num1; j++)
                                {
                                    IPath geometry = (IPath) ((IGeometryCollection) hitTest).Geometry[j];
                                    pointCount = pointCount + (geometry as IPointCollection).PointCount;
                                }
                                num2 = num2 + pointCount;
                                lineMovePointFeedbackClass.Start(hitTest as IPolyline, num2, shape);
                                this.ilist_0.Add(lineMovePointFeedbackClass);
                                this.ilist_1.Add(shape);
                            }
                        }
                        else
                        {
                            if (edgeFeature.FromJunctionEID != eID)
                            {
                                point =
                                    (shapeCopy as IPointCollection).Point[(shapeCopy as IPointCollection).PointCount - 1
                                    ];
                                lineMovePointFeedbackClass.Start(shapeCopy as IPolyline,
                                    (shapeCopy as IPointCollection).PointCount - 1, point);
                            }
                            else
                            {
                                point = (shapeCopy as IPointCollection).Point[0];
                                lineMovePointFeedbackClass.Start(shapeCopy as IPolyline, 0, point);
                            }
                            this.ilist_0.Add(lineMovePointFeedbackClass);
                            this.ilist_1.Add(point);
                        }
                    }
                }
            }
        }

        public void MoveTo(IPoint ipoint_1)
        {
            double x = ipoint_1.X - this.ipoint_0.X;
            double y = ipoint_1.Y - this.ipoint_0.Y;
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            for (int i = 0; i < this.ilist_1.Count; i++)
            {
                IPoint item = this.ilist_1[i] as IPoint;
                pointClass.X = item.X + x;
                pointClass.Y = item.Y + y;
                (this.ilist_0[i] as IDisplayFeedback).MoveTo(pointClass);
            }
            if (this.imoveGeometryFeedback_0 != null)
            {
                this.imoveGeometryFeedback_0.MoveTo(ipoint_1);
            }
        }

        public void Refresh(int int_0)
        {
            if (this.imoveGeometryFeedback_0 != null)
            {
                this.imoveGeometryFeedback_0.Refresh(int_0);
            }
            for (int i = 0; i < this.ilist_1.Count; i++)
            {
                (this.ilist_0[i] as IDisplayFeedback).Refresh(int_0);
            }
        }

        public void Start(IPoint ipoint_1)
        {
            this.ipoint_0 = ipoint_1;
            this.method_0();
            if (this.imoveGeometryFeedback_0 != null)
            {
                this.imoveGeometryFeedback_0.Start(ipoint_1);
            }
        }
    }
}