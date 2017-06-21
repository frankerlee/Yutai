using System;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class ApplyMath : ApplyConst
    {
        public static GPoint GetDroopPoint(GPoint ptLnBeg, GPoint ptLnEnd, GPoint objPoint)
        {
            CVeObj cVeObj = new CVeObj(ptLnBeg, ptLnEnd);
            CVeObj veObj = new CVeObj(ptLnBeg, objPoint);
            CVeObj droopVe = cVeObj.GetDroopVe(veObj);
            GPoint gPoint = new GPoint(droopVe.X, droopVe.Y);
            gPoint.OffSet(ptLnBeg);
            return gPoint;
        }

        public static double MakeAngleTo2P(double dAngle)
        {
            int num = (int)(dAngle / 360.0);
            double num2 = dAngle - (double)(360 * num);
            double result;
            if (num2 >= 0.0)
            {
                result = num2;
            }
            else
            {
                result = num2 + 360.0;
            }
            return result;
        }

        public static long GetInterSectPointofTwoLinesect(GPoint ptFirstLnBeg, GPoint ptFirstLnEnd, GPoint ptSecondLnBeg, GPoint ptSecondLnEnd, ref GPoint ptInsect)
        {
            double angleToPt = ptFirstLnBeg.GetAngleToPt(ptFirstLnEnd);
            double angleToPt2 = ptSecondLnBeg.GetAngleToPt(ptSecondLnEnd);
            return ApplyMath.GetInterSectPointofTwoLinesect(ptFirstLnBeg, angleToPt, ptSecondLnBeg, angleToPt2, ref ptInsect);
        }

        public static long GetInterSectPointofTwoLinesect(GPoint ptFirstLnBeg, double dAngle1, GPoint ptSecondLnBeg, double dAngle2, ref GPoint ptInsect)
        {
            dAngle1 = ApplyMath.MakeAngleTo2P(dAngle1);
            dAngle2 = ApplyMath.MakeAngleTo2P(dAngle2);
            long result;
            if (Math.Abs(dAngle1 - 90.0) < 1E-08 || Math.Abs(dAngle1 - 270.0) < 1E-08)
            {
                if (Math.Abs(dAngle2 - 90.0) < 1E-08 || Math.Abs(dAngle2 - 270.0) < 1E-08)
                {
                    result = 0L;
                }
                else
                {
                    double num = (ptFirstLnBeg.X - ptSecondLnBeg.X) / Math.Cos(dAngle2 * 0.017453292519943295);
                    ptInsect.X = ptFirstLnBeg.X;
                    ptInsect.Y = ptSecondLnBeg.Y + num * Math.Sin(dAngle2 * 0.017453292519943295);
                    result = 1L;
                }
            }
            else if (Math.Abs(dAngle2 - 90.0) < 1E-08 || Math.Abs(dAngle2 - 270.0) < 1E-08)
            {
                double num2 = (ptSecondLnBeg.X - ptFirstLnBeg.X) / Math.Cos(dAngle1 * 0.017453292519943295);
                ptInsect.X = ptSecondLnBeg.X;
                ptInsect.Y = ptFirstLnBeg.Y + num2 * Math.Sin(dAngle1 * 0.017453292519943295);
                result = 1L;
            }
            else if (Math.Abs(Math.Tan(dAngle1 * 0.017453292519943295) - Math.Tan(dAngle2 * 0.017453292519943295)) < 1E-08)
            {
                result = 0L;
            }
            else
            {
                double num3 = (ptSecondLnBeg.X - ptFirstLnBeg.X) / Math.Cos(dAngle1 * 0.017453292519943295);
                double num = (ptFirstLnBeg.Y - ptSecondLnBeg.Y + num3 * Math.Sin(dAngle1 * 0.017453292519943295)) / (Math.Sin(dAngle2 * 0.017453292519943295) - Math.Tan(dAngle1 * 0.017453292519943295) * Math.Cos(dAngle2 * 0.017453292519943295));
                double num2 = num3 + num * Math.Cos(dAngle2 * 0.017453292519943295) / Math.Cos(dAngle1 * 0.017453292519943295);
                ptInsect.X = ptFirstLnBeg.X + num2 * Math.Cos(dAngle1 * 0.017453292519943295);
                ptInsect.Y = ptFirstLnBeg.Y + num2 * Math.Sin(dAngle1 * 0.017453292519943295);
                result = 1L;
            }
            return result;
        }

        public static void GetPointOnLineZ(double dX1, double dY1, double dZ1, double dX2, double dY2, double dZ2, double dX, double dY, out double dZ)
        {
            double num = dX2 - dX1;
            double num2 = dY2 - dY1;
            double num3 = dZ2 - dZ1;
            double num4 = dX - dX1;
            double num5 = dY - dY1;
            double num6 = Math.Sqrt(num * num + num2 * num2);
            double num7 = Math.Sqrt(num4 * num4 + num5 * num5);
            if (num6 < 0.001)
            {
                dZ = dZ1;
            }
            else
            {
                double num8 = num7 / num6;
                double num9 = num8 * num3 + dZ1;
                dZ = num9;
            }
        }
    }
}