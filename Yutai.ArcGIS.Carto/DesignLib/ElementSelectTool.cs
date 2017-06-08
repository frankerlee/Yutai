using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElementSelectTool : BaseTool, IToolContextMenu
    {
        private bool bool_0 = false;
        private Enum0 enum0_0;
        private ICalloutFeedback icalloutFeedback_0 = new CalloutFeedbackClass();
        private IElement ielement_0;
        private IElement ielement_1;
        private IGeometry igeometry_0;
        private IYTHookHelper _hookHelper;
        private IMoveImageFeedback imoveImageFeedback_0 = new MoveImageFeedbackClass();
        private INewEnvelopeFeedback inewEnvelopeFeedback_0 = new NewEnvelopeFeedbackClass();
        private IPoint ipoint_0 = new PointClass();
        private IPoint ipoint_1 = new PointClass();
        private IPoint ipoint_2 = new PointClass();
        private IPoint ipoint_3 = new PointClass();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private IResizeEnvelopeFeedback2 iresizeEnvelopeFeedback2_0 = new ResizeEnvelopeFeedbackClass();
        private object object_0;

        public ElementSelectTool()
        {
            base.m_cursor = Cursors.Default;
            base.m_bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.ElementSelect.bmp"));
            base.m_name = "ElementSelectTool";
            base.m_caption = "选择元素";
        }

        public void Init()
        {
            this.ipopuMenuWrap_0.Clear();
            string[] strArray2 = new string[] { "DeleteElement", "-", "ElmentProperty" };
            bool flag = false;
            for (int i = 0; i < strArray2.Length; i++)
            {
                if ((i + 1) != strArray2.Length)
                {
                    flag = string.Compare(strArray2[i + 1], "-") == 0;
                }
                else
                {
                    flag = false;
                }
                this.ipopuMenuWrap_0.AddItem(strArray2[i], flag);
            }
        }

        private void method_0(JLK.ExtendClass.IOleFrame ioleFrame_0)
        {
            ElementChangeEvent.EditElementProperty(ioleFrame_0 as IElement);
            this._hookHelper.ActiveView.Refresh();
        }

        private int method_1(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            int num = -1;
            try
            {
                if (igraphicsContainerSelect_0 != null)
                {
                    if (this.ipoint_0 == null)
                    {
                        return num;
                    }
                    if (this.ipoint_0.IsEmpty)
                    {
                        return num;
                    }
                    int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                    num = 0;
                    for (int i = 0; i <= (elementSelectionCount - 1); i++)
                    {
                        IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                        if (!element.Locked)
                        {
                            this.method_39(element, elementSelectionCount);
                            ISelectionTracker selectionTracker = element.SelectionTracker;
                            if (selectionTracker != null)
                            {
                                int num5 = selectionTracker.QueryCursor(this.ipoint_0);
                                if (num5 != 0)
                                {
                                    return num5;
                                }
                            }
                        }
                    }
                }
                return num;
            }
            catch
            {
            }
            return num;
        }

        private bool method_10(IGraphicsContainerSelect igraphicsContainerSelect_0, int int_0, int int_1)
        {
            try
            {
                IElement element;
                esriTrackerLocation location3;
                IPoint point = this.method_2(int_0, int_1);
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                for (int i = 0; i <= (elementSelectionCount - 1); i++)
                {
                    element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            IDisplayFeedback resizeFeedback = new ResizeEnvelopeFeedbackClass();
                            selectionTracker.QueryResizeFeedback(ref resizeFeedback);
                            if ((resizeFeedback != null) && (resizeFeedback is IResizeEnvelopeFeedback))
                            {
                                esriEnvelopeConstraints esriEnvelopeConstraintsAspect;
                                esriTrackerLocation location = selectionTracker.HitTest(point);
                                IResizeEnvelopeFeedback feedback2 = resizeFeedback as IResizeEnvelopeFeedback;
                                switch (location)
                                {
                                    case esriTrackerLocation.LocationTopLeft:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                                        break;

                                    case esriTrackerLocation.LocationTopMiddle:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopMiddle;
                                        break;

                                    case esriTrackerLocation.LocationTopRight:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopRight;
                                        break;

                                    case esriTrackerLocation.LocationMiddleLeft:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleLeft;
                                        break;

                                    case esriTrackerLocation.LocationMiddleRight:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleRight;
                                        break;

                                    case esriTrackerLocation.LocationBottomLeft:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomLeft;
                                        break;

                                    case esriTrackerLocation.LocationBottomMiddle:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomMiddle;
                                        break;

                                    case esriTrackerLocation.LocationBottomRight:
                                        feedback2.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomRight;
                                        break;
                                }
                                IBoundsProperties properties = element as IBoundsProperties;
                                if (properties.FixedAspectRatio)
                                {
                                    esriEnvelopeConstraintsAspect = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                                }
                                else
                                {
                                    esriEnvelopeConstraintsAspect = feedback2.Constraint;
                                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraintsAspect;
                                }
                                if (esriEnvelopeConstraintsAspect == esriEnvelopeConstraints.esriEnvelopeConstraintsAspect)
                                {
                                    this.iresizeEnvelopeFeedback2_0.AspectRatio = feedback2.AspectRatio;
                                }
                            }
                            location3 = selectionTracker.HitTest(point);
                            if ((location3 != esriTrackerLocation.LocationInterior) && (location3 != esriTrackerLocation.LocationNone))
                            {
                                goto Label_017D;
                            }
                        }
                    }
                }
                goto Label_018F;
            Label_017D:
                this.method_12(element, location3, point);
                return true;
            }
            catch
            {
            }
        Label_018F:
            return false;
        }

        private void method_11(esriTrackerLocation esriTrackerLocation_0)
        {
            switch (esriTrackerLocation_0)
            {
                case esriTrackerLocation.LocationTopLeft:
                case esriTrackerLocation.LocationBottomRight:
                    base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trcknwse.cur"));
                    break;

                case esriTrackerLocation.LocationTopMiddle:
                case esriTrackerLocation.LocationBottomMiddle:
                    base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trckns.cur"));
                    break;

                case esriTrackerLocation.LocationTopRight:
                case esriTrackerLocation.LocationBottomLeft:
                    base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trcknesw.cur"));
                    break;

                case esriTrackerLocation.LocationMiddleLeft:
                case esriTrackerLocation.LocationMiddleRight:
                    base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trckwe.cur"));
                    break;

                default:
                    base.m_cursor = Cursors.Default;
                    break;
            }
        }

        private void method_12(IElement ielement_2, esriTrackerLocation esriTrackerLocation_0, IPoint ipoint_4)
        {
            try
            {
                IGeometry geometry;
                bool fixedSize = false;
                if (ielement_2 is IBoundsProperties)
                {
                    IBoundsProperties properties = ielement_2 as IBoundsProperties;
                    fixedSize = properties.FixedSize;
                }
                if (fixedSize)
                {
                    IEnvelope bounds = new EnvelopeClass();
                    ielement_2.QueryBounds(this._hookHelper.ActiveView.ScreenDisplay, bounds);
                    geometry = bounds;
                }
                else
                {
                    geometry = ielement_2.Geometry;
                }
                this.iresizeEnvelopeFeedback2_0.Display = this._hookHelper.ActiveView.ScreenDisplay;
                esriEnvelopeEdge edge = this.method_13(esriTrackerLocation_0);
                this.iresizeEnvelopeFeedback2_0.ResizeEdge = edge;
                this.enum0_0 = Enum0.eResizing;
                this.ielement_0 = ielement_2;
                this.iresizeEnvelopeFeedback2_0.Start(geometry, this.ipoint_0);
            }
            catch
            {
            }
        }

        private esriEnvelopeEdge method_13(esriTrackerLocation esriTrackerLocation_0)
        {
            esriEnvelopeEdge esriEnvelopeEdgeTopLeft = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
            try
            {
                switch (esriTrackerLocation_0)
                {
                    case esriTrackerLocation.LocationTopLeft:
                        return esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;

                    case esriTrackerLocation.LocationTopMiddle:
                        return esriEnvelopeEdge.esriEnvelopeEdgeTopMiddle;

                    case esriTrackerLocation.LocationTopRight:
                        return esriEnvelopeEdge.esriEnvelopeEdgeTopRight;

                    case esriTrackerLocation.LocationMiddleLeft:
                        return esriEnvelopeEdge.esriEnvelopeEdgeMiddleLeft;

                    case esriTrackerLocation.LocationMiddleRight:
                        return esriEnvelopeEdge.esriEnvelopeEdgeMiddleRight;

                    case esriTrackerLocation.LocationBottomLeft:
                        return esriEnvelopeEdge.esriEnvelopeEdgeBottomLeft;

                    case esriTrackerLocation.LocationBottomMiddle:
                        return esriEnvelopeEdge.esriEnvelopeEdgeBottomMiddle;

                    case esriTrackerLocation.LocationBottomRight:
                        return esriEnvelopeEdge.esriEnvelopeEdgeBottomRight;
                }
                return esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
            }
            catch
            {
            }
            return esriEnvelopeEdgeTopLeft;
        }

        private IGeometry method_14(IPoint ipoint_4)
        {
            try
            {
                double num2;
                double num3;
                double num = this.method_15();
                IEnvelope envelope = new EnvelopeClass();
                ipoint_4.QueryCoords(out num2, out num3);
                envelope.PutCoords(num2 - num, num3 - num, num2 + num, num3 + num);
                return envelope;
            }
            catch
            {
            }
            return null;
        }

        private double method_15()
        {
            try
            {
                IDisplayTransformation displayTransformation = this._hookHelper.ActiveView.ScreenDisplay.DisplayTransformation;
                tagPOINT devPoints = new tagPOINT();
                WKSPoint mapPoints = new WKSPoint();
                devPoints.x = 3;
                devPoints.y = 3;
                displayTransformation.TransformCoords(ref mapPoints, ref devPoints, 1, 6);
                return mapPoints.X;
            }
            catch
            {
            }
            return 0.0;
        }

        private void method_16(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IActiveView activeView = this._hookHelper.ActiveView;
                if (igraphicsContainerSelect_0.ElementSelectionCount == 1)
                {
                    ICallout callout;
                    IElement element = igraphicsContainerSelect_0.SelectedElement(0);
                    if (this.method_30(element, out callout))
                    {
                        this.method_31(element, null);
                    }
                }
            }
            catch
            {
            }
        }

        private void method_17(IGraphicsContainerSelect igraphicsContainerSelect_0, IMoveImageFeedback imoveImageFeedback_1, IGeometry igeometry_1)
        {
            try
            {
                IDisplay display = imoveImageFeedback_1.Display;
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                IPolygon outline = new PolygonClass();
                IPolygon polygon2 = new PolygonClass();
                if (elementSelectionCount == 1)
                {
                    igraphicsContainerSelect_0.SelectedElement(0).QueryOutline(display, outline);
                }
                else
                {
                    for (int j = elementSelectionCount - 1; j >= 0; j--)
                    {
                        igraphicsContainerSelect_0.SelectedElement(j).QueryOutline(display, polygon2);
                        ITopologicalOperator @operator = polygon2 as ITopologicalOperator;
                        @operator.Simplify();
                        IGeometry geometry = (outline as ITopologicalOperator).Union(polygon2);
                        if (geometry != null)
                        {
                            outline = geometry as IPolygon;
                        }
                    }
                }
                IClone clone = outline as IClone;
                igeometry_1 = clone.Clone() as IGeometry;
                for (int i = elementSelectionCount - 1; i >= 0; i--)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (element is IMapFrame)
                    {
                        IMapFrame frame = element as IMapFrame;
                        this.method_34(frame, display);
                    }
                    else
                    {
                        element.Draw(display, null);
                    }
                }
                IMoveImageFeedback2 feedback = imoveImageFeedback_1 as IMoveImageFeedback2;
                feedback.PolygonBounds = outline;
            }
            catch
            {
            }
        }

        private bool method_18(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            bool flag = false;
            try
            {
                flag = false;
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                ISymbol symbol = null;
                IGeometry geometry = null;
                if (igraphicsContainerSelect_0.ElementSelectionCount == 1)
                {
                    IPoint point;
                    IElement element = this.method_19(this.ipoint_0);
                    if ((element != null) && this.method_20(element, out point))
                    {
                        this.icalloutFeedback_0.Display = activeView.ScreenDisplay;
                        if (this.method_28(element, symbol, geometry))
                        {
                            this.icalloutFeedback_0.Start(symbol, geometry, this.ipoint_0);
                            this.icalloutFeedback_0.MoveTo(this.ipoint_0);
                            this.ielement_1 = element;
                            flag = true;
                        }
                    }
                }
                return flag;
            }
            catch
            {
            }
            return flag;
        }

        private IElement method_19(IPoint ipoint_4)
        {
            IElement element = null;
            try
            {
                double tolerance = this.method_15();
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainerSelect select = activeView as IGraphicsContainerSelect;
                ISpatialReference spatialReference = null;
                if (activeView is IMap)
                {
                    IMap map = activeView as IMap;
                    spatialReference = map.SpatialReference;
                }
                if (spatialReference != null)
                {
                    spatialReference = ipoint_4.SpatialReference;
                }
                IEnumElement selectedElements = select.SelectedElements;
                if (selectedElements != null)
                {
                    for (IElement element4 = selectedElements.Next(); element4 != null; element4 = selectedElements.Next())
                    {
                        if (element4.HitTest(ipoint_4.X, ipoint_4.Y, tolerance))
                        {
                            return element4;
                        }
                    }
                }
                return element;
            }
            catch
            {
            }
            return element;
        }

        private IPoint method_2(int int_0, int int_1)
        {
            try
            {
                return this._hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_0, int_1);
            }
            catch
            {
            }
            return null;
        }

        private bool method_20(IElement ielement_2, out IPoint ipoint_4)
        {
            ipoint_4 = null;
            try
            {
                ICallout callout = null;
                if (this.method_30(ielement_2, out callout))
                {
                    ipoint_4 = callout.AnchorPoint;
                    if (ipoint_4 != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
            }
            return false;
        }

        private void method_21(int int_0)
        {
            try
            {
                if ((this.ipoint_1.X == this.ipoint_0.X) && (this.ipoint_1.Y == this.ipoint_0.Y))
                {
                    if ((int_0 == 1) || (int_0 == 2))
                    {
                        this.method_33(int_0);
                    }
                    else
                    {
                        this.method_35();
                    }
                }
                else if (int_0 != 2)
                {
                    this.method_22(this.ipoint_0, this.ipoint_1);
                }
            }
            catch
            {
            }
        }

        private void method_22(IPoint ipoint_4, IPoint ipoint_5)
        {
            IMoveElementOperation operation = new MoveElementOperation();
            double x = ipoint_4.X - ipoint_5.X;
            double y = ipoint_4.Y - ipoint_5.Y;
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            operation.Point = point;
            IActiveView activeView = this._hookHelper.ActiveView;
            operation.ActiveView = activeView;
            operation.Elements = this.method_8().SelectedElements;
            this._hookHelper.OperationStack.Do(operation);
        }

        private void method_23(int int_0)
        {
            try
            {
                int x;
                int num2;
                int y;
                int num4;
                IEnvelope envelope = this.inewEnvelopeFeedback_0.Stop();
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                ISpatialReference spatialReference = null;
                if (activeView is IMap)
                {
                    IMap map = activeView as IMap;
                    spatialReference = map.SpatialReference;
                }
                if (spatialReference != null)
                {
                    this.ipoint_0.SpatialReference = spatialReference;
                    envelope.SpatialReference = spatialReference;
                }
                IEnumElement element = null;
                bool flag = false;
                if (this.ipoint_2.X >= this.ipoint_3.X)
                {
                    x = (int) this.ipoint_3.X;
                    num2 = (int) this.ipoint_2.X;
                }
                else
                {
                    num2 = (int) this.ipoint_3.X;
                    x = (int) this.ipoint_2.X;
                }
                if (this.ipoint_2.Y >= this.ipoint_3.Y)
                {
                    y = (int) this.ipoint_3.Y;
                    num4 = (int) this.ipoint_2.Y;
                }
                else
                {
                    num4 = (int) this.ipoint_3.Y;
                    y = (int) this.ipoint_2.Y;
                }
                if (((num2 - x) <= 4) && ((num4 - y) <= 4))
                {
                    double tolerance = this.method_15();
                    element = graphicsContainer.LocateElements(this.ipoint_0, tolerance);
                    flag = true;
                }
                else
                {
                    element = graphicsContainer.LocateElementsByEnvelope(envelope);
                }
                IGraphicsContainerSelect select = this.method_8();
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                if (element == null)
                {
                    select.UnselectAllElements();
                }
                else
                {
                    IMapFrame frame = null;
                    if ((int_0 > 0) && flag)
                    {
                        this.method_26(int_0, element, select);
                    }
                    else
                    {
                        select.UnselectAllElements();
                        element.Reset();
                        for (IElement element2 = element.Next(); element2 != null; element2 = element.Next())
                        {
                            if (element2 is IMapFrame)
                            {
                                frame = element2 as IMapFrame;
                            }
                            else
                            {
                                frame = null;
                            }
                            this.method_24(select, element2);
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (frame != null)
                        {
                            this.method_25(frame as IElement);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void method_24(IGraphicsContainerSelect igraphicsContainerSelect_0, IElement ielement_2)
        {
            try
            {
                igraphicsContainerSelect_0.SelectElement(ielement_2);
                igraphicsContainerSelect_0.DominantElement = ielement_2;
                this.method_25(ielement_2);
                this._hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
            }
            catch
            {
            }
        }

        private void method_25(IElement ielement_2)
        {
            try
            {
                if (ielement_2 is IMapFrame)
                {
                    IMapFrame frame = ielement_2 as IMapFrame;
                    IActiveView activeView = this._hookHelper.ActiveView;
                    IMap map = frame.Map;
                    activeView.FocusMap = map;
                }
            }
            catch
            {
            }
        }

        private void method_26(int int_0, IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IElement element;
                bool flag = this.method_27(ienumElement_0, igraphicsContainerSelect_0, out element);
                if (element != null)
                {
                    IActiveView activeView = this._hookHelper.ActiveView;
                    if (flag)
                    {
                        if (int_0 == 2)
                        {
                            igraphicsContainerSelect_0.DominantElement = element;
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                        }
                        else
                        {
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                            igraphicsContainerSelect_0.UnselectElement(element);
                        }
                    }
                    else
                    {
                        this.method_24(igraphicsContainerSelect_0, element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                    }
                }
            }
            catch
            {
            }
        }

        private bool method_27(IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0, out IElement ielement_2)
        {
            bool flag = false;
            ielement_2 = null;
            try
            {
                ielement_2 = null;
                ienumElement_0.Reset();
                IElement element = ienumElement_0.Next();
                if (element == null)
                {
                    flag = false;
                    return false;
                }
                ielement_2 = element;
                return igraphicsContainerSelect_0.ElementSelected(element);
            }
            catch
            {
            }
            return flag;
        }

        private bool method_28(IElement ielement_2, ISymbol isymbol_0, IGeometry igeometry_1)
        {
            bool flag = false;
            try
            {
                isymbol_0 = null;
                igeometry_1 = null;
                if (ielement_2 is ITextElement)
                {
                    ITextElement element = ielement_2 as ITextElement;
                    ITextSymbol symbol = element.Symbol;
                    if (!(symbol is IFormattedTextSymbol))
                    {
                        flag = false;
                        return false;
                    }
                    isymbol_0 = symbol as ISymbol;
                }
                else
                {
                    if (!(ielement_2 is IMarkerElement))
                    {
                        goto Label_0087;
                    }
                    IMarkerElement element2 = ielement_2 as IMarkerElement;
                    IMarkerSymbol symbol2 = element2.Symbol;
                    if (!(element2 is IMarkerBackgroundSupport))
                    {
                        flag = false;
                        return false;
                    }
                    isymbol_0 = symbol2 as ISymbol;
                }
                igeometry_1 = ielement_2.Geometry;
                flag = true;
                return true;
            Label_0087:
                flag = false;
                return false;
            }
            catch
            {
            }
            return flag;
        }

        private void method_29(IElement ielement_2)
        {
            try
            {
                ICallout callout;
                this.icalloutFeedback_0.Stop();
                if (this.method_30(ielement_2, out callout))
                {
                    callout.AnchorPoint = this.ipoint_0;
                    this.method_31(ielement_2, callout);
                }
            }
            catch
            {
            }
        }

        private bool method_3(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                if (elementSelectionCount == 1)
                {
                    IGeometry geometry = this.method_14(this.ipoint_0);
                    for (int i = 0; i < elementSelectionCount; i++)
                    {
                        IPoint point;
                        IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                        if ((!element.Locked && this.method_20(element, out point)) && (point != null))
                        {
                            IGeometry other = this.method_14(point);
                            IRelationalOperator @operator = geometry as IRelationalOperator;
                            if (!@operator.Disjoint(other))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
            }
            return false;
        }

        private bool method_30(IElement ielement_2, out ICallout icallout_0)
        {
            icallout_0 = null;
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextElement element = ielement_2 as ITextElement;
                    ITextSymbol symbol = element.Symbol;
                    if (!(symbol is IFormattedTextSymbol))
                    {
                        return false;
                    }
                    IFormattedTextSymbol symbol2 = symbol as IFormattedTextSymbol;
                    ITextBackground background = symbol2.Background;
                    if (background == null)
                    {
                        return false;
                    }
                    if (!(background is ICallout))
                    {
                        return false;
                    }
                    icallout_0 = background as ICallout;
                    return true;
                }
                if (ielement_2 is IMarkerElement)
                {
                    IMarkerElement element2 = ielement_2 as IMarkerElement;
                    IMarkerSymbol symbol3 = element2.Symbol;
                    if (!(symbol3 is IMarkerBackgroundSupport))
                    {
                        return false;
                    }
                    IMarkerBackgroundSupport support = symbol3 as IMarkerBackgroundSupport;
                    IMarkerBackground background2 = support.Background;
                    if (background2 == null)
                    {
                        return false;
                    }
                    if (!(background2 is ICallout))
                    {
                        return false;
                    }
                    icallout_0 = background2 as ICallout;
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private void method_31(IElement ielement_2, ICallout icallout_0)
        {
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextElement element = ielement_2 as ITextElement;
                    ITextSymbol symbol = element.Symbol;
                    if (symbol is IFormattedTextSymbol)
                    {
                        IFormattedTextSymbol symbol2 = symbol as IFormattedTextSymbol;
                        symbol2.Background = null;
                        if (icallout_0 != null)
                        {
                            ITextBackground background = icallout_0 as ITextBackground;
                            symbol2.Background = background;
                        }
                        element.Symbol = symbol;
                    }
                }
                else if (ielement_2 is IMarkerElement)
                {
                    IMarkerElement element2 = ielement_2 as IMarkerElement;
                    IMarkerSymbol symbol3 = element2.Symbol;
                    if (symbol3 is IMarkerBackgroundSupport)
                    {
                        IMarkerBackgroundSupport support = symbol3 as IMarkerBackgroundSupport;
                        support.Background = null;
                        if (icallout_0 != null)
                        {
                            IMarkerBackground background2 = icallout_0 as IMarkerBackground;
                            support.Background = background2;
                        }
                        element2.Symbol = symbol3;
                    }
                }
            }
            catch
            {
            }
        }

        private void method_32(IElement ielement_2)
        {
            try
            {
                IGeometry geometry = this.iresizeEnvelopeFeedback2_0.Stop();
                if (this.bool_0 && (geometry != null))
                {
                    this._hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
                    IResizeElementOperation operation = new ResizeElementOperation {
                        Element = ielement_2,
                        Geometry = geometry
                    };
                    this._hookHelper.OperationStack.Do(operation);
                    this._hookHelper.ActiveView.GraphicsContainer.UpdateElement(ielement_2);
                    this._hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void method_33(int int_0)
        {
            try
            {
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer is IMap)
                {
                    IMap map = activeView as IMap;
                    ISpatialReference spatialReference = map.SpatialReference;
                    if (spatialReference != null)
                    {
                        this.ipoint_0.SpatialReference = spatialReference;
                    }
                }
                double tolerance = this.method_15();
                IEnumElement element = graphicsContainer.LocateElements(this.ipoint_0, tolerance);
                if (element != null)
                {
                    this.method_26(int_0, element, graphicsContainer as IGraphicsContainerSelect);
                }
            }
            catch
            {
            }
        }

        private void method_34(IMapFrame imapFrame_0, IDisplay idisplay_0)
        {
            try
            {
                IFrameDraw draw = null;
                if (imapFrame_0 is IFrameDraw)
                {
                    draw = imapFrame_0 as IFrameDraw;
                    draw.DrawBackground(idisplay_0, null);
                }
                IActiveView map = imapFrame_0.Map as IActiveView;
                IScreenDisplay screenDisplay = map.ScreenDisplay;
                int hDC = idisplay_0.hDC;
                tagRECT cacheRect = new tagRECT();
                tagRECT deviceFrame = screenDisplay.DisplayTransformation.DeviceFrame;
                screenDisplay.DrawCache(hDC, -3, ref deviceFrame, ref cacheRect);
                if (draw != null)
                {
                    draw.DrawForeground(idisplay_0, null);
                }
            }
            catch
            {
            }
        }

        private void method_35()
        {
            try
            {
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (activeView is IMap)
                {
                    IMap map = activeView as IMap;
                    ISpatialReference spatialReference = map.SpatialReference;
                    if (spatialReference != null)
                    {
                        this.ipoint_0.SpatialReference = spatialReference;
                    }
                }
                double tolerance = this.method_15();
                IEnumElement element = graphicsContainer.LocateElements(this.ipoint_0, tolerance);
                if (element != null)
                {
                    IGraphicsContainerSelect select = this.method_8();
                    if (select != null)
                    {
                        IElement element2 = select.SelectedElement(0);
                        if ((element2 != null) && this.method_37(element2, element))
                        {
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                            select.UnselectAllElements();
                            this.method_36(element, select, element2);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void method_36(IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0, IElement ielement_2)
        {
            try
            {
                bool flag = false;
                bool flag2 = false;
                if (ielement_2 == null)
                {
                    flag = true;
                }
                ienumElement_0.Reset();
                IElement element = ienumElement_0.Next();
                while (element == null)
                {
                Label_0020:
                    if (0 == 0)
                    {
                        goto Label_0055;
                    }
                    if (flag)
                    {
                        this.method_24(igraphicsContainerSelect_0, element);
                        flag2 = true;
                    }
                    if (this.method_38(element, ielement_2))
                    {
                        flag = true;
                    }
                    element = ienumElement_0.Next();
                }
                goto Label_0020;
            Label_0055:
                if (!flag2)
                {
                    ienumElement_0.Reset();
                    element = ienumElement_0.Next();
                    this.method_24(igraphicsContainerSelect_0, element);
                }
            }
            catch
            {
            }
        }

        private bool method_37(IElement ielement_2, IEnumElement ienumElement_0)
        {
            bool flag = false;
            try
            {
                ienumElement_0.Reset();
                for (IElement element = ienumElement_0.Next(); element != null; element = ienumElement_0.Next())
                {
                    if (this.method_38(element, ielement_2))
                    {
                        goto Label_0034;
                    }
                }
                flag = false;
                return false;
            Label_0034:
                flag = true;
                return true;
            }
            catch
            {
            }
            return flag;
        }

        private bool method_38(IElement ielement_2, IElement ielement_3)
        {
            bool flag = false;
            try
            {
                if (ielement_2 == ielement_3)
                {
                    flag = true;
                    return true;
                }
                return flag;
            }
            catch
            {
            }
            return flag;
        }

        private void method_39(IElement ielement_2, int int_0)
        {
            try
            {
                if (ielement_2 is IGroupElement)
                {
                    IGroupElement element = ielement_2 as IGroupElement;
                    IEnumElement elements = element.Elements;
                    if (elements != null)
                    {
                        elements.Reset();
                        for (IElement element3 = elements.Next(); element3 != null; element3 = elements.Next())
                        {
                            this.method_39(element3, 0);
                        }
                    }
                }
                else if (ielement_2 is IElementEditCallout)
                {
                    IElementEditCallout callout = ielement_2 as IElementEditCallout;
                    callout.EditingCallout = int_0 == 1;
                }
            }
            catch
            {
            }
        }

        private bool method_4(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IEnvelope envelope = this.method_14(this.ipoint_0) as IEnvelope;
                IPolygon outline = new PolygonClass();
                IEnumElement selectedElements = igraphicsContainerSelect_0.SelectedElements;
                selectedElements.Reset();
                for (IElement element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                {
                    if (!element2.Locked)
                    {
                        element2.QueryOutline(this._hookHelper.ActiveView.ScreenDisplay, outline);
                        IRelationalOperator @operator = envelope as IRelationalOperator;
                        if ((@operator != null) && !@operator.Disjoint(outline))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        private bool method_40()
        {
            IActiveView activeView = this._hookHelper.ActiveView;
            IDeleteElementOperation operation = new DeleteElementOperation {
                ActiveView = activeView,
                Elements = this.method_8().SelectedElements
            };
            this._hookHelper.OperationStack.Do(operation);
            activeView = null;
            return true;
        }

        private void method_41(string string_0)
        {
            IActiveView activeView = this._hookHelper.ActiveView;
            if ((activeView != null) && (activeView is IPageLayout))
            {
                IGraphicsContainer container = activeView as IGraphicsContainer;
                if (container != null)
                {
                    IGraphicsContainerSelect select = this.method_8();
                    if (select != null)
                    {
                        int elementSelectionCount = select.ElementSelectionCount;
                        if (elementSelectionCount >= 1)
                        {
                            IEnumElement selectedElements = select.SelectedElements;
                            if (selectedElements != null)
                            {
                                IElement element2;
                                IGroupElement element3;
                                switch (string_0)
                                {
                                    case "Top":
                                        container.BringToFront(selectedElements);
                                        break;

                                    case "Privious":
                                        container.BringForward(selectedElements);
                                        break;

                                    case "Next":
                                        container.SendBackward(selectedElements);
                                        break;

                                    case "Bottom":
                                        container.SendToBack(selectedElements);
                                        break;

                                    case "FromGroup":
                                        selectedElements.Reset();
                                        for (element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                                        {
                                            if (element2 is IGroupElement)
                                            {
                                                element3 = element2 as IGroupElement;
                                                for (int i = element3.ElementCount - 1; i <= 0; i += -1)
                                                {
                                                    IElement element = element3.get_Element(i);
                                                    container.MoveElementFromGroup(element3, element, i);
                                                }
                                                container.DeleteElement(element2);
                                            }
                                        }
                                        break;

                                    case "ToGroup":
                                        if (elementSelectionCount < 2)
                                        {
                                            return;
                                        }
                                        element3 = new GroupElementClass();
                                        selectedElements.Reset();
                                        for (element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                                        {
                                            container.MoveElementToGroup(element2, element3);
                                        }
                                        container.AddElement(element3 as IElement, 0);
                                        break;
                                }
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                            }
                        }
                    }
                }
            }
        }

        private void method_42()
        {
            IActiveView activeView = this._hookHelper.ActiveView;
            if ((activeView != null) && (activeView is IPageLayout))
            {
                IGraphicsContainer container = activeView as IGraphicsContainer;
                if (container != null)
                {
                    IGraphicsContainerSelect select = this.method_8();
                    if ((select != null) && (select.ElementSelectionCount == 1))
                    {
                        IEnumElement selectedElements = select.SelectedElements;
                        if (selectedElements != null)
                        {
                            selectedElements.Reset();
                            if (selectedElements.Next() == null)
                            {
                            }
                        }
                    }
                }
            }
        }

        private int method_43(IGraphicsContainer igraphicsContainer_0)
        {
            int num = 0;
            igraphicsContainer_0.Reset();
            for (IElement element = igraphicsContainer_0.Next(); element != null; element = igraphicsContainer_0.Next())
            {
                if (element is IMapFrame)
                {
                    num++;
                    if (num >= 2)
                    {
                        return num;
                    }
                }
                else if (element is IGroupElement)
                {
                    num += this.method_44(element as IGroupElement);
                    if (num >= 2)
                    {
                        return num;
                    }
                }
            }
            return num;
        }

        private int method_44(IGroupElement igroupElement_0)
        {
            int num = 0;
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            for (IElement element2 = elements.Next(); element2 != null; element2 = elements.Next())
            {
                if (element2 is IMapFrame)
                {
                    num++;
                    if (num >= 2)
                    {
                        return num;
                    }
                }
                else if (element2 is IGroupElement)
                {
                    num += this.method_44(element2 as IGroupElement);
                    if (num >= 2)
                    {
                        return num;
                    }
                }
            }
            return num;
        }

        private void method_45(IGraphicsContainer igraphicsContainer_0)
        {
            double num;
            double num2;
            IMapFrame frame = new MapFrameClass();
            IMap map = new MapClass {
                Name = "Layers"
            };
            frame.Map = map;
            IElement element = frame as IElement;
            IEnvelope envelope = new EnvelopeClass();
            IPageLayout layout = igraphicsContainer_0 as IPageLayout;
            layout.Page.QuerySize(out num, out num2);
            envelope.PutCoords(1.0, 1.0, num - 1.0, num2 - 1.0);
            element.Geometry = envelope;
            igraphicsContainer_0.AddElement(frame as IElement, 0);
        }

        private void method_5(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                this.imoveImageFeedback_0.Display = this._hookHelper.ActiveView.ScreenDisplay;
                this.method_17(igraphicsContainerSelect_0, this.imoveImageFeedback_0, this.igeometry_0);
                this.enum0_0 = Enum0.eMoving;
                this.imoveImageFeedback_0.Start(this.ipoint_0);
            }
            catch
            {
            }
        }

        private void method_6(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IClone clone = this.ipoint_0 as IClone;
                this.igeometry_0 = clone.Clone() as IGeometry;
                this.enum0_0 = Enum0.eMovingAnchor;
                this.method_18(igraphicsContainerSelect_0);
            }
            catch
            {
            }
        }

        private void method_7()
        {
            try
            {
                this.inewEnvelopeFeedback_0.Display = this._hookHelper.ActiveView.ScreenDisplay;
                this.enum0_0 = Enum0.eSelecting;
                this.inewEnvelopeFeedback_0.Start(this.ipoint_0);
            }
            catch
            {
            }
        }

        private IGraphicsContainerSelect method_8()
        {
            try
            {
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer == null)
                {
                    return null;
                }
                IViewManager manager = activeView as IViewManager;
                ISelection elementSelection = manager.ElementSelection;
                return (graphicsContainer as IGraphicsContainerSelect);
            }
            catch
            {
            }
            return null;
        }

        private bool method_9(IGraphicsContainerSelect igraphicsContainerSelect_0, int int_0, int int_1)
        {
            try
            {
                esriTrackerLocation location;
                IPoint point = this.method_2(int_0, int_1);
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                for (int i = 0; i <= (elementSelectionCount - 1); i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            location = selectionTracker.HitTest(point);
                            if ((location != esriTrackerLocation.LocationInterior) && (location != esriTrackerLocation.LocationNone))
                            {
                                goto Label_005E;
                            }
                        }
                    }
                }
                goto Label_006E;
            Label_005E:
                this.method_11(location);
                return true;
            }
            catch
            {
            }
        Label_006E:
            return false;
        }

        public override void OnCreate(object object_1)
        {
            this._hookHelper.Hook = object_1;
            this.enum0_0 = Enum0.eDormant;
        }

        public override void OnDblClick()
        {
            IGraphicsContainerSelect select = this.method_8();
            if (select.ElementSelectionCount != 0)
            {
                IElement element = select.SelectedElement(0);
                if (element is JLK.ExtendClass.IOleFrame)
                {
                    IActiveView activeView = this._hookHelper.ActiveView;
                    (element as JLK.ExtendClass.IOleFrame).OLEEditComplete += new OLEEditCompleteHandler(this.method_0);
                    (element as JLK.ExtendClass.IOleFrame).Edit(activeView.ScreenDisplay.hWnd);
                }
                else
                {
                    ElementChangeEvent.EditElementProperty(element);
                }
            }
        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            try
            {
                if (int_0 == 0x10)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                }
                else if (int_0 == 0x11)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                }
                if (int_0 == 0x1b)
                {
                    if (this.enum0_0 == Enum0.eSelecting)
                    {
                        this.inewEnvelopeFeedback_0.Stop();
                    }
                    else if (this.enum0_0 == Enum0.eMoving)
                    {
                        this.imoveImageFeedback_0.Refresh(0);
                        this.imoveImageFeedback_0.ClearImage();
                    }
                    else if ((this.enum0_0 == Enum0.eMovingAnchor) || (this.enum0_0 == Enum0.eMovingcallout))
                    {
                        this.icalloutFeedback_0.Stop();
                    }
                    else if (this.enum0_0 == Enum0.eResizing)
                    {
                        this.iresizeEnvelopeFeedback2_0.Stop();
                    }
                    this.enum0_0 = Enum0.eDormant;
                }
                if (int_0 == 0x2e)
                {
                    this.method_40();
                }
            }
            catch
            {
            }
        }

        public override void OnKeyUp(int int_0, int int_1)
        {
            try
            {
                this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsNone;
                this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsNone;
                if (int_0 != 0x2e)
                {
                }
            }
            catch
            {
            }
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            try
            {
                this.bool_0 = false;
                IGraphicsContainerSelect activeView = this._hookHelper.ActiveView as IGraphicsContainerSelect;
                int elementSelectionCount = activeView.ElementSelectionCount;
                if ((int_0 == 1) || (elementSelectionCount == 0))
                {
                    this.ipoint_0 = this.method_2(int_2, int_3);
                    this.ipoint_1.X = this.ipoint_0.X;
                    this.ipoint_1.Y = this.ipoint_0.Y;
                    IGraphicsContainerSelect select2 = this.method_8();
                    this.ipoint_2.X = int_2;
                    this.ipoint_2.Y = int_3;
                    this.ipoint_3.X = 0.0;
                    this.ipoint_3.Y = 0.0;
                    if (this.method_3(select2))
                    {
                        this.method_6(select2);
                    }
                    else if (!this.method_10(select2, int_2, int_3))
                    {
                        if (this.method_4(select2))
                        {
                            if (this.method_18(select2))
                            {
                                this.enum0_0 = Enum0.eMovingcallout;
                            }
                            else
                            {
                                this.method_5(select2);
                            }
                        }
                        else
                        {
                            this.method_7();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            try
            {
                if (int_0 == 0)
                {
                    base.m_cursor = Cursors.Default;
                    IGraphicsContainerSelect activeView = this._hookHelper.ActiveView as IGraphicsContainerSelect;
                    if (activeView.ElementSelectionCount != 0)
                    {
                        this.ipoint_0 = this.method_2(int_2, int_3);
                        this.ipoint_1.X = this.ipoint_0.X;
                        this.ipoint_1.Y = this.ipoint_0.Y;
                        IGraphicsContainerSelect select2 = this.method_8();
                        if (!this.method_9(select2, int_2, int_3) && this.method_4(select2))
                        {
                            base.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trck4way.cur"));
                        }
                    }
                }
                else
                {
                    this.bool_0 = true;
                    this.ipoint_0 = this.method_2(int_2, int_3);
                    switch (this.enum0_0)
                    {
                        case Enum0.eSelecting:
                            this.inewEnvelopeFeedback_0.MoveTo(this.ipoint_0);
                            break;

                        case Enum0.eMoving:
                            this.imoveImageFeedback_0.MoveTo(this.ipoint_0);
                            break;

                        case Enum0.eResizing:
                            this.iresizeEnvelopeFeedback2_0.MoveTo(this.ipoint_0);
                            break;

                        case Enum0.eMovingAnchor:
                            this.icalloutFeedback_0.MoveAnchorTo(this.ipoint_0);
                            break;

                        case Enum0.eMovingcallout:
                            goto Label_0154;
                    }
                }
                return;
            Label_0154:
                this.icalloutFeedback_0.MoveTo(this.ipoint_0);
            }
            catch
            {
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            try
            {
                if (int_0 == 1)
                {
                    switch (this.enum0_0)
                    {
                        case Enum0.eSelecting:
                            this.ipoint_3.X = int_2;
                            this.ipoint_3.Y = int_3;
                            this.method_23(int_1);
                            break;

                        case Enum0.eMoving:
                            this.imoveImageFeedback_0.Refresh(0);
                            this.imoveImageFeedback_0.ClearImage();
                            this.method_21(int_1);
                            break;

                        case Enum0.eResizing:
                            this.method_32(this.ielement_0);
                            break;

                        case Enum0.eMovingAnchor:
                            this.method_29(this.ielement_1);
                            break;

                        case Enum0.eMovingcallout:
                            this.icalloutFeedback_0.Refresh(0);
                            this.icalloutFeedback_0.Stop();
                            this.method_21(int_1);
                            break;
                    }
                    if (this.enum0_0 != Enum0.eDormant)
                    {
                        this.enum0_0 = Enum0.eDormant;
                    }
                    this.ipoint_2.X = 0.0;
                    this.ipoint_2.Y = 0.0;
                    this.ipoint_3.X = 0.0;
                    this.ipoint_3.Y = 0.0;
                }
                else if (int_0 != 2)
                {
                }
            }
            catch
            {
            }
        }

        public object ContextMenu
        {
            get
            {
                return "";
            }
        }

        public override bool Enabled
        {
            get
            {
                IActiveView activeView = this._hookHelper.ActiveView;
                if (activeView == null)
                {
                    return false;
                }
                return (activeView is IGraphicsContainer);
            }
        }

        public IPopuMenuWrap PopuMenu
        {
            set
            {
                this.ipopuMenuWrap_0 = value;
            }
        }

        private enum Enum0
        {
            eDormant,
            eSelecting,
            eMoving,
            eResizing,
            eMovingAnchor,
            eMovingcallout
        }
    }
}

