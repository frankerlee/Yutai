using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    internal class TextSymbolDrawEx : StyleDraw
    {
        public TextSymbolDrawEx(ISymbol isymbol_0) : base(isymbol_0)
        {
        }

        public override void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1)
        {
            IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
            IEnvelope envelope = new Envelope() as ESRI.ArcGIS.Geometry.IEnvelope;
            envelope.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                (double) rectangle_0.Bottom);
            tagRECT tagRECT;
            tagRECT.left = rectangle_0.Left;
            tagRECT.right = rectangle_0.Right;
            tagRECT.bottom = rectangle_0.Bottom;
            tagRECT.top = rectangle_0.Top;
            displayTransformation.set_DeviceFrame(ref tagRECT);
            displayTransformation.Bounds = envelope;
            displayTransformation.Resolution = 72.0;
            displayTransformation.ReferenceScale = 1.0;
            displayTransformation.ScaleRatio = 1.0;
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = (double) ((rectangle_0.Left + rectangle_0.Right)/2);
            point.Y = (double) ((rectangle_0.Bottom + rectangle_0.Top)/2);
            ISymbol symbol = this.m_pStyle as ISymbol;
            symbol.SetupDC(int_0, displayTransformation);
            ISimpleTextSymbol simpleTextSymbol = (ISimpleTextSymbol) symbol;
            string text = simpleTextSymbol.Text;
            bool clip = simpleTextSymbol.Clip;
            simpleTextSymbol.Text = "AaBbYyZz";
            simpleTextSymbol.Clip = true;
            symbol.Draw(point);
            simpleTextSymbol.Text = text;
            simpleTextSymbol.Clip = clip;
            symbol.ResetDC();
        }
    }
}