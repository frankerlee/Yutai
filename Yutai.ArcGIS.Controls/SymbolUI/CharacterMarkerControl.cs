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
    internal class CharacterMarkerControl : UserControl, CommonInterface
    {
        private ComboBoxEdit cboFontName;
        private ColorEdit colorEdit1;
        private Container components;
        private FontListView fontlistView;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private bool m_CanDo;
        public ICharacterMarkerSymbol m_CharacterMarkerSymbol;
        public double m_unit;
        private SpinEdit numUpDownAngle;
        private SpinEdit numUpDownNuicode;
        private SpinEdit numUpDownSize;
        private SpinEdit numUpDownXOffset;
        private SpinEdit numUpDownYOffset;

        public event ValueChangedHandler ValueChanged;

        public CharacterMarkerControl()
        {
            int num;
            this.m_unit = 1.0;
            this.m_CanDo = true;
            this.components = null;
            this.InitializeComponent();
            for (num = 0x20; num < 0x100; num++)
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
            System.Drawing.Font font = new System.Drawing.Font((string) this.cboFontName.Properties.Items[this.cboFontName.SelectedIndex], 10f);
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
            this.numUpDownSize.Value = (decimal) ((((double) this.numUpDownSize.Value) / this.m_unit) * newunit);
            this.numUpDownXOffset.Value = (decimal) ((((double) this.numUpDownXOffset.Value) / this.m_unit) * newunit);
            this.numUpDownYOffset.Value = (decimal) ((((double) this.numUpDownYOffset.Value) / this.m_unit) * newunit);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                this.numUpDownNuicode.Value = this.fontlistView.SelectedIndex + 0x20;
            }
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

        public void InitControl()
        {
            this.m_CanDo = false;
            this.numUpDownAngle.Value = (decimal) this.m_CharacterMarkerSymbol.Angle;
            this.numUpDownSize.Value = (decimal) (this.m_CharacterMarkerSymbol.Size * this.m_unit);
            this.numUpDownXOffset.Value = (decimal) (this.m_CharacterMarkerSymbol.XOffset * this.m_unit);
            this.numUpDownYOffset.Value = (decimal) (this.m_CharacterMarkerSymbol.YOffset * this.m_unit);
            this.numUpDownNuicode.Value = this.m_CharacterMarkerSymbol.CharacterIndex;
            this.SetColorEdit(this.colorEdit1, this.m_CharacterMarkerSymbol.Color);
            this.fontlistView.SelectedIndex = this.m_CharacterMarkerSymbol.CharacterIndex - 0x20;
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label3 = new Label();
            this.label7 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label6 = new Label();
            this.label8 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.fontlistView = new FontListView();
            this.numUpDownXOffset = new SpinEdit();
            this.numUpDownYOffset = new SpinEdit();
            this.numUpDownNuicode = new SpinEdit();
            this.numUpDownSize = new SpinEdit();
            this.numUpDownAngle = new SpinEdit();
            this.cboFontName = new ComboBoxEdit();
            this.colorEdit1.Properties.BeginInit();
            this.numUpDownXOffset.Properties.BeginInit();
            this.numUpDownYOffset.Properties.BeginInit();
            this.numUpDownNuicode.Properties.BeginInit();
            this.numUpDownSize.Properties.BeginInit();
            this.numUpDownAngle.Properties.BeginInit();
            this.cboFontName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "字体";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0xec);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "编码";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(280, 0x23);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 0x11);
            this.label7.TabIndex = 0x26;
            this.label7.Text = "角度";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(280, 0xd6);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 0x2f;
            this.label5.Text = "Y偏移";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(280, 0xb6);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 0x2c;
            this.label4.Text = "X偏移";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(280, 10);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 0x29;
            this.label6.Text = "大小";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(280, 0x62);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 0x11);
            this.label8.TabIndex = 50;
            this.label8.Text = "颜色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(320, 0x60);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0x39;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.fontlistView.AutoScroll = true;
            this.fontlistView.AutoScrollMinSize = new Size(0x10b, 150);
            this.fontlistView.BackColor = SystemColors.ControlLightLight;
            this.fontlistView.ItemSize = new Size(0x19, 0x19);
            this.fontlistView.Location = new Point(8, 0x30);
            this.fontlistView.Name = "fontlistView";
            this.fontlistView.SelectedIndex = -1;
            this.fontlistView.Size = new Size(0x10b, 150);
            this.fontlistView.TabIndex = 0x3a;
            this.fontlistView.TabStop = false;
            this.fontlistView.KeyPress += new KeyPressEventHandler(this.fontlistView_KeyPress);
            this.fontlistView.KeyDown += new KeyEventHandler(this.fontlistView_KeyDown);
            this.fontlistView.MouseDown += new MouseEventHandler(this.fontlistView_MouseDown);
            this.fontlistView.SelectedIndexChanged += new SelectedIndexChangedHandler(this.fontlistView_SelectedIndexChanged);
            int[] bits = new int[4];
            this.numUpDownXOffset.EditValue = new decimal(bits);
            this.numUpDownXOffset.Location = new Point(320, 0xb0);
            this.numUpDownXOffset.Name = "numUpDownXOffset";
            this.numUpDownXOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownXOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numUpDownXOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownXOffset.Properties.EditFormat.FormatString = "0.####";
            this.numUpDownXOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits2 = new int[4];
            bits2[0] = 100;
            this.numUpDownXOffset.Properties.MaxValue = new decimal(bits2);
            int[] bits3 = new int[4];
            bits3[0] = 100;
            bits3[3] = -2147483648;
            this.numUpDownXOffset.Properties.MinValue = new decimal(bits3);
            this.numUpDownXOffset.Properties.UseCtrlIncrement = false;
            this.numUpDownXOffset.Size = new Size(0x48, 0x17);
            this.numUpDownXOffset.TabIndex = 0x3b;
            this.numUpDownXOffset.TextChanged += new EventHandler(this.numUpDownXOffset_ValueChanged);
            int[] bits4 = new int[4];
            this.numUpDownYOffset.EditValue = new decimal(bits4);
            this.numUpDownYOffset.Location = new Point(320, 0xd0);
            this.numUpDownYOffset.Name = "numUpDownYOffset";
            this.numUpDownYOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownYOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numUpDownYOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownYOffset.Properties.EditFormat.FormatString = "0.####";
            this.numUpDownYOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits5 = new int[4];
            bits5[0] = 100;
            this.numUpDownYOffset.Properties.MaxValue = new decimal(bits5);
            int[] bits6 = new int[4];
            bits6[0] = 100;
            bits6[3] = -2147483648;
            this.numUpDownYOffset.Properties.MinValue = new decimal(bits6);
            this.numUpDownYOffset.Properties.UseCtrlIncrement = false;
            this.numUpDownYOffset.Size = new Size(0x48, 0x17);
            this.numUpDownYOffset.TabIndex = 60;
            this.numUpDownYOffset.TextChanged += new EventHandler(this.numUpDownYOffset_ValueChanged);
            int[] bits7 = new int[4];
            bits7[0] = 0x21;
            this.numUpDownNuicode.EditValue = new decimal(bits7);
            this.numUpDownNuicode.Location = new Point(0x30, 0xe8);
            this.numUpDownNuicode.Name = "numUpDownNuicode";
            this.numUpDownNuicode.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits8 = new int[4];
            bits8[0] = 0xff;
            this.numUpDownNuicode.Properties.MaxValue = new decimal(bits8);
            int[] bits9 = new int[4];
            bits9[0] = 0x21;
            this.numUpDownNuicode.Properties.MinValue = new decimal(bits9);
            this.numUpDownNuicode.Properties.UseCtrlIncrement = false;
            this.numUpDownNuicode.Size = new Size(0x48, 0x17);
            this.numUpDownNuicode.TabIndex = 0x3d;
            this.numUpDownNuicode.EditValueChanged += new EventHandler(this.numUpDownNuicode_EditValueChanged);
            this.numUpDownNuicode.TextChanged += new EventHandler(this.numUpDownNuicode_ValueChanged);
            int[] bits10 = new int[4];
            this.numUpDownSize.EditValue = new decimal(bits10);
            this.numUpDownSize.Location = new Point(320, 8);
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownSize.Properties.DisplayFormat.FormatString = "0.####";
            this.numUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownSize.Properties.EditFormat.FormatString = "0.####";
            this.numUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits11 = new int[4];
            bits11[0] = 100;
            this.numUpDownSize.Properties.MaxValue = new decimal(bits11);
            this.numUpDownSize.Properties.UseCtrlIncrement = false;
            this.numUpDownSize.Size = new Size(0x40, 0x17);
            this.numUpDownSize.TabIndex = 0x3e;
            this.numUpDownSize.EditValueChanged += new EventHandler(this.numUpDownSize_EditValueChanged);
            this.numUpDownSize.TextChanged += new EventHandler(this.numUpDownSize_ValueChanged);
            int[] bits12 = new int[4];
            this.numUpDownAngle.EditValue = new decimal(bits12);
            this.numUpDownAngle.Location = new Point(320, 0x20);
            this.numUpDownAngle.Name = "numUpDownAngle";
            this.numUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownAngle.Properties.DisplayFormat.FormatString = "0.####";
            this.numUpDownAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownAngle.Properties.EditFormat.FormatString = "0.####";
            this.numUpDownAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits13 = new int[4];
            bits13[0] = 360;
            this.numUpDownAngle.Properties.MaxValue = new decimal(bits13);
            int[] bits14 = new int[4];
            bits14[0] = 360;
            bits14[3] = -2147483648;
            this.numUpDownAngle.Properties.MinValue = new decimal(bits14);
            this.numUpDownAngle.Properties.UseCtrlIncrement = false;
            this.numUpDownAngle.Size = new Size(0x40, 0x17);
            this.numUpDownAngle.TabIndex = 0x3f;
            this.numUpDownAngle.EditValueChanged += new EventHandler(this.numUpDownAngle_EditValueChanged);
            this.numUpDownAngle.TextChanged += new EventHandler(this.numUpDownAngle_ValueChanged);
            this.cboFontName.EditValue = "";
            this.cboFontName.Location = new Point(0x30, 8);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontName.Size = new Size(0xd8, 0x17);
            this.cboFontName.TabIndex = 0x40;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            base.Controls.Add(this.cboFontName);
            base.Controls.Add(this.numUpDownAngle);
            base.Controls.Add(this.numUpDownSize);
            base.Controls.Add(this.numUpDownNuicode);
            base.Controls.Add(this.numUpDownYOffset);
            base.Controls.Add(this.numUpDownXOffset);
            base.Controls.Add(this.fontlistView);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Name = "CharacterMarkerControl";
            base.Size = new Size(0x198, 280);
            base.Load += new EventHandler(this.CharacterMarkerControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.numUpDownXOffset.Properties.EndInit();
            this.numUpDownYOffset.Properties.EndInit();
            this.numUpDownNuicode.Properties.EndInit();
            this.numUpDownSize.Properties.EndInit();
            this.numUpDownAngle.Properties.EndInit();
            this.cboFontName.Properties.EndInit();
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
                    this.fontlistView.SelectedIndex = this.m_CharacterMarkerSymbol.CharacterIndex - 0x20;
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
                    this.m_CharacterMarkerSymbol.Size = ((double) this.numUpDownSize.Value) / this.m_unit;
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
                    this.m_CharacterMarkerSymbol.XOffset = ((double) this.numUpDownXOffset.Value) / this.m_unit;
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
                    this.m_CharacterMarkerSymbol.YOffset = ((double) this.numUpDownYOffset.Value) / this.m_unit;
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

