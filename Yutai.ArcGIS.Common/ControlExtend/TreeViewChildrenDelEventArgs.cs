using System;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class TreeViewChildrenDelEventArgs : EventArgs
    {
        private TreeNode treeNode_0;
        private TreeNodeCollection treeNodeCollection_0;

        public TreeViewChildrenDelEventArgs()
        {
            this.treeNode_0 = null;
            this.treeNodeCollection_0 = null;
        }

        public TreeViewChildrenDelEventArgs(TreeNode treeNode_1, TreeNodeCollection treeNodeCollection_1)
        {
            this.treeNode_0 = null;
            this.treeNodeCollection_0 = null;
            this.treeNode_0 = treeNode_1;
            this.treeNodeCollection_0 = treeNodeCollection_1;
        }

        public TreeNode Node
        {
            get { return this.treeNode_0; }
            set { this.treeNode_0 = value; }
        }

        public TreeNodeCollection Nodes
        {
            get { return this.treeNodeCollection_0; }
            set { this.treeNodeCollection_0 = value; }
        }
    }
}