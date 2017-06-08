namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Utility.Geodatabase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSimpleDataLoader : Form
    {
        private SimpleButton btnOK;
        private SimpleButton btnOpen;
        private Container container_0 = null;
        private IGxDataset igxDataset_0 = null;
        private IGxDataset igxDataset_1 = null;
        private Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;

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
                dataloaders.add_Step(new IFeatureProgress_StepEventHandler(this.method_3));
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmSimpleDataLoader));
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.btnOpen = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择数据";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(0x10, 0x20);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(0xe0, 0x15);
            this.textEdit1.TabIndex = 1;
            this.btnOpen.Image = (Image) manager.GetObject("btnOpen.Image");
            this.btnOpen.Location = new Point(0x100, 0x20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(0x18, 0x18);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnOK.Location = new Point(160, 0x48);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0xe0, 0x48);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Text = "取消";
            this.progressBar1.Location = new Point(0x10, 0x48);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x88, 0x10);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 0x7a);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSimpleDataLoader";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "数据装载";
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

