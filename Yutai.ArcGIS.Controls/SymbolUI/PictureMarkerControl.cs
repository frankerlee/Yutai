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
    internal partial class PictureMarkerControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public IPictureMarkerSymbol m_PictureMarkerSymbol;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public PictureMarkerControl()
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
            this.numericUpDownSize.Value = (decimal) ((((double) this.numericUpDownSize.Value) / this.m_unit) * newunit);
            this.numericUpDownXOffset.Value = (decimal) ((((double) this.numericUpDownXOffset.Value) / this.m_unit) * newunit);
            this.numericUpDownYOffset.Value = (decimal) ((((double) this.numericUpDownYOffset.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void chkSwapFGBG_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_PictureMarkerSymbol.SwapForeGroundBackGroundColor = this.chkSwapFGBG.Checked;
                this.refresh(e);
            }
        }

        private void colorEditBackgroundColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor backgroundColor = this.m_PictureMarkerSymbol.BackgroundColor;
                this.UpdateColorFromColorEdit(this.colorEditBackgroundColor, backgroundColor);
                this.m_PictureMarkerSymbol.BackgroundColor = backgroundColor;
                this.refresh(e);
            }
        }

        private void colorEditForeColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_PictureMarkerSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEditForeColor, pColor);
                this.m_PictureMarkerSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void colorEditTransColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor bitmapTransparencyColor = this.m_PictureMarkerSymbol.BitmapTransparencyColor;
                this.UpdateColorFromColorEdit(this.colorEditTransColor, bitmapTransparencyColor);
                this.m_PictureMarkerSymbol.BitmapTransparencyColor = bitmapTransparencyColor;
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
            this.numericUpDownAngle.Value = (decimal) this.m_PictureMarkerSymbol.Angle;
            this.numericUpDownSize.Value = (decimal) (this.m_PictureMarkerSymbol.Size * this.m_unit);
            this.numericUpDownXOffset.Value = (decimal) (this.m_PictureMarkerSymbol.XOffset * this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_PictureMarkerSymbol.YOffset * this.m_unit);
            if (this.m_PictureMarkerSymbol.Picture != null)
            {
                if (((stdole.IPicture) this.m_PictureMarkerSymbol.Picture).Type == 1)
                {
                    this.chkSwapFGBG.Enabled = true;
                    this.chkSwapFGBG.Checked = this.m_PictureMarkerSymbol.SwapForeGroundBackGroundColor;
                    this.colorEditForeColor.Enabled = true;
                    this.colorEditTransColor.Enabled = false;
                }
                else
                {
                    this.chkSwapFGBG.Enabled = false;
                    this.chkSwapFGBG.Checked = this.m_PictureMarkerSymbol.SwapForeGroundBackGroundColor;
                    this.colorEditForeColor.Enabled = false;
                    this.colorEditTransColor.Enabled = true;
                }
            }
            if (this.m_PictureMarkerSymbol.Color == null)
            {
                this.colorEditForeColor.Enabled = false;
                this.chkSwapFGBG.Enabled = false;
            }
            else
            {
                this.chkSwapFGBG.Enabled = false;
                this.chkSwapFGBG.Checked = this.m_PictureMarkerSymbol.SwapForeGroundBackGroundColor;
                this.SetColorEdit(this.colorEditForeColor, this.m_PictureMarkerSymbol.Color);
            }
            if (this.m_PictureMarkerSymbol.BitmapTransparencyColor == null)
            {
                this.colorEditTransColor.Enabled = false;
            }
            else
            {
                this.SetColorEdit(this.colorEditTransColor, this.m_PictureMarkerSymbol.BitmapTransparencyColor);
            }
            this.SetColorEdit(this.colorEditBackgroundColor, this.m_PictureMarkerSymbol.BackgroundColor);
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
                this.m_PictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureBitmap, dialog.FileName);
                this.refresh(e);
            }
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
                    this.m_PictureMarkerSymbol.Angle = (double) this.numericUpDownAngle.Value;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownSize.Value == 0M) && !IsNmuber(this.numericUpDownSize.Text))
                {
                    this.numericUpDownSize.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownSize.ForeColor = SystemColors.WindowText;
                    this.m_PictureMarkerSymbol.Size = ((double) this.numericUpDownSize.Value) / this.m_unit;
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
                    this.m_PictureMarkerSymbol.XOffset = ((double) this.numericUpDownXOffset.Value) / this.m_unit;
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
                    this.m_PictureMarkerSymbol.YOffset = ((double) this.numericUpDownYOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void PictureMarkerControl_Load(object sender, EventArgs e)
        {
            if (this.m_PictureMarkerSymbol.Picture == null)
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

