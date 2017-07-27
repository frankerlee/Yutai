using System;
using System.Collections.Generic;
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
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Common
{
    class CmdDeletePoint : YutaiTool
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

        public CmdDeletePoint(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _context.SetCurrentTool(this);
            _pointSnapper = new PointSnapper();
            (_pointSnapper as PointSnapper).Map = _context.FocusMap;
            _tolerance = _context.Config.EngineSnapEnvironment.SnapTolerance;
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "删除管点";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_DeletePoint;
            base.m_name = "PipelineEditor_DeletePoint";
            base._key = "PipelineEditor_DeletePoint";
            base.m_toolTip = "删除管线点，合并相连管线";
            base.m_checked = false;
            base.m_message = "删除管线点，合并相连管线";
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
                if (_plugin.CurrentLayer == null)
                    return false;
                IBasicLayerInfo pointLayerInfo = _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                if (pointLayerInfo != null)
                    _pointFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, pointLayerInfo.AliasName, true) as IFeatureLayer;
                IBasicLayerInfo lineLayerInfo = _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                if (lineLayerInfo != null)
                    _lineFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, lineLayerInfo.AliasName, true) as IFeatureLayer;
                if (_pointFeatureLayer == null || _lineFeatureLayer == null)
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
            frmDeletePipeline frm = new frmDeletePipeline(_lineFeature1, _lineFeature2, linkPoint, _lineFeatureLayer);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _pointFeature.Delete();
                _lineFeature1.Delete();
                _lineFeature2.Delete();
                int oid = frm.FeatureOID;
                IFeature feature = _lineFeatureLayer.FeatureClass.GetFeature(oid);
                _context.FocusMap.ClearSelection();
                _context.FocusMap.SelectFeature(_lineFeatureLayer, feature);
                _context.ActiveView.Refresh();
            }
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
