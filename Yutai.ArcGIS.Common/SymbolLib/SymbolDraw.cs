using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    internal class SymbolDraw
    {
        public static void DrawSymbol(int int_0, Rectangle rectangle_0, object object_0, double double_0)
        {
            IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                (double) rectangle_0.Bottom);
            tagRECT tagRECT;
            tagRECT.left = rectangle_0.Left;
            tagRECT.right = rectangle_0.Right;
            tagRECT.bottom = rectangle_0.Bottom;
            tagRECT.top = rectangle_0.Top;
            displayTransformation.set_DeviceFrame(ref tagRECT);
            displayTransformation.Bounds = envelope;
            if (double_0 < 1.0 && object_0 is ILineSymbol)
            {
                displayTransformation.Resolution = 36.0/double_0;
            }
            else
            {
                displayTransformation.Resolution = 72.0;
            }
            displayTransformation.ReferenceScale = 1.0;
            displayTransformation.ScaleRatio = double_0;
            ISymbol symbol;
            if (object_0 is ISymbol)
            {
                symbol = (ISymbol) object_0;
            }
            else if (object_0 is IColorRamp)
            {
                IGradientFillSymbol gradientFillSymbol = new GradientFillSymbol();
                ILineSymbol outline = gradientFillSymbol.Outline;
                outline.Width = 0.0;
                gradientFillSymbol.Outline = outline;
                gradientFillSymbol.ColorRamp = (IColorRamp) object_0;
                gradientFillSymbol.GradientAngle = 180.0;
                gradientFillSymbol.GradientPercentage = 1.0;
                gradientFillSymbol.IntervalCount = 100;
                gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;
                symbol = (ISymbol) gradientFillSymbol;
            }
            else if (object_0 is IColor)
            {
                symbol = (ISymbol) new ColorSymbol
                {
                    Color = (IColor) object_0
                };
            }
            else if (object_0 is IAreaPatch)
            {
                symbol = new SimpleFillSymbol() as ISymbol;
                IRgbColor rgbColor = new RgbColor();
                rgbColor.Red = 227;
                rgbColor.Green = 236;
                rgbColor.Blue = 19;
                ((IFillSymbol) symbol).Color = rgbColor;
            }
            else if (object_0 is ILinePatch)
            {
                symbol = new SimpleLineSymbol() as ISymbol;
            }
            else
            {
                if (object_0 is INorthArrow)
                {
                    IDisplay display = new ScreenDisplay();
                    display.StartDrawing(int_0, 0);
                    display.DisplayTransformation = displayTransformation;
                    ((IMapSurround) object_0).Draw(display, null, envelope);
                    display.FinishDrawing();
                    return;
                }
                if (object_0 is IMapSurround)
                {
                    IDisplay display = new ScreenDisplay();
                    display.StartDrawing(int_0, 0);
                    display.DisplayTransformation = displayTransformation;
                    IEnvelope envelope2 = new Envelope() as IEnvelope;
                    envelope2.PutCoords((double) (rectangle_0.Left + 5), (double) (rectangle_0.Top + 5),
                        (double) (rectangle_0.Right - 5), (double) (rectangle_0.Bottom - 5));
                    ((IMapSurround) object_0).Draw(display, null, envelope2);
                    display.FinishDrawing();
                    return;
                }
                if (object_0 is IBackground)
                {
                    IDisplay display = new ScreenDisplay();
                    display.StartDrawing(int_0, 0);
                    display.DisplayTransformation = displayTransformation;
                    IGeometry geometry = ((IBackground) object_0).GetGeometry(display, envelope);
                    ((IBackground) object_0).Draw(display, geometry);
                    display.FinishDrawing();
                    return;
                }
                if (object_0 is IShadow)
                {
                    IDisplay display = new ScreenDisplay();
                    display.StartDrawing(int_0, 0);
                    display.DisplayTransformation = displayTransformation;
                    IGeometry geometry = ((IShadow) object_0).GetGeometry(display, envelope);
                    ((IShadow) object_0).Draw(display, geometry);
                    display.FinishDrawing();
                    return;
                }
                if (object_0 is IBorder)
                {
                    IDisplay display = new ScreenDisplay();
                    display.StartDrawing(int_0, 0);
                    display.DisplayTransformation = displayTransformation;
                    IPointCollection pointCollection = new Polyline();
                    object value = System.Reflection.Missing.Value;
                    IPoint point = new ESRI.ArcGIS.Geometry.Point();
                    point.PutCoords((double) (rectangle_0.X + 4), (double) rectangle_0.Top);
                    pointCollection.AddPoint(point, ref value, ref value);
                    point.PutCoords((double) (rectangle_0.X + 4), (double) rectangle_0.Bottom);
                    pointCollection.AddPoint(point, ref value, ref value);
                    point.PutCoords((double) (rectangle_0.Right - 4), (double) rectangle_0.Bottom);
                    pointCollection.AddPoint(point, ref value, ref value);
                    IGeometry geometry = ((IBorder) object_0).GetGeometry(display, (IGeometry) pointCollection);
                    ((IBorder) object_0).Draw(display, geometry);
                    display.FinishDrawing();
                    return;
                }
                return;
            }
            if (symbol is IPictureFillSymbol || symbol is IPictureLineSymbol)
            {
                symbol.SetupDC(int_0, displayTransformation);
            }
            else
            {
                symbol.SetupDC(int_0, displayTransformation);
            }
            if (symbol is IMarkerSymbol)
            {
                SymbolDraw.DrawSymbol((IMarkerSymbol) symbol, rectangle_0);
            }
            else if (symbol is ILineSymbol)
            {
                SymbolDraw.DrawSymbol((ILineSymbol) symbol, rectangle_0, false);
            }
            else if (symbol is IFillSymbol)
            {
                SymbolDraw.DrawSymbol((IFillSymbol) symbol, rectangle_0);
            }
            else if (symbol is ITextSymbol)
            {
                SymbolDraw.DrawSymbol((ITextSymbol) symbol, rectangle_0);
            }
            symbol.ResetDC();
        }

        private static void DrawSymbol(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
        {
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = (double) ((rectangle_0.Left + rectangle_0.Right)/2);
            point.Y = (double) ((rectangle_0.Bottom + rectangle_0.Top)/2);
            ((ISymbol) imarkerSymbol_0).Draw(point);
        }

        private static void DrawSymbol(ILineSymbol ilineSymbol_0, Rectangle rectangle_0, bool bool_0)
        {
            if (ilineSymbol_0 is IPictureLineSymbol)
            {
                if (((IPictureLineSymbol) ilineSymbol_0).Picture == null)
                {
                    return;
                }
            }
            else if (ilineSymbol_0 is ITemplate)
            {
                bool flag = false;
                int i = 0;
                while (i < ((ITemplate) ilineSymbol_0).PatternElementCount)
                {
                    double num;
                    double num2;
                    ((ITemplate) ilineSymbol_0).GetPatternElement(i, out num, out num2);
                    if (num + num2 <= 0.0)
                    {
                        i++;
                    }
                    else
                    {
                        flag = true;
                        IL_75:
                        if (flag)
                        {
                            object value = System.Reflection.Missing.Value;
                            IPointCollection pointCollection = new Polyline();
                            IPoint point = new ESRI.ArcGIS.Geometry.Point();
                            if (bool_0)
                            {
                                point.PutCoords((double) (rectangle_0.Left + 2), (double) (rectangle_0.Bottom + 2));
                                pointCollection.AddPoint(point, ref value, ref value);
                                point.PutCoords((double) ((rectangle_0.Width - 4)/3 + rectangle_0.Left + 2),
                                    (double) (rectangle_0.Top - 2));
                                pointCollection.AddPoint(point, ref value, ref value);
                                point.PutCoords((double) ((rectangle_0.Width - 4)/3*2 + rectangle_0.Left + 2),
                                    (double) (rectangle_0.Bottom + 2));
                                pointCollection.AddPoint(point, ref value, ref value);
                                point.PutCoords((double) (rectangle_0.Right - 2), (double) (rectangle_0.Top - 2));
                                pointCollection.AddPoint(point, ref value, ref value);
                            }
                            else
                            {
                                point.PutCoords((double) (rectangle_0.Left + 3),
                                    (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
                                pointCollection.AddPoint(point, ref value, ref value);
                                point.PutCoords((double) (rectangle_0.Right - 3),
                                    (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
                                pointCollection.AddPoint(point, ref value, ref value);
                            }
                            ((ISymbol) ilineSymbol_0).Draw((IGeometry) pointCollection);
                        }
                        return;
                    }
                }
            }
        }

        private static void DrawSymbol(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
        {
            object value = System.Reflection.Missing.Value;
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            IPointCollection pointCollection = new Polygon();
            point.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Top + 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Bottom - 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Bottom - 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
            pointCollection.AddPoint(point, ref value, ref value);
            ((ISymbol) ifillSymbol_0).Draw((IGeometry) pointCollection);
        }

        private static void DrawSymbol(ITextSymbol itextSymbol_0, Rectangle rectangle_0)
        {
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = (double) ((rectangle_0.Left + rectangle_0.Right)/2);
            point.Y = (double) ((rectangle_0.Bottom + rectangle_0.Top)/2);
            ISimpleTextSymbol simpleTextSymbol = (ISimpleTextSymbol) itextSymbol_0;
            string text = simpleTextSymbol.Text;
            bool clip = simpleTextSymbol.Clip;
            simpleTextSymbol.Text = "AaBbYyZz";
            simpleTextSymbol.Clip = true;
            ((ISymbol) itextSymbol_0).Draw(point);
            simpleTextSymbol.Text = text;
            simpleTextSymbol.Clip = clip;
        }
    }
}