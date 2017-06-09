using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class CartoLineControl : UserControl, CommonInterface
    {
        private ColorEdit colorEdit1;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private bool m_CanDo = true;
        public ICartographicLineSymbol m_CartographLineSymbol;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownWidth;
        private RadioGroup radioGroupLineCapStyle;
        private RadioGroup radioGroupLineJoinStyle;

        public event ValueChangedHandler ValueChanged;

        public CartoLineControl()
        {
            this.InitializeComponent();
        }

        private void CartoLineControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_CartographLineSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_CartographLineSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
            r = (int) num;
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            switch (this.m_CartographLineSymbol.Cap)
            {
                case esriLineCapStyle.esriLCSButt:
                    this.radioGroupLineCapStyle.SelectedIndex = 0;
                    break;

                case esriLineCapStyle.esriLCSRound:
                    this.radioGroupLineCapStyle.SelectedIndex = 1;
                    break;

                case esriLineCapStyle.esriLCSSquare:
                    this.radioGroupLineCapStyle.SelectedIndex = 2;
                    break;
            }
            switch (this.m_CartographLineSymbol.Join)
            {
                case esriLineJoinStyle.esriLJSMitre:
                    this.radioGroupLineJoinStyle.SelectedIndex = 0;
                    break;

                case esriLineJoinStyle.esriLJSRound:
                    this.radioGroupLineJoinStyle.SelectedIndex = 1;
                    break;

                case esriLineJoinStyle.esriLJSBevel:
                    this.radioGroupLineJoinStyle.SelectedIndex = 2;
                    break;
            }
            this.numericUpDownWidth.Value = (decimal) (this.m_CartographLineSymbol.Width * this.m_unit);
            this.SetColorEdit(this.colorEdit1, this.m_CartographLineSymbol.Color);
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.radioGroupLineCapStyle = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.radioGroupLineJoinStyle = new RadioGroup();
            this.numericUpDownWidth = new SpinEdit();
            this.colorEdit1.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioGroupLineCapStyle.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.radioGroupLineJoinStyle.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x18);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 1;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x3a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "宽度";
            this.groupBox1.Controls.Add(this.radioGroupLineCapStyle);
            this.groupBox1.Location = new Point(0x10, 0x60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(80, 0x60);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "线头";
            this.radioGroupLineCapStyle.Location = new Point(12, 0x10);
            this.radioGroupLineCapStyle.Name = "radioGroupLineCapStyle";
            this.radioGroupLineCapStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupLineCapStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupLineCapStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupLineCapStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "平头"), new RadioGroupItem(null, "圆头"), new RadioGroupItem(null, "方头") });
            this.radioGroupLineCapStyle.Size = new Size(0x38, 0x40);
            this.radioGroupLineCapStyle.TabIndex = 50;
            this.radioGroupLineCapStyle.SelectedIndexChanged += new EventHandler(this.radioGroupLineCapStyle_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.radioGroupLineJoinStyle);
            this.groupBox2.Location = new Point(0x80, 0x60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(80, 0x60);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线连接";
            this.radioGroupLineJoinStyle.Location = new Point(12, 0x10);
            this.radioGroupLineJoinStyle.Name = "radioGroupLineJoinStyle";
            this.radioGroupLineJoinStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupLineJoinStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupLineJoinStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupLineJoinStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "平头"), new RadioGroupItem(null, "圆头"), new RadioGroupItem(null, "方头") });
            this.radioGroupLineJoinStyle.Size = new Size(0x38, 0x40);
            this.radioGroupLineJoinStyle.TabIndex = 0x33;
            this.radioGroupLineJoinStyle.SelectedIndexChanged += new EventHandler(this.radioGroupLineJoinStyle_SelectedIndexChanged);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(0x38, 0x38);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits2 = new int[4];
            bits2[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits2);
            this.numericUpDownWidth.Properties.UseCtrlIncrement = false;
            this.numericUpDownWidth.Size = new Size(80, 0x17);
            this.numericUpDownWidth.TabIndex = 0x30;
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Name = "CartoLineControl";
            base.Size = new Size(0x120, 0xe8);
            base.Load += new EventHandler(this.CartoLineControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioGroupLineCapStyle.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.radioGroupLineJoinStyle.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public static bool IsNmuber(string str)
        {
            if (str.Length > 0)
            {
                int num2;
                int num = 0;
                if ((str[0] < '0') || (str[0] > '9'))
                {
                    if (str[0] != '.')
                    {
                        if (str[0] != '-')
                        {
                            if (str[0] != '+')
                            {
                                return false;
                            }
                            for (num2 = 1; num2 < str.Length; num2++)
                            {
                                if ((str[num2] < '0') || (str[num2] > '9'))
                                {
                                    if (str[num2] != '.')
                                    {
                                        return false;
                                    }
                                    if (num == 1)
                                    {
                                        return false;
                                    }
                                    num++;
                                }
                            }
                        }
                        else
                        {
                            for (num2 = 1; num2 < str.Length; num2++)
                            {
                                if ((str[num2] < '0') || (str[num2] > '9'))
                                {
                                    if (str[num2] != '.')
                                    {
                                        return false;
                                    }
                                    if (num == 1)
                                    {
                                        return false;
                                    }
                                    num++;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (num2 = 1; num2 < str.Length; num2++)
                        {
                            if ((str[num2] < '0') || (str[num2] > '9'))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    for (num2 = 1; num2 < str.Length; num2++)
                    {
                        if ((str[num2] < '0') || (str[num2] > '9'))
                        {
                            if (str[num2] != '.')
                            {
                                return false;
                            }
                            if (num == 1)
                            {
                                return false;
                            }
                            num++;
                        }
                    }
                }
            }
            return true;
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownWidth.Value <= 0M)
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownWidth.ForeColor = SystemColors.WindowText;
                    this.m_CartographLineSymbol.Width = ((double) this.numericUpDownWidth.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void radioGroupLineCapStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                switch (this.radioGroupLineCapStyle.SelectedIndex)
                {
                    case 0:
                        this.m_CartographLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
                        break;

                    case 1:
                        this.m_CartographLineSymbol.Cap = esriLineCapStyle.esriLCSRound;
                        break;

                    case 2:
                        this.m_CartographLineSymbol.Cap = esriLineCapStyle.esriLCSSquare;
                        break;
                }
                this.refresh(e);
            }
        }

        private void radioGroupLineJoinStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                switch (this.radioGroupLineCapStyle.SelectedIndex)
                {
                    case 0:
                        this.m_CartographLineSymbol.Join = esriLineJoinStyle.esriLJSMitre;
                        break;

                    case 1:
                        this.m_CartographLineSymbol.Join = esriLineJoinStyle.esriLJSRound;
                        break;

                    case 2:
                        this.m_CartographLineSymbol.Join = esriLineJoinStyle.esriLJSBevel;
                        break;
                }
                this.refresh(e);
            }
        }

        private void refresh(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }
    }
}

