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
    internal class CASSDataConvertControl : UserControl
    {
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
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
        private Label label2;
        private Label labelFeatureClass;
        private Label lblObj;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private TextEdit txtOutLocation;

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
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "CASS交换文件(*.cas)|*.cas",
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    IDatasetName unk = new CadDrawingNameClass();
                    IWorkspaceName name2 = new WorkspaceNameClass {
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
            frmOpenFile file = new frmOpenFile {
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

        private void CASSDataConvertControl_Load(object sender, EventArgs e)
        {
            this.method_5();
        }

        public void Clear()
        {
            this.iarray_1.RemoveAll();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Do()
        {
            try
            {
                this.panel1.Visible = true;
                Dataloaders dataloaders = new Dataloaders();
                (dataloaders.Converter as IFeatureProgress_Event).Step+=(new IFeatureProgress_StepEventHandler(this.method_12));
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
                Logger.Current.Error("",exception, "");
                MessageBox.Show(exception.Message);
            }
        }

        private void InitializeComponent()
        {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CASSDataConvertControl));
            this.lblSelectObjects = new Label();
            this.txtOutLocation = new TextEdit();
            this.label2 = new Label();
            this.listView1 = new ListView();
            this.btnDelete = new SimpleButton();
            this.btnSelectInputFeatures = new SimpleButton();
            this.btnSelectOutLocation = new SimpleButton();
            this.panel1 = new Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblObj = new Label();
            this.labelFeatureClass = new Label();
            this.txtOutLocation.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x67, 0x11);
            this.lblSelectObjects.TabIndex = 0;
            this.lblSelectObjects.Text = "南方CASS交换文件";
            this.txtOutLocation.Location = new Point(8, 0xd8);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Size = new Size(0xf8, 0x17);
            this.txtOutLocation.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0xc0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出位置";
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(8, 0x20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x100, 0x98);
            this.listView1.TabIndex = 6;
            this.listView1.View = View.SmallIcon;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x110, 0x48);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x110, 0x20);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 8;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.btnSelectOutLocation.Image = (Image) resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(0x108, 0xd8);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 10;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblObj);
            this.panel1.Controls.Add(this.labelFeatureClass);
            this.panel1.Location = new Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x130, 240);
            this.panel1.TabIndex = 11;
            this.panel1.Visible = false;
            this.progressBar2.Location = new Point(8, 0x80);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(0x100, 0x18);
            this.progressBar2.TabIndex = 3;
            this.progressBar1.Location = new Point(8, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x100, 0x18);
            this.progressBar1.TabIndex = 2;
            this.lblObj.AutoSize = true;
            this.lblObj.Location = new Point(8, 0x55);
            this.lblObj.Name = "lblObj";
            this.lblObj.Size = new Size(0, 0x11);
            this.lblObj.TabIndex = 1;
            this.labelFeatureClass.AutoSize = true;
            this.labelFeatureClass.Location = new Point(8, 8);
            this.labelFeatureClass.Name = "labelFeatureClass";
            this.labelFeatureClass.Size = new Size(0, 0x11);
            this.labelFeatureClass.TabIndex = 0;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.lblSelectObjects);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.btnDelete);
            base.Name = "CASSDataConvertControl";
            base.Size = new Size(0x130, 0x108);
            base.Load += new EventHandler(this.CASSDataConvertControl_Load);
            this.txtOutLocation.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
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
                IWorkspaceName name2 = new WorkspaceNameClass {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = idatasetName_0.WorkspaceName.PathName
                };
                name.Name = idatasetName_0.Name + ":" + string_0;
                name.WorkspaceName = name2;
                return name;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
            return null;
        }

        private IDatasetName method_3(IDatasetName idatasetName_0, string string_0)
        {
            try
            {
                new GxCadDataset();
                IDatasetName name = new TableNameClass();
                IWorkspaceName name2 = new WorkspaceNameClass {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = idatasetName_0.WorkspaceName.PathName
                };
                name.Name = idatasetName_0.Name + ":" + string_0;
                name.WorkspaceName = name2;
                return name;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
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
                if (((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriLocalDatabaseWorkspace) || ((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriRemoteDatabaseWorkspace))
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
                    IWorkspaceName name = new WorkspaceNameClass {
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
            set
            {
                this.igxObject_0 = value;
            }
        }

        public IGxObject OutGxObject
        {
            set
            {
                this.igxObject_1 = value;
            }
        }

        public IGxObjectFilter OutGxObjectFilter
        {
            set
            {
                this.igxObjectFilter_0 = value;
            }
        }
    }
}

