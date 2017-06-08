using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class FlowShowScalePropertyPage : UserControl
    {
        private Container components = null;
        private Label label1;
        private Label label2;
        private RadioButton rdoNoScale;
        private RadioButton rdoScaleSet;
        private TextBox txtMaxScale;
        private TextBox txtMinScale;

        public FlowShowScalePropertyPage()
        {
            this.InitializeComponent();
        }

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
            this.rdoNoScale = new RadioButton();
            this.rdoScaleSet = new RadioButton();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtMinScale = new TextBox();
            this.txtMaxScale = new TextBox();
            base.SuspendLayout();
            this.rdoNoScale.Checked = true;
            this.rdoNoScale.Location = new System.Drawing.Point(0x10, 0x18);
            this.rdoNoScale.Name = "rdoNoScale";
            this.rdoNoScale.Size = new Size(0x88, 0x18);
            this.rdoNoScale.TabIndex = 0;
            this.rdoNoScale.TabStop = true;
            this.rdoNoScale.Text = "所有比例尺均显示";
            this.rdoNoScale.CheckedChanged += new EventHandler(this.rdoNoScale_CheckedChanged);
            this.rdoScaleSet.Location = new System.Drawing.Point(0x10, 0x30);
            this.rdoScaleSet.Name = "rdoScaleSet";
            this.rdoScaleSet.Size = new Size(120, 0x18);
            this.rdoScaleSet.TabIndex = 1;
            this.rdoScaleSet.Text = "不显示箭头，当";
            this.rdoScaleSet.CheckedChanged += new EventHandler(this.rdoScaleSet_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 90);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "缩小超过1:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x7a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "放大超过1:";
            this.txtMinScale.Enabled = false;
            this.txtMinScale.Location = new System.Drawing.Point(0x58, 0x58);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(0x58, 0x15);
            this.txtMinScale.TabIndex = 4;
            this.txtMinScale.Text = "";
            this.txtMaxScale.Enabled = false;
            this.txtMaxScale.Location = new System.Drawing.Point(0x58, 120);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(0x58, 0x15);
            this.txtMaxScale.TabIndex = 5;
            this.txtMaxScale.Text = "";
            this.txtMaxScale.TextChanged += new EventHandler(this.txtMaxScale_TextChanged);
            base.Controls.Add(this.txtMaxScale);
            base.Controls.Add(this.txtMinScale);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoScaleSet);
            base.Controls.Add(this.rdoNoScale);
            base.Name = "FlowShowScalePropertyPage";
            base.Size = new Size(0xd8, 0xd0);
            base.ResumeLayout(false);
        }

        private void rdoNoScale_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMaxScale.Enabled = false;
            this.txtMinScale.Enabled = false;
        }

        private void rdoScaleSet_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMaxScale.Enabled = true;
            this.txtMinScale.Enabled = true;
        }

        private void txtMaxScale_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

