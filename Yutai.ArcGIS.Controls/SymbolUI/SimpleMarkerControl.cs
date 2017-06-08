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
    internal class SimpleMarkerControl : UserControl, CommonInterface
    {
        private ComboBoxEdit cboStyle;
        private CheckEdit chkUseOutline;
        private ColorEdit colorEdit1;
        private ColorEdit colorEditOutline;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private bool m_CanDo = true;
        public ISimpleMarkerSymbol m_SimpleMarkerSymbol;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownOutlineWidth;
        private SpinEdit numericUpDownSize;
        private SpinEdit numericUpDownXOffset;
        private SpinEdit numericUpDownYOffset;

        public event ValueChangedHandler ValueChanged;

        public SimpleMarkerControl()
        {
            this.InitializeComponent();
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_SimpleMarkerSymbol.Style = (esriSimpleMarkerStyle) this.cboStyle.SelectedIndex;
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownOutlineWidth.Value = (decimal) ((((double) this.numericUpDownOutlineWidth.Value) / this.m_unit) * newunit);
            this.numericUpDownSize.Value = (decimal) ((((double) this.numericUpDownSize.Value) / this.m_unit) * newunit);
            this.numericUpDownXOffset.Value = (decimal) ((((double) this.numericUpDownXOffset.Value) / this.m_unit) * newunit);
            this.numericUpDownYOffset.Value = (decimal) ((((double) this.numericUpDownYOffset.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void chkUseOutline_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_SimpleMarkerSymbol.Outline = this.chkUseOutline.Checked;
                this.colorEditOutline.Enabled = this.chkUseOutline.Checked;
                this.numericUpDownOutlineWidth.Enabled = this.chkUseOutline.Checked;
                this.refresh(e);
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_SimpleMarkerSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_SimpleMarkerSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void colorEditOutline_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor outlineColor = this.m_SimpleMarkerSymbol.OutlineColor;
                this.UpdateColorFromColorEdit(this.colorEditOutline, outlineColor);
                this.m_SimpleMarkerSymbol.OutlineColor = outlineColor;
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.colorEditOutline = new ColorEdit();
            this.numericUpDownSize = new SpinEdit();
            this.numericUpDownYOffset = new SpinEdit();
            this.numericUpDownXOffset = new SpinEdit();
            this.numericUpDownOutlineWidth = new SpinEdit();
            this.cboStyle = new ComboBoxEdit();
            this.chkUseOutline = new CheckEdit();
            this.colorEdit1.Properties.BeginInit();
            this.colorEditOutline.Properties.BeginInit();
            this.numericUpDownSize.Properties.BeginInit();
            this.numericUpDownYOffset.Properties.BeginInit();
            this.numericUpDownXOffset.Properties.BeginInit();
            this.numericUpDownOutlineWidth.Properties.BeginInit();
            this.cboStyle.Properties.BeginInit();
            this.chkUseOutline.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "样式";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x55);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "大小";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x75);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 7;
            this.label4.Text = "X偏移";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x95);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 10;
            this.label5.Text = "Y偏移";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xb8, 0x72);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x42, 0x11);
            this.label6.TabIndex = 14;
            this.label6.Text = "轮廓线颜色";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xb8, 0x91);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x36, 0x11);
            this.label7.TabIndex = 0x10;
            this.label7.Text = "轮廓线宽";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x10);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0x2d;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.colorEditOutline.EditValue = Color.Empty;
            this.colorEditOutline.Location = new Point(0x100, 0x70);
            this.colorEditOutline.Name = "colorEditOutline";
            this.colorEditOutline.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditOutline.Size = new Size(0x30, 0x17);
            this.colorEditOutline.TabIndex = 0x2e;
            this.colorEditOutline.EditValueChanged += new EventHandler(this.colorEditOutline_EditValueChanged);
            int[] bits = new int[4];
            this.numericUpDownSize.EditValue = new decimal(bits);
            this.numericUpDownSize.Location = new Point(0x38, 80);
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownSize.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownSize.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownSize.Properties.MaxValue = new decimal(bits);
            this.numericUpDownSize.Properties.UseCtrlIncrement = false;
            this.numericUpDownSize.Size = new Size(80, 0x17);
            this.numericUpDownSize.TabIndex = 0x45;
            this.numericUpDownSize.TextChanged += new EventHandler(this.numericUpDownSize_ValueChanged);
            bits = new int[4];
            this.numericUpDownYOffset.EditValue = new decimal(bits);
            this.numericUpDownYOffset.Location = new Point(0x38, 0x90);
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
            this.numericUpDownYOffset.TabIndex = 0x44;
            this.numericUpDownYOffset.TextChanged += new EventHandler(this.numericUpDownYOffset_ValueChanged);
            bits = new int[4];
            this.numericUpDownXOffset.EditValue = new decimal(bits);
            this.numericUpDownXOffset.Location = new Point(0x38, 0x70);
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
            this.numericUpDownXOffset.TabIndex = 0x43;
            this.numericUpDownXOffset.TextChanged += new EventHandler(this.numericUpDownXOffset_ValueChanged);
            bits = new int[4];
            this.numericUpDownOutlineWidth.EditValue = new decimal(bits);
            this.numericUpDownOutlineWidth.Location = new Point(0x100, 0x90);
            this.numericUpDownOutlineWidth.Name = "numericUpDownOutlineWidth";
            this.numericUpDownOutlineWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownOutlineWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownOutlineWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownOutlineWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownOutlineWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownOutlineWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownOutlineWidth.Properties.UseCtrlIncrement = false;
            this.numericUpDownOutlineWidth.Size = new Size(80, 0x17);
            this.numericUpDownOutlineWidth.TabIndex = 70;
            this.numericUpDownOutlineWidth.TextChanged += new EventHandler(this.numericUpDownOutlineWidth_ValueChanged);
            this.cboStyle.EditValue = "";
            this.cboStyle.Location = new Point(0x38, 0x30);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStyle.Properties.Items.AddRange(new object[] { "圆形", "方形", "十字形", "X形", "棱形" });
            this.cboStyle.Size = new Size(80, 0x17);
            this.cboStyle.TabIndex = 0x47;
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            this.chkUseOutline.Location = new Point(0xb8, 80);
            this.chkUseOutline.Name = "chkUseOutline";
            this.chkUseOutline.Properties.Caption = "使用轮廓线";
            this.chkUseOutline.Size = new Size(0x58, 0x13);
            this.chkUseOutline.TabIndex = 0x48;
            this.chkUseOutline.CheckedChanged += new EventHandler(this.chkUseOutline_CheckedChanged);
            base.Controls.Add(this.chkUseOutline);
            base.Controls.Add(this.cboStyle);
            base.Controls.Add(this.numericUpDownOutlineWidth);
            base.Controls.Add(this.numericUpDownSize);
            base.Controls.Add(this.numericUpDownYOffset);
            base.Controls.Add(this.numericUpDownXOffset);
            base.Controls.Add(this.colorEditOutline);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "SimpleMarkerControl";
            base.Size = new Size(0x178, 0x110);
            base.Load += new EventHandler(this.SimpleMarkerControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.colorEditOutline.Properties.EndInit();
            this.numericUpDownSize.Properties.EndInit();
            this.numericUpDownYOffset.Properties.EndInit();
            this.numericUpDownXOffset.Properties.EndInit();
            this.numericUpDownOutlineWidth.Properties.EndInit();
            this.cboStyle.Properties.EndInit();
            this.chkUseOutline.Properties.EndInit();
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

        private void numericUpDownOutlineWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownOutlineWidth.Value == 0M) && !IsNmuber(this.numericUpDownOutlineWidth.Text))
                {
                    this.numericUpDownOutlineWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownOutlineWidth.ForeColor = SystemColors.WindowText;
                    this.m_SimpleMarkerSymbol.OutlineSize = ((double) this.numericUpDownOutlineWidth.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownSize.Value <= 0M)
                {
                    this.numericUpDownSize.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownSize.ForeColor = SystemColors.WindowText;
                    this.m_SimpleMarkerSymbol.Size = ((double) this.numericUpDownSize.Value) / this.m_unit;
                    this.refresh(e);
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
                    this.m_SimpleMarkerSymbol.XOffset = ((double) this.numericUpDownXOffset.Value) / this.m_unit;
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
                    this.m_SimpleMarkerSymbol.YOffset = ((double) this.numericUpDownYOffset.Value) / this.m_unit;
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

        private void SimpleMarkerControl_Load(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            this.numericUpDownOutlineWidth.Value = (decimal) (this.m_SimpleMarkerSymbol.OutlineSize * this.m_unit);
            this.numericUpDownSize.Value = (decimal) (this.m_SimpleMarkerSymbol.Size * this.m_unit);
            this.numericUpDownXOffset.Value = (decimal) (this.m_SimpleMarkerSymbol.XOffset * this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_SimpleMarkerSymbol.YOffset * this.m_unit);
            this.numericUpDownOutlineWidth.Value = (decimal) (this.m_SimpleMarkerSymbol.OutlineSize * this.m_unit);
            this.cboStyle.SelectedIndex = (int) this.m_SimpleMarkerSymbol.Style;
            this.chkUseOutline.Checked = this.m_SimpleMarkerSymbol.Outline;
            this.SetColorEdit(this.colorEdit1, this.m_SimpleMarkerSymbol.Color);
            this.SetColorEdit(this.colorEditOutline, this.m_SimpleMarkerSymbol.OutlineColor);
            this.colorEditOutline.Enabled = this.m_SimpleMarkerSymbol.Outline;
            this.numericUpDownOutlineWidth.Enabled = this.m_SimpleMarkerSymbol.Outline;
            this.m_CanDo = true;
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

