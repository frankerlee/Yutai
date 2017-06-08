using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    internal class frmViewerOrMagnifierPropertyPage : Form
    {
        private Button btnOK;
        private Button button2;
        private ComboBox cboFixedScale;
        private ComboBox cboMagnify;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private MagnifierOrViewerProperty m_proprty = null;
        private RadioButton rdoFixedScale;
        private RadioButton rdoMagnifier;
        private RadioButton rdoMagnifyBy;
        private RadioButton rdoViewer;

        public frmViewerOrMagnifierPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoMagnifier.Checked)
            {
                this.m_proprty.m_Formtype = FormType.Magnifier;
            }
            else
            {
                this.m_proprty.m_Formtype = FormType.Viewer;
            }
            if (this.rdoMagnifyBy.Checked)
            {
                this.m_proprty.m_ZoomType = ZoomType.MagnifyBy;
                this.m_proprty.m_magnify = this.cboMagnify.Text;
            }
            else
            {
                this.m_proprty.m_ZoomType = ZoomType.FixedScale;
                this.m_proprty.m_scale = this.cboFixedScale.Text;
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

        private void frmViewerOrMagnifierPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_proprty.m_Formtype == FormType.Magnifier)
            {
                this.rdoMagnifier.Checked = true;
                this.rdoViewer.Checked = false;
            }
            else
            {
                this.rdoViewer.Checked = true;
                this.rdoMagnifier.Checked = false;
            }
            if (this.m_proprty.m_ZoomType == ZoomType.FixedScale)
            {
                this.rdoFixedScale.Checked = true;
                this.rdoMagnifyBy.Checked = false;
                this.cboFixedScale.Text = this.m_proprty.m_scale;
                this.cboMagnify.Text = this.m_proprty.m_magnify;
                this.cboFixedScale.Enabled = true;
                this.cboMagnify.Enabled = false;
            }
            else
            {
                this.rdoMagnifyBy.Checked = true;
                this.rdoFixedScale.Checked = false;
                this.cboMagnify.Text = this.m_proprty.m_magnify;
                this.cboFixedScale.Text = this.m_proprty.m_scale;
                this.cboFixedScale.Enabled = false;
                this.cboMagnify.Enabled = true;
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.rdoMagnifier = new RadioButton();
            this.rdoViewer = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.rdoFixedScale = new RadioButton();
            this.rdoMagnifyBy = new RadioButton();
            this.cboMagnify = new ComboBox();
            this.cboFixedScale = new ComboBox();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoViewer);
            this.groupBox1.Controls.Add(this.rdoMagnifier);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(250, 0x51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模式";
            this.rdoMagnifier.AutoSize = true;
            this.rdoMagnifier.Location = new Point(7, 0x15);
            this.rdoMagnifier.Name = "rdoMagnifier";
            this.rdoMagnifier.Size = new Size(0x3b, 0x10);
            this.rdoMagnifier.TabIndex = 0;
            this.rdoMagnifier.TabStop = true;
            this.rdoMagnifier.Text = "放大镜";
            this.rdoMagnifier.UseVisualStyleBackColor = true;
            this.rdoMagnifier.CheckedChanged += new EventHandler(this.rdoMagnifier_CheckedChanged);
            this.rdoViewer.AutoSize = true;
            this.rdoViewer.Location = new Point(7, 0x36);
            this.rdoViewer.Name = "rdoViewer";
            this.rdoViewer.Size = new Size(0x2f, 0x10);
            this.rdoViewer.TabIndex = 1;
            this.rdoViewer.TabStop = true;
            this.rdoViewer.Text = "窗口";
            this.rdoViewer.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.cboFixedScale);
            this.groupBox2.Controls.Add(this.cboMagnify);
            this.groupBox2.Controls.Add(this.rdoFixedScale);
            this.groupBox2.Controls.Add(this.rdoMagnifyBy);
            this.groupBox2.Location = new Point(8, 0x60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xfe, 0x51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缩放";
            this.rdoFixedScale.AutoSize = true;
            this.rdoFixedScale.Location = new Point(7, 0x36);
            this.rdoFixedScale.Name = "rdoFixedScale";
            this.rdoFixedScale.Size = new Size(0x47, 0x10);
            this.rdoFixedScale.TabIndex = 1;
            this.rdoFixedScale.TabStop = true;
            this.rdoFixedScale.Text = "固定比例";
            this.rdoFixedScale.UseVisualStyleBackColor = true;
            this.rdoMagnifyBy.AutoSize = true;
            this.rdoMagnifyBy.Location = new Point(7, 0x15);
            this.rdoMagnifyBy.Name = "rdoMagnifyBy";
            this.rdoMagnifyBy.Size = new Size(0x47, 0x10);
            this.rdoMagnifyBy.TabIndex = 0;
            this.rdoMagnifyBy.TabStop = true;
            this.rdoMagnifyBy.Text = "放大倍数";
            this.rdoMagnifyBy.UseVisualStyleBackColor = true;
            this.rdoMagnifyBy.CheckedChanged += new EventHandler(this.rdoMagnifyBy_CheckedChanged);
            this.cboMagnify.FormattingEnabled = true;
            this.cboMagnify.Items.AddRange(new object[] { "200%", "400%", "600%", "800%", "1000%" });
            this.cboMagnify.Location = new Point(0x55, 0x10);
            this.cboMagnify.Name = "cboMagnify";
            this.cboMagnify.Size = new Size(0x79, 20);
            this.cboMagnify.TabIndex = 2;
            this.cboMagnify.Text = "200%";
            this.cboFixedScale.FormattingEnabled = true;
            this.cboFixedScale.Items.AddRange(new object[] { "<NONE>", "1:1000", "1:10000", "1:24000", "1:100000", "1:250000", "1:500000", "1:750000", "1:1000000", "1:5000000", "1:10000000" });
            this.cboFixedScale.Location = new Point(0x55, 0x36);
            this.cboFixedScale.Name = "cboFixedScale";
            this.cboFixedScale.Size = new Size(0x79, 20);
            this.cboFixedScale.TabIndex = 3;
            this.cboFixedScale.Text = "1:1000";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(80, 0xb7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0xaf, 0xb7);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x110, 0xea);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "frmViewerOrMagnifierPropertyPage";
            this.Text = "属性";
            base.Load += new EventHandler(this.frmViewerOrMagnifierPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void rdoMagnifier_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoMagnifier.Checked)
            {
                this.rdoMagnifyBy.Enabled = true;
                this.cboMagnify.Enabled = true;
            }
            else
            {
                this.rdoMagnifyBy.Enabled = false;
                this.cboMagnify.Enabled = false;
                if (this.rdoMagnifyBy.Checked)
                {
                    this.rdoFixedScale.Checked = true;
                }
            }
        }

        private void rdoMagnifyBy_CheckedChanged(object sender, EventArgs e)
        {
            this.cboFixedScale.Enabled = !this.rdoMagnifyBy.Checked;
            this.cboMagnify.Enabled = this.rdoMagnifyBy.Checked;
        }

        internal MagnifierOrViewerProperty Property
        {
            set
            {
                this.m_proprty = value;
            }
        }
    }
}

