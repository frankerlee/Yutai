using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class RasterRenderPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IRasterLayer irasterLayer_1 = null;
        private IStyleGallery istyleGallery_0 = null;
        private IUserControl iuserControl_0 = null;
        private RasterClassifiedRenderPage rasterClassifiedRenderPage_0 = new RasterClassifiedRenderPage();
        private RasterRGBRendererPage rasterRGBRendererPage_0 = new RasterRGBRendererPage();
        private RasterStretchedRenderPage rasterStretchedRenderPage_0 = new RasterStretchedRenderPage();

        public RasterRenderPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.iuserControl_0 != null)
            {
                this.iuserControl_0.Apply();
            }
            return true;
        }

 private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.listBox1.Text == "Stretched")
                {
                    if (this.iuserControl_0 != null)
                    {
                        this.iuserControl_0.Visible = false;
                    }
                    this.rasterStretchedRenderPage_0.Visible = true;
                    this.iuserControl_0 = this.rasterStretchedRenderPage_0;
                }
                else if (this.listBox1.Text == "分类")
                {
                    if (this.iuserControl_0 != null)
                    {
                        this.iuserControl_0.Visible = false;
                    }
                    this.rasterClassifiedRenderPage_0.Visible = true;
                    this.iuserControl_0 = this.rasterClassifiedRenderPage_0;
                }
                else if (this.listBox1.Text == "RGB")
                {
                    if (this.iuserControl_0 != null)
                    {
                        this.iuserControl_0.Visible = false;
                    }
                    this.rasterRGBRendererPage_0.Visible = true;
                    this.iuserControl_0 = this.rasterRGBRendererPage_0;
                }
            }
        }

        private void method_0()
        {
            if (((this.irasterLayer_1 != null) && ((this.ibasicMap_0 == null) || this.irasterLayer_1.Renderer.CanRender(this.irasterLayer_1.Raster))) && (this.irasterLayer_1.Raster != null))
            {
                int count = 1;
                if (this.irasterLayer_1.Raster is IRasterBandCollection)
                {
                    count = (this.irasterLayer_1.Raster as IRasterBandCollection).Count;
                }
                this.listBox1.Items.Clear();
                if (count == 3)
                {
                    this.listBox1.Items.Add("Stretched");
                    this.listBox1.Items.Add("RGB");
                }
                else
                {
                    this.listBox1.Items.Add("Stretched");
                    this.listBox1.Items.Add("分类");
                }
                if (this.iuserControl_0 != null)
                {
                    (this.iuserControl_0 as UserControl).Visible = false;
                }
                IRasterRenderer renderer = this.irasterLayer_1.Renderer;
                if (renderer is IRasterStretchColorRampRenderer)
                {
                    this.listBox1.SelectedIndex = 0;
                    this.iuserControl_0 = this.rasterStretchedRenderPage_0;
                    this.rasterClassifiedRenderPage_0.Visible = false;
                    this.rasterStretchedRenderPage_0.Visible = true;
                    this.rasterRGBRendererPage_0.Visible = false;
                }
                else if (renderer is IRasterClassifyColorRampRenderer)
                {
                    this.listBox1.SelectedIndex = 1;
                    this.iuserControl_0 = this.rasterClassifiedRenderPage_0;
                    this.rasterClassifiedRenderPage_0.Visible = true;
                    this.rasterStretchedRenderPage_0.Visible = false;
                    this.rasterRGBRendererPage_0.Visible = false;
                }
                else if (renderer is IRasterRGBRenderer)
                {
                    this.listBox1.SelectedIndex = 1;
                    this.iuserControl_0 = this.rasterRGBRendererPage_0;
                    this.rasterClassifiedRenderPage_0.Visible = false;
                    this.rasterStretchedRenderPage_0.Visible = false;
                    this.rasterRGBRendererPage_0.Visible = true;
                }
                else if (!(renderer is IRasterUniqueValueRenderer))
                {
                }
            }
        }

        private void RasterRenderPropertyPage_Load(object sender, EventArgs e)
        {
            this.rasterClassifiedRenderPage_0.Dock = DockStyle.Fill;
            this.rasterStretchedRenderPage_0.Dock = DockStyle.Fill;
            this.rasterRGBRendererPage_0.Dock = DockStyle.Fill;
            this.rasterStretchedRenderPage_0.Visible = false;
            this.rasterClassifiedRenderPage_0.Visible = false;
            this.panel1.Controls.Add(this.rasterClassifiedRenderPage_0);
            this.panel1.Controls.Add(this.rasterStretchedRenderPage_0);
            this.rasterRGBRendererPage_0.Visible = false;
            this.panel1.Controls.Add(this.rasterRGBRendererPage_0);
            this.method_0();
            this.bool_0 = true;
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public ILayer Layer
        {
            set
            {
                this.irasterLayer_1 = value as IRasterLayer;
            }
        }

        public object SelectItem
        {
            set
            {
                this.irasterLayer_1 = value as IRasterLayer;
                this.rasterClassifiedRenderPage_0.CurrentLayer = this.irasterLayer_1;
                this.rasterStretchedRenderPage_0.CurrentLayer = this.irasterLayer_1;
                this.rasterRGBRendererPage_0.CurrentLayer = this.irasterLayer_1;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
                this.rasterClassifiedRenderPage_0.StyleGallery = this.istyleGallery_0;
                this.rasterStretchedRenderPage_0.StyleGallery = this.istyleGallery_0;
            }
        }
    }
}

