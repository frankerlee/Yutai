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
    internal class MarkerLineControl : UserControl
    {
        private NewSymbolButton btnMarkSymbol;
        private Container components = null;
        private Label label1;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnMarkSymbol = new NewSymbolButton();
            this.label1 = new Label();
            base.SuspendLayout();
            this.btnMarkSymbol.Location = new Point(0x58, 0x48);
            this.btnMarkSymbol.Name = "btnMarkSymbol";
            this.btnMarkSymbol.Size = new Size(0x60, 0x38);
            this.btnMarkSymbol.Style = null;
            this.btnMarkSymbol.TabIndex = 1;
            this.btnMarkSymbol.Click += new EventHandler(this.btnMarkSymbol_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x58);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "点符号:";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnMarkSymbol);
            base.Name = "MarkerLineControl";
            base.Size = new Size(0x1a8, 0x108);
            base.Load += new EventHandler(this.MarkerLineControl_Load);
            base.ResumeLayout(false);
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

