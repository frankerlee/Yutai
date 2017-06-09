using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class NewDatasetSpatialRefPage : UserControl
    {
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem GeoToolStripMenuItem;
        private IContainer icontainer_0 = null;
        private ImageList imageList_0;
        private ISpatialReference ispatialReference_0 = null;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private Label label1;
        private Label label2;
        private ToolStripMenuItem ProjToolStripMenuItem;
        private TextEdit textBoxName;
        private TreeView treeView1;

        public NewDatasetSpatialRefPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.ispatialReference_0 == null)
            {
                return false;
            }
            NewObjectClassHelper.m_pObjectClassHelper.SpatialReference = this.ispatialReference_0;
            return true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterDatasets(), true);
            file.AllowMultiSelect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    IGeoDataset dataset2 = dataset.Dataset as IGeoDataset;
                    if (dataset2 != null)
                    {
                        this.ispatialReference_0 = dataset2.SpatialReference;
                        this.textBoxName.Text = this.ispatialReference_0.Name;
                        this.textBoxName.Tag = this.ispatialReference_0;
                        if (this.ispatialReference_0 is IUnknownCoordinateSystem)
                        {
                            this.btnModify.Enabled = false;
                        }
                        else
                        {
                            this.btnModify.Enabled = true;
                        }
                        IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                        if (NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision != precision.IsHighPrecision)
                        {
                            if (precision.IsHighPrecision)
                            {
                                precision.IsHighPrecision = NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision;
                                (this.ispatialReference_0 as ISpatialReferenceResolution).ConstructFromHorizon();
                            }
                            else
                            {
                                precision.IsHighPrecision = NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision;
                            }
                        }
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!(this.ispatialReference_0 is IUnknownCoordinateSystem))
            {
                frmSpatialRefrence refrence = new frmSpatialRefrence {
                    SpatialRefrence = this.ispatialReference_0
                };
                if (refrence.ShowDialog() == DialogResult.OK)
                {
                    this.ispatialReference_0 = refrence.SpatialRefrence;
                    this.textBoxName.Text = this.ispatialReference_0.Name;
                    this.textBoxName.Tag = this.ispatialReference_0;
                    this.method_0();
                }
                refrence.Dispose();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            System.Drawing.Point position = new System.Drawing.Point(this.btnNew.Location.X, this.btnNew.Location.Y + this.btnNew.Height);
            this.contextMenuStrip1.Show(this, position);
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void GeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumGeographicCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.textBoxName.Text = this.ispatialReference_0.Name;
                this.textBoxName.Tag = this.ispatialReference_0;
                this.method_0();
            }
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDatasetSpatialRefPage));
            this.btnImport = new SimpleButton();
            this.btnModify = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.textBoxName = new TextEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.treeView1 = new TreeView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.GeoToolStripMenuItem = new ToolStripMenuItem();
            this.ProjToolStripMenuItem = new ToolStripMenuItem();
            this.textBoxName.Properties.BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.btnImport.Location = new System.Drawing.Point(0x10a, 0x55);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(0x38, 0x18);
            this.btnImport.TabIndex = 0x1b;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.btnModify.Location = new System.Drawing.Point(0x10a, 0x95);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(0x38, 0x18);
            this.btnModify.TabIndex = 0x1a;
            this.btnModify.Text = "修改...";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnNew.Location = new System.Drawing.Point(0x10a, 0x75);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x38, 0x18);
            this.btnNew.TabIndex = 0x19;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(0x36, 0x31);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(0xd8, 0x15);
            this.textBoxName.TabIndex = 0x18;
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0x33);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x17;
            this.label1.Text = "名字:";
            this.label2.AllowDrop = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x135, 0x18);
            this.label2.TabIndex = 0x1c;
            this.label2.Text = "选择数据集XY轴的坐标系统";
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "ProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(1, "SelectedProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(2, "Project.bmp");
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new System.Drawing.Point(15, 0x55);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(0xf5, 0xb8);
            this.treeView1.TabIndex = 0x1d;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.GeoToolStripMenuItem, this.ProjToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x99, 70);
            this.GeoToolStripMenuItem.Name = "GeoToolStripMenuItem";
            this.GeoToolStripMenuItem.Size = new Size(0x98, 0x16);
            this.GeoToolStripMenuItem.Text = "地理坐标系";
            this.GeoToolStripMenuItem.Click += new EventHandler(this.GeoToolStripMenuItem_Click);
            this.ProjToolStripMenuItem.Name = "ProjToolStripMenuItem";
            this.ProjToolStripMenuItem.Size = new Size(0x98, 0x16);
            this.ProjToolStripMenuItem.Text = "投影坐标系";
            this.ProjToolStripMenuItem.Click += new EventHandler(this.ProjToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.label1);
            base.Name = "NewDatasetSpatialRefPage";
            base.Size = new Size(0x17a, 0x165);
            base.Load += new EventHandler(this.NewDatasetSpatialRefPage_Load);
            this.textBoxName.Properties.EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
        }

        private void NewDatasetSpatialRefPage_Load(object sender, EventArgs e)
        {
            string[] directories;
            int num;
            TreeNode node2;
            string[] files;
            int num2;
            TreeNode node3;
            ISpatialReferenceFactory3 factory = new SpatialReferenceEnvironmentClass();
            TreeNode node = new TreeNode("地理坐标系文件夹", 0, 1);
            this.treeView1.Nodes.Add(node);
            string startupPath = Application.StartupPath;
            string path = System.IO.Path.Combine(startupPath, @"Coordinate Systems\Geographic Coordinate Systems");
            if (Directory.Exists(path))
            {
                directories = Directory.GetDirectories(path);
                for (num = 0; num < directories.Length; num++)
                {
                    node2 = new TreeNode(System.IO.Path.GetFileName(directories[num]), 0, 1);
                    node.Nodes.Add(node2);
                    files = Directory.GetFiles(directories[num]);
                    num2 = 0;
                    while (num2 < files.Length)
                    {
                        if (System.IO.Path.GetExtension(files[num2]).ToLower() == ".prj")
                        {
                            node3 = new TreeNode(System.IO.Path.GetFileName(files[num2]), 2, 2) {
                                Tag = factory.CreateESRISpatialReferenceFromPRJFile(files[num2])
                            };
                            node2.Nodes.Add(node3);
                        }
                        num2++;
                    }
                }
            }
            node = new TreeNode("投影坐标系文件夹", 0, 1);
            this.treeView1.Nodes.Add(node);
            path = System.IO.Path.Combine(startupPath, @"Coordinate Systems\Projected Coordinate Systems");
            if (Directory.Exists(path))
            {
                directories = Directory.GetDirectories(path);
                for (num = 0; num < directories.Length; num++)
                {
                    node2 = new TreeNode(System.IO.Path.GetFileName(directories[num]), 0, 1);
                    node.Nodes.Add(node2);
                    string[] strArray3 = Directory.GetDirectories(directories[num]);
                    for (int i = 0; i < strArray3.Length; i++)
                    {
                        TreeNode node4 = new TreeNode(System.IO.Path.GetFileName(strArray3[i]), 0, 1);
                        node2.Nodes.Add(node4);
                        files = Directory.GetFiles(strArray3[i]);
                        for (num2 = 0; num2 < files.Length; num2++)
                        {
                            if (System.IO.Path.GetExtension(files[num2]).ToLower() == ".prj")
                            {
                                node3 = new TreeNode(System.IO.Path.GetFileName(files[num2]), 2, 2) {
                                    Tag = factory.CreateESRISpatialReferenceFromPRJFile(files[num2])
                                };
                                node4.Nodes.Add(node3);
                            }
                        }
                    }
                }
            }
            node = new TreeNode("未知坐标系统", 2, 2) {
                Tag = SpatialReferenctOperator.ConstructCoordinateSystem(NewObjectClassHelper.m_pObjectClassHelper.Workspace as IGeodatabaseRelease)
            };
            this.treeView1.Nodes.Add(node);
        }

        private void ProjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumProjectCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.textBoxName.Text = this.ispatialReference_0.Name;
                this.textBoxName.Tag = this.ispatialReference_0;
                this.method_0();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is ISpatialReference)
            {
                this.textBoxName.Text = (e.Node.Tag as ISpatialReference).Name;
                this.textBoxName.Tag = e.Node.Tag;
                this.ispatialReference_0 = this.textBoxName.Tag as ISpatialReference;
            }
        }
    }
}

