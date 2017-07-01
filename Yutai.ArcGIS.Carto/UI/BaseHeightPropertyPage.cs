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
    public partial class BaseHeightPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private I3DProperties i3DProperties_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0 = null;

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
                IRasterSurface surface2 = new RasterSurfaceClass
                {
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
                IRasterSurface surface2 = new RasterSurfaceClass
                {
                    RasterBand = band
                };
                surface = surface2 as ISurface;
            }
            return surface;
        }

        public IBasicMap FocusMap
        {
            set { this.ibasicMap_0 = value; }
        }

        public bool IsPageDirty
        {
            get { return this.bool_0; }
        }

        public object SelectItem
        {
            set { this.ilayer_0 = value as ILayer; }
        }

        internal partial class SuferWrap
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
                get { return this.isurface_0; }
            }
        }
    }
}