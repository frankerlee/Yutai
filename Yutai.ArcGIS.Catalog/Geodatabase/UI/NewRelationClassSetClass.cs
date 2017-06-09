using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class NewRelationClassSetClass : UserControl
    {
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TreeView treeViewDest;
        private TreeView treeViewSource;
        private TextEdit txtRelationClassName;

        public NewRelationClassSetClass()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.treeViewDest = new TreeView();
            this.treeViewSource = new TreeView();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtRelationClassName = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.txtRelationClassName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x48, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "关系类名称:";
            this.groupBox1.Controls.Add(this.treeViewDest);
            this.groupBox1.Controls.Add(this.treeViewSource);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(0x10, 0x40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe8, 0x108);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表/要素类";
            this.treeViewDest.HideSelection = false;
            this.treeViewDest.ImageIndex = -1;
            this.treeViewDest.Location = new Point(0x10, 0xa8);
            this.treeViewDest.Name = "treeViewDest";
            this.treeViewDest.SelectedImageIndex = -1;
            this.treeViewDest.Size = new Size(200, 80);
            this.treeViewDest.TabIndex = 4;
            this.treeViewDest.AfterSelect += new TreeViewEventHandler(this.treeViewDest_AfterSelect);
            this.treeViewSource.HideSelection = false;
            this.treeViewSource.ImageIndex = -1;
            this.treeViewSource.Location = new Point(0x10, 0x30);
            this.treeViewSource.Name = "treeViewSource";
            this.treeViewSource.SelectedImageIndex = -1;
            this.treeViewSource.Size = new Size(200, 80);
            this.treeViewSource.TabIndex = 3;
            this.treeViewSource.AfterSelect += new TreeViewEventHandler(this.treeViewSource_AfterSelect);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x8d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x55, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "目标表/要素类";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x48, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "源表/要素类";
            this.txtRelationClassName.EditValue = "";
            this.txtRelationClassName.Location = new Point(0x18, 0x20);
            this.txtRelationClassName.Name = "txtRelationClassName";
            this.txtRelationClassName.Size = new Size(0xe0, 0x17);
            this.txtRelationClassName.TabIndex = 2;
            this.txtRelationClassName.EditValueChanged += new EventHandler(this.txtRelationClassName_EditValueChanged);
            base.Controls.Add(this.txtRelationClassName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.Name = "NewRelationClassSetClass";
            base.Size = new Size(0x110, 0x158);
            base.Load += new EventHandler(this.NewRelationClassSetClass_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtRelationClassName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(IDatasetName idatasetName_0, TreeNode treeNode_0)
        {
            try
            {
                IEnumDatasetName subsetNames = idatasetName_0.SubsetNames;
                subsetNames.Reset();
                for (IDatasetName name2 = subsetNames.Next(); name2 != null; name2 = subsetNames.Next())
                {
                    TreeNode node = new TreeNode(name2.Name) {
                        Tag = (name2 as IName).Open()
                    };
                    treeNode_0.Nodes.Add(node);
                }
            }
            catch (Exception)
            {
            }
        }

        private void NewRelationClassSetClass_Load(object sender, EventArgs e)
        {
            if (NewRelationClassHelper.m_pWorkspace != null)
            {
                IEnumDatasetName name = NewRelationClassHelper.m_pWorkspace.get_DatasetNames(esriDatasetType.esriDTAny);
                name.Reset();
                for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                {
                    if (((name2.Type == esriDatasetType.esriDTFeatureClass) || (name2.Type == esriDatasetType.esriDTTable)) || (name2.Type == esriDatasetType.esriDTFeatureDataset))
                    {
                        TreeNode node = new TreeNode(name2.Name) {
                            Tag = (name2 as IName).Open()
                        };
                        this.treeViewSource.Nodes.Add(node);
                        if (name2.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            this.method_0(name2, node);
                        }
                        node = new TreeNode(name2.Name) {
                            Tag = (name2 as IName).Open()
                        };
                        this.treeViewDest.Nodes.Add(node);
                        if (name2.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            this.method_0(name2, node);
                        }
                    }
                }
            }
        }

        private void treeViewDest_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NewRelationClassHelper.DestinationClass = e.Node.Tag as IObjectClass;
            if (NewRelationClassHelper.DestinationClass != null)
            {
                NewRelationClassHelper.backwardLabel = (NewRelationClassHelper.DestinationClass as IDataset).Name;
            }
        }

        private void treeViewSource_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NewRelationClassHelper.OriginClass = e.Node.Tag as IObjectClass;
            if (NewRelationClassHelper.OriginClass != null)
            {
                NewRelationClassHelper.forwardLabel = (NewRelationClassHelper.OriginClass as IDataset).Name;
            }
        }

        private void txtRelationClassName_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.relClassName = this.txtRelationClassName.Text;
        }
    }
}

