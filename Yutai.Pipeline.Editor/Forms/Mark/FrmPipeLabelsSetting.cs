using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;
using Yutai.ArcGIS.Common;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.Xml;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Pipeline.Editor.Forms.Mark
{
    public partial class FrmPipeLabelsSetting : Form
    {
        private IAppContext _context;
        private IPipelineConfig _pipelineConfig;
        private IBasicLayerInfo _pointLayerInfo;
        private IBasicLayerInfo _lineLayerInfo;
        private IFeatureLayer _pointFeatureLayer;
        private IFeatureLayer _lineFeatureLayer;
        private IBasicLayerInfo _pointAnnLayerInfo;
        private IBasicLayerInfo _lineAnnLayerInfo;
        private IWorkspace _workspace;

        public FrmPipeLabelsSetting(IAppContext context, IPipelineConfig pipelineConfig)
        {
            InitializeComponent();
            _context = context;
            _pipelineConfig = pipelineConfig;
            LoadPipelineLayers();
            ComboBoxHelper.AddItemsFromSystemFonts(cmbSetAnnoPointFront);
            ComboBoxHelper.AddItemsFromSystemFonts(cmbSetAnnoLineFront);
        }

        private void LoadPipelineLayers()
        {
            try
            {
                ComboBoxHelper.AddItemsFromList(_pipelineConfig.Layers, cmbPipelineLayers, "Name", "Code");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                IDataset dataset = _pointFeatureLayer.FeatureClass as IDataset;
                if (dataset == null)
                    return;
                _workspace = dataset.Workspace;

                if (string.IsNullOrWhiteSpace(cboScale.Text))
                {
                    MessageBox.Show(@"比例尺未选择！");
                    return;
                }

                if (chbisAnoPoint.Checked)
                    ConvertPointAnnotation();
                if (chbisAnoLine.Checked)
                    ConvertLineAnnotation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void chbisAnoPoint_CheckedChanged(object sender, EventArgs e)
        {
            panelPoint.Enabled = chbisAnoPoint.Checked;
            if (chbisAnoPoint.Checked)
            {
                IPipelineLayer pipelineLayer = cmbPipelineLayers.SelectedItem as IPipelineLayer;
                if (pipelineLayer?.Layers == null || pipelineLayer.Layers.Count <= 0)
                    return;
                chbisAnoPoint.Checked = LoadPointLayerInfo(pipelineLayer);
            }
        }

        private void chbisAnoLine_CheckedChanged(object sender, EventArgs e)
        {
            panelLine.Enabled = chbisAnoLine.Checked;
            if (chbisAnoLine.Checked)
            {
                IPipelineLayer pipelineLayer = cmbPipelineLayers.SelectedItem as IPipelineLayer;
                if (pipelineLayer?.Layers == null || pipelineLayer.Layers.Count <= 0)
                    return;
                chbisAnoLine.Checked = LoadLineLayerInfo(pipelineLayer);
            }
        }

        private bool LoadPointLayerInfo(IPipelineLayer pipelineLayer)
        {
            _pointAnnLayerInfo = pipelineLayer.GetLayers(enumPipelineDataType.AnnoPoint).FirstOrDefault();
            if (_pointAnnLayerInfo?.FeatureClass == null)
            {
                MessageBox.Show(@"没有配置点注记图层！");
                return false;
            }
            txtPointAno.Text = _pointAnnLayerInfo.Name;

            _pointLayerInfo = pipelineLayer.GetLayers(enumPipelineDataType.Point).FirstOrDefault();
            if (_pointLayerInfo?.FeatureClass == null)
            {
                MessageBox.Show(@"没有配置点图层！");
                return false;
            }
            tabPage1.Text = _pointLayerInfo.Name;

            ComboBoxHelper.AddItemsFromFields(_pointLayerInfo.FeatureClass.Fields, cmbPointFields);

            _pointFeatureLayer = CommonHelper.GetLayerByFeatureClassName(_context.FocusMap,
                _pointLayerInfo.EsriClassName);
            if (_pointFeatureLayer == null)
            {
                MessageBox.Show(@"当前地图中无可用图层！");
                return false;
            }
            return true;
        }

        private bool LoadLineLayerInfo(IPipelineLayer pipelineLayer)
        {
            _lineAnnLayerInfo = pipelineLayer.GetLayers(enumPipelineDataType.AnnoLine).FirstOrDefault();
            if (_lineAnnLayerInfo?.FeatureClass == null)
            {
                MessageBox.Show(@"没有配置线注记图层！");
                return false;
            }
            txtLineAno.Text = _lineAnnLayerInfo.Name;

            _lineLayerInfo = pipelineLayer.GetLayers(enumPipelineDataType.Line).FirstOrDefault();
            if (_lineLayerInfo?.FeatureClass == null)
            {
                MessageBox.Show(@"没有配置线图层！");
                return false;
            }
            tabPage2.Text = _lineLayerInfo.Name;

            ComboBoxHelper.AddItemsFromFields(_lineLayerInfo.FeatureClass.Fields, cmbLineFields);

            _lineFeatureLayer = CommonHelper.GetLayerByFeatureClassName(_context.FocusMap,
                _lineLayerInfo.EsriClassName);
            if (_lineFeatureLayer == null)
            {
                MessageBox.Show(@"当前地图中无可用图层！");
                return false;
            }
            return true;
        }

        private void cboScale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void cmbPipelineLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            IPipelineLayer pipelineLayer = cmbPipelineLayers.SelectedItem as IPipelineLayer;
            if (pipelineLayer?.Layers == null || pipelineLayer.Layers.Count <= 0)
                return;
            _pointLayerInfo = pipelineLayer.GetLayers(enumPipelineDataType.Point).FirstOrDefault();
            if (_pointLayerInfo?.FeatureClass != null)
            {
                _pointFeatureLayer = CommonHelper.GetLayerByFeatureClassName(_context.FocusMap,
                    _pointLayerInfo.EsriClassName);
                IDataset dataset = _pointFeatureLayer?.FeatureClass as IDataset;
                if (dataset != null)
                    this.txtSavePath.Text = dataset.Workspace.PathName;
            }

            chbisAnoPoint_CheckedChanged(null, null);
            chbisAnoLine_CheckedChanged(null, null);
        }

        private void ConvertPointAnnotation()
        {
            IPointPlacementPriorities pointPlacementPriorities = new PointPlacementPrioritiesClass();
            pointPlacementPriorities.AboveLeft = (int)numAboveLeft.Value;
            pointPlacementPriorities.AboveCenter = (int)numAboveCenter.Value;
            pointPlacementPriorities.AboveRight = (int)numAboveRight.Value;
            pointPlacementPriorities.CenterRight = (int)numCenterRight.Value;
            pointPlacementPriorities.BelowRight = (int)numBelowRight.Value;
            pointPlacementPriorities.BelowCenter = (int)numBelowCenter.Value;
            pointPlacementPriorities.BelowLeft = (int)numBelowLeft.Value;
            pointPlacementPriorities.CenterLeft = (int)numCenterLeft.Value;

            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = cmbSetAnnoPointColor.Color.R;
            rgbColor.Blue = cmbSetAnnoPointColor.Color.B;
            rgbColor.Green = cmbSetAnnoPointColor.Color.G;
            esriBasicOverposterWeight labelWeight = (esriBasicOverposterWeight)Enum.Parse(typeof(esriBasicOverposterWeight), radioGroupPointLabelWeight.EditValue.ToString());
            esriBasicOverposterWeight featureWeight = (esriBasicOverposterWeight)Enum.Parse(typeof(esriBasicOverposterWeight), radioGroupPointFeatureWeight.EditValue.ToString());
            esriLabelWhichFeatures selectionType = (esriLabelWhichFeatures)Enum.Parse(typeof(esriLabelWhichFeatures), radioGroupSelectionType.EditValue.ToString());

            string expression = $"[{cmbPointFields.SelectedItem}]";

            ConvertAnnotation(_context.FocusMap, _pointFeatureLayer, txtPointAno.Text, Convert.ToDouble(cboScale.Text), _workspace, null,
                pointPlacementPriorities, labelWeight, featureWeight,
                checkBoxPointPlaceOverlappingLabels.Checked == false, cmbSetAnnoPointFront.Text,
                (double)numSetAnnoPointFrontSize.Value, rgbColor, expression, chbPointLinked.Checked, selectionType);
        }

        private void ConvertLineAnnotation()
        {
            ILineLabelPosition pPosition = new LineLabelPositionClass();
            pPosition.Parallel = true;
            pPosition.Above = checkedListBoxLineLabelPosition.Items[0].CheckState == CheckState.Checked;
            pPosition.InLine = checkedListBoxLineLabelPosition.Items[1].CheckState == CheckState.Checked;
            pPosition.Below = checkedListBoxLineLabelPosition.Items[2].CheckState == CheckState.Checked;

            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = cmbSetAnnoLineColor.Color.R;
            rgbColor.Blue = cmbSetAnnoLineColor.Color.B;
            rgbColor.Green = cmbSetAnnoLineColor.Color.G;

            esriBasicOverposterWeight labelWeight = (esriBasicOverposterWeight)Enum.Parse(typeof(esriBasicOverposterWeight), radioGroupLineLabelWeight.EditValue.ToString());
            esriBasicOverposterWeight featureWeight = (esriBasicOverposterWeight)Enum.Parse(typeof(esriBasicOverposterWeight), radioGroupPointFeatureWeight.EditValue.ToString());
            esriLabelWhichFeatures selectionType = (esriLabelWhichFeatures)Enum.Parse(typeof(esriLabelWhichFeatures), radioGroupSelectionType.EditValue.ToString());

            string expression = $"[{cmbLineFields.SelectedItem}]";

            ConvertAnnotation(_context.FocusMap, _lineFeatureLayer, txtLineAno.Text, Convert.ToDouble(cboScale.Text), _workspace, pPosition, null,
                labelWeight, featureWeight,
                checkBoxLinePlaceOverlappingLabels.Checked == false, cmbSetAnnoLineFront.Text,
                (double)numSetAnnoLineFrontSize.Value, rgbColor, expression, chbLineLinked.Checked, selectionType);
        }

        private void ConvertAnnotation(IMap map, ILayer featureLayer, string annotationFeatureClassName, double scale,
            IWorkspace workspace, ILineLabelPosition lineLabelPosition,
            IPointPlacementPriorities pointPlacementPriorities,
            esriBasicOverposterWeight labelWeight, esriBasicOverposterWeight featureWeight, bool tagUnplaced,
            string fontName, double fontSize, IRgbColor rgbColor, string expression, bool featureLinked,
            esriLabelWhichFeatures labelWhichFeatures)
        {
            if (workspace.Type != esriWorkspaceType.esriFileSystemWorkspace && featureLayer is IGeoFeatureLayer)
            {
                IGeoFeatureLayer geoFeatureLayer = featureLayer as IGeoFeatureLayer;
                IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass =
                    geoFeatureLayer.AnnotationProperties;
                annotateLayerPropertiesCollectionClass.Clear();
                ILabelEngineLayerProperties labelEngineLayerProperties = new LabelEngineLayerPropertiesClass();
                IBasicOverposterLayerProperties basicOverposterLayerProperties =
                    new BasicOverposterLayerPropertiesClass();
                if (lineLabelPosition != null)
                    basicOverposterLayerProperties.LineLabelPosition = lineLabelPosition;
                else if (pointPlacementPriorities != null)
                    basicOverposterLayerProperties.PointPlacementPriorities = pointPlacementPriorities;
                basicOverposterLayerProperties.LabelWeight = labelWeight;
                basicOverposterLayerProperties.FeatureWeight = featureWeight;

                IOverposterLayerProperties2 overposterLayerProperties2 =
                    basicOverposterLayerProperties as IOverposterLayerProperties2;
                overposterLayerProperties2.TagUnplaced = tagUnplaced;

                labelEngineLayerProperties.BasicOverposterLayerProperties = basicOverposterLayerProperties;
                stdole.IFontDisp pFont = new stdole.StdFontClass() as stdole.IFontDisp;
                pFont.Name = fontName;
                ITextSymbol pTextSymbol = new TextSymbolClass();
                pTextSymbol.Size = fontSize;
                pTextSymbol.Font = pFont;
                pTextSymbol.Color = rgbColor;
                labelEngineLayerProperties.Symbol = pTextSymbol;
                labelEngineLayerProperties.Expression = expression;

                IAnnotateLayerProperties annotateLayerProperties =
                    labelEngineLayerProperties as IAnnotateLayerProperties;
                annotateLayerProperties.DisplayAnnotation = true;

                IAnnotationLayer annotationLayer =
                    CommonHelper.GetLayerByFeatureClassName(_context.FocusMap, annotationFeatureClassName) as
                        IAnnotationLayer;
                CommonHelper.SetReferenceScale(annotationLayer, scale);
                annotateLayerProperties.FeatureLayer = geoFeatureLayer;
                annotateLayerProperties.GraphicsContainer = annotationLayer as IGraphicsContainer;
                annotateLayerProperties.AddUnplacedToGraphicsContainer = true;
                annotateLayerProperties.CreateUnplacedElements = true;
                annotateLayerProperties.DisplayAnnotation = true;
                annotateLayerProperties.FeatureLinked = featureLinked;
                annotateLayerProperties.LabelWhichFeatures = labelWhichFeatures;
                annotateLayerProperties.UseOutput = true;
                annotateLayerPropertiesCollectionClass.Add(annotateLayerProperties);

                annotateLayerPropertiesCollectionClass.Sort();
                IAnnotateMapProperties annotateMapPropertiesClass = new AnnotateMapPropertiesClass()
                {
                    AnnotateLayerPropertiesCollection = annotateLayerPropertiesCollectionClass
                };
                ITrackCancel cancelTrackerClass = new CancelTrackerClass();
                IAnnotateMap2 annotateMap2 = map.AnnotationEngine as IAnnotateMap2;
                IOverposterProperties overposterProperties = (map as IMapOverposter).OverposterProperties;
                annotateMap2.Label(overposterProperties, annotateMapPropertiesClass, map, cancelTrackerClass);
                map.AddLayer(annotationLayer as ILayer);
                geoFeatureLayer.DisplayAnnotation = false;
                map.ReferenceScale = scale;
                map.MapScale = scale;
                IActiveView activeView = map as IActiveView;
                activeView.Refresh();
            }
        }
    }
}





