using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Forms.Common;
using Yutai.Pipeline.Editor.Forms.Profession;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.Properties;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Common
{
    class CmdSplitLine : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private IFeature _feature;
        private ICodeSetting _codeSetting;
        private IFeatureLayer _pointFeatureLayer;
        private IFeatureLayer _lineFeatureLayer;
        private string _gdbhFieldName;
        private string _qdbhFieldName;
        private string _zdbhFieldName;

        private string _dmgcFieldName;
        private string _qdmsFieldName;
        private string _zdmsFieldName;
        private string _qdgcFieldName;
        private string _zdgcFieldName;

        private int _idxGdbhField;
        private int _idxQdbhField;
        private int _idxZdbhField;

        private int _idxDmgcField;
        private int _idxQdmsField;
        private int _idxZdmsField;
        private int _idxQdgcField;
        private int _idxZdgcField;


        public CmdSplitLine(IAppContext context, PipelineEditorPlugin plugin)
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
            {
                _gdbhFieldName = pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.GDBH);
                _dmgcFieldName = pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.DMGC);
                _pointFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, pointLayerInfo.AliasName, true) as IFeatureLayer;
                _idxGdbhField = _pointFeatureLayer.FeatureClass.FindField(_gdbhFieldName);
                _idxDmgcField = _pointFeatureLayer.FeatureClass.FindField(_dmgcFieldName);
            }
            IBasicLayerInfo lineLayerInfo = _plugin.CurrentLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
            if (lineLayerInfo != null)
            {
                _qdbhFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDBH);
                _zdbhFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDBH);
                _qdmsFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS);
                _zdmsFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS);
                _qdgcFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC);
                _zdgcFieldName = lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC);
                _lineFeatureLayer = CommonHelper.GetLayerByName(_context.FocusMap, lineLayerInfo.AliasName, true) as IFeatureLayer;
                _idxQdbhField = _lineFeatureLayer.FeatureClass.FindField(_qdbhFieldName);
                _idxZdbhField = _lineFeatureLayer.FeatureClass.FindField(_zdbhFieldName);
                _idxQdmsField = _lineFeatureLayer.FeatureClass.FindField(_qdmsFieldName);
                _idxZdmsField = _lineFeatureLayer.FeatureClass.FindField(_zdmsFieldName);
                _idxQdgcField = _lineFeatureLayer.FeatureClass.FindField(_qdgcFieldName);
                _idxZdgcField = _lineFeatureLayer.FeatureClass.FindField(_zdgcFieldName);
            }
            if (_pointFeatureLayer == null || _lineFeatureLayer == null)
            {
                MessageBox.Show(@"点图层或线图层未设置！");
                _context.ClearCurrentTool();
                return;
            }
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            _feature = featureSelection.Next();
            if (_feature == null)
            {
                MessageBox.Show(@"未选择将打断的线要素！");
                _context.ClearCurrentTool();
                return;
            }
            _context.SetCurrentTool(this);
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "管线打断";
            base.m_category = "PipelineEditor";
            this.m_bitmap = Resources.icon_SplitLine;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resources.Digitise));
            base.m_name = "PipelineEditor_SplitLine";
            base._key = "PipelineEditor_SplitLine";
            base.m_toolTip = "管线打断";
            base.m_checked = false;
            base.m_message = "管线打断";
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

        public override void OnDblClick()
        {

        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (_feature == null)
                return;
            IPoint mapPoint = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IInvalidArea invalidAreaClass = new InvalidAreaClass()
            {
                Display = (_context.FocusMap as IActiveView).ScreenDisplay
            };
            invalidAreaClass.Add(_feature);
            Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation(_feature.Class as IDataset);
            try
            {
                if (_codeSetting == null)
                    _codeSetting = new FrmCode(_pointFeatureLayer, _gdbhFieldName);
                _codeSetting.Next();
                if (_codeSetting.ShowDialog() == DialogResult.OK)
                {
                    ILineFeatureCalculate pCalculate = new LineFeatureCalculate(_feature, _lineFeatureLayer, _pointFeatureLayer);
                    pCalculate.LineFeatureLayer = _lineFeatureLayer;
                    pCalculate.PointFeatureLayer = _pointFeatureLayer;
                    pCalculate.LineFeature = _feature;
                    pCalculate.Point = mapPoint;
                    pCalculate.IdxDMGCField = _idxDmgcField;
                    pCalculate.IdxQDMSField = _idxQdmsField;
                    pCalculate.IdxZDMSField = _idxZdmsField;
                    double dmgcValue = pCalculate.GetGroundHeightByPoint();
                    double gxmsValue = pCalculate.GetDepthByPoint();
                    ISet set = (_feature as IFeatureEdit).Split(mapPoint);
                    set.Reset();

                    IFeature feature = set.Next() as IFeature;
                    if (CommonHelper.IsFromPoint(feature.Shape as IPolyline, mapPoint))
                    {
                        feature.Value[_idxQdbhField] = _codeSetting.Code;
                        feature.Value[_idxQdmsField] = gxmsValue.ToString("##0.0000");
                        feature.Value[_idxQdgcField] = (dmgcValue-gxmsValue).ToString("##0.0000");
                    }
                    else
                    {
                        feature.Value[_idxZdbhField] = _codeSetting.Code;
                        feature.Value[_idxZdmsField] = gxmsValue.ToString("##0.0000");
                        feature.Value[_idxZdgcField] = (dmgcValue - gxmsValue).ToString("##0.0000");
                    }
                    feature.Store();

                    feature = set.Next() as IFeature;
                    if (CommonHelper.IsFromPoint(feature.Shape as IPolyline, mapPoint))
                    {
                        feature.Value[_idxQdbhField] = _codeSetting.Code;
                        feature.Value[_idxQdmsField] = gxmsValue.ToString("##0.0000");
                        feature.Value[_idxQdgcField] = (dmgcValue - gxmsValue).ToString("##0.0000");
                    }
                    else
                    {
                        feature.Value[_idxZdbhField] = _codeSetting.Code;
                        feature.Value[_idxZdmsField] = gxmsValue.ToString("##0.0000");
                        feature.Value[_idxZdgcField] = (dmgcValue - gxmsValue).ToString("##0.0000");
                    }
                    feature.Store();


                    IFeature newFeature = _pointFeatureLayer.FeatureClass.CreateFeature();
                    newFeature.Value[_idxGdbhField] = _codeSetting.Code;
                    newFeature.Value[_idxDmgcField] = dmgcValue.ToString("##0.0000");
                    IPoint nearPoint = CommonHelper.GetNearestPoint(feature.Shape as IPolyline, mapPoint);
                    newFeature.Shape = GeometryHelper.CreatePoint(nearPoint.X, nearPoint.Y, nearPoint.Z, nearPoint.M, FeatureClassUtil.CheckHasZ(_pointFeatureLayer.FeatureClass), FeatureClassUtil.CheckHasM(_pointFeatureLayer.FeatureClass));
                    newFeature.Store();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Yutai.ArcGIS.Common.Editor.Editor.StopEditOperation(_feature.Class as IDataset);
            invalidAreaClass.Invalidate(-2);
            _context.ClearCurrentTool();
            _feature = null;
            _context.ActiveView.Refresh();
        }
    }
}

