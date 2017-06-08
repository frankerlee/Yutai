using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class LabelFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnProperty;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private ComboBoxEdit cboFormat;
        private CheckBox chkBold;
        private CheckBox chkIta;
        private CheckEdit chkLabelBottom;
        private CheckEdit chkLabelLeft;
        private CheckEdit chkLabelRight;
        private CheckEdit chkLabelTop;
        private CheckBox chkUnderLine;
        private CheckEdit chkverticalBottom;
        private CheckEdit chkverticalLeft;
        private CheckEdit chkverticalRight;
        private CheckEdit chkverticalTop;
        private ColorEdit colorEdit1;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ImageList imageList1;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label9;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "标注";
        private IGridLabel pGridLabel = null;
        private SpinEdit txtLabelOffset;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelFormatPropertyPage()
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
                GridAxisPropertyPage.m_pMapGrid.SetLabelVisibility(this.chkLabelLeft.Checked, this.chkLabelTop.Checked, this.chkLabelRight.Checked, this.chkLabelBottom.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom, !this.chkverticalBottom.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft, !this.chkverticalLeft.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisRight, !this.chkverticalRight.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisTop, !this.chkverticalTop.Checked);
                stdole.IFontDisp font = this.pGridLabel.Font;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    font.Name = this.cboFontName.Text;
                }
                font.Size = (decimal) double.Parse(this.cboFontSize.Text);
                font.Bold = this.chkBold.Checked;
                font.Italic = this.chkIta.Checked;
                font.Underline = this.chkUnderLine.Checked;
                this.pGridLabel.Font = font;
                IColor pColor = this.pGridLabel.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.pGridLabel.Color = pColor;
                this.pGridLabel.LabelOffset = (double) this.txtLabelOffset.Value;
                GridAxisPropertyPage.m_pMapGrid.LabelFormat = this.pGridLabel;
            }
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty {
                Text = "格网标注属性"
            };
            bool flag = false;
            if (this.cboFormat.SelectedIndex == 0)
            {
                NumericFormatPropertyPage page = new NumericFormatPropertyPage();
                property.AddPage(page);
                INumberFormat format = (this.pGridLabel as IFormattedGridLabel).Format;
                flag = property.EditProperties(format);
                if (flag)
                {
                    (this.pGridLabel as IFormattedGridLabel).Format = format;
                }
            }
            else if (this.cboFormat.SelectedIndex == 1)
            {
                MixedLabelPropertyPage page2 = new MixedLabelPropertyPage();
                property.AddPage(page2);
                flag = property.EditProperties(this.pGridLabel);
            }
            else
            {
                CornerGridLabelPropertyPage page3 = new CornerGridLabelPropertyPage();
                property.AddPage(page3);
                PrincipalDigitsLabelPropertyPage page4 = new PrincipalDigitsLabelPropertyPage();
                property.AddPage(page4);
                flag = property.EditProperties(this.pGridLabel);
            }
            if (flag)
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

        private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.cboFormat.SelectedIndex == 0)
                {
                    this.pGridLabel = new FormattedGridLabelClass();
                }
                else if (this.cboFormat.SelectedIndex == 1)
                {
                    this.pGridLabel = new MixedFontGridLabelClass();
                }
                else
                {
                    this.pGridLabel = new DMSGridLabelClass();
                }
                this.m_CanDo = false;
                this.SetGridLabel();
                this.m_CanDo = false;
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

        private void chkLabelTop_CheckedChanged(object sender, EventArgs e)
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

        private void chkSubBottom_CheckedChanged(object sender, EventArgs e)
        {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelFormatPropertyPage));
            this.groupBox3 = new GroupBox();
            this.chkverticalRight = new CheckEdit();
            this.chkverticalBottom = new CheckEdit();
            this.chkverticalLeft = new CheckEdit();
            this.chkverticalTop = new CheckEdit();
            this.label10 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnProperty = new SimpleButton();
            this.txtLabelOffset = new SpinEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.chkUnderLine = new CheckBox();
            this.imageList1 = new ImageList(this.components);
            this.chkIta = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.cboFormat = new ComboBoxEdit();
            this.label9 = new Label();
            this.chkLabelRight = new CheckEdit();
            this.chkLabelBottom = new CheckEdit();
            this.chkLabelLeft = new CheckEdit();
            this.chkLabelTop = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.groupBox3.SuspendLayout();
            this.chkverticalRight.Properties.BeginInit();
            this.chkverticalBottom.Properties.BeginInit();
            this.chkverticalLeft.Properties.BeginInit();
            this.chkverticalTop.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtLabelOffset.Properties.BeginInit();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.cboFormat.Properties.BeginInit();
            this.chkLabelRight.Properties.BeginInit();
            this.chkLabelBottom.Properties.BeginInit();
            this.chkLabelLeft.Properties.BeginInit();
            this.chkLabelTop.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox3.Controls.Add(this.chkverticalRight);
            this.groupBox3.Controls.Add(this.chkverticalBottom);
            this.groupBox3.Controls.Add(this.chkverticalLeft);
            this.groupBox3.Controls.Add(this.chkverticalTop);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new Point(0x10, 0x108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x108, 80);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "边框属性";
            this.chkverticalRight.Location = new Point(0xb0, 0x30);
            this.chkverticalRight.Name = "chkverticalRight";
            this.chkverticalRight.Properties.Caption = "右";
            this.chkverticalRight.Size = new Size(0x30, 0x13);
            this.chkverticalRight.TabIndex = 0x10;
            this.chkverticalRight.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkverticalBottom.Location = new Point(120, 0x30);
            this.chkverticalBottom.Name = "chkverticalBottom";
            this.chkverticalBottom.Properties.Caption = "下";
            this.chkverticalBottom.Size = new Size(0x30, 0x13);
            this.chkverticalBottom.TabIndex = 15;
            this.chkverticalBottom.CheckedChanged += new EventHandler(this.chkSubBottom_CheckedChanged);
            this.chkverticalLeft.Location = new Point(0x40, 0x30);
            this.chkverticalLeft.Name = "chkverticalLeft";
            this.chkverticalLeft.Properties.Caption = "左";
            this.chkverticalLeft.Size = new Size(0x30, 0x13);
            this.chkverticalLeft.TabIndex = 14;
            this.chkverticalLeft.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkverticalTop.Location = new Point(8, 0x30);
            this.chkverticalTop.Name = "chkverticalTop";
            this.chkverticalTop.Properties.Caption = "上";
            this.chkverticalTop.Size = new Size(0x30, 0x13);
            this.chkverticalTop.TabIndex = 13;
            this.chkverticalTop.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(8, 0x18);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "垂直标注";
            this.groupBox2.Controls.Add(this.btnProperty);
            this.groupBox2.Controls.Add(this.txtLabelOffset);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboFontSize);
            this.groupBox2.Controls.Add(this.colorEdit1);
            this.groupBox2.Controls.Add(this.chkUnderLine);
            this.groupBox2.Controls.Add(this.chkIta);
            this.groupBox2.Controls.Add(this.chkBold);
            this.groupBox2.Controls.Add(this.cboFontName);
            this.groupBox2.Controls.Add(this.cboFormat);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new Point(8, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x110, 0xa8);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注样式";
            this.btnProperty.Location = new Point(40, 0x88);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(0x58, 0x18);
            this.btnProperty.TabIndex = 0x20;
            this.btnProperty.Text = "附加属性";
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            int[] bits = new int[4];
            this.txtLabelOffset.EditValue = new decimal(bits);
            this.txtLabelOffset.Location = new Point(0xc0, 0x68);
            this.txtLabelOffset.Name = "txtLabelOffset";
            this.txtLabelOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtLabelOffset.Size = new Size(0x40, 0x15);
            this.txtLabelOffset.TabIndex = 0x1f;
            this.txtLabelOffset.EditValueChanged += new EventHandler(this.txtLabelOffset_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x48);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "大小:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(120, 0x70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 0x1d;
            this.label3.Text = "标注偏移:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x68);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 0x1c;
            this.label2.Text = "颜色:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x1b;
            this.label1.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(0x30, 0x48);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x15);
            this.cboFontSize.TabIndex = 0x1a;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x30, 0x68);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 0x19;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.chkUnderLine.Appearance = Appearance.Button;
            this.chkUnderLine.ImageIndex = 2;
            this.chkUnderLine.ImageList = this.imageList1;
            this.chkUnderLine.Location = new Point(0xd8, 0x48);
            this.chkUnderLine.Name = "chkUnderLine";
            this.chkUnderLine.Size = new Size(0x1c, 0x18);
            this.chkUnderLine.TabIndex = 0x18;
            this.chkUnderLine.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.chkIta.Appearance = Appearance.Button;
            this.chkIta.ImageIndex = 1;
            this.chkIta.ImageList = this.imageList1;
            this.chkIta.Location = new Point(0xb8, 0x48);
            this.chkIta.Name = "chkIta";
            this.chkIta.Size = new Size(0x1c, 0x18);
            this.chkIta.TabIndex = 0x17;
            this.chkIta.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(0x98, 0x48);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 0x16;
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboFontName.Location = new Point(0x30, 0x30);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 0x15;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.cboFormat.EditValue = "格式化";
            this.cboFormat.Location = new Point(0x30, 0x10);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFormat.Properties.Items.AddRange(new object[] { "格式化", "混合字体", "角标注" });
            this.cboFormat.Size = new Size(0x80, 0x15);
            this.cboFormat.TabIndex = 20;
            this.cboFormat.SelectedIndexChanged += new EventHandler(this.cboFormat_SelectedIndexChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(8, 0x18);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 12);
            this.label9.TabIndex = 0x12;
            this.label9.Text = "格式:";
            this.chkLabelRight.Location = new Point(0xb0, 0x18);
            this.chkLabelRight.Name = "chkLabelRight";
            this.chkLabelRight.Properties.Caption = "右";
            this.chkLabelRight.Size = new Size(0x30, 0x13);
            this.chkLabelRight.TabIndex = 3;
            this.chkLabelRight.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkLabelBottom.Location = new Point(120, 0x18);
            this.chkLabelBottom.Name = "chkLabelBottom";
            this.chkLabelBottom.Properties.Caption = "下";
            this.chkLabelBottom.Size = new Size(0x30, 0x13);
            this.chkLabelBottom.TabIndex = 2;
            this.chkLabelBottom.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkLabelLeft.Location = new Point(0x40, 0x18);
            this.chkLabelLeft.Name = "chkLabelLeft";
            this.chkLabelLeft.Properties.Caption = "左";
            this.chkLabelLeft.Size = new Size(0x30, 0x13);
            this.chkLabelLeft.TabIndex = 1;
            this.chkLabelLeft.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkLabelTop.Location = new Point(8, 0x18);
            this.chkLabelTop.Name = "chkLabelTop";
            this.chkLabelTop.Properties.Caption = "上";
            this.chkLabelTop.Size = new Size(0x30, 0x13);
            this.chkLabelTop.TabIndex = 0;
            this.chkLabelTop.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.groupBox1.Controls.Add(this.chkLabelRight);
            this.groupBox1.Controls.Add(this.chkLabelBottom);
            this.groupBox1.Controls.Add(this.chkLabelLeft);
            this.groupBox1.Controls.Add(this.chkLabelTop);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x38);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注轴";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelFormatPropertyPage";
            base.Size = new Size(0x130, 0x170);
            base.Load += new EventHandler(this.LabelFormatPropertyPage_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.chkverticalRight.Properties.EndInit();
            this.chkverticalBottom.Properties.EndInit();
            this.chkverticalLeft.Properties.EndInit();
            this.chkverticalTop.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtLabelOffset.Properties.EndInit();
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.cboFormat.Properties.EndInit();
            this.chkLabelRight.Properties.EndInit();
            this.chkLabelBottom.Properties.EndInit();
            this.chkLabelLeft.Properties.EndInit();
            this.chkLabelTop.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LabelFormatPropertyPage_Load(object sender, EventArgs e)
        {
            if (GridAxisPropertyPage.m_pMapGrid != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool rightVis = false;
                bool bottomVis = false;
                GridAxisPropertyPage.m_pMapGrid.QueryLabelVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkLabelBottom.Checked = bottomVis;
                this.chkLabelLeft.Checked = leftVis;
                this.chkLabelRight.Checked = rightVis;
                this.chkLabelTop.Checked = topVis;
                this.pGridLabel = GridAxisPropertyPage.m_pMapGrid.LabelFormat;
                if (this.pGridLabel is IMixedFontGridLabel2)
                {
                    this.cboFormat.SelectedIndex = 1;
                }
                else if (this.pGridLabel is IFormattedGridLabel)
                {
                    this.cboFormat.SelectedIndex = 0;
                }
                else
                {
                    this.cboFormat.SelectedIndex = 2;
                }
                this.SetGridLabel();
                this.m_CanDo = true;
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

        private void SetGridLabel()
        {
            this.chkverticalBottom.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom);
            this.chkverticalLeft.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft);
            this.chkverticalRight.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisRight);
            this.chkverticalTop.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisTop);
            string name = this.pGridLabel.Font.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.pGridLabel.Font.Size.ToString();
            this.chkBold.Checked = this.pGridLabel.Font.Bold;
            this.chkIta.Checked = this.pGridLabel.Font.Italic;
            this.chkUnderLine.Checked = this.pGridLabel.Font.Underline;
            this.SetColorEdit(this.colorEdit1, this.pGridLabel.Color);
            this.txtLabelOffset.Text = this.pGridLabel.LabelOffset.ToString();
        }

        public void SetObjects(object @object)
        {
        }

        private void txtLabelOffset_EditValueChanged(object sender, EventArgs e)
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
                this.m_Title = value;
            }
        }
    }
}

