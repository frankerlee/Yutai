using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class LineFillControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public ILineFillSymbol m_LineFillSymbol;
        public IStyleGallery m_pSG;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public LineFillControl()
        {
            this.InitializeComponent();
        }

        private void btnFillLine_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_LineFillSymbol.LineSymbol != null)
                {
                    selector.SetSymbol((ISymbol) this.m_LineFillSymbol.LineSymbol);
                }
                else
                {
                    selector.SetSymbol(new SimpleLineSymbolClass());
                }
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_LineFillSymbol.LineSymbol = (ILineSymbol) selector.GetSymbol();
                    this.btnFillLine.Style = this.m_LineFillSymbol.LineSymbol;
                    this.btnFillLine.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void btnOutline_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_LineFillSymbol.Outline == null)
                {
                    selector.SetSymbol(new SimpleLineSymbolClass());
                }
                else
                {
                    selector.SetSymbol((ISymbol) this.m_LineFillSymbol.Outline);
                }
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_LineFillSymbol.Outline = (ILineSymbol) selector.GetSymbol();
                    this.btnOutline.Style = this.m_LineFillSymbol.Outline;
                    this.btnOutline.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownOffset.Value = (decimal) ((((double) this.numericUpDownOffset.Value)/this.m_unit)*newunit);
            this.numericUpDownSpace.Value = (decimal) ((((double) this.numericUpDownSpace.Value)/this.m_unit)*newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_LineFillSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_LineFillSymbol.Color = pColor;
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
            this.numericUpDownAngle.Value = (decimal) this.m_LineFillSymbol.Angle;
            this.numericUpDownOffset.Value = (decimal) (this.m_LineFillSymbol.Offset*this.m_unit);
            this.numericUpDownSpace.Value = (decimal) (this.m_LineFillSymbol.Separation*this.m_unit);
            this.SetColorEdit(this.colorEdit1, this.m_LineFillSymbol.Color);
            this.btnFillLine.Style = this.m_LineFillSymbol.LineSymbol;
            this.btnOutline.Style = this.m_LineFillSymbol.Outline;
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

        private void LineFillControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
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
                    this.m_LineFillSymbol.Angle = (double) this.numericUpDownAngle.Value;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownOffset.Value == 0M) && !IsNmuber(this.numericUpDownOffset.Text))
                {
                    this.numericUpDownOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownOffset.ForeColor = SystemColors.WindowText;
                    this.m_LineFillSymbol.Offset = ((double) this.numericUpDownOffset.Value)/this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownSpace_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownSpace.Value <= 0M)
                {
                    this.numericUpDownSpace.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownSpace.ForeColor = SystemColors.WindowText;
                    this.m_LineFillSymbol.Separation = ((double) this.numericUpDownSpace.Value)/this.m_unit;
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