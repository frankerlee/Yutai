using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class TocTreeViewBase : UserControl
    {
        public IBasicMap m_FocusMap = null;
        public TOCTreeNode m_FocusMapNode = null;
        protected TOCTreeNode m_pLHitNode = null;
        protected TOCTreeNodeCollection m_pNodes = new TOCTreeNodeCollection();
        protected TOCTreeNodeCollection m_pSelectedNodes = new TOCTreeNodeCollection();

        public void ClearSelection()
        {
            for (int i = 0; i < this.m_pSelectedNodes.Count; i++)
            {
                TOCTreeNode node = this.m_pSelectedNodes[i] as TOCTreeNode;
                node.IsSelected = false;
            }
            this.m_pLHitNode = null;
            this.SelectedNode = null;
            this.m_pSelectedNodes.Clear();
        }

        public void SelectNode(TOCTreeNode pSelectedNode)
        {
            if (this.m_pSelectedNodes.IndexOf(pSelectedNode) == -1)
            {
                if (this.SelectedNode == null)
                {
                    this.SelectedNode = pSelectedNode;
                }
                pSelectedNode.IsSelected = true;
                this.m_pSelectedNodes.Add(pSelectedNode);
            }
        }

        public TOCTreeNodeCollection Nodes
        {
            get
            {
                return this.m_pNodes;
            }
        }

        public TOCTreeNode SelectedNode { get; set; }

        public TOCTreeNodeCollection SelectedNodes
        {
            get
            {
                return this.m_pSelectedNodes;
            }
        }
    }
}

