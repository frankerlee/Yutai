using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmViewerOrMagnifierPropertyPage
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
            this.groupBox1.Size = new Size(250, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模式";
            this.rdoMagnifier.AutoSize = true;
            this.rdoMagnifier.Location = new Point(7, 21);
            this.rdoMagnifier.Name = "rdoMagnifier";
            this.rdoMagnifier.Size = new Size(59, 16);
            this.rdoMagnifier.TabIndex = 0;
            this.rdoMagnifier.TabStop = true;
            this.rdoMagnifier.Text = "放大镜";
            this.rdoMagnifier.UseVisualStyleBackColor = true;
            this.rdoMagnifier.CheckedChanged += new EventHandler(this.rdoMagnifier_CheckedChanged);
            this.rdoViewer.AutoSize = true;
            this.rdoViewer.Location = new Point(7, 54);
            this.rdoViewer.Name = "rdoViewer";
            this.rdoViewer.Size = new Size(47, 16);
            this.rdoViewer.TabIndex = 1;
            this.rdoViewer.TabStop = true;
            this.rdoViewer.Text = "窗口";
            this.rdoViewer.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.cboFixedScale);
            this.groupBox2.Controls.Add(this.cboMagnify);
            this.groupBox2.Controls.Add(this.rdoFixedScale);
            this.groupBox2.Controls.Add(this.rdoMagnifyBy);
            this.groupBox2.Location = new Point(8, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(254, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缩放";
            this.rdoFixedScale.AutoSize = true;
            this.rdoFixedScale.Location = new Point(7, 54);
            this.rdoFixedScale.Name = "rdoFixedScale";
            this.rdoFixedScale.Size = new Size(71, 16);
            this.rdoFixedScale.TabIndex = 1;
            this.rdoFixedScale.TabStop = true;
            this.rdoFixedScale.Text = "固定比例";
            this.rdoFixedScale.UseVisualStyleBackColor = true;
            this.rdoMagnifyBy.AutoSize = true;
            this.rdoMagnifyBy.Location = new Point(7, 21);
            this.rdoMagnifyBy.Name = "rdoMagnifyBy";
            this.rdoMagnifyBy.Size = new Size(71, 16);
            this.rdoMagnifyBy.TabIndex = 0;
            this.rdoMagnifyBy.TabStop = true;
            this.rdoMagnifyBy.Text = "放大倍数";
            this.rdoMagnifyBy.UseVisualStyleBackColor = true;
            this.rdoMagnifyBy.CheckedChanged += new EventHandler(this.rdoMagnifyBy_CheckedChanged);
            this.cboMagnify.FormattingEnabled = true;
            this.cboMagnify.Items.AddRange(new object[] { "200%", "400%", "600%", "800%", "1000%" });
            this.cboMagnify.Location = new Point(85, 16);
            this.cboMagnify.Name = "cboMagnify";
            this.cboMagnify.Size = new Size(121, 20);
            this.cboMagnify.TabIndex = 2;
            this.cboMagnify.Text = "200%";
            this.cboFixedScale.FormattingEnabled = true;
            this.cboFixedScale.Items.AddRange(new object[] { "<NONE>", "1:1000", "1:10000", "1:24000", "1:100000", "1:250000", "1:500000", "1:750000", "1:1000000", "1:5000000", "1:10000000" });
            this.cboFixedScale.Location = new Point(85, 54);
            this.cboFixedScale.Name = "cboFixedScale";
            this.cboFixedScale.Size = new Size(121, 20);
            this.cboFixedScale.TabIndex = 3;
            this.cboFixedScale.Text = "1:1000";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(80, 183);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(175, 183);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(272, 234);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.Name = "frmViewerOrMagnifierPropertyPage";
            this.Text = "属性";
            base.Load += new EventHandler(this.frmViewerOrMagnifierPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnOK;
        private Button button2;
        private ComboBox cboFixedScale;
        private ComboBox cboMagnify;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton rdoFixedScale;
        private RadioButton rdoMagnifier;
        private RadioButton rdoMagnifyBy;
        private RadioButton rdoViewer;
    }
}