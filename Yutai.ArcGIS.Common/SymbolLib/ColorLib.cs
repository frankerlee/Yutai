using System.Drawing;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    internal class ColorLib
    {
        public static IColor NullColor
        {
            get
            {
                return new RgbColor
                {
                    NullColor = true
                };
            }
        }

        public static IColor Red
        {
            get { return ColorLib.CreatColor(255, 255, 0, 0); }
        }

        public static IColor Green
        {
            get { return ColorLib.CreatColor(255, 0, 255, 0); }
        }

        public static IColor Blue
        {
            get { return ColorLib.CreatColor(255, 0, 0, 255); }
        }

        public static IColor Yellow
        {
            get { return ColorLib.CreatColor(255, 255, 255, 0); }
        }

        public static IColor Black
        {
            get
            {
                return ColorLib.CreatColor((int) Color.Black.A, (int) Color.Black.R, (int) Color.Black.G,
                    (int) Color.Black.B);
            }
        }

        public static IColor Orange
        {
            get
            {
                return ColorLib.CreatColor((int) Color.Orange.A, (int) Color.Orange.R, (int) Color.Orange.G,
                    (int) Color.Orange.B);
            }
        }

        public static IColor CreatColor(int int_0, int int_1, int int_2, int int_3)
        {
            return new RgbColor
            {
                Transparency = (byte) int_0,
                Red = int_1,
                Green = int_2,
                Blue = int_3
            };
        }

        public static IColor CreatColorByCMYK(int int_0)
        {
            return new RgbColor
            {
                CMYK = int_0
            };
        }

        public static IColor CreatColorByRGB(int int_0)
        {
            return new RgbColor
            {
                RGB = int_0
            };
        }
    }
}