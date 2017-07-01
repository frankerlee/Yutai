using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class CharacterMarkerControl : UserControl, CommonInterface
    {
        public ICharacterMarkerSymbol m_CharacterMarkerSymbol;
        public double m_unit;

        public event ValueChangedHandler ValueChanged;

        public CharacterMarkerControl()
        {
            int num;
            this.m_unit = 1.0;
            this.m_CanDo = true;
            this.components = null;
            this.InitializeComponent();
            for (num = 32; num < 256; num++)
            {
                string str = new string((char) num, 1);
                this.fontlistView.Add(str);
            }
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (num = 0; num < fonts.Families.Length; num++)
            {
                this.cboFontName.Properties.Items.Add(fonts.Families[num].Name);
            }
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Drawing.Font font =
                new System.Drawing.Font((string) this.cboFontName.Properties.Items[this.cboFontName.SelectedIndex], 10f);
            this.fontlistView.Font = font;
            if (this.m_CanDo)
            {
                stdole.IFontDisp disp = this.m_CharacterMarkerSymbol.Font;
                disp.Name = (string) this.cboFontName.Properties.Items[this.cboFontName.SelectedIndex];
                this.m_CharacterMarkerSymbol.Font = disp;
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numUpDownSize.Value = (decimal) ((((double) this.numUpDownSize.Value)/this.m_unit)*newunit);
            this.numUpDownXOffset.Value = (decimal) ((((double) this.numUpDownXOffset.Value)/this.m_unit)*newunit);
            this.numUpDownYOffset.Value = (decimal) ((((double) this.numUpDownYOffset.Value)/this.m_unit)*newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void CharacterMarkerControl_Load(object sender, EventArgs e)
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_CharacterMarkerSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_CharacterMarkerSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void fontlistView_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void fontlistView_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void fontlistView_MouseDown(object sender, MouseEventArgs e)
        {
            bool flag = this.fontlistView.Focus();
            base.ActiveControl = this.fontlistView;
        }

        private void fontlistView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.numUpDownNuicode.Value = this.fontlistView.SelectedIndex + 32;
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

        public void InitControl()
        {
            this.m_CanDo = false;
            this.numUpDownAngle.Value = (decimal) this.m_CharacterMarkerSymbol.Angle;
            this.numUpDownSize.Value = (decimal) (this.m_CharacterMarkerSymbol.Size*this.m_unit);
            this.numUpDownXOffset.Value = (decimal) (this.m_CharacterMarkerSymbol.XOffset*this.m_unit);
            this.numUpDownYOffset.Value = (decimal) (this.m_CharacterMarkerSymbol.YOffset*this.m_unit);
            this.numUpDownNuicode.Value = this.m_CharacterMarkerSymbol.CharacterIndex;
            this.SetColorEdit(this.colorEdit1, this.m_CharacterMarkerSymbol.Color);
            this.fontlistView.SelectedIndex = this.m_CharacterMarkerSymbol.CharacterIndex - 32;
            this.fontlistView.MakeSelectItemVisible();
            for (int i = 0; i < this.cboFontName.Properties.Items.Count; i++)
            {
                if (this.m_CharacterMarkerSymbol.Font.Name == this.cboFontName.Properties.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
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

        private void numUpDownAngle_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void numUpDownAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numUpDownAngle.Value == 0M) && !IsNmuber(this.numUpDownAngle.Text))
                {
                    this.numUpDownAngle.ForeColor = Color.Red;
                }
                else
                {
                    this.numUpDownAngle.ForeColor = SystemColors.WindowText;
                    this.m_CharacterMarkerSymbol.Angle = (double) this.numUpDownAngle.Value;
                    this.refresh(e);
                }
            }
        }

        private void numUpDownNuicode_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void numUpDownNuicode_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numUpDownNuicode.Value == 0M) && !IsNmuber(this.numUpDownNuicode.Text))
                {
                    this.numUpDownNuicode.ForeColor = Color.Red;
                }
                else
                {
                    this.numUpDownNuicode.ForeColor = SystemColors.WindowText;
                    this.m_CharacterMarkerSymbol.CharacterIndex = (int) this.numUpDownNuicode.Value;
                    this.m_CanDo = false;
                    this.fontlistView.SelectedIndex = this.m_CharacterMarkerSymbol.CharacterIndex - 32;
                    this.fontlistView.MakeSelectItemVisible();
                    this.m_CanDo = true;
                    this.refresh(e);
                }
            }
        }

        private void numUpDownSize_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void numUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numUpDownSize.Value <= 0M)
                {
                    this.numUpDownSize.ForeColor = Color.Red;
                }
                else
                {
                    this.numUpDownSize.ForeColor = SystemColors.WindowText;
                    this.m_CharacterMarkerSymbol.Size = ((double) this.numUpDownSize.Value)/this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numUpDownXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numUpDownXOffset.Value == 0M) && !IsNmuber(this.numUpDownXOffset.Text))
                {
                    this.numUpDownXOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numUpDownXOffset.ForeColor = SystemColors.WindowText;
                    this.m_CharacterMarkerSymbol.XOffset = ((double) this.numUpDownXOffset.Value)/this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numUpDownYOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numUpDownYOffset.Value == 0M) && !IsNmuber(this.numUpDownYOffset.Text))
                {
                    this.numUpDownYOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numUpDownYOffset.ForeColor = SystemColors.WindowText;
                    this.m_CharacterMarkerSymbol.YOffset = ((double) this.numUpDownYOffset.Value)/this.m_unit;
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