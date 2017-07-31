using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Forms.Common;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.Properties;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Common
{
    class CmdPipelineStretch : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private IPointSnapper _pointSnapper;
        private IFeatureLayer _pointFeatureLayer;
        private IFeatureLayer _lineFeatureLayer;
        private double _tolerance = 0.01;
        private IFeature _pointFeature;
        private IFeature _lineFeature1;
        private IFeature _lineFeature2;

        public CmdPipelineStretch(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {

            if (_plugin.CurrentLayer == null)
            {
                MessageBox.Show(@"未设置当前编辑图层！");
                _context.ClearCurrentTool();
                return;
            }
            IBasicLayerInfo pointLayerInfo = _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
            if (pointLayerInfo != null)
                _pointFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, pointLayerInfo.AliasName, true) as IFeatureLayer;
            IBasicLayerInfo lineLayerInfo = _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
            if (lineLayerInfo != null)
                _lineFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, lineLayerInfo.AliasName, true) as IFeatureLayer;
            if (_pointFeatureLayer == null || _lineFeatureLayer == null)
            {
                MessageBox.Show(@"点图层或线图层未设置！");
                _context.ClearCurrentTool();
                return;
            }

            _context.SetCurrentTool(this);
            _pointSnapper = new PointSnapper();
            (_pointSnapper as PointSnapper).Map = _context.FocusMap;
            _tolerance = _context.Config.EngineSnapEnvironment.SnapTolerance;
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "管线取直";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_PipelineStretch;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resources.Digitise));
            base.m_name = "PipelineEditor_PipelineStretch";
            base._key = "PipelineEditor_PipelineStretch";
            base.m_toolTip = "管线取直";
            base.m_checked = false;
            base.m_message = "选择相连两根管线段，修改中间点位使两条线段在一根直线上";
            base._itemType = RibbonItemType.Tool;
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap == null)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    return false;
                return true;
            }
        }

        private void SelectByClick(int x, int y)
        {
            IMap map = _context.FocusMap;
            IEnvelope envelope = new Envelope() as IEnvelope;
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            envelope.PutCoords(point.X, point.Y, point.X, point.Y);
            envelope.XMin = (envelope.XMin - _tolerance);
            envelope.YMin = (envelope.YMin - _tolerance);
            envelope.YMax = (envelope.YMax + _tolerance);
            envelope.XMax = (envelope.XMax + _tolerance);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(envelope, selectionEnvironment, true);
            _context.ActiveView.Refresh();
        }

        private void SelectByShape(IGeometry geometry)
        {
            IMap map = _context.FocusMap;
            IEnvelope envelope = geometry.Envelope;
            envelope.XMin = (envelope.XMin - _tolerance);
            envelope.YMin = (envelope.YMin - _tolerance);
            envelope.YMax = (envelope.YMax + _tolerance);
            envelope.XMax = (envelope.XMax + _tolerance);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(envelope, selectionEnvironment, false);
            _context.ActiveView.Refresh();
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button != 1)
                return;

            if (_context.Config.EngineSnapEnvironment.SnapToleranceUnits ==
                esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
            {
                _tolerance = ArcGIS.Common.Helpers.CommonHelper.ConvertPixelsToMapUnits(_context.ActiveView,
                    _tolerance);
            }
            this.SelectByClick(x, y);
            IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
            _pointFeature = enumFeature.Next();
            IPoint linkPoint = _pointFeature?.Shape as IPoint;
            if (linkPoint == null)
                return;
            this.SelectByShape(linkPoint);
            IFeatureSelection featureSelection = _lineFeatureLayer as IFeatureSelection;
            ICursor cursor;
            featureSelection.SelectionSet.Search(null, false, out cursor);

            if ((_lineFeature1 = cursor.NextRow() as IFeature) == null)
                return;
            if ((_lineFeature2 = cursor.NextRow() as IFeature) == null)
                return;
            IPolyline polyline1 = _lineFeature1.Shape as IPolyline;
            IPolyline polyline2 = _lineFeature2.Shape as IPolyline;
            if (polyline1 == null || polyline1.IsEmpty || polyline2 == null || polyline2.IsEmpty)
            {
                MessageBox.Show(@"所选要素几何图形为空！");
                return;
            }

            IPoint point1 = GeometryHelper.GetAnotherPoint(polyline1, linkPoint);
            IPoint point2 = GeometryHelper.GetAnotherPoint(polyline2, linkPoint);
            IPoint verticalPoint = GeometryHelper.GetVerticalPoint(point1, point2, linkPoint);
            double distance = GeometryHelper.GetDistance(linkPoint, verticalPoint);
            _context.FocusMap.ClearSelection();
            _context.FocusMap.SelectFeature(_pointFeatureLayer, _pointFeature);
            _context.FocusMap.SelectFeature(_lineFeatureLayer, _lineFeature1);
            _context.FocusMap.SelectFeature(_lineFeatureLayer, _lineFeature2);
            if (MessageBox.Show($@"要素：{_pointFeature.OID}，将移动{distance:0.0000}米，是否继续？", @"提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<IFeature> features = new List<IFeature>();
                features.Add(_lineFeature1);
                features.Add(_lineFeature2);
                CommonHelper.MovePointWithLine(_pointFeature, features, verticalPoint, _tolerance);
                _context.ActiveView.Refresh();
            }
            else
            {
                _context.FocusMap.ClearSelection();
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
        }

        public override void OnDblClick()
        {
            try
            {
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
