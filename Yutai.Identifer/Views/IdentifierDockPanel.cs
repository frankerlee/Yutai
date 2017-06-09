using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Api.Enums;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;
using Yutai.UI.Helpers;

namespace Yutai.Plugins.Identifer.Views
{
    public partial class IdentifierDockPanel : DockPanelControlBase, IIdentifierView
    {
        private readonly IAppContext _context;
        private const string TopMostLayer = "<最顶图层>";
        private const string SelectableLayers = "<可选图层>";
        private const string VisibleLayers = "<可见图层>";
        private const string AllLayers = "<所有图层>";
        public event Action ModeChanged;
        public event Action ItemSelected;
        List<LayerFilterProperties> layerFilterSet;
        private List<LayerIdentifiedResult> identifiedResultsList;
        private IActiveViewEvents_Event activeViewEvents;
        private IMapControlEvents2_Event mapEvent;
        private IEnvelope _envelope;
        private bool _hasLinked = false;

        public IdentifierDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            InitializeComponent();
            _context = context;
            TabPosition = 3;
            layerFilterSet = new List<LayerFilterProperties>();
            identifiedResultsList = new List<LayerIdentifiedResult>();
            InitializeLayerFilters(null);
            //初始化属性显示窗口数据
            InitializeAttributesList();
            mapEvent = _context.MapControl as IMapControlEvents2_Event;
            mapEvent.OnMapReplaced += MapEventOnOnMapReplaced;
            //InitializeActiveViewEvents();
            btnZoom.Tag = 0;
            InitModeCombo();
        }
        private void InitModeCombo()
        {
           
            _cboIdentifierMode.SelectedIndexChanged += (s, e) =>
            {
                if (_cboIdentifierMode.SelectedIndex < 0) return;
                if (_cboIdentifierMode.SelectedIndex < 4)
                {
                    AppConfig.Instance.IdentifierMode = (IdentifierMode) _cboIdentifierMode.SelectedIndex;
                }
                else
                {
                    AppConfig.Instance.IdentifierMode =IdentifierMode.CurrentLayer;
                }
                FireModeChanged();
            };
        }

        private void FireModeChanged()
        {
            var handler = ModeChanged;
            if (handler != null)
            {
                handler();
            }
        }
        private void MapEventOnOnMapReplaced(object newMap)
        {
            // InitializeActiveViewEvents();
            //加载过滤器列表
            InitializeLayerFilters(null);
        }


