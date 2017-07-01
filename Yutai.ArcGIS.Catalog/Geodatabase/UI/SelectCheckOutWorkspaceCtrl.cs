using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class SelectCheckOutWorkspaceCtrl : UserControl
    {
        private Container container_0 = null;
        private IArray iarray_0 = null;
        private int int_0 = 0;

        public SelectCheckOutWorkspaceCtrl(int int_1)
        {
            this.InitializeComponent();
            this.int_0 = int_1;
        }

        public bool Do()
        {
            IEnumNameEdit edit;
            IEnumName enumName;
            IName name2;
            IList list;
            IList list2;
            IDatasetName featureDatasetName;
            string name;
            if (this.EditWorkspacelist.SelectedItems.Count == 0)
            {
                return false;
            }
            CheckInOutWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as CheckInOutWorkspaceInfo;
            if (this.int_0 == 0)
            {
                CheckOutHelper.m_pHelper.MasterWorkspaceName = (tag.Workspace as IDataset).FullName as IWorkspaceName;
                edit = new NamesEnumeratorClass();
                enumName = tag.EnumName;
                enumName.Reset();
                name2 = enumName.Next();
                list = new ArrayList();
                list2 = new ArrayList();
                while (name2 != null)
                {
                    if (name2 is IFeatureClassName)
                    {
                        if ((name2 as IFeatureClassName).FeatureDatasetName != null)
                        {
                            featureDatasetName = (name2 as IFeatureClassName).FeatureDatasetName;
                            name = featureDatasetName.Name;
                            if (list.IndexOf(name) == -1)
                            {
                                list.Add(name);
                                edit.Add(featureDatasetName as IName);
                            }
                        }
                        else
                        {
                            name = (name2 as IDatasetName).Name;
                            if (list2.IndexOf(name) == -1)
                            {
                                list2.Add(name);
                                edit.Add(name2);
                            }
                        }
                    }
                    else if (name2 is IFeatureDatasetName)
                    {
                        name = (name2 as IDatasetName).Name;
                        if (list.IndexOf(name) == -1)
                        {
                            list.Add(name);
                            edit.Add(name2);
                        }
                    }
                    else
                    {
                        name = (name2 as IDatasetName).Name;
                        if (list2.IndexOf(name) == -1)
                        {
                            list2.Add(name);
                            edit.Add(name2);
                        }
                    }
                    name2 = enumName.Next();
                }
                CheckOutHelper.m_pHelper.EnumName = edit as IEnumName;
            }
            else if (this.int_0 == 1)
            {
                IWorkspaceName fullName = (tag.Workspace as IDataset).FullName as IWorkspaceName;
                if (fullName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    CheckInHelper.m_pHelper.MasterWorkspaceName = fullName;
                    CheckInHelper.m_pHelper.CheckoutWorkspaceName = null;
                }
                else
                {
                    CheckInHelper.m_pHelper.MasterWorkspaceName = null;
                    CheckInHelper.m_pHelper.CheckoutWorkspaceName = fullName;
                }
            }
            else if (this.int_0 == 2)
            {
                ExtractionDataHelper.m_pHelper.MasterWorkspaceName =
                    (tag.Workspace as IDataset).FullName as IWorkspaceName;
                edit = new NamesEnumeratorClass();
                enumName = tag.EnumName;
                enumName.Reset();
                name2 = enumName.Next();
                list = new ArrayList();
                list2 = new ArrayList();
                while (name2 != null)
                {
                    if (name2 is IFeatureClassName)
                    {
                        if ((name2 as IFeatureClassName).FeatureDatasetName != null)
                        {
                            featureDatasetName = (name2 as IFeatureClassName).FeatureDatasetName;
                            name = featureDatasetName.Name;
                            if (list.IndexOf(name) == -1)
                            {
                                list.Add(name);
                                edit.Add(featureDatasetName as IName);
                            }
                        }
                        else
                        {
                            name = (name2 as IDatasetName).Name;
                            if (list2.IndexOf(name) == -1)
                            {
                                list2.Add(name);
                                edit.Add(name2);
                            }
                        }
                    }
                    else if (name2 is IFeatureDatasetName)
                    {
                        name = (name2 as IDatasetName).Name;
                        if (list.IndexOf(name) == -1)
                        {
                            list.Add(name);
                            edit.Add(name2);
                        }
                    }
                    else
                    {
                        name = (name2 as IDatasetName).Name;
                        if (list2.IndexOf(name) == -1)
                        {
                            list2.Add(name);
                            edit.Add(name2);
                        }
                    }
                    name2 = enumName.Next();
                }
                ExtractionDataHelper.m_pHelper.EnumName = edit as IEnumName;
            }
            else if (this.int_0 == 3)
            {
                ExportChangesHelper.m_pHelper.CheckoutWorkspaceName =
                    (tag.Workspace as IDataset).FullName as IWorkspaceName;
            }
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

        private void method_0(IWorkspace iworkspace_0, out string string_0, out string string_1)
        {
            string_0 = iworkspace_0.PathName;
            if (iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                IPropertySet connectionProperties = iworkspace_0.ConnectionProperties;
                string_0 = connectionProperties.GetProperty("Version").ToString();
                string str = connectionProperties.GetProperty("DB_CONNECTION_PROPERTIES").ToString();
                string_0 = string_0 + "(" + str + ")";
                string_1 = "空间数据库连接";
            }
            else
            {
                string_1 = "个人空间数据库";
            }
        }

        private void SelectCheckOutWorkspaceCtrl_Load(object sender, EventArgs e)
        {
            if (this.int_0 == 1)
            {
                this.label1.Text = "选择要检入的空间数据库";
                this.label2.Text = "可以检入的图层和表";
            }
            else if (this.int_0 == 2)
            {
                this.label1.Text = "选择要提取数据的空间数据库";
                this.label2.Text = "可以提取的图层和表";
            }
            else if (this.int_0 == 3)
            {
                this.label1.Text = "选择要导出变化的检出空间数据库";
                this.label2.Text = "可以导出变化的图层和表";
            }
            if (this.iarray_0 != null)
            {
                string[] items = new string[2];
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(i) as CheckInOutWorkspaceInfo;
                    this.method_0(info.Workspace, out items[0], out items[1]);
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