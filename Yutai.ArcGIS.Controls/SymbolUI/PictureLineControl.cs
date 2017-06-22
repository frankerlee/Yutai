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
    internal partial class PictureLineControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public IPictureLineSymbol m_PictureLineSymbol;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public PictureLineControl()
        {
            this.InitializeComponent();
        }

        private void btnSelectPicture_Click(object sender, EventArgs e)
        {
            this.LoadPictureFile(e);
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void chkSwapFGBG_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_PictureLineSymbol.SwapForeGroundBackGroundColor = this.chkSwapFGBG.Checked;
                this.refresh(e);
            }
        }

        private void colorEditBackgroundColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor backgroundColor = this.m_PictureLineSymbol.BackgroundColor;
                this.UpdateColorFromColorEdit(this.colorEditBackgroundColor, backgroundColor);
                this.m_PictureLineSymbol.BackgroundColor = backgroundColor;
                this.refresh(e);
            }
        }

        private void colorEditForeColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_PictureLineSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEditForeColor, pColor);
                this.m_PictureLineSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void colorEditTransColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor bitmapTransparencyColor = this.m_PictureLineSymbol.BitmapTransparencyColor;
                this.UpdateColorFromColorEdit(this.colorEditTransColor, bitmapTransparencyColor);
                this.m_PictureLineSymbol.BitmapTransparencyColor = bitmapTransparencyColor;
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
            this.numericUpDownWidth.Value = (decimal) this.m_PictureLineSymbol.Width;
            this.numericUpDownXScale.Value = (decimal) this.m_PictureLineSymbol.XScale;
            this.numericUpDownYScale.Value = (decimal) this.m_PictureLineSymbol.YScale;
            if (this.m_PictureLineSymbol.Picture != null)
            {
                if (((stdole.IPicture) this.m_PictureLineSymbol.Picture).Type == 1)
                {
                    this.chkSwapFGBG.Enabled = true;
                    this.chkSwapFGBG.Checked = this.m_PictureLineSymbol.SwapForeGroundBackGroundColor;
                    this.colorEditForeColor.Enabled = true;
                    this.colorEditTransColor.Enabled = false;
                }
                else
                {
                    this.chkSwapFGBG.Enabled = false;
                    this.colorEditForeColor.Enabled = false;
                    this.colorEditTransColor.Enabled = true;
                }
            }
            if (this.m_PictureLineSymbol.Color == null)
            {
                this.colorEditForeColor.Enabled = false;
                this.chkSwapFGBG.Enabled = false;
            }
            else
            {
                this.chkSwapFGBG.Enabled = true;
                this.chkSwapFGBG.Checked = this.m_PictureLineSymbol.SwapForeGroundBackGroundColor;
                this.SetColorEdit(this.colorEditForeColor, this.m_PictureLineSymbol.Color);
            }
            if (this.m_PictureLineSymbol.BitmapTransparencyColor == null)
            {
                this.colorEditTransColor.Enabled = false;
            }
            else
            {
                this.SetColorEdit(this.colorEditTransColor, this.m_PictureLineSymbol.BitmapTransparencyColor);
            }
            this.SetColorEdit(this.colorEditBackgroundColor, this.m_PictureLineSymbol.BackgroundColor);
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

        private void LoadPictureFile(EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Bitmaps (*.bmp)|*.bmp|Enhanced Metafiles (*.emf)|*.emf",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                esriIPictureType esriIPictureBitmap;
                if (dialog.FilterIndex == 1)
                {
                    esriIPictureBitmap = esriIPictureType.esriIPictureBitmap;
                }
                else
                {
                    esriIPictureBitmap = esriIPictureType.esriIPictureEMF;
                }
                this.m_PictureLineSymbol.CreateLineSymbolFromFile(esriIPictureBitmap, dialog.FileName);
                this.refresh(e);
            }
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
                    this.m_PictureLineSymbol.Width = ((double) this.numericUpDownWidth.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownXScale_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownXScale.Value == 0M)
                {
                    this.numericUpDownXScale.ForeColor = Color.Red;
                }
                else if ((this.numericUpDownXScale.Value == 0M) && !IsNmuber(this.numericUpDownXScale.Text))
                {
                    this.numericUpDownXScale.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownXScale.ForeColor = SystemColors.WindowText;
                    this.m_PictureLineSymbol.XScale = (double) this.numericUpDownXScale.Value;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownYScale_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownYScale.Value == 0M)
                {
                    this.numericUpDownYScale.ForeColor = Color.Red;
                }
                else if ((this.numericUpDownYScale.Value == 0M) && !IsNmuber(this.numericUpDownYScale.Text))
                {
                    this.numericUpDownYScale.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownYScale.ForeColor = SystemColors.WindowText;
                    this.m_PictureLineSymbol.YScale = (double) this.numericUpDownYScale.Value;
                    this.refresh(e);
                }
            }
        }

        private void PictureLineControl_Load(object sender, EventArgs e)
        {
            if (this.m_PictureLineSymbol.Picture == null)
            {
                this.LoadPictureFile(e);
            }
            this.InitControl();
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

