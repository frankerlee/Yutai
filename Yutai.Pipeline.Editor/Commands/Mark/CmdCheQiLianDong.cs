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
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Commands;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    class CmdCheQiLianDong : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private ICheQiConfig _cheQiConfig;
        private IGeoFeatureLayer _geoFeatureLayer;
        private IPointSnapper _pointSnapper;
        private INewLineFeedback _lineFeedback;
        private double _tolerance = 0.01;
        private IFeature _annoFeature;
        private IFeature _lineFeature;

        public CmdCheQiLianDong(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_cheQiConfig.FlagLayer == null)
            {
                MessageBox.Show(@"未选择扯旗图层！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(_cheQiConfig.Expression))
            {
                MessageBox.Show(@"扯旗表达式为空！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _geoFeatureLayer = _cheQiConfig.FlagLayer as IGeoFeatureLayer;
            _context.SetCurrentTool(this);
            _pointSnapper = new PointSnapper();
            (_pointSnapper as PointSnapper).Map = _context.FocusMap;
            _tolerance = _context.Config.EngineSnapEnvironment.SnapTolerance;
        }
        private void SelectByClick(int x, int y)
        {
            IMap map = _context.FocusMap;
            IEnvelope envelope = new Envelope() as IEnvelope;
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IActiveView activeView = (IActiveView)map;
            envelope.PutCoords(point.X, point.Y, point.X, point.Y);
            double num3 = activeView.Extent.Width / 200.0;
            envelope.XMin = (envelope.XMin - num3);
            envelope.YMin = (envelope.YMin - num3);
            envelope.YMax = (envelope.YMax + num3);
            envelope.XMax = (envelope.XMax + num3);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(envelope, selectionEnvironment, true);
            _context.ActiveView.Refresh();
        }

        private void SelectByShape(IGeometry geometry)
        {
            IMap map = _context.FocusMap;
            IEnvelope envelope = geometry.Envelope;
            IActiveView activeView = (IActiveView)map;
            double num3 = activeView.Extent.Width / 200.0;
            envelope.XMin = (envelope.XMin - 1);
            envelope.YMin = (envelope.YMin - 1);
            envelope.YMax = (envelope.YMax + 1);
            envelope.XMax = (envelope.XMax + 1);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            selectionEnvironment.SearchTolerance = 1;
            map.SelectByShape(envelope, selectionEnvironment, false);
            _context.ActiveView.Refresh();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "扯旗注记联动";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_CheQiLianDong";
            base._key = "PipelineEditor_CheQiLianDong";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
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
                if (_plugin.CheQiConfig == null)
                    return false;
                _cheQiConfig = _plugin.CheQiConfig;
                return true;
            }
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button != 1)
                return;

            if (_lineFeedback == null)
            {
                this.SelectByClick(x, y);

                if (_context.FocusMap.SelectionCount != 1)
                {
                    return;
                }
                IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                _annoFeature = enumFeature.Next();
                IAnnotationFeature annotationFeature = _annoFeature as IAnnotationFeature;
                if (annotationFeature == null)
                {
                    _annoFeature = null;
                    return;
                }
                this.SelectByShape(_annoFeature.Shape);
                IFeatureSelection featureSelection = _cheQiConfig.FlagLineLayer as IFeatureSelection;
                if (featureSelection?.SelectionSet == null || featureSelection.SelectionSet.Count != 1)
                    return;
                ICursor cursor;
                featureSelection.SelectionSet.Search(null, false, out cursor);
                if (cursor == null)
                    return;
                _lineFeature = cursor.NextRow() as IFeature;
                IPolyline polyline = _lineFeature?.Shape as IPolyline;
                if (polyline == null)
                    return;
                _lineFeedback = new NewLineFeedbackClass()
                {
                    Display = _context.ActiveView.ScreenDisplay
                };
                _lineFeedback.Start(polyline.FromPoint);
            }
            else
            {
                IActiveView activeView = _context.ActiveView;
                if (_context.Config.EngineSnapEnvironment.SnapToleranceUnits ==
                    esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
                {
                    _tolerance = ArcGIS.Common.Helpers.CommonHelper.ConvertPixelsToMapUnits(activeView, _tolerance);
                }
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
            if (snappingResult != null)
                point = snappingResult.Location;
            _lineFeedback?.MoveTo(point);
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
                IPointCollection pointCollection = _lineFeature.Shape as IPointCollection;
                IPointCollection newPointCollection = polyline as IPointCollection;

                IAnnotationFeature annotationFeature = _annoFeature as IAnnotationFeature;
                if (annotationFeature == null)
                    return;
                IElement element = annotationFeature.Annotation;
                IPoint annoPoint = new PointClass();
                annoPoint.X = (element.Geometry as IPoint).X +
                            (polyline.ToPoint.X -
                             pointCollection.Point[pointCollection.PointCount - 2].X);
                annoPoint.Y = (element.Geometry as IPoint).Y +
                            (polyline.ToPoint.Y -
                             pointCollection.Point[pointCollection.PointCount - 2].Y);
                element.Geometry = annoPoint;
                annotationFeature.Annotation = element;
                _annoFeature.Store();

                IPoint toPoint = new PointClass();
                toPoint.X = newPointCollection.Point[newPointCollection.PointCount - 1].X +
                            (pointCollection.Point[pointCollection.PointCount - 1].X -
                             pointCollection.Point[pointCollection.PointCount - 2].X);
                toPoint.Y = newPointCollection.Point[newPointCollection.PointCount - 1].Y +
                            (pointCollection.Point[pointCollection.PointCount - 1].Y -
                             pointCollection.Point[pointCollection.PointCount - 2].Y);
                newPointCollection.AddPoint(toPoint);
                _lineFeature.Shape = newPointCollection as IPolyline;
                _lineFeature.Store();
                _context.ActiveView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
