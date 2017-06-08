namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Utility.Geodatabase;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmFeatureClass2VCT : Form
    {
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private IList ilist_0 = new ArrayList();
        private IName iname_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private ISpatialReference ispatialReference_0 = null;
        private Label label2;
        private Label labelFileName;
        private Label labelFN;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private string string_0 = "";
        private TextEdit txtOutLocation;

        public frmFeatureClass2VCT()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                if (this.listView1.Items[i].Selected)
                {
                    this.listView1.Items.RemoveAt(i);
                    this.ilist_0.RemoveAt(i);
                }
            }
            if (this.ilist_0.Count == 0)
            {
                this.ispatialReference_0 = null;
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
                            IDataset dataset = (obj2 as IGxDataset).Dataset;
                            if (this.ispatialReference_0 == null)
                            {
                                this.ispatialReference_0 = (dataset as IGeoDataset).SpatialReference;
                            }
                            else
                            {
                                ISpatialReference spatialReference = (dataset as IGeoDataset).SpatialReference;
                                if (!(!(spatialReference is IUnknownCoordinateSystem) || (this.ispatialReference_0 is IUnknownCoordinateSystem)))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                                if (!(!(spatialReference is IProjectedCoordinateSystem) || (this.ispatialReference_0 is IProjectedCoordinateSystem)))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                                if (!(!(spatialReference is IGeographicCoordinateSystem) || (this.ispatialReference_0 is IGeographicCoordinateSystem)))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                                if (((spatialReference is IProjectedCoordinateSystem) && (this.ispatialReference_0 is IProjectedCoordinateSystem)) && !(spatialReference as IClone).IsEqual(this.ispatialReference_0 as IClone))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                            }
                            this.ilist_0.Add(dataset);
                            this.listView1.Items.Add(obj2.FullName);
                        }
                    }
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "VCT文件(*.vct)|*.vct"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOutLocation.Text = dialog.FileName;
            }
        }

        public bool CanDo()
        {
            if (this.listView1.Items.Count == 0)
            {
                MessageBox.Show("请输入需要转换的数据");
                return false;
            }
            if (this.txtOutLocation.Text.Length == 0)
            {
                MessageBox.Show("请选择输出位置");
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmFeatureClass2VCT));
            this.btnDelete = new SimpleButton();
            this.listView1 = new ListView();
            this.label2 = new Label();
            this.txtOutLocation = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.lblSelectObjects = new Label();
            this.btnSelectOutLocation = new SimpleButton();
            this.panel1 = new Panel();
            this.labelFN = new Label();
            this.labelFileName = new Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtOutLocation.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new System.Drawing.Point(0x110, 0x40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(8, 0x20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x100, 0x98);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0xc0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x47, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "输出VCT文件";
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new System.Drawing.Point(8, 0xd8);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(0xf8, 0x15);
            this.txtOutLocation.TabIndex = 10;
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new System.Drawing.Point(0x110, 0x18);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 9;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new System.Drawing.Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x35, 12);
            this.lblSelectObjects.TabIndex = 8;
            this.lblSelectObjects.Text = "选择要素";
            this.btnSelectOutLocation.Image = (Image) manager.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new System.Drawing.Point(0x110, 0xd8);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 14;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.panel1.Controls.Add(this.labelFN);
            this.panel1.Controls.Add(this.labelFileName);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x160, 0xf2);
            this.panel1.TabIndex = 0x10;
            this.panel1.Visible = false;
            this.labelFN.AutoSize = true;
            this.labelFN.Location = new System.Drawing.Point(0x18, 0x40);
            this.labelFN.Name = "labelFN";
            this.labelFN.Size = new Size(0, 12);
            this.labelFN.TabIndex = 0x13;
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(0x18, 0x18);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new Size(0, 12);
            this.labelFileName.TabIndex = 0x12;
            this.progressBar2.Location = new System.Drawing.Point(0x18, 0x58);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(0x100, 0x18);
            this.progressBar2.TabIndex = 0x10;
            this.simpleButton1.Location = new System.Drawing.Point(0xa8, 0x100);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x48, 0x18);
            this.simpleButton1.TabIndex = 0x11;
            this.simpleButton1.Text = "转换";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(0xf8, 0x100);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(80, 0x18);
            this.simpleButton2.TabIndex = 0x12;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x170, 0x125);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblSelectObjects);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFeatureClass2VCT";
            this.Text = "导出VCT数据";
            this.txtOutLocation.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void method_0(string string_1)
        {
            this.string_0 = string_1;
            this.labelFN.Text = "处理:" + string_1;
        }

        private void method_1(int int_2)
        {
            this.int_0 = int_2;
            this.progressBar2.Minimum = int_2;
        }

        private void method_2(int int_2)
        {
            this.int_1 = int_2;
            this.progressBar2.Maximum = int_2;
        }

        private void method_3(int int_2)
        {
            this.progressBar2.Value = int_2;
        }

        private void method_4(int int_2)
        {
            this.progressBar2.Step = int_2;
        }

        private void method_5()
        {
            this.int_0++;
            this.progressBar2.Increment(1);
            this.labelFN.Text = "处理图层<" + this.string_0 + "> ,转换第" + this.int_0.ToString() + " 个对象, 共 " + this.int_1.ToString() + " 个对象";
            Application.DoEvents();
        }

        private void method_6(object object_0, string string_1)
        {
            this.labelFileName.Text = string_1;
            Application.DoEvents();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.CanDo())
            {
                this.panel1.Visible = true;
                this.simpleButton1.Enabled = false;
                this.simpleButton2.Enabled = false;
                VCTWrite write = new VCTWrite();
                write.ProgressMessage += new ProgressMessageHandle(this.method_6);
                write.add_Step(new IFeatureProgress_StepEventHandler(this.method_5));
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    write.AddDataset(this.ilist_0[i] as IDataset);
                }
                write.Write(this.txtOutLocation.Text);
                this.panel1.Visible = false;
                this.simpleButton1.Enabled = true;
                this.simpleButton2.Enabled = true;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }
    }
}

