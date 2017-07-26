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
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    class CmdDuoYaoSuCheQi : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private IMultiCheQiConfig _multiCheQiConfig;
        private INewLineFeedback _lineFeedback;
        private IGeoFeatureLayer _geoFeatureLayer;
        private IPolyline _polyline;

        public CmdDuoYaoSuCheQi(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_multiCheQiConfig.FlagLayerList == null)
            {
                MessageBox.Show(@"未选择扯旗图层！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _context.SetCurrentTool(this);
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "多要素扯旗";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_DuoYaoSuCheQi";
            base._key = "PipelineEditor_DuoYaoSuCheQi";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
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
                if (_plugin.MultiCheQiConfig == null)
                    return false;
                _multiCheQiConfig = _plugin.MultiCheQiConfig;
                return true;
            }
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button != 1)
                return;

            IActiveView activeView = _context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
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

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (_lineFeedback == null)
                return;

            IActiveView activeView = _context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            _lineFeedback?.MoveTo(point);
        }

        public override void OnDblClick()
        {
            try
            {
                ArcGIS.Common.Editor.Editor.StartEditOperation();
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
                ISelectionEnvironment selectionEnvironment = new SelectionEnvironmentClass();
                selectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
                _context.FocusMap.SelectByShape(_polyline, selectionEnvironment, false);
                ISelection selection = _context.FocusMap.FeatureSelection;
                IEnumFeatureSetup enumFeatureSetup = selection as IEnumFeatureSetup;
                IEnumFeature enumFeature = enumFeatureSetup as IEnumFeature;
                if (enumFeature == null)
                    return;
                enumFeature.Reset();
                IFeature feature;
                List<MultiCheQiModel> modelList = new List<MultiCheQiModel>();
                while ((feature = enumFeature.Next()) != null)
                {
                    IFeatureClass featureClass = feature.Class as IFeatureClass;
                    if (featureClass == null)
                        continue;
                    IFeatureLayer featureLayer =
                        _multiCheQiConfig.FlagLayerList.FirstOrDefault(
                            c => c.FeatureClass.FeatureClassID == featureClass.FeatureClassID);
                    if (featureLayer == null)
                        continue;
                    modelList.Add(new MultiCheQiModel(featureLayer, feature, _multiCheQiConfig.FieldSettingList, _polyline));
                }
                if (modelList.Count <= 0)
                    return;

                _context.ActiveView.ScreenDisplay.StartDrawing(_context.ActiveView.ScreenDisplay.hDC, 0);
                IAnnotationClassExtension annotationClassExtension = _multiCheQiConfig.FlagAnnoLayer.FeatureClass.Extension as IAnnotationClassExtension;
                IElement headerElement = CommonHelper.CreateHeaderElements(_multiCheQiConfig, _polyline.ToPoint);
                IFeature headerFeature = _multiCheQiConfig.FlagAnnoLayer.FeatureClass.CreateFeature();
                IAnnotationFeature headerAnnotationFeature = headerFeature as IAnnotationFeature;
                headerAnnotationFeature.Annotation = headerElement;
                headerFeature.Store();
                double xLength = headerFeature.Shape.Envelope.Width;
                double yLength = headerFeature.Shape.Envelope.Height;

                IPoint headerPoint = new PointClass();
                headerPoint.X = (headerElement.Geometry as IPoint).X - xLength / 2;
                headerPoint.Y = (headerElement.Geometry as IPoint).Y + yLength * (modelList.Count + 0.5);
                headerElement.Geometry = headerPoint;
                headerAnnotationFeature.Annotation = headerElement;
                headerFeature.Store();
                annotationClassExtension.Draw(headerAnnotationFeature, _context.ActiveView.ScreenDisplay, null);
                List<MultiCheQiModel> models = new List<MultiCheQiModel>(modelList.OrderBy(c => c.Distance));
                List<IElement> elements = CommonHelper.CreateContentElements(_multiCheQiConfig, models,
                    headerElement.Geometry as IPoint, headerFeature.Shape.Envelope.Width,
                    headerFeature.Shape.Envelope.Height);
                foreach (IElement element in elements)
                {
                    IFeature contentFeature = _multiCheQiConfig.FlagAnnoLayer.FeatureClass.CreateFeature();
                    IAnnotationFeature contentAnnotationFeature = contentFeature as IAnnotationFeature;
                    contentAnnotationFeature.Annotation = element;
                    contentFeature.Store();
                    annotationClassExtension.Draw(contentAnnotationFeature, _context.ActiveView.ScreenDisplay, null);
                }
                _context.ActiveView.ScreenDisplay.FinishDrawing();

                IPointCollection pointCollection = _polyline as IPointCollection;
                IPoint point1 = new PointClass();
                point1.X = _polyline.ToPoint.X - xLength;
                point1.Y = _polyline.ToPoint.Y;

                IPoint point2 = new PointClass();
                point2.X = _polyline.ToPoint.X - xLength;
                point2.Y = _polyline.ToPoint.Y + yLength * (modelList.Count + 1);

                pointCollection.AddPoint(point1);
                pointCollection.AddPoint(point2);
                IFeature lineFeature = _multiCheQiConfig.FlagLineLayer.FeatureClass.CreateFeature();
                lineFeature.Shape = pointCollection as IPolyline;
                lineFeature.Store();

                _context.ActiveView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                ArcGIS.Common.Editor.Editor.StopEditOperation();
            }
        }
    }
}
