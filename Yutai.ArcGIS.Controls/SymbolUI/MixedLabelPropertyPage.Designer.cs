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
    partial class MixedLabelPropertyPage
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            this.groupBox1.Location = new Point(8, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(256, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "二级字体";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(10, 48);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "大小:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "颜色:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(50, 48);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(64, 23);
            this.cboFontSize.TabIndex = 36;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(50, 80);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 35;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.chkUnderLine.Appearance = Appearance.Button;
            this.chkUnderLine.ImageIndex = 2;
            this.chkUnderLine.ImageList = this.imageList1;
            this.chkUnderLine.Location = new Point(192, 48);
            this.chkUnderLine.Name = "chkUnderLine";
            this.chkUnderLine.Size = new Size(28, 24);
            this.chkUnderLine.TabIndex = 34;
            this.chkUnderLine.CheckedChanged += new EventHandler(this.chkUnderLine_CheckedChanged);
            this.imageList1.ImageSize = new Size(16, 16);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.chkIta.Appearance = Appearance.Button;
            this.chkIta.ImageIndex = 1;
            this.chkIta.ImageList = this.imageList1;
            this.chkIta.Location = new Point(160, 48);
            this.chkIta.Name = "chkIta";
            this.chkIta.Size = new Size(28, 24);
            this.chkIta.TabIndex = 33;
            this.chkIta.CheckedChanged += new EventHandler(this.chkIta_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(128, 48);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(28, 24);
            this.chkBold.TabIndex = 32;
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboFontName.Location = new Point(50, 24);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(184, 20);
            this.cboFontName.TabIndex = 31;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.radioGroup.Location = new Point(16, 16);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "根据小数位分组"), new RadioGroupItem(null, "指定组中的小数位数") });
            this.radioGroup.Size = new Size(136, 48);
            this.radioGroup.TabIndex = 1;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            int[] bits = new int[4];
            this.txtNumGroupedDigits.EditValue = new decimal(bits);
            this.txtNumGroupedDigits.Location = new Point(40, 72);
            this.txtNumGroupedDigits.Name = "txtNumGroupedDigits";
            this.txtNumGroupedDigits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtNumGroupedDigits.Properties.Enabled = false;
            this.txtNumGroupedDigits.Properties.UseCtrlIncrement = false;
            this.txtNumGroupedDigits.Size = new Size(80, 23);
            this.txtNumGroupedDigits.TabIndex = 2;
            this.txtNumGroupedDigits.EditValueChanged += new EventHandler(this.txtNumGroupedDigits_EditValueChanged);
            this.btnNumberFormat.Location = new Point(56, 240);
            this.btnNumberFormat.Name = "btnNumberFormat";
            this.btnNumberFormat.Size = new Size(72, 24);
            this.btnNumberFormat.TabIndex = 3;
            this.btnNumberFormat.Text = "数字格式";
            this.btnNumberFormat.Click += new EventHandler(this.btnNumberFormat_Click);
            base.Controls.Add(this.btnNumberFormat);
            base.Controls.Add(this.txtNumGroupedDigits);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.groupBox1);
            base.Name = "MixedLabelPropertyPage";
            base.Size = new Size(288, 288);
            base.Load += new EventHandler(this.MixedLabelPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.radioGroup.Properties.EndInit();
            this.txtNumGroupedDigits.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components;
        private SimpleButton btnNumberFormat;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private CheckBox chkBold;
        private CheckBox chkIta;
        private CheckBox chkUnderLine;
        private ColorEdit colorEdit1;
        private GroupBox groupBox1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label4;
        private RadioGroup radioGroup;
        private SpinEdit txtNumGroupedDigits;
    }
}