using System.Data;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class YTExportExcel
    {
        public YTExportExcel()
        {
        }

        public static void ExcelTemplatePrint(bool bool_0, object object_0, string string_0, string string_1, int int_0,
            int int_1)
        {
            int i;
            DataTable object0 = null;
            if (object_0 is DataTable)
            {
                object0 = object_0 as DataTable;
            }
            else if (object_0 is DataSet)
            {
                object0 = (object_0 as DataSet).Tables[0];
            }
            if (object0 != null)
            {
                ExcelAccess excelAccess = new ExcelAccess();
                excelAccess.Open(string_0);
                excelAccess.IsVisibledExcel = true;
                excelAccess.FormCaption = string_1;
                int int0 = int_0 + int_1;
                for (i = 0; i < object0.Rows.Count; i++)
                {
                    excelAccess.InsertRow(int_0, int_0);
                    excelAccess.SetRowHeight(int_0, 14.25f);
                }
                for (i = 0; i < object0.Rows.Count; i++)
                {
                    for (int j = 0; j < object0.Columns.Count; j++)
                    {
                        excelAccess.SetCellText(int0 + i, j + 1, object0.Rows[i][j].ToString());
                    }
                }
                excelAccess.SetBordersEdge(int0, 1, int0 + object0.Rows.Count - 1, object0.Columns.Count, false);
                for (i = int_1; i > 0; i--)
                {
                    excelAccess.DeleteRow(int_0 + i - 1);
                }
                if (!bool_0)
                {
                    excelAccess.ShowExcel();
                }
                else
                {
                    excelAccess.PrintPreview();
                    excelAccess.Close();
                }
            }
        }

        public static void ExcelTemplatePrint(bool bool_0, object object_0, string string_0, string string_1, int int_0,
            int int_1, int int_2)
        {
            int i;
            DataTable object0 = null;
            if (object_0 is DataTable)
            {
                object0 = object_0 as DataTable;
            }
            else if (object_0 is DataSet)
            {
                object0 = (object_0 as DataSet).Tables[0];
            }
            if (object0 != null)
            {
                ExcelAccess excelAccess = new ExcelAccess();
                excelAccess.Open(string_0);
                excelAccess.IsVisibledExcel = true;
                excelAccess.FormCaption = string_1;
                for (i = 0; i < object0.Rows.Count; i++)
                {
                    excelAccess.InsertRow(int_0, int_0);
                    excelAccess.SetRowHeight(int_0, 14.25f);
                }
                for (i = 0; i < object0.Rows.Count; i++)
                {
                    for (int j = 0; j < object0.Columns.Count; j++)
                    {
                        excelAccess.SetCellText(int_2 + i, j + 1, object0.Rows[i][j].ToString());
                    }
                }
                excelAccess.SetBordersEdge(int_2, 1, int_2 + object0.Rows.Count - 1, object0.Columns.Count, false);
                for (i = int_1; i > 0; i--)
                {
                    excelAccess.DeleteRow(int_0 + i - 1);
                }
                if (!bool_0)
                {
                    excelAccess.ShowExcel();
                }
                else
                {
                    excelAccess.PrintPreview();
                    excelAccess.Close();
                }
            }
        }

        public static void ExportExcel(bool bool_0, string string_0, object object_0, string string_1)
        {
            DataTable object0 = null;
            if (object_0 is DataTable)
            {
                object0 = object_0 as DataTable;
            }
            else if (object_0 is DataSet)
            {
                object0 = (object_0 as DataSet).Tables[0];
            }
            if (object0 != null)
            {
                Font font = new Font("黑体", 21f, FontStyle.Bold);
                ExcelAccess excelAccess = new ExcelAccess();
                excelAccess.Open();
                excelAccess.FormCaption = string_1;
                int num = 1;
                if ((string_0 == null ? false : string_0.Trim() != ""))
                {
                    num = 3;
                    excelAccess.MergeCells(1, 1, 1, object0.Columns.Count);
                    excelAccess.SetFont(1, 1, 1, object0.Columns.Count, font);
                    excelAccess.SetCellText(1, 1, 1, object0.Columns.Count, string_0);
                }
                excelAccess.SetCellText(object0, num, 1, true, true);
                if (!bool_0)
                {
                    excelAccess.ShowExcel();
                }
                else
                {
                    excelAccess.PrintPreview();
                    excelAccess.Close();
                }
                font.Dispose();
            }
        }
    }
}