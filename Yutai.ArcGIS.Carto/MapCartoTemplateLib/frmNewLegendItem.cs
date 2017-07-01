using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class frmNewLegendItem : Form
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private IFillSymbol ifillSymbol_1 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private IMarkerSymbol imarkerSymbol_0 = new SimpleMarkerSymbolClass();
        private IStyleGallery istyleGallery_0 = null;
        private MapCartoTemplateLib.YTLegendItem jlklenendItem_0 = null;

        public frmNewLegendItem()
        {
            this.InitializeComponent();
            (this.ifillSymbol_1 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.newSymbolButton1.Style = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.jlklenendItem_0 == null)
            {
                this.jlklenendItem_0 = new MapCartoTemplateLib.YTLegendItem(this.btnStyle.Style as ISymbol,
                    this.txtLegendItemName.Text, this.newSymbolButton1.Style as ISymbol);
            }
            else
            {
                this.jlklenendItem_0.Description = this.txtLegendItemName.Text;
                this.jlklenendItem_0.BackSymbol = this.newSymbolButton1.Style as ISymbol;
                this.jlklenendItem_0.Symbol = this.btnStyle.Style as ISymbol;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
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
            if (this.jlklenendItem_0 != null)
            {
                if (this.jlklenendItem_0.Symbol is IMarkerSymbol)
                {
                    this.btnStyle.Style = this.jlklenendItem_0.Symbol as IMarkerSymbol;
                    this.rdoPointSymbol.Checked = true;
                }
                else if (this.jlklenendItem_0.Symbol is ILineSymbol)
                {
                    this.btnStyle.Style = this.jlklenendItem_0.Symbol as ILineSymbol;
                    this.rdoLineSymbol.Checked = true;
                }
                else if (this.jlklenendItem_0.Symbol is IFillSymbol)
                {
                    this.btnStyle.Style = this.jlklenendItem_0.Symbol as IFillSymbol;
                    this.rdoFillSymbol.Checked = true;
                }
                this.newSymbolButton1.Style = this.jlklenendItem_0.BackSymbol;
                this.txtLegendItemName.Text = this.jlklenendItem_0.Description;
            }
            else if (this.rdoPointSymbol.Checked)
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

        private void newSymbolButton1_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
            selector.SetSymbol(this.ifillSymbol_1);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.newSymbolButton1.Style = selector.GetSymbol();
                this.ifillSymbol_1 = this.btnStyle.Style as IFillSymbol;
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

        internal MapCartoTemplateLib.YTLegendItem YTLegendItem
        {
            get { return this.jlklenendItem_0; }
            set
            {
                this.bool_1 = false;
                this.jlklenendItem_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
        }
    }
}