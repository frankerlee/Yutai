using Yutai.Pipeline.Editor.SqlCe;

namespace Yutai.Pipeline.Editor.Axf
{
    public class AXF_TableInfo
    {
        private int _addCount = 0;
        private int _deleteCount = 0;
        private int _geometryType;
        private int _modifyCount = 0;
        private int _noEditCount = 0;
        private string _tableName;
        private SqlCeResultSet _sqlCeResultSet;

        public AXF_TableInfo(SqlCeDb sqlCeDb, string tableName, int geometryType)
        {
            this._tableName = tableName;
            this._geometryType = geometryType;
            _sqlCeResultSet = new SqlCeResultSet(tableName, sqlCeDb);
            this.AddCount = _sqlCeResultSet.AddCount;
            this.ModifyCount = _sqlCeResultSet.ModifyCount;
            this.DeleteCount = _sqlCeResultSet.DeleteCount;
            this.NoEditCount = _sqlCeResultSet.NoEditCount;
        }

        public int AddCount
        {
            get
            {
                return this._addCount;
            }
            set
            {
                this._addCount = value;
            }
        }

        public int DeleteCount
        {
            get
            {
                return this._deleteCount;
            }
            set
            {
                this._deleteCount = value;
            }
        }

        public int GeometryType
        {
            get
            {
                return this._geometryType;
            }
            set
            {
                this._geometryType = value;
            }
        }

        public int ModifyCount
        {
            get
            {
                return this._modifyCount;
            }
            set
            {
                this._modifyCount = value;
            }
        }

        public int NoEditCount
        {
            get
            {
                return this._noEditCount;
            }
            set
            {
                this._noEditCount = value;
            }
        }

        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }
    }
}
