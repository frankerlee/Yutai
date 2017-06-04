using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.Helpers;
using Point = ESRI.ArcGIS.Geometry.Point;

namespace Yutai.ArcGIS.Common.Display
{
	public class NewPolygonFeedbackEx : IOperation, INewPolygonFeedback, IDisplayFeedback, INewPolygonFeedbackEx
	{
		private IPointCollection ipointCollection_0 = null;

		private ISegmentCollection isegmentCollection_0 = null;

		private IGeometryCollection igeometryCollection_0 = null;

		private ISymbol isymbol_0 = new SimpleLineSymbol() as ISymbol;

		private IScreenDisplay iscreenDisplay_0 = null;

		private IPoint ipoint_0 = null;

		private ISymbol isymbol_1 = null;

		private enumLineType enumLineType_0 = enumLineType.LTLine;

		private bool bool_0 = false;

		private short short_0 = 0;

		private IList ilist_0 = new ArrayList();

		private IList ilist_1 = new ArrayList();

		private bool bool_1 = false;

		public bool CanRedo
		{
			get
			{
				return this.ilist_0.Count > 0;
			}
		}

		public bool CanSquareAndFinish
		{
			get
			{
				bool flag;
				flag = (this.enumLineType_0 != enumLineType.LTLine || this.ipointCollection_0.PointCount < 3 ? false : true);
				return flag;
			}
		}

		public bool CanUndo
		{
			get
			{
				bool flag;
				if (this.ipointCollection_0.PointCount > 0)
				{
					flag = true;
				}
				else if (this.isegmentCollection_0.SegmentCount > 0)
				{
					flag = true;
				}
				else if (this.igeometryCollection_0.GeometryCount <= 0)
				{
					flag = (this.ilist_1.Count <= 0 ? false : true);
				}
				else
				{
					flag = true;
				}
				return flag;
			}
		}

		public IScreenDisplay Display
		{
			set
			{
				this.iscreenDisplay_0 = value;
				this.short_0 = this.iscreenDisplay_0.AddCache();
			}
		}

		public string MenuString
		{
			get
			{
				return "";
			}
		}

		public ISymbol Symbol
		{
			get
			{
				return this.isymbol_0;
			}
			set
			{
				if (value is ILineSymbol)
				{
					this.isymbol_0 = value;
				}
			}
		}

		public NewPolygonFeedbackEx()
		{
			(this.isymbol_0 as ILineSymbol).Width = 0.8;
			this.isymbol_1 = new SimpleMarkerSymbol() as ISymbol;
			(this.isymbol_1 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
			(this.isymbol_1 as ISimpleMarkerSymbol).Size = 5;
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = 0,
				Green = 255,
				Blue = 0
			};
			(this.isymbol_1 as ISimpleMarkerSymbol).Color = rgbColorClass;
		}

		public void AddPart(IGeometry igeometry_0)
		{
			this.ilist_0.Clear();
			this.ilist_1.Clear();
			if (this.ipointCollection_0 != null)
			{
				this.method_5();
			}
			else
			{
				this.ipointCollection_0 = new Polygon();
				this.igeometryCollection_0 = new Polygon() as IGeometryCollection;
				this.isegmentCollection_0 = new Polygon() as ISegmentCollection;
			}
			this.ipointCollection_0.AddPointCollection(igeometry_0 as IPointCollection);
		}

		public void AddPoint(IPoint ipoint_1)
		{
			ICircularArc circularArc;
			this.ilist_0.Clear();
			this.ilist_1.Clear();
			object value = Missing.Value;
			this.method_11();
			this.method_13(this.isymbol_0);
			switch (this.enumLineType_0)
			{
				case enumLineType.LTLine:
				{
					this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
					this.method_13(this.isymbol_0);
					this.method_11();
					return;
				}
				case enumLineType.LTCircularArc:
				{
					if (this.ipointCollection_0.PointCount == 2)
					{
						circularArc = this.method_7(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], ipoint_1);
						this.isegmentCollection_0.AddSegment(circularArc as ISegment, ref value, ref value);
						ipoint_1 = this.ipointCollection_0.Point[1];
						this.ipointCollection_0 = new Polygon();
						this.ipoint_0 = null;
						this.bool_0 = false;
					}
					this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
					this.method_13(this.isymbol_0);
					this.method_11();
					return;
				}
				case enumLineType.LTTangentCircularArc:
				{
					circularArc = this.method_6((this.ipointCollection_0 as ISegmentCollection).Segment[0], ipoint_1);
					this.isegmentCollection_0.AddSegment(circularArc as ISegment, ref value, ref value);
					ipoint_1 = circularArc.ToPoint;
					this.ipointCollection_0 = new Polygon();
					this.ipoint_0 = null;
					this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
					this.method_13(this.isymbol_0);
					this.method_11();
					return;
				}
				case enumLineType.LTBezierCurve:
				{
					this.method_13(this.isymbol_0);
					this.method_11();
					return;
				}
				default:
				{
					this.method_13(this.isymbol_0);
					this.method_11();
					return;
				}
			}
		}

