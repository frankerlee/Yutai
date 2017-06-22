using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class XYDomainControl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private bool bool_2 = false;
        private Container container_0 = null;

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
            if (((ISpatialReference2GEN) this.ispatialReference_0).HasXYPrecision())
            {
                ((ISpatialReference2GEN) this.ispatialReference_0).GetDomain(ref xMin, ref xMax, ref yMin, ref yMax);
                double num6 = xMax - xMin;
                double num7 = yMax - yMin;
                num6 = (num6 > num7) ? num6 : num7;
                num5 = 2147483645.0 / num6;
            }
            else
            {
                this.ispatialReference_0.SetDomain(xMin, xMax, yMin, yMax);
            }
            this.textBoxMinX.Text = xMin.ToString();
            this.textBoxMinY.Text = yMin.ToString();
            this.textBoxMaxX.Text = xMax.ToString();
            this.textBoxMaxY.Text = yMax.ToString();
            this.textBoxPrecision.Text = num5.ToString();
        }

 private void method_0(TextEdit textEdit_0)
        {
            if (CommonHelper.IsNmuber(textEdit_0.Text))
            {
                double num5;
                double num6;
                textEdit_0.ForeColor = SystemColors.WindowText;
                double xMin = Convert.ToDouble(this.textBoxMinX.Text);
                double yMin = Convert.ToDouble(this.textBoxMinY.Text);
                double xMax = Convert.ToDouble(this.textBoxMaxX.Text);
                double yMax = Convert.ToDouble(this.textBoxMaxY.Text);
                if (xMin > xMax)
                {
                    num5 = Convert.ToDouble(this.textBoxPrecision.Text);
                    num6 = 2147483645.0 / num5;
                    xMax = xMin + num6;
                    this.textBoxMaxX.Text = xMax.ToString();
                }
                else if (yMin > yMax)
                {
                    num5 = Convert.ToDouble(this.textBoxPrecision.Text);
                    num6 = 2147483645.0 / num5;
                    yMax = yMin + num6;
                    this.textBoxMaxY.Text = yMax.ToString();
                }
                else
                {
                    num6 = xMax - xMin;
                    double num7 = yMax - yMin;
                    num6 = (num6 > num7) ? num6 : num7;
                    this.textBoxPrecision.Text = (2147483645.0 / num6).ToString();
                }
                this.ispatialReference_0.SetDomain(xMin, xMax, yMin, yMax);
            }
            else
            {
                textEdit_0.ForeColor = Color.Red;
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (CommonHelper.IsNmuber(this.textBoxPrecision.Text))
                {
                    this.textBoxPrecision.ForeColor = SystemColors.WindowText;
                    double num = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num2 = 2147483645.0 / num;
                    double xMin = Convert.ToDouble(this.textBoxMinX.Text);
                    double yMin = Convert.ToDouble(this.textBoxMinY.Text);
                    double xMax = xMin + num2;
                    double yMax = yMin + num2;
                    this.ispatialReference_0.SetDomain(xMin, xMax, yMin, yMax);
                    this.textBoxMaxX.Text = xMax.ToString();
                    this.textBoxMaxY.Text = yMax.ToString();
                    this.bool_0 = true;
                }
                else
                {
                    this.textBoxPrecision.ForeColor = Color.Red;
                    this.bool_0 = true;
                }
            }
        }

        private void textBoxMaxX_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.method_0(this.textBoxMaxX);
                this.bool_0 = true;
            }
        }

        private void textBoxMaxY_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.method_0(this.textBoxMaxY);
                this.bool_0 = true;
            }
        }

        private void textBoxMinX_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.method_0(this.textBoxMinX);
                this.bool_0 = true;
            }
        }

        private void textBoxMinY_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.method_0(this.textBoxMinY);
                this.bool_0 = true;
            }
        }

        private void XYDomainControl_Load(object sender, EventArgs e)
        {
            if (!this.bool_1)
            {
                this.textBoxMaxX.Properties.ReadOnly = false;
                this.textBoxMaxY.Properties.ReadOnly = false;
                this.textBoxMinY.Properties.ReadOnly = false;
                this.textBoxMinX.Properties.ReadOnly = false;
                this.textBoxPrecision.Properties.ReadOnly = false;
            }
            this.InitControl();
            this.bool_0 = true;
        }

        public bool IsEdit
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public bool IsHighPrecision
        {
            set
            {
                this.bool_2 = value;
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
                IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                this.bool_2 = precision.IsHighPrecision;
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

