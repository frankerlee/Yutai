using System;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CVeObj : ApplyConst
    {
        private double double_0;

        private double double_1;

        public double X
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        public double Y
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }

        public CVeObj()
        {
            this.double_0 = 0.0;
            this.double_1 = 0.0;
        }

        public CVeObj(double tx, double ty)
        {
            this.double_0 = tx;
            this.double_1 = ty;
        }

        public CVeObj(GPoint ptBeg, GPoint ptEnd)
        {
            this.double_0 = ptEnd.X - ptBeg.X;
            this.double_1 = ptEnd.Y - ptBeg.Y;
        }

        public double GetLength()
        {
            return Math.Sqrt(this.double_0 * this.double_0 + this.double_1 * this.double_1);
        }

        public double GetAngle()
        {
            CVeObj cVeObj = new CVeObj(1.0, 0.0);
            double dotProduct = this.GetDotProduct(cVeObj);
            double num = this.GetLength() * cVeObj.GetLength();
            double result;
            if (1E-10 >= Math.Abs(num))
            {
                result = 0.0;
            }
            else
            {
                double num2 = dotProduct / num;
                if (Math.Abs(num2) > 0.99999999 && Math.Abs(num2) < 1.00000001)
                {
                    num2 = (double)((num2 > 0.0) ? 1 : -1);
                }
                double num3 = Math.Acos(num2);
                num3 *= 57.295779513082323;
                if (this.double_1 < 0.0)
                {
                    num3 = 360.0 - num3;
                }
                result = num3;
            }
            return result;
        }

        public CVeObj GetVeNumProduct(double dNum)
        {
            return new CVeObj
            {
                X = this.double_0 * dNum,
                Y = this.double_1 * dNum
            };
        }

        public double GetDotProduct(CVeObj veObj)
        {
            return this.double_0 * veObj.X + this.double_1 * veObj.Y;
        }

        public double GetArrowProduct(CVeObj veObj)
        {
            return this.double_0 * veObj.Y - this.double_1 * veObj.X;
        }

        public double GetAngleToVe(CVeObj veObj)
        {
            double dotProduct = this.GetDotProduct(veObj);
            double num = this.GetLength() * veObj.GetLength();
            double result;
            if (1E-10 >= Math.Abs(num))
            {
                result = 0.0;
            }
            else
            {
                double num2 = dotProduct / num;
                if (Math.Abs(num2) > 0.99999999 && Math.Abs(num2) < 1.00000001)
                {
                    num2 = (double)((num2 > 0.0) ? 1 : -1);
                }
                double num3 = Math.Acos(num2);
                result = num3 * 57.295779513082323;
            }
            return result;
        }

        public static CVeObj operator +(CVeObj veObj1, CVeObj veObj2)
        {
            return new CVeObj
            {
                X = veObj1.X + veObj2.X,
                Y = veObj1.Y + veObj2.Y
            };
        }

        public static CVeObj operator -(CVeObj veObj1, CVeObj veObj2)
        {
            return new CVeObj
            {
                X = veObj1.X - veObj2.X,
                Y = veObj1.Y - veObj2.Y
            };
        }

        public CVeObj GetUnitVe()
        {
            CVeObj cVeObj = new CVeObj();
            double length = this.GetLength();
            CVeObj result;
            if (1E-16 >= length)
            {
                result = cVeObj;
            }
            else
            {
                cVeObj.X = this.double_0 / length;
                cVeObj.Y = this.double_1 / length;
                result = cVeObj;
            }
            return result;
        }

        public CVeObj GetDroopVe(CVeObj veObj)
        {
            new CVeObj();
            CVeObj unitVe = this.GetUnitVe();
            return unitVe.GetVeNumProduct(unitVe.GetDotProduct(veObj));
        }

        public static bool operator ==(CVeObj veObj1, CVeObj veObj2)
        {
            bool flag = Math.Abs(veObj1.GetLength() - veObj2.GetLength()) < 1E-08;
            veObj1.GetAngle();
            veObj2.GetAngle();
            bool flag2 = Math.Abs(veObj1.GetAngle() - veObj2.GetAngle()) < 1E-07;
            return flag && flag2;
        }

        public static bool operator !=(CVeObj veObj1, CVeObj veObj2)
        {
            bool flag = Math.Abs(veObj1.GetLength() - veObj2.GetLength()) >= 1E-08;
            veObj1.GetAngle();
            veObj2.GetAngle();
            bool flag2 = Math.Abs(veObj1.GetAngle() - veObj2.GetAngle()) >= 1E-07;
            return flag || flag2;
        }

        public static CVeObj GetAngleUnitVe(double dAgl)
        {
            return new CVeObj
            {
                X = Math.Cos(dAgl * 0.017453292519943295),
                Y = Math.Sin(dAgl * 0.017453292519943295)
            };
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }
    }
}