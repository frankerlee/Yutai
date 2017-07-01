using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class SelectExportChangeWKCtrl : UserControl
    {
        private Container container_0 = null;
        private IArray iarray_0 = null;

        public SelectExportChangeWKCtrl()
        {
            this.InitializeComponent();
        }

        public bool Do()
        {
            if (this.EditWorkspacelist.SelectedItems.Count == 0)
            {
                return false;
            }
            CheckInOutWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as CheckInOutWorkspaceInfo;
            ExportChangesHelper.m_pHelper.CheckoutWorkspaceName = (tag.Workspace as IDataset).FullName as IWorkspaceName;
            return true;
        }

        private void EditWorkspacelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CanEditDatasetList.Items.Clear();
            if (this.EditWorkspacelist.SelectedItems.Count != 0)
            {
                try
                {
                    CheckInOutWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as CheckInOutWorkspaceInfo;
                    IEnumName enumName = tag.EnumName;
                    enumName.Reset();
                    for (IName name2 = enumName.Next(); name2 != null; name2 = enumName.Next())
                    {
                        this.CanEditDatasetList.Items.Add((name2 as IDatasetName).Name);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void SelectExportChangeWKCtrl_Load(object sender, EventArgs e)
        {
            if (this.iarray_0 != null)
            {
                string[] items = new string[2];
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(i) as CheckInOutWorkspaceInfo;
                    items[0] = info.Workspace.PathName;
                    items[1] = "空间数据库连接";
                    ListViewItem item = new ListViewItem(items)
                    {
                        Tag = info
                    };
                    this.EditWorkspacelist.Items.Add(item);
                }
            }
        }

        public IArray WorkspaceArray
        {
            set { this.iarray_0 = value; }
        }
    }
}