using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;


namespace Yutai.ArcGIS.Catalog.Utility
{
    
    public class AddDataHelper
    {
        private IAppContext iapplication_0 ;

        private object object_0 = null;

        private IActiveView iactiveView_0;

        private List<string> list_0 = new List<string>();

        private IEnvelope ienvelope_0 = null;

        private List<ILayer> list_1 = null;

        public bool HasTable
        {
            get;
            protected set;
        }

        public IMapControl2 InMapCtrl
        {
            get;
            set;
        }

        public AddDataHelper(object object_1)
        {
            this.object_0 = object_1;
            this.HasTable = false;
        }

        public void LoadData(IList ilist_0)
        {
            IEnumDataset subsets;
            IDataset j;
            string str;
            IFeatureClass dataset;
            IFeatureLayer cadFeatureLayerClass;
            ITopologyLayer topologyLayerClass;
            IEnumFeatureClass classes;
            IFeatureClass m;
            IFeatureClass n;
            IFeatureLayer featureLayerClass;
            this.list_1 = new List<ILayer>();
            List<object> objs = new List<object>();
            foreach (object ilist0 in ilist_0)
            {
                if (ilist0 is IGxDataset)
                {
                    if ((ilist0 as IGxDataset).Type != esriDatasetType.esriDTFeatureDataset)
                    {
                        objs.Add(ilist0);
                    }
                    else
                    {
                        IEnumGxObject children = (ilist0 as IGxObjectContainer).Children;
                        children.Reset();
                        for (IGxObject i = children.Next(); i != null; i = children.Next())
                        {
                            objs.Add(i);
                        }
                    }
                }
                else if (!(ilist0 is IDataset))
                {
                    objs.Add(ilist0);
                }
                else
                {
                    if ((ilist0 as IDataset).Type != esriDatasetType.esriDTFeatureDataset)
                    {
                        continue;
                    }
                    subsets = (ilist0 as IDataset).Subsets;
                    subsets.Reset();
                    for (j = subsets.Next(); j != null; j = subsets.Next())
                    {
                        objs.Add(j);
                    }
                }
            }
            ProcessAssist processAssist = new ProcessAssist();
            processAssist.InitProgress();
            processAssist.SetMaxValue(objs.Count);
            processAssist.SetMessage("正在加载数据,请稍候...");
            processAssist.Start();
            try
            {
                IDataset item = null;
                IGxDataset gxDataset = null;
                for (int k = 0; k < objs.Count; k++)
                {
                    string name = "";
                    if (objs[k] is IGxObject)
                    {
                        name = (objs[k] as IGxObject).Name;
                    }
                    else if (objs[k] is IDataset)
                    {
                        name = (objs[k] as IDataset).Name;
                    }
                    str = string.Format("加载图层[{0}]，第{1}/{2}个", name, k + 1, objs.Count);
                    processAssist.Increment(1);
                    processAssist.SetMessage(str);
                    if (!(objs[k] is IGxLayer))
                    {
                        if (objs[k] is IGxDataset)
                        {
                            gxDataset = objs[k] as IGxDataset;
                            item = gxDataset.Dataset;
                        }
                        else if (objs[k] is IDataset)
                        {
                            item = objs[k] as IDataset;
                        }
                        if (item != null)
                        {
                            if (item.Type == esriDatasetType.esriDTFeatureClass)
                            {
                                dataset = (IFeatureClass)item;
                                if ((gxDataset as IGxObject).Category.IndexOf("CAD") != -1)
                                {
                                    if (dataset.FeatureType != esriFeatureType.esriFTCoverageAnnotation)
                                    {
                                        cadFeatureLayerClass = new CadFeatureLayerClass();
                                    }
                                    else
                                    {
                                        cadFeatureLayerClass = new CadAnnotationLayerClass();
                                    }
                                    cadFeatureLayerClass.FeatureClass = dataset;
                                    cadFeatureLayerClass.Name = dataset.AliasName;
                                }
                                else if (dataset.FeatureType == esriFeatureType.esriFTAnnotation)
                                {
                                    cadFeatureLayerClass = new FDOGraphicsLayerClass()
                                    {
                                        FeatureClass = dataset,
                                        Name = dataset.AliasName
                                    };
                                }
                                else if (dataset.FeatureType != esriFeatureType.esriFTDimension)
                                {
                                    cadFeatureLayerClass = new FeatureLayerClass()
                                    {
                                        FeatureClass = dataset,
                                        Name = dataset.AliasName
                                    };
                                }
                                else
                                {
                                    cadFeatureLayerClass = new DimensionLayerClass()
                                    {
                                        FeatureClass = dataset,
                                        Name = dataset.AliasName
                                    };
                                }
                                this.method_0(cadFeatureLayerClass);
                            }
                            else if (item.Type == esriDatasetType.esriDTCadDrawing)
                            {
                                if ((gxDataset as IGxObject).Category != "CAD绘图")
                                {
                                    IEnumGxObject enumGxObject = (gxDataset as IGxObjectContainer).Children;
                                    enumGxObject.Reset();
                                    for (IGxDataset l = enumGxObject.Next() as IGxDataset; l != null; l = enumGxObject.Next() as IGxDataset)
                                    {
                                        dataset = l.Dataset as IFeatureClass;
                                        if (dataset != null)
                                        {
                                            if (dataset.FeatureType != esriFeatureType.esriFTAnnotation)
                                            {
                                                cadFeatureLayerClass = new CadFeatureLayerClass()
                                                {
                                                    FeatureClass = dataset,
                                                    Name = dataset.AliasName
                                                };
                                            }
                                            else
                                            {
                                                cadFeatureLayerClass = new FDOGraphicsLayerClass()
                                                {
                                                    FeatureClass = dataset,
                                                    Name = dataset.AliasName
                                                };
                                            }
                                            this.method_0(cadFeatureLayerClass);
                                        }
                                    }
                                }
                                else
                                {
                                    ICadLayer cadLayerClass = new CadLayerClass()
                                    {
                                        CadDrawingDataset = gxDataset.Dataset as ICadDrawingDataset,
                                        Name = gxDataset.Dataset.Name
                                    };
                                    this.method_0(cadLayerClass);
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTFeatureDataset)
                            {
                                subsets = item.Subsets;
                                subsets.Reset();
                                for (j = subsets.Next(); j != null; j = subsets.Next())
                                {
                                    if (j.Type == esriDatasetType.esriDTFeatureClass)
                                    {
                                        dataset = j as IFeatureClass;
                                        if (dataset.FeatureType != esriFeatureType.esriFTAnnotation)
                                        {
                                            cadFeatureLayerClass = new FeatureLayerClass()
                                            {
                                                FeatureClass = dataset,
                                                Name = dataset.AliasName
                                            };
                                            this.method_0(cadFeatureLayerClass);
                                        }
                                        else
                                        {
                                            cadFeatureLayerClass = new FDOGraphicsLayerClass()
                                            {
                                                FeatureClass = dataset,
                                                Name = dataset.AliasName
                                            };
                                            this.method_0(cadFeatureLayerClass);
                                        }
                                    }
                                    else if (j.Type == esriDatasetType.esriDTTopology)
                                    {
                                        topologyLayerClass = new TopologyLayerClass()
                                        {
                                            Topology = j as ITopology
                                        };
                                        (topologyLayerClass as ILayer).Name = j.Name;
                                        this.method_0(topologyLayerClass as ILayer);
                                    }
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTTopology)
                            {
                                if (MessageBox.Show(string.Concat("是否将参加拓扑-", item.Name, "-的所有要素添加到地图中"), "添加拓扑层", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                {
                                    classes = (item as IFeatureClassContainer).Classes;
                                    classes.Reset();
                                    for (m = classes.Next(); m != null; m = classes.Next())
                                    {
                                        cadFeatureLayerClass = new FeatureLayerClass()
                                        {
                                            FeatureClass = m,
                                            Name = m.AliasName
                                        };
                                        this.method_0(cadFeatureLayerClass);
                                    }
                                }
                                topologyLayerClass = new TopologyLayerClass()
                                {
                                    Topology = item as ITopology
                                };
                                (topologyLayerClass as ILayer).Name = item.Name;
                                this.method_0(topologyLayerClass as ILayer);
                            }
                            else if (item.Type == esriDatasetType.esriDTTin)
                            {
                                ITinLayer tinLayerClass = new TinLayerClass()
                                {
                                    Dataset = (ITin)item,
                                    Name = item.Name
                                };
                                this.method_0(tinLayerClass);
                            }
                            else if (item.Type == esriDatasetType.esriDTRasterCatalog)
                            {
                                IGdbRasterCatalogLayer gdbRasterCatalogLayerClass = null;
                                gdbRasterCatalogLayerClass = new GdbRasterCatalogLayerClass();
                                if (gdbRasterCatalogLayerClass.Setup((ITable)(item as IRasterCatalog)))
                                {
                                    this.method_0(gdbRasterCatalogLayerClass as ILayer);
                                }
                            }
                            else if (!(item.Type == esriDatasetType.esriDTRasterDataset ? false : item.Type != esriDatasetType.esriDTRasterBand))
                            {
                                bool flag = true;
                                if (!((IRasterPyramid)item).Present)
                                {
                                    if (this.iapplication_0.PyramidPromptType == PyramidPromptType.AlwaysBuildNoPrompt)
                                    {
                                        ((IRasterPyramid)item).Create();
                                    }
                                    else if (this.iapplication_0.PyramidPromptType == PyramidPromptType.AlwaysPrompt)
                                    {
                                        DialogResult dialogResult = BuildPyramidSet.Show();
                                        if (dialogResult == DialogResult.Yes)
                                        {
                                            ((IRasterPyramid)item).Create();
                                        }
                                        else if (dialogResult == DialogResult.Cancel)
                                        {
                                            flag = false;
                                        }
                                    }
                                }
                                if (flag)
                                {
                                    IRasterLayer rasterLayerClass = new RasterLayerClass()
                                    {
                                        Cached = true
                                    };
                                    rasterLayerClass.CreateFromDataset((IRasterDataset)item);
                                    rasterLayerClass.Name = item.Name;
                                    this.method_0(rasterLayerClass);
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTTable)
                            {
                                if (this.object_0 is IStandaloneTableCollection)
                                {
                                    try
                                    {
                                        IStandaloneTableCollection object0 = this.object_0 as IStandaloneTableCollection;
                                        IPropertySet connectionProperties = item.Workspace.ConnectionProperties;
                                        bool flag1 = false;
                                        int num = 0;
                                        while (true)
                                        {
                                            if (num < object0.StandaloneTableCount)
                                            {
                                                ITable table = object0.StandaloneTable[num].Table;
                                                if (!connectionProperties.IsEqual((table as IDataset).Workspace.ConnectionProperties) || !((table as IDataset).Name == item.Name))
                                                {
                                                    num++;
                                                }
                                                else
                                                {
                                                    flag1 = true;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (!flag1)
                                        {
                                            object0.AddStandaloneTable(new StandaloneTableClass()
                                            {
                                                Table = item as ITable
                                            });
                                            this.HasTable = true;
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        CErrorLog.writeErrorLog(this, exception, "");
                                    }
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTGeometricNetwork)
                            {
                                IGeometricNetwork geometricNetwork = item as IGeometricNetwork;
                                if (geometricNetwork != null)
                                {
                                    IEnumFeatureClass classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTSimpleJunction];
                                    classesByType.Reset();
                                    for (n = classesByType.Next(); n != null; n = classesByType.Next())
                                    {
                                        featureLayerClass = new FeatureLayerClass()
                                        {
                                            FeatureClass = n,
                                            Name = (n as IDataset).Name
                                        };
                                        this.method_0(featureLayerClass);
                                    }
                                    classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTComplexJunction];
                                    classesByType.Reset();
                                    for (n = classesByType.Next(); n != null; n = classesByType.Next())
                                    {
                                        featureLayerClass = new FeatureLayerClass()
                                        {
                                            FeatureClass = n,
                                            Name = (n as IDataset).Name
                                        };
                                        this.method_0(featureLayerClass);
                                    }
                                    classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTSimpleEdge];
                                    classesByType.Reset();
                                    for (n = classesByType.Next(); n != null; n = classesByType.Next())
                                    {
                                        featureLayerClass = new FeatureLayerClass()
                                        {
                                            FeatureClass = n,
                                            Name = (n as IDataset).Name
                                        };
                                        this.method_0(featureLayerClass);
                                    }
                                    classesByType = geometricNetwork.ClassesByType[esriFeatureType.esriFTComplexEdge];
                                    classesByType.Reset();
                                    for (n = classesByType.Next(); n != null; n = classesByType.Next())
                                    {
                                        featureLayerClass = new FeatureLayerClass()
                                        {
                                            FeatureClass = n,
                                            Name = (n as IDataset).Name
                                        };
                                        this.method_0(featureLayerClass);
                                    }
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTNetworkDataset)
                            {
                                INetworkDataset networkDataset = item as INetworkDataset;
                                this.method_0(new NetworkLayerClass()
                                {
                                    NetworkDataset = networkDataset
                                } as ILayer);
                                if (MessageBox.Show(string.Concat("是否将参加网络要素集-", item.Name, "-的所有要素添加到地图中"), "添加拓扑层", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                {
                                    classes = (item as IFeatureClassContainer).Classes;
                                    classes.Reset();
                                    for (m = classes.Next(); m != null; m = classes.Next())
                                    {
                                        cadFeatureLayerClass = new FeatureLayerClass()
                                        {
                                            FeatureClass = m,
                                            Name = m.AliasName
                                        };
                                        this.method_1(cadFeatureLayerClass);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.method_0((objs[k] as IGxLayer).Layer);
                    }
                }
                processAssist.End();
                if (this.list_0.Count > 0)
                {
                    MessageBox.Show("警告：数据范围不一致。\r\n 一个或多个添加的图层范围与关联的空间坐标系范围不一致。请检查数据问题。存在问题图层信息请查看错误日志!", "管理平台", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string list0 in this.list_0)
                    {
                        if (stringBuilder.Length > 0)
                        {
                            stringBuilder.Append(",");
                        }
                        stringBuilder.Append(list0);
                    }
                    CErrorLog.writeErrorLog(stringBuilder, null, "图层范围不一致!");
                }
                foreach (ILayer list1 in this.list_1)
                {
                    list1.Visible = true;
                }
                if (this.object_0 is IMap && this.ienvelope_0 != null)
                {
                    IMap ienvelope0 = this.object_0 as IMap;
                    if (ienvelope0.LayerCount > 0)
                    {
                        ISpatialReference spatialReference = ienvelope0.SpatialReference;
                        if ((spatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference == null ? false : spatialReference != null))
                        {
                            this.ienvelope_0.Project(spatialReference);
                        }
                        (ienvelope0 as IActiveView).Extent = this.ienvelope_0;
                        (ienvelope0 as IActiveView).Refresh();
                    }
                }
                if (this.iapplication_0 != null)
                {
                    //((this.iapplication_0).MapDocumentChanged();
                }
                else if (ApplicationRef.Application != null)
                {
                    ApplicationRef.Application.MapDocumentChanged();
                }
            }
            catch (Exception exception1)
            {
                str = exception1.ToString();
            }
        }

        private void method_0(ILayer ilayer_0)
        {
            bool flag;
            ilayer_0.Visible = false;
            this.list_1.Add(ilayer_0);
            bool flag1 = true;
            if (ilayer_0 is IGeoDataset && !SpatialReferenctOperator.ValideFeatureClass(ilayer_0 as IGeoDataset))
            {
                this.list_0.Add(ilayer_0.Name);
                flag1 = false;
            }
            if (flag1)
            {
                bool flag2 = true;
                if (ilayer_0 is IGeoFeatureLayer)
                {
                    IFeatureClass featureClass = (ilayer_0 as IGeoFeatureLayer).FeatureClass;
                    flag2 = featureClass.FeatureCount(null) > 0;
                }
                else if (ilayer_0 is IFDOGraphicsLayer)
                {
                    ITable attributeTable = (ilayer_0 as IAttributeTable).AttributeTable;
                    flag2 = attributeTable.RowCount(null) > 0;
                }
                if (flag2)
                {
                    if (this.ienvelope_0 != null)
                    {
                        IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                        if ((areaOfInterest.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference == null ? false : areaOfInterest.SpatialReference != null))
                        {
                            if (!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) || !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem))
                            {
                                flag = (this.ienvelope_0.SpatialReference != null ? true : areaOfInterest.SpatialReference != null);
                            }
                            else
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                            else
                            {
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                        }
                    }
                    else
                    {
                        this.ienvelope_0 = ilayer_0.AreaOfInterest;
                    }
                }
            }
            if (this.object_0 is IMapLayers2)
            {
                this.method_2(this.object_0 as IMapLayers2, ilayer_0, flag1);
            }
            else if (this.object_0 is IMap)
            {
                this.method_3(this.object_0 as IMap, ilayer_0, flag1);
            }
            else if (this.object_0 is IGroupLayer)
            {
                this.method_5(this.object_0 as IGroupLayer, ilayer_0, flag1);
            }
            if (this.InMapCtrl != null && this.iapplication_0.MapControl.Map == this.object_0 && this.iapplication_0.MapControl.Map.LayerCount > this.InMapCtrl.Map.LayerCount)
            {
                int layerIndexInMap = CommonHelper.GetLayerIndexInMap(this.iapplication_0.MapControl.Map as IBasicMap, ilayer_0);
                (this.InMapCtrl.Map as IMapLayers).InsertLayer(ilayer_0, true, layerIndexInMap);
            }
        }

        private void method_1(ILayer ilayer_0)
        {
            bool flag = false;
            if (this.object_0 is IMapLayers2)
            {
                this.method_2(this.object_0 as IMapLayers2, ilayer_0, flag);
            }
            else if (this.object_0 is IMap)
            {
                this.method_3(this.object_0 as IMap, ilayer_0, flag);
            }
            else if (this.object_0 is IBasicMap)
            {
                this.method_4(this.object_0 as IBasicMap, ilayer_0, flag);
            }
            else if (this.object_0 is IGroupLayer)
            {
                this.method_5(this.object_0 as IGroupLayer, ilayer_0, flag);
            }
        }

        private void method_2(IMapLayers2 imapLayers2_0, ILayer ilayer_0, bool bool_1)
        {
            Exception exception;
            try
            {
                if (imapLayers2_0 is IScene)
                {
                    try
                    {
                        imapLayers2_0.AddLayer(ilayer_0);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        if (!(exception is COMException))
                        {
                            MessageBox.Show(exception.Message);
                        }
                        else if ((exception as COMException).ErrorCode != -2147219200)
                        {
                            MessageBox.Show(exception.Message);
                        }
                        else
                        {
                            MessageBox.Show("无法添加具有未知空间参考的数据");
                        }
                    }
                }
                else if (!(imapLayers2_0 is IGlobe))
                {
                    int layerIndex = -1;
                    layerIndex = CommonHelper.GetLayerIndex(imapLayers2_0 as IMap, ilayer_0);
                    imapLayers2_0.InsertLayer(ilayer_0, true, layerIndex);
                }
                else
                {
                    try
                    {
                        imapLayers2_0.AddLayer(ilayer_0);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        if (!(exception is COMException))
                        {
                            MessageBox.Show(exception.Message);
                        }
                        else if ((exception as COMException).ErrorCode != -2147219200)
                        {
                            MessageBox.Show(exception.Message);
                        }
                        else
                        {
                            MessageBox.Show("无法添加具有未知空间参考的数据");
                        }
                    }
                }
            }
            catch (Exception exception3)
            {
            }
        }

        private void method_3(IMap imap_0, ILayer ilayer_0, bool bool_1)
        {
            try
            {
                if (bool_1)
                {
                    bool flag = true;
                    if (ilayer_0 is IGeoFeatureLayer)
                    {
                        IFeatureClass featureClass = (ilayer_0 as IGeoFeatureLayer).FeatureClass;
                        flag = featureClass.FeatureCount(null) > 0;
                    }
                    else if (ilayer_0 is IFDOGraphicsLayer)
                    {
                        ITable attributeTable = (ilayer_0 as IAttributeTable).AttributeTable;
                        flag = attributeTable.RowCount(null) > 0;
                    }
                    if (flag)
                    {
                        if (this.ienvelope_0 != null)
                        {
                            IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                            if ((areaOfInterest.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference == null ? false : areaOfInterest.SpatialReference != null))
                            {
                                areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                        }
                        else
                        {
                            this.ienvelope_0 = ilayer_0.AreaOfInterest;
                        }
                    }
                }
                int layerIndex = 0;
                layerIndex = CommonHelper.GetLayerIndex(imap_0, ilayer_0);
                if (!(imap_0 is IMapLayers2))
                {
                    imap_0.AddLayer(ilayer_0);
                    if (layerIndex != 0)
                    {
                        imap_0.MoveLayer(ilayer_0, layerIndex);
                    }
                }
                else
                {
                    (imap_0 as IMapLayers2).InsertLayer(ilayer_0, true, layerIndex);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void method_4(IBasicMap ibasicMap_0, ILayer ilayer_0, bool bool_1)
        {
            try
            {
                if (bool_1)
                {
                    bool flag = true;
                    if (ilayer_0 is IGeoFeatureLayer)
                    {
                        IFeatureClass featureClass = (ilayer_0 as IGeoFeatureLayer).FeatureClass;
                        flag = featureClass.FeatureCount(null) > 0;
                    }
                    else if (ilayer_0 is IFDOGraphicsLayer)
                    {
                        ITable attributeTable = (ilayer_0 as IAttributeTable).AttributeTable;
                        flag = attributeTable.RowCount(null) > 0;
                    }
                    if (flag)
                    {
                        if (this.ienvelope_0 != null)
                        {
                            IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                            if ((areaOfInterest.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference == null ? false : areaOfInterest.SpatialReference != null))
                            {
                                areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                        }
                        else
                        {
                            this.ienvelope_0 = ilayer_0.AreaOfInterest;
                        }
                    }
                }
                ibasicMap_0.AddLayer(ilayer_0);
            }
            catch (Exception exception)
            {
            }
        }

        private void method_5(IGroupLayer igroupLayer_0, ILayer ilayer_0, bool bool_1)
        {
            if (bool_1)
            {
                bool flag = true;
                if (ilayer_0 is IGeoFeatureLayer)
                {
                    IFeatureClass featureClass = (ilayer_0 as IGeoFeatureLayer).FeatureClass;
                    flag = featureClass.FeatureCount(null) > 0;
                }
                if (flag)
                {
                    if (this.ienvelope_0 != null)
                    {
                        IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                        if ((areaOfInterest.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem || this.ienvelope_0.SpatialReference == null ? false : areaOfInterest.SpatialReference != null))
                        {
                            areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                            this.ienvelope_0.Union(areaOfInterest);
                        }
                    }
                    else
                    {
                        this.ienvelope_0 = ilayer_0.AreaOfInterest;
                    }
                }
            }
            igroupLayer_0.Add(ilayer_0);
        }
    }
}