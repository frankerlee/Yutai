using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class SelectExportChangeWKCtrl
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
            this.CanEditDatasetList = new ListBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.EditWorkspacelist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            base.SuspendLayout();
            this.CanEditDatasetList.ItemHeight = 12;
            this.CanEditDatasetList.Location = new Point(16, 160);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(280, 88);
            this.CanEditDatasetList.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 136);
            this.label2.Name = "label2";
            this.label2.Size = new Size(128, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "导出以下层或表的变化";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(165, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择要导出变化的检出数据库";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.EditWorkspacelist.Location = new Point(16, 32);
            this.EditWorkspacelist.MultiSelect = false;
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(280, 96);
            this.EditWorkspacelist.TabIndex = 5;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.columnHeader_0.Text = "源";
            this.columnHeader_0.Width = 147;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 119;
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.EditWorkspacelist);
            base.Name = "SelectExportChangeWKCtrl";
            base.Size = new Size(344, 296);
            base.Load += new EventHandler(this.SelectExportChangeWKCtrl_Load);
            base.ResumeLayout(false);
        }

       
        private ListBox CanEditDatasetList;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ListView EditWorkspacelist;
        private Label label1;
        private Label label2;
    }
}