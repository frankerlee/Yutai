using System;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class ConvertHelper
    {
        public ConvertHelper()
        {
        }

        public static double ObjectToDouble(object str)
        {
            double num;
            try
            {
                if (str != null)
                {
                    double num1 = 0;
                    double.TryParse(str.ToString(), out num1);
                    num = num1;
                }
                else
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static double ObjectToDouble(string str)
        {
            double num;
            try
            {
                if ((str == "" ? false : str != null))
                {
                    double num1 = 0;
                    double.TryParse(str, out num1);
                    num = num1;
                }
                else
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static double ObjectToFloat(object str)
        {
            double num;
            try
            {
                if (str != null)
                {
                    float single = 0f;
                    float.TryParse(str.ToString(), out single);
                    num = (double) single;
                }
                else
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static double ObjectToFloat(string str)
        {
            double num;
            try
            {
                if ((str == "" ? false : str != null))
                {
                    float single = 0f;
                    float.TryParse(str, out single);
                    num = (double) single;
                }
                else
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static int ObjectToInt(object str)
        {
            int num;
            try
            {
                if (str != null)
                {
                    int num1 = 0;
                    int.TryParse(str.ToString(), out num1);
                    num = num1;
                }
                else
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static int ObjectToInt(string str)
        {
            int num;
            try
            {
                if (str == null)
                {
                    num = 0;
                }
                else if (!(str == ""))
                {
                    int num1 = 0;
                    int.TryParse(str, out num1);
                    num = num1;
                }
                else
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static string ObjectToString(object str)
        {
            string str1;
            try
            {
                str1 = (str != null ? Convert.ToString(str) : "");
            }
            catch
            {
                str1 = "";
            }
            return str1;
        }
    }
}