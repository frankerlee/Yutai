using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class TextElementValueSetPage
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
            this.checkBox1 = new CheckBox();
            this.btnExpress = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本值";
            this.textBox1.Location = new Point(15, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(237, 60);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(17, 91);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(72, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "竖向文本";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.btnExpress.Location = new Point(177, 87);
            this.btnExpress.Name = "btnExpress";
            this.btnExpress.Size = new Size(75, 23);
            this.btnExpress.TabIndex = 6;
            this.btnExpress.Text = "表达式";
            this.btnExpress.UseVisualStyleBackColor = true;
            this.btnExpress.Click += new EventHandler(this.btnExpress_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnExpress);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "TextElementValueSetPage";
            base.Size = new Size(291, 139);
            base.Load += new EventHandler(this.TextElementValueSetPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Button btnExpress;
        private CheckBox checkBox1;
        private Label label1;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private TextBox textBox1;
    }
}