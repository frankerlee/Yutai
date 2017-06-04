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
	internal class frmSelectEditDatasource : Form
	{
		private IMap imap_0 = null;

		private Label label1;

		private ListView EditWorkspacelist;

		private ColumnHeader columnHeader_0;

		private ColumnHeader columnHeader_1;

		private Label label2;

		private ListBox CanEditDatasetList;

		private System.ComponentModel.Container container_0 = null;

		private Button btnOK;

		private Button btnCancel;

		private IArray iarray_0 = null;

		public IArray EditWorkspaceInfo
		{
			set
			{
				this.iarray_0 = value;
			}
		}

		public IMap Map
		{
			set
			{
				this.imap_0 = value;
			}
		}

		public frmSelectEditDatasource()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.CanEditDatasetList.Items.Count != 0)
			{
				EditWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as EditWorkspaceInfo;
				IWorkspaceEdit workspace = tag.Workspace as IWorkspaceEdit;
				if (!workspace.IsBeingEdited())
				{
					try
					{
						workspace.StartEditing(true);
						Editor.EditWorkspace = workspace;
						Editor.EditMap = this.imap_0;
					}
					catch (COMException cOMException1)
					{
						COMException cOMException = cOMException1;
						if (cOMException.ErrorCode != -2147217069)
						{
							Logger.Current.Error("", cOMException, "");
						}
						else
						{
							MessageBox.Show("不能编辑数据，其他应用程序正在使用该数据源!");
						}
					}
					catch (Exception exception)
					{
						Logger.Current.Error("", exception, "");
					}
				}
			}
			else
			{
				MessageBox.Show("不能编辑任何图层，请检查是否加载了要素图层，加载的要素图层是否已经进行了版本注册或是否有更新权限！", "开始编辑", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void EditWorkspacelist_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CanEditDatasetList.Items.Clear();
			this.btnOK.Enabled = false;
			if (this.EditWorkspacelist.SelectedItems.Count != 0)
			{
				this.btnOK.Enabled = true;
				try
				{
					EditWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as EditWorkspaceInfo;
					for (int i = 0; i < tag.LayerArray.Count; i++)
					{
						IFeatureLayer element = tag.LayerArray.Element[i] as IFeatureLayer;
						this.CanEditDatasetList.Items.Add(element.Name);
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.ToString());
				}
				if (this.CanEditDatasetList.Items.Count != 0)
				{
					this.btnOK.Enabled = true;
				}
				else
				{
					this.btnOK.Enabled = false;
				}
			}
		}

		private void frmSelectEditDatasource_Load(object sender, EventArgs e)
		{
			this.method_0();
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

		private void method_0()
		{
			if (this.iarray_0 != null)
			{
				string[] pathName = new string[2];
				for (int i = 0; i < this.iarray_0.Count; i++)
				{
					EditWorkspaceInfo element = this.iarray_0.Element[i] as EditWorkspaceInfo;
					if (element.Workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
					{
						pathName[0] = element.Workspace.PathName;
					}
					else
					{
						IPropertySet connectionProperties = element.Workspace.ConnectionProperties;
						string str = "";
						try
						{
							str = connectionProperties.GetProperty("Version").ToString();
						}
						catch
						{
						}
						string str1 = connectionProperties.GetProperty("DB_CONNECTION_PROPERTIES").ToString();
						str = string.Concat(str, "(", str1);
						try
						{
							str1 = connectionProperties.GetProperty("User").ToString();
							str = string.Concat(str, "-", str1);
						}
						catch
						{
						}
						str = string.Concat(str, ")");
						pathName[0] = str;
					}
					pathName[1] = "";
					switch (element.Workspace.Type)
					{
						case esriWorkspaceType.esriFileSystemWorkspace:
						{
							pathName[1] = "Shapefiles";
							break;
						}
						case esriWorkspaceType.esriLocalDatabaseWorkspace:
						{
							pathName[1] = "个人空间数据库";
							break;
						}
						case esriWorkspaceType.esriRemoteDatabaseWorkspace:
						{
							pathName[1] = "空间数据库连接";
							break;
						}
					}
					ListViewItem listViewItem = new ListViewItem(pathName)
					{
						Tag = element
					};
					this.EditWorkspacelist.Items.Add(listViewItem);
				}
			}
		}
	}
}