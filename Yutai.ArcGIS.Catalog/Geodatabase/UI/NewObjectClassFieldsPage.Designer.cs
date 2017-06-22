using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewObjectClassFieldsPage
    {
        protected override void Dispose(bool bool_4)
        {
            if (bool_4 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_4);
        }

       
        private void InitializeComponent()
        {
            this.btnImportStruct = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.listView2 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            base.SuspendLayout();
            this.btnImportStruct.Location = new System.Drawing.Point(296, 391);
            this.btnImportStruct.Name = "btnImportStruct";
            this.btnImportStruct.Size = new Size(56, 24);
            this.btnImportStruct.TabIndex = 5;
            this.btnImportStruct.Text = "导入...";
            this.btnImportStruct.Click += new EventHandler(this.btnImportStruct_Click);
            this.groupBox1.Location = new System.Drawing.Point(16, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(272, 224);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段属性";
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView2.ComboBoxBgColor = Color.GhostWhite;
            this.listView2.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView2.EditBgColor = Color.GhostWhite;
            this.listView2.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(16, 13);
            this.listView2.LockRowCount = 0;
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(336, 167);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.Details;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.ValueChanged += new Common.ControlExtend.ValueChangedHandler(this.method_6);
            this.listView2.RowDelete += new RowDeleteHandler(this.method_7);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_0.Text = "字段名";
            this.lvcolumnHeader_0.Width = 203;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ComboBox;
            this.lvcolumnHeader_1.Text = "数据类型";
            this.lvcolumnHeader_1.Width = 126;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnImportStruct);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView2);
            base.Name = "NewObjectClassFieldsPage";
            base.Size = new Size(368, 424);
            base.Load += new EventHandler(this.NewObjectClassFieldsPage_Load);
            base.ResumeLayout(false);
        }

       
        private bool bool_3;
        private SimpleButton btnImportStruct;
        private GroupBox groupBox1;
        private IFields ifields_0;
        private EditListView listView2;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
    }
}