using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class ScaleBarFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        internal static IScaleBar m_pScaleBar;
        private MapTemplateElement mapTemplateElement_0 = null;
        private string string_0 = "格式";

        public event OnValueChangeEventHandler OnValueChange;

        static ScaleBarFormatPropertyPage()
        {
            old_acctor_mc();
        }

        public ScaleBarFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround = (m_pScaleBar as IClone).Clone() as IMapSurround;
            }
        }

        private void btnStyleSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    if (m_pScaleBar != null)
                    {
                        selector.SetSymbol(m_pScaleBar);
                    }
                    else
                    {
                        selector.SetSymbol(new ScalebarClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        m_pScaleBar = selector.GetSymbol() as IScaleBar;
                        this.method_0();
                        IStyleGalleryItem styleGalleryItemAt =
                            this.cboStyle.GetStyleGalleryItemAt(this.cboStyle.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = m_pScaleBar;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem
                                {
                                    Name = "<定制>",
                                    Item = m_pScaleBar
                                };
                                this.cboStyle.Add(styleGalleryItemAt);
                                this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem
                            {
                                Name = "<定制>",
                                Item = m_pScaleBar
                            };
                            this.cboStyle.Add(styleGalleryItemAt);
                            this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                        }
                        this.method_4();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbol1Selector_Click(object sender, EventArgs e)
        {
            try
            {
                ISymbol pSym = null;
                if (m_pScaleBar is IScaleLine)
                {
                    pSym = (m_pScaleBar as IScaleLine).LineSymbol as ISymbol;
                }
                else if (m_pScaleBar is ISingleFillScaleBar)
                {
                    pSym = (m_pScaleBar as ISingleFillScaleBar).FillSymbol as ISymbol;
                }
                else if (m_pScaleBar is IDoubleFillScaleBar)
                {
                    pSym = (m_pScaleBar as IDoubleFillScaleBar).FillSymbol1 as ISymbol;
                }
                if (pSym != null)
                {
                    frmSymbolSelector selector = new frmSymbolSelector();
                    if (selector != null)
                    {
                        selector.SetStyleGallery(ApplicationBase.StyleGallery);
                        selector.SetSymbol(pSym);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            if (m_pScaleBar is IScaleLine)
                            {
                                (m_pScaleBar as IScaleLine).LineSymbol = selector.GetSymbol() as ILineSymbol;
                            }
                            else if (m_pScaleBar is ISingleFillScaleBar)
                            {
                                (m_pScaleBar as ISingleFillScaleBar).FillSymbol = selector.GetSymbol() as IFillSymbol;
                            }
                            else if (m_pScaleBar is IDoubleFillScaleBar)
                            {
                                (m_pScaleBar as IDoubleFillScaleBar).FillSymbol1 = selector.GetSymbol() as IFillSymbol;
                            }
                            this.method_4();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbol2Selector_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_pScaleBar is IDoubleFillScaleBar)
                {
                    ISymbol pSym = (m_pScaleBar as IDoubleFillScaleBar).FillSymbol2 as ISymbol;
                    frmSymbolSelector selector = new frmSymbolSelector();
                    if (selector != null)
                    {
                        selector.SetStyleGallery(ApplicationBase.StyleGallery);
                        selector.SetSymbol(pSym);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            (m_pScaleBar as IDoubleFillScaleBar).FillSymbol2 = selector.GetSymbol() as IFillSymbol;
                            this.method_4();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.bool_0 = false;
                        this.method_1(labelSymbol);
                        this.bool_0 = true;
                        this.method_4();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void cboFontSize_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.cboFontSize.Text);
                    ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                    if (!(labelSymbol.Size == num))
                    {
                        labelSymbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.method_4();
                    }
                }
                catch
                {
                }
            }
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IStyleGalleryItem selectStyleGalleryItem = this.cboStyle.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem == null)
                {
                    m_pScaleBar = null;
                }
                else
                {
                    m_pScaleBar = (selectStyleGalleryItem.Item as IClone).Clone() as IScaleBar;
                }
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
                this.method_4();
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void colorBar_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor barColor = m_pScaleBar.BarColor;
                this.method_2(this.colorBar, barColor);
                m_pScaleBar.BarColor = barColor;
                this.method_4();
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                IColor color = labelSymbol.Color;
                this.method_2(this.colorTextSymbol, color);
                labelSymbol.Color = color;
                m_pScaleBar.LabelSymbol = labelSymbol;
                this.method_4();
            }
        }

        private void method_0()
        {
            if (m_pScaleBar != null)
            {
                this.method_3(this.colorBar, m_pScaleBar.BarColor);
                this.txtBarSize.Text = m_pScaleBar.BarHeight.ToString("0.##");
                this.method_1(m_pScaleBar.LabelSymbol);
            }
        }

        private void method_1(ITextSymbol itextSymbol_0)
        {
            this.method_3(this.colorTextSymbol, itextSymbol_0.Color);
            stdole.IFontDisp font = itextSymbol_0.Font;
            this.chkBold.Checked = font.Bold;
            this.chkItalic.Checked = font.Italic;
            this.chkUnderline.Checked = font.Underline;
            this.cboFontName.Text = font.Name;
            this.cboFontSize.Text = itextSymbol_0.Size.ToString("0.##");
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = ColorManage.EsriRGB(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                ColorManage.GetEsriRGB((uint) icolor_0.RGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_4()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private static void old_acctor_mc()
        {
            m_pScaleBar = null;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void ScaleBarFormatPropertyPage_Load(object sender, EventArgs e)
        {
            IStyleGalleryItem item;
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
            if (this.istyleGallery_0 != null)
            {
                item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Scale Bars", "", "");
                item2.Reset();
                item = item2.Next();
                while (item != null)
                {
                    this.cboStyle.Add(item);
                    item = item2.Next();
                }
                if (this.cboStyle.Items.Count > 0)
                {
                    this.cboStyle.SelectedIndex = 0;
                }
            }
            if (m_pScaleBar != null)
            {
                item = new MyStyleGalleryItem
                {
                    Name = "<定制>",
                    Item = m_pScaleBar
                };
                this.cboStyle.SelectStyleGalleryItem(item);
            }
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
            this.imapSurroundFrame_0 = this.mapTemplateElement_0.Element as IMapSurroundFrame;
            if (this.imapSurroundFrame_0 != null)
            {
                m_pScaleBar = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleBar;
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}