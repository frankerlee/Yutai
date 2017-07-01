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
    internal partial class ArrowMarkerControl : UserControl, CommonInterface
    {
        public IArrowMarkerSymbol m_ArrowMarkerSymbol;
        private bool m_CanDo = false;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public ArrowMarkerControl()
        {
            this.InitializeComponent();
        }

        private void ArrowMarkerControl_Load(object sender, EventArgs e)
        {
            this.numericUpDownLength.Value = (decimal) (this.m_ArrowMarkerSymbol.Length*this.m_unit);
            this.numericUpDownWidth.Value = (decimal) (this.m_ArrowMarkerSymbol.Width*this.m_unit);
            this.numericUpDownXOffset.Value = (decimal) (this.m_ArrowMarkerSymbol.XOffset*this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_ArrowMarkerSymbol.YOffset*this.m_unit);
            this.numericUpDownAngle.Value = (decimal) this.m_ArrowMarkerSymbol.Angle;
            this.SetColorEdit(this.colorEdit1, this.m_ArrowMarkerSymbol.Color);
            this.m_CanDo = true;
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownLength.Value = (decimal) ((((double) this.numericUpDownLength.Value)/this.m_unit)*newunit);
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value)/this.m_unit)*newunit);
            this.numericUpDownXOffset.Value =
                (decimal) ((((double) this.numericUpDownXOffset.Value)/this.m_unit)*newunit);
            this.numericUpDownYOffset.Value =
                (decimal) ((((double) this.numericUpDownYOffset.Value)/this.m_unit)*newunit);
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

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
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
                    this.m_ArrowMarkerSymbol.Length = ((double) this.numericUpDownLength.Value)/this.m_unit;
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
                    this.m_ArrowMarkerSymbol.Width = ((double) this.numericUpDownWidth.Value)/this.m_unit;
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
                    this.m_ArrowMarkerSymbol.XOffset = ((double) this.numericUpDownXOffset.Value)/this.m_unit;
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
                    this.m_ArrowMarkerSymbol.YOffset = ((double) this.numericUpDownYOffset.Value)/this.m_unit;
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