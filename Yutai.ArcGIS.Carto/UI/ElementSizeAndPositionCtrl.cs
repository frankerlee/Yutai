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
    public partial class ElementSizeAndPositionCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IElement ielement_0 = null;
        private string string_0 = "大小和位置";

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
                                PointClass class2 = new PointClass
                                {
                                    X = xMin,
                                    Y = yMin
                                };
                                IPoint inPoint = class2;
                                PointClass class3 = new PointClass
                                {
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
                        (this.ielement_0 as ITransform2D).Transform(esriTransformDirection.esriTransformForward,
                            transformation);
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
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}