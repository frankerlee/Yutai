using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class frmNewLegendItem : Form
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private IMarkerSymbol imarkerSymbol_0 = new SimpleMarkerSymbolClass();
        private IStyleGallery istyleGallery_0 = null;
        private YTLegendItem jlklenendItem_0 = null;

        public frmNewLegendItem()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.jlklenendItem_0 = new YTLegendItem(this.btnStyle.Style as ISymbol, this.txtLegendItemName.Text);
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if ((this.rdoPointSymbol.Checked || this.rdoLineSymbol.Checked) || !this.rdoFillSymbol.Checked)
            {
            }
            selector.SetStyleGallery(this.istyleGallery_0);
            selector.SetSymbol(this.btnStyle.Style);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.btnStyle.Style = selector.GetSymbol();
                if (this.rdoPointSymbol.Checked)
                {
                    this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                }
                else if (this.rdoLineSymbol.Checked)
                {
                    this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                }
                else
                {
                    this.ifillSymbol_0 = this.btnStyle.Style as IFillSymbol;
                }
            }
        }

        private void frmNewLegendItem_Load(object sender, EventArgs e)
        {
            this.bool_0 = true;
            if (this.rdoPointSymbol.Checked)
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
            }
            else if (this.rdoLineSymbol.Checked)
            {
                this.btnStyle.Style = this.ilineSymbol_0;
            }
            else
            {
                this.btnStyle.Style = this.ifillSymbol_0;
            }
        }

        private void rdoFillSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnStyle.Style = this.ifillSymbol_0;
            }
        }

        private void rdoLineSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnOK.Enabled = true;
                this.btnStyle.Style = this.ilineSymbol_0;
            }
        }

        private void rdoPointSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
            }
        }

        private void txtLegendItemName_TextChanged(object sender, EventArgs e)
        {
        }

        internal YTLegendItem YTLegendItem
        {
            get { return this.jlklenendItem_0; }
            set { this.jlklenendItem_0 = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
        }
    }
}