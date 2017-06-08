using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class LabelFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
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
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IContainer icontainer_0;
        private IGridLabel igridLabel_0 = null;
        private ImageList imageList_0;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label9;
        protected IMapGrid m_pMapGrid = null;
        private string string_0 = "标注";
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
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.m_pMapGrid.SetLabelVisibility(this.chkLabelLeft.Checked, this.chkLabelTop.Checked, this.chkLabelRight.Checked, this.chkLabelBottom.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom, !this.chkverticalBottom.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft, !this.chkverticalLeft.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisRight, !this.chkverticalRight.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisTop, !this.chkverticalTop.Checked);
                stdole.IFontDisp font = this.igridLabel_0.Font;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    font.Name = this.cboFontName.Text;
                }
                font.Size = double.Parse(this.cboFontSize.Text);
                font.Bold = this.chkBold.Checked;
                font.Italic = this.chkIta.Checked;
                font.Underline = this.chkUnderLine.Checked;
                this.igridLabel_0.Font = font;
                IColor color = this.igridLabel_0.Color;
                this.method_3(this.colorEdit1, color);
                this.igridLabel_0.Color = color;
                this.igridLabel_0.LabelOffset = (double) this.txtLabelOffset.Value;
                this.m_pMapGrid.LabelFormat = this.igridLabel_0;
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
                INumberFormat format = (this.igridLabel_0 as IFormattedGridLabel).Format;
                if (flag = property.EditProperties(format))
                {
                    (this.igridLabel_0 as IFormattedGridLabel).Format = format;
                }
            }
            else if (this.cboFormat.SelectedIndex == 1)
            {
                MixedLabelPropertyPage page2 = new MixedLabelPropertyPage();
                property.AddPage(page2);
                flag = property.EditProperties(this.igridLabel_0);
            }
            else
            {
                CornerGridLabelPropertyPage page3 = new CornerGridLabelPropertyPage();
                property.AddPage(page3);
                PrincipalDigitsLabelPropertyPage page4 = new PrincipalDigitsLabelPropertyPage();
                property.AddPage(page4);
                flag = property.EditProperties(this.igridLabel_0);
            }
            if (flag)
            {
                this.bool_1 = true;
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
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboFormat.SelectedIndex == 0)
                {
                    this.igridLabel_0 = new FormattedGridLabelClass();
                }
                else if (this.cboFormat.SelectedIndex == 1)
                {
                    this.igridLabel_0 = new MixedFontGridLabelClass();
                }
                else
                {
                    this.igridLabel_0 = new DMSGridLabelClass();
                }
                this.bool_0 = false;
                this.method_4();
                this.bool_0 = false;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkLabelTop_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkverticalBottom_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(LabelFormatPropertyPage));
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
            this.imageList_0 = new ImageList(this.icontainer_0);
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
            this.groupBox3.Location = new Point(130, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(150, 0x42);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "边框属性";
            this.chkverticalRight.Location = new Point(0x68, 0x2a);
            this.chkverticalRight.Name = "chkverticalRight";
            this.chkverticalRight.Properties.Caption = "右";
            this.chkverticalRight.Size = new Size(0x30, 0x13);
            this.chkverticalRight.TabIndex = 0x10;
            this.chkverticalRight.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkverticalBottom.Location = new Point(0x3e, 0x2a);
            this.chkverticalBottom.Name = "chkverticalBottom";
            this.chkverticalBottom.Properties.Caption = "下";
            this.chkverticalBottom.Size = new Size(0x30, 0x13);
            this.chkverticalBottom.TabIndex = 15;
            this.chkverticalBottom.CheckedChanged += new EventHandler(this.chkverticalBottom_CheckedChanged);
            this.chkverticalLeft.Location = new Point(0x68, 0x11);
            this.chkverticalLeft.Name = "chkverticalLeft";
            this.chkverticalLeft.Properties.Caption = "左";
            this.chkverticalLeft.Size = new Size(0x30, 0x13);
            this.chkverticalLeft.TabIndex = 14;
            this.chkverticalLeft.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkverticalTop.Location = new Point(0x3e, 0x11);
            this.chkverticalTop.Name = "chkverticalTop";
            this.chkverticalTop.Properties.Caption = "上";
            this.chkverticalTop.Size = new Size(0x30, 0x13);
            this.chkverticalTop.TabIndex = 13;
            this.chkverticalTop.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(6, 0x11);
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
            this.chkUnderLine.ImageList = this.imageList_0;
            this.chkUnderLine.Location = new Point(0xd8, 0x48);
            this.chkUnderLine.Name = "chkUnderLine";
            this.chkUnderLine.Size = new Size(0x1c, 0x18);
            this.chkUnderLine.TabIndex = 0x18;
            this.chkUnderLine.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Magenta;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.imageList_0.Images.SetKeyName(2, "");
            this.imageList_0.Images.SetKeyName(3, "");
            this.imageList_0.Images.SetKeyName(4, "");
            this.imageList_0.Images.SetKeyName(5, "");
            this.imageList_0.Images.SetKeyName(6, "");
            this.chkIta.Appearance = Appearance.Button;
            this.chkIta.ImageIndex = 1;
            this.chkIta.ImageList = this.imageList_0;
            this.chkIta.Location = new Point(0xb8, 0x48);
            this.chkIta.Name = "chkIta";
            this.chkIta.Size = new Size(0x1c, 0x18);
            this.chkIta.TabIndex = 0x17;
            this.chkIta.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList_0;
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
            this.chkLabelRight.Location = new Point(0x30, 0x29);
            this.chkLabelRight.Name = "chkLabelRight";
            this.chkLabelRight.Properties.Caption = "右";
            this.chkLabelRight.Size = new Size(0x30, 0x13);
            this.chkLabelRight.TabIndex = 3;
            this.chkLabelRight.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkLabelBottom.Location = new Point(6, 0x29);
            this.chkLabelBottom.Name = "chkLabelBottom";
            this.chkLabelBottom.Properties.Caption = "下";
            this.chkLabelBottom.Size = new Size(0x30, 0x13);
            this.chkLabelBottom.TabIndex = 2;
            this.chkLabelBottom.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkLabelLeft.Location = new Point(0x30, 20);
            this.chkLabelLeft.Name = "chkLabelLeft";
            this.chkLabelLeft.Properties.Caption = "左";
            this.chkLabelLeft.Size = new Size(0x30, 0x13);
            this.chkLabelLeft.TabIndex = 1;
            this.chkLabelLeft.CheckedChanged += new EventHandler(this.chkLabelTop_CheckedChanged);
            this.chkLabelTop.Location = new Point(8, 20);
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
            this.groupBox1.Size = new Size(0x70, 0x42);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注轴";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelFormatPropertyPage";
            base.Size = new Size(0x132, 260);
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
            if (this.m_pMapGrid != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool rightVis = false;
                bool bottomVis = false;
                this.m_pMapGrid.QueryLabelVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkLabelBottom.Checked = bottomVis;
                this.chkLabelLeft.Checked = leftVis;
                this.chkLabelRight.Checked = rightVis;
                this.chkLabelTop.Checked = topVis;
                this.igridLabel_0 = this.m_pMapGrid.LabelFormat;
                if (this.igridLabel_0 is IMixedFontGridLabel2)
                {
                    this.cboFormat.SelectedIndex = 1;
                }
                else if (this.igridLabel_0 is IFormattedGridLabel)
                {
                    this.cboFormat.SelectedIndex = 0;
                }
                else
                {
                    this.cboFormat.SelectedIndex = 2;
                }
                this.method_4();
                this.bool_0 = true;
            }
        }

        private void method_0(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
                this.method_1(rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_1(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
             int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_2(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_2(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_4()
        {
            this.chkverticalBottom.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom);
            this.chkverticalLeft.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft);
            this.chkverticalRight.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisRight);
            this.chkverticalTop.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisTop);
            string name = this.igridLabel_0.Font.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.igridLabel_0.Font.Size.ToString();
            this.chkBold.Checked = this.igridLabel_0.Font.Bold;
            this.chkIta.Checked = this.igridLabel_0.Font.Italic;
            this.chkUnderLine.Checked = this.igridLabel_0.Font.Underline;
            this.method_0(this.colorEdit1, this.igridLabel_0.Color);
            this.txtLabelOffset.Text = this.igridLabel_0.LabelOffset.ToString();
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.m_pMapGrid = object_0 as IMapGrid;
        }

        private void txtLabelOffset_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
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
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

