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
    internal partial class SimpleLineControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public ISimpleLineSymbol m_pSimpleLineSymbol;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public SimpleLineControl()
        {
            this.InitializeComponent();
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.cboStyle.SelectedIndex > 0)
                {
                    this.m_CanDo = false;
                    this.numericUpDownWidth.Value = (decimal) this.m_unit;
                    this.m_pSimpleLineSymbol.Width = 1.0;
                    this.m_CanDo = true;
                    this.numericUpDownWidth.Enabled = false;
                }
                else
                {
                    this.numericUpDownWidth.Enabled = true;
                }
                this.m_pSimpleLineSymbol.Style = (esriSimpleLineStyle) this.cboStyle.SelectedIndex;
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value)/this.m_unit)*newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_pSimpleLineSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_pSimpleLineSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.cboStyle.SelectedIndex = (int) this.m_pSimpleLineSymbol.Style;
            this.numericUpDownWidth.Value = (decimal) (this.m_pSimpleLineSymbol.Width*this.m_unit);
            this.SetColorEdit(this.colorEdit1, this.m_pSimpleLineSymbol.Color);
            this.m_CanDo = true;
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

        private void numericUpDownWidth_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownWidth.Value < 0M)
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else if ((this.numericUpDownWidth.Value == 0M) && !IsNmuber(this.numericUpDownWidth.Text))
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownWidth.ForeColor = SystemColors.WindowText;
                    this.m_pSimpleLineSymbol.Width = ((double) this.numericUpDownWidth.Value)/this.m_unit;
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

        private void SimpleLineControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
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