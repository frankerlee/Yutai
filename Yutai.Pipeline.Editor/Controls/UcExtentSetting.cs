using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcExtentSetting : UserControl
    {
        private IMap _map;

        public UcExtentSetting()
        {
            InitializeComponent();
            this.Enabled = false;
        }

        public IMap Map
        {
            set
            {
                _map = value;
                if (_map == null)
                    this.Enabled = false;
                else
                    this.Enabled = true;
            }
        }

        public IDictionary<int, IGeometry> BoundGeometrys
        {
            get
            {
                IDictionary<int, IGeometry> boundGeometrys = new Dictionary<int, IGeometry>();

                IActiveView activeView = _map as IActiveView;
                if (activeView == null)
                    return boundGeometrys;
                int type = (int)radioGroupExtentType.EditValue;
                switch (type)
                {
                    case 0: // 全部范围
                        {
                            boundGeometrys.Add(0, activeView.FullExtent);
                        }
                        break;
                    case 1: // 当前视图
                        {
                            boundGeometrys.Add(0, activeView.Extent);
                        }
                        break;
                    case 2: // 依据索引
                        {
                            for (int i = 0; i < checkedListBoxIndexes.Items.Count; i++)
                            {
                                if (checkedListBoxIndexes.GetItemChecked(i))
                                {
                                    int oid = (int)checkedListBoxIndexes.Items[i];
                                    if (oid >= 0)
                                    {
                                        IFeature feature = IndexLayer.FeatureClass.GetFeature(oid);
                                        if (feature != null && !feature.Shape.IsEmpty)
                                        {
                                            boundGeometrys.Add(oid, feature.ShapeCopy.Envelope);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                return boundGeometrys;
            }
        }

        private IFeatureLayer IndexLayer
        {
            get { return ucSelectFeatureClass.SelectFeatureLayer; }
        }

        private void radioGroupExtentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int editValue = (int)radioGroupExtentType.EditValue;
            if (editValue == 2)
            {
                groupBoxIndexLayer.Enabled = true;
                LoadIndexLayers();
            }
            else
                groupBoxIndexLayer.Enabled = false;
        }

        private void LoadIndexLayers()
        {
            if (_map == null)
                return;
            ucSelectFeatureClass.GeometryType = esriGeometryType.esriGeometryPolygon;
            ucSelectFeatureClass.Map = _map;
        }

        private void buttonByView_Click(object sender, EventArgs e)
        {
            if (checkedListBoxIndexes.Items.Count > 0)
                checkedListBoxIndexes.Items.Clear();
            if (IndexLayer == null)
            {
                MessageBox.Show(@"请选择索引图层！");
                return;
            }
            IActiveView activeView = _map as IActiveView;
            if (activeView == null)
                return;
            IEnvelope envelope = activeView.Extent;
            List<IFeature> features = MapHelper.GetFeaturesByEnvelope(envelope, IndexLayer);
            for (int i = 0; i < features.Count; i++)
            {
                IFeature feature = features[i];
                checkedListBoxIndexes.Items.Add(feature.OID);
            }
        }

        private void buttonBySelection_Click(object sender, EventArgs e)
        {
            if (checkedListBoxIndexes.Items.Count > 0)
                checkedListBoxIndexes.Items.Clear();
            if (IndexLayer == null)
            {
                MessageBox.Show(@"请选择索引图层！");
                return;
            }
            List<IFeature> features = MapHelper.GetFeaturesBySelected(IndexLayer);
            for (int i = 0; i < features.Count; i++)
            {
                IFeature feature = features[i];
                checkedListBoxIndexes.Items.Add(feature.OID);
            }
        }

        private void buttonByAll_Click(object sender, EventArgs e)
        {
            if (checkedListBoxIndexes.Items.Count > 0)
                checkedListBoxIndexes.Items.Clear();
            if (IndexLayer == null)
            {
                MessageBox.Show(@"请选择索引图层！");
                return;
            }
            IActiveView activeView = _map as IActiveView;
            if (activeView == null)
                return;
            IEnvelope envelope = activeView.FullExtent;
            List<IFeature> features = MapHelper.GetFeaturesByEnvelope(envelope, IndexLayer);
            for (int i = 0; i < features.Count; i++)
            {
                IFeature feature = features[i];
                checkedListBoxIndexes.Items.Add(feature.OID);
            }
        }

        private void buttonRefersh_Click(object sender, EventArgs e)
        {
            LoadIndexLayers();
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxIndexes.Items.Count; i++)
            {
                checkedListBoxIndexes.SetItemChecked(i, true);
            }
        }

        private void buttonSelectNull_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxIndexes.Items.Count; i++)
            {
                checkedListBoxIndexes.SetItemChecked(i, false);
            }
        }

    }
}
