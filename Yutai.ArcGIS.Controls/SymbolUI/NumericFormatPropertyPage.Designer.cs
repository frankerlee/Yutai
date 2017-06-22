using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class NumericFormatPropertyPage
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
            this.groupBox1 = new GroupBox();
            this.txtRoundingValue = new SpinEdit();
            this.rdoRoundingOption = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.txtAlignmentWidth = new SpinEdit();
            this.rdoAlignmentOption = new RadioGroup();
            this.chkUseSeparator = new CheckEdit();
            this.chkZeroPad = new CheckEdit();
            this.chkShowPlusSign = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.txtRoundingValue.Properties.BeginInit();
            this.rdoRoundingOption.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtAlignmentWidth.Properties.BeginInit();
            this.rdoAlignmentOption.Properties.BeginInit();
            this.chkUseSeparator.Properties.BeginInit();
            this.chkZeroPad.Properties.BeginInit();
            this.chkShowPlusSign.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtRoundingValue);
            this.groupBox1.Controls.Add(this.rdoRoundingOption);
            this.groupBox1.Location = new Point(8, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(184, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "环绕";
            int[] bits = new int[4];
            this.txtRoundingValue.EditValue = new decimal(bits);
            this.txtRoundingValue.Location = new Point(24, 56);
            this.txtRoundingValue.Name = "txtRoundingValue";
            this.txtRoundingValue.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtRoundingValue.Size = new Size(80, 23);
            this.txtRoundingValue.TabIndex = 1;
            this.txtRoundingValue.EditValueChanged += new EventHandler(this.txtRoundingValue_EditValueChanged);
            this.rdoRoundingOption.Location = new Point(16, 12);
            this.rdoRoundingOption.Name = "rdoRoundingOption";
            this.rdoRoundingOption.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoRoundingOption.Properties.Appearance.Options.UseBackColor = true;
            this.rdoRoundingOption.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoRoundingOption.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "保留十进制的个数"), new RadioGroupItem(null, "重要数位个数") });
            this.rdoRoundingOption.Size = new Size(120, 40);
            this.rdoRoundingOption.TabIndex = 0;
            this.rdoRoundingOption.SelectedIndexChanged += new EventHandler(this.rdoRoundingOption_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtAlignmentWidth);
            this.groupBox2.Controls.Add(this.rdoAlignmentOption);
            this.groupBox2.Location = new Point(8, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(184, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "对齐";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(120, 72);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "字符";
            bits = new int[4];
            this.txtAlignmentWidth.EditValue = new decimal(bits);
            this.txtAlignmentWidth.Location = new Point(24, 64);
            this.txtAlignmentWidth.Name = "txtAlignmentWidth";
            this.txtAlignmentWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtAlignmentWidth.Properties.UseCtrlIncrement = false;
            this.txtAlignmentWidth.Size = new Size(80, 23);
            this.txtAlignmentWidth.TabIndex = 2;
            this.txtAlignmentWidth.EditValueChanged += new EventHandler(this.txtAlignmentWidth_EditValueChanged);
            this.rdoAlignmentOption.Location = new Point(16, 16);
            this.rdoAlignmentOption.Name = "rdoAlignmentOption";
            this.rdoAlignmentOption.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoAlignmentOption.Properties.Appearance.Options.UseBackColor = true;
            this.rdoAlignmentOption.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoAlignmentOption.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "左"), new RadioGroupItem(null, "右") });
            this.rdoAlignmentOption.Size = new Size(88, 40);
            this.rdoAlignmentOption.TabIndex = 1;
            this.rdoAlignmentOption.SelectedIndexChanged += new EventHandler(this.rdoAlignmentOption_SelectedIndexChanged);
            this.chkUseSeparator.Location = new Point(8, 200);
            this.chkUseSeparator.Name = "chkUseSeparator";
            this.chkUseSeparator.Properties.Caption = "显示数以千计的分隔符";
            this.chkUseSeparator.Size = new Size(160, 19);
            this.chkUseSeparator.TabIndex = 2;
            this.chkUseSeparator.CheckedChanged += new EventHandler(this.chkUseSeparator_CheckedChanged);
            this.chkZeroPad.Location = new Point(8, 224);
            this.chkZeroPad.Name = "chkZeroPad";
            this.chkZeroPad.Properties.Caption = "以零为基础";
            this.chkZeroPad.Size = new Size(160, 19);
            this.chkZeroPad.TabIndex = 3;
            this.chkZeroPad.CheckedChanged += new EventHandler(this.chkZeroPad_CheckedChanged);
            this.chkShowPlusSign.Location = new Point(8, 248);
            this.chkShowPlusSign.Name = "chkShowPlusSign";
            this.chkShowPlusSign.Properties.Caption = "显示加符号";
            this.chkShowPlusSign.Size = new Size(160, 19);
            this.chkShowPlusSign.TabIndex = 4;
            this.chkShowPlusSign.CheckedChanged += new EventHandler(this.chkShowPlusSign_CheckedChanged);
            base.Controls.Add(this.chkShowPlusSign);
            base.Controls.Add(this.chkZeroPad);
            base.Controls.Add(this.chkUseSeparator);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "NumericFormatPropertyPage";
            base.Size = new Size(208, 280);
            base.Load += new EventHandler(this.NumericFormatPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtRoundingValue.Properties.EndInit();
            this.rdoRoundingOption.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtAlignmentWidth.Properties.EndInit();
            this.rdoAlignmentOption.Properties.EndInit();
            this.chkUseSeparator.Properties.EndInit();
            this.chkZeroPad.Properties.EndInit();
            this.chkShowPlusSign.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private CheckEdit chkShowPlusSign;
        private CheckEdit chkUseSeparator;
        private CheckEdit chkZeroPad;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private RadioGroup rdoAlignmentOption;
        private RadioGroup rdoRoundingOption;
        private SpinEdit txtAlignmentWidth;
        private SpinEdit txtRoundingValue;
        private IAppContext _context;
    }
}