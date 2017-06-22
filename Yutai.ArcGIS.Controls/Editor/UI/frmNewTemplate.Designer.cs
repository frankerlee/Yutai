using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmNewTemplate
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
            this.panel2 = new Panel();
            this.button4 = new Button();
            this.btnFinish = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel1 = new Panel();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.btnFinish);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 222);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(434, 37);
            this.panel2.TabIndex = 1;
            this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button4.Location = new Point(326, 6);
            this.button4.Name = "button4";
            this.button4.Size = new Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
            this.btnFinish.Location = new Point(226, 6);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new Size(75, 23);
            this.btnFinish.TabIndex = 2;
            this.btnFinish.Text = "完成";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new EventHandler(this.btnFinish_Click);
            this.btnNext.Location = new Point(145, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new Point(53, 6);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(75, 23);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(434, 222);
            this.panel1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(434, 259);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Name = "frmNewTemplate";
            this.Text = "编辑模板创建向导";
            base.Load += new EventHandler(this.frmNewTemplate_Load);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnFinish;
        private Button btnLast;
        private Button btnNext;
        private Button button4;
        private Panel panel1;
        private Panel panel2;
    }
}