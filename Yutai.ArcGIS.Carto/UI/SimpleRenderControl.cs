using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class SimpleRenderControl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ISimpleRenderer isimpleRenderer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public SimpleRenderControl()
        {
            //  MessageBox.Show("Testing.....");
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.isimpleRenderer_0.Label = this.txtLabel.Text;
            this.isimpleRenderer_0.Description = this.txtDescription.Text;
            IObjectCopy copy = new ObjectCopyClass();
            ISimpleRenderer renderer = copy.Copy(this.isimpleRenderer_0) as ISimpleRenderer;
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Click Style Button");
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnStyle.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnStyle.Style = selector.GetSymbol();
                        this.isimpleRenderer_0.Symbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void method_0()
        {
            if (this.isimpleRenderer_0 != null)
            {
                this.btnStyle.Style = this.isimpleRenderer_0.Symbol;
                this.txtLabel.Text = this.isimpleRenderer_0.Label;
                this.txtDescription.Text = this.isimpleRenderer_0.Description;
            }
        }

        private IColor method_1()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return new RgbColorClass
            {
                Red = (int) (255.0*random.NextDouble()),
                Green = (int) (255.0*random.NextDouble()),
                Blue = (int) (255.0*random.NextDouble())
            };
        }

        private ISymbol method_2(esriGeometryType esriGeometryType_0)
        {
            ISymbol symbol = null;
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    symbol = new SimpleMarkerSymbolClass();
                    (symbol as IMarkerSymbol).Color = this.method_1();
                    return symbol;

                case esriGeometryType.esriGeometryPolyline:
                    symbol = new SimpleLineSymbolClass();
                    (symbol as ILineSymbol).Color = this.method_1();
                    return symbol;

                case esriGeometryType.esriGeometryPolygon:
                    symbol = new SimpleFillSymbolClass();
                    (symbol as IFillSymbol).Color = this.method_1();
                    return symbol;
            }
            return symbol;
        }

        private void SimpleRenderControl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.igeoFeatureLayer_0 == null)
                {
                    this.isimpleRenderer_0 = null;
                }
                else
                {
                    ISimpleRenderer pInObject = this.igeoFeatureLayer_0.Renderer as ISimpleRenderer;
                    if (pInObject == null)
                    {
                        if (this.isimpleRenderer_0 == null)
                        {
                            this.isimpleRenderer_0 = new SimpleRendererClass();
                            if (this.igeoFeatureLayer_0.FeatureClass != null)
                            {
                                this.isimpleRenderer_0.Symbol =
                                    this.method_2(this.igeoFeatureLayer_0.FeatureClass.ShapeType);
                            }
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.isimpleRenderer_0 = copy.Copy(pInObject) as ISimpleRenderer;
                    }
                    if (this.bool_0)
                    {
                        this.method_0();
                    }
                }
            }
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
    }
}