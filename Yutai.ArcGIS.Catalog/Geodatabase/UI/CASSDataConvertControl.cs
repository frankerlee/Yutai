using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class CASSDataConvertControl : UserControl
    {
        private Container container_0 = null;
        private IArray iarray_0 = new ArrayClass();
        private IArray iarray_1 = new ArrayClass();
        private IGxObject igxObject_0 = null;
        private IGxObject igxObject_1 = null;
        private IGxObjectFilter igxObjectFilter_0 = null;
        private IName iname_0 = null;
        private IName iname_1 = null;
        private int int_0 = 0;
        private int int_1 = 1;

        public CASSDataConvertControl()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        public void Add(IGxObjectFilter igxObjectFilter_1)
        {
            if (igxObjectFilter_1 != null)
            {
                this.iarray_1.Add(igxObjectFilter_1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                if (this.listView1.Items[i].Selected)
                {
                    this.iarray_0.Remove(i);
                    this.listView1.Items.RemoveAt(i);
                }
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "CASS交换文件(*.cas)|*.cas",
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    IDatasetName unk = new CadDrawingNameClass();
                    IWorkspaceName name2 = new WorkspaceNameClass
                    {
                        WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                        PathName = Path.GetDirectoryName(dialog.FileNames[i])
                    };
                    unk.Name = Path.GetFileName(dialog.FileNames[i]);
                    unk.WorkspaceName = name2;
                    this.listView1.Items.Add(dialog.FileNames[i]);
                    this.iarray_0.Add(unk);
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile
            {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            if (this.igxObjectFilter_0 != null)
            {
                file.AddFilter(this.igxObjectFilter_0, true);
            }
            else
            {
                file.AddFilter(new MyGxFilterWorkspaces(), true);
                file.AddFilter(new MyGxFilterFeatureDatasets(), false);
            }
            if (file.DoModalSaveLocation() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.igxObject_1 = items.get_Element(0) as IGxObject;
                    this.iname_1 = this.igxObject_1.InternalObjectName;
                    if (this.igxObject_1 is IGxDatabase)
                    {
                        this.iname_1 = this.igxObject_1.InternalObjectName;
                    }
                    else if (this.igxObject_1 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass
                        {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (this.igxObject_1.InternalObjectName as IFileName).Path
                        };
                        this.iname_1 = name as IName;
                    }
                    else if (this.igxObject_1 is IGxDataset)
                    {
                        IDatasetName internalObjectName = this.igxObject_1.InternalObjectName as IDatasetName;
                        if (internalObjectName.Type != esriDatasetType.esriDTFeatureDataset)
                        {
                            return;
                        }
                        this.iname_1 = internalObjectName as IName;
                    }
                    this.txtOutLocation.Text = this.igxObject_1.FullName;
                }
            }
        }

        public bool CanDo()
        {
            if (this.iarray_0.Count == 0)
            {
                MessageBox.Show("请输入需要转换的要素类或表");
                return false;
            }
            if (this.iname_1 == null)
            {
                MessageBox.Show("请选择输出位置");
                return false;
            }
            return true;
        }

        private void CASSDataConvertControl_Load(object sender, EventArgs e)
        {
            this.method_5();
        }

        public void Clear()
        {
            this.iarray_1.RemoveAll();
        }

        public void Do()
        {
            try
            {
                this.panel1.Visible = true;
                Dataloaders dataloaders = new Dataloaders();
                (dataloaders.Converter as IFeatureProgress_Event).Step +=
                    (new IFeatureProgress_StepEventHandler(this.method_12));
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = this.iarray_0.Count;
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    this.progressBar1.Increment(1);
                    IDatasetName name1 = this.iarray_0.get_Element(i) as IDatasetName;
                }
                this.igxObject_1.Refresh();
                this.panel1.Visible = false;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
                MessageBox.Show(exception.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
        }

        private IGxCatalog method_0(IGxObject igxObject_2)
        {
            if (igxObject_2 is IGxCatalog)
            {
                return (igxObject_2 as IGxCatalog);
            }
            for (IGxObject obj2 = igxObject_2.Parent; obj2 != null; obj2 = obj2.Parent)
            {
                if (obj2 is IGxCatalog)
                {
                    return (obj2 as IGxCatalog);
                }
            }
            return null;
        }

        private IDatasetName method_1(IDatasetName idatasetName_0, string string_0)
        {
            try
            {
                IWorkspaceName name = new WorkspaceNameClass
                {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = idatasetName_0.WorkspaceName.PathName
                };
                return new FeatureClassNameClass {Name = idatasetName_0.Name + ":" + string_0, WorkspaceName = name};
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return null;
        }

        private void method_10(int int_2)
        {
        }

        private void method_11(int int_2)
        {
            this.int_1 = int_2;
        }

        private void method_12()
        {
            this.int_0++;
            this.lblObj.Text = "处理第 " + this.int_0.ToString() + "个对象";
            this.progressBar2.Increment(this.int_1);
            Application.DoEvents();
        }

        private IDatasetName method_2(IDatasetName idatasetName_0, string string_0)
        {
            try
            {
                new GxCadDataset();
                IDatasetName name = new FeatureDatasetNameClass();
                IWorkspaceName name2 = new WorkspaceNameClass
                {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = idatasetName_0.WorkspaceName.PathName
                };
                name.Name = idatasetName_0.Name + ":" + string_0;
                name.WorkspaceName = name2;
                return name;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return null;
        }

        private IDatasetName method_3(IDatasetName idatasetName_0, string string_0)
        {
            try
            {
                new GxCadDataset();
                IDatasetName name = new TableNameClass();
                IWorkspaceName name2 = new WorkspaceNameClass
                {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = idatasetName_0.WorkspaceName.PathName
                };
                name.Name = idatasetName_0.Name + ":" + string_0;
                name.WorkspaceName = name2;
                return name;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return null;
        }

        private void method_4(IName iname_2, Dataloaders dataloaders_0)
        {
            IWorkspace2 workspace;
            string str2;
            int num;
            string name = (iname_2 as IDatasetName).Name;
            this.labelFeatureClass.Text = "转换:" + name;
            Application.DoEvents();
            if (this.iname_1 is IWorkspaceName)
            {
                if (((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriLocalDatabaseWorkspace) ||
                    ((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriRemoteDatabaseWorkspace))
                {
                    workspace = this.iname_1.Open() as IWorkspace2;
                    str2 = name;
                    num = 1;
                    try
                    {
                        while (workspace.get_NameExists((iname_2 as IDatasetName).Type, str2))
                        {
                            str2 = name + "_" + num.ToString();
                            num++;
                        }
                    }
                    catch
                    {
                    }
                    name = str2;
                }
                else
                {
                    string str3 = (this.iname_1 as IWorkspaceName).PathName + @"\" + name;
                    str2 = str3 + ".shp";
                    for (num = 1; File.Exists(str2); num++)
                    {
                        str2 = str3 + "_" + num.ToString() + ".shp";
                    }
                    name = Path.GetFileNameWithoutExtension(str2);
                }
            }
            else if (this.iname_1 is IDatasetName)
            {
                workspace = (this.iname_1.Open() as IDataset).Workspace as IWorkspace2;
                str2 = name;
                for (num = 1; workspace.get_NameExists((iname_2 as IDatasetName).Type, str2); num++)
                {
                    str2 = name + "_" + num.ToString();
                }
                name = str2;
            }
            dataloaders_0.ConvertData(iname_2 as IDatasetName, this.iname_1, name, null);
        }

        private void method_5()
        {
            if (this.igxObject_1 != null)
            {
                if (this.igxObject_1 is IGxDatabase)
                {
                    this.txtOutLocation.Text = this.igxObject_1.FullName;
                    this.iname_1 = this.igxObject_1.InternalObjectName;
                }
                else if (this.igxObject_1 is IGxDataset)
                {
                    this.txtOutLocation.Text = this.igxObject_1.FullName;
                    this.iname_1 = this.igxObject_1.InternalObjectName;
                }
                else if (this.igxObject_1 is IGxFolder)
                {
                    IWorkspaceName name = new WorkspaceNameClass
                    {
                        WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                        PathName = (this.igxObject_1.InternalObjectName as IFileName).Path
                    };
                    this.iname_1 = name as IName;
                }
            }
        }

        private bool method_6()
        {
            return false;
        }

        private void method_7(string string_0)
        {
        }

        private void method_8(int int_2)
        {
            if (int_2 > 0)
            {
                this.progressBar2.Maximum = int_2;
            }
        }

        private void method_9(int int_2)
        {
            this.int_0 = int_2;
            this.progressBar2.Minimum = int_2;
        }

        public IGxObject InGxObject
        {
            set { this.igxObject_0 = value; }
        }

        public IGxObject OutGxObject
        {
            set { this.igxObject_1 = value; }
        }

        public IGxObjectFilter OutGxObjectFilter
        {
            set { this.igxObjectFilter_0 = value; }
        }
    }
}