using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class PictureSelectPage
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
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.btnSelectPicture = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图片文件";
            this.textBox1.Location = new Point(63, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(289, 21);
            this.textBox1.TabIndex = 1;
            this.btnSelectPicture.Location = new Point(277, 45);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(75, 23);
            this.btnSelectPicture.TabIndex = 2;
            this.btnSelectPicture.Text = "选择图片";
            this.btnSelectPicture.UseVisualStyleBackColor = true;
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "PictureSelectPage";
            base.Size = new Size(375, 192);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Button btnSelectPicture;
        private Label label1;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private TextBox textBox1;
    }
}