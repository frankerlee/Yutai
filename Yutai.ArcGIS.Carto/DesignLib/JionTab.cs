using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class JionTab
    {
        private double double_0 = 4.5;
        private double double_1 = 2.4;
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private string string_0 = "=接图表1";
        private string string_1 = "=接图表4";
        private string string_2 = "=接图表7";
        private string string_3 = "=接图表2";
        private string string_4 = "=接图表8";
        private string string_5 = "=接图表3";
        private string string_6 = "=接图表6";
        private string string_7 = "=接图表9";

        public IElement CreateJionTab(IActiveView iactiveView_0, IPoint ipoint_0)
        {
            IGroupElement element = new GroupElementClass();
            (element as IElementProperties).Name = "接图表";
            (element as IElementProperties).Type = "接图表";
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
            double num = this.double_1/3.0;
            double num2 = this.double_0/3.0;
            double num3 = 0.3;
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
                symbol = this.ilineSymbol_0;
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
                point2.PutCoords(ipoint_0.X, ipoint_0.Y + num3);
                inPoint.PutCoords(ipoint_0.X, (ipoint_0.Y + num3) + this.double_1);
                point3.PutCoords(ipoint_0.X + this.double_0, ipoint_0.Y + num3);
                point4.PutCoords(ipoint_0.X + this.double_0, (ipoint_0.Y + num3) + this.double_1);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point2, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(inPoint, ref missing, ref missing);
                element3.Geometry = polyline;
                element.AddElement(element3);
                IPoint point5 = new PointClass();
                IPoint point6 = new PointClass();
                point5.PutCoords(inPoint.X, inPoint.Y - num);
                point6.PutCoords(point4.X, point4.Y - num);
                points = polyline2 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element4.Geometry = polyline2;
                element.AddElement(element4);
                point5.PutCoords(inPoint.X, inPoint.Y - (num*2.0));
                point6.PutCoords(point4.X, point4.Y - (num*2.0));
                points = polyline3 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element5.Geometry = polyline3;
                element.AddElement(element5);
                point5.PutCoords(inPoint.X + num2, inPoint.Y);
                point6.PutCoords(inPoint.X + num2, point2.Y);
                points = polyline4 as IPointCollection;
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(point6, ref missing, ref missing);
                element6.Geometry = polyline4;
                element.AddElement(element6);
                point5.PutCoords(inPoint.X + (num2*2.0), inPoint.Y);
                point6.PutCoords(inPoint.X + (num2*2.0), point2.Y);
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
                IRgbColor color = new RgbColorClass
                {
                    Red = 0,
                    Green = 0,
                    Blue = 0
                };
                symbol2.Outline = this.ilineSymbol_0;
                symbol2.Color = color;
                symbol2.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
                element10.Symbol = symbol2;
                points = polygon as IPointCollection;
                point5.PutCoords(inPoint.X + num2, inPoint.Y - num);
                points.AddPoint(point5, ref missing, ref missing);
                point5.PutCoords(inPoint.X + (num2*2.0), inPoint.Y - num);
                points.AddPoint(point5, ref missing, ref missing);
                point5.PutCoords(inPoint.X + (num2*2.0), inPoint.Y - (num*2.0));
                points.AddPoint(point5, ref missing, ref missing);
                point5.PutCoords(inPoint.X + num2, inPoint.Y - (num*2.0));
                points.AddPoint(point5, ref missing, ref missing);
                polygon.Close();
                element8.Geometry = polygon;
                element.AddElement(element8);
                IEnvelope envelope = polygon.Envelope;
                new EnvelopeClass();
                if (this.string_0.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_0, inPoint.X + (num2/2.0),
                        inPoint.Y - (num/2.0), envelope));
                }
                if (this.string_1.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_1, inPoint.X + (num2/2.0),
                        inPoint.Y - (1.5*num), envelope));
                }
                if (this.string_2.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_2, inPoint.X + (num2/2.0),
                        inPoint.Y - (2.5*num), envelope));
                }
                if (this.string_3.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_3, inPoint.X + (1.5*num2),
                        inPoint.Y - (0.5*num), envelope));
                }
                if (this.string_4.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_4, inPoint.X + (1.5*num2),
                        inPoint.Y - (2.5*num), envelope));
                }
                if (this.string_5.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_5, inPoint.X + (2.5*num2),
                        inPoint.Y - (0.5*num), envelope));
                }
                if (this.string_6.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_6, inPoint.X + (2.5*num2),
                        inPoint.Y - (1.5*num), envelope));
                }
                if (this.string_7.Trim().Length > 0)
                {
                    element.AddElement(this.method_0(iactiveView_0, this.string_7, inPoint.X + (2.5*num2),
                        inPoint.Y - (2.5*num), envelope));
                }
            }
            catch (Exception)
            {
            }
            return (element as IElement);
        }

        private IElement method_0(IActiveView iactiveView_0, string string_8, double double_2, double double_3,
            IEnvelope ienvelope_0)
        {
            ITextElement element = new TextElementClass();
            ITextSymbol symbol = new TextSymbolClass
            {
                HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter,
                VerticalAlignment = esriTextVerticalAlignment.esriTVACenter
            };
            IFontDisp font = symbol.Font;
            font.Size = 7.09M;
            symbol.Font = font;
            element.Symbol = symbol;
            element.Text = string_8;
            IElement element2 = element as IElement;
            IPoint origin = new PointClass();
            IEnvelope bounds = new EnvelopeClass();
            origin.PutCoords(double_2, double_3);
            element2.Geometry = origin;
            element2.QueryBounds(iactiveView_0.ScreenDisplay, bounds);
            if (bounds.Width > ienvelope_0.Width)
            {
                double sx = ienvelope_0.Width/bounds.Width;
                double sy = ienvelope_0.Height/bounds.Height;
                (element2 as ITransform2D).Scale(origin, sx, sy);
            }
            return element2;
        }

        public string Row1Col1Text
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public string Row1Col2Text
        {
            get { return this.string_3; }
            set { this.string_3 = value; }
        }

        public string Row1Col3Text
        {
            get { return this.string_5; }
            set { this.string_5 = value; }
        }

        public string Row2Col1Text
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public string Row2Col3Text
        {
            get { return this.string_6; }
            set { this.string_6 = value; }
        }

        public string Row3Col1Text
        {
            get { return this.string_2; }
            set { this.string_2 = value; }
        }

        public string Row3Col2Text
        {
            get { return this.string_4; }
            set { this.string_4 = value; }
        }

        public string Row3Col3Text
        {
            get { return this.string_7; }
            set { this.string_7 = value; }
        }
    }
}