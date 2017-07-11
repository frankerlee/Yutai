using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Controls
{
	internal sealed class AddDataHelper : System.Windows.Forms.Control
	{
		public delegate void MessageHandler(IList ilist_0);

		private OnLoadCompleteHand onLoadCompleteHand_0;

		private IActiveView iactiveView_0;

		public IAppContext m_pApp = null;

		private List<ILayer> list_0 = null;

	    private IGroupLayer _parentLayer = null;

        public IGroupLayer ParentLayer
        {
            get
            {
                return _parentLayer;
            }

            set
            {
                _parentLayer = value;
            }
        }

        public event OnLoadCompleteHand OnLoadComplete
		{
			add
			{
				OnLoadCompleteHand onLoadCompleteHand = this.onLoadCompleteHand_0;
				OnLoadCompleteHand onLoadCompleteHand2;
				do
				{
					onLoadCompleteHand2 = onLoadCompleteHand;
					OnLoadCompleteHand value2 = (OnLoadCompleteHand)Delegate.Combine(onLoadCompleteHand2, value);
					onLoadCompleteHand = Interlocked.CompareExchange<OnLoadCompleteHand>(ref this.onLoadCompleteHand_0, value2, onLoadCompleteHand2);
				}
				while (onLoadCompleteHand != onLoadCompleteHand2);
			}
			remove
			{
				OnLoadCompleteHand onLoadCompleteHand = this.onLoadCompleteHand_0;
				OnLoadCompleteHand onLoadCompleteHand2;
				do
				{
					onLoadCompleteHand2 = onLoadCompleteHand;
					OnLoadCompleteHand value2 = (OnLoadCompleteHand)Delegate.Remove(onLoadCompleteHand2, value);
					onLoadCompleteHand = Interlocked.CompareExchange<OnLoadCompleteHand>(ref this.onLoadCompleteHand_0, value2, onLoadCompleteHand2);
				}
				while (onLoadCompleteHand != onLoadCompleteHand2);
			}
		}

		public AddDataHelper(IActiveView iactiveView_1)
		{
			this.CreateHandle();
			base.CreateControl();
			this.iactiveView_0 = iactiveView_1;
		}

		public void InvokeMethod(IList ilist_0)
		{
			if (!base.IsDisposed && base.IsHandleCreated)
			{
				base.Invoke(new AddDataHelper.MessageHandler(this.LoadData), new object[]
				{
					ilist_0
				});
			}
		}

		private IMap GetMap()
		{
			return this.iactiveView_0.FocusMap;
		}

		private void AddLayer(IMap imap_0, ILayer ilayer_0, ref IEnvelope ienvelope_0, bool bool_0)
		{
			try
			{
				ilayer_0.Visible = false;
				this.list_0.Add(ilayer_0);
				if (bool_0)
				{
					bool flag = true;
					if (ilayer_0 is IGeoFeatureLayer)
					{
						IFeatureClass featureClass = (ilayer_0 as IGeoFeatureLayer).FeatureClass;
						flag = (featureClass.FeatureCount(null) > 0);
					}
					else if (ilayer_0 is IFDOGraphicsLayer)
					{
						ITable attributeTable = (ilayer_0 as IAttributeTable).AttributeTable;
						flag = (attributeTable.RowCount(null) > 0);
					}
					if (flag)
					{
						if (ienvelope_0 == null)
						{
							ienvelope_0 = ilayer_0.AreaOfInterest;
						}
						else
						{
							IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
							if (!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) && !(ienvelope_0.SpatialReference is IUnknownCoordinateSystem) && ienvelope_0.SpatialReference != null && areaOfInterest.SpatialReference != null)
							{
								areaOfInterest.Project(ienvelope_0.SpatialReference);
								ienvelope_0.Union(areaOfInterest);
							}
						}
					}
				}

			    if (_parentLayer != null)
			    {
			        _parentLayer.Add(ilayer_0);
			        return;
			    }
				int layerIndex = Common.GetLayerIndex(imap_0, ilayer_0);
				if (imap_0 is IMapLayers2)
				{
					(imap_0 as IMapLayers2).InsertLayer(ilayer_0, true, layerIndex);
				}
				else
				{
					imap_0.AddLayer(ilayer_0);
					if (layerIndex != 0)
					{
						imap_0.MoveLayer(ilayer_0, layerIndex);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void AddLayer(IGroupLayer igroupLayer_0, ILayer ilayer_0, ref IEnvelope ienvelope_0)
		{
			bool flag = true;
			if (ilayer_0 is IGeoFeatureLayer)
			{
				IFeatureClass featureClass = (ilayer_0 as IGeoFeatureLayer).FeatureClass;
				flag = (featureClass.FeatureCount(null) > 0);
			}
			if (flag)
			{
				if (ienvelope_0 == null)
				{
					ienvelope_0 = ilayer_0.AreaOfInterest;
				}
				else
				{
					IEnvelope areaOfInterest = ilayer_0.AreaOfInterest;
					if (!(areaOfInterest.SpatialReference is IUnknownCoordinateSystem) && !(ienvelope_0.SpatialReference is IUnknownCoordinateSystem) && ienvelope_0.SpatialReference != null && areaOfInterest.SpatialReference != null)
					{
						areaOfInterest.Project(ienvelope_0.SpatialReference);
						ienvelope_0.Union(areaOfInterest);
					}
				}
			}
			igroupLayer_0.Add(ilayer_0);
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
                ILayer layer = null;
                IMap map = this.GetMap();
                this.list_0 = new List<ILayer>();
                List<string> strs = new List<string>();
                IEnvelope envelope = null;
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
                    bool flag = true;
                    str = string.Format("加载图层[{0}]，第{1}/{2}个", name, k + 1, objs.Count);
                    processAssist.Increment(1);
                    processAssist.SetMessage(str);
                    if (objs[k] is IGxLayer)
                    {
                        layer = (objs[k] as IGxLayer).Layer;
                        if (layer is IGeoDataset && !SpatialReferenctOperator.ValideFeatureClass(layer as IGeoDataset))
                        {
                            strs.Add(layer.Name);
                            flag = false;
                        }
                        this.AddLayer(map, layer, ref envelope, flag);
                    }
                    else if (!(objs[k] is IGxAGSObject))
                    {
                        if (objs[k] is IGxDataset)
                        {
                            gxDataset = objs[k] as IGxDataset;
                            item = gxDataset.Dataset;
                        }
                        else if (ilist_0[k] is IDataset)
                        {
                            item = objs[k] as IDataset;
                        }
                        if (item != null)
                        {
                            if (item is IGeoDataset)
                            {
                                flag = SpatialReferenctOperator.ValideFeatureClass(item as IGeoDataset);
                            }
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
                                if (!flag)
                                {
                                    strs.Add(cadFeatureLayerClass.Name);
                                }
                                this.AddLayer(map, cadFeatureLayerClass, ref envelope, flag);
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
                                            flag = true;
                                            if (cadFeatureLayerClass is IGeoDataset)
                                            {
                                                bool flag1 = SpatialReferenctOperator.ValideFeatureClass(cadFeatureLayerClass as IGeoDataset);
                                                flag = flag1;
                                                if (!flag1)
                                                {
                                                    strs.Add(cadFeatureLayerClass.Name);
                                                }
                                            }
                                            this.AddLayer(map, cadFeatureLayerClass, ref envelope, flag);
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
                                    if (!flag)
                                    {
                                        strs.Add(cadLayerClass.Name);
                                    }
                                    this.AddLayer(map, cadLayerClass, ref envelope, flag);
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
                                            flag = true;
                                            if (cadFeatureLayerClass is IGeoDataset)
                                            {
                                                bool flag2 = SpatialReferenctOperator.ValideFeatureClass(cadFeatureLayerClass as IGeoDataset);
                                                flag = flag2;
                                                if (!flag2)
                                                {
                                                    strs.Add(cadFeatureLayerClass.Name);
                                                }
                                            }
                                            this.AddLayer(map, cadFeatureLayerClass, ref envelope, flag);
                                        }
                                        else
                                        {
                                            cadFeatureLayerClass = new FDOGraphicsLayerClass()
                                            {
                                                FeatureClass = dataset,
                                                Name = dataset.AliasName
                                            };
                                            flag = true;
                                            if (cadFeatureLayerClass is IGeoDataset)
                                            {
                                                bool flag3 = SpatialReferenctOperator.ValideFeatureClass(cadFeatureLayerClass as IGeoDataset);
                                                flag = flag3;
                                                if (!flag3)
                                                {
                                                    strs.Add(cadFeatureLayerClass.Name);
                                                }
                                            }
                                            this.AddLayer(map, cadFeatureLayerClass, ref envelope, flag);
                                        }
                                    }
                                    else if (j.Type == esriDatasetType.esriDTTopology)
                                    {
                                        topologyLayerClass = new TopologyLayerClass()
                                        {
                                            Topology = j as ITopology
                                        };
                                        (topologyLayerClass as ILayer).Name = j.Name;
                                        this.AddLayer(map, topologyLayerClass as ILayer, ref envelope, true);
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
                                        flag = true;
                                        if (cadFeatureLayerClass is IGeoDataset)
                                        {
                                            bool flag4 = SpatialReferenctOperator.ValideFeatureClass(cadFeatureLayerClass as IGeoDataset);
                                            flag = flag4;
                                            if (!flag4)
                                            {
                                                strs.Add(cadFeatureLayerClass.Name);
                                            }
                                        }
                                        this.AddLayer(map, cadFeatureLayerClass, ref envelope, flag);
                                    }
                                }
                                topologyLayerClass = new TopologyLayerClass()
                                {
                                    Topology = item as ITopology
                                };
                                (topologyLayerClass as ILayer).Name = item.Name;
                                this.AddLayer(map, topologyLayerClass as ILayer, ref envelope, true);
                            }
                            else if (item.Type == esriDatasetType.esriDTTin)
                            {
                                ITinLayer tinLayerClass = new TinLayerClass()
                                {
                                    Dataset = (ITin)item,
                                    Name = item.Name
                                };
                                flag = true;
                                if (tinLayerClass is IGeoDataset)
                                {
                                    bool flag5 = SpatialReferenctOperator.ValideFeatureClass(tinLayerClass as IGeoDataset);
                                    flag = flag5;
                                    if (!flag5)
                                    {
                                        strs.Add(tinLayerClass.Name);
                                    }
                                }
                                this.AddLayer(map, tinLayerClass, ref envelope, flag);
                            }
                            else if (item.Type == esriDatasetType.esriDTRasterCatalog)
                            {
                                IGdbRasterCatalogLayer gdbRasterCatalogLayerClass = null;
                                gdbRasterCatalogLayerClass = new GdbRasterCatalogLayerClass();
                                if (gdbRasterCatalogLayerClass.Setup((ITable)(item as IRasterCatalog)))
                                {
                                    bool flag6 = SpatialReferenctOperator.ValideFeatureClass(gdbRasterCatalogLayerClass as IGeoDataset);
                                    flag = flag6;
                                    if (!flag6)
                                    {
                                        strs.Add((gdbRasterCatalogLayerClass as ILayer).Name);
                                    }
                                    this.AddLayer(map, gdbRasterCatalogLayerClass as ILayer, ref envelope, flag);
                                }
                            }
                            else if (!(item.Type == esriDatasetType.esriDTRasterDataset ? false : item.Type != esriDatasetType.esriDTRasterBand))
                            {
                                bool flag7 = true;
                                if (!((IRasterPyramid)item).Present)
                                {
                                    if (this.m_pApp.PyramidPromptType == PyramidPromptType.AlwaysBuildNoPrompt)
                                    {
                                        ((IRasterPyramid)item).Create();
                                    }
                                    else if (this.m_pApp.PyramidPromptType == PyramidPromptType.AlwaysPrompt)
                                    {
                                        DialogResult dialogResult = BuildPyramidSet.Show();
                                        if (dialogResult == DialogResult.Yes)
                                        {
                                            ((IRasterPyramid)item).Create();
                                        }
                                        else if (dialogResult == DialogResult.Cancel)
                                        {
                                            flag7 = false;
                                        }
                                    }
                                }
                                if (flag7)
                                {
                                    IRasterLayer rasterLayerClass = new RasterLayerClass()
                                    {
                                        Cached = true
                                    };
                                    rasterLayerClass.CreateFromDataset((IRasterDataset)item);
                                    rasterLayerClass.Name = item.Name;
                                    bool flag8 = SpatialReferenctOperator.ValideFeatureClass(rasterLayerClass as IGeoDataset);
                                    flag = flag8;
                                    if (!flag8)
                                    {
                                        strs.Add(rasterLayerClass.Name);
                                    }
                                    this.AddLayer(map, rasterLayerClass, ref envelope, flag);
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTTable)
                            {
                                try
                                {
                                    IStandaloneTableCollection standaloneTableCollection = this.GetMap() as IStandaloneTableCollection;
                                    IPropertySet connectionProperties = item.Workspace.ConnectionProperties;
                                    bool flag9 = false;
                                    int num = 0;
                                    while (true)
                                    {
                                        if (num < standaloneTableCollection.StandaloneTableCount)
                                        {
                                            ITable table = standaloneTableCollection.StandaloneTable[num].Table;
                                            if (!connectionProperties.IsEqual((table as IDataset).Workspace.ConnectionProperties) || !((table as IDataset).Name == item.Name))
                                            {
                                                num++;
                                            }
                                            else
                                            {
                                                flag9 = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (!flag9)
                                    {
                                        standaloneTableCollection.AddStandaloneTable(new StandaloneTableClass()
                                        {
                                            Table = item as ITable
                                        });
                                    }
                                }
                                catch (Exception exception)
                                {
                                    CErrorLog.writeErrorLog(this, exception, "");
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
                                        this.AddLayer(map, featureLayerClass, ref envelope, true);
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
                                        this.AddLayer(map, featureLayerClass, ref envelope, true);
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
                                        this.AddLayer(map, featureLayerClass, ref envelope, true);
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
                                        this.AddLayer(map, featureLayerClass, ref envelope, true);
                                    }
                                }
                            }
                            else if (item.Type == esriDatasetType.esriDTNetworkDataset)
                            {
                                INetworkLayer networkLayerClass = new NetworkLayerClass()
                                {
                                    NetworkDataset = item as INetworkDataset
                                };
                                this.AddLayer(map, networkLayerClass as ILayer, ref envelope, true);
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
                                        this.AddLayer(map, cadFeatureLayerClass, ref envelope, true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        IMapServerLayer mapServerLayerClass = new MapServerLayerClass();
                        mapServerLayerClass.ServerConnect((objs[k] as IGxAGSObject).AGSServerObjectName, (objs[k] as IGxAGSObject).DefaultMapName);
                        this.AddLayer(map, mapServerLayerClass as ILayer, ref envelope, false);
                    }
                }
                processAssist.End();
                bool layerCount = this.GetMap().LayerCount > 0;
                if (strs.Count > 0)
                {
                    MessageBox.Show("警告：数据范围不一致。\r\n 一个或多个添加的图层范围与关联的空间坐标系范围不一致。请检查数据问题。存在问题图层信息请查看错误日志!", "管理平台", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string str1 in strs)
                    {
                        if (stringBuilder.Length > 0)
                        {
                            stringBuilder.Append(",");
                        }
                        stringBuilder.Append(str1);
                    }
                    CErrorLog.writeErrorLog(stringBuilder, null, "图层范围不一致!");
                }
                foreach (ILayer list0 in this.list_0)
                {
                    list0.Visible = true;
                }
                if (layerCount && envelope != null)
                {
                    IMap map1 = this.GetMap();
                    ISpatialReference spatialReference = map1.SpatialReference;
                    if ((spatialReference is IUnknownCoordinateSystem || envelope.SpatialReference is IUnknownCoordinateSystem || envelope.SpatialReference == null ? false : spatialReference != null))
                    {
                        envelope.Project(spatialReference);
                    }
                    (map1 as IActiveView).Extent = envelope;
                    (map1 as IActiveView).Refresh();
                }
                if (this.m_pApp != null)
                {
                    this.m_pApp.MapDocumentChanged();
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
            if (this.onLoadCompleteHand_0 != null)
            {
                this.onLoadCompleteHand_0();
            }
        }

        private void method_3()
		{
		}
	}
    public delegate void OnLoadCompleteHand();
}
