using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class frmStyleConvertEx
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtDest = new TextBox();
            this.btnConvert = new Button();
            this.button2 = new Button();
            this.btnSelect = new Button();
            this.btnSaveTo = new Button();
            this.listBox1 = new ListBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源符号库";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 179);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目的符号库";
            this.txtDest.Location = new Point(80, 174);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new Size(259, 21);
            this.txtDest.TabIndex = 3;
            this.btnConvert.Location = new Point(248, 201);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(54, 25);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(317, 201);
            this.button2.Name = "button2";
            this.button2.Size = new Size(54, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnSelect.Location = new Point(345, 14);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(31, 21);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "....";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.btnSaveTo.Location = new Point(345, 174);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new Size(31, 22);
            this.btnSaveTo.TabIndex = 7;
            this.btnSaveTo.Text = "....";
            this.btnSaveTo.UseVisualStyleBackColor = true;
            this.btnSaveTo.Click += new EventHandler(this.btnSaveTo_Click);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(80, 17);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(259, 148);
            this.listBox1.TabIndex = 8;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(399, 237);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.btnSaveTo);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.txtDest);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmStyleConvertEx";
            this.Text = "符号库互转工具";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    
        private IContainer components = null;
        private Button btnConvert;
        private Button btnSaveTo;
        private Button btnSelect;
        private Button button2;
        private Label label1;
        private Label label2;
        private ListBox listBox1;
        private TextBox txtDest;
    }
}