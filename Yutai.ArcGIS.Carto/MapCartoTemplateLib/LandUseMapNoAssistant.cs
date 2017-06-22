using System;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public sealed class LandUseMapNoAssistant : MapNoAssistant
    {
        public LandUseMapNoAssistant(string string_1) : base(string_1)
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
            char ch = base.MapNo[1];
            int num3 = ch;
            int num4 = Convert.ToInt16(base.MapNo.Substring(2, 2));
            int num5 = Convert.ToInt16(base.MapNo.Substring(4, 2));
            int num6 = Convert.ToInt16(base.MapNo.Substring(6, 2));
            long num7 = (num3 - 65) + 1;
            long num8 = num4;
            if (num8 >= 30L)
            {
                num8 -= 30L;
            }
            long num9 = num5;
            long num10 = num6;
            double_1 = ((num8 - 1.0) * 6.0) + ((num10 - 1.0) * num2);
            double_0 = ((num7 - 1.0) * 4.0) + (((4.0 / num) - num9) * num);
            double_3 = num2;
            double_2 = num;
            return true;
        }

        public override int GetScale()
        {
            if (base.MapNo.Length != 8)
            {
                throw new MapNoFormatException();
            }
            switch (base.MapNo[0])
            {
                case 'B':
                    return 500000;

                case 'C':
                    return 250000;

                case 'D':
                    return 100000;

                case 'E':
                    return 50000;

                case 'F':
                    return 25000;

                case 'G':
                    return 10000;

                case 'H':
                    return 5000;

                case 'I':
                    return 2000;
            }
            throw new MapNoFormatException();
        }

        private void method_0(out double double_0, out double double_1)
        {
            double_0 = 0.0;
            double_1 = 0.0;
            switch (base.MapNo[0])
            {
                case 'B':
                    double_1 = 1.5;
                    double_0 = 1.0;
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
                    double_1 = 0.03125;
                    double_0 = 0.020833333333333332;
                    break;
            }
        }

        public override bool Validate()
        {
            if (base.MapNo.Length == 8)
            {
                if ((base.MapNo[0] < 'B') || (base.MapNo[0] > 'G'))
                {
                    return false;
                }
                if ((base.MapNo[1] < 'A') || (base.MapNo[1] > 'N'))
                {
                    return false;
                }
                try
                {
                    int num = int.Parse(base.MapNo.Substring(4, 2));
                    int num2 = int.Parse(base.MapNo.Substring(6, 2));
                    switch (base.MapNo[0])
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
                }
                catch
                {
                }
            }
            return false;
        }
    }
}

