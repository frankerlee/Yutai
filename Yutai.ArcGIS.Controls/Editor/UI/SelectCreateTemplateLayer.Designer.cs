using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class SelectCreateTemplateLayer
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
            this.checkedListBox1 = new CheckedListBox();
            this.btnClearAll = new Button();
            this.btnSelectAll = new Button();
            this.label1 = new Label();
            this.button1 = new Button();
            base.SuspendLayout();
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(14, 30);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(263, 164);
            this.checkedListBox1.TabIndex = 8;
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.btnClearAll.Location = new Point(299, 88);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(102, 23);
            this.btnClearAll.TabIndex = 7;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new Point(299, 59);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(102, 23);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(125, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "选择要创建模板的图层";
            this.button1.Location = new Point(299, 30);
            this.button1.Name = "button1";
            this.button1.Size = new Size(102, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "选择可见图层";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Name = "SelectCreateTemplateLayer";
            base.Size = new Size(415, 208);
            base.Load += new EventHandler(this.SelectCreateTemplateLayer_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private Button btnClearAll;
        private Button btnSelectAll;
        private Button button1;
        private CheckedListBox checkedListBox1;
        private Label label1;
    }
}