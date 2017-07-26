using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    class CmdCheQi : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private ICheQiConfig _cheQiConfig;
        private INewLineFeedback _lineFeedback;
        private IFeature _feature;
        private IGeoFeatureLayer _geoFeatureLayer;
        private IPolyline _polyline;
        private IPointSnapper _pointSnapper;
        private double _tolerance = 0.01;

        public CmdCheQi(IAppContext context, PipelineEditorPlugin plugin)
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

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "扯旗";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_CheQi";
            base._key = "PipelineEditor_CheQi";
            base.m_toolTip = "管线扯旗工具";
            base.m_checked = false;
            base.m_message = "管线扯旗工具";
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

            IActiveView activeView = _context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            ISnappingResult snappingResult = _pointSnapper.Snap(point);
            if (snappingResult == null)
            {
                if (_lineFeedback == null)
                {
                    _lineFeedback = new NewLineFeedbackClass()
                    {
                        Display = activeView.ScreenDisplay
                    };
                    _lineFeedback.Start(point);
                }
                else
                {
                    _lineFeedback.AddPoint(point);
                }
            }
            else
            {
                point = snappingResult.Location;

                if (_lineFeedback == null)
                {
                    _lineFeedback = new NewLineFeedbackClass()
                    {
                        Display = activeView.ScreenDisplay
                    };
                    _lineFeedback.Start(point);
                }
                else
                {
                    _lineFeedback.AddPoint(point);
                }
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

        public override void OnDblClick()
        {
            try
            {
                if (_lineFeedback == null)
                    return;
                _polyline = _lineFeedback.Stop();
                if (_lineFeedback != null)
                {
                    _context.ActiveView.Refresh();
                    _lineFeedback = null;
                }
                if (_polyline == null)
                    return;

                if (_context.Config.EngineSnapEnvironment.SnapToleranceUnits ==
                    esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
                {
                    _tolerance = ArcGIS.Common.Helpers.CommonHelper.ConvertPixelsToMapUnits(_context.ActiveView, _tolerance);
                }
                _feature = MapHelper.GetFirstFeatureFromPointSearchInGeoFeatureLayer(_tolerance, _polyline.FromPoint,
                    _geoFeatureLayer,
                    _context.ActiveView);


                if (_feature == null)
                {
                    MessageBox.Show(@"没有找到要扯旗的要素，请重新选择！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    AddElement(_polyline);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AddElement(IPolyline polyline)
        {
            try
            {
                ArcGIS.Common.Editor.Editor.StartEditOperation();

                string strLineInfo = CommonHelper.GetIntersectInformationFlagLineOnlyOne(polyline, _cheQiConfig,
                    _feature);
                if (string.IsNullOrWhiteSpace(strLineInfo))
                {
                    MessageBox.Show(@"扯旗字段为空，请重新设置扯旗字段", @"扯旗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IPoint referPoint = new PointClass();
                referPoint.X = _polyline.ToPoint.X;
                referPoint.Y = _polyline.ToPoint.Y;

                stdole.IFontDisp fontDisp = new StdFontClass() as IFontDisp;
                fontDisp.Name = _cheQiConfig.FontName;
                fontDisp.Size = _cheQiConfig.FontSize;
                fontDisp.Italic = _cheQiConfig.Italic;
                fontDisp.Underline = _cheQiConfig.Underline;
                fontDisp.Bold = _cheQiConfig.Bold;
                fontDisp.Strikethrough = _cheQiConfig.Strikethrough;

                ITextSymbol textSymbol = new TextSymbolClass();
                textSymbol.Size = (double)_cheQiConfig.FontSize;
                textSymbol.Font = fontDisp;
                textSymbol.Color = _cheQiConfig.FontColor;
                textSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

                ITextElement textElement = new TextElementClass();
                textElement.Text = strLineInfo;
                textElement.Symbol = textSymbol;
                textElement.ScaleText = true;

                IPoint textPoint = new PointClass();
                textPoint.X = referPoint.X;
                textPoint.Y = referPoint.Y + 1;

                IElement element = textElement as IElement;
                element.Geometry = textPoint;

                IFeature annoFeature = _cheQiConfig.FlagAnnoLayer.FeatureClass.CreateFeature();
                IAnnotationClassExtension annotationClassExtension = _cheQiConfig.FlagAnnoLayer.FeatureClass.Extension as IAnnotationClassExtension;
                IAnnotationFeature annotationFeature = new AnnotationFeatureClass();
                annotationFeature = annoFeature as IAnnotationFeature;
                annotationFeature.Annotation = element;
                annotationFeature.LinkedFeatureID = _feature.OID;
                annoFeature.Store();
                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, 0);
                annotationClassExtension.Draw(annotationFeature, _context.ActiveView.ScreenDisplay, null);
                _context.ActiveView.ScreenDisplay.FinishDrawing();

                double maxLength = annoFeature.Shape.Envelope.Width;
                IFeatureClass flagLineFeatureClass = _cheQiConfig.FlagLineLayer.FeatureClass;
                IFeature feature = flagLineFeatureClass.CreateFeature();
                IPointCollection pointCollection = _polyline as IPointCollection;
                IPoint point = new PointClass();
                point.Y = referPoint.Y;
                point.X = referPoint.X + maxLength;
                pointCollection.AddPoint(point);
                feature.Shape = pointCollection as IPolyline;
                feature.Store();

                _context.ActiveView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                ArcGIS.Common.Editor.Editor.StartEditOperation();
            }
        }
    }
}



