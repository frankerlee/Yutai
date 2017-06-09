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
    public class frmMultiDataLoader : Form
    {
        private SimpleButton btnDelete;
        private SimpleButton btnOK;
        private SimpleButton btnOpen;
        private SimpleButton btnSelectInputFeatures;
        private Container container_0 = null;
        private IGxDataset igxDataset_0 = null;
        private IList ilist_0 = new ArrayList();
        private Label label1;
        private Label lblFN;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBarFN;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiDataLoader));
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.btnOpen = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.listView1 = new ListView();
            this.btnSelectInputFeatures = new SimpleButton();
            this.lblSelectObjects = new Label();
            this.panel1 = new Panel();
            this.lblFN = new Label();
            this.progressBarFN = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textEdit1.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择目标数据";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(8, 0x20);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(0xe0, 0x15);
            this.textEdit1.TabIndex = 1;
            this.btnOpen.Image = (Image) resources.GetObject("btnOpen.Image");
            this.btnOpen.Location = new Point(0x100, 0x20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(0x18, 0x18);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnOK.Location = new Point(0xa8, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0xe8, 280);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Text = "取消";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x10a, 0x87);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 0x12;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(2, 0x67);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x100, 0x98);
            this.listView1.TabIndex = 0x11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x10a, 0x5f);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 0x10;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(2, 0x4f);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x59, 12);
            this.lblSelectObjects.TabIndex = 15;
            this.lblSelectObjects.Text = "输入要合并数据";
            this.panel1.Controls.Add(this.lblFN);
            this.panel1.Controls.Add(this.progressBarFN);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x128, 0x108);
            this.panel1.TabIndex = 0x13;
            this.panel1.Visible = false;
            this.lblFN.AutoSize = true;
            this.lblFN.Location = new Point(0x10, 8);
            this.lblFN.Name = "lblFN";
            this.lblFN.Size = new Size(0, 12);
            this.lblFN.TabIndex = 0x11;
            this.progressBarFN.Location = new Point(0x10, 40);
            this.progressBarFN.Name = "progressBarFN";
            this.progressBarFN.Size = new Size(0xb0, 0x10);
            this.progressBarFN.TabIndex = 0x10;
            this.progressBar1.Location = new Point(0x10, 0x58);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0xb0, 0x10);
            this.progressBar1.TabIndex = 15;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 0x137);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMultiDataLoader";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "数据装载";
            this.textEdit1.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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

