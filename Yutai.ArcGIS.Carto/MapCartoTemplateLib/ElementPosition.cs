using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class ElementPosition : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblOffsetX;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private RadioButton rdoLeftCenter;
        private RadioButton rdoLeftLow;
        private RadioButton rdoLeftUpper;
        private RadioButton rdoLowCenter;
        private RadioButton rdoLowLeft;
        private RadioButton rdoLowRight;
        private RadioButton rdoRightCenter;
        private RadioButton rdoRightLow;
        private RadioButton rdoRightUpper;
        private RadioButton rdoUpperCenter;
        private RadioButton rdoUpperLeft;
        private RadioButton rdoUpperRight;
        private TextBox txtH;
        private TextBox txtOffsetX;
        private TextBox txtOffsetY;
        private TextBox txtW;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementPosition()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                double num = Convert.ToDouble(this.txtW.Text);
                double num2 = Convert.ToDouble(this.txtH.Text);
                if ((num == 0.0) && (num2 == 0.0))
                {
                    num = 1.0;
                }
                if (this.mapTemplateElement_0.Element is IRectangleElement)
                {
                    IEnvelope envelope = this.mapTemplateElement_0.Element.Geometry.Envelope;
                    if (num <= 0.0)
                    {
                        num = 1.0;
                    }
                    if (num2 <= 0.0)
                    {
                        num = 1.0;
                    }
                    envelope.XMax = envelope.XMin + num;
                    envelope.YMax = envelope.YMin + num2;
                    this.mapTemplateElement_0.Element.Geometry = envelope;
                }
                this.mapTemplateElement_0.ElementLocation.XOffset = Convert.ToDouble(this.txtOffsetX.Text);
                this.mapTemplateElement_0.ElementLocation.YOffset = Convert.ToDouble(this.txtOffsetY.Text);
                if (this.rdoLeftCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LeftCenter;
                }
                else if (this.rdoLeftLow.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LeftLower;
                }
                else if (this.rdoLeftUpper.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LeftUpper;
                }
                else if (this.rdoLowCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LowerCenter;
                }
                else if (this.rdoLowLeft.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LowerLeft;
                }
                else if (this.rdoLowRight.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LowerRight;
                }
                else if (this.rdoRightCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.RightCenter;
                }
                else if (this.rdoRightLow.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.RightLower;
                }
                else if (this.rdoRightUpper.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.RightUpper;
                }
                else if (this.rdoUpperCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.UpperrCenter;
                }
                else if (this.rdoUpperLeft.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.UpperLeft;
                }
                else if (this.rdoUpperRight.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.UpperRight;
                }
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void ElementPosition_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label2 = new Label();
            this.rdoLowRight = new RadioButton();
            this.rdoLowLeft = new RadioButton();
            this.rdoUpperRight = new RadioButton();
            this.rdoUpperLeft = new RadioButton();
            this.rdoLowCenter = new RadioButton();
            this.rdoLeftCenter = new RadioButton();
            this.rdoLeftLow = new RadioButton();
            this.rdoRightLow = new RadioButton();
            this.rdoRightCenter = new RadioButton();
            this.rdoRightUpper = new RadioButton();
            this.rdoUpperCenter = new RadioButton();
            this.rdoLeftUpper = new RadioButton();
            this.lblOffsetX = new Label();
            this.label1 = new Label();
            this.txtOffsetX = new TextBox();
            this.txtOffsetY = new TextBox();
            this.txtW = new TextBox();
            this.txtH = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox2 = new GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoLowRight);
            this.groupBox1.Controls.Add(this.rdoLowLeft);
            this.groupBox1.Controls.Add(this.rdoUpperRight);
            this.groupBox1.Controls.Add(this.rdoUpperLeft);
            this.groupBox1.Controls.Add(this.rdoLowCenter);
            this.groupBox1.Controls.Add(this.rdoLeftCenter);
            this.groupBox1.Controls.Add(this.rdoLeftLow);
            this.groupBox1.Controls.Add(this.rdoRightLow);
            this.groupBox1.Controls.Add(this.rdoRightCenter);
            this.groupBox1.Controls.Add(this.rdoRightUpper);
            this.groupBox1.Controls.Add(this.rdoUpperCenter);
            this.groupBox1.Controls.Add(this.rdoLeftUpper);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x121, 0x84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "放置位置";
            this.label2.BorderStyle = BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(60, 0x2d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(130, 0x3a);
            this.label2.TabIndex = 12;
            this.label2.Text = "图框";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            this.rdoLowRight.AutoSize = true;
            this.rdoLowRight.Location = new System.Drawing.Point(0xa8, 0x6d);
            this.rdoLowRight.Name = "rdoLowRight";
            this.rdoLowRight.Size = new Size(0x2f, 0x10);
            this.rdoLowRight.TabIndex = 11;
            this.rdoLowRight.Text = "下右";
            this.rdoLowRight.UseVisualStyleBackColor = true;
            this.rdoLowRight.CheckedChanged += new EventHandler(this.rdoLowRight_CheckedChanged);
            this.rdoLowLeft.AutoSize = true;
            this.rdoLowLeft.Location = new System.Drawing.Point(0x35, 0x6d);
            this.rdoLowLeft.Name = "rdoLowLeft";
            this.rdoLowLeft.Size = new Size(0x2f, 0x10);
            this.rdoLowLeft.TabIndex = 10;
            this.rdoLowLeft.Text = "下左";
            this.rdoLowLeft.UseVisualStyleBackColor = true;
            this.rdoUpperRight.AutoSize = true;
            this.rdoUpperRight.Location = new System.Drawing.Point(0x9f, 20);
            this.rdoUpperRight.Name = "rdoUpperRight";
            this.rdoUpperRight.Size = new Size(0x2f, 0x10);
            this.rdoUpperRight.TabIndex = 9;
            this.rdoUpperRight.Text = "上右";
            this.rdoUpperRight.UseVisualStyleBackColor = true;
            this.rdoUpperLeft.AutoSize = true;
            this.rdoUpperLeft.Location = new System.Drawing.Point(0x35, 20);
            this.rdoUpperLeft.Name = "rdoUpperLeft";
            this.rdoUpperLeft.Size = new Size(0x2f, 0x10);
            this.rdoUpperLeft.TabIndex = 8;
            this.rdoUpperLeft.Text = "上左";
            this.rdoUpperLeft.UseVisualStyleBackColor = true;
            this.rdoLowCenter.AutoSize = true;
            this.rdoLowCenter.Location = new System.Drawing.Point(0x6a, 0x6d);
            this.rdoLowCenter.Name = "rdoLowCenter";
            this.rdoLowCenter.Size = new Size(0x2f, 0x10);
            this.rdoLowCenter.TabIndex = 7;
            this.rdoLowCenter.Text = "下中";
            this.rdoLowCenter.UseVisualStyleBackColor = true;
            this.rdoLowCenter.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoLeftCenter.AutoSize = true;
            this.rdoLeftCenter.Location = new System.Drawing.Point(6, 0x41);
            this.rdoLeftCenter.Name = "rdoLeftCenter";
            this.rdoLeftCenter.Size = new Size(0x2f, 0x10);
            this.rdoLeftCenter.TabIndex = 6;
            this.rdoLeftCenter.Text = "左中";
            this.rdoLeftCenter.UseVisualStyleBackColor = true;
            this.rdoLeftCenter.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoLeftLow.AutoSize = true;
            this.rdoLeftLow.Location = new System.Drawing.Point(6, 0x57);
            this.rdoLeftLow.Name = "rdoLeftLow";
            this.rdoLeftLow.Size = new Size(0x2f, 0x10);
            this.rdoLeftLow.TabIndex = 5;
            this.rdoLeftLow.Text = "左下";
            this.rdoLeftLow.UseVisualStyleBackColor = true;
            this.rdoLeftLow.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoRightLow.AutoSize = true;
            this.rdoRightLow.Location = new System.Drawing.Point(0xd4, 0x57);
            this.rdoRightLow.Name = "rdoRightLow";
            this.rdoRightLow.Size = new Size(0x2f, 0x10);
            this.rdoRightLow.TabIndex = 4;
            this.rdoRightLow.Text = "右下";
            this.rdoRightLow.UseVisualStyleBackColor = true;
            this.rdoRightLow.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoRightCenter.AutoSize = true;
            this.rdoRightCenter.Location = new System.Drawing.Point(0xd4, 0x41);
            this.rdoRightCenter.Name = "rdoRightCenter";
            this.rdoRightCenter.Size = new Size(0x2f, 0x10);
            this.rdoRightCenter.TabIndex = 3;
            this.rdoRightCenter.Text = "右中";
            this.rdoRightCenter.UseVisualStyleBackColor = true;
            this.rdoRightCenter.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoRightUpper.AutoSize = true;
            this.rdoRightUpper.Location = new System.Drawing.Point(0xd4, 0x2a);
            this.rdoRightUpper.Name = "rdoRightUpper";
            this.rdoRightUpper.Size = new Size(0x2f, 0x10);
            this.rdoRightUpper.TabIndex = 2;
            this.rdoRightUpper.Text = "右上";
            this.rdoRightUpper.UseVisualStyleBackColor = true;
            this.rdoRightUpper.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoUpperCenter.AutoSize = true;
            this.rdoUpperCenter.Location = new System.Drawing.Point(0x6a, 20);
            this.rdoUpperCenter.Name = "rdoUpperCenter";
            this.rdoUpperCenter.Size = new Size(0x2f, 0x10);
            this.rdoUpperCenter.TabIndex = 1;
            this.rdoUpperCenter.Text = "上中";
            this.rdoUpperCenter.UseVisualStyleBackColor = true;
            this.rdoUpperCenter.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.rdoLeftUpper.AutoSize = true;
            this.rdoLeftUpper.Checked = true;
            this.rdoLeftUpper.Location = new System.Drawing.Point(6, 0x2a);
            this.rdoLeftUpper.Name = "rdoLeftUpper";
            this.rdoLeftUpper.Size = new Size(0x2f, 0x10);
            this.rdoLeftUpper.TabIndex = 0;
            this.rdoLeftUpper.TabStop = true;
            this.rdoLeftUpper.Text = "左上";
            this.rdoLeftUpper.UseVisualStyleBackColor = true;
            this.rdoLeftUpper.CheckedChanged += new EventHandler(this.rdoLeftUpper_CheckedChanged);
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.Location = new System.Drawing.Point(0x10, 0x95);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new Size(0x35, 12);
            this.lblOffsetX.TabIndex = 1;
            this.lblOffsetX.Text = "水平偏移";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0xb6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "竖直偏移";
            this.txtOffsetX.Location = new System.Drawing.Point(0x4b, 0x92);
            this.txtOffsetX.Name = "txtOffsetX";
            this.txtOffsetX.Size = new Size(0x49, 0x15);
            this.txtOffsetX.TabIndex = 3;
            this.txtOffsetX.Text = "0";
            this.txtOffsetX.TextChanged += new EventHandler(this.txtOffsetY_TextChanged);
            this.txtOffsetY.Location = new System.Drawing.Point(0x4b, 0xb3);
            this.txtOffsetY.Name = "txtOffsetY";
            this.txtOffsetY.Size = new Size(0x49, 0x15);
            this.txtOffsetY.TabIndex = 4;
            this.txtOffsetY.Text = "0";
            this.txtOffsetY.TextChanged += new EventHandler(this.txtOffsetY_TextChanged);
            this.txtW.Location = new System.Drawing.Point(0x27, 0x31);
            this.txtW.Name = "txtW";
            this.txtW.Size = new Size(90, 0x15);
            this.txtW.TabIndex = 8;
            this.txtW.Text = "0";
            this.txtW.TextChanged += new EventHandler(this.txtW_TextChanged);
            this.txtH.Location = new System.Drawing.Point(0x27, 0x10);
            this.txtH.Name = "txtH";
            this.txtH.Size = new Size(90, 0x15);
            this.txtH.TabIndex = 7;
            this.txtH.Text = "0";
            this.txtH.TextChanged += new EventHandler(this.txtH_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x36);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "宽";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "高";
            this.groupBox2.Controls.Add(this.txtW);
            this.groupBox2.Controls.Add(this.txtH);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(0x9d, 0x8d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x90, 0x4f);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "大小";
            this.groupBox2.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.txtOffsetY);
            base.Controls.Add(this.txtOffsetX);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblOffsetX);
            base.Controls.Add(this.groupBox1);
            base.Name = "ElementPosition";
            base.Size = new Size(0x13a, 0xdf);
            base.Load += new EventHandler(this.ElementPosition_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            this.bool_0 = false;
            switch (this.mapTemplateElement_0.ElementLocation.LocationType)
            {
                case LocationType.UpperLeft:
                    this.rdoUpperLeft.Checked = true;
                    break;

                case LocationType.UpperrCenter:
                    this.rdoUpperCenter.Checked = true;
                    break;

                case LocationType.UpperRight:
                    this.rdoUpperRight.Checked = true;
                    break;

                case LocationType.LeftUpper:
                    this.rdoLeftUpper.Checked = true;
                    break;

                case LocationType.RightUpper:
                    this.rdoRightUpper.Checked = true;
                    break;

                case LocationType.LeftCenter:
                    this.rdoLeftCenter.Checked = true;
                    break;

                case LocationType.RightCenter:
                    this.rdoRightCenter.Checked = true;
                    break;

                case LocationType.LeftLower:
                    this.rdoLeftLow.Checked = true;
                    break;

                case LocationType.RightLower:
                    this.rdoRightLow.Checked = true;
                    break;

                case LocationType.LowerLeft:
                    this.rdoLowLeft.Checked = true;
                    break;

                case LocationType.LowerCenter:
                    this.rdoLowCenter.Checked = true;
                    break;

                case LocationType.LowerRight:
                    this.rdoLowRight.Checked = true;
                    break;
            }
            this.txtOffsetX.Text = this.mapTemplateElement_0.ElementLocation.XOffset.ToString("0.##");
            this.txtOffsetY.Text = this.mapTemplateElement_0.ElementLocation.YOffset.ToString("0.##");
            if (this.mapTemplateElement_0.Element is IRectangleElement)
            {
                this.groupBox2.Visible = true;
                IEnvelope envelope = this.mapTemplateElement_0.Element.Geometry.Envelope;
                if (!envelope.IsEmpty)
                {
                    this.txtH.Text = envelope.Height.ToString();
                    this.txtW.Text = envelope.Width.ToString();
                }
            }
            this.bool_0 = true;
        }

        private void rdoLeftUpper_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoLowRight_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapCartoTemplateLib.MapTemplateElement;
        }

        private void txtH_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtOffsetY_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtW_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_0();
            }
        }

        public string Title
        {
            get
            {
                return "位置";
            }
            set
            {
            }
        }
    }
}

