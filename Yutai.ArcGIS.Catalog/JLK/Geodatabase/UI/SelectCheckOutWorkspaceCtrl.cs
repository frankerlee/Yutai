namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class SelectCheckOutWorkspaceCtrl : UserControl
    {
        private ListBox CanEditDatasetList;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private ListView EditWorkspacelist;
        private IArray iarray_0 = null;
        private int int_0 = 0;
        private Label label1;
        private Label label2;

        public SelectCheckOutWorkspaceCtrl(int int_1)
        {
            this.InitializeComponent();
            this.int_0 = int_1;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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
                ExtractionDataHelper.m_pHelper.MasterWorkspaceName = (tag.Workspace as IDataset).FullName as IWorkspaceName;
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
                ExportChangesHelper.m_pHelper.CheckoutWorkspaceName = (tag.Workspace as IDataset).FullName as IWorkspaceName;
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
            this.CanEditDatasetList.Location = new Point(0x10, 160);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(280, 0x58);
            this.CanEditDatasetList.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x88);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x80, 0x11);
            this.label2.TabIndex = 6;
            this.label2.Text = "以下层或表检出时可用";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(140, 0x11);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择要检出的空间数据库";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.EditWorkspacelist.Location = new Point(0x10, 0x20);
            this.EditWorkspacelist.MultiSelect = false;
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(280, 0x60);
            this.EditWorkspacelist.TabIndex = 5;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.columnHeader_0.Text = "源";
            this.columnHeader_0.Width = 0x93;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 0x77;
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.EditWorkspacelist);
            base.Name = "SelectCheckOutWorkspaceCtrl";
            base.Size = new Size(0x158, 0x128);
            base.Load += new EventHandler(this.SelectCheckOutWorkspaceCtrl_Load);
            base.ResumeLayout(false);
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
                    ListViewItem item = new ListViewItem(items) {
                        Tag = info
                    };
                    this.EditWorkspacelist.Items.Add(item);
                }
            }
        }

        public IArray WorkspaceArray
        {
            set
            {
                this.iarray_0 = value;
            }
        }
    }
}

