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
    internal partial class SimpleMarkerControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public ISimpleMarkerSymbol m_SimpleMarkerSymbol;
        public double m_unit = 1.0;

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
            this.numericUpDownOutlineWidth.Value =
                (decimal) ((((double) this.numericUpDownOutlineWidth.Value)/this.m_unit)*newunit);
            this.numericUpDownSize.Value = (decimal) ((((double) this.numericUpDownSize.Value)/this.m_unit)*newunit);
            this.numericUpDownXOffset.Value =
                (decimal) ((((double) this.numericUpDownXOffset.Value)/this.m_unit)*newunit);
            this.numericUpDownYOffset.Value =
                (decimal) ((((double) this.numericUpDownYOffset.Value)/this.m_unit)*newunit);
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
                    this.m_SimpleMarkerSymbol.OutlineSize = ((double) this.numericUpDownOutlineWidth.Value)/this.m_unit;
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
                    this.m_SimpleMarkerSymbol.Size = ((double) this.numericUpDownSize.Value)/this.m_unit;
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
                    this.m_SimpleMarkerSymbol.XOffset = ((double) this.numericUpDownXOffset.Value)/this.m_unit;
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
                    this.m_SimpleMarkerSymbol.YOffset = ((double) this.numericUpDownYOffset.Value)/this.m_unit;
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
            this.numericUpDownOutlineWidth.Value = (decimal) (this.m_SimpleMarkerSymbol.OutlineSize*this.m_unit);
            this.numericUpDownSize.Value = (decimal) (this.m_SimpleMarkerSymbol.Size*this.m_unit);
            this.numericUpDownXOffset.Value = (decimal) (this.m_SimpleMarkerSymbol.XOffset*this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_SimpleMarkerSymbol.YOffset*this.m_unit);
            this.numericUpDownOutlineWidth.Value = (decimal) (this.m_SimpleMarkerSymbol.OutlineSize*this.m_unit);
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