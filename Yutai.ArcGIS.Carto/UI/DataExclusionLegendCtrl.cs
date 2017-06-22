using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class DataExclusionLegendCtrl : UserControl
    {
        private Container container_0 = null;
        private esriGeometryType esriGeometryType_0 = esriGeometryType.esriGeometryPoint;
        private IDataExclusion idataExclusion_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public DataExclusionLegendCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.idataExclusion_0 != null)
            {
                this.idataExclusion_0.ShowExclusionClass = this.chkShowExclusionClass.Checked;
                if (this.idataExclusion_0.ShowExclusionClass)
                {
                    this.idataExclusion_0.ExclusionSymbol = this.btnStyle.Style as ISymbol;
                    this.idataExclusion_0.ExclusionLabel = this.txtLabel.Text;
                    this.idataExclusion_0.ExclusionDescription = this.txtDescription.Text;
                }
            }
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
                        this.idataExclusion_0.ExclusionSymbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void chkShowExclusionClass_CheckedChanged(object sender, EventArgs e)
        {
            this.btnStyle.Enabled = this.chkShowExclusionClass.Checked;
            this.txtLabel.Enabled = this.chkShowExclusionClass.Checked;
            this.txtDescription.Enabled = this.chkShowExclusionClass.Checked;
        }

        private void DataExclusionLegendCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            bool showExclusionClass = false;
            if (this.idataExclusion_0 != null)
            {
                showExclusionClass = this.idataExclusion_0.ShowExclusionClass;
            }
            this.chkShowExclusionClass.Checked = showExclusionClass;
            this.btnStyle.Enabled = showExclusionClass;
            this.txtLabel.Enabled = showExclusionClass;
            this.txtDescription.Enabled = showExclusionClass;
            if (showExclusionClass)
            {
                if (this.idataExclusion_0.ExclusionSymbol == null)
                {
                    this.idataExclusion_0.ExclusionSymbol = this.method_1(this.esriGeometryType_0);
                }
                this.btnStyle.Style = this.idataExclusion_0.ExclusionSymbol;
                this.txtLabel.Text = this.idataExclusion_0.ExclusionLabel;
                this.txtDescription.Text = this.idataExclusion_0.ExclusionDescription;
            }
        }

        private ISymbol method_1(esriGeometryType esriGeometryType_1)
        {
            switch (esriGeometryType_1)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                {
                    IMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                        Size = 3.0
                    };
                    return (symbol as ISymbol);
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ILineSymbol symbol2 = new SimpleLineSymbolClass {
                        Width = 0.2
                    };
                    return (symbol2 as ISymbol);
                }
                case esriGeometryType.esriGeometryPolygon:
                    return new SimpleFillSymbolClass();
            }
            return null;
        }

        public IDataExclusion DataExclusion
        {
            set
            {
                this.idataExclusion_0 = value;
            }
        }

        public esriGeometryType GeometryType
        {
            set
            {
                this.esriGeometryType_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }
    }
}

