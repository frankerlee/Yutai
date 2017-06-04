using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	internal class FillSymbolDrawEx : StyleDraw
	{
		public FillSymbolDrawEx(ISymbol isymbol_0) : base(isymbol_0)
		{
		}

		public override void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1)
		{
			IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
			IEnvelope envelope = new Envelope() as IEnvelope;
			envelope.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
			tagRECT tagRECT;
			tagRECT.left = rectangle_0.Left;
			tagRECT.right = rectangle_0.Right;
			tagRECT.bottom = rectangle_0.Bottom;
			tagRECT.top = rectangle_0.Top;
			displayTransformation.set_DeviceFrame ( ref tagRECT);
			displayTransformation.Bounds = envelope;
			displayTransformation.Resolution = double_0;
			displayTransformation.ReferenceScale = 1.0;
			displayTransformation.ScaleRatio = double_1;
			ISymbol symbol = this.m_pStyle as ISymbol;
			symbol.SetupDC(int_0, displayTransformation);
			object value = System.Reflection.Missing.Value;
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			IPointCollection pointCollection = new Polygon();
			point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Top + 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Bottom - 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Bottom - 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
			pointCollection.AddPoint(point, ref value, ref value);
			symbol.Draw((IGeometry)pointCollection);
			symbol.ResetDC();
		}
	}
}
