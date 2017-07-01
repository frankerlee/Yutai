using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmVersions : Form
    {
        private IVersionedWorkspace iversionedWorkspace_0 = null;

        public frmVersions()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            new ChangeVersionUtil().ChangeVersionByName(this.igraphicsContainer_0,
                this.iversionedWorkspace_0 as IFeatureWorkspace, this.string_0);
            base.Close();
        }

        private void frmVersions_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            if (this.iversionedWorkspace_0 != null)
            {
                this.btnOK.Enabled = false;
                this.string_1 = (this.iversionedWorkspace_0 as IVersion).VersionName;
                this.string_0 = null;
                IVersionInfo versionInfo = this.iversionedWorkspace_0.DefaultVersion.VersionInfo;
                IEnumVersionInfo children = versionInfo.Children;
                TreeNode node = new TreeNode(versionInfo.VersionName)
                {
                    Tag = versionInfo
                };
                if (versionInfo.VersionName == this.string_1)
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                }
                else
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                }
                this.treeView1.Nodes.Add(node);
                this.method_1(node, children);
                node.ExpandAll();
            }
        }

        private void method_1(TreeNode treeNode_0, IEnumVersionInfo ienumVersionInfo_0)
        {
            ienumVersionInfo_0.Reset();
            for (IVersionInfo info = ienumVersionInfo_0.Next(); info != null; info = ienumVersionInfo_0.Next())
            {
                TreeNode node = new TreeNode(info.VersionName)
                {
                    Tag = info
                };
                if (info.VersionName == this.string_1)
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                }
                else
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                }
                treeNode_0.Nodes.Add(node);
                this.method_1(node, info.Children);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.string_0 = e.Node.Text;
            this.btnOK.Enabled = true;
        }

        public IGraphicsContainer GraphicsContainer
        {
            set { this.igraphicsContainer_0 = value; }
        }

        public IVersionedWorkspace VersionedWorkspace
        {
            set { this.iversionedWorkspace_0 = value; }
        }
    }
}