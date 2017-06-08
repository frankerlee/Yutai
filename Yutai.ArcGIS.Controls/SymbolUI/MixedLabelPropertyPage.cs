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
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class MixedLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnNumberFormat;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private CheckBox chkBold;
        private CheckBox chkIta;
        private CheckBox chkUnderLine;
        private ColorEdit colorEdit1;
        private IContainer components;
        private GroupBox groupBox1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label4;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMixedFontGridLabel2 m_pMixedFormatGridLabel = null;
        private INumberFormat m_pNumberFormat = null;
        private string m_Title = "混合字体标注";
        private RadioGroup radioGroup;
        private SpinEdit txtNumGroupedDigits;

        public event OnValueChangeEventHandler OnValueChange;

        public MixedLabelPropertyPage()
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
                stdole.IFontDisp secondaryFont = this.m_pMixedFormatGridLabel.SecondaryFont;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    secondaryFont.Name = this.cboFontName.Text;
                }
                secondaryFont.Bold = this.chkBold.Checked;
                secondaryFont.Italic = this.chkIta.Checked;
                secondaryFont.Underline = this.chkUnderLine.Checked;
                this.m_pMixedFormatGridLabel.SecondaryFont = secondaryFont;
                IColor secondaryColor = this.m_pMixedFormatGridLabel.SecondaryColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, secondaryColor);
                this.m_pMixedFormatGridLabel.SecondaryColor = secondaryColor;
                this.m_pMixedFormatGridLabel.SecondaryFontSize = double.Parse(this.cboFontSize.Text);
                if (this.radioGroup.SelectedIndex == 0)
                {
                    this.m_pMixedFormatGridLabel.NumGroupedDigits = 0;
                }
                else
                {
                    this.m_pMixedFormatGridLabel.NumGroupedDigits = (short) this.txtNumGroupedDigits.Value;
                }
            }
        }

        private void btnNumberFormat_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty {
                Text = "数字格式属性"
            };
            NumericFormatPropertyPage page = new NumericFormatPropertyPage();
            property.AddPage(page);
            if (property.EditProperties(this.m_pNumberFormat))
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
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
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MixedLabelPropertyPage));
            this.groupBox1 = new GroupBox();
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
            this.radioGroup = new RadioGroup();
            this.txtNumGroupedDigits = new SpinEdit();
            this.btnNumberFormat = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.radioGroup.Properties.BeginInit();
            this.txtNumGroupedDigits.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.chkUnderLine);
            this.groupBox1.Controls.Add(this.chkIta);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Location = new Point(8, 0x68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x100, 0x80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "二级字体";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(10, 0x30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 0x27;
            this.label4.Text = "大小:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 0x26;
            this.label2.Text = "颜色:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0x25;
            this.label1.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(50, 0x30);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x17);
            this.cboFontSize.TabIndex = 0x24;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(50, 80);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0x23;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.chkUnderLine.Appearance = Appearance.Button;
            this.chkUnderLine.ImageIndex = 2;
            this.chkUnderLine.ImageList = this.imageList1;
            this.chkUnderLine.Location = new Point(0xc0, 0x30);
            this.chkUnderLine.Name = "chkUnderLine";
            this.chkUnderLine.Size = new Size(0x1c, 0x18);
            this.chkUnderLine.TabIndex = 0x22;
            this.chkUnderLine.CheckedChanged += new EventHandler(this.chkUnderLine_CheckedChanged);
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.chkIta.Appearance = Appearance.Button;
            this.chkIta.ImageIndex = 1;
            this.chkIta.ImageList = this.imageList1;
            this.chkIta.Location = new Point(160, 0x30);
            this.chkIta.Name = "chkIta";
            this.chkIta.Size = new Size(0x1c, 0x18);
            this.chkIta.TabIndex = 0x21;
            this.chkIta.CheckedChanged += new EventHandler(this.chkIta_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(0x80, 0x30);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 0x20;
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboFontName.Location = new Point(50, 0x18);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 0x1f;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.radioGroup.Location = new Point(0x10, 0x10);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "根据小数位分组"), new RadioGroupItem(null, "指定组中的小数位数") });
            this.radioGroup.Size = new Size(0x88, 0x30);
            this.radioGroup.TabIndex = 1;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            int[] bits = new int[4];
            this.txtNumGroupedDigits.EditValue = new decimal(bits);
            this.txtNumGroupedDigits.Location = new Point(40, 0x48);
            this.txtNumGroupedDigits.Name = "txtNumGroupedDigits";
            this.txtNumGroupedDigits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtNumGroupedDigits.Properties.Enabled = false;
            this.txtNumGroupedDigits.Properties.UseCtrlIncrement = false;
            this.txtNumGroupedDigits.Size = new Size(80, 0x17);
            this.txtNumGroupedDigits.TabIndex = 2;
            this.txtNumGroupedDigits.EditValueChanged += new EventHandler(this.txtNumGroupedDigits_EditValueChanged);
            this.btnNumberFormat.Location = new Point(0x38, 240);
            this.btnNumberFormat.Name = "btnNumberFormat";
            this.btnNumberFormat.Size = new Size(0x48, 0x18);
            this.btnNumberFormat.TabIndex = 3;
            this.btnNumberFormat.Text = "数字格式";
            this.btnNumberFormat.Click += new EventHandler(this.btnNumberFormat_Click);
            base.Controls.Add(this.btnNumberFormat);
            base.Controls.Add(this.txtNumGroupedDigits);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.groupBox1);
            base.Name = "MixedLabelPropertyPage";
            base.Size = new Size(0x120, 0x120);
            base.Load += new EventHandler(this.MixedLabelPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.radioGroup.Properties.EndInit();
            this.txtNumGroupedDigits.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MixedLabelPropertyPage_Load(object sender, EventArgs e)
        {
            string name = this.m_pMixedFormatGridLabel.SecondaryFont.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            if (this.m_pMixedFormatGridLabel.NumGroupedDigits <= 0)
            {
                this.radioGroup.SelectedIndex = 0;
                this.txtNumGroupedDigits.Value = 3M;
            }
            else
            {
                this.radioGroup.SelectedIndex = 1;
                this.txtNumGroupedDigits.Value = this.m_pMixedFormatGridLabel.NumGroupedDigits;
            }
            this.cboFontSize.Text = this.m_pMixedFormatGridLabel.SecondaryFontSize.ToString();
            this.chkBold.Checked = this.m_pMixedFormatGridLabel.SecondaryFont.Bold;
            this.chkIta.Checked = this.m_pMixedFormatGridLabel.SecondaryFont.Italic;
            this.chkUnderLine.Checked = this.m_pMixedFormatGridLabel.SecondaryFont.Underline;
            this.SetColorEdit(this.colorEdit1, this.m_pMixedFormatGridLabel.SecondaryColor);
            this.m_CanDo = true;
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtNumGroupedDigits.Enabled = this.radioGroup.SelectedIndex == 1;
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
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

        public void SetObjects(object @object)
        {
            this.m_pMixedFormatGridLabel = @object as IMixedFontGridLabel2;
            this.m_pNumberFormat = (this.m_pMixedFormatGridLabel as IFormattedGridLabel).Format;
            if (this.m_pNumberFormat == null)
            {
                this.m_pNumberFormat = new NumericFormatClass();
                (this.m_pMixedFormatGridLabel as IFormattedGridLabel).Format = this.m_pNumberFormat;
            }
        }

        private void txtNumGroupedDigits_EditValueChanged(object sender, EventArgs e)
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

