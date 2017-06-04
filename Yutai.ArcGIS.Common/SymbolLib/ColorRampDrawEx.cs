using System.Drawing;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	internal class ColorRampDrawEx : StyleDraw
	{
		public ColorRampDrawEx(IColorRamp icolorRamp_0) : base(icolorRamp_0)
		{
		}

		public override void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1)
		{
			IGradientFillSymbol gradientFillSymbol = new GradientFillSymbol();
			ILineSymbol outline = gradientFillSymbol.Outline;
			outline.Width = 0.0;
			gradientFillSymbol.Outline = outline;
			gradientFillSymbol.ColorRamp = (this.m_pStyle as IColorRamp);
			gradientFillSymbol.GradientAngle = 180.0;
			gradientFillSymbol.GradientPercentage = 1.0;
			gradientFillSymbol.IntervalCount = 100;
			gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;
			FillSymbolDraw fillSymbolDraw = new FillSymbolDraw(gradientFillSymbol as ISymbol);
			fillSymbolDraw.Draw(int_0, rectangle_0, double_0, double_1);
		}
	}
}
