using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class ZDomainControl : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsEdit = true;

        public ZDomainControl()
        {
            this.InitializeComponent();
        }

        public void InitControl()
        {
            double outZMin = -10000.0;
            double outZMax = 11474.83645;
            double num3 = 100000.0;
            if (((ISpatialReference2GEN) this.m_pSpatialRefrence).HasZPrecision())
            {
                ((ISpatialReference2GEN) this.m_pSpatialRefrence).GetZDomain(ref outZMin, ref outZMax);
                double num4 = outZMax - outZMin;
                num3 = 2147483645.0/num4;
            }
            else
            {
                this.m_pSpatialRefrence.SetZDomain(outZMin, outZMax);
            }
            this.textBoxMinValue.Text = outZMin.ToString();
            this.textBoxMaxValue.Text = outZMax.ToString();
            this.textBoxPrecision.Text = num3.ToString();
        }

        private void textBoxMaxValue_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                this.TextChange(this.textBoxMaxValue);
                this.m_CanDo = true;
            }
        }

        private void textBoxMinValue_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                this.TextChange(this.textBoxMinValue);
                this.m_CanDo = true;
            }
        }

        private void textBoxPrecision_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                if (CommonHelper.IsNmuber(this.textBoxPrecision.Text))
                {
                    this.textBoxPrecision.ForeColor = SystemColors.WindowText;
                    double num3 = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num4 = 2147483645.0/num3;
                    double inZMin = Convert.ToDouble(this.textBoxMinValue.Text);
                    double inZMax = inZMin + num4;
                    this.m_pSpatialRefrence.SetZDomain(inZMin, inZMax);
                    this.textBoxMaxValue.Text = inZMax.ToString();
                    this.m_CanDo = true;
                }
                else
                {
                    this.textBoxPrecision.ForeColor = Color.Red;
                    this.m_CanDo = true;
                }
            }
        }

        private void TextChange(TextEdit textBox)
        {
            if (CommonHelper.IsNmuber(textBox.Text))
            {
                textBox.ForeColor = SystemColors.WindowText;
            }
            else
            {
                textBox.ForeColor = Color.Red;
                return;
            }
            double inZMax = Convert.ToDouble(this.textBoxMaxValue.Text);
            double inZMin = Convert.ToDouble(this.textBoxMinValue.Text);
            double num4 = inZMax - inZMin;
            if (num4 < 0.0)
            {
                double num3 = Convert.ToDouble(this.textBoxPrecision.Text);
                num4 = 2147483645.0/num3;
                inZMax = inZMin + num4;
                this.textBoxMaxValue.Text = inZMax.ToString();
            }
            else
            {
                this.textBoxPrecision.Text = (2147483645.0/num4).ToString();
            }
            this.m_pSpatialRefrence.SetZDomain(inZMin, inZMax);
        }

        private void ZDomainControl_Load(object sender, EventArgs e)
        {
            if (!this.m_IsEdit)
            {
                this.textBoxMaxValue.Properties.ReadOnly = false;
                this.textBoxMinValue.Properties.ReadOnly = false;
                this.textBoxPrecision.Properties.ReadOnly = false;
            }
            this.InitControl();
            this.m_CanDo = true;
        }

        public bool IsEdit
        {
            set { this.m_IsEdit = value; }
        }

        public ISpatialReference SpatialRefrence
        {
            get { return this.m_pSpatialRefrence; }
            set
            {
                this.m_pSpatialRefrence = value;
                if (this.m_CanDo)
                {
                    this.m_CanDo = false;
                    this.InitControl();
                    this.m_CanDo = true;
                }
            }
        }
    }
}