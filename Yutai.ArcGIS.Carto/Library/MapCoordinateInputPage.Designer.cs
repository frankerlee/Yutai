using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class MapCoordinateInputPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtY = new TextBox();
            this.txtX = new TextBox();
            this.label2 = new Label();
            this.label1 = new Label();
            base.SuspendLayout();
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(23, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(119, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "图幅内任一点坐标";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(23, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(107, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "图幅左下角坐标";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(205, 75);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "米";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(208, 112);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "米";
            this.txtY.Location = new Point(68, 108);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(131, 21);
            this.txtY.TabIndex = 28;
            this.txtX.Location = new Point(68, 72);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(131, 21);
            this.txtX.TabIndex = 27;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(23, 111);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "纵坐标";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(21, 75);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "横坐标";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtY);
            base.Controls.Add(this.txtX);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Name = "MapCoordinateInputPage";
            base.Size = new Size(271, 269);
            base.Load += new EventHandler(this.MapCoordinateInputPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox txtX;
        private TextBox txtY;
    }
}