        public void InitializeActiveViewEvents()
        {
            try
            {
                _hasLinked = true;
                activeViewEvents = _context.MapControl.ActiveView as IActiveViewEvents_Event;
                activeViewEvents.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(InitializeLayerFilters);
                activeViewEvents.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(InitializeLayerFilters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActiveViewEventsOnItemAdded(object item)
        {
            InitializeLayerFilters(item);
        }

        public void ClearActiveViewEvents()
        {
            activeViewEvents.ItemAdded -= new IActiveViewEvents_ItemAddedEventHandler(InitializeLayerFilters);
            activeViewEvents.ItemDeleted -= new IActiveViewEvents_ItemDeletedEventHandler(InitializeLayerFilters);
            mapEvent.OnMapReplaced -= MapEventOnOnMapReplaced;
            //清空列表
            layerFilterSet = null;
            identifiedResultsList = null;
            trvFeatures.Nodes.Clear();
            _hasLinked = false;
        }

        private void InitializeAttributesList()
        {
            //清空数据列表
            lstAttribute.Items.Clear();
            lstAttribute.Columns.Clear();
            //添加列头
            ColumnHeader noResultsInfo = new ColumnHeader();
            noResultsInfo.Text = "没有选中要查询的要素。";
            noResultsInfo.Width = 200;
            lstAttribute.Columns.Add(noResultsInfo);
        }

        private void InitializeLayerFilters(object item)
        {
            //清空列表先
            layerFilterSet.Clear();

            //初始化默认图层过滤器
            InitializeBasicLayerFilters();

            //加载地图中的默认图层
            DisplayLayersFromMapControl();

            //将过滤器添加到下拉框
            DisplayLayerFilters();
        }

        private void DisplayLayerFilters()
        {
            //保存清空前的选中状态
            int selectedIndex = _cboIdentifierMode.SelectedIndex;
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            //清空先
            _cboIdentifierMode.Items.Clear();


            int filterCount = layerFilterSet.Count;
            //加载所有图层过滤条件
            for (int i = 0; i < filterCount; i++)
            {
                LayerFilterProperties filterItem = layerFilterSet[i];
                _cboIdentifierMode.Items.Add(filterItem.LayerFilterName);
            }
            //设定默认过滤条件
            _cboIdentifierMode.SelectedIndex = selectedIndex;
        }

        private void InitializeBasicLayerFilters()
        {
            //添加默认过滤器
            //最顶图层
            LayerFilterProperties topMostLayerProperty = new LayerFilterProperties();
            //topMostLayerProperty.HeaderImage = null;
            topMostLayerProperty.LayerCategory = string.Empty;
            topMostLayerProperty.LayerFeatureType = LayerFeatureType.None;
            topMostLayerProperty.LayerFilterName = TopMostLayer;
            topMostLayerProperty.TargetLayer = null;

            //可见图层
            LayerFilterProperties visibleLayerProperty = new LayerFilterProperties();
            //visibleLayerProperty.HeaderImage = null;
            visibleLayerProperty.LayerCategory = string.Empty;
            visibleLayerProperty.LayerFeatureType = LayerFeatureType.None;
            visibleLayerProperty.LayerFilterName = VisibleLayers;
            visibleLayerProperty.TargetLayer = null;
            //visibleLayerProperty.MapWindow = associateMapWindow;
            //可选图层
            LayerFilterProperties selectableLayerProperty = new LayerFilterProperties();
            //selectableLayerProperty.HeaderImage = null;
            selectableLayerProperty.LayerCategory = string.Empty;
            selectableLayerProperty.LayerFeatureType = LayerFeatureType.None;
            selectableLayerProperty.LayerFilterName = SelectableLayers;
            selectableLayerProperty.TargetLayer = null;
            //selectableLayerProperty.MapWindow = associateMapWindow;
            //所有图层
            LayerFilterProperties allLayerProperty = new LayerFilterProperties();
            //allLayerProperty.HeaderImage = null;
            allLayerProperty.LayerCategory = string.Empty;
            allLayerProperty.LayerFeatureType = LayerFeatureType.None;
            allLayerProperty.LayerFilterName = AllLayers;
            allLayerProperty.TargetLayer = null;
            //allLayerProperty.MapWindow = associateMapWindow;

            //保存图层引用
            layerFilterSet.Add(topMostLayerProperty);
            layerFilterSet.Add(visibleLayerProperty);
            layerFilterSet.Add(selectableLayerProperty);
            layerFilterSet.Add(allLayerProperty);
        }

        private void DisplayLayersFromMapControl()
        {
            IMap map = _context.MapControl.Map;
            if (map.LayerCount < 1) return;
            //获取指定类型图层
            UID uid = new UID();
            //表示搜索的是IDataLayer
            uid.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
            //布尔参数表示要搜索GroupLayer中的图层
            IEnumLayer layers = map.get_Layers(uid, true);
            layers.Reset();
            ILayer layer = layers.Next();
            while (layer != null)
            {
                LayerFilterProperties layerProperty = new LayerFilterProperties();
                //layerProperty.HeaderImage = null;
                layerProperty.LayerCategory = string.Empty;
                layerProperty.LayerFilterName = layer.Name;
                layerProperty.LayerFeatureType = GetLayerFeatureType(layer);
                layerProperty.TargetLayer = layer;
                //保存引用
                layerFilterSet.Add(layerProperty);
                layer = layers.Next();
            }
            //释放Com对象
            System.Runtime.InteropServices.Marshal.ReleaseComObject(layers);
        }

        private LayerFeatureType GetLayerFeatureType(ILayer layer)
        {
            LayerFeatureType featureType = LayerFeatureType.None;
            if (layer is IFeatureLayer)
            {
                IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;
                switch (featureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        featureType = LayerFeatureType.Point;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        featureType = LayerFeatureType.Polyline;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        featureType = LayerFeatureType.Polygon;
                        break;
                }
            }
            else if (layer is IRasterLayer)
            {
                featureType = LayerFeatureType.Raster;
            }
            else if (layer is IGroupLayer)
            {
                featureType = LayerFeatureType.GroupLayer;
            }
            return featureType;
        }

     
       

        public void UpdateView()
        {
        }

        private List<LayerFilterProperties> SearchIdentifyLayers()
        {
            List<LayerFilterProperties> IdentifyLayers = null;

            switch (AppConfig.Instance.IdentifierMode)
            {
                case IdentifierMode.AllLayer:
                    IdentifyLayers = getAllLayers;
                    break;
                case IdentifierMode.SelectableLayer:
                    IdentifyLayers = getSelectableLayers;
                    break;
                case IdentifierMode.VisibleLayer:
                    IdentifyLayers = getVisibleLayers;
                    break;
                case IdentifierMode.TopLayer:
                    LayerFilterProperties layerProp = getTopmostLayer;
                    if (layerProp != null)
                    {
                        IdentifyLayers = new List<LayerFilterProperties>();
                        IdentifyLayers.Add(getTopmostLayer);
                    }
                    break;
                default:
                    IdentifyLayers = new List<LayerFilterProperties>();
                    IdentifyLayers.Add(getTargetLayer);
                    break;
            }
            return IdentifyLayers;
        }

        private List<LayerFilterProperties> getSelectableLayers
        {
            get
            {
                List<LayerFilterProperties> layers = new List<LayerFilterProperties>();
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if (esriLayer is IFeatureLayer)
                    {
                        if ((esriLayer as IFeatureLayer).Selectable)
                        {
                            layers.Add(layerProp);
                        }
                    }
                }
                return layers;
            }
        }

        private LayerFilterProperties getTopmostLayer
        {
            //必须保证index为0的图层不是GroupLayer或CompositeLayer
            get
            {
                LayerFilterProperties layer = null;
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if (!(esriLayer is IGroupLayer) &&
                        !(esriLayer is ICompositeLayer))
                    {
                        layer = layerProp;
                        break;
                    }
                }
                return layer;
            }
        }

        private List<LayerFilterProperties> getVisibleLayers
        {
            get
            {
                List<LayerFilterProperties> layers = new List<LayerFilterProperties>();
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if ((esriLayer is IGroupLayer) ||
                        (esriLayer is ICompositeLayer))
                    {
                        continue;
                    }
                    if (esriLayer.Visible)
                    {
                        layers.Add(layerProp);
                    }
                }
                return layers;
            }
        }

