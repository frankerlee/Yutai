using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmMultiDataLoader : Form
    {
        private Container container_0 = null;
        private IGxDataset igxDataset_0 = null;
        private IList ilist_0 = new ArrayList();

        public frmMultiDataLoader()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                if (this.listView1.Items[i].Selected)
                {
                    this.ilist_0.RemoveAt(i);
                    this.listView1.Items.RemoveAt(i);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.igxDataset_0 == null)
            {
                MessageBox.Show("请选择要装载的数据!");
            }
            else
            {
                this.panel1.Visible = true;
                ITable dataset = this.igxDataset_0.Dataset as ITable;
                this.progressBarFN.Minimum = 0;
                this.progressBarFN.Maximum = this.ilist_0.Count;
                Dataloaders dataloaders = new Dataloaders();
                dataloaders.Step+=(new IFeatureProgress_StepEventHandler(this.method_3));
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    ITable table2 = (this.ilist_0[i] as IName).Open() as ITable;
                    this.lblFN.Text = "处理" + (table2 as IDataset).Name + "...";
                    this.progressBarFN.Increment(1);
                    dataloaders.LoadData(dataset, null, table2, 500);
                    Marshal.ReleaseComObject(table2);
                    table2 = null;
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                this.btnOK.DialogResult = DialogResult.OK;
                Marshal.ReleaseComObject(dataset);
                dataset = null;
                base.Close();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterTablesAndFeatureClasses(), true);
            file.AllowMultiSelect = false;
            this.btnSelectInputFeatures.Enabled = false;
            if (file.DoModalOpen() == DialogResult.OK)
            {
                this.igxDataset_0 = null;
                if (file.Items.Count != 0)
                {
                    IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                    if (dataset != null)
                    {
                        this.igxDataset_0 = dataset;
                        this.textEdit1.Text = (this.igxDataset_0 as IGxObject).Name;
                        this.btnSelectInputFeatures.Enabled = true;
                    }
                    else
                    {
                        this.igxDataset_0 = null;
                        this.textEdit1.Text = "";
                    }
                }
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "添加数据"
            };
            file.RemoveAllFilters();
            file.AllowMultiSelect = true;
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        IGxObject obj2 = items.get_Element(i) as IGxObject;
                        if (obj2 is IGxDataset)
                        {
                            IGxDataset dataset = obj2 as IGxDataset;
                            if ((this.igxDataset_0.Type == esriDatasetType.esriDTFeatureClass) && (dataset.Type == esriDatasetType.esriDTFeatureClass))
                            {
                                if ((this.igxDataset_0.Dataset as IFeatureClass).ShapeType == (dataset.Dataset as IFeatureClass).ShapeType)
                                {
                                    if (this.method_0(this.igxDataset_0.DatasetName.WorkspaceName, dataset.DatasetName.WorkspaceName) && (this.igxDataset_0.DatasetName.Name == dataset.DatasetName.Name))
                                    {
                                        MessageBox.Show("源对象类和目标对象类必须不同!" + obj2.FullName + "不能导入");
                                    }
                                    else
                                    {
                                        this.ilist_0.Add(obj2.InternalObjectName);
                                        this.listView1.Items.Add(obj2.FullName);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("装载数据必须和源数据有相同的几何数据类型!" + obj2.FullName + "不能导入");
                                }
                            }
                            else if ((this.igxDataset_0.Type == esriDatasetType.esriDTTable) && (dataset.Type == esriDatasetType.esriDTTable))
                            {
                                this.ilist_0.Add(obj2.InternalObjectName);
                                this.listView1.Items.Add(obj2.FullName);
                            }
                            else if ((this.igxDataset_0.Type == esriDatasetType.esriDTFeatureClass) && (dataset.Type == esriDatasetType.esriDTTable))
                            {
                                this.ilist_0.Add(obj2.InternalObjectName);
                                this.listView1.Items.Add(obj2.FullName);
                            }
                            else
                            {
                                MessageBox.Show("装载数据必须和源数据有相同的几何数据类型!" + obj2.FullName + "不能导入");
                            }
                        }
                    }
                }
            }
        }

 private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
        }

        private bool method_0(IWorkspaceName iworkspaceName_0, IWorkspaceName iworkspaceName_1)
        {
            return iworkspaceName_0.ConnectionProperties.IsEqual(iworkspaceName_1.ConnectionProperties);
        }

        private void method_1(int int_0)
        {
            this.progressBar1.Minimum = int_0;
            this.progressBar1.Value = int_0;
        }

        private void method_2(int int_0)
        {
            this.progressBar1.Maximum = int_0;
        }

        private void method_3()
        {
            this.progressBar1.Increment(1);
        }

        public IGxDataset OutLocation
        {
            set
            {
                this.igxDataset_0 = value;
            }
        }
    }
}

