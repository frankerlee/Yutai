using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Pipeline.Editor.Views
{
    public partial class IdentifyRoadNameDockPanel : DockPanelControlBase, IIdentifyRoadNameView
    {
        private readonly IAppContext _context;
        private int _idxPolyRoadField;
        private int _idxPipeRoadField;
        private IDictionary<int, IGeometry> _envelopDictionary;

        public IdentifyRoadNameDockPanel(IAppContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public override string Caption
        {
            get { return "道路名称识别"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock => DockPanelState.Bottom;


        public override string DockName => DefaultDockName;

        public override string DefaultNestDockName => "";

        public const string DefaultDockName = "PipelineEditor_IdentifyRoadName";

        public IFeatureLayer RoadLayer => ucFeatureClass_RoadLayer.SelectFeatureLayer;

        public IFeatureLayer PipeLayer => ucFeatureClass_PipeLayer.SelectFeatureLayer;

        public void Initialize(IAppContext context)
        {
            ucExtentSetting.Map = _context.FocusMap;
            ucFeatureClass_RoadLayer.GeometryType = esriGeometryType.esriGeometryPolygon;
            ucFeatureClass_RoadLayer.Map = _context.FocusMap;
            ucFeatureClass_PipeLayer.GeometryTypes = new List<esriGeometryType>()
            {
                esriGeometryType.esriGeometryPoint,
                esriGeometryType.esriGeometryPolyline
            };
            ucFeatureClass_PipeLayer.Map = _context.FocusMap;
        }

        private void ucFeatureClass_RoadLayer_SelectComplateEvent()
        {
            if (RoadLayer == null)
                return;
            ucField_PolyRoadName.FeatureClass = RoadLayer.FeatureClass;
        }

        private void ucFeatureClass_PipeLayer_SelectComplateEvent()
        {
            if (PipeLayer == null)
                return;
            ucField_PipeRoadName.FeatureClass = PipeLayer.FeatureClass;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string msg = Check();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                ArcGIS.Common.Editor.Editor.StartEditOperation();
                foreach (KeyValuePair<int, IGeometry> keyValuePair in _envelopDictionary)
                {
                    
                //}
                //foreach (BoundFeatureInfo boundFeatureInfo in _envelopList)
                //{
                    ISpatialFilter spatialFilter = new SpatialFilterClass();
                    spatialFilter.Geometry = keyValuePair.Value;
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    spatialFilter.OutputSpatialReference[PipeLayer.FeatureClass.ShapeFieldName] =
                        _context.FocusMap.SpatialReference;
                    spatialFilter.GeometryField = PipeLayer.FeatureClass.ShapeFieldName;
                    IFeatureCursor polygonFeatureCursor = RoadLayer.Search(spatialFilter, false);
                    IFeature polygonFeature;
                    while ((polygonFeature = polygonFeatureCursor.NextFeature()) != null)
                    {
                        object objRoadName = polygonFeature.Value[_idxPolyRoadField];
                        if (objRoadName is DBNull || objRoadName == null)
                            continue;
                        spatialFilter.Geometry = polygonFeature.ShapeCopy;
                        IFeatureCursor pipeFeatureCursor = PipeLayer.FeatureClass.Update(spatialFilter, false);
                        IFeature pipeFeature;
                        while ((pipeFeature = pipeFeatureCursor.NextFeature()) != null)
                        {
                            pipeFeature.Value[_idxPipeRoadField] = objRoadName;
                            pipeFeatureCursor.UpdateFeature(pipeFeature);
                            pipeFeature.Store();
                        }
                        Marshal.ReleaseComObject(pipeFeatureCursor);
                    }
                    Marshal.ReleaseComObject(polygonFeatureCursor);

                    ISpatialFilter spatialFilter2 = new SpatialFilterClass();
                    spatialFilter2.Geometry = keyValuePair.Value;
                    spatialFilter2.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    IQueryFilter queryFilter = spatialFilter2 as IQueryFilter;
                    queryFilter.WhereClause = string.Format(" {0} is Null or {0} = '' ", ucField_PipeRoadName.Field.Name);
                    IFeatureCursor featureCursor = PipeLayer.FeatureClass.Update(queryFilter, false);
                    IFeature feature;
                    while ((feature = featureCursor.NextFeature()) != null)
                    {
                        ITopologicalOperator pTopologicalOperator = feature.ShapeCopy as ITopologicalOperator;
                        if (pTopologicalOperator == null)
                            continue;
                        IPolygon polygon = pTopologicalOperator.Buffer((double)numSearchRadius.Value) as IPolygon;
                        List<IFeature> features = MapHelper.GetAllFeaturesFromPolygonInGeoFeatureLayer(polygon,
                            (IGeoFeatureLayer)RoadLayer, _context.ActiveView);
                        if (features.Count <= 0)
                            continue;
                        IFeature pFeature = MapHelper.GetMinDistanceFeature(features, feature.ShapeCopy);
                        if (pFeature == null)
                            continue;
                        object obj = pFeature.Value[_idxPolyRoadField];
                        if (obj is DBNull || string.IsNullOrEmpty(obj.ToString()))
                            continue;
                        feature.Value[_idxPipeRoadField] = obj;
                        featureCursor.UpdateFeature(feature);
                        feature.Store();
                    }
                    Marshal.ReleaseComObject(featureCursor);
                }

                MessageBox.Show("执行完成");
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                ArcGIS.Common.Editor.Editor.StopEditOperation();
            }
        }

        private string Check()
        {
            _envelopDictionary = ucExtentSetting.BoundGeometrys;
            if (_envelopDictionary == null || _envelopDictionary.Count <= 0)
                return "未选择识别范围！";
            _idxPolyRoadField = ucField_PolyRoadName.FieldIndex;
            _idxPipeRoadField = ucField_PipeRoadName.FieldIndex;
            if (_idxPolyRoadField < 0 || _idxPipeRoadField < 0)
                return "未选择道路名称字段";
            if (ucField_PipeRoadName.Field.Length < ucField_PolyRoadName.Field.Length)
                return "字段长度不够，请设置字段长度后执行！";
            return null;
        }
    }
}
