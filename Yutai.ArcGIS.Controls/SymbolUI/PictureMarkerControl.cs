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
    internal class PictureMarkerControl : UserControl, CommonInterface
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
        private Label label7;
        private Label lblPathName;
        private bool m_CanDo = true;
        public IPictureMarkerSymbol m_PictureMarkerSymbol;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownAngle;
        private SpinEdit numericUpDownSize;
        private SpinEdit numericUpDownXOffset;
        private SpinEdit numericUpDownYOffset;

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
            this.label7 = new Label();
            this.numericUpDownAngle = new SpinEdit();
            this.numericUpDownSize = new SpinEdit();
            this.numericUpDownYOffset = new SpinEdit();
            this.numericUpDownXOffset = new SpinEdit();
            this.chkSwapFGBG = new CheckEdit();
            this.btnSelectPicture = new SimpleButton();
            this.colorEditForeColor.Properties.BeginInit();
            this.colorEditBackgroundColor.Properties.BeginInit();
            this.colorEditTransColor.Properties.BeginInit();
            this.numericUpDownAngle.Properties.BeginInit();
            this.numericUpDownSize.Properties.BeginInit();
            this.numericUpDownYOffset.Properties.BeginInit();
            this.numericUpDownXOffset.Properties.BeginInit();
            this.chkSwapFGBG.Properties.BeginInit();
            base.SuspendLayout();
            this.lblPathName.Location = new Point(0x60, 0x10);
            this.lblPathName.Name = "lblPathName";
            this.lblPathName.Size = new Size(280, 0x18);
            this.lblPathName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x56);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "角度";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x76);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "X偏移";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x94);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y偏移";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(160, 0x56);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2a, 0x11);
            this.label4.TabIndex = 8;
            this.label4.Text = "前景色";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(160, 0x76);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2a, 0x11);
            this.label5.TabIndex = 9;
            this.label5.Text = "背景色";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(160, 0x94);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x2a, 0x11);
            this.label6.TabIndex = 10;
            this.label6.Text = "透明色";
            this.colorEditForeColor.EditValue = Color.Empty;
            this.colorEditForeColor.Location = new Point(0xd8, 80);
            this.colorEditForeColor.Name = "colorEditForeColor";
            this.colorEditForeColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditForeColor.Size = new Size(0x30, 0x17);
            this.colorEditForeColor.TabIndex = 11;
            this.colorEditForeColor.EditValueChanged += new EventHandler(this.colorEditForeColor_EditValueChanged);
            this.colorEditBackgroundColor.EditValue = Color.Empty;
            this.colorEditBackgroundColor.Location = new Point(0xd8, 0x70);
            this.colorEditBackgroundColor.Name = "colorEditBackgroundColor";
            this.colorEditBackgroundColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditBackgroundColor.Size = new Size(0x30, 0x17);
            this.colorEditBackgroundColor.TabIndex = 12;
            this.colorEditBackgroundColor.EditValueChanged += new EventHandler(this.colorEditBackgroundColor_EditValueChanged);
            this.colorEditTransColor.EditValue = Color.Empty;
            this.colorEditTransColor.Location = new Point(0xd8, 0x90);
            this.colorEditTransColor.Name = "colorEditTransColor";
            this.colorEditTransColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditTransColor.Size = new Size(0x30, 0x17);
            this.colorEditTransColor.TabIndex = 13;
            this.colorEditTransColor.EditValueChanged += new EventHandler(this.colorEditTransColor_EditValueChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x10, 0x35);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 0x11);
            this.label7.TabIndex = 15;
            this.label7.Text = "大小";
            int[] bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(0x40, 80);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownAngle.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownAngle.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Properties.UseCtrlIncrement = false;
            this.numericUpDownAngle.Size = new Size(0x40, 0x17);
            this.numericUpDownAngle.TabIndex = 0x43;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            bits = new int[4];
            this.numericUpDownSize.EditValue = new decimal(bits);
            this.numericUpDownSize.Location = new Point(0x40, 0x30);
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
            this.numericUpDownSize.Size = new Size(0x40, 0x17);
            this.numericUpDownSize.TabIndex = 0x42;
            this.numericUpDownSize.TextChanged += new EventHandler(this.numericUpDownSize_ValueChanged);
            bits = new int[4];
            this.numericUpDownYOffset.EditValue = new decimal(bits);
            this.numericUpDownYOffset.Location = new Point(0x40, 0x90);
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
            this.numericUpDownYOffset.Size = new Size(0x40, 0x17);
            this.numericUpDownYOffset.TabIndex = 0x41;
            this.numericUpDownYOffset.TextChanged += new EventHandler(this.numericUpDownYOffset_ValueChanged);
            bits = new int[4];
            this.numericUpDownXOffset.EditValue = new decimal(bits);
            this.numericUpDownXOffset.Location = new Point(0x40, 0x70);
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
            this.numericUpDownXOffset.Size = new Size(0x40, 0x17);
            this.numericUpDownXOffset.TabIndex = 0x40;
            this.numericUpDownXOffset.TextChanged += new EventHandler(this.numericUpDownXOffset_ValueChanged);
            this.chkSwapFGBG.Location = new Point(0x10, 0xb0);
            this.chkSwapFGBG.Name = "chkSwapFGBG";
            this.chkSwapFGBG.Properties.Caption = "交换前后景色";
            this.chkSwapFGBG.Size = new Size(0x80, 0x13);
            this.chkSwapFGBG.TabIndex = 0x53;
            this.chkSwapFGBG.CheckedChanged += new EventHandler(this.chkSwapFGBG_CheckedChanged);
            this.btnSelectPicture.Location = new Point(0x10, 0x10);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(0x40, 0x18);
            this.btnSelectPicture.TabIndex = 0x54;
            this.btnSelectPicture.Text = "图片...";
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.chkSwapFGBG);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.numericUpDownSize);
            base.Controls.Add(this.numericUpDownYOffset);
            base.Controls.Add(this.numericUpDownXOffset);
            base.Controls.Add(this.label7);
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
            base.Name = "PictureMarkerControl";
            base.Size = new Size(0x178, 240);
            base.Load += new EventHandler(this.PictureMarkerControl_Load);
            this.colorEditForeColor.Properties.EndInit();
            this.colorEditBackgroundColor.Properties.EndInit();
            this.colorEditTransColor.Properties.EndInit();
            this.numericUpDownAngle.Properties.EndInit();
            this.numericUpDownSize.Properties.EndInit();
            this.numericUpDownYOffset.Properties.EndInit();
            this.numericUpDownXOffset.Properties.EndInit();
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

