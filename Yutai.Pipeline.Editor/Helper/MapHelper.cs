using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Helper
{
    public class MapHelper
    {
        public static IEnvelope GetCurrentExtent(IMap map)
        {
            IActiveView pView = map as IActiveView;
            if (pView == null)
                return null;
            return pView.Extent;
        }

        public static List<IFeatureLayer> GetFeatureLayers(IMap map, esriGeometryType type = esriGeometryType.esriGeometryAny)
        {
            List<IFeatureLayer> list = new List<IFeatureLayer>();
            if (map == null)
                return list;
            List<IFeatureLayer> tempFeatureLayers = GetAllFeaturelayerInMap(map);
            foreach (IFeatureLayer tempFeatureLayer in tempFeatureLayers)
            {
                if (type == esriGeometryType.esriGeometryAny)
                {
                    list.Add(tempFeatureLayer);
                }
                else if (tempFeatureLayer.FeatureClass != null && tempFeatureLayer.FeatureClass.ShapeType == type)
                {
                    list.Add(tempFeatureLayer);
                }
            }
            return list;
        }

        public static List<IFeatureLayer> GetFeatureLayers(IMap map, List<esriGeometryType> types)
        {
            List<IFeatureLayer> list = new List<IFeatureLayer>();
            if (map == null)
                return list;
            List<IFeatureLayer> tempFeatureLayers = GetAllFeaturelayerInMap(map);
            foreach (IFeatureLayer tempFeatureLayer in tempFeatureLayers)
            {
                if (tempFeatureLayer.FeatureClass != null && types.Contains(tempFeatureLayer.FeatureClass.ShapeType))
                {
                    list.Add(tempFeatureLayer);
                }
            }
            return list;
        }

        public static IFeatureLayer GetFeatureLayerByFeatureClass(IMap map, IFeatureClass featureClass)
        {
            List<IFeatureLayer> tempFeatureLayers = GetAllFeaturelayerInMap(map);
            foreach (IFeatureLayer tempFeatureLayer in tempFeatureLayers)
            {
                if (tempFeatureLayer.FeatureClass.ObjectClassID == featureClass.ObjectClassID)
                {
                    return tempFeatureLayer;
                }
            }
            return null;
        }

        public static List<IFeature> GetFeaturesByEnvelope(IEnvelope envelope, IFeatureLayer featureLayer)
        {
            List<IFeature> list = new List<IFeature>();

            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.GeometryField = featureLayer.FeatureClass.ShapeFieldName;
            pSpatialFilter.Geometry = envelope;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor pFeatureCursor = featureLayer.Search(pSpatialFilter, false);
            IFeature pFeature;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                list.Add(pFeature);
            }
            Marshal.ReleaseComObject(pFeatureCursor);
            return list;
        }

        public static List<IFeature> GetFeaturesBySelected(IFeatureLayer featureLayer)
        {
            List<IFeature> list = new List<IFeature>();

            IFeatureSelection pFeatureSelection = featureLayer as IFeatureSelection;
            if (pFeatureSelection == null)
            {
                return list;
            }
            ISelectionSet pSelectionSet = pFeatureSelection.SelectionSet;
            ICursor pCursor = null;
            pSelectionSet.Search(null, true, out pCursor);
            IFeatureCursor pFeatureCursor = pCursor as IFeatureCursor;

            if (pFeatureCursor == null)
                return list;
            IFeature pFeature;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                list.Add(pFeature);
            }
            Marshal.ReleaseComObject(pFeatureCursor);
            Marshal.ReleaseComObject(pCursor);
            return list;
        }
        
        public static IFeature GetFirstFeatureFromPointSearchInGeoFeatureLayer(Double searchTolerance, IPoint point, IFeatureClass featureClass, IActiveView activeView)
        {
            if (searchTolerance < 0 || point == null || featureClass == null || activeView == null)
            {
                return null;
            }

            IMap map = activeView.FocusMap;

            // Expand the points envelope to give better search results    
            //IEnvelope envelope = point.Envelope;
            //envelope.Expand(searchTolerance, searchTolerance, false);

            ITopologicalOperator pTopologicalOperator = point as ITopologicalOperator;
            String shapeFieldName = featureClass.ShapeFieldName;

            // Create a new spatial filter and use the new envelope as the geometry    
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pTopologicalOperator.Buffer(searchTolerance);
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            spatialFilter.set_OutputSpatialReference(shapeFieldName, map.SpatialReference);
            spatialFilter.GeometryField = shapeFieldName;

            // Do the search
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);

            // Get the first feature
            IFeature feature = featureCursor.NextFeature();
            Marshal.ReleaseComObject(featureCursor);
            if (!(feature == null))
            {
                return feature;
            }
            else
            {
                return null;
            }
        }

        public static IFeatureCursor GetAllFeaturesFromPointSearchInGeoFeatureLayer(Double searchTolerance, IPoint point, IGeoFeatureLayer geoFeatureLayer, IActiveView activeView)
        {

            if (searchTolerance < 0 || point == null || geoFeatureLayer == null || activeView == null)
            {
                return null;
            }
            IMap map = activeView.FocusMap;

            // Expand the points envelope to give better search results    
            //IEnvelope envelope = point.Envelope;
            //envelope.Expand(searchTolerance, searchTolerance, false);

            ITopologicalOperator pTopologicalOperator = point as ITopologicalOperator;

            IFeatureClass featureClass = geoFeatureLayer.FeatureClass;
            String shapeFieldName = featureClass.ShapeFieldName;

            // Create a new spatial filter and use the new envelope as the geometry    
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pTopologicalOperator.Buffer(searchTolerance);
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            spatialFilter.set_OutputSpatialReference(shapeFieldName, map.SpatialReference);
            spatialFilter.GeometryField = shapeFieldName;

            // Do the search
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);

            return featureCursor;
        }

        public static IFeatureCursor GetAllFeaturesFromPointSearchInGeoFeatureLayer(Double searchTolerance, IPoint point, IFeatureClass featureClass, IActiveView activeView)
        {

            if (searchTolerance < 0 || point == null || featureClass == null || activeView == null)
            {
                return null;
            }
            IMap map = activeView.FocusMap;

            // Expand the points envelope to give better search results    
            //IEnvelope envelope = point.Envelope;
            //envelope.Expand(searchTolerance, searchTolerance, false);
            ITopologicalOperator pTopologicalOperator = point as ITopologicalOperator;

            String shapeFieldName = featureClass.ShapeFieldName;

            // Create a new spatial filter and use the new envelope as the geometry    
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pTopologicalOperator.Buffer(searchTolerance);
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            spatialFilter.set_OutputSpatialReference(shapeFieldName, map.SpatialReference);
            spatialFilter.GeometryField = shapeFieldName;

            // Do the search
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);

            return featureCursor;
        }

        

        public static List<IFeature> GetAllFeaturesFromPolygonInGeoFeatureLayer(IPolygon polygon, IGeoFeatureLayer geoFeatureLayer, IActiveView activeView)
        {
            List<IFeature> list = new List<IFeature>();
            if (polygon == null || geoFeatureLayer == null || activeView == null)
            {
                return list;
            }
            IMap pMap = activeView.FocusMap;

            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = polygon;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            pSpatialFilter.set_OutputSpatialReference(geoFeatureLayer.FeatureClass.ShapeFieldName, pMap.SpatialReference);
            pSpatialFilter.GeometryField = geoFeatureLayer.FeatureClass.ShapeFieldName;

            IFeatureCursor pFeatureCursor = geoFeatureLayer.Search(pSpatialFilter, false);
            IFeature pFeature;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                list.Add(pFeature);
            }
            Marshal.ReleaseComObject(pFeatureCursor);
            return list;
        }

        public static List<IFeature> GetAllFeaturesFromPolygonInGeoFeatureLayer(IPolygon polygon, IFeatureLayer featureLayer, IMap map, double distance = 0)
        {
            List<IFeature> list = new List<IFeature>();
            if (polygon == null || featureLayer == null || map == null)
                return list;
            ITopologicalOperator pTopologicalOperator = polygon as ITopologicalOperator;
            if (pTopologicalOperator == null)
                return list;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pTopologicalOperator.Buffer(distance);
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            pSpatialFilter.OutputSpatialReference[featureLayer.FeatureClass.ShapeFieldName] = map.SpatialReference;
            pSpatialFilter.GeometryField = featureLayer.FeatureClass.ShapeFieldName;

            IFeatureCursor pFeatureCursor = featureLayer.Search(pSpatialFilter, false);
            IFeature pFeature;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                list.Add(pFeature);
            }
            Marshal.ReleaseComObject(pFeatureCursor);
            return list;
        }

        public static IFeature GetMaxAreaFeature(List<IFeature> features)
        {
            IArea pMaxArea = features[0].Shape as IArea;
            IFeature pMaxFeature = features[0];
            foreach (IFeature feature in features)
            {
                IArea pArea = feature.Shape as IArea;
                if (pMaxArea.Area < pArea.Area)
                {
                    pMaxArea = pArea;
                    pMaxFeature = feature;
                }
            }
            return pMaxFeature;
        }

        public static IFeature GetMinDistanceFeature(List<IFeature> features, IGeometry geometry)
        {
            double pMinDistance = GetPointToGeometryDistance(geometry, features[0].ShapeCopy);

            IFeature pMinDisFeature = features[0];
            foreach (IFeature feature in features)
            {
                double tempDistance = GetPointToGeometryDistance(geometry, feature.ShapeCopy);
                if (tempDistance < pMinDistance)
                {
                    pMinDistance = tempDistance;
                    pMinDisFeature = feature;
                }
            }
            return pMinDisFeature;
        }

        public static double GetPointToGeometryDistance(IGeometry relGeometry, IGeometry geometry)
        {
            IProximityOperator proximityOperator = geometry as IProximityOperator;
            switch (relGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    {
                        return proximityOperator.ReturnDistance(relGeometry as IPoint);
                    }
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    {
                        return proximityOperator.ReturnDistance(relGeometry as IPolyline);
                    }
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    {
                        return proximityOperator.ReturnDistance(relGeometry as IPolygon);
                    }
                    break;
            }
            return 99999;
        }

        public static IFeature GetFirstAnnoFeatureFromPointSearchInFeatureLayer(Double searchTolerance,
            IPoint
                pPoint,
            IFeatureLayer
                pFeatureLayer,
            IActiveView
                activeView)
        {
            if (searchTolerance < 0 || pPoint == null || pFeatureLayer == null || activeView == null)
            {
                return null;
            }

            IMap m_map = activeView.FocusMap;

            IEnvelope envelope = pPoint.Envelope;
            envelope.Expand(0.1, 0.1, false);
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            string shapeFieldName = pFeatureClass.ShapeFieldName;

            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = envelope;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            spatialFilter.set_OutputSpatialReference(shapeFieldName, m_map.SpatialReference);
            spatialFilter.GeometryField = shapeFieldName;

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(spatialFilter, false);

            IFeature pFeature = pFeatureCursor.NextFeature();

            if (pFeature != null)
            {
                return pFeature;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Zooms to geometry.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="pMap">The p map.</param>
        public static void ZoomToGeometry(IGeometry geometry, IMap pMap)
        {
            if (geometry == null)
            {
                return;
            }

            IRgbColor color = new RgbColor();
            color.Red = 255;
            color.Green = 0;
            color.Blue = 255;
            IEnvelope pEnv = geometry.Envelope;
            if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pEnv.XMax = pEnv.XMax + 20;
                pEnv.XMin = pEnv.XMin - 20;
                pEnv.YMax = pEnv.YMax + 20;
                pEnv.YMin = pEnv.YMin - 20;
            }
            else
            {
                pEnv.Expand(1.2, 1.2, true);
            }
            IActiveView pView = ((IActiveView)pMap);
            pView.Extent = pEnv;
            pView.Refresh();
        }
        
        /// <summary>
        /// Flashes the geometry.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="map">The map.</param>
        public static void FlashGeometry(IGeometry geometry, IMap map)
        {
            if (geometry == null)
            {
                return;
            }

            IRgbColor color = new RgbColor();
            color.Red = 255;
            color.Green = 0;
            color.Blue = 255;
            IDisplay display = ((IActiveView)map).ScreenDisplay as IDisplay;
            Int32 delay = 100;
            display.StartDrawing(display.hDC, (Int16)esriScreenCache.esriNoScreenCache); // Explicit Cast

            switch (geometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPolygon:
                    {
                        //Set the flash geometry's symbol.
                        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                        simpleFillSymbol.Color = color;
                        ISymbol symbol = simpleFillSymbol as ISymbol; // Dynamic Cast
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input polygon geometry.
                        display.SetSymbol(symbol);
                        display.DrawPolygon(geometry);
                        Thread.Sleep(delay);
                        display.DrawPolygon(geometry);
                        break;
                    }

                case esriGeometryType.esriGeometryPolyline:
                    {
                        //Set the flash geometry's symbol.
                        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
                        simpleLineSymbol.Width = 4;
                        simpleLineSymbol.Color = color;
                        ISymbol symbol = simpleLineSymbol as ISymbol; // Dynamic Cast
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input polyline geometry.
                        display.SetSymbol(symbol);
                        display.DrawPolyline(geometry);
                        Thread.Sleep(delay);
                        display.DrawPolyline(geometry);
                        break;
                    }

                case esriGeometryType.esriGeometryPoint:
                    {
                        //Set the flash geometry's symbol.
                        ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
                        simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                        simpleMarkerSymbol.Size = 30;
                        simpleMarkerSymbol.Color = color;
                        ISymbol symbol = simpleMarkerSymbol as ISymbol; // Dynamic Cast
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input point geometry.
                        display.SetSymbol(symbol);
                        display.DrawPoint(geometry);
                        Thread.Sleep(delay);
                        display.DrawPoint(geometry);
                        break;
                    }

                case esriGeometryType.esriGeometryMultipoint:
                    {
                        //Set the flash geometry's symbol.
                        ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
                        simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                        simpleMarkerSymbol.Size = 12;
                        simpleMarkerSymbol.Color = color;
                        ISymbol symbol = simpleMarkerSymbol as ISymbol; // Dynamic Cast
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input multipoint geometry.
                        display.SetSymbol(symbol);
                        display.DrawMultipoint(geometry);
                        Thread.Sleep(delay);
                        display.DrawMultipoint(geometry);
                        break;
                    }
            }
            display.FinishDrawing();
        }

        
        /// <summary>
        /// Pans to geometry.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="pGeometry">The p geometry.</param>
        /// <param name="isFlash">if set to <c>true</c> [is flash].</param>
        /// <param name="mMap"></param>
        public static void PanToGeometry(IActiveView view, IGeometry pGeometry, bool isFlash, IMap m_map)
        {

            if (pGeometry.IsEmpty == true)
                return;
            IEnvelope pEnv = pGeometry.Envelope;
            IEnvelope pExtent = view.Extent;
            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pExtent.CenterAt((IPoint)pGeometry);
            }
            else
            {
                IPoint pPnt = new PointClass();
                pPnt.X = (pEnv.XMin + pEnv.XMax) / 2;
                pPnt.Y = (pEnv.YMin + pEnv.YMax) / 2;
                pExtent.CenterAt(pPnt);
                pPnt = null;

            }
            view.Extent = pExtent;
            view.Refresh();
            MapHelper.FlashGeometry(pGeometry, m_map);
        }
        
        /// <summary>
        /// Gets the name of the layer by.
        /// </summary>
        /// <param name="pMap">The p map.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <param name="isFeatureLayer"></param>
        /// <returns>ILayer.</returns>
        public static ILayer GetLayerByName(IMap pMap, string layerName, bool isFeatureLayer = false)
        {
            try                                                      //try循环遍历所有图层
            {
                IEnumLayer enumLayer = pMap.get_Layers(null, true);   //enumLayer里保存了所有图层
                enumLayer.Reset();
                ILayer layer;
                while ((layer = enumLayer.Next()) != null)
                {
                    if (isFeatureLayer && (layer is IFeatureLayer) == false)
                        continue;
                    if (layer.Name == layerName)
                    {
                        return layer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// Gets the name of the current feature layer by.
        /// </summary>
        /// <param name="pMap">The p map.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>IFeatureLayer.</returns>
        public static IFeatureLayer GetCurrentFeatureLayerByName(IMap pMap, string layerName)
        {
            ILayer pLayer = MapHelper.GetLayerByName(pMap, layerName, true);
            return (IFeatureLayer)pLayer;
        }

        /// <summary>
        /// Gets all featurelayer in map返回地图中包括grouplayer中在内的所有的地理图层featurelayer.
        /// </summary>
        /// <param name="pMap">The p map.</param>
        /// <returns></returns>
        public static List<IFeatureLayer> GetAllFeaturelayerInMap(IMap pMap)
        {
            List<IFeatureLayer> list = new List<IFeatureLayer>();
            IEnumLayer layers = pMap.Layers;
            layers.Reset();
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Visible)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (featureLayer != null)
                    {
                        list.Add(featureLayer);
                    }
                }
                layer = layers.Next();
            }
            return list;
        }

        /// <summary>
        /// Gets all feature layers in map.
        /// </summary>
        /// <param name="pMap">The p map.</param>
        /// <returns></returns>
        public static IDictionary<string, IFeatureLayer> GetAllFeatureLayersInMap(IMap pMap)
        {
            if (pMap == null) return null;

            IDictionary<string, IFeatureLayer> layerlist = new Dictionary<string, IFeatureLayer>();
            try
            {
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    ILayer layer = pMap.get_Layer(i);
                    ReturnAllFeatureLayerInlayerGroup(layer, ref layerlist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return layerlist;
        }

        /// <summary>
        /// Returns all feature layer inlayer group.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="layerlist">The layerlist.</param>
        public static void ReturnAllFeatureLayerInlayerGroup(ILayer layer, ref IDictionary<string, IFeatureLayer> layerlist)
        {
            try
            {
                if (layer is IGroupLayer)
                {
                    ICompositeLayer groupLayer = layer as ICompositeLayer;
                    for (int i = 0; i < groupLayer.Count; i++)
                    {
                        ILayer tempLayer = groupLayer.get_Layer(i);
                        ReturnAllFeatureLayerInlayerGroup(tempLayer, ref layerlist);
                    }
                }
                else
                {
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as FeatureLayer;
                        layerlist.Add(featureLayer.FeatureClass.AliasName, featureLayer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns all feature layer inlayer group.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="layerlist">The layerlist.</param>
        public static void ReturnAllFeatureLayerInlayerGroup(ILayer layer, ref List<IFeatureLayer> layerlist)
        {
            try
            {
                if (layer is IGroupLayer)
                {
                    ICompositeLayer groupLayer = layer as ICompositeLayer;
                    for (int i = 0; i < groupLayer.Count; i++)
                    {
                        ILayer tempLayer = groupLayer.get_Layer(i);
                        ReturnAllFeatureLayerInlayerGroup(tempLayer, ref layerlist);
                    }
                }
                else
                {
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as FeatureLayer;
                        layerlist.Add(featureLayer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过名字获得索引图层.
        /// </summary>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns></returns>
        public static IFeatureLayer GetIndexLayerByName(string layerName, IMap pMap)
        {
            UID pUID = new UIDClass();
            pUID.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer pEnumLayer = pMap.get_Layers(pUID, true);
            pEnumLayer.Reset();
            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer.Name == layerName) return (IFeatureLayer)pLayer;

                pLayer = pEnumLayer.Next();
            }
            return null;
        }

        /// <summary>
        /// Gets all index layers in map.
        /// </summary>
        /// <param name="pMap">The p map.</param>
        /// <returns></returns>
        //public static IDictionary<string, IFeatureLayer> GetAllIndexLayersInMap(IMap pMap)
        //{
        //    if (pMap == null) return null;

        //    IDictionary<string, IFeatureLayer> layerlist = new Dictionary<string, IFeatureLayer>();
        //    try
        //    {
        //        for (int i = 0; i < pMap.LayerCount; i++)
        //        {
        //            ILayer layer = pMap.get_Layer(i);
        //            ReturnAllIndexLayerInlayerGroup(layer, ref layerlist);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    return layerlist;
        //}

        /// <summary>
        /// Returns all index layer inlayer group.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="layerlist">The layerlist.</param>
        //public static void ReturnAllIndexLayerInlayerGroup(ILayer layer, ref IDictionary<string, IFeatureLayer> layerlist)
        //{
        //    try
        //    {
        //        if (layer is IGroupLayer)
        //        {
        //            ICompositeLayer groupLayer = layer as ICompositeLayer;
        //            for (int i = 0; i < groupLayer.Count; i++)
        //            {
        //                ILayer tempLayer = groupLayer.get_Layer(i);
        //                ReturnAllIndexLayerInlayerGroup(tempLayer, ref layerlist);
        //            }
        //        }
        //        else
        //        {
        //            if (layer is IFeatureLayer)
        //            {
        //                IFeatureLayer featureLayer = layer as FeatureLayer;
        //                if (GSWorkspaceHelper.IsGSIndexClass(featureLayer.FeatureClass))
        //                {
        //                    layerlist.Add(featureLayer.FeatureClass.AliasName, featureLayer);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
