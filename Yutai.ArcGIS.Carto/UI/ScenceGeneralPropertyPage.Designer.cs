using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class ScenceGeneralPropertyPage
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
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.comboBox1 = new ComboBox();
            this.button1 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "描述";
            this.textBox1.Location = new Point(5, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(274, 117);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 147);
            this.label2.Name = "label2";
            this.label2.Size = new Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "竖直方向夸大因子";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "0", "0.1", "0.5", "1", "1.5", "2", "5", "10" });
            this.comboBox1.Location = new Point(110, 144);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(65, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "1";
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.button1.Location = new Point(181, 142);
            this.button1.Name = "button1";
            this.button1.Size = new Size(98, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "根据场景计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "ScenceGeneralPropertyPage";
            base.Size = new Size(291, 229);
            base.Load += new EventHandler(this.ScenceGeneralPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
    }
}