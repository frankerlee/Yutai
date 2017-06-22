using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class ElementSizeAndPositionCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IElement ielement_0 = null;
        private string string_0 = "大小和位置";

        public event OnValueChangeEventHandler OnValueChange;

        public ElementSizeAndPositionCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
        }

        public void Cancel()
        {
        }

 private void ElementSizeAndPositionCtrl_Load(object sender, EventArgs e)
        {
            IEnvelope outEnvelope = new EnvelopeClass();
            this.ielement_0.Geometry.QueryEnvelope(outEnvelope);
            ICallout callout = null;
            this.method_1(this.ielement_0, out callout);
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
        }

 private void method_0()
        {
        }

        private bool method_1(IElement ielement_1, out ICallout icallout_0)
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
            this.method_0();
        }

        public void SetObjects(object object_0)
        {
            this.ielement_0 = object_0 as IElement;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
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

