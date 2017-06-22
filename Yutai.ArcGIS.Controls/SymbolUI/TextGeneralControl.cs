using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class TextGeneralControl : UserControl, CommonInterface
    {
        private bool m_CanDo = false;
        public ITextSymbol m_pTextSymbol = null;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public TextGeneralControl()
        {
            this.InitializeComponent();
            this.cboFontName.Properties.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Properties.Items.Add(fonts.Families[i].Name);
            }
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                stdole.IFontDisp font = this.m_pTextSymbol.Font;
                font.Name = (string) this.cboFontName.Properties.Items[this.cboFontName.SelectedIndex];
                this.m_pTextSymbol.Font = font;
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.spinEditXOffset.Value = (decimal) ((((double) this.spinEditXOffset.Value) / this.m_unit) * newunit);
            this.spinEditYOffset.Value = (decimal) ((((double) this.spinEditYOffset.Value) / this.m_unit) * newunit);
            this.numUpDownSize.Value = (decimal) ((((double) this.numUpDownSize.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.m_pTextSymbol.Font;
            font.Bold = this.chkBold.Checked;
            this.m_pTextSymbol.Font = font;
            this.refresh(e);
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.m_pTextSymbol.Font;
            font.Italic = this.chkItalic.Checked;
            this.m_pTextSymbol.Font = font;
            this.refresh(e);
        }

        private void chkStrike_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.m_pTextSymbol.Font;
            font.Strikethrough = this.chkStrike.Checked;
            this.m_pTextSymbol.Font = font;
            this.refresh(e);
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.m_pTextSymbol.Font;
            font.Underline = this.chkUnderline.Checked;
            this.m_pTextSymbol.Font = font;
            this.refresh(e);
        }

        private void chkVert_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextSymbol as ICharacterOrientation).CJKCharactersRotation = this.chkVert.Checked;
                this.refresh(e);
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_pTextSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_pTextSymbol.Color = pColor;
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
            if (this.m_pTextSymbol == null)
            {
                this.m_pTextSymbol = new TextSymbolClass();
            }
            this.SetColorEdit(this.colorEdit1, this.m_pTextSymbol.Color);
            this.numUpDownSize.Value = (decimal) this.m_pTextSymbol.Size;
            this.spinEditXOffset.Value = (decimal) ((this.m_pTextSymbol as ISimpleTextSymbol).XOffset * this.m_unit);
            this.spinEditYOffset.Value = (decimal) ((this.m_pTextSymbol as ISimpleTextSymbol).YOffset * this.m_unit);
            this.spinEditAngle.Value = (decimal) (this.m_pTextSymbol as ISimpleTextSymbol).Angle;
            this.radioGroupHor.SelectedIndex = (int) this.m_pTextSymbol.HorizontalAlignment;
            this.radioGroupVert.SelectedIndex = (int) this.m_pTextSymbol.VerticalAlignment;
            this.chkVert.Checked = (this.m_pTextSymbol as ICharacterOrientation).CJKCharactersRotation;
            stdole.IFontDisp font = this.m_pTextSymbol.Font;
            this.chkBold.Checked = font.Bold;
            this.chkItalic.Checked = font.Italic;
            this.chkUnderline.Checked = font.Underline;
            this.chkStrike.Checked = font.Strikethrough;
            for (int i = 0; i < this.cboFontName.Properties.Items.Count; i++)
            {
                if (this.m_pTextSymbol.Font.Name == this.cboFontName.Properties.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.m_CanDo = true;
        }

 private void numUpDownSize_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void numUpDownSize_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numUpDownSize.Value <= 0M)
                {
                    this.numUpDownSize.Value = (decimal) (this.m_pTextSymbol.Size * this.m_unit);
                }
                else
                {
                    this.m_pTextSymbol.Size = ((double) this.numUpDownSize.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void radioGroupHor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pTextSymbol.HorizontalAlignment = (esriTextHorizontalAlignment) this.radioGroupHor.SelectedIndex;
                this.refresh(e);
            }
        }

        private void radioGroupVert_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pTextSymbol.VerticalAlignment = (esriTextVerticalAlignment) this.radioGroupVert.SelectedIndex;
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

        private void spinEditAngle_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pTextSymbol.Angle = (double) this.spinEditAngle.Value;
                this.refresh(e);
            }
        }

        private void spinEditXOffset_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.spinEditAngle.Value < 0M)
                {
                    this.spinEditXOffset.Value = (decimal) ((this.m_pTextSymbol as ISimpleTextSymbol).XOffset * this.m_unit);
                }
                else
                {
                    (this.m_pTextSymbol as ISimpleTextSymbol).XOffset = ((double) this.spinEditXOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void spinEditYOffset_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.spinEditAngle.Value < 0M)
                {
                    this.spinEditYOffset.Value = (decimal) ((this.m_pTextSymbol as ISimpleTextSymbol).YOffset * this.m_unit);
                }
                else
                {
                    (this.m_pTextSymbol as ISimpleTextSymbol).YOffset = ((double) this.spinEditYOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void TextGeneralControl_Load(object sender, EventArgs e)
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

