using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using IOleFrame = Yutai.ArcGIS.Common.ExtendClass.IOleFrame;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdElementProperty : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null) return false;
                if (this.GetSelection() == null) return false;
                return this.GetSelection().ElementSelectionCount == 1;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_toolTip = "属性";
            this.m_category = "元素";

            base.m_bitmap = Properties.Resources.icon_property;
            base.m_name = "Printing_ElementProperty";
            _key = "Printing_ElementProperty";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
            _needUpdateEvent = true;
        }

        public CmdElementProperty(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            this.method_1();
        }


        private IGraphicsContainerSelect GetSelection()
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
                //IViewManager viewManager = activeView as IViewManager;
                //ISelection arg_2E_0 = viewManager.ElementSelection;
                result = (graphicsContainer as IGraphicsContainerSelect);
                return result;
            }
            catch
            {
            }
            result = null;
            return result;
        }

        private bool method_1()
        {
            IGraphicsContainerSelect graphicsContainerSelect = this.GetSelection();
            bool result;
            if (graphicsContainerSelect.ElementSelectionCount == 0)
            {
                result = false;
            }
            else
            {
                IElement element = graphicsContainerSelect.SelectedElement(0);
                IEditElementPropertiesOperation editElementPropertiesOperation = new EditElementPropertiesOperation();
                editElementPropertiesOperation.ActiveView = this._context.ActiveView;
                editElementPropertiesOperation.Element = element;
                if (!(element is IOleFrame) && ElementChangeEvent.IsFireElementPropertyEditEvent)
                {
                    ElementChangeEvent.EditElementProperty(element);
                    result = true;
                }
                else
                {
                    if (element is ITextElement)
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
                        IElement arg_43A_0 = (element as IClone).Clone() as IElement;
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
                    else if (element is ILineElement)
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
                    result = true;
                }
            }
            return result;
        }
    }
}