using System;
using System.Drawing;
using System.IO;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Controls.Controls.Export;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class LayerConfigTreeView : TreeViewWrapBase
    {
        private BarButtonItem AddLayer;
        private BarButtonItem ApplyRenderProject;
        private BarButtonItem ClearScaleRange;
        private BarButtonItem DeleteAllLayer;
        private BarButtonItem DeleteLayer;
        private BarButtonItem ExportLayer;
        private BarButtonItem ExportMap;
        private BarButtonItem LabelFeature;
        private BarButtonItem LayerProperty;
        private BarManager m_barManager1 = null;
        private bool m_CanDo = true;
        private IActiveViewEvents_Event m_ipPageLayout = null;
        private IActiveViewEvents_Event m_pConnectActiveEvent = null;
        private IMapControl2 m_pInMapCtrl = null;
        private ITable m_pLayerConfigTable = null;
        private PopupMenu m_pLayerPopupMenu = null;
        protected IMapControl2 m_pMapCtrl = null;
        protected IPageLayoutControl2 m_pPageLayoutCtrl = null;
        private IStyleGallery m_pSG = null;
        private TOCTreeView m_pTOCTreeView = null;
        private IWorkspace m_pWorkspace = null;
        private BarButtonItem MapFrameProperty;
        private BarButtonItem NewGroupLayer;
        private BarButtonItem OpenAttributeTable;
        private BarButtonItem SaveRenderProject;
        private BarButtonItem SetMaximumScale;
        private BarButtonItem SetMinimumScale;
        private BarSubItem SetVisibleScale;
        private BarButtonItem ZoomToLayer;

        public LayerConfigTreeView(TOCTreeView pTOCTreeView)
        {
            this.m_pTOCTreeView = pTOCTreeView;
            this.m_pTOCTreeView.CanDrag = false;
            this.m_pTOCTreeView.NodeReordering +=
                new TOCTreeView.NodeReorderingEventHandler(this.m_pTOCTreeView_NodeReordering);
            this.m_pTOCTreeView.NodeReordered +=
                new TOCTreeView.NodeReorderedEventHandler(this.m_pTOCTreeView_NodeReordered);
            this.m_pTOCTreeView.AfterSelect += new TOCTreeView.AfterSelectEventHandler(this.m_pTOCTreeView_AfterSelect);
            this.m_pTOCTreeView.StyleGallery = this.m_pSG;
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
            TreeViewEvent.LayerOrderChanged +=
                new TreeViewEvent.LayerOrderChangedHandler(this.TreeViewEvent_LayerOrderChanged);
            this.m_barManager1 = this.m_pTOCTreeView.BarManager as BarManager;
            this.CreateMenu(this.m_barManager1);
            this.m_pLayerPopupMenu = this.m_pTOCTreeView.PopupMenu as PopupMenu;
            this.m_barManager1.ItemClick += new ItemClickEventHandler(this.m_barManager1_ItemClick);
            this.m_pTOCTreeView.Disposed += new EventHandler(this.m_pTOCTreeView_Disposed);
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
            LayerPopupMenu.ClearLinks();
            if (pNode != null)
            {
                if (pNode.TOCNodeType == NodeType.Layer)
                {
                    if (pNode.Tag is ITopologyLayer)
                    {
                        return false;
                    }
                    LayerPopupMenu.AddItem(this.OpenAttributeTable);
                    LayerPopupMenu.AddItem(this.ZoomToLayer).BeginGroup = true;
                    LayerPopupMenu.AddItem(this.SetVisibleScale);
                    LayerPopupMenu.AddItem(this.LabelFeature).BeginGroup = true;
                    LayerPopupMenu.AddItem(this.ExportLayer).BeginGroup = true;
                    LayerPopupMenu.AddItem(this.LayerProperty).BeginGroup = true;
                    return true;
                }
                if (pNode.TOCNodeType == NodeType.GroupLayer)
                {
                    LayerPopupMenu.AddItem(this.ZoomToLayer).BeginGroup = true;
                    LayerPopupMenu.AddItem(this.SetVisibleScale);
                    LayerPopupMenu.AddItem(this.ExportLayer).BeginGroup = true;
                    LayerPopupMenu.AddItem(this.LayerProperty).BeginGroup = true;
                    return true;
                }
                if (pNode.TOCNodeType == NodeType.Table)
                {
                    LayerPopupMenu.AddItem(this.OpenAttributeTable);
                    return true;
                }
                if (pNode.TOCNodeType == NodeType.Map)
                {
                    LayerPopupMenu.AddItem(this.ExportMap).BeginGroup = true;
                    LayerPopupMenu.AddItem(this.MapFrameProperty).BeginGroup = true;
                    return true;
                }
            }
            return false;
        }

        private void CreateMenu(BarManager barManager1)
        {
            this.ZoomToLayer = new BarButtonItem();
            this.SetVisibleScale = new BarSubItem();
            this.SetMinimumScale = new BarButtonItem();
            this.SetMaximumScale = new BarButtonItem();
            this.ClearScaleRange = new BarButtonItem();
            this.LabelFeature = new BarButtonItem();
            this.AddLayer = new BarButtonItem();
            this.DeleteLayer = new BarButtonItem();
            this.LayerProperty = new BarButtonItem();
            this.ExportLayer = new BarButtonItem();
            this.NewGroupLayer = new BarButtonItem();
            this.MapFrameProperty = new BarButtonItem();
            this.ExportMap = new BarButtonItem();
            this.OpenAttributeTable = new BarButtonItem();
            this.DeleteAllLayer = new BarButtonItem();
            this.SaveRenderProject = new BarButtonItem();
            this.ApplyRenderProject = new BarButtonItem();
            barManager1.Items.AddRange(new BarItem[]
            {
                this.ZoomToLayer, this.SetVisibleScale, this.SetMinimumScale, this.SetMaximumScale, this.ClearScaleRange,
                this.LabelFeature, this.AddLayer, this.DeleteLayer, this.LayerProperty, this.ExportLayer,
                this.NewGroupLayer, this.MapFrameProperty, this.ExportMap, this.OpenAttributeTable, this.DeleteAllLayer,
                this.SaveRenderProject,
                this.ApplyRenderProject
            });
            this.ZoomToLayer.Caption = "缩放到图层";
            this.ZoomToLayer.Id = 0;
            this.ZoomToLayer.Name = "ZoomToLayer";
            this.SetVisibleScale.Caption = "可见比例尺范围";
            this.SetVisibleScale.Id = 1;
            this.SetVisibleScale.LinksPersistInfo.AddRange(new LinkPersistInfo[]
            {
                new LinkPersistInfo(this.SetMinimumScale), new LinkPersistInfo(this.SetMaximumScale),
                new LinkPersistInfo(this.ClearScaleRange)
            });
            this.SetVisibleScale.Name = "SetVisibleScale";
            this.SetMinimumScale.Caption = "设置最小范围";
            this.SetMinimumScale.Id = 2;
            this.SetMinimumScale.Name = "SetMinimumScale";
            this.SetMaximumScale.Caption = "设置最大范围";
            this.SetMaximumScale.Id = 3;
            this.SetMaximumScale.Name = "SetMaximumScale";
            this.ClearScaleRange.Caption = "清除比例范围";
            this.ClearScaleRange.Id = 4;
            this.ClearScaleRange.Name = "ClearScaleRange";
            this.LabelFeature.Caption = "标注要素";
            this.LabelFeature.Id = 5;
            this.LabelFeature.Name = "LabelFeature";
            this.AddLayer.Caption = "添加图层";
            this.AddLayer.Id = 6;
            this.AddLayer.Name = "AddLayer";
            this.DeleteLayer.Caption = "删除图层";
            this.DeleteLayer.Id = 7;
            this.DeleteLayer.Name = "DeleteLayer";
            this.LayerProperty.Caption = "图层属性";
            this.LayerProperty.Id = 8;
            this.LayerProperty.Name = "LayerProperty";
            this.ExportLayer.Caption = "导出图层为图像";
            this.ExportLayer.Id = 9;
            this.ExportLayer.Name = "ExportLayer";
            this.NewGroupLayer.Caption = "新建组合图层";
            this.NewGroupLayer.Id = 10;
            this.NewGroupLayer.Name = "NewGroupLayer";
            this.MapFrameProperty.Caption = "属性";
            this.MapFrameProperty.Id = 11;
            this.MapFrameProperty.Name = "MapFrameProperty";
            this.ExportMap.Caption = "输出地图";
            this.ExportMap.Id = 12;
            this.ExportMap.Name = "ExportMap";
            this.OpenAttributeTable.Caption = "打开属性表";
            this.OpenAttributeTable.Id = 13;
            this.OpenAttributeTable.Name = "OpenAttributeTable";
            this.DeleteAllLayer.Caption = "删除所有图层";
            this.DeleteAllLayer.Id = 15;
            this.DeleteAllLayer.Name = "DeleteAllLayer";
            this.DeleteAllLayer.ItemClick += new ItemClickEventHandler(this.DeleteAllLayer_ItemClick);
            this.SaveRenderProject.Caption = "保存渲染方案";
            this.SaveRenderProject.Id = 16;
            this.SaveRenderProject.Name = "SaveRenderProject";
            this.SaveRenderProject.ItemClick += new ItemClickEventHandler(this.SaveRenderProject_ItemClick);
            this.ApplyRenderProject.Caption = "应用渲染方案";
            this.ApplyRenderProject.Id = 17;
            this.ApplyRenderProject.Name = "ApplyRenderProject";
            this.ApplyRenderProject.ItemClick += new ItemClickEventHandler(this.ApplyRenderProject_ItemClick);
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
            TOCTreeNode node2 = this.FindLayerNodeContain(pSelNode);
            if (node2.TOCNodeType == NodeType.GroupLayer)
            {
                IGroupLayer data = node2.Tag as IGroupLayer;
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
                    tag.SpatialReference = new UnknownCoordinateSystemClass();
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

        private void DoLayerProperty(TOCTreeNode pSelNode)
        {
        }

        private void DoMapFrameProperty(TOCTreeNode pSelNode)
        {
            IBasicMap tag;
            if (pSelNode.TOCNodeType == NodeType.Map)
            {
                tag = pSelNode.Tag as IBasicMap;
            }
            else if (pSelNode.TOCNodeType == NodeType.MapFrame)
            {
                tag = (pSelNode.Tag as IMapFrame).Map as IBasicMap;
            }
            else
            {
                TOCTreeNode node = this.FindMapNodeByNode(pSelNode);
                if (node.TOCNodeType == NodeType.Map)
                {
                    tag = node.Tag as IBasicMap;
                }
                else
                {
                    tag = (node.Tag as IMapFrame).Map as IBasicMap;
                }
            }
            if (tag == null)
            {
            }
        }

        private void DoNewGroupLayer(TOCTreeNode pSelNode)
        {
            IGroupLayer layer = new GroupLayerClass
            {
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

        private TOCTreeNode FindItem(TOCTreeNode pRootItem, int OID)
        {
            for (int i = 0; i < pRootItem.Nodes.Count; i++)
            {
                TOCTreeNodeEx ex = pRootItem.Nodes[i] as TOCTreeNodeEx;
                if ((ex != null) && (ex.OID == OID))
                {
                    return ex;
                }
                TOCTreeNode node = this.FindItem(ex, OID);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        private TOCTreeNode FindItem(TOCTreeNode pRootItem, string name)
        {
            for (int i = 0; i < pRootItem.Nodes.Count; i++)
            {
                TOCTreeNode node = pRootItem.Nodes[i] as TOCTreeNode;
                if (node.Text == name)
                {
                    return node;
                }
                TOCTreeNode node2 = this.FindItem(node, name);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
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
                if (((pTOCNode.Parent.TOCNodeType == NodeType.Map) || (pTOCNode.Parent.TOCNodeType == NodeType.MapFrame)) ||
                    (pTOCNode.Parent.TOCNodeType == NodeType.GroupLayer))
                {
                    return pTOCNode.Parent;
                }
                return this.FindMapNodeByNode(pTOCNode.Parent);
            }
            return null;
        }

        private TOCTreeNode FindMapNodeByMap(IBasicMap pMap)
        {
            for (int i = 0; i < this.m_pTOCTreeView.Nodes.Count; i++)
            {
                TOCTreeNode node = this.m_pTOCTreeView.Nodes[i] as TOCTreeNode;
                if (node.Tag is IBasicMap)
                {
                    if (node.Tag == pMap)
                    {
                        return node;
                    }
                }
                else if ((node.Tag is IMapFrame) && ((node.Tag as IMapFrame).Map == pMap))
                {
                    return node;
                }
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

        private TOCTreeNode GetParentItem(ILayer pLayer, TOCTreeNode pMapItem, ref int LayerOID)
        {
            if (this.m_pLayerConfigTable != null)
            {
                IQueryFilter queryFilter = new QueryFilterClass
                {
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

        private TOCTreeNode GetWorkspaceTreeNode(IBasicMap pMap, IWorkspace pWorkspace)
        {
            IPropertySet connectionProperties = pWorkspace.ConnectionProperties;
            for (int i = 0; i < this.m_pTOCTreeView.Nodes.Count; i++)
            {
                TOCTreeNode node = this.m_pTOCTreeView.Nodes[i] as TOCTreeNode;
                if ((node.Tag is IBasicMap) && (node.Tag == pMap))
                {
                    if (node.Nodes.Count > 0)
                    {
                        for (int j = 0; j < node.Nodes.Count; j++)
                        {
                            TOCTreeNode node2 = node.Nodes[j] as TOCTreeNode;
                            IWorkspace tag = node2.Tag as IWorkspace;
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
                    node2 = new TOCTreeNodeEx(name)
                    {
                        TOCNodeType = NodeType.Folder
                    };
                    (node2 as TOCTreeNodeEx).OID = ChildOID;
                    node.Nodes.Add(node2);
                    return node2;
                }
                node2 = new TOCTreeNodeEx(name)
                {
                    TOCNodeType = NodeType.Folder
                };
                (node2 as TOCTreeNodeEx).OID = ChildOID;
                pParentItem.Nodes.Add(node2);
                return node2;
            }
            return null;
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
                int layerOID = 0;
                TOCTreeNode node = this.GetParentItem(pLayer, pMapNode, ref layerOID);
                if (node == null)
                {
                    node = pMapNode;
                }
                TOCTreeNode pNode = new TOCTreeNodeEx(pLayer.Name, true, true);
                (pNode as TOCTreeNodeEx).OID = layerOID;
                pNode.Checked = pLayer.Visible;
                pNode.Tag = pLayer;
                node.Nodes.Add(pNode);
                if (pLayer is ITinLayer)
                {
                    ITinLayer layer = pLayer as ITinLayer;
                    for (num = 0; num < layer.RendererCount; num++)
                    {
                        ITinRenderer renderer = layer.GetRenderer(num);
                        TOCTreeNode node3 = new TOCTreeNodeEx(renderer.Name)
                        {
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
                    pNode = new TOCTreeNodeEx(group.Heading)
                    {
                        Tag = group
                    };
                    pParantNode.Nodes.Add(pNode);
                }
                int classCount = group.ClassCount;
                for (int j = 0; j < classCount; j++)
                {
                    ILegendClass class2 = group.get_Class(j);
                    node2 = new TOCTreeNodeEx(class2.Label, false, true)
                    {
                        Tag = class2
                    };
                    pNode.Nodes.Add(node2);
                }
            }
        }

        private void InsertMapFrameToTree(IElement pElement, TOCTreeNodeCollection pParantNodes)
        {
            if (pElement is IMapFrame)
            {
                IMapFrame frame = (IMapFrame) pElement;
                string name = frame.Map.Name;
                if (name == "")
                {
                    name = "Scene";
                }
                TOCTreeNode pNode = new TOCTreeNodeEx(name, false, true);
                Bitmap bitmap =
                    new Bitmap(
                        base.GetType()
                            .Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.layers.bmp"));
                pNode.Image = bitmap;
                pNode.Tag = frame;
                pParantNodes.Add(pNode);
                this.InsertMapToTree((IBasicMap) frame.Map, pNode);
            }
            else if (pElement is IGroupElement)
            {
                IGroupElement element = (IGroupElement) pElement;
                IEnumElement elements = element.Elements;
                elements.Reset();
                for (IElement element3 = elements.Next(); element3 != null; element3 = elements.Next())
                {
                    this.InsertMapFrameToTree(element3, pParantNodes);
                }
            }
        }

        private void InsertMapToTree(IBasicMap pMap, TOCTreeNode pMapNode)
        {
            int layerCount = pMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer pLayer = pMap.get_Layer(i);
                this.InsertLayerToTree(pMap, pLayer, pMapNode);
            }
        }

        private bool IsEqual(IWorkspace pWorkspace1, IWorkspace pWorkspace2)
        {
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
                        if (tag is IMap)
                        {
                            mapScale = (tag as IMap).MapScale;
                        }
                    }
                    catch
                    {
                    }
                    switch (e.Item.Name)
                    {
                        case "SetMinimumScale":
                            (selectedNode.Tag as ILayer).MinimumScale = mapScale;
                            break;

                        case "SetMaximumScale":
                            (selectedNode.Tag as ILayer).MaximumScale = mapScale;
                            break;

                        case "ClearScaleRange":
                            (selectedNode.Tag as ILayer).MinimumScale = 0.0;
                            (selectedNode.Tag as ILayer).MaximumScale = 0.0;
                            (tag as IActiveView).Refresh();
                            break;

                        case "ZoomToLayer":
                            if ((selectedNode.Tag as ILayer).AreaOfInterest != null)
                            {
                                (tag as IActiveView).Extent = (selectedNode.Tag as ILayer).AreaOfInterest;
                                (tag as IActiveView).Refresh();
                            }
                            break;

                        case "LabelFeature":
                        {
                            IGeoFeatureLayer data = selectedNode.Tag as IGeoFeatureLayer;
                            if (data != null)
                            {
                                data.DisplayAnnotation = !data.DisplayAnnotation;
                                (tag as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, data, null);
                            }
                            break;
                        }
                        case "LabelToAnno":
                        {
                            IFeatureLayer layer2 = selectedNode.Tag as IFeatureLayer;
                            if (layer2 != null)
                            {
                                IFeatureWorkspace workspace =
                                    (layer2.FeatureClass as IDataset).Workspace as IFeatureWorkspace;
                                IFDOGraphicsLayerFactory factory = new FDOGraphicsLayerFactoryClass();
                            }
                            break;
                        }
                        case "OpenAttributeTable":
                        {
                            ITable table = selectedNode.Tag as ITable;
                            if (table != null)
                            {
                                new frmAttributeTable {Table = table, Map = tag}.Show();
                            }
                            break;
                        }
                        case "AddLayer":
                            if (selectedNode.TOCNodeType != NodeType.GroupLayer)
                            {
                                if (selectedNode.TOCNodeType == NodeType.Map)
                                {
                                    this.m_pTOCTreeView.Invalidate();
                                }
                                else if (selectedNode.TOCNodeType == NodeType.MapFrame)
                                {
                                    this.m_pTOCTreeView.Invalidate();
                                }
                                break;
                            }
                            this.m_pTOCTreeView.Invalidate();
                            break;

                        case "DeleteLayer":
                            this.DoDeleteLayer(selectedNode);
                            break;

                        case "LayerProperty":
                            this.DoLayerProperty(selectedNode);
                            break;

                        case "MapFrameProperty":
                            this.DoMapFrameProperty(selectedNode);
                            break;

                        case "ExportLayer":
                        {
                            frmExportMap map2 = new frmExportMap
                            {
                                ActiveView = tag as IActiveView,
                                Layer = selectedNode.Tag as ILayer
                            };
                            map2.ShowDialog();
                            break;
                        }
                        case "ExportMap":
                            new frmExportMap {ActiveView = tag as IActiveView}.ShowDialog();
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
            this.RefreshTree();
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
        }

        private void m_pTOCTreeView_NodeReordering(TOCTreeNode FirstNode, TOCTreeNode LastNode)
        {
        }

        private IFeatureRenderer ReadRender(string filename)
        {
            System.IO.FileStream input = new System.IO.FileStream(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(input);
            int count = reader.ReadInt32();
            byte[] buffer = reader.ReadBytes(count);
            IMemoryBlobStream stream2 = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass
            {
                Stream = stream2
            };
            ((IMemoryBlobStreamVariant) stream2).ImportFromVariant(buffer);
            IPropertySet set = new PropertySetClass();
            (set as IPersistStream).Load(pstm);
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
                    IActiveView view = pageLayout as IActiveView;
                    IGraphicsContainer container = pageLayout as IGraphicsContainer;
                    container.Reset();
                    for (IElement element = container.Next(); element != null; element = container.Next())
                    {
                        this.InsertMapFrameToTree(element, this.m_pTOCTreeView.Nodes);
                    }
                }
                else if (this.m_pMapCtrl != null)
                {
                    string name = this.m_pMapCtrl.Map.Name;
                    if (name == "")
                    {
                        name = "Scene";
                    }
                    TOCTreeNode pNode = new TOCTreeNodeEx(name, false, true);
                    base.m_FocusMapNode = pNode;
                    base.m_FocusMapNode.NodeFont = new Font("Arial", 8f, FontStyle.Bold);
                    Bitmap bitmap =
                        new Bitmap(
                            base.GetType()
                                .Assembly.GetManifestResourceStream(
                                    "Yutai.ArcGIS.Controls.Controls.TOCTreeview.layers.bmp"));
                    pNode.Image = bitmap;
                    pNode.Tag = this.m_pMapCtrl.Map;
                    this.m_pTOCTreeView.Nodes.Add(pNode);
                    this.InsertMapToTree(this.m_pMapCtrl.Map as IBasicMap, pNode);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            this.m_pTOCTreeView.ExpandAll();
            this.m_pTOCTreeView.Calculate();
            this.m_pTOCTreeView.SetScroll();
            this.m_pTOCTreeView.Invalidate(this.m_pTOCTreeView.ClientRectangle);
        }

        private void SaveRender(IFeatureRenderer pRender, string filename)
        {
            object obj2;
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass
            {
                Stream = stream
            };
            IPropertySet set = new PropertySetClass();
            IPersistStream stream3 = set as IPersistStream;
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

        public object Hook
        {
            set
            {
                try
                {
                    try
                    {
                        if (this.m_pConnectActiveEvent != null)
                        {
                            this.m_pConnectActiveEvent.ItemAdded -=
                                (new IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                            this.m_pConnectActiveEvent.ItemDeleted -=
                                (new IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                            this.m_pConnectActiveEvent.ItemReordered -=
                            (new IActiveViewEvents_ItemReorderedEventHandler(
                                this.m_pConnectActiveEvent_ItemReordered));
                            this.m_pConnectActiveEvent.ContentsCleared -=
                            (new IActiveViewEvents_ContentsClearedEventHandler(
                                this.m_pConnectActiveEvent_ContentsCleared));
                        }
                    }
                    catch
                    {
                    }
                    this.m_pConnectActiveEvent = null;
                    this.m_pMapCtrl = null;
                    this.m_pPageLayoutCtrl = null;
                    if (value is IMapControl2)
                    {
                        this.m_pMapCtrl = value as IMapControl2;
                        IMapControlEvents2_Event pMapCtrl = this.m_pMapCtrl as IMapControlEvents2_Event;
                        if (this.m_pConnectActiveEvent != null)
                        {
                            this.m_pConnectActiveEvent = null;
                        }
                        this.m_pConnectActiveEvent = (IActiveViewEvents_Event) this.m_pMapCtrl.Map;
                    }
                    else if (value is IPageLayoutControl2)
                    {
                        this.m_pPageLayoutCtrl = value as IPageLayoutControl2;
                        IActiveView activeView = this.m_pPageLayoutCtrl.ActiveView;
                        this.m_pConnectActiveEvent = (IActiveViewEvents_Event) activeView.FocusMap;
                    }
                    if (this.m_pConnectActiveEvent != null)
                    {
                        this.m_pConnectActiveEvent.ItemAdded +=
                            (new IActiveViewEvents_ItemAddedEventHandler(this.m_pConnectActiveEvent_ItemAdded));
                        this.m_pConnectActiveEvent.ItemDeleted +=
                            (new IActiveViewEvents_ItemDeletedEventHandler(this.m_pConnectActiveEvent_ItemDeleted));
                        this.m_pConnectActiveEvent.ItemReordered +=
                            (new IActiveViewEvents_ItemReorderedEventHandler(this.m_pConnectActiveEvent_ItemReordered));
                        this.m_pConnectActiveEvent.ContentsCleared +=
                        (new IActiveViewEvents_ContentsClearedEventHandler(
                            this.m_pConnectActiveEvent_ContentsCleared));
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public ITable LayerConfigTable
        {
            set
            {
                this.m_pLayerConfigTable = value;
                this.m_pWorkspace = (this.m_pLayerConfigTable as IDataset).Workspace;
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