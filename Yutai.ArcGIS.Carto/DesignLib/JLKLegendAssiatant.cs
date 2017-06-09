using System;
using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class JLKLegendAssiatant
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private double double_0 = 10.0;
        private double double_1 = 10.0;
        private double double_2 = 10.0;
        private double double_3 = 10.0;
        private double double_4 = 1.0;
        private IList<ISymbol> ilist_0 = new List<ISymbol>();
        private IList<string> ilist_1 = new List<string>();
        private int int_0 = 3;
        private ITextSymbol itextSymbol_0 = null;
        private ITextSymbol itextSymbol_1 = null;
        private string string_0 = "图例";

        public void Create(IActiveView iactiveView_0, IEnvelope ienvelope_0)
        {
            IPoint upperLeft = ienvelope_0.UpperLeft;
            int num = this.ilist_0.Count / this.int_0;
            if ((num * this.double_0) < this.ilist_0.Count)
            {
                num++;
            }
            int num2 = 0;
            double num3 = 0.0;
            double height = 0.0;
            upperLeft.X += 0.1;
            upperLeft.Y += 0.1;
            IGroupElement data = new GroupElementClass();
            if (this.string_0.Length > 0)
            {
                IElement element = this.method_5(upperLeft);
                IEnvelope envelope = new EnvelopeClass();
                element.QueryBounds(iactiveView_0.ScreenDisplay, envelope);
                height = envelope.Height;
                data.AddElement(element);
            }
            double x = upperLeft.X;
            double y = upperLeft.Y - height;
            IPoint point2 = new PointClass();
            IEnvelope bounds = new EnvelopeClass();
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                ISymbol local1 = this.ilist_0[i];
                point2.PutCoords(x, y);
                IElement element3 = this.method_6(point2, this.ilist_0[i], this.ilist_1[i]);
                element3.QueryBounds(iactiveView_0.ScreenDisplay, bounds);
                if (element3 is IGroupElement)
                {
                    for (int j = 0; j < (element3 as IGroupElement).ElementCount; j++)
                    {
                        data.AddElement((element3 as IGroupElement).get_Element(j));
                    }
                }
                else
                {
                    data.AddElement(element3);
                }
                num3 = (num3 > bounds.Width) ? num3 : bounds.Width;
                y = (y - this.double_3) - this.double_1;
                num2++;
                if (num2 == num)
                {
                    y = upperLeft.Y - height;
                    x += num3 + this.double_0;
                    num2 = 0;
                }
            }
            (data as IElement).QueryBounds(iactiveView_0.ScreenDisplay, bounds);
            double sx = ienvelope_0.Width / bounds.Width;
            double sy = ienvelope_0.Height / bounds.Height;
            ienvelope_0.Expand(-0.05, -0.05, false);
            (data as ITransform2D).Scale(ienvelope_0.UpperLeft, sx, sy);
            ienvelope_0.Expand(0.05, 0.05, false);
            if (this.bool_0)
            {
                data.AddElement(this.method_10(ienvelope_0));
            }
            (iactiveView_0 as IGraphicsContainer).AddElement(data as IElement, -1);
            iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, data, null);
        }

        public void Create(IActiveView iactiveView_0, IPoint ipoint_0)
        {
            int num = this.ilist_0.Count / this.int_0;
            if ((num * this.double_0) < this.ilist_0.Count)
            {
                num++;
            }
            int num2 = 0;
            double num3 = 0.0;
            double height = 0.0;
            IGroupElement data = new GroupElementClass();
            ipoint_0.X += 0.1;
            ipoint_0.Y += 0.1;
            if (this.string_0.Length > 0)
            {
                IElement element = this.method_5(ipoint_0);
                IEnvelope envelope = new EnvelopeClass();
                element.QueryBounds(iactiveView_0.ScreenDisplay, envelope);
                height = envelope.Height;
                data.AddElement(element);
            }
            double x = ipoint_0.X;
            double y = ipoint_0.Y - height;
            IPoint point = new PointClass();
            IEnvelope bounds = new EnvelopeClass();
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                ISymbol local1 = this.ilist_0[i];
                point.PutCoords(x, y);
                IElement element3 = this.method_6(point, this.ilist_0[i], this.ilist_1[i]);
                element3.QueryBounds(iactiveView_0.ScreenDisplay, bounds);
                if (element3 is IGroupElement)
                {
                    for (int j = 0; j < (element3 as IGroupElement).ElementCount; j++)
                    {
                        data.AddElement((element3 as IGroupElement).get_Element(j));
                    }
                }
                else
                {
                    data.AddElement(element3);
                }
                (data as IGroupElement2).Refresh();
                num3 = (num3 > bounds.Width) ? num3 : bounds.Width;
                y = (y - this.double_3) - this.double_1;
                num2++;
                if (num2 == num)
                {
                    y = ipoint_0.Y - height;
                    x += num3 + this.double_0;
                    num2 = 0;
                    num3 = 0.0;
                }
            }
            IEnvelope envelope3 = new EnvelopeClass();
            (data as IElement).QueryBounds(iactiveView_0.ScreenDisplay, envelope3);
            envelope3.Expand(0.05, 0.05, false);
            try
            {
                if (this.bool_0)
                {
                    data.AddElement(this.method_10(envelope3));
                }
                (data as IElement).QueryBounds(iactiveView_0.ScreenDisplay, envelope3);
                (data as ITransform2D).Scale(envelope3.UpperLeft, 1.0, 1.0);
                (iactiveView_0 as IGraphicsContainer).AddElement(data as IElement, -1);
            }
            catch (Exception)
            {
            }
            iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, data, null);
        }

        public IElement CreateElement(IActiveView iactiveView_0, IPoint ipoint_0)
        {
            int num = this.ilist_0.Count / this.int_0;
            if ((num * this.double_0) < this.ilist_0.Count)
            {
                num++;
            }
            int num2 = 0;
            double num3 = 0.0;
            double height = 0.0;
            IGroupElement element = new GroupElementClass();
            ipoint_0.X += 0.1;
            ipoint_0.Y += 0.1;
            if (this.string_0.Length > 0)
            {
                IElement element2 = this.method_5(ipoint_0);
                IEnvelope envelope = new EnvelopeClass();
                element2.QueryBounds(iactiveView_0.ScreenDisplay, envelope);
                height = envelope.Height;
                element.AddElement(element2);
            }
            double x = ipoint_0.X;
            double y = ipoint_0.Y - height;
            IPoint point = new PointClass();
            IEnvelope bounds = new EnvelopeClass();
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                ISymbol local1 = this.ilist_0[i];
                point.PutCoords(x, y);
                IElement element3 = this.method_6(point, this.ilist_0[i], this.ilist_1[i]);
                element3.QueryBounds(iactiveView_0.ScreenDisplay, bounds);
                if (element3 is IGroupElement)
                {
                    for (int j = 0; j < (element3 as IGroupElement).ElementCount; j++)
                    {
                        element.AddElement((element3 as IGroupElement).get_Element(j));
                    }
                }
                else
                {
                    element.AddElement(element3);
                }
                (element as IGroupElement2).Refresh();
                num3 = (num3 > bounds.Width) ? num3 : bounds.Width;
                y = (y - this.double_3) - this.double_1;
                num2++;
                if (num2 == num)
                {
                    y = ipoint_0.Y - height;
                    x += num3 + this.double_0;
                    num2 = 0;
                    num3 = 0.0;
                }
            }
            IEnvelope envelope3 = new EnvelopeClass();
            (element as IElement).QueryBounds(iactiveView_0.ScreenDisplay, envelope3);
            envelope3.Expand(0.05, 0.05, false);
            try
            {
                if (this.bool_0)
                {
                    element.AddElement(this.method_10(envelope3));
                }
                (element as IElement).QueryBounds(iactiveView_0.ScreenDisplay, envelope3);
                (element as ITransform2D).Scale(envelope3.UpperLeft, 1.0, 1.0);
            }
            catch (Exception)
            {
            }
            (element as IElementProperties).Name = "图例";
            return (element as IElement);
        }

        protected ITextSymbol FontStyle(double double_5, esriTextHorizontalAlignment esriTextHorizontalAlignment_0, esriTextVerticalAlignment esriTextVerticalAlignment_0)
        {
            return new TextSymbolClass { Size = double_5, Color = ColorManage.CreatColor(0, 0, 0), HorizontalAlignment = esriTextHorizontalAlignment_0, VerticalAlignment = esriTextVerticalAlignment_0 };
        }

        public void Init(int int_1)
        {
            if (int_1 == 1)
            {
                this.double_0 = 0.1;
                this.double_1 = 0.1;
                this.double_2 = 1.0;
                this.double_3 = 1.0;
                this.double_4 = 0.1;
            }
            ISymbol item = new SimpleMarkerSymbolClass();
            (item as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCircle;
            this.ilist_0.Add(item);
            this.ilist_1.Add("圆retr678978");
            item = new SimpleMarkerSymbolClass();
            (item as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
            this.ilist_0.Add(item);
            this.ilist_1.Add("十字形");
            item = new SimpleMarkerSymbolClass();
            (item as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSDiamond;
            this.ilist_0.Add(item);
            this.ilist_1.Add("菱形");
            item = new SimpleLineSymbolClass();
            (item as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSDashDot;
            this.ilist_0.Add(item);
            this.ilist_1.Add("线");
            item = new SimpleLineSymbolClass();
            (item as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSSolid;
            this.ilist_0.Add(item);
            this.ilist_1.Add("线2");
            item = new SimpleLineSymbolClass();
            (item as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSDashDot;
            (item as ISimpleLineSymbol).Width = 3.0;
            this.ilist_0.Add(item);
            this.ilist_1.Add("线3");
            item = new SimpleFillSymbolClass();
            (item as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
            this.ilist_0.Add(item);
            this.ilist_1.Add("面1");
            item = new SimpleFillSymbolClass();
            (item as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSHorizontal;
            this.ilist_0.Add(item);
            this.ilist_1.Add("面2");
        }

        public void Load(string string_1)
        {
            XmlDocument document = new XmlDocument();
            document.Load(string_1);
            XmlElement documentElement = document.DocumentElement;
            this.method_0(documentElement);
        }

        public void LoadXml(string string_1)
        {
            if (string_1.Length != 0)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(string_1);
                XmlElement documentElement = document.DocumentElement;
                this.method_0(documentElement);
            }
        }

        private void method_0(XmlNode xmlNode_0)
        {
            int num = 0;
        Label_0002:
            if (num >= xmlNode_0.Attributes.Count)
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "legenditems")
                    {
                        this.method_1(node);
                    }
                }
            }
            else
            {
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "title":
                        this.string_0 = attribute.Value;
                        break;

                    case "column":
                        this.int_0 = int.Parse(attribute.Value);
                        break;

                    case "rowspace":
                        this.double_1 = double.Parse(attribute.Value);
                        break;

                    case "columnspace":
                        this.double_0 = double.Parse(attribute.Value);
                        break;

                    case "hasborder":
                        try
                        {
                            this.bool_0 = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
                goto Label_0002;
            }
        }

        private void method_1(XmlNode xmlNode_0)
        {
            int num = 0;
        Label_0002:
            if (num >= xmlNode_0.Attributes.Count)
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "legenditem")
                    {
                        this.method_2(node);
                    }
                }
            }
            else
            {
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "width":
                        this.double_2 = double.Parse(attribute.Value);
                        break;

                    case "height":
                        this.double_3 = double.Parse(attribute.Value);
                        break;

                    case "labelspace":
                        this.double_4 = double.Parse(attribute.Value);
                        break;

                    case "hasborder":
                        try
                        {
                            this.bool_1 = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
                goto Label_0002;
            }
        }

        private IElement method_10(IEnvelope ienvelope_0)
        {
            IFillSymbol symbol = new SimpleFillSymbolClass();
            (symbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            IPolygon polygon = new PolygonClass();
            object missing = Type.Missing;
            (polygon as IPointCollection).AddPoint(ienvelope_0.LowerLeft, ref missing, ref missing);
            (polygon as IPointCollection).AddPoint(ienvelope_0.UpperLeft, ref missing, ref missing);
            (polygon as IPointCollection).AddPoint(ienvelope_0.UpperRight, ref missing, ref missing);
            (polygon as IPointCollection).AddPoint(ienvelope_0.LowerRight, ref missing, ref missing);
            (polygon as IPointCollection).AddPoint(ienvelope_0.LowerLeft, ref missing, ref missing);
            IElement element = new PolygonElementClass {
                Geometry = polygon
            };
            (element as IFillShapeElement).Symbol = symbol;
            return element;
        }

        private IElement method_11(IPoint ipoint_0, IFillSymbol ifillSymbol_0, string string_1)
        {
            IGroupElement element = new GroupElementClass();
            IPolygon polygon = new PolygonClass();
            object missing = Type.Missing;
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_0.Y);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_0.Y - this.double_3);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X + this.double_2, ipoint_0.Y - this.double_3);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X + this.double_2, ipoint_0.Y);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_0.Y);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            IElement element2 = new PolygonElementClass {
                Geometry = polygon
            };
            (element2 as IFillShapeElement).Symbol = ifillSymbol_0;
            element.AddElement(element2);
            if (this.bool_1)
            {
                element.AddElement(this.method_9(ipoint_0));
            }
            if (string_1.Length > 0)
            {
                element.AddElement(this.method_12(ipoint_0, string_1, 10));
            }
            return (element as IElement);
        }

        private IElement method_12(IPoint ipoint_0, string string_1, int int_1)
        {
            IPoint point = new PointClass();
            point.PutCoords((ipoint_0.X + this.double_2) + this.double_4, ipoint_0.Y - (this.double_3 / 2.0));
            IElement element = new TextElementClass {
                Geometry = point
            };
            (element as ITextElement).Text = string_1;
            (element as ITextElement).Symbol = this.FontStyle((double) int_1, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVACenter);
            return element;
        }

        private void method_2(XmlNode xmlNode_0)
        {
            ISymbol item = null;
            string str = "";
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "description":
                        str = attribute.Value;
                        break;

                    case "symbol":
                        item = this.method_3(attribute.Value);
                        break;
                }
            }
            if (item != null)
            {
                this.ilist_0.Add(item);
                this.ilist_1.Add(str);
            }
        }

        private ISymbol method_3(string string_1)
        {
            int num2;
            byte[] buffer = Convert.FromBase64String(string_1);
            int num = buffer.Length - 0x10;
            byte[] b = new byte[0x10];
            for (num2 = 0; num2 < 0x10; num2++)
            {
                b[num2] = buffer[num2];
            }
            Guid clsid = new Guid(b);
            IPersistStream stream = Activator.CreateInstance(Type.GetTypeFromCLSID(clsid)) as IPersistStream;
            byte[] buffer3 = new byte[num];
            for (num2 = 0; num2 < num; num2++)
            {
                buffer3[num2] = buffer[num2 + 0x10];
            }
            IMemoryBlobStream pstm = new MemoryBlobStreamClass();
            ((IMemoryBlobStreamVariant) pstm).ImportFromVariant(buffer3);
            stream.Load(pstm);
            return (stream as ISymbol);
        }

        private void method_4(IGraphicsContainer igraphicsContainer_0, IEnvelope ienvelope_0)
        {
            IGroupElement2 element = null;
            IGraphicsContainerSelect select = igraphicsContainer_0 as IGraphicsContainerSelect;
            IEnumElement selectedElements = select.SelectedElements;
            selectedElements.Reset();
            IElement element3 = selectedElements.Next();
            element = new GroupElementClass();
            (element as IElement).Geometry = ienvelope_0;
            while (element3 != null)
            {
                element.AddElement(element3);
                element.Refresh();
                element3 = selectedElements.Next();
            }
            igraphicsContainer_0.AddElement(element as IElement, -1);
            (element as IElement).QueryBounds((igraphicsContainer_0 as IActiveView).ScreenDisplay, ienvelope_0);
            selectedElements.Reset();
            for (element3 = selectedElements.Next(); element3 != null; element3 = selectedElements.Next())
            {
                igraphicsContainer_0.DeleteElement(element3);
            }
            select.SelectElement(element as IElement);
        }

        private IElement method_5(IPoint ipoint_0)
        {
            IPoint point = new PointClass();
            point.PutCoords(ipoint_0.X, ipoint_0.Y);
            IElement element = new TextElementClass {
                Geometry = point
            };
            (element as ITextElement).Text = this.string_0;
            (element as ITextElement).Symbol = this.FontStyle(20.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            return element;
        }

        private IElement method_6(IPoint ipoint_0, ISymbol isymbol_0, string string_1)
        {
            if (isymbol_0 is IMarkerSymbol)
            {
                return this.method_7(ipoint_0, isymbol_0 as IMarkerSymbol, string_1);
            }
            if (isymbol_0 is ILineSymbol)
            {
                return this.method_8(ipoint_0, isymbol_0 as ILineSymbol, string_1);
            }
            if (isymbol_0 is IFillSymbol)
            {
                return this.method_11(ipoint_0, isymbol_0 as IFillSymbol, string_1);
            }
            return null;
        }

        private IElement method_7(IPoint ipoint_0, IMarkerSymbol imarkerSymbol_0, string string_1)
        {
            IGroupElement element = new GroupElementClass();
            IPoint point = new PointClass();
            point.PutCoords(ipoint_0.X + (this.double_2 / 2.0), ipoint_0.Y - (this.double_3 / 2.0));
            IElement element2 = new MarkerElementClass {
                Geometry = point
            };
            (element2 as IMarkerElement).Symbol = imarkerSymbol_0;
            element.AddElement(element2);
            if (this.bool_1)
            {
                element.AddElement(this.method_9(ipoint_0));
            }
            if (string_1.Length > 0)
            {
                element.AddElement(this.method_12(ipoint_0, string_1, 10));
            }
            return (element as IElement);
        }

        private IElement method_8(IPoint ipoint_0, ILineSymbol ilineSymbol_0, string string_1)
        {
            IGroupElement element = new GroupElementClass();
            IPolyline polyline = new PolylineClass();
            object missing = Type.Missing;
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X + (this.double_2 / 10.0), ipoint_0.Y - (this.double_3 / 2.0));
            (polyline as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X + (this.double_2 * 0.9), ipoint_0.Y - (this.double_3 / 2.0));
            (polyline as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            IElement element2 = new LineElementClass {
                Geometry = polyline
            };
            (element2 as ILineElement).Symbol = ilineSymbol_0;
            element.AddElement(element2);
            if (this.bool_1)
            {
                element.AddElement(this.method_9(ipoint_0));
            }
            if (string_1.Length > 0)
            {
                element.AddElement(this.method_12(ipoint_0, string_1, 10));
            }
            return (element as IElement);
        }

        private IElement method_9(IPoint ipoint_0)
        {
            IFillSymbol symbol = new SimpleFillSymbolClass();
            (symbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            IPolygon polygon = new PolygonClass();
            object missing = Type.Missing;
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_0.Y);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_0.Y - this.double_3);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X + this.double_2, ipoint_0.Y - this.double_3);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X + this.double_2, ipoint_0.Y);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_0.Y);
            (polygon as IPointCollection).AddPoint(inPoint, ref missing, ref missing);
            IElement element = new PolygonElementClass {
                Geometry = polygon
            };
            (element as IFillShapeElement).Symbol = symbol;
            return element;
        }
    }
}

