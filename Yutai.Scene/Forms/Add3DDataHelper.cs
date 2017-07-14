using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Scene.Forms
{
    internal sealed class Add3DDataHelper : System.Windows.Forms.Control
    {
        public delegate void MessageHandler(IList ilist_0);

        private IBasicMap ibasicMap_0;

        public Add3DDataHelper(IBasicMap ibasicMap_1)
        {
            this.CreateHandle();
            base.CreateControl();
            this.ibasicMap_0 = ibasicMap_1;
        }

        public void InvokeMethod(IList ilist_0)
        {
            if (!base.IsDisposed && base.IsHandleCreated)
            {
                base.Invoke(new Add3DDataHelper.MessageHandler(this.LoadData), new object[]
                {
                    ilist_0
                });
            }
        }

        private IBasicMap method_0()
        {
            return this.ibasicMap_0;
        }

        public void LoadData(IList ilist_0)
        {
            try
            {
                List<ILayer> list = new List<ILayer>();
                for (int i = 0; i < ilist_0.Count; i++)
                {
                    if (ilist_0[i] is IGxLayer)
                    {
                        list.Add((ilist_0[i] as IGxLayer).Layer);
                    }
                    else
                    {
                        IGxDataset gxDataset = ilist_0[i] as IGxDataset;
                        if (gxDataset != null)
                        {
                            IDataset dataset = gxDataset.Dataset;
                            if (dataset != null)
                            {
                                if (dataset.Type == esriDatasetType.esriDTFeatureClass)
                                {
                                    IFeatureClass featureClass = (IFeatureClass)dataset;
                                    IFeatureLayer featureLayer;
                                    if ((gxDataset as IGxObject).Category.IndexOf("CAD") != -1)
                                    {
                                        if (featureClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                                        {
                                            featureLayer = new CadAnnotationLayer() as IFeatureLayer;
                                        }
                                        else
                                        {
                                            featureLayer = new CadFeatureLayer() as IFeatureLayer;
                                        }
                                        featureLayer.FeatureClass = featureClass;
                                        featureLayer.Name = featureClass.AliasName;
                                    }
                                    else if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                                    {
                                        featureLayer = new FDOGraphicsLayer() as IFeatureLayer;
                                        featureLayer.FeatureClass = featureClass;
                                        featureLayer.Name = featureClass.AliasName;
                                    }
                                    else if (featureClass.FeatureType == esriFeatureType.esriFTDimension)
                                    {
                                        featureLayer = new DimensionLayer() as IFeatureLayer;
                                        featureLayer.FeatureClass = featureClass;
                                        featureLayer.Name = featureClass.AliasName;
                                    }
                                    else
                                    {
                                        featureLayer = new FeatureLayer();
                                        featureLayer.FeatureClass = featureClass;
                                        featureLayer.Name = featureClass.AliasName;
                                    }
                                    list.Add(featureLayer);
                                }
                                else if (dataset.Type == esriDatasetType.esriDTCadDrawing)
                                {
                                    if ((gxDataset as IGxObject).Category == "CAD绘图")
                                    {
                                        ICadLayer item = new CadLayer() as ICadLayer;
                                        item.CadDrawingDataset = gxDataset.Dataset as ICadDrawingDataset;
                                        item.Name = gxDataset.Dataset.Name;
                                        list.Add(item);
                                    }
                                    else
                                    {
                                        IEnumGxObject children = (gxDataset as IGxObjectContainer).Children;
                                        children.Reset();
                                        for (IGxDataset gxDataset2 = children.Next() as IGxDataset; gxDataset2 != null; gxDataset2 = (children.Next() as IGxDataset))
                                        {
                                            IFeatureClass featureClass = gxDataset2.Dataset as IFeatureClass;
                                            if (featureClass != null)
                                            {
                                                IFeatureLayer featureLayer;
                                                if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                                                {
                                                    featureLayer = new FDOGraphicsLayer() as IFeatureLayer;
                                                    featureLayer.FeatureClass = featureClass;
                                                    featureLayer.Name = featureClass.AliasName;
                                                }
                                                else
                                                {
                                                    featureLayer = new CadFeatureLayer() as IFeatureLayer;
                                                    featureLayer.FeatureClass = featureClass;
                                                    featureLayer.Name = featureClass.AliasName;
                                                }
                                                list.Add(featureLayer);
                                            }
                                        }
                                    }
                                }
                                else if (dataset.Type == esriDatasetType.esriDTFeatureDataset)
                                {
                                    IEnumDataset subsets = dataset.Subsets;
                                    subsets.Reset();
                                    for (IDataset dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                                    {
                                        if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                                        {
                                            IFeatureClass featureClass = dataset2 as IFeatureClass;
                                            if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                                            {
                                                IFeatureLayer item = new FDOGraphicsLayer() as IFeatureLayer;
                                                item.FeatureClass = featureClass;
                                                item.Name = featureClass.AliasName;
                                                list.Add(item);
                                            }
                                            else
                                            {
                                                list.Add(new FeatureLayer
                                                {
                                                    FeatureClass = featureClass,
                                                    Name = featureClass.AliasName
                                                });
                                            }
                                        }
                                        else if (dataset2.Type == esriDatasetType.esriDTTopology)
                                        {
                                            ITopologyLayer topologyLayer = new TopologyLayer() as ITopologyLayer;
                                            topologyLayer.Topology = (dataset2 as ITopology);
                                            (topologyLayer as ILayer).Name = dataset2.Name;
                                            list.Add(topologyLayer as ILayer);
                                        }
                                    }
                                }
                                else if (dataset.Type == esriDatasetType.esriDTTopology)
                                {
                                    if (System.Windows.Forms.MessageBox.Show("是否将参加拓扑-" + dataset.Name + "-的所有要素添加到地图中", "添加拓扑层", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        IEnumFeatureClass classes = (dataset as IFeatureClassContainer).Classes;
                                        classes.Reset();
                                        for (IFeatureClass featureClass2 = classes.Next(); featureClass2 != null; featureClass2 = classes.Next())
                                        {
                                            list.Add(new FeatureLayer
                                            {
                                                FeatureClass = featureClass2,
                                                Name = featureClass2.AliasName
                                            });
                                        }
                                    }
                                    ITopologyLayer topologyLayer = new TopologyLayer() as ITopologyLayer;
                                    topologyLayer.Topology = (dataset as ITopology);
                                    (topologyLayer as ILayer).Name = dataset.Name;
                                    list.Add(topologyLayer as ILayer);
                                }
                                else if (dataset.Type == esriDatasetType.esriDTTin)
                                {
                                    list.Add(new TinLayer
                                    {
                                        Dataset = (ITin)dataset,
                                        Name = dataset.Name
                                    });
                                }
                                else if (dataset.Type == esriDatasetType.esriDTRasterDataset || dataset.Type == esriDatasetType.esriDTRasterBand)
                                {
                                    bool flag = true;
                                    if (!((IRasterPyramid)dataset).Present)
                                    {
                                        if (ApplicationRef.PyramidPromptType == PyramidPromptType.AlwaysBuildNoPrompt)
                                        {
                                            ((IRasterPyramid)dataset).Create();
                                        }
                                        else if (ApplicationRef.PyramidPromptType == PyramidPromptType.AlwaysPrompt)
                                        {
                                            System.Windows.Forms.DialogResult dialogResult = BuildPyramidSet.Show();
                                            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                                            {
                                                ((IRasterPyramid)dataset).Create();
                                            }
                                            else if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                                            {
                                                flag = false;
                                            }
                                        }
                                    }
                                    if (flag)
                                    {
                                        IRasterLayer rasterLayer = new RasterLayer
                                        {
                                            Cached = true
                                        };
                                        rasterLayer.CreateFromDataset((IRasterDataset)dataset);
                                        rasterLayer.Name = dataset.Name;
                                        list.Add(rasterLayer);
                                    }
                                }
                                else
                                {
                                    if (dataset.Type == esriDatasetType.esriDTTable)
                                    {
                                        try
                                        {
                                            IRasterCatalogTable rasterCatalogTable = new RasterCatalogTable();
                                            rasterCatalogTable.Table = (ITable)dataset;
                                            rasterCatalogTable.Update();
                                            IRasterCatalogLayer rasterCatalogLayer = new RasterCatalogLayer() as IRasterCatalogLayer;
                                            rasterCatalogLayer.Create(rasterCatalogTable);
                                            rasterCatalogLayer.Name = dataset.BrowseName;
                                            list.Add(rasterCatalogLayer);
                                            goto IL_8B0;
                                        }
                                        catch (Exception ex)
                                        {
                                            try
                                            {
                                                IStandaloneTableCollection standaloneTableCollection = this.method_0() as IStandaloneTableCollection;
                                                IPropertySet connectionProperties = dataset.Workspace.ConnectionProperties;
                                                bool flag2 = false;
                                                for (int j = 0; j < standaloneTableCollection.StandaloneTableCount; j++)
                                                {
                                                    ITable table = standaloneTableCollection.get_StandaloneTable(j).Table;
                                                    if (
                                                        connectionProperties.IsEqual(
                                                            (table as IDataset).Workspace.ConnectionProperties) &&
                                                        (table as IDataset).Name == dataset.Name)
                                                    {
                                                        flag2 = true;
                                                    }
                                                    else
                                                    {
                                                        standaloneTableCollection.AddStandaloneTable(new StandaloneTable
                                                        {
                                                            Table = dataset as ITable
                                                        });
                                                    }
                                                }
                                                
                                            }
                                            catch (Exception exception_)
                                            {
                                                CErrorLog.writeErrorLog(this, exception_, "");
                                            }
                                           
                                            goto IL_8B0;
                                        }
                                    }
                                    if (dataset.Type == esriDatasetType.esriDTGeometricNetwork)
                                    {
                                        IGeometricNetwork geometricNetwork = dataset as IGeometricNetwork;
                                        if (geometricNetwork != null)
                                        {
                                            IEnumFeatureClass enumFeatureClass = geometricNetwork.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
                                            enumFeatureClass.Reset();
                                            for (IFeatureClass featureClass3 = enumFeatureClass.Next(); featureClass3 != null; featureClass3 = enumFeatureClass.Next())
                                            {
                                                list.Add(new FeatureLayer
                                                {
                                                    FeatureClass = featureClass3,
                                                    Name = (featureClass3 as IDataset).Name
                                                });
                                            }
                                            enumFeatureClass = geometricNetwork.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
                                            enumFeatureClass.Reset();
                                            for (IFeatureClass featureClass3 = enumFeatureClass.Next(); featureClass3 != null; featureClass3 = enumFeatureClass.Next())
                                            {
                                                list.Add(new FeatureLayer
                                                {
                                                    FeatureClass = featureClass3,
                                                    Name = (featureClass3 as IDataset).Name
                                                });
                                            }
                                            enumFeatureClass = geometricNetwork.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
                                            enumFeatureClass.Reset();
                                            for (IFeatureClass featureClass3 = enumFeatureClass.Next(); featureClass3 != null; featureClass3 = enumFeatureClass.Next())
                                            {
                                                list.Add(new FeatureLayer
                                                {
                                                    FeatureClass = featureClass3,
                                                    Name = (featureClass3 as IDataset).Name
                                                });
                                            }
                                            enumFeatureClass = geometricNetwork.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
                                            enumFeatureClass.Reset();
                                            for (IFeatureClass featureClass3 = enumFeatureClass.Next(); featureClass3 != null; featureClass3 = enumFeatureClass.Next())
                                            {
                                                list.Add(new FeatureLayer
                                                {
                                                    FeatureClass = featureClass3,
                                                    Name = (featureClass3 as IDataset).Name
                                                });
                                            }
                                        }
                                    }
                                    else if (dataset.Type == esriDatasetType.esriDTNetworkDataset)
                                    {
                                        INetworkDataset networkDataset = dataset as INetworkDataset;
                                        INetworkLayer item = new NetworkLayer() as INetworkLayer;
                                        item.NetworkDataset = networkDataset;
                                  
                                        list.Add( item as ILayer);
                                        if (System.Windows.Forms.MessageBox.Show("是否将参加网络要素集-" + dataset.Name + "-的所有要素添加到地图中", "添加拓扑层", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            IEnumFeatureClass classes = (dataset as IFeatureClassContainer).Classes;
                                            classes.Reset();
                                            for (IFeatureClass featureClass2 = classes.Next(); featureClass2 != null; featureClass2 = classes.Next())
                                            {
                                                list.Add(new FeatureLayer
                                                {
                                                    FeatureClass = featureClass2,
                                                    Name = featureClass2.AliasName
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    IL_8B0:;
                }
                int num = list.Count;
                ILayer layer = null;
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is IRasterCatalogLayer)
                    {
                        this.method_0().AddLayer(layer);
                        (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                        list.RemoveAt(i);
                        num--;
                    }
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is IRasterLayer)
                    {
                        this.method_0().AddLayer(layer);
                        (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                        list.RemoveAt(i);
                        num--;
                    }
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is ITinLayer)
                    {
                        this.method_0().AddLayer(layer);
                        (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                        list.RemoveAt(i);
                        num--;
                    }
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is ICadLayer)
                    {
                        this.method_0().AddLayer(layer);
                        (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                        list.RemoveAt(i);
                        num--;
                    }
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;
                        IFeatureClass featureClass = featureLayer.FeatureClass;
                        if (featureClass.ShapeType == esriGeometryType.esriGeometryPolygon || featureClass.ShapeType == esriGeometryType.esriGeometryEnvelope)
                        {
                            this.method_0().AddLayer(layer);
                            (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                            list.RemoveAt(i);
                            num--;
                        }
                    }
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;
                        IFeatureClass featureClass = featureLayer.FeatureClass;
                        if (featureClass.ShapeType == esriGeometryType.esriGeometryLine || featureClass.ShapeType == esriGeometryType.esriGeometryBezier3Curve || featureClass.ShapeType == esriGeometryType.esriGeometryCircularArc || featureClass.ShapeType == esriGeometryType.esriGeometryEllipticArc || featureClass.ShapeType == esriGeometryType.esriGeometryPath || featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            try
                            {
                                this.method_0().AddLayer(layer);
                                goto IL_B56;
                            }
                            catch
                            {
                                goto IL_B56;
                            }
                            goto IL_B4D;
                            IL_B56:
                            (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                            list.RemoveAt(i);
                            num--;
                        }
                    }
                    IL_B4D:;
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;
                        IFeatureClass featureClass = featureLayer.FeatureClass;
                        if (featureClass.ShapeType == esriGeometryType.esriGeometryMultipoint || featureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                        {
                            this.method_0().AddLayer(layer);
                            (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                            list.RemoveAt(i);
                            num--;
                        }
                    }
                }
                for (int i = num - 1; i >= 0; i--)
                {
                    layer = list[i];
                    this.method_0().AddLayer(layer);
                    (this.method_0() as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
                    list.RemoveAt(i);
                    num--;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void method_1()
        {
        }
    }
}
