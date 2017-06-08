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
    internal class TextGeneralControl : UserControl, CommonInterface
    {
        private ComboBoxEdit cboFontName;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkStrike;
        private CheckBox chkUnderline;
        private CheckBox chkVert;
        private ColorEdit colorEdit1;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private bool m_CanDo = false;
        public ITextSymbol m_pTextSymbol = null;
        public double m_unit = 1.0;
        private SpinEdit numUpDownSize;
        private RadioGroup radioGroupHor;
        private RadioGroup radioGroupVert;
        private SpinEdit spinEditAngle;
        private SpinEdit spinEditXOffset;
        private SpinEdit spinEditYOffset;

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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextGeneralControl));
            this.colorEdit1 = new ColorEdit();
            this.label8 = new Label();
            this.label6 = new Label();
            this.label1 = new Label();
            this.numUpDownSize = new SpinEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.spinEditXOffset = new SpinEdit();
            this.spinEditYOffset = new SpinEdit();
            this.spinEditAngle = new SpinEdit();
            this.label5 = new Label();
            this.groupBox1 = new GroupBox();
            this.radioGroupVert = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.radioGroupHor = new RadioGroup();
            this.imageList1 = new ImageList(this.components);
            this.chkUnderline = new CheckBox();
            this.chkItalic = new CheckBox();
            this.chkBold = new CheckBox();
            this.chkStrike = new CheckBox();
            this.cboFontName = new ComboBoxEdit();
            this.chkVert = new CheckBox();
            this.colorEdit1.Properties.BeginInit();
            this.numUpDownSize.Properties.BeginInit();
            this.spinEditXOffset.Properties.BeginInit();
            this.spinEditYOffset.Properties.BeginInit();
            this.spinEditAngle.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioGroupVert.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.radioGroupHor.Properties.BeginInit();
            this.cboFontName.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x30, 0x48);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 0x4c;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(8, 0x4b);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 12);
            this.label8.TabIndex = 0x4b;
            this.label8.Text = "颜色";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xd8, 11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 0x4a;
            this.label6.Text = "大小";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0x48;
            this.label1.Text = "字体";
            int[] bits = new int[4];
            this.numUpDownSize.EditValue = new decimal(bits);
            this.numUpDownSize.Location = new Point(0x100, 8);
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numUpDownSize.Properties.MaxValue = new decimal(bits);
            this.numUpDownSize.Size = new Size(0x40, 0x15);
            this.numUpDownSize.TabIndex = 80;
            this.numUpDownSize.EditValueChanged += new EventHandler(this.numUpDownSize_EditValueChanged);
            this.numUpDownSize.TextChanged += new EventHandler(this.numUpDownSize_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x2c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x52;
            this.label2.Text = "样式";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x90, 0x4b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 0x54;
            this.label3.Text = "X偏移";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x90, 0x6a);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 0x55;
            this.label4.Text = "Y偏移";
            bits = new int[4];
            this.spinEditXOffset.EditValue = new decimal(bits);
            this.spinEditXOffset.Location = new Point(200, 0x48);
            this.spinEditXOffset.Name = "spinEditXOffset";
            this.spinEditXOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEditXOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.spinEditXOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.spinEditXOffset.Properties.MaxValue = new decimal(bits);
            this.spinEditXOffset.Size = new Size(0x40, 0x15);
            this.spinEditXOffset.TabIndex = 0x56;
            this.spinEditXOffset.TextChanged += new EventHandler(this.spinEditXOffset_TextChanged);
            bits = new int[4];
            this.spinEditYOffset.EditValue = new decimal(bits);
            this.spinEditYOffset.Location = new Point(200, 0x66);
            this.spinEditYOffset.Name = "spinEditYOffset";
            this.spinEditYOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEditYOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.spinEditYOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.spinEditYOffset.Properties.MaxValue = new decimal(bits);
            this.spinEditYOffset.Size = new Size(0x40, 0x15);
            this.spinEditYOffset.TabIndex = 0x57;
            this.spinEditYOffset.TextChanged += new EventHandler(this.spinEditYOffset_TextChanged);
            bits = new int[4];
            this.spinEditAngle.EditValue = new decimal(bits);
            this.spinEditAngle.Location = new Point(0x30, 0x66);
            this.spinEditAngle.Name = "spinEditAngle";
            this.spinEditAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEditAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.spinEditAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 360;
            this.spinEditAngle.Properties.MaxValue = new decimal(bits);
            this.spinEditAngle.Size = new Size(0x40, 0x15);
            this.spinEditAngle.TabIndex = 0x59;
            this.spinEditAngle.TextChanged += new EventHandler(this.spinEditAngle_TextChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x6a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 0x58;
            this.label5.Text = "角度";
            this.groupBox1.Controls.Add(this.radioGroupVert);
            this.groupBox1.Location = new Point(8, 0x88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x88, 0x70);
            this.groupBox1.TabIndex = 0x5b;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "垂直对齐";
            this.radioGroupVert.Location = new Point(0x10, 0x10);
            this.radioGroupVert.Name = "radioGroupVert";
            this.radioGroupVert.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.radioGroupVert.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupVert.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupVert.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "顶部对齐"), new RadioGroupItem(null, "居中"), new RadioGroupItem(null, "基准线对齐"), new RadioGroupItem(null, "底部对齐") });
            this.radioGroupVert.Size = new Size(0x70, 0x58);
            this.radioGroupVert.TabIndex = 0x5b;
            this.radioGroupVert.SelectedIndexChanged += new EventHandler(this.radioGroupVert_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.radioGroupHor);
            this.groupBox2.Location = new Point(0xb0, 0x88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(120, 0x70);
            this.groupBox2.TabIndex = 0x5c;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "水平对齐";
            this.radioGroupHor.Location = new Point(0x10, 0x10);
            this.radioGroupHor.Name = "radioGroupHor";
            this.radioGroupHor.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.radioGroupHor.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupHor.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupHor.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "左对齐"), new RadioGroupItem(null, "居中"), new RadioGroupItem(null, "右对齐"), new RadioGroupItem(null, "两端") });
            this.radioGroupHor.Size = new Size(0x48, 0x58);
            this.radioGroupHor.TabIndex = 0x5b;
            this.radioGroupHor.SelectedIndexChanged += new EventHandler(this.radioGroupHor_SelectedIndexChanged);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList1;
            this.chkUnderline.Location = new Point(0x70, 0x27);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(0x1c, 0x18);
            this.chkUnderline.TabIndex = 0x5f;
            this.chkUnderline.Click += new EventHandler(this.chkUnderline_Click);
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList1;
            this.chkItalic.Location = new Point(80, 0x27);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(0x1c, 0x18);
            this.chkItalic.TabIndex = 0x5e;
            this.chkItalic.Click += new EventHandler(this.chkItalic_Click);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(0x30, 0x27);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 0x5d;
            this.chkBold.Click += new EventHandler(this.chkBold_Click);
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.chkStrike.Appearance = Appearance.Button;
            this.chkStrike.ImageIndex = 3;
            this.chkStrike.ImageList = this.imageList1;
            this.chkStrike.Location = new Point(0x90, 0x27);
            this.chkStrike.Name = "chkStrike";
            this.chkStrike.Size = new Size(0x1c, 0x18);
            this.chkStrike.TabIndex = 0x60;
            this.chkStrike.Click += new EventHandler(this.chkStrike_Click);
            this.cboFontName.EditValue = "";
            this.cboFontName.Location = new Point(0x30, 8);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontName.Size = new Size(0x98, 0x15);
            this.cboFontName.TabIndex = 0x61;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.chkVert.AutoSize = true;
            this.chkVert.Location = new Point(0xda, 0x27);
            this.chkVert.Name = "chkVert";
            this.chkVert.Size = new Size(0x48, 0x10);
            this.chkVert.TabIndex = 0x62;
            this.chkVert.Text = "竖向文本";
            this.chkVert.UseVisualStyleBackColor = true;
            this.chkVert.CheckedChanged += new EventHandler(this.chkVert_CheckedChanged);
            base.Controls.Add(this.chkVert);
            base.Controls.Add(this.cboFontName);
            base.Controls.Add(this.chkStrike);
            base.Controls.Add(this.chkItalic);
            base.Controls.Add(this.chkBold);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.spinEditAngle);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.spinEditYOffset);
            base.Controls.Add(this.spinEditXOffset);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.numUpDownSize);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.chkUnderline);
            base.Name = "TextGeneralControl";
            base.Size = new Size(0x160, 0x108);
            base.Load += new EventHandler(this.TextGeneralControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.numUpDownSize.Properties.EndInit();
            this.spinEditXOffset.Properties.EndInit();
            this.spinEditYOffset.Properties.EndInit();
            this.spinEditAngle.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioGroupVert.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.radioGroupHor.Properties.EndInit();
            this.cboFontName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

