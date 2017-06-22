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
	internal partial class frmSelectEditDatasource : Form
	{
		private IMap imap_0 = null;







		private System.ComponentModel.Container container_0 = null;



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