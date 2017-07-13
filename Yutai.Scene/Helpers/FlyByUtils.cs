using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Scene.Helpers
{
    internal sealed class FlyByUtils
    {
        internal enum FlyByElementType
        {
            FLYBY_ANCHORS = 2,
            FLYBY_PATH,
            FLYBY_OBSERVER,
            FLYBY_TARGET,
            FLYBY_STATIC,
            FLYBY_BACKDROP,
            FLYBY_ALL
        }

        internal enum FlyByDirection
        {
            FLYBY_DIR_BACKWARDS = -1,
            FLYBY_DIR_FORWARD = 1
        }

        internal enum FlyByLoop
        {
            FLYBY_LOOP_NO = 9,
            FLYBY_LOOP_ONCE,
            FLYBY_LOOP_CONTINUOUS
        }

        public static bool bStillDrawing;

        public static bool bDrawStatic;

        public static IElement pObserverElem;

        public static IElement pTargetElem;

        public static IElement pPathElem;

        private static bool LayerIsExist(IScene iscene_0, ILayer ilayer_0)
        {
            bool result;
            for (int i = 0; i < iscene_0.LayerCount; i++)
            {
                if (iscene_0.get_Layer(i) == ilayer_0)
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static void AddFlyByGraphic(ISceneGraph isceneGraph_0, IGeometry igeometry_0, FlyByUtils.FlyByElementType flyByElementType_0, System.Drawing.Color color_0, System.Drawing.Color color_1, System.Drawing.Color color_2, System.Drawing.Color color_3, bool bool_0)
        {
            if (!igeometry_0.IsEmpty)
            {
                IGraphicsContainer3D graphicsContainer3D = isceneGraph_0.Scene.BasicGraphicsLayer as IGraphicsContainer3D;
                if (!FlyByUtils.LayerIsExist(isceneGraph_0.Scene, graphicsContainer3D as ILayer))
                {
                    isceneGraph_0.Scene.AddLayer(graphicsContainer3D as ILayer, true);
                }
                switch (flyByElementType_0)
                {
                    case FlyByUtils.FlyByElementType.FLYBY_PATH:
                        if (FlyByUtils.pPathElem != null)
                        {
                            graphicsContainer3D.DeleteElement(FlyByUtils.pPathElem);
                        }
                        break;
                    case FlyByUtils.FlyByElementType.FLYBY_OBSERVER:
                        if (FlyByUtils.pObserverElem != null)
                        {
                            graphicsContainer3D.DeleteElement(FlyByUtils.pObserverElem);
                        }
                        break;
                    case FlyByUtils.FlyByElementType.FLYBY_TARGET:
                        if (FlyByUtils.pTargetElem != null)
                        {
                            graphicsContainer3D.DeleteElement(FlyByUtils.pTargetElem);
                        }
                        break;
                }
                ISymbol flyBySymbol = FlyByUtils.GetFlyBySymbol(color_0, color_1, color_2, color_3, flyByElementType_0);
                IElement element;
                switch (igeometry_0.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        {
                            element = new MarkerElement();
                            IMarkerElement markerElement = element as IMarkerElement;
                            markerElement.Symbol = (flyBySymbol as IMarkerSymbol);
                            break;
                        }
                    case esriGeometryType.esriGeometryMultipoint:
                        return;
                    case esriGeometryType.esriGeometryPolyline:
                        {
                            element = new LineElement();
                            ILineElement lineElement = element as ILineElement;
                            lineElement.Symbol = (flyBySymbol as ILineSymbol);
                            break;
                        }
                    default:
                        return;
                }
                switch (flyByElementType_0)
                {
                    case FlyByUtils.FlyByElementType.FLYBY_ANCHORS:
                        {
                            IElementProperties elementProperties = element as IElementProperties;
                            elementProperties.Name = "SceneFlyBy.AnchorPoint";
                            break;
                        }
                    case FlyByUtils.FlyByElementType.FLYBY_PATH:
                        FlyByUtils.pPathElem = element;
                        break;
                    case FlyByUtils.FlyByElementType.FLYBY_OBSERVER:
                        FlyByUtils.pObserverElem = element;
                        break;
                    case FlyByUtils.FlyByElementType.FLYBY_TARGET:
                        FlyByUtils.pTargetElem = element;
                        break;
                    case FlyByUtils.FlyByElementType.FLYBY_STATIC:
                        {
                            IElementProperties elementProperties = element as IElementProperties;
                            elementProperties.Name = "SceneFlyBy.StaticLoc";
                            break;
                        }
                }
                element.Geometry = igeometry_0;
                graphicsContainer3D.AddElement(element);
                if (bool_0)
                {
                    isceneGraph_0.RefreshViewers();
                }
            }
        }

        public static void DeleteFlyByElement(ISceneGraph isceneGraph_0, FlyByUtils.FlyByElementType flyByElementType_0, bool bool_0)
        {
            IGraphicsLayer basicGraphicsLayer = isceneGraph_0.Scene.BasicGraphicsLayer;
            IGraphicsContainer3D graphicsContainer3D = basicGraphicsLayer as IGraphicsContainer3D;
            switch (flyByElementType_0)
            {
                case FlyByUtils.FlyByElementType.FLYBY_PATH:
                    if (FlyByUtils.pPathElem != null)
                    {
                        graphicsContainer3D.DeleteElement(FlyByUtils.pPathElem);
                    }
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_OBSERVER:
                    if (FlyByUtils.pObserverElem != null)
                    {
                        graphicsContainer3D.DeleteElement(FlyByUtils.pObserverElem);
                    }
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_TARGET:
                    if (FlyByUtils.pTargetElem != null)
                    {
                        graphicsContainer3D.DeleteElement(FlyByUtils.pTargetElem);
                    }
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_STATIC:
                    graphicsContainer3D.Reset();
                    for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                    {
                        if (elementProperties.Name == "SceneFlyBy.StaticLoc")
                        {
                            graphicsContainer3D.DeleteElement(elementProperties as IElement);
                            break;
                        }
                    }
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_BACKDROP:
                    graphicsContainer3D.Reset();
                    for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                    {
                        if (elementProperties.Name == "_BACKDROP_")
                        {
                            graphicsContainer3D.DeleteElement(elementProperties as IElement);
                            break;
                        }
                    }
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_ALL:
                    if (FlyByUtils.pPathElem != null)
                    {
                        graphicsContainer3D.DeleteElement(FlyByUtils.pPathElem);
                    }
                    if (FlyByUtils.pObserverElem != null)
                    {
                        graphicsContainer3D.DeleteElement(FlyByUtils.pObserverElem);
                    }
                    if (FlyByUtils.pTargetElem != null)
                    {
                        graphicsContainer3D.DeleteElement(FlyByUtils.pTargetElem);
                    }
                    graphicsContainer3D.Reset();
                    for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                    {
                        if (elementProperties.Name == "_BACKDROP_" || elementProperties.Name == "SceneFlyBy.AnchorPoint" || elementProperties.Name == "SceneFlyBy.StaticLoc")
                        {
                            graphicsContainer3D.DeleteElement(elementProperties as IElement);
                            graphicsContainer3D.Reset();
                        }
                    }
                    break;
            }
            if (bool_0)
            {
                isceneGraph_0.RefreshViewers();
            }
        }

        public static ISymbol GetFlyBySymbol(System.Drawing.Color color_0, System.Drawing.Color color_1, System.Drawing.Color color_2, System.Drawing.Color color_3, FlyByUtils.FlyByElementType flyByElementType_0)
        {
            ISymbol result = null;
            ISimpleLineSymbol simpleLineSymbol = null;
            ISimpleMarkerSymbol simpleMarkerSymbol = null;
            if (flyByElementType_0 == FlyByUtils.FlyByElementType.FLYBY_PATH)
            {
                simpleLineSymbol = new SimpleLineSymbol();
            }
            else
            {
                simpleMarkerSymbol = new SimpleMarkerSymbol();
            }
            IRgbColor rgbColor = new RgbColor();
            switch (flyByElementType_0)
            {
                case FlyByUtils.FlyByElementType.FLYBY_ANCHORS:
                    if (FlyByUtils.bStillDrawing)
                    {
                        rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lime);
                    }
                    else
                    {
                        rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(color_3);
                    }
                    simpleMarkerSymbol.Size = 8.0;
                    simpleMarkerSymbol.Color = rgbColor;
                    simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    result = (simpleMarkerSymbol as ISymbol);
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_PATH:
                    if (FlyByUtils.bStillDrawing)
                    {
                        rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    }
                    else
                    {
                        rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(color_0);
                    }
                    simpleLineSymbol.Width = 4.0;
                    simpleLineSymbol.Color = rgbColor;
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    result = (simpleLineSymbol as ISymbol);
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_OBSERVER:
                    rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(color_1);
                    simpleMarkerSymbol.Color = rgbColor;
                    simpleMarkerSymbol.Size = 8.0;
                    simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    result = (simpleMarkerSymbol as ISymbol);
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_TARGET:
                    rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(color_2);
                    simpleMarkerSymbol.Size = 8.0;
                    simpleMarkerSymbol.Color = rgbColor;
                    simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    result = (simpleMarkerSymbol as ISymbol);
                    break;
                case FlyByUtils.FlyByElementType.FLYBY_STATIC:
                    rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta);
                    simpleMarkerSymbol.Size = 8.0;
                    simpleMarkerSymbol.Color = rgbColor;
                    simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    result = (simpleMarkerSymbol as ISymbol);
                    break;
            }
            return result;
        }
    }
}
