using System;
using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Identifer.Common;
using Yutai.Shared;

namespace Yutai.Plugins.Identifer.Helpers
{
    public class MapHelper
    {
        public MapHelper()
        {
        }

        public static void AddDataset(IBasicMap pMap, IDataset pDataset, string dsName)
        {
            IFeatureLayer fDOGraphicsLayerClass;
            IFeatureClass j;
            IFeatureLayer featureLayerClass;
            if (dsName == null)
            {
                dsName = "";
            }
            switch (pDataset.Type)
            {
                case esriDatasetType.esriDTFeatureDataset:
                {
                    IEnumDataset subsets = pDataset.Subsets;
                    subsets.Reset();
                    for (IDataset i = subsets.Next(); i != null; i = subsets.Next())
                    {
                        MapHelper.AddDataset(pMap, i, dsName);
                    }
                    return;
                }
                case esriDatasetType.esriDTFeatureClass:
                {
                    IFeatureClass idataset0 = (IFeatureClass) pDataset;
                    if (idataset0.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        fDOGraphicsLayerClass = new FDOGraphicsLayerClass();
                        try
                        {
                            fDOGraphicsLayerClass.FeatureClass = idataset0;
                            fDOGraphicsLayerClass.Name = string.Concat(dsName, idataset0.AliasName);
                            pMap.AddLayer(fDOGraphicsLayerClass);
                            return;
                        }
                        catch (Exception exception)
                        {
                            exception.ToString();
                            return;
                        }
                    }
                    else if (idataset0.FeatureType != esriFeatureType.esriFTDimension)
                    {
                        fDOGraphicsLayerClass = new FeatureLayerClass()
                        {
                            FeatureClass = idataset0,
                            Name = string.Concat(dsName, idataset0.AliasName)
                        };
                        pMap.AddLayer(fDOGraphicsLayerClass);
                        return;
                    }
                    else
                    {
                        fDOGraphicsLayerClass = new DimensionLayerClass()
                        {
                            FeatureClass = idataset0,
                            Name = string.Concat(dsName, idataset0.AliasName)
                        };
                        pMap.AddLayer(fDOGraphicsLayerClass);
                        return;
                    }
                    break;
                }
                case esriDatasetType.esriDTPlanarGraph:
                case esriDatasetType.esriDTText:
                case esriDatasetType.esriDTRelationshipClass:
                {
                    return;
                }
                case esriDatasetType.esriDTGeometricNetwork:
                {
                    IGeometricNetwork geometricNetwork = pDataset as IGeometricNetwork;
                    if (geometricNetwork == null)
                    {
                        return;
                    }
                    IEnumFeatureClass classesByType =
                        geometricNetwork.ClassesByType[esriFeatureType.esriFTSimpleJunction];
                    classesByType.Reset();
                    for (j = classesByType.Next(); j != null; j = classesByType.Next())
                    {
                        featureLayerClass = new FeatureLayerClass()
                        {
                            FeatureClass = j,
                            Name = string.Concat(dsName, (j as IDataset).Name)
                        };
                        pMap.AddLayer(featureLayerClass);
                    }
                    classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTComplexJunction];
                    classesByType.Reset();
                    for (j = classesByType.Next(); j != null; j = classesByType.Next())
                    {
                        featureLayerClass = new FeatureLayerClass()
                        {
                            FeatureClass = j,
                            Name = string.Concat(dsName, (j as IDataset).Name)
                        };
                        pMap.AddLayer(featureLayerClass);
                    }
                    classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTSimpleEdge];
                    classesByType.Reset();
                    for (j = classesByType.Next(); j != null; j = classesByType.Next())
                    {
                        featureLayerClass = new FeatureLayerClass()
                        {
                            FeatureClass = j,
                            Name = string.Concat(dsName, (j as IDataset).Name)
                        };
                        pMap.AddLayer(featureLayerClass);
                    }
                    classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTComplexEdge];
                    classesByType.Reset();
                    for (j = classesByType.Next(); j != null; j = classesByType.Next())
                    {
                        featureLayerClass = new FeatureLayerClass()
                        {
                            FeatureClass = j,
                            Name = string.Concat(dsName, (j as IDataset).Name)
                        };
                        pMap.AddLayer(featureLayerClass);
                    }
                    return;
                }
                case esriDatasetType.esriDTTopology:
                {
                    ITopologyLayer topologyLayerClass = new TopologyLayerClass()
                    {
                        Topology = pDataset as ITopology
                    };
                    (topologyLayerClass as ILayer).Name = string.Concat(dsName, pDataset.Name);
                    pMap.AddLayer(topologyLayerClass as ILayer);
                    return;
                }
                case esriDatasetType.esriDTTable:
                {
                    try
                    {
                        IRasterCatalogTable rasterCatalogTableClass = new RasterCatalogTable()
                        {
                            Table = (ITable) pDataset
                        };
                        rasterCatalogTableClass.Update();
                        IRasterCatalogLayer rasterCatalogLayerClass = new RasterCatalogLayerClass();
                        rasterCatalogLayerClass.Create(rasterCatalogTableClass);
                        rasterCatalogLayerClass.Name = string.Concat(dsName, pDataset.BrowseName);
                        pMap.AddLayer(rasterCatalogLayerClass);
                        return;
                    }
                    catch
                    {
                        try
                        {
                            IStandaloneTableCollection ibasicMap0 = pMap as IStandaloneTableCollection;
                            IPropertySet connectionProperties = pDataset.Workspace.ConnectionProperties;
                            bool flag = false;
                            int num = 0;
                            while (true)
                            {
                                if (num < ibasicMap0.StandaloneTableCount)
                                {
                                    ITable table = ibasicMap0.StandaloneTable[num].Table;
                                    if (
                                        !connectionProperties.IsEqual((table as IDataset).Workspace.ConnectionProperties) ||
                                        !((table as IDataset).Name == pDataset.Name))
                                    {
                                        num++;
                                    }
                                    else
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                ibasicMap0.AddStandaloneTable(new StandaloneTableClass()
                                {
                                    Table = pDataset as ITable
                                });
                            }
                        }
                        catch (Exception exception1)
                        {
                            Logger.Current.Write(exception1.Message, LogLevel.Error, null);
                        }
                        return;
                    }
                    break;
                }
                case esriDatasetType.esriDTRasterDataset:
                case esriDatasetType.esriDTRasterBand:
                {
                    IRasterLayer rasterLayerClass = new RasterLayerClass();
                    rasterLayerClass.CreateFromDataset((IRasterDataset) pDataset);
                    rasterLayerClass.Name = string.Concat(dsName, pDataset.Name);
                    pMap.AddLayer(rasterLayerClass);
                    return;
                }
                case esriDatasetType.esriDTTin:
                {
                    ITinLayer tinLayerClass = new TinLayerClass()
                    {
                        Dataset = (ITin) pDataset,
                        Name = string.Concat(dsName, pDataset.Name)
                    };
                    pMap.AddLayer(tinLayerClass);
                    return;
                }
                case esriDatasetType.esriDTCadDrawing:
                {
                    ICadLayer cadLayerClass = new CadLayerClass()
                    {
                        CadDrawingDataset = pDataset as ICadDrawingDataset,
                        Name = pDataset.Name
                    };
                    pMap.AddLayer(cadLayerClass);
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        public static void AddEditLayerToList(ICompositeLayer compLayer, IList layerList)
        {
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is ICompositeLayer)
                {
                    MapHelper.AddEditLayerToList(layer as ICompositeLayer, layerList);
                }
                else if (layer is IFeatureLayer) //&& Editor.CheckWorkspaceEdit(layer, "IsBeingEdited"))
                {
                    layerList.Add(new LayerObject(layer));
                }
            }
        }