        private List<LayerFilterProperties> getAllLayers
        {
            get
            {
                List<LayerFilterProperties> layers = new List<LayerFilterProperties>();
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if ((esriLayer is IGroupLayer) ||
                        (esriLayer is ICompositeLayer))
                    {
                        continue;
                    }
                    layers.Add(layerProp);
                }
                return layers;
            }
        }

        private LayerFilterProperties getTargetLayer
        {
            get
            {
                int selectedIndex = _cboIdentifierMode.SelectedIndex;
                return layerFilterSet[selectedIndex];
            }
        }

        public void Identify(IEnvelope envelope)
        {
            if (_hasLinked == false) return;
            //if (_hasLinked == false)
            //{
            //    InitializeActiveViewEvents();
            //    _hasLinked = true;
            //}
            List<LayerFilterProperties> searchLayers = SearchIdentifyLayers();
            if (envelope == null)
            {
                envelope = _envelope;
            }
            if (envelope == null) return;
            ExecuteIdentify(searchLayers, envelope);
            _envelope = envelope;
            DisplayIdentifyResults();
        }

        private void DisplayIdentifyResults()
        {
            //清空以前所有显示结果
            trvFeatures.Nodes.Clear();
            //初始化属性显示窗口数据
            lstAttribute.Items.Clear();
            lstAttribute.Columns.Clear();
            //声明变量
            List<IFeatureIdentifyObj> IdentifyObjs = null;
            IFeatureIdentifyObj IdentifyObj = null;
            //列出查询结果
            int resultsCount = identifiedResultsList.Count;
            for (int i = 0; i < resultsCount; i++)
            {
                LayerIdentifiedResult layerIdentifiedResult = identifiedResultsList[i];
                //添加图层节点
                TreeNode layerNode = trvFeatures.Nodes.Add(layerIdentifiedResult.IdentifyLayer.Name);
                //添加要素节点
                IdentifyObjs = layerIdentifiedResult.IdentifiedFeatureObjList;
                for (int k = 0; k < IdentifyObjs.Count; k++)
                {
                    IdentifyObj = IdentifyObjs[k];
                    IRowIdentifyObject rowIdentifyObj = IdentifyObj as IRowIdentifyObject;
                    IFeature identifyFeature = rowIdentifyObj.Row as IFeature;
                    layerNode.Nodes.Add(identifyFeature.OID.ToString());
                }
            }
            //展开所有节点
            trvFeatures.ExpandAll();
            //默认显示第一图层的第一个查询要素
            if (trvFeatures.Nodes.Count < 1)
            {
                ShowClickedNodeInfo(null, false);
            }
            //显示第一图层的第一个查询要素
            else
            {
                TreeNode topNode = trvFeatures.Nodes[0].Nodes[0];
                //显示第一节点
                trvFeatures.TopNode = topNode.Parent;
                ShowClickedNodeInfo(topNode, false);
            }
        }

        private void ShowClickedNodeInfo(TreeNode nodeClicked, bool doFlash)
        {
            if (nodeClicked == null)
            {
                //初始化树型显示窗口
                InitializeAttributesList();
                return;
            }
            //获取点击点对应的要素
            int nodeLevel = nodeClicked.Level;
            //获取点击的是图层还是图层下的要素
            //若featureIndex < 0则表示点击的是图层,闪烁图层下选中的所有要素
            //反之，则表示点击的是图层下的要素，获取图层索引和要素索引，
            //用于在结果列表中安索引获取要素或要素集属性
            int layerIndex = -1;
            int featureIndex = -1;
            if (nodeLevel > 0)
            {
                TreeNode parentNode = nodeClicked.Parent;
                layerIndex = parentNode.Index;
                featureIndex = nodeClicked.Index;
            }
            else
            {
                layerIndex = nodeClicked.Index;
            }
            //获取对应要素
            LayerIdentifiedResult layerResult = identifiedResultsList[layerIndex];
            //点击了图层下的要素
            if (featureIndex > -1)
            {
                IFeatureIdentifyObj identifyObjDefault = layerResult.IdentifiedFeatureObjList[featureIndex];
                IFeature featureDefault = (identifyObjDefault as IRowIdentifyObject).Row as IFeature;
                //显示属性
                ShowFeatureAttributes(featureDefault);
                DisplayCoordinates(featureDefault.Shape);
                if (ZoomToShape)
                {
                    EsriUtils.ZoomToGeometry(featureDefault.Shape, _context.MapControl.Map, 2);
                    if (doFlash)
                    {
                        FlashUtility.FlashGeometry(featureDefault.Shape, _context.MapControl);
                    }
                }
            }
            //点击了图层，同时闪烁图层下的所有要素图形
            else
            {
                //flashObjects.FlashObjects(layerResult);
            }
        }

        private void ShowFeatureAttributes(IFeature identifiedFeature)
        {
            if (identifiedFeature == null)
            {
                //初始化属性列表
                InitializeAttributesList();
                return;
            }
            //清空数据列表
            lstAttribute.Items.Clear();
            lstAttribute.Columns.Clear();
            //若查询数据不为空则显示数据
            //添加列头
            ColumnHeader fieldHeader = new ColumnHeader();
            fieldHeader.Text = "字段名";
            fieldHeader.Width = 85;
            ColumnHeader valueHeader = new ColumnHeader();
            valueHeader.Text = "属性值";
            valueHeader.Width = lstAttribute.Width - fieldHeader.Width - 25;
            lstAttribute.Columns.AddRange(new ColumnHeader[] {fieldHeader, valueHeader});
            //添加值对
            IFields fields = identifiedFeature.Fields;
            //几何图形
            IGeometry shape = identifiedFeature.Shape;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                ListViewItem lvi = new ListViewItem(field.AliasName);
                string fieldValue = string.Empty;
                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    fieldValue = shape.GeometryType.ToString().Substring(12);
                }
                else
                {
                    fieldValue = identifiedFeature.get_Value(i).ToString();
                }
                lvi.SubItems.Add(fieldValue);
                lstAttribute.Items.Add(lvi);
            }
        }

        private string MapUnitChinese(esriUnits mapUnit)
        {
            string mapUnitChinese = "未知单位";
            switch (mapUnit)
            {
                case esriUnits.esriCentimeters:
                    mapUnitChinese = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    mapUnitChinese = "分米";
                    break;
                case esriUnits.esriDecimeters:
                    mapUnitChinese = "";
                    break;
                //case esriUnits.esriFeet:
                //    mapUnitChinese = "";
                //    break;
                //case esriUnits.esriInches:
                //    mapUnitChinese = "";
                //    break;
                case esriUnits.esriKilometers:
                    mapUnitChinese = "千米";
                    break;
                case esriUnits.esriMeters:
                    mapUnitChinese = "米";
                    break;
                case esriUnits.esriMiles:
                    mapUnitChinese = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    mapUnitChinese = "毫米";
                    break;
                //case esriUnits.esriYards:
                //    mapUnitChinese = "";
                //    break;
            }
            return mapUnitChinese;
        }

        private void DisplayCoordinates(IGeometry geometry)
        {
            double x, y;
            GeometryHelper.QueryGeometryLocation(geometry,out x,out y);
            DisplayCoordinates(x, y);
        }
        private void DisplayCoordinates(double mapX, double mapY)
        {
            if (IsDisposed) return;
            //鼠标在按下移动时显示当时坐标
            this.txtCoords.Text = string.Format("{0}, {1}",
                                      mapX.ToString("########.##"), mapY.ToString("########.##")) + " " +
                                  MapUnitChinese(_context.MapControl.MapUnits);
        }

        private double ConvertPixelsToMapUnits(IActiveView pActiveView, double pixelUnits)
        {
            // Uses the ratio of the size of the map in pixels to map units to do the conversion
            IPoint p1 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperLeft;
            IPoint p2 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperRight;
            int x1, x2, y1, y2;
            pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p1, out x1, out y1);
            pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p2, out x2, out y2);
            double pixelExtent = x2 - x1;
            double realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            double sizeOfOnePixel = realWorldDisplayExtent/pixelExtent;
            return pixelUnits*sizeOfOnePixel;
        }

        public IEnvelope GetIdentifyEnvelope(IEnvelope inGeom)
        {
            double dist = ConvertPixelsToMapUnits(_context.MapControl.ActiveView, _context.Config.IdentifierToPixel);
            if (inGeom.Width == 0 || inGeom.Height == 0)
            {
                inGeom.XMin = inGeom.XMin - dist;
                inGeom.YMin = inGeom.YMin - dist;
                inGeom.XMax = inGeom.XMax + dist;
                inGeom.YMax = inGeom.YMax + dist;
            }
            return inGeom;
        }

        private void ExecuteIdentify(List<LayerFilterProperties> searchLayers, IGeometry identifyGeom)
        {
            object Missing = Type.Missing;
            IEnvelope searchEnvelope = GetIdentifyEnvelope(identifyGeom.Envelope);
            identifiedResultsList.Clear();

            int identifiedObjCount = 0;
            //获取用于查询的图层的数量
            int searchLayersCount = searchLayers.Count;
            //初始化进度条
            //IdentifyProgress.Visible = true;
            //IdentifyProgress.Maximum = searchLayersCount;
            ////初始化闪烁对象
            //flashObjects.MapControl = associateMapControl.Object as IMapControl2;
            //flashObjects.Init();
            //遍历所有图层
            for (int i = 0; i < searchLayersCount; i++)
            {
                LayerFilterProperties filterProps = searchLayers[i];
                ILayer layer = filterProps.TargetLayer;
                //新建查询结果列表对象
                LayerIdentifiedResult layerIdentifiedResult = new LayerIdentifiedResult();
                //先保存查询图层对象
                layerIdentifiedResult.IdentifyLayer = layer;
                layerIdentifiedResult.GeometryType = filterProps.LayerFeatureType;
                //首先获得查询结果列表对象，以备后面往里添加结果使用
                List<IFeatureIdentifyObj> identifiedObjList = layerIdentifiedResult.IdentifiedFeatureObjList;
                //执行查询，返回查询结果
                IArray identifyResult = Identify(layer, searchEnvelope);
                //处理异常情况
                if (identifyResult != null)
                {
                    //依次获取每一个查询结果对象
                    for (int k = 0; k < identifyResult.Count; k++)
                    {
                        identifiedObjCount++;
                        IFeatureIdentifyObj identifiedFeatureObj = identifyResult.get_Element(k) as IFeatureIdentifyObj;
                        //闪烁要素
                        IFeature identifiedFeature = (identifiedFeatureObj as IRowIdentifyObject).Row as IFeature;
                        //添加闪烁图形
                        //flashObjects.AddGeometry(identifiedFeature.Shape);
                        //保存查询结果
                        identifiedObjList.Add(identifiedFeatureObj);
                    }
                    identifiedResultsList.Add(layerIdentifiedResult);
                }
                //显示查询进度
                //IdentifyProgress.Value = i + 1;
                Application.DoEvents();
            }
            //隐藏进度条
            // IdentifyProgress.Visible = false;
            //显示查询到的要素数量
            // lblFeatureCount.Text = "查询到 " + identifiedObjCount + " 条记录";
        }

        private IArray Identify(ILayer identifyLayer, IGeometry identifyGeom)
        {
            //保存结果的变量
            IArray identifyObjs = null;
            //设置查询形状
            if (identifyGeom == null)
            {
                //返回空值
                return identifyObjs;
            }
            //若是点的话做点的缓冲区
            IGeometry hitGeometry = identifyGeom;
            //判断图层类型,设置点击要素
            if (hitGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                ITopologicalOperator topoOp = identifyGeom as ITopologicalOperator;
                hitGeometry = topoOp.Buffer(1);
            }
            ///判断图层的类型并作相应处理
            ///图层是要素图层
            if (identifyLayer is IFeatureLayer)
            {
                //获取要素图层
                IFeatureLayer featureLayer = identifyLayer as IFeatureLayer;
                //开始获取信息操作
                IIdentify2 identify2 = featureLayer as IIdentify2;
                //获取查询结果
                identifyObjs = identify2.Identify(hitGeometry, null);
            }
            ///图层是栅格数据层
            else if (identifyLayer is IRasterLayer)
            {
            }
            //返回获取要素的集合
            return identifyObjs;
        }


        public IdentifierMode Mode
        {
            get { return (IdentifierMode) _cboIdentifierMode.SelectedValue; }
        }

        public bool ZoomToShape
        {
            get { return btnZoom.Checked; }
        }

        public void Clear()
        {
            trvFeatures.Nodes.Clear();
        }


        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield return btnClear; }
        }

        private void lstAttribute_Resize(object sender, EventArgs e)
        {
            if (lstAttribute.Columns.Count < 2) return;
            ColumnHeader field = lstAttribute.Columns[0];
            ColumnHeader value = lstAttribute.Columns[1];
            //设置字段值列宽度
            value.Width = lstAttribute.Width - field.Width - 25;
        }

        private void trvFeatures_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            //显示节点的信息
            ShowClickedNodeInfo(e.Node, true);
        }

        private void FireItemSelected()
        {
            var handler = ItemSelected;
            if (handler != null)
            {
                handler();
            }
        }

        private void _cboIdentifierMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cboIdentifierMode.SelectedIndex < 4)
            {
                _context.Config.IdentifierMode = (IdentifierMode) _cboIdentifierMode.SelectedIndex;
            }
            else
            {
                _context.Config.IdentifierMode= IdentifierMode.CurrentLayer;
            }
            Identify(_envelope);
        }

        public override Bitmap Image { get { return Properties.Resources.icon_information; } }
        public override string Caption
        {
            get { return "信息查看"; }
            set { Caption = value; }
        }
        public override DockPanelState DefaultDock { get { return DockPanelState.Right; } }
        public override string DockName { get { return DefaultDockName; } }
        public virtual string DefaultNestDockName { get { return ""; } }
        public const string DefaultDockName = "Plug_Identifer_Result";
    }
}