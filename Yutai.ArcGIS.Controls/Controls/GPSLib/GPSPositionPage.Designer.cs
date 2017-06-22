using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    partial class GPSPositionPage
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
            this.lblLat = new Label();
            this.label4 = new Label();
            this.lblLong = new Label();
            this.label1 = new Label();
            this.lblSpeed = new Label();
            this.label6 = new Label();
            this.lblAlti = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.label10 = new Label();
            this.lblHeading = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblHeading);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblSpeed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblAlti);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblLat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblLong);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(209, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.lblLat.AutoSize = true;
            this.lblLat.Location = new Point(49, 49);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new Size(23, 12);
            this.lblLat.TabIndex = 3;
            this.lblLat.Text = "N/A";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 49);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "纬度";
            this.lblLong.AutoSize = true;
            this.lblLong.Location = new Point(49, 27);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new Size(23, 12);
            this.lblLong.TabIndex = 1;
            this.lblLong.Text = "N/A";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "经度";
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new Point(49, 93);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new Size(23, 12);
            this.lblSpeed.TabIndex = 7;
            this.lblSpeed.Text = "N/A";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(14, 93);
            this.label6.Name = "label6";
            this.label6.Size = new Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "速度";
            this.lblAlti.AutoSize = true;
            this.lblAlti.Location = new Point(49, 71);
            this.lblAlti.Name = "lblAlti";
            this.lblAlti.Size = new Size(23, 12);
            this.lblAlti.TabIndex = 5;
            this.lblAlti.Text = "N/A";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(14, 71);
            this.label8.Name = "label8";
            this.label8.Size = new Size(29, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "高度";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(110, 93);
            this.label9.Name = "label9";
            this.label9.Size = new Size(59, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "公里/小时";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(110, 71);
            this.label10.Name = "label10";
            this.label10.Size = new Size(17, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "米";
            this.lblHeading.AutoSize = true;
            this.lblHeading.Location = new Point(49, 120);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new Size(23, 12);
            this.lblHeading.TabIndex = 9;
            this.lblHeading.Text = "N/A";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(14, 120);
            this.label12.Name = "label12";
            this.label12.Size = new Size(47, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "Heading";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(110, 120);
            this.label13.Name = "label13";
            this.label13.Size = new Size(17, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "度";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox1);
            base.Name = "GPSPositionPage";
            base.Size = new Size(239, 176);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    
        private IContainer components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label10;
        private Label label12;
        private Label label13;
        private Label label4;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label lblAlti;
        private Label lblHeading;
        private Label lblLat;
        private Label lblLong;
        private Label lblSpeed;
    }
}