using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmDatasetUnion : Form
    {
        private Container container_0 = null;
        private IWorkspace iworkspace_0 = null;

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

