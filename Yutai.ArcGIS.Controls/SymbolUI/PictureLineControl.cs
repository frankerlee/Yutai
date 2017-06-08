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
    internal class PictureLineControl : UserControl, CommonInterface
    {
        private SimpleButton btnSelectPicture;
        private CheckEdit chkSwapFGBG;
        private ColorEdit colorEditBackgroundColor;
        private ColorEdit colorEditForeColor;
        private ColorEdit colorEditTransColor;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblPathName;
        private bool m_CanDo = true;
        public IPictureLineSymbol m_PictureLineSymbol;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownWidth;
        private SpinEdit numericUpDownXScale;
        private SpinEdit numericUpDownYScale;

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

        private void InitializeComponent()
        {
            this.lblPathName = new Label();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.colorEditForeColor = new ColorEdit();
            this.colorEditBackgroundColor = new ColorEdit();
            this.colorEditTransColor = new ColorEdit();
            this.numericUpDownYScale = new SpinEdit();
            this.numericUpDownXScale = new SpinEdit();
            this.numericUpDownWidth = new SpinEdit();
            this.chkSwapFGBG = new CheckEdit();
            this.btnSelectPicture = new SimpleButton();
            this.colorEditForeColor.Properties.BeginInit();
            this.colorEditBackgroundColor.Properties.BeginInit();
            this.colorEditTransColor.Properties.BeginInit();
            this.numericUpDownYScale.Properties.BeginInit();
            this.numericUpDownXScale.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.chkSwapFGBG.Properties.BeginInit();
            base.SuspendLayout();
            this.lblPathName.Location = new Point(0x60, 0x10);
            this.lblPathName.Name = "lblPathName";
            this.lblPathName.Size = new Size(280, 0x18);
            this.lblPathName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x4c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "宽度";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x6b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "X方向比";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x8a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x30, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y方向比";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(160, 0x4c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2a, 0x11);
            this.label4.TabIndex = 8;
            this.label4.Text = "前景色";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(160, 0x6b);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2a, 0x11);
            this.label5.TabIndex = 9;
            this.label5.Text = "背景色";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(160, 0x8a);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x2a, 0x11);
            this.label6.TabIndex = 10;
            this.label6.Text = "透明色";
            this.colorEditForeColor.EditValue = Color.Empty;
            this.colorEditForeColor.Location = new Point(0xd8, 0x48);
            this.colorEditForeColor.Name = "colorEditForeColor";
            this.colorEditForeColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditForeColor.Size = new Size(0x30, 0x17);
            this.colorEditForeColor.TabIndex = 11;
            this.colorEditForeColor.EditValueChanged += new EventHandler(this.colorEditForeColor_EditValueChanged);
            this.colorEditBackgroundColor.EditValue = Color.Empty;
            this.colorEditBackgroundColor.Location = new Point(0xd8, 0x68);
            this.colorEditBackgroundColor.Name = "colorEditBackgroundColor";
            this.colorEditBackgroundColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditBackgroundColor.Size = new Size(0x30, 0x17);
            this.colorEditBackgroundColor.TabIndex = 12;
            this.colorEditBackgroundColor.EditValueChanged += new EventHandler(this.colorEditBackgroundColor_EditValueChanged);
            this.colorEditTransColor.EditValue = Color.Empty;
            this.colorEditTransColor.Location = new Point(0xd8, 0x88);
            this.colorEditTransColor.Name = "colorEditTransColor";
            this.colorEditTransColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditTransColor.Size = new Size(0x30, 0x17);
            this.colorEditTransColor.TabIndex = 13;
            this.colorEditTransColor.EditValueChanged += new EventHandler(this.colorEditTransColor_EditValueChanged);
            int[] bits = new int[4];
            this.numericUpDownYScale.EditValue = new decimal(bits);
            this.numericUpDownYScale.Location = new Point(0x48, 0x88);
            this.numericUpDownYScale.Name = "numericUpDownYScale";
            this.numericUpDownYScale.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownYScale.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownYScale.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownYScale.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownYScale.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownYScale.Properties.MaxValue = new decimal(bits);
            this.numericUpDownYScale.Properties.UseCtrlIncrement = false;
            this.numericUpDownYScale.Size = new Size(0x40, 0x17);
            this.numericUpDownYScale.TabIndex = 0x4f;
            this.numericUpDownYScale.TextChanged += new EventHandler(this.numericUpDownYScale_ValueChanged);
            bits = new int[4];
            this.numericUpDownXScale.EditValue = new decimal(bits);
            this.numericUpDownXScale.Location = new Point(0x48, 0x68);
            this.numericUpDownXScale.Name = "numericUpDownXScale";
            this.numericUpDownXScale.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownXScale.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownXScale.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownXScale.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownXScale.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownXScale.Properties.MaxValue = new decimal(bits);
            this.numericUpDownXScale.Properties.UseCtrlIncrement = false;
            this.numericUpDownXScale.Size = new Size(0x40, 0x17);
            this.numericUpDownXScale.TabIndex = 0x4e;
            this.numericUpDownXScale.TextChanged += new EventHandler(this.numericUpDownXScale_ValueChanged);
            bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(0x48, 0x48);
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
            this.numericUpDownWidth.Size = new Size(0x40, 0x17);
            this.numericUpDownWidth.TabIndex = 80;
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            this.chkSwapFGBG.Location = new Point(0x10, 0xa8);
            this.chkSwapFGBG.Name = "chkSwapFGBG";
            this.chkSwapFGBG.Properties.Caption = "交换前后景色";
            this.chkSwapFGBG.Size = new Size(0x80, 0x13);
            this.chkSwapFGBG.TabIndex = 0x51;
            this.chkSwapFGBG.CheckedChanged += new EventHandler(this.chkSwapFGBG_CheckedChanged);
            this.btnSelectPicture.Location = new Point(0x10, 0x10);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(0x40, 0x18);
            this.btnSelectPicture.TabIndex = 0x55;
            this.btnSelectPicture.Text = "图片...";
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.chkSwapFGBG);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.numericUpDownYScale);
            base.Controls.Add(this.numericUpDownXScale);
            base.Controls.Add(this.colorEditTransColor);
            base.Controls.Add(this.colorEditBackgroundColor);
            base.Controls.Add(this.colorEditForeColor);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblPathName);
            base.Name = "PictureLineControl";
            base.Size = new Size(0x198, 280);
            base.Load += new EventHandler(this.PictureLineControl_Load);
            this.colorEditForeColor.Properties.EndInit();
            this.colorEditBackgroundColor.Properties.EndInit();
            this.colorEditTransColor.Properties.EndInit();
            this.numericUpDownYScale.Properties.EndInit();
            this.numericUpDownXScale.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.chkSwapFGBG.Properties.EndInit();
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

