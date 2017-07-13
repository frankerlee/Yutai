using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Office.Interop.Excel;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;
using IPoint = ESRI.ArcGIS.Geometry.IPoint;
using WorkspaceHelper = Yutai.Pipeline.Editor.Helper.WorkspaceHelper;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    public partial class FrmImportExcelData : Form
    {
        private const string PREFIXP = "EXCELP";
        private const string PREFIXL = "EXCELL";

        private const int STARTROWIDX = 1;  // excel标题行位置

        private IMap _map;
        private ExcelHelper _excelHelper;
        private Dictionary<int, string> _pointColumnNameList;
        private Dictionary<int, string> _surveyColumnNameList;
        private Dictionary<int, string> _lineColumnNameList;
        private List<FieldMapping> _targetPointFieldMappingList;
        private List<FieldMapping> _targetLineFieldMappingList;
        private IWorkspace _targetWorkspace;
        private ISpatialReference _spatialReference;
        private List<CustomPoint> _pointList;
        private Worksheet _pointWorksheet;
        private Worksheet _lineWorksheet;
        private Worksheet _surveyWorksheet;

        private List<CustomFeatureClass> _pointFeatureClassList;
        private List<CustomFeatureClass> _lineFeatureClassList;

        public FrmImportExcelData(IAppContext context)
        {
            InitializeComponent();
            _map = context.FocusMap;
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtExcelPath.Text = openFileDialog1.FileName;
                _excelHelper = new ExcelHelper(txtExcelPath.Text);
                ComboBoxHelper.AddItemsFromList(new List<_Worksheet>(_excelHelper.WorksheetList), cmbPointSheet, "Name", "Name");
                if (!chkHasCoordInfo.Checked)
                    ComboBoxHelper.AddItemsFromList(new List<_Worksheet>(_excelHelper.WorksheetList), cmbSurveySheet, "Name", "Name");
                ComboBoxHelper.AddItemsFromList(new List<_Worksheet>(_excelHelper.WorksheetList), cmbLineSheet, "Name", "Name");
            }
        }

        private void cmbPointSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbPointSheet.DisplayMember))
                    return;
                _pointWorksheet = cmbPointSheet.SelectedItem as Worksheet;
                if (_pointWorksheet == null)
                    return;
                _pointColumnNameList = _excelHelper.GetValueListByRow(_pointWorksheet, 1);
                if (_pointColumnNameList == null || _pointColumnNameList.Count <= 0)
                    return;
                SetPointCombobox();
                LoadPointFieldMapping();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void cmbSurveySheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbSurveySheet.DisplayMember))
                    return;
                _surveyWorksheet = cmbSurveySheet.SelectedItem as Worksheet;
                if (_surveyWorksheet == null)
                    return;
                _surveyColumnNameList = _excelHelper.GetValueListByRow(_surveyWorksheet, 1);
                if (_surveyColumnNameList == null || _surveyColumnNameList.Count <= 0)
                    return;
                SetSurveyCombobox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void cmbLineSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbLineSheet.DisplayMember))
                    return;
                _lineWorksheet = cmbLineSheet.SelectedItem as Worksheet;
                if (_lineWorksheet == null)
                    return;
                _lineColumnNameList = _excelHelper.GetValueListByRow(_lineWorksheet, 1);
                if (_lineColumnNameList == null || _lineColumnNameList.Count <= 0)
                    return;
                SetLineCombobox();
                LoadLineFieldMapping();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SetPointCombobox()
        {
            ComboBoxHelper.AddItemsFromList(_pointColumnNameList.ToList(), cmbNoField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_pointColumnNameList.ToList(), cmbXField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_pointColumnNameList.ToList(), cmbYField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_pointColumnNameList.ToList(), cmbZField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_pointColumnNameList.ToList(), cmbPointGroupField, "Value", "Key");
        }

        private void SetSurveyCombobox()
        {
            ComboBoxHelper.AddItemsFromList(_surveyColumnNameList.ToList(), cmbSurveyNoField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_surveyColumnNameList.ToList(), cmbSurveyXField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_surveyColumnNameList.ToList(), cmbSurveyYField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_surveyColumnNameList.ToList(), cmbSurveyZField, "Value", "Key");
        }

        private void SetLineCombobox()
        {
            ComboBoxHelper.AddItemsFromList(_lineColumnNameList.ToList(), cmbStartNoField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_lineColumnNameList.ToList(), cmbEndNoField, "Value", "Key");
            ComboBoxHelper.AddItemsFromList(_lineColumnNameList.ToList(), cmbLineGroupField, "Value", "Key");
        }

        private void LoadPointFieldMapping()
        {
            _targetPointFieldMappingList = new List<FieldMapping>();
            foreach (KeyValuePair<int, string> pair in _pointColumnNameList)
            {
                _targetPointFieldMappingList.Add(new FieldMapping()
                {
                    Name = pair.Value,
                    AliasName = pair.Value,
                    Length = 200,
                    Type = esriFieldType.esriFieldTypeString,
                    OriginFieldName = pair.Value,
                    IndexOriginField = pair.Key
                });
            }
        }

        private void LoadLineFieldMapping()
        {
            _targetLineFieldMappingList = new List<FieldMapping>();
            foreach (KeyValuePair<int, string> pair in _lineColumnNameList)
            {
                _targetLineFieldMappingList.Add(new FieldMapping()
                {
                    Name = pair.Value,
                    AliasName = pair.Value,
                    Length = 200,
                    Type = esriFieldType.esriFieldTypeString,
                    OriginFieldName = pair.Value,
                    IndexOriginField = pair.Key
                });
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ISpatialReferenceDialog2 pSpatialReferenceDialog2 = new SpatialReferenceDialogClass();
            _spatialReference = pSpatialReferenceDialog2.DoModalCreate(true, true, true, 0);
            if (_spatialReference == null)
                return; IWorkspaceEdit pWorkspaceEdit = _targetWorkspace as IWorkspaceEdit;
            if (pWorkspaceEdit == null)
                return;
            pWorkspaceEdit.StartEditing(true);
            pWorkspaceEdit.StartEditOperation();
            try
            {
                _pointList = new List<CustomPoint>();
                _pointFeatureClassList = new List<CustomFeatureClass>();
                _lineFeatureClassList = new List<CustomFeatureClass>();
                if (chkHasCoordInfo.Checked == false)
                    GetSurveyPointList();
                ExcelToPointFeatureClass();
                ExcelToLineFeatureClass();
                foreach (CustomFeatureClass customFeatureClass in _pointFeatureClassList)
                {
                    WorkspaceHelper.LoadFeatureClass(_map, customFeatureClass.FeatureClass);
                }
                foreach (CustomFeatureClass customFeatureClass in _lineFeatureClassList)
                {
                    WorkspaceHelper.LoadFeatureClass(_map, customFeatureClass.FeatureClass);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);

                foreach (Process p in Process.GetProcessesByName("Excel"))
                {
                    if (string.IsNullOrEmpty(p.MainWindowTitle))
                    {
                        p.Kill();
                    }
                }
            }
        }

        private void btnSelectGDB_Click(object sender, EventArgs e)
        {
            _targetWorkspace = GxDialogHelper.SelectWorkspaceDialog();
            if (_targetWorkspace == null)
                return;
            txtGDBPath.Text = _targetWorkspace.PathName;
        }

        private IFeatureClass CreateFeatureClass(string tableName, esriGeometryType type, List<FieldMapping> fieldMappings)
        {
            IWorkspaceEdit pWorkspaceEdit = _targetWorkspace as IWorkspaceEdit;
            if (pWorkspaceEdit == null)
                return null;
            if (pWorkspaceEdit.IsBeingEdited())
            {
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
            }
            IFeatureClass pFeatureClass = WorkspaceHelper.CreateFeatureClass(_targetWorkspace, tableName, type, _spatialReference);
            FeatureClassUtil.AddZ(pFeatureClass);
            FeatureClassUtil.AddM(pFeatureClass);
            foreach (FieldMapping fieldMapping in fieldMappings)
            {
                switch (fieldMapping.Type)
                {
                    case esriFieldType.esriFieldTypeInteger:
                        FeatureClassUtil.AddIntField(pFeatureClass, fieldMapping.Name, fieldMapping.Length, null, fieldMapping.AliasName);
                        break;
                    case esriFieldType.esriFieldTypeDouble:
                        FeatureClassUtil.AddDoubleField(pFeatureClass, fieldMapping.Name, fieldMapping.Length, fieldMapping.Precision, null, fieldMapping.AliasName);
                        break;
                    case esriFieldType.esriFieldTypeString:
                        FeatureClassUtil.AddStringField(pFeatureClass, fieldMapping.Name, fieldMapping.Length, null, fieldMapping.AliasName);
                        break;
                    case esriFieldType.esriFieldTypeDate:
                        FeatureClassUtil.AddDateField(pFeatureClass, fieldMapping.Name, fieldMapping.AliasName);
                        break;
                    default:
                        break;
                }
                fieldMapping.IndexTargetField = pFeatureClass.FindField(fieldMapping.Name);
            }

            return pFeatureClass;
        }

        private void GetSurveyPointList()
        {
            _pointList = new List<CustomPoint>();
            int pointRowCount = _excelHelper.GetRowCount(_surveyWorksheet.Name);
            int idxNoColumn = Convert.ToInt32(cmbSurveyNoField.SelectedValue);
            int idxXColumn = Convert.ToInt32(cmbSurveyXField.SelectedValue);
            int idxYColumn = Convert.ToInt32(cmbSurveyYField.SelectedValue);
            int idxZColumn = Convert.ToInt32(cmbSurveyZField.SelectedValue);

            for (int i = 2; i < pointRowCount; i++)
            {
                string no = ((Range)_surveyWorksheet.Cells[i, idxNoColumn]).Value2.ToString().Trim();
                object sX = ((Range)_surveyWorksheet.Cells[i, idxXColumn]).Value2;
                object sY = ((Range)_surveyWorksheet.Cells[i, idxYColumn]).Value2;
                object sZ = ((Range)_surveyWorksheet.Cells[i, idxZColumn]).Value2;
                if (sX == null || string.IsNullOrEmpty(sX.ToString()) || sY == null || string.IsNullOrEmpty(sY.ToString()) || sZ == null || string.IsNullOrEmpty(sZ.ToString()))
                    continue;
                double x = Convert.ToDouble(sX);
                double y = Convert.ToDouble(sY);
                double z = Convert.ToDouble(sZ);
                IPoint point;
                if (checkBoxConvertCoordinateSystem.Checked)
                    point = GeometryHelper.CreatePoint(y, x, z, 0, true, true);
                else
                    point = GeometryHelper.CreatePoint(x, y, z, 0, true, true);
                _pointList.Add(new CustomPoint
                {
                    No = no,
                    Point = point
                });
            }
        }

        private void ExcelToPointFeatureClass()
        {
            IWorkspaceEdit pWorkspaceEdit = _targetWorkspace as IWorkspaceEdit;
            if (pWorkspaceEdit == null)
                return;
            int pointRowCount = _excelHelper.GetRowCount(_pointWorksheet.Name);
            int idxNoColumn = Convert.ToInt32(cmbNoField.SelectedValue);
            int idxXColumn = Convert.ToInt32(cmbXField.SelectedValue);
            int idxYColumn = Convert.ToInt32(cmbYField.SelectedValue);
            int idxZColumn = Convert.ToInt32(cmbZField.SelectedValue);
            int idxGroupColumn = Convert.ToInt32(cmbPointGroupField.SelectedValue);
            
            for (int i = 2; i <= pointRowCount; i++)
            {
                try
                {
                    string groupName;
                    if (checkBoxIsGroup.Checked)
                        groupName = ((Range)_pointWorksheet.Cells[i, idxGroupColumn]).Value2.ToString().Trim();
                    else
                        groupName = _pointWorksheet.Name.Trim();
                    CustomFeatureClass pCustomFeatureClass = _pointFeatureClassList.FirstOrDefault(c => c.AName == groupName);
                    if (pCustomFeatureClass == null)
                    {
                        try
                        {
                            IFeatureClass pFeatureClass = CreateFeatureClass($"{groupName}_Point", esriGeometryType.esriGeometryPoint, _targetPointFieldMappingList);
                            pCustomFeatureClass = new CustomFeatureClass
                            {
                                AName = groupName,
                                FeatureClass = pFeatureClass
                            };
                            _pointFeatureClassList.Add(pCustomFeatureClass);
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"{i};{e.Message}({groupName})");
                        }
                    }
                    if (pWorkspaceEdit.IsBeingEdited() == false)
                    {
                        pWorkspaceEdit.StartEditing(true);
                        pWorkspaceEdit.StartEditOperation();
                    }
                    IFeatureCursor featureCursor = pCustomFeatureClass.FeatureClass.Insert(true);
                    IFeatureBuffer newFeature = pCustomFeatureClass.FeatureClass.CreateFeatureBuffer();
                    IPoint point = null;
                    string no = ((Range)_pointWorksheet.Cells[i, idxNoColumn]).Value2.ToString().Trim();
                    if (chkHasCoordInfo.Checked)
                    {
                        object sX = ((Range)_pointWorksheet.Cells[i, idxXColumn]).Value2;
                        object sY = ((Range)_pointWorksheet.Cells[i, idxYColumn]).Value2;
                        object sZ = ((Range)_pointWorksheet.Cells[i, idxZColumn]).Value2;
                        if (string.IsNullOrEmpty(sX?.ToString()) || string.IsNullOrEmpty(sY?.ToString()) || string.IsNullOrEmpty(sZ?.ToString()))
                            continue;
                        double x = Convert.ToDouble(sX);
                        double y = Convert.ToDouble(sY);
                        double z = Convert.ToDouble(sZ);
                        if (checkBoxConvertCoordinateSystem.Checked)
                            point = GeometryHelper.CreatePoint(y, x, z, 0, true, true);
                        else
                            point = GeometryHelper.CreatePoint(x, y, z, 0, true, true);
                        _pointList.Add(new CustomPoint
                        {
                            No = no,
                            Point = point
                        });
                    }
                    else
                    {
                        CustomPoint customPoint = _pointList.FirstOrDefault(c => c.No == no);
                        if (customPoint != null)
                            point = customPoint.Point;
                    }
                    if (point != null)
                        newFeature.Shape = point;
                    foreach (FieldMapping fieldMapping in _targetPointFieldMappingList)
                    {
                        try
                        {
                            if (fieldMapping.IndexOriginField <= 0)
                                continue;
                            object obj = ((Range)_pointWorksheet.Cells[i, fieldMapping.IndexOriginField]).Value2;
                            //string str = ((Range)_pointWorksheet.Cells[i, fieldMapping.IndexOriginField]).Value2.ToString();
                            newFeature.Value[fieldMapping.IndexTargetField] = obj == null || obj.ToString() == "<空>" || obj.ToString() == "" ? null : obj;
                            if (point != null)
                                if (fieldMapping.OriginFieldName == ((KeyValuePair<int, string>)(cmbXField.SelectedItem)).Value)
                                {
                                    newFeature.Value[fieldMapping.IndexTargetField] = point.X.ToString("#0.0000");
                                }
                                else if (fieldMapping.OriginFieldName == ((KeyValuePair<int, string>)(cmbYField.SelectedItem)).Value)
                                {
                                    newFeature.Value[fieldMapping.IndexTargetField] = point.Y.ToString("#0.0000");
                                }
                                else if (fieldMapping.OriginFieldName == ((KeyValuePair<int, string>)(cmbZField.SelectedItem)).Value)
                                {
                                    newFeature.Value[fieldMapping.IndexTargetField] = point.Z.ToString("#0.0000");
                                }
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"{i};{e.Message}");
                        }
                    }
                    featureCursor.InsertFeature(newFeature);
                    Marshal.ReleaseComObject(featureCursor);
                }
                catch (Exception e)
                {
                    throw new Exception($"{i};{e.Message}");
                }
            }
        }

        private void ExcelToLineFeatureClass()
        {
            if (cmbStartNoField.SelectedValue == null || cmbEndNoField.SelectedValue == null || cmbLineGroupField.SelectedValue == null)
                return;
            IWorkspaceEdit pWorkspaceEdit = _targetWorkspace as IWorkspaceEdit;
            if (pWorkspaceEdit == null)
                return;
            int lineRowCount = _excelHelper.GetRowCount(_lineWorksheet.Name);
            int idxSNoColumn = Convert.ToInt32(cmbStartNoField.SelectedValue);
            int idxENoColumn = Convert.ToInt32(cmbEndNoField.SelectedValue);
            int idxGroupColumn = Convert.ToInt32(cmbLineGroupField.SelectedValue);
            
            for (int i = 2; i <= lineRowCount; i++)
            {
                try
                {
                    string groupName;

                    if (checkBoxIsGroup.Checked)
                        groupName = ((Range)_lineWorksheet.Cells[i, idxGroupColumn]).Value2.ToString().Trim();
                    else
                        groupName = _pointWorksheet.Name.Trim();
                    CustomFeatureClass pCustomFeatureClass = _lineFeatureClassList.FirstOrDefault(c => c.AName == groupName);
                    if (pCustomFeatureClass == null)
                    {
                        try
                        {
                            IFeatureClass pFeatureClass = CreateFeatureClass(string.Format("{0}_Line", groupName), esriGeometryType.esriGeometryPolyline, _targetLineFieldMappingList);
                            pCustomFeatureClass = new CustomFeatureClass
                            {
                                AName = groupName,
                                FeatureClass = pFeatureClass
                            };
                            _lineFeatureClassList.Add(pCustomFeatureClass);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format("{0};{1}({2})", i, e.Message, groupName));
                        }
                    }
                    if (pWorkspaceEdit.IsBeingEdited() == false)
                    {
                        pWorkspaceEdit.StartEditing(true);
                        pWorkspaceEdit.StartEditOperation();
                    }
                    IFeatureCursor featureCursor = pCustomFeatureClass.FeatureClass.Insert(true);
                    IFeatureBuffer newFeature = pCustomFeatureClass.FeatureClass.CreateFeatureBuffer();
                    foreach (FieldMapping fieldMapping in _targetLineFieldMappingList)
                    {
                        try
                        {
                            if (fieldMapping.IndexOriginField <= 0)
                            {
                                continue;
                            }

                            object str = ((Range)_lineWorksheet.Cells[i, fieldMapping.IndexOriginField]).Value2;
                            if (str == null || string.IsNullOrEmpty(str.ToString()) || str.ToString() == "#N/A")
                                continue;
                            newFeature.Value[fieldMapping.IndexTargetField] = str.ToString() == "<空>" || str.ToString() == "" ? null : str;
                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format("{0};{1}", i, e.Message));
                        }
                    }
                    object sno = ((Range)_lineWorksheet.Cells[i, idxSNoColumn]).Value2;
                    object eno = ((Range)_lineWorksheet.Cells[i, idxENoColumn]).Value2;
                    if (sno == null || string.IsNullOrEmpty(sno.ToString().Trim()) || eno == null || string.IsNullOrEmpty(eno.ToString().Trim()))
                        continue;
                    IPolyline polyline = new PolylineClass();
                    IZAware pZAware = polyline as IZAware;
                    pZAware.ZAware = true;
                    IMAware pMAware = polyline as IMAware;
                    pMAware.MAware = true;
                    CustomPoint startCustomPoint = _pointList.FirstOrDefault(c => c.No == sno.ToString().Trim());
                    if (startCustomPoint == null)
                        continue;
                    polyline.FromPoint = startCustomPoint.Point;
                    CustomPoint endCustomPoint = _pointList.FirstOrDefault(c => c.No == eno.ToString().Trim());
                    if (endCustomPoint == null)
                        continue;
                    polyline.ToPoint = endCustomPoint.Point;
                    newFeature.Shape = polyline;
                    featureCursor.InsertFeature(newFeature);
                    Marshal.ReleaseComObject(featureCursor);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("{0};{1}", i, e.Message));
                }
            }
        }

        private void checkBoxIsGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsGroup.Checked)
            {
                cmbPointGroupField.Enabled = true;
                cmbLineGroupField.Enabled = true;
            }
            else
            {
                cmbPointGroupField.Enabled = false;
                cmbLineGroupField.Enabled = false;
            }
        }

        private void chkHasCoordInfo_CheckedChanged(object sender, EventArgs e)
        {
            panelSurvey.Enabled = !chkHasCoordInfo.Checked;
            if (!chkHasCoordInfo.Checked)
            { ComboBoxHelper.AddItemsFromList(new List<_Worksheet>(_excelHelper.WorksheetList), cmbSurveySheet, "Name", "Name"); }
        }
    }
}
