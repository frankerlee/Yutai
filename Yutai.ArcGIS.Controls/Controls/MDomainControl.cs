using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class MDomainControl : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsEdit = true;

        public MDomainControl()
        {
            this.InitializeComponent();
        }

        public void InitControl()
        {
            double outMMin = -10000.0;
            double outMMax = 11474.83645;
            double num3 = 100000.0;
            if (((ISpatialReference2GEN) this.m_pSpatialRefrence).HasMPrecision())
            {
                ((ISpatialReference2GEN) this.m_pSpatialRefrence).GetMDomain(ref outMMin, ref outMMax);
                double num4 = outMMax - outMMin;
                num3 = 2147483645.0/num4;
            }
            else
            {
                this.m_pSpatialRefrence.SetMDomain(outMMin, outMMax);
            }
            this.textBoxMinValue.Text = outMMin.ToString();
            this.textBoxMaxValue.Text = outMMax.ToString();
            this.textBoxPrecision.Text = num3.ToString();
        }

        private void MDomainControl_Load(object sender, EventArgs e)
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
                    double inMMin = Convert.ToDouble(this.textBoxMinValue.Text);
                    double inMMax = inMMin + num4;
                    this.m_pSpatialRefrence.SetMDomain(inMMin, inMMax);
                    this.textBoxMaxValue.Text = inMMax.ToString();
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
            double inMMax = Convert.ToDouble(this.textBoxMaxValue.Text);
            double inMMin = Convert.ToDouble(this.textBoxMinValue.Text);
            double num4 = inMMax - inMMin;
            if (num4 < 0.0)
            {
                double num3 = Convert.ToDouble(this.textBoxPrecision.Text);
                num4 = 2147483645.0/num3;
                inMMax = inMMin + num4;
                this.textBoxMaxValue.Text = inMMax.ToString();
            }
            else
            {
                this.textBoxPrecision.Text = (2147483645.0/num4).ToString();
            }
            this.m_pSpatialRefrence.SetMDomain(inMMin, inMMax);
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