		public void ChangeLineType(enumLineType enumLineType_1)
		{
			IPoint point;
			ISegment segment;
			if (enumLineType_1 != this.enumLineType_0)
			{
				if (this.ipointCollection_0 == null)
				{
					this.enumLineType_0 = enumLineType_1;
				}
				else if (this.ipointCollection_0.PointCount != 0)
				{
					Graphics graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
					IntPtr hdc = graphic.GetHdc();
					this.Refresh(hdc.ToInt32());
					object value = Missing.Value;
					switch (this.enumLineType_0)
					{
						case enumLineType.LTLine:
						{
							if (this.ipointCollection_0.PointCount > 1)
							{
								this.isegmentCollection_0.AddSegmentCollection(this.ipointCollection_0 as ISegmentCollection);
							}
							point = this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1];
							this.ipointCollection_0 = new Polygon();
							if (enumLineType_1 != enumLineType.LTTangentCircularArc)
							{
								this.ipointCollection_0.AddPoint(point, ref value, ref value);
								break;
							}
							else
							{
								segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
								(this.ipointCollection_0 as ISegmentCollection).AddSegment(segment, ref value, ref value);
								break;
							}
						}
						case enumLineType.LTCircularArc:
						{
							point = this.ipointCollection_0.Point[0];
							this.ipointCollection_0 = new Polygon();
							this.ipointCollection_0.AddPoint(point, ref value, ref value);
							this.bool_0 = false;
							break;
						}
						case enumLineType.LTTangentCircularArc:
						{
							if ((this.ipointCollection_0 as ISegmentCollection).SegmentCount <= 0)
							{
								this.enumLineType_0 = enumLineType.LTCircularArc;
								point = this.ipointCollection_0.Point[0];
								this.ipointCollection_0 = new Polygon();
								this.ipointCollection_0.AddPoint(point, ref value, ref value);
							}
							else
							{
								segment = (this.ipointCollection_0 as ISegmentCollection).Segment[(this.ipointCollection_0 as ISegmentCollection).SegmentCount - 1];
								this.ipointCollection_0 = new Polygon();
								(this.ipointCollection_0 as ISegmentCollection).AddSegment(segment, ref value, ref value);
							}
							this.bool_0 = false;
							break;
						}
					}
					this.Refresh(hdc.ToInt32());
					graphic.ReleaseHdc(hdc);
					this.enumLineType_0 = enumLineType_1;
				}
				else
				{
					this.enumLineType_0 = enumLineType_1;
				}
			}
		}

		public void CompletePart()
		{
			this.ilist_0.Clear();
			this.method_5();
		}

		public void Do()
		{
		}

		~NewPolygonFeedbackEx()
		{
			this.igeometryCollection_0 = null;
			this.isegmentCollection_0 = null;
			this.ipointCollection_0 = null;
			try
			{
				this.iscreenDisplay_0.RemoveCache(this.short_0);
			}
			catch
			{
			}
		}

		public bool HitTest(IPoint ipoint_1, double double_0, out IPoint ipoint_2)
		{
			bool flag;
			ipoint_2 = new Point() as IPoint;
			bool flag1 = false;
			double num = 0;
			int num1 = -1;
			int num2 = -1;
			if (this.ipointCollection_0.PointCount > 0)
			{
				int num3 = 0;
				while (num3 < this.ipointCollection_0.PointCount)
				{
					IPoint point = this.ipointCollection_0.Point[num3];
					if ((ipoint_1 as IProximityOperator).ReturnDistance(point) < double_0)
					{
						ipoint_2.PutCoords(point.X, point.Y);
						flag = true;
						return flag;
					}
					else
					{
						num3++;
					}
				}
			}
			if ((this.isegmentCollection_0.SegmentCount <= 0 ? true : !(this.isegmentCollection_0 as IHitTest).HitTest(ipoint_1, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_2, ref num, ref num1, ref num2, ref flag1)))
			{
				flag = ((this.igeometryCollection_0.GeometryCount <= 0 ? true : !(this.igeometryCollection_0 as IHitTest).HitTest(ipoint_1, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_2, ref num, ref num1, ref num2, ref flag1)) ? false : true);
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		private void method_0(int int_0)
		{
			IPointCollection polylineClass;
			if (this.ipointCollection_0.PointCount != 0)
			{
				int pointCount = (this.isegmentCollection_0 as IPointCollection).PointCount;
				object value = Missing.Value;
				if (this.ipointCollection_0.PointCount != 1)
				{
					if (this.ipointCollection_0.PointCount > 1)
					{
						polylineClass = new Polyline();
						polylineClass.AddPointCollection(this.ipointCollection_0);
						this.isymbol_0.ROP2 = esriRasterOpCode.esriROPCopyPen;
						this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						this.isymbol_0.ResetDC();
					}
					int num = this.ipointCollection_0.PointCount;
					IPoint point = null;
					IPoint point1 = null;
					if (pointCount > 0)
					{
						point = (this.isegmentCollection_0 as IPointCollection).Point[0];
					}
					else if (num > 1)
					{
						point = this.ipointCollection_0.Point[0];
					}
					point1 = this.ipointCollection_0.Point[num - 1];
					polylineClass = new Polyline();
					this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
					this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
					if (this.ipoint_0 != null)
					{
						polylineClass.AddPoint(point1, ref value, ref value);
						polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
						polylineClass.AddPoint(point, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						polylineClass.RemovePoints(0, 3);
					}
					this.isymbol_0.ResetDC();
				}
				else
				{
					this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
					this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
					if (this.ipoint_0 != null)
					{
						this.ipointCollection_0.AddPoint(this.ipoint_0, ref value, ref value);
						if (pointCount > 0)
						{
							this.ipointCollection_0.AddPoint((this.isegmentCollection_0 as IPointCollection).Point[0], ref value, ref value);
						}
						this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
						if (pointCount <= 0)
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						}
						else
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 2, 2);
						}
					}
					this.isymbol_0.ResetDC();
				}
			}
		}

		private void method_1(IPoint ipoint_1, int int_0)
		{
			IPointCollection polylineClass;
			if (this.ipointCollection_0.PointCount != 0)
			{
				int pointCount = (this.isegmentCollection_0 as IPointCollection).PointCount;
				object value = Missing.Value;
				if (this.ipointCollection_0.PointCount != 1)
				{
					if (this.ipointCollection_0.PointCount > 1)
					{
						polylineClass = new Polyline();
						polylineClass.AddPointCollection(this.ipointCollection_0);
						this.isymbol_0.ROP2 = esriRasterOpCode.esriROPCopyPen;
						this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						this.isymbol_0.ResetDC();
					}
					int num = this.ipointCollection_0.PointCount;
					IPoint point = null;
					IPoint point1 = null;
					if (pointCount > 0)
					{
						point = (this.isegmentCollection_0 as IPointCollection).Point[0];
					}
					else if (num > 1)
					{
						point = this.ipointCollection_0.Point[0];
					}
					point1 = this.ipointCollection_0.Point[num - 1];
					polylineClass = new Polyline();
					this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
					this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
					if (this.ipoint_0 != null)
					{
						polylineClass.AddPoint(point1, ref value, ref value);
						polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
						polylineClass.AddPoint(point, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						polylineClass.RemovePoints(0, 3);
					}
					if (ipoint_1 != null)
					{
						polylineClass.AddPoint(point1, ref value, ref value);
						polylineClass.AddPoint(ipoint_1, ref value, ref value);
						polylineClass.AddPoint(point, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						polylineClass.RemovePoints(0, 3);
					}
					this.ipoint_0 = ipoint_1;
					this.isymbol_0.ResetDC();
				}
				else
				{
					this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
					this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
					if (this.ipoint_0 != null)
					{
						this.ipointCollection_0.AddPoint(this.ipoint_0, ref value, ref value);
						if (pointCount > 0)
						{
							this.ipointCollection_0.AddPoint((this.isegmentCollection_0 as IPointCollection).Point[0], ref value, ref value);
						}
						this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
						if (pointCount <= 0)
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						}
						else
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 2, 2);
						}
					}
					if (ipoint_1 != null)
					{
						this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
						if (pointCount > 0)
						{
							this.ipointCollection_0.AddPoint((this.isegmentCollection_0 as IPointCollection).Point[0], ref value, ref value);
						}
						this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
						if (pointCount <= 0)
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						}
						else
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 2, 2);
						}
					}
					this.isymbol_0.ResetDC();
					this.ipoint_0 = ipoint_1;
				}
			}
		}

		private void method_10()
		{
			object item = this.ilist_1[this.ilist_1.Count - 1];
			this.ilist_0.Add(item);
			this.ilist_1.RemoveAt(this.ilist_1.Count - 1);
			object value = Missing.Value;
			if (item is IPoint)
			{
				if (this.ipointCollection_0.PointCount > 1)
				{
					this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
					this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
				}
				else if (this.ipointCollection_0.PointCount == 1)
				{
					if (this.isegmentCollection_0.SegmentCount <= 0)
					{
						this.ipointCollection_0.RemovePoints(0, 1);
					}
					else
					{
						ISegment segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
						this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
						this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
					}
				}
			}
			else if (item is ICircularArc)
			{
				this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
				this.ipointCollection_0.AddPoint((item as ICurve).FromPoint, ref value, ref value);
			}
			else if (!(item is IList))
			{
				IGeometry geometry = this.igeometryCollection_0.Geometry[this.igeometryCollection_0.GeometryCount - 1];
				this.igeometryCollection_0.RemoveGeometries(this.igeometryCollection_0.GeometryCount - 1, 1);
				this.isegmentCollection_0.AddSegmentCollection(geometry as ISegmentCollection);
			}
			else
			{
				this.UndoToPoint((item as IList)[0] as IPoint);
			}
		}

		private void method_11()
		{
			if (this.iscreenDisplay_0 != null)
			{
				IPoint point = null;
				if (this.ipointCollection_0.PointCount > 0)
				{
					point = this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1];
				}
				else if ((this.isegmentCollection_0 as IPointCollection).PointCount > 0)
				{
					point = (this.isegmentCollection_0 as IPointCollection).Point[(this.isegmentCollection_0 as IPointCollection).PointCount - 1];
				}
				else if ((this.igeometryCollection_0 as IPointCollection).PointCount > 0)
				{
					point = (this.igeometryCollection_0 as IPointCollection).Point[(this.igeometryCollection_0 as IPointCollection).PointCount - 1];
				}
				if (point != null)
				{
					Graphics graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
					IntPtr hdc = graphic.GetHdc();
					this.isymbol_1.ROP2 = esriRasterOpCode.esriROPXOrPen;
					this.isymbol_1.SetupDC(hdc.ToInt32(), this.iscreenDisplay_0.DisplayTransformation);
					this.isymbol_1.Draw(point);
					this.isymbol_1.ResetDC();
					graphic.ReleaseHdc(hdc);
					graphic.Dispose();
				}
			}
		}

		private void method_12(ISymbol isymbol_2)
		{
		}

		private void method_13(ISymbol isymbol_2)
		{
		}

		private void method_2(IPoint ipoint_1)
		{
			IPoint point;
			ISegmentCollection polylineClass;
			ICircularArc circularArc;
			ILine lineClass;
			IPointCollection pointCollection;
			object value = Missing.Value;
			int pointCount = (this.isegmentCollection_0 as IPointCollection).PointCount;
			int num = this.ipointCollection_0.PointCount;
			IPoint point1 = null;
			if (pointCount > 0)
			{
				point1 = (this.isegmentCollection_0 as IPointCollection).Point[0];
			}
			else if (num > 1 && this.bool_0)
			{
				point1 = this.ipointCollection_0.Point[0];
			}
			if (this.enumLineType_0 != enumLineType.LTCircularArc)
			{
				if (this.enumLineType_0 == enumLineType.LTTangentCircularArc)
				{
					if (num != 1)
					{
						polylineClass = new Polyline() as ISegmentCollection;
						circularArc = null;
						if (this.ipoint_0 != null)
						{
							circularArc = this.method_6((this.ipointCollection_0 as ISegmentCollection).Segment[0], this.ipoint_0);
							polylineClass.AddSegment(circularArc as ISegment, ref value, ref value);
							this.isymbol_0.Draw(polylineClass as IGeometry);
							if (pointCount > 0)
							{
								point1 = (this.isegmentCollection_0 as IPointCollection).Point[0];
								IPoint toPoint = circularArc.ToPoint;
								pointCollection = new Polyline();
								pointCollection.AddPoint(this.ipoint_0, ref value, ref value);
								pointCollection.AddPoint(point1, ref value, ref value);
								this.isymbol_0.Draw(pointCollection as IGeometry);
							}
						}
						if (ipoint_1 != null)
						{
							circularArc = this.method_6((this.ipointCollection_0 as ISegmentCollection).Segment[0], ipoint_1);
							polylineClass = null;
							polylineClass = new Polyline() as ISegmentCollection;
                            polylineClass.AddSegment(circularArc as ISegment, ref value, ref value);
							this.isymbol_0.Draw(polylineClass as IGeometry);
							if (pointCount > 0)
							{
								pointCollection = new Polyline();
								IPoint toPoint1 = circularArc.ToPoint;
								pointCollection.AddPoint(ipoint_1, ref value, ref value);
								pointCollection.AddPoint(point1, ref value, ref value);
								this.isymbol_0.Draw(pointCollection as IGeometry);
							}
						}
						this.ipoint_0 = ipoint_1;
					}
					else
					{
						if (this.ipoint_0 != null)
						{
							this.ipointCollection_0.AddPoint(this.ipoint_0, ref value, ref value);
							this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						}
						if (ipoint_1 != null)
						{
							this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
							this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						}
						if (pointCount > 0)
						{
							point1 = (this.isegmentCollection_0 as IPointCollection).Point[0];
							IPoint point2 = this.ipointCollection_0.Point[0];
							pointCollection = new Polyline();
							if (this.ipoint_0 != null)
							{
								pointCollection.AddPoint(this.ipoint_0, ref value, ref value);
								pointCollection.AddPoint(point1, ref value, ref value);
								this.isymbol_0.Draw(pointCollection as IGeometry);
								pointCollection.RemovePoints(0, 2);
							}
							if (ipoint_1 != null)
							{
								pointCollection.AddPoint(ipoint_1, ref value, ref value);
								pointCollection.AddPoint(point1, ref value, ref value);
								this.isymbol_0.Draw(pointCollection as IGeometry);
								pointCollection.RemovePoints(0, 2);
							}
						}
						this.ipoint_0 = ipoint_1;
					}
				}
			}
			else if (num == 1)
			{
				if (this.ipoint_0 != null)
				{
					this.ipointCollection_0.AddPoint(this.ipoint_0, ref value, ref value);
					if (point1 != null)
					{
						this.ipointCollection_0.AddPoint(point1, ref value, ref value);
						point = this.ipointCollection_0.Point[0];
						this.ipointCollection_0.AddPoint(point, ref value, ref value);
					}
					this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
					this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
					if (point1 != null)
					{
						this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 2, 2);
					}
				}
				if (ipoint_1 != null)
				{
					this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
					if (point1 != null)
					{
						this.ipointCollection_0.AddPoint(point1, ref value, ref value);
						point = this.ipointCollection_0.Point[0];
						this.ipointCollection_0.AddPoint(point, ref value, ref value);
					}
					this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
					this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
					if (point1 != null)
					{
						this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 2, 2);
					}
				}
				this.ipoint_0 = ipoint_1;
			}
			else if (num == 2)
			{
				polylineClass = new Polyline() as ISegmentCollection;
                circularArc = null;
				if (this.ipoint_0 != null)
				{
					if (!this.bool_0)
					{
						this.bool_0 = true;
						if (point1 != null)
						{
							this.ipointCollection_0.AddPoint(point1, ref value, ref value);
							point = this.ipointCollection_0.Point[0];
							this.ipointCollection_0.AddPoint(point, ref value, ref value);
						}
						this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
						if (point1 != null)
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 2, 2);
						}
					}
					else
					{
						circularArc = this.method_7(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], this.ipoint_0);
						polylineClass.AddSegment(circularArc as ISegment, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						if (point1 != null)
						{
							polylineClass = new Polyline() as ISegmentCollection;
                            IPoint point3 = this.ipointCollection_0.Point[1];
							lineClass = new Line();
							lineClass.PutCoords(this.ipointCollection_0.Point[1], point1);
							polylineClass.AddSegment(lineClass as ISegment, ref value, ref value);
							this.isymbol_0.Draw(polylineClass as IGeometry);
						}
					}
				}
				if (ipoint_1 != null)
				{
					circularArc = this.method_7(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], ipoint_1);
					polylineClass = null;
					polylineClass = new Polyline() as ISegmentCollection;
                    polylineClass.AddSegment(circularArc as ISegment, ref value, ref value);
					this.isymbol_0.Draw(polylineClass as IGeometry);
					if (point1 == null)
					{
						point1 = this.ipointCollection_0.Point[0];
					}
					if (point1 != null)
					{
						polylineClass = new Polyline() as ISegmentCollection;
                        lineClass = new Line();
						lineClass.PutCoords(this.ipointCollection_0.Point[1], point1);
						polylineClass.AddSegment(lineClass as ISegment, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
					}
				}
				this.ipoint_0 = ipoint_1;
			}
		}

		private void method_3()
		{
			IPointCollection polylineClass;
			ISegmentCollection segmentCollection;
			ICircularArc circularArc;
			object value = Missing.Value;
			int pointCount = (this.isegmentCollection_0 as IPointCollection).PointCount;
			int num = this.ipointCollection_0.PointCount;
			IPoint point = null;
			IPoint toPoint = null;
			if (this.enumLineType_0 != enumLineType.LTCircularArc)
			{
				if (this.enumLineType_0 == enumLineType.LTTangentCircularArc)
				{
					if (num != 1)
					{
						segmentCollection = new Polyline() as ISegmentCollection;
                        circularArc = null;
						if (this.ipoint_0 != null)
						{
							circularArc = this.method_6((this.ipointCollection_0 as ISegmentCollection).Segment[0], this.ipoint_0);
							segmentCollection.AddSegment(circularArc as ISegment, ref value, ref value);
							this.isymbol_0.Draw(segmentCollection as IGeometry);
							if (pointCount > 0)
							{
								point = (this.isegmentCollection_0 as IPointCollection).Point[0];
								toPoint = circularArc.ToPoint;
								polylineClass = new Polyline();
								polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
								polylineClass.AddPoint(point, ref value, ref value);
								this.isymbol_0.Draw(polylineClass as IGeometry);
							}
						}
					}
					else
					{
						if (this.ipoint_0 != null)
						{
							this.ipointCollection_0.AddPoint(this.ipoint_0, ref value, ref value);
							this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						}
						if (pointCount > 0)
						{
							point = (this.isegmentCollection_0 as IPointCollection).Point[0];
							toPoint = this.ipointCollection_0.Point[0];
							polylineClass = new Polyline();
							if (this.ipoint_0 != null)
							{
								polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
								polylineClass.AddPoint(point, ref value, ref value);
								this.isymbol_0.Draw(polylineClass as IGeometry);
								polylineClass.RemovePoints(0, 2);
							}
						}
					}
				}
			}
			else if (num == 1)
			{
				if (this.ipoint_0 != null)
				{
					this.ipointCollection_0.AddPoint(this.ipoint_0, ref value, ref value);
					this.isymbol_0.Draw(this.ipointCollection_0 as IGeometry);
					this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
				}
				if (pointCount > 0)
				{
					point = (this.isegmentCollection_0 as IPointCollection).Point[0];
					toPoint = this.ipointCollection_0.Point[0];
					polylineClass = new Polyline();
					if (this.ipoint_0 != null)
					{
						polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
						polylineClass.AddPoint(point, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						polylineClass.RemovePoints(0, 2);
					}
				}
			}
			else if (num == 2)
			{
				segmentCollection = new Polyline() as ISegmentCollection;
				circularArc = null;
				if (this.ipoint_0 != null)
				{
					circularArc = this.method_7(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], this.ipoint_0);
					segmentCollection.AddSegment(circularArc as ISegment, ref value, ref value);
					this.isymbol_0.Draw(segmentCollection as IGeometry);
				}
				if (pointCount > 0)
				{
					point = (this.isegmentCollection_0 as IPointCollection).Point[0];
					toPoint = this.ipointCollection_0.Point[1];
					polylineClass = new Polyline();
					if (this.ipoint_0 != null)
					{
						polylineClass.AddPoint(toPoint, ref value, ref value);
						polylineClass.AddPoint(point, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
						polylineClass.RemovePoints(0, 2);
					}
				}
			}
		}

		private void method_4()
		{
			if ((this.isymbol_0 == null ? false : this.iscreenDisplay_0 != null))
			{
				Graphics graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
				IntPtr hdc = graphic.GetHdc();
				this.method_8(this.isymbol_0, hdc.ToInt32());
				this.isymbol_0.ResetDC();
				try
				{
					switch (this.enumLineType_0)
					{
						case enumLineType.LTLine:
						{
							this.method_0(hdc.ToInt32());
							break;
						}
						case enumLineType.LTCircularArc:
						case enumLineType.LTTangentCircularArc:
						{
							this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
							this.isymbol_0.SetupDC(hdc.ToInt32(), this.iscreenDisplay_0.DisplayTransformation);
							this.method_3();
							break;
						}
					}
				}
				catch
				{
				}
				this.isymbol_0.ResetDC();
				graphic.ReleaseHdc(hdc);
				graphic.Dispose();
			}
		}

		private void method_5()
		{
			if (this.ipointCollection_0.PointCount != 0)
			{
				switch (this.enumLineType_0)
				{
					case enumLineType.LTLine:
					{
						if (this.ipointCollection_0.PointCount > 1)
						{
							this.isegmentCollection_0.AddSegmentCollection(this.ipointCollection_0 as ISegmentCollection);
						}
						IPoint point = this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1];
						this.ipointCollection_0 = new Polygon();
						this.ipoint_0 = null;
						break;
					}
					case enumLineType.LTCircularArc:
					{
						IPoint point1 = this.ipointCollection_0.Point[0];
						this.ipointCollection_0 = new Polygon();
						this.ipoint_0 = null;
						break;
					}
				}
			}
			if (this.isegmentCollection_0.SegmentCount > 0)
			{
				(this.isegmentCollection_0 as IPolygon).Close();
				this.igeometryCollection_0.AddGeometryCollection(this.isegmentCollection_0 as IGeometryCollection);
				this.isegmentCollection_0 = new Polygon() as ISegmentCollection;
			}
			this.iscreenDisplay_0.Invalidate(null, true, this.iscreenDisplay_0.ActiveCache);
		}

		private ICircularArc method_6(ISegment isegment_0, IPoint ipoint_1)
		{
			ICircularArc circularArc;
			try
			{
				IConstructCircularArc circularArcClass = new CircularArc() as IConstructCircularArc;
				circularArcClass.ConstructTangentAndPoint(isegment_0, false, ipoint_1);
				circularArc = circularArcClass as ICircularArc;
			}
			catch (Exception exception)
			{
				exception.ToString();
				ICircularArc circularArcClass1 = new CircularArc()
				{
					FromPoint = isegment_0.ToPoint,
					ToPoint = ipoint_1
				};
				circularArc = circularArcClass1;
			}
			return circularArc;
		}

		private ICircularArc method_7(IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
		{
			ICircularArc circularArc;
			try
			{
				IConstructCircularArc circularArcClass = new CircularArc() as IConstructCircularArc;
				circularArcClass.ConstructThreePoints(ipoint_1, ipoint_3, ipoint_2, false);
				circularArc = circularArcClass as ICircularArc;
			}
			catch (Exception exception)
			{
				exception.ToString();
				ICircularArc circularArcClass1 = new CircularArc()
				{
					FromPoint = ipoint_1,
					ToPoint = ipoint_2
				};
				circularArc = circularArcClass1;
			}
			return circularArc;
		}

		private void method_8(ISymbol isymbol_2, int int_0)
		{
			this.isymbol_0.ROP2 = esriRasterOpCode.esriROPCopyPen;
			this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
			if (this.igeometryCollection_0.GeometryCount > 0)
			{
				isymbol_2.Draw(this.igeometryCollection_0 as IGeometry);
			}
			if (this.isegmentCollection_0.SegmentCount > 0)
			{
				isymbol_2.Draw(this.isegmentCollection_0 as IGeometry);
			}
			if ((this.ipointCollection_0.PointCount <= 1 ? false : this.enumLineType_0 == enumLineType.LTLine))
			{
				IPointCollection polylineClass = new Polyline();
				polylineClass.AddPointCollection(this.ipointCollection_0);
				this.isymbol_0.Draw(polylineClass as IGeometry);
			}
		}

		private void method_9(IList ilist_2)
		{
			for (int i = ilist_2.Count - 1; i > 0; i--)
			{
				object item = ilist_2[i];
				ilist_2.RemoveAt(i);
				object value = Missing.Value;
				if (item is IPoint)
				{
					this.ipointCollection_0.AddPoint(item as IPoint, ref value, ref value);
				}
				else if (item is ICircularArc)
				{
					if (this.ipointCollection_0.PointCount > 1)
					{
						this.isegmentCollection_0.AddSegmentCollection(this.ipointCollection_0 as ISegmentCollection);
					}
					this.ipointCollection_0.RemovePoints(0, this.ipointCollection_0.PointCount);
					this.isegmentCollection_0.AddSegment(item as ISegment, ref value, ref value);
					this.ipointCollection_0.AddPoint((item as ICircularArc).ToPoint, ref value, ref value);
				}
				else if (item is IRing)
				{
					this.igeometryCollection_0.AddGeometry(item as IGeometry, ref value, ref value);
				}
				else if (!(item is IList))
				{
					this.method_5();
				}
				else
				{
					this.method_9(item as IList);
				}
			}
		}

		public void MoveTo(IPoint ipoint_1)
		{
			if ((this.isymbol_0 == null ? false : this.iscreenDisplay_0 != null))
			{
				Graphics graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
				IntPtr hdc = graphic.GetHdc();
				this.method_8(this.isymbol_0, hdc.ToInt32());
				this.isymbol_0.ResetDC();
				try
				{
					switch (this.enumLineType_0)
					{
						case enumLineType.LTLine:
						{
							this.method_1(ipoint_1, hdc.ToInt32());
							break;
						}
						case enumLineType.LTCircularArc:
						case enumLineType.LTTangentCircularArc:
						{
							this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
							this.isymbol_0.SetupDC(hdc.ToInt32(), this.iscreenDisplay_0.DisplayTransformation);
							this.method_2(ipoint_1);
							break;
						}
					}
				}
				catch
				{
				}
				this.isymbol_0.ResetDC();
				graphic.ReleaseHdc(hdc);
				graphic.Dispose();
			}
		}

		public void Redo()
		{
			if (this.ilist_0.Count > 0)
			{
				object item = this.ilist_0[this.ilist_0.Count - 1];
				this.ilist_1.Add(item);
				this.ilist_0.RemoveAt(this.ilist_0.Count - 1);
				object value = Missing.Value;
				if (item is IPoint)
				{
					this.ipointCollection_0.AddPoint(item as IPoint, ref value, ref value);
				}
				else if (item is ICircularArc)
				{
					if (this.ipointCollection_0.PointCount > 1)
					{
						this.isegmentCollection_0.AddSegmentCollection(this.ipointCollection_0 as ISegmentCollection);
					}
					this.ipointCollection_0.RemovePoints(0, this.ipointCollection_0.PointCount);
					this.isegmentCollection_0.AddSegment(item as ISegment, ref value, ref value);
					this.ipointCollection_0.AddPoint((item as ICircularArc).ToPoint, ref value, ref value);
				}
				else if (!(item is IList))
				{
					this.method_5();
				}
				else
				{
					this.method_9(item as IList);
				}
				this.iscreenDisplay_0.Invalidate(null, true, this.iscreenDisplay_0.ActiveCache);
			}
		}

		public void Refresh(int int_0)
		{
			this.method_4();
			this.method_13(this.isymbol_0);
			this.method_11();
		}

		public IPolygon SquareAndFinish(ISpatialReference ispatialReference_0)
		{
			IPolygon polygon;
			if (this.CanSquareAndFinish)
			{
				ILine lineClass = new Line();
				lineClass.PutCoords(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1]);
				IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
				(pointClass as IConstructPoint).ConstructDeflection(lineClass, 10, 1.5707963267949);
				lineClass.PutCoords(pointClass, this.ipointCollection_0.Point[0]);
				int pointCount = this.ipointCollection_0.PointCount;
				ILine ispatialReference0 = new Line();
				ispatialReference0.PutCoords(this.ipointCollection_0.Point[pointCount - 1], this.ipointCollection_0.Point[pointCount - 2]);
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				(point as IConstructPoint).ConstructDeflection(ispatialReference0, 10, 1.5707963267949);
				ispatialReference0.PutCoords(point, this.ipointCollection_0.Point[pointCount - 1]);
				IConstructLine constructLine = new Line() as IConstructLine;
				lineClass.SpatialReference = ispatialReference_0;
				constructLine.ConstructExtended(lineClass, esriSegmentExtension.esriExtendAtFrom);
				lineClass = constructLine as ILine;
				constructLine = new Line() as IConstructLine;
				constructLine.ConstructExtended(lineClass, esriSegmentExtension.esriExtendAtTo);
				IConstructLine lineClass1 = new Line() as IConstructLine;
				ispatialReference0.SpatialReference = ispatialReference_0;
				lineClass1.ConstructExtended(ispatialReference0, esriSegmentExtension.esriExtendAtFrom);
				ispatialReference0 = lineClass1 as ILine;
				lineClass1 = new Line() as IConstructLine;
				lineClass1.ConstructExtended(ispatialReference0, esriSegmentExtension.esriExtendAtTo);
				IPolyline polylineClass = new Polyline() as IPolyline;
				object value = Missing.Value;
				(polylineClass as ISegmentCollection).AddSegment(constructLine as ISegment, ref value, ref value);
				IPolyline polyline = new Polyline() as IPolyline;
				(polyline as ISegmentCollection).AddSegment(lineClass1 as ISegment, ref value, ref value);
				IGeometry geometry = (polylineClass as ITopologicalOperator).Intersect(polyline, esriGeometryDimension.esriGeometry0Dimension);
				IPoint point1 = null;
				if (geometry is IPoint)
				{
					point1 = geometry as IPoint;
				}
				else if (geometry is IPointCollection && (geometry as IPointCollection).PointCount > 0)
				{
					point1 = (geometry as IPointCollection).Point[0];
				}
				if (point1 != null)
				{
					this.AddPoint(point1);
					polygon = this.Stop();
				}
				else
				{
					polygon = null;
				}
			}
			else
			{
				polygon = null;
			}
			return polygon;
		}

		public void Start(IPoint ipoint_1)
		{
			this.ipointCollection_0 = new Polygon();
			this.igeometryCollection_0 = new Polygon() as IGeometryCollection;
			this.isegmentCollection_0 = new Polygon() as ISegmentCollection;
			object value = Missing.Value;
			this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
			this.method_11();
		}

		public IPolygon Stop()
		{
			if (this.ipointCollection_0.PointCount != 0)
			{
				switch (this.enumLineType_0)
				{
					case enumLineType.LTLine:
					{
						if (this.ipointCollection_0.PointCount <= 1)
						{
							break;
						}
						this.isegmentCollection_0.AddSegmentCollection(this.ipointCollection_0 as ISegmentCollection);
						break;
					}
				}
			}
			if (this.isegmentCollection_0.SegmentCount > 0)
			{
				(this.isegmentCollection_0 as IPolygon).Close();
				this.igeometryCollection_0.AddGeometryCollection(this.isegmentCollection_0 as IGeometryCollection);
				this.isegmentCollection_0 = new Polygon() as ISegmentCollection;
			}
			IPolygon igeometryCollection0 = null;
			igeometryCollection0 = this.igeometryCollection_0 as IPolygon;
			this.ipointCollection_0 = new Polygon();
			this.isegmentCollection_0 = new Polygon() as ISegmentCollection;
			return igeometryCollection0;
		}

		public void Undo()
		{
			object value;
			ISegment segment;
			IGeometry geometry;
			if (this.ilist_1.Count <= 0)
			{
				if (this.ipointCollection_0.PointCount > 1)
				{
					this.ilist_0.Add(this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1]);
					this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
					this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
					return;
				}
				if (this.ipointCollection_0.PointCount == 1)
				{
					if (this.isegmentCollection_0.SegmentCount <= 0)
					{
						this.ilist_0.Add(this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1]);
						this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						if (this.igeometryCollection_0.GeometryCount > 0)
						{
							value = Missing.Value;
							this.ilist_0.Add("part");
							geometry = this.igeometryCollection_0.Geometry[this.igeometryCollection_0.GeometryCount - 1];
							this.igeometryCollection_0.RemoveGeometries(this.igeometryCollection_0.GeometryCount - 1, 1);
							this.isegmentCollection_0.AddSegmentCollection(geometry as ISegmentCollection);
							segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
							if (!(segment is ICircularArc))
							{
								this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
								this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
							}
							else
							{
								this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
								this.ilist_0.Add(segment);
								this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
								this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
							}
						}
					}
					else
					{
						value = Missing.Value;
						segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
						if (!(segment is ICircularArc))
						{
							this.ilist_0.Add(this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1]);
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
							this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
							this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
						}
						else
						{
							this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
							this.ilist_0.Add(segment);
							this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
							this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
						}
					}
					this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
				}
				else if (this.igeometryCollection_0.GeometryCount > 0)
				{
					value = Missing.Value;
					this.ilist_0.Add("part");
					geometry = this.igeometryCollection_0.Geometry[this.igeometryCollection_0.GeometryCount - 1];
					this.igeometryCollection_0.RemoveGeometries(this.igeometryCollection_0.GeometryCount - 1, 1);
					this.isegmentCollection_0.AddSegmentCollection(geometry as ISegmentCollection);
					segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
					if (!(segment is ICircularArc))
					{
						this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
						this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
					}
					else
					{
						this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
						this.ilist_0.Add(segment);
						this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
						this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
					}
					this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
				}
			}
			else
			{
				this.method_10();
			}
			this.method_11();
		}

		public bool UndoToPoint(IPoint ipoint_1)
		{
			int i;
			object value;
			int num;
			int pointCount;
			ISegment segment;
			bool flag;
			bool flag1;
			int num1;
			IHitTest ipointCollection0 = this.ipointCollection_0 as IHitTest;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			bool flag2 = false;
			double num2 = 0;
			int num3 = -1;
			int num4 = -1;
			double mapUnits = CommonHelper.ConvertPixelsToMapUnits(this.iscreenDisplay_0, 10);
			IList arrayLists = new ArrayList();
			arrayLists.Add(ipoint_1);
			if (!(this.ipointCollection_0.PointCount <= 0 ? true : !ipointCollection0.HitTest(ipoint_1, mapUnits, esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num2, ref num3, ref num4, ref flag2)))
			{
				if ((num3 == -1 || num4 == -1 ? false : num4 < this.ipointCollection_0.PointCount))
				{
					for (i = this.ipointCollection_0.PointCount - 1; i > num4; i--)
					{
						arrayLists.Add(this.ipointCollection_0.Point[i]);
					}
					this.ipointCollection_0.RemovePoints(num4 + 1, this.ipointCollection_0.PointCount - num4 - 1);
				}
			}
			else if ((this.isegmentCollection_0.SegmentCount <= 0 ? true : !(this.isegmentCollection_0 as IHitTest).HitTest(ipoint_1, mapUnits, esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num2, ref num3, ref num4, ref flag2)))
			{
				if ((this.igeometryCollection_0.GeometryCount <= 0 ? false : (this.igeometryCollection_0 as IHitTest).HitTest(ipoint_1, mapUnits, esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num2, ref num3, ref num4, ref flag2)))
				{
					goto Label1;
				}
				flag = false;
				return flag;
			}
			else if ((num3 == -1 ? false : num4 != -1))
			{
				for (i = this.ipointCollection_0.PointCount - 1; i >= 0; i--)
				{
					arrayLists.Add(this.ipointCollection_0.Point[i]);
				}
				this.ipointCollection_0.RemovePoints(0, this.ipointCollection_0.PointCount);
				if ((this.isegmentCollection_0 as IPointCollection).PointCount > num4)
				{
					value = Missing.Value;
					num = 0;
					pointCount = (this.isegmentCollection_0 as IPointCollection).PointCount - num4 - 1;
					while (pointCount > 0)
					{
						segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
						if (!(segment is ICircularArc))
						{
							if (this.ipointCollection_0.PointCount <= 0)
							{
								arrayLists.Add(segment.ToPoint);
							}
							else
							{
								arrayLists.Add(this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1]);
								this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
							}
							this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
							this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
						}
						else
						{
							if (this.ipointCollection_0.PointCount > 0)
							{
								this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
							}
							arrayLists.Add(segment);
							this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
							this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
						}
						num++;
						if (num == pointCount)
						{
							if (arrayLists.Count <= 0)
							{
								flag = false;
							}
							else
							{
								num1 = this.ilist_0.Add(arrayLists);
								this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
								flag = true;
							}
							return flag;
						}
					}
				}
			}
			if (arrayLists.Count <= 0)
			{
				flag = false;
			}
			else
			{
				num1 = this.ilist_0.Add(arrayLists);
				this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
				flag = true;
			}
			return flag;
		Label1:
			flag1 = (num3 == -1 ? true : num4 == -1);
			if (!flag1)
			{
				for (i = this.ipointCollection_0.PointCount - 1; i >= 0; i--)
				{
					arrayLists.Add(this.ipointCollection_0.Point[i]);
				}
				if (this.ipointCollection_0.PointCount > 0)
				{
					this.ipointCollection_0.RemovePoints(0, this.ipointCollection_0.PointCount);
				}
				for (i = this.isegmentCollection_0.SegmentCount - 1; i >= 0; i--)
				{
					arrayLists.Add(this.isegmentCollection_0.Segment[i]);
				}
				if (this.isegmentCollection_0.SegmentCount > 0)
				{
					this.isegmentCollection_0.RemoveSegments(0, this.isegmentCollection_0.SegmentCount, false);
				}
				for (i = this.igeometryCollection_0.GeometryCount - 1; i > num3; i--)
				{
					arrayLists.Add(this.igeometryCollection_0.Geometry[i]);
				}
				value = Missing.Value;
				IGeometry geometry = this.igeometryCollection_0.Geometry[num3];
				this.igeometryCollection_0.RemoveGeometries(num3, this.igeometryCollection_0.GeometryCount - num3);
				this.isegmentCollection_0.AddSegmentCollection(geometry as ISegmentCollection);
				arrayLists.Add("part");
				if ((this.isegmentCollection_0 as IPointCollection).PointCount > num4)
				{
					num = 0;
					pointCount = (this.isegmentCollection_0 as IPointCollection).PointCount - num4 - 1;
					do
					{
						if (pointCount <= 0)
						{
							if (arrayLists.Count <= 0)
							{
								flag = false;
							}
							else
							{
								num1 = this.ilist_0.Add(arrayLists);
								this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
								flag = true;
							}
							return flag;
						}
						segment = this.isegmentCollection_0.Segment[this.isegmentCollection_0.SegmentCount - 1];
						if (!(segment is ICircularArc))
						{
							if (this.ipointCollection_0.PointCount <= 0)
							{
								arrayLists.Add(segment.ToPoint);
							}
							else
							{
								arrayLists.Add(this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - 1]);
								this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
							}
							this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
							this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
						}
						else
						{
							if (this.ipointCollection_0.PointCount > 0)
							{
								this.ipointCollection_0.RemovePoints(this.ipointCollection_0.PointCount - 1, 1);
							}
							arrayLists.Add(segment);
							this.isegmentCollection_0.RemoveSegments(this.isegmentCollection_0.SegmentCount - 1, 1, false);
							this.ipointCollection_0.AddPoint(segment.FromPoint, ref value, ref value);
						}
						num++;
					}
					while (num != pointCount);
					if (arrayLists.Count <= 0)
					{
						flag = false;
					}
					else
					{
						num1 = this.ilist_0.Add(arrayLists);
						this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
						flag = true;
					}
					return flag;
				}
				else
				{
					if (arrayLists.Count <= 0)
					{
						flag = false;
					}
					else
					{
						num1 = this.ilist_0.Add(arrayLists);
						this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
						flag = true;
					}
					return flag;
				}
			}
			else
			{
				if (arrayLists.Count <= 0)
				{
					flag = false;
				}
				else
				{
					num1 = this.ilist_0.Add(arrayLists);
					this.iscreenDisplay_0.Invalidate(null, true, this.short_0);
					flag = true;
				}
				return flag;
			}
		}

		public bool UndoToStep(int int_0)
		{
			bool point;
			IPoint point1 = null;
			if (this.ipointCollection_0.PointCount <= int_0)
			{
				int_0 = int_0 - this.ipointCollection_0.PointCount;
				IPointCollection isegmentCollection0 = this.isegmentCollection_0 as IPointCollection;
				if (isegmentCollection0.PointCount <= int_0)
				{
					int_0 = int_0 - isegmentCollection0.PointCount;
					isegmentCollection0 = this.igeometryCollection_0 as IPointCollection;
					if (isegmentCollection0.PointCount <= int_0)
					{
						point = false;
					}
					else
					{
						point1 = isegmentCollection0.Point[isegmentCollection0.PointCount - int_0 - 1];
						point = this.UndoToPoint(point1);
					}
				}
				else
				{
					point1 = isegmentCollection0.Point[isegmentCollection0.PointCount - int_0 - 1];
					point = this.UndoToPoint(point1);
				}
			}
			else
			{
				point1 = this.ipointCollection_0.Point[this.ipointCollection_0.PointCount - int_0 - 1];
				point = this.UndoToPoint(point1);
			}
			return point;
		}
	}
}