using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class MDomainControl : UserControl
    {
        private Container components = null;
        private Label label1;
        private Label label3;
        private Label label5;
        private bool m_CanDo = false;
        private bool m_IsEdit = true;
        private ISpatialReference m_pSpatialRefrence;
        private TextEdit textBoxMaxValue;
        private TextEdit textBoxMinValue;
        private TextEdit textBoxPrecision;

        public MDomainControl()
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
            double outMMin = -10000.0;
            double outMMax = 11474.83645;
            double num3 = 100000.0;
            if (((ISpatialReference2GEN) this.m_pSpatialRefrence).HasMPrecision())
            {
                ((ISpatialReference2GEN) this.m_pSpatialRefrence).GetMDomain(ref outMMin, ref outMMax);
                double num4 = outMMax - outMMin;
                num3 = 2147483645.0 / num4;
            }
            else
            {
                this.m_pSpatialRefrence.SetMDomain(outMMin, outMMax);
            }
            this.textBoxMinValue.Text = outMMin.ToString();
            this.textBoxMaxValue.Text = outMMax.ToString();
            this.textBoxPrecision.Text = num3.ToString();
        }

        private void InitializeComponent()
        {
            this.label5 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.textBoxMinValue = new TextEdit();
            this.textBoxMaxValue = new TextEdit();
            this.textBoxPrecision = new TextEdit();
            this.textBoxMinValue.Properties.BeginInit();
            this.textBoxMaxValue.Properties.BeginInit();
            this.textBoxPrecision.Properties.BeginInit();
            base.SuspendLayout();
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 0x38);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 0x11;
            this.label5.Text = "精度";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x98, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2a, 0x11);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "最大值";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 0x11);
            this.label1.TabIndex = 15;
            this.label1.Text = "最小值";
            this.textBoxMinValue.EditValue = "";
            this.textBoxMinValue.Location = new System.Drawing.Point(0x38, 0x18);
            this.textBoxMinValue.Name = "textBoxMinValue";
            this.textBoxMinValue.Size = new Size(0x58, 0x17);
            this.textBoxMinValue.TabIndex = 0x15;
            this.textBoxMinValue.TextChanged += new EventHandler(this.textBoxMinValue_TextChanged);
            this.textBoxMaxValue.EditValue = "";
            this.textBoxMaxValue.Location = new System.Drawing.Point(200, 0x18);
            this.textBoxMaxValue.Name = "textBoxMaxValue";
            this.textBoxMaxValue.Size = new Size(0x58, 0x17);
            this.textBoxMaxValue.TabIndex = 0x16;
            this.textBoxMaxValue.TextChanged += new EventHandler(this.textBoxMaxValue_TextChanged);
            this.textBoxPrecision.EditValue = "";
            this.textBoxPrecision.Location = new System.Drawing.Point(0x38, 0x38);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new Size(0x58, 0x17);
            this.textBoxPrecision.TabIndex = 0x17;
            this.textBoxPrecision.TextChanged += new EventHandler(this.textBoxPrecision_TextChanged);
            base.Controls.Add(this.textBoxPrecision);
            base.Controls.Add(this.textBoxMaxValue);
            base.Controls.Add(this.textBoxMinValue);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Name = "MDomainControl";
            base.Size = new Size(0x130, 280);
            base.Load += new EventHandler(this.MDomainControl_Load);
            this.textBoxMinValue.Properties.EndInit();
            this.textBoxMaxValue.Properties.EndInit();
            this.textBoxPrecision.Properties.EndInit();
            base.ResumeLayout(false);
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
                if (Common.IsNmuber(this.textBoxPrecision.Text))
                {
                    this.textBoxPrecision.ForeColor = SystemColors.WindowText;
                    double num3 = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num4 = 2147483645.0 / num3;
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
            if (Common.IsNmuber(textBox.Text))
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
                num4 = 2147483645.0 / num3;
                inMMax = inMMin + num4;
                this.textBoxMaxValue.Text = inMMax.ToString();
            }
            else
            {
                this.textBoxPrecision.Text = (2147483645.0 / num4).ToString();
            }
            this.m_pSpatialRefrence.SetMDomain(inMMin, inMMax);
        }

        public bool IsEdit
        {
            set
            {
                this.m_IsEdit = value;
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

