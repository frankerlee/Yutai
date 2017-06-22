using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;

namespace Yutai.ArcGIS.Catalog
{
    partial class frmAddDatabaseServer
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
            this.button1 = new Button();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label3 = new Label();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "空间数据库服务器:";
            this.button1.Enabled = false;
            this.button1.FlatStyle = FlatStyle.Popup;
            this.button1.Location = new Point(92, 112);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "示例:";
            this.textBox1.Location = new Point(125, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(131, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(123, 72);
            this.label3.Name = "label3";
            this.label3.Size = new Size(119, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = @"myserver\sqlexpress";
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = FlatStyle.Popup;
            this.button2.Location = new Point(205, 112);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 176);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Name = "frmAddDatabaseServer";
            this.Text = "添加空间数据库服务器";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
    }
}