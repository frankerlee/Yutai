using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Historical
{
    partial class frmHistorialMarkerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialMarkerManager));
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.btnNew = new SimpleButton();
            this.btnEdit = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnColse = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "历史标记";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(15, 26);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(343, 192);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 162;
            this.columnHeader2.Text = "时间标记";
            this.columnHeader2.Width = 155;
            this.btnNew.Location = new Point(7, 238);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(75, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new Point(88, 238);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "编辑...";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(170, 238);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnColse.Location = new Point(293, 238);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new Size(75, 23);
            this.btnColse.TabIndex = 5;
            this.btnColse.Text = "关闭";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(380, 273);
            base.Controls.Add(this.btnColse);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHistorialMarkerManager";
            this.Text = "历史标记管理";
            base.Load += new EventHandler(this.frmHistorialMarkerManager_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private SimpleButton btnColse;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private SimpleButton btnNew;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Label label1;
        private ListView listView1;
    }
}