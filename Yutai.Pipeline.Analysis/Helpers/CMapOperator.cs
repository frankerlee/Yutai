using System.Collections;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Analysis.Helpers
{
    public class CMapOperator
    {


        public static ILayer GetILayerByAliasName(IMap XMap, ILayer ilayer_0, string sAliasName)
        {
            ILayer layer;
            sAliasName = sAliasName.Substring(sAliasName.LastIndexOf(".") + 1);
            if (ilayer_0 != null)
            {
                for (int i = 0; i < ((ICompositeLayer) ilayer_0).Count; i++)
                {
                    ILayer layer1 = ((ICompositeLayer) ilayer_0).Layer[i];
                    if (layer1 is IGroupLayer)
                    {
                        ILayer layerByAliasName = CMapOperator.GetILayerByAliasName(XMap, layer1, sAliasName);
                        if (layerByAliasName != null)
                        {
                            layer = layerByAliasName;
                            return layer;
                        }
                    }
                    else if (layer1 is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = (IFeatureLayer) layer1;
                        if (featureLayer.FeatureClass != null)
                        {
                            string aliasName = featureLayer.FeatureClass.AliasName;
                            aliasName = aliasName.Substring(aliasName.LastIndexOf(".") + 1);
                            if (aliasName.ToUpper() == sAliasName.ToUpper())
                            {
                                layer = layer1;
                                return layer;
                            }
                        }
                    }
                }
                layer = null;
                return layer;
            }
            else
            {
                for (int j = 0; j < XMap.LayerCount; j++)
                {
                    ILayer layer2 = XMap.Layer[j];
                    if (layer2 is IGroupLayer)
                    {
                        ILayer layerByAliasName1 = CMapOperator.GetILayerByAliasName(XMap, layer2, sAliasName);
                        if (layerByAliasName1 != null)
                        {
                            layer = layerByAliasName1;
                            return layer;
                        }
                    }
                    else if (layer2 is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer1 = (IFeatureLayer) layer2;
                        if (featureLayer1.FeatureClass != null)
                        {
                            string str = featureLayer1.FeatureClass.AliasName;
                            str = str.Substring(str.LastIndexOf(".") + 1);
                            if (str.ToUpper() == sAliasName.ToUpper())
                            {
                                layer = layer2;
                                return layer;
                            }
                        }
                    }
                    else if (layer2 is IRasterLayer)
                    {
                        string name = ((IRasterLayer) layer2).Name;
                        name = name.Substring(name.LastIndexOf(".") + 1);
                        if (name.ToUpper() == sAliasName.ToUpper())
                        {
                            layer = layer2;
                            return layer;
                        }
                    }
                }
                layer = null;
                return layer;
            }
            return layer;
        }

        public static ILayer GetILayerByName(IMap XMap, ILayer ilayer_0, string sLayerName)
        {
            ILayer layer;
            if (ilayer_0 != null)
            {
                for (int i = 0; i < ((ICompositeLayer) ilayer_0).Count; i++)
                {
                    ILayer layer1 = ((ICompositeLayer) ilayer_0).Layer[i];
                    if (layer1 is IGroupLayer)
                    {
                        ILayer layerByName = CMapOperator.GetILayerByName(XMap, layer1, sLayerName);
                        if (layerByName != null)
                        {
                            layer = layerByName;
                            return layer;
                        }
                    }
                    else if (layer1.Name == sLayerName)
                    {
                        layer = layer1;
                        return layer;
                    }
                }
                layer = null;
                return layer;
            }
            else
            {
                for (int j = 0; j < XMap.LayerCount; j++)
                {
                    ILayer layer2 = XMap.Layer[j];
                    if (layer2 is IGroupLayer)
                    {
                        ILayer layerByName1 = CMapOperator.GetILayerByName(XMap, layer2, sLayerName);
                        if (layerByName1 != null)
                        {
                            layer = layerByName1;
                            return layer;
                        }
                    }
                    else if (layer2.Name == sLayerName)
                    {
                        layer = layer2;
                        return layer;
                    }
                }
                layer = null;
                return layer;
            }
            return layer;
        }

        public static void GetMapILayers(IMap XMap, ILayer ilayer_0, ArrayList Layers)
        {
            if (ilayer_0 != null)
            {
                for (int i = 0; i < ((ICompositeLayer) ilayer_0).Count; i++)
                {
                    ILayer layer = ((ICompositeLayer) ilayer_0).Layer[i];
                    if (!(layer is IGroupLayer))
                    {
                        Layers.Add(layer);
                    }
                    else
                    {
                        CMapOperator.GetMapILayers(XMap, layer, Layers);
                    }
                }
            }
            else
            {
                for (int j = 0; j < XMap.LayerCount; j++)
                {
                    ILayer layer1 = XMap.Layer[j];
                    if (!(layer1 is IGroupLayer))
                    {
                        Layers.Add(layer1);
                    }
                    else
                    {
                        CMapOperator.GetMapILayers(XMap, layer1, Layers);
                    }
                }
            }
        }

        public static void GetMapLayerAliasNames(IMap XMap, ILayer ilayer_0, ArrayList Layers)
        {
            if (ilayer_0 != null)
            {
                for (int i = 0; i < ((ICompositeLayer) ilayer_0).Count; i++)
                {
                    ILayer layer = ((ICompositeLayer) ilayer_0).Layer[i];
                    if (!(layer is IGroupLayer))
                    {
                        IFeatureLayer featureLayer = (IFeatureLayer) layer;
                        if (featureLayer.FeatureClass != null)
                        {
                            string aliasName = featureLayer.FeatureClass.AliasName;
                            Layers.Add(aliasName.Substring(aliasName.LastIndexOf(".") + 1).ToUpper());
                        }
                    }
                    else
                    {
                        CMapOperator.GetMapLayerAliasNames(XMap, layer, Layers);
                    }
                }
            }
            else
            {
                for (int j = 0; j < XMap.LayerCount; j++)
                {
                    ILayer layer1 = XMap.Layer[j];
                    if (!(layer1 is IGroupLayer))
                    {
                        IFeatureLayer featureLayer1 = (IFeatureLayer) layer1;
                        if (featureLayer1.FeatureClass != null)
                        {
                            string str = featureLayer1.FeatureClass.AliasName;
                            Layers.Add(str.Substring(str.LastIndexOf(".") + 1).ToUpper());
                        }
                    }
                    else
                    {
                        CMapOperator.GetMapLayerAliasNames(XMap, layer1, Layers);
                    }
                }
            }
        }

        public static void GetMapLayerNames(IMap XMap, ILayer ilayer_0, ArrayList Layers)
        {
            if (ilayer_0 != null)
            {
                for (int i = 0; i < ((ICompositeLayer) ilayer_0).Count; i++)
                {
                    ILayer layer = ((ICompositeLayer) ilayer_0).Layer[i];
                    if (!(layer is IGroupLayer))
                    {
                        Layers.Add(layer.Name);
                    }
                    else
                    {
                        CMapOperator.GetMapLayerNames(XMap, layer, Layers);
                    }
                }
            }
            else
            {
                for (int j = 0; j < XMap.LayerCount; j++)
                {
                    ILayer layer1 = XMap.Layer[j];
                    if (!(layer1 is IGroupLayer))
                    {
                        Layers.Add(layer1.Name);
                    }
                    else
                    {
                        CMapOperator.GetMapLayerNames(XMap, layer1, Layers);
                    }
                }
            }


        }

        public static void ShowFeatureWithWink(IDisplay ipDisplay, IGeometry pShape)
        {
            ipDisplay.DisplayTransformation.FromPoints((double)3);
            
            for (int i = 0; i < 3; i++)
            {
                DrawWinkFeature(ipDisplay as IScreenDisplay, pShape);
            }
        }

        public static  void DrawWinkFeature(IScreenDisplay pDisplay, IGeometry pGeo)
        {
            IRgbColor rgbColorClass = new RgbColor()
            {
                Blue = 0,
                Green =0,
                Red = 255,
               
            };
            short activeCache = pDisplay.ActiveCache;
            pDisplay.ActiveCache = -1;
            pDisplay.StartDrawing(pDisplay.hDC, -1);
            switch (pGeo.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    {
                        ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
                        ((ISymbol)simpleMarkerSymbolClass).ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                        simpleMarkerSymbolClass.Color = rgbColorClass;
                        simpleMarkerSymbolClass.Size = (double)9;
                        pDisplay.SetSymbol((ISymbol)simpleMarkerSymbolClass);
                        pDisplay.DrawPoint(pGeo);
                        Thread.Sleep((int)1000);
                        pDisplay.DrawPoint(pGeo);
                        Thread.Sleep((int)1000);
                        pDisplay.FinishDrawing();
                        pDisplay.ActiveCache = activeCache;
                        //mUiCurrentCounter = this;
                        //mUiCurrentCounter.m_uiCurrentCounter = mUiCurrentCounter.m_uiCurrentCounter + 1;
                        return;
                    }
                case esriGeometryType.esriGeometryMultipoint:
                    {
                        pDisplay.FinishDrawing();
                        pDisplay.ActiveCache = activeCache;
                        //mUiCurrentCounter = this;
                        //mUiCurrentCounter.m_uiCurrentCounter = mUiCurrentCounter.m_uiCurrentCounter + 1;
                        return;
                    }
                case esriGeometryType.esriGeometryPolyline:
                    {
                        ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
                        ((ISymbol)simpleLineSymbolClass).ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                        simpleLineSymbolClass.Color = rgbColorClass;
                        simpleLineSymbolClass.Width = (double)3;
                        pDisplay.SetSymbol((ISymbol)simpleLineSymbolClass);
                        pDisplay.DrawPolyline(pGeo);
                        Thread.Sleep((int)1000);
                        pDisplay.DrawPolyline(pGeo);
                        Thread.Sleep((int)1000);
                        pDisplay.FinishDrawing();
                        pDisplay.ActiveCache = activeCache;
                        //mUiCurrentCounter = this;
                        //mUiCurrentCounter.m_uiCurrentCounter = mUiCurrentCounter.m_uiCurrentCounter + 1;
                        return;
                    }
                case esriGeometryType.esriGeometryPolygon:
                    {
                        ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
                        ((ISymbol)simpleFillSymbolClass).ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                        simpleFillSymbolClass.Color = rgbColorClass;
                        pDisplay.SetSymbol((ISymbol)simpleFillSymbolClass);
                        pDisplay.DrawPolygon(pGeo);
                        Thread.Sleep((int)1000);
                        pDisplay.DrawPolygon(pGeo);
                        Thread.Sleep((int)1000);
                        pDisplay.FinishDrawing();
                        pDisplay.ActiveCache = activeCache;
                        //mUiCurrentCounter = this;
                        //mUiCurrentCounter.m_uiCurrentCounter = mUiCurrentCounter.m_uiCurrentCounter + 1;
                        return;
                    }
                default:
                    {
                        pDisplay.FinishDrawing();
                        pDisplay.ActiveCache = activeCache;
                        //mUiCurrentCounter = this;
                        //mUiCurrentCounter.m_uiCurrentCounter = mUiCurrentCounter.m_uiCurrentCounter + 1;
                        return;
                    }
            }
        }

    }
}