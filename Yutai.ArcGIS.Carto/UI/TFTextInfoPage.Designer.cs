using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TFTextInfoPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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
            this.groupBox4.Location = new Point(11, 189);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(344, 105);
            this.groupBox4.TabIndex = 58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "外图框右下角注释";
            this.txtRightLowTxt.Location = new Point(8, 19);
            this.txtRightLowTxt.Multiline = true;
            this.txtRightLowTxt.Name = "txtRightLowTxt";
            this.txtRightLowTxt.Size = new Size(317, 77);
            this.txtRightLowTxt.TabIndex = 4;
            this.groupBox11.Controls.Add(this.txtLeftLowTxt);
            this.groupBox11.Location = new Point(9, 78);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new Size(346, 105);
            this.groupBox11.TabIndex = 56;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "外图框左下角注释";
            this.txtLeftLowTxt.Location = new Point(8, 19);
            this.txtLeftLowTxt.Multiline = true;
            this.txtLeftLowTxt.Name = "txtLeftLowTxt";
            this.txtLeftLowTxt.Size = new Size(319, 77);
            this.txtLeftLowTxt.TabIndex = 4;
            this.groupBox14.Controls.Add(this.txtRightUpTxt);
            this.groupBox14.Location = new Point(9, 15);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(346, 57);
            this.groupBox14.TabIndex = 57;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "外图框右上角注释";
            this.txtRightUpTxt.Location = new Point(6, 27);
            this.txtRightUpTxt.Name = "txtRightUpTxt";
            this.txtRightUpTxt.Size = new Size(321, 21);
            this.txtRightUpTxt.TabIndex = 4;
            this.groupBox12.Controls.Add(this.txtZTDW);
            this.groupBox12.Location = new Point(3, 300);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new Size(352, 43);
            this.groupBox12.TabIndex = 63;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "制图单位";
            this.txtZTDW.Location = new Point(18, 16);
            this.txtZTDW.Name = "txtZTDW";
            this.txtZTDW.Size = new Size(315, 21);
            this.txtZTDW.TabIndex = 5;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox12);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox11);
            base.Controls.Add(this.groupBox14);
            base.Name = "TFTextInfoPage";
            base.Size = new Size(380, 359);
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

       
        private GroupBox groupBox11;
        private GroupBox groupBox12;
        private GroupBox groupBox14;
        private GroupBox groupBox4;
        private TextBox txtLeftLowTxt;
        private TextBox txtRightLowTxt;
        private TextBox txtRightUpTxt;
        private TextBox txtZTDW;
    }
}