using System;
using System.Collections;
using System.Drawing;

namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFRectangle : DXFFigure
    {
        public DXFPoint LeftTop
        {
            get { return new DXFPoint(this.data.point1.X, this.data.point.Y, 0f); }
            set
            {
                this.data.point1.X = value.X;
                this.data.point.Y = value.Y;
            }
        }

        public DXFPoint RightBottom
        {
            get { return new DXFPoint(this.data.point.X, this.data.point1.Y, 0f); }
            set
            {
                this.data.point.X = value.X;
                this.data.point1.Y = value.Y;
            }
        }

        public DXFRectangle()
        {
        }

        public DXFRectangle(DXFData aData) : base(aData)
        {
        }

        public override void ExportAsDXF(DXFExport ADXFExport)
        {
            ADXFExport.BeginPoly(this.data, 4);
            ADXFExport.AddVertex(this.data.point);
            ADXFExport.AddVertex(new DXFPoint(this.data.point1.X, this.data.point.Y, 0f));
            ADXFExport.AddVertex(this.data.point1);
            ADXFExport.AddVertex(new DXFPoint(this.data.point.X, this.data.point1.Y, 0f));
            ADXFExport.AddVertex(this.data.point);
        }

        public override bool IntersecRect(Rect aRect)
        {
            Rect x1 = new Rect()
            {
                X1 = (int) Math.Round((double) this.data.point.X),
                Y1 = (int) Math.Round((double) this.data.point.Y),
                X2 = (int) Math.Round((double) this.data.point1.X),
                Y2 = (int) Math.Round((double) this.data.point1.Y)
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
            rectangle = Rectangle.Intersect(new Rectangle(aRect.X1, aRect.Y1, aRect.X2, aRect.Y2),
                new Rectangle(x1.X1, x1.Y1, x1.X2, x1.Y2));
            if (rectangle.X <= 0 && rectangle.Y <= 0 && rectangle.Width <= 0 && rectangle.Height <= 0)
            {
                return false;
            }
            return true;
        }

        public override void ParseToLines(ArrayList NewElemes)
        {
            DXFLine dXFLine = new DXFLine();
            dXFLine.StartPoint.X = this.data.point1.X;
            dXFLine.StartPoint.Y = this.data.point.Y;
            dXFLine.EndPoint.X = this.data.point.X;
            dXFLine.EndPoint.Y = this.data.point.Y;
            NewElemes.Add(dXFLine.Clone());
            dXFLine.StartPoint.X = this.data.point.X;
            dXFLine.StartPoint.Y = this.data.point.Y;
            dXFLine.EndPoint.X = this.data.point.X;
            dXFLine.EndPoint.Y = this.data.point1.Y;
            NewElemes.Add(dXFLine.Clone());
            dXFLine.StartPoint.X = this.data.point.X;
            dXFLine.StartPoint.Y = this.data.point1.Y;
            dXFLine.EndPoint.X = this.data.point1.X;
            dXFLine.EndPoint.Y = this.data.point1.Y;
            NewElemes.Add(dXFLine.Clone());
            dXFLine.StartPoint.X = this.data.point1.X;
            dXFLine.StartPoint.Y = this.data.point1.Y;
            dXFLine.EndPoint.X = this.data.point1.X;
            dXFLine.EndPoint.Y = this.data.point.Y;
            NewElemes.Add(dXFLine.Clone());
        }
    }
}