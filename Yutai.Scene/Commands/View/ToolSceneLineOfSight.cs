using System;
using System.Reflection;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class ToolSceneLineOfSight : YutaiTool
    {
        private IAppContext _context;
        private IScenePlugin _plugin;
        public static bool bCurvEnabled;

        public static double ObserverOffset;

        public static double TargetOffset;

       

        private IActiveView iactiveView_0;

        private IDisplayTransformation idisplayTransformation_0;

        private IDisplayFeedback idisplayFeedback_0;

        private INewLineFeedback inewLineFeedback_0;

        private bool bool_0;

        private IPointCollection ipointCollection_0;

        private int int_0 = 0;

        private int int_1 = 0;

        private ISurface isurface_0 = null;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0) return false;

                if ((this._plugin.Scene as IBasicMap).LayerCount == 0)
                {
                    result = false;
                }
                else if (this._plugin.CurrentLayer == null)
                {
                    result = false;
                }
                else
                {
                    this.isurface_0 = this.GetSurfaceFromLayer(this._plugin.CurrentLayer);
                    result = (this.isurface_0 != null);
                }
                return result;
            }
        }

        static ToolSceneLineOfSight()
        {
            // 注意: 此类型已标记为 'beforefieldinit'.
            ToolSceneLineOfSight.old_acctor_mc();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public ToolSceneLineOfSight(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_LineOfSight";
            _itemType = RibbonItemType.Tool;
            this.m_caption = "可视性";
            this.m_category = "3D";
            this.m_name = "Scene_LineOfSight";
            this.m_toolTip = "可视性";
            this.m_bitmap = Properties.Resources.los;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.cross.cur"));
        }

        public ISurface GetSurfaceFromLayer(ILayer ilayer_0)
        {
            ISurface surface = null;
            if (ilayer_0 == null)
            {
                surface = null;
            }
            else if (ilayer_0 is ITinLayer)
            {
                ITinLayer tinLayer = ilayer_0 as ITinLayer;
                surface = (tinLayer.Dataset as ISurface);
            }
            else if (ilayer_0 is IRasterLayer)
            {
                IRasterLayer rasterLayer = ilayer_0 as IRasterLayer;
                if (surface == null)
                {
                    IRasterBandCollection rasterBandCollection = rasterLayer.Raster as IRasterBandCollection;
                    IRasterBand rasterBand = rasterBandCollection.Item(0);
                    surface = (new RasterSurface
                    {
                        RasterBand = rasterBand
                    } as ISurface);
                }
            }
            return surface;
        }

        

        public override void OnClick()
        {
            _plugin.CurrentTool = this;
            if (this.isurface_0 != null)
            {
                IGeoDataset geoDataset;
                if (this.isurface_0 is ITin)
                {
                    geoDataset = (this.isurface_0 as IGeoDataset);
                }
                else
                {
                    IRasterSurface rasterSurface = this.isurface_0 as IRasterSurface;
                    geoDataset = (rasterSurface.RasterBand.RasterDataset as IGeoDataset);
                }
                ISpatialReference spatialReference = geoDataset.SpatialReference;
                ILinearUnit zCoordinateUnit = spatialReference.ZCoordinateUnit;
                if (zCoordinateUnit != null && spatialReference is IProjectedCoordinateSystem)
                {
                    ToolSceneLineOfSight.bCurvEnabled = true;
                }
            }
        }

        public override void OnMouseDown(int int_2, int int_3, int int_4, int int_5)
        {
            IPoint point;
            object obj;
            object obj2;
            this._plugin.SceneGraph.Locate(this._plugin.ActiveViewer, int_4, int_5, esriScenePickMode.esriScenePickGeography, true, out point, out obj, out obj2);
            if (point != null)
            {
                IClone clone = point as IClone;
                IPoint point2 = clone.Clone() as IPoint;
                point2.Z /= this._plugin.SceneGraph.VerticalExaggeration;
                point2.SpatialReference = this._plugin.Scene.SpatialReference;
                IDisplay3D display3D = this._plugin.SceneGraph as IDisplay3D;
                display3D.FlashLocation(point2);
                if (this.ipointCollection_0 == null)
                {
                    this.ipointCollection_0 = new Polyline();
                    (this.ipointCollection_0 as IGeometry).SpatialReference = this._plugin.Scene.SpatialReference;
                }
                object value = Missing.Value;
                this.ipointCollection_0.AddPoint(point, ref value, ref value);
                IPolyline polyline = null;
                if (this.ipointCollection_0.PointCount == 2)
                {
                    clone = (this.ipointCollection_0 as IClone);
                    polyline = (clone.Clone() as IPolyline);
                    this.ipointCollection_0 = null;
                }
                if (polyline != null)
                {
                    this.bool_0 = false;
                    this.ipointCollection_0 = null;
                    IPoint fromPoint = polyline.FromPoint;
                    fromPoint.Z = this.isurface_0.GetElevation(fromPoint);
                    IPoint toPoint = polyline.ToPoint;
                    toPoint.Z = this.isurface_0.GetElevation(toPoint);
                    if (!this.isurface_0.IsVoidZ(fromPoint.Z) && !this.isurface_0.IsVoidZ(toPoint.Z))
                    {
                        fromPoint.Z += ToolSceneLineOfSight.ObserverOffset;
                        toPoint.Z += ToolSceneLineOfSight.TargetOffset;
                        object obj3 = 1;
                        IPoint point3;
                        IPolyline ipolyline_;
                        IPolyline ipolyline_2;
                        bool flag;
                        this.isurface_0.GetLineOfSight(fromPoint, toPoint, out point3, out ipolyline_, out ipolyline_2, out flag, ToolSceneLineOfSight.bCurvEnabled, ToolSceneLineOfSight.bCurvEnabled, ref obj3);
                        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                        IRgbColor rgbColor = new RgbColor();
                        simpleLineSymbol.Width = 2.0;
                        simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                        IMultiPatch multiPatch = new MultiPatch() as IMultiPatch;
                        multiPatch.SpatialReference = this._plugin.Scene.SpatialReference;
                        IMultiPatch multiPatch2 = new MultiPatch() as IMultiPatch;
                        multiPatch2.SpatialReference = this._plugin.Scene.SpatialReference;
                        double num;
                        GeometryOperator.CreateVerticalLOSPatches(flag, fromPoint, toPoint, ipolyline_, ipolyline_2, multiPatch as IGeometryCollection, multiPatch2 as IGeometryCollection, out num);
                        if (!multiPatch.IsEmpty)
                        {
                            rgbColor.Green = 255;
                            rgbColor.Red = 0;
                            simpleFillSymbol.Color = rgbColor;
                            this.AddGraphic(this._plugin.Scene, multiPatch, simpleFillSymbol as ISymbol, false, false, "");
                        }
                        if (!multiPatch2.IsEmpty)
                        {
                            rgbColor.Green = 0;
                            rgbColor.Red = 255;
                            simpleFillSymbol.Color = rgbColor;
                            this.AddGraphic(this._plugin.Scene, multiPatch2, simpleFillSymbol as ISymbol, false, false, "");
                        }
                    }
                }
            }
        }

        public void AddGraphic(IScene iscene_0, IGeometry igeometry_0, ISymbol isymbol_0, bool bool_1, bool bool_2, string string_0)
        {
            if (!igeometry_0.IsEmpty)
            {
                IGraphicsLayer basicGraphicsLayer = iscene_0.BasicGraphicsLayer;
                IElement element = null;
                esriGeometryType geometryType = igeometry_0.GeometryType;
                switch (geometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    {
                        element = new MarkerElement();
                        IMarkerElement markerElement = element as IMarkerElement;
                        if (isymbol_0 != null)
                        {
                            markerElement.Symbol = (isymbol_0 as IMarkerSymbol);
                        }
                        else
                        {
                            markerElement.Symbol = new SimpleMarkerSymbol();
                        }
                        break;
                    }
                    case esriGeometryType.esriGeometryMultipoint:
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                    {
                        element = new LineElement();
                        ILineElement lineElement = element as ILineElement;
                        if (isymbol_0 != null)
                        {
                            lineElement.Symbol = (isymbol_0 as ILineSymbol);
                        }
                        else
                        {
                            lineElement.Symbol = new SimpleLineSymbol();
                        }
                        break;
                    }
                    case esriGeometryType.esriGeometryPolygon:
                    {
                        element = new PolygonElement();
                        IFillShapeElement fillShapeElement = element as IFillShapeElement;
                        if (isymbol_0 != null)
                        {
                            fillShapeElement.Symbol = (isymbol_0 as IFillSymbol);
                        }
                        else
                        {
                            fillShapeElement.Symbol = new SimpleFillSymbol();
                        }
                        break;
                    }
                    default:
                        if (geometryType == esriGeometryType.esriGeometryMultiPatch)
                        {
                            element = new MultiPatchElement();
                            IFillShapeElement fillShapeElement = element as IFillShapeElement;
                            if (isymbol_0 != null)
                            {
                                fillShapeElement.Symbol = (isymbol_0 as IFillSymbol);
                            }
                            else
                            {
                                fillShapeElement.Symbol = new SimpleFillSymbol();
                            }
                        }
                        break;
                }
                if (element != null)
                {
                    element.Geometry = igeometry_0;
                    if (string_0.Length > 0)
                    {
                        IElementProperties elementProperties = element as IElementProperties;
                        elementProperties.Name = string_0;
                    }
                    IGraphicsContainer3D graphicsContainer3D = basicGraphicsLayer as IGraphicsContainer3D;
                    graphicsContainer3D.AddElement(element);
                    IGraphicsSelection graphicsSelection = graphicsContainer3D as IGraphicsSelection;
                    if (bool_2)
                    {
                        if (!bool_1)
                        {
                            graphicsSelection.UnselectAllElements();
                        }
                        graphicsSelection.SelectElement(element);
                    }
                    iscene_0.SceneGraph.RefreshViewers();
                }
            }
        }

        private static void old_acctor_mc()
        {
            ToolSceneLineOfSight.bCurvEnabled = false;
            ToolSceneLineOfSight.ObserverOffset = 0.0;
            ToolSceneLineOfSight.TargetOffset = 0.0;
        }
    }
}