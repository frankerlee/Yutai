namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using Yutai.Catalog;
    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SelectedDataLoaderCtrl : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private Button btnSelectInputFeatures;
        private IContainer icontainer_0 = null;
        [CompilerGenerated]
        private IDataset idataset_0;
        private Label label1;
        private Label label2;
        private ListBox SourceDatalistBox;
        private Button txtInputFeatureClass;

        public SelectedDataLoaderCtrl()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.SourceDatalistBox.Items.IndexOf(this.txtInputFeatureClass.Tag) != -1)
            {
                MessageBox.Show("数据素已存在!");
            }
            else if (this.method_2())
            {
                this.SourceDatalistBox.Items.Add(this.txtInputFeatureClass.Tag);
                this.txtInputFeatureClass.Tag = null;
                this.txtInputFeatureClass.Text = "";
                this.btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("新添加数据集表结构不一致，无法添加!");
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "选择要素"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterTablesAndFeatureClasses(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                if (file.Items.Count == 0)
                {
                    return;
                }
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    if ((this.Table.Type == esriDatasetType.esriDTFeatureClass) && (dataset.Type == esriDatasetType.esriDTFeatureClass))
                    {
                        if (this.FeatureClass.ShapeType == (dataset.Dataset as IFeatureClass).ShapeType)
                        {
                            if (this.method_1(this.Table.Workspace, dataset.DatasetName.WorkspaceName) && ((this.FeatureClass as IDataset).Name == dataset.DatasetName.Name))
                            {
                                MessageBox.Show("源对象类和目标对象类必须不同!");
                                return;
                            }
                            this.txtInputFeatureClass.Text = (dataset as IGxObject).FullName;
                            this.txtInputFeatureClass.Tag = dataset;
                        }
                        else
                        {
                            MessageBox.Show("装载数据必须和源数据有相同的几何数据类型!");
                        }
                    }
                    else if ((this.Table.Type == esriDatasetType.esriDTTable) && (dataset.Type == esriDatasetType.esriDTTable))
                    {
                        if (this.method_1(this.Table.Workspace, dataset.DatasetName.WorkspaceName) && (this.Table.Name == dataset.DatasetName.Name))
                        {
                            MessageBox.Show("源对象类和目标对象类必须不同!");
                            return;
                        }
                        this.txtInputFeatureClass.Text = (dataset as IGxObject).FullName;
                        this.txtInputFeatureClass.Tag = dataset;
                    }
                }
                else
                {
                    this.txtInputFeatureClass.Text = "";
                    this.txtInputFeatureClass.Tag = null;
                }
            }
            if (this.txtInputFeatureClass.Text.Length > 0)
            {
                this.btnAdd.Enabled = true;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SelectedDataLoaderCtrl));
            this.btnAdd = new Button();
            this.txtInputFeatureClass = new Button();
            this.label2 = new Label();
            this.btnDelete = new Button();
            this.label1 = new Label();
            this.SourceDatalistBox = new ListBox();
            this.btnSelectInputFeatures = new Button();
           
            ((ISupportInitialize) this.SourceDatalistBox).BeginInit();
            base.SuspendLayout();
            this.btnAdd.Enabled = false;
           
            this.btnAdd.Location = new Point(0x10d, 0x58);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x18, 0x18);
            this.btnAdd.TabIndex = 0x12;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.txtInputFeatureClass.Text="";
            this.txtInputFeatureClass.Location = new Point(13, 0x20);
            this.txtInputFeatureClass.Name = "txtInputFeatureClass";
            this.txtInputFeatureClass.Enabled = false;
            this.txtInputFeatureClass.Size = new Size(0xf8, 0x15);
            this.txtInputFeatureClass.TabIndex = 0x11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(13, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "输入数据";
            this.btnDelete.Enabled = false;
           // this.btnDelete.Image = (Image) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x10d, 120);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 15;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 0x40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "源数据列表";
            this.SourceDatalistBox.ItemHeight = 15;
            this.SourceDatalistBox.Location = new Point(13, 0x58);
            this.SourceDatalistBox.Name = "SourceDatalistBox";
            this.SourceDatalistBox.SelectionMode = SelectionMode.MultiExtended;
            this.SourceDatalistBox.Size = new Size(0xf8, 0x88);
            this.SourceDatalistBox.TabIndex = 13;
            //this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x10d, 0x20);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 12;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.txtInputFeatureClass);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.SourceDatalistBox);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Name = "SelectedDataLoaderCtrl";
            base.Size = new Size(0x13b, 0xf9);
           
            ((ISupportInitialize) this.SourceDatalistBox).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool method_0(IWorkspaceName iworkspaceName_0, IWorkspaceName iworkspaceName_1)
        {
            return iworkspaceName_0.ConnectionProperties.IsEqual(iworkspaceName_1.ConnectionProperties);
        }

        private bool method_1(IWorkspace iworkspace_0, IWorkspaceName iworkspaceName_0)
        {
            return iworkspace_0.ConnectionProperties.IsEqual(iworkspaceName_0.ConnectionProperties);
        }

        private bool method_2()
        {
            if (this.SourceDatalistBox.Items.Count != 0)
            {
                IGxDataset dataset = this.SourceDatalistBox.Items[0] as IGxDataset;
                IFields fields = (dataset.Dataset as ITable).Fields;
                IGxDataset tag = this.txtInputFeatureClass.Tag as IGxDataset;
                IFields fields2 = (tag.Dataset as ITable).Fields;
                if (fields2.FieldCount != fields.FieldCount)
                {
                    return false;
                }
                bool flag2 = false;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    flag2 = false;
                    IField field = fields.get_Field(i);
                    if (field.Editable)
                    {
                        for (int j = 0; j < fields2.FieldCount; j++)
                        {
                            IField field2 = fields2.get_Field(i);
                            if ((string.Compare(field.Name, field2.Name, true) == 0) && (field.Type == field2.Type))
                            {
                                flag2 = true;
                            }
                        }
                        if (!flag2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public IFeatureClass FeatureClass
        {
            get
            {
                return (this.Table as IFeatureClass);
            }
        }

        public List<ITable> LoadTables
        {
            get
            {
                List<ITable> list = new List<ITable>();
                foreach (object obj2 in this.SourceDatalistBox.Items)
                {
                    if (obj2 is IGxDataset)
                    {
                        list.Add((obj2 as IGxDataset).Dataset as ITable);
                    }
                }
                return list;
            }
        }

        public IList SelectDataset
        {
            get
            {
                return this.SourceDatalistBox.Items;
            }
        }

        public IDataset Table
        {
            [CompilerGenerated]
            get
            {
                return this.idataset_0;
            }
            [CompilerGenerated]
            set
            {
                this.idataset_0 = value;
            }
        }
    }
}

