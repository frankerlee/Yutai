using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmSelectEditDatasource
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
            this.EditWorkspacelist = new ListView();
            this.ColumnHeader1 = new ColumnHeader();
            this.ColumnHeader2 = new ColumnHeader();
            this.label2 = new Label();
            this.CanEditDatasetList = new ListBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(202, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择要编辑的目录或数据库中的数据";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.ColumnHeader1, this.ColumnHeader2 });
            this.EditWorkspacelist.Location = new Point(8, 32);
            this.EditWorkspacelist.MultiSelect = false;
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(280, 96);
            this.EditWorkspacelist.TabIndex = 1;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.ColumnHeader1.Text = "源";
            this.ColumnHeader1.Width = 147;
            this.ColumnHeader2.Text = "类型";
            this.ColumnHeader2.Width = 119;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 136);
            this.label2.Name = "label2";
            this.label2.Size = new Size(116, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "能被编辑的图层或表";
            this.CanEditDatasetList.ItemHeight = 12;
            this.CanEditDatasetList.Location = new Point(8, 160);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(272, 88);
            this.CanEditDatasetList.TabIndex = 3;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(120, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(200, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 300);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.EditWorkspacelist);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectEditDatasource";
            this.Text = "开始编辑";
            base.Load += new EventHandler(this.frmSelectEditDatasource_Load);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private Button btnCancel;
        private Button btnOK;
        private ListBox CanEditDatasetList;
        private ColumnHeader ColumnHeader1;
        private ColumnHeader ColumnHeader2;
        private ListView EditWorkspacelist;
        private Label label1;
        private Label label2;
    }
}