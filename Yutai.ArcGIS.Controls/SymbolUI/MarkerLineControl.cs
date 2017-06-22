using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class MarkerLineControl : UserControl
    {
        public IMarkerLineSymbol m_pMarkerLineSymbol;
        public IStyleGallery m_pSG;

        public event ValueChangedHandler ValueChanged;

        public MarkerLineControl()
        {
            this.InitializeComponent();
        }

        private void btnMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_pMarkerLineSymbol.MarkerSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_pMarkerLineSymbol.MarkerSymbol).Clone();
                }
                else
                {
                    pSym = new SimpleMarkerSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_pMarkerLineSymbol.MarkerSymbol = (IMarkerSymbol) selector.GetSymbol();
                    this.btnMarkSymbol.Style = this.m_pMarkerLineSymbol.MarkerSymbol;
                    this.btnMarkSymbol.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

 private void MarkerLineControl_Load(object sender, EventArgs e)
        {
            this.btnMarkSymbol.Style = this.m_pMarkerLineSymbol.MarkerSymbol;
        }

        private void refresh(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }
    }
}

