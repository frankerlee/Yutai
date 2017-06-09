using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmProject : Form, IProjectForm
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectIn;
        private SimpleButton btnSelectOut;
        private SimpleButton btnSR;
        private Container container_0 = null;
        private FolderBrowserDialog folderBrowserDialog_0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IDataset idataset_0;
        private IFeatureProgress_Event ifeatureProgress_Event_0;
        private IGxDataset igxDataset_0 = null;
        private IName iname_0 = null;
        private int int_0;
        private int int_1;
        private int int_2 = 0;
        private int int_3 = 0;
        private ISpatialReference ispatialReference_0 = new UnknownCoordinateSystemClass();
        private IWorkspace iworkspace_0;
        private OpenFileDialog openFileDialog_0;
        private PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private string string_0;
        private TextEdit txtInputFeat;
        private TextEdit txtOutFeat;
        private TextEdit txtOutSR;

        public frmProject()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((this.string_0 != null) && (this.string_0.Length != 0))
            {
                if (this.bool_0)
                {
                    MessageBox.Show("输出要素类已存在，请重新指定输出要素类名！");
                }
                else if (this.ispatialReference_0 == null)
                {
                    MessageBox.Show("请输入投影坐标系！");
                }
                else if (this.ispatialReference_0 is IGeographicCoordinateSystem)
                {
                    MessageBox.Show("请输入投影坐标系！");
                }
                else if (this.ispatialReference_0 is IUnknownCoordinateSystem)
                {
                    MessageBox.Show("请输入投影坐标系！");
                }
                else
                {
                    double num = 1000.0;
                    this.string_0 = System.IO.Path.GetFileName(this.txtOutFeat.Text);
                    ISpatialReference spatialReference = ((IGeoDataset) this.idataset_0).SpatialReference;
                    (this.ispatialReference_0 as IControlPrecision2).IsHighPrecision = (spatialReference as IControlPrecision2).IsHighPrecision;
                    if (spatialReference.HasXYPrecision())
                    {
                        double num2;
                        double num3;
                        double num4;
                        double num5;
                        spatialReference.GetDomain(out num2, out num3, out num4, out num5);
                        new PointClass();
                        if (!(spatialReference is IUnknownCoordinateSystem))
                        {
                            IEnvelope extent;
                            if (spatialReference is IProjectedCoordinateSystem)
                            {
                                extent = ((IGeoDataset) this.idataset_0).Extent;
                                extent.PutCoords(num2, num4, num3, num5);
                                extent.Project(this.ispatialReference_0);
                                if (!extent.IsEmpty)
                                {
                                    this.ispatialReference_0.SetDomain(extent.XMin, extent.XMax, extent.YMin, extent.YMax);
                                    num = extent.Width / 2.0;
                                }
                            }
                            else if (spatialReference is IGeographicCoordinateSystem)
                            {
                                extent = ((IGeoDataset) this.idataset_0).Extent;
                                extent.Project(this.ispatialReference_0);
                                if (!extent.IsEmpty)
                                {
                                    this.ispatialReference_0.SetDomain(extent.XMin, extent.XMax, extent.YMin, extent.YMax);
                                    num = extent.Width / 2.0;
                                }
                            }
                        }
                    }
                    if (spatialReference.HasZPrecision())
                    {
                        double num6;
                        double num7;
                        spatialReference.GetZDomain(out num6, out num7);
                        this.ispatialReference_0.SetZDomain(num6, num7);
                    }
                    if (spatialReference.HasMPrecision())
                    {
                        double num8;
                        double num9;
                        spatialReference.GetZDomain(out num8, out num9);
                        this.ispatialReference_0.SetZDomain(num8, num9);
                    }
                    SpatialReferenctOperator.ChangeCoordinateSystem(this.iworkspace_0 as IGeodatabaseRelease, this.ispatialReference_0, false);
                    this.progressBar1.Visible = true;
                    this.int_2 = this.int_3;
                    this.progressBar1.Value = this.int_3;
                    SRLibCommonFunc.m_pfrm = this;
                    if (this.idataset_0 is IFeatureClass)
                    {
                        SRLibCommonFunc.Project((IFeatureClass) this.idataset_0, this.ispatialReference_0, this.iworkspace_0, this.string_0, num);
                    }
                    else if (this.idataset_0 is IFeatureDataset)
                    {
                        SRLibCommonFunc.Project((IFeatureDataset) this.idataset_0, this.ispatialReference_0, this.iworkspace_0, this.string_0);
                    }
                    this.progressBar1.Visible = false;
                    this.string_0 = "";
                    this.idataset_0 = null;
                    this.iworkspace_0 = null;
                    this.txtInputFeat.Text = "";
                    this.txtOutFeat.Text = "";
                }
            }
        }

        private void btnSelectIn_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                AllowMultiSelect = false,
                Text = "选择要素"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.igxDataset_0 = items.get_Element(0) as IGxDataset;
                    this.idataset_0 = this.igxDataset_0.Dataset;
                    this.txtInputFeat.Text = (this.igxDataset_0 as IGxObject).FullName;
                    this.iworkspace_0 = this.idataset_0.Workspace;
                    string tableName = this.idataset_0.Name + "_Project1";
                    IFieldChecker checker = new FieldCheckerClass {
                        InputWorkspace = this.iworkspace_0
                    };
                    checker.ValidateTableName(tableName, out this.string_0);
                    this.txtOutFeat.Text = this.iworkspace_0.PathName + @"\" + this.string_0;
                    if ((this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) || (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
                    {
                        if (((IWorkspace2) this.iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, this.string_0))
                        {
                            this.bool_0 = true;
                        }
                        else
                        {
                            this.bool_0 = false;
                        }
                    }
                    else if (this.iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        if (File.Exists(this.txtOutFeat.Text + ".shp"))
                        {
                            this.bool_0 = true;
                        }
                        else
                        {
                            this.bool_0 = false;
                        }
                    }
                }
            }
        }

        private void btnSelectOut_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (file.DoModalSave() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    IGxObject obj2 = items.get_Element(0) as IGxObject;
                    if (obj2 is IGxDatabase)
                    {
                        this.iname_0 = obj2.InternalObjectName;
                    }
                    else if (obj2 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (obj2.InternalObjectName as IFileName).Path
                        };
                        this.iname_0 = name as IName;
                    }
                    else if (obj2 is IGxDataset)
                    {
                        return;
                    }
                    this.iworkspace_0 = this.iname_0.Open() as IWorkspace;
                    string saveName = file.SaveName;
                    IFieldChecker checker = new FieldCheckerClass {
                        InputWorkspace = this.iworkspace_0
                    };
                    checker.ValidateTableName(saveName, out this.string_0);
                    this.txtOutFeat.Text = this.iworkspace_0.PathName + @"\" + this.string_0;
                    if ((this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) || (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
                    {
                        if (((IWorkspace2) this.iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, this.string_0))
                        {
                            this.bool_0 = true;
                        }
                        else
                        {
                            this.bool_0 = false;
                        }
                    }
                    else if (this.iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        if (File.Exists(this.txtOutFeat.Text + ".shp"))
                        {
                            this.bool_0 = true;
                        }
                        else
                        {
                            this.bool_0 = false;
                        }
                    }
                }
            }
        }

        private void btnSR_Click(object sender, EventArgs e)
        {
            frmSpatialReference reference = new frmSpatialReference {
                SpatialRefrence = this.ispatialReference_0
            };
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = reference.SpatialRefrence;
                this.txtOutSR.Text = this.ispatialReference_0.Name;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            this.ispatialReference_0 = null;
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProject));
            this.groupBox1 = new GroupBox();
            this.btnSelectIn = new SimpleButton();
            this.txtInputFeat = new TextEdit();
            this.groupBox2 = new GroupBox();
            this.pictureBox1 = new PictureBox();
            this.btnSelectOut = new SimpleButton();
            this.txtOutFeat = new TextEdit();
            this.groupBox3 = new GroupBox();
            this.btnSR = new SimpleButton();
            this.txtOutSR = new TextEdit();
            this.openFileDialog_0 = new OpenFileDialog();
            this.folderBrowserDialog_0 = new FolderBrowserDialog();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.txtInputFeat.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.txtOutFeat.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtOutSR.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSelectIn);
            this.groupBox1.Controls.Add(this.txtInputFeat);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x178, 0x38);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入要素集或要素类";
            this.btnSelectIn.Image = (Image) resources.GetObject("btnSelectIn.Image");
            this.btnSelectIn.Location = new System.Drawing.Point(320, 0x18);
            this.btnSelectIn.Name = "btnSelectIn";
            this.btnSelectIn.Size = new Size(0x20, 0x18);
            this.btnSelectIn.TabIndex = 15;
            this.btnSelectIn.Click += new EventHandler(this.btnSelectIn_Click);
            this.txtInputFeat.EditValue = "";
            this.txtInputFeat.Location = new System.Drawing.Point(0x10, 0x18);
            this.txtInputFeat.Name = "txtInputFeat";
            this.txtInputFeat.Properties.ReadOnly = true;
            this.txtInputFeat.Size = new Size(0x128, 0x15);
            this.txtInputFeat.TabIndex = 14;
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnSelectOut);
            this.groupBox2.Controls.Add(this.txtOutFeat);
            this.groupBox2.Location = new System.Drawing.Point(0x10, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x178, 0x38);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出要数集或要素类";
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new System.Drawing.Point(8, 0x10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(10, 10);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0x11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.btnSelectOut.Image = (Image) resources.GetObject("btnSelectOut.Image");
            this.btnSelectOut.Location = new System.Drawing.Point(320, 0x18);
            this.btnSelectOut.Name = "btnSelectOut";
            this.btnSelectOut.Size = new Size(0x20, 0x18);
            this.btnSelectOut.TabIndex = 0x10;
            this.btnSelectOut.Click += new EventHandler(this.btnSelectOut_Click);
            this.txtOutFeat.EditValue = "";
            this.txtOutFeat.Location = new System.Drawing.Point(0x10, 0x18);
            this.txtOutFeat.Name = "txtOutFeat";
            this.txtOutFeat.Properties.ReadOnly = true;
            this.txtOutFeat.Size = new Size(0x128, 0x15);
            this.txtOutFeat.TabIndex = 15;
            this.txtOutFeat.TextChanged += new EventHandler(this.txtOutFeat_TextChanged);
            this.groupBox3.Controls.Add(this.btnSR);
            this.groupBox3.Controls.Add(this.txtOutSR);
            this.groupBox3.Location = new System.Drawing.Point(0x10, 0x98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x178, 0x38);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输出坐标系";
            this.btnSR.Image = (Image) resources.GetObject("btnSR.Image");
            this.btnSR.Location = new System.Drawing.Point(320, 20);
            this.btnSR.Name = "btnSR";
            this.btnSR.Size = new Size(0x20, 0x18);
            this.btnSR.TabIndex = 0x11;
            this.btnSR.Click += new EventHandler(this.btnSR_Click);
            this.txtOutSR.EditValue = "";
            this.txtOutSR.Location = new System.Drawing.Point(0x10, 0x16);
            this.txtOutSR.Name = "txtOutSR";
            this.txtOutSR.Properties.ReadOnly = true;
            this.txtOutSR.Size = new Size(0x128, 0x15);
            this.txtOutSR.TabIndex = 0x10;
            this.openFileDialog_0.Filter = "shp文件|*.shp";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0xf8, 0xd8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x148, 0xd8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.progressBar1.Location = new System.Drawing.Point(0x18, 0xe0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0xd8, 0x10);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x198, 0x105);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmProject";
            this.Text = "投影";
            this.groupBox1.ResumeLayout(false);
            this.txtInputFeat.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.txtOutFeat.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtOutSR.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private bool method_0()
        {
            if (this.iworkspace_0 == null)
            {
                this.bool_0 = false;
            }
            else if ((this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) || (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                if (((IWorkspace2) this.iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, this.string_0))
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
                }
            }
            else if (this.iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                if (File.Exists(this.txtOutFeat.Text + ".shp"))
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
                }
            }
            return this.bool_0;
        }

        private bool method_1()
        {
            return this.bool_1;
        }

        private void method_2(string string_1)
        {
        }

        private void method_3(int int_4)
        {
            this.int_1 = int_4;
            if (this.int_1 > 0)
            {
                this.progressBar1.Maximum = this.int_1;
            }
        }

        private void method_4(int int_4)
        {
            this.int_3 = int_4;
            this.progressBar1.Minimum = this.int_3;
        }

        private void method_5(int int_4)
        {
        }

        private void method_6(int int_4)
        {
            this.int_0 = int_4;
        }

        private void method_7()
        {
            this.int_2 += this.int_0;
            if (this.int_2 <= this.int_1)
            {
                this.progressBar1.Increment(this.int_0);
            }
        }

        private void txtOutFeat_TextChanged(object sender, EventArgs e)
        {
            if (this.method_0())
            {
                this.pictureBox1.Visible = true;
            }
            else
            {
                this.pictureBox1.Visible = false;
            }
        }

        public IFeatureDataConverter FeatureProgress
        {
            set
            {
                this.ifeatureProgress_Event_0 = (IFeatureProgress_Event) value;
                this.ifeatureProgress_Event_0.Step+=(new IFeatureProgress_StepEventHandler(this.method_7));
            }
        }
    }
}

