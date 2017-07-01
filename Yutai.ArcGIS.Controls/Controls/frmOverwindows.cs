using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Overview;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmOverwindows : DockContent
    {
        private bool m_bUseScale = false;
        public bool m_CanDo = false;
        private IGeometry m_ClipBounds = null;
        private OverviewLayerSettings m_layersettings = null;
        private bool m_MustOneLayer = false;
        private OverwindowsLayersType m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
        public IElement m_pElement;
        public IEnvelope m_pEnvelope = null;
        private IActiveView m_pMainAvtiveView = null;
        private IMapControl2 m_pMainMapControl = null;
        private IStyleGallery m_pSG = null;
        private bool m_UseMainActiveViewLayerCopy = true;
        private bool m_ZoomWithMainView = false;

        public frmOverwindows()
        {
            this.InitializeComponent();
        }

        private bool AddGroupLayer(IGroupLayer pGroupLayer)
        {
            for (int i = (pGroupLayer as ICompositeLayer).Count - 1; i >= 0; i--)
            {
                ILayer pLayer = (pGroupLayer as ICompositeLayer).get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    this.AddGroupLayer(pLayer as IGroupLayer);
                }
                else if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = pLayer as IFeatureLayer;
                    if (((layer2 != null) && !(layer2 is IFDOGraphicsLayer)) &&
                        (layer2.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        this.AddLayer(pLayer, 0);
                        return true;
                    }
                }
            }
            return false;
        }

        private void AddLayer()
        {
            this.axMapControl1.ClearLayers();
            for (int i = 0; i < this.m_pMainMapControl.LayerCount; i++)
            {
                ILayer pLayer = this.m_pMainMapControl.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    this.AddLayer(pLayer as IGroupLayer);
                }
                else if (this.IsNavigationLayer(pLayer))
                {
                    this.AddLayer(pLayer, this.axMapControl1.LayerCount);
                }
            }
        }

        private void AddLayer(IGroupLayer pGroupLayer)
        {
            ICompositeLayer layer = pGroupLayer as ICompositeLayer;
            for (int i = 0; i < layer.Count; i++)
            {
                ILayer pLayer = layer.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    this.AddLayer(pLayer as IGroupLayer);
                }
                else if (this.IsNavigationLayer(pLayer))
                {
                    this.AddLayer(pLayer, this.axMapControl1.LayerCount);
                }
            }
        }

        private void AddLayer(ILayer pLayer, int nIndex)
        {
            if (this.m_UseMainActiveViewLayerCopy)
            {
                IObjectCopy copy = new ObjectCopyClass();
                ILayer layer = copy.Copy(pLayer) as ILayer;
                layer.Visible = true;
                this.axMapControl1.AddLayer(layer, nIndex);
            }
            else
            {
                this.axMapControl1.AddLayer(pLayer, nIndex);
            }
        }

        private void axMapControl1_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (((e.button != 2) && (this.m_pMainAvtiveView != null)) && (this.axMapControl1.LayerCount > 0))
            {
                IEnvelope extent;
                IEnvelope envelope = this.axMapControl1.TrackRectangle();
                if ((envelope.IsEmpty || (envelope.Width == 0.0)) || (envelope.Height == 0.0))
                {
                    extent = this.m_pMainAvtiveView.Extent;
                    envelope.XMin = e.mapX - ((extent.XMax - extent.XMin)/2.0);
                    envelope.XMax = e.mapX + ((extent.XMax - extent.XMin)/2.0);
                    envelope.YMin = e.mapY - ((extent.YMax - extent.YMin)/2.0);
                    envelope.YMax = e.mapY + ((extent.YMax - extent.YMin)/2.0);
                }
                else
                {
                    extent = this.m_pMainAvtiveView.Extent;
                    double num = extent.Width/extent.Height;
                    if (envelope.Width > (envelope.Height*num))
                    {
                        envelope.YMin = envelope.YMax - (envelope.Width/num);
                    }
                    else
                    {
                        envelope.XMax = envelope.XMin + (envelope.Height*num);
                    }
                }
                if (!((this.m_pElement.Geometry == null) && this.m_pElement.Geometry.IsEmpty))
                {
                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, this.m_pElement,
                        this.m_pEnvelope);
                }
                this.m_pEnvelope = envelope;
                this.m_pElement.Geometry = envelope;
                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.m_pElement,
                    this.m_pEnvelope);
                this.m_CanDo = false;
                Cursor.Current = Cursors.WaitCursor;
                this.m_pMainAvtiveView.Extent = envelope;
                this.m_pMainAvtiveView.Refresh();
                Cursor.Current = Cursors.Default;
                this.m_CanDo = true;
            }
        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if ((e.button == 2) && (this.m_OverwindowsLayersType != OverwindowsLayersType.LayerSettings))
            {
                this.contextMenuStrip1.Show(this.axMapControl1, new System.Drawing.Point(e.x, e.y));
            }
        }

        public void DrawRectangle(IActiveView pActiveView)
        {
            if ((this.m_pElement != null) && (pActiveView.FocusMap.LayerCount > 0))
            {
                IDisplay screenDisplay = pActiveView.ScreenDisplay;
                if (!((this.m_pElement.Geometry == null) && this.m_pElement.Geometry.IsEmpty))
                {
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.m_pElement, null);
                }
                this.m_pElement.Geometry = this.m_pEnvelope;
                if (!((this.m_pElement.Geometry == null) && this.m_pElement.Geometry.IsEmpty))
                {
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.m_pElement, null);
                }
            }
        }

        private void frmOverviewWindow_ContentsChanged()
        {
            if (this.m_pMainAvtiveView.FocusMap.LayerCount == 0)
            {
                this.RestMap();
            }
            else
            {
                this.RestMap();
                this.Init();
            }
        }

        private void frmOverwindows_Load(object sender, EventArgs e)
        {
            this.axMapControl1.ShowScrollbars = false;
            this.m_pElement = new RectangleElementClass();
            this.m_pFillSymbol = new SimpleFillSymbolClass();
            ((ISimpleFillSymbol) this.m_pFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            ILineSymbol symbol = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Green = 0,
                Blue = 255
            };
            symbol.Color = color;
            symbol.Width = 1.0;
            this.m_pFillSymbol.Outline = symbol;
            IFillShapeElement pElement = this.m_pElement as IFillShapeElement;
            pElement.Symbol = this.m_pFillSymbol;
            this.axMapControl1.ActiveView.GraphicsContainer.AddElement(this.m_pElement, 0);
            this.m_CanDo = true;
            this.SetMainAvtiveView(this.m_pMainMapControl.ActiveView);
            this.Init();
        }

        private void frmOverwindows_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            if (this.m_CanDo)
            {
                this.m_pEnvelope = this.m_pMainMapControl.ActiveView.Extent;
                IPoint point = new PointClass();
                point.PutCoords((this.m_pEnvelope.XMin + this.m_pEnvelope.XMax)/2.0,
                    (this.m_pEnvelope.YMin + this.m_pEnvelope.YMax)/2.0);
                if (this.m_ZoomWithMainView)
                {
                    this.axMapControl1.ActiveView.Extent = this.m_pEnvelope.Envelope;
                    this.axMapControl1.ActiveView.Refresh();
                }
                this.DrawRectangle(this.axMapControl1.ActiveView);
            }
            if (this.m_bUseScale)
            {
                this.SetLayerVisible(MapHelper.GetMapScale(this.m_pMainMapControl.Map));
            }
        }

        private void frmOverwindows_OnViewRefreshed(object ActiveView, int viewDrawPhase, object layerOrElement,
            object envelope)
        {
        }

        private void Init()
        {
            (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced +=
                (new IMapControlEvents2_OnMapReplacedEventHandler(this.OverviewWindowCtrl_OnMapReplaced));
            (this.m_pMainMapControl as IMapControlEvents2_Event).OnViewRefreshed +=
                (new IMapControlEvents2_OnViewRefreshedEventHandler(this.frmOverwindows_OnViewRefreshed));
            (this.m_pMainMapControl as IMapControlEvents2_Event).OnExtentUpdated +=
                (new IMapControlEvents2_OnExtentUpdatedEventHandler(this.frmOverwindows_OnExtentUpdated));
            if (this.m_OverwindowsLayersType == OverwindowsLayersType.LayerSettings)
            {
                this.AddLayer();
                this.SetLayerVisible(MapHelper.GetMapScale(this.m_pMainMapControl.Map));
            }
            else
            {
                this.RestMap();
                this.LoadMap(this.m_pMainMapControl.Map);
            }
            if (this.m_pMainMapControl != null)
            {
                (this.axMapControl1.Map as IMapAdmin2).ClipBounds = this.m_pMainMapControl.Map.ClipGeometry;
            }
            this.axMapControl1.Map.ClearSelection();
        }

        private void Init1()
        {
            IFeatureLayer layer = null;
            IFeatureLayer layer3;
            for (int i = 0; i < this.m_pMainMapControl.LayerCount; i++)
            {
                ILayer layer2 = this.m_pMainMapControl.get_Layer(i);
                if (layer2 is IFeatureLayer)
                {
                    layer = layer2 as IFeatureLayer;
                    if ((layer.FeatureClass != null) &&
                        (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        layer3 = new FeatureLayerClass
                        {
                            FeatureClass = layer.FeatureClass
                        };
                        this.axMapControl1.Map.AddLayer(layer3);
                        if (this.m_ClipBounds != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                        }
                        else if (this.m_pMainMapControl.Map.ClipGeometry != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                        }
                        else
                        {
                            this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        }
                        this.axMapControl1.ActiveView.Refresh();
                        return;
                    }
                }
            }
            if (layer != null)
            {
                layer3 = new FeatureLayerClass
                {
                    FeatureClass = layer.FeatureClass
                };
                this.axMapControl1.Map.AddLayer(layer3);
                if (this.m_ClipBounds != null)
                {
                    this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                }
                else if (this.m_pMainMapControl.Map.ClipGeometry != null)
                {
                    this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                }
                else
                {
                    this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                }
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        public void InitLayerSettings()
        {
            try
            {
                this.m_layersettings = (OverviewLayerSettings) ConfigurationManager.GetSection("LayerSettings");
                if (this.m_layersettings.LayerSettings.Count == 0)
                {
                    this.m_bUseScale = false;
                    this.m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
                }
                else
                {
                    this.m_bUseScale = true;
                    if (this.m_OverwindowsLayersType != OverwindowsLayersType.LayerSettings)
                    {
                        this.m_OverwindowsLayersType = OverwindowsLayersType.LayerSettings;
                    }
                }
            }
            catch (Exception)
            {
                this.m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
                this.m_bUseScale = false;
            }
        }

        public void InitLayerSettings(string xml)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlElement documentElement = document.DocumentElement;
                OverviewWindowsLayerSettingsSectionHandler handler = new OverviewWindowsLayerSettingsSectionHandler();
                this.m_layersettings = (OverviewLayerSettings) handler.Create(null, null, documentElement.ChildNodes[0]);
                if (this.m_layersettings.LayerSettings.Count == 0)
                {
                    this.m_bUseScale = false;
                    this.m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
                }
                else
                {
                    this.m_bUseScale = true;
                    if (this.m_OverwindowsLayersType != OverwindowsLayersType.LayerSettings)
                    {
                        this.m_OverwindowsLayersType = OverwindowsLayersType.LayerSettings;
                    }
                }
            }
            catch
            {
                this.m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
                this.m_bUseScale = false;
            }
        }

        private bool IsNavigationLayer(ILayer pLayer)
        {
            return this.m_layersettings.LayerSettings.ContainsKey(pLayer.Name);
        }

        public void LoadMap(IMap pMap)
        {
            this.RestMap();
            ILayer pLayer = null;
            IObjectCopy copy = new ObjectCopyClass();
            for (int i = pMap.LayerCount - 1; i >= 0; i--)
            {
                pLayer = pMap.get_Layer(i);
                if (this.m_OverwindowsLayersType == OverwindowsLayersType.AllLayer)
                {
                    this.AddLayer(pLayer, 0);
                }
                else if (pLayer is IGroupLayer)
                {
                    if (this.AddGroupLayer(pLayer as IGroupLayer) &&
                        (this.m_OverwindowsLayersType == OverwindowsLayersType.BottomPolygonLayer))
                    {
                        break;
                    }
                }
                else if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = pLayer as IFeatureLayer;
                    if (((layer2 != null) && !(layer2 is IFDOGraphicsLayer)) &&
                        (layer2.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        this.AddLayer(pLayer, 0);
                        if (this.m_OverwindowsLayersType == OverwindowsLayersType.BottomPolygonLayer)
                        {
                            break;
                        }
                    }
                }
            }
            if (((this.m_pMainMapControl.Map.LayerCount > 0) && (this.axMapControl1.Map.LayerCount == 0)) &&
                this.m_MustOneLayer)
            {
                pLayer = this.m_pMainMapControl.Map.get_Layer(this.m_pMainMapControl.Map.LayerCount - 1);
                this.AddLayer(pLayer, 0);
            }
            this.axMapControl1.Map.RecalcFullExtent();
            if (this.m_ZoomWithMainView)
            {
                this.m_pEnvelope = this.m_pMainMapControl.ActiveView.Extent;
                IEnvelope extent = this.axMapControl1.ActiveView.Extent;
                this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                this.axMapControl1.ActiveView.Refresh();
            }
            else
            {
                if (this.m_ClipBounds != null)
                {
                    this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                }
                else if (this.m_pMainMapControl.Map.ClipGeometry != null)
                {
                    this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                }
                else
                {
                    this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                }
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        private void OverviewWindow_AfterDraw(IDisplay Display, esriViewDrawPhase phase)
        {
            if (this.m_pMainAvtiveView.FocusMap.LayerCount == 0)
            {
                if (this.axMapControl1.Map.LayerCount > 0)
                {
                    this.RestMap();
                }
            }
            else
            {
                if (!this.m_UseMainActiveViewLayerCopy)
                {
                    this.axMapControl1.ActiveView.Refresh();
                }
                this.axMapControl1.ActiveView.GraphicsContainer.Reset();
                if ((this.axMapControl1.ActiveView.GraphicsContainer.Next() == null) && (this.m_pElement != null))
                {
                    this.axMapControl1.ActiveView.GraphicsContainer.AddElement(this.m_pElement, 0);
                }
                this.DrawRectangle(this.axMapControl1.ActiveView);
            }
        }

        private void OverviewWindow_ContentsCleared()
        {
            this.RestMap();
        }

        private void OverviewWindow_ItemAdded(object Item)
        {
            if (Item is ILayer)
            {
                IFeatureLayer layer = Item as IFeatureLayer;
                if (((layer != null) && !(layer is IFDOGraphicsLayer)) &&
                    (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                {
                    this.AddLayer(Item as ILayer, 0);
                    if (this.m_ClipBounds != null)
                    {
                        this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                    }
                    else if (this.m_pMainMapControl.Map.ClipGeometry != null)
                    {
                        this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                    }
                    else
                    {
                        this.axMapControl1.Map.RecalcFullExtent();
                        this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                    }
                    this.axMapControl1.ActiveView.Refresh();
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
        }

        private void OverviewWindow_ItemDeleted(object Item)
        {
        }

        private void OverviewWindow_ItemReordered(object Item, int toIndex)
        {
            if (Item is ILayer)
            {
            }
        }

        private void OverviewWindow_SizeChanged(object sender, EventArgs e)
        {
        }

        private void OverviewWindowCtrl_OnMapReplaced(object newMap)
        {
            this.SetMainAvtiveView(this.m_pMainMapControl.ActiveView);
            this.Init();
        }

        internal void RestMap()
        {
            this.axMapControl1.Map.ClearLayers();
            this.axMapControl1.Map.SpatialReferenceLocked = false;
            this.axMapControl1.Map.SpatialReference = null;
            this.axMapControl1.Map.MapUnits = esriUnits.esriUnknownUnits;
            this.axMapControl1.Map.DistanceUnits = esriUnits.esriUnknownUnits;
            this.axMapControl1.Map.RecalcFullExtent();
            this.axMapControl1.ActiveView.Refresh();
        }

        private void SetLayerVisible(double mapscale)
        {
            IEnvelope areaOfInterest = null;
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {
                ILayer layer = this.axMapControl1.get_Layer(i);
                if (this.m_layersettings.LayerSettings.ContainsKey(layer.Name))
                {
                    OverviewLayerSetting setting =
                        this.m_layersettings.LayerSettings[layer.Name] as OverviewLayerSetting;
                    if ((setting.MinScale == 0.0) && (setting.MaxScale == 0.0))
                    {
                        if (areaOfInterest == null)
                        {
                            areaOfInterest = layer.AreaOfInterest;
                        }
                        else
                        {
                            areaOfInterest.Union(layer.AreaOfInterest);
                        }
                    }
                    else if ((setting.MinScale == 0.0) && (mapscale < setting.MaxScale))
                    {
                        layer.Visible = false;
                    }
                    else if ((mapscale > setting.MinScale) || (mapscale < setting.MaxScale))
                    {
                        layer.Visible = false;
                    }
                    else if ((setting.MaxScale == 0.0) && (mapscale > setting.MinScale))
                    {
                        layer.Visible = false;
                    }
                    else
                    {
                        layer.Visible = true;
                        if (areaOfInterest == null)
                        {
                            areaOfInterest = layer.AreaOfInterest;
                        }
                        else
                        {
                            areaOfInterest.Union(layer.AreaOfInterest);
                        }
                    }
                }
            }
            if (areaOfInterest != null)
            {
                this.axMapControl1.ActiveView.Extent = areaOfInterest;
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        private void SetMainAvtiveView(IActiveView av)
        {
            if (this.m_pMainAvtiveView != null)
            {
                try
                {
                    (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemAdded -=
                        (new IActiveViewEvents_ItemAddedEventHandler(this.OverviewWindow_ItemAdded));
                    (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemDeleted -=
                        (new IActiveViewEvents_ItemDeletedEventHandler(this.OverviewWindow_ItemDeleted));
                    (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemReordered -=
                        (new IActiveViewEvents_ItemReorderedEventHandler(this.OverviewWindow_ItemReordered));
                    (this.m_pMainAvtiveView as IActiveViewEvents_Event).AfterDraw -=
                        (new IActiveViewEvents_AfterDrawEventHandler(this.OverviewWindow_AfterDraw));
                    (this.m_pMainAvtiveView as IActiveViewEvents_Event).ContentsCleared -=
                        (new IActiveViewEvents_ContentsClearedEventHandler(this.OverviewWindow_ContentsCleared));
                    (this.m_pMainAvtiveView as IActiveViewEvents_Event).ContentsChanged -=
                        (new IActiveViewEvents_ContentsChangedEventHandler(this.frmOverviewWindow_ContentsChanged));
                }
                catch
                {
                }
            }
            this.m_pMainAvtiveView = null;
            this.m_pMainAvtiveView = av;
            this.m_pEnvelope = this.m_pMainAvtiveView.Extent;
            try
            {
                (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemAdded +=
                    (new IActiveViewEvents_ItemAddedEventHandler(this.OverviewWindow_ItemAdded));
                (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemDeleted +=
                    (new IActiveViewEvents_ItemDeletedEventHandler(this.OverviewWindow_ItemDeleted));
                (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemReordered +=
                    (new IActiveViewEvents_ItemReorderedEventHandler(this.OverviewWindow_ItemReordered));
                (this.m_pMainAvtiveView as IActiveViewEvents_Event).AfterDraw +=
                    (new IActiveViewEvents_AfterDrawEventHandler(this.OverviewWindow_AfterDraw));
                (this.m_pMainAvtiveView as IActiveViewEvents_Event).ContentsCleared +=
                    (new IActiveViewEvents_ContentsClearedEventHandler(this.OverviewWindow_ContentsCleared));
                (this.m_pMainAvtiveView as IActiveViewEvents_Event).ContentsChanged +=
                    (new IActiveViewEvents_ContentsChangedEventHandler(this.frmOverviewWindow_ContentsChanged));
            }
            catch
            {
            }
        }

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exception exception;
            IFillShapeElement pElement;
            IEnvelope extent;
            if (this.m_OverwindowsLayersType == OverwindowsLayersType.AllLayer)
            {
                OverviewWindowsAllLayerProperty property = new OverviewWindowsAllLayerProperty
                {
                    Map = this.m_pMainAvtiveView.FocusMap,
                    OverviewMap = this.axMapControl1.Map,
                    MapCtrlBackgroundColor = this.axMapControl1.BackColor,
                    StyleGallery = this.m_pSG,
                    FillSymbol = this.m_pFillSymbol,
                    ZoomWithMainView = this.m_ZoomWithMainView
                };
                if (property.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.axMapControl1.BackColor = property.MapCtrlBackgroundColor;
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        MessageBox.Show(exception.Message);
                    }
                    this.m_pFillSymbol = property.FillSymbol;
                    pElement = this.m_pElement as IFillShapeElement;
                    pElement.Symbol = this.m_pFillSymbol;
                    this.m_ZoomWithMainView = property.ZoomWithMainView;
                    if (this.m_ZoomWithMainView)
                    {
                        extent = this.axMapControl1.ActiveView.Extent;
                        this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    else
                    {
                        if (this.m_ClipBounds != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                        }
                        else if (this.m_pMainMapControl.Map.ClipGeometry != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                        }
                        else
                        {
                            this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        }
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
            else
            {
                OverviewWindowsProperty property2 = new OverviewWindowsProperty
                {
                    Map = this.m_pMainAvtiveView.FocusMap,
                    OverviewMap = this.axMapControl1.Map,
                    MapCtrlBackgroundColor = this.axMapControl1.BackColor,
                    StyleGallery = this.m_pSG,
                    FillSymbol = this.m_pFillSymbol,
                    ZoomWithMainView = this.m_ZoomWithMainView
                };
                if (property2.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.axMapControl1.BackColor = property2.MapCtrlBackgroundColor;
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        MessageBox.Show(exception.Message);
                    }
                    this.m_pFillSymbol = property2.FillSymbol;
                    pElement = this.m_pElement as IFillShapeElement;
                    pElement.Symbol = this.m_pFillSymbol;
                    this.m_ZoomWithMainView = property2.ZoomWithMainView;
                    if (this.m_ZoomWithMainView)
                    {
                        extent = this.axMapControl1.ActiveView.Extent;
                        this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    else
                    {
                        if (this.m_ClipBounds != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                        }
                        else if (this.m_pMainMapControl.Map.ClipGeometry != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                        }
                        else
                        {
                            this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        }
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
        }

        public IGeometry ClipBounds
        {
            set
            {
                (this.axMapControl1.Map as IMapAdmin2).ClipBounds = value;
                this.m_ClipBounds = value;
                if (this.m_ClipBounds != null)
                {
                    this.axMapControl1.ActiveView.Extent = this.m_ClipBounds.Envelope;
                }
                else
                {
                    this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                }
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        public IMapControl2 MainMapControl
        {
            set
            {
                if (this.m_pMainAvtiveView != null)
                {
                    try
                    {
                        (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemAdded -=
                            (new IActiveViewEvents_ItemAddedEventHandler(this.OverviewWindow_ItemAdded));
                        (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemDeleted -=
                            (new IActiveViewEvents_ItemDeletedEventHandler(this.OverviewWindow_ItemDeleted));
                        (this.m_pMainAvtiveView as IActiveViewEvents_Event).ItemReordered -=
                            (new IActiveViewEvents_ItemReorderedEventHandler(this.OverviewWindow_ItemReordered));
                        (this.m_pMainAvtiveView as IActiveViewEvents_Event).AfterDraw -=
                            (new IActiveViewEvents_AfterDrawEventHandler(this.OverviewWindow_AfterDraw));
                        (this.m_pMainAvtiveView as IActiveViewEvents_Event).ContentsCleared -=
                            (new IActiveViewEvents_ContentsClearedEventHandler(this.OverviewWindow_ContentsCleared));
                        (this.m_pMainAvtiveView as IActiveViewEvents_Event).ContentsChanged -=
                            (new IActiveViewEvents_ContentsChangedEventHandler(this.frmOverviewWindow_ContentsChanged));
                    }
                    catch
                    {
                    }
                }
                this.m_pMainAvtiveView = null;
                if (this.m_pMainMapControl != null)
                {
                    try
                    {
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced -=
                            (new IMapControlEvents2_OnMapReplacedEventHandler(this.OverviewWindowCtrl_OnMapReplaced));
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnViewRefreshed -=
                            (new IMapControlEvents2_OnViewRefreshedEventHandler(this.frmOverwindows_OnViewRefreshed));
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnExtentUpdated -=
                            (new IMapControlEvents2_OnExtentUpdatedEventHandler(this.frmOverwindows_OnExtentUpdated));
                    }
                    catch
                    {
                    }
                }
                this.m_pMainMapControl = value;
                if (this.m_pMainMapControl != null)
                {
                    this.m_pMainAvtiveView = this.m_pMainMapControl.ActiveView;
                    this.m_pEnvelope = this.m_pMainAvtiveView.Extent;
                    if (this.m_CanDo)
                    {
                        this.Init();
                    }
                }
            }
        }

        public IMapControl2 MainMapControl2
        {
            set
            {
                bool flag = this.m_pMainMapControl == null;
                if (this.m_pMainMapControl != null)
                {
                    try
                    {
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnExtentUpdated -=
                            (new IMapControlEvents2_OnExtentUpdatedEventHandler(this.frmOverwindows_OnExtentUpdated));
                    }
                    catch
                    {
                    }
                }
                this.m_pMainMapControl = value;
                if (this.m_pMainMapControl != null)
                {
                    this.m_pMainAvtiveView = this.m_pMainMapControl.ActiveView;
                    this.m_pEnvelope = this.m_pMainAvtiveView.Extent;
                    if (flag)
                    {
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced +=
                            (new IMapControlEvents2_OnMapReplacedEventHandler(this.OverviewWindowCtrl_OnMapReplaced));
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnViewRefreshed +=
                            (new IMapControlEvents2_OnViewRefreshedEventHandler(this.frmOverwindows_OnViewRefreshed));
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnExtentUpdated +=
                            (new IMapControlEvents2_OnExtentUpdatedEventHandler(this.frmOverwindows_OnExtentUpdated));
                    }
                    (this.m_pMainMapControl as IMapControlEvents2_Event).OnExtentUpdated +=
                        (new IMapControlEvents2_OnExtentUpdatedEventHandler(this.frmOverwindows_OnExtentUpdated));
                    (this.axMapControl1.Map as IMapAdmin2).ClipBounds = this.m_pMainMapControl.Map.ClipGeometry;
                    if (this.m_pMainMapControl.Map.ClipGeometry != null)
                    {
                        this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                    }
                    else
                    {
                        this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                    }
                    this.axMapControl1.ActiveView.Refresh();
                }
            }
        }

        public bool MustOneLayer
        {
            set { this.m_MustOneLayer = value; }
        }

        public OverwindowsLayersType OverwindowsLayersType
        {
            set
            {
                this.m_OverwindowsLayersType = value;
                if (this.m_OverwindowsLayersType == OverwindowsLayersType.LayerSettings)
                {
                    this.InitLayerSettings();
                }
            }
        }

        public bool UseMainActiveViewLayerCopy
        {
            get { return this.m_UseMainActiveViewLayerCopy; }
            set { this.m_UseMainActiveViewLayerCopy = value; }
        }

        public bool ZoomWithMainView
        {
            get { return this.m_ZoomWithMainView; }
            set
            {
                this.m_ZoomWithMainView = value;
                if (this.m_CanDo)
                {
                    if (this.m_ZoomWithMainView)
                    {
                        this.m_pEnvelope = this.m_pMainMapControl.ActiveView.Extent;
                        IEnvelope extent = this.axMapControl1.ActiveView.Extent;
                        if (this.m_pMainAvtiveView != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                        }
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    else
                    {
                        if (this.m_pMainMapControl.Map.ClipGeometry != null)
                        {
                            this.axMapControl1.ActiveView.Extent = this.m_pMainMapControl.Map.ClipGeometry.Envelope;
                        }
                        else
                        {
                            this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        }
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
        }
    }
}