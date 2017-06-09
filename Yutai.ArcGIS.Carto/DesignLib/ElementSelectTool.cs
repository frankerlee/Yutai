using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Plugins.Interfaces;
using IOleFrame = Yutai.ArcGIS.Common.ExtendClass.IOleFrame;


namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElementSelectTool : BaseTool, IToolContextMenu
    {
        private IYTHookHelper ikhookHelper_0;

        private object object_0;

        private INewEnvelopeFeedback inewEnvelopeFeedback_0;

        private IMoveImageFeedback imoveImageFeedback_0;

        private IResizeEnvelopeFeedback2 iresizeEnvelopeFeedback2_0;

        private ICalloutFeedback icalloutFeedback_0;

        private bool bool_0 = false;

        private IPoint ipoint_0;

        private IPoint ipoint_1;

        private IPoint ipoint_2;

        private IPoint ipoint_3;

        private IElement ielement_0;

        private IGeometry igeometry_0;

        private IElement ielement_1;

        private ElementSelectTool.Enum0 enum0_0;

        private IPopuMenuWrap ipopuMenuWrap_0 = null;

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
                bool flag;
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                if (activeView != null)
                {
                    flag = (!(activeView is IGraphicsContainer) ? false : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public IPopuMenuWrap PopuMenu
        {
            set
            {
                this.ipopuMenuWrap_0 = value;
            }
        }

        public ElementSelectTool()
        {
            this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedbackClass();
            this.imoveImageFeedback_0 = new MoveImageFeedbackClass();
            this.iresizeEnvelopeFeedback2_0 = new ResizeEnvelopeFeedbackClass();
            this.icalloutFeedback_0 = new CalloutFeedbackClass();
            this.ipoint_0 = new PointClass();
            this.ipoint_1 = new PointClass();
            this.ipoint_2 = new PointClass();
            this.ipoint_3 = new PointClass();
            this.m_cursor = Cursors.Default;
            this.m_bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.ElementSelect.bmp"));
            this.m_name = "ElementSelectTool";
            this.m_caption = "选择元素";
        }

        public void Init()
        {
            this.ipopuMenuWrap_0.Clear();
            string[] strArrays = new string[] { "DeleteElement", "-", "ElmentProperty" };
            bool flag = false;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                flag = (i + 1 == (int)strArrays.Length ? false : string.Compare(strArrays[i + 1], "-") == 0);
                this.ipopuMenuWrap_0.AddItem(strArrays[i], flag);
            }
        }

        private void method_0(IOleFrame ioleFrame_0)
        {
            ElementChangeEvent.EditElementProperty(ioleFrame_0 as IElement);
            this.ikhookHelper_0.ActiveView.Refresh();
        }

        private int method_1(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            int num;
            int num1 = -1;
            try
            {
                if (igraphicsContainerSelect_0 == null)
                {
                    num = num1;
                }
                else if (this.ipoint_0 == null)
                {
                    num = num1;
                }
                else if (!this.ipoint_0.IsEmpty)
                {
                    int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                    num1 = 0;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 <= elementSelectionCount - 1)
                        {
                            IElement element = igraphicsContainerSelect_0.SelectedElement(num2);
                            if (!element.Locked)
                            {
                                this.method_39(element, elementSelectionCount);
                                ISelectionTracker selectionTracker = element.SelectionTracker;
                                if (selectionTracker != null)
                                {
                                    int num3 = selectionTracker.QueryCursor(this.ipoint_0);
                                    if (num3 != 0)
                                    {
                                        num1 = num3;
                                        break;
                                    }
                                }
                            }
                            num2++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    num = num1;
                }
                else
                {
                    num = num1;
                }
            }
            catch
            {
                num = num1;
                return num;
            }
            return num;
        }

        private bool method_10(IGraphicsContainerSelect igraphicsContainerSelect_0, int int_0, int int_1)
        {
            esriEnvelopeConstraints constraint;
            bool flag;
            try
            {
                IPoint point = this.method_2(int_0, int_1);
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                for (int i = 0; i <= elementSelectionCount - 1; i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            IDisplayFeedback resizeEnvelopeFeedbackClass = new ResizeEnvelopeFeedbackClass();
                            selectionTracker.QueryResizeFeedback(ref resizeEnvelopeFeedbackClass);
                            if (resizeEnvelopeFeedbackClass != null && resizeEnvelopeFeedbackClass is IResizeEnvelopeFeedback)
                            {
                                esriTrackerLocation _esriTrackerLocation = selectionTracker.HitTest(point);
                                IResizeEnvelopeFeedback resizeEnvelopeFeedback = resizeEnvelopeFeedbackClass as IResizeEnvelopeFeedback;
                                switch (_esriTrackerLocation)
                                {
                                    case esriTrackerLocation.LocationTopLeft:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationTopMiddle:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopMiddle;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationTopRight:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopRight;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationMiddleLeft:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleLeft;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationMiddleRight:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleRight;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationBottomLeft:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomLeft;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationBottomMiddle:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomMiddle;
                                            break;
                                        }
                                    case esriTrackerLocation.LocationBottomRight:
                                        {
                                            resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomRight;
                                            break;
                                        }
                                }
                                if (!(element as IBoundsProperties).FixedAspectRatio)
                                {
                                    constraint = resizeEnvelopeFeedback.Constraint;
                                    this.iresizeEnvelopeFeedback2_0.Constraint = constraint;
                                }
                                else
                                {
                                    constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                                }
                                if (constraint == esriEnvelopeConstraints.esriEnvelopeConstraintsAspect)
                                {
                                    this.iresizeEnvelopeFeedback2_0.AspectRatio = resizeEnvelopeFeedback.AspectRatio;
                                }
                            }
                            esriTrackerLocation _esriTrackerLocation1 = selectionTracker.HitTest(point);
                            if ((_esriTrackerLocation1 == esriTrackerLocation.LocationInterior ? false : _esriTrackerLocation1 != esriTrackerLocation.LocationNone))
                            {
                                this.method_12(element, _esriTrackerLocation1, point);
                                flag = true;
                                return flag;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            flag = false;
            return flag;
        }

        private void method_11(esriTrackerLocation esriTrackerLocation_0)
        {
            switch (esriTrackerLocation_0)
            {
                case esriTrackerLocation.LocationTopLeft:
                case esriTrackerLocation.LocationBottomRight:
                    {
                        this.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trcknwse.cur"));
                        break;
                    }
                case esriTrackerLocation.LocationTopMiddle:
                case esriTrackerLocation.LocationBottomMiddle:
                    {
                        this.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trckns.cur"));
                        break;
                    }
                case esriTrackerLocation.LocationTopRight:
                case esriTrackerLocation.LocationBottomLeft:
                    {
                        this.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trcknesw.cur"));
                        break;
                    }
                case esriTrackerLocation.LocationMiddleLeft:
                case esriTrackerLocation.LocationMiddleRight:
                    {
                        this.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trckwe.cur"));
                        break;
                    }
                default:
                    {
                        this.m_cursor = Cursors.Default;
                        break;
                    }
            }
        }

        private void method_12(IElement ielement_2, esriTrackerLocation esriTrackerLocation_0, IPoint ipoint_4)
        {
            IGeometry geometry;
            try
            {
                bool fixedSize = false;
                if (ielement_2 is IBoundsProperties)
                {
                    fixedSize = (ielement_2 as IBoundsProperties).FixedSize;
                }
                if (!fixedSize)
                {
                    geometry = ielement_2.Geometry;
                }
                else
                {
                    IEnvelope envelopeClass = new EnvelopeClass();
                    ielement_2.QueryBounds(this.ikhookHelper_0.ActiveView.ScreenDisplay, envelopeClass);
                    geometry = envelopeClass;
                }
                this.iresizeEnvelopeFeedback2_0.Display = this.ikhookHelper_0.ActiveView.ScreenDisplay;
                esriEnvelopeEdge _esriEnvelopeEdge = this.method_13(esriTrackerLocation_0);
                this.iresizeEnvelopeFeedback2_0.ResizeEdge = _esriEnvelopeEdge;
                this.enum0_0 = ElementSelectTool.Enum0.eResizing;
                this.ielement_0 = ielement_2;
                this.iresizeEnvelopeFeedback2_0.Start(geometry, this.ipoint_0);
            }
            catch
            {
            }
        }

        private esriEnvelopeEdge method_13(esriTrackerLocation esriTrackerLocation_0)
        {
            esriEnvelopeEdge _esriEnvelopeEdge;
            esriEnvelopeEdge _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
            try
            {
                switch (esriTrackerLocation_0)
                {
                    case esriTrackerLocation.LocationTopLeft:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                            break;
                        }
                    case esriTrackerLocation.LocationTopMiddle:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeTopMiddle;
                            break;
                        }
                    case esriTrackerLocation.LocationTopRight:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeTopRight;
                            break;
                        }
                    case esriTrackerLocation.LocationMiddleLeft:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeMiddleLeft;
                            break;
                        }
                    case esriTrackerLocation.LocationMiddleRight:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeMiddleRight;
                            break;
                        }
                    case esriTrackerLocation.LocationBottomLeft:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeBottomLeft;
                            break;
                        }
                    case esriTrackerLocation.LocationBottomMiddle:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeBottomMiddle;
                            break;
                        }
                    case esriTrackerLocation.LocationBottomRight:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeBottomRight;
                            break;
                        }
                    default:
                        {
                            _esriEnvelopeEdge1 = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                            break;
                        }
                }
                _esriEnvelopeEdge = _esriEnvelopeEdge1;
                return _esriEnvelopeEdge;
            }
            catch
            {
            }
            _esriEnvelopeEdge = _esriEnvelopeEdge1;
            return _esriEnvelopeEdge;
        }

        private IGeometry method_14(IPoint ipoint_4)
        {
            double num;
            double num1;
            IGeometry geometry;
            IGeometry geometry1 = null;
            try
            {
                double num2 = this.method_15();
                IEnvelope envelopeClass = new EnvelopeClass();
                ipoint_4.QueryCoords(out num, out num1);
                envelopeClass.PutCoords(num - num2, num1 - num2, num + num2, num1 + num2);
                geometry1 = envelopeClass;
                geometry = geometry1;
                return geometry;
            }
            catch
            {
            }
            geometry = geometry1;
            return geometry;
        }

        private double method_15()
        {
            double num;
            double x = 0;
            try
            {
                IDisplayTransformation displayTransformation = this.ikhookHelper_0.ActiveView.ScreenDisplay.DisplayTransformation;
                tagPOINT _tagPOINT = new tagPOINT();
                WKSPoint wKSPoint = new WKSPoint();
                _tagPOINT.x = 3;
                _tagPOINT.y = 3;
                displayTransformation.TransformCoords(ref wKSPoint, ref _tagPOINT, 1, 6);
                x = wKSPoint.X;
                num = x;
                return num;
            }
            catch
            {
            }
            num = x;
            return num;
        }

        private void method_16(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            ICallout callout;
            try
            {
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                if (igraphicsContainerSelect_0.ElementSelectionCount == 1)
                {
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
            IElement element;
            try
            {
                IDisplay display2 = imoveImageFeedback_1.Display_2;
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                IPolygon polygonClass = new PolygonClass();
                IPolygon polygon = new PolygonClass();
                if (elementSelectionCount != 1)
                {
                    for (int i = elementSelectionCount - 1; i >= 0; i--)
                    {
                        element = igraphicsContainerSelect_0.SelectedElement(i);
                        element.QueryOutline(display2, polygon);
                        (polygon as ITopologicalOperator).Simplify();
                        IGeometry geometry = (polygonClass as ITopologicalOperator).Union(polygon);
                        if (geometry != null)
                        {
                            polygonClass = geometry as IPolygon;
                        }
                    }
                }
                else
                {
                    element = igraphicsContainerSelect_0.SelectedElement(0);
                    element.QueryOutline(display2, polygonClass);
                }
                igeometry_1 = (polygonClass as IClone).Clone() as IGeometry;
                for (int j = elementSelectionCount - 1; j >= 0; j--)
                {
                    element = igraphicsContainerSelect_0.SelectedElement(j);
                    if (!(element is IMapFrame))
                    {
                        element.Draw(display2, null);
                    }
                    else
                    {
                        this.method_34(element as IMapFrame, display2);
                    }
                }
                (imoveImageFeedback_1 as IMoveImageFeedback2).PolygonBounds = polygonClass;
            }
            catch
            {
            }
        }

        private bool method_18(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            IPoint point;
            bool flag;
            bool flag1 = false;
            try
            {
                flag1 = false;
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                ISymbol symbol = null;
                IGeometry geometry = null;
                if (igraphicsContainerSelect_0.ElementSelectionCount == 1)
                {
                    IElement element = this.method_19(this.ipoint_0);
                    if (element != null && this.method_20(element, out point))
                    {
                        this.icalloutFeedback_0.Display = activeView.ScreenDisplay;
                        if (this.method_28(element, symbol, geometry))
                        {
                            this.icalloutFeedback_0.Start(symbol, geometry, this.ipoint_0);
                            this.icalloutFeedback_0.MoveTo(this.ipoint_0);
                            this.ielement_1 = element;
                            flag1 = true;
                        }
                    }
                }
                flag = flag1;
                return flag;
            }
            catch
            {
            }
            flag = flag1;
            return flag;
        }

        private IElement method_19(IPoint ipoint_4)
        {
            IElement element;
            IElement element1 = null;
            try
            {
                double num = this.method_15();
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                IGraphicsContainerSelect graphicsContainerSelect = activeView as IGraphicsContainerSelect;
                ISpatialReference spatialReference = null;
                if (activeView is IMap)
                {
                    spatialReference = (activeView as IMap).SpatialReference;
                }
                if (spatialReference != null)
                {
                    spatialReference = ipoint_4.SpatialReference;
                }
                IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                if (selectedElements != null)
                {
                    IElement element2 = selectedElements.Next();
                    while (true)
                    {
                        if (element2 == null)
                        {
                            break;
                        }
                        else if (element2.HitTest(ipoint_4.X, ipoint_4.Y, num))
                        {
                            element1 = element2;
                            break;
                        }
                        else
                        {
                            element2 = selectedElements.Next();
                        }
                    }
                    element = element1;
                    return element;
                }
                else
                {
                    element = element1;
                    return element;
                }
            }
            catch
            {
            }
            element = element1;
            return element;
        }

        private IPoint method_2(int int_0, int int_1)
        {
            IPoint mapPoint;
            try
            {
                IScreenDisplay screenDisplay = this.ikhookHelper_0.ActiveView.ScreenDisplay;
                mapPoint = screenDisplay.DisplayTransformation.ToMapPoint(int_0, int_1);
                return mapPoint;
            }
            catch
            {
            }
            mapPoint = null;
            return mapPoint;
        }

        private bool method_20(IElement ielement_2, out IPoint ipoint_4)
        {
            bool flag;
            ipoint_4 = null;
            try
            {
                ICallout callout = null;
                if (this.method_30(ielement_2, out callout))
                {
                    ipoint_4 = callout.AnchorPoint;
                    if (ipoint_4 != null)
                    {
                        flag = true;
                        return flag;
                    }
                }
                flag = false;
                return flag;
            }
            catch
            {
            }
            flag = false;
            return flag;
        }

        private void method_21(int int_0)
        {
            try
            {
                if (!(this.ipoint_1.X != this.ipoint_0.X ? true : this.ipoint_1.Y != this.ipoint_0.Y))
                {
                    if ((int_0 == 1 ? false : int_0 != 2))
                    {
                        this.method_35();
                    }
                    else
                    {
                        this.method_33(int_0);
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
            IMoveElementOperation moveElementOperation = new MoveElementOperation();
            double x = ipoint_4.X - ipoint_5.X;
            double y = ipoint_4.Y - ipoint_5.Y;
            IPoint pointClass = new PointClass();
            pointClass.PutCoords(x, y);
            moveElementOperation.Point = pointClass;
            moveElementOperation.ActiveView = this.ikhookHelper_0.ActiveView;
            moveElementOperation.Elements = this.method_8().SelectedElements;
            this.ikhookHelper_0.OperationStack.Do(moveElementOperation);
        }

        private void method_23(int int_0)
        {
            int x;
            int num;
            int y;
            int y1;
            try
            {
                IEnvelope envelope = this.inewEnvelopeFeedback_0.Stop();
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                ISpatialReference spatialReference = null;
                if (activeView is IMap)
                {
                    spatialReference = (activeView as IMap).SpatialReference;
                }
                if (spatialReference != null)
                {
                    this.ipoint_0.SpatialReference = spatialReference;
                    envelope.SpatialReference = spatialReference;
                }
                IEnumElement enumElement = null;
                bool flag = false;
                if (this.ipoint_2.X < this.ipoint_3.X)
                {
                    num = (int)this.ipoint_3.X;
                    x = (int)this.ipoint_2.X;
                }
                else
                {
                    x = (int)this.ipoint_3.X;
                    num = (int)this.ipoint_2.X;
                }
                if (this.ipoint_2.Y < this.ipoint_3.Y)
                {
                    y1 = (int)this.ipoint_3.Y;
                    y = (int)this.ipoint_2.Y;
                }
                else
                {
                    y = (int)this.ipoint_3.Y;
                    y1 = (int)this.ipoint_2.Y;
                }
                if ((num - x > 4 ? true : y1 - y > 4))
                {
                    enumElement = graphicsContainer.LocateElementsByEnvelope(envelope);
                }
                else
                {
                    double num1 = this.method_15();
                    enumElement = graphicsContainer.LocateElements(this.ipoint_0, num1);
                    flag = true;
                }
                IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                if (enumElement != null)
                {
                    IMapFrame mapFrame = null;
                    if ((int_0 <= 0 ? true : !flag))
                    {
                        graphicsContainerSelect.UnselectAllElements();
                        enumElement.Reset();
                        for (IElement i = enumElement.Next(); i != null; i = enumElement.Next())
                        {
                            if (!(i is IMapFrame))
                            {
                                mapFrame = null;
                            }
                            else
                            {
                                mapFrame = i as IMapFrame;
                            }
                            this.method_24(graphicsContainerSelect, i);
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (mapFrame != null)
                        {
                            this.method_25(mapFrame as IElement);
                        }
                    }
                    else
                    {
                        this.method_26(int_0, enumElement, graphicsContainerSelect);
                    }
                }
                else
                {
                    graphicsContainerSelect.UnselectAllElements();
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
                this.ikhookHelper_0.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
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
                    this.ikhookHelper_0.ActiveView.FocusMap = (ielement_2 as IMapFrame).Map;
                }
            }
            catch
            {
            }
        }

        private void method_26(int int_0, IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            IElement element;
            try
            {
                bool flag = this.method_27(ienumElement_0, igraphicsContainerSelect_0, out element);
                if (element != null)
                {
                    IActiveView activeView = this.ikhookHelper_0.ActiveView;
                    if (!flag)
                    {
                        this.method_24(igraphicsContainerSelect_0, element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                    }
                    else if (int_0 != 2)
                    {
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                        igraphicsContainerSelect_0.UnselectElement(element);
                    }
                    else
                    {
                        igraphicsContainerSelect_0.DominantElement = element;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        private bool method_27(IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0, out IElement ielement_2)
        {
            bool flag;
            bool flag1 = false;
            ielement_2 = null;
            try
            {
                ielement_2 = null;
                ienumElement_0.Reset();
                IElement element = ienumElement_0.Next();
                if (element != null)
                {
                    ielement_2 = element;
                    flag1 = (!igraphicsContainerSelect_0.ElementSelected(element) ? false : true);
                    flag = flag1;
                    return flag;
                }
                else
                {
                    flag1 = false;
                    flag = false;
                    return flag;
                }
            }
            catch
            {
            }
            flag = flag1;
            return flag;
        }

        private bool method_28(IElement ielement_2, ISymbol isymbol_0, IGeometry igeometry_1)
        {
            bool flag;
            bool flag1 = false;
            try
            {
                isymbol_0 = null;
                igeometry_1 = null;
                if (ielement_2 is ITextElement)
                {
                    ITextSymbol symbol = (ielement_2 as ITextElement).Symbol;
                    if (symbol is IFormattedTextSymbol)
                    {
                        isymbol_0 = symbol as ISymbol;
                    }
                    else
                    {
                        flag1 = false;
                        flag = false;
                        return flag;
                    }
                }
                else if (!(ielement_2 is IMarkerElement))
                {
                    flag1 = false;
                    flag = false;
                    return flag;
                }
                else
                {
                    IMarkerElement ielement2 = ielement_2 as IMarkerElement;
                    IMarkerSymbol markerSymbol = ielement2.Symbol;
                    if (ielement2 is IMarkerBackgroundSupport)
                    {
                        isymbol_0 = markerSymbol as ISymbol;
                    }
                    else
                    {
                        flag1 = false;
                        flag = false;
                        return flag;
                    }
                }
                igeometry_1 = ielement_2.Geometry;
                flag1 = true;
                flag = true;
            }
            catch
            {
                flag = flag1;
                return flag;
            }
            return flag;
        }

        private void method_29(IElement ielement_2)
        {
            ICallout ipoint0;
            try
            {
                this.icalloutFeedback_0.Stop();
                if (this.method_30(ielement_2, out ipoint0))
                {
                    ipoint0.AnchorPoint = this.ipoint_0;
                    this.method_31(ielement_2, ipoint0);
                }
            }
            catch
            {
            }
        }

        private bool method_3(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            bool flag;
            IPoint point;
            try
            {
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                if (elementSelectionCount == 1)
                {
                    IGeometry geometry = this.method_14(this.ipoint_0);
                    int num = 0;
                    while (num < elementSelectionCount)
                    {
                        IElement element = igraphicsContainerSelect_0.SelectedElement(num);
                        if (element.Locked || !this.method_20(element, out point) || point == null || (geometry as IRelationalOperator).Disjoint(this.method_14(point)))
                        {
                            num++;
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                    flag = false;
                }
                else
                {
                    flag = false;
                }
            }
            catch
            {
                flag = false;
                return flag;
            }
            return flag;
        }

        private bool method_30(IElement ielement_2, out ICallout icallout_0)
        {
            bool flag;
            icallout_0 = null;
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextSymbol symbol = (ielement_2 as ITextElement).Symbol;
                    if (symbol is IFormattedTextSymbol)
                    {
                        ITextBackground background = (symbol as IFormattedTextSymbol).Background;
                        if (background == null)
                        {
                            flag = false;
                        }
                        else if (background is ICallout)
                        {
                            icallout_0 = background as ICallout;
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else if (!(ielement_2 is IMarkerElement))
                {
                    flag = false;
                    return flag;
                }
                else
                {
                    IMarkerSymbol markerSymbol = (ielement_2 as IMarkerElement).Symbol;
                    if (markerSymbol is IMarkerBackgroundSupport)
                    {
                        IMarkerBackground markerBackground = (markerSymbol as IMarkerBackgroundSupport).Background;
                        if (markerBackground == null)
                        {
                            flag = false;
                        }
                        else if (markerBackground is ICallout)
                        {
                            icallout_0 = markerBackground as ICallout;
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch
            {
                flag = false;
                return flag;
            }
            return flag;
        }

        private void method_31(IElement ielement_2, ICallout icallout_0)
        {
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextElement ielement2 = ielement_2 as ITextElement;
                    ITextSymbol symbol = ielement2.Symbol;
                    if (symbol is IFormattedTextSymbol)
                    {
                        IFormattedTextSymbol icallout0 = symbol as IFormattedTextSymbol;
                        icallout0.Background = null;
                        if (icallout_0 != null)
                        {
                            icallout0.Background = icallout_0 as ITextBackground;
                        }
                        ielement2.Symbol = symbol;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (ielement_2 is IMarkerElement)
                {
                    IMarkerElement markerElement = ielement_2 as IMarkerElement;
                    IMarkerSymbol markerSymbol = markerElement.Symbol;
                    if (markerSymbol is IMarkerBackgroundSupport)
                    {
                        IMarkerBackgroundSupport markerBackgroundSupport = markerSymbol as IMarkerBackgroundSupport;
                        markerBackgroundSupport.Background = null;
                        if (icallout_0 != null)
                        {
                            markerBackgroundSupport.Background = icallout_0 as IMarkerBackground;
                        }
                        markerElement.Symbol = markerSymbol;
                    }
                    else
                    {
                        return;
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
                if (this.bool_0)
                {
                    if (geometry != null)
                    {
                        this.ikhookHelper_0.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
                        IResizeElementOperation resizeElementOperation = new ResizeElementOperation()
                        {
                            Element = ielement_2,
                            Geometry = geometry
                        };
                        this.ikhookHelper_0.OperationStack.Do(resizeElementOperation);
                        this.ikhookHelper_0.ActiveView.GraphicsContainer.UpdateElement(ielement_2);
                        this.ikhookHelper_0.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
                    }
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private void method_33(int int_0)
        {
            try
            {
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer is IMap)
                {
                    ISpatialReference spatialReference = (activeView as IMap).SpatialReference;
                    if (spatialReference != null)
                    {
                        this.ipoint_0.SpatialReference = spatialReference;
                    }
                }
                double num = this.method_15();
                IEnumElement enumElement = graphicsContainer.LocateElements(this.ipoint_0, num);
                if (enumElement != null)
                {
                    this.method_26(int_0, enumElement, graphicsContainer as IGraphicsContainerSelect);
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
                IFrameDraw imapFrame0 = null;
                if (imapFrame_0 is IFrameDraw)
                {
                    imapFrame0 = imapFrame_0 as IFrameDraw;
                    imapFrame0.DrawBackground(idisplay_0, null);
                }
                IScreenDisplay screenDisplay = (imapFrame_0.Map as IActiveView).ScreenDisplay;
                int idisplay0 = idisplay_0.hDC;
                tagRECT _tagRECT = new tagRECT();
                tagRECT deviceFrame = screenDisplay.DisplayTransformation.get_DeviceFrame();
                screenDisplay.DrawCache(idisplay0, -3, ref deviceFrame, ref _tagRECT);
                if (imapFrame0 != null)
                {
                    imapFrame0.DrawForeground(idisplay_0, null);
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
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (activeView is IMap)
                {
                    ISpatialReference spatialReference = (activeView as IMap).SpatialReference;
                    if (spatialReference != null)
                    {
                        this.ipoint_0.SpatialReference = spatialReference;
                    }
                }
                double num = this.method_15();
                IEnumElement enumElement = graphicsContainer.LocateElements(this.ipoint_0, num);
                if (enumElement != null)
                {
                    IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
                    if (graphicsContainerSelect != null)
                    {
                        IElement element = graphicsContainerSelect.SelectedElement(0);
                        if (element != null)
                        {
                            if (this.method_37(element, enumElement))
                            {
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                                graphicsContainerSelect.UnselectAllElements();
                                this.method_36(enumElement, graphicsContainerSelect, element);
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                            }
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
                bool flag1 = false;
                if (ielement_2 == null)
                {
                    flag = true;
                }
                ienumElement_0.Reset();
                IElement element = ienumElement_0.Next();
                while (true)
                {
                    if ((element == null ? true : flag1))
                    {
                        break;
                    }
                    if (flag)
                    {
                        this.method_24(igraphicsContainerSelect_0, element);
                        flag1 = true;
                    }
                    if (this.method_38(element, ielement_2))
                    {
                        flag = true;
                    }
                    element = ienumElement_0.Next();
                }
                if (!flag1)
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
            bool flag;
            bool flag1 = false;
            try
            {
                ienumElement_0.Reset();
                IElement element = ienumElement_0.Next();
                while (element != null)
                {
                    if (this.method_38(element, ielement_2))
                    {
                        flag1 = true;
                        flag = true;
                        return flag;
                    }
                    else
                    {
                        element = ienumElement_0.Next();
                    }
                }
                flag1 = false;
                flag = false;
                return flag;
            }
            catch
            {
            }
            flag = flag1;
            return flag;
        }

        private bool method_38(IElement ielement_2, IElement ielement_3)
        {
            bool flag;
            bool flag1 = false;
            try
            {
                if (ielement_2 != ielement_3)
                {
                    flag = flag1;
                    return flag;
                }
                else
                {
                    flag1 = true;
                    flag = true;
                    return flag;
                }
            }
            catch
            {
            }
            flag = flag1;
            return flag;
        }

        private void method_39(IElement ielement_2, int int_0)
        {
            try
            {
                if (ielement_2 is IGroupElement)
                {
                    IEnumElement elements = (ielement_2 as IGroupElement).Elements;
                    if (elements != null)
                    {
                        elements.Reset();
                        for (IElement i = elements.Next(); i != null; i = elements.Next())
                        {
                            this.method_39(i, 0);
                        }
                    }
                }
                else if (ielement_2 is IElementEditCallout)
                {
                    (ielement_2 as IElementEditCallout).EditingCallout = int_0 == 1;
                }
            }
            catch
            {
            }
        }

        private bool method_4(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            bool flag;
            try
            {
                IEnvelope envelope = this.method_14(this.ipoint_0) as IEnvelope;
                IPolygon polygonClass = new PolygonClass();
                IEnumElement selectedElements = igraphicsContainerSelect_0.SelectedElements;
                selectedElements.Reset();
                for (IElement i = selectedElements.Next(); i != null; i = selectedElements.Next())
                {
                    if (!i.Locked)
                    {
                        i.QueryOutline(this.ikhookHelper_0.ActiveView.ScreenDisplay, polygonClass);
                        IRelationalOperator relationalOperator = envelope as IRelationalOperator;
                        if (relationalOperator != null && !relationalOperator.Disjoint(polygonClass))
                        {
                            flag = true;
                            return flag;
                        }
                    }
                }
            }
            catch
            {
            }
            flag = false;
            return flag;
        }

        private bool method_40()
        {
            IActiveView activeView = this.ikhookHelper_0.ActiveView;
            IDeleteElementOperation deleteElementOperation = new DeleteElementOperation()
            {
                ActiveView = activeView,
                Elements = this.method_8().SelectedElements
            };
            this.ikhookHelper_0.OperationStack.Do(deleteElementOperation);
            activeView = null;
            return true;
        }

        private void method_41(string string_0)
        {
            IElement i;
            IGroupElement groupElementClass;
            IActiveView activeView = this.ikhookHelper_0.ActiveView;
            if (activeView != null && activeView is IPageLayout)
            {
                IGraphicsContainer graphicsContainer = activeView as IGraphicsContainer;
                if (graphicsContainer != null)
                {
                    IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
                    if (graphicsContainerSelect != null)
                    {
                        int elementSelectionCount = graphicsContainerSelect.ElementSelectionCount;
                        if (elementSelectionCount >= 1)
                        {
                            IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                            if (selectedElements != null)
                            {
                                string string0 = string_0;
                                if (string0 != null)
                                {
                                    if (string0 == "Top")
                                    {
                                        graphicsContainer.BringToFront(selectedElements);
                                    }
                                    else if (string0 == "Privious")
                                    {
                                        graphicsContainer.BringForward(selectedElements);
                                    }
                                    else if (string0 == "Next")
                                    {
                                        graphicsContainer.SendBackward(selectedElements);
                                    }
                                    else if (string0 == "Bottom")
                                    {
                                        graphicsContainer.SendToBack(selectedElements);
                                    }
                                    else if (string0 == "FromGroup")
                                    {
                                        selectedElements.Reset();
                                        for (i = selectedElements.Next(); i != null; i = selectedElements.Next())
                                        {
                                            if (i is IGroupElement)
                                            {
                                                groupElementClass = i as IGroupElement;
                                                for (int j = groupElementClass.ElementCount - 1; j <= 0; j = j + -1)
                                                {
                                                    graphicsContainer.MoveElementFromGroup(groupElementClass, groupElementClass.Element[j], j);
                                                }
                                                graphicsContainer.DeleteElement(i);
                                            }
                                        }
                                    }
                                    else if (string0 == "ToGroup")
                                    {
                                        if (elementSelectionCount < 2)
                                        {
                                            return;
                                        }
                                        groupElementClass = new GroupElementClass();
                                        selectedElements.Reset();
                                        for (i = selectedElements.Next(); i != null; i = selectedElements.Next())
                                        {
                                            graphicsContainer.MoveElementToGroup(i, groupElementClass);
                                        }
                                        graphicsContainer.AddElement(groupElementClass as IElement, 0);
                                    }
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
            IActiveView activeView = this.ikhookHelper_0.ActiveView;
            if (activeView != null && activeView is IPageLayout && activeView is IGraphicsContainer)
            {
                IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
                if (graphicsContainerSelect != null && graphicsContainerSelect.ElementSelectionCount == 1)
                {
                    IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                    if (selectedElements != null)
                    {
                        selectedElements.Reset();
                      
                    }
                }
            }
        }

        private int method_43(IGraphicsContainer igraphicsContainer_0)
        {
            int num;
            int num1 = 0;
            igraphicsContainer_0.Reset();
            IElement element = igraphicsContainer_0.Next();
            while (true)
            {
                if (element != null)
                {
                    if (element is IMapFrame)
                    {
                        num1++;
                        if (num1 >= 2)
                        {
                            num = num1;
                            break;
                        }
                    }
                    else if (element is IGroupElement)
                    {
                        num1 = num1 + this.method_44(element as IGroupElement);
                        if (num1 >= 2)
                        {
                            num = num1;
                            break;
                        }
                    }
                    element = igraphicsContainer_0.Next();
                }
                else
                {
                    num = num1;
                    break;
                }
            }
            return num;
        }

        private int method_44(IGroupElement igroupElement_0)
        {
            int num;
            int num1 = 0;
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            IElement element = elements.Next();
            while (true)
            {
                if (element != null)
                {
                    if (element is IMapFrame)
                    {
                        num1++;
                        if (num1 >= 2)
                        {
                            num = num1;
                            break;
                        }
                    }
                    else if (element is IGroupElement)
                    {
                        num1 = num1 + this.method_44(element as IGroupElement);
                        if (num1 >= 2)
                        {
                            num = num1;
                            break;
                        }
                    }
                    element = elements.Next();
                }
                else
                {
                    num = num1;
                    break;
                }
            }
            return num;
        }

        private void method_45(IGraphicsContainer igraphicsContainer_0)
        {
            double num;
            double num1;
            IMapFrame mapFrameClass = new MapFrameClass()
            {
                Map = new MapClass()
                {
                    Name = "Layers"
                }
            };
            IElement element = mapFrameClass as IElement;
            IEnvelope envelopeClass = new EnvelopeClass();
            (igraphicsContainer_0 as IPageLayout).Page.QuerySize(out num, out num1);
            envelopeClass.PutCoords(1, 1, num - 1, num1 - 1);
            element.Geometry = envelopeClass;
            igraphicsContainer_0.AddElement(mapFrameClass as IElement, 0);
        }

        private void method_5(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                this.imoveImageFeedback_0.Display = this.ikhookHelper_0.ActiveView.ScreenDisplay;
                this.method_17(igraphicsContainerSelect_0, this.imoveImageFeedback_0, this.igeometry_0);
                this.enum0_0 = ElementSelectTool.Enum0.eMoving;
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
                this.igeometry_0 = (this.ipoint_0 as IClone).Clone() as IGeometry;
                this.enum0_0 = ElementSelectTool.Enum0.eMovingAnchor;
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
                this.inewEnvelopeFeedback_0.Display = this.ikhookHelper_0.ActiveView.ScreenDisplay;
                this.enum0_0 = ElementSelectTool.Enum0.eSelecting;
                this.inewEnvelopeFeedback_0.Start(this.ipoint_0);
            }
            catch
            {
            }
        }

        private IGraphicsContainerSelect method_8()
        {
            IGraphicsContainerSelect graphicsContainerSelect;
            try
            {
                IActiveView activeView = this.ikhookHelper_0.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer != null)
                {
                    ISelection elementSelection = (activeView as IViewManager).ElementSelection;
                    graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
                    return graphicsContainerSelect;
                }
                else
                {
                    graphicsContainerSelect = null;
                    return graphicsContainerSelect;
                }
            }
            catch
            {
            }
            graphicsContainerSelect = null;
            return graphicsContainerSelect;
        }

        private bool method_9(IGraphicsContainerSelect igraphicsContainerSelect_0, int int_0, int int_1)
        {
            bool flag;
            try
            {
                IPoint point = this.method_2(int_0, int_1);
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                for (int i = 0; i <= elementSelectionCount - 1; i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            esriTrackerLocation _esriTrackerLocation = selectionTracker.HitTest(point);
                            if ((_esriTrackerLocation == esriTrackerLocation.LocationInterior ? false : _esriTrackerLocation != esriTrackerLocation.LocationNone))
                            {
                                this.method_11(_esriTrackerLocation);
                                flag = true;
                                return flag;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            flag = false;
            return flag;
        }

        public override void OnCreate(object object_1)
        {
            this.ikhookHelper_0.Hook = object_1;
            this.enum0_0 = ElementSelectTool.Enum0.eDormant;
        }

        public override void OnDblClick()
        {
            IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
            if (graphicsContainerSelect.ElementSelectionCount != 0)
            {
                IElement element = graphicsContainerSelect.SelectedElement(0);
                if (!(element is IOleFrame))
                {
                    ElementChangeEvent.EditElementProperty(element);
                }
                else
                {
                    IActiveView activeView = this.ikhookHelper_0.ActiveView;
                    (element as IOleFrame).OLEEditComplete += new OLEEditCompleteHandler(this.method_0);
                    (element as IOleFrame).Edit(activeView.ScreenDisplay.hWnd);
                }
            }
        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            try
            {
                if (int_0 == 16)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                }
                else if (int_0 == 17)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                }
                if (int_0 == 27)
                {
                    if (this.enum0_0 == ElementSelectTool.Enum0.eSelecting)
                    {
                        this.inewEnvelopeFeedback_0.Stop();
                    }
                    else if (this.enum0_0 == ElementSelectTool.Enum0.eMoving)
                    {
                        this.imoveImageFeedback_0.Refresh(0);
                        this.imoveImageFeedback_0.ClearImage();
                    }
                    else if (!(this.enum0_0 == ElementSelectTool.Enum0.eMovingAnchor ? false : this.enum0_0 != ElementSelectTool.Enum0.eMovingcallout))
                    {
                        this.icalloutFeedback_0.Stop();
                    }
                    else if (this.enum0_0 == ElementSelectTool.Enum0.eResizing)
                    {
                        this.iresizeEnvelopeFeedback2_0.Stop();
                    }
                    this.enum0_0 = ElementSelectTool.Enum0.eDormant;
                }
                if (int_0 == 46)
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
                int elementSelectionCount = (this.ikhookHelper_0.ActiveView as IGraphicsContainerSelect).ElementSelectionCount;
                if ((int_0 == 1 ? true : elementSelectionCount == 0))
                {
                    this.ipoint_0 = this.method_2(int_2, int_3);
                    this.ipoint_1.X = this.ipoint_0.X;
                    this.ipoint_1.Y = this.ipoint_0.Y;
                    IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
                    this.ipoint_2.X = (double)int_2;
                    this.ipoint_2.Y = (double)int_3;
                    this.ipoint_3.X = 0;
                    this.ipoint_3.Y = 0;
                    if (this.method_3(graphicsContainerSelect))
                    {
                        this.method_6(graphicsContainerSelect);
                    }
                    else if (!this.method_10(graphicsContainerSelect, int_2, int_3))
                    {
                        if (!this.method_4(graphicsContainerSelect))
                        {
                            this.method_7();
                        }
                        else if (!this.method_18(graphicsContainerSelect))
                        {
                            this.method_5(graphicsContainerSelect);
                        }
                        else
                        {
                            this.enum0_0 = ElementSelectTool.Enum0.eMovingcallout;
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
                if (int_0 != 0)
                {
                    this.bool_0 = true;
                    this.ipoint_0 = this.method_2(int_2, int_3);
                    switch (this.enum0_0)
                    {
                        case ElementSelectTool.Enum0.eDormant:
                            {
                                return;
                            }
                        case ElementSelectTool.Enum0.eSelecting:
                            {
                                this.inewEnvelopeFeedback_0.MoveTo(this.ipoint_0);
                                return;
                            }
                        case ElementSelectTool.Enum0.eMoving:
                            {
                                this.imoveImageFeedback_0.MoveTo(this.ipoint_0);
                                return;
                            }
                        case ElementSelectTool.Enum0.eResizing:
                            {
                                this.iresizeEnvelopeFeedback2_0.MoveTo(this.ipoint_0);
                                return;
                            }
                        case ElementSelectTool.Enum0.eMovingAnchor:
                            {
                                this.icalloutFeedback_0.MoveAnchorTo(this.ipoint_0);
                                return;
                            }
                        case ElementSelectTool.Enum0.eMovingcallout:
                            {
                                this.icalloutFeedback_0.MoveTo(this.ipoint_0);
                                return;
                            }
                    }
                }
                else
                {
                    this.m_cursor = Cursors.Default;
                    if ((this.ikhookHelper_0.ActiveView as IGraphicsContainerSelect).ElementSelectionCount != 0)
                    {
                        this.ipoint_0 = this.method_2(int_2, int_3);
                        this.ipoint_1.X = this.ipoint_0.X;
                        this.ipoint_1.Y = this.ipoint_0.Y;
                        IGraphicsContainerSelect graphicsContainerSelect = this.method_8();
                        if (this.method_9(graphicsContainerSelect, int_2, int_3))
                        {
                            return;
                        }
                        else if (this.method_4(graphicsContainerSelect))
                        {
                            this.m_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("JLK.CartoDesignLib.trck4way.cur"));
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            try
            {
                if (int_0 != 1)
                {
                    
                }
                else
                {
                    switch (this.enum0_0)
                    {
                        case ElementSelectTool.Enum0.eSelecting:
                            {
                                this.ipoint_3.X = (double)int_2;
                                this.ipoint_3.Y = (double)int_3;
                                this.method_23(int_1);
                                break;
                            }
                        case ElementSelectTool.Enum0.eMoving:
                            {
                                this.imoveImageFeedback_0.Refresh(0);
                                this.imoveImageFeedback_0.ClearImage();
                                this.method_21(int_1);
                                break;
                            }
                        case ElementSelectTool.Enum0.eResizing:
                            {
                                this.method_32(this.ielement_0);
                                break;
                            }
                        case ElementSelectTool.Enum0.eMovingAnchor:
                            {
                                this.method_29(this.ielement_1);
                                break;
                            }
                        case ElementSelectTool.Enum0.eMovingcallout:
                            {
                                this.icalloutFeedback_0.Refresh(0);
                                this.icalloutFeedback_0.Stop();
                                this.method_21(int_1);
                                break;
                            }
                    }
                    if (this.enum0_0 != ElementSelectTool.Enum0.eDormant)
                    {
                        this.enum0_0 = ElementSelectTool.Enum0.eDormant;
                    }
                    this.ipoint_2.X = 0;
                    this.ipoint_2.Y = 0;
                    this.ipoint_3.X = 0;
                    this.ipoint_3.Y = 0;
                }
            }
            catch
            {
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