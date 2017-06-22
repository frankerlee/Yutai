using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ObjectFieldsPage
    {
        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

       
        private void InitializeComponent()
        {
            this.listView2 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.groupBox1 = new GroupBox();
            this.btnImportStruct = new SimpleButton();
            base.SuspendLayout();
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView2.ComboBoxBgColor = Color.GhostWhite;
            this.listView2.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView2.EditBgColor = Color.GhostWhite;
            this.listView2.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(8, 8);
            this.listView2.LockRowCount = 0;
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(336, 184);
            this.listView2.TabIndex = 0;
            this.listView2.View = View.Details;
            this.listView2.KeyDown += new KeyEventHandler(this.listView2_KeyDown);
            this.listView2.KeyPress += new KeyPressEventHandler(this.listView2_KeyPress);
            this.listView2.ValueChanged += new Common.ControlExtend.ValueChangedHandler(this.method_3);
            this.listView2.RowDelete += new RowDeleteHandler(this.method_5);
            this.listView2.Click += new EventHandler(this.listView2_Click);
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_0.Text = "字段名";
            this.lvcolumnHeader_0.Width = 203;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ComboBox;
            this.lvcolumnHeader_1.Text = "数据类型";
            this.lvcolumnHeader_1.Width = 126;
            this.groupBox1.Location = new System.Drawing.Point(8, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(272, 248);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段属性";
            this.btnImportStruct.Location = new System.Drawing.Point(288, 424);
            this.btnImportStruct.Name = "btnImportStruct";
            this.btnImportStruct.Size = new Size(56, 24);
            this.btnImportStruct.TabIndex = 2;
            this.btnImportStruct.Text = "导入...";
            this.btnImportStruct.Click += new EventHandler(this.btnImportStruct_Click);
            base.Controls.Add(this.btnImportStruct);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView2);
            base.Name = "ObjectFieldsPage";
            base.Size = new Size(368, 464);
            base.Load += new EventHandler(this.ObjectFieldsPage_Load);
            base.ResumeLayout(false);
        }

       
        private bool bool_2;
        private SimpleButton btnImportStruct;
        private Container container_0;
        private GroupBox groupBox1;
        private IFields ifields_0;
        private IWorkspace iworkspace_0;
        private EditListView listView2;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
    }
}