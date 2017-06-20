using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmTextDisplaySet : Form
    {
        private SimpleButton btnExpression;
        private ComboBoxEdit cboDisplayPrecision;
        private CheckEdit chkAdjustTextOrientation;
        private CheckEdit chkShowSign;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IHatchDefinition ihatchDefinition_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioGroup rdoTextDisplay;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton3;
        private TextEdit txtPrefix;
        private TextEdit txtSuffix;

        public frmTextDisplaySet()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmTextDisplaySet_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextDisplaySet));
            this.groupBox1 = new GroupBox();
            this.btnExpression = new SimpleButton();
            this.txtSuffix = new TextEdit();
            this.label3 = new Label();
            this.txtPrefix = new TextEdit();
            this.label2 = new Label();
            this.rdoTextDisplay = new RadioGroup();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.chkAdjustTextOrientation = new CheckEdit();
            this.chkShowSign = new CheckEdit();
            this.cboDisplayPrecision = new ComboBoxEdit();
            this.label4 = new Label();
            this.simpleButton2 = new SimpleButton();
            this.simpleButton3 = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtSuffix.Properties.BeginInit();
            this.txtPrefix.Properties.BeginInit();
            this.rdoTextDisplay.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkAdjustTextOrientation.Properties.BeginInit();
            this.chkShowSign.Properties.BeginInit();
            this.cboDisplayPrecision.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnExpression);
            this.groupBox1.Controls.Add(this.txtSuffix);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPrefix);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoTextDisplay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x160, 0x90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注文本";
            this.btnExpression.Enabled = false;
            this.btnExpression.Location = new Point(0xb0, 0x68);
            this.btnExpression.Name = "btnExpression";
            this.btnExpression.Size = new Size(0x40, 0x18);
            this.btnExpression.TabIndex = 6;
            this.btnExpression.Text = "表达式...";
            this.txtSuffix.EditValue = "";
            this.txtSuffix.Location = new Point(0x110, 0x48);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new Size(0x38, 0x15);
            this.txtSuffix.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xe0, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "后缀:";
            this.txtPrefix.EditValue = "";
            this.txtPrefix.Location = new Point(160, 0x48);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new Size(0x38, 0x15);
            this.txtPrefix.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(120, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "前缀:";
            this.rdoTextDisplay.Location = new Point(8, 40);
            this.rdoTextDisplay.Name = "rdoTextDisplay";
            this.rdoTextDisplay.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoTextDisplay.Properties.Appearance.Options.UseBackColor = true;
            this.rdoTextDisplay.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoTextDisplay.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "仅标注度量值"), new RadioGroupItem(null, "添加前缀/后缀"), new RadioGroupItem(null, "构造一个文本表达式") });
            this.rdoTextDisplay.Size = new Size(0x98, 0x60);
            this.rdoTextDisplay.TabIndex = 1;
            this.rdoTextDisplay.SelectedIndexChanged += new EventHandler(this.rdoTextDisplay_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "指定用来标注刻度的文本";
            this.groupBox2.Controls.Add(this.chkAdjustTextOrientation);
            this.groupBox2.Controls.Add(this.chkShowSign);
            this.groupBox2.Controls.Add(this.cboDisplayPrecision);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new Point(8, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x160, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其他设置";
            this.chkAdjustTextOrientation.Location = new Point(0x10, 0x40);
            this.chkAdjustTextOrientation.Name = "chkAdjustTextOrientation";
            this.chkAdjustTextOrientation.Properties.Caption = "当线的方向改变时翻转文本";
            this.chkAdjustTextOrientation.Size = new Size(0xa8, 0x13);
            this.chkAdjustTextOrientation.TabIndex = 3;
            this.chkShowSign.Location = new Point(0x10, 0x58);
            this.chkShowSign.Name = "chkShowSign";
            this.chkShowSign.Properties.Caption = "为负度量值显示负标记";
            this.chkShowSign.Size = new Size(160, 0x13);
            this.chkShowSign.TabIndex = 2;
            this.cboDisplayPrecision.EditValue = "0";
            this.cboDisplayPrecision.Location = new Point(0x88, 0x18);
            this.cboDisplayPrecision.Name = "cboDisplayPrecision";
            this.cboDisplayPrecision.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDisplayPrecision.Properties.Items.AddRange(new object[] { "0", "0.0", "0.00", "0.000", "0.0000", "0.00000", "0.000000", "0.0000000", "0.00000000", "0.000000000" });
            this.cboDisplayPrecision.Size = new Size(0xb0, 0x15);
            this.cboDisplayPrecision.TabIndex = 1;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x6b, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "文本中的数字精度:";
            this.simpleButton2.DialogResult = DialogResult.OK;
            this.simpleButton2.Location = new Point(0xd8, 0x120);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "确定";
            this.simpleButton2.Click += new EventHandler(this.simpleButton2_Click);
            this.simpleButton3.DialogResult = DialogResult.Cancel;
            this.simpleButton3.Location = new Point(0x120, 0x120);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x38, 0x18);
            this.simpleButton3.TabIndex = 8;
            this.simpleButton3.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x180, 0x145);
            base.Controls.Add(this.simpleButton3);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmTextDisplaySet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "标注设置";
            base.Load += new EventHandler(this.frmTextDisplaySet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtSuffix.Properties.EndInit();
            this.txtPrefix.Properties.EndInit();
            this.rdoTextDisplay.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.chkAdjustTextOrientation.Properties.EndInit();
            this.chkShowSign.Properties.EndInit();
            this.cboDisplayPrecision.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            if (this.ihatchDefinition_0 != null)
            {
                this.chkAdjustTextOrientation.Checked = this.ihatchDefinition_0.AdjustTextOrientation;
                this.chkShowSign.Checked = this.ihatchDefinition_0.ShowSign;
                this.rdoTextDisplay.SelectedIndex = (int) this.ihatchDefinition_0.TextDisplay;
                this.cboDisplayPrecision.SelectedIndex = this.ihatchDefinition_0.DisplayPrecision;
            }
        }

        private void rdoTextDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoTextDisplay.SelectedIndex == 0)
            {
                this.txtPrefix.Enabled = false;
                this.txtSuffix.Enabled = false;
                this.btnExpression.Enabled = false;
                this.cboDisplayPrecision.Enabled = true;
                this.chkShowSign.Enabled = true;
            }
            else if (this.rdoTextDisplay.SelectedIndex == 1)
            {
                this.txtPrefix.Enabled = true;
                this.txtSuffix.Enabled = true;
                this.btnExpression.Enabled = false;
                this.txtPrefix.Text = this.ihatchDefinition_0.Prefix;
                this.txtSuffix.Text = this.ihatchDefinition_0.Suffix;
                this.cboDisplayPrecision.Enabled = true;
                this.chkShowSign.Enabled = true;
            }
            else if (this.rdoTextDisplay.SelectedIndex == 2)
            {
                this.txtPrefix.Enabled = false;
                this.txtSuffix.Enabled = false;
                this.btnExpression.Enabled = true;
                this.cboDisplayPrecision.Enabled = false;
                this.chkShowSign.Enabled = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.ihatchDefinition_0.AdjustTextOrientation = this.chkAdjustTextOrientation.Checked;
            this.ihatchDefinition_0.ShowSign = this.chkShowSign.Checked;
            this.ihatchDefinition_0.TextDisplay = (esriHatchTextDisplay) this.rdoTextDisplay.SelectedIndex;
            this.ihatchDefinition_0.DisplayPrecision = this.cboDisplayPrecision.SelectedIndex;
        }

        public IHatchDefinition HatchDefinition
        {
            get
            {
                return this.ihatchDefinition_0;
            }
            set
            {
                this.ihatchDefinition_0 = value;
            }
        }
    }
}

