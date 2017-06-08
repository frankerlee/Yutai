using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class PrincipalDigitsLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private CheckBox chkBold;
        private CheckBox chkIta;
        private CheckBox chkUnderLine;
        private ColorEdit colorEdit1;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private bool m_CanDo = false;
        private IPrincipalDigitsGridLabel m_GridLable = null;
        private bool m_IsPageDirty = false;
        private string m_Title = "主要数字";
        private SpinEdit txtBaseDigitCount;
        private TextEdit txtEastingSuffix;
        private TextEdit txtNorthingSuffix;
        private SpinEdit txtPrincipalDigitCount;
        private TextEdit txtUnitSuffix;

        public event OnValueChangeEventHandler OnValueChange;

        public PrincipalDigitsLabelPropertyPage()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                stdole.IFontDisp smallLabelFont = this.m_GridLable.SmallLabelFont;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    smallLabelFont.Name = this.cboFontName.Text;
                }
                smallLabelFont.Size = (decimal) double.Parse(this.cboFontSize.Text);
                smallLabelFont.Bold = this.chkBold.Checked;
                smallLabelFont.Italic = this.chkIta.Checked;
                smallLabelFont.Underline = this.chkUnderLine.Checked;
                this.m_GridLable.SmallLabelFont = smallLabelFont;
                IColor smallLabelColor = this.m_GridLable.SmallLabelColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, smallLabelColor);
                this.m_GridLable.SmallLabelColor = smallLabelColor;
                this.m_GridLable.BaseDigitCount = (int) this.txtBaseDigitCount.Value;
                this.m_GridLable.PrincipalDigitCount = (int) this.txtPrincipalDigitCount.Value;
                this.m_GridLable.UnitSuffix = this.txtUnitSuffix.Text;
                this.m_GridLable.NorthingSuffix = this.txtNorthingSuffix.Text;
                this.m_GridLable.EastingSuffix = this.txtEastingSuffix.Text;
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkIta_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUnderLine_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
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

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrincipalDigitsLabelPropertyPage));
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.label4 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.chkUnderLine = new CheckBox();
            this.imageList1 = new ImageList(this.components);
            this.chkIta = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.label3 = new Label();
            this.label5 = new Label();
            this.txtPrincipalDigitCount = new SpinEdit();
            this.txtBaseDigitCount = new SpinEdit();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtUnitSuffix = new TextEdit();
            this.txtEastingSuffix = new TextEdit();
            this.txtNorthingSuffix = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.txtPrincipalDigitCount.Properties.BeginInit();
            this.txtBaseDigitCount.Properties.BeginInit();
            this.txtUnitSuffix.Properties.BeginInit();
            this.txtEastingSuffix.Properties.BeginInit();
            this.txtNorthingSuffix.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.chkUnderLine);
            this.groupBox1.Controls.Add(this.chkIta);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x100, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "次要字体";
            this.groupBox2.Controls.Add(this.txtBaseDigitCount);
            this.groupBox2.Controls.Add(this.txtPrincipalDigitCount);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new Point(8, 0x88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xf8, 0x60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数字";
            this.groupBox3.Controls.Add(this.txtNorthingSuffix);
            this.groupBox3.Controls.Add(this.txtEastingSuffix);
            this.groupBox3.Controls.Add(this.txtUnitSuffix);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new Point(0x10, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xf8, 0x68);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "字符串";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x35);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 0x30;
            this.label4.Text = "大小:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x55);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 0x2f;
            this.label2.Text = "颜色:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x1d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0x2e;
            this.label1.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(0x38, 0x35);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x17);
            this.cboFontSize.TabIndex = 0x2d;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x55);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0x2c;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.chkUnderLine.Appearance = Appearance.Button;
            this.chkUnderLine.ImageIndex = 2;
            this.chkUnderLine.ImageList = this.imageList1;
            this.chkUnderLine.Location = new Point(200, 0x35);
            this.chkUnderLine.Name = "chkUnderLine";
            this.chkUnderLine.Size = new Size(0x1c, 0x18);
            this.chkUnderLine.TabIndex = 0x2b;
            this.chkUnderLine.CheckedChanged += new EventHandler(this.chkUnderLine_CheckedChanged);
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.chkIta.Appearance = Appearance.Button;
            this.chkIta.ImageIndex = 1;
            this.chkIta.ImageList = this.imageList1;
            this.chkIta.Location = new Point(0xa8, 0x35);
            this.chkIta.Name = "chkIta";
            this.chkIta.Size = new Size(0x1c, 0x18);
            this.chkIta.TabIndex = 0x2a;
            this.chkIta.CheckedChanged += new EventHandler(this.chkIta_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(0x88, 0x35);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 0x29;
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboFontName.Location = new Point(0x38, 0x1d);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 40;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x18, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4f, 0x11);
            this.label3.TabIndex = 0;
            this.label3.Text = "主要数字位数";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x18, 0x38);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x4f, 0x11);
            this.label5.TabIndex = 1;
            this.label5.Text = "基础数字位数";
            int[] bits = new int[4];
            bits[0] = 2;
            this.txtPrincipalDigitCount.EditValue = new decimal(bits);
            this.txtPrincipalDigitCount.Location = new Point(120, 0x18);
            this.txtPrincipalDigitCount.Name = "txtPrincipalDigitCount";
            this.txtPrincipalDigitCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtPrincipalDigitCount.Properties.UseCtrlIncrement = false;
            this.txtPrincipalDigitCount.Size = new Size(80, 0x17);
            this.txtPrincipalDigitCount.TabIndex = 2;
            this.txtPrincipalDigitCount.EditValueChanged += new EventHandler(this.txtPrincipalDigitCount_EditValueChanged);
            bits = new int[4];
            bits[0] = 4;
            this.txtBaseDigitCount.EditValue = new decimal(bits);
            this.txtBaseDigitCount.Location = new Point(120, 0x38);
            this.txtBaseDigitCount.Name = "txtBaseDigitCount";
            this.txtBaseDigitCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBaseDigitCount.Properties.UseCtrlIncrement = false;
            this.txtBaseDigitCount.Size = new Size(80, 0x17);
            this.txtBaseDigitCount.TabIndex = 3;
            this.txtBaseDigitCount.EditValueChanged += new EventHandler(this.txtBaseDigitCount_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x36, 0x11);
            this.label6.TabIndex = 3;
            this.label6.Text = "东向后缀";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x36, 0x11);
            this.label7.TabIndex = 2;
            this.label7.Text = "单位后缀";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(8, 0x48);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x36, 0x11);
            this.label8.TabIndex = 4;
            this.label8.Text = "北向后缀";
            this.txtUnitSuffix.EditValue = "m.";
            this.txtUnitSuffix.Location = new Point(0x48, 0x10);
            this.txtUnitSuffix.Name = "txtUnitSuffix";
            this.txtUnitSuffix.Size = new Size(0x58, 0x17);
            this.txtUnitSuffix.TabIndex = 5;
            this.txtUnitSuffix.EditValueChanged += new EventHandler(this.txtUnitSuffix_EditValueChanged);
            this.txtEastingSuffix.EditValue = "E";
            this.txtEastingSuffix.Location = new Point(0x48, 0x2c);
            this.txtEastingSuffix.Name = "txtEastingSuffix";
            this.txtEastingSuffix.Size = new Size(0x58, 0x17);
            this.txtEastingSuffix.TabIndex = 6;
            this.txtEastingSuffix.EditValueChanged += new EventHandler(this.txtEastingSuffix_EditValueChanged);
            this.txtNorthingSuffix.EditValue = "N";
            this.txtNorthingSuffix.Location = new Point(0x48, 0x48);
            this.txtNorthingSuffix.Name = "txtNorthingSuffix";
            this.txtNorthingSuffix.Size = new Size(0x58, 0x17);
            this.txtNorthingSuffix.TabIndex = 7;
            this.txtNorthingSuffix.EditValueChanged += new EventHandler(this.txtNorthingSuffix_EditValueChanged);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "PrincipalDigitsLabelPropertyPage";
            base.Size = new Size(0x120, 0x160);
            base.Load += new EventHandler(this.PrincipalDigitsLabelPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.txtPrincipalDigitCount.Properties.EndInit();
            this.txtBaseDigitCount.Properties.EndInit();
            this.txtUnitSuffix.Properties.EndInit();
            this.txtEastingSuffix.Properties.EndInit();
            this.txtNorthingSuffix.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void PrincipalDigitsLabelPropertyPage_Load(object sender, EventArgs e)
        {
            string name = this.m_GridLable.SmallLabelFont.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.m_GridLable.SmallLabelSize.ToString();
            this.chkBold.Checked = this.m_GridLable.SmallLabelFont.Bold;
            this.chkIta.Checked = this.m_GridLable.SmallLabelFont.Italic;
            this.chkUnderLine.Checked = this.m_GridLable.SmallLabelFont.Underline;
            this.SetColorEdit(this.colorEdit1, this.m_GridLable.SmallLabelColor);
            this.txtBaseDigitCount.Value = this.m_GridLable.BaseDigitCount;
            this.txtPrincipalDigitCount.Value = this.m_GridLable.PrincipalDigitCount;
            this.txtUnitSuffix.Text = this.m_GridLable.UnitSuffix;
            this.txtNorthingSuffix.Text = this.m_GridLable.NorthingSuffix;
            this.txtEastingSuffix.Text = this.m_GridLable.EastingSuffix;
            this.m_CanDo = true;
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

        public void SetObjects(object @object)
        {
            this.m_GridLable = @object as IPrincipalDigitsGridLabel;
        }

        private void txtBaseDigitCount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtEastingSuffix_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtNorthingSuffix_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtPrincipalDigitCount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtUnitSuffix_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
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

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
            }
        }
    }
}

