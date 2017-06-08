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
    internal class ArrowMarkerControl : UserControl, CommonInterface
    {
        private ColorEdit colorEdit1;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        public IArrowMarkerSymbol m_ArrowMarkerSymbol;
        private bool m_CanDo = false;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownAngle;
        private SpinEdit numericUpDownLength;
        private SpinEdit numericUpDownWidth;
        private SpinEdit numericUpDownXOffset;
        private SpinEdit numericUpDownYOffset;

        public event ValueChangedHandler ValueChanged;

        public ArrowMarkerControl()
        {
            this.InitializeComponent();
        }

        private void ArrowMarkerControl_Load(object sender, EventArgs e)
        {
            this.numericUpDownLength.Value = (decimal) (this.m_ArrowMarkerSymbol.Length * this.m_unit);
            this.numericUpDownWidth.Value = (decimal) (this.m_ArrowMarkerSymbol.Width * this.m_unit);
            this.numericUpDownXOffset.Value = (decimal) (this.m_ArrowMarkerSymbol.XOffset * this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_ArrowMarkerSymbol.YOffset * this.m_unit);
            this.numericUpDownAngle.Value = (decimal) this.m_ArrowMarkerSymbol.Angle;
            this.SetColorEdit(this.colorEdit1, this.m_ArrowMarkerSymbol.Color);
            this.m_CanDo = true;
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownLength.Value = (decimal) ((((double) this.numericUpDownLength.Value) / this.m_unit) * newunit);
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value) / this.m_unit) * newunit);
            this.numericUpDownXOffset.Value = (decimal) ((((double) this.numericUpDownXOffset.Value) / this.m_unit) * newunit);
            this.numericUpDownYOffset.Value = (decimal) ((((double) this.numericUpDownYOffset.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_ArrowMarkerSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_ArrowMarkerSymbol.Color = pColor;
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

        private void InitializeComponent()
        {
            this.label7 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.label2 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.numericUpDownAngle = new SpinEdit();
            this.numericUpDownLength = new SpinEdit();
            this.numericUpDownWidth = new SpinEdit();
            this.numericUpDownXOffset = new SpinEdit();
            this.numericUpDownYOffset = new SpinEdit();
            this.colorEdit1.Properties.BeginInit();
            this.numericUpDownAngle.Properties.BeginInit();
            this.numericUpDownLength.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.numericUpDownXOffset.Properties.BeginInit();
            this.numericUpDownYOffset.Properties.BeginInit();
            base.SuspendLayout();
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 120);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 0x11);
            this.label7.TabIndex = 0x23;
            this.label7.Text = "角度";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x88, 0x56);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 0x1d;
            this.label5.Text = "Y偏移";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x88, 0x36);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 0x1a;
            this.label4.Text = "X偏移";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x36);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 0x11);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "长";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0x13;
            this.label1.Text = "颜色";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 0x11);
            this.label2.TabIndex = 0x26;
            this.label2.Text = "宽";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x30, 0x10);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0x2c;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            int[] bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(0x30, 120);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownAngle.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownAngle.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Properties.UseCtrlIncrement = false;
            this.numericUpDownAngle.Size = new Size(80, 0x17);
            this.numericUpDownAngle.TabIndex = 0x2d;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            bits = new int[4];
            this.numericUpDownLength.EditValue = new decimal(bits);
            this.numericUpDownLength.Location = new Point(0x30, 0x30);
            this.numericUpDownLength.Name = "numericUpDownLength";
            this.numericUpDownLength.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownLength.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownLength.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownLength.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownLength.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownLength.Properties.MaxValue = new decimal(bits);
            this.numericUpDownLength.Properties.UseCtrlIncrement = false;
            this.numericUpDownLength.Size = new Size(80, 0x17);
            this.numericUpDownLength.TabIndex = 0x2e;
            this.numericUpDownLength.TextChanged += new EventHandler(this.numericUpDownLength_ValueChanged);
            bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(0x30, 80);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Properties.UseCtrlIncrement = false;
            this.numericUpDownWidth.Size = new Size(80, 0x17);
            this.numericUpDownWidth.TabIndex = 0x2f;
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            bits = new int[4];
            this.numericUpDownXOffset.EditValue = new decimal(bits);
            this.numericUpDownXOffset.Location = new Point(0xb0, 0x30);
            this.numericUpDownXOffset.Name = "numericUpDownXOffset";
            this.numericUpDownXOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownXOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownXOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownXOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownXOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownXOffset.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.numericUpDownXOffset.Properties.MinValue = new decimal(bits);
            this.numericUpDownXOffset.Properties.UseCtrlIncrement = false;
            this.numericUpDownXOffset.Size = new Size(80, 0x17);
            this.numericUpDownXOffset.TabIndex = 0x30;
            this.numericUpDownXOffset.TextChanged += new EventHandler(this.numericUpDownXOffset_ValueChanged);
            bits = new int[4];
            this.numericUpDownYOffset.EditValue = new decimal(bits);
            this.numericUpDownYOffset.Location = new Point(0xb0, 80);
            this.numericUpDownYOffset.Name = "numericUpDownYOffset";
            this.numericUpDownYOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownYOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownYOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownYOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownYOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownYOffset.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.numericUpDownYOffset.Properties.MinValue = new decimal(bits);
            this.numericUpDownYOffset.Properties.UseCtrlIncrement = false;
            this.numericUpDownYOffset.Size = new Size(80, 0x17);
            this.numericUpDownYOffset.TabIndex = 0x31;
            this.numericUpDownYOffset.TextChanged += new EventHandler(this.numericUpDownYOffset_ValueChanged);
            base.Controls.Add(this.numericUpDownYOffset);
            base.Controls.Add(this.numericUpDownXOffset);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.numericUpDownLength);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Name = "ArrowMarkerControl";
            base.Size = new Size(0x158, 240);
            base.Load += new EventHandler(this.ArrowMarkerControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.numericUpDownAngle.Properties.EndInit();
            this.numericUpDownLength.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.numericUpDownXOffset.Properties.EndInit();
            this.numericUpDownYOffset.Properties.EndInit();
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

        private void numericUpDownAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownAngle.Value == 0M) && !IsNmuber(this.numericUpDownAngle.Text))
                {
                    this.numericUpDownAngle.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownAngle.ForeColor = SystemColors.WindowText;
                    this.m_ArrowMarkerSymbol.Angle = (double) this.numericUpDownAngle.Value;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownLength_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownLength.Value <= 0M)
                {
                    this.numericUpDownLength.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownLength.ForeColor = SystemColors.WindowText;
                    this.m_ArrowMarkerSymbol.Length = ((double) this.numericUpDownLength.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
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
                    this.m_ArrowMarkerSymbol.Width = ((double) this.numericUpDownWidth.Value) / this.m_unit;
                    this.refresh(e);
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged(this, e);
                    }
                }
            }
        }

        private void numericUpDownXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownXOffset.Value == 0M) && !IsNmuber(this.numericUpDownXOffset.Text))
                {
                    this.numericUpDownXOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownXOffset.ForeColor = SystemColors.WindowText;
                    this.m_ArrowMarkerSymbol.XOffset = ((double) this.numericUpDownXOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownYOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownYOffset.Value == 0M) && !IsNmuber(this.numericUpDownYOffset.Text))
                {
                    this.numericUpDownYOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownYOffset.ForeColor = SystemColors.WindowText;
                    this.m_ArrowMarkerSymbol.YOffset = ((double) this.numericUpDownYOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
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

