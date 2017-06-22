using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class MDomainControl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private Container container_0 = null;

        public MDomainControl()
        {
            this.InitializeComponent();
        }

 public void InitControl()
        {
            double outMMin = -10000.0;
            double outMMax = 11474.83645;
            double num3 = 100000.0;
            if (((ISpatialReference2GEN) this.ispatialReference_0).HasMPrecision())
            {
                ((ISpatialReference2GEN) this.ispatialReference_0).GetMDomain(ref outMMin, ref outMMax);
                double num4 = outMMax - outMMin;
                num3 = 2147483645.0 / num4;
            }
            else
            {
                this.ispatialReference_0.SetMDomain(outMMin, outMMax);
            }
            this.textBoxMinValue.Text = outMMin.ToString();
            this.textBoxMaxValue.Text = outMMax.ToString();
            this.textBoxPrecision.Text = num3.ToString();
        }

 private void MDomainControl_Load(object sender, EventArgs e)
        {
            if (!this.bool_1)
            {
                this.textBoxMaxValue.Properties.ReadOnly = false;
                this.textBoxMinValue.Properties.ReadOnly = false;
                this.textBoxPrecision.Properties.ReadOnly = false;
            }
            this.InitControl();
            this.bool_0 = true;
        }

        private void method_0(TextEdit textEdit_0)
        {
            if (CommonHelper.IsNmuber(textEdit_0.Text))
            {
                textEdit_0.ForeColor = SystemColors.WindowText;
                double inMMax = Convert.ToDouble(this.textBoxMaxValue.Text);
                double inMMin = Convert.ToDouble(this.textBoxMinValue.Text);
                double num3 = inMMax - inMMin;
                if (num3 < 0.0)
                {
                    double num4 = Convert.ToDouble(this.textBoxPrecision.Text);
                    num3 = 2147483645.0 / num4;
                    inMMax = inMMin + num3;
                    this.textBoxMaxValue.Text = inMMax.ToString();
                }
                else
                {
                    this.textBoxPrecision.Text = (2147483645.0 / num3).ToString();
                }
                this.ispatialReference_0.SetMDomain(inMMin, inMMax);
            }
            else
            {
                textEdit_0.ForeColor = Color.Red;
            }
        }

        private void textBoxMaxValue_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.method_0(this.textBoxMaxValue);
                this.bool_0 = true;
            }
        }

        private void textBoxMinValue_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.method_0(this.textBoxMinValue);
                this.bool_0 = true;
            }
        }

        private void textBoxPrecision_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (CommonHelper.IsNmuber(this.textBoxPrecision.Text))
                {
                    this.textBoxPrecision.ForeColor = SystemColors.WindowText;
                    double num = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num2 = 2147483645.0 / num;
                    double inMMin = Convert.ToDouble(this.textBoxMinValue.Text);
                    double inMMax = inMMin + num2;
                    this.ispatialReference_0.SetMDomain(inMMin, inMMax);
                    this.textBoxMaxValue.Text = inMMax.ToString();
                    this.bool_0 = true;
                }
                else
                {
                    this.textBoxPrecision.ForeColor = Color.Red;
                    this.bool_0 = true;
                }
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.ispatialReference_0;
            }
            set
            {
                this.ispatialReference_0 = value;
                if (this.bool_0)
                {
                    this.bool_0 = false;
                    this.InitControl();
                    this.bool_0 = true;
                }
            }
        }
    }
}

