using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto
{
    internal class FenFuMapClass : BaseMap
    {
        private double double_2 = 0.0;
        private double double_3 = 0.0;
        private IList<IElement> ilist_0 = new List<IElement>();
        private string string_10 = "";
        private string string_11 = "";
        private string string_12 = "";
        private string string_13 = "";
        private string string_14 = "";
        private string string_15 = "";
        private string string_16 = "";
        private string string_4 = "";
        private string string_5 = "";
        private string string_6 = "";
        private string string_7 = "";
        private string string_8 = "";
        private string string_9 = "";

        public FenFuMapClass()
        {
            base.MapType = "分幅图";
            base.InOutDist = 80.0;
            base.TitleDist = 150.0;
        }

        public override void Draw()
        {
            int num = 0;
            IElement element = null;
            IEnvelope envelope = null;
            base.m_pActiveView.FocusMap.ReferenceScale = base.m_ReferenceScale;
            this.DrawInsideFrame();
            this.DrawOutFrame();
            this.DrawTitle();
            this.DrawRemark();
            this.DrawGrid();
            this.DrawJionTab();
            this.method_4();
            if (base.NeedLegend)
            {
                this.DrawLegend();
            }
            if (base.m_pActiveView != null)
            {
                base.GraphicsLayer = new CompositeGraphicsLayerClass();
                ILayer graphicsLayer = base.m_GraphicsLayer as ILayer;
                graphicsLayer.SpatialReference = base.m_pActiveView.FocusMap.SpatialReference;
                graphicsLayer.Name = "制图层";
                (base.m_GraphicsLayer as IGeoDatasetSchemaEdit).AlterSpatialReference(base.m_pActiveView.FocusMap.SpatialReference);
                IGraphicsContainer container = base.m_GraphicsLayer as IGraphicsContainer;
                element = this.method_8();
                if (element != null)
                {
                    container.AddElement(element, 0);
                }
                for (num = 0; num < this.ilist_0.Count; num++)
                {
                    container.AddElement(this.ilist_0[num], 0);
                }
                base.m_pActiveView.FocusMap.AddLayer(graphicsLayer);
                envelope = this.method_9(base.InOutDist, base.InOutDist, base.InOutDist + this.double_3, base.InOutDist + this.double_2);
                if (envelope != null)
                {
                    base.m_pActiveView.Extent = envelope;
                }
                base.m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_GraphicsLayer, base.m_pActiveView.Extent);
            }
            this.method_11();
        }

        public override void DrawBackFrame()
        {
        }

        public void DrawGrid()
        {
            string str = "";
            string str2 = "";
            ITextElement element = null;
            ITextElement element2 = null;
            ITextElement element3 = null;
            ITextElement element4 = null;
            double num = 40.0;
            double num2 = 20.0;
            double num3 = 10.0;
            int num4 = 13;
            int num5 = 10;
            IPoint point = null;
            IElement item = null;
            ITextSymbol symbol = new TextSymbolClass();
            ITextSymbol symbol2 = new TextSymbolClass();
            ITextSymbol symbol3 = new TextSymbolClass();
            ITextSymbol symbol4 = new TextSymbolClass();
            ITextSymbol symbol5 = new TextSymbolClass();
            ITextSymbol symbol6 = new TextSymbolClass();
            int num6 = 0;
            int num7 = 0;
            int num8 = 0x3e8;
            double x = 0.0;
            double y = 0.0;
            IPoint point2 = new PointClass();
            object missing = System.Type.Missing;
            IElementProperties2 properties = null;
            IMarkerElement element6 = null;
            ISymbol symbol7 = new SimpleMarkerSymbolClass();
            ISimpleMarkerSymbol symbol8 = symbol7 as ISimpleMarkerSymbol;
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol8.Size = 10.0;
            symbol8.Style = esriSimpleMarkerStyle.esriSMSCross;
            symbol8.Color = color;
            double num11 = 0.0;
            double num12 = 0.0;
            double num13 = 0.0;
            double num14 = 0.0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            double num20 = 0.0;
            double num21 = 0.0;
            double num22 = 0.0;
            double num23 = 0.0;
            try
            {
                symbol = base.FontStyle((double) num4, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
                symbol2 = base.FontStyle((double) num5, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
                symbol3 = base.FontStyle((double) num5, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
                symbol4 = base.FontStyle((double) num4, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
                symbol5 = base.FontStyle((double) num5, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
                symbol6 = base.FontStyle((double) num4, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
                if (base.LeftUp.Y < base.RightUp.Y)
                {
                    y = Math.Truncate(base.LeftUp.Y);
                    num23 = (base.RightUp.Y + base.InOutDist) + 1.0;
                }
                else
                {
                    y = Math.Truncate(base.RightUp.Y);
                    num23 = (base.LeftUp.Y + base.InOutDist) + 1.0;
                }
                num15 = (int) (y % ((double) num8));
                if (num15 != 0)
                {
                    num12 = y - num15;
                }
                else
                {
                    num12 = y;
                }
                if (base.LeftUp.X > base.LeftLow.X)
                {
                    x = Math.Truncate((double) (base.LeftUp.X + 1.0));
                    num20 = (base.LeftLow.X - base.InOutDist) - 1.0;
                }
                else
                {
                    x = Math.Truncate((double) (base.LeftLow.X + 1.0));
                    num20 = (base.LeftUp.X - base.InOutDist) - 1.0;
                }
                num15 = (int) (x % ((double) num8));
                if (num15 != 0)
                {
                    num11 = x + (num8 - num15);
                }
                else
                {
                    num11 = x;
                }
                if (base.LeftLow.Y < base.RightLow.Y)
                {
                    y = Math.Truncate(base.LeftLow.Y);
                    num21 = (base.LeftLow.Y - base.InOutDist) - 1.0;
                }
                else
                {
                    y = Math.Truncate(base.RightLow.Y);
                    num21 = (base.RightLow.Y - base.InOutDist) - 1.0;
                }
                num15 = (int) (y % ((double) num8));
                if (num15 != 0)
                {
                    num14 = y + (num8 - num15);
                }
                else
                {
                    num14 = y;
                }
                if (base.RightUp.X > base.RightLow.X)
                {
                    x = Math.Truncate(base.RightLow.X);
                    num22 = (base.RightUp.X + base.InOutDist) + 1.0;
                }
                else
                {
                    x = Math.Truncate(base.RightUp.X);
                    num22 = (base.RightLow.X + base.InOutDist) + 1.0;
                }
                num15 = (int) (x % ((double) num8));
                if (num15 != 0)
                {
                    num13 = x - num15;
                }
                else
                {
                    num13 = x;
                }
                num16 = ((int) (num12 - num14)) / num8;
                num17 = ((int) (num13 - num11)) / num8;
                for (num18 = 0; num18 <= num16; num18++)
                {
                    y = num12 - (num18 * num8);
                    for (num19 = 0; num19 <= num17; num19++)
                    {
                        x = num11 + (num19 * num8);
                        point2.PutCoords(x, y);
                        item = new MarkerElementClass {
                            Geometry = point2
                        };
                        element6 = item as IMarkerElement;
                        element6.Symbol = symbol8;
                        properties = item as IElementProperties2;
                        properties.Type = "公里网";
                        this.ilist_0.Add(item);
                    }
                }
                IPolygon polygon = new PolygonClass();
                new PolygonElementClass();
                polygon = this.method_5();
                double num1 = base.LeftUp.X - base.InOutDist;
                double num24 = base.LeftUp.X;
                double num25 = base.RightUp.X;
                double num26 = base.RightUp.X + base.InOutDist;
                IPoint inPoint = null;
                IPoint point4 = null;
                IPoint point5 = null;
                IPoint point6 = null;
                IPolyline polyline = new PolylineClass();
                IPolyline polyline2 = new PolylineClass();
                IPolyline polyline3 = new PolylineClass();
                IPoint point7 = null;
                IPoint point8 = null;
                IPointCollection points = null;
                IPointCollection points2 = null;
                IElement element7 = null;
                IElement element8 = null;
                ILineElement element9 = null;
                ILineSymbol symbol9 = this.method_2();
                for (num18 = 0; num18 <= num16; num18++)
                {
                    y = num12 - (num18 * num8);
                    point7 = new PointClass();
                    point8 = new PointClass();
                    point7.PutCoords(num20, y);
                    point8.PutCoords(num22, y);
                    points = polyline3 as IPointCollection;
                    if (points.PointCount >= 1)
                    {
                        points.RemovePoints(0, points.PointCount);
                    }
                    points.AddPoint(point7, ref missing, ref missing);
                    points.AddPoint(point8, ref missing, ref missing);
                    points = this.method_6(polyline3, polygon);
                    inPoint = points.get_Point(0);
                    point4 = points.get_Point(1);
                    point5 = points.get_Point(2);
                    point6 = points.get_Point(3);
                    element7 = new LineElementClass();
                    element9 = element7 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline as IPointCollection;
                    if (points2.PointCount >= 1)
                    {
                        points2.RemovePoints(0, points2.PointCount);
                    }
                    points2.AddPoint(inPoint, ref missing, ref missing);
                    points2.AddPoint(point4, ref missing, ref missing);
                    element7.Geometry = polyline;
                    element8 = new LineElementClass();
                    element9 = element8 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline2 as IPointCollection;
                    if (points2.PointCount >= 1)
                    {
                        points2.RemovePoints(0, points2.PointCount);
                    }
                    points2.AddPoint(point5, ref missing, ref missing);
                    points2.AddPoint(point6, ref missing, ref missing);
                    element8.Geometry = polyline2;
                    this.ilist_0.Add(element7);
                    this.ilist_0.Add(element8);
                    element = new TextElementClass();
                    element2 = new TextElementClass();
                    element3 = new TextElementClass();
                    element4 = new TextElementClass();
                    num6 = Math.Truncate((double) (y / 100000.0));
                    str = num6.ToString();
                    num7 = Math.Truncate((double) ((y - (num6 * 0x186a0)) / 1000.0));
                    str2 = num7.ToString();
                    if (str2.Length < 2)
                    {
                        str2 = "0" + str2;
                    }
                    element.Text = str2;
                    element2.Text = str;
                    element3.Text = str2;
                    element4.Text = str;
                    point = new PointClass();
                    point.PutCoords(inPoint.X, inPoint.Y + num2);
                    item = element as IElement;
                    item.Geometry = point;
                    element.Symbol = symbol;
                    this.ilist_0.Add(item);
                    if ((num18 == 0) || (num18 == num16))
                    {
                        point = new PointClass();
                        point.PutCoords(point4.X, point4.Y + num);
                        item = element2 as IElement;
                        item.Geometry = point;
                        element2.Symbol = symbol2;
                        this.ilist_0.Add(item);
                    }
                    point = new PointClass();
                    point.PutCoords(point5.X, point5.Y + num2);
                    item = element3 as IElement;
                    item.Geometry = point;
                    element3.Symbol = symbol;
                    this.ilist_0.Add(item);
                    if ((num18 == 0) || (num18 == num16))
                    {
                        point = new PointClass();
                        point.PutCoords(point6.X, point6.Y + num);
                        item = element4 as IElement;
                        item.Geometry = point;
                        element4.Symbol = symbol2;
                        this.ilist_0.Add(item);
                    }
                }
                for (num18 = 0; num18 <= num17; num18++)
                {
                    x = num11 + (num18 * num8);
                    point7 = new PointClass();
                    point8 = new PointClass();
                    point7.PutCoords(x, num23);
                    point8.PutCoords(x, num21);
                    points = polyline3 as IPointCollection;
                    if (points.PointCount >= 1)
                    {
                        points.RemovePoints(0, points.PointCount);
                    }
                    points.AddPoint(point7, ref missing, ref missing);
                    points.AddPoint(point8, ref missing, ref missing);
                    points = this.method_6(polyline3, polygon);
                    inPoint = points.get_Point(0);
                    point4 = points.get_Point(1);
                    point5 = points.get_Point(2);
                    point6 = points.get_Point(3);
                    element7 = new LineElementClass();
                    element9 = element7 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline as IPointCollection;
                    if (points2.PointCount >= 1)
                    {
                        points2.RemovePoints(0, points2.PointCount);
                    }
                    points2.AddPoint(inPoint, ref missing, ref missing);
                    points2.AddPoint(point4, ref missing, ref missing);
                    element7.Geometry = polyline;
                    element8 = new LineElementClass();
                    element9 = element8 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline2 as IPointCollection;
                    if (points2.PointCount >= 1)
                    {
                        points2.RemovePoints(0, points2.PointCount);
                    }
                    points2.AddPoint(point5, ref missing, ref missing);
                    points2.AddPoint(point6, ref missing, ref missing);
                    element8.Geometry = polyline2;
                    this.ilist_0.Add(element7);
                    this.ilist_0.Add(element8);
                    element = new TextElementClass();
                    element2 = new TextElementClass();
                    element3 = new TextElementClass();
                    element4 = new TextElementClass();
                    num6 = Math.Truncate((double) (x / 100000.0));
                    str = num6.ToString();
                    str2 = ((int) Math.Truncate((double) ((x - (num6 * 0x186a0)) / 1000.0))).ToString();
                    if (str2.Length < 2)
                    {
                        str2 = "0" + str2;
                    }
                    element.Text = str2;
                    element2.Text = str;
                    element3.Text = str2;
                    element4.Text = str;
                    point = new PointClass();
                    point.PutCoords(inPoint.X, inPoint.Y - num3);
                    item = element as IElement;
                    item.Geometry = point;
                    element.Symbol = symbol4;
                    this.ilist_0.Add(item);
                    if ((num18 == 0) || (num18 == num17))
                    {
                        point = new PointClass();
                        point.PutCoords(inPoint.X, inPoint.Y - num3);
                        item = element2 as IElement;
                        item.Geometry = point;
                        element2.Symbol = symbol3;
                        this.ilist_0.Add(item);
                    }
                    point = new PointClass();
                    point.PutCoords(point6.X, point6.Y + num3);
                    item = element3 as IElement;
                    item.Geometry = point;
                    element3.Symbol = symbol6;
                    this.ilist_0.Add(item);
                    if ((num18 == 0) || (num18 == num17))
                    {
                        point = new PointClass();
                        point.PutCoords(point6.X, point6.Y + num3);
                        item = element4 as IElement;
                        item.Geometry = point;
                        element4.Symbol = symbol5;
                        this.ilist_0.Add(item);
                    }
                }
                inPoint = new PointClass();
                point4 = new PointClass();
                point5 = new PointClass();
                IPolyline polyline4 = new PolylineClass();
                IElement element10 = null;
                inPoint.PutCoords(base.LeftUp.X, base.LeftUp.Y + base.InOutDist);
                point4.PutCoords(base.LeftUp.X, base.LeftUp.Y);
                point5.PutCoords(base.LeftUp.X - base.InOutDist, base.LeftUp.Y);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                {
                    points.RemovePoints(0, points.PointCount);
                }
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                this.ilist_0.Add(element10);
                inPoint.PutCoords(base.RightUp.X, base.RightUp.Y + base.InOutDist);
                point4.PutCoords(base.RightUp.X, base.RightUp.Y);
                point5.PutCoords(base.RightUp.X + base.InOutDist, base.RightUp.Y);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                {
                    points.RemovePoints(0, points.PointCount);
                }
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                this.ilist_0.Add(element10);
                inPoint.PutCoords(base.RightLow.X, base.RightLow.Y - base.InOutDist);
                point4.PutCoords(base.RightLow.X, base.RightLow.Y);
                point5.PutCoords(base.RightLow.X + base.InOutDist, base.RightLow.Y);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                {
                    points.RemovePoints(0, points.PointCount);
                }
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                this.ilist_0.Add(element10);
                inPoint.PutCoords(base.LeftLow.X - base.InOutDist, base.LeftLow.Y);
                point4.PutCoords(base.LeftLow.X, base.LeftLow.Y);
                point5.PutCoords(base.LeftLow.X, base.LeftLow.Y - base.InOutDist);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                {
                    points.RemovePoints(0, points.PointCount);
                }
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                this.ilist_0.Add(element10);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawInsideFrame()
        {
            IElement item = new LineElementClass();
            IElementProperties2 properties = null;
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = System.Type.Missing;
            try
            {
                if (!((base.LeftUp == null) || base.LeftUp.IsEmpty))
                {
                    points.AddPoint(base.LeftUp, ref missing, ref missing);
                }
                if (!((base.LeftLow == null) || base.LeftLow.IsEmpty))
                {
                    points.AddPoint(base.LeftLow, ref missing, ref missing);
                }
                if (!((base.RightLow == null) || base.RightLow.IsEmpty))
                {
                    points.AddPoint(base.RightLow, ref missing, ref missing);
                }
                if (!((base.RightUp == null) || base.RightUp.IsEmpty))
                {
                    points.AddPoint(base.RightUp, ref missing, ref missing);
                }
                if (!((base.LeftUp == null) || base.LeftUp.IsEmpty))
                {
                    points.AddPoint(base.LeftUp, ref missing, ref missing);
                }
                item.Geometry = polyline;
                properties = item as IElementProperties2;
                properties.Type = "内框";
                symbol = this.method_2();
                element2 = item as ILineElement;
                element2.Symbol = symbol;
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void DrawJionTab()
        {
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = new TextElementClass();
            ITextElement element3 = new TextElementClass();
            ITextElement element4 = new TextElementClass();
            ITextElement element5 = new TextElementClass();
            ITextElement element6 = new TextElementClass();
            ITextElement element7 = new TextElementClass();
            ITextElement element8 = new TextElementClass();
            ITextElement element9 = new TextElementClass();
            ILineElement element10 = null;
            ILineSymbol symbol = null;
            double num = 390.0;
            double num2 = 210.0;
            double num3 = num2 / 3.0;
            double num4 = num / 3.0;
            double num5 = 20.0 + base.InOutDist;
            this.double_3 = num2;
            object missing = System.Type.Missing;
            IElement element11 = new LineElementClass();
            IElement element12 = new LineElementClass();
            IElement element13 = new LineElementClass();
            IElement element14 = new LineElementClass();
            IElement element15 = new LineElementClass();
            IPolyline polyline = new PolylineClass();
            IPolyline polyline2 = new PolylineClass();
            IPolyline polyline3 = new PolylineClass();
            IPolyline polyline4 = new PolylineClass();
            IPolyline polyline5 = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            IPoint inPoint = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            IPoint point5 = new PointClass();
            try
            {
                symbol = this.method_2();
                element10 = element11 as ILineElement;
                element10.Symbol = symbol;
                element10 = element12 as ILineElement;
                element10.Symbol = symbol;
                element10 = element13 as ILineElement;
                element10.Symbol = symbol;
                element10 = element14 as ILineElement;
                element10.Symbol = symbol;
                element10 = element15 as ILineElement;
                element10.Symbol = symbol;
                point3.PutCoords(base.LeftUp.X, base.LeftUp.Y + num5);
                inPoint.PutCoords(base.LeftUp.X, (base.LeftUp.Y + num5) + num2);
                point4.PutCoords(base.LeftUp.X + num, base.LeftUp.Y + num5);
                point5.PutCoords(base.LeftUp.X + num, (base.LeftUp.Y + num5) + num2);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(inPoint, ref missing, ref missing);
                element11.Geometry = polyline;
                this.ilist_0.Add(element11);
                IPoint point6 = new PointClass();
                IPoint point7 = new PointClass();
                point6.PutCoords(inPoint.X, inPoint.Y - num3);
                point7.PutCoords(point5.X, point5.Y - num3);
                points = polyline2 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element12.Geometry = polyline2;
                this.ilist_0.Add(element12);
                point6.PutCoords(inPoint.X, inPoint.Y - (num3 * 2.0));
                point7.PutCoords(point5.X, point5.Y - (num3 * 2.0));
                points = polyline3 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element13.Geometry = polyline3;
                this.ilist_0.Add(element13);
                point6.PutCoords(inPoint.X + num4, inPoint.Y);
                point7.PutCoords(inPoint.X + num4, point3.Y);
                points = polyline4 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element14.Geometry = polyline4;
                this.ilist_0.Add(element14);
                point6.PutCoords(inPoint.X + (num4 * 2.0), inPoint.Y);
                point7.PutCoords(inPoint.X + (num4 * 2.0), point3.Y);
                points = polyline5 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element15.Geometry = polyline5;
                this.ilist_0.Add(element15);
                IPolygon polygon = new PolygonClass();
                IElement element16 = new PolygonElementClass();
                IPolygonElement element17 = element16 as IPolygonElement;
                ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass();
                IFillShapeElement element18 = element17 as IFillShapeElement;
                IRgbColor color = new RgbColorClass {
                    Red = 0,
                    Green = 0,
                    Blue = 0
                };
                symbol2.Outline = this.method_2();
                symbol2.Color = color;
                symbol2.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
                element18.Symbol = symbol2;
                points = polygon as IPointCollection;
                point6.PutCoords(inPoint.X + num4, inPoint.Y - num3);
                points.AddPoint(point6, ref missing, ref missing);
                point6.PutCoords(inPoint.X + (num4 * 2.0), inPoint.Y - num3);
                points.AddPoint(point6, ref missing, ref missing);
                point6.PutCoords(inPoint.X + (num4 * 2.0), inPoint.Y - (num3 * 2.0));
                points.AddPoint(point6, ref missing, ref missing);
                point6.PutCoords(inPoint.X + num4, inPoint.Y - (num3 * 2.0));
                points.AddPoint(point6, ref missing, ref missing);
                polygon.Close();
                element16.Geometry = polygon;
                this.ilist_0.Add(element16);
                element2.Text = this.string_8;
                element3.Text = this.string_9;
                element4.Text = this.string_10;
                element5.Text = this.string_11;
                element6.Text = this.string_12;
                element7.Text = this.string_13;
                element8.Text = this.string_14;
                element9.Text = this.string_15;
                item = element2 as IElement;
                point.PutCoords(inPoint.X + (num4 / 2.0), inPoint.Y - (num3 / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element3 as IElement;
                point.PutCoords(inPoint.X + (num4 / 2.0), inPoint.Y - ((3.0 * num3) / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element4 as IElement;
                point.PutCoords(inPoint.X + (num4 / 2.0), inPoint.Y - ((5.0 * num3) / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element5 as IElement;
                point.PutCoords(inPoint.X + ((3.0 * num4) / 2.0), inPoint.Y - (num3 / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element6 as IElement;
                point.PutCoords(inPoint.X + ((3.0 * num4) / 2.0), inPoint.Y - ((5.0 * num3) / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element7 as IElement;
                point.PutCoords(inPoint.X + ((5.0 * num4) / 2.0), inPoint.Y - (num3 / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element8 as IElement;
                point.PutCoords(inPoint.X + ((5.0 * num4) / 2.0), inPoint.Y - ((3.0 * num3) / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element9 as IElement;
                point.PutCoords(inPoint.X + ((5.0 * num4) / 2.0), inPoint.Y - ((5.0 * num3) / 2.0));
                item.Geometry = point;
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawLegend()
        {
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
        }

        public override void DrawOutFrame()
        {
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            IElement item = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = System.Type.Missing;
            try
            {
                if (!((base.LeftUp == null) || base.LeftUp.IsEmpty))
                {
                    inPoint.PutCoords(base.LeftUp.X - base.InOutDist, base.LeftUp.Y + base.InOutDist);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
                if (!((base.LeftLow == null) || base.LeftLow.IsEmpty))
                {
                    point2.PutCoords(base.LeftLow.X - base.InOutDist, base.LeftLow.Y - base.InOutDist);
                    points.AddPoint(point2, ref missing, ref missing);
                }
                if (!((base.RightLow == null) || base.RightLow.IsEmpty))
                {
                    point4.PutCoords(base.RightLow.X + base.InOutDist, base.RightLow.Y - base.InOutDist);
                    points.AddPoint(point4, ref missing, ref missing);
                }
                if (!((base.RightUp == null) || base.RightUp.IsEmpty))
                {
                    point3.PutCoords(base.RightUp.X + base.InOutDist, base.RightUp.Y + base.InOutDist);
                    points.AddPoint(point3, ref missing, ref missing);
                }
                if (!((base.LeftUp == null) || base.LeftUp.IsEmpty))
                {
                    inPoint.PutCoords(base.LeftUp.X - base.InOutDist, base.LeftUp.Y + base.InOutDist);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
                item.Geometry = polyline;
                properties = item as IElementProperties2;
                properties.Type = "外框";
                symbol = this.method_3();
                element2 = item as ILineElement;
                element2.Symbol = symbol;
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawRemark()
        {
            double num = 45.0;
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = new TextElementClass();
            ITextElement element3 = new TextElementClass();
            ITextElement element4 = new TextElementClass();
            ITextElement element5 = new TextElementClass();
            ITextElement element6 = new TextElementClass();
            ITextElement element7 = new TextElementClass();
            ITextSymbol symbol = null;
            double num2 = 0.0;
            double num3 = 0.0;
            try
            {
                element2.Text = this.string_4;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
                element2.Symbol = symbol;
                element3.Text = this.string_5;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
                element3.Symbol = symbol;
                element4.Text = this.string_6;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
                element4.Symbol = symbol;
                element5.Text = this.method_0(this.string_7);
                symbol = base.FontStyle(20.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
                element5.Symbol = symbol;
                element7.Text = this.string_7;
                element7.Symbol = symbol;
                this.method_1(element5, out num3, out num2);
                element6.Text = this.string_16;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
                element6.Symbol = symbol;
                item = element2 as IElement;
                point.PutCoords(base.RightUp.X, (base.RightUp.Y + base.InOutDist) + num);
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element3 as IElement;
                point.PutCoords(base.RightLow.X, (base.RightLow.Y - base.InOutDist) - num);
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element4 as IElement;
                point.PutCoords(base.LeftLow.X, (base.LeftLow.Y - base.InOutDist) - num);
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element5 as IElement;
                point.PutCoords((base.LeftLow.X - base.InOutDist) - num, base.LeftLow.Y + num2);
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element6 as IElement;
                point.PutCoords((base.LeftLow.X + base.RightLow.X) / 2.0, (base.LeftLow.Y - base.InOutDist) - num);
                item.Geometry = point;
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawTitle()
        {
            try
            {
                IPoint point = new PointClass();
                point.PutCoords((base.LeftUp.X + base.RightUp.X) / 2.0, (base.RightUp.Y + base.InOutDist) + base.TitleDist);
                IElement item = new TextElementClass {
                    Geometry = point
                };
                ITextElement element2 = item as ITextElement;
                element2.Text = base.MapTM + "\n" + base.MapTH;
                IElementProperties2 properties = element2 as IElementProperties2;
                ITextSymbol symbol = base.FontStyle(25.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
                element2.Symbol = symbol;
                properties.Type = "图名";
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private string method_0(string string_17)
        {
            string str = "\n";
            int startIndex = 0;
            string str2 = "";
            for (startIndex = 0; startIndex < (string_17.Trim().Length - 1); startIndex++)
            {
                str2 = str2 + string_17.Substring(startIndex, 1) + str;
            }
            return str2;
        }

        private void method_1(ITextElement itextElement_0, out double double_4, out double double_5)
        {
            double xSize = 0.0;
            double ySize = 0.0;
            double_4 = 0.0;
            double_5 = 0.0;
            double num3 = 2.54;
            base.m_pActiveView.FocusMap.ReferenceScale = base.m_ReferenceScale;
            try
            {
                base.m_pActiveView.ScreenDisplay.StartDrawing(base.m_pActiveView.ScreenDisplay.hDC, 0);
                itextElement_0.Symbol.GetTextSize(base.m_pActiveView.ScreenDisplay.hDC, base.m_pActiveView.ScreenDisplay.DisplayTransformation, itextElement_0.Text, out xSize, out ySize);
                base.m_pActiveView.ScreenDisplay.FinishDrawing();
                double_4 = ((xSize * (num3 / 72.0)) / 100.0) * base.m_ReferenceScale;
                double_5 = ((ySize * (num3 / 72.0)) / 100.0) * base.m_ReferenceScale;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private IGeometry method_10()
        {
            double x = 0.0;
            double y = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            IPoint inPoint = null;
            IPoint point2 = null;
            IPoint point3 = null;
            IPoint point4 = null;
            IPointCollection points = null;
            IGeometry geometry = new RingClass();
            object missing = System.Type.Missing;
            try
            {
                inPoint = new PointClass();
                point2 = new PointClass();
                point3 = new PointClass();
                point4 = new PointClass();
                x = base.LeftLow.X - 1000.0;
                y = base.LeftLow.Y - 1000.0;
                num3 = base.RightUp.X + 1000.0;
                num4 = base.RightUp.Y + 1000.0;
                inPoint.PutCoords(x, num4);
                point2.PutCoords(num3, num4);
                point3.PutCoords(num3, y);
                point4.PutCoords(x, y);
                points = (IPointCollection) geometry;
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point2, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(inPoint, ref missing, ref missing);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return geometry;
        }

        private void method_11()
        {
            if (this.ilist_0 != null)
            {
                this.ilist_0.Clear();
            }
        }

        private ILineSymbol method_2()
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

        private ILineSymbol method_3()
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

        private void method_4()
        {
            string str = "";
            string str2 = "\x00b0";
            string str3 = "'";
            string str4 = "″";
            string str5 = "";
            long num = 0L;
            int num2 = 0;
            int num3 = 0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double x = 0.0;
            double y = 0.0;
            bool flag = false;
            double num10 = 0.0;
            double num11 = 0.0;
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = null;
            ITextSymbol symbol = null;
            ITextSymbol symbol2 = null;
            ITextSymbol symbol3 = null;
            ITextSymbol symbol4 = null;
            symbol = base.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            symbol2 = base.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = base.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            symbol4 = base.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            x = base.LeftLow.X;
            y = base.LeftLow.Y;
            THTools tools = new THTools();
            str = base.MapTH.Trim();
            if (str != "")
            {
                if (str.Contains("-"))
                {
                    flag = tools.FileName2BL_cqtx(str, ref num5, ref num4, ref num6, ref num7);
                }
                else
                {
                    flag = tools.FileName2BL_tx(str, out num5, out num4, out num6, out num7);
                }
                if (flag)
                {
                    THTools.DEG2DDDMMSS(num4, ref num, ref num2, ref num3);
                    str5 = num.ToString() + str2 + num2.ToString() + str3 + num3.ToString() + str4;
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString();
                    element2.Symbol = symbol;
                    this.method_1(element2, out num10, out num11);
                    element2.Text = str5;
                    point.PutCoords(x - num10, y - base.InOutDist);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = str5;
                    element2.Symbol = symbol2;
                    point.PutCoords(base.LeftUp.X - num10, base.LeftUp.Y + base.InOutDist);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    num4 += num6;
                    THTools.DEG2DDDMMSS(num4, ref num, ref num2, ref num3);
                    str5 = num.ToString() + str2 + num2.ToString() + str3 + num3.ToString() + str4;
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2 + str4;
                    element2.Symbol = symbol;
                    this.method_1(element2, out num10, out num11);
                    element2.Text = str5;
                    point.PutCoords(base.RightLow.X - num10, base.RightLow.Y - base.InOutDist);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = str5;
                    element2.Symbol = symbol2;
                    point.PutCoords(base.RightUp.X - num10, base.RightUp.Y + base.InOutDist);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    THTools.DEG2DDDMMSS(num5, ref num, ref num2, ref num3);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(base.LeftLow.X - (base.InOutDist / 2.0), base.LeftLow.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Symbol = symbol4;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    point.PutCoords(base.LeftLow.X - ((base.InOutDist * 9.0) / 10.0), base.LeftLow.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(base.LeftUp.X - (base.InOutDist / 2.0), base.LeftUp.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    element2.Symbol = symbol4;
                    point.PutCoords(base.LeftUp.X - ((base.InOutDist * 9.0) / 10.0), base.LeftUp.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    num5 += num7;
                    THTools.DEG2DDDMMSS(num5, ref num, ref num2, ref num3);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(base.RightLow.X + (base.InOutDist / 2.0), base.RightLow.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Symbol = symbol4;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    point.PutCoords(base.RightLow.X + ((base.InOutDist * 1.0) / 10.0), base.RightLow.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    this.method_1(element2, out num10, out num11);
                    this.double_2 = num10;
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(base.RightUp.X + (base.InOutDist / 2.0), base.RightUp.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    element2.Symbol = symbol4;
                    point.PutCoords(base.RightUp.X + ((base.InOutDist * 1.0) / 10.0), base.RightUp.Y);
                    item.Geometry = point;
                    this.ilist_0.Add(item);
                }
            }
        }

        private IPolygon method_5()
        {
            IGeometryCollection geometrys = new PolygonClass();
            IRing inGeometry = new RingClass();
            IRing ring2 = new RingClass();
            IPointCollection points = null;
            IPointCollection points2 = null;
            object missing = System.Type.Missing;
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            points = inGeometry as IPointCollection;
            points.AddPoint(base.LeftUp, ref missing, ref missing);
            points.AddPoint(base.RightUp, ref missing, ref missing);
            points.AddPoint(base.RightLow, ref missing, ref missing);
            points.AddPoint(base.LeftLow, ref missing, ref missing);
            inGeometry.Close();
            geometrys.AddGeometry(inGeometry, ref missing, ref missing);
            inPoint.PutCoords(base.LeftUp.X - base.InOutDist, base.LeftUp.Y + base.InOutDist);
            point4.PutCoords(base.LeftLow.X - base.InOutDist, base.LeftLow.Y - base.InOutDist);
            point3.PutCoords(base.RightLow.X + base.InOutDist, base.RightLow.Y - base.InOutDist);
            point2.PutCoords(base.RightUp.X + base.InOutDist, base.RightUp.Y + base.InOutDist);
            points2 = ring2 as IPointCollection;
            points2.AddPoint(inPoint, ref missing, ref missing);
            points2.AddPoint(point2, ref missing, ref missing);
            points2.AddPoint(point3, ref missing, ref missing);
            points2.AddPoint(point4, ref missing, ref missing);
            ring2.Close();
            geometrys.AddGeometry(ring2, ref missing, ref missing);
            return (geometrys as IPolygon);
        }

        private IPointCollection method_6(IPolyline ipolyline_0, IPolygon ipolygon_0)
        {
            IMultipoint multipoint = null;
            ITopologicalOperator @operator = ipolygon_0 as ITopologicalOperator;
            @operator.Simplify();
            multipoint = @operator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
            return (multipoint as IPointCollection);
        }

        private ISymbol method_7()
        {
            IFillSymbol symbol = null;
            ILineSymbol symbol2 = null;
            IColor color = null;
            color = new RgbColorClass {
                NullColor = true
            };
            symbol = new SimpleFillSymbolClass();
            symbol2 = new SimpleLineSymbolClass();
            symbol.Color = color;
            IRgbColor color2 = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color2;
            symbol2.Width = 1.0;
            symbol.Outline = symbol2;
            return (symbol as ISymbol);
        }

        private IElement method_8()
        {
            object missing = System.Type.Missing;
            IPointCollection points = null;
            IElement element = new PolygonElementClass();
            IFillShapeElement element2 = element as IFillShapeElement;
            new RgbColorClass();
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = null;
            IGeometryCollection geometrys = new PolygonClass();
            IGeometry geometry3 = null;
            try
            {
                points = (IPointCollection) inGeometry;
                points.AddPoint(base.LeftLow, ref missing, ref missing);
                points.AddPoint(base.RightLow, ref missing, ref missing);
                points.AddPoint(base.RightUp, ref missing, ref missing);
                points.AddPoint(base.LeftUp, ref missing, ref missing);
                points.AddPoint(base.LeftLow, ref missing, ref missing);
                geometry2 = this.method_10();
                if (!geometry2.IsEmpty)
                {
                    geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                    geometrys.AddGeometry(geometry2, ref missing, ref missing);
                }
                geometry3 = geometrys as IGeometry;
                if (!geometry3.IsEmpty)
                {
                    geometry3.SpatialReference = geometry2.SpatialReference;
                    element.Geometry = geometry3;
                    element2.Symbol = base.GetBackStyle();
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return element;
        }

        private IEnvelope method_9(double double_4, double double_5, double double_6, double double_7)
        {
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            object missing = System.Type.Missing;
            IEnvelope envelope = null;
            IPointCollection points = null;
            IPolygon polygon = new PolygonClass();
            try
            {
                inPoint.PutCoords(base.LeftLow.X - double_4, base.LeftLow.Y - double_7);
                point2.PutCoords(base.RightLow.X + double_5, base.RightLow.Y - double_7);
                point3.PutCoords(base.RightUp.X + double_5, base.RightUp.Y + double_6);
                point4.PutCoords(base.LeftUp.X - double_4, base.LeftUp.Y + double_6);
                points = (IPointCollection) polygon;
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point2, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                polygon.Close();
                if (!polygon.IsEmpty)
                {
                    envelope = polygon.Envelope;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return envelope;
        }

        public string MapLeftBorderOutText
        {
            set
            {
                this.string_7 = value;
            }
        }

        public string MapLeftLowText
        {
            set
            {
                this.string_6 = value;
            }
        }

        public string MapRightLowTex
        {
            set
            {
                this.string_5 = value;
            }
        }

        public string MapRightUpText
        {
            set
            {
                this.string_4 = value;
            }
        }

        public string MapRow1Col1Text
        {
            set
            {
                this.string_8 = value;
            }
        }

        public string MapRow1Col2Text
        {
            set
            {
                this.string_11 = value;
            }
        }

        public string MapRow1Col3Text
        {
            set
            {
                this.string_13 = value;
            }
        }

        public string MapRow2Col1Text
        {
            set
            {
                this.string_9 = value;
            }
        }

        public string MapRow2Col3Text
        {
            set
            {
                this.string_14 = value;
            }
        }

        public string MapRow3Col1Text
        {
            set
            {
                this.string_10 = value;
            }
        }

        public string MapRow3Col2Text
        {
            set
            {
                this.string_12 = value;
            }
        }

        public string MapRow3Col3Text
        {
            set
            {
                this.string_15 = value;
            }
        }

        public string MapScaleText
        {
            set
            {
                this.string_16 = value;
            }
        }
    }
}

