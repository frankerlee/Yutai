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
    public class frmVersionDifferences : Form
    {
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private BarManager barManager_0;
        private SimpleButton btnGenerateReport;
        private GroupBox groupBox1;
        private IContainer icontainer_0;
        private IVersionedWorkspace iversionedWorkspace_0;
        private IVersionInfo iversionInfo_0;
        private IWorkspace iworkspace_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBoxControl lstReport;
        private BarButtonItem mnuSetChildVersion;
        private BarButtonItem mnuSetParentVersion;
        private PopupMenu popupMenu1;
        private string string_0;
        private string string_1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TreeView treeViewDataset;
        private TreeView treeViewVersions;
        private TextEdit txtChildVersion;
        private TextEdit txtParentVersion;
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
                this.versioningUtil_0.GetDifferences1(this.iworkspace_0, this.txtParentVersion.Text, this.txtChildVersion.Text, this.treeViewDataset.Nodes, list);
                for (int i = 0; i < list.Count; i++)
                {
                    this.lstReport.Items.Add(list[i]);
                }
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
                        node = new TreeNode(name2.Name) {
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
                                TreeNode node2 = new TreeNode(name4.Name) {
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
                        node = new TreeNode(name4.Name) {
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

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersionDifferences));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.groupBox1 = new GroupBox();
            this.txtChildVersion = new TextEdit();
            this.txtParentVersion = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.treeViewVersions = new TreeView();
            this.tabPage2 = new TabPage();
            this.treeViewDataset = new TreeView();
            this.lstReport = new ListBoxControl();
            this.label3 = new Label();
            this.btnGenerateReport = new SimpleButton();
            this.barManager_0 = new BarManager(this.icontainer_0);
            this.barDockControl_0 = new BarDockControl();
            this.barDockControl_1 = new BarDockControl();
            this.barDockControl_2 = new BarDockControl();
            this.barDockControl_3 = new BarDockControl();
            this.mnuSetParentVersion = new BarButtonItem();
            this.mnuSetChildVersion = new BarButtonItem();
            this.popupMenu1 = new PopupMenu(this.icontainer_0);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.txtChildVersion.Properties.BeginInit();
            this.txtParentVersion.Properties.BeginInit();
            this.tabPage2.SuspendLayout();
            ((ISupportInitialize) this.lstReport).BeginInit();
            this.barManager_0.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0xe8, 0x138);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.treeViewVersions);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(0xe0, 0x11f);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "版本";
            this.groupBox1.Controls.Add(this.txtChildVersion);
            this.groupBox1.Controls.Add(this.txtParentVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 0x98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比较版本";
            this.txtChildVersion.EditValue = "";
            this.txtChildVersion.Location = new Point(0x10, 0x58);
            this.txtChildVersion.Name = "txtChildVersion";
            this.txtChildVersion.Size = new Size(160, 0x15);
            this.txtChildVersion.TabIndex = 3;
            this.txtParentVersion.EditValue = "";
            this.txtParentVersion.Location = new Point(0x10, 40);
            this.txtParentVersion.Name = "txtParentVersion";
            this.txtParentVersion.Size = new Size(160, 0x15);
            this.txtParentVersion.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "子版本";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "父/祖先版本";
            this.treeViewVersions.Location = new Point(8, 8);
            this.treeViewVersions.Name = "treeViewVersions";
            this.treeViewVersions.Size = new Size(200, 0x80);
            this.treeViewVersions.TabIndex = 0;
            this.treeViewVersions.MouseUp += new MouseEventHandler(this.treeViewVersions_MouseUp);
            this.treeViewVersions.Click += new EventHandler(this.treeViewVersions_Click);
            this.tabPage2.Controls.Add(this.treeViewDataset);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new Size(0xe0, 0x11f);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据集";
            this.treeViewDataset.CheckBoxes = true;
            this.treeViewDataset.Location = new Point(8, 8);
            this.treeViewDataset.Name = "treeViewDataset";
            this.treeViewDataset.Size = new Size(0xd0, 0x108);
            this.treeViewDataset.TabIndex = 0;
            this.lstReport.ItemHeight = 15;
            this.lstReport.Location = new Point(0x100, 0x20);
            this.lstReport.Name = "lstReport";
            this.lstReport.Size = new Size(360, 0x120);
            this.lstReport.TabIndex = 1;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x100, 8);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "报告";
            this.btnGenerateReport.Location = new Point(0x98, 0x148);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new Size(80, 0x18);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "产生报告";
            this.btnGenerateReport.Click += new EventHandler(this.btnGenerateReport_Click);
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[] { this.mnuSetParentVersion, this.mnuSetChildVersion });
            this.barManager_0.MaxItemId = 2;
            this.mnuSetParentVersion.Caption = "设置为父版本";
            this.mnuSetParentVersion.Id = 0;
            this.mnuSetParentVersion.Name = "mnuSetParentVersion";
            this.mnuSetParentVersion.ItemClick += new ItemClickEventHandler(this.mnuSetParentVersion_ItemClick);
            this.mnuSetChildVersion.Caption = "设置为子版本";
            this.mnuSetChildVersion.Id = 1;
            this.mnuSetChildVersion.Name = "mnuSetChildVersion";
            this.mnuSetChildVersion.ItemClick += new ItemClickEventHandler(this.mnuSetChildVersion_ItemClick);
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.mnuSetParentVersion), new LinkPersistInfo(this.mnuSetChildVersion) });
            this.popupMenu1.Manager = this.barManager_0;
            this.popupMenu1.Name = "popupMenu1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x278, 0x165);
            base.Controls.Add(this.btnGenerateReport);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lstReport);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            
            base.Name = "frmVersionDifferences";
            this.Text = "版本差异";
            base.Load += new EventHandler(this.frmVersionDifferences_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtChildVersion.Properties.EndInit();
            this.txtParentVersion.Properties.EndInit();
            this.tabPage2.ResumeLayout(false);
            ((ISupportInitialize) this.lstReport).EndInit();
            this.barManager_0.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
                    TreeNode node = new TreeNode(iversionInfo_1.VersionName) {
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
                TreeNode node = new TreeNode(versionInfo.VersionName) {
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
                string[] strArray = this.iversionInfo_0.VersionName.ToUpper().Split(new char[] { '.' });
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
            set
            {
                this.iversionedWorkspace_0 = value;
            }
        }
    }
}

