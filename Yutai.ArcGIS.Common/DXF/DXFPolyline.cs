using System;
using System.Collections;
using System.Drawing;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFPolyline : DXFFigure
	{
		public int PointCount
		{
			get
			{
				return this.data.count;
			}
		}

		public ArrayList Points
		{
			get
			{
				return this.data.points;
			}
			set
			{
				this.data.points = value;
			}
		}

		public DXFPolyline()
		{
		}

		public DXFPolyline(DXFData aData, int aIndex) : base(aData)
		{
			if (aData.count > 0 && aIndex < aData.count)
			{
				this.Clear();
				this.data.count = this.data.points.Count;
				ArrayList arrayLists = new ArrayList();
				for (int i = 0; i < ((ArrayList)aData.points[aIndex]).Count; i++)
				{
					DXFPoint item = (DXFPoint)((ArrayList)aData.points[aIndex])[i];
					if (item.X < this.data.point.X)
					{
						this.data.point.X = item.X;
					}
					if (item.Y < this.data.point.Y)
					{
						this.data.point.Y = item.Y;
					}
					if (item.X > this.data.point1.X)
					{
						this.data.point1.X = item.X;
					}
					if (item.Y > this.data.point1.Y)
					{
						this.data.point1.Y = item.Y;
					}
					this.Points.Add(item);
				}
			}
		}

		public void AddRect(Rectangle aRect)
		{
			int k;
			if (this.PointCount == 0)
			{
				this.Clear();
				ArrayList arrayLists = new ArrayList();
				DXFPoint dXFPoint = new DXFPoint()
				{
					X = (float)aRect.Left,
					Y = (float)aRect.Top
				};
				arrayLists.Add(dXFPoint);
				dXFPoint.X = (float)aRect.Right;
				dXFPoint.Y = (float)aRect.Top;
				arrayLists.Add(dXFPoint);
				dXFPoint.X = (float)aRect.Right;
				dXFPoint.Y = (float)aRect.Bottom;
				arrayLists.Add(dXFPoint);
				dXFPoint.X = (float)aRect.Left;
				dXFPoint.Y = (float)aRect.Bottom;
				arrayLists.Add(dXFPoint);
				return;
			}
			for (int i = 0; i < this.PointCount - 2; i++)
			{
				for (int j = 1; j < 4; j++)
				{
					DXFPoint dXFPoint1 = new DXFPoint();
					this.Points.Add(dXFPoint1);
				}
				if (((DXFPoint)this.Points[i]).X == (float)aRect.Left && ((DXFPoint)this.Points[i + 1]).X == (float)aRect.Left && (((DXFPoint)this.Points[i]).Y > (float)aRect.Top || ((DXFPoint)this.Points[i + 1]).Y > (float)aRect.Bottom) || ((DXFPoint)this.Points[i]).Y >= (float)aRect.Top && ((DXFPoint)this.Points[i + 1]).Y >= (float)aRect.Bottom)
				{
					for (k = i + 1; k < this.PointCount - 1; k++)
					{
						this.Points[this.PointCount - 1 + 4 - (k - i - 1)] = (DXFPoint)this.Points[this.PointCount - 1 - (k - i - 1)];
					}
					((DXFPoint)this.Points[i + 1]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 1]).Y = (float)aRect.Bottom;
					((DXFPoint)this.Points[i + 2]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 2]).Y = (float)aRect.Bottom;
					((DXFPoint)this.Points[i + 3]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 3]).Y = (float)aRect.Top;
					((DXFPoint)this.Points[i + 4]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 4]).Y = (float)aRect.Top;
					return;
				}
				if (((DXFPoint)this.Points[i]).Y == (float)aRect.Top && ((DXFPoint)this.Points[i + 1]).Y == (float)aRect.Top && (((DXFPoint)this.Points[i]).X > (float)aRect.Left || ((DXFPoint)this.Points[i + 1]).X > (float)aRect.Right) || ((DXFPoint)this.Points[i]).X >= (float)aRect.Left && ((DXFPoint)this.Points[i + 1]).X >= (float)aRect.Right)
				{
					for (k = i + 1; k < this.PointCount - 1; k++)
					{
						this.Points[this.PointCount - 1 + 4 - (k - i - 1)] = (DXFPoint)this.Points[this.PointCount - 1 - (k - i - 1)];
					}
					((DXFPoint)this.Points[i + 1]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 1]).Y = (float)aRect.Top;
					((DXFPoint)this.Points[i + 2]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 2]).Y = (float)aRect.Bottom;
					((DXFPoint)this.Points[i + 3]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 3]).Y = (float)aRect.Bottom;
					((DXFPoint)this.Points[i + 4]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 4]).Y = (float)aRect.Top;
					return;
				}
				if (((DXFPoint)this.Points[i]).X == (float)aRect.Right && ((DXFPoint)this.Points[i + 1]).X == (float)aRect.Right && (((DXFPoint)this.Points[i]).Y > (float)aRect.Top || ((DXFPoint)this.Points[i + 1]).Y > (float)aRect.Bottom) || ((DXFPoint)this.Points[i]).Y >= (float)aRect.Top && ((DXFPoint)this.Points[i]).Y >= (float)aRect.Bottom)
				{
					for (k = i + 1; k < this.data.count - 1; k++)
					{
						this.Points[this.PointCount - 1 + 4 - (k - i - 1)] = (DXFPoint)this.Points[this.PointCount - 1 - (k - i - 1)];
					}
					((DXFPoint)this.Points[i + 1]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 1]).Y = (float)aRect.Top;
					((DXFPoint)this.Points[i + 2]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 2]).Y = (float)aRect.Top;
					((DXFPoint)this.Points[i + 3]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 3]).Y = (float)aRect.Bottom;
					((DXFPoint)this.Points[i + 4]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 4]).Y = (float)aRect.Bottom;
					return;
				}
				if (((DXFPoint)this.Points[i]).Y == (float)aRect.Bottom && ((DXFPoint)this.Points[i + 1]).Y == (float)aRect.Bottom && (this.min(((DXFPoint)this.Points[i]).X, ((DXFPoint)this.Points[i + 1]).X) > (float)aRect.Left || this.min(((DXFPoint)this.Points[i]).X, ((DXFPoint)this.Points[i + 1]).X) < (float)aRect.Right) || this.max(((DXFPoint)this.Points[i]).X, ((DXFPoint)this.Points[i + 1]).X) <= (float)aRect.Left && this.max(((DXFPoint)this.Points[i]).X, ((DXFPoint)this.Points[i + 1]).X) <= (float)aRect.Right)
				{
					for (k = i + 1; k < this.PointCount - 1; k++)
					{
						this.Points[this.PointCount - 1 + 4 - (k - i - 1)] = (DXFPoint)this.Points[this.PointCount - 1 - (k - i - 1)];
					}
					((DXFPoint)this.Points[i + 1]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 1]).Y = (float)aRect.Bottom;
					((DXFPoint)this.Points[i + 2]).X = (float)aRect.Left;
					((DXFPoint)this.Points[i + 2]).Y = (float)aRect.Top;
					((DXFPoint)this.Points[i + 3]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 3]).Y = (float)aRect.Top;
					((DXFPoint)this.Points[i + 4]).X = (float)aRect.Right;
					((DXFPoint)this.Points[i + 4]).Y = (float)aRect.Bottom;
					return;
				}
			}
			if (this.Points != null)
			{
				Rectangle rectangle = new Rectangle();
				rectangle = aRect;
				this.Points.Add(rectangle);
			}
		}

		public void BeginPolygonFromRect()
		{
			ArrayList arrayLists = new ArrayList();
		}

		public void Clear()
		{
			this.data.points.Clear();
		}

		public void EndPolygonFromRect()
		{
			if (this.Points != null)
			{
				for (int i = this.Points.Count - 1; i > 0; i--)
				{
					this.AddRect((Rectangle)this.Points[i]);
				}
				this.Points.Clear();
			}
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			ADXFExport.BeginPoly(this.data, this.data.points.Count);
			for (int i = 0; i < this.data.points.Count; i++)
			{
				ADXFExport.AddVertex((DXFPoint)this.data.points[i]);
			}
		}

		private DXFPoint GetPoint(int aIndex)
		{
			return (DXFPoint)this.data.points[aIndex];
		}

		public override bool IntersecRect(Rect aRect)
		{
			Rect x1 = new Rect()
			{
				X1 = (int)Math.Round((double)this.data.point.X),
				Y1 = (int)Math.Round((double)this.data.point.Y),
				X2 = (int)Math.Round((double)this.data.point1.X),
				Y2 = (int)Math.Round((double)this.data.point1.Y)
			};
			if (x1.X2 < x1.X1)
			{
				int x2 = x1.X2;
				x1.X2 = x1.X1;
				x1.X1 = x2;
			}
			if (x1.Y2 < x1.Y1)
			{
				int y2 = x1.Y2;
				x1.Y2 = x1.Y1;
				x1.Y1 = y2;
			}
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			rectangle = Rectangle.Intersect(new Rectangle(aRect.X1, aRect.Y1, aRect.X2, aRect.Y2), new Rectangle(x1.X1, x1.Y1, x1.X2, x1.Y2));
			if (rectangle.X <= 0 && rectangle.Y <= 0 && rectangle.Width <= 0 && rectangle.Height <= 0)
			{
				return false;
			}
			return true;
		}

		private float max(float aPar1, float aPar2)
		{
			if (aPar1 > aPar2)
			{
				return aPar1;
			}
			return aPar2;
		}

		private float min(float aPar1, float aPar2)
		{
			if (aPar1 > aPar2)
			{
				return aPar2;
			}
			return aPar1;
		}

		public override void ParseToLines(ArrayList NewElemes)
		{
			DXFLine dXFLine = new DXFLine(this.data);
			for (int i = 0; i < this.PointCount - 1; i++)
			{
				dXFLine.Layer = base.Layer;
				dXFLine.StartPoint = (DXFPoint)this.GetPoint(i).Clone();
				if (i >= this.PointCount - 1)
				{
					dXFLine.EndPoint = (DXFPoint)this.GetPoint(0).Clone();
				}
				else
				{
					dXFLine.EndPoint = (DXFPoint)this.GetPoint(i + 1).Clone();
				}
				NewElemes.Add(dXFLine.Clone());
			}
		}
	}
}