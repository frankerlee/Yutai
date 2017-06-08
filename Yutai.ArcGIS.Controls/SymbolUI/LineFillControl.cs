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
    internal class LineFillControl : UserControl, CommonInterface
    {
        private NewSymbolButton btnFillLine;
        private NewSymbolButton btnOutline;
        private ColorEdit colorEdit1;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private bool m_CanDo = true;
        public ILineFillSymbol m_LineFillSymbol;
        public IStyleGallery m_pSG;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownAngle;
        private SpinEdit numericUpDownOffset;
        private SpinEdit numericUpDownSpace;

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
            this.numericUpDownOffset.Value = (decimal) ((((double) this.numericUpDownOffset.Value) / this.m_unit) * newunit);
            this.numericUpDownSpace.Value = (decimal) ((((double) this.numericUpDownSpace.Value) / this.m_unit) * newunit);
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
            this.numericUpDownAngle.Value = (decimal) this.m_LineFillSymbol.Angle;
            this.numericUpDownOffset.Value = (decimal) (this.m_LineFillSymbol.Offset * this.m_unit);
            this.numericUpDownSpace.Value = (decimal) (this.m_LineFillSymbol.Separation * this.m_unit);
            this.SetColorEdit(this.colorEdit1, this.m_LineFillSymbol.Color);
            this.btnFillLine.Style = this.m_LineFillSymbol.LineSymbol;
            this.btnOutline.Style = this.m_LineFillSymbol.Outline;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.label7 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.numericUpDownAngle = new SpinEdit();
            this.numericUpDownOffset = new SpinEdit();
            this.numericUpDownSpace = new SpinEdit();
            this.btnFillLine = new NewSymbolButton();
            this.btnOutline = new NewSymbolButton();
            this.label4 = new Label();
            this.label5 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.numericUpDownAngle.Properties.BeginInit();
            this.numericUpDownOffset.Properties.BeginInit();
            this.numericUpDownSpace.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x18);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 8;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 7;
            this.label1.Text = "颜色";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x10, 0x38);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 0x11);
            this.label7.TabIndex = 0x2c;
            this.label7.Text = "角度";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x58);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 0x2e;
            this.label2.Text = "偏移";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 120);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 0x30;
            this.label3.Text = "间隔";
            int[] bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(0x38, 0x38);
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
            this.numericUpDownAngle.TabIndex = 0x48;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            bits = new int[4];
            this.numericUpDownOffset.EditValue = new decimal(bits);
            this.numericUpDownOffset.Location = new Point(0x38, 0x58);
            this.numericUpDownOffset.Name = "numericUpDownOffset";
            this.numericUpDownOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownOffset.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.numericUpDownOffset.Properties.MinValue = new decimal(bits);
            this.numericUpDownOffset.Properties.UseCtrlIncrement = false;
            this.numericUpDownOffset.Size = new Size(0x40, 0x17);
            this.numericUpDownOffset.TabIndex = 0x49;
            this.numericUpDownOffset.TextChanged += new EventHandler(this.numericUpDownOffset_ValueChanged);
            bits = new int[4];
            this.numericUpDownSpace.EditValue = new decimal(bits);
            this.numericUpDownSpace.Location = new Point(0x38, 120);
            this.numericUpDownSpace.Name = "numericUpDownSpace";
            this.numericUpDownSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownSpace.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownSpace.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownSpace.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownSpace.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownSpace.Properties.MaxValue = new decimal(bits);
            this.numericUpDownSpace.Properties.UseCtrlIncrement = false;
            this.numericUpDownSpace.Size = new Size(0x40, 0x17);
            this.numericUpDownSpace.TabIndex = 0x4a;
            this.numericUpDownSpace.TextChanged += new EventHandler(this.numericUpDownSpace_ValueChanged);
            this.btnFillLine.Location = new Point(0xe0, 0x18);
            this.btnFillLine.Name = "btnFillLine";
            this.btnFillLine.Size = new Size(0x58, 0x20);
            this.btnFillLine.Style = null;
            this.btnFillLine.TabIndex = 0x4b;
            this.btnFillLine.Click += new EventHandler(this.btnFillLine_Click);
            this.btnOutline.Location = new Point(0xe0, 80);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(0x58, 0x20);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 0x4c;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x90, 0x20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x48, 0x11);
            this.label4.TabIndex = 0x4d;
            this.label4.Text = "填充线符号:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x90, 0x58);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x48, 0x11);
            this.label5.TabIndex = 0x4e;
            this.label5.Text = "轮廓线符号:";
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.btnFillLine);
            base.Controls.Add(this.numericUpDownSpace);
            base.Controls.Add(this.numericUpDownOffset);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Name = "LineFillControl";
            base.Size = new Size(0x158, 0xe8);
            base.Load += new EventHandler(this.LineFillControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.numericUpDownAngle.Properties.EndInit();
            this.numericUpDownOffset.Properties.EndInit();
            this.numericUpDownSpace.Properties.EndInit();
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
                    this.m_LineFillSymbol.Offset = ((double) this.numericUpDownOffset.Value) / this.m_unit;
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
                    this.m_LineFillSymbol.Separation = ((double) this.numericUpDownSpace.Value) / this.m_unit;
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

