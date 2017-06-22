using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmAttachment
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
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.btnOpen = new Button();
            this.btnSaveAs = new Button();
            this.btnAllSaveAs = new Button();
            this.btnDelete = new Button();
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            this.btnOk = new Button();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(326, 181);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.listView1_MouseDoubleClick);
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 207;
            this.columnHeader2.Text = "大小";
            this.columnHeader2.Width = 84;
            this.btnOpen.Location = new Point(345, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnSaveAs.Enabled = false;
            this.btnSaveAs.Location = new Point(345, 42);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new Size(75, 23);
            this.btnSaveAs.TabIndex = 2;
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new EventHandler(this.btnSaveAs_Click);
            this.btnAllSaveAs.Location = new Point(345, 71);
            this.btnAllSaveAs.Name = "btnAllSaveAs";
            this.btnAllSaveAs.Size = new Size(75, 23);
            this.btnAllSaveAs.TabIndex = 3;
            this.btnAllSaveAs.Text = "全部保存";
            this.btnAllSaveAs.UseVisualStyleBackColor = true;
            this.btnAllSaveAs.Click += new EventHandler(this.btnAllSaveAs_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(345, 170);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "移除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAdd.Location = new Point(345, 141);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(337, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOk.Location = new Point(244, 216);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(424, 244);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.btnAllSaveAs);
            base.Controls.Add(this.btnSaveAs);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAttachment";
            this.Text = "附件";
            base.Load += new EventHandler(this.frmAttachment_Load);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnAdd;
        private Button btnAllSaveAs;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnOk;
        private Button btnOpen;
        private Button btnSaveAs;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ListView listView1;
    }
}