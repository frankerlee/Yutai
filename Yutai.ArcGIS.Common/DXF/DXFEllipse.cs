using System;
using System.Collections;
using System.Drawing;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFEllipse : DXFFigure
	{
		private bool ellipse;

		public DXFPoint LeftTop
		{
			get
			{
				if (this.ellipse)
				{
					return (DXFPoint)this.data.point.Clone();
				}
				return new DXFPoint(this.data.point.X - this.data.radius, this.data.point.Y - this.data.radius, 0f);
			}
		}

		public DXFPoint PCenter
		{
			get
			{
				return (DXFPoint)this.data.point.Clone();
			}
			set
			{
				this.data.point = (DXFPoint)value.Clone();
			}
		}

		public DXFPoint RightBottom
		{
			get
			{
				if (!this.ellipse)
				{
					return new DXFPoint(this.data.point.X + this.data.radius, this.data.point.Y + this.data.radius, 0f);
				}
				return new DXFPoint(this.data.point.X + this.data.scale.X * 10f, this.data.point.Y - this.data.scale.Y * 10f, 0f);
			}
		}

		public DXFEllipse()
		{
			this.ellipse = false;
		}

		public DXFEllipse(DXFData aData, bool aEllipse) : base(aData)
		{
			this.ellipse = false;
			this.ellipse = aEllipse;
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			if (!this.ellipse)
			{
				ADXFExport.AddName("CIRCLE", "AcDbCircle");
				ADXFExport.AddColor(this.data);
				ADXFExport.AddThickness(this.data);
				ADXFExport.Add3DPoint(10, this.data.point);
				ADXFExport.AddFloat(40, ADXFExport.MM(this.data.radius));
				return;
			}
			ADXFExport.AddName("ELLIPSE", "AcDbEllipse");
			ADXFExport.AddColor(this.data);
			ADXFExport.AddThickness(this.data);
			ADXFExport.Add3DPoint(10, this.data.point);
			ADXFExport.Add3DPoint(11, this.data.point1);
			ADXFExport.AddFloat(40, this.data.radius);
		}

		public override bool IntersecRect(Rect aRect)
		{
			float single;
			float single1;
			if (this.ellipse)
			{
				single = Math.Abs((float)(this.data.point1.X - this.data.point.X));
				single1 = Math.Abs((float)(this.data.point1.Y - this.data.point.Y));
				if (single == 0f)
				{
					single = single1 * this.data.radius;
				}
				if (single1 == 0f)
				{
					single1 = single * this.data.radius;
				}
			}
			else
			{
				single = this.data.radius;
				single1 = single;
			}
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			rectangle = Rectangle.Intersect(new Rectangle(aRect.X1, aRect.Y1, aRect.X2, aRect.Y2), new Rectangle((int)Math.Round((double)(this.data.point.X - single)), (int)Math.Round((double)(this.data.point.Y - single1)), (int)Math.Round((double)(this.data.point.X + single)), (int)Math.Round((double)(this.data.point.Y + single1))));
			if (rectangle.X <= 0 && rectangle.Y <= 0 && rectangle.Width <= 0 && rectangle.Height <= 0)
			{
				return false;
			}
			return true;
		}

		public override void ParseToLines(ArrayList NewElemes)
		{
			float single;
			float single1;
			DXFPoint dXFPoint = new DXFPoint();
			DXFLine dXFLine = new DXFLine();
			float x = this.data.point.X;
			float y = this.data.point.Y;
			if (this.ellipse)
			{
				single = Math.Abs(this.data.point1.X);
				single1 = Math.Abs(this.data.point1.Y);
				if (single == 0f)
				{
					single = single1 * this.data.radius;
				}
				if (single1 == 0f)
				{
					single1 = single * this.data.radius;
				}
			}
			else
			{
				single = this.data.radius;
				single1 = single;
			}
			float single2 = this.data.startAngle * 3.141593f / 180f;
			float single3 = this.data.endAngle * 3.141593f / 180f;
			int num = (int)Math.Round((double)((single3 - single2) / 3.141593f * 16f));
			if (num < 4)
			{
				num = 4;
			}
			float single4 = (single3 - single2) / (float)(num - 1);
			for (int i = 0; i < num - 2; i++)
			{
				dXFLine.Layer = base.Layer;
				float single5 = (float)Math.Sin((double)single2);
				float single6 = (float)Math.Cos((double)single2);
				dXFPoint.X = x + single * single6;
				dXFPoint.Y = y + single1 * single5;
				dXFLine.StartPoint = (DXFPoint)dXFPoint.Clone();
				single2 = single2 + single4;
				single5 = (float)Math.Sin((double)single2);
				single6 = (float)Math.Cos((double)single2);
				dXFPoint.X = x + single * single6;
				dXFPoint.Y = y + single1 * single5;
				dXFLine.EndPoint = (DXFPoint)dXFPoint.Clone();
				NewElemes.Add(dXFLine.Clone());
			}
		}

		public void SetEllipse()
		{
			if (this.data.point1.X > this.data.point1.Y)
			{
				this.data.point1.X = this.data.radius;
				this.data.radius = this.data.point1.Y;
				this.data.point1.Y = 0f;
				return;
			}
			this.data.point1.Y = this.data.radius;
			this.data.radius = this.data.point1.X;
			this.data.point1.X = 0f;
		}
	}
}