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

namespace Yutai.ArcGIS.Controls.Controls
{
    public class OverviewWindowsEx : UserControl
    {
        private AxMapControl axMapControl1;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private IContainer components = null;
        private bool m_bUseScale = true;
        public bool m_CanDo = false;
        private OverviewLayerSettings m_layersettings = null;
        private bool m_MustOneLayer = true;
        private OverwindowsLayersType m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
        private IActiveViewEvents_Event m_pAVEnt = null;
        public IElement m_pElement;
        public IEnvelope m_pEnvelope = null;
        private IFillSymbol m_pFillSymbol;
        private AxMapControl m_pMainMapControl = null;
        private IStyleGallery m_pSG = null;
        private bool m_UseMainActiveViewLayerCopy = true;
        private bool m_ZoomWithMainView = false;
        private BarButtonItem OverviewProperty;
        private PopupMenu popupMenu1;
        private BarButtonItem UpdateView;

        public OverviewWindowsEx()
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
                    if (((layer2 != null) && !(layer2 is IFDOGraphicsLayer)) && (layer2.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
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
                this.axMapControl1.AddLayer(copy.Copy(pLayer) as ILayer, nIndex);
            }
            else
            {
                this.axMapControl1.AddLayer(pLayer, nIndex);
            }
        }

        private void axMapControl1_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            if (this.m_pMainMapControl.LayerCount == 0)
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
                if (this.axMapControl1.ActiveView.GraphicsContainer.Next() == null)
                {
                    this.axMapControl1.ActiveView.GraphicsContainer.AddElement(this.m_pElement, 0);
                }
                this.DrawRectangle(this.axMapControl1.ActiveView);
            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if ((e.button != 2) && (this.axMapControl1.LayerCount > 0))
            {
                IEnvelope extent;
                IEnvelope envelope = this.axMapControl1.TrackRectangle();
                if ((envelope.IsEmpty || (envelope.Width == 0.0)) || (envelope.Height == 0.0))
                {
                    extent = this.m_pMainMapControl.Extent;
                    envelope.XMin = e.mapX - ((extent.XMax - extent.XMin) / 2.0);
                    envelope.XMax = e.mapX + ((extent.XMax - extent.XMin) / 2.0);
                    envelope.YMin = e.mapY - ((extent.YMax - extent.YMin) / 2.0);
                    envelope.YMax = e.mapY + ((extent.YMax - extent.YMin) / 2.0);
                }
                else
                {
                    extent = this.m_pMainMapControl.Extent;
                    double num = extent.Width / extent.Height;
                    if (envelope.Width > (envelope.Height * num))
                    {
                        envelope.YMin = envelope.YMax - (envelope.Width / num);
                    }
                    else
                    {
                        envelope.XMax = envelope.XMin + (envelope.Height * num);
                    }
                }
                if (!((this.m_pElement.Geometry == null) && this.m_pElement.Geometry.IsEmpty))
                {
                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, this.m_pElement, this.m_pEnvelope);
                }
                this.m_pEnvelope = envelope;
                this.m_pElement.Geometry = envelope;
                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.m_pElement, this.m_pEnvelope);
                this.m_CanDo = false;
                Cursor.Current = Cursors.WaitCursor;
                this.m_pMainMapControl.Extent = envelope;
                this.m_pMainMapControl.ActiveView.Refresh();
                Cursor.Current = Cursors.Default;
                this.m_CanDo = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DrawRectangle(IActiveView pActiveView)
        {
            if (pActiveView.FocusMap.LayerCount > 0)
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

        private void Init()
        {
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
                    if ((layer.FeatureClass != null) && (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        layer3 = new FeatureLayerClass {
                            FeatureClass = layer.FeatureClass
                        };
                        this.axMapControl1.Map.AddLayer(layer3);
                        this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        this.axMapControl1.ActiveView.Refresh();
                        return;
                    }
                }
            }
            if (layer != null)
            {
                layer3 = new FeatureLayerClass {
                    FeatureClass = layer.FeatureClass
                };
                this.axMapControl1.Map.AddLayer(layer3);
                this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewWindowsEx));
            this.axMapControl1 = new AxMapControl();
            this.barManager1 = new BarManager(this.components);
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.UpdateView = new BarButtonItem();
            this.OverviewProperty = new BarButtonItem();
            this.popupMenu1 = new PopupMenu(this.components);
            this.axMapControl1.BeginInit();
            this.barManager1.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(150, 150);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnAfterDraw += new IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.axMapControl1_OnAfterDraw);
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.UpdateView, this.OverviewProperty });
            this.barManager1.MaxItemId = 2;
            this.UpdateView.Caption = "更新显示";
            this.UpdateView.Id = 0;
            this.UpdateView.Name = "UpdateView";
            this.OverviewProperty.Caption = "属性";
            this.OverviewProperty.Id = 1;
            this.OverviewProperty.Name = "OverviewProperty";
            this.OverviewProperty.ItemClick += new ItemClickEventHandler(this.OverviewProperty_ItemClick);
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.OverviewProperty) });
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.axMapControl1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "OverviewWindowsEx";
            base.Load += new EventHandler(this.OverviewWindowsEx_Load);
            base.SizeChanged += new EventHandler(this.OverviewWindowsEx_SizeChanged);
            this.axMapControl1.EndInit();
            this.barManager1.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
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
            }
            catch (Exception)
            {
                this.m_bUseScale = false;
                this.m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
            }
            if (this.m_OverwindowsLayersType != OverwindowsLayersType.LayerSettings)
            {
                this.barManager1.SetPopupContextMenu(this.axMapControl1, this.popupMenu1);
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
            }
            catch
            {
                this.m_bUseScale = false;
                this.m_OverwindowsLayersType = OverwindowsLayersType.BottomPolygonLayer;
            }
            if (this.m_OverwindowsLayersType != OverwindowsLayersType.LayerSettings)
            {
                this.barManager1.SetPopupContextMenu(this.axMapControl1, this.popupMenu1);
            }
        }

        private bool IsNavigationLayer(ILayer pLayer)
        {
            return ((this.m_layersettings != null) && this.m_layersettings.LayerSettings.ContainsKey(pLayer.Name));
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
                    if (this.AddGroupLayer(pLayer as IGroupLayer) && (this.m_OverwindowsLayersType == OverwindowsLayersType.BottomPolygonLayer))
                    {
                        return;
                    }
                }
                else if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = pLayer as IFeatureLayer;
                    if (((layer2 != null) && !(layer2 is IFDOGraphicsLayer)) && (layer2.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        this.AddLayer(pLayer, 0);
                        if (this.m_OverwindowsLayersType != OverwindowsLayersType.BottomPolygonLayer)
                        {
                            break;
                        }
                        return;
                    }
                }
            }
            if (((this.m_pMainMapControl.Map.LayerCount > 0) && (this.axMapControl1.Map.LayerCount == 0)) && this.m_MustOneLayer)
            {
                pLayer = this.m_pMainMapControl.Map.get_Layer(this.m_pMainMapControl.Map.LayerCount - 1);
                this.AddLayer(pLayer, 0);
            }
            this.axMapControl1.Map.RecalcFullExtent();
            this.axMapControl1.Extent = this.axMapControl1.FullExtent;
            this.axMapControl1.ActiveView.Refresh();
        }

        private void m_pAVEnt_AfterDraw(IDisplay Display, esriViewDrawPhase phase)
        {
        }

        private void m_pMainMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            if (this.m_CanDo)
            {
                this.m_pEnvelope = this.m_pMainMapControl.Extent;
                if (this.m_ZoomWithMainView)
                {
                    IEnvelope extent = this.axMapControl1.ActiveView.Extent;
                    double num = extent.Width / extent.Height;
                    if (this.m_pEnvelope.Width > (this.m_pEnvelope.Height * num))
                    {
                        this.m_pEnvelope.YMin = this.m_pEnvelope.YMax - (this.m_pEnvelope.Width / num);
                    }
                    else
                    {
                        this.m_pEnvelope.XMax = this.m_pEnvelope.XMin + (this.m_pEnvelope.Height * num);
                    }
                    this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                    this.axMapControl1.ActiveView.Refresh();
                }
                if (!this.axMapControl1.IsDisposed)
                {
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
            if (this.m_OverwindowsLayersType == OverwindowsLayersType.LayerSettings)
            {
                this.SetLayerVisible(MapHelper.GetMapScale(this.m_pMainMapControl.Map));
            }
        }

        private void m_pMainMapControl_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            this.RestMap();
            if (this.m_pAVEnt != null)
            {
                try
                {
                    this.m_pAVEnt.remove_ItemAdded(new IActiveViewEvents_ItemAddedEventHandler(this.pAVEnt_ItemAdded));
                    this.m_pAVEnt.remove_ItemDeleted(new IActiveViewEvents_ItemDeletedEventHandler(this.pAVEnt_ItemDeleted));
                    this.m_pAVEnt.remove_AfterDraw(new IActiveViewEvents_AfterDrawEventHandler(this.m_pAVEnt_AfterDraw));
                }
                catch
                {
                }
            }
            this.m_pAVEnt = this.m_pMainMapControl.ActiveView as IActiveViewEvents_Event;
            try
            {
                this.m_pAVEnt.add_ItemAdded(new IActiveViewEvents_ItemAddedEventHandler(this.pAVEnt_ItemAdded));
                this.m_pAVEnt.add_ItemDeleted(new IActiveViewEvents_ItemDeletedEventHandler(this.pAVEnt_ItemDeleted));
                this.m_pAVEnt.add_AfterDraw(new IActiveViewEvents_AfterDrawEventHandler(this.m_pAVEnt_AfterDraw));
            }
            catch
            {
            }
            if (this.m_OverwindowsLayersType == OverwindowsLayersType.LayerSettings)
            {
                this.AddLayer();
                this.SetLayerVisible(MapHelper.GetMapScale(this.m_pMainMapControl.Map));
            }
            else
            {
                this.Init();
            }
            this.DrawRectangle(this.axMapControl1.ActiveView);
        }

        private void OverviewProperty_ItemClick(object sender, ItemClickEventArgs e)
        {
            Exception exception;
            IFillShapeElement pElement;
            IEnvelope extent;
            double num;
            if (this.m_OverwindowsLayersType == OverwindowsLayersType.AllLayer)
            {
                OverviewWindowsAllLayerProperty property = new OverviewWindowsAllLayerProperty {
                    Map = this.m_pMainMapControl.Map,
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
                        num = extent.Width / extent.Height;
                        if (this.m_pEnvelope.Width > (this.m_pEnvelope.Height * num))
                        {
                            this.m_pEnvelope.YMin = this.m_pEnvelope.YMax - (this.m_pEnvelope.Width / num);
                        }
                        else
                        {
                            this.m_pEnvelope.XMax = this.m_pEnvelope.XMin + (this.m_pEnvelope.Height * num);
                        }
                        this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    else
                    {
                        this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
            else
            {
                OverviewWindowsProperty property2 = new OverviewWindowsProperty {
                    Map = this.m_pMainMapControl.Map,
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
                        num = extent.Width / extent.Height;
                        if (this.m_pEnvelope.Width > (this.m_pEnvelope.Height * num))
                        {
                            this.m_pEnvelope.YMin = this.m_pEnvelope.YMax - (this.m_pEnvelope.Width / num);
                        }
                        else
                        {
                            this.m_pEnvelope.XMax = this.m_pEnvelope.XMin + (this.m_pEnvelope.Height * num);
                        }
                        this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    else
                    {
                        this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
        }

        private void OverviewWindowsEx_Load(object sender, EventArgs e)
        {
            this.axMapControl1.ShowScrollbars = false;
            this.m_pElement = new RectangleElementClass();
            this.m_pFillSymbol = new SimpleFillSymbolClass();
            ((ISimpleFillSymbol) this.m_pFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            ILineSymbol symbol = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 0,
                Green = 0,
                Blue = 0xff
            };
            symbol.Color = color;
            symbol.Width = 1.0;
            this.m_pFillSymbol.Outline = symbol;
            IFillShapeElement pElement = this.m_pElement as IFillShapeElement;
            pElement.Symbol = this.m_pFillSymbol;
            this.m_CanDo = true;
            this.axMapControl1.ActiveView.GraphicsContainer.AddElement(this.m_pElement, 0);
        }

        private void OverviewWindowsEx_SizeChanged(object sender, EventArgs e)
        {
        }

        private void pAVEnt_ItemAdded(object Item)
        {
            if (this.m_OverwindowsLayersType == OverwindowsLayersType.LayerSettings)
            {
                this.AddLayer();
                this.SetLayerVisible(MapHelper.GetMapScale(this.m_pMainMapControl.Map));
                this.DrawRectangle(this.axMapControl1.ActiveView);
            }
            else if ((this.axMapControl1.LayerCount <= 0) && (Item is ILayer))
            {
                IFeatureLayer layer = Item as IFeatureLayer;
                if (((layer != null) && !(layer is IFDOGraphicsLayer)) && (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                {
                    this.RestMap();
                    this.AddLayer(Item as ILayer, 0);
                    this.axMapControl1.Extent = this.axMapControl1.FullExtent;
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
        }

        private void pAVEnt_ItemDeleted(object Item)
        {
            this.AddLayer();
            this.SetLayerVisible(MapHelper.GetMapScale(this.m_pMainMapControl.Map));
            this.DrawRectangle(this.axMapControl1.ActiveView);
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
                    OverviewLayerSetting setting = this.m_layersettings.LayerSettings[layer.Name] as OverviewLayerSetting;
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

        public AxMapControl MainMapControl
        {
            set
            {
                if (this.m_pAVEnt != null)
                {
                    try
                    {
                        this.m_pAVEnt.remove_ItemAdded(new IActiveViewEvents_ItemAddedEventHandler(this.pAVEnt_ItemAdded));
                        this.m_pAVEnt.remove_ItemDeleted(new IActiveViewEvents_ItemDeletedEventHandler(this.pAVEnt_ItemDeleted));
                        this.m_pAVEnt.remove_AfterDraw(new IActiveViewEvents_AfterDrawEventHandler(this.m_pAVEnt_AfterDraw));
                    }
                    catch
                    {
                    }
                }
                if (this.m_pMainMapControl != null)
                {
                    try
                    {
                        this.m_pMainMapControl.OnExtentUpdated -= new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.m_pMainMapControl_OnExtentUpdated);
                        this.m_pMainMapControl.OnMapReplaced -= new IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.m_pMainMapControl_OnMapReplaced);
                    }
                    catch
                    {
                    }
                }
                this.m_pMainMapControl = value;
                if (this.m_pMainMapControl != null)
                {
                    this.m_pEnvelope = this.m_pMainMapControl.Extent;
                    this.m_pAVEnt = this.m_pMainMapControl.ActiveView as IActiveViewEvents_Event;
                    this.m_pAVEnt.add_ItemAdded(new IActiveViewEvents_ItemAddedEventHandler(this.pAVEnt_ItemAdded));
                    this.m_pAVEnt.add_ItemDeleted(new IActiveViewEvents_ItemDeletedEventHandler(this.pAVEnt_ItemDeleted));
                    this.m_pAVEnt.add_AfterDraw(new IActiveViewEvents_AfterDrawEventHandler(this.m_pAVEnt_AfterDraw));
                    this.m_pMainMapControl.OnExtentUpdated += new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.m_pMainMapControl_OnExtentUpdated);
                    this.m_pMainMapControl.OnMapReplaced += new IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.m_pMainMapControl_OnMapReplaced);
                    if (this.m_CanDo)
                    {
                        this.Init();
                    }
                }
            }
        }

        public bool MustOneLayer
        {
            set
            {
                this.m_MustOneLayer = value;
            }
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

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }

        public bool UseMainActiveViewLayerCopy
        {
            get
            {
                return this.m_UseMainActiveViewLayerCopy;
            }
            set
            {
                this.m_UseMainActiveViewLayerCopy = value;
            }
        }

        public bool ZoomWithMainView
        {
            get
            {
                return this.m_ZoomWithMainView;
            }
            set
            {
                this.m_ZoomWithMainView = value;
                if (this.m_CanDo)
                {
                    if (this.m_ZoomWithMainView)
                    {
                        IEnvelope extent = this.axMapControl1.ActiveView.Extent;
                        double num = extent.Width / extent.Height;
                        if (this.m_pEnvelope.Width > (this.m_pEnvelope.Height * num))
                        {
                            this.m_pEnvelope.YMin = this.m_pEnvelope.YMax - (this.m_pEnvelope.Width / num);
                        }
                        else
                        {
                            this.m_pEnvelope.XMax = this.m_pEnvelope.XMin + (this.m_pEnvelope.Height * num);
                        }
                        this.axMapControl1.ActiveView.Extent = this.m_pEnvelope;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    else
                    {
                        this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                        this.axMapControl1.ActiveView.Refresh();
                    }
                    this.DrawRectangle(this.axMapControl1.ActiveView);
                }
            }
        }
    }
}

