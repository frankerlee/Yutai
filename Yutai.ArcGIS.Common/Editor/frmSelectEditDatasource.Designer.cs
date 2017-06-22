using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor
{
	    partial class frmSelectEditDatasource
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.EditWorkspacelist = new ListView();
			this.columnHeader_0 = new ColumnHeader();
			this.columnHeader_1 = new ColumnHeader();
			this.label2 = new Label();
			this.CanEditDatasetList = new ListBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(202, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "选择要编辑的目录或数据库中的数据";
			ListView.ColumnHeaderCollection columns = this.EditWorkspacelist.Columns;
			ColumnHeader[] columnHeader0 = new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 };
			columns.AddRange(columnHeader0);
			this.EditWorkspacelist.Location = new Point(8, 32);
			this.EditWorkspacelist.MultiSelect = false;
			this.EditWorkspacelist.Name = "EditWorkspacelist";
			this.EditWorkspacelist.Size = new System.Drawing.Size(280, 96);
			this.EditWorkspacelist.TabIndex = 1;
			this.EditWorkspacelist.View = View.Details;
			this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
			this.columnHeader_0.Text = "源";
			this.columnHeader_0.Width = 147;
			this.columnHeader_1.Text = "类型";
			this.columnHeader_1.Width = 119;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(8, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "能被编辑的图层或表";
			this.CanEditDatasetList.ItemHeight = 12;
			this.CanEditDatasetList.Location = new Point(8, 160);
			this.CanEditDatasetList.Name = "CanEditDatasetList";
			this.CanEditDatasetList.Size = new System.Drawing.Size(272, 88);
			this.CanEditDatasetList.TabIndex = 3;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new Point(120, 264);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 24);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(200, 264);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消";
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(292, 300);
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

	
		private Label label1;
		private ListView EditWorkspacelist;
		private ColumnHeader columnHeader_0;
		private ColumnHeader columnHeader_1;
		private Label label2;
		private ListBox CanEditDatasetList;
		private Button btnOK;
		private Button btnCancel;
    }
}