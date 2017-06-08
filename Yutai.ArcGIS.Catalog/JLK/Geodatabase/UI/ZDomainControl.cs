namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using JLK.Utility.BaseClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ZDomainControl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private Container container_0 = null;
        private ISpatialReference ispatialReference_0;
        private Label label1;
        private Label label3;
        private Label label5;
        private TextEdit textBoxMaxValue;
        private TextEdit textBoxMinValue;
        private TextEdit textBoxPrecision;

        public ZDomainControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public void InitControl()
        {
            double outZMin = -10000.0;
            double outZMax = 11474.83645;
            double num3 = 100000.0;
            if (((ISpatialReference2GEN) this.ispatialReference_0).HasZPrecision())
            {
                ((ISpatialReference2GEN) this.ispatialReference_0).GetZDomain(ref outZMin, ref outZMax);
                double num4 = outZMax - outZMin;
                num3 = 2147483645.0 / num4;
            }
            else
            {
                this.ispatialReference_0.SetZDomain(outZMin, outZMax);
            }
            this.textBoxMinValue.Text = outZMin.ToString();
            this.textBoxMaxValue.Text = outZMax.ToString();
            this.textBoxPrecision.Text = num3.ToString();
        }

        private void InitializeComponent()
        {
            this.label5 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.textBoxPrecision = new TextEdit();
            this.textBoxMaxValue = new TextEdit();
            this.textBoxMinValue = new TextEdit();
            this.textBoxPrecision.Properties.BeginInit();
            this.textBoxMaxValue.Properties.BeginInit();
            this.textBoxMinValue.Properties.BeginInit();
            base.SuspendLayout();
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 0x44);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 11;
            this.label5.Text = "精度";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 0x24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2a, 0x11);
            this.label3.TabIndex = 10;
            this.label3.Text = "最大值";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 0x11);
            this.label1.TabIndex = 9;
            this.label1.Text = "最小值";
            this.textBoxPrecision.EditValue = "";
            this.textBoxPrecision.Location = new System.Drawing.Point(0x38, 0x40);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new Size(0x58, 0x17);
            this.textBoxPrecision.TabIndex = 0x1a;
            this.textBoxPrecision.TextChanged += new EventHandler(this.textBoxPrecision_TextChanged);
            this.textBoxMaxValue.EditValue = "";
            this.textBoxMaxValue.Location = new System.Drawing.Point(0xd0, 0x20);
            this.textBoxMaxValue.Name = "textBoxMaxValue";
            this.textBoxMaxValue.Size = new Size(0x58, 0x17);
            this.textBoxMaxValue.TabIndex = 0x19;
            this.textBoxMaxValue.TextChanged += new EventHandler(this.textBoxMaxValue_TextChanged);
            this.textBoxMinValue.EditValue = "";
            this.textBoxMinValue.Location = new System.Drawing.Point(0x38, 0x20);
            this.textBoxMinValue.Name = "textBoxMinValue";
            this.textBoxMinValue.Size = new Size(0x58, 0x17);
            this.textBoxMinValue.TabIndex = 0x18;
            this.textBoxMinValue.TextChanged += new EventHandler(this.textBoxMinValue_TextChanged);
            base.Controls.Add(this.textBoxPrecision);
            base.Controls.Add(this.textBoxMaxValue);
            base.Controls.Add(this.textBoxMinValue);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Name = "ZDomainControl";
            base.Size = new Size(0x130, 0x150);
            base.Load += new EventHandler(this.ZDomainControl_Load);
            this.textBoxPrecision.Properties.EndInit();
            this.textBoxMaxValue.Properties.EndInit();
            this.textBoxMinValue.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(TextEdit textEdit_0)
        {
            if (Common.IsNmuber(textEdit_0.Text))
            {
                textEdit_0.ForeColor = SystemColors.WindowText;
                double inZMax = Convert.ToDouble(this.textBoxMaxValue.Text);
                double inZMin = Convert.ToDouble(this.textBoxMinValue.Text);
                double num3 = inZMax - inZMin;
                if (num3 < 0.0)
                {
                    double num4 = Convert.ToDouble(this.textBoxPrecision.Text);
                    num3 = 2147483645.0 / num4;
                    inZMax = inZMin + num3;
                    this.textBoxMaxValue.Text = inZMax.ToString();
                }
                else
                {
                    this.textBoxPrecision.Text = (2147483645.0 / num3).ToString();
                }
                this.ispatialReference_0.SetZDomain(inZMin, inZMax);
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
                if (Common.IsNmuber(this.textBoxPrecision.Text))
                {
                    this.textBoxPrecision.ForeColor = SystemColors.WindowText;
                    double num = Convert.ToDouble(this.textBoxPrecision.Text);
                    double num2 = 2147483645.0 / num;
                    double inZMin = Convert.ToDouble(this.textBoxMinValue.Text);
                    double inZMax = inZMin + num2;
                    this.ispatialReference_0.SetZDomain(inZMin, inZMax);
                    this.textBoxMaxValue.Text = inZMax.ToString();
                    this.bool_0 = true;
                }
                else
                {
                    this.textBoxPrecision.ForeColor = Color.Red;
                    this.bool_0 = true;
                }
            }
        }

        private void ZDomainControl_Load(object sender, EventArgs e)
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

