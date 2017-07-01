using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmExtendSet : Form
    {
        private IContainer icontainer_0 = null;

        public frmExtendSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IEnvelope extent;
            if (this.rdoCurrentMapExtend.Checked)
            {
                extent = (this.Map as IActiveView).Extent;
                this.ClipGeometry = extent;
            }
            else if (this.rdoCustomExtend.Checked)
            {
                double num;
                double num2;
                double num3;
                double num4;
                if (!double.TryParse(this.txtBottom.Text, out num))
                {
                    MessageBox.Show("底部值输入错误!");
                    return;
                }
                if (!double.TryParse(this.txtLeft.Text, out num2))
                {
                    MessageBox.Show("左边值输入错误!");
                    return;
                }
                if (!double.TryParse(this.txtTop.Text, out num3))
                {
                    MessageBox.Show("顶部值输入错误!");
                    return;
                }
                if (!double.TryParse(this.txtRight.Text, out num4))
                {
                    MessageBox.Show("右边值输入错误!");
                    return;
                }
                extent = new EnvelopeClass
                {
                    XMin = num2,
                    XMax = num4,
                    YMax = num3,
                    YMin = num
                };
                this.ClipGeometry = extent;
            }
            else if (this.rdoLayerExtend.Checked)
            {
                IEnumGeometry geometry;
                ITopologicalOperator @operator;
                if (this.cboLayers.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择图层!");
                    return;
                }
                extent = null;
                ILayer layer = (this.cboLayers.SelectedItem as LayerObject).Layer;
                if (this.cboFeatures.SelectedIndex == 0)
                {
                    geometry = new EnumFeatureGeometryClass();
                    (geometry as IEnumGeometryBind).BindGeometrySource(null, (layer as IGeoFeatureLayer).FeatureClass);
                    @operator = new PolygonClass();
                    @operator.ConstructUnion(geometry);
                    this.ClipGeometry = @operator as IGeometry;
                }
                else if (this.cboFeatures.SelectedIndex == 1)
                {
                    IQueryFilter outputFilter = new SpatialFilterClass();
                    (outputFilter as ISpatialFilter).Geometry = this.Extend;
                    (outputFilter as ISpatialFilter).SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    geometry = new EnumFeatureGeometryClass();
                    (geometry as IEnumGeometryBind).BindGeometrySource(outputFilter,
                        (layer as IGeoFeatureLayer).FeatureClass);
                    IGeometryFactory3 factory = new GeometryEnvironmentClass();
                    IGeometry geometry2 = factory.CreateGeometryFromEnumerator(geometry);
                    int geometryCount = (geometry2 as IGeometryCollection).GeometryCount;
                    @operator = new PolygonClass();
                    @operator.ConstructUnion(geometry2 as IEnumGeometry);
                    this.ClipGeometry = @operator as IGeometry;
                }
                else
                {
                    geometry = new EnumFeatureGeometryClass();
                    (geometry as IEnumGeometryBind).BindGeometrySource(null, (layer as IFeatureSelection).SelectionSet);
                    @operator = new PolygonClass();
                    @operator.ConstructUnion(geometry);
                    this.ClipGeometry = @operator as IGeometry;
                }
            }
            else
            {
                this.ClipGeometry = this.method_3(this.Map as IGraphicsContainerSelect);
            }
            base.DialogResult = DialogResult.OK;
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLayers.SelectedIndex >= 0)
            {
                ILayer layer = (this.cboLayers.SelectedItem as LayerObject).Layer;
                if (layer is IFeatureLayer)
                {
                    this.cboFeatures.Items.Clear();
                    this.cboFeatures.Items.Add("全部");
                    this.cboFeatures.Items.Add("可见");
                    this.cboFeatures.Enabled = true;
                    if ((layer as IFeatureSelection).SelectionSet.Count > 0)
                    {
                        this.cboFeatures.Items.Add("已选择");
                    }
                    this.cboFeatures.SelectedIndex = 0;
                }
                else
                {
                    this.cboFeatures.Enabled = false;
                }
            }
        }

        private void frmExtendSet_Load(object sender, EventArgs e)
        {
            if (this.Map != null)
            {
                this.Extend = (this.Map as IActiveView).Extent;
                if (this.Map.LayerCount > 0)
                {
                    IEnumLayer layer = this.Map.get_Layers(null, true);
                    layer.Reset();
                    for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
                    {
                        if (((layer2 is IFeatureLayer) &&
                             ((layer2 as IFeatureLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)) &&
                            ((layer2 as IFeatureLayer).FeatureClass.FeatureCount(null) > 0))
                        {
                            this.cboLayers.Items.Add(new LayerObject(layer2));
                        }
                    }
                    if (this.cboLayers.Items.Count > 0)
                    {
                        this.cboLayers.SelectedIndex = 0;
                    }
                }
                this.rdoLayerExtend.Enabled = this.cboLayers.Items.Count > 0;
                if (this.ClipGeometry != null)
                {
                    this.rdoCustomExtend.Checked = true;
                    this.method_0(this.ClipGeometry.Envelope);
                }
                else if (this.ExtendType == ExtendSetType.Range)
                {
                    if (this.Map is IMapAutoExtentOptions)
                    {
                        IMapAutoExtentOptions map = this.Map as IMapAutoExtentOptions;
                        if (map.AutoExtentBounds != null)
                        {
                            this.rdoCustomExtend.Checked = true;
                            this.method_0(map.AutoExtentBounds);
                        }
                        else
                        {
                            this.method_0((this.Map as IActiveView).Extent);
                        }
                    }
                }
                else if (this.ExtendType == ExtendSetType.FullExtentRange)
                {
                    this.method_0((this.Map as IActiveView).FullExtent);
                }
                else if (this.Map is IMapClipOptions)
                {
                    if ((this.Map as IMapClipOptions).ClipType == esriMapClipType.esriMapClipMapExtent)
                    {
                        this.rdoCurrentMapExtend.Checked = true;
                        this.method_0((this.Map as IActiveView).Extent);
                    }
                    else if ((this.Map as IMapClipOptions).ClipType == esriMapClipType.esriMapClipShape)
                    {
                        this.rdoCustomExtend.Checked = true;
                        if ((this.Map as IMapClipOptions).ClipGeometry != null)
                        {
                            this.method_0((this.Map as IMapClipOptions).ClipGeometry.Envelope);
                        }
                        else
                        {
                            this.method_0((this.Map as IActiveView).Extent);
                        }
                    }
                }
                if (this.Map is IGraphicsContainerSelect)
                {
                    this.rdoGraphicsExtend.Enabled = (this.Map as IGraphicsContainerSelect).ElementSelectionCount > 0;
                }
            }
        }

        private void method_0(IEnvelope ienvelope_1)
        {
            this.txtTop.Text = ienvelope_1.YMax.ToString("0.000");
            this.txtBottom.Text = ienvelope_1.YMin.ToString("0.000");
            this.txtLeft.Text = ienvelope_1.XMin.ToString("0.000");
            this.txtRight.Text = ienvelope_1.XMax.ToString("0.000");
        }

        private void method_1(IEnvelope ienvelope_1)
        {
            if (this.ExtendType == ExtendSetType.ClipRange)
            {
                (this.Map as IMapClipOptions).ClipType = esriMapClipType.esriMapClipShape;
                (this.Map as IMapClipOptions).ClipGeometry = ienvelope_1;
            }
            else if (this.ExtendType == ExtendSetType.FullExtentRange)
            {
                this.Map.AreaOfInterest = ienvelope_1;
            }
            else
            {
                (this.Map as IMapAutoExtentOptions).AutoExtentBounds = ienvelope_1;
            }
        }

        private IEnvelope method_2(IFeatureLayer ifeatureLayer_0)
        {
            try
            {
                ICursor cursor;
                IFeatureSelection selection = ifeatureLayer_0 as IFeatureSelection;
                if (selection.SelectionSet.Count == 0)
                {
                    return null;
                }
                IEnvelope extent = null;
                IEnvelope inEnvelope = null;
                IFeature feature = null;
                double dx = 0.01;
                selection.SelectionSet.Search(null, false, out cursor);
                IRow row = cursor.NextRow();
                while (true)
                {
                    if (row == null)
                    {
                        break;
                    }
                    feature = row as IFeature;
                    if ((feature != null) && (feature.Shape != null))
                    {
                        try
                        {
                            if (extent == null)
                            {
                                extent = feature.Extent;
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    extent.Expand(dx, dx, false);
                                }
                            }
                            else
                            {
                                inEnvelope = feature.Extent;
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    inEnvelope.Expand(dx, dx, false);
                                }
                                extent.Union(inEnvelope);
                            }
                        }
                        catch
                        {
                        }
                    }
                    row = cursor.NextRow();
                }
                ComReleaser.ReleaseCOMObject(cursor);
                if ((extent != null) && ((extent.Width == 0.0) || (extent.Height == 0.0)))
                {
                    extent.Expand(dx, dx, false);
                    return extent;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private IEnvelope method_3(IGraphicsContainerSelect igraphicsContainerSelect_0)
        {
            try
            {
                IEnumElement selectedElements = igraphicsContainerSelect_0.SelectedElements;
                selectedElements.Reset();
                IEnvelope envelope = null;
                IEnvelope inEnvelope = null;
                for (IElement element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                {
                    if (envelope == null)
                    {
                        envelope = element2.Geometry.Envelope;
                    }
                    else
                    {
                        inEnvelope = element2.Geometry.Envelope;
                        envelope.Union(inEnvelope);
                    }
                }
                if ((envelope != null) && ((envelope.Width == 0.0) || (envelope.Height == 0.0)))
                {
                    envelope.Expand(0.001, 0.001, false);
                    return envelope;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private void rdoCustomExtend_CheckedChanged(object sender, EventArgs e)
        {
            this.panelExtend.Enabled = this.rdoCustomExtend.Checked;
        }

        private void rdoLayerExtend_CheckedChanged(object sender, EventArgs e)
        {
            this.panelLayer.Enabled = this.rdoLayerExtend.Checked;
        }

        public IGeometry ClipGeometry { get; set; }

        public IEnvelope Extend { get; set; }

        public ExtendSetType ExtendType { get; set; }

        public IBasicMap Map { get; set; }
    }
}