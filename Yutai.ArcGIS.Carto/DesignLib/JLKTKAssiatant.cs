using System;
using System.Collections.Generic;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using stdole;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class JLKTKAssiatant
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = true;
        private double double_0 = 1.0;
        private double double_1 = 0.1;
        private double double_10 = 13.0;
        private double double_2 = 1000.0;
        private double double_3 = 1000.0;
        private double double_4 = 10000.0;
        private double double_5 = 0.3;
        private double double_6 = 1000.0;
        private double double_7 = 0.0;
        private double double_8 = 0.0;
        private double double_9 = 13.0;
        private ISymbol isymbol_0 = null;
        private ISymbol isymbol_1 = null;
        private SpheroidType spheroidType_0 = SpheroidType.Xian1980;
        private string string_0 = "";
        private string string_1 = "图名";
        private string string_2 = "图号";
        private string string_3 = "1:10000";
        private string string_4 = "宋体";
        private StripType stripType_0 = StripType.STThreeDeg;
        private TKType tktype_0 = TKType.TKStandard;

        public IElement CreateCornerShortLine(IPoint ipoint_0, IPoint ipoint_1)
        {
            ILineSymbol symbol = this.method_1();
            IGroupElement element = new GroupElementClass();
            element.AddElement(this.method_19(ipoint_0.X, ipoint_0.Y, -this.double_0, -this.double_0, symbol));
            element.AddElement(this.method_19(ipoint_0.X, ipoint_1.Y, -this.double_0, this.double_0, symbol));
            element.AddElement(this.method_19(ipoint_1.X, ipoint_1.Y, this.double_0, this.double_0, symbol));
            element.AddElement(this.method_19(ipoint_1.X, ipoint_0.Y, this.double_0, -this.double_0, symbol));
            return (element as IElement);
        }

        public IElement CreateCornerShortLine(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            ILineSymbol symbol = this.method_1();
            IGroupElement element = new GroupElementClass();
            element.AddElement(this.method_19(ipoint_0.X, ipoint_0.Y, -this.double_0, -this.double_0, symbol));
            element.AddElement(this.method_19(ipoint_1.X, ipoint_1.Y, -this.double_0, this.double_0, symbol));
            element.AddElement(this.method_19(ipoint_2.X, ipoint_2.Y, this.double_0, this.double_0, symbol));
            element.AddElement(this.method_19(ipoint_3.X, ipoint_3.Y, this.double_0, -this.double_0, symbol));
            return (element as IElement);
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

        public void CreateTK(IPageLayout ipageLayout_0, string string_5)
        {
            IMapFrame frame = CommandFunction.FindFocusMapFrame(ipageLayout_0);
            if (frame != null)
            {
                THTools tools = new THTools();
                bool flag = false;
                IList<IPoint> list = tools.GetProjectCoord(string_5, this.spheroidType_0 == SpheroidType.Xian1980,
                    this.stripType_0 == StripType.STSixDeg, 0, ref flag);
                if (flag)
                {
                    this.Scale = THTools.GetTHScale(string_5);
                    if (!this.bool_1)
                    {
                        this.double_2 *= this.Scale/100.0;
                        this.double_3 *= this.Scale/100.0;
                        this.bool_1 = true;
                    }
                    double xMin = (list[3].X < list[0].X) ? list[3].X : list[0].X;
                    double xMax = (list[1].X > list[2].X) ? list[1].X : list[2].X;
                    double yMin = (list[3].Y < list[2].Y) ? list[3].Y : list[2].Y;
                    double yMax = (list[1].Y > list[0].Y) ? list[1].Y : list[0].Y;
                    double num5 = ((xMax - xMin)/this.double_4)*100.0;
                    double num6 = ((yMax - yMin)/this.double_4)*100.0;
                    IEnvelope from = (frame as IElement).Geometry.Envelope;
                    IEnvelope to = new EnvelopeClass();
                    to.PutCoords(4.0, 4.0, num5 + 4.0, num6 + 4.0);
                    IAffineTransformation2D transformationd = new AffineTransformation2DClass();
                    transformationd.DefineFromEnvelopes(from, to);
                    (frame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
                    to = new EnvelopeClass();
                    to.PutCoords(xMin, yMin, xMax, yMax);
                    (frame.Map as IActiveView).Extent = to;
                    frame.MapBounds = to;
                    frame.ExtentType = esriExtentTypeEnum.esriExtentBounds;
                    if (num5 < 0.0)
                    {
                        num5 = -num5;
                    }
                    ipageLayout_0.Page.PutCustomSize(num5 + 10.0, num6 + 10.0);
                    JLKTransformation transformation = new JLKTransformation(ipageLayout_0 as IActiveView);
                    IGroupElement element = this.method_0(ipageLayout_0, list[3], list[0], list[1], list[2]);
                    IElement element2 = null;
                    try
                    {
                        element2 = this.method_16(ipageLayout_0 as IActiveView, string_5,
                            transformation.ToPageLayoutPoint(list[3]), transformation.ToPageLayoutPoint(list[0]),
                            transformation.ToPageLayoutPoint(list[1]), transformation.ToPageLayoutPoint(list[2]));
                    }
                    catch (Exception)
                    {
                    }
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
                    (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        public void CreateTK(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            if (!this.bool_1)
            {
                this.double_2 *= this.Scale/100.0;
                this.double_3 *= this.Scale/100.0;
                this.bool_1 = true;
            }
            JLKTransformation transformation = new JLKTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            if (this.isymbol_0 == null)
            {
                element2 = this.method_22(transformation, ipoint_0, ipoint_1);
            }
            else if (this.isymbol_0 is IMarkerSymbol)
            {
                element2 = this.method_22(transformation, ipoint_0, ipoint_1);
            }
            else
            {
                element2 = this.method_25(transformation, ipoint_0, ipoint_1);
            }
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.method_9(point, point2, this.method_3());
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            try
            {
                element2 = this.method_28(transformation, ipoint_0, ipoint_1);
                if (element2 != null)
                {
                    element.AddElement(element2);
                }
            }
            catch
            {
            }
            element2 = this.method_8(point, point2);
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

        public void CreateTK(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2,
            IPoint ipoint_3)
        {
            int num7;
            IElement element3;
            double xMin = (ipoint_0.X < ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double xMax = (ipoint_2.X > ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double yMin = (ipoint_0.Y < ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double yMax = (ipoint_1.Y > ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            double num5 = ((xMax - xMin)/this.double_4)*100.0;
            double num6 = ((yMax - yMin)/this.double_4)*100.0;
            IGraphicsContainer graphicsContainer = (ipageLayout_0 as IActiveView).GraphicsContainer;
            IMapFrame frame = graphicsContainer.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
            ipageLayout_0.Page.PutCustomSize(num5 + 15.0, num6 + 15.0);
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(5.0, 5.0, num5 + 5.0, num6 + 5.0);
            (frame as IElement).Geometry = envelope;
            graphicsContainer.UpdateElement(frame as IElement);
            IEnvelope envelope2 = new EnvelopeClass();
            envelope2.PutCoords(xMin, yMin, xMax, yMax);
            if (envelope2 != null)
            {
                (frame.Map as IActiveView).Extent = envelope2;
                frame.MapBounds = envelope2;
                frame.ExtentType = esriExtentTypeEnum.esriExtentBounds;
            }
            JLKTransformation transformation = new JLKTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IPoint point3 = transformation.ToPageLayoutPoint(ipoint_2);
            IPoint point4 = transformation.ToPageLayoutPoint(ipoint_3);
            if (!this.bool_1)
            {
                this.double_2 *= this.Scale/100.0;
                this.double_3 *= this.Scale/100.0;
                this.bool_1 = true;
            }
            IGroupElement group = new GroupElementClass();
            IElement element = null;
            element = this.method_24(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            List<IElement> list = new List<IElement>();
            if (element != null)
            {
                if (element is IGroupElement)
                {
                    for (num7 = 0; num7 < (element as IGroupElement).ElementCount; num7++)
                    {
                        element3 = (element as IGroupElement).get_Element(num7);
                        (ipageLayout_0 as IGraphicsContainer).AddElement(element3, -1);
                        list.Add(element3);
                    }
                }
                else
                {
                    (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
                    list.Add(element);
                }
            }
            element = this.method_12(point, point2, point3, point4, this.method_3());
            if (element != null)
            {
                if (element is IGroupElement)
                {
                    for (num7 = 0; num7 < (element as IGroupElement).ElementCount; num7++)
                    {
                        element3 = (element as IGroupElement).get_Element(num7);
                        (ipageLayout_0 as IGraphicsContainer).AddElement(element3, -1);
                        list.Add(element3);
                    }
                }
                else
                {
                    (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
                    list.Add(element);
                }
            }
            try
            {
                element = this.method_31(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
                if (element != null)
                {
                    if (element is IGroupElement)
                    {
                        num7 = 0;
                        while (num7 < (element as IGroupElement).ElementCount)
                        {
                            element3 = (element as IGroupElement).get_Element(num7);
                            (ipageLayout_0 as IGraphicsContainer).AddElement(element3, -1);
                            list.Add(element3);
                            num7++;
                        }
                    }
                    else
                    {
                        (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
                        list.Add(element);
                    }
                }
            }
            catch
            {
            }
            element = this.CreateCornerShortLine(point, point2, point3, point4);
            if (element != null)
            {
                if (element is IGroupElement)
                {
                    for (num7 = 0; num7 < (element as IGroupElement).ElementCount; num7++)
                    {
                        element3 = (element as IGroupElement).get_Element(num7);
                        (ipageLayout_0 as IGraphicsContainer).AddElement(element3, -1);
                        list.Add(element3);
                    }
                }
                else
                {
                    (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
                    list.Add(element);
                }
            }
            element = this.method_7(point, point2, point3, point4);
            if (element != null)
            {
                if (element is IGroupElement)
                {
                    for (num7 = 0; num7 < (element as IGroupElement).ElementCount; num7++)
                    {
                        element3 = (element as IGroupElement).get_Element(num7);
                        (ipageLayout_0 as IGraphicsContainer).AddElement(element3, -1);
                        list.Add(element3);
                    }
                }
                else
                {
                    (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
                    list.Add(element);
                }
            }
            try
            {
                (group as IElementProperties).Type = "图框";
                for (num7 = 0; num7 < list.Count; num7++)
                {
                    (ipageLayout_0 as IGraphicsContainer).MoveElementToGroup(list[num7], group);
                }
                (ipageLayout_0 as IGraphicsContainer).AddElement(group as IElement, -1);
                if (!this.bool_0)
                {
                }
            }
            catch
            {
            }
        }

        public void CreateTK(IPageLayout ipageLayout_0, double double_11, double double_12, double double_13,
            double double_14, double double_15)
        {
            IMapFrame frame = CommandFunction.FindFocusMapFrame(ipageLayout_0);
            if (frame != null)
            {
                THTools tools = new THTools();
                if ((frame.Map.SpatialReference != null) && (frame.Map.SpatialReference is IProjectedCoordinateSystem))
                {
                    IList<IPoint> list = tools.GetProjectCoord(double_11, double_12, double_13, double_14,
                        frame.Map.SpatialReference as IProjectedCoordinateSystem);
                    this.Scale = double_15;
                    if (!this.bool_1)
                    {
                        this.double_2 *= this.Scale/100.0;
                        this.double_3 *= this.Scale/100.0;
                        this.bool_1 = true;
                    }
                    double xMin = (list[3].X < list[0].X) ? list[3].X : list[0].X;
                    double xMax = (list[1].X > list[2].X) ? list[1].X : list[2].X;
                    double yMin = (list[3].Y < list[2].Y) ? list[3].Y : list[2].Y;
                    double yMax = (list[1].Y > list[0].Y) ? list[1].Y : list[0].Y;
                    double num5 = ((xMax - xMin)/this.double_4)*100.0;
                    double num6 = ((yMax - yMin)/this.double_4)*100.0;
                    IEnvelope from = (frame as IElement).Geometry.Envelope;
                    IEnvelope to = new EnvelopeClass();
                    to.PutCoords(4.0, 4.0, num5 + 4.0, num6 + 4.0);
                    IAffineTransformation2D transformationd = new AffineTransformation2DClass();
                    transformationd.DefineFromEnvelopes(from, to);
                    (frame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
                    to = new EnvelopeClass();
                    to.PutCoords(xMin, yMin, xMax, yMax);
                    (frame.Map as IActiveView).Extent = to;
                    ipageLayout_0.Page.PutCustomSize(num5 + 10.0, num6 + 10.0);
                    JLKTransformation transformation = new JLKTransformation(ipageLayout_0 as IActiveView);
                    IGroupElement element = this.method_0(ipageLayout_0, list[3], list[0], list[1], list[2]);
                    IElement element2 = null;
                    try
                    {
                        element2 = this.method_17(ipageLayout_0 as IActiveView, double_11, double_12, double_13,
                            double_14, transformation.ToPageLayoutPoint(list[3]),
                            transformation.ToPageLayoutPoint(list[0]), transformation.ToPageLayoutPoint(list[1]),
                            transformation.ToPageLayoutPoint(list[2]));
                    }
                    catch (Exception)
                    {
                    }
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
                    (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
        }

        public void DrawBackFrame(IPoint ipoint_0, IPoint ipoint_1)
        {
        }

        public void DrawLegend(IPoint ipoint_0, IPoint ipoint_1)
        {
        }

        protected ITextSymbol FontStyle(double double_11, esriTextHorizontalAlignment esriTextHorizontalAlignment_0,
            esriTextVerticalAlignment esriTextVerticalAlignment_0)
        {
            ITextSymbol symbol = new TextSymbolClass();
            IFontDisp font = symbol.Font;
            font.Name = this.string_4;
            symbol.Font = font;
            IRgbColor color = new RgbColorClass
            {
                Blue = 0,
                Red = 0,
                Green = 0
            };
            symbol.Size = double_11;
            symbol.Color = color;
            symbol.HorizontalAlignment = esriTextHorizontalAlignment_0;
            symbol.VerticalAlignment = esriTextVerticalAlignment_0;
            return symbol;
        }

        private IGroupElement method_0(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2,
            IPoint ipoint_3)
        {
            int num;
            JLKTransformation transformation = new JLKTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IPoint point3 = transformation.ToPageLayoutPoint(ipoint_2);
            IPoint point4 = transformation.ToPageLayoutPoint(ipoint_3);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            if (this.isymbol_0 == null)
            {
                element2 = this.method_24(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            }
            else if (this.isymbol_0 is IMarkerSymbol)
            {
                element2 = this.method_24(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            }
            else
            {
                element2 = this.method_27(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            }
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
            element2 = this.method_12(point, point2, point3, point4, this.method_3());
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
            element2 = this.method_7(point, point2, point3, point4);
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

        private ILineSymbol method_1()
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = 1.0;
            return symbol2;
        }

        private IElement method_10(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_2)
        {
            IElement element = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            double num = this.double_0 + (this.double_5/2.0);
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - num, ipoint_0.Y - num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - num, ipoint_1.Y + num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + num, ipoint_1.Y + num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + num, ipoint_0.Y - num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - num, ipoint_0.Y - num);
                points.AddPoint(inPoint, ref missing, ref missing);
                element.Geometry = polyline;
                properties = element as IElementProperties2;
                properties.Type = "外框";
                symbol = isymbol_2 as ILineSymbol;
                symbol.Width = this.double_5/0.0352777778;
                element2 = element as ILineElement;
                element2.Symbol = symbol;
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_11(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_2)
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
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5,
                    (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5,
                    (ipoint_1.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.double_0) + this.double_5,
                    (ipoint_1.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.double_0) + this.double_5,
                    (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5,
                    (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(geometry2, ref missing, ref missing);
                element.Geometry = geometrys as IGeometry;
                element2.Symbol = isymbol_2 as IFillSymbol;
                properties = element as IElementProperties2;
                properties.Type = "外框";
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_12(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, ISymbol isymbol_2)
        {
            if (isymbol_2 is ILineSymbol)
            {
                return this.method_13(ipoint_0, ipoint_1, ipoint_2, ipoint_3, isymbol_2 as ILineSymbol);
            }
            return this.method_14(ipoint_0, ipoint_1, ipoint_2, ipoint_3, isymbol_2 as IFillSymbol);
        }

        private IElement method_13(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3,
            ILineSymbol ilineSymbol_0)
        {
            IElement element = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            ilineSymbol_0.Width = this.double_5/0.0352777778;
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

        private IElement method_14(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3,
            IFillSymbol ifillSymbol_0)
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
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5,
                    (ipoint_0.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X - this.double_0) - this.double_5,
                    (ipoint_1.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_2.X + this.double_0) + this.double_5,
                    (ipoint_2.Y + this.double_0) + this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_3.X + this.double_0) + this.double_5,
                    (ipoint_3.Y - this.double_0) - this.double_5);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.double_0) - this.double_5,
                    (ipoint_0.Y - this.double_0) - this.double_5);
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

        private IElement method_15(IActiveView iactiveView_0, string string_5, IPoint ipoint_0, IPoint ipoint_1)
        {
            if (string_5 != "")
            {
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                bool flag = false;
                THTools tools = new THTools();
                if (string_5.Contains("-"))
                {
                    flag = tools.FileName2BL_cqtx(string_5, ref num2, ref num, ref num3, ref num4);
                }
                else
                {
                    flag = tools.FileName2BL_tx(string_5, out num2, out num, out num3, out num4);
                }
                if (flag)
                {
                    return this.method_18(iactiveView_0, num, num2, num3, num4, ipoint_0, ipoint_1);
                }
            }
            return null;
        }

        private IElement method_16(IActiveView iactiveView_0, string string_5, IPoint ipoint_0, IPoint ipoint_1,
            IPoint ipoint_2, IPoint ipoint_3)
        {
            if (string_5 != "")
            {
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                bool flag = false;
                THTools tools = new THTools();
                if (string_5.Contains("-"))
                {
                    flag = tools.FileName2BL_cqtx(string_5, ref num2, ref num, ref num3, ref num4);
                }
                else
                {
                    flag = tools.FileName2BL(string_5, out num2, out num, out num3, out num4);
                }
                if (flag)
                {
                    return this.method_17(iactiveView_0, num, num2, num3, num4, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
                }
            }
            return null;
        }

        private IElement method_17(IActiveView iactiveView_0, double double_11, double double_12, double double_13,
            double double_14, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
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
            symbol = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            symbol2 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter,
                esriTextVerticalAlignment.esriTVABottom);
            symbol4 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            THTools.DEG2DDDMMSS(double_11, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_6(iactiveView_0, element3, out num4, out num5);
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
            double_11 += double_13;
            THTools.DEG2DDDMMSS(double_11, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_6(iactiveView_0, element3, out num4, out num5);
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
            THTools.DEG2DDDMMSS(double_12, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_0.X - (this.double_0/2.0), ipoint_0.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(ipoint_0.X - ((this.double_0*9.0)/10.0), ipoint_0.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_1.X - (this.double_0/2.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_1.X - ((this.double_0*9.0)/10.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            double_12 += double_14;
            THTools.DEG2DDDMMSS(double_12, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_3.X + (this.double_0/2.0), ipoint_3.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(ipoint_3.X + ((this.double_0*1.0)/10.0), ipoint_3.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_2.X + (this.double_0/2.0), ipoint_2.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_2.X + ((this.double_0*1.0)/10.0), ipoint_2.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            return (element as IElement);
        }

        private IElement method_18(IActiveView iactiveView_0, double double_11, double double_12, double double_13,
            double double_14, IPoint ipoint_0, IPoint ipoint_1)
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
            symbol = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            symbol2 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter,
                esriTextVerticalAlignment.esriTVABottom);
            symbol4 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            x = ipoint_0.X;
            y = ipoint_0.Y;
            THTools.DEG2DDDMMSS(double_11, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_6(iactiveView_0, element3, out num6, out num7);
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
            double_11 += double_13;
            THTools.DEG2DDDMMSS(double_11, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol;
            point.PutCoords(10.0, 10.0);
            element2.Geometry = point;
            this.method_6(iactiveView_0, element3, out num6, out num7);
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
            THTools.DEG2DDDMMSS(double_12, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(x - (this.double_0/2.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(x - ((this.double_0*9.0)/10.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(x - (this.double_0/2.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_0.X - ((this.double_0*9.0)/10.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            double_12 += double_14;
            THTools.DEG2DDDMMSS(double_12, ref num, ref num2, ref num3);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_1.X + (this.double_0/2.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Symbol = symbol4;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            point.PutCoords(ipoint_1.X + ((this.double_0*1.0)/10.0), y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num.ToString() + str;
            element3.Symbol = symbol3;
            point.PutCoords(ipoint_1.X + (this.double_0/2.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            element2 = new TextElementClass();
            element3 = element2 as ITextElement;
            element3.Text = num2.ToString() + str2 + num3.ToString() + str3;
            element3.Symbol = symbol4;
            point.PutCoords(ipoint_1.X + ((this.double_0*1.0)/10.0), ipoint_1.Y);
            element2.Geometry = point;
            element.AddElement(element2);
            return (element as IElement);
        }

        private IElement method_19(double double_11, double double_12, double double_13, double double_14,
            ILineSymbol ilineSymbol_0)
        {
            object missing = Type.Missing;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(double_11 + double_13, double_12);
            points.AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(double_11, double_12);
            points.AddPoint(inPoint, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(double_11, double_12 + double_14);
            points.AddPoint(inPoint, ref missing, ref missing);
            IElement element = new LineElementClass();
            ILineElement element2 = element as ILineElement;
            element2.Symbol = ilineSymbol_0;
            element.Geometry = polyline;
            return element;
        }

        private ILineSymbol method_2(int int_0)
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = int_0;
            return symbol2;
        }

        private IPoint method_20(IPoint ipoint_0)
        {
            double x = ipoint_0.X;
            if (this.double_6 > 0.0)
            {
                x = Math.Truncate((double) (x/this.double_6))*this.double_6;
                x += this.double_6;
            }
            double y = ipoint_0.Y;
            if (this.double_6 > 0.0)
            {
                y = Math.Truncate((double) (ipoint_0.Y/this.double_6))*this.double_6;
                y += this.double_6;
            }
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return point;
        }

        private IPoint method_21(double double_11, double double_12)
        {
            double x = double_11;
            if (this.double_6 > 0.0)
            {
                x = Math.Truncate((double) (double_11/this.double_6))*this.double_6;
                if (x != double_11)
                {
                    x += this.double_6;
                }
            }
            double y = double_12;
            if (this.double_6 > 0.0)
            {
                y = Math.Truncate((double) (double_12/this.double_6))*this.double_6;
                if (y != double_12)
                {
                    y += this.double_6;
                }
            }
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return point;
        }

        private IElement method_22(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IGroupElement element = new GroupElementClass();
            IPoint point = ipoint_0;
            IMarkerSymbol symbol = this.isymbol_0 as IMarkerSymbol;
            if (symbol == null)
            {
                symbol = new SimpleMarkerSymbolClass
                {
                    Size = 10.0
                };
                (symbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                symbol.Color = ColorManage.CreatColor(0, 0, 0);
            }
            double x = point.X + this.double_2;
            for (double i = point.Y + this.double_3; x < ipoint_1.X; i = point.Y + this.double_3)
            {
                while (i < ipoint_1.Y)
                {
                    IPoint point2 = new PointClass();
                    point2.PutCoords(x, i);
                    IElement element2 = new MarkerElementClass
                    {
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

        private IElement method_23(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_20(ipoint_0);
            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass
            {
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
                    IElement element2 = new MarkerElementClass
                    {
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

        private IElement method_24(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1,
            IPoint ipoint_2, IPoint ipoint_3)
        {
            IMarkerSymbol symbol;
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_21(num, num2);
            if (this.isymbol_0 is IMarkerSymbol)
            {
                symbol = this.isymbol_0 as IMarkerSymbol;
            }
            else
            {
                symbol = new SimpleMarkerSymbolClass
                {
                    Size = 10.0
                };
                (symbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                symbol.Color = ColorManage.CreatColor(0, 0, 0);
            }
            double x = point.X;
            for (double i = point.Y; x < num3; i = point.Y)
            {
                while (i < num4)
                {
                    IPoint point2 = new PointClass();
                    point2.PutCoords(x, i);
                    IElement element2 = new MarkerElementClass
                    {
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

        private IElement method_25(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IPolyline polyline;
            IPointCollection points;
            IPoint point2;
            IElement element2;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_20(ipoint_0);
            ILineSymbol symbol = this.isymbol_0 as ILineSymbol;
            if (symbol == null)
            {
                symbol = new SimpleLineSymbolClass
                {
                    Color = ColorManage.CreatColor(0, 0, 0)
                };
            }
            object missing = Type.Missing;
            double x = point.X;
            double y = point.Y;
            while (x < ipoint_1.X)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                point2 = new PointClass();
                point2.PutCoords(x, ipoint_0.Y);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(point2), ref missing, ref missing);
                point2.PutCoords(x, ipoint_1.Y);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(point2), ref missing, ref missing);
                element2 = new LineElementClass
                {
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
                element2 = new LineElementClass
                {
                    Geometry = polyline
                };
                (element2 as ILineElement).Symbol = symbol;
                element.AddElement(element2);
                y += this.double_3;
            }
            return (element as IElement);
        }

        private IPointCollection method_26(IPolyline ipolyline_0, IPolygon ipolygon_0)
        {
            IMultipoint multipoint = null;
            ITopologicalOperator @operator = ipolygon_0 as ITopologicalOperator;
            @operator.Simplify();
            multipoint = @operator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
            return (multipoint as IPointCollection);
        }

        private IElement method_27(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1,
            IPoint ipoint_2, IPoint ipoint_3)
        {
            ILineSymbol symbol;
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
            IPoint point = this.method_21(num, num2);
            if (this.isymbol_0 is ILineSymbol)
            {
                symbol = this.isymbol_0 as ILineSymbol;
            }
            else
            {
                symbol = new SimpleLineSymbolClass
                {
                    Color = ColorManage.CreatColor(0, 0, 0)
                };
            }
            double num9 = point.X;
            double num10 = point.Y;
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
                points2 = this.method_26(polyline, polygon);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(0)), ref missing, ref missing);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(1)), ref missing, ref missing);
                element2 = new LineElementClass
                {
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
                points2 = this.method_26(polyline, polygon);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(0)), ref missing, ref missing);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(1)), ref missing, ref missing);
                element2 = new LineElementClass
                {
                    Geometry = polyline
                };
                (element2 as ILineElement).Symbol = symbol;
                element.AddElement(element2);
                num10 += this.double_3;
            }
            return (element as IElement);
        }

        private IElement method_28(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
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
            IPoint point = this.method_20(ipoint_0);
            double x = point.X;
            double y = point.Y;
            string str = "";
            string str2 = "";
            IElement element2 = null;
            this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol = this.FontStyle(this.double_10, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            this.FontStyle(this.double_10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            this.FontStyle(this.double_10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol2 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVATop);
            this.FontStyle(this.double_10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol4 = this.method_1();
            while (true)
            {
                if (x > ipoint_1.X)
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
                    if ((x == point.X) || (x == ipoint_1.X))
                    {
                        point3.PutCoords(point2.X, point2.Y - this.double_0);
                    }
                    else
                    {
                        point3.PutCoords(point2.X, point2.Y + 0.5);
                    }
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol4;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    if ((x == point.X) || (x == ipoint_1.X))
                    {
                        point4.PutCoords(point2.X, point2.Y - this.double_0);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        element5.Symbol = symbol2;
                        element5.Text = x.ToString();
                        element.AddElement(element5 as IElement);
                    }
                    else if (this.bool_2)
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X, point2.Y - this.double_0);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                        element5.Symbol = symbol2;
                        element5.Text = x.ToString();
                        element.AddElement(element5 as IElement);
                    }
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(x, ipoint_1.Y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    if ((x == point.X) || (x == ipoint_1.X))
                    {
                        point3.PutCoords(point2.X, point2.Y + this.double_0);
                    }
                    else
                    {
                        point3.PutCoords(point2.X, point2.Y - 0.5);
                    }
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol4;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    if ((x == point.X) || (x == ipoint_1.X))
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X, point2.Y + this.double_0);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        element5.Symbol = symbol3;
                        element5.Text = x.ToString();
                        element.AddElement(element5 as IElement);
                    }
                    else if (this.bool_2)
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X, point2.Y + this.double_0);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                        element5.Symbol = symbol3;
                        element5.Text = x.ToString();
                        element.AddElement(element5 as IElement);
                    }
                }
                catch
                {
                }
                x += this.double_2;
            }
            while (y <= ipoint_1.Y)
            {
                try
                {
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(ipoint_0.X, y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    if ((y == point.Y) || (y == ipoint_1.Y))
                    {
                        point3.PutCoords(point2.X - this.double_0, point2.Y);
                    }
                    else
                    {
                        point3.PutCoords(point2.X + 0.5, point2.Y);
                    }
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol4;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    this.method_32(y, 100000.0, out str, out str2);
                    element5 = new TextElementClass();
                    point4 = new PointClass();
                    if ((y == point.Y) || (y == ipoint_1.Y))
                    {
                        point4.PutCoords(point2.X - this.double_0, point2.Y);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        element5.Symbol = symbol;
                        element5.Text = y.ToString();
                        element.AddElement(element5 as IElement);
                    }
                    else if (this.bool_2)
                    {
                        point4.PutCoords(point2.X - this.double_0, point2.Y);
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        element5.Symbol = symbol;
                        element5.Text = y.ToString();
                        element.AddElement(element5 as IElement);
                    }
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(ipoint_1.X, y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    if ((y == point.Y) || (y == ipoint_1.Y))
                    {
                        point3.PutCoords(point2.X + this.double_0, point2.Y);
                    }
                    else
                    {
                        point3.PutCoords(point2.X - 0.5, point2.Y);
                    }
                    points.AddPoint(point3, ref missing, ref missing);
                    element3 = new LineElementClass();
                    element4 = element3 as ILineElement;
                    element4.Symbol = symbol4;
                    element3.Geometry = polyline;
                    element.AddElement(element3);
                    if ((y == point.Y) || (y == ipoint_1.Y))
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X + this.double_0, point2.Y);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        element5.Text = y.ToString();
                        symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        element5.Symbol = symbol3;
                        element.AddElement(element5 as IElement);
                    }
                    else if (this.bool_2)
                    {
                        element5 = new TextElementClass();
                        point4 = new PointClass();
                        point4.PutCoords(point2.X + this.double_0, point2.Y);
                        element2 = element5 as IElement;
                        element2.Geometry = point4;
                        symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                        element5.Text = y.ToString();
                        element5.Symbol = symbol3;
                        element.AddElement(element5 as IElement);
                    }
                }
                catch
                {
                }
                y += this.double_3;
            }
            return (element as IElement);
        }

        private IElement method_29(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
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
            IPoint point = this.method_20(ipoint_0);
            double x = point.X;
            double y = point.Y;
            string str = "";
            string str2 = "";
            double num3 = 0.4;
            double num4 = 0.2;
            double num5 = 0.1;
            IElement element2 = null;
            ITextSymbol symbol = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol2 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol4 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol5 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol6 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol7 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol8 = this.method_1();
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

        private ISymbol method_3()
        {
            if (this.isymbol_1 != null)
            {
                return this.isymbol_1;
            }
            ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass();
            ILineSymbol symbol3 = null;
            ISimpleLineSymbol symbol4 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol4.Color = color;
            symbol4.Width = 2.0;
            symbol3 = symbol4;
            color.Red = 100;
            color.Blue = 100;
            color.Green = 100;
            symbol2.Color = color;
            symbol2.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            symbol2.Outline = symbol3;
            return (symbol2 as ISymbol);
        }

        private IPolygon method_30(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, bool bool_3)
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
            if (bool_3)
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

        private IElement method_31(JLKTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1,
            IPoint ipoint_2, IPoint ipoint_3)
        {
            IPolyline polyline;
            IPointCollection points;
            IPoint point2;
            IPoint point3;
            IPointCollection points2;
            IElement element3;
            ILineElement element4;
            ITextElement element5;
            IPoint point4;
            IPolygon polygon = this.method_30(jlktransformation_0.ToPageLayoutPoint(ipoint_0),
                jlktransformation_0.ToPageLayoutPoint(ipoint_1), jlktransformation_0.ToPageLayoutPoint(ipoint_2),
                jlktransformation_0.ToPageLayoutPoint(ipoint_3), true);
            IGroupElement element = new GroupElementClass();
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            double x = (ipoint_0.X < ipoint_1.X) ? (ipoint_0.X - 3.0) : (ipoint_1.X - 3.0);
            double y = (ipoint_0.Y < ipoint_3.Y) ? (ipoint_0.Y - 3.0) : (ipoint_3.Y - 3.0);
            double num7 = (ipoint_2.X > ipoint_3.X) ? (ipoint_2.X + 3.0) : (ipoint_3.X + 3.0);
            double num8 = (ipoint_1.Y > ipoint_2.Y) ? (ipoint_1.Y + 3.0) : (ipoint_2.Y + 3.0);
            IPoint point = this.method_21(num, num2);
            double num9 = point.X;
            double num10 = point.Y;
            string str = "";
            string str2 = "";
            double num11 = 0.4;
            double num12 = 0.2;
            IElement element2 = null;
            ITextSymbol symbol = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol2 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol4 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol5 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol6 = this.FontStyle((double) 10, esriTextHorizontalAlignment.esriTHARight,
                esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol7 = this.FontStyle((double) 13, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol8 = this.method_1();
            bool flag = true;
            try
            {
                while (num9 <= num3)
                {
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    point2 = new PointClass();
                    point2.PutCoords(num9, y);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X, ((point2.Y - this.double_0) - this.double_5) - 10.0);
                    points.AddPoint(point3, ref missing, ref missing);
                    point2 = new PointClass();
                    point2.PutCoords(num9, num8);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X, ((point2.Y + this.double_0) + this.double_5) + 10.0);
                    points.AddPoint(point3, ref missing, ref missing);
                    points2 = this.method_26(polyline, polygon);
                    points.RemovePoints(0, points.PointCount);
                    if (points2.PointCount < 4)
                    {
                        IPointCollection points3 = points2;
                        points2 = new MultipointClass();
                        points2.AddPoint(points3.get_Point(0), ref missing, ref missing);
                        if (num9 == point.X)
                        {
                            point2 = jlktransformation_0.ToPageLayoutPoint(ipoint_1);
                            points2.AddPoint(point2, ref missing, ref missing);
                            point2 = jlktransformation_0.ToPageLayoutPoint(ipoint_0);
                            points2.AddPoint(point2, ref missing, ref missing);
                        }
                        else
                        {
                            point2 = jlktransformation_0.ToPageLayoutPoint(ipoint_2);
                            points2.AddPoint(point2, ref missing, ref missing);
                            point2 = jlktransformation_0.ToPageLayoutPoint(ipoint_3);
                            points2.AddPoint(point2, ref missing, ref missing);
                        }
                        points2.AddPoint(points3.get_Point(1), ref missing, ref missing);
                    }
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
                    flag = false;
                }
            }
            catch
            {
            }
            flag = true;
            while (num10 <= num4)
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
                points2 = this.method_26(polyline, polygon);
                if (points2.PointCount < 4)
                {
                    points2 = new MultipointClass();
                    point2 = new PointClass();
                    point2.PutCoords(x + 3.0, num10);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X - this.double_0, point2.Y);
                    points2.AddPoint(point3, ref missing, ref missing);
                    points2.AddPoint(point2, ref missing, ref missing);
                    point2 = new PointClass();
                    point2.PutCoords(num8 - 3.0, num10);
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    points2.AddPoint(point2, ref missing, ref missing);
                    point3 = new PointClass();
                    point3.PutCoords(point2.X + this.double_0, point2.Y);
                    points2.AddPoint(point3, ref missing, ref missing);
                }
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

        private void method_32(double double_11, double double_12, out string string_5, out string string_6)
        {
            string_5 = "";
            string_6 = "";
            int num = (int) Math.Truncate((double) (double_11/double_12));
            string_5 = num.ToString();
            string_6 = ((int) Math.Truncate((double) (((double_11 - (num*double_12))/double_12)*100.0))).ToString();
            if (string_6.Length < 2)
            {
                string_6 = "0" + string_6;
            }
        }

        private ILineSymbol method_4()
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = 2.0;
            return symbol2;
        }

        private string method_5(string string_5)
        {
            string str = "\n";
            int startIndex = 0;
            string str2 = "";
            for (startIndex = 0; startIndex < string_5.Trim().Length; startIndex++)
            {
                str2 = str2 + string_5.Substring(startIndex, 1) + str;
            }
            return str2;
        }

        private void method_6(IActiveView iactiveView_0, ITextElement itextElement_0, out double double_11,
            out double double_12)
        {
            double_11 = 0.0;
            double_12 = 0.0;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                (itextElement_0 as IElement).QueryBounds(iactiveView_0.ScreenDisplay, bounds);
                double_11 = bounds.Width;
                double_12 = bounds.Height;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private IElement method_7(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
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
            symbol = this.method_2(1);
            element2 = element as ILineElement;
            element2.Symbol = symbol;
            return element;
        }

        private IElement method_8(IPoint ipoint_0, IPoint ipoint_1)
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
            symbol = this.method_2(1);
            element2 = element as ILineElement;
            element2.Symbol = symbol;
            return element;
        }

        private IElement method_9(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_2)
        {
            if (isymbol_2 is ILineSymbol)
            {
                return this.method_10(ipoint_0, ipoint_1, isymbol_2);
            }
            return this.method_11(ipoint_0, ipoint_1, isymbol_2);
        }

        public double BigFontSize
        {
            get { return this.double_9; }
            set { this.double_9 = value; }
        }

        public string FontName
        {
            get { return this.string_4; }
            set { this.string_4 = value; }
        }

        public ISymbol GridSymbol
        {
            get { return this.isymbol_0; }
            set { this.isymbol_0 = value; }
        }

        public bool HasLegend
        {
            get { return this.bool_0; }
            set { this.bool_0 = value; }
        }

        public double InOutDist
        {
            get { return this.double_0; }
            set { this.double_0 = value; }
        }

        public string LegendTemplate
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public string MapTH
        {
            get { return this.string_2; }
            set { this.string_2 = value; }
        }

        public string MapTM
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public double OutBorderWidth
        {
            get { return this.double_5; }
            set { this.double_5 = value; }
        }

        public ISymbol OutSideStyle
        {
            get { return this.isymbol_1; }
            set { this.isymbol_1 = value; }
        }

        public double Scale
        {
            get { return this.double_4; }
            set
            {
                this.double_4 = value;
                this.string_3 = "1:" + value.ToString();
                if (value == 0.0)
                {
                    this.double_4 = 1.0;
                }
            }
        }

        public double SmallFontSize
        {
            get { return this.double_10; }
            set { this.double_10 = value; }
        }

        public SpheroidType SpheroidType
        {
            get { return this.spheroidType_0; }
            set { this.spheroidType_0 = value; }
        }

        public double StartCoodinateMultiple
        {
            get { return this.double_6; }
            set { this.double_6 = value; }
        }

        public double StartX
        {
            get { return this.double_7; }
            set { this.double_7 = value; }
        }

        public double StartY
        {
            get { return this.double_8; }
            set { this.double_8 = value; }
        }

        public StripType StripType
        {
            get { return this.stripType_0; }
            set { this.stripType_0 = value; }
        }

        public double TitleDist
        {
            get { return this.double_1; }
            set { this.double_1 = value; }
        }

        public TKType TKType
        {
            get { return this.tktype_0; }
            set { this.tktype_0 = value; }
        }

        public double XInterval
        {
            get { return this.double_2; }
            set { this.double_2 = value; }
        }

        public double YInterval
        {
            get { return this.double_3; }
            set { this.double_3 = value; }
        }
    }
}