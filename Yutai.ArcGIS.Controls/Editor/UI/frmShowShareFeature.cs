using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal partial class frmShowShareFeature : Form
    {
        private IMap m_pFocusMap = null;
        private ITopologyGraph m_pTopologyGraph = null;

        public frmShowShareFeature()
        {
            this.InitializeComponent();
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
                        Yutai.ArcGIS.Common.Display.Flash.FlashFeature(pFocusMap.ScreenDisplay, feature);
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

