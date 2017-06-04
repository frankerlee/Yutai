using System;
using System.Drawing;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.Symbol
{
	public class MakeBasicColors
	{
		public MakeBasicColors()
		{
		}

		public static ICmykColor MakeCMYK(MakeBasicColors.basicColorEnum basicColorEnum_0)
		{
			ICmykColor cmykColorClass = new CmykColor()
			{
				RGB = MakeBasicColors.MakeRGB(basicColorEnum_0).RGB
			};
			return cmykColorClass;
		}

		public static IHlsColor MakeHLS(MakeBasicColors.basicColorEnum basicColorEnum_0)
		{
			IHlsColor hlsColorClass = new HlsColor()
			{
				RGB = MakeBasicColors.MakeRGB(basicColorEnum_0).RGB
			};
			return hlsColorClass;
		}

		public static IHsvColor MakeHSV(MakeBasicColors.basicColorEnum basicColorEnum_0)
		{
			IHsvColor hsvColorClass = new HsvColor()
			{
				RGB = MakeBasicColors.MakeRGB(basicColorEnum_0).RGB
			};
			return hsvColorClass;
		}

		public static IRgbColor MakeRandomRGB()
		{
			Random random = new Random();
			IHsvColor hsvColorClass = new HsvColor()
			{
				Hue = random.Next(10, 350),
				Saturation = 100,
				Value = 100
			};
			return new RgbColor()
			{
				RGB = hsvColorClass.RGB
			};
		}

		public static IRgbColor MakeRGB(MakeBasicColors.basicColorEnum basicColorEnum_0)
		{
			IRgbColor rgbColor;
			switch (basicColorEnum_0)
			{
				case MakeBasicColors.basicColorEnum.colorBlack:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 0, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorDarkRed:
				{
					rgbColor = MakeBasicColors.MakeRGB(128, 0, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorDarkGreen:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 128, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorDarkYellow:
				{
					rgbColor = MakeBasicColors.MakeRGB(128, 128, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorDarkBlue:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 0, 128);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorDarkPurple:
				{
					rgbColor = MakeBasicColors.MakeRGB(128, 0, 128);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorDarkCyan:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 128, 128);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorPaleGray:
				{
					rgbColor = MakeBasicColors.MakeRGB(192, 192, 192);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorMidGray:
				{
					rgbColor = MakeBasicColors.MakeRGB(128, 128, 128);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorRed:
				{
					rgbColor = MakeBasicColors.MakeRGB(255, 0, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorGreen:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 255, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorYellow:
				{
					rgbColor = MakeBasicColors.MakeRGB(255, 255, 0);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorBlue:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 0, 255);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorMagenta:
				{
					rgbColor = MakeBasicColors.MakeRGB(255, 0, 255);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorCyan:
				{
					rgbColor = MakeBasicColors.MakeRGB(0, 255, 255);
					break;
				}
				case MakeBasicColors.basicColorEnum.colorWhite:
				{
					rgbColor = MakeBasicColors.MakeRGB(255, 255, 255);
					break;
				}
				default:
				{
					rgbColor = MakeBasicColors.MakeRGB(255, 255, 255);
					break;
				}
			}
			return rgbColor;
		}

		public static IRgbColor MakeRGB(int int_0, int int_1, int int_2)
		{
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = int_0,
				Green = int_1,
				Blue = int_2
			};
			return rgbColorClass;
		}

		public static IRgbColor MakeRGB(int int_0)
		{
			return new RgbColor()
			{
				RGB = int_0
			};
		}

		public static IRgbColor MakeRGB(Color color_0)
		{
			return new RgbColor()
			{
				RGB = ColorTranslator.ToWin32(color_0)
			};
		}

		public static Color RGBToSystemColor(IRgbColor irgbColor_0)
		{
			return ColorTranslator.FromWin32(irgbColor_0.RGB);
		}

		public enum basicColorEnum
		{
			colorBlack,
			colorDarkRed,
			colorDarkGreen,
			colorDarkYellow,
			colorDarkBlue,
			colorDarkPurple,
			colorDarkCyan,
			colorPaleGray,
			colorMidGray,
			colorRed,
			colorGreen,
			colorYellow,
			colorBlue,
			colorMagenta,
			colorCyan,
			colorWhite
		}
	}
}