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
    internal class frmVersions : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private IContainer icontainer_0;
        private IGraphicsContainer igraphicsContainer_0;
        private ImageList imageList_0;
        private IVersionedWorkspace iversionedWorkspace_0 = null;
        private string string_0;
        private string string_1;
        private TreeView treeView1;

        public frmVersions()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            new ChangeVersionUtil().ChangeVersionByName(this.igraphicsContainer_0, this.iversionedWorkspace_0 as IFeatureWorkspace, this.string_0);
            base.Close();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmVersions_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersions));
            this.treeView1 = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.treeView1.HideSelection = false;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new Point(0x10, 0x10);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(200, 0xa8);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x48, 0xc0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x90, 0xc0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xe8, 0xdd);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "frmVersions";
            this.Text = "改变版本";
            base.Load += new EventHandler(this.frmVersions_Load);
            base.ResumeLayout(false);
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
                TreeNode node = new TreeNode(versionInfo.VersionName) {
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
                TreeNode node = new TreeNode(info.VersionName) {
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
            set
            {
                this.igraphicsContainer_0 = value;
            }
        }

        public IVersionedWorkspace VersionedWorkspace
        {
            set
            {
                this.iversionedWorkspace_0 = value;
            }
        }
    }
}

