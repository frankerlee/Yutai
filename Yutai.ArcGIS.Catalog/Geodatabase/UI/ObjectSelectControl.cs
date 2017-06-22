using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class ObjectSelectControl : UserControl
    {
        private Container container_0 = null;
        [CompilerGenerated]
        private frmOpenFile frmOpenFile_0 = new frmOpenFile();
        private IGxObject igxObject_0 = null;
        private IGxObject igxObject_1 = null;
        private IName iname_0 = null;
        private IName iname_1 = null;
        private int int_0 = 0;
        private int int_1 = 1;

        public ObjectSelectControl()
        {
            this.InitializeComponent();
        }

        private void btnQueryDef_Click(object sender, EventArgs e)
        {
            if (this.igxObject_0 != null)
            {
                frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder {
                    Table = (this.igxObject_0 as IGxDataset).Dataset as ITable,
                    WhereCaluse = this.txtWhere.Text
                };
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    this.txtWhere.Text = builder.WhereCaluse;
                }
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            this.frmOpenFile_0.Text = "选择要素";
            this.frmOpenFile_0.RemoveAllFilters();
            if (this.ImportDatasetType == esriDatasetType.esriDTTable)
            {
                this.frmOpenFile_0.AddFilter(new MyGxFilterTables(), true);
            }
            else
            {
                this.frmOpenFile_0.AddFilter(new MyGxFilterFeatureClasses(), true);
            }
            if (this.frmOpenFile_0.DoModalOpen() == DialogResult.OK)
            {
                IArray items = this.frmOpenFile_0.Items;
                if (items.Count != 0)
                {
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    this.iname_0 = this.igxObject_0.InternalObjectName;
                    this.textEditInputFeatures.Text = this.igxObject_0.FullName;
                    this.txtOutFeatureClassName.Text = this.igxObject_0.Name;
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            try
            {
                this.frmOpenFile_0.Text = "保存位置";
                this.frmOpenFile_0.RemoveAllFilters();
                this.frmOpenFile_0.AddFilter(new MyGxFilterWorkspaces(), true);
                this.frmOpenFile_0.AddFilter(new MyGxFilterFeatureDatasets(), false);
                if (this.frmOpenFile_0.DoModalSaveLocation() == DialogResult.OK)
                {
                    IArray items = this.frmOpenFile_0.Items;
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
                            if (internalObjectName.Type == esriDatasetType.esriDTFeatureDataset)
                            {
                                this.iname_1 = internalObjectName as IName;
                            }
                            else
                            {
                                return;
                            }
                        }
                        this.txtOutLocation.Text = this.igxObject_1.FullName;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public bool CanDo()
        {
            if (this.iname_0 == null)
            {
                MessageBox.Show("请输入需要转换的要素类或表");
                return false;
            }
            if (this.iname_1 == null)
            {
                MessageBox.Show("请选择输出位置");
                return false;
            }
            if (this.txtOutFeatureClassName.Text.Length == 0)
            {
                MessageBox.Show("请选择输出要素名");
                return false;
            }
            return true;
        }

 public void Do()
        {
            try
            {
                IDatasetName name;
                this.panel1.Visible = true;
                Dataloaders dataloaders = new Dataloaders();
                (dataloaders.Converter as IFeatureProgress_Event).Step+=(new IFeatureProgress_StepEventHandler(this.method_6));
                IQueryFilter filter = null;
                if (this.txtWhere.Text.Length > 0)
                {
                    filter = new QueryFilterClass {
                        WhereClause = this.txtWhere.Text
                    };
                }
                dataloaders.ConvertData(this.iname_0 as IDatasetName, this.iname_1, this.txtOutFeatureClassName.Text, filter);
                IGxObject obj2 = null;
                this.method_0(this.igxObject_1);
                if (this.iname_1 is IWorkspaceName)
                {
                    if ((this.iname_1 as IWorkspaceName).Type == esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        obj2 = new GxShapefileDataset();
                    }
                    else
                    {
                        obj2 = new GxDataset();
                    }
                    if ((this.iname_0 as IDatasetName).Type == esriDatasetType.esriDTFeatureClass)
                    {
                        name = new FeatureClassNameClass();
                    }
                    else
                    {
                        name = new TableNameClass();
                    }
                    name.Name = this.txtOutFeatureClassName.Text;
                    name.WorkspaceName = this.iname_1 as IWorkspaceName;
                    (obj2 as IGxDataset).DatasetName = name;
                    this.igxObject_1.Refresh();
                }
                else if (this.iname_1 is IDatasetName)
                {
                    obj2 = new GxDataset();
                    if ((this.iname_0 as IDatasetName).Type == esriDatasetType.esriDTFeatureClass)
                    {
                        name = new FeatureClassNameClass();
                    }
                    else
                    {
                        name = new TableNameClass();
                    }
                    name.Name = this.txtOutFeatureClassName.Text;
                    name.WorkspaceName = (this.iname_1 as IDatasetName).WorkspaceName;
                    (obj2 as IGxDataset).DatasetName = name;
                    this.igxObject_1.Refresh();
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
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

        private void method_1()
        {
            if (this.igxObject_0 != null)
            {
                this.textEditInputFeatures.Text = this.igxObject_0.FullName;
                this.iname_0 = this.igxObject_0.InternalObjectName;
            }
            else if (this.igxObject_1 != null)
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

        private bool method_2()
        {
            return false;
        }

        private void method_3(int int_2)
        {
            this.progressBar1.Maximum = int_2;
        }

        private void method_4(int int_2)
        {
            this.int_0 = int_2;
            this.progressBar1.Minimum = int_2;
        }

        private void method_5(int int_2)
        {
            this.int_1 = int_2;
        }

        private void method_6()
        {
            this.int_0++;
            this.lblInfo.Text = "处理第 " + this.int_0.ToString() + "个对象";
            this.progressBar1.Increment(this.int_1);
            Application.DoEvents();
        }

        private void ObjectSelectControl_Load(object sender, EventArgs e)
        {
            this.method_1();
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

        public IGxObject OutGxObject
        {
            set
            {
                this.igxObject_1 = value;
            }
        }
    }
}

