using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcSelectFeatureClasses : UserControl
    {
        private IMap _map;
        private List<IFeatureLayer> _featureLayers;
        private esriGeometryType _geometryType;
        private List<esriGeometryType> _geometryTypes;

        public UcSelectFeatureClasses()
        {
            InitializeComponent();
        }

        public IMap Map
        {
            get { return _map; }
            set
            {
                _map = value;
                LoadLayers();
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
                LoadLayers();
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
                LoadLayers();
            }
        }

        public List<IFeatureLayer> SelectedFeatureLayerList
        {
            get
            {
                List<IFeatureLayer> list = new List<IFeatureLayer>();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string layerName = checkedListBox1.Items[i].ToString();
                        list.Add(_featureLayers.FirstOrDefault(c => c.Name == layerName));
                    }
                }
                return list;
            }
            set
            {
                if (value == null)
                    value = new List<IFeatureLayer>();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    string layerName = checkedListBox1.Items[i].ToString();
                    if (value.FirstOrDefault(c => c.Name == layerName) != null)
                        checkedListBox1.SetItemChecked(i, true);
                    else
                        checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void LoadLayers()
        {
            checkedListBox1.Items.Clear();
            if (_map == null)
                return;
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
            for (int i = 0; i < _featureLayers.Count; i++)
            {
                IFeatureLayer featureLayer = _featureLayers[i];
                checkedListBox1.Items.Add(featureLayer.Name);
            }
        }

        public void SelectAll()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        public void SelectClear()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
    }
}
