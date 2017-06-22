using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    [ToolboxItem(false)]
    public partial class BackgroundSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ISymbolBackground m_pOldSymbolBackground = null;
        private ISymbolBackground m_pSymbolBackground = null;
        private string m_Title = "符号";

        public event OnValueChangeEventHandler OnValueChange;

        public BackgroundSymbolPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
        }

        public BackgroundSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_pOldSymbolBackground.FillSymbol = this.m_pSymbolBackground.FillSymbol;
                this.m_IsPageDirty = false;
            }
        }

        private void BorderSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            this.symbolItem1.Symbol = this.m_pSymbolBackground;
            this.Init();
            this.m_CanDo = true;
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IFillSymbol fillSymbol = this.m_pSymbolBackground.FillSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(_context.StyleGallery);
                selector.SetSymbol(fillSymbol);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    fillSymbol = selector.GetSymbol() as IFillSymbol;
                    this.m_pSymbolBackground.FillSymbol = fillSymbol;
                    this.m_CanDo = false;
                    this.Init();
                    this.m_CanDo = true;
                    this.m_IsPageDirty = true;
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IFillSymbol fillSymbol = this.m_pSymbolBackground.FillSymbol;
                IColor pColor = fillSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                fillSymbol.Color = pColor;
                this.m_pSymbolBackground.FillSymbol = fillSymbol;
                this.m_IsPageDirty = true;
                this.refresh(e);
            }
        }

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IFillSymbol fillSymbol = this.m_pSymbolBackground.FillSymbol;
                ILineSymbol outline = fillSymbol.Outline;
                if (outline != null)
                {
                    IColor pColor = outline.Color;
                    this.UpdateColorFromColorEdit(this.colorEdit2, pColor);
                    outline.Color = pColor;
                    fillSymbol.Outline = outline;
                    this.m_pSymbolBackground.FillSymbol = fillSymbol;
                    this.m_IsPageDirty = true;
                    this.refresh(e);
                }
            }
        }

 private void GetRGB(uint rgb, out int r, out int g, out int b)
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
            if (this.m_pSymbolBackground != null)
            {
                IFillSymbol fillSymbol = this.m_pSymbolBackground.FillSymbol;
                if (fillSymbol != null)
                {
                    this.colorEdit1.Enabled = true;
                    this.txtWidth.Enabled = true;
                    this.SetColorEdit(this.colorEdit1, fillSymbol.Color);
                    ILineSymbol outline = fillSymbol.Outline;
                    if (outline != null)
                    {
                        this.colorEdit2.Enabled = true;
                        this.txtWidth.Enabled = true;
                        this.SetColorEdit(this.colorEdit2, outline.Color);
                        this.txtWidth.Value = (decimal) outline.Width;
                    }
                    else
                    {
                        this.colorEdit2.Enabled = false;
                        this.txtWidth.Enabled = false;
                    }
                }
                else
                {
                    this.colorEdit1.Enabled = false;
                    this.colorEdit2.Enabled = false;
                    this.txtWidth.Enabled = false;
                }
            }
        }

 private void refresh(EventArgs e)
        {
            if (this.OnValueChange != null)
            {
                this.symbolItem1.Invalidate();
                this.OnValueChange();
            }
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
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
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        public void SetObjects(object @object)
        {
            this.m_pOldSymbolBackground = @object as ISymbolBackground;
            if (this.m_pOldSymbolBackground != null)
            {
                this.m_pSymbolBackground = (this.m_pOldSymbolBackground as IClone).Clone() as ISymbolBackground;
            }
            else
            {
                this.m_pSymbolBackground = null;
            }
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IFillSymbol fillSymbol = this.m_pSymbolBackground.FillSymbol;
                ILineSymbol outline = fillSymbol.Outline;
                if (outline != null)
                {
                    if (this.txtWidth.Value <= 0M)
                    {
                        this.txtWidth.Value = (decimal) outline.Width;
                    }
                    else
                    {
                        outline.Width = (double) this.txtWidth.Value;
                        fillSymbol.Outline = outline;
                        this.m_pSymbolBackground.FillSymbol = fillSymbol;
                        this.m_IsPageDirty = true;
                        this.refresh(e);
                    }
                }
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
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

        public ISymbolBackground SymbolBackground
        {
            set
            {
                this.m_pOldSymbolBackground = value;
                if (this.m_pOldSymbolBackground != null)
                {
                    this.m_pSymbolBackground = (this.m_pOldSymbolBackground as IClone).Clone() as ISymbolBackground;
                }
                else
                {
                    this.m_pSymbolBackground = null;
                }
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

