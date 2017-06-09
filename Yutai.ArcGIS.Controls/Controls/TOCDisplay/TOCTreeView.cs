using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    [ToolboxBitmap(typeof(TOCTreeView), "TocTreeView.bmp"), ToolboxItem(true)]
    public class TOCTreeView : TocTreeViewBase
    {
        private License _license = null;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private IContainer components = null;
        private bool m_bDrag = false;
        private bool m_CanDrag = true;
        private bool m_CanEditStyle = true;
        private object m_hook = null;
        private int m_Indent = 0x10;
        private Point m_MarkerPoint = new Point(-10, -10);
        private TOCTreeNode m_pFirstHitNode = null;
        private TOCTreeNode m_pLastHitNode = null;
        private TOCTreeNode m_pLHitNode = null;
        private TOCTreeNode m_pSelectedNode = null;
        private IStyleGallery m_pSG = null;
        private bool m_ShowLines = false;
        private bool m_ShowPlusMinus = true;
        private bool m_ShowRootLines = false;
        private PopupMenu popupMenu1;

        public event AfterSelectEventHandler AfterSelect;

        public event LayerVisibleChangedEventHandler LayerVisibleChanged;

        public event NodeReorderedEventHandler NodeReordered;

        public event NodeReorderingEventHandler NodeReordering;

        public TOCTreeView()
        {
            this.InitializeComponent();
            base.m_pNodes.Owner = this;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
        }

        private void AddToSelection(TOCTreeNode pSelNode)
        {
            if (base.m_pSelectedNodes.IndexOf(pSelNode) == -1)
            {
                pSelNode.IsSelected = true;
                base.m_pSelectedNodes.InternalAdd(pSelNode);
            }
        }

        public void Calculate()
        {
            Point autoScrollPosition = base.AutoScrollPosition;
            int x = autoScrollPosition.X;
            int y = autoScrollPosition.Y;
            try
            {
                Graphics pGraphics = base.CreateGraphics();
                for (int i = 0; i < base.m_pNodes.Count; i++)
                {
                    (base.m_pNodes[i] as TOCTreeNode).CalculateBounds(pGraphics, ref x, ref y, this.m_Indent);
                }
                pGraphics.Dispose();
            }
            catch
            {
            }
        }

        private void ChageNode(TOCTreeNode pFirstNode, TOCTreeNode pLastNode)
        {
            TOCTreeNode parent = pFirstNode.Parent;
            TOCTreeNode pLastOldParent = pLastNode.Parent;
            if (pLastOldParent != pFirstNode)
            {
                int index;
                NodeType nodeType = pFirstNode.GetNodeType();
                NodeType type2 = pLastNode.GetNodeType();
                if (nodeType == type2)
                {
                    if (this.NodeReordering != null)
                    {
                        this.NodeReordering(pFirstNode, pLastNode);
                    }
                    if (((nodeType == NodeType.MapFrame) || (nodeType == NodeType.Map)) || (nodeType == NodeType.Layer))
                    {
                        if (pFirstNode.Parent == null)
                        {
                            pFirstNode.TreeView.Nodes.Remove(pFirstNode);
                        }
                        else
                        {
                            pFirstNode.Parent.Nodes.Remove(pFirstNode);
                        }
                        if (pLastNode.Parent == null)
                        {
                            index = pLastNode.TreeView.Nodes.IndexOf(pLastNode);
                            if (pFirstNode.NodeRect.Top < pLastNode.NodeRect.Top)
                            {
                                index++;
                            }
                            if (index == -1)
                            {
                                index = 0;
                            }
                            if (pLastNode.TreeView.Nodes.Count == index)
                            {
                                pLastNode.TreeView.Nodes.Add(pFirstNode);
                            }
                            else
                            {
                                pLastNode.TreeView.Nodes.Insert(index, pFirstNode);
                            }
                        }
                        else
                        {
                            index = pLastNode.Parent.Nodes.IndexOf(pLastNode);
                            if (pFirstNode.NodeRect.Top < pLastNode.NodeRect.Top)
                            {
                                index++;
                            }
                            if (index == -1)
                            {
                                index = 0;
                            }
                            if (pLastNode.Parent.Nodes.Count == index)
                            {
                                pLastNode.Parent.Nodes.Add(pFirstNode);
                            }
                            else
                            {
                                pLastNode.Parent.Nodes.Insert(index, pFirstNode);
                            }
                        }
                    }
                    else
                    {
                        switch (nodeType)
                        {
                            case NodeType.GroupLayer:
                                pFirstNode.Parent.Nodes.Remove(pFirstNode);
                                if (pLastNode.Nodes.Count > 0)
                                {
                                    pLastNode.Nodes.Insert(0, pFirstNode);
                                }
                                else
                                {
                                    pLastNode.Nodes.Add(pFirstNode);
                                }
                                break;

                            case NodeType.Folder:
                                pFirstNode.Parent.Nodes.Remove(pFirstNode);
                                if (pLastNode.Nodes.Count > 0)
                                {
                                    pLastNode.Nodes.Insert(0, pFirstNode);
                                }
                                else
                                {
                                    pLastNode.Nodes.Add(pFirstNode);
                                }
                                break;
                        }
                    }
                }
                else if (((type2 == NodeType.MapFrame) || (type2 == NodeType.Map)) || (type2 == NodeType.GroupLayer))
                {
                    switch (nodeType)
                    {
                        case NodeType.GroupLayer:
                        case NodeType.Layer:
                            if (this.NodeReordering != null)
                            {
                                this.NodeReordering(pFirstNode, pLastNode);
                            }
                            pFirstNode.Parent.Nodes.Remove(pFirstNode);
                            if (pLastNode.Nodes.Count > 0)
                            {
                                pLastNode.Nodes.Insert(0, pFirstNode);
                            }
                            else
                            {
                                pLastNode.Nodes.Add(pFirstNode);
                            }
                            break;

                        case NodeType.Folder:
                            if ((type2 == NodeType.MapFrame) || (type2 == NodeType.Map))
                            {
                                if (this.NodeReordering != null)
                                {
                                    this.NodeReordering(pFirstNode, pLastNode);
                                }
                                pFirstNode.Parent.Nodes.Remove(pFirstNode);
                                if (pLastNode.Nodes.Count > 0)
                                {
                                    pLastNode.Nodes.Insert(0, pFirstNode);
                                }
                                else
                                {
                                    pLastNode.Nodes.Add(pFirstNode);
                                }
                            }
                            else
                            {
                                if (this.NodeReordering != null)
                                {
                                    this.NodeReordering(pFirstNode, pLastNode);
                                }
                                pFirstNode.Parent.Nodes.Remove(pFirstNode);
                                index = pLastNode.Parent.Nodes.IndexOf(pLastNode);
                                if (pFirstNode.NodeRect.Top < pLastNode.NodeRect.Top)
                                {
                                    index++;
                                }
                                if (index == -1)
                                {
                                    index = 0;
                                }
                                if (pLastNode.Parent.Nodes.Count == index)
                                {
                                    pLastNode.Parent.Nodes.Add(pFirstNode);
                                }
                                else
                                {
                                    pLastNode.Parent.Nodes.Insert(index, pFirstNode);
                                }
                            }
                            break;
                    }
                }
                else if (nodeType == NodeType.Folder)
                {
                    switch (type2)
                    {
                        case NodeType.Layer:
                        case NodeType.GroupLayer:
                            if (this.NodeReordering != null)
                            {
                                this.NodeReordering(pFirstNode, pLastNode);
                            }
                            pFirstNode.Parent.Nodes.Remove(pFirstNode);
                            index = pLastNode.Parent.Nodes.IndexOf(pLastNode);
                            if (pFirstNode.NodeRect.Top < pLastNode.NodeRect.Top)
                            {
                                index++;
                            }
                            if (index == -1)
                            {
                                index = 0;
                            }
                            if (pLastNode.Parent.Nodes.Count == index)
                            {
                                pLastNode.Parent.Nodes.Add(pFirstNode);
                            }
                            else
                            {
                                pLastNode.Parent.Nodes.Insert(index, pFirstNode);
                            }
                            break;
                    }
                }
                else if (nodeType == NodeType.Layer)
                {
                    if (type2 == NodeType.Folder)
                    {
                        if (this.NodeReordering != null)
                        {
                            this.NodeReordering(pFirstNode, pLastNode);
                        }
                        pFirstNode.Parent.Nodes.Remove(pFirstNode);
                        if (pLastNode.Nodes.Count > 0)
                        {
                            pLastNode.Nodes.Insert(0, pFirstNode);
                        }
                        else
                        {
                            pLastNode.Nodes.Add(pFirstNode);
                        }
                    }
                }
                else if ((nodeType == NodeType.GroupLayer) && (type2 == NodeType.Layer))
                {
                    if (this.NodeReordering != null)
                    {
                        this.NodeReordering(pFirstNode, pLastNode);
                    }
                    pFirstNode.Parent.Nodes.Remove(pFirstNode);
                    index = pLastNode.Parent.Nodes.IndexOf(pLastNode);
                    if (pFirstNode.NodeRect.Top < pLastNode.NodeRect.Top)
                    {
                        index++;
                    }
                    if (index == -1)
                    {
                        index = 0;
                    }
                    if (pLastNode.Parent.Nodes.Count == index)
                    {
                        pLastNode.Parent.Nodes.Add(pFirstNode);
                    }
                    else
                    {
                        pLastNode.Parent.Nodes.Insert(index, pFirstNode);
                    }
                }
                if (this.NodeReordered != null)
                {
                    this.NodeReordered(parent, pFirstNode, pLastOldParent, pLastNode);
                }
            }
        }

        public void ClearSelection()
        {
            for (int i = 0; i < base.m_pSelectedNodes.Count; i++)
            {
                TOCTreeNode node = base.m_pSelectedNodes[i] as TOCTreeNode;
                node.IsSelected = false;
            }
            this.m_pLHitNode = null;
            base.m_pSelectedNodes.Clear();
        }

        public void CollapseAll()
        {
            for (int i = 0; i < base.m_pNodes.Count; i++)
            {
                (base.m_pNodes[i] as TOCTreeNode).Collapse();
            }
            this.Calculate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Draw()
        {
            Graphics pGraphics = base.CreateGraphics();
            for (int i = 0; i < base.m_pNodes.Count; i++)
            {
                (base.m_pNodes[i] as TOCTreeNode).Draw(pGraphics);
            }
        }

        private void DrawMarker(Graphics pGraphics, Pen pPen, Point StartPt, Point EndPt)
        {
            pGraphics.DrawLine(pPen, StartPt, EndPt);
            pGraphics.DrawLine(pPen, StartPt.X, StartPt.Y + 3, StartPt.X, StartPt.Y - 6);
            pGraphics.DrawLine(pPen, EndPt.X, EndPt.Y + 3, EndPt.X, EndPt.Y - 6);
        }

        public void Expand()
        {
            for (int i = 0; i < base.m_pNodes.Count; i++)
            {
                (base.m_pNodes[i] as TOCTreeNode).Expand();
            }
            this.Calculate();
        }

        public void ExpandAll()
        {
            for (int i = 0; i < base.m_pNodes.Count; i++)
            {
                (base.m_pNodes[i] as TOCTreeNode).ExpandAll();
            }
            this.Calculate();
        }

        private TOCTreeNode FindMapNode(TOCTreeNode pChildNode)
        {
            if (pChildNode.Parent != null)
            {
                if ((pChildNode.Parent.GetNodeType() == NodeType.Map) || (pChildNode.Parent.GetNodeType() == NodeType.MapFrame))
                {
                    return pChildNode.Parent;
                }
                return this.FindMapNode(pChildNode.Parent);
            }
            return null;
        }

        [DllImport("user32")]
        public static extern int GetKeyState(int nVirtKey);
        public TOCTreeNode GetNodeAt(Point point)
        {
            return this.GetNodeAt(point.X, point.Y);
        }

        public TOCTreeNode GetNodeAt(int x, int y)
        {
            for (int i = 0; i < base.m_pNodes.Count; i++)
            {
                TOCTreeNode pParentNode = base.m_pNodes[i] as TOCTreeNode;
                if (pParentNode.IsVisible && pParentNode.Bounds.Contains(x, y))
                {
                    if (pParentNode.NodeRect.Contains(x, y))
                    {
                        return pParentNode;
                    }
                    if (pParentNode.IsExpanded)
                    {
                        return this.GetNodeAt(pParentNode, x, y);
                    }
                }
            }
            return null;
        }

        private TOCTreeNode GetNodeAt(TOCTreeNode pParentNode, int x, int y)
        {
            for (int i = 0; i < pParentNode.Nodes.Count; i++)
            {
                TOCTreeNode node = pParentNode.Nodes[i] as TOCTreeNode;
                if (node.IsVisible && node.Bounds.Contains(x, y))
                {
                    if (node.NodeRect.Contains(x, y))
                    {
                        return node;
                    }
                    if (node.IsExpanded)
                    {
                        return this.GetNodeAt(node, x, y);
                    }
                }
            }
            return null;
        }

        public int GetNodeCount(bool includeSubTrees)
        {
            int count = base.m_pNodes.Count;
            if (includeSubTrees)
            {
                for (int i = 0; i < base.m_pNodes.Count; i++)
                {
                    count += (base.m_pNodes[i] as TOCTreeNode).GetNodeCount(includeSubTrees);
                }
            }
            return count;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.barManager1 = new BarManager(this.components);
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.popupMenu1 = new PopupMenu(this.components);
            base.SuspendLayout();
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "TOCTreeView";
            base.DoubleClick += new EventHandler(this.TOCTreeView_DoubleClick);
            base.Load += new EventHandler(this.TOCTreeView_Load);
            base.MouseDown += new MouseEventHandler(this.TOCTreeView_MouseDown);
            base.MouseMove += new MouseEventHandler(this.TOCTreeView_MouseMove);
            base.Paint += new PaintEventHandler(this.TOCTreeView_Paint);
            base.MouseUp += new MouseEventHandler(this.TOCTreeView_MouseUp);
            base.ResumeLayout(false);
        }

        private void InsertToSelection(TOCTreeNode pSelNode)
        {
            if (base.m_pSelectedNodes.IndexOf(pSelNode) == -1)
            {
                TOCTreeNodeCollection nodes;
                int index = -1;
                if (pSelNode.Parent == null)
                {
                    nodes = base.Nodes;
                }
                else
                {
                    nodes = pSelNode.Parent.Nodes;
                }
                index = nodes.IndexOf(pSelNode);
                pSelNode.IsSelected = true;
                bool flag = false;
                for (int i = 0; i < base.m_pSelectedNodes.Count; i++)
                {
                    int num3 = nodes.IndexOf(base.m_pSelectedNodes[i]);
                    if (index < num3)
                    {
                        if (i == (base.m_pSelectedNodes.Count - 1))
                        {
                            base.m_pSelectedNodes.InternalAdd(pSelNode);
                        }
                        else
                        {
                            base.m_pSelectedNodes.InteralInsert(i, pSelNode);
                        }
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    base.m_pSelectedNodes.InternalAdd(pSelNode);
                }
            }
        }

        private bool IsCtrlDown()
        {
            int num = GetKeyState(0xa2) >> 4;
            if (num != 0)
            {
                return true;
            }
            num = GetKeyState(0xa3) >> 4;
            return (num != 0);
        }

        private bool IsShiftDown()
        {
            int num = GetKeyState(160) >> 4;
            if (num != 0)
            {
                return true;
            }
            num = GetKeyState(0xa1) >> 4;
            return (num != 0);
        }

        public void SelectNode(TOCTreeNode pSelectedNode)
        {
            if (base.m_pSelectedNodes.IndexOf(pSelectedNode) == -1)
            {
                pSelectedNode.IsSelected = true;
                base.m_pSelectedNodes.Add(pSelectedNode);
            }
        }

        public void SetScroll()
        {
            int width = 0;
            int height = 0;
            int num3 = 0;
            if (base.m_pNodes.Count > 0)
            {
                for (int i = 0; i < base.m_pNodes.Count; i++)
                {
                    Rectangle bounds = (base.m_pNodes[i] as TOCTreeNode).Bounds;
                    num3 = (num3 > bounds.Right) ? num3 : bounds.Right;
                }
                width = num3 - base.AutoScrollPosition.X;
            }
            else
            {
                width = base.ClientRectangle.Width;
            }
            if (base.m_pNodes.Count > 0)
            {
                height = (height + (base.m_pNodes[base.m_pNodes.Count - 1] as TOCTreeNode).Bounds.Bottom) - (base.m_pNodes[0] as TOCTreeNode).Bounds.Top;
                height += 10;
            }
            else
            {
                height = base.ClientRectangle.Height;
            }
            base.AutoScrollMinSize = new Size(width, height);
        }

        private void TOCTreeView_DoubleClick(object sender, EventArgs e)
        {
        }

        private void TOCTreeView_Load(object sender, EventArgs e)
        {
            base.VScroll = true;
            base.HScroll = true;
        }

        private void TOCTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int index;
                int num2;
                frmSymbolSelector selector;
                ISymbol tag;
                bool flag;
                Point point = new Point(e.X, e.Y);
                TOCTreeNode nodeAt = this.GetNodeAt(e.X, e.Y);
                if (nodeAt == null)
                {
                    this.ClearSelection();
                    if (this.AfterSelect != null)
                    {
                        this.AfterSelect(null);
                    }
                    base.Invalidate();
                    return;
                }
                if (((e.Button == MouseButtons.Right) && (base.m_pSelectedNodes.Count > 0)) && (base.m_pSelectedNodes.IndexOf(nodeAt) != -1))
                {
                    return;
                }
                if (base.m_pSelectedNodes.Count == 0)
                {
                    this.m_pLHitNode = nodeAt;
                    this.AddToSelection(nodeAt);
                    goto Label_02A9;
                }
                TOCTreeNode node2 = base.m_pSelectedNodes[0] as TOCTreeNode;
                if (((node2.TOCNodeType != nodeAt.TOCNodeType) && ((node2.TOCNodeType != NodeType.GroupLayer) || (nodeAt.TOCNodeType != NodeType.Layer))) && ((node2.TOCNodeType != NodeType.Layer) || (nodeAt.TOCNodeType != NodeType.GroupLayer)))
                {
                    goto Label_0290;
                }
                if (!this.IsShiftDown())
                {
                    goto Label_022E;
                }
                TOCTreeNode pLHitNode = this.m_pLHitNode;
                this.ClearSelection();
                if (pLHitNode.Parent != nodeAt.Parent)
                {
                    goto Label_0221;
                }
                if (node2.Parent == null)
                {
                    index = base.Nodes.IndexOf(pLHitNode);
                    num2 = base.Nodes.IndexOf(nodeAt);
                }
                else
                {
                    index = node2.Parent.Nodes.IndexOf(pLHitNode);
                    num2 = node2.Parent.Nodes.IndexOf(nodeAt);
                }
                TOCTreeNode pSelNode = pLHitNode;
                TOCTreeNode node5 = nodeAt;
                if (index > num2)
                {
                    pSelNode = nodeAt;
                    node5 = pLHitNode;
                }
                goto Label_0210;
            Label_01EB:
                this.AddToSelection(pSelNode);
                pSelNode = pSelNode.NextNode;
                if (pSelNode == node5)
                {
                    goto Label_0215;
                }
            Label_0210:
                flag = true;
                goto Label_01EB;
            Label_0215:
                this.AddToSelection(node5);
                goto Label_02A1;
            Label_0221:
                this.AddToSelection(nodeAt);
                goto Label_02A1;
            Label_022E:
                if (this.IsCtrlDown())
                {
                    if (base.m_pSelectedNodes.IndexOf(nodeAt) == -1)
                    {
                        this.InsertToSelection(nodeAt);
                    }
                    else
                    {
                        nodeAt.IsSelected = false;
                        base.m_pSelectedNodes.Remove(nodeAt);
                    }
                }
                else
                {
                    this.ClearSelection();
                    this.AddToSelection(nodeAt);
                }
                goto Label_02A1;
            Label_0290:
                this.ClearSelection();
                this.AddToSelection(nodeAt);
            Label_02A1:
                this.m_pLHitNode = nodeAt;
            Label_02A9:
                if (base.m_pSelectedNodes.Count != 1)
                {
                    goto Label_096E;
                }
                this.m_pSelectedNode = base.m_pSelectedNodes[0] as TOCTreeNode;
                if (this.AfterSelect != null)
                {
                    this.AfterSelect(this.m_pSelectedNode);
                }
                if (e.Button == MouseButtons.Right)
                {
                    base.Invalidate();
                }
                else
                {
                    switch (this.m_pSelectedNode.HitTest(e.X, e.Y))
                    {
                        case HitType.Expand:
                            if (!this.m_pSelectedNode.IsExpanded)
                            {
                                goto Label_037E;
                            }
                            this.m_pSelectedNode.Collapse();
                            goto Label_038C;

                        case HitType.Check:
                            goto Label_03C9;

                        case HitType.Image:
                            if (!this.m_CanEditStyle)
                            {
                                break;
                            }
                            if (!(this.m_pSelectedNode.Tag is ISymbol))
                            {
                                goto Label_0727;
                            }
                            try
                            {
                                selector = new frmSymbolSelector();
                                if (selector == null)
                                {
                                    break;
                                }
                                selector.SetStyleGallery(this.m_pSG);
                                tag = this.m_pSelectedNode.Tag as ISymbol;
                                selector.SetSymbol(tag);
                                if (selector.ShowDialog() == DialogResult.OK)
                                {
                                    this.m_pSelectedNode.Tag = selector.GetSymbol();
                                    node2 = this.FindMapNode(this.m_pSelectedNode);
                                    if (node2 != null)
                                    {
                                        if (node2.Tag is IBasicMap)
                                        {
                                            TreeViewEvent.OnLayerPropertyChanged(this, node2.Tag as IBasicMap, this.m_pSelectedNode.Parent.Tag as ILayer);
                                            (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, this.m_pSelectedNode.Parent.Tag, null);
                                        }
                                        else if (node2.Tag is IMapFrame)
                                        {
                                            TreeViewEvent.OnLayerPropertyChanged(this, (node2.Tag as IMapFrame).Map as IBasicMap, this.m_pSelectedNode.Parent.Tag as ILayer);
                                            ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, this.m_pSelectedNode.Parent.Tag, null);
                                        }
                                        DocumentManager.DocumentChanged(this.m_hook);
                                    }
                                }
                            }
                            catch
                            {
                            }
                            goto Label_08D9;

                        case HitType.Text:
                            goto Label_0914;
                    }
                }
                return;
            Label_037E:
                this.m_pSelectedNode.Expand();
            Label_038C:
                this.Calculate();
                this.SetScroll();
                base.Invalidate();
                if (ApplicationRef.Application.ActiveControl != null)
                {
                    ApplicationRef.Application.ActiveControl.Invalidate();
                }
                return;
            Label_03C9:
                this.m_pSelectedNode.Checked = !this.m_pSelectedNode.Checked;
                base.Invalidate(this.m_pSelectedNode.Bounds);
                if (ApplicationRef.Application.ActiveControl != null)
                {
                    ApplicationRef.Application.ActiveControl.Invalidate();
                }
                if (this.m_pSelectedNode.Tag is ILayer)
                {
                    (this.m_pSelectedNode.Tag as ILayer).Visible = this.m_pSelectedNode.Checked;
                    node2 = this.FindMapNode(this.m_pSelectedNode);
                    if (node2 != null)
                    {
                        if (node2.Tag is IBasicMap)
                        {
                            TreeViewEvent.OnLayerVisibleChanged(this, node2.Tag as IBasicMap, this.m_pSelectedNode.Tag as ILayer);
                            (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, this.m_pSelectedNode.Tag, null);
                        }
                        else if (node2.Tag is IMapFrame)
                        {
                            TreeViewEvent.OnLayerVisibleChanged(this, (node2.Tag as IMapFrame).Map as IBasicMap, this.m_pSelectedNode.Tag as ILayer);
                            ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, this.m_pSelectedNode.Tag, null);
                        }
                        DocumentManager.DocumentChanged(this.m_hook);
                    }
                }
                if (this.LayerVisibleChanged != null)
                {
                    this.LayerVisibleChanged(this.m_pSelectedNode);
                }
                return;
            Label_0727:
                if (this.m_pSelectedNode.Tag is ILegendClass)
                {
                    try
                    {
                        selector = new frmSymbolSelector();
                        if (selector == null)
                        {
                            return;
                        }
                        selector.SetStyleGallery(this.m_pSG);
                        tag = (this.m_pSelectedNode.Tag as ILegendClass).Symbol;
                        selector.SetSymbol(tag);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            (this.m_pSelectedNode.Tag as ILegendClass).Symbol = selector.GetSymbol() as ISymbol;
                            node2 = this.FindMapNode(this.m_pSelectedNode);
                            if (node2 != null)
                            {
                                if (node2.Tag is IBasicMap)
                                {
                                    TreeViewEvent.OnLayerPropertyChanged(this, node2.Tag as IBasicMap, this.m_pSelectedNode.Parent.Tag as ILayer);
                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, this.m_pSelectedNode.Parent.Tag, null);
                                }
                                else if (node2.Tag is IMapFrame)
                                {
                                    TreeViewEvent.OnLayerPropertyChanged(this, (node2.Tag as IMapFrame).Map as IBasicMap, this.m_pSelectedNode.Parent.Tag as ILayer);
                                    ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, this.m_pSelectedNode.Parent.Tag, null);
                                }
                                DocumentManager.DocumentChanged(this.m_hook);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            Label_08D9:
                this.Calculate();
                this.SetScroll();
                base.Invalidate();
                if (ApplicationRef.Application.ActiveControl != null)
                {
                    ApplicationRef.Application.ActiveControl.Invalidate();
                }
                return;
            Label_0914:
                if (this.m_CanDrag)
                {
                    switch (this.m_pSelectedNode.GetNodeType())
                    {
                        case NodeType.MapFrame:
                        case NodeType.Layer:
                        case NodeType.GroupLayer:
                        case NodeType.Folder:
                            this.m_pFirstHitNode = this.m_pSelectedNode;
                            break;
                    }
                }
                base.Invalidate();
                return;
            Label_096E:
                if (this.AfterSelect != null)
                {
                    this.AfterSelect(null);
                }
            }
            catch (Exception)
            {
            }
        }

        private void TOCTreeView_MouseMove(object sender, MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) && this.m_CanDrag) && (this.m_pFirstHitNode != null))
            {
                this.m_bDrag = true;
            }
            else
            {
                return;
            }
            Point point = new Point(e.X, e.Y);
            TOCTreeNode nodeAt = this.GetNodeAt(e.X, e.Y);
            Point startPt = new Point(0, 0);
            Point endPt = new Point(0, 0);
            Point point4 = new Point(0, 0);
            Pen pPen = new Pen(Color.White, 3f);
            Pen pen2 = new Pen(Color.Black, 3f);
            if ((nodeAt == null) || (nodeAt == this.m_pFirstHitNode))
            {
                if (this.m_MarkerPoint.Y > 0)
                {
                    startPt = new Point(0, this.m_MarkerPoint.Y);
                    endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                    this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                }
                this.m_pLastHitNode = nodeAt;
            }
            else
            {
                NodeType nodeType = nodeAt.GetNodeType();
                switch (nodeType)
                {
                    case NodeType.Map:
                    case NodeType.MapFrame:
                    case NodeType.Folder:
                    case NodeType.GroupLayer:
                    {
                        this.m_pLastHitNode = nodeAt;
                        Rectangle nodeRect = nodeAt.NodeRect;
                        if (nodeRect.Bottom != this.m_MarkerPoint.Y)
                        {
                            startPt = new Point(0, this.m_MarkerPoint.Y);
                            endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                            this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                            this.m_MarkerPoint.X = nodeRect.Left;
                            this.m_MarkerPoint.Y = nodeRect.Bottom;
                            startPt.X = 0;
                            startPt.Y = this.m_MarkerPoint.Y;
                            endPt.X = base.ClientRectangle.Width;
                            endPt.Y = this.m_MarkerPoint.Y;
                            this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                        }
                        return;
                    }
                    case NodeType.Symbol:
                    case NodeType.LegendClass:
                        switch (this.m_pFirstHitNode.GetNodeType())
                        {
                            case NodeType.Layer:
                            case NodeType.GroupLayer:
                            {
                                TOCTreeNode parent = nodeAt.Parent;
                                if ((parent != this.m_pFirstHitNode) && (parent != null))
                                {
                                    this.m_pLastHitNode = parent;
                                    if (this.m_pFirstHitNode.NodeRect.Bottom < this.m_pLastHitNode.NodeRect.Bottom)
                                    {
                                        if (this.m_pLastHitNode.NextNode != null)
                                        {
                                            point4 = new Point(parent.NextNode.NodeRect.Left, parent.NextNode.NodeRect.Top);
                                            if (point4.Y != this.m_MarkerPoint.Y)
                                            {
                                                startPt = new Point(0, this.m_MarkerPoint.Y);
                                                endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                                                this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                                                if (parent.NextNode != this.m_pFirstHitNode)
                                                {
                                                    this.m_MarkerPoint = point4;
                                                    startPt.X = 0;
                                                    startPt.Y = this.m_MarkerPoint.Y;
                                                    endPt.X = base.ClientRectangle.Width;
                                                    endPt.Y = this.m_MarkerPoint.Y;
                                                    this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (parent.Parent != null)
                                            {
                                                if (parent.Parent.NextNode != null)
                                                {
                                                    point4 = new Point(parent.Parent.NextNode.NodeRect.Left, parent.Parent.NextNode.NodeRect.Top);
                                                }
                                                else
                                                {
                                                    point4 = new Point(this.Bounds.Left, this.Bounds.Bottom);
                                                }
                                            }
                                            if (point4.Y != this.m_MarkerPoint.Y)
                                            {
                                                startPt = new Point(0, this.m_MarkerPoint.Y);
                                                endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                                                this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                                                if (parent == this.m_pFirstHitNode)
                                                {
                                                    this.m_MarkerPoint.X = -10;
                                                    this.m_MarkerPoint.Y = -10;
                                                }
                                                else
                                                {
                                                    this.m_MarkerPoint = point4;
                                                    startPt.X = 0;
                                                    startPt.Y = this.m_MarkerPoint.Y;
                                                    endPt.X = base.ClientRectangle.Width;
                                                    endPt.Y = this.m_MarkerPoint.Y;
                                                    this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        point4 = new Point(this.m_pLastHitNode.NodeRect.Left, this.m_pLastHitNode.NodeRect.Top);
                                        if (point4.Y != this.m_MarkerPoint.Y)
                                        {
                                            startPt = new Point(0, this.m_MarkerPoint.Y);
                                            endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                                            this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                                            if (parent.PrevNode != this.m_pFirstHitNode)
                                            {
                                                this.m_MarkerPoint = point4;
                                                startPt.X = 0;
                                                startPt.Y = this.m_MarkerPoint.Y;
                                                endPt.X = base.ClientRectangle.Width;
                                                endPt.Y = this.m_MarkerPoint.Y;
                                                this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        return;
                }
                if (nodeType == NodeType.Layer)
                {
                    switch (this.m_pFirstHitNode.GetNodeType())
                    {
                        case NodeType.Layer:
                        case NodeType.GroupLayer:
                        case NodeType.Folder:
                            this.m_pLastHitNode = nodeAt;
                            if (this.m_pFirstHitNode.NodeRect.Bottom < this.m_pLastHitNode.NodeRect.Bottom)
                            {
                                if (nodeAt.NextNode != null)
                                {
                                    point4 = new Point(nodeAt.NextNode.NodeRect.Left, nodeAt.NextNode.NodeRect.Top);
                                    if (point4.Y != this.m_MarkerPoint.Y)
                                    {
                                        startPt = new Point(0, this.m_MarkerPoint.Y);
                                        endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                                        this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                                        if (nodeAt == this.m_pFirstHitNode)
                                        {
                                            this.m_MarkerPoint.X = -10;
                                            this.m_MarkerPoint.Y = -10;
                                        }
                                        else
                                        {
                                            this.m_MarkerPoint = point4;
                                            startPt.X = 0;
                                            startPt.Y = this.m_MarkerPoint.Y;
                                            endPt.X = base.ClientRectangle.Width;
                                            endPt.Y = this.m_MarkerPoint.Y;
                                            this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                                        }
                                    }
                                }
                                else
                                {
                                    if (nodeAt.Parent != null)
                                    {
                                        if (nodeAt.Parent.NextNode != null)
                                        {
                                            point4 = new Point(nodeAt.Parent.NextNode.NodeRect.Left, nodeAt.Parent.NextNode.NodeRect.Top);
                                        }
                                        else
                                        {
                                            point4 = new Point(this.Bounds.Right, this.Bounds.Bottom);
                                        }
                                    }
                                    if (point4.Y != this.m_MarkerPoint.Y)
                                    {
                                        startPt = new Point(0, this.m_MarkerPoint.Y);
                                        endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                                        this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                                        if (nodeAt == this.m_pFirstHitNode)
                                        {
                                            this.m_MarkerPoint.X = -10;
                                            this.m_MarkerPoint.Y = -10;
                                        }
                                        else
                                        {
                                            this.m_MarkerPoint = point4;
                                            startPt.X = 0;
                                            startPt.Y = this.m_MarkerPoint.Y;
                                            endPt.X = base.ClientRectangle.Width;
                                            endPt.Y = this.m_MarkerPoint.Y;
                                            this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                point4 = new Point(nodeAt.NodeRect.Left, nodeAt.NodeRect.Top);
                                if (point4.Y != this.m_MarkerPoint.Y)
                                {
                                    startPt = new Point(0, this.m_MarkerPoint.Y);
                                    endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                                    this.DrawMarker(base.CreateGraphics(), pPen, startPt, endPt);
                                    if (nodeAt == this.m_pFirstHitNode)
                                    {
                                        this.m_MarkerPoint.X = -10;
                                        this.m_MarkerPoint.Y = -10;
                                    }
                                    else
                                    {
                                        this.m_MarkerPoint = point4;
                                        startPt.X = 0;
                                        startPt.Y = this.m_MarkerPoint.Y;
                                        endPt.X = base.ClientRectangle.Width;
                                        endPt.Y = this.m_MarkerPoint.Y;
                                        this.DrawMarker(base.CreateGraphics(), pen2, startPt, endPt);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void TOCTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.m_pLastHitNode = null;
                this.m_pFirstHitNode = null;
            }
            else
            {
                if (this.m_bDrag)
                {
                    Point startPt = new Point(0, this.m_MarkerPoint.Y);
                    Point endPt = new Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
                    this.DrawMarker(base.CreateGraphics(), new Pen(Color.White, 3f), startPt, endPt);
                    this.m_MarkerPoint.X = -10;
                    this.m_MarkerPoint.Y = -10;
                    if (((this.m_pFirstHitNode != null) && (this.m_pLastHitNode != null)) && (this.m_pLastHitNode != this.m_pFirstHitNode))
                    {
                        this.ChageNode(this.m_pFirstHitNode, this.m_pLastHitNode);
                        this.Calculate();
                        this.SetScroll();
                        base.Invalidate();
                    }
                    this.m_bDrag = false;
                }
                this.m_pLastHitNode = null;
                this.m_pFirstHitNode = null;
            }
        }

        private void TOCTreeView_Paint(object sender, PaintEventArgs e)
        {
            this.Calculate();
            this.Draw();
        }

        public override string ToString()
        {
            return "TOCTreeView";
        }

        public object BarManager
        {
            get
            {
                return this.barManager1;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                int height;
                int x = base.AutoScrollPosition.X;
                int y = base.AutoScrollPosition.Y;
                int width = base.ClientRectangle.Width;
                if (base.m_pNodes.Count > 1)
                {
                    height = (base.m_pNodes[base.m_pNodes.Count - 1] as TOCTreeNode).Bounds.Bottom - (base.m_pNodes[0] as TOCTreeNode).Bounds.Top;
                }
                else
                {
                    height = (base.m_pNodes[0] as TOCTreeNode).Bounds.Height;
                }
                return new Rectangle(x, y, width, height + 10);
            }
        }

        public bool CanDrag
        {
            get
            {
                return this.m_CanDrag;
            }
            set
            {
                this.m_CanDrag = value;
            }
        }

        public bool CanEditStyle
        {
            get
            {
                return this.m_CanEditStyle;
            }
            set
            {
                this.m_CanEditStyle = value;
            }
        }

        internal object Hook
        {
            set
            {
                this.m_hook = value;
            }
        }

        public int Indent
        {
            get
            {
                return this.m_Indent;
            }
            set
            {
                this.m_Indent = value;
            }
        }

        public object PopupMenu
        {
            get
            {
                return this.popupMenu1;
            }
        }

        public TOCTreeNode SelectedNode
        {
            get
            {
                return this.m_pSelectedNode;
            }
            set
            {
                this.m_pSelectedNode = value;
            }
        }

        public bool ShowLines
        {
            get
            {
                return this.m_ShowLines;
            }
            set
            {
                this.m_ShowLines = value;
            }
        }

        public bool ShowPlusMinus
        {
            get
            {
                return this.m_ShowPlusMinus;
            }
            set
            {
                this.m_ShowPlusMinus = value;
            }
        }

        public bool ShowRootLines
        {
            get
            {
                return this.m_ShowRootLines;
            }
            set
            {
                this.m_ShowRootLines = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }

        public TOCTreeNode TopNode
        {
            get
            {
                return null;
            }
        }

        public int VisibleCount
        {
            get
            {
                return 0;
            }
        }

        public delegate void AfterSelectEventHandler(TOCTreeNode pSelectNode);

        public delegate void LayerVisibleChangedEventHandler(TOCTreeNode pSelectNode);

        public delegate void NodeReorderedEventHandler(TOCTreeNode pFirstOldParent, TOCTreeNode FirstNode, TOCTreeNode pLastOldParent, TOCTreeNode LastNode);

        public delegate void NodeReorderingEventHandler(TOCTreeNode FirstNode, TOCTreeNode LastNode);
    }
}

