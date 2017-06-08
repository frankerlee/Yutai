using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class TFTextInfoPage : UserControl
    {
        private GroupBox groupBox11;
        private GroupBox groupBox12;
        private GroupBox groupBox14;
        private GroupBox groupBox4;
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;
        private TextBox txtLeftLowTxt;
        private TextBox txtRightLowTxt;
        private TextBox txtRightUpTxt;
        private TextBox txtZTDW;

        public TFTextInfoPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.jlktkassiatant_0.LeftLowText = this.txtLeftLowTxt.Text;
            this.jlktkassiatant_0.RightLowText = this.txtRightLowTxt.Text;
            this.jlktkassiatant_0.RightUpText = this.txtRightUpTxt.Text;
            this.jlktkassiatant_0.LeftBorderOutText = this.txtZTDW.Text;
            return true;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Init()
        {
            this.txtLeftLowTxt.Text = this.jlktkassiatant_0.LeftLowText;
            this.txtRightLowTxt.Text = this.jlktkassiatant_0.RightLowText;
            this.txtRightUpTxt.Text = this.jlktkassiatant_0.RightUpText;
            this.txtZTDW.Text = this.jlktkassiatant_0.LeftBorderOutText;
        }

        private void InitializeComponent()
        {
            this.groupBox4 = new GroupBox();
            this.txtRightLowTxt = new TextBox();
            this.groupBox11 = new GroupBox();
            this.txtLeftLowTxt = new TextBox();
            this.groupBox14 = new GroupBox();
            this.txtRightUpTxt = new TextBox();
            this.groupBox12 = new GroupBox();
            this.txtZTDW = new TextBox();
            this.groupBox4.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox12.SuspendLayout();
            base.SuspendLayout();
            this.groupBox4.Controls.Add(this.txtRightLowTxt);
            this.groupBox4.Location = new Point(11, 0xbd);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x158, 0x69);
            this.groupBox4.TabIndex = 0x3a;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "外图框右下角注释";
            this.txtRightLowTxt.Location = new Point(8, 0x13);
            this.txtRightLowTxt.Multiline = true;
            this.txtRightLowTxt.Name = "txtRightLowTxt";
            this.txtRightLowTxt.Size = new Size(0x13d, 0x4d);
            this.txtRightLowTxt.TabIndex = 4;
            this.groupBox11.Controls.Add(this.txtLeftLowTxt);
            this.groupBox11.Location = new Point(9, 0x4e);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new Size(0x15a, 0x69);
            this.groupBox11.TabIndex = 0x38;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "外图框左下角注释";
            this.txtLeftLowTxt.Location = new Point(8, 0x13);
            this.txtLeftLowTxt.Multiline = true;
            this.txtLeftLowTxt.Name = "txtLeftLowTxt";
            this.txtLeftLowTxt.Size = new Size(0x13f, 0x4d);
            this.txtLeftLowTxt.TabIndex = 4;
            this.groupBox14.Controls.Add(this.txtRightUpTxt);
            this.groupBox14.Location = new Point(9, 15);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(0x15a, 0x39);
            this.groupBox14.TabIndex = 0x39;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "外图框右上角注释";
            this.txtRightUpTxt.Location = new Point(6, 0x1b);
            this.txtRightUpTxt.Name = "txtRightUpTxt";
            this.txtRightUpTxt.Size = new Size(0x141, 0x15);
            this.txtRightUpTxt.TabIndex = 4;
            this.groupBox12.Controls.Add(this.txtZTDW);
            this.groupBox12.Location = new Point(3, 300);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new Size(0x160, 0x2b);
            this.groupBox12.TabIndex = 0x3f;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "制图单位";
            this.txtZTDW.Location = new Point(0x12, 0x10);
            this.txtZTDW.Name = "txtZTDW";
            this.txtZTDW.Size = new Size(0x13b, 0x15);
            this.txtZTDW.TabIndex = 5;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox12);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox11);
            base.Controls.Add(this.groupBox14);
            base.Name = "TFTextInfoPage";
            base.Size = new Size(380, 0x167);
            base.Load += new EventHandler(this.TFTextInfoPage_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            base.ResumeLayout(false);
        }

        private void TFTextInfoPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        internal YTTKAssiatant YTTKAssiatant
        {
            set
            {
                this.jlktkassiatant_0 = value;
            }
        }
    }
}

