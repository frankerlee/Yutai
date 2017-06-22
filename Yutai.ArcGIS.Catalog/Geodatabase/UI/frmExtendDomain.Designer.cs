using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmExtendDomain
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
            this.DomainListView = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.btnDeleteCode = new SimpleButton();
            this.btnEditCode = new SimpleButton();
            this.btnAddCode = new SimpleButton();
            this.listView1 = new ListView();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.simpleButton1 = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.DomainListView.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.DomainListView.FullRowSelect = true;
            this.DomainListView.GridLines = true;
            this.DomainListView.HideSelection = false;
            this.DomainListView.Location = new Point(12, 12);
            this.DomainListView.MultiSelect = false;
            this.DomainListView.Name = "DomainListView";
            this.DomainListView.Size = new Size(309, 97);
            this.DomainListView.TabIndex = 1;
            this.DomainListView.UseCompatibleStateImageBehavior = false;
            this.DomainListView.View = View.Details;
            this.DomainListView.SelectedIndexChanged += new EventHandler(this.DomainListView_SelectedIndexChanged);
            this.columnHeader_0.Text = "域名";
            this.columnHeader_0.Width = 125;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 157;
            this.btnDeleteCode.Location = new Point(327, 72);
            this.btnDeleteCode.Name = "btnDeleteCode";
            this.btnDeleteCode.Size = new Size(64, 24);
            this.btnDeleteCode.TabIndex = 10;
            this.btnDeleteCode.Text = "删除";
            this.btnDeleteCode.Click += new EventHandler(this.btnDeleteCode_Click);
            this.btnEditCode.Location = new Point(327, 42);
            this.btnEditCode.Name = "btnEditCode";
            this.btnEditCode.Size = new Size(64, 24);
            this.btnEditCode.TabIndex = 9;
            this.btnEditCode.Text = "编辑...";
            this.btnEditCode.Click += new EventHandler(this.btnEditCode_Click);
            this.btnAddCode.Location = new Point(327, 12);
            this.btnAddCode.Name = "btnAddCode";
            this.btnAddCode.Size = new Size(64, 24);
            this.btnAddCode.TabIndex = 8;
            this.btnAddCode.Text = "添加...";
            this.btnAddCode.Click += new EventHandler(this.btnAddCode_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_2, this.columnHeader_3, this.columnHeader_4 });
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(12, 115);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(379, 143);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_2.Text = "表名";
            this.columnHeader_2.Width = 115;
            this.columnHeader_3.Text = "名称字段";
            this.columnHeader_3.Width = 130;
            this.columnHeader_4.Text = "值字段";
            this.columnHeader_4.Width = 107;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new Point(327, 264);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(64, 24);
            this.simpleButton1.TabIndex = 13;
            this.simpleButton1.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(257, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(407, 300);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteCode);
            base.Controls.Add(this.btnEditCode);
            base.Controls.Add(this.btnAddCode);
            base.Controls.Add(this.DomainListView);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExtendDomain";
            this.Text = "扩展域值管理";
            base.Load += new EventHandler(this.frmExtendDomain_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddCode;
        private SimpleButton btnDeleteCode;
        private SimpleButton btnEditCode;
        private SimpleButton btnOK;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private ListView DomainListView;
        private IWorkspace iworkspace_0;
        private ListView listView1;
        private SimpleButton simpleButton1;
            private bool bool_0;
            private bool bool_1;
            private bool bool_2;
            private IDomain idomain_0;
            private IDomain idomain_1;
            private string string_0;
    }
}