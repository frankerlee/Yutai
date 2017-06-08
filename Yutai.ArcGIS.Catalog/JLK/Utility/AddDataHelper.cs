namespace JLK.Utility
{
    using ESRI.ArcGIS.Analyst3D;
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Controls;
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.DataSourcesRaster;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using ESRI.ArcGIS.GlobeCore;
    using JLK.Catalog;
    using JLK.Utility.BaseClass;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class AddDataHelper
    {
        [CompilerGenerated]
        private bool bool_0;
        private IActiveView iactiveView_0;
        private IApplication iapplication_0 = ApplicationRef.Application;
        private IEnvelope ienvelope_0 = null;
        [CompilerGenerated]
        private IMapControl2 imapControl2_0;
        private List<string> list_0 = new List<string>();
        private List<ILayer> list_1 = null;
        private object object_0 = null;

        public AddDataHelper(object object_1)
        {
            this.object_0 = object_1;
            this.HasTable = false;
        }

        public void LoadData(IList ilist_0)
        {
            IEnumDataset subsets;
            IDataset dataset2;
            string str2;
            this.list_1 = new List<ILayer>();
            List<object> list = new List<object>();
            foreach (object obj2 in ilist_0)
            {
                if (obj2 is IGxDataset)
                {
                    if ((obj2 as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumGxObject children = (obj2 as IGxObjectContainer).Children;
                        children.Reset();
                        for (IGxObject obj4 = children.Next(); obj4 != null; obj4 = children.Next())
                        {
                            list.Add(obj4);
                        }
                    }
                    else
                    {
                        list.Add(obj2);
                    }
                }
                else if (obj2 is IDataset)
                {
                    if ((obj2 as IDataset).Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        subsets = (obj2 as IDataset).Subsets;
                        subsets.Reset();
                        for (dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                        {
                            list.Add(dataset2);
                        }
                    }
                }
                else
                {
                    list.Add(obj2);
                }
            }
            ProcessAssist assist = new ProcessAssist();
            assist.InitProgress();
            assist.SetMaxValue(list.Count);
            assist.SetMessage("正在加载数据,请稍候...");
            assist.Start();
            try
            {
                ILayer layer = null;
                IDataset dataset = null;
                IGxDataset dataset4 = null;
                int num = 0;
                while (true)
                {
                    if (num >= list.Count)
                    {
                        break;
                    }
                    string name = "";
                    if (list[num] is IGxObject)
                    {
                        name = (list[num] as IGxObject).Name;
                    }
                    else if (list[num] is IDataset)
                    {
                        name = (list[num] as IDataset).Name;
                    }
                    str2 = string.Format("加载图层[{0}]，第{1}/{2}个", name, num + 1, list.Count);
                    assist.Increment(1);
                    assist.SetMessage(str2);
                    if (list[num] is IGxLayer)
                    {
                        layer = (list[num] as IGxLayer).Layer;
                        this.method_0(layer);
                    }
                    else
                    {
                        if (list[num] is IGxDataset)
                        {
                            dataset4 = list[num] as IGxDataset;
                            dataset = dataset4.Dataset;
                        }
                        else if (list[num] is IDataset)
                        {
                            dataset = list[num] as IDataset;
                        }
                        if (dataset != null)
                        {
                            IFeatureClass class2;
                            IFeatureLayer layer2;
                            if (dataset.Type == esriDatasetType.esriDTFeatureClass)
                            {
                                class2 = (IFeatureClass) dataset;
                                if ((dataset4 as IGxObject).Category.IndexOf("CAD") != -1)
                                {
                                    if (class2.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                                    {
                                        layer2 = new CadAnnotationLayerClass();
                                    }
                                    else
                                    {
                                        layer2 = new CadFeatureLayerClass();
                                    }
                                    layer2.FeatureClass = class2;
                                    layer2.Name = class2.AliasName;
                                }
                                else if (class2.FeatureType == esriFeatureType.esriFTAnnotation)
                                {
                                    layer2 = new FDOGraphicsLayerClass {
                                        FeatureClass = class2,
                                        Name = class2.AliasName
                                    };
                                }
                                else if (class2.FeatureType == esriFeatureType.esriFTDimension)
                                {
                                    layer2 = new DimensionLayerClass {
                                        FeatureClass = class2,
                                        Name = class2.AliasName
                                    };
                                }
                                else
                                {
                                    layer2 = new FeatureLayerClass {
                                        FeatureClass = class2,
                                        Name = class2.AliasName
                                    };
                                }
                                this.method_0(layer2);
                            }
                            else if (dataset.Type == esriDatasetType.esriDTCadDrawing)
                            {
                                if ((dataset4 as IGxObject).Category == "CAD绘图")
                                {
                                    ICadLayer layer3 = new CadLayerClass {
                                        CadDrawingDataset = dataset4.Dataset as ICadDrawingDataset,
                                        Name = dataset4.Dataset.Name
                                    };
                                    this.method_0(layer3);
                                }
                                else
                                {
                                    IEnumGxObject obj5 = (dataset4 as IGxObjectContainer).Children;
                                    obj5.Reset();
                                    for (IGxDataset dataset5 = obj5.Next() as IGxDataset; dataset5 != null; dataset5 = obj5.Next() as IGxDataset)
                                    {
                                        class2 = dataset5.Dataset as IFeatureClass;
                                        if (class2 != null)
                                        {
                                            if (class2.FeatureType == esriFeatureType.esriFTAnnotation)
                                            {
                                                layer2 = new FDOGraphicsLayerClass {
                                                    FeatureClass = class2,
                                                    Name = class2.AliasName
                                                };
                                            }
                                            else
                                            {
                                                layer2 = new CadFeatureLayerClass {
                                                    FeatureClass = class2,
                                                    Name = class2.AliasName
                                                };
                                            }
                                            this.method_0(layer2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ITopologyLayer layer4;
                                if (dataset.Type == esriDatasetType.esriDTFeatureDataset)
                                {
                                    subsets = dataset.Subsets;
                                    subsets.Reset();
                                    for (dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                                    {
                                        if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                                        {
                                            class2 = dataset2 as IFeatureClass;
                                            if (class2.FeatureType == esriFeatureType.esriFTAnnotation)
                                            {
                                                layer2 = new FDOGraphicsLayerClass {
                                                    FeatureClass = class2,
                                                    Name = class2.AliasName
                                                };
                                                this.method_0(layer2);
                                            }
                                            else
                                            {
                                                layer2 = new FeatureLayerClass {
                                                    FeatureClass = class2,
                                                    Name = class2.AliasName
                                                };
                                                this.method_0(layer2);
                                            }
                                        }
                                        else if (dataset2.Type == esriDatasetType.esriDTTopology)
                                        {
                                            layer4 = new TopologyLayerClass {
                                                Topology = dataset2 as ITopology
                                            };
                                            (layer4 as ILayer).Name = dataset2.Name;
                                            this.method_0(layer4 as ILayer);
                                        }
                                    }
                                }
                                else
                                {
                                    IEnumFeatureClass classes;
                                    IFeatureClass class4;
                                    if (dataset.Type == esriDatasetType.esriDTTopology)
                                    {
                                        if (MessageBox.Show("是否将参加拓扑-" + dataset.Name + "-的所有要素添加到地图中", "添加拓扑层", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                        {
                                            classes = (dataset as IFeatureClassContainer).Classes;
                                            classes.Reset();
                                            for (class4 = classes.Next(); class4 != null; class4 = classes.Next())
                                            {
                                                layer2 = new FeatureLayerClass {
                                                    FeatureClass = class4,
                                                    Name = class4.AliasName
                                                };
                                                this.method_0(layer2);
                                            }
                                        }
                                        layer4 = new TopologyLayerClass {
                                            Topology = dataset as ITopology
                                        };
                                        (layer4 as ILayer).Name = dataset.Name;
                                        this.method_0(layer4 as ILayer);
                                    }
                                    else if (dataset.Type == esriDatasetType.esriDTTin)
                                    {
                                        ITinLayer layer5 = new TinLayerClass {
                                            Dataset = (ITin) dataset,
                                            Name = dataset.Name
                                        };
                                        this.method_0(layer5);
                                    }
                                    else if (dataset.Type == esriDatasetType.esriDTRasterCatalog)
                                    {
                                        IGdbRasterCatalogLayer layer6 = null;
                                        layer6 = new GdbRasterCatalogLayerClass();
                                        IRasterCatalog catalog = null;
                                        catalog = dataset as IRasterCatalog;
                                        if (layer6.Setup((ITable) catalog))
                                        {
                                            this.method_0(layer6 as ILayer);
                                        }
                                    }
                                    else if ((dataset.Type == esriDatasetType.esriDTRasterDataset) || (dataset.Type == esriDatasetType.esriDTRasterBand))
                                    {
                                        bool flag = true;
                                        if (!((IRasterPyramid) dataset).Present)
                                        {
                                            if (this.iapplication_0.PyramidPromptType == PyramidPromptType.AlwaysBuildNoPrompt)
                                            {
                                                ((IRasterPyramid) dataset).Create();
                                            }
                                            else if (this.iapplication_0.PyramidPromptType == PyramidPromptType.AlwaysPrompt)
                                            {
                                                switch (BuildPyramidSet.Show())
                                                {
                                                    case DialogResult.Yes:
                                                        ((IRasterPyramid) dataset).Create();
                                                        break;

                                                    case DialogResult.Cancel:
                                                        flag = false;
                                                        break;
                                                }
                                            }
                                        }
                                        if (flag)
                                        {
                                            RasterLayerClass class5 = new RasterLayerClass {
                                                Cached = true
                                            };
                                            IRasterLayer layer7 = class5;
                                            layer7.CreateFromDataset((IRasterDataset) dataset);
                                            layer7.Name = dataset.Name;
                                            this.method_0(layer7);
                                        }
                                    }
                                    else if (dataset.Type == esriDatasetType.esriDTTable)
                                    {
                                        if (this.object_0 is IStandaloneTableCollection)
                                        {
                                            try
                                            {
                                                IStandaloneTableCollection tables = this.object_0 as IStandaloneTableCollection;
                                                IPropertySet connectionProperties = dataset.Workspace.ConnectionProperties;
                                                bool flag2 = false;
                                                for (int i = 0; i < tables.StandaloneTableCount; i++)
                                                {
                                                    ITable table = tables.get_StandaloneTable(i).Table;
                                                    if (connectionProperties.IsEqual((table as IDataset).Workspace.ConnectionProperties) && ((table as IDataset).Name == dataset.Name))
                                                    {
                                                        goto Label_0877;
                                                    }
                                                }
                                                goto Label_087A;
                                            Label_0877:
                                                flag2 = true;
                                            Label_087A:
                                                if (!flag2)
                                                {
                                                    IStandaloneTable table2 = new StandaloneTableClass {
                                                        Table = dataset as ITable
                                                    };
                                                    tables.AddStandaloneTable(table2);
                                                    this.HasTable = true;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                CErrorLog.writeErrorLog(this, exception, "");
                                            }
                                        }
                                    }
                                    else if (dataset.Type == esriDatasetType.esriDTGeometricNetwork)
                                    {
                                        IGeometricNetwork network = dataset as IGeometricNetwork;
                                        if (network != null)
                                        {
                                            IFeatureClass class7;
                                            IFeatureLayer layer8;
                                            IEnumFeatureClass class6 = network.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
                                            class6.Reset();
                                            for (class7 = class6.Next(); class7 != null; class7 = class6.Next())
                                            {
                                                layer8 = new FeatureLayerClass {
                                                    FeatureClass = class7,
                                                    Name = (class7 as IDataset).Name
                                                };
                                                this.method_0(layer8);
                                            }
                                            class6 = network.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
                                            class6.Reset();
                                            for (class7 = class6.Next(); class7 != null; class7 = class6.Next())
                                            {
                                                layer8 = new FeatureLayerClass {
                                                    FeatureClass = class7,
                                                    Name = (class7 as IDataset).Name
                                                };
                                                this.method_0(layer8);
                                            }
                                            class6 = network.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
                                            class6.Reset();
                                            for (class7 = class6.Next(); class7 != null; class7 = class6.Next())
                                            {
                                                layer8 = new FeatureLayerClass {
                                                    FeatureClass = class7,
                                                    Name = (class7 as IDataset).Name
                                                };
                                                this.method_0(layer8);
                                            }
                                            class6 = network.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
                                            class6.Reset();
                                            for (class7 = class6.Next(); class7 != null; class7 = class6.Next())
                                            {
                                                layer8 = new FeatureLayerClass {
                                                    FeatureClass = class7,
                                                    Name = (class7 as IDataset).Name
                                                };
                                                this.method_0(layer8);
                                            }
                                        }
                                    }
                                    else if (dataset.Type == esriDatasetType.esriDTNetworkDataset)
                                    {
                                        INetworkDataset dataset6 = dataset as INetworkDataset;
                                        INetworkLayer layer9 = new NetworkLayerClass {
                                            NetworkDataset = dataset6
                                        };
                                        this.method_0(layer9 as ILayer);
                                        if (MessageBox.Show("是否将参加网络要素集-" + dataset.Name + "-的所有要素添加到地图中", "添加拓扑层", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                        {
                                            classes = (dataset as IFeatureClassContainer).Classes;
                                            classes.Reset();
                                            for (class4 = classes.Next(); class4 != null; class4 = classes.Next())
                                            {
                                                layer2 = new FeatureLayerClass {
                                                    FeatureClass = class4,
                                                    Name = class4.AliasName
                                                };
                                                this.method_1(layer2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    num++;
                }
                assist.End();
                if (this.list_0.Count > 0)
                {
                    MessageBox.Show("警告：数据范围不一致。\r\n 一个或多个添加的图层范围与关联的空间坐标系范围不一致。请检查数据问题。存在问题图层信息请查看错误日志!", "管理平台", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    StringBuilder builder = new StringBuilder();
                    foreach (string str3 in this.list_0)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append(",");
                        }
                        builder.Append(str3);
                    }
                    CErrorLog.writeErrorLog(builder, null, "图层范围不一致!");
                }
                foreach (ILayer layer10 in this.list_1)
                {
                    layer10.Visible = true;
                }
                if ((this.object_0 is IMap) && (this.ienvelope_0 != null))
                {
                    IMap map = this.object_0 as IMap;
                    if (map.LayerCount > 0)
                    {
                        ISpatialReference spatialReference = map.SpatialReference;
                        if (((!(spatialReference is IUnknownCoordinateSystem) && !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) && (this.ienvelope_0.SpatialReference != null)) && (spatialReference != null))
                        {
                            this.ienvelope_0.Project(spatialReference);
                        }
                        (map as IActiveView).Extent = this.ienvelope_0;
                        (map as IActiveView).Refresh();
                    }
                }
                if (this.iapplication_0 != null)
                {
                    this.iapplication_0.MapDocumentChanged();
                }
                else if (ApplicationRef.Application != null)
                {
                    ApplicationRef.Application.MapDocumentChanged();
                }
            }
            catch (Exception exception2)
            {
                str2 = exception2.ToString();
            }
        }

        private void method_0(ILayer ilayer_0)
        {
            ilayer_0.Visible = false;
            this.list_1.Add(ilayer_0);
            bool flag = true;
            if ((ilayer_0 is IGeoDataset) && !SpatialReferenctOperator.ValideFeatureClass(ilayer_0 as IGeoDataset))
            {
                this.list_0.Add(ilayer_0.Name);
                flag = false;
            }
            if (flag)
            {
                bool flag2 = true;
                if (ilayer_0 is IGeoFeatureLayer)
                {
                    flag2 = (ilayer_0 as IGeoFeatureLayer).FeatureClass.FeatureCount(null) > 0;
                }
                else if (ilayer_0 is IFDOGraphicsLayer)
                {
                    flag2 = (ilayer_0 as IAttributeTable).AttributeTable.RowCount(null) > 0;
                }
                if (flag2)
                {
                    if (this.ienvelope_0 == null)
                    {
                        this.ienvelope_0 = ilayer_0.AreaOfInterest;
                    }
                    else
                    {
                        IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                        if (((!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) && !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) && (this.ienvelope_0.SpatialReference != null)) && (areaOfInterest.SpatialReference != null))
                        {
                            if (!((!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) || !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) ? ((this.ienvelope_0.SpatialReference != null) || (areaOfInterest.SpatialReference != null)) : false))
                            {
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                            else
                            {
                                areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                        }
                    }
                }
            }
            if (this.object_0 is IMapLayers2)
            {
                this.method_2(this.object_0 as IMapLayers2, ilayer_0, flag);
            }
            else if (this.object_0 is IMap)
            {
                this.method_3(this.object_0 as IMap, ilayer_0, flag);
            }
            else if (this.object_0 is IGroupLayer)
            {
                this.method_5(this.object_0 as IGroupLayer, ilayer_0, flag);
            }
            if (((this.InMapCtrl != null) && (this.iapplication_0.FocusMap == this.object_0)) && (this.iapplication_0.FocusMap.LayerCount > this.InMapCtrl.Map.LayerCount))
            {
                int layerIndexInMap = Common.GetLayerIndexInMap(this.iapplication_0.FocusMap as IBasicMap, ilayer_0);
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
            try
            {
                Exception exception;
                if (imapLayers2_0 is IScene)
                {
                    try
                    {
                        imapLayers2_0.AddLayer(ilayer_0);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        if (exception is COMException)
                        {
                            if ((exception as COMException).ErrorCode == -2147219200)
                            {
                                MessageBox.Show("无法添加具有未知空间参考的数据");
                            }
                            else
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }
                }
                else if (imapLayers2_0 is IGlobe)
                {
                    try
                    {
                        imapLayers2_0.AddLayer(ilayer_0);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        if (exception is COMException)
                        {
                            if ((exception as COMException).ErrorCode == -2147219200)
                            {
                                MessageBox.Show("无法添加具有未知空间参考的数据");
                            }
                            else
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }
                }
                else
                {
                    int position = -1;
                    position = Common.GetLayerIndex(imapLayers2_0 as IMap, ilayer_0);
                    imapLayers2_0.InsertLayer(ilayer_0, true, position);
                }
            }
            catch (Exception)
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
                        flag = (ilayer_0 as IGeoFeatureLayer).FeatureClass.FeatureCount(null) > 0;
                    }
                    else if (ilayer_0 is IFDOGraphicsLayer)
                    {
                        flag = (ilayer_0 as IAttributeTable).AttributeTable.RowCount(null) > 0;
                    }
                    if (flag)
                    {
                        if (this.ienvelope_0 == null)
                        {
                            this.ienvelope_0 = ilayer_0.AreaOfInterest;
                        }
                        else
                        {
                            IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                            if (((!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) && !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) && (this.ienvelope_0.SpatialReference != null)) && (areaOfInterest.SpatialReference != null))
                            {
                                areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                        }
                    }
                }
                int position = 0;
                position = Common.GetLayerIndex(imap_0, ilayer_0);
                if (imap_0 is IMapLayers2)
                {
                    (imap_0 as IMapLayers2).InsertLayer(ilayer_0, true, position);
                }
                else
                {
                    imap_0.AddLayer(ilayer_0);
                    if (position != 0)
                    {
                        imap_0.MoveLayer(ilayer_0, position);
                    }
                }
            }
            catch (Exception)
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
                        flag = (ilayer_0 as IGeoFeatureLayer).FeatureClass.FeatureCount(null) > 0;
                    }
                    else if (ilayer_0 is IFDOGraphicsLayer)
                    {
                        flag = (ilayer_0 as IAttributeTable).AttributeTable.RowCount(null) > 0;
                    }
                    if (flag)
                    {
                        if (this.ienvelope_0 == null)
                        {
                            this.ienvelope_0 = ilayer_0.AreaOfInterest;
                        }
                        else
                        {
                            IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                            if (((!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) && !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) && (this.ienvelope_0.SpatialReference != null)) && (areaOfInterest.SpatialReference != null))
                            {
                                areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                                this.ienvelope_0.Union(areaOfInterest);
                            }
                        }
                    }
                }
                ibasicMap_0.AddLayer(ilayer_0);
            }
            catch (Exception)
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
                    flag = (ilayer_0 as IGeoFeatureLayer).FeatureClass.FeatureCount(null) > 0;
                }
                if (flag)
                {
                    if (this.ienvelope_0 == null)
                    {
                        this.ienvelope_0 = ilayer_0.AreaOfInterest;
                    }
                    else
                    {
                        IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
                        if (((!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) && !(this.ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) && (this.ienvelope_0.SpatialReference != null)) && (areaOfInterest.SpatialReference != null))
                        {
                            areaOfInterest.Project(this.ienvelope_0.SpatialReference);
                            this.ienvelope_0.Union(areaOfInterest);
                        }
                    }
                }
            }
            igroupLayer_0.Add(ilayer_0);
        }

        public bool HasTable
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.bool_0 = value;
            }
        }

        public IMapControl2 InMapCtrl
        {
            [CompilerGenerated]
            get
            {
                return this.imapControl2_0;
            }
            [CompilerGenerated]
            set
            {
                this.imapControl2_0 = value;
            }
        }
    }
}

