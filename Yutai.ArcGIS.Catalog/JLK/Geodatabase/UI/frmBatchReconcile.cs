namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class frmBatchReconcile : Form
    {
        private SimpleButton btnClose;
        private SimpleButton btnRefresh;
        private SimpleButton btnStart;
        private CheckEdit chkDeleteOnPost;
        private CheckEdit chkPost;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private IVersionedWorkspace iversionedWorkspace_0;
        private TextEdit txtMessage;
        private TreeView VersionTreeView;

        public frmBatchReconcile()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.VersionTreeView.Nodes.Clear();
            this.method_2();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.btnClose.Enabled = false;
            string text = this.VersionTreeView.SelectedNode.Text;
            try
            {
                IVersion version = this.iversionedWorkspace_0.FindVersion(text);
                IEnumVersionInfo children = version.VersionInfo.Children;
                if (children.Next() == null)
                {
                    MessageBox.Show("版本[" + text + "]无要合并的子版本！");
                    this.btnStart.Enabled = true;
                    this.btnClose.Enabled = true;
                    return;
                }
                this.txtMessage.Text = "开始合并";
                if (this.chkPost.Checked)
                {
                    children.Reset();
                    this.method_3(this.iversionedWorkspace_0, children, version.VersionName, true, this.chkDeleteOnPost.Checked);
                }
                this.method_3(this.iversionedWorkspace_0, version.VersionInfo.Children, version.VersionName, false, false);
                this.txtMessage.Text = "合并完成";
            }
            catch (COMException exception)
            {
                if (exception.ErrorCode == -2147215997)
                {
                    MessageBox.Show("该用户不是版本 [" + text + "] 的所有者，无法修改版本的信息");
                }
                this.txtMessage.Text = "合并失败!";
            }
            this.btnStart.Enabled = true;
            this.btnClose.Enabled = true;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmBatchReconcile_Load(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmBatchReconcile));
            this.groupBox2 = new GroupBox();
            this.chkDeleteOnPost = new CheckEdit();
            this.chkPost = new CheckEdit();
            this.btnRefresh = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.VersionTreeView = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.btnStart = new SimpleButton();
            this.btnClose = new SimpleButton();
            this.txtMessage = new TextEdit();
            this.groupBox2.SuspendLayout();
            this.chkDeleteOnPost.Properties.BeginInit();
            this.chkPost.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtMessage.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.chkDeleteOnPost);
            this.groupBox2.Controls.Add(this.chkPost);
            this.groupBox2.Location = new Point(8, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe0, 0x48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选项";
            this.chkDeleteOnPost.Location = new Point(0x10, 0x2b);
            this.chkDeleteOnPost.Name = "chkDeleteOnPost";
            this.chkDeleteOnPost.Properties.Caption = "合并后删除子版本";
            this.chkDeleteOnPost.Size = new Size(0x80, 0x13);
            this.chkDeleteOnPost.TabIndex = 1;
            this.chkPost.Location = new Point(0x10, 0x11);
            this.chkPost.Name = "chkPost";
            this.chkPost.Properties.Caption = "统一子版本到母版本";
            this.chkPost.Size = new Size(0x88, 0x13);
            this.chkPost.TabIndex = 0;
            this.btnRefresh.Location = new Point(0x98, 0xd7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(0x48, 0x18);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新版本树";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.groupBox1.Controls.Add(this.VersionTreeView);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe0, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择父版本";
            this.VersionTreeView.ImageIndex = 0;
            this.VersionTreeView.ImageList = this.imageList_0;
            this.VersionTreeView.Location = new Point(8, 0x18);
            this.VersionTreeView.Name = "VersionTreeView";
            this.VersionTreeView.SelectedImageIndex = 0;
            this.VersionTreeView.Size = new Size(0xd0, 0xa8);
            this.VersionTreeView.TabIndex = 0;
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.imageList_0.Images.SetKeyName(2, "");
            this.btnStart.Location = new Point(80, 320);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(80, 0x18);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始合并";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0xa8, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x40, 0x18);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.txtMessage.EditValue = "";
            this.txtMessage.Location = new Point(8, 0x160);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtMessage.Properties.Appearance.Options.UseBackColor = true;
            this.txtMessage.Properties.ReadOnly = true;
            this.txtMessage.Size = new Size(0xe0, 0x15);
            this.txtMessage.TabIndex = 8;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(240, 0x17d);
            base.Controls.Add(this.txtMessage);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnRefresh);
            base.Controls.Add(this.groupBox1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmBatchReconcile";
            this.Text = "批量提交";
            base.Load += new EventHandler(this.frmBatchReconcile_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkDeleteOnPost.Properties.EndInit();
            this.chkPost.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.txtMessage.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(IVersionedWorkspace iversionedWorkspace_1, TreeView treeView_0)
        {
            try
            {
                IVersionInfo versionInfo = iversionedWorkspace_1.DefaultVersion.VersionInfo;
                TreeNode node = new TreeNode(versionInfo.VersionName) {
                    SelectedImageIndex = 0,
                    Tag = versionInfo
                };
                treeView_0.Nodes.Add(node);
                this.method_1(versionInfo, node);
                treeView_0.SelectedNode = node;
            }
            catch
            {
            }
        }

        private void method_1(IVersionInfo iversionInfo_0, TreeNode treeNode_0)
        {
            try
            {
                treeNode_0.Expand();
                IEnumVersionInfo children = iversionInfo_0.Children;
                iversionInfo_0 = children.Next();
                while (iversionInfo_0 != null)
                {
                    TreeNode node = new TreeNode(iversionInfo_0.VersionName) {
                        Tag = iversionInfo_0
                    };
                    treeNode_0.Nodes.Add(node);
                    this.method_1(iversionInfo_0, node);
                    iversionInfo_0 = children.Next();
                }
            }
            catch
            {
            }
        }

        private void method_10(IVersion iversion_0, int int_0)
        {
            TreeNode node = this.method_8(iversion_0.VersionName);
            node.ImageIndex = int_0;
            node.SelectedImageIndex = int_0;
            this.VersionTreeView.Refresh();
        }

        private bool method_11(IVersion iversion_0)
        {
            try
            {
                IEnumLockInfo versionLocks = iversion_0.VersionLocks;
                if (versionLocks.Next() != null)
                {
                    this.txtMessage.Text = "版本 " + iversion_0.VersionName + "被锁定";
                    ILockInfo info2 = versionLocks.Next();
                    return true;
                }
            }
            catch
            {
                return true;
            }
            return false;
        }

        private void method_2()
        {
            this.method_0(this.iversionedWorkspace_0, this.VersionTreeView);
        }

        private bool method_3(IVersionedWorkspace iversionedWorkspace_1, IEnumVersionInfo ienumVersionInfo_0, string string_0, bool bool_0, bool bool_1)
        {
            bool flag = true;
            try
            {
                for (IVersionInfo info = ienumVersionInfo_0.Next(); info != null; info = ienumVersionInfo_0.Next())
                {
                    IVersionEdit edit;
                    if (bool_0)
                    {
                        if (!this.method_3(iversionedWorkspace_1, info.Children, info.VersionName, bool_0, bool_1))
                        {
                            flag = false;
                            this.txtMessage.Text = "无法合并 " + info.VersionName + "的所有子版";
                        }
                        else
                        {
                            this.txtMessage.Text = "正在合并 " + info.VersionName;
                            edit = iversionedWorkspace_1.FindVersion(info.VersionName) as IVersionEdit;
                            if (!this.method_4(edit, string_0, bool_0, bool_1))
                            {
                                flag = true;
                            }
                        }
                    }
                    else
                    {
                        this.txtMessage.Text = "正在合并 " + info.VersionName;
                        edit = iversionedWorkspace_1.FindVersion(info.VersionName) as IVersionEdit;
                        if (this.method_4(edit, string_0, bool_0, bool_1))
                        {
                            this.method_3(iversionedWorkspace_1, info.Children, info.VersionName, bool_0, bool_1);
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private bool method_4(IVersionEdit iversionEdit_0, string string_0, bool bool_0, bool bool_1)
        {
            bool flag = true;
            IVersion version = iversionEdit_0 as IVersion;
            this.method_10(version, 0);
            try
            {
                if (this.method_11(iversionEdit_0 as IVersion))
                {
                    flag = false;
                }
                else
                {
                    this.method_5(iversionEdit_0 as IWorkspaceEdit);
                    this.txtMessage.Text = "正在将版本 " + version.VersionName + " 合并到版本 " + string_0;
                    if (iversionEdit_0.Reconcile(string_0))
                    {
                        this.txtMessage.Text = "在合并版本 " + version.VersionName + " 到版本 " + string_0 + " 时发现冲突";
                        this.method_6(iversionEdit_0 as IWorkspaceEdit, false);
                        flag = false;
                    }
                    else if (iversionEdit_0.CanPost() && bool_0)
                    {
                        this.txtMessage.Text = "合并 " + version.VersionName;
                        iversionEdit_0.Post(string_0);
                        this.method_6(iversionEdit_0 as IWorkspaceEdit, true);
                        if (bool_1)
                        {
                            this.method_7(iversionEdit_0 as IVersion);
                        }
                    }
                    else
                    {
                        this.method_6(iversionEdit_0 as IWorkspaceEdit, true);
                    }
                }
                if (flag)
                {
                    this.method_10(version, 2);
                    return flag;
                }
                this.method_10(version, 1);
            }
            catch
            {
                this.method_6(iversionEdit_0 as IWorkspaceEdit, false);
                flag = false;
            }
            return flag;
        }

        private void method_5(IWorkspaceEdit iworkspaceEdit_0)
        {
            iworkspaceEdit_0.StartEditing(false);
            iworkspaceEdit_0.StartEditOperation();
        }

        private void method_6(IWorkspaceEdit iworkspaceEdit_0, bool bool_0)
        {
            if (bool_0)
            {
                iworkspaceEdit_0.StopEditOperation();
                iworkspaceEdit_0.StopEditing(true);
            }
            else
            {
                iworkspaceEdit_0.AbortEditOperation();
                iworkspaceEdit_0.StopEditing(false);
            }
        }

        private void method_7(IVersion iversion_0)
        {
            try
            {
                string versionName = iversion_0.VersionName;
                iversion_0.Delete();
                TreeNode node = this.method_8(versionName);
                if (node.Parent != null)
                {
                    node.Parent.Nodes.Remove(node);
                }
                else
                {
                    this.VersionTreeView.Nodes.Remove(node);
                }
                this.VersionTreeView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private TreeNode method_8(string string_0)
        {
            TreeNodeCollection nodes = this.VersionTreeView.Nodes;
            TreeNode node = null;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Text.ToUpper() == string_0.ToUpper())
                {
                    return nodes[i];
                }
                if (nodes[i].Nodes.Count > 0)
                {
                    node = this.method_9(string_0, nodes[i].Nodes);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return node;
        }

        private TreeNode method_9(string string_0, TreeNodeCollection treeNodeCollection_0)
        {
            TreeNode node = null;
            for (int i = 0; i < treeNodeCollection_0.Count; i++)
            {
                if (treeNodeCollection_0[i].Text.ToUpper() == string_0.ToUpper())
                {
                    return treeNodeCollection_0[i];
                }
                if (treeNodeCollection_0[i].Nodes.Count > 0)
                {
                    node = this.method_9(string_0, treeNodeCollection_0[i].Nodes);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return node;
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