        public static void AddEditLayerToList(IBasicMap compLayer, IList layerList)
        {
            for (int i = 0; i < compLayer.LayerCount; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is ICompositeLayer)
                {
                    MapHelper.AddEditLayerToList(layer as ICompositeLayer, layerList);
                }
                else if (layer is IFeatureLayer) // && Editor.CheckWorkspaceEdit(layer, "IsBeingEdited"))
                {
                    layerList.Add(new LayerObject(layer));
                }
            }
        }

        public static void AddFeatureLayer(IBasicMap compLayer, IFeatureClass pFClass)
        {
            IFeatureLayer featureLayerClass = new FeatureLayerClass()
            {
                FeatureClass = pFClass,
                Name = pFClass.AliasName
            };
            compLayer.AddLayer(featureLayerClass);
        }

        public static void AddFeatureLayer(IBasicMap compLayer, IFeatureClass[] pFClasses)
        {
            for (int i = 0; i < (int) pFClasses.Length; i++)
            {
                IFeatureLayer featureLayerClass = new FeatureLayerClass()
                {
                    FeatureClass = pFClasses[i],
                    Name = pFClasses[i].AliasName
                };
                compLayer.AddLayer(featureLayerClass);
            }
        }

        internal static void AddTable(IBasicMap pMap, ITable pTable)
        {
        }

        public static void CopyMap(IMap pMap, IMap targetMap, bool bCopyTable, bool bSetExtent)
        {
            int i;
            targetMap.ClearLayers();
            targetMap.SpatialReferenceLocked = false;
            targetMap.DistanceUnits = pMap.DistanceUnits;
            targetMap.MapUnits = pMap.MapUnits;
            targetMap.SpatialReference = pMap.SpatialReference;
            targetMap.Name = pMap.Name;
            for (i = pMap.LayerCount - 1; i >= 0; i--)
            {
                targetMap.AddLayer(pMap.Layer[i]);
            }
            IGraphicsContainer graphicsContainer = pMap as IGraphicsContainer;
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            int num = 0;
            while (element != null)
            {
                (targetMap as IGraphicsContainer).AddElement(element, num);
                num++;
                element = graphicsContainer.Next();
            }
            if (bCopyTable)
            {
                ITableCollection tableCollection = pMap as ITableCollection;
                for (i = 0; i < tableCollection.TableCount; i++)
                {
                    (targetMap as ITableCollection).AddTable(tableCollection.Table[i]);
                }
            }
            if (bSetExtent)
            {
                (targetMap as IActiveView).Extent = (pMap as IActiveView).Extent;
            }
        }

        public static void CopyMap(IMap pMap, IMap targetMap)
        {
            IObjectCopy objectCopyClass = new ObjectCopy();
            object obj = objectCopyClass.Copy(pMap);
            object imap1 = targetMap;
            objectCopyClass.Overwrite(obj, ref imap1);
        }

        public static void DeleteLayer(IBasicMap pMap, string layerName)
        {
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    ILayer layer = pMap.Layer[num];
                    if (layer.Name == layerName)
                    {
                        pMap.DeleteLayer(layer);
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void DeleteLayer(IBasicMap pMap, string[] string_0)
        {
            for (int i = 0; i < (int) string_0.Length; i++)
            {
                MapHelper.DeleteLayer(pMap, string_0[i]);
            }
        }

        public static IFeatureLayer FindFeatureLayerByFCName(IBasicMap pMap, string fcName, bool bool_0)
        {
            IFeatureLayer featureLayer;
            string lower = fcName.ToLower();
            UID uIDClass = new UID()
            {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layers = pMap.Layers[uIDClass, true];
            layers.Reset();
            ILayer layer = layers.Next();
            while (true)
            {
                if (layer != null)
                {
                    if (layer is IFeatureLayer)
                    {
                        string name = ((layer as IFeatureLayer).FeatureClass as IDataset).Name;
                        string[] strArrays = name.Split(new char[] {'.'});
                        if (strArrays[(int) strArrays.Length - 1].ToLower() == lower)
                        {
                            if (!bool_0)
                            {
                                featureLayer = layer as IFeatureLayer;
                                break;
                            }
                            else if (
                                (((layer as IFeatureLayer).FeatureClass as IDataset).Workspace as IWorkspaceEdit)
                                    .IsBeingEdited())
                            {
                                featureLayer = layer as IFeatureLayer;
                                break;
                            }
                        }
                    }
                    layer = layers.Next();
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        public static IFeatureLayer FindFeatureLayerByName(IBasicMap pMap, string layerName, bool bool_0)
        {
            IFeatureLayer featureLayer;
            string lower = layerName.ToLower();
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    IFeatureLayer layer = pMap.Layer[num] as IFeatureLayer;
                    if (layer != null && layer.Name.ToLower() == lower)
                    {
                        if (!bool_0)
                        {
                            featureLayer = layer;
                            break;
                        }
                        else if (((layer.FeatureClass as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
                        {
                            featureLayer = layer;
                            break;
                        }
                    }
                    num++;
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        public static ILayer FindLayer(IBasicMap pMap, string layerName)
        {
            ILayer layer;
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    ILayer layer1 = pMap.Layer[num];
                    if (layer1.Name == layerName)
                    {
                        layer = layer1;
                        break;
                    }
                    else
                    {
                        if (layer1 is IGroupLayer)
                        {
                            ILayer layer2 = MapHelper.FindLayer(layer1 as ICompositeLayer, layerName);
                            if (layer2 != null)
                            {
                                layer = layer2;
                                break;
                            }
                        }
                        num++;
                    }
                }
                else
                {
                    layer = null;
                    break;
                }
            }
            return layer;
        }

        private static ILayer FindLayer(ICompositeLayer compLayer, string layerName)
        {
            ILayer layer;
            int num = 0;
            while (true)
            {
                if (num < compLayer.Count)
                {
                    ILayer layer1 = compLayer.Layer[num];
                    if (layer1.Name == layerName)
                    {
                        layer = layer1;
                        break;
                    }
                    else
                    {
                        if (layer1 is IGroupLayer)
                        {
                            ILayer layer2 = MapHelper.FindLayer(layer1 as ICompositeLayer, layerName);
                            if (layer2 != null)
                            {
                                layer = layer2;
                                break;
                            }
                        }
                        num++;
                    }
                }
                else
                {
                    layer = null;
                    break;
                }
            }
            return layer;
        }

        public static IFeatureLayer FindLayerByFeatureClassName(IBasicMap pMap, string fcName)
        {
            IFeatureLayer featureLayer;
            string lower = fcName.ToLower();
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    IFeatureLayer layer = pMap.Layer[num] as IFeatureLayer;
                    if (layer == null || !(layer.FeatureClass.AliasName.ToLower() == lower))
                    {
                        num++;
                    }
                    else
                    {
                        featureLayer = layer;
                        break;
                    }
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        public static IFeatureLayer FindLayerByFeatureClassName(IBasicMap pMap, string fcName, bool bool_0)
        {
            IFeatureLayer featureLayer;
            string lower = fcName.ToLower();
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    IFeatureLayer layer = pMap.Layer[num] as IFeatureLayer;
                    if ((layer == null ? false : layer.FeatureClass != null) &&
                        layer.FeatureClass.AliasName.ToLower() == lower)
                    {
                        if (!bool_0)
                        {
                            featureLayer = layer;
                            break;
                        }
                        else if (((layer.FeatureClass as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
                        {
                            featureLayer = layer;
                            break;
                        }
                    }
                    num++;
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        internal static IFeatureLayer GetLayerByFeature(ICompositeLayer compLayer, IFeature pFeature)
        {
            IFeatureLayer featureLayer;
            int num = 0;
            while (true)
            {
                if (num < compLayer.Count)
                {
                    ILayer layer = compLayer.Layer[num];
                    if (layer is IGroupLayer)
                    {
                        IFeatureLayer layerByFeature = MapHelper.GetLayerByFeature(layer as ICompositeLayer, pFeature);
                        if (layerByFeature != null)
                        {
                            featureLayer = layerByFeature;
                            break;
                        }
                    }
                    else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == pFeature.Class)
                    {
                        featureLayer = layer as IFeatureLayer;
                        break;
                    }
                    num++;
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        public static IFeatureLayer GetLayerByFeature(IBasicMap pMap, IFeature pFeature)
        {
            IFeatureLayer featureLayer;
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    ILayer layer = pMap.Layer[num];
                    if (layer is IGroupLayer)
                    {
                        IFeatureLayer layerByFeature = MapHelper.GetLayerByFeature(layer as ICompositeLayer, pFeature);
                        if (layerByFeature != null)
                        {
                            featureLayer = layerByFeature;
                            break;
                        }
                    }
                    else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == pFeature.Class)
                    {
                        featureLayer = layer as IFeatureLayer;
                        break;
                    }
                    num++;
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        public static double GetMapScale(IMap imap_0)
        {
            double mapScale;
            if (imap_0.MapUnits != esriUnits.esriUnknownUnits)
            {
                mapScale = imap_0.MapScale;
            }
            else
            {
                IActiveView imap0 = imap_0 as IActiveView;
                tagRECT deviceFrame = imap0.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                double num = (double) (deviceFrame.right - deviceFrame.left);
                double width = imap0.Extent.Width*imap0.ScreenDisplay.DisplayTransformation.Resolution;
                mapScale = width/num;
            }
            return mapScale;
        }

        internal static IRepresentationClass GetRepresentationClassByFeature(ICompositeLayer compLayer,
            IFeature pFeature)
        {
            IRepresentationClass representationClass;
            IRepresentationClass representationClassByFeature = null;
            int num = 0;
            while (true)
            {
                if (num < compLayer.Count)
                {
                    ILayer layer = compLayer.Layer[num];
                    if (layer is IGroupLayer)
                    {
                        representationClassByFeature =
                            MapHelper.GetRepresentationClassByFeature(layer as ICompositeLayer, pFeature);
                        if (representationClassByFeature != null)
                        {
                            representationClass = representationClassByFeature;
                            break;
                        }
                    }
                    else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == pFeature.Class &&
                             (layer as IGeoFeatureLayer).Renderer is IRepresentationRenderer)
                    {
                        representationClass =
                            ((layer as IGeoFeatureLayer).Renderer as IRepresentationRenderer).RepresentationClass;
                        break;
                    }
                    num++;
                }
                else
                {
                    representationClass = null;
                    break;
                }
            }
            return representationClass;
        }

        public static IRepresentationClass GetRepresentationClassByFeature(IBasicMap pMap, IFeature pFeature)
        {
            IRepresentationClass representationClass;
            IRepresentationClass representationClassByFeature = null;
            int num = 0;
            while (true)
            {
                if (num < pMap.LayerCount)
                {
                    ILayer layer = pMap.Layer[num];
                    if (layer is IGroupLayer)
                    {
                        representationClassByFeature =
                            MapHelper.GetRepresentationClassByFeature(layer as ICompositeLayer, pFeature);
                        if (representationClassByFeature != null)
                        {
                            representationClass = representationClassByFeature;
                            break;
                        }
                    }
                    else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == pFeature.Class &&
                             (layer as IGeoFeatureLayer).Renderer is IRepresentationRenderer)
                    {
                        representationClass =
                            ((layer as IGeoFeatureLayer).Renderer as IRepresentationRenderer).RepresentationClass;
                        break;
                    }
                    num++;
                }
                else
                {
                    representationClass = null;
                    break;
                }
            }
            return representationClass;
        }

        public static bool LayerIsExist(IBasicMap pMap, ILayer pLayer)
        {
            bool flag;
            IEnumLayer layers = pMap.Layers[null, true];
            layers.Reset();
            ILayer layer = layers.Next();
            while (true)
            {
                if (layer == null)
                {
                    flag = false;
                    break;
                }
                else if (layer == pLayer)
                {
                    flag = true;
                    break;
                }
                else
                {
                    layer = layers.Next();
                }
            }
            return flag;
        }

        public static void MoveLayer(IBasicMap pMap, string pName, int index)
        {
            try
            {
                (pMap as IMapLayers).MoveLayer(MapHelper.FindLayer(pMap, pName), index);
            }
            catch
            {
            }
        }
    }
}