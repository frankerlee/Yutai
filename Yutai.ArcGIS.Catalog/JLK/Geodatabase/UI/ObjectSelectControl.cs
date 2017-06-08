namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Query.UI;
    using JLK.Utility;
    using JLK.Utility.Geodatabase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ObjectSelectControl : UserControl
    {
        private SimpleButton btnQueryDef;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private Container container_0 = null;
        [CompilerGenerated]
        private esriDatasetType esriDatasetType_0;
        private frmOpenFile frmOpenFile_0 = new frmOpenFile();
        private IGxObject igxObject_0 = null;
        private IGxObject igxObject_1 = null;
        private IName iname_0 = null;
        private IName iname_1 = null;
        private int int_0 = 0;
        private int int_1 = 1;
        private Label label1;
        private Label label2;
        private Label lblInfo;
        private Label lblOutFeatureClassName;
        private Label lblSelectObjects;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private TextEdit textEditInputFeatures;
        private TextEdit txtOutFeatureClassName;
        private TextEdit txtOutLocation;
        private TextEdit txtWhere;

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
                IDatasetName name;
                this.panel1.Visible = true;
                Dataloaders dataloaders = new Dataloaders();
                (dataloaders.Converter as IFeatureProgress_Event).add_Step(new IFeatureProgress_StepEventHandler(this.method_6));
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
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ObjectSelectControl));
            this.lblSelectObjects = new Label();
            this.textEditInputFeatures = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutLocation = new TextEdit();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.lblOutFeatureClassName = new Label();
            this.txtOutFeatureClassName = new TextEdit();
            this.label1 = new Label();
            this.txtWhere = new TextEdit();
            this.btnQueryDef = new SimpleButton();
            this.panel1 = new Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new Label();
            this.textEditInputFeatures.Properties.BeginInit();
            this.txtOutLocation.Properties.BeginInit();
            this.txtOutFeatureClassName.Properties.BeginInit();
            this.txtWhere.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x38, 0x10);
            this.lblSelectObjects.TabIndex = 0;
            this.lblSelectObjects.Text = "输入要素类";
            this.textEditInputFeatures.EditValue = "";
            this.textEditInputFeatures.Location = new Point(8, 0x18);
            this.textEditInputFeatures.Name = "textEditInputFeatures";
            this.textEditInputFeatures.Properties.ReadOnly = true;
            this.textEditInputFeatures.Size = new Size(0x100, 0x15);
            this.textEditInputFeatures.TabIndex = 1;
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x110, 0x18);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 2;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new Point(8, 0x48);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(0xf8, 0x15);
            this.txtOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Image = (Image) manager.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(0x110, 0x48);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 4;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出位置";
            this.lblOutFeatureClassName.AutoSize = true;
            this.lblOutFeatureClassName.Location = new Point(8, 0x68);
            this.lblOutFeatureClassName.Name = "lblOutFeatureClassName";
            this.lblOutFeatureClassName.Size = new Size(0x4d, 12);
            this.lblOutFeatureClassName.TabIndex = 6;
            this.lblOutFeatureClassName.Text = "输出要素类名";
            this.txtOutFeatureClassName.EditValue = "";
            this.txtOutFeatureClassName.Location = new Point(8, 120);
            this.txtOutFeatureClassName.Name = "txtOutFeatureClassName";
            this.txtOutFeatureClassName.Size = new Size(0xf8, 0x15);
            this.txtOutFeatureClassName.TabIndex = 7;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x98);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "表达式(可选)";
            this.txtWhere.EditValue = "";
            this.txtWhere.Location = new Point(8, 0xa8);
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.Size = new Size(0xf8, 0x15);
            this.txtWhere.TabIndex = 9;
            this.btnQueryDef.Image = (Image) manager.GetObject("btnQueryDef.Image");
            this.btnQueryDef.Location = new Point(0x108, 0xa8);
            this.btnQueryDef.Name = "btnQueryDef";
            this.btnQueryDef.Size = new Size(0x18, 0x18);
            this.btnQueryDef.TabIndex = 10;
            this.btnQueryDef.Click += new EventHandler(this.btnQueryDef_Click);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x120, 0xc0);
            this.panel1.TabIndex = 11;
            this.panel1.Visible = false;
            this.progressBar1.Location = new Point(0x10, 0x30);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0xf8, 0x18);
            this.progressBar1.TabIndex = 1;
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new Point(0x10, 0x10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(0, 12);
            this.lblInfo.TabIndex = 0;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnQueryDef);
            base.Controls.Add(this.txtWhere);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtOutFeatureClassName);
            base.Controls.Add(this.lblOutFeatureClassName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.textEditInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Name = "ObjectSelectControl";
            base.Size = new Size(0x130, 0xd0);
            base.Load += new EventHandler(this.ObjectSelectControl_Load);
            this.textEditInputFeatures.Properties.EndInit();
            this.txtOutLocation.Properties.EndInit();
            this.txtOutFeatureClassName.Properties.EndInit();
            this.txtWhere.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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

