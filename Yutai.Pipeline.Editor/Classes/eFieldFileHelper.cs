using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ClosedXML.Excel;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Emuns;

namespace Yutai.Pipeline.Editor.Classes
{
    public class eFieldFileHelper
    {
        public eFieldFileHelper()
        {
        }

        public eFieldFileHelper(IWorkspace targetWks, string basename, string xlsxfile, string xyzfile,
            SurveyCoordType coordType, string surveyer, DateTime surveyDate)
        {
            TargetWks = targetWks;
            Basename = basename;
            Xlsxfile = xlsxfile;
            Xyzfile = xyzfile;
            CoordType = coordType;
            Surveyer = surveyer;
            SurveyDate = surveyDate;
        }

        public string Basename { get; set; } = "";

        public SurveyCoordType CoordType { get; set; }

        public DateTime SurveyDate { get; set; }

        public string Surveyer { get; set; }

        public IWorkspace TargetWks { get; set; }

        public string Xlsxfile { get; set; } = "";

        public string Xyzfile { get; set; } = "";

        private IPointCollection CreateLine(IPointCollection pnts, DataRow startRow, DataRow[] lineRows)
        {
            IPointCollection pointCollection;
            DataRow nextRow = null;
            var surveyNo = startRow["SurveyNO"].ToString().Trim();
            var dataRowArray = lineRows;
            for (var i = 0; i < dataRowArray.Length; i++)
            {
                var dataRow = dataRowArray[i];
                if (dataRow["SurveyNO"].ToString().Trim() != surveyNo)
                    if ((dataRow["LinkedSurveyNO"] != null) && (dataRow["LinkedSurveyNO"].ToString() != string.Empty))
                        if (dataRow["LinkedSurveyNO"].ToString().Trim() == surveyNo)
                        {
                            IPoint pPoint = new PointClass();
                            pPoint.PutCoords((double) dataRow["CX"], (double) dataRow["CY"]);
                            object rComRefCallLocal8 = Missing.Value;
                            object rComRefCallLocal9 = Missing.Value;
                            pnts.AddPoint(pPoint, ref rComRefCallLocal8, ref rComRefCallLocal9);
                            nextRow = dataRow;
                            break;
                        }
            }
            if (nextRow == null)
            {
                pointCollection = pnts;
            }
            else
            {
                CreateLine(pnts, nextRow, lineRows);
                pointCollection = pnts;
            }
            return pointCollection;
        }

        private DataTable JoinDataTables(DataTable dtA, DataTable dtB, params Func<DataRow, DataRow, bool>[] joinOn)
        {
            var dtResult = new DataTable();
            foreach (DataColumn col in dtA.Columns)
                if (dtResult.Columns[col.ColumnName] == null)
                    dtResult.Columns.Add(col.ColumnName, col.DataType);
            foreach (DataColumn column in dtB.Columns)
                if (dtResult.Columns[column.ColumnName] == null)
                    dtResult.Columns.Add(column.ColumnName, column.DataType);
            foreach (DataRow row in dtA.Rows)
                foreach (var fromRow in dtB.AsEnumerable().Where(rowB =>
                {
                    bool flag;
                    var cSu0024U003Cu003E8Localsc = joinOn;
                    var num = 0;
                    while (true)
                        if (num >= cSu0024U003Cu003E8Localsc.Length)
                        {
                            flag = true;
                            break;
                        }
                        else if (cSu0024U003Cu003E8Localsc[num](row, rowB))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    return flag;
                }))
                {
                    var insertRow = dtResult.NewRow();
                    foreach (DataColumn colA in dtA.Columns)
                        insertRow[colA.ColumnName] = row[colA.ColumnName];
                    foreach (DataColumn colB in dtB.Columns)
                        insertRow[colB.ColumnName] = fromRow[colB.ColumnName];
                    dtResult.Rows.Add(insertRow);
                }
            return dtResult;
        }

