using System;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public sealed class GBMapNoAssistant : MapNoAssistant
    {
        public GBMapNoAssistant(string string_1) : base(string_1)
        {
        }

        public override bool GetBLInfo(out double double_0, out double double_1, out double double_2, out double double_3)
        {
            double num;
            double num2;
            double_0 = 0.0;
            double_1 = 0.0;
            double_2 = 0.0;
            double_3 = 0.0;
            this.method_0(out num, out num2);
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            num5 = Convert.ToInt16(base.MapNo[0]);
            num6 = Convert.ToInt16(base.MapNo.Substring(1, 2));
            if (base.MapNo.Length == 10)
            {
                num3 = Convert.ToInt16(base.MapNo.Substring(4, 3));
                num4 = Convert.ToInt16(base.MapNo.Substring(7, 3));
            }
            else
            {
                num3 = Convert.ToInt16(base.MapNo.Substring(4, 4));
                num4 = Convert.ToInt16(base.MapNo.Substring(8, 4));
            }
            long num7 = (num5 - 0x41) + 1;
            long num8 = num6;
            if (num8 > 30L)
            {
                num8 -= 30L;
            }
            long num9 = num3;
            long num10 = num4;
            double_1 = ((num8 - 1.0) * 6.0) + ((num10 - 1.0) * num2);
            double_0 = ((num7 - 1.0) * 4.0) + (((4.0 / num) - num9) * num);
            double_3 = num2;
            double_2 = num;
            return true;
        }

        public override int GetScale()
        {
            if (base.MapNo.Length < 10)
            {
                throw new MapNoFormatException();
            }
            switch (base.MapNo[3])
            {
                case 'B':
                    return 0x7a120;

                case 'C':
                    return 0x3d090;

                case 'D':
                    return 0x186a0;

                case 'E':
                    return 0xc350;

                case 'F':
                    return 0x61a8;

                case 'G':
                    return 0x2710;

                case 'H':
                    return 0x1388;

                case 'I':
                    return 0x7d0;

                case 'J':
                    return 0x3e8;

                case 'K':
                    return 500;
            }
            throw new MapNoFormatException();
        }

        private void method_0(out double double_0, out double double_1)
        {
            double_0 = 0.0;
            double_1 = 0.0;
            switch (base.MapNo[3])
            {
                case 'B':
                    double_1 = 3.0;
                    double_0 = 2.0;
                    break;

                case 'C':
                    double_1 = 1.5;
                    double_0 = 1.0;
                    break;

                case 'D':
                    double_1 = 0.5;
                    double_0 = 0.33333333333333331;
                    break;

                case 'E':
                    double_1 = 0.25;
                    double_0 = 0.16666666666666666;
                    break;

                case 'F':
                    double_1 = 0.125;
                    double_0 = 0.083333333333333329;
                    break;

                case 'G':
                    double_1 = 0.0625;
                    double_0 = 0.041666666666666664;
                    break;

                case 'H':
                    double_1 = 0.03125;
                    double_0 = 0.020833333333333332;
                    break;

                case 'I':
                    double_1 = 0.010416666666666666;
                    double_0 = 0.0069444444444444441;
                    break;

                case 'J':
                    double_1 = 0.005208333333333333;
                    double_0 = 0.003472222222222222;
                    break;

                case 'K':
                    double_1 = 0.0026041666666666665;
                    double_0 = 0.001736111111111111;
                    break;
            }
        }

        public override bool Validate()
        {
            if ((base.MapNo[0] >= 'A') && (base.MapNo[0] <= 'N'))
            {
                int num;
                try
                {
                    num = int.Parse(base.MapNo.Substring(1, 2));
                    if ((num < 0x2b) || (num > 0x35))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                if ((base.MapNo[3] < 'B') || (base.MapNo[3] > 'K'))
                {
                    return false;
                }
                try
                {
                    int num2;
                    if ((base.MapNo[3] == 'J') || (base.MapNo[3] == 'K'))
                    {
                        num = int.Parse(base.MapNo.Substring(4, 4));
                        num2 = int.Parse(base.MapNo.Substring(8, 4));
                    }
                    else
                    {
                        num = int.Parse(base.MapNo.Substring(4, 3));
                        num2 = int.Parse(base.MapNo.Substring(7, 3));
                        switch (base.MapNo[3])
                        {
                            case 'B':
                                return ((((num >= 1) && (num <= 2)) && (num2 >= 1)) && (num2 <= 2));

                            case 'C':
                                return ((((num >= 1) && (num <= 4)) && (num2 >= 1)) && (num2 <= 4));

                            case 'D':
                                return ((((num >= 1) && (num <= 12)) && (num2 >= 1)) && (num2 <= 12));

                            case 'E':
                                return ((((num >= 1) && (num <= 0x18)) && (num2 >= 1)) && (num2 <= 0x18));

                            case 'F':
                                return ((((num >= 1) && (num <= 0x30)) && (num2 >= 1)) && (num2 <= 0x30));

                            case 'G':
                                return ((((num >= 1) && (num <= 0x60)) && (num2 >= 1)) && (num2 <= 0x60));

                            case 'H':
                                return ((((num >= 1) && (num <= 0xc0)) && (num2 >= 1)) && (num2 <= 0xc0));
                        }
                    }
                }
                catch
                {
                }
            }
            return false;
        }
    }
}

