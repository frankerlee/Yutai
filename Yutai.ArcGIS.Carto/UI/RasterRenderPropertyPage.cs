using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public class RasterRenderPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IRasterLayer irasterLayer_1 = null;
        private IStyleGallery istyleGallery_0 = null;
        private IUserControl iuserControl_0 = null;
        private ListBox listBox1;
        private Panel panel1;
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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.listBox1 = new ListBox();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(6, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(120, 0x100);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.panel1.Location = new Point(0x84, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x18d, 0x10d);
            this.panel1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.listBox1);
            base.Name = "RasterRenderPropertyPage";
            base.Size = new Size(0x214, 0x114);
            base.Load += new EventHandler(this.RasterRenderPropertyPage_Load);
            base.ResumeLayout(false);
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

