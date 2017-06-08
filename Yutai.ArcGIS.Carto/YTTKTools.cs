using System;
using System.Collections.Generic;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.DesignLib;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Carto
{
    internal class YTTKTools
    {
        private bool bool_0 = false;
        private double double_0 = 1.0;
        private double double_1 = 0.1;
        private double double_2 = 1000.0;
        private double double_3 = 1000.0;
        private double double_4 = 10000.0;
        private double double_5 = 0.3;
        private double double_6 = 1000.0;
        private SpheroidType spheroidType_0 = SpheroidType.Xian1980;
        private string string_0 = "";
        private string string_1 = "";
        private string string_10 = "";
        private string string_11 = "";
        private string string_12 = "";
        private string string_13 = "";
        private string string_14 = "";
        private string string_15 = "1:10000";
        private string string_2 = "";
        private string string_3 = "";
        private string string_4 = "";
        private string string_5 = "";
        private string string_6 = "";
        private string string_7 = "";
        private string string_8 = "";
        private string string_9 = "";
        private StripType stripType_0 = StripType.STThreeDeg;
        private TKType tktype_0 = TKType.TKStandard;

        public IElement CreateCornerShortLine(IPoint ipoint_0, IPoint ipoint_1)
        {
            ILineSymbol symbol = this.method_3();
            IGroupElement element = new GroupElementClass();
            element.AddElement(this.method_21(ipoint_0.X, ipoint_0.Y, -this.double_0, -this.double_0, symbol));
            element.AddElement(this.method_21(ipoint_0.X, ipoint_1.Y, -this.double_0, this.double_0, symbol));
            element.AddElement(this.method_21(ipoint_1.X, ipoint_1.Y, this.double_0, this.double_0, symbol));
            element.AddElement(this.method_21(ipoint_1.X, ipoint_0.Y, this.double_0, -this.double_0, symbol));
            return (element as IElement);
        }

        public IElement CreateCornerShortLine(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            ILineSymbol symbol = this.method_3();
            IGroupElement element = new GroupElementClass();
            element.AddElement(this.method_21(ipoint_0.X, ipoint_0.Y, -this.double_0, -this.double_0, symbol));
            element.AddElement(this.method_21(ipoint_1.X, ipoint_1.Y, -this.double_0, this.double_0, symbol));
            element.AddElement(this.method_21(ipoint_2.X, ipoint_2.Y, this.double_0, this.double_0, symbol));
            element.AddElement(this.method_21(ipoint_3.X, ipoint_3.Y, this.double_0, -this.double_0, symbol));
            return (element as IElement);
        }

        public IElement CreateJionTab(IActiveView iactiveView_0, IPoint ipoint_0)
        {
            IGroupElement element = new GroupElementClass();
            new PointClass();
            new TextElementClass();
            new TextElementClass();
            new TextElementClass();
            new TextElementClass();
            new TextElementClass();
            new TextElementClass();
            new TextElementClass();
            new TextElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            double num = 3.9;
            double num2 = 2.1;
            double num3 = num2 / 3.0;
            double num4 = num / 3.0;
            double num5 = (0.2 + this.double_0) + this.double_5;
            object missing = Type.Missing;
            IElement element3 = new LineElementClass();
            IElement element4 = new LineElementClass();
            IElement element5 = new LineElementClass();
            IElement element6 = new LineElementClass();
            IElement element7 = new LineElementClass();
            IPolyline polyline = new PolylineClass();
            IPolyline polyline2 = new PolylineClass();
            IPolyline polyline3 = new PolylineClass();
            IPolyline polyline4 = new PolylineClass();
            IPolyline polyline5 = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            try
            {
                symbol = this.method_3();
                element2 = element3 as ILineElement;
                element2.Symbol = symbol;
                element2 = element4 as ILineElement;
                element2.Symbol = symbol;
                element2 = element5 as ILineElement;
                element2.Symbol = symbol;
                element2 = element6 as ILineElement;
                element2.Symbol = symbol;
                element2 = element7 as ILineElement;
                element2.Symbol = symbol;
                point2.PutCoords(ipoint_0.X, ipoint_0.Y + num5);
                inPoint.PutCoords(ipoint_0.X, (ipoint_0.Y + num5) + num2);
                point3.PutCoords(ipoint_0.X + num, ipoint_0.Y + num5);
                point4.PutCoords(ipoint_0.X + num, (ipoint_0.Y + num5) + num2);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point2, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(inPoint, ref missing, ref missing);
                element3.Geometry = polyline;
                element.AddElement(element3);
                IPoint point5 = new PointClass();
                IPoint point6 = new PointClass();
                point5.PutCoords(inPoint.X, inPoint.Y - num3);
                point6.PutCoords(point4.X, point4.Y - num3);
                points = polyline2 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element4.Geometry = polyline2;
                element.AddElement(element4);
                point5.PutCoords(inPoint.X, inPoint.Y - (num3 * 2.0));
                point6.PutCoords(point4.X, point4.Y - (num3 * 2.0));
                points = polyline3 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element5.Geometry = polyline3;
                element.AddElement(element5);
                point5.PutCoords(inPoint.X + num4, inPoint.Y);
                point6.PutCoords(inPoint.X + num4, point2.Y);
                points = polyline4 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element6.Geometry = polyline4;
                element.AddElement(element6);
                point5.PutCoords(inPoint.X + (num4 * 2.0), inPoint.Y);
                point6.PutCoords(inPoint.X + (num4 * 2.0), point2.Y);
                points = polyline5 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element7.Geometry = polyline5;
                element.AddElement(element7);
                IPolygon polygon = new PolygonClass();
                IElement element8 = new PolygonElementClass();
                IPolygonElement element9 = element8 as IPolygonElement;
                ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass();
                IFillShapeElement element10 = element9 as IFillShapeElement;
                IRgbColor color = new RgbColorClass {
                    Red = 0,
                    Green = 0,
                    Blue = 0
                };
                symbol2.Outline = this.method_3();
                symbol2.Color = color;
                symbol2.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
                element10.Symbol = symbol2;
                points = polygon as IPointCollection;
                point5.PutCoords(inPoint.X + num4, inPoint.Y - num3);
                points.AddPoint(point5, ref missing, ref missing);
                point5.PutCoords(inPoint.X + (num4 * 2.0), inPoint.Y - num3);
                points.AddPoint(point5, ref missing, ref missing);
                point5.PutCoords(inPoint.X + (num4 * 2.0), inPoint.Y - (num3 * 2.0));
                points.AddPoint(point5, ref missing, ref missing);
                point5.PutCoords(inPoint.X + num4, inPoint.Y - (num3 * 2.0));
                points.AddPoint(point5, ref missing, ref missing);
                polygon.Close();
                element8.Geometry = polygon;
                element.AddElement(element8);
                IEnvelope envelope = polygon.Envelope;
                new EnvelopeClass();
                if (this.string_7.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_7, inPoint.X + (num4 / 2.0), inPoint.Y - (num3 / 2.0), envelope));
                }
                if (this.string_8.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_8, inPoint.X + (num4 / 2.0), inPoint.Y - (1.5 * num3), envelope));
                }
                if (this.string_9.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_9, inPoint.X + (num4 / 2.0), inPoint.Y - (2.5 * num3), envelope));
                }
                if (this.string_10.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_10, inPoint.X + (1.5 * num4), inPoint.Y - (0.5 * num3), envelope));
                }
                if (this.string_11.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_11, inPoint.X + (1.5 * num4), inPoint.Y - (2.5 * num3), envelope));
                }
                if (this.string_12.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_12, inPoint.X + (2.5 * num4), inPoint.Y - (0.5 * num3), envelope));
                }
                if (this.string_13.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_13, inPoint.X + (2.5 * num4), inPoint.Y - (1.5 * num3), envelope));
                }
                if (this.string_13.Trim().Length > 0)
                {
                    element.AddElement(this.method_33(iactiveView_0, this.string_13, inPoint.X + (2.5 * num4), inPoint.Y - (2.5 * num3), envelope));
                }
            }
            catch (Exception)
            {
            }
            return (element as IElement);
        }

        public void CreateMapGrid(IMapFrame imapFrame_0)
        {
        }

        public IElement CreateRemarkText(IActiveView iactiveView_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            double num = 0.15 + this.double_5;
            IPoint point = new PointClass();
            IElement element = null;
            IGroupElement element2 = new GroupElementClass();
            ITextSymbol symbol = null;
            try
            {
                if (this.string_3.Length > 0)
                {
                    ITextElement element3 = new TextElementClass {
                        Text = this.string_3
                    };
                    symbol = this.FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
                    element3.Symbol = symbol;
                    element = element3 as IElement;
                    point.PutCoords(ipoint_1.X, (ipoint_1.Y + this.double_0) + num);
                    element.Geometry = point;
                    element2.AddElement(element);
                }
                if (this.string_4.Length > 0)
                {
                    ITextElement element4 = new TextElementClass {
                        Text = this.string_4
                    };
                    symbol = this.FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
                    element4.Symbol = symbol;
                    element = element4 as IElement;
                    point.PutCoords(ipoint_1.X, (ipoint_0.Y - this.double_0) - num);
                    element.Geometry = point;
                    element2.AddElement(element);
                }
                if (this.string_5.Length > 0)
                {
                    ITextElement element5 = new TextElementClass {
                        Text = this.string_5
                    };
                    symbol = this.FontStyle(15.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
                    element5.Symbol = symbol;
                    element = element5 as IElement;
                    point.PutCoords(ipoint_0.X, (ipoint_0.Y - this.double_0) - num);
                    element.Geometry = point;
                    element2.AddElement(element);
                }
                if (this.string_6.Length > 0)
                {
                    double num2;
                    double num3;
                    ITextElement element6 = new TextElementClass {
                        Text = this.method_7(this.string_6)
                    };
                    symbol = this.FontStyle(20.0, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
                    element6.Symbol = symbol;
                    ITextElement element7 = new TextElementClass {
                        Text = this.string_6,
                        Symbol = symbol
                    };
                    point.PutCoords(0.0, 0.0);
                    element = element6 as IElement;
                    element.Geometry = point;
                    this.method_8(iactiveView_0, element6, out num2, out num3);
                    point.PutCoords((ipoint_0.X - this.double_0) - num, ipoint_0.Y + num3);
                    element.Geometry = point;
                    element2.AddElement(element);
                }
                ITextElement element8 = new TextElementClass {
                    Text = this.string_15
                };
                symbol = this.FontStyle(15.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
                element8.Symbol = symbol;
                element = element8 as IElement;
                point.PutCoords((ipoint_0.X + ipoint_1.X) / 2.0, (ipoint_0.Y - this.double_0) - num);
                element.Geometry = point;
                element2.AddElement(element);
            }
            catch (Exception)
            {
            }
            return (element2 as IElement);
        }

        public IElement CreateTitle(IActiveView iactiveView_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            try
            {
                IPoint point = new PointClass();
                point.PutCoords((ipoint_0.X + ipoint_1.X) / 2.0, ((ipoint_1.Y + this.double_0) + this.double_1) + this.double_5);
                ITextSymbol symbol = this.FontStyle(25.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
                IElement element = new TextElementClass {
                    Geometry = point
                };
                ITextElement element2 = element as ITextElement;
                element2.Symbol = symbol;
                if (this.string_2.Length > 0)
                {
                    double num;
                    double num2;
                    element2.Text = this.string_2;
                    this.method_8(iactiveView_0, element2, out num, out num2);
                    point.Y += num2;
                    element.Geometry = point;
                    element2.Text = this.string_1 + "\n" + this.string_2;
                }
                else
                {
                    element2.Text = this.string_1;
                }
                IElementProperties2 properties = element2 as IElementProperties2;
                properties.Type = "图名";
                return element;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public void CreateTK(IPageLayout ipageLayout_0)
        {
            if (this.tktype_0 == TKType.TKRand)
            {
                IEnvelope extent = ((ipageLayout_0 as IActiveView).FocusMap as IActiveView).Extent;
                this.CreateTK(ipageLayout_0, extent.LowerLeft, extent.UpperLeft, extent.UpperRight, extent.LowerRight);
            }
            else
            {
                this.CreateTK(ipageLayout_0, this.string_2);
            }
        }

        public void CreateTK(IPageLayout ipageLayout_0, string string_16)
        {
            IMapFrame frame = this.method_1(ipageLayout_0);
            if (frame != null)
            {
                THTools tools = new THTools();
                bool flag = false;
                IList<IPoint> list = tools.GetProjectCoord(string_16, this.spheroidType_0 == SpheroidType.Xian1980, this.stripType_0 == StripType.STSixDeg, 0, ref flag);
                if (flag)
                {
                    this.Scale = THTools.GetTHScale(string_16);
                    double xMin = (list[3].X < list[0].X) ? list[3].X : list[0].X;
                    double xMax = (list[1].X > list[2].X) ? list[1].X : list[2].X;
                    double yMin = (list[3].Y < list[2].Y) ? list[3].Y : list[2].Y;
                    double yMax = (list[1].Y > list[0].Y) ? list[1].Y : list[0].Y;
                    double num5 = ((xMax - xMin) / this.double_4) * 100.0;
                    double num6 = ((yMax - yMin) / this.double_4) * 100.0;
                    IEnvelope from = (frame as IElement).Geometry.Envelope;
                    IEnvelope to = new EnvelopeClass();
                    to.PutCoords(from.XMin, from.YMin, from.XMin + num5, from.YMin + num6);
                    IAffineTransformation2D transformationd = new AffineTransformation2DClass();
                    transformationd.DefineFromEnvelopes(from, to);
                    (frame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
                    to = new EnvelopeClass();
                    to.PutCoords(xMin, yMin, xMax, yMax);
                    (frame.Map as IActiveView).Extent = to;
                    ipageLayout_0.Page.PutCustomSize(num5 + 2.0, num6 + 2.0);
                    YTTransformation transformation = new YTTransformation(ipageLayout_0 as IActiveView);
                    IGroupElement element = this.method_2(ipageLayout_0, list[3], list[0], list[1], list[2]);
                    IElement element2 = null;
                    element2 = this.method_18(ipageLayout_0 as IActiveView, string_16, transformation.ToPageLayoutPoint(list[3]), transformation.ToPageLayoutPoint(list[0]), transformation.ToPageLayoutPoint(list[1]), transformation.ToPageLayoutPoint(list[2]));
                    IEnvelope bounds = new EnvelopeClass();
                    if (element2 != null)
                    {
                        if (element2 is IGroupElement)
                        {
                            element2.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                            (element2 as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
                        }
                        element.AddElement(element2);
                    }
                    (element as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                    (ipageLayout_0 as IGraphicsContainer).AddElement(element as IElement, -1);
                    (element as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
                    (ipageLayout_0 as IGraphicsContainer).UpdateElement(element as IElement);
                    if (this.bool_0)
                    {
                        YTLegendAssiatant assiatant = new YTLegendAssiatant();
                        assiatant.Load(this.string_0);
                        IPoint point = transformation.ToPageLayoutPoint(list[2]);
                        point.X = (point.X + this.double_0) + this.double_5;
                        assiatant.Create(ipageLayout_0 as IActiveView, point);
                    }
                    (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        public void CreateTK(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            YTTransformation transformation = new YTTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            element2 = this.method_24(transformation, ipoint_0, ipoint_1);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.method_11(point, point2, this.method_5() as ISymbol);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            try
            {
            }
            catch
            {
            }
            element2 = this.CreateTitle(ipageLayout_0 as IActiveView, point, point2);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.CreateCornerShortLine(point, point2);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.method_10(point, point2);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.CreateRemarkText(ipageLayout_0 as IActiveView, point, point2);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            IEnvelope bounds = new EnvelopeClass();
            (element as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (element as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
            (ipageLayout_0 as IGraphicsContainer).AddElement(element as IElement, -1);
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public void CreateTK(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            YTTransformation transformation = new YTTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IPoint point3 = transformation.ToPageLayoutPoint(ipoint_2);
            IPoint point4 = transformation.ToPageLayoutPoint(ipoint_3);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            element2 = this.method_25(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.method_14(point, point2, point3, point4, this.method_5() as ISymbol);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            try
            {
                element2 = this.method_31(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
                if (element2 != null)
                {
                    element.AddElement(element2);
                }
            }
            catch
            {
            }
            element2 = this.CreateTitle(ipageLayout_0 as IActiveView, point, point3);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.CreateCornerShortLine(point, point2, point3, point4);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.method_9(point, point2, point3, point4);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.CreateRemarkText(ipageLayout_0 as IActiveView, point, point3);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            IEnvelope bounds = new EnvelopeClass();
            (element as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (element as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
            (element as IElementProperties).Type = "图框";
            (ipageLayout_0 as IGraphicsContainer).AddElement(element as IElement, -1);
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            if (this.bool_0)
            {
                YTLegendAssiatant assiatant = new YTLegendAssiatant();
                assiatant.Load(this.string_0);
                assiatant.Create(ipageLayout_0 as IActiveView, point3);
            }
        }

        public void DrawBackFrame(IPoint ipoint_0, IPoint ipoint_1)
        {
        }

        public void DrawLegend(IPoint ipoint_0, IPoint ipoint_1)
        {
        }

        protected ITextSymbol FontStyle(double double_7, esriTextHorizontalAlignment esriTextHorizontalAlignment_0, esriTextVerticalAlignment esriTextVerticalAlignment_0)
        {
            ITextSymbol symbol = new TextSymbolClass();
            IRgbColor color = new RgbColorClass {
                Blue = 0,
                Red = 0,
                Green = 0
            };
            symbol.Size = double_7;
            symbol.Color = color;
            symbol.HorizontalAlignment = esriTextHorizontalAlignment_0;
            symbol.VerticalAlignment = esriTextVerticalAlignment_0;
            return symbol;
        }

        private IMapFrame method_0(IGroupElement igroupElement_0, IMap imap_0)
        {
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            for (IElement element2 = elements.Next(); element2 != null; element2 = elements.Next())
            {
                if (element2 is IMapFrame)
                {
                    if (imap_0 == (element2 as IMapFrame).Map)
                    {
                        return (element2 as IMapFrame);
                    }
                }
                else if (element2 is IGroupElement)
                {
                    IMapFrame frame2 = this.method_0(element2 as IGroupElement, imap_0);
                    if (frame2 != null)
                    {
                        return frame2;
                    }
                }
            }
            return null;
        }

        private IMapFrame method_1(IPageLayout ipageLayout_0)
        {
            IActiveView view = ipageLayout_0 as IActiveView;
            IMap focusMap = view.FocusMap;
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if (element is IMapFrame)
                {
                    if (focusMap == (element as IMapFrame).Map)
                    {
                        return (element as IMapFrame);
                    }
                }
                else if (element is IGroupElement)
                {
                    IMapFrame frame2 = this.method_0(element as IGroupElement, focusMap);
                    if (frame2 != null)
                    {
                        return frame2;
                    }
                }
            }
            return null;
        }

        private IElement method_10(IPoint ipoint_0, IPoint ipoint_1)
        {
            IElement element = new LineElementClass();
            IElementProperties2 properties = null;
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_1.Y);
            points.AddPoint(inPoint, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_1.X, ipoint_0.Y);
            points.AddPoint(inPoint, ref missing, ref missing);
            points.AddPoint(ipoint_0, ref missing, ref missing);
            element.Geometry = polyline;
            properties = element as IElementProperties2;
            properties.Type = "内框";
            symbol = this.method_4(1);
            element2 = element as ILineElement;
            element2.Symbol = symbol;
            return element;
        }

        private IElement method_11(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_0)
        {
            if (isymbol_0 is ILineSymbol)
            {
                return this.method_12(ipoint_0, ipoint_1, isymbol_0);
            }
            return this.method_13(ipoint_0, ipoint_1, isymbol_0);
        }

        private IElement method_12(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_0)
        {
            IElement element = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_1.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + this.double_0, ipoint_1.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                element.Geometry = polyline;
                properties = element as IElementProperties2;
                properties.Type = "外框";
                symbol = isymbol_0 as ILineSymbol;
                element2 = element as ILineElement;
                element2.Symbol = symbol;
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_13(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_0)
        {
            IElement element = new PolygonElementClass();
            IFillShapeElement element2 = element as IFillShapeElement;
            IGeometryCollection geometrys = new PolygonClass();
            IElementProperties2 properties = null;
            object missing = Type.Missing;
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = new RingClass();
            IPointCollection points = inGeometry as IPointCollection;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_1.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + this.double_0, ipoint_1.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                points = geometry2 as IPointCollection;
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5, (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5, (ipoint_1.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.double_0) + this.double_5, (ipoint_1.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.double_0) + this.double_5, (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5, (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(geometry2, ref missing, ref missing);
                element.Geometry = geometrys as IGeometry;
                element2.Symbol = isymbol_0 as IFillSymbol;
                properties = element as IElementProperties2;
                properties.Type = "外框";
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_14(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, ISymbol isymbol_0)
        {
            if (isymbol_0 is ILineSymbol)
            {
                return this.method_15(ipoint_0, ipoint_1, ipoint_2, ipoint_3, isymbol_0 as ILineSymbol);
            }
            return this.method_16(ipoint_0, ipoint_1, ipoint_2, ipoint_3, isymbol_0 as IFillSymbol);
        }

        private IElement method_15(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, ILineSymbol ilineSymbol_0)
        {
            IElement element = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X - this.double_0, ipoint_1.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_2.X + this.double_0, ipoint_2.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_3.X + this.double_0, ipoint_3.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                element.Geometry = polyline;
                properties = element as IElementProperties2;
                properties.Type = "外框";
                symbol = ilineSymbol_0;
                element2 = element as ILineElement;
                element2.Symbol = symbol;
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_16(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, IFillSymbol ifillSymbol_0)
        {
            IElement element = new PolygonElementClass();
            IFillShapeElement element2 = element as IFillShapeElement;
            IGeometryCollection geometrys = new PolygonClass();
            IElementProperties2 properties = null;
            object missing = Type.Missing;
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = new RingClass();
            IPointCollection points = inGeometry as IPointCollection;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X - this.double_0, ipoint_1.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_2.X + this.double_0, ipoint_2.Y + this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_3.X + this.double_0, ipoint_3.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                points = geometry2 as IPointCollection;
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5, (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X - this.double_0) - this.double_5, (ipoint_1.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_2.X + this.double_0) + this.double_5, (ipoint_2.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_3.X + this.double_0) + this.double_5, (ipoint_3.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5, (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(geometry2, ref missing, ref missing);
                element.Geometry = geometrys as IGeometry;
                element2.Symbol = ifillSymbol_0;
                properties = element as IElementProperties2;
                properties.Type = "外框";
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_17(IActiveView iactiveView_0, string string_16, IPoint ipoint_0, IPoint ipoint_1)
        {
            if (string_16 != "")
            {
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                bool flag = false;
                THTools tools = new THTools();
                if (string_16.Contains("-"))
                {
                    flag = tools.FileName2BL_cqtx(string_16, ref num2, ref num, ref num3, ref num4);
                }
                else
                {
                    flag = tools.FileName2BL_tx(string_16, out num2, out num, out num3, out num4);
                }
                if (flag)
                {
                    return this.method_20(iactiveView_0, num, num2, num3, num4, ipoint_0, ipoint_1);
                }
            }
            return null;
        }

        private IElement method_18(IActiveView iactiveView_0, string string_16, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            if (string_16 != "")
            {
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                bool flag = false;
                THTools tools = new THTools();
                if (string_16.Contains("-"))
                {
                    flag = tools.FileName2BL_cqtx(string_16, ref num2, ref num, ref num3, ref num4);
                }
                else
                {
                    flag = tools.FileName2BL_tx(string_16, out num2, out num, out num3, out num4);
                }
                if (flag)
                {
                    return this.method_19(iactiveView_0, num, num2, num3, num4, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
                }
            }
            return null;
        }

        private IElement method_19(IActiveView iactiveView_0, double double_7, double double_8, double double_9, double double_10, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IGroupElement element = new GroupElementClass();
            string str = "\x00b0";
            string str2 = "'";
            string str3 = "″";
            string str4 = "";
            long num = 0L;
            int num2 = 0;
            int num3 = 0;
            double num4 = 0.0;
            double num5 = 0.0;
            IPoint point = new PointClass();
            IElement element2 = null;
            ITextElement element3 = null;
            ITextSymbol symbol = null;
            ITextSymbol symbol2 = null;
            ITextSymbol symbol3 = null;
            ITextSymbol symbol4 = null;
            symbol = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            symbol2 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            symbol4 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            THTools.DEG2DDDMMSS(double_7, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_8(iactiveView_0, element3, out num4, out num5);
            element3.Text = str4;
            point.PutCoords(ipoint_0.X - num4, ipoint_0.Y - this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = str4;
            element3.Symbol = symbol2;
            point.PutCoords(ipoint_1.X - num4, ipoint_1.Y + this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            double_7 += double_9;
            THTools.DEG2DDDMMSS(double_7, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_8(iactiveView_0, element3, out num4, out num5);
            element3.Text = str4;
            point.PutCoords(ipoint_3.X - num4, ipoint_3.Y - this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = str4;
            element3.Symbol = symbol2;
            point.PutCoords(ipoint_2.X - num4, ipoint_2.Y + this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            THTools.DEG2DDDMMSS(double_8, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_0.X - (this.double_0 / 2.0), ipoint_0.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(ipoint_0.X - ((this.double_0 * 9.0) / 10.0), ipoint_0.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_1.X - (this.double_0 / 2.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_1.X - ((this.double_0 * 9.0) / 10.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            double_8 += double_10;
            THTools.DEG2DDDMMSS(double_8, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_3.X + (this.double_0 / 2.0), ipoint_3.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(ipoint_3.X + ((this.double_0 * 1.0) / 10.0), ipoint_3.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_2.X + (this.double_0 / 2.0), ipoint_2.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_2.X + ((this.double_0 * 1.0) / 10.0), ipoint_2.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            return (element as IElement);
        }

        private IGroupElement method_2(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            int num;
            YTTransformation transformation = new YTTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IPoint point3 = transformation.ToPageLayoutPoint(ipoint_2);
            IPoint point4 = transformation.ToPageLayoutPoint(ipoint_3);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            element2 = this.method_25(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            new EnvelopeClass();
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.method_14(point, point2, point3, point4, this.method_5() as ISymbol);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.method_31(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.CreateTitle(ipageLayout_0 as IActiveView, point, point3);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.CreateCornerShortLine(point, point2, point3, point4);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.method_9(point, point2, point3, point4);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.CreateJionTab(ipageLayout_0 as IActiveView, point2);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.CreateRemarkText(ipageLayout_0 as IActiveView, point, point3);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                    return element;
                }
                element.AddElement(element2);
            }
            return element;
        }

        private IElement method_20(IActiveView iactiveView_0, double double_7, double double_8, double double_9, double double_10, IPoint ipoint_0, IPoint ipoint_1)
        {
            IGroupElement element = new GroupElementClass();
            string str = "\x00b0";
            string str2 = "'";
            string str3 = "″";
            string str4 = "";
            long num = 0L;
            int num2 = 0;
            int num3 = 0;
            double x = 0.0;
            double y = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            IPoint point = new PointClass();
            IElement element2 = null;
            ITextElement element3 = null;
            ITextSymbol symbol = null;
            ITextSymbol symbol2 = null;
            ITextSymbol symbol3 = null;
            ITextSymbol symbol4 = null;
            symbol = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            symbol2 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            symbol4 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            x = ipoint_0.X;
            y = ipoint_0.Y;
            THTools.DEG2DDDMMSS(double_7, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_8(iactiveView_0, element3, out num6, out num7);
            element3.Text = str4;
            point.PutCoords(x - num6, y - this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = str4;
            element3.Symbol = symbol2;
            point.PutCoords(x - num6, ipoint_1.Y + this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            double_7 += double_9;
            THTools.DEG2DDDMMSS(double_7, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_8(iactiveView_0, element3, out num6, out num7);
            element3.Text = str4;
            point.PutCoords(ipoint_1.X - num6, y - this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = str4;
            element3.Symbol = symbol2;
            point.PutCoords(ipoint_1.X - num6, ipoint_1.Y + this.double_0);
            element2.Geometry = point;
            element.AddElement(element2);
            THTools.DEG2DDDMMSS(double_8, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(x - (this.double_0 / 2.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(x - ((this.double_0 * 9.0) / 10.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(x - (this.double_0 / 2.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_0.X - ((this.double_0 * 9.0) / 10.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            double_8 += double_10;
            THTools.DEG2DDDMMSS(double_8, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_1.X + (this.double_0 / 2.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(ipoint_1.X + ((this.double_0 * 1.0) / 10.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_1.X + (this.double_0 / 2.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_1.X + ((this.double_0 * 1.0) / 10.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            return (element as IElement);
        }

        private IElement method_21(double double_7, double double_8, double double_9, double double_10, ILineSymbol ilineSymbol_0)
        {
            object missing = Type.Missing;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(double_7 + double_9, double_8);
            points.AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(double_7, double_8);
            points.AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(double_7, double_8 + double_10);
            points.AddPoint(inPoint, ref missing, ref missing);
            IElement element = new LineElementClass();
            ILineElement element2 = element as ILineElement;
            element2.Symbol = ilineSymbol_0;
            element.Geometry = polyline;
            return element;
        }

        private IPoint method_22(IPoint ipoint_0)
        {
            double x = Math.Truncate((double) (ipoint_0.X / this.double_6)) * this.double_6;
            x += this.double_6;
            double y = Math.Truncate((double) (ipoint_0.Y / this.double_6)) * this.double_6;
            y += this.double_6;
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return point;
        }

        private IPoint method_23(double double_7, double double_8)
        {
            double x = Math.Truncate((double) (double_7 / this.double_6)) * this.double_6;
            x += this.double_6;
            double y = Math.Truncate((double) (double_8 / this.double_6)) * this.double_6;
            y += this.double_6;
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return point;
        }

        private IElement method_24(YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_22(ipoint_0);
            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                Size = 10.0,
                Style = esriSimpleMarkerStyle.esriSMSCross,
                Color = ColorManage.CreatColor(0, 0, 0)
            };
            double x = point.X;
            for (double i = point.Y; x < ipoint_1.X; i = point.Y)
            {
                while (i < ipoint_1.Y)
                {
                    IPoint point2 = new PointClass();
                    point2.PutCoords(x, i);
                    IElement element2 = new MarkerElementClass {
                        Geometry = jlktransformation_0.ToPageLayoutPoint(point2)
                    };
                    (element2 as IMarkerElement).Symbol = symbol;
                    element.AddElement(element2);
                    i += this.double_3;
                }
                x += this.double_2;
            }
            (element as IElementProperties2).Type = "公里网";
            return (element as IElement);
        }

        private IElement method_25(YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_23(num, num2);
            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                Size = 10.0,
                Style = esriSimpleMarkerStyle.esriSMSCross,
                Color = ColorManage.CreatColor(0, 0, 0)
            };
            double x = point.X;
            for (double i = point.Y; x < num3; i = point.Y)
            {
                while (i < num4)
                {
                    IPoint point2 = new PointClass();
                    point2.PutCoords(x, i);
                    IElement element2 = new MarkerElementClass {
                        Geometry = jlktransformation_0.ToPageLayoutPoint(point2)
                    };
                    (element2 as IMarkerElement).Symbol = symbol;
                    element.AddElement(element2);
                    i += this.double_3;
                }
                x += this.double_2;
            }
            (element as IElementProperties2).Type = "公里网";
            return (element as IElement);
        }

        private IElement method_26(YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IPolyline polyline;
            IPointCollection points;
            IPoint point2;
            IElement element2;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_22(ipoint_0);
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass();
            object missing = Type.Missing;
            double x = point.X;
            double y = point.Y;
            symbol.Color = ColorManage.CreatColor(0, 0, 0);
            while (x < ipoint_1.X)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(x, ipoint_0.Y);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(point2), ref missing, ref missing);
                point2.PutCoords(x, ipoint_1.Y);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(point2), ref missing, ref missing);
                element2 = new LineElementClass {
                    Geometry = polyline
                };
                (element2 as ILineElement).Symbol = symbol;
                element.AddElement(element2);
                x += this.double_2;
            }
            while (y < ipoint_1.Y)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(ipoint_0.X, y);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(point2), ref missing, ref missing);
                point2.PutCoords(ipoint_1.X, y);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(point2), ref missing, ref missing);
                element2 = new LineElementClass {
                    Geometry = polyline
                };
                (element2 as ILineElement).Symbol = symbol;
                element.AddElement(element2);
                y += this.double_3;
            }
            return (element as IElement);
        }

        private IPointCollection method_27(IPolyline ipolyline_0, IPolygon ipolygon_0)
        {
            IMultipoint multipoint = null;
            ITopologicalOperator @operator = ipolygon_0 as ITopologicalOperator;
            @operator.Simplify();
            multipoint = @operator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
            return (multipoint as IPointCollection);
        }

        private IElement method_28(YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IPolyline polyline;
            IPoint point2;
            IPointCollection points2;
            IElement element2;
            IPolygon polygon = new PolygonClass();
            IPointCollection points = null;
            object missing = Type.Missing;
            points = polygon as IPointCollection;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            points.AddPoint(ipoint_2, ref missing, ref missing);
            points.AddPoint(ipoint_3, ref missing, ref missing);
            points.AddPoint(ipoint_0, ref missing, ref missing);
            IGroupElement element = new GroupElementClass();
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            double x = (ipoint_0.X < ipoint_1.X) ? (ipoint_0.X - 1.0) : (ipoint_1.X - 1.0);
            double y = (ipoint_0.Y < ipoint_3.Y) ? (ipoint_0.Y - 1.0) : (ipoint_3.Y - 1.0);
            double num7 = (ipoint_2.X > ipoint_3.X) ? (ipoint_2.X + 1.0) : (ipoint_3.X + 1.0);
            double num8 = (ipoint_1.Y > ipoint_2.Y) ? (ipoint_1.Y + 1.0) : (ipoint_2.Y + 1.0);
            IPoint point = this.method_23(num, num2);
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass();
            double num9 = point.X;
            double num10 = point.Y;
            symbol.Color = ColorManage.CreatColor(0, 0, 0);
            while (num9 < num3)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(num9, y);
                points.AddPoint(point2, ref missing, ref missing);
                point2 = new PointClass();
                point2.PutCoords(num9, num8);
                points.AddPoint(point2, ref missing, ref missing);
                points2 = this.method_27(polyline, polygon);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(0)), ref missing, ref missing);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(1)), ref missing, ref missing);
                element2 = new LineElementClass {
                    Geometry = polyline
                };
                (element2 as ILineElement).Symbol = symbol;
                element.AddElement(element2);
                num9 += this.double_2;
            }
            while (num10 < num4)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(x, num10);
                points.AddPoint(point2, ref missing, ref missing);
                point2 = new PointClass();
                point2.PutCoords(num7, num10);
                points.AddPoint(point2, ref missing, ref missing);
                points2 = this.method_27(polyline, polygon);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(0)), ref missing, ref missing);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(1)), ref missing, ref missing);
                element2 = new LineElementClass {
                    Geometry = polyline
                };
                (element2 as ILineElement).Symbol = symbol;
                element.AddElement(element2);
                num10 += this.double_3;
            }
            return (element as IElement);
        }

        private IElement method_29(YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IPolyline polyline;
            IPointCollection points;
            IPoint point2;
            IPoint point3;
            IElement element3;
            ILineElement element4;
            ITextElement element5;
            IPoint point4;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_22(ipoint_0);
            double x = point.X;
            double y = point.Y;
            string str = "";
            string str2 = "";
            double num3 = 0.4;
            double num4 = 0.2;
            double num5 = 0.1;
            IElement element2 = null;
            ITextSymbol symbol = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol2 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol4 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol5 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol6 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol7 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol8 = this.method_3();
            bool flag = true;
            while (true)
            {
                if (x >= ipoint_1.X)
                {
                    break;
                }
                try
                {
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(x, ipoint_0.Y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X, point2.Y - this.double_0);
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol8;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    this.method_32(x, 100000.0, out str, out str2);
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X, point2.Y - num5);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Symbol = symbol5;
                    element5.Text = str2;
                    element.AddElement(element5 as IElement);
                    if (flag || ((x + this.double_2) > ipoint_1.X))
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X, point2.Y - num5);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        element5.Text = str;
                        element5.Symbol = symbol4;
                        element.AddElement(element5 as IElement);
                    }
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(x, ipoint_1.Y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X, point2.Y + this.double_0);
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol8;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X, point2.Y + num5);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Symbol = symbol7;
                    element5.Text = str2;
                    element.AddElement(element5 as IElement);
                    if (flag || ((x + this.double_2) > ipoint_1.X))
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X, point2.Y + num5);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        element5.Text = str;
                        element5.Symbol = symbol6;
                        element.AddElement(element5 as IElement);
                    }
                }
                catch
                {
                }
                x += this.double_2;
                flag = false;
            }
            flag = true;
            while (true)
            {
                if (y >= ipoint_1.Y)
                {
                    break;
                }
                try
                {
                    double num6;
                    double num7;
                    double num8;
                    double num9;
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(ipoint_0.X, y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X - this.double_0, point2.Y);
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol8;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    this.method_32(y, 100000.0, out str, out str2);
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X, point2.Y + num4);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Symbol = symbol2;
                    element5.Text = str;
                    jlktransformation_0.TextWidth(element5, out num6, out num7);
                    point4.PutCoords(point2.X, point2.Y + num4);
                    element2.Geometry = point4;
                    element5.Symbol = symbol;
                    element5.Text = str2;
                    jlktransformation_0.TextWidth(element5, out num8, out num9);
                    element.AddElement(element5 as IElement);
                    if (flag || ((y + this.double_3) > ipoint_1.Y))
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X - num8, point2.Y + num3);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        element5.Symbol = symbol3;
                        element5.Text = str;
                        element.AddElement(element5 as IElement);
                    }
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(ipoint_1.X, y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X + this.double_0, point2.Y);
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol8;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X + num6, point2.Y + num4);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Text = str2;
                    element5.Symbol = symbol7;
                    element.AddElement(element5 as IElement);
                    if (flag || ((y + this.double_3) > ipoint_1.Y))
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X, point2.Y + num3);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        element5.Symbol = symbol2;
                        element5.Text = str;
                        element.AddElement(element5 as IElement);
                    }
                }
                catch
                {
                }
                flag = false;
                y += this.double_3;
            }
            return (element as IElement);
        }

        private ILineSymbol method_3()
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = 1.0;
            return symbol2;
        }

        private IPolygon method_30(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, bool bool_1)
        {
            IGeometryCollection geometrys = new PolygonClass();
            IRing inGeometry = new RingClass();
            IPointCollection points = null;
            object missing = Type.Missing;
            points = inGeometry as IPointCollection;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            points.AddPoint(ipoint_2, ref missing, ref missing);
            points.AddPoint(ipoint_3, ref missing, ref missing);
            inGeometry.Close();
            geometrys.AddGeometry(inGeometry, ref missing, ref missing);
            if (bool_1)
            {
                IRing ring2 = new RingClass();
                IPointCollection points2 = null;
                points2 = ring2 as IPointCollection;
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.double_0, ipoint_0.Y - this.double_0);
                points2.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X - this.double_0, ipoint_1.Y + this.double_0);
                points2.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_2.X + this.double_0, ipoint_2.Y + this.double_0);
                points2.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_3.X + this.double_0, ipoint_3.Y - this.double_0);
                points2.AddPoint(inPoint, ref missing, ref missing);
                ring2.Close();
                geometrys.AddGeometry(ring2, ref missing, ref missing);
            }
            return (geometrys as IPolygon);
        }

        private IElement method_31(YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            bool flag;
            IPolyline polyline;
            IPointCollection points;
            IPoint point2;
            IPoint point3;
            IPointCollection points2;
            IElement element3;
            ILineElement element4;
            ITextElement element5;
            IPoint point4;
            IPolygon polygon = this.method_30(jlktransformation_0.ToPageLayoutPoint(ipoint_0), jlktransformation_0.ToPageLayoutPoint(ipoint_1), jlktransformation_0.ToPageLayoutPoint(ipoint_2), jlktransformation_0.ToPageLayoutPoint(ipoint_3), true);
            IGroupElement element = new GroupElementClass();
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            double x = (ipoint_0.X < ipoint_1.X) ? (ipoint_0.X - 3.0) : (ipoint_1.X - 3.0);
            double y = (ipoint_0.Y < ipoint_3.Y) ? (ipoint_0.Y - 3.0) : (ipoint_3.Y - 3.0);
            double num7 = (ipoint_2.X > ipoint_3.X) ? (ipoint_2.X + 3.0) : (ipoint_3.X + 3.0);
            double num8 = (ipoint_1.Y > ipoint_2.Y) ? (ipoint_1.Y + 3.0) : (ipoint_2.Y + 3.0);
            IPoint point = this.method_23(num, num2);
            double num9 = point.X;
            double num10 = point.Y;
            string str = "";
            string str2 = "";
            double num11 = 0.4;
            double num12 = 0.2;
            IElement element2 = null;
            ITextSymbol symbol = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol2 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol4 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol5 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol6 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol7 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol8 = this.method_3();
            for (flag = true; num9 < num3; flag = false)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(num9, y);
                point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                point3 = new PointClass();
                point3.PutCoords(point2.X, (point2.Y - this.double_0) - 10.0);
                points.AddPoint(point3, ref missing, ref missing);
                point2 = new PointClass();
                point2.PutCoords(num9, num8);
                point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                point3 = new PointClass();
                point3.PutCoords(point2.X, (point2.Y + this.double_0) + 10.0);
                points.AddPoint(point3, ref missing, ref missing);
                points2 = this.method_27(polyline, polygon);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(points2.get_Point(2), ref missing, ref missing);
                points.AddPoint(points2.get_Point(3), ref missing, ref missing);
                point2 = points2.get_Point(2);
                point3 = points2.get_Point(3);
                point2.Y = (point2.Y > point3.Y) ? point2.Y : point3.Y;
                element3 = new LineElementClass();
                element4 = element3 as ILineElement;
                element4.Symbol = symbol8;
                element3.Geometry = polyline;
                element.AddElement(element3);
                this.method_32(num9, 100000.0, out str, out str2);
                element5 = new TextElementClass();
                point4 = new PointClass();
                point4.PutCoords(point2.X, point2.Y);
                element2 = element5 as IElement;
                element2.Geometry = point4;
                element5.Symbol = symbol5;
                element5.Text = str2;
                element.AddElement(element5 as IElement);
                if (flag || ((num9 + this.double_2) > num7))
                {
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X, point2.Y);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Text = str;
                    element5.Symbol = symbol4;
                    element.AddElement(element5 as IElement);
                }
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                points.AddPoint(points2.get_Point(0), ref missing, ref missing);
                points.AddPoint(points2.get_Point(1), ref missing, ref missing);
                point2 = points2.get_Point(0);
                point3 = points2.get_Point(1);
                point2.Y = (point2.Y < point3.Y) ? point2.Y : point3.Y;
                element3 = new LineElementClass();
                element4 = element3 as ILineElement;
                element4.Symbol = symbol8;
                element3.Geometry = polyline;
                element.AddElement(element3);
                element5 = new TextElementClass();
                point4 = new PointClass();
                point4.PutCoords(point2.X, point2.Y);
                element2 = element5 as IElement;
                element2.Geometry = point4;
                element5.Symbol = symbol7;
                element5.Text = str2;
                element.AddElement(element5 as IElement);
                if (flag || ((num9 + this.double_2) > num7))
                {
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X, point2.Y);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Text = str;
                    element5.Symbol = symbol6;
                    element.AddElement(element5 as IElement);
                }
                num9 += this.double_2;
            }
            flag = true;
            while (num10 < num4)
            {
                double num13;
                double num14;
                double num15;
                double num16;
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(x, num10);
                point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                point3 = new PointClass();
                point3.PutCoords((point2.X - this.double_0) - 10.0, point2.Y);
                points.AddPoint(point3, ref missing, ref missing);
                point2 = new PointClass();
                point2.PutCoords(num7, num10);
                point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                point3 = new PointClass();
                point3.PutCoords((point2.X + this.double_0) + 10.0, point2.Y);
                points.AddPoint(point3, ref missing, ref missing);
                points2 = this.method_27(polyline, polygon);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(points2.get_Point(0), ref missing, ref missing);
                points.AddPoint(points2.get_Point(1), ref missing, ref missing);
                point2 = points2.get_Point(0);
                point3 = points2.get_Point(1);
                point2.X = (point2.X > point3.X) ? point2.X : point3.X;
                element3 = new LineElementClass();
                element4 = element3 as ILineElement;
                element4.Symbol = symbol8;
                element3.Geometry = polyline;
                element.AddElement(element3);
                this.method_32(num10, 100000.0, out str, out str2);
                element5 = new TextElementClass();
                point4 = new PointClass();
                point4.PutCoords(point2.X, point2.Y + num12);
                element2 = element5 as IElement;
                element2.Geometry = point4;
                element5.Symbol = symbol2;
                element5.Text = str;
                jlktransformation_0.TextWidth(element5, out num13, out num14);
                point4.PutCoords(point2.X, point2.Y + num12);
                element2.Geometry = point4;
                element5.Symbol = symbol;
                element5.Text = str2;
                jlktransformation_0.TextWidth(element5, out num15, out num16);
                element.AddElement(element5 as IElement);
                if (flag || ((num10 + this.double_3) > num8))
                {
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X - num15, point2.Y + num11);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Symbol = symbol3;
                    element5.Text = str;
                    element.AddElement(element5 as IElement);
                }
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                points.AddPoint(points2.get_Point(2), ref missing, ref missing);
                points.AddPoint(points2.get_Point(3), ref missing, ref missing);
                point2 = points2.get_Point(2);
                point3 = points2.get_Point(3);
                point2.X = (point2.X < point3.X) ? point2.X : point3.X;
                element3 = new LineElementClass();
                element4 = element3 as ILineElement;
                element4.Symbol = symbol8;
                element3.Geometry = polyline;
                element.AddElement(element3);
                element5 = new TextElementClass();
                point4 = new PointClass();
                point4.PutCoords(point2.X + num13, point2.Y + num12);
                element2 = element5 as IElement;
                element2.Geometry = point4;
                element5.Text = str2;
                element5.Symbol = symbol7;
                element.AddElement(element5 as IElement);
                if (flag || ((num10 + this.double_3) > num8))
                {
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    point4.PutCoords(point2.X, point2.Y + num11);
                    element2 = element5 as IElement;
                    element2.Geometry = point4;
                    element5.Symbol = symbol2;
                    element5.Text = str;
                    element.AddElement(element5 as IElement);
                }
                flag = false;
                num10 += this.double_3;
            }
            return (element as IElement);
        }

        private void method_32(double double_7, double double_8, out string string_16, out string string_17)
        {
            string_16 = "";
            string_17 = "";
            int num = (int) Math.Truncate((double) (double_7 / double_8));
            string_16 = num.ToString();
            string_17 = ((int) Math.Truncate((double) (((double_7 - (num * double_8)) / double_8) * 100.0))).ToString();
            if (string_17.Length < 2)
            {
                string_17 = "0" + string_17;
            }
        }

        private IElement method_33(IActiveView iactiveView_0, string string_16, double double_7, double double_8, IEnvelope ienvelope_0)
        {
            ITextElement element = new TextElementClass {
                Text = string_16
            };
            IElement element2 = element as IElement;
            IPoint origin = new PointClass();
            IEnvelope bounds = new EnvelopeClass();
            origin.PutCoords(double_7, double_8);
            element2.Geometry = origin;
            element2.QueryBounds(iactiveView_0.ScreenDisplay, bounds);
            if (bounds.Width > ienvelope_0.Width)
            {
                double sx = ienvelope_0.Width / bounds.Width;
                double sy = ienvelope_0.Height / bounds.Height;
                (element2 as ITransform2D).Scale(origin, sx, sy);
            }
            return element2;
        }

        private ILineSymbol method_4(int int_0)
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = int_0;
            return symbol2;
        }

        private IFillSymbol method_5()
        {
            ISimpleFillSymbol symbol = new SimpleFillSymbolClass();
            ILineSymbol symbol2 = null;
            ISimpleLineSymbol symbol3 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol3.Color = color;
            symbol3.Width = 2.0;
            symbol2 = symbol3;
            color.Red = 100;
            color.Blue = 100;
            color.Green = 100;
            symbol.Color = color;
            symbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            symbol.Outline = symbol2;
            return symbol;
        }

        private ILineSymbol method_6()
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = 2.0;
            return symbol2;
        }

        private string method_7(string string_16)
        {
            string str = "\n";
            int startIndex = 0;
            string str2 = "";
            for (startIndex = 0; startIndex < string_16.Trim().Length; startIndex++)
            {
                str2 = str2 + string_16.Substring(startIndex, 1) + str;
            }
            return str2;
        }

        private void method_8(IActiveView iactiveView_0, ITextElement itextElement_0, out double double_7, out double double_8)
        {
            double_7 = 0.0;
            double_8 = 0.0;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                (itextElement_0 as IElement).QueryBounds(iactiveView_0.ScreenDisplay, bounds);
                double_7 = bounds.Width;
                double_8 = bounds.Height;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private IElement method_9(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IElement element = new LineElementClass();
            IElementProperties2 properties = null;
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            points.AddPoint(ipoint_2, ref missing, ref missing);
            points.AddPoint(ipoint_3, ref missing, ref missing);
            points.AddPoint(ipoint_0, ref missing, ref missing);
            element.Geometry = polyline;
            properties = element as IElementProperties2;
            properties.Type = "内框";
            symbol = this.method_4(1);
            element2 = element as ILineElement;
            element2.Symbol = symbol;
            return element;
        }

        public bool HasLegend
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public double InOutDist
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        public string LeftBorderOutText
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string LeftLowText
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public string LegendTemplate
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string MapTH
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string MapTM
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public double OutBorderWidth
        {
            get
            {
                return this.double_5;
            }
            set
            {
                this.double_5 = value;
            }
        }

        public string RightLowText
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string RightUpText
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public string Row1Col1Text
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public string Row1Col2Text
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = value;
            }
        }

        public string Row1Col3Text
        {
            get
            {
                return this.string_12;
            }
            set
            {
                this.string_12 = value;
            }
        }

        public string Row2Col1Text
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public string Row2Col3Text
        {
            get
            {
                return this.string_13;
            }
            set
            {
                this.string_13 = value;
            }
        }

        public string Row3Col1Text
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public string Row3Col2Text
        {
            get
            {
                return this.string_11;
            }
            set
            {
                this.string_11 = value;
            }
        }

        public string Row3Col3Text
        {
            get
            {
                return this.string_14;
            }
            set
            {
                this.string_14 = value;
            }
        }

        public double Scale
        {
            get
            {
                return this.double_4;
            }
            set
            {
                this.double_4 = value;
                this.string_15 = "1:" + value.ToString();
            }
        }

        public SpheroidType SpheroidType
        {
            get
            {
                return this.spheroidType_0;
            }
            set
            {
                this.spheroidType_0 = value;
            }
        }

        public double StartCoodinateMultiple
        {
            get
            {
                return this.double_6;
            }
            set
            {
                this.double_6 = value;
            }
        }

        public StripType StripType
        {
            get
            {
                return this.stripType_0;
            }
            set
            {
                this.stripType_0 = value;
            }
        }

        public double TitleDist
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }

        public TKType TKType
        {
            get
            {
                return this.tktype_0;
            }
            set
            {
                this.tktype_0 = value;
            }
        }

        public double XInterval
        {
            get
            {
                return this.double_2;
            }
            set
            {
                this.double_2 = value;
            }
        }

        public double YInterval
        {
            get
            {
                return this.double_3;
            }
            set
            {
                this.double_3 = value;
            }
        }
    }
}

