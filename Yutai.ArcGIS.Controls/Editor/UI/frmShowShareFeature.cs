using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class frmShowShareFeature : Form
    {
        private Container components = null;
        private IMap m_pFocusMap = null;
        private ITopologyGraph m_pTopologyGraph = null;
        private TreeView treeView1;

        public frmShowShareFeature()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Flash(int x, int y)
        {
            try
            {
                TreeNode nodeAt = this.treeView1.GetNodeAt(x, y);
                if ((nodeAt != null) && (nodeAt.Parent != null))
                {
                    IActiveView pFocusMap = (IActiveView) this.m_pFocusMap;
                    int tag = (int) nodeAt.Tag;
                    IFeature feature = (nodeAt.Parent.Tag as IFeatureClass).GetFeature(tag);
                    if (feature != null)
                    {
                        Display.Flash.FlashFeature(pFocusMap.ScreenDisplay, feature);
                    }
                }
            }
            catch
            {
            }
        }

        private void frmShowShareFeature_Load(object sender, EventArgs e)
        {
            IEnumTopologyParent selectionParents = this.m_pTopologyGraph.SelectionParents;
            selectionParents.Reset();
            esriTopologyParent parent2 = selectionParents.Next();
            IFeatureClass pFC = null;
            TreeNode node = null;
            while (parent2.m_pFC != null)
            {
                if (pFC != parent2.m_pFC)
                {
                    pFC = parent2.m_pFC;
                    node = new TreeNode(pFC.AliasName) {
                        Tag = pFC
                    };
                    this.treeView1.Nodes.Add(node);
                }
                TreeNode node2 = new TreeNode(parent2.m_FID.ToString()) {
                    Tag = parent2.m_FID
                };
                node.Nodes.Add(node2);
                parent2 = selectionParents.Next();
            }
        }

        private void InitializeComponent()
        {
            this.treeView1 = new TreeView();
            base.SuspendLayout();
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(0xda, 0xad);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseDown += new MouseEventHandler(this.treeView1_MouseDown);
            this.treeView1.Click += new EventHandler(this.treeView1_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xda, 0xad);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmShowShareFeature";
            this.Text = "共享要素";
            base.Load += new EventHandler(this.frmShowShareFeature_Load);
            base.ResumeLayout(false);
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Flash(e.X, e.Y);
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pFocusMap = value;
            }
        }

        public ITopologyGraph TopologyGraph
        {
            set
            {
                this.m_pTopologyGraph = value;
            }
        }
    }
}

