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
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class OverviewWindowsAllLayerProperty : Form
    {
        private Color m_BackgroundColor = Color.White;
        private IFillSymbol m_pFillSymbol = new SimpleFillSymbolClass();
        private IMap m_pMap = null;
        private IMap m_pOverviewMap = null;
        private IStyleGallery m_pSG = null;
        private bool m_ZoomWithMainView = false;

        public OverviewWindowsAllLayerProperty()
        {
            this.InitializeComponent();
        }

        private void btnFillSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(this.m_pSG);
                selector.SetSymbol(this.btnFillSymbol.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnFillSymbol.Style = selector.GetSymbol();
                }
            }
            catch
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_ZoomWithMainView = this.checkEdit1.Checked;
            this.m_BackgroundColor = this.colorEdit1.Color;
            this.m_pFillSymbol = this.btnFillSymbol.Style as IFillSymbol;
        }

 private void OverviewWindowsProperty_Load(object sender, EventArgs e)
        {
            this.checkEdit1.Checked = this.m_ZoomWithMainView;
            this.colorEdit1.Color = this.m_BackgroundColor;
            this.btnFillSymbol.Style = this.m_pFillSymbol;
            ILayer layer2 = null;
            if (this.m_pOverviewMap.LayerCount > 0)
            {
                layer2 = this.m_pOverviewMap.get_Layer(0);
            }
        }

        private void ReadGroupLayer(IGroupLayer pGroupLayer, ILayer pOverviewLayer, ref int nIndex)
        {
        }

        internal void RestMap(IMap pMap)
        {
            pMap.ClearLayers();
            pMap.SpatialReferenceLocked = false;
            pMap.SpatialReference = null;
            pMap.MapUnits = esriUnits.esriUnknownUnits;
            pMap.DistanceUnits = esriUnits.esriUnknownUnits;
            pMap.RecalcFullExtent();
        }

        public IFillSymbol FillSymbol
        {
            get
            {
                return this.m_pFillSymbol;
            }
            set
            {
                if (value != null)
                {
                    this.m_pFillSymbol = value;
                }
            }
        }

        public IMap Map
        {
            set
            {
                this.m_pMap = value;
            }
        }

        public Color MapCtrlBackgroundColor
        {
            get
            {
                return this.m_BackgroundColor;
            }
            set
            {
                this.m_BackgroundColor = value;
            }
        }

        public IMap OverviewMap
        {
            set
            {
                this.m_pOverviewMap = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }

        public bool ZoomWithMainView
        {
            get
            {
                return this.m_ZoomWithMainView;
            }
            set
            {
                this.m_ZoomWithMainView = value;
            }
        }

        internal partial class LayerWrap
        {
            private ILayer m_pLayer = null;

            public LayerWrap(ILayer pLayer)
            {
                this.m_pLayer = pLayer;
            }

            public override string ToString()
            {
                if (this.m_pLayer == null)
                {
                    return "<无>";
                }
                return this.m_pLayer.Name;
            }

            public ILayer Layer
            {
                get
                {
                    return this.m_pLayer;
                }
            }
        }
    }
}

