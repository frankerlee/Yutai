using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Controls.SymbolUI;
using Yutai.Plugins.Events;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class TOCTreeViewEx : TocTreeViewBase
    {
        private TOCTreeViewType _type = TOCTreeViewType.TOCTree;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private List<BarItem> baritems = new List<BarItem>();
        private BarManager barManager1;
        private IContainer components = null;
        private List<bool> isgroups = new List<bool>();
        private bool m_bDrag = false;
        private bool m_CanDo = true;
        private object m_hook = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_ipPageLayout = null;
        protected MapAndPageLayoutControls m_mappagelayout = null;
        private System.Drawing.Point m_MarkerPoint = new System.Drawing.Point(-10, -10);
        private IApplication m_pApp = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_pConnectActiveEvent = null;
        protected ILayer m_pCurrentLayer = null;
        private TOCTreeNode m_pFirstHitNode = null;
        private IGlobeControlDefault m_pGlobeControl = null;
        private IMapControl2 m_pInMapCtrl = null;
        private TOCTreeNode m_pLastHitNode = null;
        private ITable m_pLayerConfigTable = null;
        protected IMapControl2 m_pMapCtrl = null;
        protected IPageLayoutControl2 m_pPageLayoutCtrl = null;
        private ISceneControlDefault m_pSceneControl = null;
        private IWorkspace m_pWorkspace = null;
        private PopupMenu popupMenu1;

        public event AfterSelectEventHandler AfterSelect;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public event LayerVisibleChangedEventHandler LayerVisibleChanged;

        public TOCTreeViewEx()
        {
            this.InitializeComponent();
            this.CanEditStyle = true;
            base.m_pNodes.Owner = this;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
            this.Indent = 0x10;
            this.CanDrag = true;
            this.ShowLines = false;
            this.TOCTreeViewType = TOCTreeViewType.TOCTree;
            TreeViewEvent.LayerPropertyChanged += new TreeViewEvent.LayerPropertyChangedHandler(this.TreeViewEvent_LayerPropertyChanged);
        }

        private void AddActiveEvent()
        {
            try
            {
                if (this.m_pConnectActiveEvent != null)
                {
                    this.m_pConnectActiveEvent.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                    this.m_pConnectActiveEvent.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                    this.m_pConnectActiveEvent.ItemReordered+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                    this.m_pConnectActiveEvent.ContentsCleared+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.m_pConnectActiveEvent_ContentsCleared));
                }
            }
            catch (Exception exception)
            {
               Logger.Current.Error("", exception, "");
            }
        }

        private void AddToSelection(TOCTreeNode pSelNode)
        {
            if (base.m_pSelectedNodes.IndexOf(pSelNode) == -1)
            {
                pSelNode.IsSelected = true;
                base.m_pSelectedNodes.InternalAdd(pSelNode);
            }
        }

        private bool BulidMenu(PopupMenu LayerPopupMenu)
        {
            if (this.baritems.Count == 0)
            {
                this.InitPopupMenu();
            }
            LayerPopupMenu.ClearLinks();
            if (base.SelectedNodes.Count > 0)
            {
                for (int i = 0; i < this.baritems.Count; i++)
                {
                    BarItem item = this.baritems[i];
                    if (item is BarSubItem)
                    {
                        for (int j = 0; j < (item as BarSubItem).ItemLinks.Count; j++)
                        {
                            BarItemLink link = (item as BarSubItem).ItemLinks[j];
                            if (link.Item is BarSubItem)
                            {
                                if (this.CheckSubMenuItem(link.Item))
                                {
                                    LayerPopupMenu.AddItem(item).BeginGroup = this.isgroups[i];
                                    break;
                                }
                            }
                            else if ((link.Item.Tag is ITOCNodePopmenuItem) && (link.Item.Tag as ITOCNodePopmenuItem).IsShow)
                            {
                                LayerPopupMenu.AddItem(item).BeginGroup = this.isgroups[i];
                                break;
                            }
                        }
                    }
                    else
                    {
                        object tag = item.Tag;
                        bool isShow = true;
                        if (tag is ITOCNodePopmenuItem)
                        {
                            isShow = (tag as ITOCNodePopmenuItem).IsShow;
                        }
                        if (isShow)
                        {
                            LayerPopupMenu.AddItem(item);
                        }
                    }
                }
            }
            return true;
        }

        public void Calculate()
        {
            System.Drawing.Point autoScrollPosition = base.AutoScrollPosition;
            int x = autoScrollPosition.X;
            int y = autoScrollPosition.Y;
            try
            {
                Graphics pGraphics = base.CreateGraphics();
                for (int i = 0; i < base.m_pNodes.Count; i++)
                {
                    (base.m_pNodes[i] as TOCTreeNode).CalculateBounds(pGraphics, ref x, ref y, this.Indent);
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
            if (pLastNode.Parent != pFirstNode)
            {
                int index;
                NodeType nodeType = pFirstNode.GetNodeType();
                NodeType type2 = pLastNode.GetNodeType();
                if (nodeType == type2)
                {
                    this.NodeReordering(pFirstNode, pLastNode);
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
                            this.NodeReordering(pFirstNode, pLastNode);
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
                                this.NodeReordering(pFirstNode, pLastNode);
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
                                this.NodeReordering(pFirstNode, pLastNode);
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
                            this.NodeReordering(pFirstNode, pLastNode);
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
                        this.NodeReordering(pFirstNode, pLastNode);
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
                    this.NodeReordering(pFirstNode, pLastNode);
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
            }
        }

        private bool CheckSubMenuItem(BarItem item)
        {
            for (int i = 0; i < (item as BarSubItem).ItemLinks.Count; i++)
            {
                BarItemLink link = (item as BarSubItem).ItemLinks[i];
                if (link.Item is BarSubItem)
                {
                    return this.CheckSubMenuItem(link.Item);
                }
                if ((link.Item.Tag is ITOCNodePopmenuItem) && (link.Item.Tag as ITOCNodePopmenuItem).IsShow)
                {
                    return true;
                }
            }
            return false;
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

        private void DrawMarker(Graphics pGraphics, Pen pPen, System.Drawing.Point StartPt, System.Drawing.Point EndPt)
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

        private TOCTreeNode FindItem(TOCTreeNode pRootItem, int OID)
        {
            for (int i = 0; i < pRootItem.Nodes.Count; i++)
            {
                TOCTreeNode node = pRootItem.Nodes[i] as TOCTreeNode;
                if ((node != null) && (node.OID == OID))
                {
                    return node;
                }
                TOCTreeNode node2 = this.FindItem(node, OID);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
        }

        private TOCTreeNode FindLayerNode(TOCTreeNode pParentNode, ILayer pLayer)
        {
            TOCTreeNode node2 = null;
            for (int i = 0; i < pParentNode.Nodes.Count; i++)
            {
                TOCTreeNode node = pParentNode.Nodes[i] as TOCTreeNode;
                if (node.Tag == pLayer)
                {
                    return node;
                }
                node2 = this.FindLayerNode(node, pLayer);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
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

        private TOCTreeNode FindMapNodeByNode(TOCTreeNode pTOCNode)
        {
            if ((pTOCNode.TOCNodeType == NodeType.Map) || (pTOCNode.TOCNodeType == NodeType.MapFrame))
            {
                return pTOCNode;
            }
            if (pTOCNode.Parent != null)
            {
                if ((pTOCNode.Parent.TOCNodeType == NodeType.Map) || (pTOCNode.Parent.TOCNodeType == NodeType.MapFrame))
                {
                    return pTOCNode.Parent;
                }
                return this.FindMapNodeByNode(pTOCNode.Parent);
            }
            return null;
        }

        private TOCTreeNode GetDBParentItem(ILayer pLayer, TOCTreeNode pMapItem, ref int LayerOID)
        {
            if (this.m_pLayerConfigTable != null)
            {
                IQueryFilter queryFilter = new QueryFilterClass {
                    WhereClause = "Name = '" + pLayer.Name + "'"
                };
                IRow row = this.m_pLayerConfigTable.Search(queryFilter, false).NextRow();
                if (row != null)
                {
                    LayerOID = row.OID;
                    object obj2 = row.Fields.FindField("ParentID");
                    int oID = 0;
                    if (!(obj2 is DBNull))
                    {
                        oID = Convert.ToInt32(row.get_Value(row.Fields.FindField("ParentID")));
                    }
                    if (oID != 0)
                    {
                        row = this.m_pLayerConfigTable.GetRow(oID);
                        string name = row.get_Value(row.Fields.FindField("Name")).ToString();
                        TOCTreeNode node = this.FindItem(pMapItem, oID);
                        if (node == null)
                        {
                            node = this.InsertItem(name, "", pMapItem, oID);
                        }
                        return node;
                    }
                }
            }
            return null;
        }

        [DllImport("user32")]
        public static extern int GetKeyState(int nVirtKey);
        private int GetLayerIndexInGroupLayer(IGroupLayer pGroupLayer, ILayer pLayer)
        {
            for (int i = 0; i < (pGroupLayer as ICompositeLayer).Count; i++)
            {
                if ((pGroupLayer as ICompositeLayer).get_Layer(i) == pLayer)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetLayerIndexInMap(IBasicMap pMap, ILayer pLayer)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i) == pLayer)
                {
                    return i;
                }
            }
            return -1;
        }

        public TOCTreeNode GetNodeAt(System.Drawing.Point point)
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

        private TOCTreeNode GetTreeNode(TOCTreeNode pParentNode, object Tag)
        {
            TOCTreeNode node = null;
            for (int i = 0; i < pParentNode.Nodes.Count; i++)
            {
                node = pParentNode.Nodes[i] as TOCTreeNode;
                if (node.Tag == Tag)
                {
                    return node;
                }
                if (node.Nodes.Count > 0)
                {
                    TOCTreeNode treeNode = this.GetTreeNode(node, Tag);
                    if (treeNode != null)
                    {
                        return treeNode;
                    }
                }
            }
            return null;
        }

        private TOCTreeNode GetWorkspaceTreeNode(IBasicMap pMap, IWorkspace pWorkspace)
        {
            IPropertySet connectionProperties = pWorkspace.ConnectionProperties;
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                TOCTreeNode node2;
                int num2;
                IWorkspace tag;
                TOCTreeNode node = this.Nodes[i] as TOCTreeNode;
                if ((node.Tag is IBasicMap) && (node.Tag == pMap))
                {
                    if (node.Nodes.Count > 0)
                    {
                        num2 = 0;
                        while (num2 < node.Nodes.Count)
                        {
                            node2 = node.Nodes[num2] as TOCTreeNode;
                            tag = node2.Tag as IWorkspace;
                            if ((tag != null) && this.IsEqual(tag, pWorkspace))
                            {
                                return node2;
                            }
                            num2++;
                        }
                    }
                    break;
                }
                if ((node.Tag is IMapFrame) && ((node.Tag as IMapFrame).Map == pMap))
                {
                    if (node.Nodes.Count > 0)
                    {
                        for (num2 = 0; num2 < node.Nodes.Count; num2++)
                        {
                            node2 = node.Nodes[num2] as TOCTreeNode;
                            tag = node2.Tag as IWorkspace;
                            if ((tag != null) && this.IsEqual(tag, pWorkspace))
                            {
                                return node2;
                            }
                        }
                    }
                    break;
                }
            }
            return null;
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
            this.barManager1.BeginInit();
            this.popupMenu1.BeginInit();
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
            base.Name = "TOCTreeViewEx";
            base.Load += new EventHandler(this.TOCTreeViewEx_Load);
            base.Paint += new PaintEventHandler(this.TOCTreeViewEx_Paint);
            base.MouseDown += new MouseEventHandler(this.TOCTreeViewEx_MouseDown);
            base.MouseMove += new MouseEventHandler(this.TOCTreeViewEx_MouseMove);
            base.MouseUp += new MouseEventHandler(this.TOCTreeViewEx_MouseUp);
            this.barManager1.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
        }

        protected void InitPopupMenu()
        {
            string xMLConfig = System.Windows.Forms.Application.StartupPath + @"\TOCTreeviewCommands.xml";
            TreeCreatePopMenuItem item = new TreeCreatePopMenuItem();
            IMapControl2 pInMapCtrl = null;
            if (this.m_pInMapCtrl != null)
            {
                pInMapCtrl = this.m_pInMapCtrl;
            }
            else if (this.m_hook is MapAndPageLayoutControls)
            {
                pInMapCtrl = (this.m_hook as MapAndPageLayoutControls).MapControl;
            }
            item.StartCreateBar(xMLConfig, this, this.m_pApp, pInMapCtrl, this.m_pPageLayoutCtrl, this.barManager1, this.baritems, this.isgroups);
        }

        private void InsertBaseMapLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pParentNode)
        {
            ICompositeLayer layer;
            int num;
            if (pLayer is IGroupLayer)
            {
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), pParentNode);
                }
            }
            else if (pLayer is ICompositeLayer)
            {
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), pParentNode);
                }
            }
            else
            {
                this.InsertLayerToTree(pLayer, pParentNode);
            }
        }

        private void InsertDBLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pMapNode)
        {
            int num;
            if (pLayer is IGroupLayer)
            {
                for (num = 0; num < (pLayer as ICompositeLayer).Count; num++)
                {
                    this.InsertDBLayerToTree(pMap, (pLayer as ICompositeLayer).get_Layer(num), pMapNode);
                }
            }
            else
            {
                int layerOID = 0;
                TOCTreeNode node = this.GetDBParentItem(pLayer, pMapNode, ref layerOID);
                if (node == null)
                {
                    node = pMapNode;
                }
                TOCTreeNode pNode = new TOCTreeNode(pLayer.Name, true, true) {
                    OID = layerOID,
                    Checked = pLayer.Visible,
                    Tag = pLayer
                };
                node.Nodes.Add(pNode);
                if (pLayer is ITinLayer)
                {
                    ITinLayer layer = pLayer as ITinLayer;
                    for (num = 0; num < layer.RendererCount; num++)
                    {
                        ITinRenderer renderer = layer.GetRenderer(num);
                        TOCTreeNode node3 = new TOCTreeNode(renderer.Name) {
                            Tag = renderer
                        };
                        pNode.Nodes.Add(node3);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer, pNode);
                    }
                }
                else if (pLayer is IGeoFeatureLayer)
                {
                    IGeoFeatureLayer layer2 = (IGeoFeatureLayer) pLayer;
                    this.InsertLegendInfoToTree((ILegendInfo) layer2.Renderer, pNode);
                }
                else if (pLayer is IRasterLayer)
                {
                    IRasterLayer layer3 = (IRasterLayer) pLayer;
                    this.InsertLegendInfoToTree((ILegendInfo) layer3.Renderer, pNode);
                }
            }
        }

        private void InsertDSLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pMapNode)
        {
            int num;
            if (pLayer is IGroupLayer)
            {
                for (num = 0; num < (pLayer as ICompositeLayer).Count; num++)
                {
                    this.InsertDSLayerToTree(pMap, (pLayer as ICompositeLayer).get_Layer(num), pMapNode);
                }
            }
            else
            {
                TOCTreeNode node3 = pMapNode;
                IWorkspace pWorkspace = null;
                if (pLayer is IFeatureLayer)
                {
                    pWorkspace = ((pLayer as IFeatureLayer).FeatureClass as IDataset).Workspace;
                }
                else if (pLayer is ITopologyLayer)
                {
                    pWorkspace = ((pLayer as ITopologyLayer).Topology as IDataset).Workspace;
                }
                else if (pLayer is ITinLayer)
                {
                    pWorkspace = ((pLayer as ITinLayer).Dataset as IDataset).Workspace;
                }
                else if (pLayer is IRasterLayer)
                {
                    IRasterLayer layer = pLayer as IRasterLayer;
                    IRaster2 raster = layer.Raster as IRaster2;
                    pWorkspace = (raster.RasterDataset as IDataset).Workspace;
                }
                else if (pLayer is IDataset)
                {
                    pWorkspace = (pLayer as IDataset).Workspace;
                }
                if (pWorkspace != null)
                {
                    IFeatureDataset featureDataset;
                    TOCTreeNode treeNode;
                    TOCTreeNode workspaceTreeNode = this.GetWorkspaceTreeNode(pMap, pWorkspace);
                    if (workspaceTreeNode == null)
                    {
                        Bitmap bitmap = null;
                        string pathName = pWorkspace.PathName;
                        if (pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            IPropertySet connectionProperties = pWorkspace.ConnectionProperties;
                            bool flag = false;
                            try
                            {
                                pathName = connectionProperties.GetProperty("Version").ToString();
                                flag = true;
                            }
                            catch
                            {
                            }
                            if (!flag)
                            {
                                try
                                {
                                    pathName = connectionProperties.GetProperty("HISTORICAL_NAME").ToString();
                                    flag = true;
                                }
                                catch
                                {
                                }
                            }
                            if (!flag)
                            {
                                try
                                {
                                    pathName = connectionProperties.GetProperty("HISTORICAL_TIMESTAMP").ToString();
                                    flag = true;
                                }
                                catch
                                {
                                }
                            }
                            string str2 = connectionProperties.GetProperty("DB_CONNECTION_PROPERTIES").ToString();
                            pathName = pathName + "(" + str2;
                            try
                            {
                                str2 = connectionProperties.GetProperty("User").ToString();
                                pathName = pathName + "-" + str2 + ")";
                            }
                            catch
                            {
                                pathName = pathName + ")";
                            }
                            bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.bmpGDBLink.bmp"));
                        }
                        else if (pWorkspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                        {
                            bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.bmpPersonGDB.bmp"));
                        }
                        else
                        {
                            bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.bmpFileWorkspace.bmp"));
                        }
                        workspaceTreeNode = new TOCTreeNode(pathName, false, true);
                        if (pLayer is IDataset)
                        {
                            workspaceTreeNode.Tag = (pLayer as IDataset).Workspace;
                        }
                        else if (pLayer is IFeatureLayer)
                        {
                            workspaceTreeNode.Tag = ((pLayer as IFeatureLayer).FeatureClass as IDataset).Workspace;
                        }
                        else if (pLayer is ITopologyLayer)
                        {
                            workspaceTreeNode.Tag = ((pLayer as ITopologyLayer).Topology as IDataset).Workspace;
                        }
                        workspaceTreeNode.Image = bitmap;
                        pMapNode.Nodes.Add(workspaceTreeNode);
                    }
                    node3 = workspaceTreeNode;
                    if (pLayer is IFeatureLayer)
                    {
                        featureDataset = (pLayer as IFeatureLayer).FeatureClass.FeatureDataset;
                        if (featureDataset != null)
                        {
                            treeNode = this.GetTreeNode(workspaceTreeNode, featureDataset);
                            if (treeNode == null)
                            {
                                treeNode = new TOCTreeNode {
                                    Text = featureDataset.Name,
                                    Tag = featureDataset
                                };
                                workspaceTreeNode.Nodes.Add(treeNode);
                            }
                            node3 = treeNode;
                        }
                    }
                    else if (pLayer is ITopologyLayer)
                    {
                        featureDataset = (pLayer as ITopologyLayer).Topology.FeatureDataset;
                        if (featureDataset != null)
                        {
                            treeNode = this.GetTreeNode(workspaceTreeNode, featureDataset);
                            if (treeNode == null)
                            {
                                treeNode = new TOCTreeNode {
                                    Text = featureDataset.Name,
                                    Tag = featureDataset
                                };
                                workspaceTreeNode.Nodes.Add(treeNode);
                            }
                            node3 = treeNode;
                        }
                    }
                }
                TOCTreeNode pNode = new TOCTreeNode(pLayer.Name, true, true) {
                    Checked = pLayer.Visible,
                    Tag = pLayer
                };
                node3.Nodes.Add(pNode);
                if (pLayer is ITinLayer)
                {
                    ITinLayer layer2 = pLayer as ITinLayer;
                    for (num = 0; num < layer2.RendererCount; num++)
                    {
                        ITinRenderer renderer = layer2.GetRenderer(num);
                        TOCTreeNode node5 = new TOCTreeNode(renderer.Name) {
                            Tag = renderer
                        };
                        pNode.Nodes.Add(node5);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer, pNode);
                    }
                }
                else if (pLayer is ITopologyLayer)
                {
                    TOCTreeNode node6;
                    IFeatureRenderer renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRAreaErrors);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("错误的面") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                    renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRLineErrors);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("错误的线") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                    renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRPointErrors);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("错误的点") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                    renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRAreaExceptions);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("有异议的区域") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                    renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRLineExceptions);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("有异议的线") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                    renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRPointExceptions);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("有异议的点") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                    renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRDirtyAreas);
                    if (renderer2 != null)
                    {
                        node6 = new TOCTreeNode("需要清理的区域") {
                            TOCNodeType = NodeType.Text
                        };
                        pNode.Nodes.Add(node6);
                        this.InsertLegendInfoToTree((ILegendInfo) renderer2, pNode);
                    }
                }
                else if (pLayer is IGeoFeatureLayer)
                {
                    IGeoFeatureLayer layer3 = (IGeoFeatureLayer) pLayer;
                    this.InsertLegendInfoToTree((ILegendInfo) layer3.Renderer, pNode);
                }
                else if (pLayer is IRasterLayer)
                {
                    IRasterLayer layer4 = (IRasterLayer) pLayer;
                    this.InsertLegendInfoToTree((ILegendInfo) layer4.Renderer, pNode);
                }
                else if (pLayer is ICompositeLayer)
                {
                    for (num = 0; num < (pLayer as ICompositeLayer).Count; num++)
                    {
                        this.InsertLayerToTree(pMap, (pLayer as ICompositeLayer).get_Layer(num), pNode);
                    }
                }
            }
        }

        private TOCTreeNode InsertItem(string name, string fcname, TOCTreeNode pParentItem, int ChildOID)
        {
            IQueryFilter filter = new QueryFilterClass();
            IRow row = this.m_pLayerConfigTable.GetRow(ChildOID);
            if (row != null)
            {
                TOCTreeNode node2;
                int oID = Convert.ToInt32(row.get_Value(row.Fields.FindField("ParentID")));
                if (oID != 0)
                {
                    row = this.m_pLayerConfigTable.GetRow(oID);
                    string str = row.get_Value(row.Fields.FindField("Name")) as string;
                    TOCTreeNode node = this.FindItem(pParentItem, oID);
                    if (node == null)
                    {
                        node = this.InsertItem(str, "", pParentItem, oID);
                    }
                    if (node == null)
                    {
                        return null;
                    }
                    node2 = new TOCTreeNode(name) {
                        TOCNodeType = NodeType.Folder,
                        OID = ChildOID
                    };
                    node.Nodes.Add(node2);
                    return node2;
                }
                node2 = new TOCTreeNode(name) {
                    TOCNodeType = NodeType.Folder,
                    OID = ChildOID
                };
                pParentItem.Nodes.Add(node2);
                return node2;
            }
            return null;
        }

        private void InsertLayerToTree(ILayer pLayer, TOCTreeNode pParentNode)
        {
            if (pLayer is ITinLayer)
            {
                ITinLayer layer = pLayer as ITinLayer;
                for (int i = 0; i < layer.RendererCount; i++)
                {
                    ITinRenderer renderer = layer.GetRenderer(i);
                    TOCTreeNode pNode = new TOCTreeNode(renderer.Name) {
                        Tag = renderer
                    };
                    pParentNode.Nodes.Add(pNode);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer, pParentNode);
                }
            }
            else if (pLayer is ITopologyLayer)
            {
                TOCTreeNode node2;
                IFeatureRenderer renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRAreaErrors);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("错误的面") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRLineErrors);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("错误的线") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRPointErrors);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("错误的点") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRAreaExceptions);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("有异议的区域") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRLineExceptions);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("有异议的线") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRPointExceptions);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("有异议的点") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRDirtyAreas);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("需要清理的区域") {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
            }
            else if (pLayer is IGeoFeatureLayer)
            {
                IGeoFeatureLayer layer2 = (IGeoFeatureLayer) pLayer;
                this.InsertLegendInfoToTree((ILegendInfo) layer2.Renderer, pParentNode);
            }
            else if (pLayer is IRasterLayer)
            {
                IRasterLayer layer3 = (IRasterLayer) pLayer;
                this.InsertLegendInfoToTree((ILegendInfo) layer3.Renderer, pParentNode);
            }
            else if (pLayer is INetworkLayer)
            {
                this.InsertLegendInfoToTree((ILegendInfo) pLayer, pParentNode);
            }
        }

        private void InsertLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pParentNode)
        {
            TOCTreeNode node;
            ICompositeLayer layer;
            int num;
            if (pLayer is IGroupLayer)
            {
                node = new TOCTreeNode(pLayer.Name, true, true) {
                    Checked = pLayer.Visible,
                    Tag = pLayer,
                    TOCNodeType = NodeType.GroupLayer
                };
                pParentNode.Nodes.Add(node);
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), node);
                }
            }
            else if (pLayer is IBasemapSubLayer)
            {
                node = new TOCTreeNode(pLayer.Name, true, true) {
                    Checked = pLayer.Visible,
                    Tag = pLayer,
                    TOCNodeType = NodeType.BasemapSubLayer
                };
                pParentNode.Nodes.Add(node);
                this.InsertBaseMapLayerToTree(pMap, (pLayer as IBasemapSubLayer).Layer, node);
            }
            else if (pLayer is ICompositeLayer)
            {
                node = new TOCTreeNode(pLayer.Name, true, true) {
                    Checked = pLayer.Visible,
                    Tag = pLayer,
                    TOCNodeType = NodeType.Layer
                };
                pParentNode.Nodes.Add(node);
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), node);
                }
            }
            else
            {
                node = new TOCTreeNode(pLayer.Name, true, true) {
                    Checked = pLayer.Visible,
                    Tag = pLayer
                };
                pParentNode.Nodes.Add(node);
                this.InsertLayerToTree(pLayer, node);
            }
        }

        private void InsertLegendInfoToTree(ILegendInfo pLegendInfo, TOCTreeNode pParantNode)
        {
            int legendGroupCount = pLegendInfo.LegendGroupCount;
            TOCTreeNode pNode = pParantNode;
            TOCTreeNode node2 = null;
            for (int i = 0; i < legendGroupCount; i++)
            {
                ILegendGroup group = pLegendInfo.get_LegendGroup(i);
                if (legendGroupCount > 1)
                {
                    pNode = new TOCTreeNode(group.Heading) {
                        Tag = group
                    };
                    pParantNode.Nodes.Add(pNode);
                }
                int classCount = group.ClassCount;
                for (int j = 0; j < classCount; j++)
                {
                    ILegendClass class2 = group.get_Class(j);
                    node2 = new TOCTreeNode(class2.Label, false, true) {
                        Tag = class2
                    };
                    pNode.Nodes.Add(node2);
                }
            }
        }

        private void InsertMapFrameToTree(IActiveView pAV, IElement pElement, TOCTreeNodeCollection pParentNodes)
        {
            if (pElement is IMapFrame)
            {
                IMapFrame frame = (IMapFrame) pElement;
                string name = frame.Map.Name;
                if (name == "")
                {
                    name = "Scene";
                }
                TOCTreeNode pNode = new TOCTreeNode(name, false, true);
                Bitmap bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.layers.bmp"));
                pNode.Image = bitmap;
                pNode.Tag = frame;
                pNode.TOCNodeType = NodeType.MapFrame;
                pParentNodes.Add(pNode);
                if (pAV.FocusMap == frame.Map)
                {
                    base.m_FocusMapNode = pNode;
                    base.m_FocusMap = frame.Map as IBasicMap;
                    base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                }
                this.InsertMapToTree((IBasicMap) frame.Map, pNode);
            }
            else if (pElement is IGroupElement)
            {
                IGroupElement element = (IGroupElement) pElement;
                IEnumElement elements = element.Elements;
                elements.Reset();
                for (IElement element3 = elements.Next(); element3 != null; element3 = elements.Next())
                {
                    this.InsertMapFrameToTree(pAV, element3, pParentNodes);
                }
            }
        }

        private void InsertMapToTree(IBasicMap pMap, TOCTreeNode pParentNode)
        {
            int layerCount = pMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer pLayer = pMap.get_Layer(i);
                if (pMap is IScene)
                {
                    if (((pMap as IScene).ActiveGraphicsLayer != pLayer) && !(pLayer is IGraphicsContainer3D))
                    {
                        if (this.TOCTreeViewType == TOCTreeViewType.DSTree)
                        {
                            this.InsertDSLayerToTree(pMap, pLayer, pParentNode);
                        }
                        else if (this.TOCTreeViewType == TOCTreeViewType.TOCTree)
                        {
                            this.InsertLayerToTree(pMap, pLayer, pParentNode);
                        }
                        else if (this.TOCTreeViewType == TOCTreeViewType.DBConfigTree)
                        {
                            this.InsertDBLayerToTree(pMap, pLayer, pParentNode);
                        }
                    }
                }
                else if (this.TOCTreeViewType == TOCTreeViewType.DSTree)
                {
                    this.InsertDSLayerToTree(pMap, pLayer, pParentNode);
                }
                else if (this.TOCTreeViewType == TOCTreeViewType.TOCTree)
                {
                    this.InsertLayerToTree(pMap, pLayer, pParentNode);
                }
            }
            if (this.TOCTreeViewType == TOCTreeViewType.DSTree)
            {
                IStandaloneTableCollection tables = pMap as IStandaloneTableCollection;
                for (int j = 0; j < tables.StandaloneTableCount; j++)
                {
                    ITable pTable = tables.get_StandaloneTable(j) as ITable;
                    this.InsertTableToTree(pMap, pTable, pParentNode);
                }
            }
        }

        private void InsertTableToTree(IBasicMap pMap, ITable pTable, TOCTreeNode pMapNode)
        {
            TOCTreeNode node3 = pMapNode;
            IWorkspace pWorkspace = null;
            if (pTable is IDataset)
            {
                pWorkspace = (pTable as IDataset).Workspace;
            }
            if (pWorkspace != null)
            {
                TOCTreeNode workspaceTreeNode = this.GetWorkspaceTreeNode(pMap, pWorkspace);
                if (workspaceTreeNode == null)
                {
                    Bitmap bitmap = null;
                    string pathName = pWorkspace.PathName;
                    if (pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        IPropertySet connectionProperties = pWorkspace.ConnectionProperties;
                        pathName = connectionProperties.GetProperty("Version").ToString();
                        string str2 = connectionProperties.GetProperty("DB_CONNECTION_PROPERTIES").ToString();
                        pathName = pathName + "(" + str2;
                        try
                        {
                            str2 = connectionProperties.GetProperty("User").ToString();
                            pathName = pathName + "-" + str2;
                        }
                        catch
                        {
                        }
                        pathName = pathName + ")";
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.bmpGDBLink.bmp"));
                    }
                    else if (pWorkspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.bmpPersonGDB.bmp"));
                    }
                    else
                    {
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.bmpFileWorkspace.bmp"));
                    }
                    workspaceTreeNode = new TOCTreeNode(pathName, false, true) {
                        Tag = (pTable as IDataset).Workspace,
                        Image = bitmap
                    };
                    pMapNode.Nodes.Add(workspaceTreeNode);
                }
                node3 = workspaceTreeNode;
            }
            TOCTreeNode pNode = new TOCTreeNode((pTable as IDataset).Name, false, true);
            Bitmap bitmap2 = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpPersonGDB.bmp"));
            pNode.Checked = true;
            pNode.Image = bitmap2;
            pNode.Tag = pTable;
            node3.Nodes.Add(pNode);
        }

        private void InsertToSelection(TOCTreeNode pSelNode)
        {
            if (base.m_pSelectedNodes.IndexOf(pSelNode) == -1)
            {
                TOCTreeNodeCollection nodes;
                int index = -1;
                if (pSelNode.Parent == null)
                {
                    nodes = this.Nodes;
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

        private bool IsEqual(IWorkspace pWorkspace1, IWorkspace pWorkspace2)
        {
            if ((pWorkspace1.Type == esriWorkspaceType.esriFileSystemWorkspace) && (pWorkspace2.Type == esriWorkspaceType.esriFileSystemWorkspace))
            {
                string str = pWorkspace1.PathName.ToLower();
                string str2 = pWorkspace2.PathName.ToLower();
                return (str == str2);
            }
            return pWorkspace1.ConnectionProperties.IsEqual(pWorkspace2.ConnectionProperties);
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

        private void m_ipPageLayout_FocusMapChanged()
        {
            try
            {
                if (this.m_pConnectActiveEvent != null)
                {
                    this.m_pConnectActiveEvent.ItemAdded-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                    this.m_pConnectActiveEvent.ItemDeleted-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                    this.m_pConnectActiveEvent.ItemReordered-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                    this.m_pConnectActiveEvent.ContentsCleared-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.m_pConnectActiveEvent_ContentsCleared));
                }
            }
            catch
            {
            }
            IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
            if (this.m_pConnectActiveEvent != null)
            {
                this.m_pConnectActiveEvent.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                this.m_pConnectActiveEvent.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                this.m_pConnectActiveEvent.ItemReordered+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                this.m_pConnectActiveEvent.ContentsCleared+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.m_pConnectActiveEvent_ContentsCleared));
            }
            base.m_FocusMap = this.m_pPageLayoutCtrl.ActiveView.FocusMap as IBasicMap;
            if (this.m_pInMapCtrl != null)
            {
                int num;
                IMap focusMap = this.m_pPageLayoutCtrl.ActiveView.FocusMap;
                this.m_pInMapCtrl.Map.ClearLayers();
                (this.m_pInMapCtrl.Map as IActiveView).ContentsChanged();
                this.m_pInMapCtrl.Map.MapUnits = focusMap.MapUnits;
                this.m_pInMapCtrl.Map.SpatialReferenceLocked = false;
                this.m_pInMapCtrl.Map.SpatialReference = focusMap.SpatialReference;
                this.m_pInMapCtrl.Map.Name = focusMap.Name;
                for (num = 0; num < focusMap.LayerCount; num++)
                {
                    ILayer layer = focusMap.get_Layer(num);
                    this.m_pInMapCtrl.AddLayer(layer, num);
                }
                (this.m_pInMapCtrl.Map as IGraphicsContainer).DeleteAllElements();
                IGraphicsContainer container = focusMap as IGraphicsContainer;
                container.Reset();
                IElement element = container.Next();
                int zorder = 0;
                while (element != null)
                {
                    (this.m_pInMapCtrl.Map as IGraphicsContainer).AddElement(element, zorder);
                    zorder++;
                    element = container.Next();
                }
                (this.m_pInMapCtrl.Map as ITableCollection).RemoveAllTables();
                ITableCollection tables = focusMap as ITableCollection;
                for (num = 0; num < tables.TableCount; num++)
                {
                    (this.m_pInMapCtrl.Map as ITableCollection).AddTable(tables.get_Table(num));
                }
                this.m_pInMapCtrl.ActiveView.Extent = (focusMap as IActiveView).Extent;
                this.m_pInMapCtrl.ActiveView.Refresh();
            }
            this.RefreshTree();
        }

        private void m_ipPageLayout_ItemAdded(object Item)
        {
            if (this.m_CanDo)
            {
                if (Item is IMapFrame)
                {
                    this.RefreshTree();
                }
                else if (Item is IGroupElement)
                {
                    IActiveView pageLayout = this.m_pPageLayoutCtrl.PageLayout as IActiveView;
                    this.InsertMapFrameToTree(pageLayout, Item as IElement, this.Nodes);
                    base.Invalidate();
                }
            }
        }

        private void m_ipPageLayout_ItemDeleted(object Item)
        {
            if (this.m_CanDo && (Item is IMapFrame))
            {
                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    TOCTreeNode pNode = this.Nodes[i] as TOCTreeNode;
                    if (pNode.Tag == Item)
                    {
                        this.Nodes.Remove(pNode);
                        base.Invalidate();
                        break;
                    }
                }
            }
        }

        private void m_pConnectActiveEvent_ContentsCleared()
        {
            if (this.m_CanDo)
            {
                this.RefreshTree();
            }
        }

        private void m_pConnectActiveEvent_ItemAdded(object Item)
        {
            if (this.m_CanDo && (Item is ILayer))
            {
                if (this.m_pInMapCtrl != null)
                {
                    this.m_pInMapCtrl.AddLayer((ILayer) Item, 0);
                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Item, null);
                }
                this.RefreshTree();
            }
        }

        private void m_pConnectActiveEvent_ItemDeleted(object Item)
        {
            if (this.m_CanDo && (Item is ILayer))
            {
                TOCTreeNode pNode = this.FindLayerNode(base.m_FocusMapNode, Item as ILayer);
                if (pNode != null)
                {
                    if (pNode.Parent != null)
                    {
                        pNode.Parent.Nodes.Remove(pNode);
                    }
                    else
                    {
                        this.Nodes.Remove(pNode);
                    }
                }
                if (this.m_pInMapCtrl != null)
                {
                    this.m_pInMapCtrl.Map.DeleteLayer(Item as ILayer);
                    if (this.m_pInMapCtrl.LayerCount == 0)
                    {
                        this.m_pInMapCtrl.Map.SpatialReferenceLocked = false;
                        this.m_pInMapCtrl.Map.SpatialReference = new UnknownCoordinateSystemClass();
                        this.m_pInMapCtrl.Map.MapUnits = esriUnits.esriUnknownUnits;
                        this.m_pInMapCtrl.Map.DistanceUnits = esriUnits.esriUnknownUnits;
                        (this.m_pInMapCtrl.Map as IActiveView).Extent = (this.m_pInMapCtrl.Map as IActiveView).FullExtent;
                    }
                    this.m_pInMapCtrl.ActiveView.Refresh();
                }
                base.Invalidate();
            }
        }

        private void m_pConnectActiveEvent_ItemReordered(object Item, int toIndex)
        {
            if (this.m_CanDo)
            {
                this.RefreshTree();
            }
        }

        private void mapctrl_OnMapReplaced(object newMap)
        {
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pMapCtrl.Map;
            this.RefreshTree();
            this.AddActiveEvent();
        }

        private void MoveLayerTo(IGroupLayer pGroupLayer, ILayer pLayer, int nIndex)
        {
            ICompositeLayer layer = pGroupLayer as ICompositeLayer;
            if (layer.Count >= 2)
            {
                if ((layer.Count - 1) == nIndex)
                {
                    pGroupLayer.Delete(pLayer);
                    pGroupLayer.Add(pLayer);
                }
                else
                {
                    int num;
                    IArray array = new ArrayClass();
                    for (num = 0; num < layer.Count; num++)
                    {
                        array.Add(layer.get_Layer(num));
                    }
                    pGroupLayer.Clear();
                    for (num = 0; num < array.Count; num++)
                    {
                        if (layer.Count == nIndex)
                        {
                            pGroupLayer.Add(pLayer);
                        }
                        ILayer layer2 = array.get_Element(num) as ILayer;
                        if (layer2 != pLayer)
                        {
                            pGroupLayer.Add(layer2);
                        }
                    }
                }
            }
        }

        private void NodeReordering(TOCTreeNode FirstNode, TOCTreeNode LastNode)
        {
            if (((FirstNode.GetNodeType() != NodeType.MapFrame) || (LastNode.GetNodeType() != NodeType.MapFrame)) && ((FirstNode.GetNodeType() != NodeType.Map) || (LastNode.GetNodeType() != NodeType.Map)))
            {
                Exception exception;
                this.m_CanDo = false;
                try
                {
                    TOCTreeNode node2;
                    TOCTreeNode node3;
                    if (((FirstNode.GetNodeType() == NodeType.Layer) && (LastNode.GetNodeType() == NodeType.Layer)) || ((((FirstNode.GetNodeType() == NodeType.GroupLayer) && (LastNode.GetNodeType() != NodeType.GroupLayer)) && (LastNode.GetNodeType() != NodeType.Map)) && (LastNode.GetNodeType() != NodeType.MapFrame)))
                    {
                        int layerIndexInMap;
                        if ((LastNode.Parent.GetNodeType() == NodeType.Map) || (LastNode.Parent.GetNodeType() == NodeType.MapFrame))
                        {
                            IBasicMap tag;
                            if (LastNode.Parent.TOCNodeType == NodeType.Map)
                            {
                                tag = LastNode.Parent.Tag as IBasicMap;
                            }
                            else
                            {
                                tag = (LastNode.Parent.Tag as IMapFrame).Map as IBasicMap;
                            }
                            layerIndexInMap = this.GetLayerIndexInMap(tag, LastNode.Tag as ILayer);
                            if (FirstNode.NodeRect.Top < LastNode.NodeRect.Top)
                            {
                                layerIndexInMap++;
                            }
                            if (layerIndexInMap == -1)
                            {
                                layerIndexInMap = 0;
                            }
                            if (FirstNode.Parent.GetNodeType() == NodeType.Map)
                            {
                                if (FirstNode.Parent == LastNode.Parent)
                                {
                                    if (FirstNode.Parent.Tag is IMap)
                                    {
                                        (FirstNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    }
                                    else if (FirstNode.Parent.Tag is IScene)
                                    {
                                        (FirstNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    }
                                    if (FirstNode.Parent.Tag is IMap)
                                    {
                                        (FirstNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    }
                                    else if (FirstNode.Parent.Tag is IScene)
                                    {
                                        (FirstNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    }
                                    (FirstNode.Parent.Tag as IActiveView).Refresh();
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                                    {
                                        this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        this.m_pInMapCtrl.ActiveView.Refresh();
                                    }
                                }
                                else
                                {
                                    (FirstNode.Parent.Tag as IBasicMap).DeleteLayer(FirstNode.Tag as ILayer);
                                    (FirstNode.Parent.Tag as IActiveView).Refresh();
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                                    {
                                        this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                        this.m_pInMapCtrl.ActiveView.Refresh();
                                    }
                                    if (LastNode.Parent.Tag is IBasicMap)
                                    {
                                        (LastNode.Parent.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                                        if (LastNode.Parent.Tag is IMap)
                                        {
                                            (LastNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        }
                                        else if (LastNode.Parent.Tag is IScene)
                                        {
                                            (LastNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        }
                                        (LastNode.Parent.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                    }
                                    else
                                    {
                                        (LastNode.Parent.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                        (LastNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        ((LastNode.Parent.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                    }
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == LastNode.Parent))
                                    {
                                        this.m_pInMapCtrl.Map.AddLayer(FirstNode.Tag as ILayer);
                                        this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        this.m_pInMapCtrl.ActiveView.Refresh();
                                    }
                                }
                            }
                            else if (FirstNode.Parent.GetNodeType() == NodeType.MapFrame)
                            {
                                if (FirstNode.Parent == LastNode.Parent)
                                {
                                    (FirstNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                                    {
                                        this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        this.m_pInMapCtrl.ActiveView.Refresh();
                                    }
                                }
                                else
                                {
                                    (FirstNode.Parent.Tag as IMapFrame).Map.DeleteLayer(FirstNode.Tag as ILayer);
                                    ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                                    {
                                        this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                        this.m_pInMapCtrl.ActiveView.Refresh();
                                    }
                                    if (LastNode.Parent.Tag is IBasicMap)
                                    {
                                        (LastNode.Parent.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                                        if (LastNode.Parent.Tag is IMap)
                                        {
                                            (LastNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        }
                                        else if (LastNode.Parent.Tag is IScene)
                                        {
                                            (LastNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        }
                                        (LastNode.Parent.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                    }
                                    else
                                    {
                                        (LastNode.Parent.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                        (LastNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        ((LastNode.Parent.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                    }
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == LastNode.Parent))
                                    {
                                        this.m_pInMapCtrl.Map.AddLayer(FirstNode.Tag as ILayer);
                                        this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                        this.m_pInMapCtrl.ActiveView.Refresh();
                                    }
                                }
                            }
                            else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                            {
                                (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                                TOCTreeNode node = this.FindMapNodeByNode(FirstNode);
                                if (node.Tag is IBasicMap)
                                {
                                    (node.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                else
                                {
                                    ((node.Tag as IMapFrame) as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if (LastNode.Parent.Tag is IBasicMap)
                                {
                                    (LastNode.Parent.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                                    if (LastNode.Parent.Tag is IMap)
                                    {
                                        (LastNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    }
                                    else if (LastNode.Parent.Tag is IScene)
                                    {
                                        (LastNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    }
                                    (LastNode.Parent.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                else
                                {
                                    (LastNode.Parent.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                    (LastNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    ((LastNode.Parent.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == LastNode.Parent))
                                {
                                    this.m_pInMapCtrl.Map.AddLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                            }
                        }
                        else if (LastNode.Parent.GetNodeType() == NodeType.GroupLayer)
                        {
                            node2 = this.FindMapNodeByNode(FirstNode);
                            node3 = this.FindMapNodeByNode(LastNode);
                            layerIndexInMap = this.GetLayerIndexInGroupLayer(LastNode.Parent.Tag as IGroupLayer, LastNode.Tag as ILayer);
                            if (FirstNode.NodeRect.Top < LastNode.NodeRect.Top)
                            {
                                layerIndexInMap++;
                            }
                            if (layerIndexInMap == -1)
                            {
                                layerIndexInMap = 0;
                            }
                            if (FirstNode.Parent.GetNodeType() == NodeType.Map)
                            {
                                (FirstNode.Parent.Tag as IBasicMap).DeleteLayer(FirstNode.Tag as ILayer);
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                                {
                                    this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                                (LastNode.Parent.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                                this.MoveLayerTo(LastNode.Parent.Tag as IGroupLayer, FirstNode.Tag as ILayer, layerIndexInMap);
                                (FirstNode.Parent.Tag as IActiveView).Refresh();
                                if (node2 != node3)
                                {
                                    if (node3.Tag is IBasicMap)
                                    {
                                        (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                    else
                                    {
                                        ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                    {
                                        this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                }
                            }
                            else if (FirstNode.Parent.GetNodeType() == NodeType.MapFrame)
                            {
                                (FirstNode.Parent.Tag as IMapFrame).Map.DeleteLayer(FirstNode.Tag as ILayer);
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                                {
                                    this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                                (LastNode.Parent.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                                this.MoveLayerTo(LastNode.Parent.Tag as IGroupLayer, FirstNode.Tag as ILayer, layerIndexInMap);
                                ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                                if (node2 != node3)
                                {
                                    if (node3.Tag is IBasicMap)
                                    {
                                        (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                    else
                                    {
                                        ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                    {
                                        this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                }
                            }
                            else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                            {
                                (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                                if (node2.Tag is IBasicMap)
                                {
                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                else
                                {
                                    ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node2))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                (LastNode.Parent.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                                this.MoveLayerTo(LastNode.Parent.Tag as IGroupLayer, FirstNode.Tag as ILayer, layerIndexInMap);
                                if (node3.Tag is IBasicMap)
                                {
                                    (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                }
                                else
                                {
                                    ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                }
                            }
                        }
                    }
                    else if (LastNode.TOCNodeType == NodeType.GroupLayer)
                    {
                        node2 = this.FindMapNodeByNode(FirstNode);
                        node3 = this.FindMapNodeByNode(LastNode);
                        if (FirstNode.Parent.GetNodeType() == NodeType.Map)
                        {
                            (FirstNode.Parent.Tag as IBasicMap).DeleteLayer(FirstNode.Tag as ILayer);
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                            {
                                this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                this.m_pInMapCtrl.ActiveView.Refresh();
                            }
                            (LastNode.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                            this.MoveLayerTo(LastNode.Tag as IGroupLayer, FirstNode.Tag as ILayer, 0);
                            (FirstNode.Parent.Tag as IActiveView).Refresh();
                            if (node2 != node3)
                            {
                                if (node3.Tag is IBasicMap)
                                {
                                    (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                else
                                {
                                    ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                            }
                        }
                        else if (FirstNode.Parent.GetNodeType() == NodeType.MapFrame)
                        {
                            ILayer layer = FirstNode.Tag as ILayer;
                            (FirstNode.Parent.Tag as IMapFrame).Map.DeleteLayer(layer);
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == FirstNode.Parent))
                            {
                                this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                this.m_pInMapCtrl.ActiveView.Refresh();
                            }
                            IGroupLayer pGroupLayer = LastNode.Tag as IGroupLayer;
                            try
                            {
                                pGroupLayer.Add(layer);
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                string str = exception.ToString();
                            }
                            this.MoveLayerTo(pGroupLayer, layer, 0);
                            ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                            if (node2 != node3)
                            {
                                if (node3.Tag is IBasicMap)
                                {
                                    (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                else
                                {
                                    ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                            }
                        }
                        else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                        {
                            (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                            if (node2.Tag is IBasicMap)
                            {
                                (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                            }
                            else
                            {
                                ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                            }
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node2))
                            {
                                this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                            }
                            (LastNode.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                            this.MoveLayerTo(LastNode.Tag as IGroupLayer, FirstNode.Tag as ILayer, 0);
                            if (node3.Tag is IBasicMap)
                            {
                                (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                            }
                            else
                            {
                                ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                            }
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                            {
                                this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                            }
                        }
                    }
                    else if ((LastNode.TOCNodeType == NodeType.Map) || (LastNode.TOCNodeType == NodeType.MapFrame))
                    {
                        if (FirstNode.Parent.GetNodeType() == NodeType.Map)
                        {
                            if (FirstNode.Parent == LastNode)
                            {
                                if (LastNode.Tag is IMap)
                                {
                                    (LastNode.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, 0);
                                }
                                else if (LastNode.Tag is IScene)
                                {
                                    (LastNode.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, 0);
                                }
                                (FirstNode.Parent.Tag as IActiveView).Refresh();
                                if ((this.m_pInMapCtrl != null) && (LastNode == base.m_FocusMapNode))
                                {
                                    this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                            }
                            else
                            {
                                (FirstNode.Parent.Tag as IBasicMap).DeleteLayer(FirstNode.Tag as ILayer);
                                (FirstNode.Parent.Tag as IActiveView).Refresh();
                                if ((this.m_pInMapCtrl != null) && (FirstNode.Parent == base.m_FocusMapNode))
                                {
                                    this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                                if (LastNode.Tag is IBasicMap)
                                {
                                    (LastNode.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                                    if (LastNode.Tag is IMap)
                                    {
                                        (LastNode.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, 0);
                                    }
                                    else if (LastNode.Tag is IScene)
                                    {
                                        (LastNode.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, 0);
                                    }
                                    (LastNode.Tag as IActiveView).Refresh();
                                }
                                else if (LastNode.Tag is IMapFrame)
                                {
                                    (LastNode.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                    (LastNode.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                    ((LastNode.Tag as IMapFrame).Map as IActiveView).Refresh();
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == LastNode))
                                {
                                    this.m_pInMapCtrl.Map.AddLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                            }
                        }
                        else if (FirstNode.Parent.GetNodeType() == NodeType.MapFrame)
                        {
                            if (FirstNode.Parent == LastNode)
                            {
                                (FirstNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                                if ((this.m_pInMapCtrl != null) && (FirstNode.Parent == base.m_FocusMapNode))
                                {
                                    this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                            }
                            else
                            {
                                (FirstNode.Parent.Tag as IMapFrame).Map.DeleteLayer(FirstNode.Tag as ILayer);
                                ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                                if ((this.m_pInMapCtrl != null) && (FirstNode.Parent == base.m_FocusMapNode))
                                {
                                    this.m_pInMapCtrl.Map.DeleteLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                                if (LastNode.Tag is IBasicMap)
                                {
                                    (LastNode.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                                    if (LastNode.Tag is IMap)
                                    {
                                        (LastNode.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, 0);
                                    }
                                    else if (LastNode.Tag is IScene)
                                    {
                                        (LastNode.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, 0);
                                    }
                                    (LastNode.Tag as IActiveView).Refresh();
                                }
                                if (LastNode.Tag is IMapFrame)
                                {
                                    (LastNode.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                    (LastNode.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                    ((LastNode.Tag as IMapFrame).Map as IActiveView).Refresh();
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == LastNode))
                                {
                                    this.m_pInMapCtrl.Map.AddLayer(FirstNode.Tag as ILayer);
                                    this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                    this.m_pInMapCtrl.ActiveView.Refresh();
                                }
                            }
                        }
                        else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                        {
                            node2 = this.FindMapNodeByNode(FirstNode);
                            node3 = this.FindMapNodeByNode(LastNode);
                            (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                            if (node2 != node3)
                            {
                                if (node2.Tag is IBasicMap)
                                {
                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                else if (node2.Tag is IMapFrame)
                                {
                                    ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node2))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                            }
                            if (LastNode.Tag is IBasicMap)
                            {
                                (LastNode.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                                if (LastNode.Tag is IMap)
                                {
                                    (LastNode.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, 0);
                                }
                                else if (LastNode.Tag is IScene)
                                {
                                    (LastNode.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, 0);
                                }
                                (LastNode.Tag as IActiveView).Refresh();
                            }
                            if (LastNode.Tag is IMapFrame)
                            {
                                (LastNode.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                (LastNode.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                ((LastNode.Tag as IMapFrame).Map as IActiveView).Refresh();
                            }
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                            {
                                this.m_pInMapCtrl.Map.AddLayer(FirstNode.Tag as ILayer);
                                this.m_pInMapCtrl.Map.MoveLayer(FirstNode.Tag as ILayer, 0);
                                this.m_pInMapCtrl.ActiveView.Refresh();
                            }
                        }
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                   Logger.Current.Error("", exception, "");
                }
                this.m_CanDo = true;
            }
        }

        public void RefreshTree()
        {
            try
            {
                IPageLayout pageLayout;
                IActiveView view;
                IGraphicsContainer container;
                IElement element;
                this.Nodes.Clear();
                if (this.m_pPageLayoutCtrl != null)
                {
                    pageLayout = this.m_pPageLayoutCtrl.PageLayout;
                    view = pageLayout as IActiveView;
                    container = pageLayout as IGraphicsContainer;
                    container.Reset();
                    for (element = container.Next(); element != null; element = container.Next())
                    {
                        this.InsertMapFrameToTree(view, element, this.Nodes);
                    }
                }
                else
                {
                    string name;
                    TOCTreeNode node;
                    Bitmap bitmap;
                    if (this.m_pMapCtrl != null)
                    {
                        name = this.m_pMapCtrl.Map.Name;
                        if (name == "")
                        {
                            name = "Scene";
                        }
                        node = new TOCTreeNode(name, false, true);
                        base.m_FocusMapNode = node;
                        base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                        base.m_FocusMap = this.m_pMapCtrl.Map as IBasicMap;
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                        node.Image = bitmap;
                        node.Tag = this.m_pMapCtrl.Map;
                        this.Nodes.Add(node);
                        this.InsertMapToTree(this.m_pMapCtrl.Map as IBasicMap, node);
                    }
                    else if (this.m_pSceneControl != null)
                    {
                        name = "场景视图";
                        node = new TOCTreeNode(name, false, true);
                        base.m_FocusMapNode = node;
                        base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                        base.m_FocusMap = this.m_pSceneControl.Scene as IBasicMap;
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                        node.Image = bitmap;
                        node.Tag = this.m_pSceneControl.Scene;
                        this.Nodes.Add(node);
                        this.InsertMapToTree(this.m_pSceneControl.Scene as IBasicMap, node);
                    }
                    else if (this.m_pGlobeControl != null)
                    {
                        name = "Globe视图";
                        node = new TOCTreeNode(name, false, true);
                        base.m_FocusMapNode = node;
                        base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                        base.m_FocusMap = this.m_pGlobeControl.Globe as IBasicMap;
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                        node.Image = bitmap;
                        node.Tag = this.m_pGlobeControl.Globe;
                        this.Nodes.Add(node);
                        this.InsertMapToTree(this.m_pGlobeControl.Globe as IBasicMap, node);
                    }
                    else if (this.m_mappagelayout != null)
                    {
                        pageLayout = this.m_mappagelayout.PageLayoutControl.PageLayout;
                        view = pageLayout as IActiveView;
                        container = pageLayout as IGraphicsContainer;
                        container.Reset();
                        for (element = container.Next(); element != null; element = container.Next())
                        {
                            this.InsertMapFrameToTree(view, element, this.Nodes);
                        }
                    }
                }
                this.ExpandAll();
                this.Calculate();
                this.SetScroll();
                if ((this.Nodes.Count == 0) && (this.m_pCurrentLayer != null))
                {
                    this.m_pCurrentLayer = null;
                    if (this.CurrentLayerChanged != null)
                    {
                        this.CurrentLayerChanged(this, new EventArgs());
                    }
                }
                base.Invalidate(base.ClientRectangle);
            }
            catch (Exception)
            {
            }
        }

        private void RemoveActiveEvent()
        {
            try
            {
                if (this.m_pConnectActiveEvent != null)
                {
                    this.m_pConnectActiveEvent.ItemAdded-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                    this.m_pConnectActiveEvent.ItemDeleted-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                    this.m_pConnectActiveEvent.ItemReordered-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                    this.m_pConnectActiveEvent.ContentsCleared-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.m_pConnectActiveEvent_ContentsCleared));
                }
            }
            catch
            {
            }
            this.m_pConnectActiveEvent = null;
        }

        public void SetMapCtrl(object MapCtrl)
        {
            this.m_pInMapCtrl = (IMapControl2) MapCtrl;
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

        private void TOCTreeViewEx_Load(object sender, EventArgs e)
        {
        }

        private void TOCTreeViewEx_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
            TOCTreeNode nodeAt = this.GetNodeAt(e.X, e.Y);
            if (nodeAt == null)
            {
                base.ClearSelection();
                this.BulidMenu(this.popupMenu1);
                if (this.AfterSelect != null)
                {
                    this.AfterSelect(null);
                }
                base.Invalidate();
            }
            else if (((e.Button != MouseButtons.Right) || (base.m_pSelectedNodes.Count <= 0)) || (base.m_pSelectedNodes.IndexOf(nodeAt) == -1))
            {
                TOCTreeNode node2;
                if (base.m_pSelectedNodes.Count == 0)
                {
                    base.m_pLHitNode = nodeAt;
                    this.AddToSelection(nodeAt);
                }
                else
                {
                    node2 = base.m_pSelectedNodes[0] as TOCTreeNode;
                    if (((node2.TOCNodeType != nodeAt.TOCNodeType) && ((node2.TOCNodeType != NodeType.GroupLayer) || (nodeAt.TOCNodeType != NodeType.Layer))) && ((node2.TOCNodeType != NodeType.Layer) || (nodeAt.TOCNodeType != NodeType.GroupLayer)))
                    {
                        base.ClearSelection();
                        this.AddToSelection(nodeAt);
                    }
                    else if (!this.IsShiftDown())
                    {
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
                            base.ClearSelection();
                            this.AddToSelection(nodeAt);
                        }
                    }
                    else
                    {
                        TOCTreeNode pLHitNode = base.m_pLHitNode;
                        base.ClearSelection();
                        if (pLHitNode.Parent != nodeAt.Parent)
                        {
                            this.AddToSelection(nodeAt);
                        }
                        else
                        {
                            int index;
                            int num2;
                            if (node2.Parent == null)
                            {
                                index = this.Nodes.IndexOf(pLHitNode);
                                num2 = this.Nodes.IndexOf(nodeAt);
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
                            while (true)
                            {
                                this.AddToSelection(pSelNode);
                                pSelNode = pSelNode.NextNode;
                                if (pSelNode == node5)
                                {
                                    this.AddToSelection(node5);
                                    break;
                                }
                            }
                        }
                    }
                    base.m_pLHitNode = nodeAt;
                }
                if (base.m_pSelectedNodes.Count != 1)
                {
                    if (this.m_pCurrentLayer != null)
                    {
                        this.m_pCurrentLayer = null;
                        if (this.CurrentLayerChanged != null)
                        {
                            this.CurrentLayerChanged(this, new EventArgs());
                        }
                    }
                    this.BulidMenu(this.popupMenu1);
                    if (this.AfterSelect != null)
                    {
                        this.AfterSelect(null);
                    }
                    base.Invalidate();
                }
                else
                {
                    base.SelectedNode = base.m_pSelectedNodes[0] as TOCTreeNode;
                    this.BulidMenu(this.popupMenu1);
                    if (this.AfterSelect != null)
                    {
                        this.AfterSelect(base.SelectedNode);
                    }
                    if (base.SelectedNode.Tag is ILayer)
                    {
                        if (this.m_pCurrentLayer != base.SelectedNode.Tag)
                        {
                            this.m_pCurrentLayer = base.SelectedNode.Tag as ILayer;
                            if (this.CurrentLayerChanged != null)
                            {
                                this.CurrentLayerChanged(this, new EventArgs());
                            }
                        }
                    }
                    else if (this.m_pCurrentLayer != null)
                    {
                        this.m_pCurrentLayer = null;
                        if (this.CurrentLayerChanged != null)
                        {
                            this.CurrentLayerChanged(this, new EventArgs());
                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        base.Invalidate();
                    }
                    else
                    {
                        switch (base.SelectedNode.HitTest(e.X, e.Y))
                        {
                            case HitType.Expand:
                                if (!base.SelectedNode.IsExpanded)
                                {
                                    base.SelectedNode.Expand();
                                }
                                else
                                {
                                    base.SelectedNode.Collapse();
                                }
                                this.Calculate();
                                this.SetScroll();
                                base.Invalidate();
                                return;

                            case HitType.Check:
                                base.SelectedNode.Checked = !base.SelectedNode.Checked;
                                if (base.SelectedNode.Tag is ILayer)
                                {
                                    (base.SelectedNode.Tag as ILayer).Visible = base.SelectedNode.Checked;
                                    node2 = this.FindMapNode(base.SelectedNode);
                                    if (node2 != null)
                                    {
                                        if (node2.Tag is IBasicMap)
                                        {
                                            (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Tag as ILayer, null);
                                        }
                                        else if (node2.Tag is IMapFrame)
                                        {
                                            if (this.m_hook is MapAndPageLayoutControls)
                                            {
                                                if ((this.m_hook as MapAndPageLayoutControls).ActiveViewType == "MapControl")
                                                {
                                                    ((this.m_hook as MapAndPageLayoutControls).ActiveControl as IMapControl2).ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Tag, null);
                                                }
                                                else
                                                {
                                                    ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Tag, null);
                                                }
                                            }
                                            else
                                            {
                                                ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Tag, null);
                                            }
                                        }
                                        DocumentManager.DocumentChanged(this.m_hook);
                                    }
                                }
                                if (this.LayerVisibleChanged != null)
                                {
                                    this.LayerVisibleChanged(base.SelectedNode);
                                }
                                return;

                            case HitType.Image:
                                frmSymbolSelector selector;
                                ISymbol tag;
                                if (!this.CanEditStyle)
                                {
                                    break;
                                }
                                if (base.SelectedNode.Tag is ISymbol)
                                {
                                    try
                                    {
                                        selector = new frmSymbolSelector();
                                        if (selector == null)
                                        {
                                            break;
                                        }
                                        selector.SetStyleGallery(ApplicationBase.StyleGallery);
                                        tag = base.SelectedNode.Tag as ISymbol;
                                        selector.SetSymbol(tag);
                                        if (selector.ShowDialog() == DialogResult.OK)
                                        {
                                            base.SelectedNode.Tag = selector.GetSymbol();
                                            node2 = this.FindMapNode(base.SelectedNode);
                                            if (node2 != null)
                                            {
                                                if (node2.Tag is IBasicMap)
                                                {
                                                    TreeViewEvent.OnLayerPropertyChanged(this, node2.Tag as IBasicMap, base.SelectedNode.Parent.Tag as ILayer);
                                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                }
                                                else if (node2.Tag is IMapFrame)
                                                {
                                                    TreeViewEvent.OnLayerPropertyChanged(this, (node2.Tag as IMapFrame).Map as IBasicMap, base.SelectedNode.Parent.Tag as ILayer);
                                                    if (this.m_hook is MapAndPageLayoutControls)
                                                    {
                                                        if ((this.m_hook as MapAndPageLayoutControls).ActiveViewType == "MapControl")
                                                        {
                                                            (this.m_hook as MapAndPageLayoutControls).ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                        }
                                                        else
                                                        {
                                                            ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                    }
                                                }
                                                DocumentManager.DocumentChanged(this.m_hook);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (base.SelectedNode.Tag is ILegendClass)
                                {
                                    try
                                    {
                                        selector = new frmSymbolSelector();
                                        if (selector == null)
                                        {
                                            break;
                                        }
                                        selector.SetStyleGallery(ApplicationBase.StyleGallery);
                                        tag = (base.SelectedNode.Tag as ILegendClass).Symbol;
                                        selector.SetSymbol(tag);
                                        if (selector.ShowDialog() == DialogResult.OK)
                                        {
                                            (base.SelectedNode.Tag as ILegendClass).Symbol = selector.GetSymbol() as ISymbol;
                                            node2 = this.FindMapNode(base.SelectedNode);
                                            if (node2 != null)
                                            {
                                                if (node2.Tag is IBasicMap)
                                                {
                                                    TreeViewEvent.OnLayerPropertyChanged(this, node2.Tag as IBasicMap, base.SelectedNode.Parent.Tag as ILayer);
                                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                }
                                                else if (node2.Tag is IMapFrame)
                                                {
                                                    TreeViewEvent.OnLayerPropertyChanged(this, (node2.Tag as IMapFrame).Map as IBasicMap, base.SelectedNode.Parent.Tag as ILayer);
                                                    if (this.m_hook is MapAndPageLayoutControls)
                                                    {
                                                        if ((this.m_hook as MapAndPageLayoutControls).ActiveViewType == "MapControl")
                                                        {
                                                            (this.m_hook as MapAndPageLayoutControls).ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                        }
                                                        else
                                                        {
                                                            ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, base.SelectedNode.Parent.Tag, null);
                                                    }
                                                }
                                                DocumentManager.DocumentChanged(this.m_hook);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                this.Calculate();
                                this.SetScroll();
                                base.Invalidate();
                                return;

                            case HitType.Text:
                                if (this.CanDrag)
                                {
                                    switch (base.SelectedNode.GetNodeType())
                                    {
                                        case NodeType.MapFrame:
                                        case NodeType.Layer:
                                        case NodeType.GroupLayer:
                                        case NodeType.Folder:
                                            this.m_pFirstHitNode = base.SelectedNode;
                                            break;
                                    }
                                }
                                base.Invalidate();
                                break;
                        }
                    }
                }
            }
        }

        private void TOCTreeViewEx_MouseMove(object sender, MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) && this.CanDrag) && (this.m_pFirstHitNode != null))
            {
                this.m_bDrag = true;
            }
            else
            {
                return;
            }
            System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
            TOCTreeNode nodeAt = this.GetNodeAt(e.X, e.Y);
            System.Drawing.Point startPt = new System.Drawing.Point(0, 0);
            System.Drawing.Point endPt = new System.Drawing.Point(0, 0);
            System.Drawing.Point point4 = new System.Drawing.Point(0, 0);
            Pen pPen = new Pen(Color.White, 3f);
            Pen pen2 = new Pen(Color.Black, 3f);
            if ((nodeAt == null) || (nodeAt == this.m_pFirstHitNode))
            {
                if (this.m_MarkerPoint.Y > 0)
                {
                    startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                    endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                            startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                            endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                                            point4 = new System.Drawing.Point(parent.NextNode.NodeRect.Left, parent.NextNode.NodeRect.Top);
                                            if (point4.Y != this.m_MarkerPoint.Y)
                                            {
                                                startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                                                endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                                                    point4 = new System.Drawing.Point(parent.Parent.NextNode.NodeRect.Left, parent.Parent.NextNode.NodeRect.Top);
                                                }
                                                else
                                                {
                                                    point4 = new System.Drawing.Point(this.Bounds.Left, this.Bounds.Bottom);
                                                }
                                            }
                                            if (point4.Y != this.m_MarkerPoint.Y)
                                            {
                                                startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                                                endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                                        point4 = new System.Drawing.Point(this.m_pLastHitNode.NodeRect.Left, this.m_pLastHitNode.NodeRect.Top);
                                        if (point4.Y != this.m_MarkerPoint.Y)
                                        {
                                            startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                                            endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                                    point4 = new System.Drawing.Point(nodeAt.NextNode.NodeRect.Left, nodeAt.NextNode.NodeRect.Top);
                                    if (point4.Y != this.m_MarkerPoint.Y)
                                    {
                                        startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                                        endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                                            point4 = new System.Drawing.Point(nodeAt.Parent.NextNode.NodeRect.Left, nodeAt.Parent.NextNode.NodeRect.Top);
                                        }
                                        else
                                        {
                                            point4 = new System.Drawing.Point(this.Bounds.Right, this.Bounds.Bottom);
                                        }
                                    }
                                    if (point4.Y != this.m_MarkerPoint.Y)
                                    {
                                        startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                                        endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                                point4 = new System.Drawing.Point(nodeAt.NodeRect.Left, nodeAt.NodeRect.Top);
                                if (point4.Y != this.m_MarkerPoint.Y)
                                {
                                    startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                                    endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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

        private void TOCTreeViewEx_MouseUp(object sender, MouseEventArgs e)
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
                    System.Drawing.Point startPt = new System.Drawing.Point(0, this.m_MarkerPoint.Y);
                    System.Drawing.Point endPt = new System.Drawing.Point(base.ClientRectangle.Width, this.m_MarkerPoint.Y);
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
                base.Invalidate();
                this.m_pLastHitNode = null;
                this.m_pFirstHitNode = null;
            }
        }

        private void TOCTreeViewEx_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                this.Calculate();
                this.Draw();
            }
            catch (Exception)
            {
            }
        }

        private void TOCTreeViewWrap_OnGlobeReplaced(object newGlobe)
        {
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pGlobeControl.Globe;
            this.AddActiveEvent();
            this.RefreshTree();
        }

        private void TOCTreeViewWrap_OnPageLayoutReplaced(object newPageLayout)
        {
            IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
            this.m_ipPageLayout = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pPageLayoutCtrl.PageLayout;
            this.m_ipPageLayout.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_ipPageLayout_ItemAdded));
            this.m_ipPageLayout.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_ipPageLayout_ItemDeleted));
            this.m_ipPageLayout.FocusMapChanged+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(this.m_ipPageLayout_FocusMapChanged));
            this.AddActiveEvent();
            this.RefreshTree();
        }

        private void TOCTreeViewWrap_OnSceneReplaced(object newScene)
        {
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pSceneControl.Scene;
            this.AddActiveEvent();
            this.RefreshTree();
        }

        private void TOCTreeViewWrapEx_OnMapDocumentChangedEvent()
        {
            this.RefreshTree();
        }

        private void TreeViewEvent_LayerPropertyChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                this.RefreshTree();
                if ((pMap == base.m_FocusMap) && (this.m_pInMapCtrl != null))
                {
                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                }
            }
        }

        public IApplication Application
        {
            set
            {
                this.m_pApp = value;
                this.Hook = this.m_pApp.Hook;
                (this.m_pApp as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.TOCTreeViewWrapEx_OnMapDocumentChangedEvent);
            }
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

        public bool CanDrag { get; set; }

        public bool CanEditStyle { get; set; }

        public ILayer CurrentLayer
        {
            get
            {
                return this.m_pCurrentLayer;
            }
        }

        public object Hook
        {
            set
            {
                this.m_hook = value;
                this.RemoveActiveEvent();
                try
                {
                    this.m_pMapCtrl = null;
                    this.m_pPageLayoutCtrl = null;
                    this.m_pSceneControl = null;
                    this.m_pGlobeControl = null;
                    this.m_mappagelayout = null;
                    if (value != null)
                    {
                        if (value is IMapControl2)
                        {
                            this.m_pMapCtrl = value as IMapControl2;
                            (this.m_pMapCtrl as IMapControlEvents2_Event).OnMapReplaced+=(new IMapControlEvents2_OnMapReplacedEventHandler(this.mapctrl_OnMapReplaced));
                            if (this.m_pConnectActiveEvent != null)
                            {
                                this.m_pConnectActiveEvent = null;
                            }
                            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pMapCtrl.Map;
                        }
                        else
                        {
                            IActiveView activeView;
                            if (value is IPageLayoutControl2)
                            {
                                this.m_pPageLayoutCtrl = value as IPageLayoutControl2;
                                (this.m_pPageLayoutCtrl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.TOCTreeViewWrap_OnPageLayoutReplaced));
                                activeView = this.m_pPageLayoutCtrl.ActiveView;
                                this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
                                this.m_ipPageLayout = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pPageLayoutCtrl.PageLayout;
                                this.m_ipPageLayout.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_ipPageLayout_ItemAdded));
                                this.m_ipPageLayout.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_ipPageLayout_ItemDeleted));
                                this.m_ipPageLayout.FocusMapChanged+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(this.m_ipPageLayout_FocusMapChanged));
                            }
                            else if (value is ISceneControlDefault)
                            {
                                this.m_pSceneControl = value as ISceneControlDefault;
                                (this.m_pSceneControl as ISceneControlEvents_Event).OnSceneReplaced+=(new ISceneControlEvents_OnSceneReplacedEventHandler(this.TOCTreeViewWrap_OnSceneReplaced));
                                this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pSceneControl.Scene;
                            }
                            else if (value is IGlobeControlDefault)
                            {
                                this.m_pGlobeControl = value as IGlobeControlDefault;
                                (this.m_pGlobeControl as IGlobeControlEvents_Event).OnGlobeReplaced+=(new IGlobeControlEvents_OnGlobeReplacedEventHandler(this.TOCTreeViewWrap_OnGlobeReplaced));
                                this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pGlobeControl.Globe;
                            }
                            else if (value is MapAndPageLayoutControls)
                            {
                                this.m_mappagelayout = value as MapAndPageLayoutControls;
                                this.m_pPageLayoutCtrl = this.m_mappagelayout.PageLayoutControl;
                                (this.m_pPageLayoutCtrl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.TOCTreeViewWrap_OnPageLayoutReplaced));
                                activeView = this.m_mappagelayout.PageLayoutControl.ActiveView;
                                this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
                                this.m_ipPageLayout = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_mappagelayout.PageLayoutControl.PageLayout;
                                this.m_ipPageLayout.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_ipPageLayout_ItemAdded));
                                this.m_ipPageLayout.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_ipPageLayout_ItemDeleted));
                                this.m_ipPageLayout.FocusMapChanged+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(this.m_ipPageLayout_FocusMapChanged));
                            }
                        }
                        this.AddActiveEvent();
                        this.RefreshTree();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public int Indent { get; set; }

        public ITable LayerConfigTable
        {
            set
            {
                this.m_pLayerConfigTable = value;
                this.m_pWorkspace = (this.m_pLayerConfigTable as IDataset).Workspace;
            }
        }

        public TOCTreeNodeCollection Nodes
        {
            get
            {
                return base.m_pNodes;
            }
        }

        public object PopupMenu
        {
            get
            {
                return this.popupMenu1;
            }
        }

        public bool ShowLines { get; set; }

        public bool ShowPlusMinus { get; set; }

        public bool ShowRootLines { get; set; }

        public IStyleGallery StyleGallery { get; set; }

        public TOCTreeViewType TOCTreeViewType
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
                this.CanDrag = this._type == TOCTreeViewType.TOCTree;
            }
        }

        public delegate void AfterSelectEventHandler(TOCTreeNode pSelectNode);

        public delegate void LayerVisibleChangedEventHandler(TOCTreeNode pSelectNode);
    }
}

