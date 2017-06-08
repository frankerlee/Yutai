using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class TreeViewAdvance : TreeView
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private bool bool_3 = true;
        private ImageList imageList_0 = null;
        protected ContextMenu opContextMenu = null;
        protected TreeNode opNode = null;
        private Timer timer_0 = null;
        private TreeNode treeNode_0 = null;
        private TreeNode treeNode_1 = null;

        public event AfterNodeAddedHandle AfterNodeAdded;

        public event AfterNodeDeletedHandle AfterNodeDeleted;

        public event AfterNodeDragDropHandle AfterNodeDragDrop;

        public event BeforeNodeAddedHandle BeforeNodeAdded;

        public event BeforeNodeDeletedHandle BeforeNodeDeleted;

        public event BeforeSubNodesDeletedHandle BeforeSubNodesDeleted;

        public TreeViewAdvance()
        {
            this.method_0();
        }

        public ArrayList GetLeafNodes(bool bool_4)
        {
            ArrayList list = new ArrayList();
            foreach (TreeNode node in base.Nodes)
            {
                this.method_1(node, bool_4, list);
            }
            return list;
        }

        private void method_0()
        {
            if (!base.DesignMode)
            {
                this.method_2();
                this.method_4();
                this.method_12();
            }
        }

        private void method_1(TreeNode treeNode_2, bool bool_4, ArrayList arrayList_0)
        {
            if (treeNode_2.Nodes.Count == 0)
            {
                if (bool_4)
                {
                    if (treeNode_2.Checked)
                    {
                        arrayList_0.Add(treeNode_2);
                    }
                }
                else
                {
                    arrayList_0.Add(treeNode_2);
                }
            }
            else
            {
                foreach (TreeNode node in treeNode_2.Nodes)
                {
                    this.method_1(node, bool_4, arrayList_0);
                }
            }
        }

        private void method_10(object sender, EventArgs e)
        {
            Debug.Assert(this.opNode != null, "Operation node is null!");
            if (this.BeforeNodeDeleted != null)
            {
                this.BeforeNodeDeleted(this, new TreeViewEventArgs(this.opNode));
            }
            ((this.opNode.Parent != null) ? this.opNode.Parent.Nodes : base.Nodes).Remove(this.opNode);
            if (this.AfterNodeDeleted != null)
            {
                this.AfterNodeDeleted(this, new TreeViewEventArgs(this.opNode));
            }
            this.opNode = null;
        }

        private void method_11(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode("节点 " + base.GetNodeCount(true).ToString());
            if (this.BeforeNodeAdded != null)
            {
                this.BeforeNodeAdded(this, new TreeViewEventArgs(node));
            }
            base.Nodes.Add(node);
            if (this.AfterNodeAdded != null)
            {
                this.AfterNodeAdded(this, new TreeViewEventArgs(node));
            }
        }

        private void method_12()
        {
            base.AfterCheck += new TreeViewEventHandler(this.TreeViewAdvance_AfterCheck);
        }

        private void method_13(TreeNode treeNode_2)
        {
            bool flag = false;
            while (treeNode_2.Parent != null)
            {
                if (treeNode_2.Checked)
                {
                    goto Label_005F;
                }
                TreeNode nextNode = treeNode_2.NextNode;
                while (nextNode != null)
                {
                    if (nextNode.Checked)
                    {
                        goto Label_0032;
                    }
                    nextNode = nextNode.NextNode;
                }
                goto Label_0034;
            Label_0032:
                flag = true;
            Label_0034:
                if (!flag)
                {
                    for (nextNode = treeNode_2.PrevNode; nextNode != null; nextNode = nextNode.PrevNode)
                    {
                        if (nextNode.Checked)
                        {
                            goto Label_005D;
                        }
                    }
                }
                goto Label_005F;
            Label_005D:
                flag = true;
            Label_005F:
                if (flag)
                {
                    break;
                }
                treeNode_2.Parent.Checked = treeNode_2.Checked;
                treeNode_2 = treeNode_2.Parent;
            }
        }

        private void method_14(TreeNode treeNode_2)
        {
            for (int i = 0; i < treeNode_2.Nodes.Count; i++)
            {
                treeNode_2.Nodes[i].Checked = treeNode_2.Checked;
                this.method_14(treeNode_2.Nodes[i]);
            }
        }

        private void method_2()
        {
            this.timer_0 = new Timer();
            this.timer_0.Interval = 200;
            this.imageList_0 = new ImageList();
            this.imageList_0.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.TransparentColor = Color.Transparent;
            this.method_3();
        }

        private void method_3()
        {
            base.DragDrop += new DragEventHandler(this.TreeViewAdvance_DragDrop);
            base.DragOver += new DragEventHandler(this.TreeViewAdvance_DragOver);
            base.DragLeave += new EventHandler(this.TreeViewAdvance_DragLeave);
            base.GiveFeedback += new GiveFeedbackEventHandler(this.TreeViewAdvance_GiveFeedback);
            base.DragEnter += new DragEventHandler(this.TreeViewAdvance_DragEnter);
            base.ItemDrag += new ItemDragEventHandler(this.TreeViewAdvance_ItemDrag);
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
        }

        private void method_4()
        {
            this.opContextMenu = new ContextMenu();
            this.opContextMenu.MenuItems.Add("展开", new EventHandler(this.method_5));
            this.opContextMenu.MenuItems.Add("折叠", new EventHandler(this.method_6));
            this.opContextMenu.MenuItems.Add("添加", new EventHandler(this.method_9));
            this.opContextMenu.MenuItems.Add("删除", new EventHandler(this.method_10));
            this.opContextMenu.MenuItems.Add("添加子节点", new EventHandler(this.method_7));
            this.opContextMenu.MenuItems.Add("删除子节点", new EventHandler(this.method_8));
            base.MouseDown += new MouseEventHandler(this.TreeViewAdvance_MouseDown);
            base.KeyDown += new KeyEventHandler(this.TreeViewAdvance_KeyDown);
        }

        private void method_5(object sender, EventArgs e)
        {
            Debug.Assert(this.opNode != null, "Operation node is null!");
            this.opNode.ExpandAll();
        }

        private void method_6(object sender, EventArgs e)
        {
            Debug.Assert(this.opNode != null, "Operation node is null!");
            this.opNode.Collapse();
        }

        private void method_7(object sender, EventArgs e)
        {
            Debug.Assert(this.opNode != null, "Operation node is null!");
            TreeNode node = new TreeNode("节点 " + base.GetNodeCount(true).ToString());
            if (this.BeforeNodeAdded != null)
            {
                this.BeforeNodeAdded(this, new TreeViewEventArgs(node));
            }
            this.opNode.Nodes.Insert(0, node);
            this.opNode.Expand();
            if (this.AfterNodeAdded != null)
            {
                this.AfterNodeAdded(this, new TreeViewEventArgs(node));
            }
        }

        private void method_8(object sender, EventArgs e)
        {
            Debug.Assert(this.opNode != null, "Operation node is null!");
            if (this.BeforeSubNodesDeleted != null)
            {
                this.BeforeSubNodesDeleted(this, new TreeViewChildrenDelEventArgs(this.opNode, this.opNode.Nodes));
            }
            this.opNode.Nodes.Clear();
        }

        private void method_9(object sender, EventArgs e)
        {
            Debug.Assert(this.opNode != null, "Operation node is null!");
            TreeNode node = new TreeNode("节点 " + base.GetNodeCount(true).ToString());
            if (this.BeforeNodeAdded != null)
            {
                this.BeforeNodeAdded(this, new TreeViewEventArgs(node));
            }
            ((this.opNode.Parent != null) ? this.opNode.Parent.Nodes : base.Nodes).Insert(this.opNode.Index + 1, node);
            if (this.AfterNodeAdded != null)
            {
                this.AfterNodeAdded(this, new TreeViewEventArgs(node));
            }
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            Point pt = base.PointToClient(Control.MousePosition);
            TreeNode nodeAt = base.GetNodeAt(pt);
            if (nodeAt != null)
            {
                if (pt.Y < 30)
                {
                    if (nodeAt.PrevVisibleNode != null)
                    {
                        nodeAt = nodeAt.PrevVisibleNode;
                        DragHelper.ImageList_DragShowNolock(false);
                        nodeAt.EnsureVisible();
                        this.Refresh();
                        DragHelper.ImageList_DragShowNolock(true);
                    }
                }
                else if ((pt.Y > (base.Size.Height - 30)) && (nodeAt.NextVisibleNode != null))
                {
                    nodeAt = nodeAt.NextVisibleNode;
                    DragHelper.ImageList_DragShowNolock(false);
                    nodeAt.EnsureVisible();
                    this.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        private void TreeViewAdvance_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Debug.Assert(base.CheckBoxes, "Item checkBoxes is unallowed!");
            if (this.RelationCheckBoxs && this.bool_3)
            {
                try
                {
                    this.bool_3 = false;
                    this.method_13(e.Node);
                    this.method_14(e.Node);
                }
                finally
                {
                    this.bool_3 = true;
                }
            }
        }

        private void TreeViewAdvance_DragDrop(object sender, DragEventArgs e)
        {
            DragHelper.ImageList_DragLeave(base.Handle);
            TreeNode nodeAt = base.GetNodeAt(base.PointToClient(new Point(e.X, e.Y)));
            if (this.treeNode_0 != nodeAt)
            {
                TreeViewDragDropEventArgs args = new TreeViewDragDropEventArgs {
                    Node = this.treeNode_0,
                    PreviousParent = this.treeNode_0.Parent
                };
                if (this.treeNode_0.Parent == null)
                {
                    base.Nodes.Remove(this.treeNode_0);
                }
                else
                {
                    this.treeNode_0.Parent.Nodes.Remove(this.treeNode_0);
                }
                nodeAt.Nodes.Add(this.treeNode_0);
                nodeAt.Expand();
                base.SelectedNode = this.treeNode_0;
                this.treeNode_0 = null;
                this.timer_0.Enabled = false;
                if (this.AfterNodeDragDrop != null)
                {
                    this.AfterNodeDragDrop(this, args);
                }
            }
        }

        private void TreeViewAdvance_DragEnter(object sender, DragEventArgs e)
        {
            DragHelper.ImageList_DragEnter(base.Handle, e.X - base.Left, e.Y - base.Top);
            this.timer_0.Enabled = true;
        }

        private void TreeViewAdvance_DragLeave(object sender, EventArgs e)
        {
            DragHelper.ImageList_DragLeave(base.Handle);
            this.timer_0.Enabled = false;
        }

        private void TreeViewAdvance_DragOver(object sender, DragEventArgs e)
        {
            Point point = base.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(point.X - base.Left, point.Y - base.Top);
            TreeNode nodeAt = base.GetNodeAt(base.PointToClient(new Point(e.X, e.Y)));
            if (nodeAt == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
                if (this.treeNode_1 != nodeAt)
                {
                    DragHelper.ImageList_DragShowNolock(false);
                    base.SelectedNode = nodeAt;
                    DragHelper.ImageList_DragShowNolock(true);
                    this.treeNode_1 = nodeAt;
                }
                for (TreeNode node2 = nodeAt; node2.Parent != null; node2 = node2.Parent)
                {
                    if (node2.Parent == this.treeNode_0)
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
        }

        private void TreeViewAdvance_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;
                this.Cursor = Cursors.Default;
            }
            else
            {
                e.UseDefaultCursors = true;
            }
        }

        private void TreeViewAdvance_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.treeNode_0 = (TreeNode) e.Item;
            base.SelectedNode = this.treeNode_0;
            this.imageList_0.Images.Clear();
            this.imageList_0.ImageSize = new Size(this.treeNode_0.Bounds.Size.Width + base.Indent, this.treeNode_0.Bounds.Height);
            Bitmap image = new Bitmap(this.treeNode_0.Bounds.Width + base.Indent, this.treeNode_0.Bounds.Height);
            Graphics graphics = Graphics.FromImage(image);
            if (base.ImageList != null)
            {
                int num = (this.treeNode_0.ImageIndex > -1) ? this.treeNode_0.ImageIndex : base.ImageIndex;
                if ((num > -1) && (num < base.ImageList.Images.Count))
                {
                    graphics.DrawImage(base.ImageList.Images[num], 0, 0);
                }
            }
            graphics.DrawString(this.treeNode_0.Text, this.Font, new SolidBrush(this.ForeColor), (float) base.Indent, 1f);
            this.imageList_0.Images.Add(image);
            Point point = base.PointToClient(Control.MousePosition);
            int num2 = ((point.X + base.Indent) - this.treeNode_0.Bounds.Left) - base.Left;
            int num3 = (point.Y - this.treeNode_0.Bounds.Top) - base.Top;
            if (DragHelper.ImageList_BeginDrag(this.imageList_0.Handle, 0, num2, num3))
            {
                base.DoDragDrop(image, DragDropEffects.Move);
                DragHelper.ImageList_EndDrag();
            }
        }

        private void TreeViewAdvance_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.bool_0 && (e.Control && (base.SelectedNode != null)))
            {
                int index;
                TreeNode selectedNode = base.SelectedNode;
                TreeNodeCollection nodes = (base.SelectedNode.Parent == null) ? base.Nodes : base.SelectedNode.Parent.Nodes;
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        if (selectedNode.Parent != null)
                        {
                            TreeNode parent = selectedNode.Parent;
                            nodes.Remove(selectedNode);
                            ((parent.Parent == null) ? base.Nodes : parent.Parent.Nodes).Insert(parent.Index + 1, selectedNode);
                        }
                        break;

                    case Keys.Up:
                        index = selectedNode.Index;
                        if (index > 0)
                        {
                            nodes.Remove(selectedNode);
                            nodes.Insert(index - 1, selectedNode);
                        }
                        break;

                    case Keys.Right:
                        if (selectedNode.PrevNode != null)
                        {
                            TreeNode prevNode = selectedNode.PrevNode;
                            nodes.Remove(selectedNode);
                            prevNode.Nodes.Add(selectedNode);
                        }
                        break;

                    case Keys.Down:
                        index = selectedNode.Index;
                        if (index < (nodes.Count - 1))
                        {
                            nodes.Remove(selectedNode);
                            nodes.Insert(index + 1, selectedNode);
                        }
                        break;
                }
                base.SelectedNode = selectedNode;
            }
        }

        private void TreeViewAdvance_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.bool_1 && (e.Button == MouseButtons.Right))
            {
                if (base.GetNodeCount(true) > 0)
                {
                    this.opNode = base.GetNodeAt(new Point(e.X, e.Y));
                    if (this.opNode != null)
                    {
                        base.SelectedNode = this.opNode;
                        this.opContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                }
                else
                {
                    ContextMenu menu = new ContextMenu();
                    menu.MenuItems.Add("添加节点", new EventHandler(this.method_11));
                    menu.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        public bool AllowReorder
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public bool RelationCheckBoxs
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool ShowPopupMenu
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public delegate void AfterNodeAddedHandle(object sender, TreeViewEventArgs e);

        public delegate void AfterNodeDeletedHandle(object sender, TreeViewEventArgs e);

        public delegate void AfterNodeDragDropHandle(object sender, TreeViewDragDropEventArgs e);

        public delegate void BeforeNodeAddedHandle(object sender, TreeViewEventArgs e);

        public delegate void BeforeNodeDeletedHandle(object sender, TreeViewEventArgs e);

        public delegate void BeforeSubNodesDeletedHandle(object sender, TreeViewChildrenDelEventArgs e);
    }
}

