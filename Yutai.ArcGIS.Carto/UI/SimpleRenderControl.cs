using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class SimpleRenderControl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private StyleButton btnStyle;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ISimpleRenderer isimpleRenderer_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private Label label2;
        private Label label3;
        private MemoEdit txtDescription;
        private TextEdit txtLabel;

        public SimpleRenderControl()
        {
          //  MessageBox.Show("Testing.....");
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.isimpleRenderer_0.Label = this.txtLabel.Text;
            this.isimpleRenderer_0.Description = this.txtDescription.Text;
            IObjectCopy copy = new ObjectCopyClass();
            ISimpleRenderer renderer = copy.Copy(this.isimpleRenderer_0) as ISimpleRenderer;
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Click Style Button");
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnStyle.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnStyle.Style = selector.GetSymbol();
                        this.isimpleRenderer_0.Symbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnStyle = new StyleButton();
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.txtDescription = new MemoEdit();
            this.label2 = new Label();
            this.txtLabel = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.txtDescription.Properties.BeginInit();
            this.txtLabel.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.btnStyle.Location = new System.Drawing.Point(0x40, 0x10);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(0x88, 0x40);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 0x2a;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLabel);
            this.groupBox2.Location = new System.Drawing.Point(0x10, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 0x98);
            this.groupBox2.TabIndex = 0x35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图例";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 0x37;
            this.label3.Text = "说明";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new System.Drawing.Point(0x30, 0x38);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x110, 0x58);
            this.txtDescription.TabIndex = 0x36;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 0x35;
            this.label2.Text = "标注";
            this.txtLabel.EditValue = "";
            this.txtLabel.Location = new System.Drawing.Point(0x30, 0x18);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(0x110, 0x17);
            this.txtLabel.TabIndex = 0x34;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "SimpleRenderControl";
            base.Size = new Size(0x188, 280);
            base.Load += new EventHandler(this.SimpleRenderControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.txtDescription.Properties.EndInit();
            this.txtLabel.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            if (this.isimpleRenderer_0 != null)
            {
                this.btnStyle.Style = this.isimpleRenderer_0.Symbol;
                this.txtLabel.Text = this.isimpleRenderer_0.Label;
                this.txtDescription.Text = this.isimpleRenderer_0.Description;
            }
        }

        private IColor method_1()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return new RgbColorClass { Red = (int) (255.0 * random.NextDouble()), Green = (int) (255.0 * random.NextDouble()), Blue = (int) (255.0 * random.NextDouble()) };
        }

        private ISymbol method_2(esriGeometryType esriGeometryType_0)
        {
            ISymbol symbol = null;
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    symbol = new SimpleMarkerSymbolClass();
                    (symbol as IMarkerSymbol).Color = this.method_1();
                    return symbol;

                case esriGeometryType.esriGeometryPolyline:
                    symbol = new SimpleLineSymbolClass();
                    (symbol as ILineSymbol).Color = this.method_1();
                    return symbol;

                case esriGeometryType.esriGeometryPolygon:
                    symbol = new SimpleFillSymbolClass();
                    (symbol as IFillSymbol).Color = this.method_1();
                    return symbol;
            }
            return symbol;
        }

        private void SimpleRenderControl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.igeoFeatureLayer_0 == null)
                {
                    this.isimpleRenderer_0 = null;
                }
                else
                {
                    ISimpleRenderer pInObject = this.igeoFeatureLayer_0.Renderer as ISimpleRenderer;
                    if (pInObject == null)
                    {
                        if (this.isimpleRenderer_0 == null)
                        {
                            this.isimpleRenderer_0 = new SimpleRendererClass();
                            if (this.igeoFeatureLayer_0.FeatureClass != null)
                            {
                                this.isimpleRenderer_0.Symbol = this.method_2(this.igeoFeatureLayer_0.FeatureClass.ShapeType);
                            }
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.isimpleRenderer_0 = copy.Copy(pInObject) as ISimpleRenderer;
                    }
                    if (this.bool_0)
                    {
                        this.method_0();
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
    }
}

