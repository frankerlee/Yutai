using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Font = System.Drawing.Font;


namespace Yutai.ArcGIS.Common.Excel
{
    public class ExcelBase
    {
        private Application application_0;

        private Workbook workbook_0;

        private bool bool_0;

        private string string_0;

        private object object_0 = Missing.Value;

        public Application Application
        {
            get
            {
                return this.application_0;
            }
        }

        public string FormCaption
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public bool IsVisibledExcel
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public Workbook Workbooks
        {
            get
            {
                return this.workbook_0;
            }
        }

        public ExcelBase()
        {
            this.bool_0 = false;
            this.string_0 = "打印预览";
            try
            {
                this.application_0 = new ApplicationClass();
            }
            catch (Exception exception)
            {
                throw new ExceptionExcelCreateInstance(string.Concat("创建Excel类实例时错误，详细信息：", exception.Message));
            }
            this.application_0.DisplayAlerts = false;
        }

        public void ClearBordersEdge(Range range_0)
        {
            this.SetBordersEdge(range_0, BordersEdge.xlLineStyleNone);
        }

        public void Close()
        {
            this.application_0.Workbooks.Close();
            this.workbook_0 = null;
            this.application_0.Quit();
            this.application_0 = null;
            this.object_0 = null;
            GC.Collect();
        }

        public void DeleteColumn(int int_0)
        {
            Range range = this.GetRange(1, int_0);
            range.Select();
            range.EntireColumn.Delete(this.object_0);
        }

        public void DeleteColumn(string string_1)
        {
            Range range = this.GetRange(1, string_1);
            range.Select();
            range.EntireColumn.Delete(this.object_0);
        }

        public void DeleteRow(int int_0)
        {
            Range range = this.GetRange(int_0, "A");
            range.Select();
            range.EntireRow.Delete(this.object_0);
        }

        public string GetCellText(Range range_0)
        {
            return range_0.Text.ToString();
        }

        public Range GetRange(int int_0, int int_1)
        {
            return this.GetRange(int_0, int_1, int_0, int_1);
        }

        public Range GetRange(int int_0, string string_1)
        {
            return this.GetRange(int_0, string_1, int_0, string_1);
        }

        public Range GetRange(int int_0, int int_1, int int_2, int int_3)
        {
            Range range = this.application_0.Range[this.application_0.Cells[int_0, int_1], this.application_0.Cells[int_2, int_3]];
            return range;
        }

        public Range GetRange(int int_0, string string_1, int int_1, string string_2)
        {
            Range range = this.application_0.Range[string.Concat(string_1, int_0.ToString()), string.Concat(string_2, int_1.ToString())];
            return range;
        }

        public void InsertColumn(int int_0)
        {
            Range range = this.GetRange(1, int_0);
            range.Select();
            range.EntireColumn.Insert(this.object_0, this.object_0);
        }

        public void InsertColumn(string string_1)
        {
            Range range = this.GetRange(1, string_1);
            range.Select();
            range.EntireColumn.Insert(this.object_0, this.object_0);
        }

        public void InsertHPageBreaks(int int_0)
        {
        }

        public void InsertHPageBreaks(string string_1)
        {
        }

        public void InsertRow(int int_0)
        {
            Range range = this.GetRange(int_0, "A");
            range.Select();
            range.EntireRow.Insert(this.object_0, this.object_0);
        }

        public void InsertRow(int int_0, int int_1)
        {
            Range rows = (Range)this.application_0.Rows[string.Concat(int_1.ToString(), ":", int_1.ToString()), this.object_0];
            rows.Select();
            rows.Copy(this.object_0);
            this.InsertRow(int_0);
        }

        public void InsertVPageBreaks(int int_0)
        {
        }

        public void MergeCells(Range range_0)
        {
            range_0.HorizontalAlignment = Constants.xlCenter;
            range_0.VerticalAlignment = Constants.xlCenter;
            range_0.WrapText = false;
            range_0.Orientation = 0;
            range_0.AddIndent = false;
            range_0.IndentLevel = 0;
            range_0.ShrinkToFit = false;
            range_0.MergeCells = false;
            range_0.Merge(this.object_0);
        }

        public void Open()
        {
            try
            {
                this.workbook_0 = this.application_0.Workbooks.Add(this.object_0);
            }
            catch (Exception exception)
            {
                throw new ExceptionExcelOpen(string.Concat("打开Excel时错误，详细信息：", exception.Message));
            }
        }

        public void Open(string string_1)
        {
            if (!File.Exists(string_1))
            {
                this.Open();
            }
            else
            {
                try
                {
                    this.workbook_0 = this.application_0.Workbooks.Add(string_1);
                }
                catch (Exception exception)
                {
                    throw new ExceptionExcelOpen(string.Concat("打开Excel时错误，详细信息：", exception.Message));
                }
            }
        }

        public void Print()
        {
            this.application_0.Visible = this.IsVisibledExcel;
            object value = Missing.Value;
            try
            {
                this.application_0.ActiveWorkbook.PrintOut(value, value, value, value, value, value, value, value);
            }
            catch
            {
            }
        }

        public void PrintPreview()
        {
            this.application_0.Caption = this.string_0;
            this.application_0.Visible = true;
            try
            {
                this.application_0.ActiveWorkbook.PrintPreview(this.object_0);
            }
            catch
            {
            }
            this.application_0.Visible = this.IsVisibledExcel;
        }

