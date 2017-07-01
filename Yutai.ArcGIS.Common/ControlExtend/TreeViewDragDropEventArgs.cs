using System;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class TreeViewDragDropEventArgs : EventArgs
    {
        private TreeNode treeNode_0;
        private TreeNode treeNode_1;

        public TreeViewDragDropEventArgs()
        {
            this.treeNode_0 = null;
            this.treeNode_1 = null;
        }

        public TreeViewDragDropEventArgs(TreeNode treeNode_2, TreeNode treeNode_3)
        {
            this.treeNode_0 = null;
            this.treeNode_1 = null;
            this.treeNode_0 = treeNode_2;
            this.treeNode_1 = treeNode_3;
        }

        public TreeNode Node
        {
            get { return this.treeNode_0; }
            set { this.treeNode_0 = value; }
        }

        public TreeNode PreviousParent
        {
            get { return this.treeNode_1; }
            set { this.treeNode_1 = value; }
        }
    }
}