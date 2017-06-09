using System;
using System.Collections;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common
{
    public class MapHelper
    {
        public static IEnvelope GetSelectFeatureEnvelop(IFeatureLayer featureLayer)
        {
            IEnvelope envelope;
            ICursor cursor;
            IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
            if (featureSelection.SelectionSet.Count != 0)
            {
                IEnvelope extent = null;
                IEnvelope extent1 = null;
                IFeature feature = null;
                double num = 5;
                featureSelection.SelectionSet.Search(null, false, out cursor);
                for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
                {
                    feature = i as IFeature;
                    if ((feature == null ? false : feature.Shape != null))
                    {
                        try
                        {
                            if (extent != null)
                            {
                                extent1 = feature.Extent;
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    extent1.Expand(num, num, false);
                                }
                                extent.Union(extent1);
                            }
                            else
                            {
                                extent = feature.Extent;
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    extent.Expand(num, num, false);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                Marshal.ReleaseComObject(cursor);
                envelope = extent;
            }
            else
            {
                envelope = null;
            }
            return envelope;
        }

        public static void Zoom2SelectedFeature(IActiveView activeView, IFeatureLayer featureLayer)
        {
            try
            {
                double num = 5;
                IEnvelope selectFeatureEnvelop = GetSelectFeatureEnvelop(featureLayer);
                if (selectFeatureEnvelop != null)
                {
                    selectFeatureEnvelop.Expand(num, num, false);
                    activeView.Extent = selectFeatureEnvelop;
                }
                activeView.Refresh();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static void AddDataset(IBasicMap ibasicMap_0, IDataset idataset_0, string string_0)
        {
            if (string_0 == null)
            {
                string_0 = "";
            }
            switch (idataset_0.Type)
            {
                case esriDatasetType.esriDTFeatureDataset:
                {
                    IEnumDataset subsets = idataset_0.Subsets;
                    subsets.Reset();
                    for (IDataset dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                    {
                        AddDataset(ibasicMap_0, dataset2, string_0);
                    }
                    break;
                }
                case esriDatasetType.esriDTFeatureClass:
                {
                    IFeatureLayer layer;
                    IFeatureClass class2 = (IFeatureClass) idataset_0;
                    if (class2.FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        if (class2.FeatureType == esriFeatureType.esriFTDimension)
                        {
                                IFeatureLayer dimensionLayer = new DimensionLayer() as IFeatureLayer;
                            dimensionLayer.FeatureClass = class2;
                            dimensionLayer.Name = string_0 + class2.AliasName;
                            layer = dimensionLayer as IFeatureLayer;
                            ibasicMap_0.AddLayer(layer);
                        }
                        else
                        {
                            layer = new FeatureLayer{
                                FeatureClass = class2,
                                Name = string_0 + class2.AliasName
                            };
                            ibasicMap_0.AddLayer(layer);
                        }
                        break;
                    }
                    layer = new FDOGraphicsLayer() as IFeatureLayer;
                    try
                    {
                        layer.FeatureClass = class2;
                        layer.Name = string_0 + class2.AliasName;
                        ibasicMap_0.AddLayer(layer);
                    }
                    catch (Exception exception)
                    {
                        exception.ToString();
                    }
                    break;
                }
                case esriDatasetType.esriDTGeometricNetwork:
                {
                    IGeometricNetwork network = idataset_0 as IGeometricNetwork;
                    if (network != null)
                    {
                        IFeatureLayer layer7;
                        IEnumFeatureClass class3 = network.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
                        class3.Reset();
                        IFeatureClass class4 = class3.Next();
                        while (class4 != null)
                        {
                            layer7 = new FeatureLayer{
                                FeatureClass = class4,
                                Name = string_0 + (class4 as IDataset).Name
                            };
                            ibasicMap_0.AddLayer(layer7);
                            class4 = class3.Next();
                        }
                        class3 = network.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
                        class3.Reset();
                        for (class4 = class3.Next(); class4 != null; class4 = class3.Next())
                        {
                            layer7 = new FeatureLayer{
                                FeatureClass = class4,
                                Name = string_0 + (class4 as IDataset).Name
                            };
                            ibasicMap_0.AddLayer(layer7);
                        }
                        class3 = network.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
                        class3.Reset();
                        for (class4 = class3.Next(); class4 != null; class4 = class3.Next())
                        {
                            layer7 = new FeatureLayer{
                                FeatureClass = class4,
                                Name = string_0 + (class4 as IDataset).Name
                            };
                            ibasicMap_0.AddLayer(layer7);
                        }
                        class3 = network.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
                        class3.Reset();
                        for (class4 = class3.Next(); class4 != null; class4 = class3.Next())
                        {
                            layer7 = new FeatureLayer{
                                FeatureClass = class4,
                                Name = string_0 + (class4 as IDataset).Name
                            };
                            ibasicMap_0.AddLayer(layer7);
                        }
                    }
                    break;
                }
                case esriDatasetType.esriDTTopology:
                {
                    ITopologyLayer layer = new TopologyLayer() as ITopologyLayer;
                    layer.Topology = idataset_0 as ITopology;
                    ITopologyLayer layer3 = layer as ITopologyLayer;
                    (layer3 as ILayer).Name = string_0 + idataset_0.Name;
                    ibasicMap_0.AddLayer(layer3 as ILayer);
                    break;
                }
                case esriDatasetType.esriDTTable:
                    try
                    {
                        IRasterCatalogTable pCatalog = new RasterCatalogTable{
                            Table = (ITable) idataset_0
                        };
                        pCatalog.Update();
                        IRasterCatalogLayer pLayer = new RasterCatalogLayer() as IRasterCatalogLayer;
                        pLayer.Create(pCatalog);
                        pLayer.Name = string_0 + idataset_0.BrowseName;
                        ibasicMap_0.AddLayer(pLayer);
                    }
                    catch
                    {
                        try
                        {
                            IStandaloneTableCollection tables = ibasicMap_0 as IStandaloneTableCollection;
                            IPropertySet connectionProperties = idataset_0.Workspace.ConnectionProperties;
                            bool flag = false;
                            for (int i = 0; i < tables.StandaloneTableCount; i++)
                            {
                                ITable table = tables.get_StandaloneTable(i).Table;
                                if (connectionProperties.IsEqual((table as IDataset).Workspace.ConnectionProperties) && ((table as IDataset).Name == idataset_0.Name))
                                {
                                    goto Label_03E1;
                                }
                            }
                            goto Label_03E4;
                        Label_03E1:
                            flag = true;
                        Label_03E4:
                            if (!flag)
                            {
                                IStandaloneTable table3 = new StandaloneTable{
                                    Table = idataset_0 as ITable
                                };
                                tables.AddStandaloneTable(table3);
                            }
                        }
                        catch (Exception exception2)
                        {
                            CErrorLog.writeErrorLog(null, exception2, "");
                        }
                    }
                    break;

                case esriDatasetType.esriDTRasterDataset:
                case esriDatasetType.esriDTRasterBand:
                {
                    IRasterLayer layer5 = new RasterLayer();
                    layer5.CreateFromDataset((IRasterDataset) idataset_0);
                    layer5.Name = string_0 + idataset_0.Name;
                    ibasicMap_0.AddLayer(layer5);
                    break;
                }
                case esriDatasetType.esriDTTin:
                {
                    ITinLayer layer4 = new TinLayer{
                        Dataset = (ITin) idataset_0,
                        Name = string_0 + idataset_0.Name
                    };
                    ibasicMap_0.AddLayer(layer4);
                    break;
                }
                case esriDatasetType.esriDTCadDrawing:
                {
                    ICadLayer layer2 = new CadLayer() as ICadLayer;
                    layer2.CadDrawingDataset = idataset_0 as ICadDrawingDataset;
                    layer2.Name = idataset_0.Name;
                    ibasicMap_0.AddLayer(layer2);
                    break;
                }
            }
        }

        public static void AddEditLayerToList(IBasicMap ibasicMap_0, IList ilist_0)
        {
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = ibasicMap_0.get_Layer(i);
                if (layer is ICompositeLayer)
                {
                    AddEditLayerToList(layer as ICompositeLayer, ilist_0);
                }
                else if ((layer is IFeatureLayer) && Editor.Editor.CheckWorkspaceEdit(layer, "IsBeingEdited"))
                {
                    ilist_0.Add(new LayerObject(layer));
                }
            }
        }

        public static void AddEditLayerToList(ICompositeLayer icompositeLayer_0, IList ilist_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is ICompositeLayer)
                {
                    AddEditLayerToList(layer as ICompositeLayer, ilist_0);
                }
                else if ((layer is IFeatureLayer) && Editor.Editor.CheckWorkspaceEdit(layer, "IsBeingEdited"))
                {
                    ilist_0.Add(new LayerObject(layer));
                }
            }
        }

        public static void AddFeatureLayer(IBasicMap ibasicMap_0, IFeatureClass ifeatureClass_0)
        {
            IFeatureLayer pLayer = new FeatureLayer{
                FeatureClass = ifeatureClass_0,
                Name = ifeatureClass_0.AliasName
            };
            ibasicMap_0.AddLayer(pLayer);
        }

        public static void AddFeatureLayer(IBasicMap ibasicMap_0, IFeatureClass[] ifeatureClass_0)
        {
            for (int i = 0; i < ifeatureClass_0.Length; i++)
            {
                IFeatureLayer pLayer = new FeatureLayer{
                    FeatureClass = ifeatureClass_0[i],
                    Name = ifeatureClass_0[i].AliasName
                };
                ibasicMap_0.AddLayer(pLayer);
            }
        }

        internal static void AddTable(IBasicMap ibasicMap_0, ITable itable_0)
        {
        }

        public static void CopyMap(IMap imap_0, IMap imap_1)
        {
            IObjectCopy copy = new ObjectCopy();
            object pInObject = copy.Copy(imap_0);
            object pOverwriteObject = imap_1;
            copy.Overwrite(pInObject, ref pOverwriteObject);
        }

        public static void CopyMap(IMap imap_0, IMap imap_1, bool bool_0, bool bool_1)
        {
            int num;
            imap_1.ClearLayers();
            imap_1.SpatialReferenceLocked = false;
            imap_1.DistanceUnits = imap_0.DistanceUnits;
            imap_1.MapUnits = imap_0.MapUnits;
            imap_1.SpatialReference = imap_0.SpatialReference;
            imap_1.Name = imap_0.Name;
            for (num = imap_0.LayerCount - 1; num >= 0; num--)
            {
                ILayer layer = imap_0.get_Layer(num);
                imap_1.AddLayer(layer);
            }
            IGraphicsContainer container = imap_0 as IGraphicsContainer;
            container.Reset();
            IElement element = container.Next();
            int zorder = 0;
            while (element != null)
            {
                (imap_1 as IGraphicsContainer).AddElement(element, zorder);
                zorder++;
                element = container.Next();
            }
            if (bool_0)
            {
                ITableCollection tables = imap_0 as ITableCollection;
                for (num = 0; num < tables.TableCount; num++)
                {
                    (imap_1 as ITableCollection).AddTable(tables.get_Table(num));
                }
            }
            if (bool_1)
            {
                (imap_1 as IActiveView).Extent = (imap_0 as IActiveView).Extent;
            }
        }

        public static void DeleteLayer(IBasicMap ibasicMap_0, string string_0)
        {
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                ILayer pLayer = ibasicMap_0.get_Layer(i);
                if (pLayer.Name == string_0)
                {
                    ibasicMap_0.DeleteLayer(pLayer);
                    break;
                }
            }
        }

        public static void DeleteLayer(IBasicMap ibasicMap_0, string[] string_0)
        {
            for (int i = 0; i < string_0.Length; i++)
            {
                DeleteLayer(ibasicMap_0, string_0[i]);
            }
        }

        public static IFeatureLayer FindFeatureLayerByFCName(IBasicMap ibasicMap_0, string string_0, bool bool_0)
        {
            string str = string_0.ToLower();
            UID uid = new UID{
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layer = ibasicMap_0.get_Layers(uid, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
            {
                if (layer2 is IFeatureLayer)
                {
                    string[] strArray = ((layer2 as IFeatureLayer).FeatureClass as IDataset).Name.Split(new char[] { '.' });
                    if (strArray[strArray.Length - 1].ToLower() == str)
                    {
                        if (!bool_0)
                        {
                            return (layer2 as IFeatureLayer);
                        }
                        IWorkspaceEdit workspace = ((layer2 as IFeatureLayer).FeatureClass as IDataset).Workspace as IWorkspaceEdit;
                        if (workspace.IsBeingEdited())
                        {
                            return (layer2 as IFeatureLayer);
                        }
                    }
                }
            }
            return null;
        }

        public static IFeatureLayer FindFeatureLayerByName(IBasicMap ibasicMap_0, string string_0, bool bool_0)
        {
            string str = string_0.ToLower();
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                IFeatureLayer layer = ibasicMap_0.get_Layer(i) as IFeatureLayer;
                if ((layer != null) && (layer.Name.ToLower() == str))
                {
                    if (!bool_0)
                    {
                        return layer;
                    }
                    IWorkspaceEdit workspace = (layer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
                    if (workspace.IsBeingEdited())
                    {
                        return layer;
                    }
                }
            }
            return null;
        }

        public static ILayer FindLayer(IBasicMap ibasicMap_0, string string_0)
        {
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = ibasicMap_0.get_Layer(i);
                if (layer.Name == string_0)
                {
                    return layer;
                }
                if (layer is IGroupLayer)
                {
                    ILayer layer3 = FindLayer(layer as ICompositeLayer, string_0);
                    if (layer3 != null)
                    {
                        return layer3;
                    }
                }
            }
            return null;
        }

        private static ILayer FindLayer(ICompositeLayer icompositeLayer_0, string string_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer.Name == string_0)
                {
                    return layer;
                }
                if (layer is IGroupLayer)
                {
                    ILayer layer3 = FindLayer(layer as ICompositeLayer, string_0);
                    if (layer3 != null)
                    {
                        return layer3;
                    }
                }
            }
            return null;
        }

        public static IFeatureLayer FindLayerByFeatureClassName(IBasicMap ibasicMap_0, string string_0)
        {
            string str = string_0.ToLower();
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                IFeatureLayer layer = ibasicMap_0.get_Layer(i) as IFeatureLayer;
                if ((layer != null) && (layer.FeatureClass.AliasName.ToLower() == str))
                {
                    return layer;
                }
            }
            return null;
        }

        public static IFeatureLayer FindLayerByFeatureClassName(IBasicMap ibasicMap_0, string string_0, bool bool_0)
        {
            string str = string_0.ToLower();
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                IFeatureLayer layer = ibasicMap_0.get_Layer(i) as IFeatureLayer;
                if (((layer != null) && (layer.FeatureClass != null)) && (layer.FeatureClass.AliasName.ToLower() == str))
                {
                    if (!bool_0)
                    {
                        return layer;
                    }
                    IWorkspaceEdit workspace = (layer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
                    if (workspace.IsBeingEdited())
                    {
                        return layer;
                    }
                }
            }
            return null;
        }

        public static IFeatureLayer GetLayerByFeature(IBasicMap ibasicMap_0, IFeature ifeature_0)
        {
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = ibasicMap_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    IFeatureLayer layerByFeature = GetLayerByFeature(layer as ICompositeLayer, ifeature_0);
                    if (layerByFeature != null)
                    {
                        return layerByFeature;
                    }
                }
                else if ((layer is IFeatureLayer) && ((layer as IFeatureLayer).FeatureClass == ifeature_0.Class))
                {
                    return (layer as IFeatureLayer);
                }
            }
            return null;
        }

        internal static IFeatureLayer GetLayerByFeature(ICompositeLayer icompositeLayer_0, IFeature ifeature_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    IFeatureLayer layerByFeature = GetLayerByFeature(layer as ICompositeLayer, ifeature_0);
                    if (layerByFeature != null)
                    {
                        return layerByFeature;
                    }
                }
                else if ((layer is IFeatureLayer) && ((layer as IFeatureLayer).FeatureClass == ifeature_0.Class))
                {
                    return (layer as IFeatureLayer);
                }
            }
            return null;
        }

        public static double GetMapScale(IMap imap_0)
        {
            if (imap_0.MapUnits == esriUnits.esriUnknownUnits)
            {
                IActiveView view = imap_0 as IActiveView;
                tagRECT deviceFrame = view.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                double num = deviceFrame.right - deviceFrame.left;
                double num2 = view.Extent.Width * view.ScreenDisplay.DisplayTransformation.Resolution;
                return (num2 / num);
            }
            return imap_0.MapScale;
        }

        public static IRepresentationClass GetRepresentationClassByFeature(IBasicMap ibasicMap_0, IFeature ifeature_0)
        {
            IRepresentationClass representationClassByFeature = null;
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = ibasicMap_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    representationClassByFeature = GetRepresentationClassByFeature(layer as ICompositeLayer, ifeature_0);
                    if (representationClassByFeature != null)
                    {
                        return representationClassByFeature;
                    }
                }
                else if (((layer is IFeatureLayer) && ((layer as IFeatureLayer).FeatureClass == ifeature_0.Class)) && ((layer as IGeoFeatureLayer).Renderer is IRepresentationRenderer))
                {
                    return ((layer as IGeoFeatureLayer).Renderer as IRepresentationRenderer).RepresentationClass;
                }
            }
            return null;
        }

        internal static IRepresentationClass GetRepresentationClassByFeature(ICompositeLayer icompositeLayer_0, IFeature ifeature_0)
        {
            IRepresentationClass representationClassByFeature = null;
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    representationClassByFeature = GetRepresentationClassByFeature(layer as ICompositeLayer, ifeature_0);
                    if (representationClassByFeature != null)
                    {
                        return representationClassByFeature;
                    }
                }
                else if (((layer is IFeatureLayer) && ((layer as IFeatureLayer).FeatureClass == ifeature_0.Class)) && ((layer as IGeoFeatureLayer).Renderer is IRepresentationRenderer))
                {
                    return ((layer as IGeoFeatureLayer).Renderer as IRepresentationRenderer).RepresentationClass;
                }
            }
            return null;
        }

        public static bool LayerIsExist(IBasicMap ibasicMap_0, ILayer ilayer_0)
        {
            IEnumLayer layer = ibasicMap_0.get_Layers(null, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
            {
                if (layer2 == ilayer_0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void MoveLayer(IBasicMap ibasicMap_0, string string_0, int int_0)
        {
            try
            {
                ILayer layer = FindLayer(ibasicMap_0, string_0);
                (ibasicMap_0 as IMapLayers).MoveLayer(layer, int_0);
            }
            catch
            {
            }
        }
    }
}

