namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using Yutai.Catalog;
   
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class frmDatasetUnion : Form
    {
        private Button btnAdd;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnOK;
        private Button btnSelectInputFeatures;
        private Button btnSelectOutDataset;
        private Button btnSelectWorkspace;
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
        private ComboBox radioGroup1;
        private ComboBox rdoDatasetType;
        private ListBox SourceDatalistBox;
        private Button txtDatasetName;
        private Button txtInputFeatureClass;
        private Button txtOutDataset;
        private Label label7;
        private Label label6;
        private Button txtWorkspaceName;

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
                            checker = new FieldChecker {
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
                            checker = new FieldChecker {
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
            //Dataloaders dataloaders = new Dataloaders();
            //for (int i = 0; i < items.Count; i++)
            //{
            //    this.lblProcess.Text = "开始导入 " + (items[i] as IGxObject).FullName + " ...";
            //    ITable table2 = (items[i] as IGxObject).InternalObjectName.Open() as ITable;
            //    dataloaders.LoadData(table2, null, table, 800);
            //    Marshal.ReleaseComObject(table2);
            //    table2 = null;
            //}
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
                        IWorkspaceName name = new WorkspaceName() as IWorkspaceName;

                        name.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                        name.PathName = (obj2.InternalObjectName as IFileName).Path;
                    
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
            this.radioGroup1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoDatasetType = new System.Windows.Forms.ComboBox();
            this.btnSelectWorkspace = new System.Windows.Forms.Button();
            this.txtDatasetName = new System.Windows.Forms.Button();
            this.txtWorkspaceName = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSelectOutDataset = new System.Windows.Forms.Button();
            this.txtOutDataset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtInputFeatureClass = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SourceDatalistBox = new System.Windows.Forms.ListBox();
            this.btnSelectInputFeatures = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblProcess = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioGroup1
            // 
            this.radioGroup1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.radioGroup1.Items.AddRange(new object[] {
            "创建新数据集",
            "使用已有数据集"});
            this.radioGroup1.Location = new System.Drawing.Point(112, 6);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Size = new System.Drawing.Size(224, 20);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoDatasetType);
            this.panel1.Controls.Add(this.btnSelectWorkspace);
            this.panel1.Controls.Add(this.txtDatasetName);
            this.panel1.Controls.Add(this.txtWorkspaceName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 72);
            this.panel1.TabIndex = 1;
            // 
            // rdoDatasetType
            // 
            this.rdoDatasetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rdoDatasetType.Items.AddRange(new object[] {
            "要素类",
            "表"});
            this.rdoDatasetType.Location = new System.Drawing.Point(216, 40);
            this.rdoDatasetType.Name = "rdoDatasetType";
            this.rdoDatasetType.Size = new System.Drawing.Size(124, 20);
            this.rdoDatasetType.TabIndex = 20;
            // 
            // btnSelectWorkspace
            // 
            this.btnSelectWorkspace.Location = new System.Drawing.Point(288, 8);
            this.btnSelectWorkspace.Name = "btnSelectWorkspace";
            this.btnSelectWorkspace.Size = new System.Drawing.Size(32, 24);
            this.btnSelectWorkspace.TabIndex = 4;
            this.btnSelectWorkspace.Text = "...";
            this.btnSelectWorkspace.Click += new System.EventHandler(this.btnSelectWorkspace_Click);
            // 
            // txtDatasetName
            // 
            this.txtDatasetName.Location = new System.Drawing.Point(88, 40);
            this.txtDatasetName.Name = "txtDatasetName";
            this.txtDatasetName.Size = new System.Drawing.Size(120, 21);
            this.txtDatasetName.TabIndex = 3;
            // 
            // txtWorkspaceName
            // 
            this.txtWorkspaceName.Location = new System.Drawing.Point(88, 8);
            this.txtWorkspaceName.Name = "txtWorkspaceName";
            this.txtWorkspaceName.Size = new System.Drawing.Size(184, 21);
            this.txtWorkspaceName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据集名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择存储位置";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnSelectOutDataset);
            this.panel2.Controls.Add(this.txtOutDataset);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 74);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // btnSelectOutDataset
            // 
            this.btnSelectOutDataset.Location = new System.Drawing.Point(304, 8);
            this.btnSelectOutDataset.Name = "btnSelectOutDataset";
            this.btnSelectOutDataset.Size = new System.Drawing.Size(32, 24);
            this.btnSelectOutDataset.TabIndex = 4;
            this.btnSelectOutDataset.Text = "...";
            this.btnSelectOutDataset.Click += new System.EventHandler(this.btnSelectOutDataset_Click);
            // 
            // txtOutDataset
            // 
            this.txtOutDataset.Location = new System.Drawing.Point(104, 8);
            this.txtOutDataset.Name = "txtOutDataset";
            this.txtOutDataset.Size = new System.Drawing.Size(184, 21);
            this.txtOutDataset.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "选择已有数据集";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtInputFeatureClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SourceDatalistBox);
            this.groupBox1.Controls.Add(this.btnSelectInputFeatures);
            this.groupBox1.Location = new System.Drawing.Point(8, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 280);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入数据";
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(304, 104);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtInputFeatureClass
            // 
            this.txtInputFeatureClass.Location = new System.Drawing.Point(8, 48);
            this.txtInputFeatureClass.Name = "txtInputFeatureClass";
            this.txtInputFeatureClass.Size = new System.Drawing.Size(288, 21);
            this.txtInputFeatureClass.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "输入数据";
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(304, 136);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 24);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "源数据列表";
            // 
            // SourceDatalistBox
            // 
            this.SourceDatalistBox.ItemHeight = 12;
            this.SourceDatalistBox.Location = new System.Drawing.Point(8, 104);
            this.SourceDatalistBox.Name = "SourceDatalistBox";
            this.SourceDatalistBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.SourceDatalistBox.Size = new System.Drawing.Size(288, 160);
            this.SourceDatalistBox.TabIndex = 13;
            this.SourceDatalistBox.SelectedIndexChanged += new System.EventHandler(this.SourceDatalistBox_SelectedIndexChanged);
            // 
            // btnSelectInputFeatures
            // 
            this.btnSelectInputFeatures.Location = new System.Drawing.Point(304, 48);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new System.Drawing.Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 12;
            this.btnSelectInputFeatures.Click += new System.EventHandler(this.btnSelectInputFeatures_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(48, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 408);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(48, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.Location = new System.Drawing.Point(8, 400);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(200, 24);
            this.lblProcess.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "数据集方式";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "数据集方式";
            // 
            // frmDatasetUnion
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(360, 437);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.panel2);
            this.Name = "frmDatasetUnion";
            this.Text = "合并数据";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

