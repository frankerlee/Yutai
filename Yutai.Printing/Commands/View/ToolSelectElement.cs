using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.DesignLib;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using DeleteElementOperation = Yutai.ArcGIS.Common.Carto.DeleteElementOperation;
using EditElementPropertiesOperation = Yutai.ArcGIS.Carto.EditElementPropertiesOperation;
using ElementChangeEvent = Yutai.ArcGIS.Common.Carto.ElementChangeEvent;
using ElementGeometryInfoPropertyPage = Yutai.ArcGIS.Carto.UI.ElementGeometryInfoPropertyPage;
using ElementSizeAndPositionCtrl = Yutai.ArcGIS.Carto.UI.ElementSizeAndPositionCtrl;
using FillSymbolPropertyPage = Yutai.ArcGIS.Carto.UI.FillSymbolPropertyPage;
using IDeleteElementOperation = Yutai.ArcGIS.Common.Carto.IDeleteElementOperation;
using IEditElementPropertiesOperation = Yutai.ArcGIS.Common.Carto.IEditElementPropertiesOperation;
using IMoveElementOperation = Yutai.ArcGIS.Common.Carto.IMoveElementOperation;
using IOleFrame = Yutai.ArcGIS.Common.ExtendClass.IOleFrame;
using IResizeElementOperation = Yutai.ArcGIS.Common.Carto.IResizeElementOperation;
using IToolContextMenu = Yutai.Plugins.Interfaces.IToolContextMenu;
using LegendItemPropertyPage = Yutai.ArcGIS.Carto.UI.LegendItemPropertyPage;
using LegendPropertyPage = Yutai.ArcGIS.Carto.UI.LegendPropertyPage;
using LineSymbolPropertyPage = Yutai.ArcGIS.Carto.UI.LineSymbolPropertyPage;
using MarkerElementPropertyPage = Yutai.ArcGIS.Carto.UI.MarkerElementPropertyPage;
using MoveElementOperation = Yutai.ArcGIS.Common.Carto.MoveElementOperation;
using NorthArrowPropertyPage = Yutai.ArcGIS.Carto.UI.NorthArrowPropertyPage;
using NumberAndLabelPropertyPage = Yutai.ArcGIS.Carto.UI.NumberAndLabelPropertyPage;
using ResizeElementOperation = Yutai.ArcGIS.Common.Carto.ResizeElementOperation;
using ScaleAndUnitsPropertyPage = Yutai.ArcGIS.Carto.UI.ScaleAndUnitsPropertyPage;
using ScaleBarFormatPropertyPage = Yutai.ArcGIS.Carto.UI.ScaleBarFormatPropertyPage;
using ScaleTextFormatPropertyPage = Yutai.ArcGIS.Carto.UI.ScaleTextFormatPropertyPage;
using ScaleTextTextPropertyPage = Yutai.ArcGIS.Carto.UI.ScaleTextTextPropertyPage;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolSelectElement : YutaiTool, IToolContextMenu
    {
        private enum Enum1
        {
            eDormant,
            eSelecting,
            eMoving,
            eResizing,
            eMovingAnchor,
            eMovingcallout
        }

        private object object_0;

        private INewEnvelopeFeedback inewEnvelopeFeedback_0;

        private IMoveImageFeedback imoveImageFeedback_0;

        private IResizeEnvelopeFeedback2 iresizeEnvelopeFeedback2_0;

        private ICalloutFeedback icalloutFeedback_0;

        private bool bool_0 = false;

        private IPoint _pPoint0;

        private IPoint _pPoint1;

        private IPoint _pPoint2;

        private IPoint _pPoint3;

        private IElement ielement_0;

        private IGeometry igeometry_0;

        private IElement ielement_1;

        private ToolSelectElement.Enum1 enum1_0;

        private IPopuMenuWrap ipopuMenuWrap_0 = null;

        public override bool Enabled
        {
            get
            {
                IActiveView activeView = this._context.ActiveView;
                return activeView != null && activeView is IGraphicsContainer;
            }
        }

        public object ContextMenu
        {
            get { return ""; }
        }

        public IPopuMenuWrap PopuMenu
        {
            set { this.ipopuMenuWrap_0 = value; }
        }

        public override void OnCreate(object hook)
        {
            this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback();
            this.imoveImageFeedback_0 = new MoveImageFeedback();
            this.iresizeEnvelopeFeedback2_0 = new ResizeEnvelopeFeedback() as IResizeEnvelopeFeedback2;
            this.icalloutFeedback_0 = new CalloutFeedback();
            this._pPoint0 = new ESRI.ArcGIS.Geometry.Point();
            this._pPoint1 = new ESRI.ArcGIS.Geometry.Point();
            this._pPoint2 = new ESRI.ArcGIS.Geometry.Point();
            this._pPoint3 = new ESRI.ArcGIS.Geometry.Point();
            this.m_cursor = System.Windows.Forms.Cursors.Default;

            this.m_name = "ElementSelectTool";
            this.m_caption = "选择";
            this.enum1_0 = ToolSelectElement.Enum1.eDormant;
            base.m_bitmap = Properties.Resources.icon_select;
            base.m_name = "Printing_SelectElement";
            _key = "Printing_SelectElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolSelectElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            try
            {
                this.bool_0 = false;
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainerSelect graphicsContainerSelect = activeView as IGraphicsContainerSelect;
                int elementSelectionCount = graphicsContainerSelect.ElementSelectionCount;
                if (button == 1 || elementSelectionCount == 0)
                {
                    this._pPoint0 = this.method_2(x, y);
                    this._pPoint1.X = this._pPoint0.X;
                    this._pPoint1.Y = this._pPoint0.Y;
                    IGraphicsContainerSelect igraphicsContainerSelect_ = this.method_9();
                    this._pPoint2.X = (double) x;
                    this._pPoint2.Y = (double) y;
                    this._pPoint3.X = 0.0;
                    this._pPoint3.Y = 0.0;
                    if (this.method_3(igraphicsContainerSelect_))
                    {
                        this.method_6(igraphicsContainerSelect_);
                    }
                    else if (!this.method_11(igraphicsContainerSelect_, x, y))
                    {
                        if (this.method_4(igraphicsContainerSelect_))
                        {
                            if (this.method_19(igraphicsContainerSelect_))
                            {
                                this.enum1_0 = ToolSelectElement.Enum1.eMovingcallout;
                            }
                            else
                            {
                                this.method_5(igraphicsContainerSelect_);
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

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            try
            {
                if (Button == 0)
                {
                    this.m_cursor = System.Windows.Forms.Cursors.Default;
                    IActiveView activeView = this._context.ActiveView;
                    IGraphicsContainerSelect graphicsContainerSelect = activeView as IGraphicsContainerSelect;
                    int elementSelectionCount = graphicsContainerSelect.ElementSelectionCount;
                    if (elementSelectionCount != 0)
                    {
                        this._pPoint0 = this.method_2(X, Y);
                        this._pPoint1.X = this._pPoint0.X;
                        this._pPoint1.Y = this._pPoint0.Y;
                        IGraphicsContainerSelect igraphicsContainerSelect_ = this.method_9();
                        if (!this.method_10(igraphicsContainerSelect_, X, Y))
                        {
                            if (this.method_4(igraphicsContainerSelect_))
                            {
                                this.m_cursor =
                                    new System.Windows.Forms.Cursor(
                                        base.GetType()
                                            .Assembly.GetManifestResourceStream("JLK.Pluge.Bitmaps.trck4way.cur"));
                            }
                        }
                    }
                }
                else
                {
                    this.bool_0 = true;
                    this._pPoint0 = this.method_2(X, Y);
                    switch (this.enum1_0)
                    {
                        case ToolSelectElement.Enum1.eSelecting:
                            this.inewEnvelopeFeedback_0.MoveTo(this._pPoint0);
                            break;
                        case ToolSelectElement.Enum1.eMoving:
                            this.imoveImageFeedback_0.MoveTo(this._pPoint0);
                            break;
                        case ToolSelectElement.Enum1.eResizing:
                            this.iresizeEnvelopeFeedback2_0.MoveTo(this._pPoint0);
                            break;
                        case ToolSelectElement.Enum1.eMovingAnchor:
                            this.icalloutFeedback_0.MoveAnchorTo(this._pPoint0);
                            break;
                        case ToolSelectElement.Enum1.eMovingcallout:
                            this.icalloutFeedback_0.MoveTo(this._pPoint0);
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            try
            {
                if (Button == 1)
                {
                    switch (this.enum1_0)
                    {
                        case ToolSelectElement.Enum1.eSelecting:
                            this._pPoint3.X = (double) X;
                            this._pPoint3.Y = (double) Y;
                            this.method_24(Shift);
                            ElementChangeEvent.ElementSelectChange();
                            break;
                        case ToolSelectElement.Enum1.eMoving:
                            this.imoveImageFeedback_0.Refresh(0);
                            this.imoveImageFeedback_0.ClearImage();
                            this.method_22(Shift);
                            break;
                        case ToolSelectElement.Enum1.eResizing:
                            this.method_33(this.ielement_0);
                            break;
                        case ToolSelectElement.Enum1.eMovingAnchor:
                            this.method_30(this.ielement_1);
                            break;
                        case ToolSelectElement.Enum1.eMovingcallout:
                            this.icalloutFeedback_0.Refresh(0);
                            this.icalloutFeedback_0.Stop();
                            this.method_22(Shift);
                            break;
                    }
                    if (this.enum1_0 != ToolSelectElement.Enum1.eDormant)
                    {
                        this.enum1_0 = ToolSelectElement.Enum1.eDormant;
                    }
                    this._pPoint2.X = 0.0;
                    this._pPoint2.Y = 0.0;
                    this._pPoint3.X = 0.0;
                    this._pPoint3.Y = 0.0;
                }
                else if (Button != 2)
                {
                }
            }
            catch
            {
            }
        }

        public override void OnKeyDown(int Button, int Shift)
        {
            try
            {
                if (Button == 16)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                }
                else if (Button == 17)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                }
                if (Button == 27)
                {
                    if (this.enum1_0 == ToolSelectElement.Enum1.eSelecting)
                    {
                        this.inewEnvelopeFeedback_0.Stop();
                    }
                    else if (this.enum1_0 == ToolSelectElement.Enum1.eMoving)
                    {
                        this.imoveImageFeedback_0.Refresh(0);
                        this.imoveImageFeedback_0.ClearImage();
                    }
                    else if (this.enum1_0 == ToolSelectElement.Enum1.eMovingAnchor ||
                             this.enum1_0 == ToolSelectElement.Enum1.eMovingcallout)
                    {
                        this.icalloutFeedback_0.Stop();
                    }
                    else if (this.enum1_0 == ToolSelectElement.Enum1.eResizing)
                    {
                        this.iresizeEnvelopeFeedback2_0.Stop();
                    }
                    this.enum1_0 = ToolSelectElement.Enum1.eDormant;
                }
                if (Button == 46)
                {
                    this.method_41();
                }
            }
            catch
            {
            }
        }

        public override void OnKeyUp(int Button, int Shift)
        {
            try
            {
                this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsNone;
                this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsNone;
                if (Button != 46)
                {
                }
            }
            catch
            {
            }
        }

        public override void OnDblClick()
        {
            IGraphicsContainerSelect graphicsContainerSelect = this.method_9();
            if (graphicsContainerSelect.ElementSelectionCount != 0)
            {
                IElement element = graphicsContainerSelect.SelectedElement(0);
                IEditElementPropertiesOperation editElementPropertiesOperation = new EditElementPropertiesOperation();
                editElementPropertiesOperation.ActiveView = this._context.ActiveView;
                editElementPropertiesOperation.Element = element;
                if (!(element is IOleFrame) && ElementChangeEvent.IsFireElementPropertyEditEvent)
                {
                    ElementChangeEvent.EditElementProperty(element);
                }
                else if (element is ITextElement)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "属性";
                    IPropertyPage propertyPage = new TextSetupCtrl();
                    propertySheet.AddPage(propertyPage);
                    propertyPage = new ElementSizeAndPositionCtrl();
                    propertySheet.AddPage(propertyPage);
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else if (element is IFractionTextElement)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "属性";
                    IPropertyPage propertyPage = new FractionTextSymbolPage();
                    (propertyPage as FractionTextSymbolPage).ActiveView = this._context.ActiveView;
                    propertySheet.AddPage(propertyPage);
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else if (element is IMapFrame)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "数据框 属性";
                    IPropertyPage propertyPage = new MapGeneralInfoCtrl();
                    propertySheet.AddPage(propertyPage);
                    propertyPage = new MapCoordinateCtrl();
                    propertySheet.AddPage(propertyPage);
                    propertyPage = new FrameProprtyPage();
                    propertySheet.AddPage(propertyPage);
                    IEnvelope envelope = element.Geometry.Envelope;
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else if (element is IPictureElement)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "图像 属性";
                    IPropertyPage propertyPage = new PicturePropertyPage();
                    propertySheet.AddPage(propertyPage);
                    propertyPage = new FrameProprtyPage();
                    propertySheet.AddPage(propertyPage);
                    IEnvelope envelope = element.Geometry.Envelope;
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else if (element is IMapSurroundFrame)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "属性";
                    IPropertyPage propertyPage;
                    if ((element as IMapSurroundFrame).MapSurround is INorthArrow)
                    {
                        propertyPage = new NorthArrowPropertyPage();
                        propertySheet.AddPage(propertyPage);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is IScaleBar)
                    {
                        propertyPage = new ScaleBarFormatPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        propertyPage = new ScaleAndUnitsPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        propertyPage = new NumberAndLabelPropertyPage();
                        propertySheet.AddPage(propertyPage);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is IScaleText)
                    {
                        propertyPage = new ScaleTextTextPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        propertyPage = new ScaleTextFormatPropertyPage();
                        propertySheet.AddPage(propertyPage);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is ILegend)
                    {
                        propertyPage = new LegendPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        propertyPage = new LegendItemPropertyPage();
                        propertySheet.AddPage(propertyPage);
                    }
                    propertyPage = new FrameProprtyPage();
                    propertySheet.AddPage(propertyPage);
                    IEnvelope envelope = element.Geometry.Envelope;
                    IElement arg_415_0 = (element as IClone).Clone() as IElement;
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else if (element is IFrameElement)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "属性";
                    IPropertyPage propertyPage = new ElementGeometryInfoPropertyPage();
                    propertySheet.AddPage(propertyPage);
                    propertyPage = new FrameProprtyPage();
                    propertySheet.AddPage(propertyPage);
                    IEnvelope envelope = element.Geometry.Envelope;
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else if (element is IFillShapeElement)
                {
                    IPropertySheet propertySheet = new frmElementProperty();
                    propertySheet.Title = "属性";
                    IPropertyPage propertyPage = new FillSymbolPropertyPage();
                    propertySheet.AddPage(propertyPage);
                    propertyPage = new ElementGeometryInfoPropertyPage();
                    propertySheet.AddPage(propertyPage);
                    IEnvelope envelope = element.Geometry.Envelope;
                    if (propertySheet.EditProperties(element))
                    {
                        IActiveView activeView = this._context.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._context.OperationStack.Do(editElementPropertiesOperation);
                    }
                }
                else
                {
                    if (element is LabelLineElementClass)
                    {
                        IPropertySheet propertySheet = new frmElementProperty();
                        propertySheet.Title = "属性";
                        IPropertyPage propertyPage = new LabelLineElementProperty();
                        propertySheet.AddPage(propertyPage);
                        IEnvelope envelope = element.Geometry.Envelope;
                        try
                        {
                            if (propertySheet.EditProperties(element))
                            {
                                IActiveView activeView = this._context.ActiveView;
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                                activeView.GraphicsContainer.UpdateElement(element);
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                                this._context.OperationStack.Do(editElementPropertiesOperation);
                            }
                            return;
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }
                    if (element is ILineElement)
                    {
                        IPropertySheet propertySheet = new frmElementProperty();
                        propertySheet.Title = "属性";
                        IPropertyPage propertyPage = new LineSymbolPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        IEnvelope envelope = element.Geometry.Envelope;
                        if (propertySheet.EditProperties(element))
                        {
                            IActiveView activeView = this._context.ActiveView;
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                            activeView.GraphicsContainer.UpdateElement(element);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                            this._context.OperationStack.Do(editElementPropertiesOperation);
                        }
                    }
                    else if (element is IMarkerElement)
                    {
                        IPropertySheet propertySheet = new frmElementProperty();
                        propertySheet.Title = "属性";
                        IPropertyPage propertyPage = new MarkerElementPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        IEnvelope envelope = element.Geometry.Envelope;
                        if (propertySheet.EditProperties(element))
                        {
                            IActiveView activeView = this._context.ActiveView;
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                            activeView.GraphicsContainer.UpdateElement(element);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                            this._context.OperationStack.Do(editElementPropertiesOperation);
                        }
                    }
                    else if (element is IJTBElement)
                    {
                        frmJTBElement frmJTBElement = new frmJTBElement();
                        frmJTBElement.JTBElement = (element as IJTBElement);
                        IEnvelope envelope = element.Geometry.Envelope;
                        if (frmJTBElement.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            IActiveView activeView = this._context.ActiveView;
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                            activeView.GraphicsContainer.UpdateElement(element);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                            this._context.OperationStack.Do(editElementPropertiesOperation);
                        }
                    }
                    else if (element is ICustomLegend)
                    {
                        IPropertySheet propertySheet = new frmElementProperty();
                        propertySheet.Title = "属性";
                        IPropertyPage propertyPage = new TLConfigPropertyPage();
                        propertySheet.AddPage(propertyPage);
                        IEnvelope envelope = element.Geometry.Envelope;
                        if (propertySheet.EditProperties(element))
                        {
                            IPoint upperLeft = element.Geometry.Envelope.UpperLeft;
                            IActiveView activeView = this._context.ActiveView;
                            (element as CustomLegend).Init(activeView, upperLeft);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                            activeView.GraphicsContainer.UpdateElement(element);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                            this._context.OperationStack.Do(editElementPropertiesOperation);
                        }
                    }
                    else if (element is IOleFrame)
                    {
                        (element as IOleFrame).OLEEditComplete += new OLEEditCompleteHandler(this.method_0);
                        IActiveView activeView = this._context.ActiveView;
                        (element as IOleFrame).Edit(activeView.ScreenDisplay.hWnd);
                    }
                }
            }
        }

        private void method_0(IOleFrame ioleFrame_0)
        {
            IActiveView activeView = this._context.ActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ioleFrame_0, null);
            ioleFrame_0.OLEEditComplete -= new OLEEditCompleteHandler(this.method_0);
        }

        private int method_1(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            int num = -1;
            int result;
            try
            {
                if (igraphicsContainerSelect_0 == null)
                {
                    return -1;
                }
                if (this._pPoint0 == null)
                {
                    return -1;
                }
                if (this._pPoint0.IsEmpty)
                {
                    return -1;
                }
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                num = 0;
                for (int i = 0; i <= elementSelectionCount - 1; i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        this.method_40(element, elementSelectionCount);
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            int num2 = selectionTracker.QueryCursor(this._pPoint0);
                            if (num2 != 0)
                            {
                                return num2;
                            }
                        }
                    }
                }
                return 0;
            }
            catch
            {
            }
            result = num;
            return result;
        }

        private IPoint method_2(int Button, int Shift)
        {
            IPoint result;
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IScreenDisplay screenDisplay = activeView.ScreenDisplay;
                IPoint point = screenDisplay.DisplayTransformation.ToMapPoint(Button, Shift);
                result = point;
                return result;
            }
            catch
            {
            }
            result = null;
            return result;
        }

        private bool method_3(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            bool result;
            try
            {
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                if (elementSelectionCount != 1)
                {
                    result = false;
                    return result;
                }
                IGeometry geometry = this.method_15(this._pPoint0);
                for (int i = 0; i < elementSelectionCount; i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    IPoint point;
                    if (!element.Locked && this.method_21(element, out point) && point != null)
                    {
                        IGeometry other = this.method_15(point);
                        IRelationalOperator relationalOperator = geometry as IRelationalOperator;
                        if (!relationalOperator.Disjoint(other))
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                result = false;
                return result;
            }
            catch
            {
            }
            result = false;
            return result;
        }

        private bool method_4(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            bool result;
            try
            {
                IEnvelope envelope = this.method_15(this._pPoint0) as IEnvelope;
                IPolygon polygon = new Polygon() as IPolygon;
                IEnumElement selectedElements = igraphicsContainerSelect_0.SelectedElements;
                selectedElements.Reset();
                for (IElement element = selectedElements.Next(); element != null; element = selectedElements.Next())
                {
                    if (!element.Locked)
                    {
                        element.QueryOutline(this._context.ActiveView.ScreenDisplay, polygon);
                        IRelationalOperator relationalOperator = envelope as IRelationalOperator;
                        if (relationalOperator != null && !relationalOperator.Disjoint(polygon))
                        {
                            result = true;
                            return result;
                        }
                    }
                }
            }
            catch
            {
            }
            result = false;
            return result;
        }

        private void method_5(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IDisplayFeedback displayFeedback = this.imoveImageFeedback_0;
                displayFeedback.Display = this._context.ActiveView.ScreenDisplay;
                this.method_18(igraphicsContainerSelect_0, this.imoveImageFeedback_0, this.igeometry_0);
                this.enum1_0 = ToolSelectElement.Enum1.eMoving;
                this.imoveImageFeedback_0.Start(this._pPoint0);
            }
            catch
            {
            }
        }

        private void method_6(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IClone clone = this._pPoint0 as IClone;
                this.igeometry_0 = (clone.Clone() as IGeometry);
                this.enum1_0 = ToolSelectElement.Enum1.eMovingAnchor;
                this.method_19(igraphicsContainerSelect_0);
            }
            catch
            {
            }
        }

        private void method_7()
        {
            try
            {
                this.inewEnvelopeFeedback_0.Display = this._context.ActiveView.ScreenDisplay;
                this.enum1_0 = ToolSelectElement.Enum1.eSelecting;
                this.inewEnvelopeFeedback_0.Start(this._pPoint0);
            }
            catch
            {
            }
        }

        private ISelection method_8()
        {
            ISelection result;
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer == null)
                {
                    result = null;
                    return result;
                }
                IViewManager viewManager = activeView as IViewManager;
                ISelection elementSelection = viewManager.ElementSelection;
                result = elementSelection;
                return result;
            }
            catch
            {
            }
            result = null;
            return result;
        }

        private IGraphicsContainerSelect method_9()
        {
            IGraphicsContainerSelect result;
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer == null)
                {
                    result = null;
                    return result;
                }
                IViewManager viewManager = activeView as IViewManager;
                ISelection elementSelection = viewManager.ElementSelection;
                activeView.Selection = elementSelection;
                result = (graphicsContainer as IGraphicsContainerSelect);
                return result;
            }
            catch
            {
            }
            result = null;
            return result;
        }

        private bool method_10(IGraphicsContainerSelect igraphicsContainerSelect_0, int Button, int Shift)
        {
            bool result;
            try
            {
                IPoint point = this.method_2(Button, Shift);
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                for (int i = 0; i <= elementSelectionCount - 1; i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            esriTrackerLocation esriTrackerLocation = selectionTracker.HitTest(point);
                            if (esriTrackerLocation != esriTrackerLocation.LocationInterior &&
                                esriTrackerLocation != esriTrackerLocation.LocationNone)
                            {
                                this.method_12(esriTrackerLocation);
                                result = true;
                                return result;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            result = false;
            return result;
        }

        private bool method_11(IGraphicsContainerSelect igraphicsContainerSelect_0, int X, int Y)
        {
            bool result;
            try
            {
                IPoint point = this.method_2(X, Y);
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                for (int i = 0; i <= elementSelectionCount - 1; i++)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                    if (!element.Locked)
                    {
                        ISelectionTracker selectionTracker = element.SelectionTracker;
                        if (selectionTracker != null)
                        {
                            IDisplayFeedback displayFeedback = new ResizeEnvelopeFeedback();
                            selectionTracker.QueryResizeFeedback(ref displayFeedback);
                            if (displayFeedback != null && displayFeedback is IResizeEnvelopeFeedback)
                            {
                                esriTrackerLocation trackerLocation = selectionTracker.HitTest(point);
                                IResizeEnvelopeFeedback resizeEnvelopeFeedback =
                                    displayFeedback as IResizeEnvelopeFeedback;
                                switch (trackerLocation)
                                {
                                    case esriTrackerLocation.LocationTopLeft:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                                        break;
                                    case esriTrackerLocation.LocationTopMiddle:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopMiddle;
                                        break;
                                    case esriTrackerLocation.LocationTopRight:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopRight;
                                        break;
                                    case esriTrackerLocation.LocationMiddleLeft:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleLeft;
                                        break;
                                    case esriTrackerLocation.LocationMiddleRight:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleRight;
                                        break;
                                    case esriTrackerLocation.LocationBottomLeft:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomLeft;
                                        break;
                                    case esriTrackerLocation.LocationBottomMiddle:
                                        resizeEnvelopeFeedback.ResizeEdge =
                                            esriEnvelopeEdge.esriEnvelopeEdgeBottomMiddle;
                                        break;
                                    case esriTrackerLocation.LocationBottomRight:
                                        resizeEnvelopeFeedback.ResizeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomRight;
                                        break;
                                }
                                IBoundsProperties boundsProperties = element as IBoundsProperties;
                                esriEnvelopeConstraints esriEnvelopeConstraints;
                                if (boundsProperties.FixedAspectRatio)
                                {
                                    esriEnvelopeConstraints = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                                    this.iresizeEnvelopeFeedback2_0.Constraint =
                                        esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                                }
                                else
                                {
                                    esriEnvelopeConstraints = resizeEnvelopeFeedback.Constraint;
                                    this.iresizeEnvelopeFeedback2_0.Constraint = esriEnvelopeConstraints;
                                }
                                if (esriEnvelopeConstraints == esriEnvelopeConstraints.esriEnvelopeConstraintsAspect)
                                {
                                    this.iresizeEnvelopeFeedback2_0.AspectRatio = resizeEnvelopeFeedback.AspectRatio;
                                }
                            }
                            esriTrackerLocation esriTrackerLocation2 = selectionTracker.HitTest(point);
                            if (esriTrackerLocation2 != esriTrackerLocation.LocationInterior &&
                                esriTrackerLocation2 != esriTrackerLocation.LocationNone)
                            {
                                this.method_13(element, esriTrackerLocation2, point);
                                result = true;
                                return result;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            result = false;
            return result;
        }

        private void method_12(esriTrackerLocation esriTrackerLocation_0)
        {
            switch (esriTrackerLocation_0)
            {
                case esriTrackerLocation.LocationTopLeft:
                case esriTrackerLocation.LocationBottomRight:
                    this.m_cursor =
                        new System.Windows.Forms.Cursor(
                            base.GetType().Assembly.GetManifestResourceStream("JLK.Pluge.Bitmaps.trcknwse.cur"));
                    break;
                case esriTrackerLocation.LocationTopMiddle:
                case esriTrackerLocation.LocationBottomMiddle:
                    this.m_cursor =
                        new System.Windows.Forms.Cursor(
                            base.GetType().Assembly.GetManifestResourceStream("JLK.Pluge.Bitmaps.trckns.cur"));
                    break;
                case esriTrackerLocation.LocationTopRight:
                case esriTrackerLocation.LocationBottomLeft:
                    this.m_cursor =
                        new System.Windows.Forms.Cursor(
                            base.GetType().Assembly.GetManifestResourceStream("JLK.Pluge.Bitmaps.trcknesw.cur"));
                    break;
                case esriTrackerLocation.LocationMiddleLeft:
                case esriTrackerLocation.LocationMiddleRight:
                    this.m_cursor =
                        new System.Windows.Forms.Cursor(
                            base.GetType().Assembly.GetManifestResourceStream("JLK.Pluge.Bitmaps.trckwe.cur"));
                    break;
                default:
                    this.m_cursor = System.Windows.Forms.Cursors.Default;
                    break;
            }
        }

        private void method_13(IElement ielement_2, esriTrackerLocation esriTrackerLocation_0, IPoint ipoint_4)
        {
            try
            {
                bool flag = false;
                if (ielement_2 is IBoundsProperties)
                {
                    IBoundsProperties boundsProperties = ielement_2 as IBoundsProperties;
                    flag = boundsProperties.FixedSize;
                }
                IGeometry envelope2;
                if (flag)
                {
                    IEnvelope envelope = new Envelope() as IEnvelope;
                    ielement_2.QueryBounds(this._context.ActiveView.ScreenDisplay, envelope);
                    envelope2 = envelope;
                }
                else
                {
                    envelope2 = ielement_2.Geometry;
                }
                this.iresizeEnvelopeFeedback2_0.Display = this._context.ActiveView.ScreenDisplay;
                esriEnvelopeEdge resizeEdge = this.method_14(esriTrackerLocation_0);
                this.iresizeEnvelopeFeedback2_0.ResizeEdge = resizeEdge;
                this.enum1_0 = ToolSelectElement.Enum1.eResizing;
                this.ielement_0 = ielement_2;
                this.iresizeEnvelopeFeedback2_0.Start(envelope2, this._pPoint0);
            }
            catch
            {
            }
        }

        private esriEnvelopeEdge method_14(esriTrackerLocation esriTrackerLocation_0)
        {
            esriEnvelopeEdge esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
            esriEnvelopeEdge result;
            try
            {
                switch (esriTrackerLocation_0)
                {
                    case esriTrackerLocation.LocationTopLeft:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                        break;
                    case esriTrackerLocation.LocationTopMiddle:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopMiddle;
                        break;
                    case esriTrackerLocation.LocationTopRight:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopRight;
                        break;
                    case esriTrackerLocation.LocationMiddleLeft:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleLeft;
                        break;
                    case esriTrackerLocation.LocationMiddleRight:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeMiddleRight;
                        break;
                    case esriTrackerLocation.LocationBottomLeft:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomLeft;
                        break;
                    case esriTrackerLocation.LocationBottomMiddle:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomMiddle;
                        break;
                    case esriTrackerLocation.LocationBottomRight:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeBottomRight;
                        break;
                    default:
                        esriEnvelopeEdge = esriEnvelopeEdge.esriEnvelopeEdgeTopLeft;
                        break;
                }
                result = esriEnvelopeEdge;
                return result;
            }
            catch
            {
            }
            result = esriEnvelopeEdge;
            return result;
        }

        private IGeometry method_15(IPoint ipoint_4)
        {
            IGeometry geometry = null;
            IGeometry result;
            try
            {
                double num = this.method_16();
                IEnvelope envelope = new Envelope() as IEnvelope;
                double num2;
                double num3;
                ipoint_4.QueryCoords(out num2, out num3);
                envelope.PutCoords(num2 - num, num3 - num, num2 + num, num3 + num);
                geometry = envelope;
                result = geometry;
                return result;
            }
            catch
            {
            }
            result = geometry;
            return result;
        }

        private double method_16()
        {
            double num = 0.0;
            double result;
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IScreenDisplay screenDisplay = activeView.ScreenDisplay;
                IDisplayTransformation displayTransformation = screenDisplay.DisplayTransformation;
                tagPOINT tagPOINT = default(tagPOINT);
                WKSPoint wKSPoint = default(WKSPoint);
                tagPOINT.x = 3;
                tagPOINT.y = 3;
                displayTransformation.TransformCoords(ref wKSPoint, ref tagPOINT, 1, 6);
                num = wKSPoint.X;
                result = num;
                return result;
            }
            catch
            {
            }
            result = num;
            return result;
        }

        private void method_17(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IActiveView arg_0C_0 = this._context.ActiveView;
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                if (elementSelectionCount == 1)
                {
                    IElement ielement_ = igraphicsContainerSelect_0.SelectedElement(0);
                    ICallout callout;
                    if (this.method_31(ielement_, out callout))
                    {
                        this.method_32(ielement_, null);
                    }
                }
            }
            catch
            {
            }
        }

        private void method_18(IGraphicsContainerSelect igraphicsContainerSelect_0,
            IMoveImageFeedback imoveImageFeedback_1, IGeometry igeometry_1)
        {
            try
            {
                IDisplay display = imoveImageFeedback_1.Display_2;
                int elementSelectionCount = igraphicsContainerSelect_0.ElementSelectionCount;
                IPolygon polygon = new Polygon() as IPolygon;
                IPolygon polygon2 = new Polygon() as IPolygon;
                if (elementSelectionCount == 1)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(0);
                    element.QueryOutline(display, polygon);
                }
                else
                {
                    for (int i = elementSelectionCount - 1; i >= 0; i--)
                    {
                        IElement element = igraphicsContainerSelect_0.SelectedElement(i);
                        element.QueryOutline(display, polygon2);
                        ITopologicalOperator topologicalOperator = polygon2 as ITopologicalOperator;
                        topologicalOperator.Simplify();
                        topologicalOperator = (polygon as ITopologicalOperator);
                        IGeometry geometry = topologicalOperator.Union(polygon2);
                        if (geometry != null)
                        {
                            polygon = (geometry as IPolygon);
                        }
                    }
                }
                IClone clone = polygon as IClone;
                igeometry_1 = (clone.Clone() as IGeometry);
                for (int j = elementSelectionCount - 1; j >= 0; j--)
                {
                    IElement element = igraphicsContainerSelect_0.SelectedElement(j);
                    if (element is IMapFrame)
                    {
                        IMapFrame imapFrame_ = element as IMapFrame;
                        this.method_35(imapFrame_, display);
                    }
                    else
                    {
                        element.Draw(display, null);
                    }
                }
                IMoveImageFeedback2 moveImageFeedback = imoveImageFeedback_1 as IMoveImageFeedback2;
                moveImageFeedback.PolygonBounds = polygon;
            }
            catch
            {
            }
        }

        private bool method_19(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            bool flag = false;
            bool result;
            try
            {
                flag = false;
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer arg_16_0 = activeView.GraphicsContainer;
                ISymbol symbol = null;
                IGeometry geometry = null;
                if (igraphicsContainerSelect_0.ElementSelectionCount == 1)
                {
                    IElement element = this.method_20(this._pPoint0);
                    IPoint point;
                    if (element != null && this.method_21(element, out point))
                    {
                        this.icalloutFeedback_0.Display = activeView.ScreenDisplay;
                        if (this.method_29(element, symbol, geometry))
                        {
                            this.icalloutFeedback_0.Start(symbol, geometry, this._pPoint0);
                            this.icalloutFeedback_0.MoveTo(this._pPoint0);
                            this.ielement_1 = element;
                            flag = true;
                        }
                    }
                }
                result = flag;
                return result;
            }
            catch
            {
            }
            result = flag;
            return result;
        }

        private IElement method_20(IPoint ipoint_4)
        {
            IElement element = null;
            IElement result;
            try
            {
                double tolerance = this.method_16();
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainerSelect graphicsContainerSelect = activeView as IGraphicsContainerSelect;
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
                IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                if (selectedElements == null)
                {
                    result = element;
                    return result;
                }
                for (IElement element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                {
                    if (element2.HitTest(ipoint_4.X, ipoint_4.Y, tolerance))
                    {
                        element = element2;
                        result = element;
                        return result;
                    }
                }
                result = element;
                return result;
            }
            catch
            {
            }
            result = element;
            return result;
        }

        private bool method_21(IElement ielement_2, out IPoint ipoint_4)
        {
            ipoint_4 = null;
            bool result;
            try
            {
                ICallout callout = null;
                if (this.method_31(ielement_2, out callout))
                {
                    ipoint_4 = callout.AnchorPoint;
                    if (ipoint_4 != null)
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
                return result;
            }
            catch
            {
            }
            result = false;
            return result;
        }

        private void method_22(int Button)
        {
            try
            {
                if (this._pPoint1.X == this._pPoint0.X && this._pPoint1.Y == this._pPoint0.Y)
                {
                    if (Button == 1 || Button == 2)
                    {
                        this.method_34(Button);
                    }
                    else
                    {
                        this.method_36();
                    }
                }
                else if (Button != 2)
                {
                    this.method_23(this._pPoint0, this._pPoint1);
                }
            }
            catch
            {
            }
        }

        private void method_23(IPoint ipoint_4, IPoint ipoint_5)
        {
            IMoveElementOperation moveElementOperation = new MoveElementOperation();
            double x = ipoint_4.X - ipoint_5.X;
            double y = ipoint_4.Y - ipoint_5.Y;
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords(x, y);
            moveElementOperation.Point = point;
            IActiveView activeView = this._context.ActiveView;
            moveElementOperation.ActiveView = activeView;
            moveElementOperation.Elements = this.method_9().SelectedElements;
            this._context.OperationStack.Do(moveElementOperation);
        }

        private void method_24(int Button)
        {
            try
            {
                IEnvelope envelope = this.inewEnvelopeFeedback_0.Stop();
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                ISpatialReference spatialReference = null;
                if (activeView is IMap)
                {
                    IMap map = activeView as IMap;
                    spatialReference = map.SpatialReference;
                }
                if (spatialReference != null)
                {
                    this._pPoint0.SpatialReference = spatialReference;
                    envelope.SpatialReference = spatialReference;
                }
                bool flag = false;
                int num;
                int num2;
                if (this._pPoint2.X >= this._pPoint3.X)
                {
                    num = (int) this._pPoint3.X;
                    num2 = (int) this._pPoint2.X;
                }
                else
                {
                    num2 = (int) this._pPoint3.X;
                    num = (int) this._pPoint2.X;
                }
                int num3;
                int num4;
                if (this._pPoint2.Y >= this._pPoint3.Y)
                {
                    num3 = (int) this._pPoint3.Y;
                    num4 = (int) this._pPoint2.Y;
                }
                else
                {
                    num4 = (int) this._pPoint3.Y;
                    num3 = (int) this._pPoint2.Y;
                }
                IEnumElement enumElement;
                if (num2 - num <= 4 && num4 - num3 <= 4)
                {
                    double tolerance = this.method_16();
                    enumElement = graphicsContainer.LocateElements(this._pPoint0, tolerance);
                    flag = true;
                }
                else
                {
                    enumElement = graphicsContainer.LocateElementsByEnvelope(envelope);
                }
                IGraphicsContainerSelect graphicsContainerSelect = this.method_9();
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                if (enumElement == null)
                {
                    graphicsContainerSelect.UnselectAllElements();
                }
                else
                {
                    IMapFrame mapFrame = null;
                    if (Button > 0 && flag)
                    {
                        this.method_27(Button, enumElement, graphicsContainerSelect);
                    }
                    else
                    {
                        graphicsContainerSelect.UnselectAllElements();
                        enumElement.Reset();
                        for (IElement element = enumElement.Next(); element != null; element = enumElement.Next())
                        {
                            if (element is IMapFrame)
                            {
                                mapFrame = (element as IMapFrame);
                            }
                            else
                            {
                                mapFrame = null;
                            }
                            this.method_25(graphicsContainerSelect, element);
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (mapFrame != null)
                        {
                            this.method_26(mapFrame as IElement);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void method_25(IGraphicsContainerSelect igraphicsContainerSelect_0, IElement ielement_2)
        {
            try
            {
                igraphicsContainerSelect_0.SelectElement(ielement_2);
                igraphicsContainerSelect_0.DominantElement = ielement_2;
                this._context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
            }
            catch
            {
            }
        }

        private void method_26(IElement ielement_2)
        {
            try
            {
                if (ielement_2 is IMapFrame)
                {
                    IMapFrame mapFrame = ielement_2 as IMapFrame;
                    IActiveView activeView = this._context.ActiveView;
                    IMap map = mapFrame.Map;
                    activeView.FocusMap = map;
                }
            }
            catch
            {
            }
        }

        private void method_27(int Button, IEnumElement ienumElement_0,
            IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IElement element;
                bool flag = this.method_28(ienumElement_0, igraphicsContainerSelect_0, out element);
                if (element != null)
                {
                    IActiveView activeView = this._context.ActiveView;
                    if (flag)
                    {
                        if (Button == 2)
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
                        this.method_25(igraphicsContainerSelect_0, element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                    }
                }
            }
            catch
            {
            }
        }

        private bool method_28(IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0,
            out IElement ielement_2)
        {
            bool flag = false;
            ielement_2 = null;
            bool result;
            try
            {
                ielement_2 = null;
                ienumElement_0.Reset();
                IElement element = ienumElement_0.Next();
                if (element == null)
                {
                    flag = false;
                    result = false;
                    return result;
                }
                ielement_2 = element;
                flag = igraphicsContainerSelect_0.ElementSelected(element);
                result = flag;
                return result;
            }
            catch
            {
            }
            result = flag;
            return result;
        }

        private bool method_29(IElement ielement_2, ISymbol isymbol_0, IGeometry igeometry_1)
        {
            bool flag = false;
            bool result;
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextElement textElement = ielement_2 as ITextElement;
                    ITextSymbol symbol = textElement.Symbol;
                    if (!(symbol is IFormattedTextSymbol))
                    {
                        flag = false;
                        result = false;
                        return result;
                    }
                    isymbol_0 = (symbol as ISymbol);
                }
                else
                {
                    if (!(ielement_2 is IMarkerElement))
                    {
                        flag = false;
                        result = false;
                        return result;
                    }
                    IMarkerElement markerElement = ielement_2 as IMarkerElement;
                    IMarkerSymbol symbol2 = markerElement.Symbol;
                    if (!(markerElement is IMarkerBackgroundSupport))
                    {
                        flag = false;
                        result = false;
                        return result;
                    }
                    isymbol_0 = (symbol2 as ISymbol);
                }
                igeometry_1 = ielement_2.Geometry;
                flag = true;
                result = true;
                return result;
            }
            catch
            {
            }
            result = flag;
            return result;
        }

        private void method_30(IElement ielement_2)
        {
            try
            {
                this.icalloutFeedback_0.Stop();
                ICallout callout;
                if (this.method_31(ielement_2, out callout))
                {
                    callout.AnchorPoint = this._pPoint0;
                    this.method_32(ielement_2, callout);
                }
            }
            catch
            {
            }
        }

        private bool method_31(IElement ielement_2, out ICallout icallout_0)
        {
            icallout_0 = null;
            bool result;
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextElement textElement = ielement_2 as ITextElement;
                    ITextSymbol symbol = textElement.Symbol;
                    if (!(symbol is IFormattedTextSymbol))
                    {
                        result = false;
                        return result;
                    }
                    IFormattedTextSymbol formattedTextSymbol = symbol as IFormattedTextSymbol;
                    ITextBackground background = formattedTextSymbol.Background;
                    if (background == null)
                    {
                        result = false;
                        return result;
                    }
                    if (!(background is ICallout))
                    {
                        result = false;
                        return result;
                    }
                    icallout_0 = (background as ICallout);
                    result = true;
                    return result;
                }
                else if (ielement_2 is IMarkerElement)
                {
                    IMarkerElement markerElement = ielement_2 as IMarkerElement;
                    IMarkerSymbol symbol2 = markerElement.Symbol;
                    if (!(symbol2 is IMarkerBackgroundSupport))
                    {
                        result = false;
                        return result;
                    }
                    IMarkerBackgroundSupport markerBackgroundSupport = symbol2 as IMarkerBackgroundSupport;
                    IMarkerBackground background2 = markerBackgroundSupport.Background;
                    if (background2 == null)
                    {
                        result = false;
                        return result;
                    }
                    if (!(background2 is ICallout))
                    {
                        result = false;
                        return result;
                    }
                    icallout_0 = (background2 as ICallout);
                    result = true;
                    return result;
                }
            }
            catch
            {
            }
            result = false;
            return result;
        }

        private void method_32(IElement ielement_2, ICallout icallout_0)
        {
            try
            {
                if (ielement_2 is ITextElement)
                {
                    ITextElement textElement = ielement_2 as ITextElement;
                    ITextSymbol symbol = textElement.Symbol;
                    if (symbol is IFormattedTextSymbol)
                    {
                        IFormattedTextSymbol formattedTextSymbol = symbol as IFormattedTextSymbol;
                        formattedTextSymbol.Background = null;
                        if (icallout_0 != null)
                        {
                            ITextBackground background = icallout_0 as ITextBackground;
                            formattedTextSymbol.Background = background;
                        }
                        textElement.Symbol = symbol;
                    }
                }
                else if (ielement_2 is IMarkerElement)
                {
                    IMarkerElement markerElement = ielement_2 as IMarkerElement;
                    IMarkerSymbol symbol2 = markerElement.Symbol;
                    if (symbol2 is IMarkerBackgroundSupport)
                    {
                        IMarkerBackgroundSupport markerBackgroundSupport = symbol2 as IMarkerBackgroundSupport;
                        markerBackgroundSupport.Background = null;
                        if (icallout_0 != null)
                        {
                            IMarkerBackground background2 = icallout_0 as IMarkerBackground;
                            markerBackgroundSupport.Background = background2;
                        }
                        markerElement.Symbol = symbol2;
                    }
                }
            }
            catch
            {
            }
        }

        private void method_33(IElement ielement_2)
        {
            try
            {
                IResizeEnvelopeFeedback2 resizeEnvelopeFeedback = this.iresizeEnvelopeFeedback2_0;
                IGeometry geometry = resizeEnvelopeFeedback.Stop();
                if (this.bool_0)
                {
                    if (geometry != null)
                    {
                        this._context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
                        IResizeElementOperation resizeElementOperation = new ResizeElementOperation();
                        resizeElementOperation.Element = ielement_2;
                        resizeElementOperation.Geometry = geometry;
                        this._context.OperationStack.Do(resizeElementOperation);
                        IGraphicsContainer graphicsContainer = this._context.ActiveView.GraphicsContainer;
                        graphicsContainer.UpdateElement(ielement_2);
                        this._context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ielement_2, null);
                    }
                }
            }
            catch (Exception exception_)
            {
                CErrorLog.writeErrorLog(this, exception_, "");
            }
        }

        private void method_34(int Button)
        {
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer is IMap)
                {
                    IMap map = activeView as IMap;
                    ISpatialReference spatialReference = map.SpatialReference;
                    if (spatialReference != null)
                    {
                        this._pPoint0.SpatialReference = spatialReference;
                    }
                }
                double tolerance = this.method_16();
                IEnumElement enumElement = graphicsContainer.LocateElements(this._pPoint0, tolerance);
                if (enumElement != null)
                {
                    this.method_27(Button, enumElement, graphicsContainer as IGraphicsContainerSelect);
                }
            }
            catch
            {
            }
        }

        private void method_35(IMapFrame imapFrame_0, IDisplay idisplay_0)
        {
            try
            {
                IFrameDraw frameDraw = null;
                if (imapFrame_0 is IFrameDraw)
                {
                    frameDraw = (imapFrame_0 as IFrameDraw);
                    frameDraw.DrawBackground(idisplay_0, null);
                }
                IMap map = imapFrame_0.Map;
                IActiveView activeView = map as IActiveView;
                IScreenDisplay screenDisplay = activeView.ScreenDisplay;
                int hDC = idisplay_0.hDC;
                tagRECT tagRECT = default(tagRECT);
                tagRECT deviceFrame = screenDisplay.DisplayTransformation.get_DeviceFrame();
                screenDisplay.DrawCache(hDC, -3, ref deviceFrame, ref tagRECT);
                if (frameDraw != null)
                {
                    frameDraw.DrawForeground(idisplay_0, null);
                }
            }
            catch
            {
            }
        }

        private void method_36()
        {
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (activeView is IMap)
                {
                    IMap map = activeView as IMap;
                    ISpatialReference spatialReference = map.SpatialReference;
                    if (spatialReference != null)
                    {
                        this._pPoint0.SpatialReference = spatialReference;
                    }
                }
                double tolerance = this.method_16();
                IEnumElement enumElement = graphicsContainer.LocateElements(this._pPoint0, tolerance);
                if (enumElement != null)
                {
                    IGraphicsContainerSelect graphicsContainerSelect = this.method_9();
                    if (graphicsContainerSelect != null)
                    {
                        IElement element = graphicsContainerSelect.SelectedElement(0);
                        if (element != null)
                        {
                            if (this.method_38(element, enumElement))
                            {
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                                graphicsContainerSelect.UnselectAllElements();
                                this.method_37(enumElement, graphicsContainerSelect, element);
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

        private void method_37(IEnumElement ienumElement_0, IGraphicsContainerSelect igraphicsContainerSelect_0,
            IElement ielement_2)
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
                while (element != null && !flag2)
                {
                    if (flag)
                    {
                        this.method_25(igraphicsContainerSelect_0, element);
                        flag2 = true;
                    }
                    if (this.method_39(element, ielement_2))
                    {
                        flag = true;
                    }
                    element = ienumElement_0.Next();
                }
                if (!flag2)
                {
                    ienumElement_0.Reset();
                    element = ienumElement_0.Next();
                    this.method_25(igraphicsContainerSelect_0, element);
                }
            }
            catch
            {
            }
        }

        private bool method_38(IElement ielement_2, IEnumElement ienumElement_0)
        {
            bool flag = false;
            bool result;
            try
            {
                ienumElement_0.Reset();
                for (IElement element = ienumElement_0.Next(); element != null; element = ienumElement_0.Next())
                {
                    if (this.method_39(element, ielement_2))
                    {
                        flag = true;
                        result = true;
                        return result;
                    }
                }
                flag = false;
                result = false;
                return result;
            }
            catch
            {
            }
            result = flag;
            return result;
        }

        private bool method_39(IElement ielement_2, IElement ielement_3)
        {
            bool flag = false;
            bool result;
            try
            {
                if (ielement_2 == ielement_3)
                {
                    flag = true;
                    result = true;
                    return result;
                }
                result = flag;
                return result;
            }
            catch
            {
            }
            result = flag;
            return result;
        }

        private void method_40(IElement ielement_2, int Button)
        {
            try
            {
                if (ielement_2 is IGroupElement)
                {
                    IGroupElement groupElement = ielement_2 as IGroupElement;
                    IEnumElement elements = groupElement.Elements;
                    if (elements != null)
                    {
                        elements.Reset();
                        for (IElement element = elements.Next(); element != null; element = elements.Next())
                        {
                            this.method_40(element, 0);
                        }
                    }
                }
                else if (ielement_2 is IElementEditCallout)
                {
                    IElementEditCallout elementEditCallout = ielement_2 as IElementEditCallout;
                    elementEditCallout.EditingCallout = (Button == 1);
                }
            }
            catch
            {
            }
        }

        private bool method_41()
        {
            IActiveView activeView = this._context.ActiveView;
            IDeleteElementOperation deleteElementOperation = new DeleteElementOperation();
            deleteElementOperation.ActiveView = activeView;
            deleteElementOperation.Elements = this.method_9().SelectedElements;
            this._context.OperationStack.Do(deleteElementOperation);
            return true;
        }

        private void method_42(string string_0)
        {
            IActiveView activeView = this._context.ActiveView;
            if (activeView != null && activeView is IPageLayout)
            {
                IGraphicsContainer graphicsContainer = activeView as IGraphicsContainer;
                if (graphicsContainer != null)
                {
                    IGraphicsContainerSelect graphicsContainerSelect = this.method_9();
                    if (graphicsContainerSelect != null)
                    {
                        int elementSelectionCount = graphicsContainerSelect.ElementSelectionCount;
                        if (elementSelectionCount >= 1)
                        {
                            IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                            if (selectedElements != null)
                            {
                                if (string_0 != null)
                                {
                                    if (!(string_0 == "Top"))
                                    {
                                        if (!(string_0 == "Privious"))
                                        {
                                            if (!(string_0 == "Next"))
                                            {
                                                if (!(string_0 == "Bottom"))
                                                {
                                                    if (!(string_0 == "FromGroup"))
                                                    {
                                                        if (string_0 == "ToGroup")
                                                        {
                                                            if (elementSelectionCount < 2)
                                                            {
                                                                return;
                                                            }
                                                            IGroupElement groupElement =
                                                                new GroupElement() as IGroupElement;
                                                            selectedElements.Reset();
                                                            for (IElement element = selectedElements.Next();
                                                                element != null;
                                                                element = selectedElements.Next())
                                                            {
                                                                graphicsContainer.MoveElementToGroup(element,
                                                                    groupElement);
                                                            }
                                                            graphicsContainer.AddElement(groupElement as IElement, 0);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        selectedElements.Reset();
                                                        for (IElement element = selectedElements.Next();
                                                            element != null;
                                                            element = selectedElements.Next())
                                                        {
                                                            if (element is IGroupElement)
                                                            {
                                                                IGroupElement groupElement = element as IGroupElement;
                                                                for (int i = groupElement.ElementCount - 1;
                                                                    i <= 0;
                                                                    i += -1)
                                                                {
                                                                    IElement element2 = groupElement.get_Element(i);
                                                                    graphicsContainer.MoveElementFromGroup(
                                                                        groupElement, element2, i);
                                                                }
                                                                graphicsContainer.DeleteElement(element);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    graphicsContainer.SendToBack(selectedElements);
                                                }
                                            }
                                            else
                                            {
                                                graphicsContainer.SendBackward(selectedElements);
                                            }
                                        }
                                        else
                                        {
                                            graphicsContainer.BringForward(selectedElements);
                                        }
                                    }
                                    else
                                    {
                                        graphicsContainer.BringToFront(selectedElements);
                                    }
                                }
                                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                            }
                        }
                    }
                }
            }
        }

        private void method_43()
        {
            IActiveView activeView = this._context.ActiveView;
            if (activeView != null && activeView is IPageLayout)
            {
                IGraphicsContainer graphicsContainer = activeView as IGraphicsContainer;
                if (graphicsContainer != null)
                {
                    IGraphicsContainerSelect graphicsContainerSelect = this.method_9();
                    if (graphicsContainerSelect != null)
                    {
                        int elementSelectionCount = graphicsContainerSelect.ElementSelectionCount;
                        if (elementSelectionCount == 1)
                        {
                            IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                            if (selectedElements != null)
                            {
                                selectedElements.Reset();
                                IElement element = selectedElements.Next();
                                if (element != null)
                                {
                                }
                            }
                        }
                    }
                }
            }
        }

        private int method_44(IGraphicsContainer igraphicsContainer_0)
        {
            int num = 0;
            igraphicsContainer_0.Reset();
            int result;
            for (IElement element = igraphicsContainer_0.Next(); element != null; element = igraphicsContainer_0.Next())
            {
                if (element is IMapFrame)
                {
                    num++;
                    if (num >= 2)
                    {
                        result = num;
                        return result;
                    }
                }
                else if (element is IGroupElement)
                {
                    num += this.method_45(element as IGroupElement);
                    if (num >= 2)
                    {
                        result = num;
                        return result;
                    }
                }
            }
            result = num;
            return result;
        }

        private int method_45(IGroupElement igroupElement_0)
        {
            int num = 0;
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            int result;
            for (IElement element = elements.Next(); element != null; element = elements.Next())
            {
                if (element is IMapFrame)
                {
                    num++;
                    if (num >= 2)
                    {
                        result = num;
                        return result;
                    }
                }
                else if (element is IGroupElement)
                {
                    num += this.method_45(element as IGroupElement);
                    if (num >= 2)
                    {
                        result = num;
                        return result;
                    }
                }
            }
            result = num;
            return result;
        }

        private void method_46(IGraphicsContainer igraphicsContainer_0)
        {
            IMapFrame mapFrame = new MapFrame() as IMapFrame;
            mapFrame.Map = new Map
            {
                Name = "Layers"
            };
            IElement element = mapFrame as IElement;
            IEnvelope envelope = new Envelope() as IEnvelope;
            IPageLayout pageLayout = igraphicsContainer_0 as IPageLayout;
            double num;
            double num2;
            pageLayout.Page.QuerySize(out num, out num2);
            envelope.PutCoords(1.0, 1.0, num - 1.0, num2 - 1.0);
            element.Geometry = envelope;
            igraphicsContainer_0.AddElement(mapFrame as IElement, 0);
        }

        public void Init()
        {
            this.ipopuMenuWrap_0.Clear();
            string[] array = new string[]
            {
                "DeleteElement",
                "-",
                "ElmentProperty"
            };
            for (int i = 0; i < array.Length; i++)
            {
                bool flag = i + 1 != array.Length && string.Compare(array[i + 1], "-") == 0;
                this.ipopuMenuWrap_0.AddItem(array[i], flag);
            }
        }

        public string[] ContextMenuKeys
        {
            get
            {
                return new string[]
                {
                    "CmdDeleteElement",
                    "-",
                    "CmdElmentProperty"
                };
            }
        }
    }
}