using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmImportExcelSet
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
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.btnOpen = new Button();
            this.textBox1 = new TextBox();
            this.cboX = new ComboBox();
            this.label3 = new Label();
            this.cboY = new ComboBox();
            this.label4 = new Label();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "属性页";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(83, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(231, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据文件";
            this.btnOpen.Location = new System.Drawing.Point(320, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(30, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.textBox1.Location = new System.Drawing.Point(83, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(231, 21);
            this.textBox1.TabIndex = 2;
            this.cboX.FormattingEnabled = true;
            this.cboX.Location = new System.Drawing.Point(83, 66);
            this.cboX.Name = "cboX";
            this.cboX.Size = new Size(231, 20);
            this.cboX.TabIndex = 6;
            this.cboX.SelectedIndexChanged += new EventHandler(this.cboX_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "纵坐标字段";
            this.cboY.FormattingEnabled = true;
            this.cboY.Location = new System.Drawing.Point(83, 94);
            this.cboY.Name = "cboY";
            this.cboY.Size = new Size(231, 20);
            this.cboY.TabIndex = 8;
            this.cboY.SelectedIndexChanged += new EventHandler(this.cboY_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "横坐标字段";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(229, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(134, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(362, 152);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboY);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboX);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmImportExcelSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "导入XY坐标";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnCancel;
        private Button btnOK;
        private Button btnOpen;
        private ComboBox cboX;
        private ComboBox cboY;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
    }
}