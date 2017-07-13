using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Axf
{
    class AXF_GEOMETRY_COLUMNS
    {
        public static string TableIdFieldName = "AXF_TABLEID";
        public static string TableNameFieldName = "F_TABLE_NAME";

        public int TableId { get; set; }
        public string TableName { get; set; }

        public static List<AXF_GEOMETRY_COLUMNS> GetAxfGeometryColumns(SqlCeResultSet sqlCeResultSet, DataGridView dataGridView)
        {
            int idxTableId = sqlCeResultSet.GetOrdinal(TableIdFieldName);
            int idxTableName = sqlCeResultSet.GetOrdinal(TableNameFieldName);
            return (from DataGridViewRow dataRow in dataGridView.Rows
                select new AXF_GEOMETRY_COLUMNS
                {
                    TableId = int.Parse(dataRow.Cells[idxTableId].Value.ToString()),
                    TableName = dataRow.Cells[idxTableName].Value.ToString()
                }).ToList();
        }
    }
}
