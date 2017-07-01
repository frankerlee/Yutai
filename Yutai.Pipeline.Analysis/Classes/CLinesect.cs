using System;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CLinesect : ApplyConst
    {
        private GPoint gpoint_0;

        private GPoint gpoint_1;

        public CLinesect()
        {
            this.gpoint_0 = new GPoint();
            this.gpoint_1 = new GPoint();
        }

        public CLinesect(GPoint tPtBeg, GPoint tPtEnd)
        {
            this.gpoint_0 = tPtBeg.GetDeepCopy();
            this.gpoint_1 = tPtEnd.GetDeepCopy();
        }

        public CLinesect(double bPtBegX, double bPtBegY, double bPtEndX, double bPtEndY)
        {
            this.gpoint_0 = new GPoint(bPtBegX, bPtBegY);
            this.gpoint_1 = new GPoint(bPtEndX, bPtEndY);
        }

        public void SetFirstPoint(GPoint tPoint)
        {
            this.gpoint_0 = tPoint;
        }

        public void SetSecondPoint(GPoint tPoint)
        {
            this.gpoint_1 = tPoint;
        }

        public GPoint GetFirstPt()
        {
            return this.gpoint_0;
        }

        public GPoint GetSecondPt()
        {
            return this.gpoint_1;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }

        public void SetPoints(GPoint ptBegin, GPoint ptEnd)
        {
            this.gpoint_0 = ptBegin;
            this.gpoint_1 = ptEnd;
        }

        public void Set(double begX, double begY, double endX, double endY)
        {
            this.gpoint_0.X = begX;
            this.gpoint_0.Y = begY;
            this.gpoint_1.X = endX;
            this.gpoint_1.Y = endY;
        }

        public double GetLength()
        {
            double num = this.gpoint_1.X - this.gpoint_0.X;
            double num2 = this.gpoint_1.Y - this.gpoint_0.Y;
            return Math.Sqrt(num*num + num2*num2);
        }

        public double GetLength2()
        {
            double num = this.gpoint_1.X - this.gpoint_0.X;
            double num2 = this.gpoint_1.Y - this.gpoint_0.Y;
            return num*num + num2*num2;
        }

        public double GetAngle()
        {
            CVeObj vecOb = this.GetVecOb();
            return vecOb.GetAngle();
        }

        public GPoint GetCentroid()
        {
            return new GPoint
            {
                X = (this.gpoint_0.X + this.gpoint_1.X)/2.0,
                Y = (this.gpoint_0.Y + this.gpoint_1.Y)/2.0
            };
        }

        public bool IsUpon(GPoint ptMouse)
        {
            double num = this.gpoint_0.GetAngleToPt(ptMouse);
            double angle = this.GetAngle();
            bool result;
            if (angle < 180.0)
            {
                result = (num > angle && num < angle + 180.0);
            }
            else
            {
                if (num < 180.0)
                {
                    num += 360.0;
                }
                result = (num > angle && num < angle + 180.0);
            }
            return result;
        }

        public bool IsPtOnLine(GPoint tPoint)
        {
            bool result;
            if (tPoint == this.gpoint_0 || tPoint == this.gpoint_1)
            {
                result = true;
            }
            else
            {
                CVeObj cVeObj = new CVeObj(this.gpoint_0, tPoint);
                CVeObj cVeObj2 = new CVeObj(tPoint, this.gpoint_1);
                CVeObj unitVe = cVeObj.GetUnitVe();
                CVeObj unitVe2 = cVeObj2.GetUnitVe();
                result = (unitVe == unitVe2);
            }
            return result;
        }

        public bool IsPtOnLine(double tX, double tY)
        {
            GPoint tPoint = new GPoint(tX, tY);
            return this.IsPtOnLine(tPoint);
        }

        public bool IsPtOnLineExt(GPoint tPoint)
        {
            CVeObj cVeObj = new CVeObj(this.gpoint_0, tPoint);
            CVeObj cVeObj2 = new CVeObj(tPoint, this.gpoint_1);
            return cVeObj.X*cVeObj2.Y - cVeObj2.X*cVeObj.Y < 1E-08;
        }

        public bool IsPtOnLineExt(double tX, double tY)
        {
            GPoint tPoint = new GPoint(tX, tY);
            return this.IsPtOnLineExt(tPoint);
        }

        public double GetAngleToLine(CLinesect tLine)
        {
            CVeObj vecOb = this.GetVecOb();
            CVeObj vecOb2 = tLine.GetVecOb();
            return vecOb.GetAngleToVe(vecOb2);
        }

        public CVeObj GetVecOb()
        {
            return new CVeObj(this.gpoint_1.X - this.gpoint_0.X, this.gpoint_1.Y - this.gpoint_0.Y);
        }

        public double GetLenPtToLine(double x, double y)
        {
            GPoint tPoint = new GPoint(x, y);
            return this.GetLenPtToLine(tPoint);
        }

        public double GetLenToPt(GPoint ptObj)
        {
            GPoint droopPoint = this.GetDroopPoint(ptObj);
            double result;
            if (this.IsPtOnLine(droopPoint))
            {
                result = this.GetLenPtToLine(ptObj);
            }
            else
            {
                double num = ptObj.DistanceToPt(this.gpoint_0);
                double num2 = ptObj.DistanceToPt(this.gpoint_1);
                result = ((num <= num2) ? num : num2);
            }
            return result;
        }

        public double GetLenToPt(double dx, double dy)
        {
            GPoint ptObj = new GPoint(dx, dy);
            return this.GetLenToPt(ptObj);
        }

        public double GetLenPtToLine(GPoint tPoint)
        {
            CVeObj cVeObj = new CVeObj(this.gpoint_0, tPoint);
            CVeObj cVeObj2 = new CVeObj(this.gpoint_0, this.gpoint_1);
            double length = cVeObj2.GetLength();
            double result;
            if (length < 1E-16)
            {
                result = cVeObj.GetLength();
            }
            else
            {
                double num = Math.Abs(cVeObj2.GetArrowProduct(cVeObj));
                result = num/length;
            }
            return result;
        }

        public GPoint GetDroopPoint(GPoint objPoint)
        {
            return ApplyMath.GetDroopPoint(this.gpoint_0, this.gpoint_1, objPoint);
        }

        public long GetInterSectPointofTwoLinesect(CLinesect lnObj, ref GPoint ptInsect)
        {
            return ApplyMath.GetInterSectPointofTwoLinesect(this.gpoint_0, this.gpoint_1, lnObj.GetFirstPt(),
                lnObj.GetSecondPt(), ref ptInsect);
        }

        public long GetInterSectPointofTwoLinesectWithHeight(CLinesect lnObj, ref GPoint ptInsect)
        {
            long interSectPointofTwoLinesect = ApplyMath.GetInterSectPointofTwoLinesect(this.gpoint_0, this.gpoint_1,
                lnObj.GetFirstPt(), lnObj.GetSecondPt(), ref ptInsect);
            long result;
            if (interSectPointofTwoLinesect == 0L)
            {
                result = interSectPointofTwoLinesect;
            }
            else
            {
                double z;
                ApplyMath.GetPointOnLineZ(this.gpoint_0.X, this.gpoint_0.Y, this.gpoint_0.Z, this.gpoint_1.X,
                    this.gpoint_1.Y, this.gpoint_1.Z, ptInsect.X, ptInsect.Y, out z);
                ptInsect.Z = z;
                double m;
                ApplyMath.GetPointOnLineZ(lnObj.gpoint_0.X, lnObj.gpoint_0.Y, lnObj.gpoint_0.Z, lnObj.gpoint_1.X,
                    lnObj.gpoint_1.Y, lnObj.gpoint_1.Z, ptInsect.X, ptInsect.Y, out m);
                ptInsect.M = m;
                result = 1L;
            }
            return result;
        }

        public long GetInterSectPointofTwoLinesectWithHeightForTranSect(CLinesect lnObj, ref GPoint ptInsect)
        {
            long interSectPointofTwoLinesect = ApplyMath.GetInterSectPointofTwoLinesect(this.gpoint_0, this.gpoint_1,
                lnObj.GetFirstPt(), lnObj.GetSecondPt(), ref ptInsect);
            long result;
            if (interSectPointofTwoLinesect == 0L)
            {
                result = interSectPointofTwoLinesect;
            }
            else
            {
                double num;
                ApplyMath.GetPointOnLineZ(lnObj.gpoint_0.X, lnObj.gpoint_0.Y, lnObj.gpoint_0.Z, lnObj.gpoint_1.X,
                    lnObj.gpoint_1.Y, lnObj.gpoint_1.Z, ptInsect.X, ptInsect.Y, out num);
                ptInsect.Z = num;
                ApplyMath.GetPointOnLineZ(lnObj.gpoint_0.X, lnObj.gpoint_0.Y, lnObj.gpoint_0.M, lnObj.gpoint_1.X,
                    lnObj.gpoint_1.Y, lnObj.gpoint_1.M, ptInsect.X, ptInsect.Y, out num);
                ptInsect.M = num;
                result = 1L;
            }
            return result;
        }

        public static bool operator ==(CLinesect tLine1, CLinesect tLine2)
        {
            return tLine1.gpoint_0 == tLine2.gpoint_0 && tLine1.gpoint_1 == tLine2.gpoint_1;
        }

        public static bool operator !=(CLinesect tLine1, CLinesect tLine2)
        {
            return tLine1.gpoint_0 != tLine2.gpoint_0 || tLine1.gpoint_1 != tLine2.gpoint_1;
        }

        private void method_0(double tx, double ty)
        {
            GPoint tPoint = new GPoint(tx, ty);
            this.gpoint_0.OffSet(tPoint);
            this.gpoint_1.OffSet(tPoint);
        }

        private void method_1(GPoint tPoint)
        {
            this.gpoint_0.OffSet(tPoint);
            this.gpoint_1.OffSet(tPoint);
        }

        private double method_2()
        {
            double x;
            if (this.gpoint_0.X > this.gpoint_1.X)
            {
                x = this.gpoint_1.X;
            }
            else
            {
                x = this.gpoint_0.X;
            }
            return x;
        }

        private double method_3()
        {
            double x;
            if (this.gpoint_0.X < this.gpoint_1.X)
            {
                x = this.gpoint_1.X;
            }
            else
            {
                x = this.gpoint_0.X;
            }
            return x;
        }

        private CLinesect method_4(double dNum)
        {
            CVeObj cVeObj = new CVeObj(new GPoint(0.0, 0.0), this.GetFirstPt());
            CVeObj cVeObj2 = new CVeObj(new GPoint(0.0, 0.0), this.GetSecondPt());
            CVeObj angleUnitVe = CVeObj.GetAngleUnitVe(this.GetAngle() + 90.0);
            cVeObj += angleUnitVe.GetVeNumProduct(dNum);
            cVeObj2 += angleUnitVe.GetVeNumProduct(dNum);
            return new CLinesect(cVeObj.X, cVeObj.Y, cVeObj2.X, cVeObj2.Y);
        }

        private bool method_5(GPoint toPoint)
        {
            double angle = this.GetAngle();
            double angleToPt = this.GetSecondPt().GetAngleToPt(toPoint);
            return Math.Abs(angle - angleToPt) < 1E-08;
        }
    }
}