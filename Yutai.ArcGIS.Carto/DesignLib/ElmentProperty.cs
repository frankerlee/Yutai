using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElmentProperty : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
    {
        private IYTHookHelper _hookHelper;

        public ElmentProperty()
        {
            base.m_caption = "属性";
            base.m_toolTip = "属性";
            base.m_name = "ElmentProperty";
            base.m_category = "元素";
        }

        private IGraphicsContainerSelect method_0()
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

        private bool method_1()
        {
            IPropertySheet sheet;
            IPropertyPage page;
            IActiveView activeView;
            IGraphicsContainerSelect select = this.method_0();
            if (select.ElementSelectionCount == 0)
            {
                return false;
            }
            IElement element = select.SelectedElement(0);
            IEditElementPropertiesOperation operation = new EditElementPropertiesOperation
            {
                ActiveView = this._hookHelper.ActiveView,
                Element = element
            };
            if (element is ITextElement)
            {
                sheet = new frmElementProperty
                {
                    Title = "属性"
                };
                page = new TextSetupCtrl();
                sheet.AddPage(page);
                page = new UI.ElementSizeAndPositionCtrl();
                sheet.AddPage(page);
                if (sheet.EditProperties(element))
                {
                    activeView = this._hookHelper.ActiveView;
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    activeView.GraphicsContainer.UpdateElement(element);
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    this._hookHelper.OperationStack.Do(operation);
                }
            }
            else
            {
                IEnvelope envelope;
                if (element is IMapFrame)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "数据框 属性"
                    };
                    page = null;
                    page = new MapGeneralInfoCtrl();
                    sheet.AddPage(page);
                    page = new MapCoordinateCtrl();
                    sheet.AddPage(page);
                    page = new FrameProprtyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
                else if (element is IPictureElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "图像 属性"
                    };
                    page = null;
                    page = new PicturePropertyPage();
                    sheet.AddPage(page);
                    page = new FrameProprtyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
                else if (element is IMapSurroundFrame)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    if ((element as IMapSurroundFrame).MapSurround is INorthArrow)
                    {
                        page = new UI.NorthArrowPropertyPage();
                        sheet.AddPage(page);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is IScaleBar)
                    {
                        page = new UI.ScaleBarFormatPropertyPage();
                        sheet.AddPage(page);
                        page = new UI.ScaleAndUnitsPropertyPage();
                        sheet.AddPage(page);
                        page = new UI.NumberAndLabelPropertyPage();
                        sheet.AddPage(page);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is IScaleText)
                    {
                        page = new UI.ScaleTextTextPropertyPage();
                        sheet.AddPage(page);
                        page = new UI.ScaleTextFormatPropertyPage();
                        sheet.AddPage(page);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is ILegend)
                    {
                        page = new UI.LegendPropertyPage();
                        sheet.AddPage(page);
                        page = new UI.LegendItemPropertyPage();
                        sheet.AddPage(page);
                    }
                    page = new FrameProprtyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    IElement element1 = (element as IClone).Clone() as IElement;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
                else if (element is IFrameElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new UI.ElementGeometryInfoPropertyPage();
                    sheet.AddPage(page);
                    page = new FrameProprtyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
                else if (element is IFillShapeElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new UI.FillSymbolPropertyPage();
                    sheet.AddPage(page);
                    page = new UI.ElementGeometryInfoPropertyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
                else if (element is ILineElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new UI.LineSymbolPropertyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
                else if (element is IMarkerElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new UI.MarkerElementPropertyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        activeView = this._hookHelper.ActiveView;
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        activeView.GraphicsContainer.UpdateElement(element);
                        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this._hookHelper.OperationStack.Do(operation);
                    }
                }
            }
            return true;
        }

        public override void OnClick()
        {
            this.method_1();
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper.Hook = object_0;
        }

        public override bool Enabled
        {
            get
            {
                if (this._hookHelper.FocusMap == null)
                {
                    return false;
                }
                return (this.method_0().ElementSelectionCount == 1);
            }
        }
    }
}