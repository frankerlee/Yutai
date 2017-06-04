using System.Collections;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class ConstructPolygonHepler
	{
		private IMap imap_0 = null;

		private bool bool_0 = false;

		private ISegmentGraph isegmentGraph_0 = new SegmentGraph();

		public IMap FocusMap
		{
			set
			{
				this.imap_0 = value;
			}
		}

		public ConstructPolygonHepler()
		{
		}

		public IPolygon BulidPoly(IArray iarray_0, int[] int_0, IGeometry igeometry_0, bool bool_1)
		{
			IPolygon polygon;
			bool flag;
			ISegmentCollection polygonClass = new Polygon() as ISegmentCollection;
			polygonClass.AddSegmentCollection((ISegmentCollection)(igeometry_0 as IClone).Clone());
			this.method_1(iarray_0, igeometry_0);
			bool flag1 = true;
			IGeometry igeometry0 = igeometry_0;
			while (true)
			{
				igeometry0 = (!flag1 ? this.method_4(iarray_0, igeometry0, ((IPolyline)igeometry0).FromPoint, bool_1, ref flag1) : this.method_4(iarray_0, igeometry0, ((IPolyline)igeometry0).ToPoint, bool_1, ref flag1));
				if (igeometry0 == null)
				{
					polygon = null;
					break;
				}
				else if (igeometry_0 == igeometry0)
				{
					((IPolygon)polygonClass).Close();
					double area = ((IArea)polygonClass).Area;
					if (area <= 0 || bool_1)
					{
						flag = (area >= 0 ? true : !bool_1);
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						polygon = null;
						break;
					}
					else
					{
						((ITopologicalOperator)polygonClass).Simplify();
						((IPolygon)polygonClass).Close();
						polygon = (IPolygon)polygonClass;
						break;
					}
				}
				else
				{
					ICurve curve = (ICurve)((IClone)igeometry0).Clone();
					int num = this.method_1(iarray_0, igeometry0);
					if (!flag1)
					{
						if (!bool_1)
						{
							int_0[num] = int_0[num] | 1;
						}
						else
						{
							int_0[num] = int_0[num] | 2;
						}
						curve.ReverseOrientation();
					}
					else if (!bool_1)
					{
						int_0[num] = int_0[num] | 2;
					}
					else
					{
						int_0[num] = int_0[num] | 1;
					}
					polygonClass.AddSegmentCollection((ISegmentCollection)curve);
				}
			}
			return polygon;
		}

		public IGeometryBag ConstructPolygonByLine(IGeometryBag igeometryBag_0)
		{
			int i;
			IPolygon polygon;
			IGeometryCollection geometryBagClass = new GeometryBag() as IGeometryCollection;
			object value = Missing.Value;
			IArray array = this.SplitLine(igeometryBag_0);
			int[] numArray = new int[array.Count];
			IArray arrayClass = new Array();
			for (i = 0; i < array.Count; i++)
			{
				numArray[i] = 0;
			}
			IGeometry element = null;
			IGeometry geometry = null;
			for (i = 0; i < array.Count; i++)
			{
				element = array.Element[i] as IGeometry;
				IPolyline polyline = (IPolyline)element;
				geometry = element;
				if (this.method_3(array, polyline.FromPoint) != 1 && this.method_3(array, polyline.ToPoint) != 1)
				{
					if (!polyline.IsClosed)
					{
						if ((numArray[i] & 2) == 0)
						{
							numArray[i] = numArray[i] | 2;
							polygon = this.BulidPoly(array, numArray, geometry, false);
							if (polygon != null)
							{
								geometryBagClass.AddGeometry(polygon, ref value, ref value);
							}
						}
						if ((numArray[i] & 1) == 0)
						{
							numArray[i] = numArray[i] | 1;
							polygon = this.BulidPoly(array, numArray, geometry, true);
							if (polygon != null)
							{
								geometryBagClass.AddGeometry(polygon, ref value, ref value);
							}
						}
					}
					else
					{
						ISegmentCollection polygonClass = new Polygon() as ISegmentCollection;
						polygonClass.AddSegmentCollection((ISegmentCollection)polyline);
						polygon = (IPolygon)polygonClass;
						((ITopologicalOperator)polygon).Simplify();
						geometryBagClass.AddGeometry(polygon, ref value, ref value);
					}
				}
			}
			return (IGeometryBag)geometryBagClass;
		}

		private void method_0(IArray iarray_0, IGeometry igeometry_0)
		{
			IRelationalOperator igeometry0 = igeometry_0 as IRelationalOperator;
			IList arrayLists = new ArrayList();
			int i = 0;
			while (true)
			{
				if (i < iarray_0.Count)
				{
					IGeometry element = iarray_0.Element[i] as IGeometry;
					if (igeometry0.Within(element))
					{
						break;
					}
					if (igeometry0.Contains(element))
					{
						arrayLists.Add(i);
					}
					i++;
				}
				else
				{
					for (i = arrayLists.Count - 1; i >= 0; i--)
					{
						iarray_0.Remove(i);
					}
					iarray_0.Add(igeometry_0);
					break;
				}
			}
		}

		private int method_1(IArray iarray_0, IGeometry igeometry_0)
		{
			int num;
			int num1 = 0;
			while (true)
			{
				if (num1 >= iarray_0.Count)
				{
					num = -1;
					break;
				}
				else if (igeometry_0 == iarray_0.Element[num1])
				{
					num = num1;
					break;
				}
				else
				{
					num1++;
				}
			}
			return num;
		}

		private void method_2()
		{
			this.bool_0 = false;
			IEnumFeature featureSelection = this.imap_0.FeatureSelection as IEnumFeature;
			IGeometryCollection geometryBagClass = new GeometryBag() as IGeometryCollection;
			featureSelection.Reset();
			for (IFeature i = featureSelection.Next(); i != null; i = featureSelection.Next())
			{
				esriGeometryType geometryType = i.Shape.GeometryType;
				if ((geometryType == esriGeometryType.esriGeometryPolygon ? true : geometryType == esriGeometryType.esriGeometryPolyline))
				{
					IClone shape = i.Shape as IClone;
					object value = Missing.Value;
					geometryBagClass.AddGeometry(shape.Clone() as IGeometry, ref value, ref value);
					this.bool_0 = true;
				}
			}
			this.isegmentGraph_0.SetEmpty();
			this.isegmentGraph_0.Load(geometryBagClass as IEnumGeometry, false, true);
		}

		private int method_3(IArray iarray_0, IPoint ipoint_0)
		{
			int num = 0;
			IRelationalOperator ipoint0 = (IRelationalOperator)ipoint_0;
			for (int i = 0; i < iarray_0.Count; i++)
			{
				if (ipoint0.Touches((IGeometry)iarray_0.Element[i]))
				{
					num++;
				}
			}
			return num;
		}

		private IGeometry method_4(IArray iarray_0, IGeometry igeometry_0, IPoint ipoint_0, bool bool_1, ref bool bool_2)
		{
			IGeometry geometry;
			IGeometry geometry1;
			IRelationalOperator ipoint0 = (IRelationalOperator)ipoint_0;
			IGeometryCollection geometryBagClass = new GeometryBag() as IGeometryCollection; 
			object value = Missing.Value;
			for (int i = 0; i < iarray_0.Count; i++)
			{
				if (!((IPolyline)iarray_0.Element[i]).IsClosed && ipoint0.Touches((IGeometry)iarray_0.Element[i]))
				{
					geometryBagClass.AddGeometry((IGeometry)iarray_0.Element[i], ref value, ref value);
				}
			}
			int geometryCount = geometryBagClass.GeometryCount;
			if (geometryCount > 1)
			{
				geometryBagClass = this.method_5(geometryBagClass, !bool_1, ipoint_0);
				int num = 0;
				while (true)
				{
					geometry1 = geometryBagClass.Geometry[num];
					if (igeometry_0 == geometry1)
					{
						break;
					}
					num++;
					if (num == geometryCount)
					{
						num = 0;
					}
				}
				do
				{
					num++;
					if (num == geometryCount)
					{
						num = 0;
					}
					geometry1 = geometryBagClass.Geometry[num];
					if (CommonHelper.distance(((ICurve)geometry1).FromPoint, ipoint_0) >= CommonHelper.distance(((ICurve)geometry1).ToPoint, ipoint_0))
					{
						if (this.method_3(iarray_0, ((ICurve)geometry1).FromPoint) <= 1)
						{
							continue;
						}
						bool_2 = false;
						geometry = geometry1;
						return geometry;
					}
					else
					{
						if (this.method_3(iarray_0, ((ICurve)geometry1).ToPoint) <= 1)
						{
							continue;
						}
						bool_2 = true;
						geometry = geometry1;
						return geometry;
					}
				}
				while (igeometry_0 != geometry1);
				geometry = null;
			}
			else
			{
				geometry = null;
			}
			return geometry;
		}

		private IGeometryCollection method_5(IGeometryCollection igeometryCollection_0, bool bool_1, IPoint ipoint_0)
		{
			int i;
			object value = Missing.Value;
			int geometryCount = igeometryCollection_0.GeometryCount;
			double[] numArray = new double[geometryCount];
			double[] numArray1 = new double[geometryCount - 1];
			for (i = 0; i < geometryCount; i++)
			{
				ICurve geometry = (ICurve)igeometryCollection_0.Geometry[i];
				ILine lineClass = new Line();
				IPointCollection pointCollection = (IPointCollection)geometry;
				if (CommonHelper.distance(geometry.FromPoint, ipoint_0) >= CommonHelper.distance(geometry.ToPoint, ipoint_0))
				{
					numArray[i] = CommonHelper.azimuth(pointCollection.Point[pointCollection.PointCount - 1], pointCollection.Point[pointCollection.PointCount - 2]);
				}
				else
				{
					numArray[i] = CommonHelper.azimuth(pointCollection.Point[0], pointCollection.Point[1]);
				}
			}
			for (i = 1; i < geometryCount; i++)
			{
				numArray1[i - 1] = numArray[i] - numArray[0];
				if (numArray1[i - 1] < 0)
				{
					numArray1[i - 1] = numArray1[i - 1] + 360;
				}
			}
			IGeometryCollection geometryBagClass = new GeometryBag() as IGeometryCollection; 
			while (geometryBagClass.GeometryCount != geometryCount)
			{
				double num = numArray[0];
				int num1 = 0;
				for (i = 1; i < geometryCount; i++)
				{
					if (num > numArray[i])
					{
						num = numArray[i];
						num1 = i;
					}
				}
				numArray[num1] = numArray[num1] + 363;
				geometryBagClass.AddGeometry(igeometryCollection_0.Geometry[num1], ref value, ref value);
			}
			if (!bool_1)
			{
				IGeometryCollection geometryCollection = new GeometryBag() as IGeometryCollection;
				geometryCollection.AddGeometry(geometryBagClass.Geometry[0], ref value, ref value);
				for (i = geometryCount - 1; i > 0; i--)
				{
					geometryCollection.AddGeometry(geometryBagClass.Geometry[i], ref value, ref value);
				}
				geometryBagClass = geometryCollection;
			}
			return geometryBagClass;
		}

		protected IArray SplitLine(IGeometryBag igeometryBag_0)
		{
			int i;
			IPolyline geometry;
			int j;
			IPointCollection pointCollection;
			IGeometryCollection igeometryBag0 = (IGeometryCollection)igeometryBag_0;
			IPointCollection[] multipointClass = new IPointCollection[igeometryBag0.GeometryCount];
			for (i = 0; i < igeometryBag0.GeometryCount; i++)
			{
				multipointClass[i] = new Multipoint();
			}
			for (i = 0; i < igeometryBag0.GeometryCount; i++)
			{
				geometry = igeometryBag0.Geometry[i] as IPolyline;
				if (geometry != null)
				{
					for (j = i + 1; j < igeometryBag0.GeometryCount; j++)
					{
						IPolyline polyline = igeometryBag0.Geometry[j] as IPolyline;
						if (polyline != null)
						{
							IRelationalOperator relationalOperator = (IRelationalOperator)geometry;
							ITopologicalOperator topologicalOperator = (ITopologicalOperator)geometry;
							if (relationalOperator.Crosses(polyline))
							{
								IGeometry geometry1 = topologicalOperator.Intersect(polyline, esriGeometryDimension.esriGeometry0Dimension);
								if (geometry1 != null && geometry1 is IPointCollection)
								{
									pointCollection = multipointClass[i];
									pointCollection.AddPointCollection((IPointCollection)geometry1);
									pointCollection = multipointClass[j];
									pointCollection.AddPointCollection((IPointCollection)geometry1);
								}
							}
						}
					}
				}
			}
			IArray arrayClass = new Array();
			try
			{
				for (i = 0; i < igeometryBag0.GeometryCount; i++)
				{
					geometry = igeometryBag0.Geometry[i] as IPolyline;
					if (geometry != null)
					{
						pointCollection = multipointClass[i];
						if (pointCollection.PointCount != 0)
						{
							((IPolycurve2)geometry).SplitAtPoints(pointCollection.EnumVertices, true, true, -1);
							IGeometryCollection geometryCollection = (IGeometryCollection)geometry;
							for (j = 0; j < geometryCollection.GeometryCount; j++)
							{
								IGeometry geometry2 = geometryCollection.Geometry[j];
								IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
								polylineClass.AddGeometries(1, ref geometry2);
								if (!((ITopologicalOperator)polylineClass).IsSimple)
								{
									((ITopologicalOperator)polylineClass).Simplify();
								}
								this.method_0(arrayClass, polylineClass as IGeometry);
							}
						}
						else if (geometry.IsClosed)
						{
							this.method_0(arrayClass, geometry);
						}
					}
				}
			}
			catch
			{
			}
			return arrayClass;
		}

		internal class TopologyEdge
		{
			private IPolyline ipolyline_0;

			internal TopologyEdge(IGeometry igeometry_0)
			{
				this.ipolyline_0 = igeometry_0 as IPolyline;
			}
		}

		internal class TopologyNode
		{
			private IPoint ipoint_0;

			internal TopologyNode(IPoint ipoint_1)
			{
				this.ipoint_0 = ipoint_1;
			}
		}
	}
}