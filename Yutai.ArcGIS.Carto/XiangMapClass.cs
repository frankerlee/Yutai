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
    internal class XiangMapClass : BaseMap
    {
        private double double_2 = 0.0;
        private double double_3 = 0.0;
        private double double_4 = 0.0;
        private IList<IElement> ilist_0 = new List<IElement>();
        private int int_0 = 0;
        private int int_1 = 0;
        private string string_4 = "";
        private string string_5 = "";
        private string string_6 = "";

        public XiangMapClass()
        {
            base.MapType = "乡图";
            base.InOutDist = 100.0;
            base.TitleDist = 150.0;
            this.double_2 = 150.0;
            base.NeedLegend = true;
        }

        public override void Draw()
        {
            IElement element = null;
            int num = 0;
            if (!this.method_8())
            {
                MessageBox.Show("请先绘制内图框。");
            }
            else
            {
                this.DrawInsideFrame();
                this.DrawOutFrame();
                this.DrawTitle();
                this.DrawRemark();
                this.DrawGrid();
                if (base.NeedLegend)
                {
                    this.DrawLegend();
                }
                if (base.m_pActiveView != null)
                {
                    base.GraphicsLayer = new CompositeGraphicsLayerClass();
                    ILayer graphicsLayer = base.m_GraphicsLayer as ILayer;
                    graphicsLayer.Name = "制图层";
                    (base.m_GraphicsLayer as IGeoDatasetSchemaEdit).AlterSpatialReference(base.m_pActiveView.FocusMap.SpatialReference);
                    IGraphicsContainer container = base.m_GraphicsLayer as IGraphicsContainer;
                    element = this.method_1();
                    if (element != null)
                    {
                        container.AddElement(element, 0);
                    }
                    for (num = 0; num < this.ilist_0.Count; num++)
                    {
                        container.AddElement(this.ilist_0[num], 1);
                    }
                    base.m_pActiveView.FocusMap.AddLayer(graphicsLayer);
                    base.m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_GraphicsLayer, null);
                }
                this.method_10();
            }
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
                polygon = this.method_3();
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
                ILineSymbol symbol9 = this.method_5(1);
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
                    points = this.method_4(polyline3, polygon);
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
                    num6 = (int) Math.Truncate((double) (y / 100000.0));
                    str = num6.ToString();
                    num7 = (int) Math.Truncate((double) ((y - (num6 * 0x186a0)) / 1000.0));
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
                    points = this.method_4(polyline3, polygon);
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
                    num6 = (int) Math.Truncate((double) (x / 100000.0));
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
                symbol = this.method_5(1);
                element2 = item as ILineElement;
                element2.Symbol = symbol;
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawLegend()
        {
            double num = this.double_3 * (base.RightUp.X - base.LeftUp.X);
            double num2 = this.double_4 * (base.RightUp.Y - base.RightLow.Y);
            double num3 = 0.01 * num;
            double num4 = 0.1 * num2;
            IPoint point = new PointClass();
            IPoint point2 = new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            switch (this.int_0)
            {
                case 0:
                    point.PutCoords(base.RightUp.X - num, base.RightUp.Y - num2);
                    point2.PutCoords(base.RightUp.X - num, base.RightUp.Y - num4);
                    break;

                case 1:
                    point.PutCoords(base.RightLow.X - num, base.RightLow.Y + num2);
                    point2.PutCoords(base.RightLow.X - num, (base.RightLow.Y + num2) - num4);
                    break;

                case 2:
                    point.PutCoords(base.LeftLow.X + num, base.LeftLow.Y + num2);
                    point2.PutCoords(base.LeftLow.X, (base.LeftLow.Y + num2) - num4);
                    break;

                case 3:
                    point.PutCoords(base.LeftUp.X + num, base.LeftUp.Y - num2);
                    point2.PutCoords(base.LeftUp.X, base.LeftUp.Y - num4);
                    break;
            }
            this.method_0(point, num, num2, num3, num4);
        }

        public override void DrawOutFrame()
        {
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            IElement item = new PolygonElementClass();
            IFillShapeElement element2 = item as IFillShapeElement;
            IGeometryCollection geometrys = new PolygonClass();
            IElementProperties2 properties = null;
            object missing = System.Type.Missing;
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = new RingClass();
            IPointCollection points = inGeometry as IPointCollection;
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
                geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                points = geometry2 as IPointCollection;
                if (!((base.LeftUp == null) || base.LeftUp.IsEmpty))
                {
                    inPoint.PutCoords((base.LeftUp.X - base.InOutDist) - this.double_2, (base.LeftUp.Y + base.InOutDist) + this.double_2);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
                if (!((base.LeftLow == null) || base.LeftLow.IsEmpty))
                {
                    point2.PutCoords((base.LeftLow.X - base.InOutDist) - this.double_2, (base.LeftLow.Y - base.InOutDist) - this.double_2);
                    points.AddPoint(point2, ref missing, ref missing);
                }
                if (!((base.RightLow == null) || base.RightLow.IsEmpty))
                {
                    point4.PutCoords((base.RightLow.X + base.InOutDist) + this.double_2, (base.RightLow.Y - base.InOutDist) - this.double_2);
                    points.AddPoint(point4, ref missing, ref missing);
                }
                if (!((base.RightUp == null) || base.RightUp.IsEmpty))
                {
                    point3.PutCoords((base.RightUp.X + base.InOutDist) + this.double_2, (base.RightUp.Y + base.InOutDist) + this.double_2);
                    points.AddPoint(point3, ref missing, ref missing);
                }
                if (!((base.LeftUp == null) || base.LeftUp.IsEmpty))
                {
                    inPoint.PutCoords((base.LeftUp.X - base.InOutDist) - this.double_2, (base.LeftUp.Y + base.InOutDist) + this.double_2);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
                geometrys.AddGeometry(geometry2, ref missing, ref missing);
                item.Geometry = geometrys as IGeometry;
                element2.Symbol = this.method_6();
                properties = item as IElementProperties2;
                properties.Type = "外框";
                this.ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawRemark()
        {
            double num = 10.0;
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = new TextElementClass();
            ITextElement element3 = new TextElementClass();
            ITextElement element4 = new TextElementClass();
            ITextSymbol symbol = null;
            try
            {
                element2.Text = this.string_4;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
                element2.Symbol = symbol;
                element3.Text = this.string_5;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
                element3.Symbol = symbol;
                element4.Text = this.string_6;
                symbol = base.FontStyle(15.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
                element4.Symbol = symbol;
                item = element2 as IElement;
                point.PutCoords(base.RightLow.X, ((base.RightLow.Y - base.InOutDist) - this.double_2) - num);
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element3 as IElement;
                point.PutCoords(base.LeftLow.X, ((base.LeftLow.Y - base.InOutDist) - this.double_2) - num);
                item.Geometry = point;
                this.ilist_0.Add(item);
                item = element4 as IElement;
                point.PutCoords((base.LeftLow.X + base.RightLow.X) / 2.0, ((base.LeftLow.Y - base.InOutDist) - this.double_2) - num);
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
                point.PutCoords((base.LeftUp.X + base.RightUp.X) / 2.0, ((base.RightUp.Y + base.InOutDist) + this.double_2) + base.TitleDist);
                IElement item = new TextElementClass {
                    Geometry = point
                };
                ITextElement element2 = item as ITextElement;
                element2.Text = base.MapTM;
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

        private void method_0(IPoint ipoint_4, double double_5, double double_6, double double_7, double double_8)
        {
            object missing = System.Type.Missing;
            IPolyline polyline = new PolylineClass();
            IPolyline polyline2 = new PolylineClass();
            IPolyline polyline3 = new PolylineClass();
            IElement item = new LineElementClass();
            IElement element2 = new LineElementClass();
            IElement element3 = new LineElementClass();
            ILineElement element4 = null;
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPointCollection points = polyline as IPointCollection;
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            IPoint point5 = new PointClass();
            IPoint point6 = new PointClass();
            IPoint point7 = new PointClass();
            IPoint point8 = new PointClass();
            IElement element5 = new TextElementClass();
            switch (this.int_0)
            {
                case 0:
                    inPoint.PutCoords(ipoint_4.X, base.RightUp.Y);
                    point2.PutCoords(base.RightUp.X, base.RightUp.Y - double_6);
                    point3.PutCoords(ipoint_4.X - double_7, base.RightUp.Y);
                    point4.PutCoords(ipoint_4.X - double_7, ipoint_4.Y - double_7);
                    point5.PutCoords(base.RightUp.X, (base.RightUp.Y - double_6) - double_7);
                    point6.PutCoords(base.RightUp.X, base.RightUp.Y - double_8);
                    point7.PutCoords(base.RightUp.X - double_5, base.RightUp.Y - double_8);
                    point8.PutCoords(base.RightUp.X - (double_5 / 2.0), base.RightUp.Y - (double_8 / 2.0));
                    break;

                case 1:
                    inPoint.PutCoords(ipoint_4.X, base.RightLow.Y);
                    point2.PutCoords(base.RightLow.X, base.RightLow.Y + double_6);
                    point3.PutCoords(ipoint_4.X - double_7, base.RightLow.Y);
                    point4.PutCoords(ipoint_4.X - double_7, ipoint_4.Y + double_7);
                    point5.PutCoords(base.RightLow.X, (base.RightLow.Y + double_6) + double_7);
                    point6.PutCoords(base.RightLow.X, (base.RightLow.Y + double_6) - double_8);
                    point7.PutCoords(base.RightLow.X - double_5, (base.RightLow.Y + double_6) - double_8);
                    point8.PutCoords(base.RightLow.X - (double_5 / 2.0), (base.RightLow.Y + double_6) - (double_8 / 2.0));
                    break;

                case 2:
                    inPoint.PutCoords(ipoint_4.X, base.LeftLow.Y);
                    point2.PutCoords(base.LeftLow.X, base.LeftLow.Y + double_6);
                    point3.PutCoords(ipoint_4.X + double_7, base.LeftLow.Y);
                    point4.PutCoords(ipoint_4.X + double_7, ipoint_4.Y + double_7);
                    point5.PutCoords(base.LeftLow.X, (base.LeftLow.Y + double_6) + double_7);
                    point6.PutCoords(base.LeftLow.X, (base.LeftLow.Y + double_6) - double_8);
                    point7.PutCoords(base.LeftLow.X + double_5, (base.LeftLow.Y + double_6) - double_8);
                    point8.PutCoords(base.LeftLow.X + (double_5 / 2.0), (base.LeftLow.Y + double_6) - (double_8 / 2.0));
                    break;

                case 3:
                    inPoint.PutCoords(ipoint_4.X, base.LeftUp.Y);
                    point2.PutCoords(base.LeftUp.X, base.LeftUp.Y - double_6);
                    point3.PutCoords(ipoint_4.X + double_7, base.LeftUp.Y);
                    point4.PutCoords(ipoint_4.X + double_7, ipoint_4.Y - double_7);
                    point5.PutCoords(base.LeftUp.X, (base.LeftUp.Y - double_6) - double_7);
                    point6.PutCoords(base.LeftUp.X, base.RightUp.Y - double_8);
                    point7.PutCoords(base.LeftUp.X + double_5, base.LeftUp.Y - double_8);
                    point8.PutCoords(base.LeftUp.X + (double_5 / 2.0), base.LeftUp.Y - (double_8 / 2.0));
                    break;
            }
            points.AddPoint(inPoint, ref missing, ref missing);
            points.AddPoint(ipoint_4, ref missing, ref missing);
            points.AddPoint(point2, ref missing, ref missing);
            item.Geometry = polyline;
            element4 = item as ILineElement;
            element4.Symbol = this.method_5(1);
            this.ilist_0.Add(item);
            points = polyline2 as IPointCollection;
            points.AddPoint(point3, ref missing, ref missing);
            points.AddPoint(point4, ref missing, ref missing);
            points.AddPoint(point5, ref missing, ref missing);
            element2.Geometry = polyline2;
            element4 = element2 as ILineElement;
            element4.Symbol = this.method_5(2);
            this.ilist_0.Add(element2);
            points = polyline3 as IPointCollection;
            points.AddPoint(point6, ref missing, ref missing);
            points.AddPoint(point7, ref missing, ref missing);
            element3.Geometry = polyline3;
            element4 = element3 as ILineElement;
            element4.Symbol = this.method_5(1);
            this.ilist_0.Add(element3);
            element5.Geometry = point8;
            ITextElement element6 = element5 as ITextElement;
            element6.Text = "图  例";
            element6.Symbol = base.FontStyle(20.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVACenter);
            this.ilist_0.Add(element5);
        }

        private IElement method_1()
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
            points = (IPointCollection) inGeometry;
            points.AddPoint(base.LeftLow, ref missing, ref missing);
            points.AddPoint(base.RightLow, ref missing, ref missing);
            points.AddPoint(base.RightUp, ref missing, ref missing);
            points.AddPoint(base.LeftUp, ref missing, ref missing);
            points.AddPoint(base.LeftLow, ref missing, ref missing);
            geometry2 = this.method_2();
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
                element2.Symbol = this.method_7();
            }
            return element;
        }

        private void method_10()
        {
            if (this.ilist_0 != null)
            {
                this.ilist_0.Clear();
            }
        }

        private IGeometry method_2()
        {
            double x = 0.0;
            double y = 0.0;
            double xMax = 0.0;
            double yMax = 0.0;
            IPoint inPoint = null;
            IPoint point2 = null;
            IPoint point3 = null;
            IPoint point4 = null;
            IPointCollection points = null;
            IGeometry geometry = new RingClass();
            object missing = System.Type.Missing;
            IFeatureWorkspace workspace = null;
            IFeatureClass class2 = null;
            IFeatureCursor cursor = null;
            IFeature feature = null;
            IGeometryCollection geometrys = new GeometryBagClass();
            IGeometryBag bag = geometrys as IGeometryBag;
            workspace = null;
            try
            {
                if (workspace == null)
                {
                    return geometry;
                }
                class2 = workspace.OpenFeatureClass("TKMC");
                if (class2 == null)
                {
                    return geometry;
                }
                cursor = class2.Search(null, false);
                for (feature = cursor.NextFeature(); feature != null; feature = cursor.NextFeature())
                {
                    if (!feature.Shape.IsEmpty)
                    {
                        geometrys.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
                    }
                }
                if (geometrys.GeometryCount >= 1)
                {
                    inPoint = new PointClass();
                    point2 = new PointClass();
                    point3 = new PointClass();
                    point4 = new PointClass();
                    x = bag.Envelope.XMin;
                    y = bag.Envelope.YMin;
                    xMax = bag.Envelope.XMax;
                    yMax = bag.Envelope.YMax;
                    inPoint.PutCoords(x, yMax);
                    point2.PutCoords(xMax, yMax);
                    point3.PutCoords(xMax, y);
                    point4.PutCoords(x, y);
                    points = (IPointCollection) geometry;
                    points.AddPoint(inPoint, ref missing, ref missing);
                    points.AddPoint(point2, ref missing, ref missing);
                    points.AddPoint(point3, ref missing, ref missing);
                    points.AddPoint(point4, ref missing, ref missing);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return geometry;
        }

        private IPolygon method_3()
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

        private IPointCollection method_4(IPolyline ipolyline_0, IPolygon ipolygon_0)
        {
            IMultipoint multipoint = null;
            ITopologicalOperator @operator = ipolygon_0 as ITopologicalOperator;
            @operator.Simplify();
            multipoint = @operator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
            return (multipoint as IPointCollection);
        }

        private ILineSymbol method_5(int int_2)
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = int_2;
            return symbol2;
        }

        private IFillSymbol method_6()
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

        private IFillSymbol method_7()
        {
            ISimpleFillSymbol symbol = new SimpleFillSymbolClass();
            new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0xff,
                Blue = 0xff,
                Green = 0xff
            };
            symbol.Color = color;
            symbol.Outline = null;
            return symbol;
        }

        private bool method_8()
        {
            bool flag = false;
            try
            {
                IGraphicsContainer graphicsContainer = base.m_pActiveView.GraphicsContainer;
                graphicsContainer.Reset();
                IElement element = graphicsContainer.Next();
                IEnvelope envelope = null;
                while (element != null)
                {
                    IElementProperties properties = (IElementProperties) element;
                    if (properties.Name == "TK")
                    {
                        goto Label_005D;
                    }
                    if (element is IMapFrame)
                    {
                        goto Label_009D;
                    }
                    element = graphicsContainer.Next();
                }
                return flag;
            Label_005D:
                envelope = element.Geometry.Envelope;
                base.LeftLow = envelope.LowerLeft;
                base.LeftUp = envelope.UpperLeft;
                base.RightLow = envelope.LowerRight;
                base.RightUp = envelope.UpperRight;
                return true;
            Label_009D:
                envelope = element.Geometry.Envelope;
                base.LeftLow = envelope.LowerLeft;
                base.LeftUp = envelope.UpperLeft;
                base.RightLow = envelope.LowerRight;
                base.RightUp = envelope.UpperRight;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void method_9()
        {
            try
            {
                IGraphicsContainer graphicsContainer = base.m_pActiveView.GraphicsContainer;
                graphicsContainer.Reset();
                IElement element = graphicsContainer.Next();
                while (element != null)
                {
                    IElementProperties properties = (IElementProperties) element;
                    if (properties.Name == "TK")
                    {
                        goto Label_004A;
                    }
                    element = graphicsContainer.Next();
                }
                return;
            Label_004A:
                graphicsContainer.DeleteElement(element);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public string MapLeftLowText
        {
            set
            {
                this.string_5 = value;
            }
        }

        public string MapRightLowTex
        {
            set
            {
                this.string_4 = value;
            }
        }

        public string MapScaleText
        {
            set
            {
                this.string_6 = value;
            }
        }
    }
}

