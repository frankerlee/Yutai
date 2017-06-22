using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class AGSDirectoryPropertyPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.btnEdit = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.Hostlist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label1 = new Label();
            base.SuspendLayout();
            this.btnEdit.Location = new Point(288, 120);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(56, 24);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "编辑...";
            this.btnDelete.Location = new Point(288, 88);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(56, 24);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除";
            this.btnAdd.Location = new Point(288, 56);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加...";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.Hostlist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.Hostlist.Location = new Point(24, 56);
            this.Hostlist.Name = "Hostlist";
            this.Hostlist.Size = new Size(256, 112);
            this.Hostlist.TabIndex = 6;
            this.Hostlist.UseCompatibleStateImageBehavior = false;
            this.Hostlist.View = View.Details;
            this.columnHeader_0.Text = "主机";
            this.columnHeader_0.Width = 115;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 128;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(185, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "server objects可用的服务器目录";
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.Hostlist);
            base.Controls.Add(this.label1);
            base.Name = "AGSDirectoryPropertyPage";
            base.Size = new Size(488, 312);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ListView Hostlist;
        private Label label1;
    }
}