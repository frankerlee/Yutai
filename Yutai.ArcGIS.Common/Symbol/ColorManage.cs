using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.Symbol
{
	public class ColorManage
	{
		public static IRgbColor Blue
		{
			get
			{
				return ColorManage.GetRGBColor(0, 0, 255);
			}
		}

		public static IRgbColor Green
		{
			get
			{
				return ColorManage.GetRGBColor(0, 255, 0);
			}
		}

		public static IColor NullColor
		{
			get
			{
				return new RgbColor()
				{
					NullColor = true
				};
			}
		}

		public static IRgbColor Red
		{
			get
			{
				return ColorManage.GetRGBColor(255, 0, 0);
			}
		}

		public ColorManage()
		{
		}

		public static IColor CreatColor(int int_0, int int_1, int int_2, int int_3)
		{
			IRgbColor rgbColorClass = new RgbColor()
			{
				Transparency = (byte)int_0,
				Red = int_1,
				Green = int_2,
				Blue = int_3
			};
			return rgbColorClass;
		}

		public static IColor CreatColor(int int_0, int int_1, int int_2)
		{
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = int_0,
				Green = int_1,
				Blue = int_2
			};
			return rgbColorClass;
		}

		public static IColor CreatColorByCMYK(int int_0)
		{
			return new RgbColor()
			{
				CMYK = int_0
			};
		}

		public static IColor CreatColorByRGB(int int_0)
		{
			return new RgbColor()
			{
				RGB = int_0
			};
		}

		public static IColorRamp CreateColorRamp()
		{
			IAlgorithmicColorRamp algorithmicColorRampClass = new AlgorithmicColorRamp();
			IRgbColor rgbColor = ColorManage.CreatColor(56, 168, 0) as IRgbColor;
			IRgbColor rgbColor1 = ColorManage.CreatColor(255, 0, 0) as IRgbColor;
			algorithmicColorRampClass.FromColor = rgbColor;
			algorithmicColorRampClass.ToColor = rgbColor1;
			return algorithmicColorRampClass;
		}

		public static IAlgorithmicColorRamp CreateColorRamp(esriColorRampAlgorithm esriColorRampAlgorithm_0)
		{
			IAlgorithmicColorRamp algorithmicColorRampClass = new AlgorithmicColorRamp()
			{
				Algorithm = esriColorRampAlgorithm_0
			};
			IRgbColor rgbColor = ColorManage.CreatColor(255, 200, 200) as IRgbColor;
			algorithmicColorRampClass.FromColor = ColorManage.CreatColor(255, 0, 0) as IRgbColor;
			algorithmicColorRampClass.ToColor = rgbColor;
			return algorithmicColorRampClass;
		}

		public static IEnumColors CreateColors(IColorRamp icolorRamp_0, int int_0)
		{
			IEnumColors colors;
			if (icolorRamp_0 != null)
			{
				icolorRamp_0.Size = int_0;
				bool flag = false;
				try
				{
					icolorRamp_0.CreateRamp(out flag);
					if (flag)
					{
						colors = icolorRamp_0.Colors;
						return colors;
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.ToString());
				}
			}
			colors = null;
			return colors;
		}

		public static int EsriRGB(int int_0, int int_1, int int_2)
		{
			uint int2 = 0;
			int2 = (uint)(0 | int_2);
			return (int)((int2 << 8 | int_1) << 8 | int_0);
		}

		public static int ESRIRGBToWindowsRGB(uint uint_0)
		{
			int num;
			int num1;
			int num2;
			ColorManage.GetEsriRGB(uint_0, out num, out num1, out num2);
			return ColorManage.RGB(num, num1, num2);
		}

		public static void GetEsriRGB(uint uint_0, out int int_0, out int int_1, out int int_2)
		{
			int_2 = (int)((uint_0 & 16711680) >> 16);
			int_1 = (int)((uint_0 & 65280) >> 8);
			int_0 = (int)(uint_0 & 255);
		}

		public static void GetRGB(uint uint_0, out int int_0, out int int_1, out int int_2)
		{
			int_0 = (int)((uint_0 & 16711680) >> 16);
			int_1 = (int)((uint_0 & 65280) >> 8);
			int_2 = (int)(uint_0 & 255);
		}

		public static IRgbColor GetRGBColor(int int_0, int int_1, int int_2)
		{
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = int_0,
				Green = int_1,
				Blue = int_2,
				UseWindowsDithering = true
			};
			return rgbColorClass;
		}

		public static int RGB(int int_0, int int_1, int int_2)
		{
			uint int0 = 0;
			int0 = (uint)(0 | int_0);
			return (int)((int0 << 8 | int_1) << 8 | int_2);
		}

		public static int WindowsRGBToESRIRGB(uint uint_0)
		{
			int num;
			int num1;
			int num2;
			ColorManage.GetRGB(uint_0, out num, out num1, out num2);
			return ColorManage.EsriRGB(num, num1, num2);
		}
	}
}