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
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.Properties;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.Pipeline.Editor.Commands.Common
{
    class CmdPointLineLinkage : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private INewLineFeedback _lineFeedback;
        private IPointSnapper _pointSnapper;
        private double _tolerance = 0.01;
        private IFeature _pointFeature;
        private List<IFeature> _lineFeatures;

        public CmdPointLineLinkage(IAppContext context, PipelineEditorPlugin plugin)
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
            base.m_caption = "点线联动";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_PointLineLinkage;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resources.Digitise));
            base.m_name = "PipelineEditor_PointLineLinkage";
            base._key = "PipelineEditor_PointLineLinkage";
            base.m_toolTip = "移动管点的时候管线也跟着动";
            base.m_checked = false;
            base.m_message = "点线联动";
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

            IActiveView activeView = _context.ActiveView;
            if (_lineFeedback == null)
            {
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
                _lineFeatures = new List<IFeature>();
                this.SelectByShape(linkPoint);
                enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                if (enumFeature == null)
                    return;
                enumFeature.Reset();
                IFeature lineFeature;
                while ((lineFeature = enumFeature.Next()) != null)
                {
                    IPolyline polyline = lineFeature.Shape as IPolyline;
                    if (polyline == null)
                        continue;
                    _lineFeatures.Add(lineFeature);
                }
                if (_lineFeatures.Count <= 0)
                    return;

                _lineFeedback = new NewLineFeedbackClass()
                {
                    Display = activeView.ScreenDisplay
                };
                _lineFeedback.Start(linkPoint);
            }
            else
            {
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                ISnappingResult snappingResult = _pointSnapper.Snap(point);
                if (snappingResult != null)
                    point = snappingResult.Location;
                _lineFeedback.AddPoint(point);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            IActiveView activeView = _context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            ISnappingResult snappingResult = _pointSnapper.Snap(point);
            if (snappingResult == null)
            {
                _lineFeedback?.MoveTo(point);
            }
            else
            {
                point = snappingResult.Location;
                _lineFeedback?.MoveTo(point);
            }
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == 27)
            {
                this._lineFeedback = null;
                _context.FocusMap.ClearSelection();
                _context.ActiveView.Refresh();
            }
        }

        public override void OnDblClick()
        {
            try
            {
                if (_lineFeedback == null)
                    return;
                IPolyline polyline = _lineFeedback.Stop();
                _context.ActiveView.Refresh();
                _lineFeedback = null;
                if (polyline == null)
                    return;

                ISnappingResult snappingResult = _pointSnapper.Snap(polyline.ToPoint);
                if (snappingResult != null)
                {
                    IPointCollection pointCollection = polyline as IPointCollection;
                    pointCollection.UpdatePoint(pointCollection.PointCount - 1, snappingResult.Location);
                }
                CommonHelper.MovePointWithLine(_pointFeature, _lineFeatures, polyline.ToPoint, _tolerance);
                //IPoint linkPoint = _pointFeature.Shape as IPoint;
                //linkPoint.PutCoords(polyline.ToPoint.X, polyline.ToPoint.Y);
                //_pointFeature.Shape = linkPoint;
                //_pointFeature.Store();

                //foreach (IFeature lineFeature in _lineFeatures)
                //{
                //    IPolyline linkPolyline = lineFeature.Shape as IPolyline;
                //    if (linkPolyline == null)
                //        continue;
                //    IPointCollection pointCollection = linkPolyline as IPointCollection;
                //    if (CommonHelper.GetDistance(linkPolyline.FromPoint, polyline.FromPoint) < _tolerance)
                //    {
                //        IPoint fromPoint = pointCollection.Point[0];
                //        fromPoint.PutCoords(polyline.ToPoint.X, polyline.ToPoint.Y);
                //        pointCollection.UpdatePoint(0, fromPoint);
                //    }
                //    else if (CommonHelper.GetDistance(linkPolyline.ToPoint, polyline.FromPoint) < _tolerance)
                //    {
                //        IPoint toPoint = pointCollection.Point[pointCollection.PointCount - 1];
                //        toPoint.PutCoords(polyline.ToPoint.X, polyline.ToPoint.Y);
                //        pointCollection.UpdatePoint(pointCollection.PointCount - 1, toPoint);
                //    }
                //    lineFeature.Shape = pointCollection as IPolyline;
                //    lineFeature.Store();
                //}
                _lineFeedback = null;
                _pointFeature = null;
                _lineFeatures = null;
                _context.ActiveView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
