using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.SqlCe
{
    public class SqlCeResultSet
    {
        private int _addCount = 0;
        private int _modifyCount = 0;
        private int _deleteCount = 0;
        private int _noEditCount = 0;

        private string _tableName;
        private SqlCeDb _sqlCeDb;
        public SqlCeResultSet(string tableName, SqlCeDb sqlCeDb)
        {
            _sqlCeDb = sqlCeDb;
            _tableName = tableName;
            if (tableName == "GEOMETRY_COLUMNS")
                return;
            SqlCeDataReader sqlCeDataReader = sqlCeDb.GetDataReader(tableName, null, SortOrder.None) as SqlCeDataReader;
            if (sqlCeDataReader == null)
                return;
            while (sqlCeDataReader.Read())
            {
                object objValue = sqlCeDataReader["AXF_STATUS"];
                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == "0")
                    _noEditCount++;
                else if (objValue.ToString().Trim() == "1")
                    _addCount++;
                else if (objValue.ToString().Trim() == "2")
                    _modifyCount++;
                else if (objValue.ToString().Trim() == "128")
                    _deleteCount++;
            }
            sqlCeDataReader.Close();
            sqlCeDataReader.Dispose();
        }

        public int AddCount
        {
            get { return _addCount; }
            set { _addCount = value; }
        }

        public int ModifyCount
        {
            get { return _modifyCount; }
            set { _modifyCount = value; }
        }

        public int DeleteCount
        {
            get { return _deleteCount; }
            set { _deleteCount = value; }
        }

        public int NoEditCount
        {
            get { return _noEditCount; }
            set { _noEditCount = value; }
        }

        public List<GeometryColumn> GetGeometryColumns()
        {
            List<GeometryColumn> list = new List<GeometryColumn>();

            SqlCeDataReader sqlCeDataReader = _sqlCeDb.GetDataReader(_tableName, null, SortOrder.None) as SqlCeDataReader;
            if (sqlCeDataReader == null)
                return list;
            while (sqlCeDataReader.Read())
            {
                list.Add(new GeometryColumn()
                {
                    TableName = sqlCeDataReader["G_TABLE_NAME"].ToString(),
                    GeometryType = Convert.ToInt32(sqlCeDataReader["GEOMETRY_TYPE"])
                });
            }
            sqlCeDataReader.Close();
            sqlCeDataReader.Dispose();
            return list;
        }
    }

    public class GeometryColumn
    {
        public string TableName { get; set; }
        public int GeometryType { get; set; }
    }
}
