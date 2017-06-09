using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Drawing;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;


namespace Yutai.ArcGIS.Common.Excel
{
    public class ExcelAccess : ExcelBase
    {
        public ExcelAccess()
        {
        }

        public void ClearBordersEdge(int int_0, int int_1)
        {
            base.SetBordersEdge(base.GetRange(int_0, int_1), BordersEdge.xlLineStyleNone);
        }

        public void ClearBordersEdge(int int_0, string string_1)
        {
            base.SetBordersEdge(base.GetRange(int_0, string_1), BordersEdge.xlLineStyleNone);
        }

        public void ClearBordersEdge(int int_0, int int_1, int int_2, int int_3)
        {
            base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlLineStyleNone);
        }

        public void ClearBordersEdge(int int_0, string string_1, int int_1, string string_2)
        {
            base.SetBordersEdge(base.GetRange(int_0, string_1, int_1, string_2), BordersEdge.xlLineStyleNone);
        }

        public string GetCellText(int int_0, int int_1)
        {
            string str = "";
            Range range = base.GetRange(int_0, int_1);
            str = range.Text.ToString();
            range = null;
            return str;
        }

        public string GetCellText(int int_0, string string_1)
        {
            string str = "";
            Range range = base.GetRange(int_0, string_1);
            str = range.Text.ToString();
            range = null;
            return str;
        }

        public void MergeCells(int int_0, int int_1, int int_2, int int_3)
        {
            base.MergeCells(base.GetRange(int_0, int_1, int_2, int_3));
        }

        public void MergeCells(int int_0, string string_1, int int_1, string string_2)
        {
            base.MergeCells(base.GetRange(int_0, string_1, int_1, string_2));
        }

        public void SetBordersEdge(int int_0, int int_1, BordersEdge bordersEdge_0, BordersLineStyle bordersLineStyle_0, BordersWeight bordersWeight_0)
        {
            base.SetBordersEdge(base.GetRange(int_0, int_1), bordersEdge_0, bordersLineStyle_0, bordersWeight_0);
        }

        public void SetBordersEdge(int int_0, string string_1, BordersEdge bordersEdge_0, BordersLineStyle bordersLineStyle_0, BordersWeight bordersWeight_0)
        {
            base.SetBordersEdge(base.GetRange(int_0, string_1), bordersEdge_0, bordersLineStyle_0, bordersWeight_0);
        }

        public void SetBordersEdge(int int_0, int int_1, int int_2, int int_3, BordersEdge bordersEdge_0, BordersLineStyle bordersLineStyle_0, BordersWeight bordersWeight_0)
        {
            base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), bordersEdge_0, bordersLineStyle_0, bordersWeight_0);
        }

        public void SetBordersEdge(int int_0, string string_1, int int_1, string string_2, BordersEdge bordersEdge_0, BordersLineStyle bordersLineStyle_0, BordersWeight bordersWeight_0)
        {
            base.SetBordersEdge(base.GetRange(int_0, string_1, int_1, string_2), bordersEdge_0, bordersLineStyle_0, bordersWeight_0);
        }

        public void SetBordersEdge(int int_0, int int_1, int int_2, int int_3, bool bool_1)
        {
            base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlLeft);
            base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlTop);
            base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlRight);
            base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlBottom);
            if (!bool_1)
            {
                base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlInsideHorizontal);
                base.SetBordersEdge(base.GetRange(int_0, int_1, int_2, int_3), BordersEdge.xlInsideVertical);
            }
        }

        public void SetCellText(int int_0, int int_1, string string_1)
        {
            base.GetRange(int_0, int_1).Cells.FormulaR1C1 = string_1;
        }

        public void SetCellText(int int_0, string string_1, string string_2)
        {
            base.GetRange(int_0, string_1).Cells.FormulaR1C1 = string_2;
        }

        public void SetCellText(int int_0, int int_1, int int_2, int int_3, string string_1)
        {
            Range range = base.GetRange(int_0, int_1, int_2, int_3);
            range.Cells.FormulaR1C1 = string_1;
            range = null;
        }

        public void SetCellText(int int_0, string string_1, int int_1, string string_2, string string_3)
        {
            Range range = base.GetRange(int_0, string_1, int_1, string_2);
            range.Cells.FormulaR1C1 = string_3;
            range = null;
        }

        public void SetCellText(DataTable dataTable_0, int int_0, int int_1, bool bool_1)
        {
            for (int i = 0; i < dataTable_0.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable_0.Columns.Count; j++)
                {
                    this.SetCellText(int_0 + i, int_1 + j, dataTable_0.Rows[i][j].ToString());
                }
            }
            if (bool_1)
            {
                this.SetBordersEdge(int_0, int_1, int_0 + dataTable_0.Rows.Count - 1, int_1 + dataTable_0.Columns.Count - 1, false);
            }
        }

        public void SetCellText(DataTable dataTable_0, int int_0, int int_1, bool bool_1, bool bool_2)
        {
            int i;
            int j;
            if (bool_2)
            {
                for (i = 0; i < dataTable_0.Columns.Count; i++)
                {
                    this.SetCellText(int_0, int_1 + i, dataTable_0.Columns[i].ColumnName);
                }
                for (i = 0; i < dataTable_0.Rows.Count; i++)
                {
                    for (j = 0; j < dataTable_0.Columns.Count; j++)
                    {
                        this.SetCellText(int_0 + i + 1, int_1 + j, dataTable_0.Rows[i][j].ToString());
                    }
                }
                if (bool_1)
                {
                    this.SetBordersEdge(int_0, int_1, int_0 + dataTable_0.Rows.Count, int_1 + dataTable_0.Columns.Count - 1, false);
                }
            }
            else
            {
                for (i = 0; i < dataTable_0.Rows.Count; i++)
                {
                    for (j = 0; j < dataTable_0.Columns.Count; j++)
                    {
                        this.SetCellText(int_0 + i, int_1 + j, dataTable_0.Rows[i][j].ToString());
                    }
                }
                if (bool_1)
                {
                    this.SetBordersEdge(int_0, int_1, int_0 + dataTable_0.Rows.Count - 1, int_1 + dataTable_0.Columns.Count - 1, false);
                }
            }
        }

        public void SetFont(int int_0, int int_1, int int_2, int int_3, Font font_0)
        {
            base.SetFont(base.GetRange(int_0, int_1, int_2, int_3), font_0, Color.Black);
        }
    }
}