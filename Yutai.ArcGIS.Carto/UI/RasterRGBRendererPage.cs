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
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class RasterRGBRendererPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IRasterRGBRenderer irasterRGBRenderer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public RasterRGBRendererPage()
        {
            this.InitializeComponent();
            this.comboBox_0 = new System.Windows.Forms.ComboBox();
            this.exListView1.CheckBoxes = true;
            this.exListView1.MySortBrush = SystemBrushes.ControlLight;
            this.exListView1.MyHighlightBrush = Brushes.Goldenrod;
            this.exListView1.GridLines = true;
            ColumnHeader header = new ColumnHeader
            {
                Text = "通道"
            };
            this.exListView1.Columns.Add(header);
            EXEditableColumnHeader header2 = new EXEditableColumnHeader("波段", this.comboBox_0, 80);
            this.exListView1.Columns.Add(header2);
            EXBoolColumnHeader header3 = new EXBoolColumnHeader("可见性", 80)
            {
                Editable = true,
                TrueImage =
                    new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Carto.checked.bmp")),
                FalseImage =
                    new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Carto.uncheck.bmp"))
            };
            this.exListView1.Columns.Add(header3);
        }

        public void Apply()
        {
            try
            {
                for (int i = 0; i < this.exListView1.Items.Count; i++)
                {
                    EXListViewItem item = this.exListView1.Items[i] as EXListViewItem;
                    string text = item.SubItems[1].Text;
                    int num2 = 0;
                    int num3 = 0;
                    while (num3 < this.comboBox_0.Items.Count)
                    {
                        if (text == this.comboBox_0.Items[num3].ToString())
                        {
                            goto Label_0078;
                        }
                        num3++;
                    }
                    goto Label_007B;
                    Label_0078:
                    num2 = num3;
                    Label_007B:
                    if (i == 0)
                    {
                        this.irasterRGBRenderer_0.RedBandIndex = num2;
                        this.irasterRGBRenderer_0.UseRedBand = (item.SubItems[2] as EXBoolListViewSubItem).BoolValue;
                    }
                    else if (i == 1)
                    {
                        this.irasterRGBRenderer_0.GreenBandIndex = num2;
                        this.irasterRGBRenderer_0.UseGreenBand = (item.SubItems[2] as EXBoolListViewSubItem).BoolValue;
                    }
                    else
                    {
                        this.irasterRGBRenderer_0.BlueBandIndex = num2;
                        this.irasterRGBRenderer_0.UseBlueBand = (item.SubItems[2] as EXBoolListViewSubItem).BoolValue;
                    }
                }
                if (this.chkShowBackground.Checked)
                {
                    try
                    {
                        double[] numArray = new double[]
                        {
                            double.Parse(this.txtBackgroundR.Text), double.Parse(this.txtBackgroundG.Text),
                            double.Parse(this.txtBackgroundB.Text)
                        };
                        (this.irasterRGBRenderer_0 as IRasterStretch2).BackgroundValue = numArray;
                    }
                    catch
                    {
                    }
                }
                IObjectCopy copy = new ObjectCopyClass();
                IRasterRGBRenderer renderer1 = copy.Copy(this.irasterRGBRenderer_0) as IRasterRGBRenderer;
                this.irasterLayer_0.Renderer = this.irasterRGBRenderer_0 as IRasterRenderer;
            }
            catch (Exception)
            {
            }
        }

        private void btnStretch_Click(object sender, EventArgs e)
        {
            new frmRasteStrechSet {RasterStretch = this.irasterRGBRenderer_0 as IRasterStretch2}.ShowDialog();
        }

        private void chkShowBackground_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.irasterRGBRenderer_0 as IRasterStretch2).Background = this.chkShowBackground.Checked;
                this.txtBackgroundR.Enabled = this.chkShowBackground.Checked;
                this.txtBackgroundG.Enabled = this.chkShowBackground.Checked;
                this.txtBackgroundB.Enabled = this.chkShowBackground.Checked;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor backgroundColor = (this.irasterRGBRenderer_0 as IRasterStretch2).BackgroundColor;
                if (backgroundColor != null)
                {
                    this.method_4(this.colorEdit1, backgroundColor);
                    (this.irasterRGBRenderer_0 as IRasterStretch2).BackgroundColor = backgroundColor;
                }
            }
        }

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor noDataColor = (this.irasterRGBRenderer_0 as IRasterDisplayProps).NoDataColor;
                if (noDataColor != null)
                {
                    this.method_4(this.colorEdit2, noDataColor);
                    (this.irasterRGBRenderer_0 as IRasterDisplayProps).NoDataColor = noDataColor;
                }
            }
        }

        private void method_0()
        {
            IRasterBandCollection raster = this.irasterLayer_0.Raster as IRasterBandCollection;
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
                this.comboBox_0.Items.Add(new BandWrap(band2));
            }
            string[] strArray = new string[] {"红色", raster.Item(this.irasterRGBRenderer_0.RedBandIndex).Bandname};
            EXListViewItem item = new EXListViewItem(strArray);
            item.SubItems.Add(new EXBoolListViewSubItem(this.irasterRGBRenderer_0.UseRedBand));
            this.exListView1.Items.Add(item);
            strArray[0] = "绿色";
            strArray[1] = raster.Item(this.irasterRGBRenderer_0.GreenBandIndex).Bandname;
            item = new EXListViewItem(strArray);
            item.SubItems.Add(new EXBoolListViewSubItem(this.irasterRGBRenderer_0.UseGreenBand));
            this.exListView1.Items.Add(item);
            strArray[0] = "蓝色";
            strArray[1] = raster.Item(this.irasterRGBRenderer_0.BlueBandIndex).Bandname;
            item = new EXListViewItem(strArray);
            item.SubItems.Add(new EXBoolListViewSubItem(this.irasterRGBRenderer_0.UseBlueBand));
            this.exListView1.Items.Add(item);
            IColor backgroundColor = (this.irasterRGBRenderer_0 as IRasterStretch2).BackgroundColor;
            if (backgroundColor != null)
            {
                this.method_1(this.colorEdit1, backgroundColor);
            }
            backgroundColor = (this.irasterRGBRenderer_0 as IRasterDisplayProps).NoDataColor;
            if (backgroundColor != null)
            {
                this.method_1(this.colorEdit2, backgroundColor);
            }
            object backgroundValue = (this.irasterRGBRenderer_0 as IRasterStretch2).BackgroundValue;
            this.chkShowBackground.Checked = (this.irasterRGBRenderer_0 as IRasterStretch2).Background;
            this.txtBackgroundR.Enabled = this.chkShowBackground.Checked;
            this.txtBackgroundG.Enabled = this.chkShowBackground.Checked;
            this.txtBackgroundB.Enabled = this.chkShowBackground.Checked;
            double[] numArray = backgroundValue as double[];
            this.txtBackgroundR.Text = numArray[0].ToString();
            this.txtBackgroundG.Text = numArray[1].ToString();
            this.txtBackgroundB.Text = numArray[2].ToString();
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
                uint rGB = (uint) icolor_0.RGB;
                this.method_2(rGB, out num2, out num3, out num4);
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
            num |= (uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
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

        private void RasterRGBRendererPage_Load(object sender, EventArgs e)
        {
            if (this.irasterRGBRenderer_0 != null)
            {
                if (this.irasterLayer_0 != null)
                {
                    this.method_0();
                }
                this.bool_0 = true;
            }
        }

        private void txtBackgroundB_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtBackgroundG_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtBackgroundR_TextChanged(object sender, EventArgs e)
        {
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.irasterLayer_0 = value as IRasterLayer;
                if (this.irasterLayer_0 == null)
                {
                    this.irasterRGBRenderer_0 = null;
                }
                else
                {
                    IRasterRGBRenderer pInObject = this.irasterLayer_0.Renderer as IRasterRGBRenderer;
                    if (pInObject == null)
                    {
                        if (this.irasterRGBRenderer_0 == null)
                        {
                            this.irasterRGBRenderer_0 = RenderHelper.RasterRGBRenderer(this.irasterLayer_0);
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.irasterRGBRenderer_0 = copy.Copy(pInObject) as IRasterRGBRenderer;
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
            get { return base.Visible; }
            set { base.Visible = value; }
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
                get { return this.irasterBand_0; }
            }
        }
    }
}