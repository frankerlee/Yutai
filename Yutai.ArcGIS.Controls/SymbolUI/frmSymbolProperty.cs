using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class frmSymbolProperty : Form
    {
        private IStyleGallery m_pSG = null;

        public frmSymbolProperty()
        {
            this.InitializeComponent();
            this.symbolItem1.HasDrawLine = false;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            this.m_pStyleGalleryItem = null;
            base.Close();
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            if (!(this.m_pStyleGalleryItem.Item is ISymbol))
            {
                frmElementProperty property;
                object item = this.m_pStyleGalleryItem.Item;
                if (item is ISymbolBorder)
                {
                    property = new frmElementProperty();
                    BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                    property.Text = "边界";
                    property.AddPage(page);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (item is ISymbolBackground)
                {
                    property = new frmElementProperty();
                    BackgroundSymbolPropertyPage page2 = new BackgroundSymbolPropertyPage();
                    property.Text = "背景";
                    property.AddPage(page2);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (item is ISymbolShadow)
                {
                    property = new frmElementProperty();
                    ShadowSymbolPropertyPage page3 = new ShadowSymbolPropertyPage();
                    property.Text = "阴影";
                    property.AddPage(page3);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (item is INorthArrow)
                {
                    property = new frmElementProperty();
                    NorthArrowPropertyPage page4 = new NorthArrowPropertyPage();
                    property.Text = "指北针";
                    property.AddPage(page4);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else
                {
                    IPropertyPage page5;
                    if (item is IScaleBar)
                    {
                        property = new frmElementProperty
                        {
                            Text = "比例尺"
                        };
                        page5 = new ScaleBarFormatPropertyPage();
                        property.AddPage(page5);
                        page5 = new ScaleAndUnitsPropertyPage();
                        property.AddPage(page5);
                        page5 = new NumberAndLabelPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(item))
                        {
                            this.symbolItem1.Symbol = item;
                            this.m_pStyleGalleryItem.Item = item;
                            this.symbolItem1.Invalidate();
                        }
                    }
                    else if (item is IScaleText)
                    {
                        property = new frmElementProperty
                        {
                            Text = "比例尺文本"
                        };
                        page5 = new ScaleTextTextPropertyPage();
                        property.AddPage(page5);
                        page5 = new ScaleTextFormatPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(item))
                        {
                            this.symbolItem1.Symbol = item;
                            this.m_pStyleGalleryItem.Item = item;
                            this.symbolItem1.Invalidate();
                        }
                    }
                    else if (item is ILegendItem)
                    {
                        property = new frmElementProperty
                        {
                            Text = "图例项"
                        };
                        page5 = new LegendItemArrangementPropertyPage();
                        property.AddPage(page5);
                        page5 = new LegendItemGeneralPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(item))
                        {
                            this.symbolItem1.Symbol = item;
                            this.m_pStyleGalleryItem.Item = item;
                            this.symbolItem1.Invalidate();
                        }
                    }
                }
            }
            else
            {
                DialogResult result;
                ISymbol pSym = (ISymbol) this.m_pStyleGalleryItem.Item;
                switch (this.m_SymbolType)
                {
                    case enumSymbolType.enumSTPoint:
                    {
                        frmPointSymbolEdit edit = new frmPointSymbolEdit
                        {
                            m_pSG = this.m_pSG
                        };
                        edit.SetSymbol(pSym);
                        result = edit.ShowDialog();
                        pSym = edit.GetSymbol();
                        break;
                    }
                    case enumSymbolType.enumSTLine:
                    {
                        frmLineSymbolEdit edit2 = new frmLineSymbolEdit
                        {
                            m_pSG = this.m_pSG
                        };
                        edit2.SetSymbol(pSym);
                        result = edit2.ShowDialog();
                        pSym = edit2.GetSymbol();
                        break;
                    }
                    case enumSymbolType.enumSTFill:
                    {
                        frmFillSymbolEdit edit3 = new frmFillSymbolEdit
                        {
                            m_pSG = this.m_pSG
                        };
                        edit3.SetSymbol(pSym);
                        result = edit3.ShowDialog();
                        pSym = edit3.GetSymbol();
                        break;
                    }
                    case enumSymbolType.enumSTText:
                    {
                        frmTextSymbolEdit edit4 = new frmTextSymbolEdit
                        {
                            m_pSG = this.m_pSG
                        };
                        edit4.SetSymbol(pSym);
                        result = edit4.ShowDialog();
                        pSym = edit4.GetSymbol();
                        break;
                    }
                    default:
                        return;
                }
                if (result == DialogResult.OK)
                {
                    this.symbolItem1.Symbol = pSym;
                    this.m_pStyleGalleryItem.Item = pSym;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        private void frmSymbolProperty_Load(object sender, EventArgs e)
        {
            if (this.m_pStyleGalleryItem != null)
            {
                this.txtName.Text = this.m_pStyleGalleryItem.Name;
                this.txtCategory.Text = this.m_pStyleGalleryItem.Category;
                this.symbolItem1.Symbol = this.m_pStyleGalleryItem.Item;
                if (this.symbolItem1.Symbol is IMarkerSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTPoint;
                }
                else if (this.symbolItem1.Symbol is ILineSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTLine;
                }
                else if (this.symbolItem1.Symbol is IFillSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTFill;
                }
                else if (this.symbolItem1.Symbol is ITextSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTText;
                }
                else if (this.symbolItem1.Symbol is IColorRamp)
                {
                    this.m_SymbolType = enumSymbolType.enumSTColorRamp;
                }
                else if (this.symbolItem1.Symbol is IColor)
                {
                    this.m_SymbolType = enumSymbolType.enumSTColor;
                }
                else if (this.symbolItem1.Symbol is ISymbolBorder)
                {
                    this.m_SymbolType = enumSymbolType.enumSTBorder;
                }
                else if (this.symbolItem1.Symbol is ISymbolBackground)
                {
                    this.m_SymbolType = enumSymbolType.enumSTBackground;
                }
                else if (this.symbolItem1.Symbol is ISymbolShadow)
                {
                    this.m_SymbolType = enumSymbolType.enumSTShadow;
                }
                else if (this.symbolItem1.Symbol is IMarkerNorthArrow)
                {
                    this.m_SymbolType = enumSymbolType.enumSTNorthArrow;
                }
                else if (this.symbolItem1.Symbol is IScaleBar)
                {
                    this.m_SymbolType = enumSymbolType.enumSTScaleBar;
                }
                else if (this.symbolItem1.Symbol is IScaleText)
                {
                    this.m_SymbolType = enumSymbolType.enumSTScaleText;
                }
                else if (this.symbolItem1.Symbol is ILegendItem)
                {
                    this.m_SymbolType = enumSymbolType.enumSTLegendItem;
                }
                else
                {
                    this.m_SymbolType = enumSymbolType.enumSTUnknown;
                }
            }
            else
            {
                this.m_pStyleGalleryItem = new ServerStyleGalleryItemClass();
                object obj2 = null;
                switch (this.m_SymbolType)
                {
                    case enumSymbolType.enumSTPoint:
                        this.txtName.Text = "点符号";
                        obj2 = new MultiLayerMarkerSymbolClass();
                        ((IMultiLayerMarkerSymbol) obj2).AddLayer(new SimpleMarkerSymbolClass());
                        break;

                    case enumSymbolType.enumSTLine:
                        this.txtName.Text = "线符号";
                        obj2 = new MultiLayerLineSymbolClass();
                        ((IMultiLayerLineSymbol) obj2).AddLayer(new SimpleLineSymbolClass());
                        break;

                    case enumSymbolType.enumSTFill:
                        this.txtName.Text = "面符号";
                        obj2 = new MultiLayerFillSymbolClass();
                        ((IMultiLayerFillSymbol) obj2).AddLayer(new SimpleFillSymbolClass());
                        break;

                    case enumSymbolType.enumSTText:
                    case enumSymbolType.enumSTColorRamp:
                    case enumSymbolType.enumSTColor:
                    case enumSymbolType.enumSTLinePatch:
                    case enumSymbolType.enumSTAreaPatch:
                        return;

                    case enumSymbolType.enumSTNorthArrow:
                        this.txtName.Text = "指北针";
                        obj2 = new MarkerNorthArrowClass();
                        break;

                    case enumSymbolType.enumSTScaleBar:
                        this.txtName.Text = "比例尺";
                        obj2 = new ScaleLineClass();
                        break;

                    case enumSymbolType.enumSTScaleText:
                        this.txtName.Text = "比例尺文本";
                        obj2 = new ScaleTextClass();
                        break;

                    case enumSymbolType.enumSTBorder:
                        this.txtName.Text = "边界";
                        obj2 = new SymbolBorderClass();
                        break;

                    case enumSymbolType.enumSTShadow:
                        this.txtName.Text = "阴影";
                        obj2 = new SymbolShadowClass();
                        break;

                    case enumSymbolType.enumSTBackground:
                        this.txtName.Text = "背景";
                        obj2 = new SymbolBackgroundClass();
                        break;

                    case enumSymbolType.enumSTLegendItem:
                        this.txtName.Text = "图例项";
                        obj2 = new HorizontalLegendItemClass();
                        break;

                    case enumSymbolType.enumSTLabel:
                        this.txtName.Text = "标注";
                        obj2 = new LabelStyleClass();
                        break;

                    default:
                        return;
                }
                this.symbolItem1.Symbol = obj2;
                this.m_pStyleGalleryItem.Name = this.txtName.Text;
                this.m_pStyleGalleryItem.Category = this.txtCategory.Text;
                this.m_pStyleGalleryItem.Item = obj2;
            }
        }

        private void tnOK_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            this.m_pStyleGalleryItem.Name = this.txtName.Text;
            this.m_pStyleGalleryItem.Category = this.txtCategory.Text;
            base.Close();
        }

        public IStyleGallery StyleGallery
        {
            set { this.m_pSG = value; }
        }

        public IStyleGalleryItem StyleGalleryItem
        {
            get { return this.m_pStyleGalleryItem; }
            set { this.m_pStyleGalleryItem = value; }
        }

        public enumSymbolType SymbolType
        {
            get { return this.m_SymbolType; }
            set { this.m_SymbolType = value; }
        }
    }
}