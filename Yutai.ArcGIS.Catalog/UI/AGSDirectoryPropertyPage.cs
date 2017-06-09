using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal class AGSDirectoryPropertyPage : UserControl
    {
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private ListView Hostlist;
        private Label label1;

        public AGSDirectoryPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

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
            this.btnEdit.Location = new Point(0x120, 120);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x38, 0x18);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "编辑...";
            this.btnDelete.Location = new Point(0x120, 0x58);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x38, 0x18);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除";
            this.btnAdd.Location = new Point(0x120, 0x38);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加...";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.Hostlist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.Hostlist.Location = new Point(0x18, 0x38);
            this.Hostlist.Name = "Hostlist";
            this.Hostlist.Size = new Size(0x100, 0x70);
            this.Hostlist.TabIndex = 6;
            this.Hostlist.UseCompatibleStateImageBehavior = false;
            this.Hostlist.View = View.Details;
            this.columnHeader_0.Text = "主机";
            this.columnHeader_0.Width = 0x73;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 0x80;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xb9, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "server objects可用的服务器目录";
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.Hostlist);
            base.Controls.Add(this.label1);
            base.Name = "AGSDirectoryPropertyPage";
            base.Size = new Size(0x1e8, 0x138);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

