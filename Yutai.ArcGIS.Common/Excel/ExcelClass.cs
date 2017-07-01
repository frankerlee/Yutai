using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Yutai.ArcGIS.Common.Excel
{
    public class ExcelClass : IDisposable
    {
        private _Application _Application_0 = null;
        private _Workbook _Workbook_0 = null;
        private _Worksheet _Worksheet_0 = null;
        private object object_0 = Missing.Value;

        public ExcelClass()
        {
            if (this._Application_0 == null)
            {
                this._Application_0 = new ApplicationClass();
                this._Application_0.DisplayAlerts = (false);
            }
        }

        public void ClearClipboard()
        {
            Clipboard.Clear();
        }

        public void Close(bool bool_0)
        {
            this.ClearClipboard();
            object obj2 = bool_0;
            if (this._Workbook_0 != null)
            {
                this._Workbook_0.Close(obj2, this.object_0, this.object_0);
                this._Workbook_0 = null;
            }
        }

        public void Copy()
        {
            if (this._Worksheet_0 != null)
            {
                try
                {
                    this._Worksheet_0.UsedRange.Select();
                }
                catch
                {
                }
                this._Worksheet_0.UsedRange.Copy(this.object_0);
            }
        }

        public void CopyRow(int int_0)
        {
            object obj2 = Missing.Value;
            Range range = this._Worksheet_0.Rows.get_Range(int_0, obj2) as Range;
            range.EntireRow.Insert((XlInsertShiftDirection) (-4121), obj2);
        }

        public void DeleteRow(int int_0)
        {
            try
            {
                object obj2 = Missing.Value;
                (this._Worksheet_0.Rows.get_Range(int_0, obj2) as Range).Delete((XlDeleteShiftDirection) (-4162));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
        }

        public void Dispose()
        {
            try
            {
                if (this._Application_0 != null)
                {
                    this.Close(false);
                    this._Application_0.Quit();
                    Marshal.ReleaseComObject(this._Application_0);
                    this._Application_0 = null;
                }
            }
            catch (Exception)
            {
            }
        }

        ~ExcelClass()
        {
            try
            {
                if (this._Application_0 != null)
                {
                    this._Application_0.Quit();
                    this._Application_0 = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertCol(int int_0, int int_1)
        {
            object obj2 = Missing.Value;
            string str = ((char) (int_1 + 65)).ToString();
            try
            {
                Range range = this._Worksheet_0.get_Range(str + "1",
                    str + this._Worksheet_0.UsedRange.Rows.Count.ToString());
                range.Select();
                range.Copy(obj2);
                this.InsertCol(int_0, range.ColumnWidth);
            }
            catch
            {
            }
        }

        public void InsertCol(int int_0, object object_1)
        {
            object obj2 = Missing.Value;
            string str = ((char) (int_0 + 65)).ToString();
            Range range = this._Worksheet_0.get_Range(str + "1", str + this._Worksheet_0.UsedRange.Rows.Count.ToString());
            range.ColumnWidth = object_1;
            range.EntireColumn.Insert((XlInsertShiftDirection) (-4121), obj2);
        }

        public void InsertRow(int int_0)
        {
            object obj2 = Missing.Value;
            Range range = this._Worksheet_0.Rows.get_Range(int_0, obj2) as Range;
            range.EntireRow.Insert((XlInsertShiftDirection) (-4121), obj2);
        }

        public void InsertRow(int int_0, int int_1)
        {
            object obj2 = Missing.Value;
            Range range = (Range) this._Worksheet_0.Rows.get_Range(int_1.ToString() + ":" + int_1.ToString(), obj2);
            range.Select();
            range.Copy(obj2);
            this.InsertRow(int_0);
        }

        public Range Merge(string string_0, string string_1)
        {
            if (this._Worksheet_0 != null)
            {
                object obj2 = string_0;
                object obj3 = string_1;
                Range range = this._Worksheet_0.get_Range(obj2, obj3);
                range.MergeCells = (true);
                return range;
            }
            return null;
        }

        public void OpenWorkbook(string string_0)
        {
            this._Workbook_0 = this._Application_0.Workbooks.Open(string_0, this.object_0, this.object_0, this.object_0,
                this.object_0, this.object_0, this.object_0, this.object_0, this.object_0, this.object_0, this.object_0,
                this.object_0, this.object_0, this.object_0, this.object_0);
            if (this._Workbook_0.Worksheets.Count > 0)
            {
                object obj2 = 1;
                this._Worksheet_0 = this._Workbook_0.Worksheets[obj2] as _Worksheet;
            }
        }

        public void PrintPreview(bool bool_0)
        {
            if (this._Workbook_0 != null)
            {
                this._Workbook_0.PrintPreview(bool_0);
            }
        }

        public void Save()
        {
            if (this._Workbook_0 != null)
            {
                this._Workbook_0.Save();
            }
        }

        public void SaveAs(string string_0)
        {
            if (this._Workbook_0 != null)
            {
                this._Workbook_0.SaveAs(string_0, this.object_0, this.object_0, this.object_0, this.object_0,
                    this.object_0, XlSaveAsAccessMode.xlShared, this.object_0, this.object_0, this.object_0,
                    this.object_0, this.object_0);
            }
        }

        public Range SetCellValue(string string_0, object object_1)
        {
            if (this._Worksheet_0 != null)
            {
                object obj2 = string_0;
                Range range = this._Worksheet_0.get_Range(obj2, this.object_0);
                if (range != null)
                {
                    range.Value2 = (object_1);
                }
                return range;
            }
            return null;
        }

        public Range SetCellValue(string string_0, object object_1, string string_1)
        {
            if (this._Worksheet_0 != null)
            {
                object obj2 = string_0;
                Range range = this._Worksheet_0.get_Range(obj2, this.object_0);
                if (range != null)
                {
                    range.NumberFormatLocal = (string_1);
                    range.Value2 = (object_1);
                }
                return range;
            }
            return null;
        }

        public _Application Application
        {
            get { return this._Application_0; }
        }

        public int CurrentWorksheetIndex
        {
            set
            {
                if ((value <= 0) || (value > this._Workbook_0.Worksheets.Count))
                {
                    throw new Exception("索引超出范围");
                }
                this._Worksheet_0 = null;
                object obj2 = value;
                this._Worksheet_0 = this._Workbook_0.Worksheets[obj2] as _Worksheet;
                if (this._Worksheet_0 != null)
                {
                    this._Worksheet_0.Activate();
                }
            }
        }

        public string CurrentWorksheetName
        {
            set
            {
                this._Worksheet_0 = null;
                for (int i = 1; i <= this._Workbook_0.Sheets.Count; i++)
                {
                    _Worksheet worksheet = this._Workbook_0.Worksheets[i] as _Worksheet;
                    if ((worksheet != null) && (worksheet.Name.ToUpper() == value.ToUpper()))
                    {
                        this._Worksheet_0 = worksheet;
                        worksheet.Activate();
                        break;
                    }
                }
            }
        }

        public bool DisplayFullScreen
        {
            get { return this._Application_0.DisplayFullScreen; }
            set { this._Application_0.DisplayFullScreen = value; }
        }

        public bool Visible
        {
            get { return this._Application_0.Visible; }
            set { this._Application_0.Visible = (value); }
        }

        public _Workbook Workbook
        {
            get { return this._Workbook_0; }
        }

        public _Worksheet Worksheet
        {
            get { return this._Worksheet_0; }
            set { this._Worksheet_0 = value; }
        }
    }
}