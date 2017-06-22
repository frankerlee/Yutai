using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class AGSHostsPropertyPage
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
            this.label1 = new Label();
            this.Hostlist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnEdit = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(178, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "可以运行server objects的主机";
            this.Hostlist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.Hostlist.Location = new Point(16, 48);
            this.Hostlist.Name = "Hostlist";
            this.Hostlist.Size = new Size(256, 112);
            this.Hostlist.TabIndex = 1;
            this.Hostlist.View = View.Details;
            this.Hostlist.SelectedIndexChanged += new EventHandler(this.Hostlist_SelectedIndexChanged);
            this.columnHeader_0.Text = "主机";
            this.columnHeader_0.Width = 115;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 128;
            this.btnAdd.Location = new Point(280, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加...";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(280, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(56, 24);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnEdit.Location = new Point(280, 112);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(56, 24);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "编辑...";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.Hostlist);
            base.Controls.Add(this.label1);
            base.Name = "AGSHostsPropertyPage";
            base.Size = new Size(352, 256);
            base.Load += new EventHandler(this.AGSHostsPropertyPage_Load);
            base.ResumeLayout(false);
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