using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Axf
{
    public class AxfField
    {
        public static string ObjectidName = "OBJECTID";
        public static string AxfTimestampName = "AXF_TIMESTAMP";
        public static string AxfStatusName = "AXF_STATUS";
        public static string AxfGenerationName = "AXF_GENERATION";

        public int Objectid { get; set; }
        public DateTime AxfTimestamp { get; set; }
        public int AxfStatus { get; set; }
        public int AxfGeneration { get; set; }

        public static List<AxfField> GetAxfFieldList(SqlCeResultSet sqlCeResultSet, DataGridView dataGridView)
        {
            int idxObjectid = sqlCeResultSet.GetOrdinal(ObjectidName);
            int idxAxfTimestamp = sqlCeResultSet.GetOrdinal(AxfTimestampName);
            int idxAxfStatus = sqlCeResultSet.GetOrdinal(AxfStatusName);
            int idxAxfGeneration = sqlCeResultSet.GetOrdinal(AxfGenerationName);
            return (from DataGridViewRow dataRow in dataGridView.Rows
                    select new AxfField
                    {
                        Objectid = string.IsNullOrEmpty(dataRow.Cells[idxObjectid].Value.ToString()) ? -1 : Convert.ToInt32(dataRow.Cells[idxObjectid].Value),
                        AxfTimestamp = string.IsNullOrEmpty(dataRow.Cells[idxAxfTimestamp].Value.ToString()) ? DateTime.MinValue : Convert.ToDateTime(dataRow.Cells[idxAxfTimestamp].Value),
                        AxfStatus = string.IsNullOrEmpty(dataRow.Cells[idxAxfStatus].Value.ToString()) ? -1 : Convert.ToInt32(dataRow.Cells[idxAxfStatus].Value),
                        AxfGeneration = string.IsNullOrEmpty(dataRow.Cells[idxAxfGeneration].Value.ToString()) ? -1 : Convert.ToInt32(dataRow.Cells[idxAxfGeneration].Value)
                    }).ToList();
        }
    }
}
