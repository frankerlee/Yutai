using System;
using System.Collections;
using System.Drawing;

namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFArc : DXFFigure
    {
        private ArcType type;

        public float EndAngle
        {
            get { return this.data.endAngle; }
            set { this.data.endAngle = value; }
        }

        public DXFPoint Point
        {
            get { return this.data.point; }
            set { this.data.point = value; }
        }

        public float Radius
        {
            get { return this.data.radius; }
            set { this.data.radius = value; }
        }

        public float StartAngle
        {
            get { return this.data.startAngle; }
            set { this.data.startAngle = value; }
        }

        public DXFArc()
        {
        }

        public DXFArc(DXFData aData) : base(aData)
        {
            this.type = (ArcType) aData.selfType;
        }

        public override void ExportAsDXF(DXFExport ADXFExport)
        {
            switch (this.type)
            {
                case ArcType.atCircle:
                {
                    ADXFExport.AddName("ARC", "AcDbCircle");
                    ADXFExport.AddColor(this.data);
                    ADXFExport.AddThickness(this.data);
                    ADXFExport.Add3DPoint(10, this.data.point);
                    ADXFExport.AddFloat(40, ADXFExport.MM(this.data.radius));
                    ADXFExport.current.Add("100");
                    ADXFExport.current.Add("AcDbArc");
                    ADXFExport.AddFloat(50, this.data.startAngle);
                    ADXFExport.AddFloat(51, this.data.endAngle);
                    return;
                }
                case ArcType.atEllipse:
                {
                    ADXFExport.AddName("ELLIPSE", "AcDbEllipse");
                    ADXFExport.AddColor(this.data);
                    ADXFExport.AddThickness(this.data);
                    ADXFExport.Add3DPoint(10, this.data.point);
                    ADXFExport.Add3DPoint(11, this.data.point1);
                    ADXFExport.AddFloat(40, this.data.radius);
                    if (Math.Abs((float) (this.data.startAngle - this.data.endAngle)) > DXFExport.accuracy)
                    {
                        ADXFExport.AddFloat(41, this.data.startAngle);
                        ADXFExport.AddFloat(42, this.data.endAngle);
                    }
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        public override bool IntersecRect(Rect aRect)
        {
            Rectangle rectangle = new Rectangle(0, 0, 0, 0);
            rectangle = Rectangle.Intersect(new Rectangle(aRect.X1, aRect.Y1, aRect.X2, aRect.Y2),
                new Rectangle((int) Math.Round((double) (this.data.point.X - this.data.radius)),
                    (int) Math.Round((double) (this.data.point.Y - this.data.radius)),
                    (int) Math.Round((double) (this.data.point.X + this.data.radius)),
                    (int) Math.Round((double) (this.data.point.Y + this.data.radius))));
            if (rectangle.X <= 0 && rectangle.Y <= 0 && rectangle.Width <= 0 && rectangle.Height <= 0)
            {
                return false;
            }
            return true;
        }

        public override void ParseToLines(ArrayList NewElemes)
        {
            DXFLine dXFLine = new DXFLine(this.data);
            DXFPoint dXFPoint = new DXFPoint();
            float x = this.data.point.X;
            float y = this.data.point.Y;
            float single = this.data.radius;
            float single1 = single;
            float single2 = this.data.startAngle*3.141593f/180f;
            float single3 = this.data.endAngle*3.141593f/180f;
            if (single3 <= single2)
            {
                single3 = single3 + 6.283185f;
            }
            int num = (int) Math.Round((double) ((single3 - single2)/3.141593f*16f));
            if (num < 4)
            {
                num = 4;
            }
            float single4 = (single3 - single2)/(float) (num - 1);
            for (int i = 0; i < num - 2; i++)
            {
                dXFLine.Layer = base.Layer;
                NewElemes.Add(dXFLine.Clone());
                float single5 = (float) Math.Sin((double) single2);
                float single6 = (float) Math.Cos((double) single2);
                dXFPoint.X = x + single*single6;
                dXFPoint.Y = y + single1*single5;
                dXFLine.StartPoint = (DXFPoint) dXFPoint.Clone();
                single2 = single2 + single4;
                single5 = (float) Math.Sin((double) single2);
                single6 = (float) Math.Cos((double) single2);
                dXFPoint.X = x + single*single6;
                dXFPoint.Y = y + single1*single5;
                dXFLine.EndPoint = (DXFPoint) dXFPoint.Clone();
            }
        }
    }
}