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
    public class RasterStretchedRenderPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private Button btnStretch;
        private System.Windows.Forms.ComboBox cboBand;
        private StyleComboBox cboColorRamp;
        private CheckBox chkHillshade;
        private CheckBox chkShowBackground;
        private ColorEdit colorEdit1;
        private ColorEdit colorEdit2;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IRasterStretchColorRampRenderer irasterStretchColorRampRenderer_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblBand;
        private Label lblHight;
        private Label lblLow;
        private Label lblZFactor;
        private TextBox txtBackground;
        private TextBox txtHeight;
        private TextBox txtLow;
        private TextBox txtMedium;
        private TextBox txtZFactor;

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
                    (color as IRgbColor).Red = 0xff;
                    (color as IRgbColor).Red = 0;
                    (color as IRgbColor).Green = 0;
                    (color as IRgbColor).Blue = 0;
                    IColor color2 = new RgbColorClass();
                    (color2 as IRgbColor).Red = 0xff;
                    (color2 as IRgbColor).Green = 0xff;
                    (color2 as IRgbColor).Blue = 0xff;
                    ramp.FromColor = color;
                    ramp.ToColor = color2;
                    this.icolorRamp_0 = ramp;
                }
                this.icolorRamp_0.Size = 0xff;
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
                    (color as IRgbColor).Red = 0xff;
                    (color as IRgbColor).Red = 0;
                    (color as IRgbColor).Green = 0;
                    (color as IRgbColor).Blue = 0;
                    IColor color2 = new RgbColorClass();
                    (color2 as IRgbColor).Red = 0xff;
                    (color2 as IRgbColor).Green = 0xff;
                    (color2 as IRgbColor).Blue = 0xff;
                    ramp.FromColor = color;
                    ramp.ToColor = color2;
                    this.icolorRamp_0 = ramp;
                }
                this.icolorRamp_0.Size = 0xff;
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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            this.label1 = new Label();
            this.label2 = new Label();
            this.lblLow = new Label();
            this.label4 = new Label();
            this.txtHeight = new TextBox();
            this.txtMedium = new TextBox();
            this.txtLow = new TextBox();
            this.cboColorRamp = new StyleComboBox(this.icontainer_0);
            this.label5 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.colorEdit2 = new ColorEdit();
            this.lblBand = new Label();
            this.cboBand = new System.Windows.Forms.ComboBox();
            this.lblHight = new Label();
            this.chkShowBackground = new CheckBox();
            this.chkHillshade = new CheckBox();
            this.lblZFactor = new Label();
            this.txtZFactor = new TextBox();
            this.label3 = new Label();
            this.btnStretch = new Button();
            this.txtBackground = new TextBox();
            this.colorEdit1.Properties.BeginInit();
            this.colorEdit2.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2e, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "值";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x77, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "标记";
            this.lblLow.AutoSize = true;
            this.lblLow.Location = new Point(0x30, 0x68);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new Size(0x29, 12);
            this.lblLow.TabIndex = 2;
            this.lblLow.Text = "label3";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xf9, 0xae);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "背景值颜色";
            this.txtHeight.Location = new Point(0x79, 0x2f);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(100, 0x15);
            this.txtHeight.TabIndex = 4;
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtMedium.Location = new Point(0x79, 0x4a);
            this.txtMedium.Name = "txtMedium";
            this.txtMedium.Size = new Size(100, 0x15);
            this.txtMedium.TabIndex = 5;
            this.txtMedium.TextChanged += new EventHandler(this.txtMedium_TextChanged);
            this.txtLow.Location = new Point(0x79, 0x65);
            this.txtLow.Name = "txtLow";
            this.txtLow.Size = new Size(100, 0x15);
            this.txtLow.TabIndex = 6;
            this.txtLow.TextChanged += new EventHandler(this.txtLow_TextChanged);
            this.cboColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboColorRamp.DropDownWidth = 160;
            this.cboColorRamp.Location = new Point(90, 0x87);
            this.cboColorRamp.Name = "cboColorRamp";
            this.cboColorRamp.Size = new Size(0x90, 0x16);
            this.cboColorRamp.TabIndex = 8;
            this.cboColorRamp.SelectedIndexChanged += new EventHandler(this.cboColorRamp_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x22, 0x8a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "颜色方案";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x14c, 0xa8);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 0x2e;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.colorEdit2.EditValue = Color.Empty;
            this.colorEdit2.Location = new Point(0x14c, 0xc3);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit2.Size = new Size(0x30, 0x15);
            this.colorEdit2.TabIndex = 0x30;
            this.colorEdit2.EditValueChanged += new EventHandler(this.colorEdit2_EditValueChanged);
            this.lblBand.AutoSize = true;
            this.lblBand.Location = new Point(0x12, 8);
            this.lblBand.Name = "lblBand";
            this.lblBand.Size = new Size(0x1d, 12);
            this.lblBand.TabIndex = 0x31;
            this.lblBand.Text = "波段";
            this.cboBand.FormattingEnabled = true;
            this.cboBand.Location = new Point(0x51, 8);
            this.cboBand.Name = "cboBand";
            this.cboBand.Size = new Size(0x108, 20);
            this.cboBand.TabIndex = 50;
            this.cboBand.SelectedIndexChanged += new EventHandler(this.cboBand_SelectedIndexChanged);
            this.lblHight.AutoSize = true;
            this.lblHight.Location = new Point(0x2e, 0x2f);
            this.lblHight.Name = "lblHight";
            this.lblHight.Size = new Size(0x29, 12);
            this.lblHight.TabIndex = 0x33;
            this.lblHight.Text = "label9";
            this.chkShowBackground.AutoSize = true;
            this.chkShowBackground.Location = new Point(0x24, 0xad);
            this.chkShowBackground.Name = "chkShowBackground";
            this.chkShowBackground.Size = new Size(0x54, 0x10);
            this.chkShowBackground.TabIndex = 0x34;
            this.chkShowBackground.Text = "显示背景值";
            this.chkShowBackground.UseVisualStyleBackColor = true;
            this.chkShowBackground.CheckedChanged += new EventHandler(this.chkShowBackground_CheckedChanged);
            this.chkHillshade.AutoSize = true;
            this.chkHillshade.Location = new Point(0x24, 0xc4);
            this.chkHillshade.Name = "chkHillshade";
            this.chkHillshade.Size = new Size(0x48, 0x10);
            this.chkHillshade.TabIndex = 0x35;
            this.chkHillshade.Text = "显示山影";
            this.chkHillshade.UseVisualStyleBackColor = true;
            this.chkHillshade.CheckedChanged += new EventHandler(this.chkHillshade_CheckedChanged);
            this.lblZFactor.AutoSize = true;
            this.lblZFactor.Location = new Point(0x77, 0xc4);
            this.lblZFactor.Name = "lblZFactor";
            this.lblZFactor.Size = new Size(0x23, 12);
            this.lblZFactor.TabIndex = 0x38;
            this.lblZFactor.Text = "Z因子";
            this.txtZFactor.Location = new Point(160, 0xc0);
            this.txtZFactor.Name = "txtZFactor";
            this.txtZFactor.Size = new Size(0x3d, 0x15);
            this.txtZFactor.TabIndex = 0x37;
            this.txtZFactor.Text = "1";
            this.txtZFactor.TextChanged += new EventHandler(this.txtZFactor_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xf9, 0xc7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 12);
            this.label3.TabIndex = 0x36;
            this.label3.Text = "空值显示颜色";
            this.btnStretch.Location = new Point(0xfb, 0xdf);
            this.btnStretch.Name = "btnStretch";
            this.btnStretch.Size = new Size(0x5e, 0x17);
            this.btnStretch.TabIndex = 0x39;
            this.btnStretch.Text = "拉伸设置...";
            this.btnStretch.UseVisualStyleBackColor = true;
            this.btnStretch.Click += new EventHandler(this.btnStretch_Click);
            this.txtBackground.Location = new Point(160, 0xa3);
            this.txtBackground.Name = "txtBackground";
            this.txtBackground.Size = new Size(0x3d, 0x15);
            this.txtBackground.TabIndex = 0x3a;
            this.txtBackground.Text = "0";
            this.txtBackground.TextChanged += new EventHandler(this.txtBackground_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtBackground);
            base.Controls.Add(this.btnStretch);
            base.Controls.Add(this.lblZFactor);
            base.Controls.Add(this.txtZFactor);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.chkHillshade);
            base.Controls.Add(this.chkShowBackground);
            base.Controls.Add(this.lblHight);
            base.Controls.Add(this.cboBand);
            base.Controls.Add(this.lblBand);
            base.Controls.Add(this.colorEdit2);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.cboColorRamp);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtLow);
            base.Controls.Add(this.txtMedium);
            base.Controls.Add(this.txtHeight);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.lblLow);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "RasterStretchedRenderPage";
            base.Size = new Size(0x1b7, 0xf9);
            base.Load += new EventHandler(this.RasterStretchedRenderPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.colorEdit2.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
            uint num = uint_0 & 0xff0000;
            int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
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
                        MinValue = 0x41,
                        MaxValue = 90,
                        MinSaturation = 0x19,
                        MaxSaturation = 0x2d,
                        Size = 5,
                        Seed = 0x17
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

        internal class BandWrap
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

