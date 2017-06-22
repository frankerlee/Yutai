using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.DesignLib;

namespace Yutai.ArcGIS.Carto.UI
{
   
    partial class TFInfoPage
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtTM = new TextBox();
            this.txtTH = new TextBox();
            this.groupSR = new GroupBox();
            this.cboDataum = new ComboBox();
            this.txtYOffset = new TextBox();
            this.rdo6 = new RadioButton();
            this.rdo3 = new RadioButton();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.txtYInterval = new TextBox();
            this.txtXInterval = new TextBox();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtStartMultiple = new TextBox();
            this.label9 = new Label();
            this.panel1 = new Panel();
            this.groupSR.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图名";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "图号";
            this.txtTM.Location = new Point(88, 20);
            this.txtTM.Name = "txtTM";
            this.txtTM.Size = new Size(190, 21);
            this.txtTM.TabIndex = 2;
            this.txtTH.Location = new Point(88, 47);
            this.txtTH.Name = "txtTH";
            this.txtTH.Size = new Size(190, 21);
            this.txtTH.TabIndex = 3;
            this.groupSR.Controls.Add(this.cboDataum);
            this.groupSR.Controls.Add(this.txtYOffset);
            this.groupSR.Controls.Add(this.rdo6);
            this.groupSR.Controls.Add(this.rdo3);
            this.groupSR.Controls.Add(this.label4);
            this.groupSR.Controls.Add(this.label3);
            this.groupSR.Location = new Point(11, 74);
            this.groupSR.Name = "groupSR";
            this.groupSR.Size = new Size(289, 120);
            this.groupSR.TabIndex = 4;
            this.groupSR.TabStop = false;
            this.groupSR.Text = "投影坐标系统参数";
            this.cboDataum.FormattingEnabled = true;
            this.cboDataum.Items.AddRange(new object[] { "北京54", "西安80" });
            this.cboDataum.Location = new Point(99, 28);
            this.cboDataum.Name = "cboDataum";
            this.cboDataum.Size = new Size(165, 20);
            this.cboDataum.TabIndex = 5;
            this.cboDataum.Text = "西安80";
            this.txtYOffset.Location = new Point(99, 56);
            this.txtYOffset.Name = "txtYOffset";
            this.txtYOffset.Size = new Size(166, 21);
            this.txtYOffset.TabIndex = 4;
            this.txtYOffset.Visible = false;
            this.rdo6.AutoSize = true;
            this.rdo6.Location = new Point(139, 82);
            this.rdo6.Name = "rdo6";
            this.rdo6.Size = new Size(65, 16);
            this.rdo6.TabIndex = 3;
            this.rdo6.Text = "6度分带";
            this.rdo6.UseVisualStyleBackColor = true;
            this.rdo3.AutoSize = true;
            this.rdo3.Checked = true;
            this.rdo3.Location = new Point(18, 86);
            this.rdo3.Name = "rdo3";
            this.rdo3.Size = new Size(65, 16);
            this.rdo3.TabIndex = 2;
            this.rdo3.TabStop = true;
            this.rdo3.Text = "3度分带";
            this.rdo3.UseVisualStyleBackColor = true;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 61);
            this.label4.Name = "label4";
            this.label4.Size = new Size(83, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "坐标偏移参数:";
            this.label4.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 32);
            this.label3.Name = "label3";
            this.label3.Size = new Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "参考椭球:";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(270, 57);
            this.label6.Name = "label6";
            this.label6.Size = new Size(17, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(270, 15);
            this.label5.Name = "label5";
            this.label5.Size = new Size(17, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "米";
            this.txtYInterval.Location = new Point(97, 48);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new Size(165, 21);
            this.txtYInterval.TabIndex = 17;
            this.txtXInterval.Location = new Point(97, 13);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new Size(165, 21);
            this.txtXInterval.TabIndex = 16;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(4, 51);
            this.label7.Name = "label7";
            this.label7.Size = new Size(77, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "格网竖直间距";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(4, 22);
            this.label8.Name = "label8";
            this.label8.Size = new Size(77, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "格网水平间距";
            this.txtStartMultiple.Location = new Point(99, 87);
            this.txtStartMultiple.Name = "txtStartMultiple";
            this.txtStartMultiple.Size = new Size(163, 21);
            this.txtStartMultiple.TabIndex = 21;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(4, 90);
            this.label9.Name = "label9";
            this.label9.Size = new Size(89, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "起始点坐标倍数";
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtStartMultiple);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtXInterval);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtYInterval);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new Point(13, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(304, 126);
            this.panel1.TabIndex = 22;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.groupSR);
            base.Controls.Add(this.txtTH);
            base.Controls.Add(this.txtTM);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TFInfoPage";
            base.Size = new Size(338, 341);
            base.Load += new EventHandler(this.TFInfoPage_Load);
            this.groupSR.ResumeLayout(false);
            this.groupSR.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBox cboDataum;
        private GroupBox groupSR;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private RadioButton rdo3;
        private RadioButton rdo6;
        private TextBox txtStartMultiple;
        private TextBox txtTH;
        private TextBox txtTM;
        private TextBox txtXInterval;
        private TextBox txtYInterval;
        private TextBox txtYOffset;
    }
}