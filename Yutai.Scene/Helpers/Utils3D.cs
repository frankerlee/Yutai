using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Scene.Helpers
{
    internal sealed class Utils3D
    {
        internal enum esriCursorTypes
        {
            cursorDefault,
            cursorHourglass = 2
        }

        public const short SWP_NOSIZE = 1;

        public const short SWP_NOMOVE = 2;

        public const short SWP_NOZORDER = 4;

        public const short SWP_NOREDRAW = 8;

        public const short SWP_NOACTIVATE = 16;

        public const short SWP_FRAMECHANGED = 32;

        public const short SWP_SHOWWINDOW = 64;

        public const short SWP_HIDEWINDOW = 128;

        public const short SWP_NOCOPYBITS = 256;

        public const short SWP_NOOWNERZORDER = 512;

        public const short SWP_DRAWFRAME = 32;

        public const short SWP_NOREPOSITION = 512;

        public const int FLAGS = 3;

        public const short HWND_TOP = 0;

        public const short HWND_BOTTOM = 1;

        public const short HWND_TOPMOST = -1;

        public const short HWND_NOTOPMOST = -2;

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int ShowCursor(int int_0);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern void Sleep(int int_0);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int SetWindowPos(int int_0, int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);

        public static void SetWin_NOTOPMOST(int int_0)
        {
            Utils3D.SetWindowPos(int_0, -2, 0, 0, 0, 0, 3);
        }

        public static void SetWin_TOPMOST(int int_0)
        {
            Utils3D.SetWindowPos(int_0, -1, 0, 0, 0, 0, 3);
        }

        public static string FormatValue(object object_0)
        {
            string result = "";
            if (Convert.ToDouble(object_0) > 1.0)
            {
                result = object_0.ToString();
            }
            else
            {
                object_0.ToString();
            }
            return result;
        }

        public static string GeomTypeAsString(IGeometry igeometry_0)
        {
            string result;
            switch (igeometry_0.GeometryType)
            {
                case esriGeometryType.esriGeometryNull:
                    result = "Null";
                    return result;
                case esriGeometryType.esriGeometryPoint:
                    result = "Point";
                    return result;
                case esriGeometryType.esriGeometryMultipoint:
                    result = "MultiPoint";
                    return result;
                case esriGeometryType.esriGeometryPolyline:
                    result = "Polyline";
                    return result;
                case esriGeometryType.esriGeometryPolygon:
                    result = "Polygon";
                    return result;
                case esriGeometryType.esriGeometryEnvelope:
                    result = "Envelope";
                    return result;
                case esriGeometryType.esriGeometryPath:
                    result = "Path";
                    return result;
                case esriGeometryType.esriGeometryAny:
                    result = "Any";
                    return result;
                case esriGeometryType.esriGeometryMultiPatch:
                    result = "Multipatch";
                    return result;
                case esriGeometryType.esriGeometryRing:
                    result = "Ring";
                    return result;
                case esriGeometryType.esriGeometryLine:
                    result = "Line";
                    return result;
                case esriGeometryType.esriGeometryCircularArc:
                    result = "CircularArc";
                    return result;
                case esriGeometryType.esriGeometryBezier3Curve:
                    result = "Bezier3Curve";
                    return result;
                case esriGeometryType.esriGeometryEllipticArc:
                    result = "EllipticArc";
                    return result;
                case esriGeometryType.esriGeometryBag:
                    result = "Bag";
                    return result;
                case esriGeometryType.esriGeometryTriangleStrip:
                    result = "TriangleStrip";
                    return result;
                case esriGeometryType.esriGeometryTriangleFan:
                    result = "TriangleFan";
                    return result;
                case esriGeometryType.esriGeometryRay:
                    result = "Ray";
                    return result;
                case esriGeometryType.esriGeometrySphere:
                    result = "Sphere";
                    return result;
            }
            result = "Other";
            return result;
        }

        public static void MakeZMAware(IGeometry igeometry_0, bool bool_0)
        {
            if (igeometry_0 is IZAware)
            {
                IZAware iZAware = igeometry_0 as IZAware;
                if (iZAware.ZAware && !bool_0)
                {
                    iZAware.DropZs();
                }
                iZAware.ZAware = bool_0;
            }
            if (igeometry_0 is IMAware)
            {
                IMAware iMAware = igeometry_0 as IMAware;
                if (iMAware.MAware && !bool_0)
                {
                    iMAware.DropMs();
                }
                iMAware.MAware = bool_0;
            }
        }

        public static void MakeOffsetZ(IGeometry igeometry_0, double double_0)
        {
            if (igeometry_0 is IZ)
            {
                Utils3D.MakeZMAware(igeometry_0, true);
                IZ iZ = igeometry_0 as IZ;
                iZ.CalculateNonSimpleZs();
                iZ.OffsetZs(double_0);
            }
        }

        public static void MakeConstantZ(IGeometry igeometry_0, double double_0)
        {
            if (igeometry_0 is IZ)
            {
                Utils3D.MakeZMAware(igeometry_0, true);
                IZ iZ = igeometry_0 as IZ;
                iZ.CalculateNonSimpleZs();
                iZ.SetConstantZ(double_0);
            }
        }

        public static void MakeConstantZAsMaxZ(IPolyline ipolyline_0)
        {
            if (ipolyline_0 is IZ)
            {
                IZAware iZAware = ipolyline_0 as IZAware;
                iZAware.ZAware = true;
                IZ iZ = ipolyline_0 as IZ;
                iZ.SetConstantZ(iZ.ZMax);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Geometry is not IZ.", "MakeConstantZAsMaxZ");
            }
        }

        public static string DataDir(bool bool_0)
        {
            return "c:\\";
        }

        public static string TextureDir()
        {
            return "c:\\";
        }

        public static string SnapshotsDir()
        {
            return "c:\\";
        }

        public static IPoint XYToFeature(ISceneGraph isceneGraph_0, int int_0, int int_1, out IFeature ifeature_0, out IFeatureLayer ifeatureLayer_0)
        {
            ifeature_0 = null;
            ifeatureLayer_0 = null;
            ISceneViewer activeViewer = isceneGraph_0.ActiveViewer;
            IPoint result;
            object obj;
            object obj2;
            isceneGraph_0.Locate(activeViewer, int_0, int_1, esriScenePickMode.esriScenePickGeography, true, out result, out obj, out obj2);
            if (obj2 != null && obj2 is IFeature)
            {
                ifeature_0 = (obj2 as IFeature);
                ifeatureLayer_0 = (obj as IFeatureLayer);
            }
            obj = null;
            obj2 = null;
            return result;
        }

        public static IPoint XYToPoint(ISceneGraph isceneGraph_0, int int_0, int int_1)
        {
            IPoint result = null;
            ISceneViewer activeViewer = isceneGraph_0.ActiveViewer;
            object obj;
            object obj2;
            isceneGraph_0.Locate(activeViewer, int_0, int_1, esriScenePickMode.esriScenePickGeography, true, out result, out obj, out obj2);
            obj = null;
            obj2 = null;
            return result;
        }

        public static bool AdjustSpatialRef(IGeometry igeometry_0, ISpatialReference ispatialReference_0)
        {
            bool result = false;
            IClone clone = igeometry_0.SpatialReference as IClone;
            if (!(clone is IUnknownCoordinateSystem) && !clone.IsEqual(ispatialReference_0 as IClone))
            {
                igeometry_0.Project(ispatialReference_0);
                result = true;
            }
            return result;
        }

        public static IPoint XYToPoint2(ISceneGraph isceneGraph_0, int int_0, int int_1, esriScenePickMode esriScenePickMode_0)
        {
            IPoint result = null;
            ISceneViewer activeViewer = isceneGraph_0.ActiveViewer;
            IHit3DSet hit3DSet;
            isceneGraph_0.LocateMultiple(activeViewer, int_0, int_1, esriScenePickMode_0, true, out hit3DSet);
            if (hit3DSet != null)
            {
                hit3DSet.OnePerLayer();
                IArray hits = hit3DSet.Hits;
                for (int i = 0; i <= hits.Count - 1; i++)
                {
                    IHit3D hit3D = hits.get_Element(i) as IHit3D;
                    result = hit3D.Point;
                }
            }
            return result;
        }

        public static IPoint XYToPoint3(ISceneGraph isceneGraph_0, int int_0, int int_1, ILayer ilayer_0)
        {
            IPoint result = null;
            ISceneViewer activeViewer = isceneGraph_0.ActiveViewer;
            IHit3DSet hit3DSet;
            isceneGraph_0.LocateMultiple(activeViewer, int_0, int_1, esriScenePickMode.esriScenePickGeography, true, out hit3DSet);
            if (hit3DSet != null)
            {
                hit3DSet.OnePerLayer();
                hit3DSet.Topmost(1.0);
                IArray hits = hit3DSet.Hits;
                for (int i = 0; i <= hits.Count - 1; i++)
                {
                    IHit3D hit3D = hits.get_Element(i) as IHit3D;
                    if (hit3D.Owner == ilayer_0)
                    {
                        result = hit3D.Point;
                    }
                }
            }
            return result;
        }

        public static void DeleteAllElementsByName(object object_0, string string_0)
        {
            if (object_0 is IScenePlugin)
            {
                IGraphicsContainer3D graphicsContainer3D = (object_0 as IScenePlugin).Scene.BasicGraphicsLayer as IGraphicsContainer3D;
                graphicsContainer3D.Reset();
                for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                {
                    if (elementProperties.Name.ToLower() == string_0.ToLower())
                    {
                        graphicsContainer3D.DeleteElement(elementProperties as IElement);
                        graphicsContainer3D.Reset();
                    }
                }
                if (graphicsContainer3D.ElementCount > 0)
                {
                    if (string_0 == "")
                    {
                        graphicsContainer3D.DeleteAllElements();
                    }
                    else
                    {
                        graphicsContainer3D.Reset();
                        for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                        {
                            if (elementProperties.Name.ToLower() == string_0.ToLower())
                            {
                                graphicsContainer3D.DeleteElement(elementProperties as IElement);
                                graphicsContainer3D.Reset();
                            }
                        }
                    }
                }
            }
            else if (object_0 is IGraphicsContainer3D)
            {
                IGraphicsContainer3D graphicsContainer3D = object_0 as IGraphicsContainer3D;
                if (graphicsContainer3D.ElementCount > 0)
                {
                    if (string_0 == "")
                    {
                        graphicsContainer3D.DeleteAllElements();
                    }
                    else
                    {
                        graphicsContainer3D.Reset();
                        for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                        {
                            if (elementProperties.Name.ToLower() == string_0.ToLower())
                            {
                                graphicsContainer3D.DeleteElement(elementProperties as IElement);
                                graphicsContainer3D.Reset();
                            }
                        }
                    }
                }
            }
        }

        public static void DeleteAllElementsWithName(object object_0, string string_0)
        {
            if (object_0 is IScenePlugin)
            {
                IGraphicsContainer3D graphicsContainer3D = (object_0 as IScenePlugin).Scene.BasicGraphicsLayer as IGraphicsContainer3D;
                graphicsContainer3D.Reset();
                for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                {
                    if (elementProperties.Name.ToLower() == string_0.ToLower())
                    {
                        graphicsContainer3D.DeleteElement(elementProperties as IElement);
                        graphicsContainer3D.Reset();
                    }
                }
                if (graphicsContainer3D.ElementCount > 0)
                {
                    graphicsContainer3D.Reset();
                    for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                    {
                        if (elementProperties.Name.IndexOf(string_0) > 0)
                        {
                            graphicsContainer3D.DeleteElement(elementProperties as IElement);
                            graphicsContainer3D.Reset();
                        }
                    }
                }
            }
            else if (object_0 is IGraphicsContainer3D)
            {
                IGraphicsContainer3D graphicsContainer3D = object_0 as IGraphicsContainer3D;
                if (graphicsContainer3D.ElementCount > 0)
                {
                    graphicsContainer3D.Reset();
                    for (IElementProperties elementProperties = graphicsContainer3D.Next() as IElementProperties; elementProperties != null; elementProperties = (graphicsContainer3D.Next() as IElementProperties))
                    {
                        if (elementProperties.Name.IndexOf(string_0) > 0)
                        {
                            graphicsContainer3D.DeleteElement(elementProperties as IElement);
                            graphicsContainer3D.Reset();
                        }
                    }
                }
            }
        }

        public static IElement AddSimpleGraphic(IGeometry igeometry_0, IRgbColor irgbColor_0, int int_0, string string_0, object object_0)
        {
            IElement element = null;
            IElement result;
            if (igeometry_0.IsEmpty)
            {
                result = null;
            }
            else
            {
                esriGeometryType geometryType = igeometry_0.GeometryType;
                IElement element2;
                switch (geometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    {
                        element2 = new MarkerElement();
                        IMarkerElement markerElement = element2 as IMarkerElement;
                        markerElement.Symbol = new SimpleMarkerSymbol
                        {
                            Color = irgbColor_0,
                            Size = (double)int_0,
                            Style = esriSimpleMarkerStyle.esriSMSCircle
                        };
                        goto IL_132;
                    }
                    case esriGeometryType.esriGeometryMultipoint:
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                    {
                        element2 = new LineElement();
                        ILineElement lineElement = element2 as ILineElement;
                        lineElement.Symbol = new SimpleLineSymbol
                        {
                            Width = (double)int_0,
                            Color = irgbColor_0,
                            Style = esriSimpleLineStyle.esriSLSSolid
                        };
                        goto IL_132;
                    }
                    case esriGeometryType.esriGeometryPolygon:
                    {
                        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                        simpleLineSymbol.Width = (double)int_0;
                        simpleLineSymbol.Color = irgbColor_0;
                        simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                        element2 = new PolygonElement();
                        IFillShapeElement fillShapeElement = element2 as IFillShapeElement;
                        fillShapeElement.Symbol = new SimpleFillSymbol
                        {
                            Color = irgbColor_0,
                            Outline = simpleLineSymbol
                        };
                        goto IL_132;
                    }
                    default:
                        if (geometryType == esriGeometryType.esriGeometryMultiPatch)
                        {
                            element2 = new MultiPatchElement();
                            IFillShapeElement fillShapeElement2 = element2 as IFillShapeElement;
                            IFillSymbol symbol = fillShapeElement2.Symbol;
                            symbol.Color = irgbColor_0;
                            fillShapeElement2.Symbol = symbol;
                            goto IL_132;
                        }
                        break;
                }
                result = element;
                return result;
                IL_132:
                Utils3D.MakeZMAware(igeometry_0, true);
                element2.Geometry = igeometry_0;
                IElementProperties elementProperties = element2 as IElementProperties;
                elementProperties.Name = string_0;
                if (object_0 is IGraphicsContainer3D)
                {
                    IGraphicsContainer3D graphicsContainer3D = object_0 as IGraphicsContainer3D;
                    graphicsContainer3D.AddElement(element2);
                }
                else if (object_0 is IScenePlugin)
                {
                    IGraphicsContainer3D graphicsContainer3D2 = (object_0 as IScenePlugin).Scene.BasicGraphicsLayer as IGraphicsContainer3D;
                    graphicsContainer3D2.AddElement(element2);
                }
                else if (object_0 is IGroupElement)
                {
                    IGroupElement groupElement = object_0 as IGroupElement;
                    groupElement.AddElement(element2);
                }
                result = element2;
            }
            return result;
        }

        public static IMultiPatch EnvelopeToBoundingBox(IEnvelope2 ienvelope2_0)
        {
            object value = Missing.Value;
            double double_;
            double double_2;
            double double_3;
            double double_4;
            ienvelope2_0.QueryCoords(out double_, out double_2, out double_3, out double_4);
            double zMin = ienvelope2_0.ZMin;
            double zMax = ienvelope2_0.ZMax;
            IMultiPatch multiPatch = new MultiPatch() as IMultiPatch;
            IGeometryCollection geometryCollection = multiPatch as IGeometryCollection;
            IPointCollection pointCollection = new TriangleStrip();
            Utils3D.MakeZMAware(pointCollection as IGeometry, true);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_, double_2, zMin), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_, double_2, zMax), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_3, double_2, zMin), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_3, double_2, zMax), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_3, double_4, zMin), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_3, double_4, zMax), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_, double_4, zMin), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_, double_4, zMax), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_, double_2, zMin), ref value, ref value);
            pointCollection.AddPoint(Utils3D.Create3DPoint(double_, double_2, zMax), ref value, ref value);
            geometryCollection.AddGeometry(pointCollection as IGeometry, ref value, ref value);
            return multiPatch;
        }

        public static IPoint Create3DPoint(double double_0, double double_1, double double_2)
        {
            return new Point
            {
                X = double_0,
                Y = double_1,
                Z = double_2
            };
        }

        public static void AddGroupElement(IGroupElement igroupElement_0, ISceneGraph isceneGraph_0)
        {
            IGraphicsContainer3D graphicsContainer3D = isceneGraph_0.Scene.BasicGraphicsLayer as IGraphicsContainer3D;
            graphicsContainer3D.AddElement(igroupElement_0 as IElement);
        }

        public static void RemoveGroupElement(IGroupElement igroupElement_0, ISceneGraph isceneGraph_0)
        {
            if (igroupElement_0.ElementCount > 0)
            {
                igroupElement_0.ClearElements();
                IGraphicsContainer3D graphicsContainer3D = isceneGraph_0.Scene.BasicGraphicsLayer as IGraphicsContainer3D;
                graphicsContainer3D.DeleteElement(igroupElement_0 as IElement);
            }
        }

        public static void FlashLocation3D(ISceneGraph isceneGraph_0, IPoint ipoint_0)
        {
            IDisplay3D display3D = isceneGraph_0 as IDisplay3D;
            display3D.FlashLocation(ipoint_0);
        }

        public static void FlashGeometry3D(ISceneGraph isceneGraph_0, IGeometry igeometry_0)
        {
            IDisplay3D display3D = isceneGraph_0 as IDisplay3D;
            display3D.AddFlashFeature(igeometry_0);
            display3D.FlashFeatures();
        }

        public static IPointCollection TriangleToPolyline(ITinTriangle itinTriangle_0)
        {
            IPointCollection pointCollection = new Ring();
            itinTriangle_0.QueryAsRing(pointCollection as IRing);
            IPointCollection pointCollection2 = new Polyline();
            pointCollection2.AddPointCollection(pointCollection);
            return pointCollection2;
        }

        public static IPointCollection TriangleToPolygon(ITinTriangle itinTriangle_0)
        {
            IPointCollection pointCollection = new Ring();
            itinTriangle_0.QueryAsRing(pointCollection as IRing);
            IPointCollection pointCollection2 = new Polygon();
            pointCollection2.AddPointCollection(pointCollection);
            return pointCollection2;
        }

        public static string GetLastBrowseLocation()
        {
            return "";
        }

        public static IEnumLayer GetMapLayers(ISceneGraph isceneGraph_0)
        {
            IBasicMap basicMap = isceneGraph_0.Scene as IBasicMap;
            IEnumLayer result;
            if (basicMap.LayerCount == 0)
            {
                result = null;
            }
            else
            {
                result = basicMap.get_Layers(null, true);
            }
            return result;
        }

        public static ILayer GetLayerByName(ISceneGraph isceneGraph_0, string string_0)
        {
            IBasicMap basicMap = isceneGraph_0.Scene as IBasicMap;
            ILayer result;
            if (basicMap.LayerCount > 0)
            {
                IEnumLayer enumLayer = basicMap.get_Layers(null, true);
                enumLayer.Reset();
                for (ILayer layer = enumLayer.Next(); layer != null; layer = enumLayer.Next())
                {
                    if (layer.Name.ToUpper() == string_0.ToUpper())
                    {
                        ILayer layer2 = layer;
                        result = layer2;
                        return result;
                    }
                }
            }
            result = null;
            return result;
        }

        public static void RefreshApp(ISceneGraph isceneGraph_0)
        {
            isceneGraph_0.RefreshViewers();
        }

        public static IRgbColor RandomColor()
        {
            IHsvColor hsvColor = new HsvColor();
            Random random = new Random();
            hsvColor.Hue = 359 * random.Next() + 1;
            hsvColor.Saturation = 100;
            hsvColor.Value = 100;
            return new RgbColor
            {
                RGB = hsvColor.RGB
            };
        }

        public static double RadiansToDegrees(double double_0)
        {
            double num = 4.0 * Math.Atan(1.0);
            double num2 = 180.0 / num;
            return double_0 * num2;
        }

        public static double DegreesToRadians(double double_0)
        {
            double num = 4.0 * Math.Atan(1.0);
            double num2 = num / 180.0;
            return double_0 * num2;
        }

        public static IMultiPatch Cylinder(IPoint ipoint_0, double double_0, double double_1, double double_2, double double_3, double double_4, bool bool_0, bool bool_1)
        {
            double num = 36.0;
            object value = Missing.Value;
            double num2 = (double_2 - double_1) / num;
            double num3 = double_2 - double_1;
            IMultiPatch multiPatch = new MultiPatch() as IMultiPatch;
            IGeometryCollection geometryCollection = multiPatch as IGeometryCollection;
            IPointCollection pointCollection = new TriangleStrip();
            IVector3D vector3D = new Vector3D() as IVector3D;
            IEncode3DProperties encode3DProperties = new GeometryEnvironment() as IEncode3DProperties;
            for (double num4 = double_2; num4 <= double_1; num4 += -num2)
            {
                double num5 = Utils3D.DegreesToRadians(num4);
                vector3D.PolarSet(-num5, 0.0, double_0);
                IPoint point = new Point();
                point.X = ipoint_0.X + vector3D.XComponent;
                point.Y = ipoint_0.Y + vector3D.YComponent;
                point.Z = double_3;
                double num6 = (num4 - double_1) / num3;
                if (bool_0)
                {
                    num6 = 1.0 + num6 * -1.0;
                }
                if (num6 <= 0.0)
                {
                    num6 = 0.001;
                }
                else if (num6 >= 1.0)
                {
                    num6 = 0.999;
                }
                double textureT;
                if (bool_1)
                {
                    textureT = 0.0;
                }
                else
                {
                    textureT = 1.0;
                }
                double m = 0.0;
                encode3DProperties.PackTexture2D(num6, textureT, out m);
                point.M = m;
                pointCollection.AddPoint(point, ref value, ref value);
                IClone clone = point as IClone;
                IPoint point2 = clone.Clone() as IPoint;
                point2.Z = double_4;
                if (bool_1)
                {
                    textureT = 1.0;
                }
                else
                {
                    textureT = 0.0;
                }
                m = 0.0;
                encode3DProperties.PackTexture2D(num6, textureT, out m);
                point2.M = m;
                pointCollection.AddPoint(point2, ref value, ref value);
            }
            IGeometry2 inGeometry = pointCollection as IGeometry2;
            geometryCollection.AddGeometry(inGeometry, ref value, ref value);
            IZAware iZAware = multiPatch as IZAware;
            iZAware.ZAware = true;
            IMAware iMAware = multiPatch as IMAware;
            iMAware.MAware = true;
            return multiPatch;
        }

        public static IGraphicsContainer3D Add3DGraphicsLayer(string string_0, ISceneGraph isceneGraph_0)
        {
            IGraphicsContainer3D result = null;
            IScene scene = isceneGraph_0.Scene;
            bool flag = false;
            if (scene.LayerCount > 0)
            {
                IEnumLayer enumLayer = scene.get_Layers(null, true);
                enumLayer.Reset();
                for (ILayer layer = enumLayer.Next(); layer != null; layer = enumLayer.Next())
                {
                    if (layer.Name == string_0 && layer is IGraphicsContainer3D)
                    {
                        result = (layer as IGraphicsContainer3D);
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                ILayer layer2 = new GraphicsLayer3D();
                layer2.Name = string_0;
                scene.AddLayer(layer2, true);
                result = (layer2 as IGraphicsContainer3D);
                Utils3D.RefreshApp(isceneGraph_0);
            }
            return result;
        }

        public static ILayer GetFeatureLayer(ref IBasicMap ibasicMap_0, ref string string_0)
        {
            ILayer result;
            if (ibasicMap_0.LayerCount > 0)
            {
                IEnumLayer enumLayer = ibasicMap_0.get_Layers(null, true);
                enumLayer.Reset();
                for (ILayer layer = enumLayer.Next(); layer != null; layer = enumLayer.Next())
                {
                    if (layer.Name == string_0 && layer is IFeatureLayer && layer.Valid)
                    {
                        result = layer;
                        return result;
                    }
                }
            }
            result = null;
            return result;
        }

        public static IPolycurve DensifyPolycurve(ref IPolycurve ipolycurve_0, ref int int_0)
        {
            IPolycurve polycurve = Utils3D.CreateClone(ipolycurve_0 as IClone) as IPolycurve;
            polycurve.Densify(Convert.ToDouble(polycurve.Length / (double)int_0), 0.0);
            return polycurve;
        }

        public static IClone CreateClone(IClone iclone_0)
        {
            return iclone_0.Clone();
        }

        public static int ThinPolyline3D(IPointCollection ipointCollection_0)
        {
            int num = -1;
            int index = 0;
            int num2 = -1;
            int num3 = 0;
            while (ipointCollection_0.PointCount != num2)
            {
                num2 = ipointCollection_0.PointCount;
                ISegmentCollection segmentCollection = ipointCollection_0 as ISegmentCollection;
                IEnumSegment enumSegments = segmentCollection.EnumSegments;
                enumSegments.Reset();
                ISegment segment;
                enumSegments.Next(out segment, ref num, ref index);
                ISegment segment2;
                enumSegments.Next(out segment2, ref num, ref index);
                while (segment2 != null)
                {
                    int num4 = segment.ReturnTurnDirection(segment2);
                    if (num4 == 1)
                    {
                        ipointCollection_0.RemovePoints(index, 1);
                    }
                    segment = segment2;
                    enumSegments.Next(out segment2, ref num, ref index);
                }
                num3++;
            }
            return num3;
        }

        private static IPointCollection ReturnMidPoints(IPointCollection ipointCollection_0)
        {
            IPointCollection pointCollection = new Polyline();
            object value = Missing.Value;
            for (int i = 1; i <= ipointCollection_0.PointCount - 2; i++)
            {
                IPoint point = ipointCollection_0.get_Point(i);
                IPoint point2 = ipointCollection_0.get_Point(i + 1);
                IPath path = new Path() as IPath;
                Utils3D.MakeZMAware(path, true);
                path.FromPoint = point;
                path.ToPoint = point2;
                IPoint point3 = new Point();
                Utils3D.MakeZMAware(point3, true);
                path.QueryPoint(esriSegmentExtension.esriNoExtension, 0.5, true, point3);
                point3.Z = (point.Z + point2.Z) / 2.0;
                pointCollection.AddPoint(Utils3D.CreateClone(point3 as IClone) as IPoint, ref value, ref value);
            }
            return null;
        }

        public static IRgbColor GetFeatureColor(IGeoFeatureLayer igeoFeatureLayer_0, IFeature ifeature_0)
        {
            IRgbColor rgbColor = new RgbColor();
            IFeatureRenderer renderer = igeoFeatureLayer_0.Renderer;
            ISymbol symbol = renderer.get_SymbolByFeature(ifeature_0);
            if (symbol is IMarkerSymbol)
            {
                IMarkerSymbol markerSymbol = symbol as IMarkerSymbol;
                rgbColor.RGB = Convert.ToInt32(markerSymbol.Color.RGB);
            }
            else if (symbol is ILineSymbol)
            {
                ILineSymbol lineSymbol = symbol as ILineSymbol;
                rgbColor.RGB = Convert.ToInt32(lineSymbol.Color.RGB);
            }
            else if (symbol is IFillSymbol)
            {
                IFillSymbol fillSymbol = symbol as IFillSymbol;
                rgbColor.RGB = Convert.ToInt32(fillSymbol.Color.RGB);
            }
            else
            {
                rgbColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
            }
            return rgbColor;
        }
    }
}