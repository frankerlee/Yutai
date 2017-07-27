using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Forms.Common
{
    public partial class frmDeletePipeline : DevExpress.XtraEditors.XtraForm
    {
        private List<TreeRecord> _treeRecords;
        private IFeatureLayer _featureLayer;
        private IFeature _lineFeature1;
        private IFeature _lineFeature2;
        private IPoint _linkPoint;

        public frmDeletePipeline(IFeature lineFeature1, IFeature lineFeature2, IPoint linkPoint, IFeatureLayer pFeatureLayer)
        {
            InitializeComponent();
            _lineFeature1 = lineFeature1;
            _lineFeature2 = lineFeature2;
            _linkPoint = linkPoint;
            _featureLayer = pFeatureLayer;
            BuildFeatureListDataSouce();
            gridControl1.DataSource = _treeRecords;
        }

        private void gridView1_CustomRowCellEditForEditing(object sender,
            DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "FName")
            {
                object obj = e.CellValue;
                TreeRecord record = _treeRecords.FirstOrDefault(p => p.FName == obj.ToString());
                if (record != null && record.IsDomain)
                {
                    RepositoryItemComboBox combo = new RepositoryItemComboBox();
                    gridControl1.RepositoryItems.AddRange(new RepositoryItem[] { combo });
                    if (record.DoaminValue != null)
                        combo.Items.AddRange(((List<CodeValuePair>)record.DoaminValue).Select(p => p.Name).ToList());
                    e.RepositoryItem = combo;
                    return;
                }
                if (record != null && record.FieldType == esriFieldType.esriFieldTypeDate)
                {
                    RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();
                    gridControl1.RepositoryItems.AddRange(new RepositoryItem[] { dateEdit });
                    e.RepositoryItem = dateEdit;
                    return;
                }
                RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
                gridControl1.RepositoryItems.AddRange(new RepositoryItem[] { textEdit });
                e.RepositoryItem = textEdit;
            }
        }

        private void BuildFeatureListDataSouce()
        {
            _treeRecords = new List<TreeRecord>();
            if (_lineFeature1 == null) return;
            if (_lineFeature2 == null) return;
            for (int i = 0; i < _lineFeature1.Fields.FieldCount; i++)
            {
                IField pField = _lineFeature1.Fields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeOID ||
                    pField.Type == esriFieldType.esriFieldTypeGeometry ||
                    pField.Type == esriFieldType.esriFieldTypeRaster || pField.Editable == false)
                    continue;
                TreeRecord pRecord = new TreeRecord();
                if (pField.Domain == null)
                {
                    pRecord = new TreeRecord(false, pField.Type, pField.AliasName, _lineFeature1.Value[i], null, i, -1,
                        _lineFeature2.Value[i], "");
                }
                else
                {
                    IDomain pDomain = pField.Domain;
                    if (pDomain is CodedValueDomain)
                    {
                        ICodedValueDomain pCodeDomain = pDomain as ICodedValueDomain;
                        if (pCodeDomain == null) continue;
                        List<CodeValuePair> pairs = new List<CodeValuePair>();
                        for (int j = 0; j < pCodeDomain.CodeCount; j++)
                        {
                            CodeValuePair pair = new CodeValuePair
                            {
                                Name = pCodeDomain.Name[j],
                                Value = pCodeDomain.Value[j]
                            };
                            pairs.Add(pair);
                        }
                        pRecord = new TreeRecord(true, pField.Type, pField.AliasName, _lineFeature1.Value[i], pairs, i,
                            -1, _lineFeature2.Value[i], "");
                    }
                    else
                    {
                        IRangeDomain pRangeDomain = pDomain as IRangeDomain;
                    }
                }
                _treeRecords.Add(pRecord);
            }
        }

        private void btnCopyFirst_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            foreach (TreeRecord treeRecord in _treeRecords)
            {
                treeRecord.MergeValue = treeRecord.FirstValue;
            }
            gridControl1.DataSource = _treeRecords;
        }

        private void btnCopySecond_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            foreach (TreeRecord treeRecord in _treeRecords)
            {
                treeRecord.MergeValue = treeRecord.SecondValue;
            }
            gridControl1.DataSource = _treeRecords;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            try
            {
                if (_lineFeature1 == null) return;
                if (_lineFeature2 == null) return;
                IPolyline firstPolyline = _lineFeature1.Shape as IPolyline;
                IPolyline secondPolyline = _lineFeature2.Shape as IPolyline;
                if (firstPolyline == null) return;
                if (secondPolyline == null) return;
                List<IPoint> points = new List<IPoint>()
                {
                    firstPolyline.FromPoint,
                    firstPolyline.ToPoint,
                    secondPolyline.FromPoint,
                    secondPolyline.ToPoint
                };
                IPoint startPoint = CommonHelper.GetFarthestPoint(firstPolyline, _linkPoint);
                IPoint endPoint = CommonHelper.GetFarthestPoint(secondPolyline, _linkPoint);
                int firstfrompointcount = points.Count(point1 => Math.Abs(firstPolyline.FromPoint.X - point1.X) < 0.01 && Math.Abs(firstPolyline.FromPoint.Y - point1.Y) < 0.01);
                IFeatureClass pFeatureClass = _featureLayer.FeatureClass;
                bool hasZ = FeatureClassUtil.CheckHasZ(pFeatureClass);
                bool hasM = FeatureClassUtil.CheckHasM(pFeatureClass);
                IFeature pFeature = pFeatureClass.CreateFeature();

                startPoint = GeometryHelper.CreatePoint(startPoint.X, startPoint.Y, startPoint.Z, startPoint.M, hasZ, hasM);
                endPoint = GeometryHelper.CreatePoint(endPoint.X, endPoint.Y, endPoint.Z, endPoint.M, hasZ, hasM);
                
                pFeature.Shape = GeometryHelper.CreatePointCollection(startPoint, endPoint, hasZ, hasM) as IPolyline;

                foreach (TreeRecord treeRecord in _treeRecords)
                {
                    if (treeRecord.IsDomain)
                    {
                        List<CodeValuePair> pairs = treeRecord.DoaminValue as List<CodeValuePair>;
                        CodeValuePair pair = pairs.FirstOrDefault(p => p.Name == treeRecord.FirstValue.ToString());
                        pFeature.Value[treeRecord.FieldId] = pair == null ? null : pair.Value;
                    }
                    pFeature.Value[treeRecord.FieldId] = treeRecord.MergeValue;
                }
                pFeature.Store();
                _featureoid = pFeature.OID;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private int _featureoid;

        public int FeatureOID
        { get { return _featureoid; } }


    }
    public class CodeValuePair
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}