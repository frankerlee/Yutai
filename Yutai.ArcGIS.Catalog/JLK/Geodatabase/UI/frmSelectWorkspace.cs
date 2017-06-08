namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class frmSelectWorkspace : Form
    {
        private SimpleButton btnOK;
        private ListBox CanEditDatasetList;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private ListView EditWorkspacelist;
        private IArray iarray_0 = null;
        private IWorkspace iworkspace_0 = null;
        private Label label1;
        private Label label2;
        private SimpleButton simpleButton1;

        public frmSelectWorkspace()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.EditWorkspacelist.SelectedItems.Count != 0)
            {
                CheckInOutWorkspaceInfo tag = this.EditWorkspacelist.SelectedItems[0].Tag as CheckInOutWorkspaceInfo;
                this.iworkspace_0 = tag.Workspace;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void frmSelectWorkspace_Load(object sender, EventArgs e)
        {
            string[] items = new string[2];
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(i) as CheckInOutWorkspaceInfo;
                this.method_2(info.Workspace, out items[0], out items[1]);
                ListViewItem item = new ListViewItem(items) {
                    Tag = info
                };
                this.EditWorkspacelist.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmSelectWorkspace));
            this.label1 = new Label();
            this.EditWorkspacelist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label2 = new Label();
            this.CanEditDatasetList = new ListBox();
            this.btnOK = new SimpleButton();
            this.simpleButton1 = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "空间数据库";
            this.EditWorkspacelist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.EditWorkspacelist.Location = new Point(8, 0x20);
            this.EditWorkspacelist.Name = "EditWorkspacelist";
            this.EditWorkspacelist.Size = new Size(0x108, 0x70);
            this.EditWorkspacelist.TabIndex = 1;
            this.EditWorkspacelist.UseCompatibleStateImageBehavior = false;
            this.EditWorkspacelist.View = View.Details;
            this.EditWorkspacelist.SelectedIndexChanged += new EventHandler(this.EditWorkspacelist_SelectedIndexChanged);
            this.columnHeader_0.Text = "";
            this.columnHeader_0.Width = 0x62;
            this.columnHeader_1.Text = "";
            this.columnHeader_1.Width = 0x76;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x98);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "空间数据库层和表";
            this.CanEditDatasetList.ItemHeight = 12;
            this.CanEditDatasetList.Location = new Point(0x10, 0xb0);
            this.CanEditDatasetList.Name = "CanEditDatasetList";
            this.CanEditDatasetList.Size = new Size(0x100, 0x58);
            this.CanEditDatasetList.TabIndex = 3;
            this.btnOK.Location = new Point(0x90, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton1.DialogResult = DialogResult.Cancel;
            this.simpleButton1.Location = new Point(0xd8, 280);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x38, 0x18);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 0x13d);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.CanEditDatasetList);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.EditWorkspacelist);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmSelectWorkspace";
            this.Text = "frmSelectWorkspace";
            base.Load += new EventHandler(this.frmSelectWorkspace_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(IMap imap_0)
        {
            int num;
            IDataset featureClass;
            IWorkspace workspace;
            IVersionedObject obj2;
            this.iarray_0 = new ArrayClass();
            new PropertySetClass();
            IFeatureLayer layer = null;
            for (num = 0; num < imap_0.LayerCount; num++)
            {
                ILayer layer2 = imap_0.get_Layer(num);
                if (layer2 is IGroupLayer)
                {
                    this.method_1(this.iarray_0, layer2 as ICompositeLayer);
                }
                else if (layer2 is IFeatureLayer)
                {
                    layer = layer2 as IFeatureLayer;
                    featureClass = layer.FeatureClass as IDataset;
                    if ((featureClass != null) && !(featureClass is ICoverageFeatureClass))
                    {
                        workspace = featureClass.Workspace;
                        if (workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            obj2 = layer.FeatureClass as IVersionedObject;
                            if (obj2.IsRegisteredAsVersioned)
                            {
                                (this.method_3(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
                            }
                        }
                    }
                }
            }
            IStandaloneTableCollection tables = imap_0 as IStandaloneTableCollection;
            for (num = 0; num < tables.StandaloneTableCount; num++)
            {
                ITable table = tables.get_StandaloneTable(num) as ITable;
                if (table is IDataset)
                {
                    featureClass = table as IDataset;
                    workspace = featureClass.Workspace;
                    if ((workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && (workspace is IWorkspaceEdit))
                    {
                        obj2 = table as IVersionedObject;
                        if (obj2.IsRegisteredAsVersioned)
                        {
                            (this.method_3(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
                        }
                    }
                }
            }
        }

        private void method_1(IArray iarray_1, ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(iarray_1, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = layer as IFeatureLayer;
                    IDataset featureClass = layer2.FeatureClass as IDataset;
                    if ((featureClass != null) && !(featureClass is ICoverageFeatureClass))
                    {
                        IWorkspace workspace = featureClass.Workspace;
                        if (workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            IVersionedObject obj2 = layer2.FeatureClass as IVersionedObject;
                            if (obj2.IsRegisteredAsVersioned)
                            {
                                (this.method_3(iarray_1, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
                            }
                        }
                    }
                }
            }
        }

        private void method_2(IWorkspace iworkspace_1, out string string_0, out string string_1)
        {
            string_0 = iworkspace_1.PathName;
            if (iworkspace_1.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                IPropertySet connectionProperties = iworkspace_1.ConnectionProperties;
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

        private CheckInOutWorkspaceInfo method_3(IArray iarray_1, IWorkspace iworkspace_1)
        {
            CheckInOutWorkspaceInfo info;
            IPropertySet connectionProperties = iworkspace_1.ConnectionProperties;
            for (int i = 0; i < iarray_1.Count; i++)
            {
                info = iarray_1.get_Element(i) as CheckInOutWorkspaceInfo;
                if (connectionProperties.IsEqual(info.Workspace.ConnectionProperties))
                {
                    return info;
                }
            }
            info = new CheckInOutWorkspaceInfo(iworkspace_1);
            iarray_1.Add(info);
            return info;
        }

        public object Hook
        {
            set
            {
                if (value is IMap)
                {
                    this.method_0(value as IMap);
                }
            }
        }

        public IWorkspace Workspace
        {
            get
            {
                return this.iworkspace_0;
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

