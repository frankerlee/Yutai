using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmClipOutSet
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
            this.groupBox1 = new GroupBox();
            this.btnOutSet = new Button();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.radioButton3 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.btnOK = new Button();
            this.btnCancle = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出类型";
            this.groupBox1.Controls.Add(this.btnOutSet);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(290, 126);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出设置";
            this.btnOutSet.Location = new Point(247, 74);
            this.btnOutSet.Name = "btnOutSet";
            this.btnOutSet.Size = new Size(37, 23);
            this.btnOutSet.TabIndex = 6;
            this.btnOutSet.Text = "...";
            this.btnOutSet.UseVisualStyleBackColor = true;
            this.btnOutSet.Click += new EventHandler(this.btnOutSet_Click);
            this.textBox1.Location = new Point(76, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(165, 21);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(17, 77);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "输出位置";
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(189, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(41, 16);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "VCT";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(102, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(65, 16);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "mapinfo";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(19, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(77, 16);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "shapefile";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(124, 154);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.Location = new Point(222, 154);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(309, 194);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmClipOutSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "裁剪输出";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private Button btnCancle;
        private Button btnOK;
        private Button btnOutSet;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private TextBox textBox1;
    }
}