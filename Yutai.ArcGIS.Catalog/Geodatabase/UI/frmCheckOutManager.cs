using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCheckOutManager : Form
    {
        private Container container_0 = null;
        private IArray iarray_0 = null;
        private IWorkspace iworkspace_0 = null;

        public frmCheckOutManager()
        {
            this.InitializeComponent();
        }

 private void frmCheckOutManager_Load(object sender, EventArgs e)
        {
            if (this.iarray_0.Count == 1)
            {
                CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(0) as CheckInOutWorkspaceInfo;
                this.iworkspace_0 = info.Workspace;
            }
            else
            {
                if (this.iarray_0.Count <= 0)
                {
                    return;
                }
                frmSelectWorkspace workspace = new frmSelectWorkspace {
                    WorkspaceArray = this.iarray_0
                };
                if (workspace.ShowDialog() != DialogResult.OK)
                {
                    base.Close();
                    return;
                }
                this.iworkspace_0 = workspace.Workspace;
            }
            IWorkspaceReplicas replicas = this.iworkspace_0 as IWorkspaceReplicas;
            IEnumReplica replica = replicas.Replicas;
            replica.Reset();
            IReplica replica2 = replica.Next();
            string[] items = new string[4];
            while (replica2 != null)
            {
                items[0] = replica2.Name;
                items[1] = replica2.Owner;
                items[2] = replica2.Version;
                items[3] = new DateTime((long) replica2.ReplicaDate).ToString();
                ListViewItem item = new ListViewItem(items) {
                    Tag = replica2
                };
                this.listView1.Items.Add(item);
                replica2 = replica.Next();
            }
        }

 private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                new frmCheckOutProperty(this.listView1.SelectedItems[0].Tag as IReplica, this.iworkspace_0).ShowDialog();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

