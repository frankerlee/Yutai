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
    public partial class ScaleTextFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        internal static IScaleText m_pOldScaleText = null;
        internal static IScaleText m_pScaleText = null;
        private string m_Title = "格式";

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleTextFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public ScaleTextFormatPropertyPage(IAppContext context)
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
                (m_pOldScaleText as IClone).Assign(m_pScaleText as IClone);
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
                    if (m_pScaleText != null)
                    {
                        selector.SetSymbol(m_pScaleText);
                    }
                    else
                    {
                        selector.SetSymbol(new ScaleTextClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        m_pScaleText = selector.GetSymbol() as IScaleText;
                        this.Init();
                        IStyleGalleryItem styleGalleryItemAt = this.cboStyle.GetStyleGalleryItemAt(this.cboStyle.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = m_pScaleText;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = m_pScaleText
                                };
                                this.cboStyle.Add(styleGalleryItemAt);
                                this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = m_pScaleText
                            };
                            this.cboStyle.Add(styleGalleryItemAt);
                            this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                        }
                        this.ValueChanged();
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                ITextSymbol pSym = m_pScaleText.Symbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(pSym);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as ITextSymbol;
                        m_pScaleText.Symbol = pSym;
                        this.SetTextSymbol(pSym);
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
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
                    ITextSymbol symbol = m_pScaleText.Symbol;
                    if (symbol.Size != num)
                    {
                        symbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleText.Symbol = symbol;
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                    m_pScaleText = null;
                }
                else
                {
                    m_pScaleText = (selectStyleGalleryItem.Item as IClone).Clone() as IScaleText;
                }
                this.m_CanDo = false;
                this.Init();
                this.m_CanDo = true;
                this.ValueChanged();
                ScaleTextEventsClass.ScaleTextChage(this);
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.ValueChanged();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.ValueChanged();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.ValueChanged();
                }
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                IColor pColor = symbol.Color;
                this.UpdateColorFromColorEdit(this.colorTextSymbol, pColor);
                symbol.Color = pColor;
                m_pScaleText.Symbol = symbol;
                ScaleTextEventsClass.ScaleTextChage(this);
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

        ~ScaleTextFormatPropertyPage()
        {
            m_pScaleText = null;
            m_pOldScaleText = null;
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
            if (m_pScaleText != null)
            {
                this.SetTextSymbol(m_pScaleText.Symbol);
            }
        }

 private void ScaleTextFormatPropertyPage_Load(object sender, EventArgs e)
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
                IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Scale Texts", "", "");
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
            if (m_pScaleText != null)
            {
                item = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = m_pScaleText
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
            if (m_pOldScaleText == null)
            {
                m_pOldScaleText = @object as IScaleText;
                if (m_pOldScaleText != null)
                {
                    m_pScaleText = (m_pOldScaleText as IClone).Clone() as IScaleText;
                }
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
            this.cboFontSize.Text = font.Size.ToString();
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

