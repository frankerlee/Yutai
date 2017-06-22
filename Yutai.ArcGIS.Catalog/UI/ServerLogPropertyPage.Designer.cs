using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class ServerLogPropertyPage
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
            this.label3 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label4 = new Label();
            this.btnSetDir = new Button();
            this.comboBox1 = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 55);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志文件路径";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(22, 82);
            this.label2.Name = "label2";
            this.label2.Size = new Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "日志保留时间";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(22, 112);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "日志级别";
            this.textBox1.Location = new Point(103, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(301, 21);
            this.textBox1.TabIndex = 3;
            this.textBox2.Location = new Point(103, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(122, 21);
            this.textBox2.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(233, 82);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "天";
            this.btnSetDir.Location = new Point(411, 49);
            this.btnSetDir.Name = "btnSetDir";
            this.btnSetDir.Size = new Size(33, 23);
            this.btnSetDir.TabIndex = 6;
            this.btnSetDir.Text = "...";
            this.btnSetDir.UseVisualStyleBackColor = true;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "关", "严重", "警告", "信息", "精细", "详细", "调试" });
            this.comboBox1.Location = new Point(105, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(121, 20);
            this.comboBox1.TabIndex = 7;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.btnSetDir);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ServerLogPropertyPage";
            base.Size = new Size(455, 275);
            base.Load += new EventHandler(this.ServerLogPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnSetDir;
        private ComboBox comboBox1;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}