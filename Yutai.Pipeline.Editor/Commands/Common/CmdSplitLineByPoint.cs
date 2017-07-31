using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Forms.Common;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.Properties;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Common
{
    class CmdSplitLineByPoint : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private IPointSnapper _pointSnapper;
        private IFeatureLayer _pointFeatureLayer;
        private IFeatureLayer _lineFeatureLayer;
        private double _tolerance = 0.01;
        private IFeature _pointFeature;
        private IFeature _lineFeature;
        private string _gdbhFieldName;
        private string _qdbhFieldName;
        private string _zdbhFieldName;

        public CmdSplitLineByPoint(IAppContext context, PipelineEditorPlugin plugin)
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
            IBasicLayerInfo pointLayerInfo =
                _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
            if (pointLayerInfo != null)
            {
                _gdbhFieldName = pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.GDBH);
                _pointFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, pointLayerInfo.AliasName, true) as IFeatureLayer;
            }
            IBasicLayerInfo lineLayerInfo =
                _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
            if (lineLayerInfo != null)
            {
                _qdbhFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDBH);
                _zdbhFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDBH);
                _lineFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, lineLayerInfo.AliasName, true) as IFeatureLayer;
            }
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
            base.m_caption = "已知点打断线";
            base.m_category = "PipelineEditor";
            this.m_bitmap = Resources.icon_SplitLineByPoint;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resources.Digitise));
            base.m_name = "PipelineEditor_SplitLineByPoint";
            base._key = "PipelineEditor_SplitLineByPoint";
            base.m_toolTip = "已知点打断线";
            base.m_checked = false;
            base.m_message = "点在管线边上，但实际管线应该穿过该点，选择线和已知点，计算垂点到已知点距离，询问是否打断，如打断，将线打段并将打段点位置移到选择点位，条件是点到线的垂点必须在线内。";
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
            IFeature feature = enumFeature.Next();
            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                _pointFeature = feature;
            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                _lineFeature = feature;
            if (_pointFeature == null || _lineFeature == null)
                return;
            _context.FocusMap.SelectFeature(_pointFeatureLayer, _pointFeature);
            _context.FocusMap.SelectFeature(_lineFeatureLayer, _lineFeature);
            try
            {
                double distance = GeometryHelper.GetDistance(_lineFeature, _pointFeature);

                if (MessageBox.Show($@"要素：{_pointFeature.OID}，距离线{distance:0.0000}米，是否继续？", @"提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    IFeature secondFeature = CommonHelper.CutOffPolylineByPoint(_lineFeatureLayer.FeatureClass, _lineFeature, _pointFeature, _gdbhFieldName, _qdbhFieldName, _zdbhFieldName);
                    _context.FocusMap.SelectFeature(_pointFeatureLayer, _pointFeature);
                    _context.FocusMap.SelectFeature(_lineFeatureLayer, _lineFeature);
                    _context.FocusMap.SelectFeature(_lineFeatureLayer, secondFeature);
                    _context.ActiveView.Refresh();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == 27)
            {
                _context.FocusMap.ClearSelection();
                _context.ActiveView.Refresh();
                _pointFeature = null;
                _lineFeature = null;
            }
        }
    }
}
