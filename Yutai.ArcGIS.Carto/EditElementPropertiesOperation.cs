﻿using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ExtendClass;

namespace Yutai.ArcGIS.Carto
{
    public class EditElementPropertiesOperation : IEditElementPropertiesOperation, IOperation
    {
        private IActiveView iactiveView_0 = null;
        private IElement ielement_0 = null;
        private IElement ielement_1 = null;
        private IElement ielement_2 = null;

        public void Do()
        {
            this.ielement_2 = (this.ielement_0 as IClone).Clone() as IElement;
        }

        private void method_0()
        {
            IPropertySheet sheet;
            IPropertyPage page;
            IElement element = this.ielement_0;
            if (element is ITextElement)
            {
                sheet = new frmElementProperty
                {
                    Title = "属性"
                };
                page = new TextSetupCtrl();
                sheet.AddPage(page);
                page = new ElementSizeAndPositionCtrl();
                sheet.AddPage(page);
                if (sheet.EditProperties(element))
                {
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
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
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
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
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
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
                        page = new NorthArrowPropertyPage();
                        sheet.AddPage(page);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is IScaleBar)
                    {
                        page = new ScaleBarFormatPropertyPage();
                        sheet.AddPage(page);
                        page = new ScaleAndUnitsPropertyPage();
                        sheet.AddPage(page);
                        page = new NumberAndLabelPropertyPage();
                        sheet.AddPage(page);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is IScaleText)
                    {
                        page = new ScaleTextTextPropertyPage();
                        sheet.AddPage(page);
                        page = new ScaleTextFormatPropertyPage();
                        sheet.AddPage(page);
                    }
                    else if ((element as IMapSurroundFrame).MapSurround is ILegend)
                    {
                        page = new LegendPropertyPage();
                        sheet.AddPage(page);
                        page = new LegendItemPropertyPage();
                        sheet.AddPage(page);
                    }
                    page = new FrameProprtyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    IElement element1 = (element as IClone).Clone() as IElement;
                    if (sheet.EditProperties(element))
                    {
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    }
                }
                else if (element is IFrameElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new ElementGeometryInfoPropertyPage();
                    sheet.AddPage(page);
                    page = new FrameProprtyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    }
                }
                else if (element is IFillShapeElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new FillSymbolPropertyPage();
                    sheet.AddPage(page);
                    page = new ElementGeometryInfoPropertyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    }
                }
                else if (element is ILineElement)
                {
                    sheet = new frmElementProperty
                    {
                        Title = "属性"
                    };
                    page = null;
                    page = new LineSymbolPropertyPage();
                    sheet.AddPage(page);
                    envelope = element.Geometry.Envelope;
                    if (sheet.EditProperties(element))
                    {
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    }
                }
                else if (element is IJTBElement)
                {
                    frmJTBElement element2 = new frmJTBElement
                    {
                        JTBElement = element as IJTBElement
                    };
                    envelope = element.Geometry.Envelope;
                    if (element2.ShowDialog() == DialogResult.OK)
                    {
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, envelope);
                        this.iactiveView_0.GraphicsContainer.UpdateElement(element);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    }
                }
            }
            this.ielement_2 = (element as IClone).Clone() as IElement;
        }

        public void Redo()
        {
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.ielement_0, null);
            (this.ielement_0 as IClone).Assign(this.ielement_2 as IClone);
            this.iactiveView_0.GraphicsContainer.UpdateElement(this.ielement_0);
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.ielement_0, null);
        }

        public void Undo()
        {
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.ielement_0, null);
            (this.ielement_0 as IClone).Assign(this.ielement_1 as IClone);
            this.iactiveView_0.GraphicsContainer.UpdateElement(this.ielement_0);
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.ielement_0, null);
        }

        public IActiveView ActiveView
        {
            set { this.iactiveView_0 = value; }
        }

        public bool CanRedo
        {
            get { return true; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public IElement Element
        {
            set
            {
                this.ielement_0 = value;
                this.ielement_1 = (this.ielement_0 as IClone).Clone() as IElement;
            }
        }

        public string MenuString
        {
            get { return "图形元素属性"; }
        }
    }
}