using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class XYDomainControl : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsEdit = true;
        private bool m_IsHighPrecision = false;

        public XYDomainControl()
        {
            this.InitializeComponent();
        }

        public void InitControl()
        {
            double xMin = -10000.0;
            double xMax = 11474.83645;
            double yMin = -10000.0;
            double yMax = 11474.83645;
            double num5 = 100000.0;
            if (((ISpatialReference2GEN) this.m_pSpatialRefrence).HasXYPrecision())
            {
                ((ISpatialReference2GEN) this.m_pSpatialRefrence).GetDomain(ref xMin, ref xMax, ref yMin, ref yMax);
                double num6 = xMax - xMin;
                double num7 = yMax - yMin;
                num6 = (num6 > num7) ? num6 : num7;
                num5 = 2147483645.0/num6;
            }
            else
            {
                this.m_pSpatialRefrence.SetDomain(xMin, xMax, yMin, yMax);
            }
            this.textBoxMinX.Text = xMin.ToString();
            this.textBoxMinY.Text = yMin.ToString();
            this.textBoxMaxX.Text = xMax.ToString();
            this.textBoxMaxY.Text = yMax.ToString();
            this.textBoxPrecision.Text = num5.ToString();
        }

        private void textBoxMaxX_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                this.TextChange(this.textBoxMaxX);
                this.m_CanDo = true;
            }
        }

        private void textBoxMaxY_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                this.TextChange(this.textBoxMaxY);
                this.m_CanDo = true;
            }
        }

        private void textBoxMinX_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                this.TextChange(this.textBoxMinX);
                this.m_CanDo = true;
            }
        }

        private void textBoxMinY_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_CanDo = false;
                this.TextChange(this.textBoxMinY);
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
                    double num5 = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num6 = 2147483645.0/num5;
                    double xMin = Convert.ToDouble(this.textBoxMinX.Text);
                    double yMin = Convert.ToDouble(this.textBoxMinY.Text);
                    double xMax = xMin + num6;
                    double yMax = yMin + num6;
                    this.m_pSpatialRefrence.SetDomain(xMin, xMax, yMin, yMax);
                    this.textBoxMaxX.Text = xMax.ToString();
                    this.textBoxMaxY.Text = yMax.ToString();
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
            double num5;
            double num6;
            if (CommonHelper.IsNmuber(textBox.Text))
            {
                textBox.ForeColor = SystemColors.WindowText;
            }
            else
            {
                textBox.ForeColor = Color.Red;
                return;
            }
            double xMin = Convert.ToDouble(this.textBoxMinX.Text);
            double yMin = Convert.ToDouble(this.textBoxMinY.Text);
            double xMax = Convert.ToDouble(this.textBoxMaxX.Text);
            double yMax = Convert.ToDouble(this.textBoxMaxY.Text);
            if (xMin > xMax)
            {
                num5 = Convert.ToDouble(this.textBoxPrecision.Text);
                num6 = 2147483645.0/num5;
                xMax = xMin + num6;
                this.textBoxMaxX.Text = xMax.ToString();
            }
            else if (yMin > yMax)
            {
                num5 = Convert.ToDouble(this.textBoxPrecision.Text);
                num6 = 2147483645.0/num5;
                yMax = yMin + num6;
                this.textBoxMaxY.Text = yMax.ToString();
            }
            else
            {
                num6 = xMax - xMin;
                double num7 = yMax - yMin;
                num6 = (num6 > num7) ? num6 : num7;
                this.textBoxPrecision.Text = (2147483645.0/num6).ToString();
            }
            this.m_pSpatialRefrence.SetDomain(xMin, xMax, yMin, yMax);
        }

        private void XYDomainControl_Load(object sender, EventArgs e)
        {
            if (!this.m_IsEdit)
            {
                this.textBoxMaxX.Properties.ReadOnly = false;
                this.textBoxMaxY.Properties.ReadOnly = false;
                this.textBoxMinY.Properties.ReadOnly = false;
                this.textBoxMinX.Properties.ReadOnly = false;
                this.textBoxPrecision.Properties.ReadOnly = false;
            }
            this.InitControl();
            this.m_CanDo = true;
        }

        public bool IsEdit
        {
            set { this.m_IsEdit = value; }
        }

        public bool IsHighPrecision
        {
            set { this.m_IsHighPrecision = value; }
        }

        public ISpatialReference SpatialRefrence
        {
            get { return this.m_pSpatialRefrence; }
            set
            {
                this.m_pSpatialRefrence = value;
                IControlPrecision2 pSpatialRefrence = this.m_pSpatialRefrence as IControlPrecision2;
                this.m_IsHighPrecision = pSpatialRefrence.IsHighPrecision;
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