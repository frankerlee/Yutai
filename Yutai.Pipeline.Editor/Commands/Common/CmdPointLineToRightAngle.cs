using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.Properties;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Profession
{
    class CmdPointLineToRightAngle : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private IPointSnapper _pointSnapper;
        private IFeatureLayer _pointFeatureLayer;
        private IFeatureLayer _lineFeatureLayer;
        private double _tolerance = 0.01;
        private IFeature _pointFeature;
        private IFeature _lineFeature;
        private IFeature _linkFeature;

        public CmdPointLineToRightAngle(IAppContext context, PipelineEditorPlugin plugin)
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
                _pointFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, pointLayerInfo.AliasName, true) as IFeatureLayer;
            }
            IBasicLayerInfo lineLayerInfo =
                _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
            if (lineLayerInfo != null)
            {
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
            _linkFeature = null;
            _lineFeature = null;
            _pointFeature = null;
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "生成直角（移动外端点）";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_PointLineToRightAngle;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resources.Digitise));
            base.m_name = "PipelineEditor_PointLineToRightAngle";
            base._key = "PipelineEditor_PointLineToRightAngle";
            base.m_toolTip = "生成直角（移动外端点）";
            base.m_checked = false;
            base.m_message = "1、选择一条基准管线；2、选择一个需要旋转的管线的外端点；3、弹出确认窗口。";
            base.m_enabled = true;
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
            IFeature feature = enumFeature.Next();
            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                _pointFeature = feature;
            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                _lineFeature = feature;
            if (_pointFeature == null || _lineFeature == null)
                return;
            this.SelectByShape(_pointFeature.Shape);
            enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
            enumFeature.Reset();
            List<IFeature> linkFeatures = new List<IFeature>();
            IFeature lineFeature;
            while ((lineFeature = enumFeature.Next()) != null)
            {
                IPolyline polyline = lineFeature.Shape as IPolyline;
                if (polyline == null)
                    continue;
                linkFeatures.Add(lineFeature);
            }
            if (linkFeatures.Count <= 0)
                return;
            _linkFeature = linkFeatures[0];
            if (_linkFeature == null || _linkFeature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                return;
            _context.FocusMap.SelectFeature(_pointFeatureLayer, _pointFeature);
            _context.FocusMap.SelectFeature(_lineFeatureLayer, _lineFeature);
            _context.FocusMap.SelectFeature(_lineFeatureLayer, _linkFeature);
            try
            {
                IPoint datumPoint = GeometryHelper.GetIntersectPoint(_lineFeature.Shape as IPolyline,
                    _linkFeature.Shape as IPolyline);
                double angle1 = 0.0, angle2 = 0.0, angle3 = 0.0, angle4 = 0.0;
                ILine line1 = new LineClass();

                line1.PutCoords(datumPoint, GeometryHelper.GetAnotherPoint(_lineFeature.Shape as IPolyline, datumPoint));
                angle1 = line1.Angle;
                ILine line2 = new LineClass();
                line2.PutCoords(datumPoint, _pointFeature.Shape as IPoint);
                angle2 = line2.Angle;

                angle3 = angle2 - angle1;
                if ((angle3 > 0 && angle3 < Math.PI) || angle3 < (0 - Math.PI))
                    angle4 = Math.PI / 2;
                else
                    angle4 = Math.PI * 1.5;
                if (MessageBox.Show($@"要素：{_linkFeature.OID}，将旋转{angle4:0.0000}，是否继续？", @"提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    IConstructPoint constructPoint = new PointClass();
                    constructPoint.ConstructDeflection(line1, line2.Length, angle4);
                    IPoint point = constructPoint as IPoint;

                    CommonHelper.MovePointWithLine(_pointFeature, new List<IFeature>() { _linkFeature }, point, _tolerance);
                    _context.ActiveView.Refresh();
                    _linkFeature = null;
                    _lineFeature = null;
                    _pointFeature = null;
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
                _linkFeature = null;
                _lineFeature = null;
                _pointFeature = null;
            }
        }
    }
}
