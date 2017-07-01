using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class TinSimpleRenderCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private ITinLayer itinLayer_0 = null;

        public TinSimpleRenderCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnStyle.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnStyle.Style = selector.GetSymbol();
                        this.itinSingleSymbolRenderer_0.Symbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void method_0()
        {
            this.txtDescription.Text = this.itinSingleSymbolRenderer_0.Description;
            this.txtLabel.Text = this.itinSingleSymbolRenderer_0.Label;
            this.btnStyle.Style = this.itinSingleSymbolRenderer_0.Symbol;
        }

        private void TinSimpleRenderCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public ILayer CurrentLayer
        {
            set { this.itinLayer_0 = value as ITinLayer; }
        }

        bool IUserControl.Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
        }

        public ITinRenderer TinRenderer
        {
            set
            {
                this.itinSingleSymbolRenderer_0 = value as ITinSingleSymbolRenderer;
                if (this.bool_0)
                {
                    this.bool_0 = false;
                    this.method_0();
                    this.bool_0 = true;
                }
            }
        }
    }
}