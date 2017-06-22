using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class RasterStretchedRenderPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IRasterStretchColorRampRenderer irasterStretchColorRampRenderer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public RasterStretchedRenderPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                IObjectCopy copy = new ObjectCopyClass();
                IRasterStretchColorRampRenderer renderer1 = copy.Copy(this.irasterStretchColorRampRenderer_0) as IRasterStretchColorRampRenderer;
                this.irasterLayer_0.Renderer = this.irasterStretchColorRampRenderer_0 as IRasterRenderer;
            }
            catch (Exception)
            {
            }
        }

        private void btnStretch_Click(object sender, EventArgs e)
        {
            new frmRasteStrechSet { RasterStretch = this.irasterStretchColorRampRenderer_0 as IRasterStretch2 }.ShowDialog();
        }

        private void cboBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.cboBand.SelectedIndex != -1))
            {
                bool flag;
                IRasterStretchColorRampRenderer renderer = this.irasterStretchColorRampRenderer_0;
                IRasterRenderer renderer2 = (IRasterRenderer) renderer;
                renderer.BandIndex = this.cboBand.SelectedIndex;
                if (this.icolorRamp_0 == null)
                {
                    IAlgorithmicColorRamp ramp = new AlgorithmicColorRampClass();
                    IColor color = new RgbColorClass();
                    (color as IRgbColor).Red = 255;
                    (color as IRgbColor).Red = 0;
                    (color as IRgbColor).Green = 0;
                    (color as IRgbColor).Blue = 0;
                    IColor color2 = new RgbColorClass();
                    (color2 as IRgbColor).Red = 255;
                    (color2 as IRgbColor).Green = 255;
                    (color2 as IRgbColor).Blue = 255;
                    ramp.FromColor = color;
                    ramp.ToColor = color2;
                    this.icolorRamp_0 = ramp;
                }
                this.icolorRamp_0.Size = 255;
                this.icolorRamp_0.CreateRamp(out flag);
                renderer.ColorRamp = this.icolorRamp_0;
                renderer2.Update();
            }
        }

        private void cboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                bool flag;
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                (this.irasterStretchColorRampRenderer_0 as IRasterRendererColorRamp).ColorScheme = this.cboColorRamp.Text;
                IRasterStretchColorRampRenderer renderer = this.irasterStretchColorRampRenderer_0;
                IRasterRenderer renderer2 = (IRasterRenderer) renderer;
                if (this.icolorRamp_0 == null)
                {
                    IAlgorithmicColorRamp ramp = new AlgorithmicColorRampClass();
                    IColor color = new RgbColorClass();
                    (color as IRgbColor).Red = 255;
                    (color as IRgbColor).Red = 0;
                    (color as IRgbColor).Green = 0;
                    (color as IRgbColor).Blue = 0;
                    IColor color2 = new RgbColorClass();
                    (color2 as IRgbColor).Red = 255;
                    (color2 as IRgbColor).Green = 255;
                    (color2 as IRgbColor).Blue = 255;
                    ramp.FromColor = color;
                    ramp.ToColor = color2;
                    this.icolorRamp_0 = ramp;
                }
                this.icolorRamp_0.Size = 255;
                this.icolorRamp_0.CreateRamp(out flag);
                renderer.ColorRamp = this.icolorRamp_0;
                renderer2.Update();
            }
        }

        private void chkHillshade_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.irasterStretchColorRampRenderer_0 as IHillShadeInfo).UseHillShade = this.chkHillshade.Checked;
                this.lblZFactor.Enabled = this.chkHillshade.Checked;
                this.txtZFactor.Enabled = this.chkHillshade.Checked;
            }
        }

        private void chkShowBackground_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).Background = this.chkShowBackground.Checked;
                this.txtBackground.Enabled = this.chkShowBackground.Checked;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor backgroundColor = (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).BackgroundColor;
                if (backgroundColor != null)
                {
                    this.method_4(this.colorEdit1, backgroundColor);
                    (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).BackgroundColor = backgroundColor;
                }
            }
        }

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor noDataColor = (this.irasterStretchColorRampRenderer_0 as IRasterDisplayProps).NoDataColor;
                if (noDataColor != null)
                {
                    this.method_4(this.colorEdit2, noDataColor);
                    (this.irasterStretchColorRampRenderer_0 as IRasterDisplayProps).NoDataColor = noDataColor;
                }
            }
        }

 private void method_0()
        {
            IRasterBandCollection raster = this.irasterLayer_0.Raster as IRasterBandCollection;
            if (this.irasterLayer_0.BandCount == 1)
            {
                this.lblBand.Visible = false;
                this.cboBand.Visible = false;
                this.cboBand.Items.Add(new BandWrap(raster.Item(0)));
            }
            else
            {
                if (raster.Count != this.irasterLayer_0.BandCount)
                {
                    IName dataSourceName = (this.irasterLayer_0 as IDataLayer2).DataSourceName;
                    if (dataSourceName != null)
                    {
                        raster = dataSourceName.Open() as IRasterBandCollection;
                    }
                }
                IEnumRasterBand bands = raster.Bands;
                bands.Reset();
                for (IRasterBand band2 = bands.Next(); band2 != null; band2 = bands.Next())
                {
                    this.cboBand.Items.Add(new BandWrap(band2));
                }
            }
            this.cboBand.SelectedIndex = this.irasterStretchColorRampRenderer_0.BandIndex;
            IColor backgroundColor = (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).BackgroundColor;
            if (backgroundColor != null)
            {
                this.method_1(this.colorEdit1, backgroundColor);
            }
            backgroundColor = (this.irasterStretchColorRampRenderer_0 as IRasterDisplayProps).NoDataColor;
            if (backgroundColor != null)
            {
                this.method_1(this.colorEdit2, backgroundColor);
            }
            this.lblHight.Text = (this.irasterStretchColorRampRenderer_0 as IRasterStretchMinMax).CustomStretchMax.ToString("0.###");
            this.lblLow.Text = (this.irasterStretchColorRampRenderer_0 as IRasterStretchMinMax).CustomStretchMin.ToString("0.###");
            this.chkHillshade.Checked = (this.irasterStretchColorRampRenderer_0 as IHillShadeInfo).UseHillShade;
            this.txtZFactor.Text = (this.irasterStretchColorRampRenderer_0 as IHillShadeInfo).ZScale.ToString();
            this.txtHeight.Text = this.irasterStretchColorRampRenderer_0.LabelHigh;
            this.txtLow.Text = this.irasterStretchColorRampRenderer_0.LabelLow;
            this.txtMedium.Text = this.irasterStretchColorRampRenderer_0.LabelMedium;
            this.lblZFactor.Enabled = this.chkHillshade.Checked;
            this.txtZFactor.Enabled = this.chkHillshade.Checked;
            this.chkShowBackground.Checked = (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).Background;
            this.txtBackground.Enabled = this.chkShowBackground.Checked;
            this.txtBackground.Text = (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).BackgroundValue.ToString();
        }

        private void method_1(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
                this.method_2((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_2(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
            int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_3(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |= ((uint)int_1);
            num = num << 8;
            return (int) (num | (uint)int_0);
        }

        private void method_4(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_3(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void RasterStretchedRenderPage_Load(object sender, EventArgs e)
        {
            int num = -1;
            if (this.irasterStretchColorRampRenderer_0 != null)
            {
                string colorScheme = (this.irasterStretchColorRampRenderer_0 as IRasterRendererColorRamp).ColorScheme;
                if (this.istyleGallery_0 != null)
                {
                    IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                    item.Reset();
                    for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                    {
                        this.cboColorRamp.Add(item2);
                        if (item2.Name == colorScheme)
                        {
                            num = this.cboColorRamp.Items.Count - 1;
                        }
                    }
                    item = null;
                    GC.Collect();
                }
                if (this.cboColorRamp.Items.Count == 0)
                {
                    this.cboColorRamp.Enabled = false;
                    IRandomColorRamp ramp = new RandomColorRampClass {
                        StartHue = 40,
                        EndHue = 120,
                        MinValue = 65,
                        MaxValue = 90,
                        MinSaturation = 25,
                        MaxSaturation = 45,
                        Size = 5,
                        Seed = 23
                    };
                    this.icolorRamp_0 = ramp;
                }
                else
                {
                    this.cboColorRamp.SelectedIndex = num;
                    if (this.cboColorRamp.SelectedIndex == -1)
                    {
                        this.cboColorRamp.SelectedIndex = 0;
                    }
                    this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                    (this.irasterStretchColorRampRenderer_0 as IRasterRendererColorRamp).ColorScheme = this.cboColorRamp.Text;
                }
                if (this.irasterLayer_0 != null)
                {
                    this.method_0();
                }
                this.bool_0 = true;
            }
        }

        private void txtBackground_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.irasterStretchColorRampRenderer_0 as IRasterStretch2).BackgroundValue = double.Parse(this.txtBackground.Text);
                }
                catch
                {
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.irasterStretchColorRampRenderer_0.LabelHigh = this.txtHeight.Text;
            }
        }

        private void txtLow_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.irasterStretchColorRampRenderer_0.LabelLow = this.txtLow.Text;
            }
        }

        private void txtMedium_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.irasterStretchColorRampRenderer_0.LabelMedium = this.txtMedium.Text;
            }
        }

        private void txtZFactor_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.irasterStretchColorRampRenderer_0 as IHillShadeInfo).ZScale = double.Parse(this.txtZFactor.Text);
                }
                catch
                {
                }
            }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.irasterLayer_0 = value as IRasterLayer;
                if (this.irasterLayer_0 == null)
                {
                    this.irasterStretchColorRampRenderer_0 = null;
                }
                else
                {
                    IRasterStretchColorRampRenderer pInObject = this.irasterLayer_0.Renderer as IRasterStretchColorRampRenderer;
                    if (pInObject == null)
                    {
                        if (this.irasterStretchColorRampRenderer_0 == null)
                        {
                            this.irasterStretchColorRampRenderer_0 = RenderHelper.RasterStretchRenderer(this.irasterLayer_0);
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.irasterStretchColorRampRenderer_0 = copy.Copy(pInObject) as IRasterStretchColorRampRenderer;
                    }
                    if (this.bool_0)
                    {
                        this.bool_0 = false;
                        this.method_0();
                        this.bool_0 = true;
                    }
                }
            }
        }

        bool IUserControl.Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }

        internal partial class BandWrap
        {
            private IRasterBand irasterBand_0 = null;

            internal BandWrap(IRasterBand irasterBand_1)
            {
                this.irasterBand_0 = irasterBand_1;
            }

            public override string ToString()
            {
                return this.irasterBand_0.Bandname;
            }

            internal IRasterBand RasterBand
            {
                get
                {
                    return this.irasterBand_0;
                }
            }
        }
    }
}

