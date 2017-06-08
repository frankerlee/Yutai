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
    public class frmExtendSet : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private ComboBox cboFeatures;
        private ComboBox cboLayers;
        [CompilerGenerated]
        private ExtendSetType extendSetType_0;
        [CompilerGenerated]
        private IBasicMap ibasicMap_0;
        private IContainer icontainer_0 = null;
        [CompilerGenerated]
        private IEnvelope ienvelope_0;
        [CompilerGenerated]
        private IGeometry igeometry_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panelExtend;
        private Panel panelLayer;
        private RadioButton rdoCurrentMapExtend;
        private RadioButton rdoCustomExtend;
        private RadioButton rdoGraphicsExtend;
        private RadioButton rdoLayerExtend;
        private TextBox txtBottom;
        private TextBox txtLeft;
        private TextBox txtRight;
        private TextBox txtTop;

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
                extent = new EnvelopeClass {
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
                    (geometry as IEnumGeometryBind).BindGeometrySource(outputFilter, (layer as IGeoFeatureLayer).FeatureClass);
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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
                        if (((layer2 is IFeatureLayer) && ((layer2 as IFeatureLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)) && ((layer2 as IFeatureLayer).FeatureClass.FeatureCount(null) > 0))
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

        private void InitializeComponent()
        {
            this.rdoCurrentMapExtend = new RadioButton();
            this.rdoLayerExtend = new RadioButton();
            this.rdoGraphicsExtend = new RadioButton();
            this.rdoCustomExtend = new RadioButton();
            this.panelExtend = new Panel();
            this.txtRight = new TextBox();
            this.label5 = new Label();
            this.txtLeft = new TextBox();
            this.label4 = new Label();
            this.txtBottom = new TextBox();
            this.label3 = new Label();
            this.txtTop = new TextBox();
            this.label2 = new Label();
            this.panelLayer = new Panel();
            this.label6 = new Label();
            this.cboFeatures = new ComboBox();
            this.cboLayers = new ComboBox();
            this.label1 = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.panelExtend.SuspendLayout();
            this.panelLayer.SuspendLayout();
            base.SuspendLayout();
            this.rdoCurrentMapExtend.AutoSize = true;
            this.rdoCurrentMapExtend.Checked = true;
            this.rdoCurrentMapExtend.Location = new System.Drawing.Point(13, 13);
            this.rdoCurrentMapExtend.Name = "rdoCurrentMapExtend";
            this.rdoCurrentMapExtend.Size = new Size(0x5f, 0x10);
            this.rdoCurrentMapExtend.TabIndex = 0;
            this.rdoCurrentMapExtend.TabStop = true;
            this.rdoCurrentMapExtend.Text = "当前可见范围";
            this.rdoCurrentMapExtend.UseVisualStyleBackColor = true;
            this.rdoLayerExtend.AutoSize = true;
            this.rdoLayerExtend.Location = new System.Drawing.Point(13, 0x2e);
            this.rdoLayerExtend.Name = "rdoLayerExtend";
            this.rdoLayerExtend.Size = new Size(0x53, 0x10);
            this.rdoLayerExtend.TabIndex = 1;
            this.rdoLayerExtend.Text = "要素的轮廓";
            this.rdoLayerExtend.UseVisualStyleBackColor = true;
            this.rdoLayerExtend.CheckedChanged += new EventHandler(this.rdoLayerExtend_CheckedChanged);
            this.rdoGraphicsExtend.AutoSize = true;
            this.rdoGraphicsExtend.Location = new System.Drawing.Point(12, 0x8b);
            this.rdoGraphicsExtend.Name = "rdoGraphicsExtend";
            this.rdoGraphicsExtend.Size = new Size(0x6b, 0x10);
            this.rdoGraphicsExtend.TabIndex = 2;
            this.rdoGraphicsExtend.Text = "所选图形的轮廓";
            this.rdoGraphicsExtend.UseVisualStyleBackColor = true;
            this.rdoCustomExtend.AutoSize = true;
            this.rdoCustomExtend.Location = new System.Drawing.Point(13, 0xa1);
            this.rdoCustomExtend.Name = "rdoCustomExtend";
            this.rdoCustomExtend.Size = new Size(0x6b, 0x10);
            this.rdoCustomExtend.TabIndex = 3;
            this.rdoCustomExtend.Text = "自定义数据范围";
            this.rdoCustomExtend.UseVisualStyleBackColor = true;
            this.rdoCustomExtend.CheckedChanged += new EventHandler(this.rdoCustomExtend_CheckedChanged);
            this.panelExtend.Controls.Add(this.txtRight);
            this.panelExtend.Controls.Add(this.label5);
            this.panelExtend.Controls.Add(this.txtLeft);
            this.panelExtend.Controls.Add(this.label4);
            this.panelExtend.Controls.Add(this.txtBottom);
            this.panelExtend.Controls.Add(this.label3);
            this.panelExtend.Controls.Add(this.txtTop);
            this.panelExtend.Controls.Add(this.label2);
            this.panelExtend.Enabled = false;
            this.panelExtend.Location = new System.Drawing.Point(0x18, 0xc3);
            this.panelExtend.Name = "panelExtend";
            this.panelExtend.Size = new Size(0x164, 0x53);
            this.panelExtend.TabIndex = 4;
            this.txtRight.Location = new System.Drawing.Point(0xcd, 0x20);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new Size(0x75, 0x15);
            this.txtRight.TabIndex = 8;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0xb6, 0x23);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "右";
            this.txtLeft.Location = new System.Drawing.Point(0x2c, 0x20);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new Size(0x81, 0x15);
            this.txtLeft.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x15, 0x23);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "左";
            this.txtBottom.Location = new System.Drawing.Point(0x7e, 0x3b);
            this.txtBottom.Name = "txtBottom";
            this.txtBottom.Size = new Size(100, 0x15);
            this.txtBottom.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x67, 0x3e);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "下";
            this.txtTop.Location = new System.Drawing.Point(0x7e, 3);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new Size(100, 0x15);
            this.txtTop.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x67, 6);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "上";
            this.panelLayer.Controls.Add(this.label6);
            this.panelLayer.Controls.Add(this.cboFeatures);
            this.panelLayer.Controls.Add(this.cboLayers);
            this.panelLayer.Controls.Add(this.label1);
            this.panelLayer.Enabled = false;
            this.panelLayer.Location = new System.Drawing.Point(0x18, 0x45);
            this.panelLayer.Name = "panelLayer";
            this.panelLayer.Size = new Size(0x180, 0x37);
            this.panelLayer.TabIndex = 5;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0xec, 4);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "要素";
            this.cboFeatures.FormattingEnabled = true;
            this.cboFeatures.Items.AddRange(new object[] { "全部", "可见" });
            this.cboFeatures.Location = new System.Drawing.Point(0xee, 0x17);
            this.cboFeatures.Name = "cboFeatures";
            this.cboFeatures.Size = new Size(130, 20);
            this.cboFeatures.TabIndex = 2;
            this.cboLayers.FormattingEnabled = true;
            this.cboLayers.Location = new System.Drawing.Point(11, 0x17);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Size = new Size(0xc6, 20);
            this.cboLayers.TabIndex = 1;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层";
            this.btnOK.Location = new System.Drawing.Point(0xe5, 0x12e);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new System.Drawing.Point(0x13d, 0x12e);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a3, 0x158);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.panelLayer);
            base.Controls.Add(this.panelExtend);
            base.Controls.Add(this.rdoCustomExtend);
            base.Controls.Add(this.rdoGraphicsExtend);
            base.Controls.Add(this.rdoLayerExtend);
            base.Controls.Add(this.rdoCurrentMapExtend);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExtendSet";
            base.Load += new EventHandler(this.frmExtendSet_Load);
            this.panelExtend.ResumeLayout(false);
            this.panelExtend.PerformLayout();
            this.panelLayer.ResumeLayout(false);
            this.panelLayer.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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

        public IGeometry ClipGeometry
        {
            [CompilerGenerated]
            get
            {
                return this.igeometry_0;
            }
            [CompilerGenerated]
            set
            {
                this.igeometry_0 = value;
            }
        }

        public IEnvelope Extend
        {
            [CompilerGenerated]
            get
            {
                return this.ienvelope_0;
            }
            [CompilerGenerated]
            set
            {
                this.ienvelope_0 = value;
            }
        }

        public ExtendSetType ExtendType
        {
            [CompilerGenerated]
            get
            {
                return this.extendSetType_0;
            }
            [CompilerGenerated]
            set
            {
                this.extendSetType_0 = value;
            }
        }

        public IBasicMap Map
        {
            [CompilerGenerated]
            get
            {
                return this.ibasicMap_0;
            }
            [CompilerGenerated]
            set
            {
                this.ibasicMap_0 = value;
            }
        }
    }
}

