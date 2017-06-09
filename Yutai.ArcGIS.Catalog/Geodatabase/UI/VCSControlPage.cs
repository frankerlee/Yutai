using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class VCSControlPage : UserControl
    {
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private IContainer icontainer_0 = null;
        private ImageList imageList_0;
        private ISpatialReferenceInfo ispatialReferenceInfo_0 = null;
        private Label label1;
        private Label label2;
        private TextEdit textBoxName;
        private TreeView treeView1;

        public VCSControlPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is ISpatialReference3)
            {
                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem = this.ispatialReferenceInfo_0 as IVerticalCoordinateSystem;
            }
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
                        if (dataset2.SpatialReference is IUnknownCoordinateSystem)
                        {
                            this.ispatialReferenceInfo_0 = null;
                            this.textBoxName.Text = "<NONE>";
                            this.textBoxName.Tag = null;
                        }
                        else
                        {
                            this.ispatialReferenceInfo_0 = (dataset2.SpatialReference as ISpatialReference3).VerticalCoordinateSystem;
                            this.textBoxName.Tag = this.ispatialReferenceInfo_0;
                            this.textBoxName.Text = this.ispatialReferenceInfo_0.Name;
                        }
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmNewVCS wvcs = new frmNewVCS {
                VerticalCoordinateSystem = this.ispatialReferenceInfo_0 as IVerticalCoordinateSystem
            };
            if (wvcs.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReferenceInfo_0 = wvcs.VerticalCoordinateSystem;
                this.textBoxName.Tag = this.ispatialReferenceInfo_0;
                this.textBoxName.Text = this.ispatialReferenceInfo_0.Name;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewVCS wvcs = new frmNewVCS();
            if (wvcs.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReferenceInfo_0 = wvcs.VerticalCoordinateSystem;
                this.textBoxName.Tag = this.ispatialReferenceInfo_0;
                this.textBoxName.Text = this.ispatialReferenceInfo_0.Name;
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
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VCSControlPage));
            this.treeView1 = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.label2 = new Label();
            this.textBoxName = new TextEdit();
            this.label1 = new Label();
            this.btnImport = new SimpleButton();
            this.btnModify = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.textBoxName.Properties.BeginInit();
            base.SuspendLayout();
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new System.Drawing.Point(0x15, 0x58);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(0xf5, 0xb8);
            this.treeView1.TabIndex = 0x21;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "ProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(1, "SelectedProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(2, "Project.bmp");
            this.label2.AllowDrop = true;
            this.label2.Location = new System.Drawing.Point(0x13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x135, 0x18);
            this.label2.TabIndex = 0x20;
            this.label2.Text = "选择数据集Z轴的坐标系统";
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(60, 0x34);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(0xd8, 0x15);
            this.textBoxName.TabIndex = 0x1f;
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 0x36);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "名字:";
            this.btnImport.Location = new System.Drawing.Point(0x110, 0x58);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(0x38, 0x18);
            this.btnImport.TabIndex = 0x24;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.btnModify.Location = new System.Drawing.Point(0x110, 0x98);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(0x38, 0x18);
            this.btnModify.TabIndex = 0x23;
            this.btnModify.Text = "修改...";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnNew.Location = new System.Drawing.Point(0x110, 120);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x38, 0x18);
            this.btnNew.TabIndex = 0x22;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.label1);
            base.Name = "VCSControlPage";
            base.Size = new Size(0x15a, 0x166);
            base.Load += new EventHandler(this.VCSControlPage_Load);
            this.textBoxName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is ISpatialReferenceInfo)
            {
                this.textBoxName.Text = (e.Node.Tag as ISpatialReferenceInfo).Name;
                this.textBoxName.Tag = e.Node.Tag;
                this.ispatialReferenceInfo_0 = this.textBoxName.Tag as ISpatialReferenceInfo;
            }
            else if (e.Node.Nodes.Count == 0)
            {
                this.textBoxName.Text = "<NONE>";
                this.textBoxName.Tag = null;
                this.ispatialReferenceInfo_0 = null;
            }
        }

        private void VCSControlPage_Load(object sender, EventArgs e)
        {
            ISpatialReferenceFactory3 factory = new SpatialReferenceEnvironmentClass();
            TreeNode node = new TreeNode("垂直坐标系文件夹", 0, 1);
            this.treeView1.Nodes.Add(node);
            string path = System.IO.Path.Combine(Application.StartupPath, @"Coordinate Systems\Vertical Coordinate Systems");
            if (Directory.Exists(path))
            {
                string[] directories = Directory.GetDirectories(path);
                for (int i = 0; i < directories.Length; i++)
                {
                    TreeNode node2 = new TreeNode(System.IO.Path.GetFileName(directories[i]), 0, 1);
                    node.Nodes.Add(node2);
                    string[] files = Directory.GetFiles(directories[i]);
                    for (int j = 0; j < files.Length; j++)
                    {
                        if (System.IO.Path.GetExtension(files[j]).ToLower() == ".prj")
                        {
                            IVerticalCoordinateSystem system = factory.CreateESRISpatialReferenceInfoFromPRJFile(files[j]) as IVerticalCoordinateSystem;
                            if (system != null)
                            {
                                TreeNode node3 = new TreeNode(System.IO.Path.GetFileName(files[j]), 2, 2) {
                                    Tag = system
                                };
                                node2.Nodes.Add(node3);
                            }
                        }
                    }
                }
            }
            node = new TreeNode("<NONE>", 2, 2) {
                Tag = null
            };
            this.treeView1.Nodes.Add(node);
        }
    }
}

