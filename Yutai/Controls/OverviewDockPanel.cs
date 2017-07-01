using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Controls
{
    public partial class OverviewDockPanel : Yutai.UI.Controls.DockPanelControlBase, IMenuProvider
    {
        private readonly IAppContext _context;
        private IActiveViewEvents_Event _activeViewEvents;
        private IMapControl2 _mainMapControl;
        private IElement _rectangleElement;
        private ISimpleFillSymbol _fillSymbol;
        private bool _canDo;
        private IMapControlEvents2_Event mapControlEvents2;
        private IEnvelope _envelope;
        private IGeometry _clipBounds;
        private string _caption;
        private Bitmap _bitmap;
        public static string DefaultDockName = "Dock_Main_Overview";
        private IMap _map;
        private IActiveView _activeView;

        public OverviewDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            InitializeComponent();
            TabPosition = 1;
            Caption = "鹰眼视图";
            _bitmap = Properties.Resources.icon_overview;

            //axMapControl1.MouseDown+= AxMapControl1OnMouseDown;
        }

        public void InitMainMap(object control)
        {
            if (_activeView != null)
            {
                _activeViewEvents.ViewRefreshed -= ActiveViewEventsOnViewRefreshed;
            }
            if (mapControlEvents2 != null)
            {
                mapControlEvents2.OnViewRefreshed -= MapControlEvents2OnOnViewRefreshed;
                mapControlEvents2.OnExtentUpdated -= MapControlEvents2OnOnExtentUpdated;
            }
            if (control is IPageLayoutControl2)
            {
                _map = _context.FocusMap;
                _activeViewEvents = _context.FocusMap as IActiveViewEvents_Event;
                _activeViewEvents.ViewRefreshed += ActiveViewEventsOnViewRefreshed;
            }
            else if (control is IMapControl3)
            {
                _mainMapControl = control as IMapControl2;
                _map = _mainMapControl.Map;
                mapControlEvents2 = _mainMapControl as IMapControlEvents2_Event;
                mapControlEvents2.OnViewRefreshed += MapControlEvents2OnOnViewRefreshed;
                mapControlEvents2.OnExtentUpdated += MapControlEvents2OnOnExtentUpdated;
            }

            _rectangleElement = new RectangleElementClass();
            _fillSymbol = new SimpleFillSymbolClass();
            _fillSymbol.Style = esriSimpleFillStyle.esriSFSNull;
            ILineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass()
            {
                Red = 0,
                Green = 0,
                Blue = 255
            };
            simpleLineSymbol.Color = color;
            simpleLineSymbol.Width = 2;
            _fillSymbol.Outline = simpleLineSymbol;
            ((IFillShapeElement) _rectangleElement).Symbol = _fillSymbol;
            axMapControl1.ActiveView.GraphicsContainer.AddElement(_rectangleElement as IElement, 0);
            _canDo = true;
        }

        private void ActiveViewEventsOnViewRefreshed(IActiveView view, esriViewDrawPhase phase, object data,
            IEnvelope envelope)
        {
            if (phase != esriViewDrawPhase.esriViewAll) return;
            if (this.axMapControl1.LayerCount == 0) return;
            if (this.CanDo)
            {
                this._envelope = envelope;
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords((this._envelope.XMin + this._envelope.XMax)/2,
                    (this._envelope.YMin + this._envelope.YMax)/2);

                this.DrawRectangle(this.axMapControl1.ActiveView);
            }
        }

        public override Bitmap Image
        {
            get { return _bitmap; }
        }

        public override string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public override string DockName
        {
            get { return "DockPanel_Main_Overview"; }
        }

        public override string DefaultNestDockName
        {
            get { return "Dock_Main_MapLegend"; }
        }

        public IGeometry ClipBounds
        {
            set
            {
                (this.axMapControl1.Map as IMapAdmin2).ClipBounds = value;
                this._clipBounds = value;
                if (this._clipBounds == null)
                {
                    this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                }
                else
                {
                    this.axMapControl1.ActiveView.Extent = this._clipBounds.Envelope;
                }
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        private bool AddGroupLayer(IGroupLayer pGroupLayer)
        {
            bool flag;
            ICompositeLayer compLayer = pGroupLayer as ICompositeLayer;
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i] as ILayer;
                if (layer is IGroupLayer)
                {
                    this.AddGroupLayer(layer as IGroupLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (featureLayer != null)
                    {
                        if (featureLayer is IFDOGraphicsLayer)
                        {
                            continue;
                        }
                        if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            this.AddLayer(layer, 0);
                            flag = true;
                            break;
                        }
                    }
                }
            }
            return true;
        }


        private void AddLayer(IGroupLayer pGroupLayer)
        {
            ICompositeLayer compositeLayer = pGroupLayer as ICompositeLayer;
            for (int i = 0; i < compositeLayer.Count; i++)
            {
                ILayer layer = compositeLayer.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.AddLayer(layer as IGroupLayer);
                }
                else
                {
                    this.AddLayer(layer, this.axMapControl1.LayerCount);
                }
            }
        }

        private void AddLayer(ILayer pLayer, int nIndex)
        {
            ILayer layer = (new ObjectCopyClass()).Copy(pLayer) as ILayer;
            layer.Visible = true;
            this.axMapControl1.AddLayer(layer, nIndex);
        }


        private void MapControlEvents2OnOnExtentUpdated(object displayTransformation, bool sizeChanged,
            object newEnvelope)
        {
            if (this.axMapControl1.LayerCount == 0) return;
            if (this.CanDo)
            {
                this._envelope = this._mainMapControl.ActiveView.Extent;
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords((this._envelope.XMin + this._envelope.XMax)/2,
                    (this._envelope.YMin + this._envelope.YMax)/2);

                this.DrawRectangle(this.axMapControl1.ActiveView);
            }
        }

        private void MapControlEvents2OnOnViewRefreshed(object activeView, int viewDrawPhase, object layerOrElement,
            object envelope)
        {
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield return contextMenuOverview.Items; }
        }

        public bool CanDo
        {
            get { return _canDo; }
            set { _canDo = value; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public void InitOverview()
        {
            XmlProject project = ((ISecureContext) _context).YutaiProject;
            if (project.Overview != null)
            {
                string oType = project.Overview.OverviewType.ToUpper().Trim();
                if (oType == "MXD")
                {
                    string mxdName = project.Overview.ObjectName;
                    FileInfo fileInfo =
                        new FileInfo(FileHelper.GetRelativePath(project.Settings.LoadAsFilename, mxdName));

                    if (fileInfo.Exists)
                    {
                        axMapControl1.LoadMxFile(fileInfo.FullName);
                    }
                    else
                    {
                        CopyLastLayerToOverview();
                    }
                }
                else
                {
                    string layerName = project.Overview.ObjectName.Trim();
                    if (string.IsNullOrEmpty(layerName))
                    {
                        CopyLastLayerToOverview();
                    }
                    else
                    {
                        CopyLayerToOverview(layerName);
                    }
                }
            }
            else
            {
                CopyLastLayerToOverview();
            }
            this.axMapControl1.ActiveView.Refresh();

            this.ClipBounds = _mainMapControl.Map.ClipGeometry;
            this.axMapControl1.Map.ClearSelection();
        }

        private void CopyLayerToOverview(string layerName)
        {
            IFeatureLayer featureLayerClass;
            IFeatureLayer featureLayer = null;
            if (_mainMapControl.LayerCount == 0) return;

            ILayer pLayer = LayerHelper.QueryLayerByDisplayName(_context.MapControl.Map, layerName) as ILayer;
            if (pLayer == null)
            {
                CopyLastLayerToOverview();
                return;
            }

            IClone pClone = pLayer as IClone;
            ILayer newLayer = pClone.Clone() as ILayer;
            newLayer.Visible = true;
            axMapControl1.Map.AddLayer(newLayer);
            this.axMapControl1.Extent = _mainMapControl.FullExtent;
        }

        private void CopyLastLayerToOverview()
        {
            IFeatureLayer featureLayerClass;
            IFeatureLayer featureLayer = null;
            if (_mainMapControl.LayerCount == 0) return;
            ILayer pLayer = _mainMapControl.Layer[0];
            IClone pClone = pLayer as IClone;
            ILayer newLayer = pClone.Clone() as ILayer;
            newLayer.Visible = true;
            axMapControl1.Map.AddLayer(newLayer);
            this.axMapControl1.Extent = _mainMapControl.FullExtent;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IEnvelope extent;
            if (e.button != 2)
            {
                if (this._mainMapControl != null)
                {
                    if (this.axMapControl1.LayerCount > 0)
                    {
                        IEnvelope xMin = this.axMapControl1.TrackRectangle();
                        if ((xMin.IsEmpty || xMin.Width == 0 ? false : xMin.Height != 0))
                        {
                            extent = this._mainMapControl.Extent;
                            double width = extent.Width/extent.Height;
                            if (xMin.Width <= xMin.Height*width)
                            {
                                xMin.XMax = xMin.XMin + xMin.Height*width;
                            }
                            else
                            {
                                xMin.YMin = xMin.YMax - xMin.Width/width;
                            }
                        }
                        else
                        {
                            extent = this._mainMapControl.Extent;
                            xMin.XMin = e.mapX - (extent.XMax - extent.XMin)/2;
                            xMin.XMax = e.mapX + (extent.XMax - extent.XMin)/2;
                            xMin.YMin = e.mapY - (extent.YMax - extent.YMin)/2;
                            xMin.YMax = e.mapY + (extent.YMax - extent.YMin)/2;
                        }
                        if ((this._rectangleElement.Geometry != null ? true : !this._rectangleElement.Geometry.IsEmpty))
                        {
                            this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground,
                                this._rectangleElement, this._envelope);
                        }
                        this._envelope = xMin;
                        this._rectangleElement.Geometry = xMin;
                        this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics,
                            this._rectangleElement, this._envelope);
                        this._canDo = false;
                        Cursor.Current = Cursors.WaitCursor;
                        this._mainMapControl.Extent = xMin;
                        this._mainMapControl.ActiveView.Refresh();
                        Cursor.Current = Cursors.Default;
                        this._canDo = true;
                    }
                }
            }
        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 2)
                this.contextMenuOverview.Show(this.axMapControl1, new System.Drawing.Point(e.x, e.y));
        }

        public void DrawRectangle(IActiveView pActiveView)
        {
            if (this._rectangleElement != null)
            {
                if (pActiveView.FocusMap.LayerCount > 0)
                {
                    IDisplay screenDisplay = pActiveView.ScreenDisplay;
                    pActiveView.GraphicsContainer.DeleteAllElements();
                    if ((this._rectangleElement.Geometry != null ? true : !this._rectangleElement.Geometry.IsEmpty))
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this._rectangleElement, null);
                    }
                    this._rectangleElement.Geometry = GeometryUtility.ConvertEnvelopeToPolygon((IGeometry) _envelope);

                    pActiveView.GraphicsContainer.AddElement(_rectangleElement, 0);
                    if ((this._rectangleElement.Geometry != null ? true : !this._rectangleElement.Geometry.IsEmpty))
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this._rectangleElement, null);
                    }
                }
            }
        }

        private void axMapControl1_Resize(object sender, EventArgs e)
        {
            axMapControl1.ActiveView.Refresh();
        }

        public void FullExtent()
        {
            axMapControl1.ActiveView.Extent = axMapControl1.ActiveView.FullExtent;
            axMapControl1.Refresh();
            return;
        }

        public void Current()
        {
            axMapControl1.ActiveView.Extent = ((IActiveView) _map).Extent;
            axMapControl1.Refresh();
            return;
        }
    }
}