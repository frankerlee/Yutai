using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class LayerSettingInfoPropertyPage
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
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnEdit = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层列表";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.listView1.Location = new Point(3, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(410, 160);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "图层名";
            this.columnHeader1.Width = 135;
            this.columnHeader2.Text = "最小比例";
            this.columnHeader2.Width = 128;
            this.columnHeader3.Text = "最大比例";
            this.columnHeader3.Width = 135;
            this.btnAdd.Location = new Point(219, 191);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(48, 24);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(287, 191);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(48, 24);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new Point(352, 191);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(48, 24);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Name = "LayerSettingInfoPropertyPage";
            base.Size = new Size(446, 277);
            base.Load += new EventHandler(this.LayerSettingInfoPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label label1;
        private ListView listView1;
    }
}