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
    public partial class BorderSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ISymbolBorder m_pOldSymbolBorder = null;
        private ISymbolBorder m_pSymbolBorder = null;
        private string m_Title = "符号";

        public event OnValueChangeEventHandler OnValueChange;

        public BorderSymbolPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
        }

        public BorderSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_pOldSymbolBorder.LineSymbol = this.m_pSymbolBorder.LineSymbol;
                this.m_IsPageDirty = false;
            }
        }

        private void BorderSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(_context.StyleGallery);
                selector.SetSymbol(lineSymbol);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    lineSymbol = selector.GetSymbol() as ILineSymbol;
                    this.m_pSymbolBorder.LineSymbol = lineSymbol;
                    this.m_CanDo = false;
                    this.SetColorEdit(this.colorEdit1, lineSymbol.Color);
                    this.txtWidth.Value = (decimal) lineSymbol.Width;
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
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                IColor pColor = lineSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                lineSymbol.Color = pColor;
                this.m_pSymbolBorder.LineSymbol = lineSymbol;
                this.m_IsPageDirty = true;
                this.refresh(e);
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
            this.symbolItem1.Symbol = this.m_pSymbolBorder;
            if (this.m_pSymbolBorder != null)
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                if (lineSymbol != null)
                {
                    this.colorEdit1.Enabled = true;
                    this.txtWidth.Properties.ReadOnly = false;
                    this.SetColorEdit(this.colorEdit1, lineSymbol.Color);
                    this.txtWidth.Value = (decimal) lineSymbol.Width;
                }
                else
                {
                    this.colorEdit1.Enabled = false;
                    this.txtWidth.Properties.ReadOnly = true;
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
            this.m_pOldSymbolBorder = @object as ISymbolBorder;
            if (this.m_pOldSymbolBorder != null)
            {
                this.m_pSymbolBorder = (this.m_pOldSymbolBorder as IClone).Clone() as ISymbolBorder;
            }
            else
            {
                this.m_pSymbolBorder = null;
            }
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                if (this.txtWidth.Value <= 0M)
                {
                    this.txtWidth.Value = (decimal) lineSymbol.Width;
                }
                else
                {
                    lineSymbol.Width = (double) this.txtWidth.Value;
                    this.m_pSymbolBorder.LineSymbol = lineSymbol;
                    this.m_IsPageDirty = true;
                    this.refresh(e);
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
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public ISymbolBorder SymbolBorder
        {
            set
            {
                this.m_pOldSymbolBorder = value;
                if (this.m_pOldSymbolBorder != null)
                {
                    this.m_pSymbolBorder = (this.m_pOldSymbolBorder as IClone).Clone() as ISymbolBorder;
                }
                else
                {
                    this.m_pSymbolBorder = null;
                }
            }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}