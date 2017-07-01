using System;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class GPoint : ApplyConst
    {
        private double double_0;

        private double double_1;

        private double double_2;

        private double double_3;

        public double X
        {
            get { return this.double_0; }
            set { this.double_0 = value; }
        }

        public double Y
        {
            get { return this.double_1; }
            set { this.double_1 = value; }
        }

        public double Z
        {
            get { return this.double_2; }
            set { this.double_2 = value; }
        }

        public double M
        {
            get { return this.double_3; }
            set { this.double_3 = value; }
        }

        public GPoint()
        {
            this.double_0 = 0.0;
            this.double_1 = 0.0;
            this.double_2 = 0.0;
            this.double_3 = 0.0;
        }

        public GPoint(double tx, double ty)
        {
            this.double_0 = tx;
            this.double_1 = ty;
            this.double_2 = 0.0;
            this.double_3 = 0.0;
        }

        public GPoint(double tx, double ty, double tz)
        {
            this.double_0 = tx;
            this.double_1 = ty;
            this.double_2 = tz;
            this.double_3 = 0.0;
        }

        public GPoint(double tx, double ty, double tz, double tm)
        {
            this.double_0 = tx;
            this.double_1 = ty;
            this.double_2 = tz;
            this.double_3 = tm;
        }

        public new string ToString()
        {
            return string.Concat(new string[]
            {
                "{X = ",
                this.X.ToString("f3"),
                "  Y = ",
                this.Y.ToString("f3"),
                "  Z = ",
                this.Z.ToString("f3"),
                "  M = ",
                this.M.ToString(),
                "}"
            });
        }

        public double DistanceToPt(GPoint toPoint)
        {
            double num = toPoint.X - this.double_0;
            double num2 = toPoint.Y - this.double_1;
            return Math.Sqrt(num*num + num2*num2);
        }

        public double DistanceToPt(double toX, double toY)
        {
            double num = toX - this.double_0;
            double num2 = toY - this.double_1;
            return Math.Sqrt(num*num + num2*num2);
        }

        public CVeObj GetVeToPt(GPoint toPoint)
        {
            return new CVeObj(toPoint.X - this.double_0, toPoint.Y - this.double_1);
        }

        public double GetAngleToPt(GPoint toPoint)
        {
            CVeObj veToPt = this.GetVeToPt(toPoint);
            return veToPt.GetAngle();
        }

        public double GetAngleToPt(double toX, double toY)
        {
            CVeObj veToPt = this.GetVeToPt(new GPoint(toX, toY));
            return veToPt.GetAngle();
        }

        public void OffSet(double dx, double dy)
        {
            this.double_0 += dx;
            this.double_1 += dy;
        }

        public void OffSet(GPoint tPoint)
        {
            this.double_0 += tPoint.X;
            this.double_1 += tPoint.Y;
        }

        public void OffSetByLen(double dAngle, double dLen)
        {
            this.double_0 += dLen*Math.Cos(dAngle*0.017453292519943295);
            this.double_1 += dLen*Math.Sin(dAngle*0.017453292519943295);
        }

        public static bool operator ==(GPoint tPoint1, GPoint tPoint2)
        {
            return Math.Abs(tPoint1.X - tPoint2.X) < 1E-08 && Math.Abs(tPoint1.Y - tPoint2.Y) < 1E-08;
        }

        public static bool operator !=(GPoint tPoint1, GPoint tPoint2)
        {
            return Math.Abs(tPoint1.X - tPoint2.X) >= 1E-08 || Math.Abs(tPoint1.Y - tPoint2.Y) >= 1E-08;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }

        public GPoint GetDeepCopy()
        {
            return new GPoint
            {
                X = this.X,
                Y = this.Y,
                Z = this.Z,
                M = this.M
            };
        }
    }
}