using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class frmStyleConvert
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
            this.txtOrigin = new TextBox();
            this.txtDest = new TextBox();
            this.btnConvert = new Button();
            this.button2 = new Button();
            this.btnSelect = new Button();
            this.btnSaveTo = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源符号库";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目的符号库";
            this.txtOrigin.Location = new Point(80, 14);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.ReadOnly = true;
            this.txtOrigin.Size = new Size(259, 21);
            this.txtOrigin.TabIndex = 2;
            this.txtDest.Location = new Point(80, 46);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new Size(259, 21);
            this.txtDest.TabIndex = 3;
            this.btnConvert.Location = new Point(248, 73);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(54, 25);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(317, 73);
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
            this.btnSaveTo.Location = new Point(345, 46);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new Size(31, 22);
            this.btnSaveTo.TabIndex = 7;
            this.btnSaveTo.Text = "....";
            this.btnSaveTo.UseVisualStyleBackColor = true;
            this.btnSaveTo.Click += new EventHandler(this.btnSaveTo_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(388, 116);
            base.Controls.Add(this.btnSaveTo);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.txtDest);
            base.Controls.Add(this.txtOrigin);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmStyleConvert";
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
        private TextBox txtDest;
        private TextBox txtOrigin;
    }
}