using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Carto.UI
{
    public class BaseHeightPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private Button btnOpenFile;
        private ComboBox cboSufer;
        private ComboBox comboBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private I3DProperties i3DProperties_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0 = null;
        private Label label1;
        private RadioButton rdoBaseExpression;
        private RadioButton rdoBaseShape;
        private RadioButton rdoBaseSurface;
        private TextBox txtBaseExpression;
        private TextBox txtOffset;
        private TextBox txtZFeator;

        public BaseHeightPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.bool_0 = false;
            if (this.i3DProperties_0 != null)
            {
                if (this.rdoBaseExpression.Checked)
                {
                    this.i3DProperties_0.BaseOption = esriBaseOption.esriBaseExpression;
                    this.i3DProperties_0.BaseExpressionString = this.txtBaseExpression.Text;
                }
                else if (this.rdoBaseSurface.Checked)
                {
                    this.i3DProperties_0.BaseOption = esriBaseOption.esriBaseSurface;
                    this.i3DProperties_0.BaseSurface = (this.cboSufer.SelectedItem as SuferWrap).Surface;
                }
                else if (this.rdoBaseShape.Checked)
                {
                    this.i3DProperties_0.BaseOption = esriBaseOption.esriBaseShape;
                }
                this.i3DProperties_0.ZFactor = double.Parse(this.txtZFeator.Text);
                this.i3DProperties_0.OffsetExpressionString = this.txtOffset.Text;
            }
            return true;
        }

        private void BaseHeightPropertyPage_Load(object sender, EventArgs e)
        {
            int num;
            for (num = 0; num < this.ibasicMap_0.LayerCount; num++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(num);
                ISurface surface = this.method_0(layer);
                if (surface != null)
                {
                    this.cboSufer.Items.Add(new SuferWrap(surface));
                }
            }
            ILayerExtensions extensions = this.ilayer_0 as ILayerExtensions;
            for (int i = 0; i <= (extensions.ExtensionCount - 1); i++)
            {
                if (extensions.get_Extension(i) is I3DProperties)
                {
                    I3DProperties properties = extensions.get_Extension(i) as I3DProperties;
                    this.i3DProperties_0 = properties;
                    if (properties.BaseOption != esriBaseOption.esriBaseSurface)
                    {
                        if (properties.BaseOption == esriBaseOption.esriBaseExpression)
                        {
                            this.rdoBaseExpression.Checked = true;
                            this.txtBaseExpression.Text = properties.BaseExpressionString;
                        }
                        else
                        {
                            this.rdoBaseShape.Checked = true;
                        }
                    }
                    else if (properties.BaseSurface != null)
                    {
                        this.rdoBaseSurface.Checked = true;
                        ISurface baseSurface = properties.BaseSurface as ISurface;
                        bool flag = true;
                        for (num = 0; num < this.cboSufer.Items.Count; num++)
                        {
                            if ((this.cboSufer.Items[num] as SuferWrap).Surface == baseSurface)
                            {
                                this.cboSufer.SelectedIndex = num;
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            this.cboSufer.Items.Add(new SuferWrap(baseSurface));
                            this.cboSufer.SelectedIndex = this.cboSufer.Items.Count - 1;
                        }
                    }
                    this.txtOffset.Text = properties.OffsetExpressionString;
                    this.txtZFeator.Text = properties.ZFactor.ToString();
                    break;
                }
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                IFeatureClass featureClass = (this.ilayer_0 as IFeatureLayer).FeatureClass;
                if (featureClass != null)
                {
                    int index = featureClass.Fields.FindField(featureClass.ShapeFieldName);
                    if (!featureClass.Fields.get_Field(index).GeometryDef.HasZ)
                    {
                        this.rdoBaseShape.Enabled = false;
                    }
                    else
                    {
                        this.rdoBaseShape.Enabled = true;
                        if (this.rdoBaseShape.Checked)
                        {
                            this.rdoBaseExpression.Checked = true;
                        }
                    }
                }
            }
            else
            {
                this.rdoBaseShape.Enabled = false;
                if (this.rdoBaseShape.Checked)
                {
                    this.rdoBaseExpression.Checked = true;
                }
            }
            if (this.cboSufer.Items.Count == 0)
            {
                this.rdoBaseSurface.Enabled = false;
                this.cboSufer.Enabled = false;
                if (this.rdoBaseSurface.Checked)
                {
                    this.rdoBaseExpression.Checked = true;
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterSurfaceDatasets(), true);
            if (file.ShowDialog() == DialogResult.OK)
            {
                IDataset dataset = (file.Items.get_Element(0) as IGxDataset).Dataset;
                if (dataset != null)
                {
                    ISurface surface = this.method_1(dataset);
                    if (surface != null)
                    {
                        this.cboSufer.Items.Add(new SuferWrap(surface));
                        this.cboSufer.SelectedIndex = this.cboSufer.Items.Count - 1;
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex == 0)
            {
                this.txtZFeator.Text = "0.3048";
                this.txtZFeator.Enabled = false;
            }
            else if (this.comboBox2.SelectedIndex == 1)
            {
                this.txtZFeator.Text = "3.2810";
                this.txtZFeator.Enabled = false;
            }
            else if (this.comboBox2.SelectedIndex == 2)
            {
                this.txtZFeator.Enabled = true;
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
            this.groupBox1 = new GroupBox();
            this.btnOpenFile = new Button();
            this.cboSufer = new ComboBox();
            this.txtBaseExpression = new TextBox();
            this.rdoBaseShape = new RadioButton();
            this.rdoBaseSurface = new RadioButton();
            this.rdoBaseExpression = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.txtZFeator = new TextBox();
            this.comboBox2 = new ComboBox();
            this.label1 = new Label();
            this.groupBox3 = new GroupBox();
            this.txtOffset = new TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Controls.Add(this.cboSufer);
            this.groupBox1.Controls.Add(this.txtBaseExpression);
            this.groupBox1.Controls.Add(this.rdoBaseShape);
            this.groupBox1.Controls.Add(this.rdoBaseSurface);
            this.groupBox1.Controls.Add(this.rdoBaseExpression);
            this.groupBox1.Location = new Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(390, 0xa1);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基准高程";
            this.btnOpenFile.Location = new Point(0x13e, 0x61);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new Size(30, 0x17);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new EventHandler(this.btnOpenFile_Click);
            this.cboSufer.FormattingEnabled = true;
            this.cboSufer.Location = new Point(20, 100);
            this.cboSufer.Name = "cboSufer";
            this.cboSufer.Size = new Size(0x124, 20);
            this.cboSufer.TabIndex = 5;
            this.txtBaseExpression.Location = new Point(20, 0x2b);
            this.txtBaseExpression.Name = "txtBaseExpression";
            this.txtBaseExpression.Size = new Size(0x124, 0x15);
            this.txtBaseExpression.TabIndex = 4;
            this.txtBaseExpression.Text = "0";
            this.rdoBaseShape.AutoSize = true;
            this.rdoBaseShape.Location = new Point(20, 0x7e);
            this.rdoBaseShape.Name = "rdoBaseShape";
            this.rdoBaseShape.Size = new Size(0xa1, 0x10);
            this.rdoBaseShape.TabIndex = 3;
            this.rdoBaseShape.Text = "使用图层Z值设置图层高程";
            this.rdoBaseShape.UseVisualStyleBackColor = true;
            this.rdoBaseSurface.AutoSize = true;
            this.rdoBaseSurface.Location = new Point(20, 0x51);
            this.rdoBaseSurface.Name = "rdoBaseSurface";
            this.rdoBaseSurface.Size = new Size(0x83, 0x10);
            this.rdoBaseSurface.TabIndex = 2;
            this.rdoBaseSurface.Text = "丛表面获取图层高程";
            this.rdoBaseSurface.UseVisualStyleBackColor = true;
            this.rdoBaseExpression.AutoSize = true;
            this.rdoBaseExpression.Checked = true;
            this.rdoBaseExpression.Location = new Point(20, 0x15);
            this.rdoBaseExpression.Name = "rdoBaseExpression";
            this.rdoBaseExpression.Size = new Size(0xbf, 0x10);
            this.rdoBaseExpression.TabIndex = 0;
            this.rdoBaseExpression.TabStop = true;
            this.rdoBaseExpression.Text = "使用常量或表达式设置图层高程";
            this.rdoBaseExpression.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.txtZFeator);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(13, 0xb1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(390, 0x47);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Z单位转换";
            this.txtZFeator.Location = new Point(0x130, 0x17);
            this.txtZFeator.Name = "txtZFeator";
            this.txtZFeator.Size = new Size(80, 0x15);
            this.txtZFeator.TabIndex = 2;
            this.txtZFeator.Text = "1.0000";
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "英尺到米", "米到英尺", "自定义" });
            this.comboBox2.Location = new Point(0xba, 0x17);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x5e, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.Text = "自定义";
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x12, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa1, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高程单位和场景单位转换因子";
            this.groupBox3.Controls.Add(this.txtOffset);
            this.groupBox3.Location = new Point(13, 0xfd);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(390, 0x34);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "偏移";
            this.txtOffset.Location = new Point(6, 20);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(0x124, 0x15);
            this.txtOffset.TabIndex = 5;
            this.txtOffset.Text = "0";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BaseHeightPropertyPage";
            base.Size = new Size(0x1ab, 0x146);
            base.Load += new EventHandler(this.BaseHeightPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        private ISurface method_0(ILayer ilayer_1)
        {
            ISurface surface = null;
            if (ilayer_1 == null)
            {
                return null;
            }
            if (ilayer_1 is ITinLayer)
            {
                ITinLayer layer = ilayer_1 as ITinLayer;
                return (layer.Dataset as ISurface);
            }
            if (ilayer_1 is IRasterLayer)
            {
                IRasterLayer layer2 = ilayer_1 as IRasterLayer;
                IRasterBand band = (layer2.Raster as IRasterBandCollection).Item(0);
                IRasterSurface surface2 = new RasterSurfaceClass {
                    RasterBand = band
                };
                surface = surface2 as ISurface;
            }
            return surface;
        }

        private ISurface method_1(IDataset idataset_0)
        {
            ISurface surface = null;
            if (idataset_0 is ITin)
            {
                return (idataset_0 as ISurface);
            }
            if (idataset_0 is IRasterBandCollection)
            {
                IRasterBand band = (idataset_0 as IRasterBandCollection).Item(0);
                IRasterSurface surface2 = new RasterSurfaceClass {
                    RasterBand = band
                };
                surface = surface2 as ISurface;
            }
            return surface;
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
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayer_0 = value as ILayer;
            }
        }

        internal class SuferWrap
        {
            private ISurface isurface_0 = null;
            private string string_0 = "";

            internal SuferWrap(ISurface isurface_1)
            {
                this.isurface_0 = isurface_1;
                if (this.isurface_0 is IDataset)
                {
                    this.string_0 = (this.isurface_0 as IDataset).BrowseName;
                }
                else if (this.isurface_0 is IRasterSurface)
                {
                    this.string_0 = (this.isurface_0 as IRasterSurface).RasterBand.Bandname;
                }
            }

            public override string ToString()
            {
                return this.string_0;
            }

            internal ISurface Surface
            {
                get
                {
                    return this.isurface_0;
                }
            }
        }
    }
}

