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
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class MGRSPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private StyleButton btnBorder;
        private StyleButton btnTickSymbol;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private System.Windows.Forms.ComboBox cboGSLFontName;
        private ComboBoxEdit cboGSLFontsize;
        private ComboBoxEdit cboGSLStyle;
        private CheckBox chkBold;
        private CheckBox chkBold_GSL;
        private CheckBox chkIta;
        private CheckBox chkIta_GSL;
        private CheckEdit chkShowGSIdentifiers;
        private CheckEdit chkShowLadderLabels;
        private CheckEdit chkShowOuterLabelsOnly;
        private CheckBox chkUnderLine;
        private CheckBox chkUnderLine_GSL;
        private ColorEdit colorEdit1;
        private ColorEdit colorEdit2;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ImageList imageList1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "MGRS";
        private SpinEdit txtColumnCount;
        private SpinEdit txtRowCount;
        private SpinEdit txtTickLength;

        public event OnValueChangeEventHandler OnValueChange;

        public MGRSPropertyPage()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
                this.cboGSLFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                IGridLadderLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridLadderLabels;
                stdole.IFontDisp ladderLabelFont = pMapGrid.LadderLabelFont;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    ladderLabelFont.Name = this.cboFontName.Text;
                }
                ladderLabelFont.Bold = this.chkBold.Checked;
                ladderLabelFont.Italic = this.chkIta.Checked;
                ladderLabelFont.Underline = this.chkUnderLine.Checked;
                pMapGrid.LadderLabelFont = ladderLabelFont;
                try
                {
                    pMapGrid.LadderLabelSize = double.Parse(this.cboFontSize.Text);
                }
                catch
                {
                }
                IColor ladderLabelColor = pMapGrid.LadderLabelColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, ladderLabelColor);
                pMapGrid.LadderLabelColor = ladderLabelColor;
                pMapGrid.LadderLabelColumnCount = (int) this.txtColumnCount.Value;
                pMapGrid.LadderLabelRowCount = (int) this.txtRowCount.Value;
                pMapGrid.ShowLadderLabels = this.chkShowLadderLabels.Checked;
                pMapGrid.ShowOuterLabelsOnly = this.chkShowOuterLabelsOnly.Checked;
                IMgrsGrid grid = GridAxisPropertyPage.m_pMapGrid as IMgrsGrid;
                ladderLabelFont = grid.GridSquareLabelFont;
                if (this.cboGSLFontName.SelectedIndex != -1)
                {
                    ladderLabelFont.Name = this.cboGSLFontName.Text;
                }
                ladderLabelFont.Bold = this.chkBold_GSL.Checked;
                ladderLabelFont.Italic = this.chkIta_GSL.Checked;
                ladderLabelFont.Underline = this.chkUnderLine_GSL.Checked;
                grid.GridSquareLabelFont = ladderLabelFont;
                try
                {
                    grid.GridSquareLabelSize = double.Parse(this.cboGSLFontsize.Text);
                }
                catch
                {
                }
                ladderLabelColor = grid.GridSquareLabelColor;
                this.UpdateColorFromColorEdit(this.colorEdit2, ladderLabelColor);
                grid.GridSquareLabelColor = ladderLabelColor;
                grid.ShowGridSquareIdentifiers = this.chkShowGSIdentifiers.Checked;
                grid.GridSquareBoundarySymbol = this.btnBorder.Style as ILineSymbol;
                grid.GridSquareLabelStyle = (esriGridSquareLabelStyleEnum) this.cboGSLStyle.SelectedIndex;
                grid.InteriorTickSymbol = this.btnTickSymbol.Style as ILineSymbol;
                grid.InteriorTickLength = (double) this.txtTickLength.Value;
                this.m_CanDo = true;
            }
        }

        private void btnBorder_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(GridAxisPropertyPage.m_pSG);
                selector.SetSymbol(this.btnBorder.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnBorder.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void btnTickSymbol_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(GridAxisPropertyPage.m_pSG);
                selector.SetSymbol(this.btnTickSymbol.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnTickSymbol.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
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

        private void cboGSLFontName_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboGSLFontsize_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboGSLStyle_SelectedIndexChanged(object sender, EventArgs e)
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

        private void chkShowGSIdentifiers_CheckedChanged(object sender, EventArgs e)
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

        private void chkShowLadderLabels_CheckedChanged(object sender, EventArgs e)
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

        private void chkShowOuterLabelsOnly_CheckedChanged(object sender, EventArgs e)
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

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MGRSPropertyPage));
            this.groupBox1 = new GroupBox();
            this.chkShowOuterLabelsOnly = new CheckEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label5 = new Label();
            this.cboFontSize = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.chkUnderLine = new CheckBox();
            this.imageList1 = new ImageList(this.components);
            this.chkIta = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.txtColumnCount = new SpinEdit();
            this.label2 = new Label();
            this.txtRowCount = new SpinEdit();
            this.label1 = new Label();
            this.chkShowLadderLabels = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.cboGSLStyle = new ComboBoxEdit();
            this.btnBorder = new StyleButton();
            this.label9 = new Label();
            this.chkShowGSIdentifiers = new CheckEdit();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.cboGSLFontsize = new ComboBoxEdit();
            this.colorEdit2 = new ColorEdit();
            this.chkUnderLine_GSL = new CheckBox();
            this.chkIta_GSL = new CheckBox();
            this.chkBold_GSL = new CheckBox();
            this.cboGSLFontName = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new GroupBox();
            this.txtTickLength = new SpinEdit();
            this.label12 = new Label();
            this.label11 = new Label();
            this.btnTickSymbol = new StyleButton();
            this.label10 = new Label();
            this.groupBox1.SuspendLayout();
            this.chkShowOuterLabelsOnly.Properties.BeginInit();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.txtColumnCount.Properties.BeginInit();
            this.txtRowCount.Properties.BeginInit();
            this.chkShowLadderLabels.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboGSLStyle.Properties.BeginInit();
            this.chkShowGSIdentifiers.Properties.BeginInit();
            this.cboGSLFontsize.Properties.BeginInit();
            this.colorEdit2.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtTickLength.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.chkShowOuterLabelsOnly);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.chkUnderLine);
            this.groupBox1.Controls.Add(this.chkIta);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Controls.Add(this.txtColumnCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRowCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkShowLadderLabels);
            this.groupBox1.Location = new Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0xb8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "梯状标注";
            this.chkShowOuterLabelsOnly.Location = new Point(40, 0x48);
            this.chkShowOuterLabelsOnly.Name = "chkShowOuterLabelsOnly";
            this.chkShowOuterLabelsOnly.Properties.Caption = "显示梯状内部标注";
            this.chkShowOuterLabelsOnly.Size = new Size(0x80, 0x13);
            this.chkShowOuterLabelsOnly.TabIndex = 40;
            this.chkShowOuterLabelsOnly.CheckedChanged += new EventHandler(this.chkShowOuterLabelsOnly_CheckedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 120);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 0x27;
            this.label4.Text = "大小:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x98);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 0x26;
            this.label3.Text = "颜色:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x60);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 0x25;
            this.label5.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(0x38, 120);
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
            this.colorEdit1.Location = new Point(0x38, 0x98);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0x23;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.chkUnderLine.Appearance = Appearance.Button;
            this.chkUnderLine.ImageIndex = 2;
            this.chkUnderLine.ImageList = this.imageList1;
            this.chkUnderLine.Location = new Point(0xe0, 120);
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
            this.chkIta.Location = new Point(0xc0, 120);
            this.chkIta.Name = "chkIta";
            this.chkIta.Size = new Size(0x1c, 0x18);
            this.chkIta.TabIndex = 0x21;
            this.chkIta.CheckedChanged += new EventHandler(this.chkIta_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(160, 120);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 0x20;
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboFontName.Location = new Point(0x38, 0x60);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 0x1f;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            int[] bits = new int[4];
            this.txtColumnCount.EditValue = new decimal(bits);
            this.txtColumnCount.Location = new Point(0x88, 40);
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtColumnCount.Properties.UseCtrlIncrement = false;
            this.txtColumnCount.Size = new Size(0x30, 0x17);
            this.txtColumnCount.TabIndex = 4;
            this.txtColumnCount.EditValueChanged += new EventHandler(this.txtRowCount_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x68, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "列";
            bits = new int[4];
            this.txtRowCount.EditValue = new decimal(bits);
            this.txtRowCount.Location = new Point(40, 40);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtRowCount.Properties.UseCtrlIncrement = false;
            this.txtRowCount.Size = new Size(0x30, 0x17);
            this.txtRowCount.TabIndex = 2;
            this.txtRowCount.EditValueChanged += new EventHandler(this.txtRowCount_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x2b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "行";
            this.chkShowLadderLabels.Location = new Point(8, 0x10);
            this.chkShowLadderLabels.Name = "chkShowLadderLabels";
            this.chkShowLadderLabels.Properties.Caption = "显示梯状内部标注";
            this.chkShowLadderLabels.Size = new Size(0x80, 0x13);
            this.chkShowLadderLabels.TabIndex = 0;
            this.chkShowLadderLabels.CheckedChanged += new EventHandler(this.chkShowLadderLabels_CheckedChanged);
            this.groupBox2.Controls.Add(this.cboGSLStyle);
            this.groupBox2.Controls.Add(this.btnBorder);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkShowGSIdentifiers);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cboGSLFontsize);
            this.groupBox2.Controls.Add(this.colorEdit2);
            this.groupBox2.Controls.Add(this.chkUnderLine_GSL);
            this.groupBox2.Controls.Add(this.chkIta_GSL);
            this.groupBox2.Controls.Add(this.chkBold_GSL);
            this.groupBox2.Controls.Add(this.cboGSLFontName);
            this.groupBox2.Location = new Point(0x10, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x110, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "100,000方形格网标注";
            this.cboGSLStyle.EditValue = "标注方格网每个角";
            this.cboGSLStyle.Location = new Point(0x80, 0x60);
            this.cboGSLStyle.Name = "cboGSLStyle";
            this.cboGSLStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboGSLStyle.Properties.Items.AddRange(new object[] { "标注方格网每个角", "标注方格网中心" });
            this.cboGSLStyle.Size = new Size(0x80, 0x17);
            this.cboGSLStyle.TabIndex = 0x34;
            this.cboGSLStyle.SelectedIndexChanged += new EventHandler(this.cboGSLStyle_SelectedIndexChanged);
            this.btnBorder.Location = new Point(80, 0x80);
            this.btnBorder.Name = "btnBorder";
            this.btnBorder.Size = new Size(0x60, 0x18);
            this.btnBorder.TabIndex = 0x33;
            this.btnBorder.Click += new EventHandler(this.btnBorder_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x10, 0x88);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 0x11);
            this.label9.TabIndex = 50;
            this.label9.Text = "边界:";
            this.chkShowGSIdentifiers.Location = new Point(0x10, 0x10);
            this.chkShowGSIdentifiers.Name = "chkShowGSIdentifiers";
            this.chkShowGSIdentifiers.Properties.Caption = "显示梯状内部标注";
            this.chkShowGSIdentifiers.Size = new Size(0x80, 0x13);
            this.chkShowGSIdentifiers.TabIndex = 0x31;
            this.chkShowGSIdentifiers.CheckedChanged += new EventHandler(this.chkShowGSIdentifiers_CheckedChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x12, 0x40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 0x11);
            this.label6.TabIndex = 0x30;
            this.label6.Text = "大小:";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x12, 0x60);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 0x11);
            this.label7.TabIndex = 0x2f;
            this.label7.Text = "颜色:";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x12, 40);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x23, 0x11);
            this.label8.TabIndex = 0x2e;
            this.label8.Text = "字体:";
            this.cboGSLFontsize.EditValue = "5";
            this.cboGSLFontsize.Location = new Point(0x3a, 0x40);
            this.cboGSLFontsize.Name = "cboGSLFontsize";
            this.cboGSLFontsize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboGSLFontsize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboGSLFontsize.Size = new Size(0x40, 0x17);
            this.cboGSLFontsize.TabIndex = 0x2d;
            this.cboGSLFontsize.SelectedIndexChanged += new EventHandler(this.cboGSLFontsize_SelectedIndexChanged);
            this.colorEdit2.EditValue = Color.Empty;
            this.colorEdit2.Location = new Point(0x3a, 0x60);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit2.Size = new Size(0x30, 0x17);
            this.colorEdit2.TabIndex = 0x2c;
            this.colorEdit2.EditValueChanged += new EventHandler(this.colorEdit2_EditValueChanged);
            this.chkUnderLine_GSL.Appearance = Appearance.Button;
            this.chkUnderLine_GSL.ImageIndex = 2;
            this.chkUnderLine_GSL.ImageList = this.imageList1;
            this.chkUnderLine_GSL.Location = new Point(0xe2, 0x40);
            this.chkUnderLine_GSL.Name = "chkUnderLine_GSL";
            this.chkUnderLine_GSL.Size = new Size(0x1c, 0x18);
            this.chkUnderLine_GSL.TabIndex = 0x2b;
            this.chkUnderLine_GSL.CheckedChanged += new EventHandler(this.chkUnderLine_CheckedChanged);
            this.chkIta_GSL.Appearance = Appearance.Button;
            this.chkIta_GSL.ImageIndex = 1;
            this.chkIta_GSL.ImageList = this.imageList1;
            this.chkIta_GSL.Location = new Point(0xc2, 0x40);
            this.chkIta_GSL.Name = "chkIta_GSL";
            this.chkIta_GSL.Size = new Size(0x1c, 0x18);
            this.chkIta_GSL.TabIndex = 0x2a;
            this.chkIta_GSL.CheckedChanged += new EventHandler(this.chkIta_CheckedChanged);
            this.chkBold_GSL.Appearance = Appearance.Button;
            this.chkBold_GSL.ImageIndex = 0;
            this.chkBold_GSL.ImageList = this.imageList1;
            this.chkBold_GSL.Location = new Point(0xa2, 0x40);
            this.chkBold_GSL.Name = "chkBold_GSL";
            this.chkBold_GSL.Size = new Size(0x1c, 0x18);
            this.chkBold_GSL.TabIndex = 0x29;
            this.chkBold_GSL.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboGSLFontName.Location = new Point(0x3a, 40);
            this.cboGSLFontName.Name = "cboGSLFontName";
            this.cboGSLFontName.Size = new Size(0xb8, 20);
            this.cboGSLFontName.TabIndex = 40;
            this.cboGSLFontName.SelectedIndexChanged += new EventHandler(this.cboGSLFontName_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.txtTickLength);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.btnTickSymbol);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new Point(0x10, 0x178);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x110, 0x40);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "内部刻度标记";
            bits = new int[4];
            this.txtTickLength.EditValue = new decimal(bits);
            this.txtTickLength.Location = new Point(0xb0, 0x18);
            this.txtTickLength.Name = "txtTickLength";
            this.txtTickLength.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtTickLength.Properties.UseCtrlIncrement = false;
            this.txtTickLength.Size = new Size(40, 0x17);
            this.txtTickLength.TabIndex = 0x38;
            this.txtTickLength.EditValueChanged += new EventHandler(this.txtRowCount_EditValueChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xe0, 0x20);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 0x11);
            this.label12.TabIndex = 0x37;
            this.label12.Text = "点";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x88, 0x20);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x23, 0x11);
            this.label11.TabIndex = 0x36;
            this.label11.Text = "长度:";
            this.btnTickSymbol.Location = new Point(0x40, 0x18);
            this.btnTickSymbol.Name = "btnTickSymbol";
            this.btnTickSymbol.Size = new Size(0x40, 0x18);
            this.btnTickSymbol.TabIndex = 0x35;
            this.btnTickSymbol.Click += new EventHandler(this.btnTickSymbol_Click);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x18, 0x20);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 0x11);
            this.label10.TabIndex = 0x34;
            this.label10.Text = "符号:";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MGRSPropertyPage";
            base.Size = new Size(0x138, 0x1c0);
            base.Load += new EventHandler(this.MGRSPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.chkShowOuterLabelsOnly.Properties.EndInit();
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.txtColumnCount.Properties.EndInit();
            this.txtRowCount.Properties.EndInit();
            this.chkShowLadderLabels.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboGSLStyle.Properties.EndInit();
            this.chkShowGSIdentifiers.Properties.EndInit();
            this.cboGSLFontsize.Properties.EndInit();
            this.colorEdit2.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtTickLength.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MGRSPropertyPage_Load(object sender, EventArgs e)
        {
            int num;
            IGridLadderLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridLadderLabels;
            string name = pMapGrid.LadderLabelFont.Name;
            for (num = 0; num < this.cboFontName.Items.Count; num++)
            {
                if (name == this.cboFontName.Items[num].ToString())
                {
                    this.cboFontName.SelectedIndex = num;
                    break;
                }
            }
            this.cboFontSize.Text = pMapGrid.LadderLabelSize.ToString();
            this.chkBold.Checked = pMapGrid.LadderLabelFont.Bold;
            this.chkIta.Checked = pMapGrid.LadderLabelFont.Italic;
            this.chkUnderLine.Checked = pMapGrid.LadderLabelFont.Underline;
            this.SetColorEdit(this.colorEdit1, pMapGrid.LadderLabelColor);
            this.txtColumnCount.Value = pMapGrid.LadderLabelColumnCount;
            this.txtRowCount.Value = pMapGrid.LadderLabelRowCount;
            this.chkShowLadderLabels.Checked = pMapGrid.ShowLadderLabels;
            this.chkShowOuterLabelsOnly.Checked = pMapGrid.ShowOuterLabelsOnly;
            IMgrsGrid grid = GridAxisPropertyPage.m_pMapGrid as IMgrsGrid;
            name = grid.GridSquareLabelFont.Name;
            for (num = 0; num < this.cboGSLFontName.Items.Count; num++)
            {
                if (name == this.cboGSLFontName.Items[num].ToString())
                {
                    this.cboGSLFontName.SelectedIndex = num;
                    break;
                }
            }
            this.cboGSLFontsize.Text = grid.GridSquareLabelSize.ToString();
            this.chkBold_GSL.Checked = grid.GridSquareLabelFont.Bold;
            this.chkIta_GSL.Checked = grid.GridSquareLabelFont.Italic;
            this.chkUnderLine_GSL.Checked = grid.GridSquareLabelFont.Underline;
            this.SetColorEdit(this.colorEdit2, grid.GridSquareLabelColor);
            this.chkShowGSIdentifiers.Checked = grid.ShowGridSquareIdentifiers;
            this.btnBorder.Style = grid.GridSquareBoundarySymbol;
            this.cboGSLStyle.SelectedIndex = (int) grid.GridSquareLabelStyle;
            this.btnTickSymbol.Style = grid.InteriorTickSymbol;
            this.txtTickLength.Value = (decimal) grid.InteriorTickLength;
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
        }

        private void txtRowCount_EditValueChanged(object sender, EventArgs e)
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

