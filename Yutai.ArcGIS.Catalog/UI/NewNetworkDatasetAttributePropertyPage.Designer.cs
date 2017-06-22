using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class NewNetworkDatasetAttributePropertyPage
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
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.columnHeader_5 = new ColumnHeader();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3, this.columnHeader_4, this.columnHeader_5 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(13, 16);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(304, 148);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "!";
            this.columnHeader_0.Width = 32;
            this.columnHeader_1.Text = "";
            this.columnHeader_1.Width = 36;
            this.columnHeader_2.Text = "名称";
            this.columnHeader_2.Width = 45;
            this.columnHeader_3.Text = "使用方式";
            this.columnHeader_4.Text = "单位";
            this.columnHeader_5.Text = "数据类型";
            this.btnAdd.Location = new Point(323, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(323, 45);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new Point(323, 75);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(75, 23);
            this.btnDeleteAll.TabIndex = 3;
            this.btnDeleteAll.Text = "删除所有";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.listView1);
            base.Name = "NewNetworkDatasetAttributePropertyPage";
            base.Size = new Size(413, 233);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private ColumnHeader columnHeader_5;
        private ListView listView1;
    }
}