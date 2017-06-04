using System;
using System.Drawing;
using System.Reflection;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Display
{
	public class NewCircularArcFeedback : IOperation, IDisplayFeedback, INewCircularArcFeedback
	{
		private ISymbol isymbol_0 = new SimpleLineSymbol() as ISymbol;

		private IScreenDisplay iscreenDisplay_0 = null;

		private IPointCollection ipointCollection_0 = new Multipoint();

		private IPoint ipoint_0 = null;

		public bool CanRedo
		{
			get
			{
				return false;
			}
		}

		public bool CanUndo
		{
			get
			{
				return false;
			}
		}

		public IScreenDisplay Display
		{
			set
			{
				this.iscreenDisplay_0 = value;
			}
		}

		public string MenuString
		{
			get
			{
				return null;
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

		public NewCircularArcFeedback()
		{
			this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
		}

		public void AddPoint(IPoint ipoint_1)
		{
			if (this.ipointCollection_0.PointCount != 3)
			{
				object value = Missing.Value;
				this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
			}
		}

		public void Do()
		{
		}

		private void method_0(int int_0)
		{
			object value;
			this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
			if (this.ipointCollection_0.PointCount == 1)
			{
				value = Missing.Value;
				IPointCollection polylineClass = new Polyline();
				polylineClass.AddPoint(this.ipointCollection_0.Point[0], ref value, ref value);
				try
				{
					if (this.ipoint_0 != null)
					{
						polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
						this.isymbol_0.Draw(polylineClass as IGeometry);
					}
				}
				catch (Exception exception)
				{
					exception.ToString();
				}
			}
			else if (this.ipointCollection_0.PointCount == 2)
			{
				ISegmentCollection segmentCollection = new Polyline() as ISegmentCollection;
				ICircularArc circularArc = null;
				value = Missing.Value;
				if (this.ipoint_0 != null)
				{
					circularArc = this.method_1(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], this.ipoint_0);
					segmentCollection.AddSegment(circularArc as ISegment, ref value, ref value);
					this.isymbol_0.Draw(segmentCollection as IGeometry);
				}
			}
			this.isymbol_0.ResetDC();
		}

		private ICircularArc method_1(IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
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

		public void MoveTo(IPoint ipoint_1)
		{
			object value;
			Graphics graphic;
			IntPtr hdc;
			if (this.ipointCollection_0.PointCount != 0)
			{
				if ((this.isymbol_0 == null ? false : this.iscreenDisplay_0 != null))
				{
					if (this.ipointCollection_0.PointCount == 1)
					{
						value = Missing.Value;
						IPointCollection polylineClass = new Polyline();
						polylineClass.AddPoint(this.ipointCollection_0.Point[0], ref value, ref value);
						try
						{
							graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
							hdc = graphic.GetHdc();
							this.isymbol_0.SetupDC(hdc.ToInt32(), this.iscreenDisplay_0.DisplayTransformation);
							if (this.ipoint_0 != null)
							{
								polylineClass.AddPoint(this.ipoint_0, ref value, ref value);
								this.isymbol_0.Draw(polylineClass as IGeometry);
								polylineClass.RemovePoints(1, 1);
							}
							this.ipoint_0 = ipoint_1;
							polylineClass.AddPoint(ipoint_1, ref value, ref value);
							this.isymbol_0.Draw(polylineClass as IGeometry);
							this.isymbol_0.ResetDC();
							graphic.ReleaseHdc(hdc);
							graphic.Dispose();
						}
						catch (Exception exception)
						{
							Logger.Current.Error("", exception, "");
						}
					}
					else if (this.ipointCollection_0.PointCount == 2)
					{
						graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
						hdc = graphic.GetHdc();
						this.isymbol_0.SetupDC(hdc.ToInt32(), this.iscreenDisplay_0.DisplayTransformation);
						ISegmentCollection segmentCollection = new Polyline() as ISegmentCollection;
						ICircularArc circularArc = null;
						value = Missing.Value;
						if (this.ipoint_0 != null)
						{
							circularArc = this.method_1(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], this.ipoint_0);
							segmentCollection.AddSegment(circularArc as ISegment, ref value, ref value);
							this.isymbol_0.Draw(segmentCollection as IGeometry);
						}
						circularArc = this.method_1(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], ipoint_1);
						segmentCollection = null;
						segmentCollection = new Polyline() as ISegmentCollection;
						segmentCollection.AddSegment(circularArc as ISegment, ref value, ref value);
						this.ipoint_0 = ipoint_1;
						this.isymbol_0.Draw(segmentCollection as IGeometry);
						this.isymbol_0.ResetDC();
						graphic.ReleaseHdc(hdc);
						graphic.Dispose();
					}
				}
			}
		}

		public void Redo()
		{
		}

		public void Refresh(int int_0)
		{
			if ((this.isymbol_0 == null ? false : this.iscreenDisplay_0 != null))
			{
				this.method_0(int_0);
			}
		}

		public void Start(IPoint ipoint_1)
		{
			this.ipointCollection_0 = new Multipoint();
			object value = Missing.Value;
			this.ipointCollection_0.AddPoint(ipoint_1, ref value, ref value);
		}

		public ICircularArc Stop()
		{
			ICircularArc circularArc;
			if (this.ipointCollection_0.PointCount != 3)
			{
				circularArc = null;
			}
			else
			{
				circularArc = this.method_1(this.ipointCollection_0.Point[0], this.ipointCollection_0.Point[1], this.ipointCollection_0.Point[2]);
			}
			return circularArc;
		}

		public void Undo()
		{
		}
	}
}