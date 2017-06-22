using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmSimpleDataLoader : Form
    {
        private Container container_0 = null;
        private IGxDataset igxDataset_0 = null;
        private IGxDataset igxDataset_1 = null;

        public frmSimpleDataLoader()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.igxDataset_1 == null)
            {
                MessageBox.Show("请选择要装载的数据!");
            }
            else
            {
                ITable dataset = this.igxDataset_1.Dataset as ITable;
                ITable table2 = this.igxDataset_0.Dataset as ITable;
                this.progressBar1.Visible = true;
                Dataloaders dataloaders = new Dataloaders();
                dataloaders.Step+=(new IFeatureProgress_StepEventHandler(this.method_3));
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                dataloaders.LoadData(dataset, null, table2, 500);
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                this.btnOK.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterTablesAndFeatureClasses(), true);
            file.AllowMultiSelect = false;
            if ((file.DoModalOpen() == DialogResult.OK) && (file.Items.Count != 0))
            {
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    if ((this.igxDataset_0.Type == esriDatasetType.esriDTFeatureClass) && (dataset.Type == esriDatasetType.esriDTFeatureClass))
                    {
                        if ((this.igxDataset_0.Dataset as IFeatureClass).ShapeType == (dataset.Dataset as IFeatureClass).ShapeType)
                        {
                            if (this.method_0(this.igxDataset_0.DatasetName.WorkspaceName, dataset.DatasetName.WorkspaceName) && (this.igxDataset_0.DatasetName.Name == dataset.DatasetName.Name))
                            {
                                MessageBox.Show("源对象类和目标对象类必须不同!");
                            }
                            else
                            {
                                this.igxDataset_1 = dataset;
                                this.textEdit1.Text = (this.igxDataset_1 as IGxObject).Name;
                            }
                        }
                        else
                        {
                            MessageBox.Show("装载数据必须和源数据有相同的几何数据类型!");
                        }
                    }
                    else
                    {
                        this.igxDataset_1 = dataset;
                        this.textEdit1.Text = (this.igxDataset_1 as IGxObject).Name;
                    }
                }
                else
                {
                    this.textEdit1.Text = "";
                }
            }
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

