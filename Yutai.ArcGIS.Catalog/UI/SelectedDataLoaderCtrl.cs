using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class SelectedDataLoaderCtrl : UserControl
    {
        private IContainer icontainer_0 = null;

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
            frmOpenFile file = new frmOpenFile
            {
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
                    if ((this.Table.Type == esriDatasetType.esriDTFeatureClass) &&
                        (dataset.Type == esriDatasetType.esriDTFeatureClass))
                    {
                        if (this.FeatureClass.ShapeType == (dataset.Dataset as IFeatureClass).ShapeType)
                        {
                            if (this.method_1(this.Table.Workspace, dataset.DatasetName.WorkspaceName) &&
                                ((this.FeatureClass as IDataset).Name == dataset.DatasetName.Name))
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
                    else if ((this.Table.Type == esriDatasetType.esriDTTable) &&
                             (dataset.Type == esriDatasetType.esriDTTable))
                    {
                        if (this.method_1(this.Table.Workspace, dataset.DatasetName.WorkspaceName) &&
                            (this.Table.Name == dataset.DatasetName.Name))
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
            get { return (this.Table as IFeatureClass); }
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
            get { return this.SourceDatalistBox.Items; }
        }

        public IDataset Table { get; set; }
    }
}