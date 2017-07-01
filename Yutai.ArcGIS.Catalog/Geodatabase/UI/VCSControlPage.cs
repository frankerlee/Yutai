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
    public partial class VCSControlPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private ISpatialReferenceInfo ispatialReferenceInfo_0 = null;

        public VCSControlPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is ISpatialReference3)
            {
                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3)
                    .VerticalCoordinateSystem = this.ispatialReferenceInfo_0 as IVerticalCoordinateSystem;
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
                            this.ispatialReferenceInfo_0 =
                                (dataset2.SpatialReference as ISpatialReference3).VerticalCoordinateSystem;
                            this.textBoxName.Tag = this.ispatialReferenceInfo_0;
                            this.textBoxName.Text = this.ispatialReferenceInfo_0.Name;
                        }
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmNewVCS wvcs = new frmNewVCS
            {
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
            string path = System.IO.Path.Combine(Application.StartupPath,
                @"Coordinate Systems\Vertical Coordinate Systems");
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
                            IVerticalCoordinateSystem system =
                                factory.CreateESRISpatialReferenceInfoFromPRJFile(files[j]) as IVerticalCoordinateSystem;
                            if (system != null)
                            {
                                TreeNode node3 = new TreeNode(System.IO.Path.GetFileName(files[j]), 2, 2)
                                {
                                    Tag = system
                                };
                                node2.Nodes.Add(node3);
                            }
                        }
                    }
                }
            }
            node = new TreeNode("<NONE>", 2, 2)
            {
                Tag = null
            };
            this.treeView1.Nodes.Add(node);
        }
    }
}