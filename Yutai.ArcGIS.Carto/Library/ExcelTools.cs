using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Yutai.ArcGIS.Carto.Library
{
    public class ExcelTools
    {
        public static void CreateExcelFile2(string string_0, string string_1)
        {
            string str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + string_0 + "; Extended Properties=Excel 8.0;";
            OleDbConnection connection = new OleDbConnection
            {
                ConnectionString = str
            };
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = string_1
            };
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static List<string> GetExcelColumns(string string_0, string string_1)
        {
            List<string> list = new List<string>();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + string_0 +
                                      ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";
            string cmdText = "SELECT * FROM [" + string_1 + "]";
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("XYDATA");
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
            connection.Open();
            try
            {
                adapter.Fill(dataSet, "XYDATA");
            }
            finally
            {
                connection.Close();
            }
            try
            {
                foreach (DataColumn column in dataSet.Tables["XYDATA"].Columns)
                {
                    list.Add(column.ColumnName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:\n{0}", exception.Message);
            }
            return list;
        }

        public static List<string> GetExcelFileSheet(string string_0)
        {
            List<string> list = null;
            if (File.Exists(string_0))
            {
                list = new List<string>();
                using (
                    OleDbConnection connection =
                        new OleDbConnection(
                            "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + string_0)
                )
                {
                    connection.Open();
                    DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
                    {
                        list.Add(oleDbSchemaTable.Rows[i][2].ToString().Trim());
                    }
                }
            }
            return list;
        }

        public static string GetExcelFirstTableName(string string_0)
        {
            string str = null;
            if (!File.Exists(string_0))
            {
                return str;
            }
            using (
                OleDbConnection connection =
                    new OleDbConnection(
                        "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + string_0))
            {
                connection.Open();
                return connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0][2].ToString().Trim();
            }
        }

        public static DataRowCollection GetExcelRows(string string_0, string string_1)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + string_0 +
                                      ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";
            string cmdText = "SELECT * FROM [" + string_1 + "]";
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("XYDATA");
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
            connection.Open();
            try
            {
                adapter.Fill(dataSet, "XYDATA");
            }
            finally
            {
                connection.Close();
            }
            try
            {
                return dataSet.Tables["XYDATA"].Rows;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:\n{0}", exception.Message);
            }
            return null;
        }
    }
}