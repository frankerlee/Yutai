using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class MarkFillControl : UserControl
    {
        private bool m_CanDo = true;
        public IMarkerFillSymbol m_MarkerFillSymbol;
        public IStyleGallery m_pSG;

        public event ValueChangedHandler ValueChanged;

        public MarkFillControl()
        {
            this.InitializeComponent();
        }

        private void btnFillMarker_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_MarkerFillSymbol.MarkerSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_MarkerFillSymbol.MarkerSymbol).Clone();
                }
                else
                {
                    pSym = new SimpleMarkerSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_MarkerFillSymbol.MarkerSymbol = (IMarkerSymbol) selector.GetSymbol();
                    this.btnFillMarker.Style = this.m_MarkerFillSymbol.MarkerSymbol;
                    this.btnFillMarker.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void btnOutline_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_MarkerFillSymbol.Outline != null)
                {
                    selector.SetSymbol((ISymbol) this.m_MarkerFillSymbol.Outline);
                }
                else
                {
                    selector.SetSymbol(new SimpleLineSymbolClass());
                }
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_MarkerFillSymbol.Outline = (ILineSymbol) selector.GetSymbol();
                    this.btnOutline.Style = this.m_MarkerFillSymbol.Outline;
                    this.btnOutline.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_MarkerFillSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_MarkerFillSymbol.Color = pColor;
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

        private void InitControl()
        {
            this.m_CanDo = false;
            if (this.m_MarkerFillSymbol.Style == esriMarkerFillStyle.esriMFSGrid)
            {
                this.radioGroupFillStyle.SelectedIndex = 0;
            }
            else
            {
                this.radioGroupFillStyle.SelectedIndex = 1;
            }
            this.SetColorEdit(this.colorEdit1, this.m_MarkerFillSymbol.Color);
            this.btnFillMarker.Style = this.m_MarkerFillSymbol.MarkerSymbol;
            this.btnOutline.Style = this.m_MarkerFillSymbol.Outline;
            this.m_CanDo = true;
        }

        private void MarkFillControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void radioGroupFillStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.radioGroupFillStyle.SelectedIndex == 0)
                {
                    this.m_MarkerFillSymbol.Style = esriMarkerFillStyle.esriMFSGrid;
                }
                else
                {
                    this.m_MarkerFillSymbol.Style = esriMarkerFillStyle.esriMFSRandom;
                }
                this.refresh(e);
            }
        }

        private void refresh(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
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

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }
    }
}