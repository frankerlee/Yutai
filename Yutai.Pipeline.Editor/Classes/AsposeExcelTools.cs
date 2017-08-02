using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;

namespace Yutai.Pipeline.Editor.Classes
{
    class AsposeExcelTools
    {
        public static bool ClearExcel(string filePath)
        {
            try
            {
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(filePath);
                workbook.Worksheets.Clear();
                workbook.Save(filePath);
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        public static bool DataTableToExcel(DataTable datatable, string filepath, out string error)
        {
            error = "";
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(filepath);
                Aspose.Cells.Worksheet sheet = workbook.Worksheets.Add(datatable.TableName);
                Aspose.Cells.Cells cells = sheet.Cells;

                int nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    nRow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {
                            cells[nRow, i].PutValue(row[i]);
                        }
                    }
                    catch (System.Exception e)
                    {
                        error = error + " DataTableToExcel: " + e.Message;
                    }
                }

                workbook.Save(filepath);
                return true;
            }
            catch (System.Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }

        public static bool DataTableToExcel2(DataTable datatable, string filepath, out string error)
        {
            error = "";
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();

            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                //为单元格添加样式    
                Aspose.Cells.Style style = wb.Styles[wb.Styles.Add()];
                //设置居中
                style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
                //设置背景颜色
                style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
                style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;

                int rowIndex = 0;
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    DataColumn col = datatable.Columns[i];
                    string columnName = col.Caption ?? col.ColumnName;
                    wb.Worksheets[0].Cells[rowIndex, i].PutValue(columnName);
                    wb.Worksheets[0].Cells[rowIndex, i].Style = style;
                }
                rowIndex++;

                foreach (DataRow row in datatable.Rows)
                {
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        wb.Worksheets[0].Cells[rowIndex, i].PutValue(row[i].ToString());
                    }
                    rowIndex++;
                }

                for (int k = 0; k < datatable.Columns.Count; k++)
                {
                    wb.Worksheets[0].AutoFitColumn(k, 0, 150);
                }
                wb.Worksheets[0].FreezePanes(1, 0, 1, datatable.Columns.Count);
                wb.Save(filepath);
                return true;
            }
            catch (Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }

        }

        /// <summary>
        /// Excel文件转换为DataTable.
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatable">DataTable:返回值</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool ExcelFileToDataTable(string filepath, out DataTable datatable, out string error)
        {
            error = "";
            datatable = null;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    datatable = null;
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(filepath);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                datatable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1);

                return true;
            }
            catch (System.Exception e)
            {
                error = e.Message;
                return false;
            }

        }

        public static bool ListsToExcelFile(string filepath, IList[] lists, out string error)
        {
            error = "";
            //----------Aspose变量初始化----------------
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
            Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
            Aspose.Cells.Cells cells = sheet.Cells;
            //-------------输入数据-------------
            int nRow = 0;
            sheet.Pictures.Clear();
            cells.Clear();
            foreach (IList list in lists)
            {

                for (int i = 0; i <= list.Count - 1; i++)
                {
                    try
                    {
                        System.Console.WriteLine(i.ToString() + "  " + list[i].GetType());
                        if (list[i].GetType().ToString() == "System.Drawing.Bitmap")
                        {
                            //插入图片数据
                            System.Drawing.Image image = (System.Drawing.Image)list[i];

                            MemoryStream mstream = new MemoryStream();

                            image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);

                            sheet.Pictures.Add(nRow, i, mstream);
                        }
                        else
                        {
                            cells[nRow, i].PutValue(list[i]);
                        }
                    }
                    catch (System.Exception e)
                    {
                        error = error + e.Message;
                    }

                }

                nRow++;
            }
            //-------------保存-------------
            workbook.Save(filepath);

            return true;
        }
    }
}
