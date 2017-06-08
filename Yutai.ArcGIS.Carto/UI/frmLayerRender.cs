using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmLayerRender : Form
    {
        private bool bool_0 = false;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ComboBoxEdit cboRenderType;
        private ChartRendererCtrl chartRendererCtrl_0 = new ChartRendererCtrl();
        private ChartRendererCtrl chartRendererCtrl_1 = new ChartRendererCtrl();
        private ClassBreaksRendererCtrl classBreaksRendererCtrl_0 = new ClassBreaksRendererCtrl();
        private Container container_0 = null;
        private DotDensityRendererCtrl dotDensityRendererCtrl_0 = new DotDensityRendererCtrl();
        private IBasicMap ibasicMap_0 = null;
        private IFeatureLayer ifeatureLayer_0 = null;
        private IUserControl iuserControl_0 = null;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
        private ProportionalSymbolRendererCtrl proportionalSymbolRendererCtrl_0 = new ProportionalSymbolRendererCtrl();
        private SimpleRenderControl simpleRenderControl_0 = new SimpleRenderControl();
        private UniqueValueRendererCtrl uniqueValueRendererCtrl_0 = new UniqueValueRendererCtrl();
        private UniqueValueRendererMoreAttributeCtrl uniqueValueRendererMoreAttributeCtrl_0 = new UniqueValueRendererMoreAttributeCtrl();

        public frmLayerRender()
        {
            this.InitializeComponent();
            this.simpleRenderControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.simpleRenderControl_0);
            this.simpleRenderControl_0.Visible = false;
            this.uniqueValueRendererCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.uniqueValueRendererCtrl_0);
            this.uniqueValueRendererCtrl_0.Visible = false;
            this.uniqueValueRendererMoreAttributeCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.uniqueValueRendererMoreAttributeCtrl_0);
            this.uniqueValueRendererMoreAttributeCtrl_0.Visible = false;
            this.classBreaksRendererCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.classBreaksRendererCtrl_0);
            this.classBreaksRendererCtrl_0.Visible = false;
            this.proportionalSymbolRendererCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.proportionalSymbolRendererCtrl_0);
            this.proportionalSymbolRendererCtrl_0.Visible = false;
            this.dotDensityRendererCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.dotDensityRendererCtrl_0);
            this.dotDensityRendererCtrl_0.Visible = false;
            this.chartRendererCtrl_0.Dock = DockStyle.Fill;
            this.chartRendererCtrl_0.ChartRenderType = 0;
            this.panel2.Controls.Add(this.chartRendererCtrl_0);
            this.chartRendererCtrl_0.Visible = false;
            this.chartRendererCtrl_1.Dock = DockStyle.Fill;
            this.chartRendererCtrl_1.ChartRenderType = 1;
            this.panel2.Controls.Add(this.chartRendererCtrl_1);
            this.chartRendererCtrl_1.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.iuserControl_0 != null)
            {
                this.iuserControl_0.Apply();
            }
        }

        private void cboRenderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.iuserControl_0 != null)
                {
                    (this.iuserControl_0 as UserControl).Visible = false;
                }
                IGeoFeatureLayer layer = this.ifeatureLayer_0 as IGeoFeatureLayer;
                switch (this.cboRenderType.Text)
                {
                    case "点密度渲染":
                        this.iuserControl_0 = this.dotDensityRendererCtrl_0;
                        this.dotDensityRendererCtrl_0.CurrentLayer = layer;
                        this.dotDensityRendererCtrl_0.Visible = true;
                        break;

                    case "单一符号渲染":
                        this.iuserControl_0 = this.simpleRenderControl_0;
                        this.simpleRenderControl_0.CurrentLayer = layer;
                        this.simpleRenderControl_0.Visible = true;
                        break;

                    case "唯一值渲染":
                        this.iuserControl_0 = this.uniqueValueRendererCtrl_0;
                        this.uniqueValueRendererCtrl_0.CurrentLayer = layer;
                        this.uniqueValueRendererCtrl_0.Visible = true;
                        break;

                    case "唯一值渲染,多字段":
                        this.iuserControl_0 = this.uniqueValueRendererMoreAttributeCtrl_0;
                        this.uniqueValueRendererMoreAttributeCtrl_0.CurrentLayer = layer;
                        this.uniqueValueRendererMoreAttributeCtrl_0.Visible = true;
                        break;

                    case "渐变染色渲染":
                        this.iuserControl_0 = this.classBreaksRendererCtrl_0;
                        this.classBreaksRendererCtrl_0.CurrentLayer = layer;
                        this.classBreaksRendererCtrl_0.Visible = true;
                        break;

                    case "比例符号渲染":
                        this.iuserControl_0 = this.proportionalSymbolRendererCtrl_0;
                        this.proportionalSymbolRendererCtrl_0.CurrentLayer = layer;
                        this.proportionalSymbolRendererCtrl_0.Visible = true;
                        break;

                    case "饼图渲染":
                        this.iuserControl_0 = this.chartRendererCtrl_0;
                        this.chartRendererCtrl_0.CurrentLayer = layer;
                        this.chartRendererCtrl_0.Visible = true;
                        break;

                    case "柱状图渲染":
                        this.iuserControl_0 = this.chartRendererCtrl_1;
                        this.chartRendererCtrl_1.CurrentLayer = layer;
                        this.chartRendererCtrl_1.Visible = true;
                        break;
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmLayerRender_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerRender));
            this.btnOK = new SimpleButton();
            this.panel1 = new Panel();
            this.cboRenderType = new ComboBoxEdit();
            this.label2 = new Label();
            this.panel2 = new Panel();
            this.btnCancel = new SimpleButton();
            this.panel1.SuspendLayout();
            this.cboRenderType.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0x158, 0x158);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel1.Controls.Add(this.cboRenderType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1e8, 0x20);
            this.panel1.TabIndex = 7;
            this.cboRenderType.EditValue = "";
            this.cboRenderType.Location = new System.Drawing.Point(0x4d, 4);
            this.cboRenderType.Name = "cboRenderType";
            this.cboRenderType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRenderType.Size = new Size(0x162, 0x15);
            this.cboRenderType.TabIndex = 5;
            this.cboRenderType.SelectedIndexChanged += new EventHandler(this.cboRenderType_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "渲染类型:";
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0x20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1e8, 0x130);
            this.panel2.TabIndex = 8;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x1a0, 0x158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1e8, 0x175);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnOK);
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.Name = "frmLayerRender";
            this.Text = "图层渲染";
            base.Load += new EventHandler(this.frmLayerRender_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cboRenderType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.bool_0 = false;
            this.cboRenderType.Properties.Items.Clear();
            IGeoFeatureLayer layer = this.ifeatureLayer_0 as IGeoFeatureLayer;
            if (layer.FeatureClass != null)
            {
                this.dotDensityRendererCtrl_0.CurrentLayer = null;
                this.simpleRenderControl_0.CurrentLayer = null;
                this.uniqueValueRendererCtrl_0.CurrentLayer = null;
                this.classBreaksRendererCtrl_0.CurrentLayer = null;
                this.proportionalSymbolRendererCtrl_0.CurrentLayer = null;
                this.chartRendererCtrl_0.CurrentLayer = null;
                this.chartRendererCtrl_1.CurrentLayer = null;
                this.uniqueValueRendererMoreAttributeCtrl_0.CurrentLayer = null;
                this.cboRenderType.Properties.Items.Add("单一符号渲染");
                this.cboRenderType.Properties.Items.Add("唯一值渲染");
                this.cboRenderType.Properties.Items.Add("唯一值渲染,多字段");
                this.cboRenderType.Properties.Items.Add("渐变染色渲染");
                this.cboRenderType.Properties.Items.Add("比例符号渲染");
                if (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    this.cboRenderType.Properties.Items.Add("点密度渲染");
                }
                this.cboRenderType.Properties.Items.Add("饼图渲染");
                this.cboRenderType.Properties.Items.Add("柱状图渲染");
                if (this.iuserControl_0 != null)
                {
                    (this.iuserControl_0 as UserControl).Visible = false;
                }
                IFeatureRenderer renderer = layer.Renderer;
                if (renderer is ISimpleRenderer)
                {
                    this.cboRenderType.Text = "单一符号渲染";
                    this.iuserControl_0 = this.simpleRenderControl_0;
                    this.simpleRenderControl_0.CurrentLayer = layer;
                    this.simpleRenderControl_0.Visible = true;
                }
                else if (renderer is IUniqueValueRenderer)
                {
                    if ((renderer as IUniqueValueRenderer).FieldCount > 1)
                    {
                        this.cboRenderType.Text = "唯一值渲染,多字段";
                        this.iuserControl_0 = this.uniqueValueRendererMoreAttributeCtrl_0;
                        this.uniqueValueRendererMoreAttributeCtrl_0.CurrentLayer = layer;
                        this.uniqueValueRendererMoreAttributeCtrl_0.Visible = true;
                    }
                    else if ((renderer as IUniqueValueRenderer).FieldCount == 1)
                    {
                        if (((renderer as IUniqueValueRenderer).LookupStyleset != null) && ((renderer as IUniqueValueRenderer).LookupStyleset.Length > 0))
                        {
                            this.cboRenderType.Text = "唯一值渲染";
                            this.iuserControl_0 = this.uniqueValueRendererCtrl_0;
                            this.uniqueValueRendererCtrl_0.CurrentLayer = layer;
                            this.uniqueValueRendererCtrl_0.Visible = true;
                        }
                        else
                        {
                            this.cboRenderType.Text = "唯一值渲染";
                            this.iuserControl_0 = this.uniqueValueRendererCtrl_0;
                            this.uniqueValueRendererCtrl_0.CurrentLayer = layer;
                            this.uniqueValueRendererCtrl_0.Visible = true;
                        }
                    }
                }
                else if (renderer is IClassBreaksRenderer)
                {
                    this.cboRenderType.Text = "渐变染色渲染";
                    this.iuserControl_0 = this.classBreaksRendererCtrl_0;
                    this.classBreaksRendererCtrl_0.CurrentLayer = layer;
                    this.classBreaksRendererCtrl_0.Visible = true;
                }
                else if (renderer is IProportionalSymbolRenderer)
                {
                    this.cboRenderType.Text = "比例符号渲染";
                    this.iuserControl_0 = this.proportionalSymbolRendererCtrl_0;
                    this.proportionalSymbolRendererCtrl_0.CurrentLayer = layer;
                    this.proportionalSymbolRendererCtrl_0.Visible = true;
                }
                else if (renderer is IDotDensityRenderer)
                {
                    this.cboRenderType.Text = "点密度渲染";
                    this.iuserControl_0 = this.dotDensityRendererCtrl_0;
                    this.dotDensityRendererCtrl_0.CurrentLayer = layer;
                    this.dotDensityRendererCtrl_0.Visible = true;
                }
                else if (renderer is IChartRenderer)
                {
                    IChartRenderer renderer2 = renderer as IChartRenderer;
                    if (renderer2.ChartSymbol is IPieChartSymbol)
                    {
                        this.cboRenderType.Text = "饼图渲染";
                        this.iuserControl_0 = this.chartRendererCtrl_0;
                        this.chartRendererCtrl_0.CurrentLayer = layer;
                        this.chartRendererCtrl_0.Visible = true;
                    }
                    else if (renderer2.ChartSymbol is IBarChartSymbol)
                    {
                        this.cboRenderType.Text = "柱状图渲染";
                        this.iuserControl_0 = this.chartRendererCtrl_1;
                        this.chartRendererCtrl_1.CurrentLayer = layer;
                        this.chartRendererCtrl_1.Visible = true;
                    }
                }
                this.bool_0 = true;
            }
        }

        public IFeatureLayer FeatureLayer
        {
            set
            {
                this.ifeatureLayer_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.simpleRenderControl_0.StyleGallery = value;
                this.uniqueValueRendererCtrl_0.StyleGallery = value;
                this.classBreaksRendererCtrl_0.StyleGallery = value;
                this.proportionalSymbolRendererCtrl_0.StyleGallery = value;
                this.dotDensityRendererCtrl_0.StyleGallery = value;
                this.chartRendererCtrl_0.StyleGallery = value;
                this.chartRendererCtrl_1.StyleGallery = value;
                this.uniqueValueRendererMoreAttributeCtrl_0.StyleGallery = value;
            }
        }

        internal class LayerObject
        {
            private ILayer ilayer_0 = null;

            public LayerObject(ILayer ilayer_1)
            {
                this.ilayer_0 = ilayer_1;
            }

            public override string ToString()
            {
                if (this.ilayer_0 != null)
                {
                    return this.ilayer_0.Name;
                }
                return "";
            }

            public ILayer Layer
            {
                get
                {
                    return this.ilayer_0;
                }
                set
                {
                    this.ilayer_0 = value;
                }
            }
        }
    }
}

