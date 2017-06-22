using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class LegendTitleUserControl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendTitleUserControl));
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
            this.groupBox1.Location = new Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(264, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图例标题";
            this.memoEditTitle.EditValue = "";
            this.memoEditTitle.Location = new Point(8, 24);
            this.memoEditTitle.Name = "memoEditTitle";
            this.memoEditTitle.Size = new Size(248, 80);
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
            this.groupBox2.Location = new Point(16, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(272, 128);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图例标题字体属性";
            this.btnUnderline.Image = (Image) resources.GetObject("btnUnderline.Image");
            this.btnUnderline.Location = new Point(112, 96);
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new Size(24, 24);
            this.btnUnderline.TabIndex = 71;
            this.btnUnderline.Visible = false;
            this.btnUnderline.Click += new EventHandler(this.btnUnderline_Click);
            this.btnItalic.Image = (Image) resources.GetObject("btnItalic.Image");
            this.btnItalic.Location = new Point(80, 96);
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new Size(24, 24);
            this.btnItalic.TabIndex = 70;
            this.btnItalic.Visible = false;
            this.btnItalic.Click += new EventHandler(this.btnItalic_Click);
            this.btnBlod.Image = (Image) resources.GetObject("btnBlod.Image");
            this.btnBlod.Location = new Point(48, 96);
            this.btnBlod.Name = "btnBlod";
            this.btnBlod.Size = new Size(24, 24);
            this.btnBlod.TabIndex = 69;
            this.btnBlod.Visible = false;
            this.btnBlod.Click += new EventHandler(this.btnBlod_Click);
            int[] bits = new int[4];
            this.numUpDownSize.EditValue = new decimal(bits);
            this.numUpDownSize.Location = new Point(56, 48);
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits2 = new int[4];
            bits2[0] = 100;
            this.numUpDownSize.Properties.MaxValue = new decimal(bits2);
            this.numUpDownSize.Properties.UseCtrlIncrement = false;
            this.numUpDownSize.Size = new Size(64, 21);
            this.numUpDownSize.TabIndex = 68;
            this.numUpDownSize.TextChanged += new EventHandler(this.numUpDownSize_TextChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(56, 24);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 67;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(16, 24);
            this.label8.Name = "label8";
            this.label8.Size = new Size(29, 17);
            this.label8.TabIndex = 66;
            this.label8.Text = "颜色";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(16, 48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(29, 17);
            this.label6.TabIndex = 65;
            this.label6.Text = "大小";
            this.cboFontName.Location = new Point(56, 72);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(168, 20);
            this.cboFontName.TabIndex = 64;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 75);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 63;
            this.label1.Text = "字体";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendTitleUserControl";
            base.Size = new Size(400, 264);
            base.Load += new EventHandler(this.LegendTitleUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.memoEditTitle.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.numUpDownSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnBlod;
        private SimpleButton btnItalic;
        private SimpleButton btnUnderline;
        private System.Windows.Forms.ComboBox cboFontName;
        private ColorEdit colorEdit1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label6;
        private Label label8;
        private MemoEdit memoEditTitle;
        private SpinEdit numUpDownSize;
    }
}