using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Axf
{
    public class AxfTables
    {
        public static string TableNameName = "TABLE_NAME";
        public static string AliasName = "ALIAS";
        public static string IsReadonlyName = "IS_READONLY";
        public static string IsHiddenName = "IS_HIDDEN";
        public static string SourceObjectidColumnName = "SOURCE_OBJECTID_COLUMN";
        public string TableName { get; set; }
        public string Alias { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsHidden { get; set; }
        public string SourceObjectidColumn { get; set; }

        public static List<AxfTables> GetAxfTablesList(SqlCeResultSet sqlCeResultSet, DataGridView dataGridView)
        {
            int idxTableName = sqlCeResultSet.GetOrdinal(TableNameName);
            int idxAlias = sqlCeResultSet.GetOrdinal(AliasName);
            int idxIsReadonly = sqlCeResultSet.GetOrdinal(IsReadonlyName);
            int idxIsHidden = sqlCeResultSet.GetOrdinal(IsHiddenName);
            int idxSourceObjectidColumn = sqlCeResultSet.GetOrdinal(SourceObjectidColumnName);
            return (from DataGridViewRow dataRow in dataGridView.Rows
                    select new AxfTables
                    {
                        TableName = dataRow.Cells[idxTableName].Value.ToString(),
                        Alias = dataRow.Cells[idxAlias].Value.ToString(),
                        IsReadonly = Convert.ToBoolean(dataRow.Cells[idxIsReadonly].Value),
                        IsHidden = Convert.ToBoolean(dataRow.Cells[idxIsHidden].Value),
                        SourceObjectidColumn = dataRow.Cells[idxSourceObjectidColumn].Value.ToString()
                    }).ToList();
        }
    }
}
