using System;
using System.Collections.Generic;
using System.Diagnostics;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class THTools
    {
        private double double_0 = 500000.0;
        private const double fc_pi = 3.1415926535;
        private static double g_iCentralMeridian;
        private int int_0;
        private int int_1 = 1;
        private StandardTypeEnum standardTypeEnum_0 = StandardTypeEnum.TX;

        public bool BL2FileName(string string_0, double double_1, double double_2, ref string string_1)
        {
            bool flag = true;
            if (this.standardTypeEnum_0 == StandardTypeEnum.TX)
            {
                return this.BL2FileName_tx(string_0, double_1, double_2, ref string_1);
            }
            if (this.standardTypeEnum_0 == StandardTypeEnum.GB)
            {
                flag = this.BL2FileName_standard(string_0, double_1, double_2, ref string_1);
            }
            return flag;
        }

        public bool BL2FileName_standard(int int_2, double double_1, double double_2, out string string_0)
        {
            double num2;
            double num3;
            string_0 = "";
            char ch = 'A';
            int num = int_2;
            if (num <= 10000)
            {
                if (num <= 1000)
                {
                    if (num != 500)
                    {
                        if (num != 1000)
                        {
                            goto Label_0145;
                        }
                        ch = 'J';
                        num2 = 0.005208333333333333;
                        num3 = 0.003472222222222222;
                    }
                    else
                    {
                        ch = 'K';
                        num2 = 0.0026041666666666665;
                        num3 = 0.001736111111111111;
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 2000:
                            ch = 'I';
                            num2 = 0.010416666666666666;
                            num3 = 0.0069444444444444441;
                            goto Label_0196;

                        case 5000:
                            ch = 'H';
                            num2 = 0.03125;
                            num3 = 0.020833333333333332;
                            goto Label_0196;
                    }
                    if (num != 10000)
                    {
                        goto Label_0145;
                    }
                    ch = 'G';
                    num2 = 0.0625;
                    num3 = 0.041666666666666664;
                }
                goto Label_0196;
            }
            if (num <= 50000)
            {
                if (num != 25000)
                {
                    if (num != 50000)
                    {
                        goto Label_0145;
                    }
                    ch = 'E';
                    num2 = 0.25;
                    num3 = 0.16666666666666666;
                }
                else
                {
                    ch = 'F';
                    num2 = 0.125;
                    num3 = 0.083333333333333329;
                }
                goto Label_0196;
            }
            switch (num)
            {
                case 100000:
                    ch = 'D';
                    num2 = 0.5;
                    num3 = 0.33333333333333331;
                    goto Label_0196;

                case 250000:
                    ch = 'C';
                    num2 = 1.5;
                    num3 = 1.0;
                    goto Label_0196;

                case 500000:
                    ch = 'B';
                    num2 = 3.0;
                    num3 = 2.0;
                    goto Label_0196;
            }
            Label_0145:
            return false;
            Label_0196:
            if ((ch < 'B') && (ch > 'K'))
            {
                return false;
            }
            long num4 = ((int) (double_1/4.0)) + 1;
            long num5 = ((int) (double_2/6.0)) + 31;
            long num6 = ((int) (4.0/num3)) - ((int) ((double_1 - (((int) (double_1/4.0))*4))/num3));
            long num7 = ((int) ((double_2 - (((int) (double_2/6.0))*6))/num2)) + 1;
            if (int_2 <= 1000)
            {
                string_0 =
                    string.Concat(new object[]
                    {
                        Convert.ToChar((long) ((Convert.ToInt16('A') + num4) - 1L)).ToString(), num5, ch.ToString(),
                        num6.ToString("0000"), num7.ToString("0000")
                    });
            }
            else
            {
                string_0 =
                    string.Concat(new object[]
                    {
                        Convert.ToChar((long) ((Convert.ToInt16('A') + num4) - 1L)).ToString(), num5, ch.ToString(),
                        num6.ToString("000"), num7.ToString("000")
                    });
            }
            return true;
        }

        public bool BL2FileName_standard(string string_0, double double_1, double double_2, ref string string_1)
        {
            double num2;
            double num3;
            long num4;
            string str = string_0;
            if (
                !(((Convert.ToInt16(str) < Convert.ToInt16("B")) |
                   ((Convert.ToInt16(str) > Convert.ToInt16("H")) & (Convert.ToInt16(str) < Convert.ToInt16("b")))) |
                  (Convert.ToInt16(str) > Convert.ToInt16("h"))))
            {
                if (Convert.ToInt16(str) < Convert.ToInt16("b"))
                {
                    str =
                        Convert.ToChar((int) ((Convert.ToInt16(str) + Convert.ToInt16("a")) - Convert.ToInt16("A")))
                            .ToString();
                }
                switch (str.ToUpper())
                {
                    case "B":
                        num2 = 3.0;
                        num3 = 2.0;
                        goto Label_01F6;

                    case "C":
                        num2 = 1.5;
                        num3 = 1.0;
                        goto Label_01F6;

                    case "D":
                        num2 = 0.5;
                        num3 = 0.33333333333333331;
                        goto Label_01F6;

                    case "E":
                        num2 = 0.25;
                        num3 = 0.16666666666666666;
                        goto Label_01F6;

                    case "F":
                        num2 = 0.125;
                        num3 = 0.083333333333333329;
                        goto Label_01F6;

                    case "G":
                        num2 = 0.0625;
                        num3 = 0.041666666666666664;
                        goto Label_01F6;

                    case "H":
                        num2 = 0.03125;
                        num3 = 0.020833333333333332;
                        goto Label_01F6;
                }
            }
            return false;
            Label_01F6:
            num4 = Convert.ToInt16((double) (double_1/4.0)) + 1;
            long num5 = Convert.ToInt16((double) (double_2/6.0)) + 1;
            long num6 = Convert.ToInt16((double) (4.0/num3)) -
                        Convert.ToInt16((double) ((double_1 - ((num4 - 1.0)*4.0))/num3));
            long num7 = Convert.ToInt16((double) ((double_2 - ((num5 - 1.0)*6.0))/num2)) + 1;
            string_1 =
                string.Concat(new object[]
                {
                    Convert.ToChar((long) ((Convert.ToInt16("A") + num4) - 1L)).ToString(), num5,
                    Convert.ToChar((int) ((Convert.ToInt16(str) - Convert.ToInt16("a")) + Convert.ToInt16("A")))
                        .ToString(),
                    num6, num7
                });
            return true;
        }

        public bool BL2FileName_tx(string string_0, double double_1, double double_2, ref string string_1)
        {
            string str;
            double num2;
            double num3;
            if ((string_0 != null) && (string_0.Trim() != ""))
            {
                str = string_0.ToUpper();
                if ((str[0] < 'B') || (str[0] > 'I'))
                {
                    return false;
                }
                switch (str.ToUpper())
                {
                    case "B":
                        num2 = 1.5;
                        num3 = 1.0;
                        goto Label_01D4;

                    case "C":
                        num2 = 1.0;
                        num3 = 0.66666666666666663;
                        goto Label_01D4;

                    case "D":
                        num2 = 0.5;
                        num3 = 0.33333333333333331;
                        goto Label_01D4;

                    case "E":
                        num2 = 0.25;
                        num3 = 0.16666666666666666;
                        goto Label_01D4;

                    case "F":
                        num2 = 0.125;
                        num3 = 0.083333333333333329;
                        goto Label_01D4;

                    case "G":
                        num2 = 0.0625;
                        num3 = 0.041666666666666664;
                        goto Label_01D4;

                    case "H":
                        num2 = 0.03125;
                        num3 = 0.020833333333333332;
                        goto Label_01D4;

                    case "I":
                        num2 = 0.0125;
                        num3 = 0.0083333333333333332;
                        goto Label_01D4;
                }
            }
            return false;
            Label_01D4:
            if ((double_1 <= 0.0) || (double_2 <= 0.0))
            {
                return false;
            }
            long num4 = (int) (Math.Truncate((double) (double_1/4.0)) + 1.0);
            long num5 = (int) (Math.Truncate((double) (double_2/6.0)) + 1.0);
            long num6 =
                (int)
                ((4.0/num3) -
                 Math.Truncate(
                     (double) (Math.Truncate((double) ((((double_1 - ((num4 - 1.0)*4.0))/num3)*10000.0) + 1.0))/10000.0)));
            long num7 = (long) (Math.Truncate((double) ((double_2 - ((num5 - 1.0)*6.0))/num2)) + 1);
            string str3 = num6.ToString();
            string str4 = num7.ToString();
            if ((str != "H") && (str != "I"))
            {
                if (num6 < 10L)
                {
                    str3 = "0" + num6.ToString();
                }
                if (num7 < 10L)
                {
                    str4 = "0" + num7.ToString();
                }
            }
            else if (str == "I")
            {
                if (num6 < 10L)
                {
                    str3 = "00" + num6.ToString();
                }
                else if ((num6 >= 10L) && (num6 < 100L))
                {
                    str3 = "0" + num6.ToString();
                }
                if (num7 < 10L)
                {
                    str3 = "00" + num7.ToString();
                }
                else if ((num7 >= 10L) && (num7 < 100L))
                {
                    str4 = "0" + num7.ToString();
                }
            }
            if (num5 < 30L)
            {
                num5 += 30L;
            }
            string_1 =
                string.Concat(new object[]
                {
                    Convert.ToChar((int) ((str[0] - 'A') + 65)).ToString(),
                    Convert.ToChar((long) ((65 + num4) - 1L)).ToString(), num5, str3, str4
                });
            return true;
        }

        public double CalculateArea(IPolygon ipolygon_0)
        {
            double num = 0.0;
            double num2 = 0.0;
            int i = 0;
            IPointCollection points = null;
            if ((ipolygon_0 == null) || ipolygon_0.IsEmpty)
            {
                return 0.0;
            }
            points = ipolygon_0 as IPointCollection;
            for (i = 1; i < (points.PointCount - 1); i++)
            {
                if (i == 1)
                {
                    num = 0.0;
                    num = points.get_Point(i).X*(points.get_Point(i + 1).Y - points.get_Point(points.PointCount - 1).Y);
                    num2 += num;
                }
                else
                {
                    num = 0.0;
                    num = points.get_Point(i).X*(points.get_Point(i + 1).Y - points.get_Point(i - 1).Y);
                    num2 += num;
                }
            }
            return (num2*0.5);
        }

        public static double CalculateTHTheoryArea(double double_1, double double_2, double double_3, double double_4,
            double double_5)
        {
            double num = 1.0 - ((double_2*double_2)/(double_1*double_1));
            double num2 = num*num;
            double num3 = num2*num;
            double num4 = num3*num;
            double num5 = (((1.0 + (2.0*num)) + (0.375*num2)) + (0.3125*num3)) + (0.2734375*num4);
            double num6 = (((0.16666666666666666*num) + (0.1875*num2)) + (0.1875*num3)) + (0.18229166666666666*num4);
            double num7 = ((0.0375*num2) + (0.0625*num3)) + (0.078125*num4);
            double num8 = (0.0089285714285714281*num3) + (0.1953125*num4);
            double num9 = 0.002170138888888889*num4;
            double num10 = ((double_5 - double_4)/180.0)*3.1415926;
            double d = ((double_4 + double_5)/360.0)*3.1415926;
            double num12 = (((3.1415926*double_2)*double_2)*double_3)/90.0;
            double num13 = (((((num5*Math.Sin(num10/2.0))*Math.Cos(d)) -
                              ((num6*Math.Sin((num10/2.0)*3.0))*Math.Cos(3.0*d))) +
                             ((num7*Math.Sin((num10/2.0)*5.0))*Math.Cos(5.0*d))) -
                            ((num8*Math.Sin((num10/2.0)*7.0))*Math.Cos(7.0*d))) +
                           ((num9*Math.Sin((num10/2.0)*9.0))*Math.Cos(9.0*d));
            return (num12*num13);
        }

        public double CalculateTKArea(double[] double_1, double[] double_2)
        {
            double num = 0.0;
            double num3 = (double) double_1.GetValue(0);
            double num1 = (double) double_2.GetValue(0);
            double num4 = (double) double_1.GetValue(1);
            object num5 = double_2.GetValue(1);
            double num6 = (double) double_1.GetValue(2);
            double num7 = (double) double_2.GetValue(2);
            double num8 = (double) double_1.GetValue(3);
            double num10 = (double) double_2.GetValue(3);
            num = Math.Abs((double) (num7 - (double) num5));
            return (((Math.Abs((double) (num6 - num8)) + Math.Abs((double) (num4 - num3)))*num)/2.0);
        }

        public static double DEG2DDDMMSS(double double_1, ref long long_0, ref int int_2, ref int int_3)
        {
            bool flag = false;
            if (double_1 < 0.0)
            {
                flag = true;
            }
            Math.Abs(double_1);
            double d = Math.Truncate(double_1);
            double num2 = Math.Truncate((double) ((double_1*60.0) - (d*60.0)));
            double num3 = ((double_1*3600.0) - (d*3600.0)) - (num2*60.0);
            if (num3 > 59.5)
            {
                num3 = 0.0;
                num2++;
            }
            if (flag)
            {
                long_0 = Convert.ToInt32((double) (-1.0*Math.Truncate(d)));
                int_2 = Convert.ToInt32((double) (-1.0*Math.Truncate(num2)));
                int_3 = Convert.ToInt32((double) (-1.0*Math.Truncate(num3)));
            }
            else
            {
                long_0 = Convert.ToInt32(Math.Truncate(d));
                int_2 = Convert.ToInt32(Math.Truncate(num2));
                int_3 = Convert.ToInt32(Math.Truncate(num3));
            }
            if (flag)
            {
                return -((d + (num2/100.0)) + (num3/10000.0));
            }
            return ((d + (num2/100.0)) + (num3/10000.0));
        }

        public double DEG2DMS(double double_1)
        {
            bool flag = false;
            if (double_1 < 0.0)
            {
                flag = true;
            }
            Math.Abs(double_1);
            double num = Math.Truncate(double_1);
            double num2 = Math.Truncate((double) ((double_1*60.0) - (num*60.0)));
            double num3 = ((double_1*3600.0) - (num*3600.0)) - (num2*60.0);
            if (num3 > 59.5)
            {
                num3 = 0.0;
                num2++;
            }
            if (flag)
            {
                return -((num + (num2/100.0)) + (num3/10000.0));
            }
            return ((num + (num2/100.0)) + (num3/10000.0));
        }

        public double DMS2DEG(double double_1)
        {
            bool flag = false;
            if (double_1 < 0.0)
            {
                flag = true;
            }
            double d = Math.Abs(double_1);
            double num2 = Math.Truncate(d);
            double num3 = Math.Truncate((double) ((d*100.0) - (num2*100.0)));
            double num4 = ((d*10000.0) - (num2*10000.0)) - (num3*100.0);
            if (flag)
            {
                return -((num2 + (num3/60.0)) + (num4/3600.0));
            }
            return ((num2 + (num3/60.0)) + (num4/3600.0));
        }

        public bool FileName2BL(string string_0, out double double_1, out double double_2, out double double_3,
            out double double_4)
        {
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            if (string_0.Length == 8)
            {
                this.standardTypeEnum_0 = StandardTypeEnum.TX;
            }
            else if (string_0.Length == 10)
            {
                this.standardTypeEnum_0 = StandardTypeEnum.GB;
            }
            switch (this.standardTypeEnum_0)
            {
                case StandardTypeEnum.TX:
                    return this.FileName2BL_tx(string_0, out double_1, out double_2, out double_3, out double_4);

                case StandardTypeEnum.GB:
                    return this.FileName2BL_standard(string_0, out double_1, out double_2, out double_3, out double_4);
            }
            return false;
        }

        public bool FileName2BL_cqtx(string string_0, ref double double_1, ref double double_2, ref double double_3,
            ref double double_4)
        {
            char[] separator = new char[1];
            string[] strArray = null;
            bool flag = true;
            int num = 0;
            try
            {
                if ((string_0 == "") || (string_0 == null))
                {
                    return flag;
                }
                string_0 = string_0.ToLower();
                string_0 = string_0.Replace('(', ' ');
                string_0 = string_0.Replace(')', ' ');
                separator[0] = '-';
                strArray = string_0.Split(separator);
                long num2 = Convert.ToInt16(char.Parse(strArray.GetValue(0).ToString()));
                long num3 = Convert.ToInt16(strArray.GetValue(1).ToString());
                long num4 = Convert.ToInt16(strArray.GetValue(2).ToString());
                long num5 = Convert.ToInt16(strArray.GetValue(3).ToString().Trim());
                if (strArray.Length >= 5)
                {
                    if (((string_0.Contains("a") || string_0.Contains("b")) || string_0.Contains("c")) ||
                        string_0.Contains("d"))
                    {
                        double_4 = 0.03125;
                        double_3 = 0.020833333333333332;
                        switch (strArray.GetValue((int) (strArray.Length - 1)).ToString())
                        {
                            case "a":
                                num = 1;
                                break;

                            case "b":
                                num = 2;
                                break;

                            case "c":
                                num = 3;
                                break;

                            case "d":
                                num = 4;
                                break;
                        }
                    }
                }
                else
                {
                    double_4 = 0.0625;
                    double_3 = 0.041666666666666664;
                }
                long num6 = (num2 - 97) + 1L;
                long num7 = num3;
                long num8 = num4;
                long num9 = num5;
                double d = ((double) (num8 - 1L))/12.0;
                int num11 = Convert.ToInt16((double) ((num8 - 1L) - (Math.Truncate(d)*12.0)));
                double num12 = ((double) (num9 - 1L))/8.0;
                int num13 = Convert.ToInt16((double) ((num9 - 1L) - (Math.Truncate(num12)*8.0)));
                int num14 = 0;
                if (strArray.Length >= 5)
                {
                    double num15 = ((double) (num - 1))/2.0;
                    num14 = Convert.ToInt16((double) ((num - 1) - (Math.Truncate(num15)*2.0)));
                    double_2 = ((((num7 - 1.0)*6.0) + (num11*0.5)) + (num13*0.0625)) + (num14*double_4);
                }
                else
                {
                    double_2 = (((num7 - 1.0)*6.0) + (num11*0.5)) + (num13*0.0625);
                }
                if (double_2 > 180.0)
                {
                    double_2 -= 180.0;
                }
                double_1 = ((((num6 - 1.0)*4.0) + ((11.0 - ((int) ((num8 - 1L)/12L)))/3.0)) +
                            ((7.0 - ((int) ((num9 - 1L)/8L)))/24.0)) + ((1.0 - ((num - 1)/2))/48.0);
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public bool FileName2BL_standard(string string_0, out double double_1, out double double_2, out double double_3,
            out double double_4)
        {
            string str2;
            int num5;
            double num7;
            double num8;
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            if ((string_0 != "") && (string_0 != null))
            {
                str2 = string_0.ToLower();
                if (str2.Length < 10)
                {
                    return false;
                }
                string str3 = str2.Substring(0, 1);
                try
                {
                    num3 = Convert.ToInt16(str3[0]);
                }
                catch (Exception)
                {
                }
                num4 = Convert.ToInt16(str2.Substring(1, 2));
                str = str2.Substring(3, 1);
                num5 = 3;
                switch (str.ToUpper())
                {
                    case "B":
                        num7 = 3.0;
                        num8 = 2.0;
                        goto Label_02A9;

                    case "C":
                        num7 = 1.5;
                        num8 = 1.0;
                        goto Label_02A9;

                    case "D":
                        num7 = 0.5;
                        num8 = 0.33333333333333331;
                        goto Label_02A9;

                    case "E":
                        num7 = 0.25;
                        num8 = 0.16666666666666666;
                        goto Label_02A9;

                    case "F":
                        num7 = 0.125;
                        num8 = 0.083333333333333329;
                        goto Label_02A9;

                    case "G":
                        num7 = 0.0625;
                        num8 = 0.041666666666666664;
                        goto Label_02A9;

                    case "H":
                        num7 = 0.03125;
                        num8 = 0.020833333333333332;
                        goto Label_02A9;

                    case "I":
                        num7 = 0.010416666666666666;
                        num8 = 0.0069444444444444441;
                        goto Label_02A9;

                    case "J":
                        num5 = 4;
                        num7 = 0.005208333333333333;
                        num8 = 0.003472222222222222;
                        goto Label_02A9;

                    case "K":
                        num5 = 4;
                        num7 = 0.0026041666666666665;
                        num8 = 0.001736111111111111;
                        goto Label_02A9;
                }
            }
            return false;
            Label_02A9:
            if (num5 == 3)
            {
                num = Convert.ToInt16(str2.Substring(4, 3));
                num2 = Convert.ToInt16(str2.Substring(7, 3));
            }
            else
            {
                num = Convert.ToInt16(str2.Substring(4, 4));
                num2 = Convert.ToInt16(str2.Substring(8, 4));
            }
            long num9 = num3 - 96;
            long num10 = num4;
            if (num10 > 30L)
            {
                num10 -= 30L;
            }
            long num11 = num;
            long num12 = num2;
            double_2 = ((num10 - 1.0)*6.0) + ((num12 - 1.0)*num7);
            double_1 = ((num9 - 1.0)*4.0) + (((4.0/num8) - num11)*num8);
            double_4 = num7;
            double_3 = num8;
            return true;
        }

        public bool FileName2BL_tx(string string_0, out double double_1, out double double_2, out double double_3,
            out double double_4)
        {
            int num;
            int num2;
            int num3;
            int num4;
            double num6;
            double num7;
            long num8;
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            string str = string_0.ToLower();
            if (string_0.Length >= 8)
            {
                string str2 = str.Substring(0, 1);
                char ch = str.Substring(1, 1)[0];
                num = ch;
                num2 = Convert.ToInt16(str.Substring(2, 2));
                num3 = Convert.ToInt16(str.Substring(4, 2));
                num4 = Convert.ToInt16(str.Substring(6, 2));
                switch (str2.ToUpper())
                {
                    case "B":
                        num6 = 1.5;
                        num7 = 1.0;
                        goto Label_0226;

                    case "C":
                        num6 = 1.5;
                        num7 = 1.0;
                        goto Label_0226;

                    case "D":
                        num6 = 0.5;
                        num7 = 0.33333333333333331;
                        goto Label_0226;

                    case "E":
                        num6 = 0.25;
                        num7 = 0.16666666666666666;
                        goto Label_0226;

                    case "F":
                        num6 = 0.125;
                        num7 = 0.083333333333333329;
                        goto Label_0226;

                    case "G":
                        num6 = 0.0625;
                        num7 = 0.041666666666666664;
                        goto Label_0226;

                    case "H":
                        num6 = 0.03125;
                        num7 = 0.020833333333333332;
                        goto Label_0226;

                    case "I":
                        num6 = 0.03125;
                        num7 = 0.020833333333333332;
                        goto Label_0226;
                }
            }
            return false;
            Label_0226:
            num8 = (num - 97) + 1;
            long num9 = num2;
            if (num9 >= 30L)
            {
                num9 -= 30L;
            }
            long num10 = num3;
            long num11 = num4;
            double_2 = ((num9 - 1.0)*6.0) + ((num11 - 1.0)*num6);
            double_1 = ((num8 - 1.0)*4.0) + (((4.0/num7) - num10)*num7);
            double_4 = num6;
            double_3 = num7;
            return true;
        }

        public bool GetBLFromXY(double double_1, double double_2, ref double double_3, ref double double_4, int int_2,
            int int_3, int int_4, double double_5)
        {
            long num;
            double num2;
            double num3;
            double num4;
            if (int_4 == 1)
            {
                num = (long) Math.Round((double) (double_5/6.0));
            }
            else
            {
                num = (long) Math.Round((double) (double_5/3.0));
            }
            if (this.int_1 == 1)
            {
                double_2 -= num*1000000;
            }
            double_1 /= 1000000.0;
            double_2 -= 500000.0;
            if (int_3 == 0)
            {
                num2 = 6399698.90178271;
                num3 = 0.0067385254147;
                num4 = (((((27.11115372595 + (9.02468257083*(double_1 - 3.0))) -
                           (0.00579740442*Math.Pow(double_1 - 3.0, 2.0))) -
                          (0.00043532572*Math.Pow(double_1 - 3.0, 3.0))) + (4.857285E-05*Math.Pow(double_1 - 3.0, 4.0))) +
                        (2.15727E-06*Math.Pow(double_1 - 3.0, 5.0))) - (1.9399E-07*Math.Pow(double_1 - 3.0, 6.0));
            }
            else
            {
                num2 = 6399596.65198801;
                num3 = 0.0067395018195;
                num4 = (((((27.11162289465 + (9.02483657729*(double_1 - 3.0))) -
                           (0.00579850656*Math.Pow(double_1 - 3.0, 2.0))) -
                          (0.00043540029*Math.Pow(double_1 - 3.0, 3.0))) + (4.858357E-05*Math.Pow(double_1 - 3.0, 4.0))) +
                        (2.15769E-06*Math.Pow(double_1 - 3.0, 5.0))) - (1.9404E-07*Math.Pow(double_1 - 3.0, 6.0));
            }
            double a = (num4*3.1415926535)/180.0;
            double x = Math.Tan(a);
            double num7 = num3*Math.Pow(Math.Cos(a), 2.0);
            double num8 = (double_2*Math.Sqrt(1.0 + num7))/num2;
            double num9 = ((90.0*Math.Pow(num8, 2.0)) -
                           ((7.5*(((5.0 + (3.0*Math.Pow(x, 2.0))) + num7) - ((9.0*num7)*Math.Pow(x, 2.0))))*
                            Math.Pow(num8, 4.0))) +
                          ((0.25*((61.0 + (90.0*Math.Pow(x, 2.0))) + (45.0*Math.Pow(x, 4.0))))*Math.Pow(num8, 6.0));
            double num10 = num4 - ((((1.0 + num7)*x)*num9)/3.1415926535);
            double num11 = ((((180.0*num8) - ((30.0*((1.0 + (2.0*Math.Pow(x, 2.0))) + num7))*Math.Pow(num8, 3.0))) +
                             ((1.5*((5.0 + (28.0*Math.Pow(x, 2.0))) + (24.0*Math.Pow(x, 4.0))))*Math.Pow(num8, 5.0)))/
                            3.1415926535)/Math.Cos(a);
            num11 += double_5;
            if (num11 < 0.0)
            {
                num11 += 360.0;
            }
            if (num11 >= 360.0)
            {
                num11 -= 360.0;
            }
            double_3 = num10;
            double_4 = num11;
            return true;
        }

        public static double GetCentralMeridian(double double_1, int int_2, bool bool_0)
        {
            double num = Convert.ToDouble(GetStripNum(double_1, bool_0, out int_2)) + int_2;
            if (bool_0)
            {
                return ((6.0*num) - 3.0);
            }
            return (3.0*num);
        }

        public string GetDFM(double double_1)
        {
            string str2 = "\x00b0";
            string str3 = "'";
            string str4 = "″";
            long num = 0L;
            int num2 = 0;
            int num3 = 0;
            DEG2DDDMMSS(double_1, ref num, ref num2, ref num3);
            return (num.ToString() + str2 + num2.ToString() + str3 + num3.ToString() + str4);
        }

        public static IEnvelope GetDomain(string string_0)
        {
            return null;
        }

        public IList<string> getJionTFH(string string_0)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            bool flag = false;
            string str6 = "";
            char[] separator = new char[1];
            string[] strArray = null;
            IList<string> list = new List<string>();
            if (string_0 == "")
            {
                return null;
            }
            if (string_0.Contains("-"))
            {
                if (!this.TFHCheckOld(string_0))
                {
                    return null;
                }
                if (!this.FileName2BL_cqtx(string_0, ref num, ref num2, ref num3, ref num4))
                {
                    return null;
                }
                separator[0] = '-';
                strArray = string_0.Split(separator);
                if (strArray != null)
                {
                    if (strArray.Length == 4)
                    {
                        str6 = "G";
                    }
                    else if (strArray.Length == 5)
                    {
                        str6 = "H";
                    }
                }
                this.BL2FileName_tx(str6, num, num2, ref str);
            }
            else
            {
                str = string_0;
                if (!this.TFHCheck(str))
                {
                    return null;
                }
            }
            str6 = str.Substring(0, 1);
            this.FileName2BL_tx(str, out num, out num2, out num3, out num4);
            num5 = num;
            num6 = num2 + num4;
            if (this.BL2FileName_tx(str6, num5, num6, ref str3) && flag)
            {
                list.Add(str3 + "【" + str5 + "】");
            }
            num5 = num - num3;
            num6 = num2;
            if (this.BL2FileName_tx(str6, num5, num6, ref str2) && flag)
            {
                list.Add(str2 + "【" + str4 + "】");
            }
            return list;
        }

        private static double GetLocalLongitude(double double_1, int int_2, bool bool_0)
        {
            SetCentralMeridian(GetCentralMeridian(double_1, int_2, bool_0));
            if (bool_0)
            {
                double num2;
                if (IsCentralMeridianRight(g_iCentralMeridian))
                {
                    num2 = double_1 - g_iCentralMeridian;
                }
                else
                {
                    num2 = double_1 - GetCentralMeridian(double_1, int_2, bool_0);
                }
                if ((num2 - 3.0) > 1E-06)
                {
                    return (num2 - 360.0);
                }
                if ((num2 + 3.0) < -1E-06)
                {
                    num2 += 360.0;
                }
                return num2;
            }
            if (double_1 <= 1.5)
            {
                if (IsCentralMeridianRight(g_iCentralMeridian))
                {
                    return ((double_1 + 360.0) - g_iCentralMeridian);
                }
                return ((double_1 + 360.0) - GetCentralMeridian(double_1, int_2, bool_0));
            }
            if (IsCentralMeridianRight(g_iCentralMeridian))
            {
                return (double_1 - g_iCentralMeridian);
            }
            return (double_1 - GetCentralMeridian(double_1, int_2, bool_0));
        }

        public IList<IPoint> GetProjectCoord(double double_1, double double_2, double double_3, double double_4,
            IProjectedCoordinateSystem iprojectedCoordinateSystem_0)
        {
            IList<IPoint> list = new List<IPoint>();
            IPoint item = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            item.PutCoords(double_1, double_3 + double_4);
            item.SpatialReference = iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
            item.Project(iprojectedCoordinateSystem_0);
            point2.PutCoords(double_1, double_3);
            point2.SpatialReference = iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
            point2.Project(iprojectedCoordinateSystem_0);
            point3.PutCoords(double_1 + double_2, double_3 + double_4);
            point3.SpatialReference = iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
            point3.Project(iprojectedCoordinateSystem_0);
            point4.PutCoords(double_1 + double_2, double_3);
            point4.SpatialReference = iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
            point4.Project(iprojectedCoordinateSystem_0);
            list.Add(item);
            list.Add(point3);
            list.Add(point4);
            list.Add(point2);
            return list;
        }

        public IList<IPoint> GetProjectCoord(string string_0, bool bool_0, bool bool_1, int int_2, ref bool bool_2)
        {
            string str = "";
            string str2 = "";
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            string str3 = "";
            char[] separator = new char[1];
            string[] strArray = null;
            IList<IPoint> list = new List<IPoint>();
            if (string_0 == "")
            {
                return null;
            }
            if (string_0.Contains("-"))
            {
                str = string_0;
                bool_2 = this.FileName2BL_cqtx(str, ref num, ref num2, ref num3, ref num4);
                if (!bool_2)
                {
                    bool_2 = false;
                    return null;
                }
                separator[0] = '-';
                strArray = str.Split(separator);
                if (strArray != null)
                {
                    if (strArray.Length == 4)
                    {
                        str3 = "G";
                    }
                    else if (strArray.Length == 5)
                    {
                        str3 = "H";
                    }
                }
                bool_2 = this.BL2FileName_tx(str3, num, num2, ref str2);
            }
            else
            {
                str2 = string_0;
                bool_2 = true;
            }
            if (!this.TFHCheck(str2))
            {
                bool_2 = false;
                return null;
            }
            if (bool_2 && (str2 != ""))
            {
                double num13;
                bool_2 = this.FileName2BL(str2, out num, out num2, out num3, out num4);
                if (num3 >= 1.5)
                {
                    bool_1 = true;
                }
                IPoint item = new PointClass();
                IPoint point2 = new PointClass();
                IPoint point3 = new PointClass();
                IPoint point4 = new PointClass();
                double num5 = 0.0;
                double num6 = 0.0;
                double num7 = 0.0;
                double num8 = 0.0;
                double num9 = 0.0;
                double num10 = 0.0;
                double num11 = 0.0;
                double num12 = 0.0;
                if (int_2 == 0)
                {
                    if (!bool_1)
                    {
                        this.MainStrip = (int) Math.Truncate((double) (num2/3.0));
                    }
                    else
                    {
                        this.MainStrip = (int) (Math.Truncate((double) (num2/6.0)) + 1);
                    }
                }
                else
                {
                    this.MainStrip = int_2;
                }
                if (!bool_1)
                {
                    num13 = this.MainStrip*3.0;
                }
                else
                {
                    num13 = (this.MainStrip*6.0) - 3.0;
                }
                GetXYFromBL(num + num3, num2, this.MainStrip, this.DadiCoord == 1, 0, bool_0, num13, this.double_0,
                    out num5, out num6);
                GetXYFromBL(num, num2, this.MainStrip, this.DadiCoord == 1, 0, bool_0, num13, this.double_0, out num7,
                    out num8);
                GetXYFromBL(num + num3, num2 + num4, this.MainStrip, this.DadiCoord == 1, 0, bool_0, num13,
                    this.double_0, out num9, out num10);
                GetXYFromBL(num, num2 + num4, this.MainStrip, this.DadiCoord == 1, 0, bool_0, num13, this.double_0,
                    out num11, out num12);
                item.PutCoords(num6, num5);
                point2.PutCoords(num8, num7);
                point3.PutCoords(num10, num9);
                point4.PutCoords(num12, num11);
                list.Add(item);
                list.Add(point3);
                list.Add(point4);
                list.Add(point2);
            }
            return list;
        }

        public IList<IPoint> GetProjectCoord(double double_1, double double_2, double double_3, double double_4,
            bool bool_0, bool bool_1, int int_2)
        {
            IList<IPoint> list = new List<IPoint>();
            IPoint item = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            if (int_2 == 0)
            {
                if (!bool_1)
                {
                    this.MainStrip = (int) Math.Truncate((double) (double_1/3.0));
                }
                else
                {
                    this.MainStrip = (int) (Math.Truncate((double) (double_1/6.0)) + 1);
                }
            }
            else
            {
                this.MainStrip = int_2;
            }
            GetXYFromBL(double_3 + double_4, double_1, this.MainStrip, this.DadiCoord == 1, 0, bool_0, bool_1,
                this.double_0, out num, out num2);
            GetXYFromBL(double_3, double_1, this.MainStrip, this.DadiCoord == 1, 0, bool_0, bool_1, this.double_0,
                out num3, out num4);
            GetXYFromBL(double_3 + double_4, double_1 + double_2, this.MainStrip, this.DadiCoord == 1, 0, bool_0, bool_1,
                this.double_0, out num5, out num6);
            GetXYFromBL(double_3, double_1 + double_2, this.MainStrip, this.DadiCoord == 1, 0, bool_0, bool_1,
                this.double_0, out num7, out num8);
            item.PutCoords(num2, num);
            point2.PutCoords(num4, num3);
            point3.PutCoords(num6, num5);
            point4.PutCoords(num8, num7);
            list.Add(item);
            list.Add(point3);
            list.Add(point4);
            list.Add(point2);
            return list;
        }

        public static int GetStripNum(double double_1, bool bool_0, out int int_2)
        {
            double num;
            if (bool_0)
            {
                num = (double_1/6.0) + 1.0;
            }
            else if (double_1 <= 1.5)
            {
                num = (((double_1 + 360.0) - 1.5)/3.0) + 1.0;
            }
            else
            {
                num = double_1/3.0;
            }
            int num2 = (int) num;
            if (bool_0)
            {
                if (num2 > 60)
                {
                    num2 -= 60;
                }
                if (num2 < 1)
                {
                    num2 += 60;
                }
            }
            else
            {
                if (num2 > 120)
                {
                    num2 -= 120;
                }
                if (num2 < 1)
                {
                    num2 += 120;
                }
            }
            int_2 = 0;
            return num2;
        }

        public void GetTFJdWd(string string_0, out double[] double_1, out double[] double_2, out double double_3)
        {
            string str = "";
            string s = "";
            string str3 = "";
            int num = 0;
            int num2 = 0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            int num9 = 0;
            double num10 = 0.0;
            double num11 = 0.0;
            double num12 = 0.0;
            double num13 = 0.0;
            double num14 = 0.0;
            double num15 = 0.0;
            double num16 = 0.0;
            double num17 = 0.0;
            double_1 = new double[4];
            double_2 = new double[4];
            string_0 = string_0.ToUpper();
            str = string_0.Substring(0, 1);
            s = string_0.Substring(1, 1);
            str3 = string_0.Substring(2, 2);
            num = int.Parse(string_0.Substring(4, 2));
            num2 = int.Parse(string_0.Substring(6, 2));
            num9 = char.Parse(s);
            num3 = (int.Parse(str3) - 1)*6;
            num4 = ((num9 - 65) + 1)*4;
            if (num3 >= 180.0)
            {
                num3 -= 180.0;
            }
            switch (str)
            {
                case "H":
                    num7 = 0.03125;
                    num8 = 0.020833333333333332;
                    num5 = num7*(num2 - 1);
                    num6 = num8*(num - 1);
                    break;

                case "G":
                    num7 = 0.0625;
                    num8 = 0.041666666666666664;
                    num5 = num7*(num2 - 1);
                    num6 = num8*(num - 1);
                    break;
            }
            num10 = num3 + num5;
            num11 = num4 - num6;
            num12 = num10 + num7;
            num13 = num11;
            num14 = num10 + num7;
            num15 = num11 + num8;
            num16 = num10;
            num17 = num11 + num8;
            double_1.SetValue(num10, 0);
            double_2.SetValue(num11, 0);
            double_1.SetValue(num12, 1);
            double_2.SetValue(num13, 1);
            double_1.SetValue(num14, 2);
            double_2.SetValue(num15, 2);
            double_1.SetValue(num16, 3);
            double_2.SetValue(num17, 3);
            double_3 = num10 + (num7/2.0);
        }

        public static int GetTHMainStrip(string string_0, bool bool_0)
        {
            double num2;
            double num3;
            double num4;
            double num5;
            int num6;
            THNO2BL(string_0, out num2, out num3, out num4, out num5);
            return GetStripNum(num3, bool_0, out num6);
        }

        public static int GetTHScale(string string_0)
        {
            string str = "";
            switch (string_0.Length)
            {
                case 8:
                    str = string_0.Substring(0, 1);
                    break;

                case 10:
                    str = string_0.Substring(3, 1);
                    break;
            }
            switch (str.ToUpper())
            {
                case "B":
                    return 500000;

                case "C":
                    return 250000;

                case "D":
                    return 100000;

                case "E":
                    return 50000;

                case "F":
                    return 25000;

                case "G":
                    return 10000;

                case "H":
                    return 5000;

                case "I":
                    return 2000;
            }
            return 0;
        }

        public static double GetTHTheoryArea(string string_0, bool bool_0, bool bool_1, bool bool_2)
        {
            double[] numArray;
            double[] numArray2;
            double[] numArray3;
            double[] numArray4;
            if (
                !GetTKCoor(string_0, bool_0, bool_1, bool_2, 500000.0, out numArray, out numArray2, out numArray3,
                    out numArray4))
            {
                return 0.0;
            }
            IPolygon polygon = new PolygonClass();
            object missing = Type.Missing;
            for (int i = 0; i < numArray3.Length; i++)
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(numArray3[i], numArray4[i]);
                (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            }
            return (polygon as IArea).Area;
        }

        public static bool GetTKCoor(string string_0, bool bool_0, bool bool_1, bool bool_2, double double_1,
            out double[] double_2, out double[] double_3, out double[] double_4, out double[] double_5)
        {
            double num = 0.0;
            double num2 = 0.0;
            double_2 = new double[4];
            double_3 = new double[4];
            double_4 = new double[4];
            double_5 = new double[4];
            bool flag = false;
            int num3 = 1;
            int tHMainStrip = GetTHMainStrip(string_0, bool_1);
            if (THNO2BL(string_0, out double_2[0], out double_3[0], out num, out num2))
            {
                double_2[1] = double_2[0];
                double_3[1] = double_3[0] + num2;
                double_2[2] = double_2[0] + num;
                double_3[2] = double_3[0] + num2;
                double_2[3] = double_2[0] + num;
                double_3[3] = double_3[0];
                for (int i = 0; i <= 3; i++)
                {
                    GetXYFromBL(double_2[i], double_3[i], tHMainStrip, bool_2, num3, bool_0, bool_1, double_1,
                        out double_4[i], out double_5[i]);
                    flag = true;
                }
            }
            return flag;
        }

        public bool GetTKCoor(string string_0, bool bool_0, bool bool_1, int int_2, int int_3, ref double[] double_1,
            ref double[] double_2, ref double[] double_3, ref double[] double_4)
        {
            double num = 0.0;
            double num2 = 0.0;
            string str = "";
            double_1 = new double[5];
            double_2 = new double[5];
            double_3 = new double[5];
            double_4 = new double[5];
            bool flag = false;
            int num3 = 1;
            this.standardTypeEnum_0 = StandardTypeEnum.TX;
            this.MainStrip = int_2;
            str = string_0;
            if (this.FileName2BL(str, out double_1[0], out double_2[0], out num, out num2))
            {
                GetXYFromBL(double_1[0], double_2[0], this.int_0, int_3 == 1, num3, bool_0, bool_1, this.double_0,
                    out double_3[0], out double_4[0]);
                double_1[1] = double_1[0];
                double_2[1] = double_2[0] + num2;
                double_1[2] = double_1[0] + num;
                double_2[2] = double_2[0] + num2;
                double_1[3] = double_1[0] + num;
                double_2[3] = double_2[0];
                for (int i = 1; i <= 3; i++)
                {
                    GetXYFromBL(double_1[i], double_2[i], this.int_0, int_3 == 1, num3, bool_0, bool_1, this.double_0,
                        out double_3[i], out double_4[i]);
                    flag = true;
                }
            }
            return flag;
        }

        public void GetXYFormBL54(double[] double_1, double[] double_2, out double[] double_3, out double[] double_4,
            bool bool_0, double double_5)
        {
            int index = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            int length = double_1.Length;
            double_3 = new double[length];
            double_4 = new double[length];
            for (index = 0; index < length; index++)
            {
                GetXYFromBL((double) double_1.GetValue(index), (double) double_2.GetValue(index), this.int_0, true, 1,
                    false, bool_0, double_5, out num2, out num3);
                double_3.SetValue(num2, index);
                double_4.SetValue(num3, index);
            }
        }

        public static void GetXYFromBL(double double_1, double double_2, int int_2, bool bool_0, int int_3, bool bool_1,
            bool bool_2, double double_3, out double double_4, out double double_5)
        {
            double num5;
            double num6;
            double num7;
            double d = (double_1/180.0)*3.1415926535;
            double num2 = GetLocalLongitude(double_2, int_3, bool_2);
            double x = ((Math.Cos(d)*3.1415926535)*num2)/180.0;
            double num4 = Math.Tan(d);
            if (bool_1)
            {
                num5 = 6399596.65198801;
                num6 = 0.0067395018195;
                num7 = (111133.0047*double_1) -
                       (((((32009.8575*Math.Sin(d)) + (133.9978*Math.Pow(Math.Sin(d), 3.0))) +
                          (0.6975*Math.Pow(Math.Sin(d), 5.0))) + (0.0039*Math.Pow(Math.Sin(d), 7.0)))*Math.Cos(d));
            }
            else
            {
                num5 = 6399698.90178271;
                num6 = 0.00673852541468;
                num7 = (111134.8611*double_1) -
                       (((((32005.7798*Math.Sin(d)) + (133.9614*Math.Pow(Math.Sin(d), 3.0))) +
                          (0.6972*Math.Pow(Math.Sin(d), 5.0))) + (0.0039*Math.Pow(Math.Sin(d), 7.0)))*Math.Cos(d));
            }
            double num8 = num6*Math.Pow(Math.Cos(d), 2.0);
            double num9 = num5/Math.Sqrt(1.0 + num8);
            double num10 = ((Math.Pow(x, 2.0)/2.0) +
                            (((((5.0 - Math.Pow(num4, 2.0)) + (9.0*num8)) + (4.0*Math.Pow(num8, 2.0)))*Math.Pow(x, 4.0))/
                             24.0)) +
                           ((((61.0 - (58.0*Math.Pow(num4, 2.0))) + Math.Pow(num4, 4.0))*Math.Pow(x, 6.0))/720.0);
            double num11 = (x + ((((1.0 - Math.Pow(num4, 2.0)) + num8)*Math.Pow(x, 3.0))/6.0)) +
                           ((((((5.0 - (18.0*Math.Pow(num4, 2.0))) + Math.Pow(num4, 4.0)) + (14.0*num8)) -
                              ((58.0*num8)*Math.Pow(num4, 2.0)))*Math.Pow(x, 5.0))/120.0);
            double_4 = num7 + ((num9*num4)*num10);
            double_5 = (num9*num11) + double_3;
            if (bool_0)
            {
                double_5 += int_2*1000000;
            }
        }

        public static void GetXYFromBL(double double_1, double double_2, int int_2, bool bool_0, int int_3, bool bool_1,
            double double_3, double double_4, out double double_5, out double double_6)
        {
            double num5;
            double num6;
            double num7;
            double d = (double_1/180.0)*3.1415926535;
            double num2 = double_2 - double_3;
            double x = ((Math.Cos(d)*3.1415926535)*num2)/180.0;
            double num4 = Math.Tan(d);
            if (bool_1)
            {
                num5 = 6399596.65198801;
                num6 = 0.0067395018195;
                num7 = (111133.0047*double_1) -
                       (((((32009.8575*Math.Sin(d)) + (133.9978*Math.Pow(Math.Sin(d), 3.0))) +
                          (0.6975*Math.Pow(Math.Sin(d), 5.0))) + (0.0039*Math.Pow(Math.Sin(d), 7.0)))*Math.Cos(d));
            }
            else
            {
                num5 = 6399698.90178271;
                num6 = 0.00673852541468;
                num7 = (111134.8611*double_1) -
                       (((((32005.7798*Math.Sin(d)) + (133.9614*Math.Pow(Math.Sin(d), 3.0))) +
                          (0.6972*Math.Pow(Math.Sin(d), 5.0))) + (0.0039*Math.Pow(Math.Sin(d), 7.0)))*Math.Cos(d));
            }
            double num8 = num6*Math.Pow(Math.Cos(d), 2.0);
            double num9 = num5/Math.Sqrt(1.0 + num8);
            double num10 = ((Math.Pow(x, 2.0)/2.0) +
                            (((((5.0 - Math.Pow(num4, 2.0)) + (9.0*num8)) + (4.0*Math.Pow(num8, 2.0)))*Math.Pow(x, 4.0))/
                             24.0)) +
                           ((((61.0 - (58.0*Math.Pow(num4, 2.0))) + Math.Pow(num4, 4.0))*Math.Pow(x, 6.0))/720.0);
            double num11 = (x + ((((1.0 - Math.Pow(num4, 2.0)) + num8)*Math.Pow(x, 3.0))/6.0)) +
                           ((((((5.0 - (18.0*Math.Pow(num4, 2.0))) + Math.Pow(num4, 4.0)) + (14.0*num8)) -
                              ((58.0*num8)*Math.Pow(num4, 2.0)))*Math.Pow(x, 5.0))/120.0);
            double_5 = num7 + ((num9*num4)*num10);
            double_6 = (num9*num11) + double_4;
            if (bool_0)
            {
                double_6 += int_2*1000000;
            }
        }

        public static bool IsCentralMeridianRight(double double_1)
        {
            return ((double_1 >= 0.0) & (double_1 < 360.0));
        }

        private double method_0(double double_1, int int_2, bool bool_0)
        {
            SetCentralMeridian(GetCentralMeridian(double_1, int_2, bool_0));
            if (bool_0)
            {
                double num2;
                if (IsCentralMeridianRight(g_iCentralMeridian))
                {
                    num2 = double_1 - g_iCentralMeridian;
                }
                else
                {
                    num2 = double_1 - GetCentralMeridian(double_1, int_2, bool_0);
                }
                if ((num2 - 3.0) > 1E-06)
                {
                    return (num2 - 360.0);
                }
                if ((num2 + 3.0) < -1E-06)
                {
                    num2 += 360.0;
                }
                return num2;
            }
            if (double_1 <= 1.5)
            {
                if (IsCentralMeridianRight(g_iCentralMeridian))
                {
                    return ((double_1 + 360.0) - g_iCentralMeridian);
                }
                return ((double_1 + 360.0) - GetCentralMeridian(double_1, int_2, bool_0));
            }
            if (IsCentralMeridianRight(g_iCentralMeridian))
            {
                return (double_1 - g_iCentralMeridian);
            }
            return (double_1 - GetCentralMeridian(double_1, int_2, bool_0));
        }

        public string OldTFHToNew(string string_0)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            int num = 0;
            int num2 = 0;
            string str4 = "";
            string str5 = "";
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            char[] separator = new char[1];
            string[] strArray = null;
            try
            {
                separator[0] = '-';
                strArray = string_0.Split(separator);
                num6 = int.Parse(strArray.GetValue(2).ToString());
                if ((num6%12) != 0)
                {
                    num7 = (num6/12) + 1;
                }
                else
                {
                    num7 = num6/12;
                }
                if ((num6%12) == 0)
                {
                    num8 = 12;
                }
                else
                {
                    num8 = num6%12;
                }
                num3 = int.Parse(strArray.GetValue(3).ToString().Replace('(', ' ').Replace(')', ' '));
                if ((num3%8) != 0)
                {
                    num4 = (num3/8) + 1;
                }
                else
                {
                    num4 = num3/8;
                }
                if ((num3%8) == 0)
                {
                    num5 = 8;
                }
                else
                {
                    num5 = num3%8;
                }
                if (((string_0.Contains("a") || string_0.Contains("b")) || string_0.Contains("c")) ||
                    string_0.Contains("d"))
                {
                    str3 = "H";
                    switch (strArray.GetValue((int) (strArray.Length - 1)).ToString())
                    {
                        case "a":
                            num9 = 1;
                            num10 = 1;
                            break;

                        case "b":
                            num9 = 1;
                            num10 = 2;
                            break;

                        case "c":
                            num9 = 2;
                            num10 = 1;
                            break;

                        case "d":
                            num9 = 2;
                            num10 = 2;
                            break;
                    }
                    num = ((((num7 - 1)*8) + (num4 - 1))*2) + num9;
                    num2 = ((((num8 - 1)*8) + (num5 - 1))*2) + num10;
                }
                else
                {
                    str3 = "G";
                    num = ((num7 - 1)*8) + num4;
                    num2 = ((num8 - 1)*8) + num5;
                }
                str2 = str3 + strArray.GetValue(0) + strArray.GetValue(1);
                str4 = num.ToString();
                str5 = num2.ToString();
                if (str4.Length == 1)
                {
                    str4 = "0" + str4;
                }
                if (str5.Length == 1)
                {
                    str5 = "0" + str5;
                }
                str = str2 + str4 + str5;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return str;
        }

        public static void SetCentralMeridian(double double_1)
        {
            if (IsCentralMeridianRight(double_1))
            {
                g_iCentralMeridian = double_1;
            }
            else
            {
                g_iCentralMeridian = -999.0;
            }
        }

        public bool TFHCheck(string string_0)
        {
            return true;
        }

        public bool TFHCheckOld(string string_0)
        {
            int num = 0;
            char[] separator = new char[1];
            string[] strArray = null;
            string s = "";
            separator[0] = '-';
            strArray = string_0.Split(separator);
            if (strArray != null)
            {
                if ((strArray.Length < 4) || (strArray.Length > 5))
                {
                    return false;
                }
                s = strArray.GetValue(0).ToString().ToUpper();
                if (s.Length != 1)
                {
                    return false;
                }
                try
                {
                    num = char.Parse(s);
                }
                catch (Exception)
                {
                    return false;
                }
                if ((num < 65) || (num > 86))
                {
                    return false;
                }
                try
                {
                    if (int.Parse(strArray.GetValue(1).ToString()) > 60)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                try
                {
                    if (int.Parse(strArray.GetValue(2).ToString()) > 144)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                try
                {
                    if (int.Parse(strArray.GetValue(3).ToString().Replace('(', ' ').Replace(')', ' ').Trim()) > 64)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                if (strArray.Length == 5)
                {
                    s = strArray.GetValue(4).ToString().ToUpper();
                    try
                    {
                        if (char.Parse(s) >= 'E')
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public string THConvert(string string_0)
        {
            string str = "";
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            string str2 = "";
            char[] separator = new char[1];
            string[] strArray = null;
            if ((string_0 == "") && (string_0 == null))
            {
                str = "";
            }
            if (string_0.Contains("-"))
            {
                if (!this.TFHCheckOld(string_0))
                {
                    str = "";
                }
                if (this.FileName2BL_cqtx(string_0, ref num, ref num2, ref num3, ref num4))
                {
                    separator[0] = '-';
                    strArray = string_0.Split(separator);
                    if (strArray != null)
                    {
                        if (strArray.Length == 4)
                        {
                            str2 = "G";
                        }
                        else if (strArray.Length == 5)
                        {
                            str2 = "H";
                        }
                    }
                    if (!this.BL2FileName_tx(str2, num, num2, ref str))
                    {
                        str = "";
                    }
                    return str;
                }
                return "";
            }
            if (!this.TFHCheck(str))
            {
                str = "";
            }
            if (this.FileName2BL_tx(string_0, out num, out num2, out num3, out num4))
            {
                str2 = string_0.Substring(0, 1).ToUpper();
                return str;
            }
            return "";
        }

        public static bool THNO2BL(string string_0, out double double_1, out double double_2, out double double_3,
            out double double_4)
        {
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            if (!((string_0 == "") | (string_0 == null)))
            {
                switch (string_0.Length)
                {
                    case 8:
                        return THNO2BL_tx(string_0, out double_1, out double_2, out double_3, out double_4);

                    case 9:
                        return THNO2BL_cqtx(string_0, out double_1, out double_2, out double_3, out double_4);

                    case 10:
                        return THNO2BL_standard(string_0, out double_1, out double_2, out double_3, out double_4);
                }
            }
            return false;
        }

        private static bool THNO2BL_cqtx(string string_0, out double double_1, out double double_2, out double double_3,
            out double double_4)
        {
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            string str = string_0.ToUpper();
            int num = 0;
            string[] strArray = str.Split(new char[] {'-'});
            if (strArray.Length != 4)
            {
                return false;
            }
            long num2 = Convert.ToInt16(strArray[0]);
            long num3 = Convert.ToInt16(strArray[1]);
            long num4 = Convert.ToInt16(strArray[2]);
            str = str.Substring(num + 1);
            str = strArray[3].Trim();
            if (str[0] == '(')
            {
                str = str.Substring(1, str.Length - 2);
            }
            long num5 = Convert.ToInt16(str.Trim());
            long num6 = (num2 - 97) + 1L;
            long num7 = num3;
            long num8 = num4;
            long num9 = num5;
            double_4 = 0.0625;
            double_3 = 0.041666666666666664;
            double d = (num8 - 1L)/12L;
            int num11 = Convert.ToInt16((double) ((num8 - 1L) - (Math.Truncate(d)*12.0)));
            double num12 = (num9 - 1L)/8L;
            int num13 = Convert.ToInt16((double) ((num9 - 1L) - (Math.Truncate(num12)*8.0)));
            double_2 = (((num7 - 1.0)*6.0) + (num11*0.5)) + (num13*0.0625);
            if (double_2 > 180.0)
            {
                double_2 -= 180.0;
            }
            double_1 = (((num6 - 1.0)*4.0) + ((11.0 - ((int) ((num8 - 1L)/12L)))/3.0)) +
                       ((7.0 - ((int) ((num9 - 1L)/8L)))/24.0);
            return true;
        }

        private static bool THNO2BL_standard(string string_0, out double double_1, out double double_2,
            out double double_3, out double double_4)
        {
            double num6;
            double num7;
            long num8;
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            if ((string_0 != "") && (string_0 != null))
            {
                string str2 = string_0.ToLower();
                if (str2.Length < 10)
                {
                    return false;
                }
                num3 = Convert.ToInt16(str2.Substring(0, 1));
                num4 = Convert.ToInt16(str2.Substring(1, 2));
                str = str2.Substring(3, 1);
                num = Convert.ToInt16(str2.Substring(4, 3));
                num2 = Convert.ToInt16(str2.Substring(7, 3));
                switch (str.ToUpper())
                {
                    case "B":
                        num6 = 1.5;
                        num7 = 1.0;
                        goto Label_025F;

                    case "C":
                        num6 = 1.5;
                        num7 = 1.0;
                        goto Label_025F;

                    case "D":
                        num6 = 0.5;
                        num7 = 0.33333333333333331;
                        goto Label_025F;

                    case "E":
                        num6 = 0.25;
                        num7 = 0.16666666666666666;
                        goto Label_025F;

                    case "F":
                        num6 = 0.125;
                        num7 = 0.083333333333333329;
                        goto Label_025F;

                    case "G":
                        num6 = 0.0625;
                        num7 = 0.041666666666666664;
                        goto Label_025F;

                    case "H":
                        num6 = 0.03125;
                        num7 = 0.020833333333333332;
                        goto Label_025F;

                    case "I":
                        num6 = 0.03125;
                        num7 = 0.020833333333333332;
                        goto Label_025F;
                }
            }
            return false;
            Label_025F:
            num8 = (num3 - 97) + 1;
            long num9 = num4;
            long num10 = num;
            long num11 = num2;
            double_2 = ((num9 - 1.0)*6.0) + ((num11 - 1.0)*num6);
            double_1 = ((num8 - 1.0)*4.0) + (((4.0/num7) - num10)*num7);
            double_4 = num6;
            double_3 = num7;
            return true;
        }

        private static bool THNO2BL_tx(string string_0, out double double_1, out double double_2, out double double_3,
            out double double_4)
        {
            int num;
            int num2;
            int num3;
            int num4;
            double num6;
            double num7;
            long num8;
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            double_4 = 0.0;
            string str = string_0.ToLower();
            if (string_0.Length >= 8)
            {
                string str2 = str.Substring(0, 1);
                char ch = str.Substring(1, 1)[0];
                num = ch;
                num2 = Convert.ToInt16(str.Substring(2, 2));
                num3 = Convert.ToInt16(str.Substring(4, 2));
                num4 = Convert.ToInt16(str.Substring(6, 2));
                switch (str2.ToUpper())
                {
                    case "B":
                        num6 = 1.5;
                        num7 = 1.0;
                        goto Label_0225;

                    case "C":
                        num6 = 1.5;
                        num7 = 1.0;
                        goto Label_0225;

                    case "D":
                        num6 = 0.5;
                        num7 = 0.33333333333333331;
                        goto Label_0225;

                    case "E":
                        num6 = 0.25;
                        num7 = 0.16666666666666666;
                        goto Label_0225;

                    case "F":
                        num6 = 0.125;
                        num7 = 0.083333333333333329;
                        goto Label_0225;

                    case "G":
                        num6 = 0.0625;
                        num7 = 0.041666666666666664;
                        goto Label_0225;

                    case "H":
                        num6 = 0.03125;
                        num7 = 0.020833333333333332;
                        goto Label_0225;

                    case "I":
                        num6 = 0.03125;
                        num7 = 0.020833333333333332;
                        goto Label_0225;
                }
            }
            return false;
            Label_0225:
            num8 = (num - 97) + 1;
            long num9 = num2;
            if (num9 >= 30L)
            {
                num9 -= 30L;
            }
            long num10 = num3;
            long num11 = num4;
            double_2 = ((num9 - 1.0)*6.0) + ((num11 - 1.0)*num6);
            double_1 = ((num8 - 1.0)*4.0) + (((4.0/num7) - num10)*num7);
            double_4 = num6;
            double_3 = num7;
            return true;
        }

        public static bool ValidateTFNo(string string_0)
        {
            int num;
            int num2;
            string str = string_0.ToUpper();
            if (string_0.Length == 8)
            {
                if ((str[0] < 'B') || (str[0] > 'G'))
                {
                    return false;
                }
                if ((str[1] < 'A') || (str[1] > 'N'))
                {
                    return false;
                }
                try
                {
                    num = int.Parse(str.Substring(4, 3));
                    num2 = int.Parse(str.Substring(7, 3));
                    switch (str[3])
                    {
                        case 'B':
                            return ((((num >= 1) && (num <= 2)) && (num2 >= 1)) && (num2 <= 2));

                        case 'C':
                            return ((((num >= 1) && (num <= 4)) && (num2 >= 1)) && (num2 <= 4));

                        case 'D':
                            return ((((num >= 1) && (num <= 12)) && (num2 >= 1)) && (num2 <= 12));

                        case 'E':
                            return ((((num >= 1) && (num <= 24)) && (num2 >= 1)) && (num2 <= 24));

                        case 'F':
                            return ((((num >= 1) && (num <= 48)) && (num2 >= 1)) && (num2 <= 48));

                        case 'G':
                            return ((((num >= 1) && (num <= 96)) && (num2 >= 1)) && (num2 <= 96));
                    }
                    goto Label_031F;
                }
                catch
                {
                    return false;
                }
            }
            if ((string_0.Length != 9) && (string_0.Length == 10))
            {
                if ((str[0] < 'A') || (str[0] > 'N'))
                {
                    return false;
                }
                try
                {
                    num = int.Parse(str.Substring(1, 2));
                    if ((num < 43) || (num > 53))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                if ((str[3] < 'B') || (str[3] > 'H'))
                {
                    return false;
                }
                try
                {
                    num = int.Parse(str.Substring(4, 3));
                    num2 = int.Parse(str.Substring(7, 3));
                    switch (str[3])
                    {
                        case 'B':
                            return ((((num >= 1) && (num <= 2)) && (num2 >= 1)) && (num2 <= 2));

                        case 'C':
                            return ((((num >= 1) && (num <= 4)) && (num2 >= 1)) && (num2 <= 4));

                        case 'D':
                            return ((((num >= 1) && (num <= 12)) && (num2 >= 1)) && (num2 <= 12));

                        case 'E':
                            return ((((num >= 1) && (num <= 24)) && (num2 >= 1)) && (num2 <= 24));

                        case 'F':
                            return ((((num >= 1) && (num <= 48)) && (num2 >= 1)) && (num2 <= 48));

                        case 'G':
                            return ((((num >= 1) && (num <= 96)) && (num2 >= 1)) && (num2 <= 96));

                        case 'H':
                            return ((((num >= 1) && (num <= 192)) && (num2 >= 1)) && (num2 <= 192));
                    }
                }
                catch
                {
                    return false;
                }
            }
            Label_031F:
            return false;
        }

        public double CentralMeridian
        {
            get { return g_iCentralMeridian; }
            set { g_iCentralMeridian = value; }
        }

        public int DadiCoord
        {
            get { return this.int_1; }
            set { this.int_1 = value; }
        }

        public int MainStrip
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        public StandardTypeEnum StandarType
        {
            get { return this.standardTypeEnum_0; }
            set { this.standardTypeEnum_0 = value; }
        }
    }
}