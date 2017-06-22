using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Common
{
    partial class frmPyramidSet
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
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.checkBox1 = new CheckBox();
            base.SuspendLayout();
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Location = new Point(48, 82);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "是";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button2.Location = new Point(144, 82);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "否";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new Point(255, 82);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new Size(245, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "该删格数据不存在金字塔，是否构建金字塔？";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(32, 55);
            this.label2.Name = "label2";
            this.label2.Size = new Size(173, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "构建金字塔需要花费一定时间。";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(40, 127);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(228, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "默认当前选择，并且不再提示该对话框";
            this.checkBox1.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(356, 162);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmPyramidSet";
            this.Text = "构建金字塔";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    
        private Button button1;
        private Button button2;
        private Button button3;
        private CheckBox checkBox1;
        private Label label1;
        private Label label2;
    }
}