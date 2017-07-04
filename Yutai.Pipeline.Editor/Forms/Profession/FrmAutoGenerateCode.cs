using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;

namespace Yutai.Pipeline.Editor.Forms.Profession
{
    public partial class FrmAutoGenerateCode : Form
    {
        private readonly IAppContext _context;
        private IPipelineLayer _pipelineLayer;
        private IBasicLayerInfo _pointLayerInfo;
        private IBasicLayerInfo _lineLayerInfo;
        private string _expression;
        private int _length;

        public FrmAutoGenerateCode(IAppContext context, IPipelineConfig pipelineConfig)
        {
            _context = context;
            InitializeComponent();
            ComboBoxHelper.AddItemsFromList(pipelineConfig.Layers, cmbPipelineLayers, "Name", "Code");

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_pointLayerInfo?.FeatureClass == null || _lineLayerInfo?.FeatureClass == null)
                return;
            int idxKeyField = _pointLayerInfo.FeatureClass.FindField(cmbKeyField.SelectedItem.ToString());
            int idxCodeField = _pointLayerInfo.FeatureClass.FindField(cmbCodeField.SelectedItem.ToString());
            int idxStartKeyField = _lineLayerInfo.FeatureClass.FindField(cmbStartKeyField.SelectedItem.ToString());
            int idxEndKeyField = _lineLayerInfo.FeatureClass.FindField(cmbEndKeyField.SelectedItem.ToString());
            int idxStartCodeField = _lineLayerInfo.FeatureClass.FindField(cmbStartCodeField.SelectedItem.ToString());
            int idxEndCodeField = _lineLayerInfo.FeatureClass.FindField(cmbEndCodeField.SelectedItem.ToString());
            if (idxKeyField == -1 || idxCodeField == -1 || idxStartKeyField == -1 || idxEndKeyField == -1 || idxStartCodeField == -1 || idxEndCodeField == -1)
                return;
            List<IField> fields = GetFields(_expression);