        public bool SaveAs(string string_1, bool bool_1)
        {
            bool flag = false;
            if (File.Exists(string_1))
            {
                if (bool_1)
                {
                    try
                    {
                        File.Delete(string_1);
                        flag = true;
                    }
                    catch (Exception exception)
                    {
                        string message = exception.Message;
                    }
                }
            }
            try
            {
                this.application_0.ActiveWorkbook.SaveCopyAs(string_1);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public void SetBordersEdge(Range range_0, BordersEdge bordersEdge_0)
        {
            this.SetBordersEdge(range_0, bordersEdge_0, BordersLineStyle.xlContinuous, BordersWeight.xlThin);
        }

        public void SetBordersEdge(Range range_0, BordersEdge bordersEdge_0, BordersLineStyle bordersLineStyle_0, BordersWeight bordersWeight_0)
        {
            range_0.Select();
            Border borders = null;
            switch (bordersEdge_0)
            {
                case BordersEdge.xlLineStyleNone:
                    {
                        range_0.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
                        range_0.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
                        break;
                    }
                case BordersEdge.xlLeft:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlEdgeLeft];
                        break;
                    }
                case BordersEdge.xlRight:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlEdgeRight];
                        break;
                    }
                case BordersEdge.xlTop:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlEdgeTop];
                        break;
                    }
                case BordersEdge.xlBottom:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlEdgeBottom];
                        break;
                    }
                case BordersEdge.xlDiagonalDown:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlDiagonalDown];
                        break;
                    }
                case BordersEdge.xlDiagonalUp:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlDiagonalUp];
                        break;
                    }
                case BordersEdge.xlInsideHorizontal:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlInsideHorizontal];
                        break;
                    }
                case BordersEdge.xlInsideVertical:
                    {
                        borders = range_0.Borders[XlBordersIndex.xlInsideVertical];
                        break;
                    }
            }
            if (borders != null)
            {
                XlLineStyle xlLineStyle = XlLineStyle.xlContinuous;
                switch (bordersLineStyle_0)
                {
                    case BordersLineStyle.xlContinuous:
                        {
                            xlLineStyle = XlLineStyle.xlContinuous;
                            break;
                        }
                    case BordersLineStyle.xlDash:
                        {
                            xlLineStyle = XlLineStyle.xlDash;
                            break;
                        }
                    case BordersLineStyle.xlDashDot:
                        {
                            xlLineStyle = XlLineStyle.xlDashDot;
                            break;
                        }
                    case BordersLineStyle.xlDashDotDot:
                        {
                            xlLineStyle = XlLineStyle.xlDashDotDot;
                            break;
                        }
                    case BordersLineStyle.xlDot:
                        {
                            xlLineStyle = XlLineStyle.xlDot;
                            break;
                        }
                    case BordersLineStyle.xlDouble:
                        {
                            xlLineStyle = XlLineStyle.xlDouble;
                            break;
                        }
                    case BordersLineStyle.xlLineStyleNone:
                        {
                            xlLineStyle = XlLineStyle.xlLineStyleNone;
                            break;
                        }
                    case BordersLineStyle.xlSlantDashDot:
                        {
                            xlLineStyle = XlLineStyle.xlSlantDashDot;
                            break;
                        }
                }
                try
                {
                    borders.LineStyle = xlLineStyle;
                }
                catch
                {
                }
                XlBorderWeight xlBorderWeight = XlBorderWeight.xlThin;
                switch (bordersWeight_0)
                {
                    case BordersWeight.xlHairline:
                        {
                            xlBorderWeight = XlBorderWeight.xlHairline;
                            break;
                        }
                    case BordersWeight.xlMedium:
                        {
                            xlBorderWeight = XlBorderWeight.xlMedium;
                            break;
                        }
                    case BordersWeight.xlThick:
                        {
                            xlBorderWeight = XlBorderWeight.xlThick;
                            break;
                        }
                    case BordersWeight.xlThin:
                        {
                            xlBorderWeight = XlBorderWeight.xlThin;
                            break;
                        }
                }
                borders.Weight = xlBorderWeight;
            }
        }

        public void SetCellText(Range range_0, string string_1)
        {
            range_0.Cells.FormulaR1C1 = string_1;
        }

        public void SetColumnWidth(int int_0, float float_0)
        {
            Range range = this.GetRange(1, int_0);
            range.Select();
            range.ColumnWidth = float_0;
        }

        public void SetColumnWidth(string string_1, float float_0)
        {
            Range range = this.GetRange(1, string_1);
            range.Select();
            range.ColumnWidth = float_0;
        }

        public void SetFont(Range range_0, Font font_0)
        {
            this.SetFont(range_0, font_0, Color.Black);
        }

        public void SetFont(Range range_0, Font font_0, Color color_0)
        {
            range_0.Select();
            range_0.Font.Name = font_0.Name;
            range_0.Font.Size = font_0.Size;
            range_0.Font.Bold = font_0.Bold;
            range_0.Font.Italic = font_0.Italic;
            //range_0.Font.Strikethrough = font_0.Strikeout;
            range_0.Font.Underline = font_0.Underline;
        }

        public void SetRowHeight(int int_0, float float_0)
        {
            Range range = this.GetRange(int_0, "A");
            range.Select();
            range.RowHeight = float_0;
        }

        public void ShowExcel()
        {
            this.application_0.Visible = true;
        }
    }
}