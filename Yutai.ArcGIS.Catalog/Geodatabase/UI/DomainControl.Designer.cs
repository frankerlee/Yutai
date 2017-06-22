using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class DomainControl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.DomainListView = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.groupBox1 = new GroupBox();
            this.btnDeleteCode = new SimpleButton();
            this.btnEditCode = new SimpleButton();
            this.btnAddCode = new SimpleButton();
            this.label1 = new Label();
            this.CodeValueListView = new ListView();
            this.columnHeader_4 = new ColumnHeader();
            this.columnHeader_5 = new ColumnHeader();
            this.DomainPropertyListView = new ListView();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.btnExtentCodeDomainMang = new SimpleButton();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.DomainListView.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.DomainListView.FullRowSelect = true;
            this.DomainListView.GridLines = true;
            this.DomainListView.HideSelection = false;
            this.DomainListView.Location = new Point(8, 8);
            this.DomainListView.MultiSelect = false;
            this.DomainListView.Name = "DomainListView";
            this.DomainListView.Size = new Size(309, 97);
            this.DomainListView.TabIndex = 0;
            this.DomainListView.UseCompatibleStateImageBehavior = false;
            this.DomainListView.View = View.Details;
            this.DomainListView.DoubleClick += new EventHandler(this.DomainListView_DoubleClick);
            this.DomainListView.SelectedIndexChanged += new EventHandler(this.DomainListView_SelectedIndexChanged);
            this.DomainListView.KeyDown += new KeyEventHandler(this.DomainListView_KeyDown);
            this.DomainListView.MouseDown += new MouseEventHandler(this.DomainListView_MouseDown);
            this.columnHeader_0.Text = "域名";
            this.columnHeader_0.Width = 125;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 157;
            this.groupBox1.Controls.Add(this.btnDeleteCode);
            this.groupBox1.Controls.Add(this.btnEditCode);
            this.groupBox1.Controls.Add(this.btnAddCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CodeValueListView);
            this.groupBox1.Controls.Add(this.DomainPropertyListView);
            this.groupBox1.Location = new Point(8, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(309, 284);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "域属性";
            this.btnDeleteCode.Location = new Point(237, 220);
            this.btnDeleteCode.Name = "btnDeleteCode";
            this.btnDeleteCode.Size = new Size(64, 24);
            this.btnDeleteCode.TabIndex = 7;
            this.btnDeleteCode.Text = "删除";
            this.btnDeleteCode.Click += new EventHandler(this.btnDeleteCode_Click);
            this.btnEditCode.Location = new Point(237, 190);
            this.btnEditCode.Name = "btnEditCode";
            this.btnEditCode.Size = new Size(64, 24);
            this.btnEditCode.TabIndex = 6;
            this.btnEditCode.Text = "编辑...";
            this.btnEditCode.Click += new EventHandler(this.btnEditCode_Click);
            this.btnAddCode.Location = new Point(237, 160);
            this.btnAddCode.Name = "btnAddCode";
            this.btnAddCode.Size = new Size(64, 24);
            this.btnAddCode.TabIndex = 5;
            this.btnAddCode.Text = "添加...";
            this.btnAddCode.Click += new EventHandler(this.btnAddCode_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 144);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "编码值";
            this.CodeValueListView.Columns.AddRange(new ColumnHeader[] { this.columnHeader_4, this.columnHeader_5 });
            this.CodeValueListView.FullRowSelect = true;
            this.CodeValueListView.GridLines = true;
            this.CodeValueListView.HideSelection = false;
            this.CodeValueListView.Location = new Point(9, 160);
            this.CodeValueListView.MultiSelect = false;
            this.CodeValueListView.Name = "CodeValueListView";
            this.CodeValueListView.Size = new Size(222, 112);
            this.CodeValueListView.TabIndex = 3;
            this.CodeValueListView.UseCompatibleStateImageBehavior = false;
            this.CodeValueListView.View = View.Details;
            this.CodeValueListView.DoubleClick += new EventHandler(this.CodeValueListView_DoubleClick);
            this.CodeValueListView.SelectedIndexChanged += new EventHandler(this.CodeValueListView_SelectedIndexChanged);
            this.CodeValueListView.KeyDown += new KeyEventHandler(this.CodeValueListView_KeyDown);
            this.CodeValueListView.MouseDown += new MouseEventHandler(this.CodeValueListView_MouseDown);
            this.columnHeader_4.Text = "代码";
            this.columnHeader_4.Width = 77;
            this.columnHeader_5.Text = "描述";
            this.columnHeader_5.Width = 121;
            this.DomainPropertyListView.Columns.AddRange(new ColumnHeader[] { this.columnHeader_2, this.columnHeader_3 });
            this.DomainPropertyListView.FullRowSelect = true;
            this.DomainPropertyListView.GridLines = true;
            this.DomainPropertyListView.HeaderStyle = ColumnHeaderStyle.None;
            this.DomainPropertyListView.Location = new Point(10, 24);
            this.DomainPropertyListView.MultiSelect = false;
            this.DomainPropertyListView.Name = "DomainPropertyListView";
            this.DomainPropertyListView.Size = new Size(248, 112);
            this.DomainPropertyListView.TabIndex = 2;
            this.DomainPropertyListView.UseCompatibleStateImageBehavior = false;
            this.DomainPropertyListView.View = View.Details;
            this.DomainPropertyListView.DoubleClick += new EventHandler(this.DomainPropertyListView_DoubleClick);
            this.DomainPropertyListView.MouseDown += new MouseEventHandler(this.DomainPropertyListView_MouseDown);
            this.columnHeader_2.Text = "域名";
            this.columnHeader_2.Width = 102;
            this.columnHeader_3.Text = "描述";
            this.columnHeader_3.Width = 138;
            this.btnExtentCodeDomainMang.Location = new Point(221, 111);
            this.btnExtentCodeDomainMang.Name = "btnExtentCodeDomainMang";
            this.btnExtentCodeDomainMang.Size = new Size(96, 24);
            this.btnExtentCodeDomainMang.TabIndex = 6;
            this.btnExtentCodeDomainMang.Text = "扩展域值管理...";
            this.btnExtentCodeDomainMang.Click += new EventHandler(this.btnExtentCodeDomainMang_Click);
            base.Controls.Add(this.btnExtentCodeDomainMang);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.DomainListView);
            base.Name = "DomainControl";
            base.Size = new Size(369, 424);
            base.Load += new EventHandler(this.DomainControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddCode;
        private SimpleButton btnDeleteCode;
        private SimpleButton btnEditCode;
        private SimpleButton btnExtentCodeDomainMang;
        private ListView CodeValueListView;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private ColumnHeader columnHeader_5;
        private System.Windows.Forms.ComboBox comboBox_0;
        private System.Windows.Forms.ComboBox comboBox_1;
        private ListView DomainListView;
        private ListView DomainPropertyListView;
        private GroupBox groupBox1;
       
        private Label label1;
        private ListViewItem listViewItem_0;
        private TextBox textBox_0;
        private TextBox textBox_1;
        private TextBox textBox_2;
     
    }
}