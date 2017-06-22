using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class MultiObjectClassSelectControl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        [CompilerGenerated]
        private IArray iarray_0 = new ArrayClass();
        private IArray iarray_1 = new ArrayClass();
        private IGxObject igxObject_0 = null;
        private IGxObject igxObject_1 = null;
        private IName iname_0 = null;
        private IName iname_1 = null;
        private int int_0 = 0;
        private int int_1 = 1;

        public MultiObjectClassSelectControl()
        {
            this.InitializeComponent();
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            if (igxObjectFilter_0 != null)
            {
                this.iarray_1.Add(igxObjectFilter_0);
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
            int num;
            frmOpenFile file = new frmOpenFile {
                Text = "添加数据"
            };
            file.RemoveAllFilters();
            file.AllowMultiSelect = true;
            if (this.iarray_1.Count != 0)
            {
                for (num = 0; num < this.iarray_1.Count; num++)
                {
                    if (num == 0)
                    {
                        file.AddFilter(this.iarray_1.get_Element(num) as IGxObjectFilter, true);
                    }
                    else
                    {
                        file.AddFilter(this.iarray_1.get_Element(num) as IGxObjectFilter, false);
                    }
                }
            }
            else
            {
                file.AddFilter(new MyGxFilterFeatureClasses(), true);
            }
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    for (num = 0; num < items.Count; num++)
                    {
                        IGxObject obj2 = items.get_Element(num) as IGxObject;
                        if (obj2 is IGxDataset)
                        {
                            if ((obj2 as IGxDataset).Type == esriDatasetType.esriDTCadDrawing)
                            {
                                IDatasetName unk = this.method_3((obj2 as IGxDataset).DatasetName, "Point");
                                this.iarray_0.Add(unk);
                                this.listView1.Items.Add(obj2.FullName + ":" + unk.Name);
                                unk = this.method_3((obj2 as IGxDataset).DatasetName, "Polyline");
                                this.iarray_0.Add(unk);
                                this.listView1.Items.Add(obj2.FullName + ":" + unk.Name);
                                unk = this.method_3((obj2 as IGxDataset).DatasetName, "Polygon");
                                this.iarray_0.Add(unk);
                                this.listView1.Items.Add(obj2.FullName + ":" + unk.Name);
                                unk = this.method_3((obj2 as IGxDataset).DatasetName, "Annotation");
                                this.iarray_0.Add(unk);
                                this.listView1.Items.Add(obj2.FullName + ":" + unk.Name);
                            }
                            else
                            {
                                this.iarray_0.Add(obj2.InternalObjectName);
                                this.listView1.Items.Add(obj2.FullName);
                            }
                        }
                    }
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            file.AddFilter(new MyGxFilterFeatureDatasets(), false);
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
                        IWorkspaceName name = new WorkspaceNameClass {
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

        public void Clear()
        {
            this.iarray_1.RemoveAll();
        }

 public void Do()
        {
            Exception exception;
            try
            {
                this.panel1.Visible = true;
                Dataloaders dataloaders = new Dataloaders();
                (dataloaders.Converter as IFeatureProgress_Event).Step+=(new IFeatureProgress_StepEventHandler(this.method_10));
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = this.iarray_0.Count;
                int index = 0;
                while (true)
                {
                    if (index >= this.iarray_0.Count)
                    {
                        break;
                    }
                    this.int_0 = 0;
                    this.progressBar1.Increment(1);
                    IName name = this.iarray_0.get_Element(index) as IName;
                    string fileNameWithoutExtension = (name as IDatasetName).Name;
                    this.labelFeatureClass.Text = "转换:" + fileNameWithoutExtension;
                    string[] strArray = fileNameWithoutExtension.Split(new char[] { '.' });
                    fileNameWithoutExtension = strArray[strArray.Length - 1];
                    Application.DoEvents();
                    try
                    {
                        IWorkspace workspace;
                        string str2;
                        int num2;
                        if (this.iname_1 is IWorkspaceName)
                        {
                            if (((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriLocalDatabaseWorkspace) || ((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriRemoteDatabaseWorkspace))
                            {
                                workspace = this.iname_1.Open() as IWorkspace;
                                str2 = fileNameWithoutExtension;
                                num2 = 1;
                                while (this.method_1(workspace, (name as IDatasetName).Type, str2.ToLower()))
                                {
                                    str2 = fileNameWithoutExtension + "_" + num2.ToString();
                                    num2++;
                                }
                                fileNameWithoutExtension = str2;
                            }
                            else
                            {
                                string str3 = (this.iname_1 as IWorkspaceName).PathName + @"\" + fileNameWithoutExtension;
                                str2 = str3 + ".shp";
                                num2 = 1;
                                while (File.Exists(str2))
                                {
                                    str2 = str3 + "_" + num2.ToString() + ".shp";
                                    num2++;
                                }
                                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(str2);
                            }
                        }
                        else if (this.iname_1 is IDatasetName)
                        {
                            workspace = (this.iname_1.Open() as IDataset).Workspace;
                            str2 = fileNameWithoutExtension;
                            for (num2 = 1; this.method_1(workspace, (name as IDatasetName).Type, str2.ToLower()); num2++)
                            {
                                str2 = fileNameWithoutExtension + "_" + num2.ToString();
                            }
                            fileNameWithoutExtension = str2;
                        }
                        try
                        {
                            if (this.bool_0)
                            {
                                double num3 = double.Parse(this.txtScale.Text.Trim());
                                dataloaders.Scale = num3;
                            }
                        }
                        catch
                        {
                        }
                        dataloaders.ConvertData(name as IDatasetName, this.iname_1, fileNameWithoutExtension, null);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Logger.Current.Error("",exception, "");
                    }
                    index++;
                }
                this.igxObject_1.Refresh();
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Logger.Current.Error("",exception, "");
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

        private bool method_1(IWorkspace iworkspace_0, esriDatasetType esriDatasetType_1, string string_0)
        {
            try
            {
                if (esriDatasetType_1 == esriDatasetType.esriDTFeatureDataset)
                {
                    IEnumDatasetName name = iworkspace_0.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                    name.Reset();
                    for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                    {
                        string[] strArray = name2.Name.Split(new char[] { '.' });
                        string str = strArray[strArray.Length - 1].ToLower();
                        if (string_0 == str)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                if ((iworkspace_0 as IWorkspace2).get_NameExists(esriDatasetType_1, string_0))
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private void method_10()
        {
            this.int_0++;
            this.lblObj.Text = "处理第 " + this.int_0.ToString() + "个对象";
            this.progressBar2.Increment(this.int_1);
            Application.DoEvents();
        }

        private void method_2()
        {
            if ((this.igxObject_0 != null) && (this.igxObject_0 is IGxObjectContainer))
            {
                IEnumGxObject children = (this.igxObject_0 as IGxObjectContainer).Children;
                children.Reset();
                for (IGxObject obj3 = children.Next(); obj3 != null; obj3 = children.Next())
                {
                    this.iarray_0.Add(obj3.InternalObjectName);
                    this.listView1.Items.Add(obj3.FullName);
                }
            }
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
                    IWorkspaceName name = new WorkspaceNameClass {
                        WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                        PathName = (this.igxObject_1.InternalObjectName as IFileName).Path
                    };
                    this.iname_1 = name as IName;
                }
            }
        }

        private IDatasetName method_3(IDatasetName idatasetName_0, string string_0)
        {
            try
            {
                IWorkspaceName name = new WorkspaceNameClass {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = idatasetName_0.WorkspaceName.PathName
                };
                return new FeatureClassNameClass { Name = idatasetName_0.Name + ":" + string_0, WorkspaceName = name };
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
            return null;
        }

        private bool method_4()
        {
            return false;
        }

        private void method_5(string string_0)
        {
        }

        private void method_6(int int_2)
        {
            if (int_2 > 0)
            {
                this.progressBar2.Maximum = int_2;
            }
        }

        private void method_7(int int_2)
        {
            this.int_0 = int_2;
            this.progressBar2.Minimum = int_2;
        }

        private void method_8(int int_2)
        {
        }

        private void method_9(int int_2)
        {
            this.int_1 = int_2;
        }

        private void MultiObjectClassSelectControl_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Height -= this.panel2.Height;
            }
            this.method_2();
        }

        public esriDatasetType ImportDatasetType
        {
            [CompilerGenerated]
            protected get
            {
                return this.esriDatasetType_0;
            }
            [CompilerGenerated]
            set
            {
                this.esriDatasetType_0 = value;
            }
        }

        public IGxObject InGxObject
        {
            set
            {
                this.igxObject_0 = value;
            }
        }

        public bool IsAnnotation
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public IGxObject OutGxObject
        {
            set
            {
                this.igxObject_1 = value;
            }
        }
    }
}

