using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Axf
{
    class AXF_LAYERS
    {
        public static string TableIdFieldName = "TABLEID";
        public static string NameFieldName = "NAME";

        public int TableId { get; set; }
        public string Name { get; set; }

        public static List<AXF_LAYERS> GetAxfLayerss(SqlCeResultSet sqlCeResultSet, DataGridView dataGridView)
        {
            int idxTableId = sqlCeResultSet.GetOrdinal(TableIdFieldName);
            int idxName = sqlCeResultSet.GetOrdinal(NameFieldName);
            return (from DataGridViewRow dataRow in dataGridView.Rows
                    select new AXF_LAYERS
                    {
                        TableId = int.Parse(dataRow.Cells[idxTableId].Value.ToString()),
                        Name = dataRow.Cells[idxName].Value.ToString()
                    }).ToList();
        }
    }
}
