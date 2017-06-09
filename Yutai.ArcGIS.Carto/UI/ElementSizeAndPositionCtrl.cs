using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public class ElementSizeAndPositionCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IElement ielement_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string string_0 = "大小和位置";
        private TextEdit txtHeight;
        private TextEdit txtWidth;
        private TextEdit txtX;
        private TextEdit txtY;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementSizeAndPositionCtrl()
        {
            this.InitializeComponent();
            this.txtWidth.EditValueChanged += new EventHandler(this.txtY_EditValueChanged);
            this.txtHeight.EditValueChanged += new EventHandler(this.txtHeight_EditValueChanged);
            this.txtX.EditValueChanged += new EventHandler(this.txtY_EditValueChanged);
            this.txtY.EditValueChanged += new EventHandler(this.txtY_EditValueChanged);
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                try
                {
                    double xMin = double.Parse(this.txtX.Text);
                    double yMin = double.Parse(this.txtY.Text);
                    double num3 = double.Parse(this.txtWidth.Text);
                    double num4 = double.Parse(this.txtHeight.Text);
                    IEnvelope to = new EnvelopeClass();
                    to.PutCoords(xMin, yMin, xMin + num3, yMin + num4);
                    IEnvelope envelope = this.ielement_0.Geometry.Envelope;
                    if ((num3 == 0.0) || (num4 == 0.0))
                    {
                        if (this.ielement_0 is ILineElement)
                        {
                            IPointCollection geometry = this.ielement_0.Geometry as IPointCollection;
                            if (geometry.PointCount == 2)
                            {
                                object missing = System.Type.Missing;
                                IPointCollection points2 = new PolylineClass();
                                PointClass class2 = new PointClass {
                                    X = xMin,
                                    Y = yMin
                                };
                                IPoint inPoint = class2;
                                PointClass class3 = new PointClass {
                                    X = xMin + num3,
                                    Y = yMin + num4
                                };
                                IPoint point2 = class3;
                                points2.AddPoint(inPoint, ref missing, ref missing);
                                points2.AddPoint(point2, ref missing, ref missing);
                                this.ielement_0.Geometry = points2 as IGeometry;
                            }
                        }
                    }
                    else
                    {
                        IAffineTransformation2D transformation = new AffineTransformation2DClass();
                        transformation.DefineFromEnvelopes(envelope, to);
                        (this.ielement_0 as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
                    }
                    this.bool_1 = false;
                }
                catch
                {
                    MessageBox.Show("数据输入错误，请检查!");
                }
            }
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void ElementSizeAndPositionCtrl_Load(object sender, EventArgs e)
        {
            try
            {
                IEnvelope outEnvelope = new EnvelopeClass();
                this.ielement_0.Geometry.QueryEnvelope(outEnvelope);
                ICallout callout = null;
                this.method_2(this.ielement_0, out callout);
                if (callout != null)
                {
                    this.txtX.Text = callout.AnchorPoint.X.ToString("0.###");
                    this.txtY.Text = callout.AnchorPoint.Y.ToString("0.###");
                }
                else
                {
                    this.txtX.Text = outEnvelope.XMin.ToString("0.###");
                    this.txtY.Text = outEnvelope.YMin.ToString("0.###");
                }
                this.txtWidth.Text = outEnvelope.Width.ToString("0.###");
                this.txtHeight.Text = outEnvelope.Height.ToString("0.###");
                if (this.ielement_0 is ILineElement)
                {
                    IPointCollection geometry = this.ielement_0.Geometry as IPointCollection;
                    if (geometry.PointCount == 2)
                    {
                        IPoint point = geometry.get_Point(0);
                        IPoint point2 = geometry.get_Point(1);
                        if (point.Y == point2.Y)
                        {
                            this.txtHeight.Enabled = false;
                        }
                        if (point.X == point2.X)
                        {
                            this.txtWidth.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
            }
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtY = new TextEdit();
            this.txtX = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.txtHeight = new TextEdit();
            this.txtWidth = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtY.Properties.BeginInit();
            this.txtX.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(160, 0x68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置";
            this.txtY.EditValue = "";
            this.txtY.Location = new System.Drawing.Point(0x30, 0x40);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(0x60, 0x15);
            this.txtY.TabIndex = 3;
            this.txtX.EditValue = "";
            this.txtX.Location = new System.Drawing.Point(0x30, 0x20);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(0x60, 0x15);
            this.txtX.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            this.groupBox2.Controls.Add(this.txtHeight);
            this.groupBox2.Controls.Add(this.txtWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(0xb8, 0x10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(160, 0x68);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "大小";
            this.txtHeight.EditValue = "";
            this.txtHeight.Location = new System.Drawing.Point(0x30, 0x40);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(0x60, 0x15);
            this.txtHeight.TabIndex = 3;
            this.txtWidth.EditValue = "";
            this.txtWidth.Location = new System.Drawing.Point(0x30, 0x20);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(0x60, 0x15);
            this.txtWidth.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "高度";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 0x11);
            this.label4.TabIndex = 0;
            this.label4.Text = "宽度";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "ElementSizeAndPositionCtrl";
            base.Size = new Size(360, 0xd8);
            base.Load += new EventHandler(this.ElementSizeAndPositionCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtY.Properties.EndInit();
            this.txtX.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0(EventArgs eventArgs_0)
        {
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1()
        {
        }

        private bool method_2(IElement ielement_1, out ICallout icallout_0)
        {
            icallout_0 = null;
            try
            {
                if (ielement_1 is ITextElement)
                {
                    ITextElement element = ielement_1 as ITextElement;
                    ITextSymbol symbol = element.Symbol;
                    if (!(symbol is IFormattedTextSymbol))
                    {
                        return false;
                    }
                    IFormattedTextSymbol symbol2 = symbol as IFormattedTextSymbol;
                    ITextBackground background = symbol2.Background;
                    if (background == null)
                    {
                        return false;
                    }
                    if (!(background is ICallout))
                    {
                        return false;
                    }
                    icallout_0 = background as ICallout;
                    return true;
                }
                if (ielement_1 is IMarkerElement)
                {
                    IMarkerElement element2 = ielement_1 as IMarkerElement;
                    IMarkerSymbol symbol3 = element2.Symbol;
                    if (!(symbol3 is IMarkerBackgroundSupport))
                    {
                        return false;
                    }
                    IMarkerBackgroundSupport support = symbol3 as IMarkerBackgroundSupport;
                    IMarkerBackground background2 = support.Background;
                    if (background2 == null)
                    {
                        return false;
                    }
                    if (!(background2 is ICallout))
                    {
                        return false;
                    }
                    icallout_0 = background2 as ICallout;
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.ielement_0 = object_0 as IElement;
        }

        private void txtHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                this.method_0(e);
            }
        }

        private void txtY_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                this.method_0(e);
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

