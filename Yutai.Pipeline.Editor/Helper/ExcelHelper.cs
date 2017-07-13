using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.Office.Interop.Excel;

namespace Yutai.Pipeline.Editor.Helper
{
    public class ExcelHelper
    {
        private Application _app;
        private Workbook _workbook;
        private List<_Worksheet> _worksheetList;

        public ExcelHelper(string path)
        {
            _app = new ApplicationClass();
            _app.DisplayAlerts = false;
            _workbook = _app.Workbooks.Open(path);
            _worksheetList = new List<_Worksheet>();
            foreach (_Worksheet worksheet in _workbook.Worksheets)
            {
                _worksheetList.Add(worksheet);
            }
        }

        public Application App
        {
            get { return _app; }
            set { _app = value; }
        }

        public Workbook Workbook
        {
            get { return _workbook; }
            set { _workbook = value; }
        }

        public List<_Worksheet> WorksheetList
        {
            get { return _worksheetList; }
            set { _worksheetList = value; }
        }

        public Worksheet GetWorksheet(string sheetName)
        {
            foreach (Worksheet worksheet in _workbook.Worksheets)
            {
                if (worksheet.Name == sheetName)
                {
                    return worksheet;
                }
            }
            return null;
        }

        public Dictionary<int, string> GetValueListByRow(Worksheet worksheet, int rowIndex)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            if (worksheet == null)
                return list;
            int columnCount = worksheet.UsedRange.CurrentRegion.Cells.Columns.Count;
            for (int i = 1; i <= columnCount; i++)
            {
                list.Add(i, ((Range)worksheet.Cells[rowIndex, i]).Text.ToString());
            }
            return list;
        }

        public Dictionary<int, string> GetValueListByColumn(Worksheet worksheet, int columnIndex)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            if (worksheet == null)
                return list;
            int rowCount = worksheet.UsedRange.CurrentRegion.Cells.Rows.Count;
            for (int i = 1; i <= rowCount; i++)
            {
                list.Add(i,((Range)worksheet.Cells[i, columnIndex]).Text.ToString());
            }
            return list;
        }

        public string GetRowColumnList(Worksheet worksheet, int rowIndex, int columnIndex)
        {
            if (worksheet == null)
                return null;
            return ((Range)worksheet.Cells[rowIndex, columnIndex]).Text.ToString();
        }

        public int GetRowCount(string sheetName)
        {
            Worksheet pWorksheet = GetWorksheet(sheetName);
            if (pWorksheet == null)
                return 0;
            return pWorksheet.UsedRange.CurrentRegion.Cells.Rows.Count;
        }

        public int GetColumnCount(string sheetName)
        {
            Worksheet pWorksheet = GetWorksheet(sheetName);
            if (pWorksheet == null)
                return 0;
            return pWorksheet.UsedRange.CurrentRegion.Cells.Columns.Count;
        }
    }

    public class RowCellValue
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }
    }
}
