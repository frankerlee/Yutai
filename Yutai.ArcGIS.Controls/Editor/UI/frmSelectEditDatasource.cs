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
    internal partial class frmSelectEditDatasource : Form
    {
        private IArray m_pEditWorkspaceInfo = null;
        private IMap m_pMap = null;

        public frmSelectEditDatasource()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.CanEditDatasetList.Items.Count == 0)
            {
                MessageBox.Show("不能编辑任何图层，请检查是否加载了要素图层，加载的要素图层是否已经进行了版本注册或是否有更新权限！", "开始编辑", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo;
                IWorkspaceEdit workspace = tag.Workspace as IWorkspaceEdit;
                if (!workspace.IsBeingEdited())
                {
                    try
                    {
                        workspace.StartEditing(true);
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace = workspace;
                        Yutai.ArcGIS.Common.Editor.Editor.EditMap = this.m_pMap;
                    }
                    catch (COMException exception)
                    {
                        if (exception.ErrorCode == -2147217069)
                        {
                            MessageBox.Show("不能编辑数据，其他应用程序正在使用该数据源!");
                        }
                        else
                        {
                           Logger.Current.Error("", exception, "");
                        }
                    }
                    catch (Exception exception2)
                    {
                       Logger.Current.Error("", exception2, "");
                    }
                }
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
                    Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo;
                    for (int i = 0; i < tag.LayerArray.Count; i++)
                    {
                        IFeatureLayer layer = tag.LayerArray.get_Element(i) as IFeatureLayer;
                        this.CanEditDatasetList.Items.Add(layer.Name);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
                if (this.CanEditDatasetList.Items.Count == 0)
                {
                    this.btnOK.Enabled = false;
                }
                else
                {
                    this.btnOK.Enabled = true;
                }
            }
        }

        private void frmSelectEditDatasource_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            if (this.m_pEditWorkspaceInfo != null)
            {
                string[] items = new string[2];
                for (int i = 0; i < this.m_pEditWorkspaceInfo.Count; i++)
                {
                    Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo info = this.m_pEditWorkspaceInfo.get_Element(i) as Yutai.ArcGIS.Common.Editor.EditWorkspaceInfo;
                    items[0] = info.Workspace.PathName;
                    items[1] = "";
                    switch (info.Workspace.Type)
                    {
                        case esriWorkspaceType.esriFileSystemWorkspace:
                            items[1] = "Shapefiles";
                            break;

                        case esriWorkspaceType.esriLocalDatabaseWorkspace:
                            items[1] = "个人空间数据库";
                            break;

                        case esriWorkspaceType.esriRemoteDatabaseWorkspace:
                            items[1] = "空间数据库连接";
                            break;
                    }
                    ListViewItem item = new ListViewItem(items) {
                        Tag = info
                    };
                    this.EditWorkspacelist.Items.Add(item);
                }
            }
        }

 public IArray EditWorkspaceInfo
        {
            set
            {
                this.m_pEditWorkspaceInfo = value;
            }
        }

        public IMap Map
        {
            set
            {
                this.m_pMap = value;
            }
        }
    }
}

