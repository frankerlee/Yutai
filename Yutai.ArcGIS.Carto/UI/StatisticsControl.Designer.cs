using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class StatisticsControl
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
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox4 = new TextBox();
            this.label4 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小值";
            this.textBox1.Location = new Point(50, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 21);
            this.textBox1.TabIndex = 1;
            this.textBox2.Location = new Point(50, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(100, 21);
            this.textBox2.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "最大值";
            this.textBox3.Location = new Point(50, 69);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(100, 21);
            this.textBox3.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 72);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "平均值";
            this.textBox4.Location = new Point(50, 98);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(100, 21);
            this.textBox4.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(3, 101);
            this.label4.Name = "label4";
            this.label4.Size = new Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "标准差";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.textBox4);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "StatisticsControl";
            base.Size = new Size(168, 139);
            base.Load += new EventHandler(this.StatisticsControl_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
    }
}