using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmVersionDifferences : Form
    {
        private VersioningUtil versioningUtil_0 = new VersioningUtil();

        public frmVersionDifferences()
        {
            this.InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if ((this.txtChildVersion.Text.Length != 0) && (this.txtParentVersion.Text.Length != 0))
            {
                this.lstReport.Items.Clear();
                IList list = new ArrayList();
                this.versioningUtil_0.GetDifferences1(this.iworkspace_0, this.txtParentVersion.Text,
                    this.txtChildVersion.Text, this.treeViewDataset.Nodes, list);
                for (int i = 0; i < list.Count; i++)
                {
                    this.lstReport.Items.Add(list[i]);
                }
            }
        }

        public void FillTreeViewWithDatasetNames(IWorkspace iworkspace_1, TreeView treeView_0)
        {
            try
            {
                treeView_0.Nodes.Clear();
                if (iworkspace_1 != null)
                {
                    TreeNode node;
                    IEnumDatasetName subsetNames;
                    IDatasetName name4;
                    IEnumDatasetName name = iworkspace_1.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                    name.Reset();
                    for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                    {
                        node = new TreeNode(name2.Name)
                        {
                            Tag = name2.Name
                        };
                        treeView_0.Nodes.Add(node);
                        subsetNames = name2.SubsetNames;
                        subsetNames.Reset();
                        name4 = subsetNames.Next();
                        while (name4 != null)
                        {
                            if (name4.Type == esriDatasetType.esriDTFeatureClass)
                            {
                                TreeNode node2 = new TreeNode(name4.Name)
                                {
                                    Tag = name4
                                };
                                node.Nodes.Add(node2);
                            }
                            name4 = subsetNames.Next();
                        }
                    }
                    subsetNames = iworkspace_1.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                    subsetNames.Reset();
                    for (name4 = subsetNames.Next(); name4 != null; name4 = subsetNames.Next())
                    {
                        node = new TreeNode(name4.Name)
                        {
                            Tag = name4.Name
                        };
                        treeView_0.Nodes.Add(node);
                    }
                }
            }
            catch
            {
            }
        }

        private void frmVersionDifferences_Load(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void method_0(IVersionInfo iversionInfo_1, TreeNode treeNode_0)
        {
            try
            {
                treeNode_0.Expand();
                IEnumVersionInfo children = iversionInfo_1.Children;
                iversionInfo_1 = children.Next();
                while (iversionInfo_1 != null)
                {
                    TreeNode node = new TreeNode(iversionInfo_1.VersionName)
                    {
                        Tag = iversionInfo_1
                    };
                    treeNode_0.Nodes.Add(node);
                    this.method_0(iversionInfo_1, node);
                    iversionInfo_1 = children.Next();
                }
            }
            catch
            {
            }
        }

        private void method_1(IVersionedWorkspace iversionedWorkspace_1, TreeView treeView_0)
        {
            try
            {
                IVersionInfo versionInfo = iversionedWorkspace_1.DefaultVersion.VersionInfo;
                TreeNode node = new TreeNode(versionInfo.VersionName)
                {
                    Tag = versionInfo
                };
                treeView_0.Nodes.Add(node);
                this.method_0(versionInfo, node);
                treeView_0.SelectedNode = node;
            }
            catch
            {
            }
        }

        private void method_2()
        {
            try
            {
                IVersion version = this.iversionedWorkspace_0 as IVersion;
                this.iversionInfo_0 = version.VersionInfo;
                string[] strArray = this.iversionInfo_0.VersionName.ToUpper().Split(new char[] {'.'});
                if (strArray[strArray.Length - 1] == "DEFAULT")
                {
                    this.string_0 = this.iversionInfo_0.VersionName;
                }
                else
                {
                    this.string_1 = this.iversionInfo_0.VersionName;
                }
                this.method_1(this.iversionedWorkspace_0, this.treeViewVersions);
                this.iworkspace_0 = this.iversionedWorkspace_0 as IWorkspace;
                this.FillTreeViewWithDatasetNames(this.iworkspace_0, this.treeViewDataset);
            }
            catch
            {
            }
        }

        private void mnuSetChildVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            IVersionInfo tag = this.treeViewVersions.SelectedNode.Tag as IVersionInfo;
            if (tag.VersionName != this.txtParentVersion.Text)
            {
                this.txtChildVersion.Text = tag.VersionName;
            }
        }

        private void mnuSetParentVersion_ItemClick(object sender, ItemClickEventArgs e)
        {
            IVersionInfo tag = this.treeViewVersions.SelectedNode.Tag as IVersionInfo;
            if (tag.VersionName != this.txtChildVersion.Text)
            {
                this.txtParentVersion.Text = tag.VersionName;
            }
        }

        private void treeViewVersions_Click(object sender, EventArgs e)
        {
        }

        private void treeViewVersions_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode nodeAt = this.treeViewVersions.GetNodeAt(e.X, e.Y);
                if (nodeAt != this.treeViewVersions.SelectedNode)
                {
                    this.treeViewVersions.SelectedNode = nodeAt;
                }
                Point p = this.treeViewVersions.PointToScreen(new Point(e.X, e.Y));
                this.popupMenu1.ShowPopup(p);
            }
        }

        public IVersionedWorkspace VersionedWorkspace
        {
            set { this.iversionedWorkspace_0 = value; }
        }
    }
}