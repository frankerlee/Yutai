using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmFilterLayerSelect
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
            this.btnSelectAll = new Button();
            this.btnClearAll = new Button();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.checkedListBox1 = new CheckedListBox();
            base.SuspendLayout();
            this.btnSelectAll.Location = new Point(217, 13);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(75, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnClearAll.Location = new Point(217, 42);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(75, 23);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(217, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(217, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(2, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(200, 164);
            this.checkedListBox1.TabIndex = 5;
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(297, 183);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFilterLayerSelect";
            this.Text = "模板过滤";
            base.Load += new EventHandler(this.frmFilterLayerSelect_Load);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnCancel;
        private Button btnClearAll;
        private Button btnOK;
        private Button btnSelectAll;
        private CheckedListBox checkedListBox1;
    }
}