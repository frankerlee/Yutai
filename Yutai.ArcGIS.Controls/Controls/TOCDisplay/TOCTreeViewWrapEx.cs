using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Events;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class TOCTreeViewWrapEx : TreeViewWrapBase
    {
        private List<BarItem> baritems = new List<BarItem>();
        private List<bool> isgroups = new List<bool>();
        private BarManager m_barManager1 = null;
        private bool m_CanDo = true;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_ipPageLayout = null;
        private IApplication m_pApp = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_pConnectActiveEvent = null;
        protected ILayer m_pCurrentLayer = null;
        private IGlobeControlDefault m_pGlobeControl = null;
        private IMapControl2 m_pInMapCtrl = null;
        private PopupMenu m_pLayerPopupMenu = null;
        protected IMapControl2 m_pMapCtrl = null;
        protected IPageLayoutControl2 m_pPageLayoutCtrl = null;
        private ISceneControlDefault m_pSceneControl = null;
        private IStyleGallery m_pSG = null;
        private TOCTreeView m_pTOCTreeView = null;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public TOCTreeViewWrapEx(TOCTreeView pTOCTreeView)
        {
            this.m_pTOCTreeView = pTOCTreeView;
            this.m_pTOCTreeView.NodeReordering +=
                new TOCTreeView.NodeReorderingEventHandler(this.m_pTOCTreeView_NodeReordering);
            this.m_pTOCTreeView.NodeReordered +=
                new TOCTreeView.NodeReorderedEventHandler(this.m_pTOCTreeView_NodeReordered);
            this.m_pTOCTreeView.AfterSelect += new TOCTreeView.AfterSelectEventHandler(this.m_pTOCTreeView_AfterSelect);
            this.m_barManager1 = this.m_pTOCTreeView.BarManager as BarManager;
            this.m_pLayerPopupMenu = this.m_pTOCTreeView.PopupMenu as PopupMenu;
            this.m_barManager1.ItemClick += new ItemClickEventHandler(this.m_barManager1_ItemClick);
            this.m_pLayerPopupMenu.BeforePopup += new CancelEventHandler(this.m_pLayerPopupMenu_BeforePopup);
            TreeViewEvent.ChildLayersDeleted +=
                new TreeViewEvent.ChildLayersDeletedHandler(this.TreeViewEvent_ChildLayersDeleted);
            TreeViewEvent.GroupLayerAddLayerChanged +=
                new TreeViewEvent.GroupLayerAddLayerChangedHandler(this.TreeViewEvent_GroupLayerAddLayerChanged);
            TreeViewEvent.LayerPropertyChanged +=
                new TreeViewEvent.LayerPropertyChangedHandler(this.TreeViewEvent_LayerPropertyChanged);
            TreeViewEvent.LayerVisibleChanged +=
                new TreeViewEvent.LayerVisibleChangedHandler(this.TreeViewEvent_LayerVisibleChanged);
            TreeViewEvent.LayerNameChanged +=
                new TreeViewEvent.LayerNameChangedHandler(this.TreeViewEvent_LayerNameChanged);
            TreeViewEvent.MapNameChanged += new TreeViewEvent.MapNameChangedHandler(this.TreeViewEvent_MapNameChanged);
            this.m_pTOCTreeView.Disposed += new EventHandler(this.m_pTOCTreeView_Disposed);
        }

        private void AddActiveEvent()
        {
            try
            {
                if (this.m_pConnectActiveEvent != null)
                {
                    this.m_pConnectActiveEvent.ItemAdded +=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(
                        this.m_pConnectActiveEvent_ItemAdded));
                    this.m_pConnectActiveEvent.ItemDeleted +=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(
                        this.m_pConnectActiveEvent_ItemDeleted));
                    this.m_pConnectActiveEvent.ItemReordered +=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(
                        this.m_pConnectActiveEvent_ItemReordered));
                    this.m_pConnectActiveEvent.ContentsCleared +=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(
                        this.m_pConnectActiveEvent_ContentsCleared));
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private bool BulidMenu(PopupMenu LayerPopupMenu)
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
            return true;
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

        private void DocumentChanged(object hook)
        {
            if (hook != null)
            {
                DocumentManager.DocumentChanged(hook);
            }
            else if (this.m_pPageLayoutCtrl != null)
            {
                DocumentManager.DocumentChanged(this.m_pPageLayoutCtrl);
            }
            else if (this.m_pMapCtrl != null)
            {
                DocumentManager.DocumentChanged(this.m_pPageLayoutCtrl);
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

        private string GetLayerName(IBasicMap pMap, string originName)
        {
            int num = 1;
            string str = originName;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i).Name == str)
                {
                    str = originName + num.ToString();
                    num++;
                }
            }
            return str;
        }

        protected void InitPopupMenu()
        {
            string xMLConfig = System.Windows.Forms.Application.StartupPath + @"\TOCTreeviewCommands.xml";
            new TreeCreatePopMenuItemOld().StartCreateBar(xMLConfig, this.m_pTOCTreeView, this, this.m_pApp,
                this.m_pInMapCtrl, this.m_pPageLayoutCtrl, this.m_barManager1, this.baritems, this.isgroups);
        }

        private void InsertBaseMapLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pParentNode, bool IsBegin)
        {
            ICompositeLayer layer;
            int num;
            if (pLayer is IGroupLayer)
            {
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), pParentNode, false);
                }
            }
            else if (pLayer is ICompositeLayer)
            {
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), pParentNode, false);
                }
            }
            else
            {
                this.InsertLayerToTree(pLayer, pParentNode);
            }
        }

        private void InsertLayerToTree(ILayer pLayer, TOCTreeNode pParentNode)
        {
            if (pLayer is ITinLayer)
            {
                ITinLayer layer = pLayer as ITinLayer;
                for (int i = 0; i < layer.RendererCount; i++)
                {
                    ITinRenderer renderer = layer.GetRenderer(i);
                    TOCTreeNode pNode = new TOCTreeNode(renderer.Name)
                    {
                        Tag = renderer
                    };
                    pParentNode.Nodes.Add(pNode);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer, pParentNode);
                }
            }
            else if (pLayer is ITopologyLayer)
            {
                TOCTreeNode node2;
                IFeatureRenderer renderer2 =
                    (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRAreaErrors);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("错误的面")
                    {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRLineErrors);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("错误的线")
                    {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRPointErrors);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("错误的点")
                    {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRAreaExceptions);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("有异议的区域")
                    {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRLineExceptions);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("有异议的线")
                    {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRPointExceptions);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("有异议的点")
                    {
                        TOCNodeType = NodeType.Text
                    };
                    pParentNode.Nodes.Add(node2);
                    this.InsertLegendInfoToTree((ILegendInfo) renderer2, pParentNode);
                }
                renderer2 = (pLayer as ITopologyLayer).get_Renderer(esriTopologyRenderer.esriTRDirtyAreas);
                if (renderer2 != null)
                {
                    node2 = new TOCTreeNode("需要清理的区域")
                    {
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

        private void InsertLayerToTree(IBasicMap pMap, ILayer pLayer, TOCTreeNode pParentNode, bool IsBegin)
        {
            TOCTreeNode node;
            ICompositeLayer layer;
            int num;
            if (pLayer is IGroupLayer)
            {
                node = new TOCTreeNode(pLayer.Name, true, true)
                {
                    Checked = pLayer.Visible,
                    Tag = pLayer,
                    TOCNodeType = NodeType.GroupLayer
                };
                if (IsBegin)
                {
                    pParentNode.Nodes.Insert(0, node);
                }
                else
                {
                    pParentNode.Nodes.Add(node);
                }
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), node, false);
                }
            }
            else if (pLayer is IBasemapSubLayer)
            {
                node = new TOCTreeNode(pLayer.Name, true, true)
                {
                    Checked = pLayer.Visible,
                    Tag = pLayer,
                    TOCNodeType = NodeType.BasemapSubLayer
                };
                if (IsBegin)
                {
                    pParentNode.Nodes.Insert(0, node);
                }
                else
                {
                    pParentNode.Nodes.Add(node);
                }
                this.InsertBaseMapLayerToTree(pMap, (pLayer as IBasemapSubLayer).Layer, node, false);
            }
            else if (pLayer is ICompositeLayer)
            {
                node = new TOCTreeNode(pLayer.Name, true, true)
                {
                    Checked = pLayer.Visible,
                    Tag = pLayer,
                    TOCNodeType = NodeType.Layer
                };
                if (IsBegin)
                {
                    pParentNode.Nodes.Insert(0, node);
                }
                else
                {
                    pParentNode.Nodes.Add(node);
                }
                layer = pLayer as ICompositeLayer;
                for (num = 0; num < layer.Count; num++)
                {
                    this.InsertLayerToTree(pMap, layer.get_Layer(num), node, false);
                }
            }
            else
            {
                node = new TOCTreeNode(pLayer.Name, true, true)
                {
                    Checked = pLayer.Visible,
                    Tag = pLayer
                };
                if (IsBegin)
                {
                    pParentNode.Nodes.Insert(0, node);
                }
                else
                {
                    pParentNode.Nodes.Add(node);
                }
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
                    pNode = new TOCTreeNode(group.Heading)
                    {
                        Tag = group
                    };
                    pParantNode.Nodes.Add(pNode);
                }
                int classCount = group.ClassCount;
                for (int j = 0; j < classCount; j++)
                {
                    ILegendClass class2 = group.get_Class(j);
                    node2 = new TOCTreeNode(class2.Label, false, true)
                    {
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
                Bitmap bitmap =
                    new Bitmap(
                        base.GetType()
                            .Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                pNode.Image = bitmap;
                pNode.Tag = frame;
                pNode.TOCNodeType = NodeType.MapFrame;
                pParentNodes.Add(pNode);
                if (pAV.FocusMap == frame.Map)
                {
                    base.m_FocusMapNode = pNode;
                    base.m_FocusMap = frame.Map as IBasicMap;
                    base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                    this.m_pTOCTreeView.m_FocusMap = frame.Map as IBasicMap;
                    this.m_pTOCTreeView.m_FocusMapNode = pNode;
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
                        this.InsertLayerToTree(pMap, pLayer, pParentNode, false);
                    }
                }
                else
                {
                    this.InsertLayerToTree(pMap, pLayer, pParentNode, false);
                }
            }
        }

        private void m_barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_pTOCTreeView.SelectedNodes.Count != 0)
            {
                double mapScale = 0.0;
                TOCTreeNode pTOCNode = this.m_pTOCTreeView.SelectedNodes[0] as TOCTreeNode;
                if (pTOCNode != null)
                {
                    TOCTreeNode node2;
                    if ((pTOCNode.TOCNodeType == NodeType.Map) || (pTOCNode.TOCNodeType == NodeType.MapFrame))
                    {
                        node2 = pTOCNode;
                    }
                    else
                    {
                        node2 = this.FindMapNodeByNode(pTOCNode);
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
        }

        private void m_ipPageLayout_FocusMapChanged()
        {
            try
            {
                if (this.m_pConnectActiveEvent != null)
                {
                    this.m_pConnectActiveEvent.ItemAdded -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(
                        this.m_pConnectActiveEvent_ItemAdded));
                    this.m_pConnectActiveEvent.ItemDeleted -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(
                        this.m_pConnectActiveEvent_ItemDeleted));
                    this.m_pConnectActiveEvent.ItemReordered -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(
                        this.m_pConnectActiveEvent_ItemReordered));
                    this.m_pConnectActiveEvent.ContentsCleared -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(
                        this.m_pConnectActiveEvent_ContentsCleared));
                }
            }
            catch
            {
            }
            IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
            if (this.m_pConnectActiveEvent != null)
            {
                this.m_pConnectActiveEvent.ItemAdded +=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                this.m_pConnectActiveEvent.ItemDeleted +=
                (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(
                    this.m_pConnectActiveEvent_ItemDeleted));
                this.m_pConnectActiveEvent.ItemReordered +=
                (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(
                    this.m_pConnectActiveEvent_ItemReordered));
                this.m_pConnectActiveEvent.ContentsCleared +=
                (new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(
                    this.m_pConnectActiveEvent_ContentsCleared));
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
                        this.m_pTOCTreeView.Nodes.Remove(pNode);
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
                        (this.m_pInMapCtrl.Map as IActiveView).Extent =
                            (this.m_pInMapCtrl.Map as IActiveView).FullExtent;
                    }
                    this.m_pInMapCtrl.ActiveView.Refresh();
                }
                this.m_pTOCTreeView.Invalidate();
            }
        }

        private void m_pConnectActiveEvent_ItemReordered(object Item, int toIndex)
        {
            if (this.m_CanDo)
            {
                this.RefreshTree();
            }
        }

        private void m_pLayerPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
        }

        private void m_pLayerPopupMenu_BeforePopup(object sender, EventArgs e)
        {
        }

        private void m_pTOCTreeView_AfterSelect(TOCTreeNode pSelectNode)
        {
            this.BulidMenu(this.m_pLayerPopupMenu);
            if (pSelectNode == null)
            {
                if (this.m_pCurrentLayer != null)
                {
                    this.m_pCurrentLayer = null;
                    if (this.CurrentLayerChanged != null)
                    {
                        this.CurrentLayerChanged(this, new EventArgs());
                    }
                }
            }
            else if (pSelectNode.Tag is ILayer)
            {
                if (this.m_pCurrentLayer != pSelectNode.Tag)
                {
                    this.m_pCurrentLayer = pSelectNode.Tag as ILayer;
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
        }

        private void m_pTOCTreeView_Disposed(object sender, EventArgs e)
        {
            TreeViewEvent.ChildLayersDeleted -=
                new TreeViewEvent.ChildLayersDeletedHandler(this.TreeViewEvent_ChildLayersDeleted);
            TreeViewEvent.GroupLayerAddLayerChanged -=
                new TreeViewEvent.GroupLayerAddLayerChangedHandler(this.TreeViewEvent_GroupLayerAddLayerChanged);
            TreeViewEvent.LayerPropertyChanged -=
                new TreeViewEvent.LayerPropertyChangedHandler(this.TreeViewEvent_LayerPropertyChanged);
            TreeViewEvent.LayerVisibleChanged -=
                new TreeViewEvent.LayerVisibleChangedHandler(this.TreeViewEvent_LayerVisibleChanged);
            TreeViewEvent.LayerNameChanged -=
                new TreeViewEvent.LayerNameChangedHandler(this.TreeViewEvent_LayerNameChanged);
            TreeViewEvent.MapNameChanged -= new TreeViewEvent.MapNameChangedHandler(this.TreeViewEvent_MapNameChanged);
        }

        private void m_pTOCTreeView_NodeReordered(TOCTreeNode pFirstOldParent, TOCTreeNode FirstNode,
            TOCTreeNode pLastOldParent, TOCTreeNode LastNode)
        {
            TreeViewEvent.OnLayerOrderChanged(this);
        }

        private void m_pTOCTreeView_NodeReordering(TOCTreeNode FirstNode, TOCTreeNode LastNode)
        {
            if (((FirstNode.GetNodeType() != NodeType.MapFrame) || (LastNode.GetNodeType() != NodeType.MapFrame)) &&
                ((FirstNode.GetNodeType() != NodeType.Map) || (LastNode.GetNodeType() != NodeType.Map)))
            {
                Exception exception;
                this.m_CanDo = false;
                try
                {
                    TOCTreeNode node2;
                    TOCTreeNode node3;
                    if (((FirstNode.GetNodeType() == NodeType.Layer) && (LastNode.GetNodeType() == NodeType.Layer)) ||
                        ((((FirstNode.GetNodeType() == NodeType.GroupLayer) &&
                           (LastNode.GetNodeType() != NodeType.GroupLayer)) && (LastNode.GetNodeType() != NodeType.Map)) &&
                         (LastNode.GetNodeType() != NodeType.MapFrame)))
                    {
                        int layerIndexInMap;
                        if ((LastNode.Parent.GetNodeType() == NodeType.Map) ||
                            (LastNode.Parent.GetNodeType() == NodeType.MapFrame))
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
                                        (FirstNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
                                    }
                                    else if (FirstNode.Parent.Tag is IScene)
                                    {
                                        (FirstNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
                                    }
                                    if (FirstNode.Parent.Tag is IMap)
                                    {
                                        (FirstNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
                                    }
                                    else if (FirstNode.Parent.Tag is IScene)
                                    {
                                        (FirstNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
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
                                            (LastNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer,
                                                layerIndexInMap);
                                        }
                                        else if (LastNode.Parent.Tag is IScene)
                                        {
                                            (LastNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer,
                                                layerIndexInMap);
                                        }
                                        (LastNode.Parent.Tag as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                    }
                                    else
                                    {
                                        (LastNode.Parent.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                        (LastNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
                                        ((LastNode.Parent.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
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
                                    (FirstNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer,
                                        layerIndexInMap);
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
                                            (LastNode.Parent.Tag as IMap).MoveLayer(FirstNode.Tag as ILayer,
                                                layerIndexInMap);
                                        }
                                        else if (LastNode.Parent.Tag is IScene)
                                        {
                                            (LastNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer,
                                                layerIndexInMap);
                                        }
                                        (LastNode.Parent.Tag as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                    }
                                    else
                                    {
                                        (LastNode.Parent.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                        (LastNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
                                        ((LastNode.Parent.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
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
                                    (node.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        FirstNode.Parent.Tag, null);
                                }
                                else
                                {
                                    ((node.Tag as IMapFrame) as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        FirstNode.Parent.Tag, null);
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
                                        (LastNode.Parent.Tag as IScene).MoveLayer(FirstNode.Tag as ILayer,
                                            layerIndexInMap);
                                    }
                                    (LastNode.Parent.Tag as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                else
                                {
                                    (LastNode.Parent.Tag as IMapFrame).Map.AddLayer(FirstNode.Tag as ILayer);
                                    (LastNode.Parent.Tag as IMapFrame).Map.MoveLayer(FirstNode.Tag as ILayer,
                                        layerIndexInMap);
                                    ((LastNode.Parent.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
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
                            layerIndexInMap = this.GetLayerIndexInGroupLayer(LastNode.Parent.Tag as IGroupLayer,
                                LastNode.Tag as ILayer);
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
                                this.MoveLayerTo(LastNode.Parent.Tag as IGroupLayer, FirstNode.Tag as ILayer,
                                    layerIndexInMap);
                                (FirstNode.Parent.Tag as IActiveView).Refresh();
                                if (node2 != node3)
                                {
                                    if (node3.Tag is IBasicMap)
                                    {
                                        (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                            LastNode.Parent.Tag, null);
                                    }
                                    else
                                    {
                                        ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                    {
                                        this.m_pInMapCtrl.ActiveView.PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
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
                                this.MoveLayerTo(LastNode.Parent.Tag as IGroupLayer, FirstNode.Tag as ILayer,
                                    layerIndexInMap);
                                ((FirstNode.Parent.Tag as IMapFrame).Map as IActiveView).Refresh();
                                if (node2 != node3)
                                {
                                    if (node3.Tag is IBasicMap)
                                    {
                                        (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                            LastNode.Parent.Tag, null);
                                    }
                                    else
                                    {
                                        ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                    if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                    {
                                        this.m_pInMapCtrl.ActiveView.PartialRefresh(
                                            esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                    }
                                }
                            }
                            else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                            {
                                (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                                if (node2.Tag is IBasicMap)
                                {
                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        FirstNode.Parent.Tag, null);
                                }
                                else
                                {
                                    ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node2))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        FirstNode.Parent.Tag, null);
                                }
                                (LastNode.Parent.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                                this.MoveLayerTo(LastNode.Parent.Tag as IGroupLayer, FirstNode.Tag as ILayer,
                                    layerIndexInMap);
                                if (node3.Tag is IBasicMap)
                                {
                                    (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        LastNode.Parent.Tag, null);
                                }
                                else
                                {
                                    ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, LastNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        LastNode.Parent.Tag, null);
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
                                    (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        LastNode.Tag, null);
                                }
                                else
                                {
                                    ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        LastNode.Tag, null);
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
                                    (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        LastNode.Tag, null);
                                }
                                else
                                {
                                    ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        LastNode.Tag, null);
                                }
                            }
                        }
                        else if (FirstNode.Parent.GetNodeType() == NodeType.GroupLayer)
                        {
                            (FirstNode.Parent.Tag as IGroupLayer).Delete(FirstNode.Tag as ILayer);
                            if (node2.Tag is IBasicMap)
                            {
                                (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                    FirstNode.Parent.Tag, null);
                            }
                            else
                            {
                                ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                    esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                            }
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node2))
                            {
                                this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                    FirstNode.Parent.Tag, null);
                            }
                            (LastNode.Tag as IGroupLayer).Add(FirstNode.Tag as ILayer);
                            this.MoveLayerTo(LastNode.Tag as IGroupLayer, FirstNode.Tag as ILayer, 0);
                            if (node3.Tag is IBasicMap)
                            {
                                (node3.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                    LastNode.Tag, null);
                            }
                            else
                            {
                                ((node3.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                    esriViewDrawPhase.esriViewGeography, LastNode.Tag, null);
                            }
                            if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node3))
                            {
                                this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                    LastNode.Tag, null);
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
                                    (node2.Tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        FirstNode.Parent.Tag, null);
                                }
                                else if (node2.Tag is IMapFrame)
                                {
                                    ((node2.Tag as IMapFrame).Map as IActiveView).PartialRefresh(
                                        esriViewDrawPhase.esriViewGeography, FirstNode.Parent.Tag, null);
                                }
                                if ((this.m_pInMapCtrl != null) && (base.m_FocusMapNode == node2))
                                {
                                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                        FirstNode.Parent.Tag, null);
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

        private void pBarItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag is ICommand)
            {
                (e.Item.Tag as ICommand).OnClick();
            }
        }

        public void RefreshNode(TOCTreeNode pNode)
        {
            if (pNode != null)
            {
                pNode.Nodes.Clear();
                if (pNode.Tag is IBasicMap)
                {
                    this.InsertMapToTree(pNode.Tag as IBasicMap, pNode);
                }
                else if (pNode.Tag is IMapFrame)
                {
                    this.InsertMapToTree((pNode.Tag as IMapFrame).Map as IBasicMap, pNode);
                }
                else if (pNode.Tag is IGroupLayer)
                {
                    ICompositeLayer tag = pNode.Tag as ICompositeLayer;
                    for (int i = 0; i < tag.Count; i++)
                    {
                        this.InsertLayerToTree(null, tag.get_Layer(i), pNode, false);
                    }
                }
                else if (pNode.Tag is ILayer)
                {
                    this.InsertLayerToTree(pNode.Tag as ILayer, pNode);
                }
                this.m_pTOCTreeView.ExpandAll();
                this.m_pTOCTreeView.Calculate();
                this.m_pTOCTreeView.SetScroll();
                this.m_pTOCTreeView.Invalidate(this.m_pTOCTreeView.ClientRectangle);
            }
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
                        this.m_pTOCTreeView.m_FocusMap = this.m_pMapCtrl.Map as IBasicMap;
                        this.m_pTOCTreeView.m_FocusMapNode = node;
                        bitmap =
                            new Bitmap(
                                base.GetType()
                                    .Assembly.GetManifestResourceStream(
                                        "Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                        node.Image = bitmap;
                        node.Tag = this.m_pMapCtrl.Map;
                        this.m_pTOCTreeView.Nodes.Add(node);
                        this.InsertMapToTree(this.m_pMapCtrl.Map as IBasicMap, node);
                    }
                    else if (this.m_pSceneControl != null)
                    {
                        name = "场景视图";
                        node = new TOCTreeNode(name, false, true);
                        base.m_FocusMapNode = node;
                        base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                        base.m_FocusMap = this.m_pSceneControl.Scene as IBasicMap;
                        this.m_pTOCTreeView.m_FocusMap = this.m_pSceneControl.Scene as IBasicMap;
                        this.m_pTOCTreeView.m_FocusMapNode = node;
                        bitmap =
                            new Bitmap(
                                base.GetType()
                                    .Assembly.GetManifestResourceStream(
                                        "Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                        node.Image = bitmap;
                        node.Tag = this.m_pSceneControl.Scene;
                        this.m_pTOCTreeView.Nodes.Add(node);
                        this.InsertMapToTree(this.m_pSceneControl.Scene as IBasicMap, node);
                    }
                    else if (this.m_pGlobeControl != null)
                    {
                        name = "Globe视图";
                        node = new TOCTreeNode(name, false, true);
                        base.m_FocusMapNode = node;
                        base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                        base.m_FocusMap = this.m_pGlobeControl.Globe as IBasicMap;
                        this.m_pTOCTreeView.m_FocusMap = this.m_pGlobeControl.Globe as IBasicMap;
                        this.m_pTOCTreeView.m_FocusMapNode = node;
                        bitmap =
                            new Bitmap(
                                base.GetType()
                                    .Assembly.GetManifestResourceStream(
                                        "Yutai.ArcGIS.Controls.Controls.TOCTreeview.ayers.bmp"));
                        node.Image = bitmap;
                        node.Tag = this.m_pGlobeControl.Globe;
                        this.m_pTOCTreeView.Nodes.Add(node);
                        this.InsertMapToTree(this.m_pGlobeControl.Globe as IBasicMap, node);
                    }
                }
                this.m_pTOCTreeView.ExpandAll();
                this.m_pTOCTreeView.Calculate();
                this.m_pTOCTreeView.SetScroll();
                if ((this.m_pTOCTreeView.Nodes.Count == 0) && (this.m_pCurrentLayer != null))
                {
                    this.m_pCurrentLayer = null;
                    if (this.CurrentLayerChanged != null)
                    {
                        this.CurrentLayerChanged(this, new EventArgs());
                    }
                }
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
                    this.m_pConnectActiveEvent.ItemAdded -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(
                        this.m_pConnectActiveEvent_ItemAdded));
                    this.m_pConnectActiveEvent.ItemDeleted -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(
                        this.m_pConnectActiveEvent_ItemDeleted));
                    this.m_pConnectActiveEvent.ItemReordered -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemReorderedEventHandler(
                        this.m_pConnectActiveEvent_ItemReordered));
                    this.m_pConnectActiveEvent.ContentsCleared -=
                    (new ESRI.ArcGIS.Carto.IActiveViewEvents_ContentsClearedEventHandler(
                        this.m_pConnectActiveEvent_ContentsCleared));
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
            this.m_ipPageLayout.ItemAdded +=
                (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(this.m_ipPageLayout_ItemAdded));
            this.m_ipPageLayout.ItemDeleted +=
                (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(this.m_ipPageLayout_ItemDeleted));
            this.m_ipPageLayout.FocusMapChanged +=
                (new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(this.m_ipPageLayout_FocusMapChanged));
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

        private void TreeViewEvent_ChildLayersDeleted(object sender, object pObj)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnChildLayersDeleted(this, pObj);
                }
                else
                {
                    this.RefreshTree();
                }
            }
        }

        private void TreeViewEvent_GroupLayerAddLayerChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                this.RefreshNode(this.m_pTOCTreeView.SelectedNode);
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

        private void TreeViewEvent_LayerPropertyChanged(object sender, IBasicMap pMap, ILayer pLayer)
        {
            if (sender != this)
            {
                if (sender == this.m_pTOCTreeView)
                {
                    TreeViewEvent.OnLayerPropertyChanged(this, pMap, pLayer);
                }
                else
                {
                    this.RefreshTree();
                }
                if ((pMap == base.m_FocusMap) && (this.m_pInMapCtrl != null))
                {
                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
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
                }
                else
                {
                    this.RefreshTree();
                }
                if ((pMap == base.m_FocusMap) && (this.m_pInMapCtrl != null))
                {
                    this.m_pInMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
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

        public IApplication Application
        {
            set
            {
                this.m_pApp = value;
                (this.m_pApp as IApplicationEvents).OnMapDocumentChangedEvent +=
                    new OnMapDocumentChangedEventHandler(this.TOCTreeViewWrapEx_OnMapDocumentChangedEvent);
            }
        }

        public ILayer CurrentLayer
        {
            get { return this.m_pCurrentLayer; }
        }

        public object Hook
        {
            set
            {
                this.RemoveActiveEvent();
                try
                {
                    this.m_pTOCTreeView.Hook = value;
                    this.m_pMapCtrl = null;
                    this.m_pPageLayoutCtrl = null;
                    this.m_pSceneControl = null;
                    this.m_pGlobeControl = null;
                    if (value != null)
                    {
                        if (value is IMapControl2)
                        {
                            this.m_pMapCtrl = value as IMapControl2;
                            (this.m_pMapCtrl as IMapControlEvents2_Event).OnMapReplaced +=
                                (new IMapControlEvents2_OnMapReplacedEventHandler(this.mapctrl_OnMapReplaced));
                            if (this.m_pConnectActiveEvent != null)
                            {
                                this.m_pConnectActiveEvent = null;
                            }
                            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pMapCtrl.Map;
                        }
                        else if (value is IPageLayoutControl2)
                        {
                            this.m_pPageLayoutCtrl = value as IPageLayoutControl2;
                            (this.m_pPageLayoutCtrl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced +=
                            (new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(
                                this.TOCTreeViewWrap_OnPageLayoutReplaced));
                            IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
                            this.m_pConnectActiveEvent = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) activeView.FocusMap;
                            this.m_ipPageLayout =
                                (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pPageLayoutCtrl.PageLayout;
                            this.m_ipPageLayout.ItemAdded +=
                            (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(
                                this.m_ipPageLayout_ItemAdded));
                            this.m_ipPageLayout.ItemDeleted +=
                            (new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(
                                this.m_ipPageLayout_ItemDeleted));
                            this.m_ipPageLayout.FocusMapChanged +=
                            (new ESRI.ArcGIS.Carto.IActiveViewEvents_FocusMapChangedEventHandler(
                                this.m_ipPageLayout_FocusMapChanged));
                        }
                        else if (value is ISceneControlDefault)
                        {
                            this.m_pSceneControl = value as ISceneControlDefault;
                            (this.m_pSceneControl as ISceneControlEvents_Event).OnSceneReplaced +=
                            (new ISceneControlEvents_OnSceneReplacedEventHandler(
                                this.TOCTreeViewWrap_OnSceneReplaced));
                            this.m_pConnectActiveEvent =
                                (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pSceneControl.Scene;
                        }
                        else if (value is IGlobeControlDefault)
                        {
                            this.m_pGlobeControl = value as IGlobeControlDefault;
                            (this.m_pGlobeControl as IGlobeControlEvents_Event).OnGlobeReplaced +=
                            (new IGlobeControlEvents_OnGlobeReplacedEventHandler(
                                this.TOCTreeViewWrap_OnGlobeReplaced));
                            this.m_pConnectActiveEvent =
                                (ESRI.ArcGIS.Carto.IActiveViewEvents_Event) this.m_pGlobeControl.Globe;
                        }
                        this.AddActiveEvent();
                    }
                }
                catch (Exception)
                {
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