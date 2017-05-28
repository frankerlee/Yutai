using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common
{
    public class SymbolHelper
    {
        // Methods  
        public static IFillSymbol CreateFillSymbol(Color fillColor, Color outlineColor)
        {
            SimpleFillSymbol class2 = new SimpleFillSymbol();
            class2.Style = esriSimpleFillStyle.esriSFSSolid;
            class2.Color = ColorHelper.CreateColor(fillColor);
            ISimpleLineSymbol symbol = new SimpleLineSymbol();
            symbol.Style = esriSimpleLineStyle.esriSLSSolid;
            symbol.Color = ColorHelper.CreateColor(outlineColor);
            symbol.Width = 1.0;
            class2.Outline = symbol;
            return class2;
        }

        public static IFillSymbol CreateFillSymbol(Color fillColor, esriSimpleFillStyle eFillStyle, ISimpleLineSymbol aOutline)
        {
            SimpleFillSymbol class2 = new SimpleFillSymbol();
            class2.Style = eFillStyle;
            class2.Color = ColorHelper.CreateColor(fillColor);
            class2.Outline = aOutline;
            return class2;
        }

        public static IFillSymbol CreateFillSymbol(Color fillColor, esriSimpleFillStyle eFillStyle, Color outlineColor, double outlineWidth, esriSimpleLineStyle outlineStyle)
        {
            SimpleFillSymbol class2 = new SimpleFillSymbol();
            class2.Style = eFillStyle;
            class2.Color = ColorHelper.CreateColor(fillColor);
            ISimpleLineSymbol symbol = new SimpleLineSymbol();
            symbol.Style = outlineStyle;
            symbol.Color = ColorHelper.CreateColor(outlineColor);
            symbol.Width = outlineWidth;
            class2.Outline = symbol;
            return class2;
        }

        public static IFontDisp CreateFont(string pFontName, float pSize)
        {
            StdFont class2 = new StdFont();
            class2.Name = pFontName;
            class2.Size = Convert.ToDecimal(pSize);
            class2.Bold = false;
            class2.Italic = false;
            class2.Underline = false;
            class2.Strikethrough = false;
            return (class2 as IFontDisp);
        }

        public static IFontDisp CreateFont(string pFontName, float pSize, bool pBold, bool pItalic, bool pUnderline, bool pStroke)
        {
            StdFont class2 = new StdFont();
            class2.Name = pFontName;
            class2.Size = Convert.ToDecimal(pSize);
            class2.Bold = pBold;
            class2.Italic = pItalic;
            class2.Underline = pUnderline;
            class2.Strikethrough = pStroke;
            return (class2 as IFontDisp);
        }

        private static IGeometry CreateGeometryFromSymbol(ISymbol sym, IEnvelope env)
        {
            IPoint point;
            if (sym is IMarkerSymbol)
            {
                IArea area = (IArea)env;
                return area.Centroid;
            }
            if ((sym is ILineSymbol) || (sym is ITextSymbol))
            {
                IPolyline polyline = new Polyline() as IPolyline;
                point = new ESRI.ArcGIS.Geometry.Point() as IPoint;
                point.PutCoords(env.LowerLeft.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2.0);
                polyline.FromPoint = point;
                point = new ESRI.ArcGIS.Geometry.Point() as IPoint;
                point.PutCoords(env.UpperRight.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2.0);
                polyline.ToPoint = point;
                if (sym is ITextSymbol)
                {
                    (sym as ITextSymbol).Text = "样本字符";
                }
                return polyline;
            }
            if (sym is IFillSymbol)
            {
                IPolygon polygon = new Polygon() as IPolygon;
                IPointCollection points = (IPointCollection)polygon;
                point = new ESRI.ArcGIS.Geometry.Point() as IPoint;
                point.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.UpperLeft.X, env.UpperLeft.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.UpperRight.X, env.UpperRight.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.LowerRight.X, env.LowerRight.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                points.AddPoints(1, ref point);
                return polygon;
            }
            return null;
        }

        public static ILineSymbol CreateLineDirectionSymbol()
        {
            ILineSymbol symbol = new CartographicLineSymbol();
            symbol.Color = ColorHelper.CreateColor(0, 0, 200);
            LineDecoration class2 = new LineDecoration();
            SimpleLineDecorationElement lineDecorationElement = new SimpleLineDecorationElement();
            lineDecorationElement.AddPosition(0.3);
            lineDecorationElement.AddPosition(0.7);
            lineDecorationElement.PositionAsRatio = true;
            IMarkerSymbol symbol2 = (lineDecorationElement.MarkerSymbol as IClone).Clone() as IMarkerSymbol;
            symbol2.Size = 9.0;
            symbol2.Color = ColorHelper.CreateColor(0, 200, 0);
            lineDecorationElement.MarkerSymbol = symbol2;
            class2.AddElement(lineDecorationElement);
            (symbol as ILineProperties).LineDecoration = class2;
            return symbol;
        }

        public static ILineSymbol CreateSimpleLineSymbol(Color lineColor, double width)
        {
            SimpleLineSymbol class2 = new SimpleLineSymbol();
            class2.Color = ColorHelper.CreateColor(lineColor);
            class2.Style = esriSimpleLineStyle.esriSLSSolid;
            class2.Width = Math.Abs(width);
            return class2;
        }

        public static ILineSymbol CreateSimpleLineSymbol(Color lineColor, double width, esriSimpleLineStyle eStyle)
        {
            SimpleLineSymbol class2 = new SimpleLineSymbol();
            class2.Color = ColorHelper.CreateColor(lineColor);
            class2.Style = eStyle;
            class2.Width = Math.Abs(width);
            return class2;
        }

        public static IMarkerSymbol CreateSimpleMarkerSymbol(Color pColor, double pSize)
        {
            SimpleMarkerSymbol class2 = new SimpleMarkerSymbol();
            class2.Color = ColorHelper.CreateColor(pColor);
            class2.Size = pSize;
            return class2;
        }

        public static ITextSymbol CreateTextSymbol(Color pColor, IFontDisp pFont, double pSize, string sText)
        {
            ITextSymbol symbol = new TextSymbol();
            symbol.Color = ColorHelper.CreateColor(pColor);
            symbol.Font = pFont;
            symbol.Size = pSize;
            symbol.Text = sText;
            return symbol;
        }

        public static IFillSymbol CreateTransparentFillSymbol(ISimpleLineSymbol aOutline)
        {
            SimpleFillSymbol class2 = new SimpleFillSymbol();
            class2.Style = esriSimpleFillStyle.esriSFSNull;
            class2.Outline = aOutline;
            return class2;
        }

        public static IFillSymbol CreateTransparentFillSymbol(Color outlineColor)
        {
            SimpleFillSymbol class2 = new SimpleFillSymbol();
            class2.Style = esriSimpleFillStyle.esriSFSNull;
            ISimpleLineSymbol symbol = new SimpleLineSymbol();
            symbol.Style = esriSimpleLineStyle.esriSLSSolid;
            symbol.Color = ColorHelper.CreateColor(outlineColor);
            symbol.Width = 1.0;
            class2.Outline = symbol;
            return class2;
        }

        public static Image StyleToImage(ISymbol sym)
        {
            return StyleToImage(sym, 0x10, 0x10);
        }

        public static ITransformation CreateTransformationFromHDC(IntPtr HDC, int width, int height)
        {
            IEnvelope env = new Envelope() as IEnvelope;
            env.PutCoords(0, 0, width, height);
            tagRECT frame = new tagRECT();
            frame.left = 0;
            frame.top = 0;
            frame.right = width;
            frame.bottom = height;
            double dpi = Graphics.FromHdc(HDC).DpiY;
            long lDpi = (long)dpi;
            if (lDpi == 0)
            {
                System.Windows.Forms.MessageBox.Show("获取设备比例尺失败!");
                return null;
            }
            IDisplayTransformation dispTrans = new DisplayTransformation() as IDisplayTransformation;
            dispTrans.Bounds = env;
            dispTrans.VisibleBounds = env;
            dispTrans.set_DeviceFrame(ref frame);
            dispTrans.Resolution = dpi;
            return dispTrans;
        }
        public static Image StyleToImage(ISymbol sym, int width, int height)
        {
            if (sym == null)
            {
                return null;
            }
            try
            {
                Image image = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(image);
                IntPtr hdc = graphics.GetHdc();
                IEnvelope env = new Envelope() as IEnvelope;
                env.XMin = 1.0;
                env.XMax = width - 1;
                env.YMin = 1.0;
                env.YMax = height - 1;
                IGeometry geometry = CreateGeometryFromSymbol(sym, env);
                if (geometry != null)
                {
                    ITransformation transformation = CreateTransformationFromHDC(hdc, width, height);
                    sym.SetupDC((int)hdc, transformation);
                    sym.Draw(geometry);
                    sym.ResetDC();
                }
                graphics.ReleaseHdc(hdc);
                return image;
            }
            catch
            {
                return null;
            }
        }
    }
}