        public bool StartImport()
        {
            int kk;
            string pFieldName;
            int pFieldIndex;
            bool flag;
            DataRow[] dataRowArray;
            int l;
            var xyzTableName = string.Concat("XYZ_", Basename);
            IFields pFields = new FieldsClass();
            var pFieldsEdit = pFields as IFieldsEdit;
            var xyzTable = new DataTable("XYZ");
            xyzTable.Columns.Add(new DataColumn("SurveyNO", typeof(string)));
            xyzTable.Columns.Add(new DataColumn("CX", typeof(double)));
            xyzTable.Columns.Add(new DataColumn("CY", typeof(double)));
            xyzTable.Columns.Add(new DataColumn("CZ", typeof(double)));
            xyzTable.Columns.Add(new DataColumn("PNTCODE", typeof(string)));
            IFieldEdit pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "SurveyNO";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "CX";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "CY";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "CZ";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "PNTCODE";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "Surveyer";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "PNTCODE";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Length_2 = 10;
            pFieldsEdit.AddField(pFieldEdit);
            if (new FileInfo(Xyzfile).Exists)
            {
                var sr = new StreamReader(Xyzfile, Encoding.GetEncoding("gb2312"));
                var pLine = sr.ReadLine().Trim();
                DataRow pRow;
                while (true)
                {
                    var str = sr.ReadLine();
                    pLine = str;
                    if (str == null)
                        break;
                    if (pLine == null ? false : pLine.Trim().Length != 0)
                    {
                        pLine = pLine.Trim();
                        var pSubs = pLine.Split(',');
                        pRow = xyzTable.NewRow();
                        pRow["SurveyNo"] = pSubs[0];
                        switch (CoordType)
                        {
                            case SurveyCoordType.NOXY:
                            {
                                pRow["CX"] = double.Parse(pSubs[1]);
                                pRow["CY"] = double.Parse(pSubs[2]);
                                break;
                            }
                            case SurveyCoordType.NOYX:
                            {
                                pRow["CX"] = double.Parse(pSubs[2]);
                                pRow["CY"] = double.Parse(pSubs[1]);
                                break;
                            }
                            case SurveyCoordType.NOCODEXY:
                            {
                                pRow["CX"] = double.Parse(pSubs[2]);
                                pRow["CY"] = double.Parse(pSubs[3]);
                                pRow["PNTCODE"] = pSubs[1];
                                break;
                            }
                            case SurveyCoordType.NOCODEYX:
                            {
                                pRow["CX"] = double.Parse(pSubs[3]);
                                pRow["CY"] = double.Parse(pSubs[2]);
                                pRow["PNTCODE"] = pSubs[1];
                                break;
                            }
                            case SurveyCoordType.NOXYZ:
                            {
                                pRow["CX"] = double.Parse(pSubs[1]);
                                pRow["CY"] = double.Parse(pSubs[2]);
                                pRow["CZ"] = double.Parse(pSubs[3]);
                                break;
                            }
                            case SurveyCoordType.NOYXZ:
                            {
                                pRow["CX"] = double.Parse(pSubs[2]);
                                pRow["CY"] = double.Parse(pSubs[1]);
                                pRow["CZ"] = double.Parse(pSubs[3]);
                                break;
                            }
                            case SurveyCoordType.NOCODEXYZ:
                            {
                                pRow["PNTCODE"] = pSubs[1];
                                pRow["CX"] = double.Parse(pSubs[2]);
                                pRow["CY"] = double.Parse(pSubs[3]);
                                pRow["CZ"] = double.Parse(pSubs[4]);
                                break;
                            }
                            case SurveyCoordType.NOCODEYXZ:
                            {
                                pRow["PNTCODE"] = pSubs[1];
                                pRow["CX"] = double.Parse(pSubs[3]);
                                pRow["CY"] = double.Parse(pSubs[2]);
                                pRow["CZ"] = double.Parse(pSubs[4]);
                                break;
                            }
                            default:
                            {
                                flag = false;
                                return flag;
                            }
                        }
                        xyzTable.Rows.Add(pRow);
                    }
                }
                sr.Close();
                var xlsxTable = new DataTable("XLSXTABLE");
                var ws = new XLWorkbook(Xlsxfile).Worksheet("Data");
                var firstRowUsed = ws.FirstRowUsed();
                ws.LastRowUsed();
                var firstColumnUsed = ws.FirstColumnUsed();
                ws.LastColumnUsed();
                firstColumnUsed.CellCount();
                var compcodeIndex = 0;
                var k = 0;
                foreach (var xlCell in firstRowUsed.Cells())
                {
                    var columnName = xlCell.GetValue<string>().Trim();
                    columnName = columnName.Replace(" ", "");
                    var column = new DataColumn(columnName, typeof(string))
                    {
                        AllowDBNull = true
                    };
                    xlsxTable.Columns.Add(column);
                    if (columnName == "CompCode")
                        compcodeIndex = k;
                    k++;
                    if (!(columnName == "SurveyNO"))
                    {
                        pFieldEdit = new FieldClass();
                        pFieldEdit.Name_2 = columnName;
                        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                        pFieldsEdit.AddField(pFieldEdit);
                    }
                }
                var firstvalueAddress = firstRowUsed.RowBelow().FirstCell().Address;
                var lastvalueAddress = ws.LastCellUsed().Address;
                int j;
                foreach (
                    var xlRangeRow in ws.Range(firstvalueAddress, lastvalueAddress).Rows((Func<IXLRangeRow, bool>) null)
                )
                {
                    j = 0;
                    pRow = xlsxTable.NewRow();
                    foreach (var xLCell in xlRangeRow.Cells())
                    {
                        if (j == compcodeIndex)
                            pRow[j] = xLCell.GetString().PadLeft(4, '0');
                        else
                            pRow[j] = xLCell.GetString();
                        j++;
                    }
                    xlsxTable.Rows.Add(pRow);
                }
                var geoFields = new FeatureClassDescriptionClass().RequiredFields;
                var geoFieldsEdit = geoFields as IFieldsEdit;
                var xyFields = new ObjectClassDescriptionClass().RequiredFields;
                var xyFieldsEdit = xyFields as IFieldsEdit;
                for (var ii = 0; ii < pFields.FieldCount; ii++)
                {
                    xyFieldsEdit.AddField(pFields.get_Field(ii));
                    geoFieldsEdit.AddField(pFields.get_Field(ii));
                }
                var esriTable = GSCSUtilsGen.Instance.CreateTable((IWorkspace2) TargetWks, xyzTableName, xyFields);
                var pointFC = GSCSUtilsGen.Instance.CreateFeatureClass((IWorkspace2) TargetWks, null,
                    string.Concat("PNT_", Basename), geoFields, null, null, "", esriGeometryType.esriGeometryPoint);
                var lineFC = GSCSUtilsGen.Instance.CreateFeatureClass((IWorkspace2) TargetWks, null,
                    string.Concat("LINE_", Basename), geoFields, null, null, "", esriGeometryType.esriGeometryPolyline);
                var areaFC = GSCSUtilsGen.Instance.CreateFeatureClass((IWorkspace2) TargetWks, null,
                    string.Concat("AREA_", Basename), geoFields, null, null, "", esriGeometryType.esriGeometryPolygon);
                var dataTable = xlsxTable;
                var dataTable1 = xyzTable;
                Func<DataRow, DataRow, bool>[] funcArray =
                {
                    (rowA, rowB) => rowA.Field<string>("SurveyNO") == rowB.Field<string>("SurveyNO")
                };
                var vResult = JoinDataTables(dataTable, dataTable1, funcArray);
                var pWorkspaceEdit = TargetWks as IWorkspaceEdit;
                pWorkspaceEdit.StartEditing(false);
                pWorkspaceEdit.StartEditOperation();
                var pCount = vResult.Rows.Count;
                var pColCount = vResult.Columns.Count;
                var pRowIndexes = new int[pColCount];
                var pPntFieldIndexes = new int[pColCount];
                var pLineFieldIndexes = new int[pColCount];
                var pAreaFieldIndexes = new int[pColCount];
                var surveyIndex = esriTable.FindField("Surveyer");
                var surveyDateIndex = esriTable.FindField("SurveyDate");
                var surveyPntIndex = pointFC.FindField("Surveyer");
                var surveyDatePntIndex = pointFC.FindField("SurveyDate");
                int i;
                for (i = 0; i < vResult.Columns.Count; i++)
                {
                    pRowIndexes[i] = esriTable.FindField(vResult.Columns[i].ColumnName);
                    pPntFieldIndexes[i] = pointFC.FindField(vResult.Columns[i].ColumnName);
                    pLineFieldIndexes[i] = lineFC.FindField(vResult.Columns[i].ColumnName);
                    pAreaFieldIndexes[i] = areaFC.FindField(vResult.Columns[i].ColumnName);
                }
                DataRow dataRow;
                for (i = 0; i < pCount; i++)
                {
                    dataRow = vResult.Rows[i];
                    var esriRow = esriTable.CreateRow();
                    var pFeature = pointFC.CreateFeature();
                    IPoint pnt = new PointClass();
                    pnt.PutCoords((double) dataRow["CX"], (double) dataRow["CY"]);
                    pFeature.Shape = pnt;
                    for (j = 0; j < pColCount; j++)
                        if (pRowIndexes[j] >= 0)
                        {
                            esriRow.set_Value(pRowIndexes[j], dataRow[j]);
                            pFeature.set_Value(pPntFieldIndexes[j], dataRow[j]);
                        }
                    esriRow.set_Value(surveyIndex, Surveyer);
                    esriRow.set_Value(surveyDateIndex, SurveyDate);
                    pFeature.set_Value(surveyPntIndex, Surveyer);
                    pFeature.set_Value(surveyDatePntIndex, SurveyDate);
                    esriRow.Store();
                    pFeature.Store();
                }
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
                pWorkspaceEdit.StartEditing(false);
                pWorkspaceEdit.StartEditOperation();
                var rows = vResult.Select("PointType='LinePoint' AND LinkedSurveyNO =''", "RecordID");
                var lineRows = vResult.Select("PointType='LinePoint'");
                IPointCollection pnts;
                IPoint pPnt;
                IPointCollection pPnts;
                IFeature pLineFeature;
                if (rows == null ? false : rows.Length > 0)
                {
                    dataRowArray = rows;
                    for (l = 0; l < dataRowArray.Length; l++)
                    {
                        dataRow = dataRowArray[l];
                        pnts = new PolylineClass();
                        pPnt = new PointClass();
                        pPnt.PutCoords((double) dataRow["CX"], (double) dataRow["CY"]);
                        object r__ComRefCallLocal0 = Missing.Value;
                        object r__ComRefCallLocal1 = Missing.Value;
                        pnts.AddPoint(pPnt, ref r__ComRefCallLocal0, ref r__ComRefCallLocal1);
                        pPnts = CreateLine(pnts, dataRow, lineRows);
                        if (pPnts == null ? false : pPnts.PointCount > 1)
                        {
                            pLineFeature = lineFC.CreateFeature();
                            for (kk = 0; kk < vResult.Columns.Count; kk++)
                            {
                                pFieldName = vResult.Columns[kk].ColumnName;
                                pFieldIndex = pLineFeature.Fields.FindField(pFieldName);
                                if (pFieldIndex > 0)
                                    pLineFeature.set_Value(pFieldIndex, dataRow[kk]);
                            }
                            pLineFeature.Shape = pPnts as IGeometry;
                            pLineFeature.Store();
                        }
                    }
                }
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
                pWorkspaceEdit.StartEditing(false);
                pWorkspaceEdit.StartEditOperation();
                rows = vResult.Select("PointType='AreaPoint' AND LinkedSurveyNO =''", "RecordID");
                lineRows = vResult.Select("PointType='AreaPoint'");
                if (rows == null ? false : rows.Length > 0)
                {
                    dataRowArray = rows;
                    for (l = 0; l < dataRowArray.Length; l++)
                    {
                        dataRow = dataRowArray[l];
                        pnts = new PolygonClass();
                        pPnt = new PointClass();
                        pPnt.PutCoords((double) dataRow["CX"], (double) dataRow["CY"]);
                        object r__ComRefCallLocal2 = Missing.Value;
                        object r__ComRefCallLocal3 = Missing.Value;
                        pnts.AddPoint(pPnt, ref r__ComRefCallLocal2, ref r__ComRefCallLocal3);
                        pPnts = CreateLine(pnts, dataRow, lineRows);
                        if (pPnts == null ? false : pPnts.PointCount > 2)
                        {
                            pLineFeature = areaFC.CreateFeature();
                            for (kk = 0; kk < vResult.Columns.Count; kk++)
                            {
                                pFieldName = vResult.Columns[kk].ColumnName;
                                pFieldIndex = pLineFeature.Fields.FindField(pFieldName);
                                if (pFieldIndex > 0)
                                    pLineFeature.set_Value(pFieldIndex, dataRow[kk]);
                            }
                            pPnt = new PointClass();
                            pPnt.PutCoords((double) dataRow["CX"], (double) dataRow["CY"]);
                            object r__ComRefCallLocal4 = Missing.Value;
                            object r__ComRefCallLocal5 = Missing.Value;
                            pnts.AddPoint(pPnt, ref r__ComRefCallLocal4, ref r__ComRefCallLocal5);
                            pLineFeature.Shape = (IGeometry) pPnts;
                            pLineFeature.Store();
                        }
                    }
                }
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private bool ValidationParameters()
        {
            bool flag;
            if (TargetWks == null)
                flag = false;
            else if (string.IsNullOrWhiteSpace(Xlsxfile))
                flag = false;
            else if (string.IsNullOrWhiteSpace(Xyzfile))
                flag = false;
            else if (!string.IsNullOrWhiteSpace(Basename))
                flag = !string.IsNullOrWhiteSpace(Surveyer) ? true : false;
            else
                flag = false;
            return flag;
        }
    }
}