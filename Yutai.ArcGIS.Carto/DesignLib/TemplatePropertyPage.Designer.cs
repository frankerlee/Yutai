using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class TemplatePropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.txtWidth = new TextBox();
            this.label12 = new Label();
            this.txtScale = new TextBox();
            this.label11 = new Label();
            this.txtHeight = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label5 = new Label();
            this.textBox3 = new TextBox();
            this.label6 = new Label();
            this.button1 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(14, 41);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "描述";
            this.textBox1.Location = new Point(61, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(185, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.textBox2.Location = new Point(61, 41);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(185, 83);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtWidth.Location = new Point(61, 157);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(140, 21);
            this.txtWidth.TabIndex = 76;
            this.txtWidth.Text = "50";
            this.txtWidth.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(14, 160);
            this.label12.Name = "label12";
            this.label12.Size = new Size(17, 12);
            this.label12.TabIndex = 75;
            this.label12.Text = "宽";
            this.txtScale.Location = new Point(61, 133);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(185, 21);
            this.txtScale.TabIndex = 74;
            this.txtScale.Text = "0";
            this.txtScale.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(14, 136);
            this.label11.Name = "label11";
            this.label11.Size = new Size(41, 12);
            this.label11.TabIndex = 73;
            this.label11.Text = "比例尺";
            this.txtHeight.Location = new Point(61, 186);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(140, 21);
            this.txtHeight.TabIndex = 78;
            this.txtHeight.Text = "50";
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 186);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 77;
            this.label4.Text = "高";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(207, 160);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 79;
            this.label3.Text = "厘米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(207, 189);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 12);
            this.label5.TabIndex = 80;
            this.label5.Text = "厘米";
            this.textBox3.Location = new Point(61, 213);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(160, 21);
            this.textBox3.TabIndex = 82;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(10, 220);
            this.label6.Name = "label6";
            this.label6.Size = new Size(29, 12);
            this.label6.TabIndex = 81;
            this.label6.Text = "图例";
            this.button1.Location = new Point(227, 213);
            this.button1.Name = "button1";
            this.button1.Size = new Size(34, 19);
            this.button1.TabIndex = 83;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtHeight);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.txtScale);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TemplatePropertyPage";
            base.Size = new Size(284, 264);
            base.Load += new EventHandler(this.TemplatePropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Button button1;
        private Label label1;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox txtHeight;
        private TextBox txtScale;
        private TextBox txtWidth;
    }
}