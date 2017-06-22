using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class ElementGeometryInfoPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IElement ielement_0 = null;
        private string string_0 = "面积";

        public event OnValueChangeEventHandler OnValueChange;

        public ElementGeometryInfoPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
        }

        public void Cancel()
        {
        }

 private void ElementGeometryInfoPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            if (this.ielement_0 != null)
            {
                if (this.ielement_0.Geometry.IsEmpty)
                {
                    this.txtPerimeter.Text = "";
                    this.txtX.Text = "";
                    this.txtY.Text = "";
                }
                else
                {
                    IEnvelope envelope = this.ielement_0.Geometry.Envelope;
                    this.txtArea.Text = (envelope as IArea).Area.ToString("0.#####");
                    this.txtPerimeter.Text = ((envelope.Width + envelope.Height) * 2.0).ToString("0.#####");
                    double num = (envelope.XMin + envelope.XMax) / 2.0;
                    this.txtX.Text = num.ToString("0.#####");
                    this.txtY.Text = ((envelope.YMin + envelope.YMax) / 2.0).ToString("0.#####");
                }
            }
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

