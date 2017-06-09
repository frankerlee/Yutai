using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class IndexGridLabelPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnFont;
        private ComboBoxEdit cboLabelType;
        private ColorEdit colorEdit1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IGridLabel igridLabel_0 = null;
        private IIndexGrid iindexGrid_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = new TextSymbolClass();
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioGroup1;

        public IndexGridLabelPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(this.istyleGallery_0);
                selector.SetSymbol(this.itextSymbol_0);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    IGridLabel labelFormat = this.iindexGrid_0.LabelFormat;
                    labelFormat.Font = this.itextSymbol_0.Font;
                    labelFormat.Color = this.itextSymbol_0.Color;
                    this.iindexGrid_0.LabelFormat = labelFormat;
                }
            }
            catch
            {
            }
        }

        private void cboLabelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                switch (this.cboLabelType.SelectedIndex)
                {
                    case 0:
                        this.igridLabel_0 = new ButtonTabStyleClass();
                        break;

                    case 1:
                        this.igridLabel_0 = new BackgroundTabStyleClass();
                        break;

                    case 2:
                        this.igridLabel_0 = new ContinuousTabStyleClass();
                        break;

                    case 3:
                        this.igridLabel_0 = new RoundedTabStyleClass();
                        break;
                }
                this.iindexGrid_0.LabelFormat = this.igridLabel_0;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor color = this.igridLabel_0.Color;
                this.method_2(this.colorEdit1, color);
                this.igridLabel_0.Color = color;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void IndexGridLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.bool_1 = true;
        }

        public void Init()
        {
            this.bool_0 = false;
            if (this.igridLabel_0 != null)
            {
                string displayName = this.igridLabel_0.DisplayName;
                this.cboLabelType.Text = displayName;
                this.method_3(this.colorEdit1, this.igridLabel_0.Color);
                this.itextSymbol_0.Font = this.igridLabel_0.Font;
                this.itextSymbol_0.Color = this.igridLabel_0.Color;
                this.itextSymbol_0.Text = "ABC 123...";
                this.btnFont.Style = this.itextSymbol_0;
            }
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.cboLabelType = new ComboBoxEdit();
            this.btnFont = new StyleButton();
            this.colorEdit1 = new ColorEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.cboLabelType.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboLabelType);
            this.groupBox1.Controls.Add(this.btnFont);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x100, 0x80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签样式";
            this.cboLabelType.EditValue = "按钮制表符";
            this.cboLabelType.Location = new Point(0x58, 0x18);
            this.cboLabelType.Name = "cboLabelType";
            this.cboLabelType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelType.Properties.Items.AddRange(new object[] { "按钮制表符", "背景填充", "连续的制表符", "圆形的制表符" });
            this.cboLabelType.Size = new Size(0x68, 0x17);
            this.cboLabelType.TabIndex = 5;
            this.cboLabelType.SelectedIndexChanged += new EventHandler(this.cboLabelType_SelectedIndexChanged);
            this.btnFont.Location = new Point(0x58, 0x58);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new Size(80, 0x18);
            this.btnFont.TabIndex = 4;
            this.btnFont.Click += new EventHandler(this.btnFont_Click);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x58, 0x38);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x58, 0x17);
            this.colorEdit1.TabIndex = 3;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x18, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "符号";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x18, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "颜色";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "标签类";
            this.groupBox2.Controls.Add(this.radioGroup1);
            this.groupBox2.Location = new Point(0x10, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标签配置";
            this.radioGroup1.Location = new Point(8, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "列标A,B,C……,行标1,2,3……"), new RadioGroupItem(null, "列标1,2,3……,行标A,B,C……") });
            this.radioGroup1.Size = new Size(0xb8, 0x38);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "IndexGridLabelPropertyPage";
            base.Size = new Size(0x128, 0x108);
            base.Load += new EventHandler(this.IndexGridLabelPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboLabelType.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
            int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_1(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_1(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
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
                this.method_0((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.iindexGrid_0 = value as IIndexGrid;
                this.igridLabel_0 = this.iindexGrid_0.LabelFormat;
                if (this.bool_1)
                {
                    this.Init();
                }
            }
        }
    }
}

