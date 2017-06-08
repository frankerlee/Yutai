using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class XYDomainControl : UserControl
    {
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private bool m_CanDo = false;
        private bool m_IsEdit = true;
        private bool m_IsHighPrecision = false;
        private ISpatialReference m_pSpatialRefrence;
        private TextEdit textBoxMaxX;
        private TextEdit textBoxMaxY;
        private TextEdit textBoxMinX;
        private TextEdit textBoxMinY;
        private TextEdit textBoxPrecision;

        public XYDomainControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                num5 = 2147483645.0 / num6;
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.textBoxPrecision = new TextEdit();
            this.textBoxMaxX = new TextEdit();
            this.textBoxMinX = new TextEdit();
            this.textBoxMaxY = new TextEdit();
            this.textBoxMinY = new TextEdit();
            this.textBoxPrecision.Properties.BeginInit();
            this.textBoxMaxX.Properties.BeginInit();
            this.textBoxMinX.Properties.BeginInit();
            this.textBoxMaxY.Properties.BeginInit();
            this.textBoxMinY.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小X";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "最小Y";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x90, 0x20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "最大X";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x90, 80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 3;
            this.label4.Text = "最大Y";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 4;
            this.label5.Text = "精度";
            this.textBoxPrecision.EditValue = "";
            this.textBoxPrecision.Location = new System.Drawing.Point(0x38, 120);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new Size(80, 0x17);
            this.textBoxPrecision.TabIndex = 0x1a;
            this.textBoxPrecision.TextChanged += new EventHandler(this.textBoxMaxY_TextChanged);
            this.textBoxMaxX.EditValue = "";
            this.textBoxMaxX.Location = new System.Drawing.Point(0xc0, 0x20);
            this.textBoxMaxX.Name = "textBoxMaxX";
            this.textBoxMaxX.Size = new Size(80, 0x17);
            this.textBoxMaxX.TabIndex = 0x19;
            this.textBoxMaxX.TextChanged += new EventHandler(this.textBoxMaxX_TextChanged);
            this.textBoxMinX.EditValue = "";
            this.textBoxMinX.Location = new System.Drawing.Point(0x38, 0x20);
            this.textBoxMinX.Name = "textBoxMinX";
            this.textBoxMinX.Size = new Size(80, 0x17);
            this.textBoxMinX.TabIndex = 0x18;
            this.textBoxMinX.TextChanged += new EventHandler(this.textBoxMinX_TextChanged);
            this.textBoxMaxY.EditValue = "";
            this.textBoxMaxY.Location = new System.Drawing.Point(0xc0, 0x48);
            this.textBoxMaxY.Name = "textBoxMaxY";
            this.textBoxMaxY.Size = new Size(80, 0x17);
            this.textBoxMaxY.TabIndex = 0x1c;
            this.textBoxMaxY.TextChanged += new EventHandler(this.textBoxMaxY_TextChanged);
            this.textBoxMinY.EditValue = "";
            this.textBoxMinY.Location = new System.Drawing.Point(0x38, 0x48);
            this.textBoxMinY.Name = "textBoxMinY";
            this.textBoxMinY.Size = new Size(80, 0x17);
            this.textBoxMinY.TabIndex = 0x1b;
            this.textBoxMinY.TextChanged += new EventHandler(this.textBoxMinY_TextChanged);
            base.Controls.Add(this.textBoxMaxY);
            base.Controls.Add(this.textBoxMinY);
            base.Controls.Add(this.textBoxPrecision);
            base.Controls.Add(this.textBoxMaxX);
            base.Controls.Add(this.textBoxMinX);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "XYDomainControl";
            base.Size = new Size(0x120, 280);
            base.Load += new EventHandler(this.XYDomainControl_Load);
            this.textBoxPrecision.Properties.EndInit();
            this.textBoxMaxX.Properties.EndInit();
            this.textBoxMinX.Properties.EndInit();
            this.textBoxMaxY.Properties.EndInit();
            this.textBoxMinY.Properties.EndInit();
            base.ResumeLayout(false);
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
                if (Common.IsNmuber(this.textBoxPrecision.Text))
                {
                    this.textBoxPrecision.ForeColor = SystemColors.WindowText;
                    double num5 = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num6 = 2147483645.0 / num5;
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
            if (Common.IsNmuber(textBox.Text))
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
            set
            {
                this.m_IsEdit = value;
            }
        }

        public bool IsHighPrecision
        {
            set
            {
                this.m_IsHighPrecision = value;
            }
        }

        public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.m_pSpatialRefrence;
            }
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

