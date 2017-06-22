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
    public partial class NewDatasetSpatialRefPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private ISpatialReference ispatialReference_0 = null;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();

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

