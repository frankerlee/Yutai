using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class TangentSnapAgent : IFeatureSnapAgent, IEngineSnapAgent
	{
		private IFeatureClass ifeatureClass_0;

		private IFeatureCache ifeatureCache_0 = new FeatureCache();

		public IFeatureCache FeatureCache
		{
			get
			{
				return this.ifeatureCache_0;
			}
			set
			{
				this.ifeatureCache_0 = value;
			}
		}

		public IFeatureClass FeatureClass
		{
			get
			{
				return this.ifeatureClass_0;
			}
			set
			{
				this.ifeatureClass_0 = value;
			}
		}

		public int GeometryHitType
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public esriGeometryHitPartType HitType
		{
			get
			{
				return esriGeometryHitPartType.esriGeometryPartNone;
			}
			set
			{
			}
		}

		public string Name
		{
			get
			{
				return "正切捕捉";
			}
		}

		public TangentSnapAgent()
		{
		}

		public bool Snap(IGeometry igeometry_0, IPoint ipoint_0, double double_0)
		{
			ILine lineClass;
			double length = 0;
			int num = 0;
			int num1 = 0;
			bool flag = true;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			double num2 = 1000;
			bool flag1 = false;
			object value = Missing.Value;
			IPoint ipoint0 = ipoint_0;
			IPoint point = null;
			point = (!(igeometry_0 is IPoint) ? ((IPointCollection)igeometry_0).Point[0] : (IPoint)igeometry_0);
			for (int i = 0; i < this.ifeatureCache_0.Count; i++)
			{
				IHitTest shape = (IHitTest)this.ifeatureCache_0.Feature[i].Shape;
				if (shape.HitTest(ipoint0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, pointClass, ref length, ref num, ref num1, ref flag))
				{
					IGeometryCollection geometryCollection = shape as IGeometryCollection;
					if (geometryCollection != null)
					{
						ISegmentCollection geometry = geometryCollection.Geometry[num] as ISegmentCollection;
						if (geometry != null)
						{
							ISegment segment = geometry.Segment[num1];
							if (segment is ICircularArc)
							{
								IPoint centerPoint = ((ICircularArc)segment).CenterPoint;
								ILine line = new Line();
								line.PutCoords(point, centerPoint);
								double length1 = line.Length;
								IConstructCircularArc circularArcClass = new CircularArc() as IConstructCircularArc;
								circularArcClass.ConstructCircle(point, length1, true);
								IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
								ISegmentCollection pathClass = new ESRI.ArcGIS.Geometry.Path() as ISegmentCollection;
								pathClass.AddSegment((ISegment)circularArcClass, ref value, ref value);
								polylineClass.AddGeometry((IGeometry)pathClass, ref value, ref value);
								((ITopologicalOperator)polylineClass).Simplify();
								IGeometryCollection polylineClass1 = new Polyline() as IGeometryCollection;
								ISegmentCollection segmentCollection = new Path() as ISegmentCollection;
								segmentCollection.AddSegment(segment, ref value, ref value);
								polylineClass1.AddGeometry((IGeometry)segmentCollection, ref value, ref value);
								((ITopologicalOperator)polylineClass1).Simplify();
								IGeometry geometry1 = ((ITopologicalOperator)polylineClass).Intersect((IGeometry)polylineClass1, esriGeometryDimension.esriGeometry0Dimension);
								if (geometry1 != null)
								{
									if (!(geometry1 is IPointCollection))
									{
										lineClass = new Line();
										lineClass.PutCoords((IPoint)geometry1, ipoint0);
										if (num2 > length)
										{
											num2 = length;
											pointClass = ((IPointCollection)geometry1).Point[0];
											ipoint_0.PutCoords(pointClass.X, pointClass.Y);
											flag1 = true;
										}
									}
									else
									{
										lineClass = new Line();
										ILine lineClass1 = new Line();
										lineClass.PutCoords(((IPointCollection)geometry1).Point[0], ipoint0);
										lineClass1.PutCoords(((IPointCollection)geometry1).Point[1], ipoint0);
										if (lineClass.Length <= lineClass1.Length)
										{
											length = lineClass.Length;
											pointClass = ((IPointCollection)geometry1).Point[0];
										}
										else
										{
											length = lineClass1.Length;
											pointClass = ((IPointCollection)geometry1).Point[1];
										}
										if (num2 > length)
										{
											num2 = length;
											pointClass = ((IPointCollection)geometry1).Point[0];
											ipoint_0.PutCoords(pointClass.X, pointClass.Y);
											flag1 = true;
										}
									}
								}
							}
						}
					}
				}
			}
			return flag1;
		}
	}
}