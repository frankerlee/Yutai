using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class DataSourceTreeView : TreeViewWrapBase
    {
        private BarButtonItem AddLayer;
        private BarButtonItem ApplyRenderProject;
        private List<BarItem> baritems = new List<BarItem>();
        private BarButtonItem ClearScaleRange;
        private BarButtonItem DeleteAllLayer;
        private BarButtonItem DeleteLayer;
        private BarButtonItem ExportLayer;
        private BarButtonItem ExportMap;
        private List<bool> isgroups = new List<bool>();
        private BarButtonItem LabelFeature;
        private BarButtonItem LayerProperty;
        private BarManager m_barManager1 = null;
        private bool m_CanDo = true;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_ipPageLayout = null;
        private IApplication m_pApp = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_pConnectActiveEvent = null;
        private IMapControl2 m_pInMapCtrl = null;
        private PopupMenu m_pLayerPopupMenu = null;
        protected IMapControl2 m_pMapCtrl = null;
        protected IPageLayoutControl2 m_pPageLayoutCtrl = null;
        private IStyleGallery m_pSG = null;
        private TOCTreeView m_pTOCTreeView = null;
        private BarButtonItem MapFrameProperty;
        private BarButtonItem NewGroupLayer;
        private BarButtonItem OpenAttributeTable;
        private BarButtonItem PanToSelectedFeature;
        private BarButtonItem SaveRenderProject;
        private BarButtonItem SetCurrentLayerSelected;
        private BarButtonItem SetMaximumScale;
        private BarButtonItem SetMinimumScale;
        private BarSubItem SetVisibleScale;
        private BarButtonItem ShowXYDataItem;
        private BarButtonItem ZoomToLayer;

        public DataSourceTreeView(TOCTreeView pTOCTreeView)
        {
            this.m_pTOCTreeView = pTOCTreeView;
            this.m_pTOCTreeView.CanDrag = false;
            this.m_pTOCTreeView.NodeReordering += new TOCTreeView.NodeReorderingEventHandler(this.m_pTOCTreeView_NodeReordering);
            this.m_pTOCTreeView.AfterSelect += new TOCTreeView.AfterSelectEventHandler(this.m_pTOCTreeView_AfterSelect);
            this.m_barManager1 = this.m_pTOCTreeView.BarManager as BarManager;
            this.m_pLayerPopupMenu = this.m_pTOCTreeView.PopupMenu as PopupMenu;
            this.m_barManager1.ItemClick += new ItemClickEventHandler(this.m_barManager1_ItemClick);
            TreeViewEvent.GroupLayerAddLayerChanged += new TreeViewEvent.GroupLayerAddLayerChangedHandler(this.TreeViewEvent_GroupLayerAddLayerChanged);
            TreeViewEvent.LayerPropertyChanged += new TreeViewEvent.LayerPropertyChangedHandler(this.TreeViewEvent_LayerPropertyChanged);
            TreeViewEvent.LayerVisibleChanged += new TreeViewEvent.LayerVisibleChangedHandler(this.TreeViewEvent_LayerVisibleChanged);
            TreeViewEvent.LayerNameChanged += new TreeViewEvent.LayerNameChangedHandler(this.TreeViewEvent_LayerNameChanged);
            TreeViewEvent.MapNameChanged += new TreeViewEvent.MapNameChangedHandler(this.TreeViewEvent_MapNameChanged);
            TreeViewEvent.ChildLayersDeleted += new TreeViewEvent.ChildLayersDeletedHandler(this.TreeViewEvent_ChildLayersDeleted);
            TreeViewEvent.LayerOrderChanged += new TreeViewEvent.LayerOrderChangedHandler(this.TreeViewEvent_LayerOrderChanged);
            this.m_pTOCTreeView.Disposed += new EventHandler(this.m_pTOCTreeView_Disposed);
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

        private void ApplyRenderer(IGeoFeatureLayer pLayer, string filename)
        {
            IFeatureRenderer renderer = this.ReadRender(filename);
            pLayer.Renderer = renderer;
        }

        private void ApplyRenderProject_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private bool BulidMenu(TOCTreeNode pNode, PopupMenu LayerPopupMenu)
        {
            if (this.baritems.Count == 0)
            {
                this.InitPopupMenu();
            }
            LayerPopupMenu.ClearLinks();
            if (this.m_pTOCTreeView.SelectedNodes.Count > 0)
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
            return false;
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

        private void DeleteAllLayer_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void DoDeleteLayer(TOCTreeNode pSelNode)
        {
            IBasicMap tag;
            TOCTreeNode node = this.FindMapNodeByNode(pSelNode);
            if (node.TOCNodeType == NodeType.Map)
            {
                tag = node.Tag as IBasicMap;
            }
            else
            {
                tag = (node.Tag as IMapFrame).Map as IBasicMap;
            }
            if (pSelNode.TOCNodeType == NodeType.Table)
            {
                (tag as IStandaloneTableCollection).RemoveStandaloneTable(pSelNode.Tag as IStandaloneTable);
                TOCTreeNode parent = pSelNode.Parent;
                parent.Nodes.Remove(pSelNode);
                if ((parent.Nodes.Count == 0) && (parent.Parent != null))
                {
                    parent.Parent.Nodes.Remove(parent);
                }
                this.m_pTOCTreeView.Invalidate();
            }
            else
            {
                TOCTreeNode node3 = this.FindLayerNodeContain(pSelNode);
                if (node3.TOCNodeType == NodeType.GroupLayer)
                {
                    IGroupLayer data = node3.Tag as IGroupLayer;
                    if (data != null)
                    {
                        (tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, data, null);
                        this.RefreshTree();
                    }
                }
                else if (tag != null)
                {
                    if (tag.LayerCount == 0)
                    {
                        if (tag is IMap)
                        {
                            (tag as IMap).SpatialReferenceLocked = false;
                        }
                        tag.SpatialReference = null;
                        if (tag is IMap)
                        {
                            (tag as IMap).MapUnits = esriUnits.esriUnknownUnits;
                            (tag as IMap).DistanceUnits = esriUnits.esriUnknownUnits;
                        }
                        (tag as IActiveView).Extent = (tag as IActiveView).FullExtent;
                    }
                    (tag as IActiveView).Refresh();
                    this.RefreshTree();
                }
            }
        }

        private void DoNewGroupLayer(TOCTreeNode pSelNode)
        {
            IGroupLayer layer = new GroupLayerClass {
                Name = "新图层组"
            };
            if (pSelNode.TOCNodeType == NodeType.GroupLayer)
            {
                IBasicMap tag;
                (pSelNode.Tag as IGroupLayer).Add(layer);
                TOCTreeNode node = this.FindMapNodeByNode(pSelNode);
                if (node.TOCNodeType == NodeType.Map)
                {
                    tag = node.Tag as IBasicMap;
                }
                else
                {
                    tag = (node.Tag as IMapFrame).Map as IBasicMap;
                }
                this.InsertLayerToTree(tag, layer, pSelNode);
                this.m_pTOCTreeView.Invalidate();
            }
            else if (pSelNode.TOCNodeType == NodeType.Map)
            {
                this.m_CanDo = false;
                (pSelNode.Tag as IBasicMap).AddLayer(layer);
                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode.Tag == pSelNode.Tag))
                {
                    this.m_pInMapCtrl.Map.AddLayer(layer);
                }
                this.m_CanDo = true;
                this.InsertLayerToTree(pSelNode.Tag as IBasicMap, layer, pSelNode);
                this.m_pTOCTreeView.Invalidate();
            }
        }

        private TOCTreeNode FindLayerNode(ILayer pLayer)
        {
            TOCTreeNode node2 = null;
            for (int i = 0; i < this.m_pTOCTreeView.Nodes.Count; i++)
            {
                TOCTreeNode pParentNode = this.m_pTOCTreeView.Nodes[i] as TOCTreeNode;
                node2 = this.FindLayerNode(pParentNode, pLayer);
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

        private TOCTreeNode FindLayerNodeContain(TOCTreeNode pTOCNode)
        {
            if (pTOCNode.Parent != null)
            {
                if (((pTOCNode.Parent.TOCNodeType == NodeType.Map) || (pTOCNode.Parent.TOCNodeType == NodeType.MapFrame)) || (pTOCNode.Parent.TOCNodeType == NodeType.GroupLayer))
                {
                    return pTOCNode.Parent;
                }
                return this.FindMapNodeByNode(pTOCNode.Parent);
            }
            return null;
        }

        private TOCTreeNode FindMapNodeByNode(TOCTreeNode pTOCNode)
        {
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

        private int GetLayerIndexInGroupLayer(ICompositeLayer pCompositeLayer, ILayer pLayer)
        {
            for (int i = 0; i < pCompositeLayer.Count; i++)
            {
                if (pCompositeLayer.get_Layer(i) == pLayer)
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
            for (int i = 0; i < this.m_pTOCTreeView.Nodes.Count; i++)
            {
                TOCTreeNode node2;
                int num2;
                IWorkspace tag;
                TOCTreeNode node = this.m_pTOCTreeView.Nodes[i] as TOCTreeNode;
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

        public void InitPopupMenu()
        {
            string xMLConfig = System.Windows.Forms.Application.StartupPath + @"\TOCTreeviewCommands.xml";
            new TreeCreatePopMenuItemOld().StartCreateBar(xMLConfig, this.m_pTOCTreeView, this, this.m_pApp, this.m_pInMapCtrl, this.m_pPageLayoutCtrl, this.m_barManager1, this.baritems, this.isgroups);
        }

        private void InsertLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pMapNode)
        {
            int num;
            if (pLayer is IGroupLayer)
            {
                for (num = 0; num < (pLayer as ICompositeLayer).Count; num++)
                {
                    this.InsertLayerToTree(pMap, (pLayer as ICompositeLayer).get_Layer(num), pMapNode);
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
                            bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpGDBLink.bmp"));
                        }
                        else if (pWorkspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                        {
                            bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpPersonGDB.bmp"));
                        }
                        else
                        {
                            bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpFileWorkspace.bmp"));
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

        private void InsertMapFrameToTree(IActiveView pAV, IElement pElement, TOCTreeNodeCollection pParantNodes)
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
                Bitmap bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.layers.bmp"));
                pNode.Image = bitmap;
                pNode.Tag = frame;
                pParantNodes.Add(pNode);
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
                    this.InsertMapFrameToTree(pAV, element3, pParantNodes);
                }
            }
        }

        private void InsertMapToTree(IBasicMap pMap, TOCTreeNode pParantNode)
        {
            int layerCount = pMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer pLayer = pMap.get_Layer(i);
                this.InsertLayerToTree(pMap, pLayer, pParantNode);
            }
            IStandaloneTableCollection tables = pMap as IStandaloneTableCollection;
            for (int j = 0; j < tables.StandaloneTableCount; j++)
            {
                ITable pTable = tables.get_StandaloneTable(j) as ITable;
                this.InsertTableToTree(pMap, pTable, pParantNode);
            }
        }

        private void InsertSubLayerToTree(ILayer pLayer, TOCTreeNode pParentNode)
        {
            TOCTreeNode pNode = new TOCTreeNode(pLayer.Name, true, true) {
                Checked = pLayer.Visible,
                Tag = pLayer,
                TOCNodeType = NodeType.AnnotationSublayer
            };
            pParentNode.Nodes.Add(pNode);
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
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpGDBLink.bmp"));
                    }
                    else if (pWorkspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpPersonGDB.bmp"));
                    }
                    else
                    {
                        bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.bmpFileWorkspace.bmp"));
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

        private void m_barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            double mapScale = 0.0;
            TOCTreeNode selectedNode = this.m_pTOCTreeView.SelectedNode;
            if (selectedNode != null)
            {
                TOCTreeNode node2;
                if ((selectedNode.TOCNodeType == NodeType.Map) || (selectedNode.TOCNodeType == NodeType.MapFrame))
                {
                    node2 = selectedNode;
                }
                else
                {
                    node2 = this.FindMapNodeByNode(selectedNode);
                }
                if (node2 != null)
                {
                    IBasicMap tag = null;
                    if (node2.Tag is IBasicMap)
                    {
                        tag = node2.Tag as IBasicMap;
                    }
                    else
                    {
                        tag = (node2.Tag as IMapFrame).Map as IBasicMap;
                    }
                    try
                    {
                        if ((tag == base.m_FocusMap) && (this.m_pInMapCtrl != null))
                        {
                            mapScale = this.m_pInMapCtrl.MapScale;
                        }
                        else if (tag is IMap)
                        {
                            mapScale = (tag as IMap).MapScale;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void m_ipPageLayout_FocusMapChanged()
        {
            if (this.m_pConnectActiveEvent != null)
            {
                this.m_pConnectActiveEvent.ItemAdded-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                this.m_pConnectActiveEvent.ItemDeleted-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                this.m_pConnectActiveEvent.ItemReordered-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                this.m_pConnectActiveEvent.ContentsCleared-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.m_pConnectActiveEvent_ContentsCleared));
            }
            this.m_pConnectActiveEvent = null;
            IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
            if (this.m_pConnectActiveEvent != null)
            {
                this.m_pConnectActiveEvent.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                this.m_pConnectActiveEvent.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                this.m_pConnectActiveEvent.ItemReordered+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                this.m_pConnectActiveEvent.ContentsCleared+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(this.m_pConnectActiveEvent_ContentsCleared));
            }
        }

        private void m_ipPageLayout_ItemAdded(object Item)
        {
            if (this.m_CanDo && (Item is IMapFrame))
            {
                IBasicMap map = ((IMapFrame) Item).Map as IBasicMap;
                if (map != null)
                {
                    IActiveView pageLayout = this.m_pPageLayoutCtrl.PageLayout as IActiveView;
                    this.InsertMapFrameToTree(pageLayout, Item as IElement, this.m_pTOCTreeView.Nodes);
                    this.m_pTOCTreeView.Invalidate();
                }
            }
        }

        private void m_ipPageLayout_ItemDeleted(object Item)
        {
            if (this.m_CanDo && (Item is IMapFrame))
            {
                for (int i = 0; i < this.m_pTOCTreeView.Nodes.Count; i++)
                {
                    TOCTreeNode pNode = this.m_pTOCTreeView.Nodes[i] as TOCTreeNode;
                    if (pNode.Tag == Item)
                    {
                        this.m_pTOCTreeView.Nodes.Remove(pNode);
                        this.m_pTOCTreeView.Invalidate();
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
                this.RefreshTree();
            }
        }

        private void m_pConnectActiveEvent_ItemDeleted(object Item)
        {
            if (this.m_CanDo && (Item is ILayer))
            {
                this.RefreshTree();
            }
        }

        private void m_pConnectActiveEvent_ItemReordered(object Item, int toIndex)
        {
        }

        private void m_pTOCTreeView_AfterSelect(TOCTreeNode pSelectNode)
        {
            this.BulidMenu(pSelectNode, this.m_pLayerPopupMenu);
            if (pSelectNode != null)
            {
                if (this.m_pApp != null)
                {
                    if (pSelectNode.TOCNodeType == NodeType.Workspace)
                    {
                        this.m_pApp.SelectedWorkspace = pSelectNode.Tag as IWorkspace;
                    }
                    else
                    {
                        this.m_pApp.SelectedWorkspace = null;
                    }
                }
            }
            else if (this.m_pApp != null)
            {
                this.m_pApp.SelectedWorkspace = null;
            }
        }

        private void m_pTOCTreeView_Disposed(object sender, EventArgs e)
        {
            TreeViewEvent.GroupLayerAddLayerChanged -= new TreeViewEvent.GroupLayerAddLayerChangedHandler(this.TreeViewEvent_GroupLayerAddLayerChanged);
            TreeViewEvent.LayerPropertyChanged -= new TreeViewEvent.LayerPropertyChangedHandler(this.TreeViewEvent_LayerPropertyChanged);
            TreeViewEvent.LayerVisibleChanged -= new TreeViewEvent.LayerVisibleChangedHandler(this.TreeViewEvent_LayerVisibleChanged);
            TreeViewEvent.LayerNameChanged -= new TreeViewEvent.LayerNameChangedHandler(this.TreeViewEvent_LayerNameChanged);
            TreeViewEvent.MapNameChanged -= new TreeViewEvent.MapNameChangedHandler(this.TreeViewEvent_MapNameChanged);
            TreeViewEvent.ChildLayersDeleted -= new TreeViewEvent.ChildLayersDeletedHandler(this.TreeViewEvent_ChildLayersDeleted);
            TreeViewEvent.LayerOrderChanged -= new TreeViewEvent.LayerOrderChangedHandler(this.TreeViewEvent_LayerOrderChanged);
        }

        private void m_pTOCTreeView_NodeReordering(TOCTreeNode FirstNode, TOCTreeNode LastNode)
        {
            if (((FirstNode.GetNodeType() != NodeType.MapFrame) || (LastNode.GetNodeType() != NodeType.MapFrame)) && ((FirstNode.GetNodeType() != NodeType.Map) || (LastNode.GetNodeType() != NodeType.Map)))
            {
                this.m_CanDo = false;
                if (((FirstNode.GetNodeType() == NodeType.Layer) && (LastNode.GetNodeType() == NodeType.Layer)) && (this.m_pMapCtrl != null))
                {
                    if (LastNode.Parent.GetNodeType() == NodeType.Map)
                    {
                        int layerIndexInMap = this.GetLayerIndexInMap(LastNode.Parent.Tag as IBasicMap, LastNode.Tag as ILayer);
                        if (FirstNode.Parent.GetNodeType() == NodeType.Map)
                        {
                            if (FirstNode.Parent.Tag is IMap)
                            {
                                (FirstNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                            }
                            else if (FirstNode.Parent.Tag is IScene)
                            {
                                (FirstNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                            }
                            (FirstNode.Parent.Tag as IActiveView).Refresh();
                        }
                        else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                        {
                            (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                            (FirstNode.Parent.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                            (FirstNode.Parent.Tag as IBasicMap).AddLayer(FirstNode.Tag as ILayer);
                            if (FirstNode.Parent.Tag is IMap)
                            {
                                (FirstNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                            }
                            else if (FirstNode.Parent.Tag is IScene)
                            {
                                (FirstNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer, layerIndexInMap);
                            }
                            (FirstNode.Parent.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, FirstNode.Tag, null);
                        }
                    }
                    else if (LastNode.Parent.GetNodeType() == NodeType.GroupLayer)
                    {
                    }
                }
                this.m_CanDo = true;
            }
        }

        private void mapctrl_OnMapReplaced(object newMap)
        {
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pMapCtrl.Map;
            this.RefreshTree();
            this.AddActiveEvent();
        }

        private void PanToSelectedFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            TOCTreeNode selectedNode = this.m_pTOCTreeView.SelectedNode;
            if (selectedNode != null)
            {
                IFeatureLayer tag = selectedNode.Tag as IFeatureLayer;
                if ((tag as IFeatureSelection).SelectionSet.Count > 0)
                {
                    ICursor cursor;
                    (tag as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                    IEnvelope extent = (cursor.NextRow() as IFeature).Extent;
                    for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
                    {
                        extent.Union((row as IFeature).Extent);
                    }
                    IPoint p = new PointClass();
                    p.PutCoords((extent.XMin + extent.XMax) / 2.0, (extent.YMin + extent.YMax) / 2.0);
                    TOCTreeNode node2 = this.FindMapNodeByNode(selectedNode);
                    if (node2 != null)
                    {
                        IBasicMap map;
                        if (node2.Tag is IBasicMap)
                        {
                            map = node2.Tag as IBasicMap;
                        }
                        else
                        {
                            map = (node2.Tag as IMapFrame).Map as IBasicMap;
                        }
                        IActiveView view = map as IActiveView;
                        IEnvelope envelope2 = view.Extent;
                        envelope2.CenterAt(p);
                        view.Extent = envelope2;
                        view.Refresh();
                        if (this.m_pInMapCtrl != null)
                        {
                            this.m_pInMapCtrl.ActiveView.Refresh();
                        }
                    }
                }
            }
        }

        private IFeatureRenderer ReadRender(string filename)
        {
            System.IO.FileStream input = new System.IO.FileStream(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(input);
            int count = reader.ReadInt32();
            byte[] buffer = reader.ReadBytes(count);
            IMemoryBlobStream stream2 = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = stream2
            };
            ((IMemoryBlobStreamVariant) stream2).ImportFromVariant(buffer);
            IPropertySet set = new PropertySetClass();
            (set as ESRI.ArcGIS.esriSystem.IPersistStream).Load(pstm);
            return (set.GetProperty("Render") as IFeatureRenderer);
        }

        public void RefreshTree()
        {
            try
            {
                this.m_pTOCTreeView.Nodes.Clear();
                if (this.m_pPageLayoutCtrl != null)
                {
                    IPageLayout pageLayout = this.m_pPageLayoutCtrl.PageLayout;
                    IActiveView pAV = pageLayout as IActiveView;
                    IGraphicsContainer container = pageLayout as IGraphicsContainer;
                    container.Reset();
                    for (IElement element = container.Next(); element != null; element = container.Next())
                    {
                        this.InsertMapFrameToTree(pAV, element, this.m_pTOCTreeView.Nodes);
                    }
                }
                else if (this.m_pMapCtrl != null)
                {
                    string name = this.m_pMapCtrl.Map.Name;
                    if (name == "")
                    {
                        name = "Scene";
                    }
                    TOCTreeNode pNode = new TOCTreeNode(name, false, true);
                    base.m_FocusMapNode = pNode;
                    base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                    base.m_FocusMap = this.m_pMapCtrl.Map as IBasicMap;
                    Bitmap bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.TOCTreeview.layers.bmp"));
                    pNode.Image = bitmap;
                    pNode.Tag = this.m_pMapCtrl.Map;
                    this.m_pTOCTreeView.Nodes.Add(pNode);
                    this.InsertMapToTree(this.m_pMapCtrl.Map as IBasicMap, pNode);
                }
                this.m_pTOCTreeView.ExpandAll();
                this.m_pTOCTreeView.Calculate();
                this.m_pTOCTreeView.SetScroll();
                this.m_pTOCTreeView.Invalidate(this.m_pTOCTreeView.ClientRectangle);
            }
            catch
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

        private void SaveRender(IFeatureRenderer pRender, string filename)
        {
            object obj2;
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = stream
            };
            IPropertySet set = new PropertySetClass();
            ESRI.ArcGIS.esriSystem.IPersistStream stream3 = set as ESRI.ArcGIS.esriSystem.IPersistStream;
            set.SetProperty("Render", pRender);
            stream3.Save(pstm, 0);
            ((IMemoryBlobStreamVariant) stream).ExportToVariant(out obj2);
            System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.CreateNew);
            BinaryWriter writer = new BinaryWriter(output);
            writer.Write(((byte[]) obj2).Length);
            writer.Write((byte[]) obj2);
            writer.Close();
            output.Close();
        }

        private void SaveRenderProject_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void SetCurrentLayerSelected_ItemClick(object sender, ItemClickEventArgs e)
        {
            TOCTreeNode selectedNode = this.m_pTOCTreeView.SelectedNode;
            if (selectedNode != null)
            {
                TOCTreeNode node2 = this.FindMapNodeByNode(selectedNode);
                if (node2 != null)
                {
                    IBasicMap tag;
                    if (node2.Tag is IBasicMap)
                    {
                        tag = node2.Tag as IBasicMap;
                    }
                    else
                    {
                        tag = (node2.Tag as IMapFrame).Map as IBasicMap;
                    }
                    for (int i = 0; i < tag.LayerCount; i++)
                    {
                        ILayer layer = tag.get_Layer(i);
                        if (layer is IGroupLayer)
                        {
                            this.UnSelectedGroupLayer(layer as ICompositeLayer);
                        }
                        else if (layer is IFeatureLayer)
                        {
                            (layer as IFeatureLayer).Selectable = false;
                        }
                    }
                    (selectedNode.Tag as IFeatureLayer).Selectable = true;
                }
            }
        }

        public void SetMapCtrl(object MapCtrl)
        {
            this.m_pInMapCtrl = (IMapControl2) MapCtrl;
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

        private void TOCTreeViewWrapEx_OnMapDocumentChangedEvent()
        {
            this.RefreshTree();
        }

        private void TreeViewEvent_ChildLayersDeleted(object sender, object pObj)
        {
            if (sender != this)
            {
                this.RefreshTree();
            }
        }

        private void TreeViewEvent_GroupLayerAddLayerChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnGroupLayerAddLayerChanged(this, pMap, pLayer);
                }
                else
                {
                    this.RefreshTree();
                }
            }
        }

        private void TreeViewEvent_LayerNameChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnLayerNameChanged(this, pMap, pLayer);
                }
                else
                {
                    this.RefreshTree();
                }
            }
        }

        private void TreeViewEvent_LayerOrderChanged(object sender)
        {
            this.RefreshTree();
        }

        private void TreeViewEvent_LayerPropertyChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnLayerPropertyChanged(this, pMap, pLayer);
                    if ((pMap == base.m_FocusMap) && (this.m_pInMapCtrl != null))
                    {
                        this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                    }
                }
                else
                {
                    this.RefreshTree();
                }
            }
        }

        private void TreeViewEvent_LayerVisibleChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnLayerVisibleChanged(this, pMap, pLayer);
                    if ((pMap == base.m_FocusMap) && (this.m_pInMapCtrl != null))
                    {
                        this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                    }
                }
                else
                {
                    this.RefreshTree();
                }
            }
        }

        private void TreeViewEvent_MapNameChanged(object sender, IBasicMap pMap)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnMapNameChanged(this, pMap);
                }
                else
                {
                    this.RefreshTree();
                }
            }
        }

        private void UnSelectedGroupLayer(ICompositeLayer pCompositeLayer)
        {
            for (int i = 0; i < pCompositeLayer.Count; i++)
            {
                ILayer layer = pCompositeLayer.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.UnSelectedGroupLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    (layer as IFeatureLayer).Selectable = false;
                }
            }
        }

        public IApplication Application
        {
            set
            {
                this.m_pApp = value;
                (this.m_pApp as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.TOCTreeViewWrapEx_OnMapDocumentChangedEvent);
            }
        }

        public object Hook
        {
            set
            {
                Exception exception;
                try
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
                    catch (Exception exception1)
                    {
                        exception = exception1;
                    }
                    this.m_pConnectActiveEvent = null;
                    this.m_pMapCtrl = null;
                    this.m_ipPageLayout = null;
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
                        else if (value is IPageLayoutControl2)
                        {
                            try
                            {
                                if (this.m_ipPageLayout != null)
                                {
                                    this.m_ipPageLayout.ItemAdded-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_ipPageLayout_ItemAdded));
                                    this.m_ipPageLayout.ItemDeleted-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_ipPageLayout_ItemDeleted));
                                    this.m_ipPageLayout.FocusMapChanged-=(new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(this.m_ipPageLayout_FocusMapChanged));
                                }
                            }
                            catch
                            {
                            }
                            this.m_pPageLayoutCtrl = value as IPageLayoutControl2;
                            IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
                            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
                            this.m_ipPageLayout = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pPageLayoutCtrl.PageLayout;
                            (this.m_pPageLayoutCtrl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.TOCTreeViewWrap_OnPageLayoutReplaced));
                            try
                            {
                                this.m_ipPageLayout.ItemAdded+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_ipPageLayout_ItemAdded));
                                this.m_ipPageLayout.ItemDeleted+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_ipPageLayout_ItemDeleted));
                                this.m_ipPageLayout.FocusMapChanged+=(new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(this.m_ipPageLayout_FocusMapChanged));
                            }
                            catch
                            {
                            }
                        }
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
                        catch (Exception exception2)
                        {
                            exception = exception2;
                           Logger.Current.Error("", exception, "");
                        }
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                   Logger.Current.Error("", exception, "");
                }
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pTOCTreeView.StyleGallery = this.m_pSG;
                this.m_pSG = value;
                this.m_pTOCTreeView.StyleGallery = this.m_pSG;
            }
        }
    }
}

