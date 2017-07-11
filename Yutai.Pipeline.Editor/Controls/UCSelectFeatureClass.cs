using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Controls
{
    public delegate void SelectComplateHandler();

    public partial class UcSelectFeatureClass : UserControl
    {
        public event SelectComplateHandler SelectComplateEvent;

        private IMap _map;
        private esriGeometryType _geometryType;
        private List<esriGeometryType> _geometryTypes;
        private IFeatureClass _selectFeatureClass;
        private IFeatureLayer _selectFeatureLayer;
        private List<IFeatureLayer> _featureLayers;
        /// <summary>
        /// 不加载当前地图的图层
        /// </summary>
        public UcSelectFeatureClass()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载当前地图的图层，不过滤图层类型
        /// </summary>
        /// <param name="map">当前地图对象</param>
        public UcSelectFeatureClass(IMap map)
        {
            InitializeComponent();
            this.Map = map;
        }

        /// <summary>
        /// 加载当前地图的图层，过滤图层类型
        /// </summary>
        /// <param name="map">当前地图对象</param>
        /// <param name="type">过滤几何类型</param>
        public UcSelectFeatureClass(IMap map, esriGeometryType type)
        {
            InitializeComponent();
            this.Map = map;
            this.GeometryType = type;
        }

        /// <summary>
        /// 加载当前地图的图层，过滤图层类型
        /// </summary>
        /// <param name="map">当前地图对象</param>
        /// <param name="types">过滤几何类型集合</param>
        public UcSelectFeatureClass(IMap map, List<esriGeometryType> types)
        {
            InitializeComponent();
            this.Map = map;
            this.GeometryTypes = types;
        }

        /// <summary>
        /// 当前地图中过滤出来的要素图层
        /// </summary>
        public List<IFeatureLayer> FeatureLayers
        {
            get { return _featureLayers; }
        }

        /// <summary>
        /// 当前选中的要素类
        /// </summary>
        public IFeatureClass SelectFeatureClass
        {
            get { return _selectFeatureClass; }
            private set
            {
                _selectFeatureClass = value;
                if (_selectFeatureClass != null)
                    OnSelectComplateEvent();
            }
        }

        public IFeatureLayer SelectFeatureLayer
        {
            get { return _selectFeatureLayer; }
            private set
            {
                _selectFeatureLayer = value;
            }
        }

        /// <summary>
        /// 设置地图对象
        /// </summary>
        public IMap Map
        {
            private get { return _map; }
            set
            {
                _map = value;
                LoadFeatureLayer();
            }
        }

        /// <summary>
        /// 设置过滤条件
        /// </summary>
        public esriGeometryType GeometryType
        {
            private get { return _geometryType; }
            set
            {
                _geometryType = value;
                LoadFeatureLayer();
            }
        }

        /// <summary>
        /// 设置过滤条件
        /// </summary>
        public List<esriGeometryType> GeometryTypes
        {
            private get { return _geometryTypes; }
            set
            {
                _geometryTypes = value;
                LoadFeatureLayer();
            }
        }

        /// <summary>
        /// 加载地图图层
        /// </summary>
        private void LoadFeatureLayer()
        {
            if (_map != null)
            {
                if (_geometryTypes != null)
                {
                    _featureLayers = MapHelper.GetFeatureLayers(_map, _geometryTypes);
                }
                else if (_geometryType != esriGeometryType.esriGeometryNull)
                {
                    _featureLayers = MapHelper.GetFeatureLayers(_map, _geometryType);
                }
                else
                {
                    _featureLayers = MapHelper.GetFeatureLayers(_map);
                }
                this.cmbFeatureLayer.DataSource = _featureLayers;
                this.cmbFeatureLayer.DisplayMember = "Name";
                this.cmbFeatureLayer.Text = null;
            }
        }

        [Browsable(true)]
        [Description("是否显示浏览按钮"), Category("扩展"), DefaultValue(true)]
        public bool VisibleOpenButton
        {
            get { return this.btnOpen.Visible; }
            set
            {
                this.btnOpen.Visible = value;
                this.splitContainer1.Panel1Collapsed = !value;
            }
        }

        [Browsable(true)]
        [Description("是否显示文本"), Category("扩展"), DefaultValue(false)]
        public bool VisibleLabel
        {
            get { return !this.splitContainer2.Panel1Collapsed; }
            set { this.splitContainer2.Panel1Collapsed = !value; }
        }

        [Browsable(true)]
        [Description("与控件关联的文本"), Category("扩展"), DefaultValue(null)]
        public string Label
        {
            get { return this.label1.Text; }
            set
            {
                this.label1.Text = value;
                this.splitContainer2.SplitterDistance = this.label1.Width;
            }
        }

        [Browsable(true)]
        [Description("与控件关联的文本宽度"), Category("扩展"), DefaultValue(0)]
        public int LabelWidth
        {
            get { return this.splitContainer2.SplitterDistance; }
            set { this.splitContainer2.SplitterDistance = value; }
        }

        [Browsable(true)]
        [Description("文本的对齐方式"), Category("扩展"), DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment LabelAlign
        {
            get { return this.label1.TextAlign; }
            set { this.label1.TextAlign = value; }
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            SelectFeatureClass = GxDialogHelper.SelectFeatureClassDialog();
            if (SelectFeatureClass != null)
                this.cmbFeatureLayer.Text = SelectFeatureClass.AliasName;
        }

        private void cmbFeatureLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFeatureLayer featureLayer = this.cmbFeatureLayer.SelectedItem as IFeatureLayer;
            if (featureLayer != null)
            {
                SelectFeatureLayer = featureLayer;
                SelectFeatureClass = featureLayer.FeatureClass;
            }
        }

        protected virtual void OnSelectComplateEvent()
        {
            var handler = SelectComplateEvent;
            if (handler != null) handler();
        }
    }
}
