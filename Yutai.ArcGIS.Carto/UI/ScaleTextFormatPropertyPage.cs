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

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class ScaleTextFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        internal static IScaleText m_pScaleText;
        private string string_0 = "格式";

        public event OnValueChangeEventHandler OnValueChange;

        static ScaleTextFormatPropertyPage()
        {
            old_acctor_mc();
        }

        public ScaleTextFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround = (m_pScaleText as IClone).Clone() as IMapSurround;
            }
        }

        private void btnStyleSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
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
                        this.method_0();
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
                        this.method_4();
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
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(pSym);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as ITextSymbol;
                        m_pScaleText.Symbol = pSym;
                        this.method_1(pSym);
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
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
                    ITextSymbol symbol = m_pScaleText.Symbol;
                    if (!(symbol.Size == num))
                    {
                        symbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleText.Symbol = symbol;
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                    m_pScaleText = null;
                }
                else
                {
                    m_pScaleText = (selectStyleGalleryItem.Item as IClone).Clone() as IScaleText;
                }
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
                this.method_4();
                ScaleTextEventsClass.ScaleTextChage(this);
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.method_4();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.method_4();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.method_4();
                }
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                IColor color = symbol.Color;
                this.method_2(this.colorTextSymbol, color);
                symbol.Color = color;
                m_pScaleText.Symbol = symbol;
                ScaleTextEventsClass.ScaleTextChage(this);
                this.method_4();
            }
        }

 private void method_0()
        {
            if (m_pScaleText != null)
            {
                this.method_1(m_pScaleText.Symbol);
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
            this.cboFontSize.Text = font.Size.ToString();
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
            m_pScaleText = null;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
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
            if (this.istyleGallery_0 != null)
            {
                item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Scale Texts", "", "");
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
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if ((this.imapSurroundFrame_0 != null) && (m_pScaleText == null))
            {
                m_pScaleText = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleText;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
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
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

