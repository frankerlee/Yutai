namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility.Geodatabase;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class frmDatasetUnion : Form
    {
        private SimpleButton btnAdd;
        private SimpleButton btnCancel;
        private SimpleButton btnDelete;
        private SimpleButton btnOK;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutDataset;
        private SimpleButton btnSelectWorkspace;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IWorkspace iworkspace_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblProcess;
        private Panel panel1;
        private Panel panel2;
        private RadioGroup radioGroup1;
        private RadioGroup rdoDatasetType;
        private ListBoxControl SourceDatalistBox;
        private TextEdit txtDatasetName;
        private TextEdit txtInputFeatureClass;
        private TextEdit txtOutDataset;
        private TextEdit txtWorkspaceName;

        public frmDatasetUnion()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IGxDataset tag = this.txtInputFeatureClass.Tag as IGxDataset;
            if (tag == null)
            {
                MessageBox.Show("请选择要素类或表!");
            }
            else if ((tag.Type == esriDatasetType.esriDTFeatureClass) || (tag.Type == esriDatasetType.esriDTTable))
            {
                if (this.SourceDatalistBox.Items.Count == 0)
                {
                    this.SourceDatalistBox.Items.Add(tag);
                }
                else
                {
                    if (this.SourceDatalistBox.Items.IndexOf(tag) != -1)
                    {
                        MessageBox.Show("数据类已存在!");
                        return;
                    }
                    IGxDataset dataset2 = this.SourceDatalistBox.Items[0] as IGxDataset;
                    if (dataset2.Type != tag.Type)
                    {
                        MessageBox.Show("要加入的数据集和已有数据集类型不一致，不能添加！");
                        return;
                    }
                    this.SourceDatalistBox.Items.Add(tag);
                }
                this.txtInputFeatureClass.Tag = null;
                this.txtInputFeatureClass.Text = "";
                this.btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("请选择要素类或表!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.SourceDatalistBox.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int index = this.SourceDatalistBox.SelectedIndices[i];
                this.SourceDatalistBox.Items.RemoveAt(index);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ITable table = null;
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    if (this.txtWorkspaceName.Tag != null)
                    {
                        IGxDataset dataset;
                        IFields fields;
                        IFieldChecker checker;
                        IEnumFieldError error;
                        IFields fields2;
                        if (this.txtDatasetName.Text.Trim() == "")
                        {
                            MessageBox.Show("请选择新建数据集的名称!");
                            return;
                        }
                        if (this.SourceDatalistBox.Items.Count == 0)
                        {
                            MessageBox.Show("请选择要合并的数据集!");
                            return;
                        }
                        if (this.rdoDatasetType.SelectedIndex == 0)
                        {
                            if (this.method_0(this.iworkspace_0, esriDatasetType.esriDTFeatureClass, this.txtDatasetName.Text.Trim()))
                            {
                                MessageBox.Show("指定的要素类名已存在,请输入其它名字!");
                                return;
                            }
                            dataset = this.SourceDatalistBox.Items[0] as IGxDataset;
                            if (dataset.Type != esriDatasetType.esriDTFeatureClass)
                            {
                                MessageBox.Show("要导入的数据集数组中存在和目标数据集不一致数据集,无法完成导入!");
                                return;
                            }
                            if (!this.method_1(dataset.Dataset))
                            {
                                MessageBox.Show("要导入的数据集数组中存在和目标数据集不一致数据集,无法完成导入!");
                                return;
                            }
                            fields = (dataset.Dataset as ITable).Fields;
                            checker = new FieldCheckerClass {
                                ValidateWorkspace = this.iworkspace_0
                            };
                            checker.Validate(fields, out error, out fields2);
                            table = (this.iworkspace_0 as IFeatureWorkspace).CreateFeatureClass(this.txtDatasetName.Text.Trim(), fields2, null, null, esriFeatureType.esriFTSimple, "Shape", "") as ITable;
                        }
                        else
                        {
                            if (this.method_0(this.iworkspace_0, esriDatasetType.esriDTTable, this.txtDatasetName.Text.Trim()))
                            {
                                MessageBox.Show("指定的表名已存在,请输入其它名字!");
                                return;
                            }
                            dataset = this.SourceDatalistBox.Items[0] as IGxDataset;
                            if (dataset.Type != esriDatasetType.esriDTTable)
                            {
                                MessageBox.Show("要导入的数据集数组中存在和目标数据集不一致数据集,无法完成导入!");
                                return;
                            }
                            if (this.method_1(dataset.Dataset))
                            {
                                MessageBox.Show("要导入的数据集数组中存在和目标数据集不一致数据集,无法完成导入!");
                                return;
                            }
                            fields = (dataset.Dataset as ITable).Fields;
                            checker = new FieldCheckerClass {
                                ValidateWorkspace = this.iworkspace_0
                            };
                            checker.Validate(fields, out error, out fields2);
                            table = (this.iworkspace_0 as IFeatureWorkspace).CreateTable(this.txtDatasetName.Text.Trim(), fields2, null, null, "");
                        }
                        break;
                    }
                    MessageBox.Show("请选择新建数据集所在工作空间!");
                    return;

                case 1:
                    if (this.txtOutDataset.Tag != null)
                    {
                        if (this.SourceDatalistBox.Items.Count == 0)
                        {
                            MessageBox.Show("请选择要合并的数据集!");
                            return;
                        }
                        IDataset dataset2 = (this.txtOutDataset.Tag as IGxDataset).Dataset;
                        if (this.method_1(dataset2))
                        {
                            MessageBox.Show("要导入的数据集数组中存在和目标数据集不一致数据集,无法完成导入!");
                            return;
                        }
                        table = dataset2 as ITable;
                        break;
                    }
                    MessageBox.Show("请选择数据导入的目标数据集!");
                    return;
            }
            IList items = this.SourceDatalistBox.Items;
            Dataloaders dataloaders = new Dataloaders();
            for (int i = 0; i < items.Count; i++)
            {
                this.lblProcess.Text = "开始导入 " + (items[i] as IGxObject).FullName + " ...";
                ITable table2 = (items[i] as IGxObject).InternalObjectName.Open() as ITable;
                dataloaders.LoadData(table2, null, table, 800);
                Marshal.ReleaseComObject(table2);
                table2 = null;
            }
            MessageBox.Show("数据合并完成!");
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "选择数据集"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count == 0)
                {
                    return;
                }
                IGxObject obj2 = items.get_Element(0) as IGxObject;
                this.txtInputFeatureClass.Text = obj2.FullName;
                this.txtInputFeatureClass.Tag = obj2;
            }
            if (this.txtInputFeatureClass.Text.Length > 0)
            {
                this.btnAdd.Enabled = true;
            }
        }

        private void btnSelectOutDataset_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "选择数据集"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    IGxObject obj2 = items.get_Element(0) as IGxObject;
                    this.txtOutDataset.Text = obj2.FullName;
                    this.txtOutDataset.Tag = obj2;
                }
            }
        }

        private void btnSelectWorkspace_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "选择工作空间"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    IGxObject obj2 = items.get_Element(0) as IGxObject;
                    if (obj2 is IGxDatabase)
                    {
                        this.iworkspace_0 = (obj2 as IGxDatabase).Workspace;
                    }
                    else if (obj2 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (obj2.InternalObjectName as IFileName).Path
                        };
                        this.iworkspace_0 = (name as IName).Open() as IWorkspace;
                    }
                    this.txtWorkspaceName.Text = this.iworkspace_0.PathName;
                    this.txtWorkspaceName.Tag = this.iworkspace_0;
                }
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmDatasetUnion));
            this.radioGroup1 = new RadioGroup();
            this.panel1 = new Panel();
            this.rdoDatasetType = new RadioGroup();
            this.btnSelectWorkspace = new SimpleButton();
            this.txtDatasetName = new TextEdit();
            this.txtWorkspaceName = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.panel2 = new Panel();
            this.btnSelectOutDataset = new SimpleButton();
            this.txtOutDataset = new TextEdit();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.btnAdd = new SimpleButton();
            this.txtInputFeatureClass = new TextEdit();
            this.label3 = new Label();
            this.btnDelete = new SimpleButton();
            this.label5 = new Label();
            this.SourceDatalistBox = new ListBoxControl();
            this.btnSelectInputFeatures = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.lblProcess = new Label();
            this.radioGroup1.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.rdoDatasetType.Properties.BeginInit();
            this.txtDatasetName.Properties.BeginInit();
            this.txtWorkspaceName.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.txtOutDataset.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtInputFeatureClass.Properties.BeginInit();
            ((ISupportInitialize) this.SourceDatalistBox).BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(8, 8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "创建新数据集"), new RadioGroupItem(null, "使用已有数据集") });
            this.radioGroup1.Size = new Size(0xe0, 0x18);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.panel1.Controls.Add(this.rdoDatasetType);
            this.panel1.Controls.Add(this.btnSelectWorkspace);
            this.panel1.Controls.Add(this.txtDatasetName);
            this.panel1.Controls.Add(this.txtWorkspaceName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(0, 0x20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x158, 0x48);
            this.panel1.TabIndex = 1;
            this.rdoDatasetType.Location = new Point(0xd8, 40);
            this.rdoDatasetType.Name = "rdoDatasetType";
            this.rdoDatasetType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoDatasetType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDatasetType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoDatasetType.Properties.Columns = 2;
            this.rdoDatasetType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "要素类"), new RadioGroupItem(null, "表") });
            this.rdoDatasetType.Size = new Size(0x7c, 0x18);
            this.rdoDatasetType.TabIndex = 20;
            this.btnSelectWorkspace.Location = new Point(0x120, 8);
            this.btnSelectWorkspace.Name = "btnSelectWorkspace";
            this.btnSelectWorkspace.Size = new Size(0x20, 0x18);
            this.btnSelectWorkspace.TabIndex = 4;
            this.btnSelectWorkspace.Text = "...";
            this.btnSelectWorkspace.Click += new EventHandler(this.btnSelectWorkspace_Click);
            this.txtDatasetName.EditValue = "";
            this.txtDatasetName.Location = new Point(0x58, 40);
            this.txtDatasetName.Name = "txtDatasetName";
            this.txtDatasetName.Size = new Size(120, 0x15);
            this.txtDatasetName.TabIndex = 3;
            this.txtWorkspaceName.EditValue = "";
            this.txtWorkspaceName.Location = new Point(0x58, 8);
            this.txtWorkspaceName.Name = "txtWorkspaceName";
            this.txtWorkspaceName.Size = new Size(0xb8, 0x15);
            this.txtWorkspaceName.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据集名称";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择存储位置";
            this.panel2.Controls.Add(this.btnSelectOutDataset);
            this.panel2.Controls.Add(this.txtOutDataset);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new Point(0, 0x20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x158, 0x4a);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            this.btnSelectOutDataset.Location = new Point(0x130, 8);
            this.btnSelectOutDataset.Name = "btnSelectOutDataset";
            this.btnSelectOutDataset.Size = new Size(0x20, 0x18);
            this.btnSelectOutDataset.TabIndex = 4;
            this.btnSelectOutDataset.Text = "...";
            this.btnSelectOutDataset.Click += new EventHandler(this.btnSelectOutDataset_Click);
            this.txtOutDataset.EditValue = "";
            this.txtOutDataset.Location = new Point(0x68, 8);
            this.txtOutDataset.Name = "txtOutDataset";
            this.txtOutDataset.Size = new Size(0xb8, 0x15);
            this.txtOutDataset.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "选择已有数据集";
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtInputFeatureClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SourceDatalistBox);
            this.groupBox1.Controls.Add(this.btnSelectInputFeatures);
            this.groupBox1.Location = new Point(8, 0x70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x158, 280);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入数据";
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = (Image) manager.GetObject("btnAdd.Image");
            this.btnAdd.Location = new Point(0x130, 0x68);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x18, 0x18);
            this.btnAdd.TabIndex = 0x12;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.txtInputFeatureClass.EditValue = "";
            this.txtInputFeatureClass.Location = new Point(8, 0x30);
            this.txtInputFeatureClass.Name = "txtInputFeatureClass";
            this.txtInputFeatureClass.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtInputFeatureClass.Properties.Appearance.Options.UseBackColor = true;
            this.txtInputFeatureClass.Properties.ReadOnly = true;
            this.txtInputFeatureClass.Size = new Size(0x120, 0x15);
            this.txtInputFeatureClass.TabIndex = 0x11;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "输入数据";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x130, 0x88);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "源数据列表";
            this.SourceDatalistBox.ItemHeight = 0x11;
            this.SourceDatalistBox.Location = new Point(8, 0x68);
            this.SourceDatalistBox.Name = "SourceDatalistBox";
            this.SourceDatalistBox.SelectionMode = SelectionMode.MultiExtended;
            this.SourceDatalistBox.Size = new Size(0x120, 160);
            this.SourceDatalistBox.TabIndex = 13;
            this.SourceDatalistBox.SelectedIndexChanged += new EventHandler(this.SourceDatalistBox_SelectedIndexChanged);
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x130, 0x30);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 12;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x128, 0x198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x30, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(0xd8, 0x198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.lblProcess.Location = new Point(8, 400);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new Size(200, 0x18);
            this.lblProcess.TabIndex = 6;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(360, 0x1b5);
            base.Controls.Add(this.lblProcess);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.radioGroup1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmDatasetUnion";
            this.Text = "合并数据";
            this.radioGroup1.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.rdoDatasetType.Properties.EndInit();
            this.txtDatasetName.Properties.EndInit();
            this.txtWorkspaceName.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.txtOutDataset.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtInputFeatureClass.Properties.EndInit();
            ((ISupportInitialize) this.SourceDatalistBox).EndInit();
            base.ResumeLayout(false);
        }

        private bool method_0(IWorkspace iworkspace_1, esriDatasetType esriDatasetType_0, string string_0)
        {
            bool flag = false;
            if (iworkspace_1 == null)
            {
                return false;
            }
            if ((iworkspace_1.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) || (iworkspace_1.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                return ((IWorkspace2) iworkspace_1).get_NameExists(esriDatasetType_0, string_0);
            }
            if (iworkspace_1.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
                return flag;
            }
            if (esriDatasetType_0 == esriDatasetType.esriDTFeatureClass)
            {
                return File.Exists(iworkspace_1.PathName + @"\" + string_0 + ".shp");
            }
            return File.Exists(iworkspace_1.PathName + @"\" + string_0 + ".dbf");
        }

        private bool method_1(IDataset idataset_0)
        {
            for (int i = 0; i < this.SourceDatalistBox.Items.Count; i++)
            {
                IGxDataset dataset = this.SourceDatalistBox.Items[i] as IGxDataset;
                if (dataset.Type != idataset_0.Type)
                {
                    return false;
                }
                if ((dataset.Type == esriDatasetType.esriDTFeatureClass) && ((dataset.DatasetName as IFeatureClassName).ShapeType != (idataset_0 as IFeatureClass).ShapeType))
                {
                    return false;
                }
            }
            return true;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    break;

                case 1:
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    break;
            }
        }

        private void SourceDatalistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SourceDatalistBox.SelectedIndices.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }
    }
}

