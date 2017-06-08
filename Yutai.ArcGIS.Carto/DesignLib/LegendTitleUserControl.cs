using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class LegendTitleUserControl : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnBlod;
        private SimpleButton btnItalic;
        private SimpleButton btnUnderline;
        private System.Windows.Forms.ComboBox cboFontName;
        private ColorEdit colorEdit1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ILegend ilegend_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private Label label6;
        private Label label8;
        private MemoEdit memoEditTitle;
        private SpinEdit numUpDownSize;

        public LegendTitleUserControl()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        private void btnBlod_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.itextSymbol_0.Font;
            font.Bold = true;
            this.itextSymbol_0.Font = font;
            this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.itextSymbol_0.Font;
            font.Italic = true;
            this.itextSymbol_0.Font = font;
            this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.itextSymbol_0.Font;
            font.Underline = true;
            this.itextSymbol_0.Font = font;
            this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                stdole.IFontDisp font = this.itextSymbol_0.Font;
                font.Name = (string) this.cboFontName.Items[this.cboFontName.SelectedIndex];
                this.itextSymbol_0.Font = font;
                this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor color = this.itextSymbol_0.Color;
                this.method_3(this.colorEdit1, color);
                this.itextSymbol_0.Color = color;
                this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public void InitControl()
        {
            this.bool_0 = false;
            if (this.itextSymbol_0 == null)
            {
                this.itextSymbol_0 = new TextSymbolClass();
            }
            this.method_0(this.colorEdit1, this.itextSymbol_0.Color);
            this.numUpDownSize.Value = (decimal) this.itextSymbol_0.Size;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (this.itextSymbol_0.Font.Name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            ResourceManager manager = new ResourceManager(typeof(LegendTitleUserControl));
            this.groupBox1 = new GroupBox();
            this.memoEditTitle = new MemoEdit();
            this.groupBox2 = new GroupBox();
            this.btnUnderline = new SimpleButton();
            this.btnItalic = new SimpleButton();
            this.btnBlod = new SimpleButton();
            this.numUpDownSize = new SpinEdit();
            this.colorEdit1 = new ColorEdit();
            this.label8 = new Label();
            this.label6 = new Label();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.memoEditTitle.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.numUpDownSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.memoEditTitle);
            this.groupBox1.Location = new Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图例标题";
            this.memoEditTitle.EditValue = "";
            this.memoEditTitle.Location = new Point(8, 0x18);
            this.memoEditTitle.Name = "memoEditTitle";
            this.memoEditTitle.Size = new Size(0xf8, 80);
            this.memoEditTitle.TabIndex = 0;
            this.memoEditTitle.EditValueChanged += new EventHandler(this.memoEditTitle_EditValueChanged);
            this.groupBox2.Controls.Add(this.btnUnderline);
            this.groupBox2.Controls.Add(this.btnItalic);
            this.groupBox2.Controls.Add(this.btnBlod);
            this.groupBox2.Controls.Add(this.numUpDownSize);
            this.groupBox2.Controls.Add(this.colorEdit1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboFontName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(0x10, 0x80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x110, 0x80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图例标题字体属性";
            this.btnUnderline.Image = manager.GetObject("btnUnderline.Image");
            this.btnUnderline.Location = new Point(0x70, 0x60);
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new Size(0x18, 0x18);
            this.btnUnderline.TabIndex = 0x47;
            this.btnUnderline.Visible = false;
            this.btnUnderline.Click += new EventHandler(this.btnUnderline_Click);
            this.btnItalic.Image = manager.GetObject("btnItalic.Image");
            this.btnItalic.Location = new Point(80, 0x60);
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new Size(0x18, 0x18);
            this.btnItalic.TabIndex = 70;
            this.btnItalic.Visible = false;
            this.btnItalic.Click += new EventHandler(this.btnItalic_Click);
            this.btnBlod.Image = manager.GetObject("btnBlod.Image");
            this.btnBlod.Location = new Point(0x30, 0x60);
            this.btnBlod.Name = "btnBlod";
            this.btnBlod.Size = new Size(0x18, 0x18);
            this.btnBlod.TabIndex = 0x45;
            this.btnBlod.Visible = false;
            this.btnBlod.Click += new EventHandler(this.btnBlod_Click);
            int[] bits = new int[4];
            this.numUpDownSize.EditValue = new decimal(bits);
            this.numUpDownSize.Location = new Point(0x38, 0x30);
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numUpDownSize.Properties.MaxValue = new decimal(bits);
            this.numUpDownSize.Properties.UseCtrlIncrement = false;
            this.numUpDownSize.Size = new Size(0x40, 0x15);
            this.numUpDownSize.TabIndex = 0x44;
            this.numUpDownSize.TextChanged += new EventHandler(this.numUpDownSize_TextChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x18);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 0x43;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x18);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 0x11);
            this.label8.TabIndex = 0x42;
            this.label8.Text = "颜色";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x30);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 0x41;
            this.label6.Text = "大小";
            this.cboFontName.Location = new Point(0x38, 0x48);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xa8, 20);
            this.cboFontName.TabIndex = 0x40;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x4b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0x3f;
            this.label1.Text = "字体";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendTitleUserControl";
            base.Size = new Size(400, 0x108);
            base.Load += new EventHandler(this.LegendTitleUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.memoEditTitle.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.numUpDownSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LegendTitleUserControl_Load(object sender, EventArgs e)
        {
            this.memoEditTitle.Text = this.ilegend_0.Title;
            this.InitControl();
            this.bool_0 = true;
        }

        private void memoEditTitle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ilegend_0.Title = this.memoEditTitle.Text;
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

        private void numUpDownSize_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.numUpDownSize.Value <= 0M)
                {
                    this.numUpDownSize.Value = (decimal) this.itextSymbol_0.Size;
                }
                else
                {
                    this.itextSymbol_0.Size = (double) this.numUpDownSize.Value;
                    this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
                }
            }
        }

        public ILegend Legend
        {
            set
            {
                this.ilegend_0 = value;
                this.itextSymbol_0 = this.ilegend_0.Format.TitleSymbol;
            }
        }
    }
}

