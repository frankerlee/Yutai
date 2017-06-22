using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class ScaleBarFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        internal static IScaleBar m_pOldScaleBar = null;
        internal static IScaleBar m_pScaleBar = null;
        private string m_Title = "格式";

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleBarFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public ScaleBarFormatPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
            this.m_pSG = _context.StyleGallery;
        }


        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                (m_pOldScaleBar as IClone).Assign(m_pScaleBar as IClone);
            }
        }

        private void btnStyleSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
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
                        this.Init();
                        IStyleGalleryItem styleGalleryItemAt = this.cboStyle.GetStyleGalleryItemAt(this.cboStyle.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = m_pScaleBar;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = m_pScaleBar
                                };
                                this.cboStyle.Add(styleGalleryItemAt);
                                this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = m_pScaleBar
                            };
                            this.cboStyle.Add(styleGalleryItemAt);
                            this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                        }
                        this.ValueChanged();
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
                        selector.SetStyleGallery(this.m_pSG);
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
                            this.ValueChanged();
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
                        selector.SetStyleGallery(this.m_pSG);
                        selector.SetSymbol(pSym);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            (m_pScaleBar as IDoubleFillScaleBar).FillSymbol2 = selector.GetSymbol() as IFillSymbol;
                            this.ValueChanged();
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
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.m_CanDo = false;
                        this.SetTextSymbol(labelSymbol);
                        this.m_CanDo = true;
                        this.ValueChanged();
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
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void cboFontSize_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    double num = double.Parse(this.cboFontSize.Text);
                    ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                    if (labelSymbol.Size != num)
                    {
                        labelSymbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.ValueChanged();
                    }
                }
                catch
                {
                }
            }
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
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
                this.m_CanDo = false;
                this.Init();
                this.m_CanDo = true;
                this.ValueChanged();
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void colorBar_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor barColor = m_pScaleBar.BarColor;
                this.UpdateColorFromColorEdit(this.colorBar, barColor);
                m_pScaleBar.BarColor = barColor;
                this.ValueChanged();
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                IColor pColor = labelSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorTextSymbol, pColor);
                labelSymbol.Color = pColor;
                m_pScaleBar.LabelSymbol = labelSymbol;
                this.ValueChanged();
            }
        }

 public static int EsriRGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        public static void GetEsriRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
        }

        public void Hide()
        {
        }

        private void Init()
        {
            if (m_pScaleBar != null)
            {
                this.SetColorEdit(this.colorBar, m_pScaleBar.BarColor);
                this.txtBarSize.Text = m_pScaleBar.BarHeight.ToString("0.##");
                this.SetTextSymbol(m_pScaleBar.LabelSymbol);
            }
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
            if (this.m_pSG != null)
            {
                item = null;
                IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Scale Bars", "", "");
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
                item = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = m_pScaleBar
                };
                this.cboStyle.SelectStyleGalleryItem(item);
            }
            this.Init();
            this.m_CanDo = true;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                GetEsriRGB((uint) pColor.RGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        public void SetObjects(object @object)
        {
            m_pOldScaleBar = @object as IScaleBar;
            if (m_pOldScaleBar != null)
            {
                m_pScaleBar = (m_pOldScaleBar as IClone).Clone() as IScaleBar;
            }
        }

        private void SetTextSymbol(ITextSymbol pTextSymbol)
        {
            this.SetColorEdit(this.colorTextSymbol, pTextSymbol.Color);
            stdole.IFontDisp font = pTextSymbol.Font;
            this.chkBold.Checked = font.Bold;
            this.chkItalic.Checked = font.Italic;
            this.chkUnderline.Checked = font.Underline;
            this.cboFontName.Text = font.Name;
            this.cboFontSize.Text = pTextSymbol.Size.ToString("0.##");
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = EsriRGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

