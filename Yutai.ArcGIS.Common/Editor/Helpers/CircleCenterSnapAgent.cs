using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class CircleCenterSnapAgent : IFeatureSnapAgent, IEngineSnapAgent
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
				return "圆心捕捉";
			}
		}

		public CircleCenterSnapAgent()
		{
		}

		public bool Snap(IGeometry igeometry_0, IPoint ipoint_0, double double_0)
		{
			bool flag;
			if (EditorLicenseProviderCheck.Check())
			{
				IPoint ipoint0 = ipoint_0;
				double length = 1000;
				bool flag1 = false;
				ILine lineClass = new Line();
				for (int i = 0; i < this.ifeatureCache_0.Count; i++)
				{
					IGeometry shape = this.ifeatureCache_0.Feature[i].Shape;
					if (shape is ISegmentCollection)
					{
						IHitTest hitTest = (IHitTest)shape;
						if ((hitTest is IPolyline ? true : hitTest is IPolygon))
						{
							double num = 0;
							int num1 = 0;
							int num2 = 0;
							bool flag2 = true;
							IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
							if (hitTest.HitTest(ipoint_0, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, pointClass, ref num, ref num1, ref num2, ref flag2))
							{
								ISegment segment = ((shape as IGeometryCollection).Geometry[num1] as ISegmentCollection).Segment[num2];
								if (segment is ICircularArc)
								{
									pointClass = ((ICircularArc)segment).CenterPoint;
									lineClass.PutCoords(ipoint0, pointClass);
									if (!flag1)
									{
										length = lineClass.Length;
										ipoint_0.PutCoords(pointClass.X, pointClass.Y);
										flag1 = true;
									}
									else if (length > lineClass.Length)
									{
										length = lineClass.Length;
										ipoint_0.PutCoords(pointClass.X, pointClass.Y);
										flag1 = true;
									}
								}
							}
						}
					}
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