            IDataset dataset = _pointLayerInfo.FeatureClass as IDataset;
            IWorkspaceEdit workspaceEdit = dataset?.Workspace as IWorkspaceEdit;
            if (workspaceEdit != null)
            {
                workspaceEdit.StartEditOperation();
                try
                {
                    IQueryFilter pQueryFilter = new QueryFilterClass();
                    IFeatureCursor pPointFeatureCursor = _pointLayerInfo.FeatureClass.Search(pQueryFilter, false);
                    IFeature pPointFeature;

                    List<CodeValue> codeValueList = new List<CodeValue>();
                    while ((pPointFeature = pPointFeatureCursor.NextFeature()) != null)
                    {
                        ITopologicalOperator pTopologicalOperator = pPointFeature.ShapeCopy as ITopologicalOperator;
                        if (pTopologicalOperator == null)
                            continue;
                        string keyValue = pPointFeature.Value[idxKeyField].ToString();
                        if (checkBoxUseAttr.Checked && string.IsNullOrEmpty(keyValue))
                            continue;

                        StringBuilder codeValue = new StringBuilder(_expression);
                        foreach (IField field in fields)
                        {
                            int idx = _pointLayerInfo.FeatureClass.Fields.FindField(field.Name);
                            codeValue.Replace($"[{field.Name}]", pPointFeature.Value[idx] is DBNull ? "" : pPointFeature.Value[idx].ToString());
                        }
                        int tempCodeValueCount = codeValueList.Count(c => c.Code == codeValue.ToString());
                        codeValueList.Add(new CodeValue
                        {
                            Code = codeValue.ToString(),
                            Value = tempCodeValueCount + 1
                        });
                        codeValue.Replace("[#####]", (tempCodeValueCount + 1).ToString().PadLeft(_length, '0'));

                        pPointFeature.Value[idxCodeField] = codeValue.ToString();
                        pPointFeature.Store();
                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        if (checkBoxUseAttr.Checked)
                            pSpatialFilter.WhereClause = $"{cmbStartKeyField.SelectedItem} = '{keyValue}'";
                        pSpatialFilter.GeometryField = _lineLayerInfo.FeatureClass.ShapeFieldName;
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        pSpatialFilter.Geometry = pTopologicalOperator.Buffer(0.0001);
                        pSpatialFilter.OutputSpatialReference[_lineLayerInfo.FeatureClass.ShapeFieldName] =
                            _context.FocusMap.SpatialReference;
                        IFeatureCursor pStartFeatureCursor = _lineLayerInfo.FeatureClass.Search(pSpatialFilter, false);
                        IFeature pStartFeature;
                        while ((pStartFeature = pStartFeatureCursor.NextFeature()) != null)
                        {
                            if (checkBoxUseAttr.Checked)
                            {
                                pStartFeature.Value[idxStartCodeField] = codeValue.ToString();
                            }
                            else
                            {
                                IPolyline polyline = pStartFeature.ShapeCopy as IPolyline;
                                if (polyline != null)
                                {
                                    switch (CommonHelper.IsFromPoint(pPointFeature.ShapeCopy as IPoint, polyline))
                                    {
                                        case 1:
                                            pStartFeature.Value[idxStartCodeField] = codeValue.ToString();
                                            break;
                                        case -1:
                                            pStartFeature.Value[idxEndCodeField] = codeValue.ToString();
                                            break;
                                        case 0:
                                            break;
                                    }
                                    pStartFeature.Store();
                                }
                            }
                        }
                        Marshal.ReleaseComObject(pStartFeatureCursor);
                        if (checkBoxUseAttr.Checked)
                        {
                            pSpatialFilter = new SpatialFilterClass();
                            pSpatialFilter.WhereClause = $"{cmbEndKeyField.SelectedItem} = '{keyValue}'";
                            pSpatialFilter.GeometryField = _lineLayerInfo.FeatureClass.ShapeFieldName;
                            pSpatialFilter.Geometry = pTopologicalOperator.Buffer(0.0001);
                            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            pSpatialFilter.OutputSpatialReference[_lineLayerInfo.FeatureClass.ShapeFieldName] = _context.FocusMap.SpatialReference;
                            IFeatureCursor pEndFeatureCursor = _lineLayerInfo.FeatureClass.Search(pSpatialFilter, false);
                            IFeature pEndFeature;
                            while ((pEndFeature = pEndFeatureCursor.NextFeature()) != null)
                            {
                                pEndFeature.Value[idxEndCodeField] = codeValue.ToString();
                                pEndFeature.Store();
                            }
                            Marshal.ReleaseComObject(pEndFeatureCursor);
                        }
                    }


                    Marshal.ReleaseComObject(pPointFeatureCursor);
                    this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    workspaceEdit.StopEditOperation();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private class CodeValue
        {
            public string Code { get; set; }
            public int Value { get; set; }
        }

        private void cmbPipelineLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pointLayerInfo = null;
            _lineLayerInfo = null;
            txtPointLayer.Text = null;
            txtLineLayer.Text = null;
            _pipelineLayer = cmbPipelineLayers.SelectedItem as IPipelineLayer;
            if (_pipelineLayer == null)
                return;
            _pointLayerInfo = _pipelineLayer.GetLayers(enumPipelineDataType.Point).FirstOrDefault();
            _lineLayerInfo = _pipelineLayer.GetLayers(enumPipelineDataType.Line).FirstOrDefault();
            if (_pointLayerInfo?.FeatureClass != null)
            {
                txtPointLayer.Text = _pointLayerInfo.AliasName;
                ComboBoxHelper.AddItemsFromFields(_pointLayerInfo.FeatureClass.Fields, cmbKeyField);
                ComboBoxHelper.AddItemsFromFields(_pointLayerInfo.FeatureClass.Fields, cmbCodeField, true, _pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.GDBH));
            }
            if (_lineLayerInfo?.FeatureClass != null)
            {
                txtLineLayer.Text = _lineLayerInfo.AliasName;
                ComboBoxHelper.AddItemsFromFields(_lineLayerInfo.FeatureClass.Fields, cmbStartKeyField);
                ComboBoxHelper.AddItemsFromFields(_lineLayerInfo.FeatureClass.Fields, cmbEndKeyField);
                ComboBoxHelper.AddItemsFromFields(_lineLayerInfo.FeatureClass.Fields, cmbStartCodeField, true, _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDBH));
                ComboBoxHelper.AddItemsFromFields(_lineLayerInfo.FeatureClass.Fields, cmbEndCodeField, true, _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDBH));
            }
        }

        private void btnCreateRule_Click(object sender, EventArgs e)
        {
            if (_pointLayerInfo?.FeatureClass == null)
                return;
            int idxCodeField = _pointLayerInfo.FeatureClass.FindField(cmbCodeField.SelectedItem.ToString());
            if (idxCodeField < 0)
                return;
            FieldCalculator frm = new FieldCalculator(_pointLayerInfo.FeatureClass.Fields);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtRule.Text = frm.Expression;
                _expression = frm.Expression;
                _length = frm.Length;
            }
        }

        private List<IField> GetFields(string expression)
        {
            List<IField> list = new List<IField>();

            expression = expression.Replace("[#####]", "");
            while (expression.Contains("[") && expression.Contains("]"))
            {
                int sIdx = expression.IndexOf("[", StringComparison.Ordinal);
                int eIdx = expression.IndexOf("]", StringComparison.Ordinal);
                string fieldName = expression.Substring(sIdx + 1, eIdx - sIdx - 1);

                int idx = _pointLayerInfo.FeatureClass.Fields.FindField(fieldName);
                if (idx >= 0)
                    list.Add(_pointLayerInfo.FeatureClass.Fields.Field[idx]);

                expression = expression.Remove(0, eIdx + 1);
            }
            return list;
        }
    }
}
