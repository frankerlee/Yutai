using System.Data;
using System.Data.Common;

namespace Yutai.ArcGIS.Common.Data
{
    public interface IDataAccessLayer
    {
        string ConnectionString { get; set; }

        string SQL { get; set; }

        ConnectionState State { get; }

        void BeginTransaction();

        void Close();

        void CommitTransaction();

        void Dispose();

        void Execute();

        IDataReader ExecuteDataReader(string string_0, IDataParameter[] idataParameter_0);

        IDataReader ExecuteDataReader(string string_0, CommandType commandType_0);

        IDataReader ExecuteDataReader(string string_0, CommandType commandType_0, IDataParameter[] idataParameter_0);

        IDataReader ExecuteDataReader(string string_0);

        DataSet ExecuteDataSet(string string_0, CommandType commandType_0, IDataParameter[] idataParameter_0);

        DataSet ExecuteDataSet(string string_0, CommandType commandType_0);

        DataSet ExecuteDataSet(string string_0);

        DataSet ExecuteDataSet(string string_0, IDataParameter[] idataParameter_0);

        DataTable ExecuteDataTable(string string_0);

        DataSet ExecuteKDDataSet(string string_0);

        int ExecuteQuery(string string_0, CommandType commandType_0, IDataParameter[] idataParameter_0);

        int ExecuteQuery(string string_0, IDataParameter[] idataParameter_0);

        int ExecuteQuery(string string_0);

        int ExecuteQuery(string string_0, CommandType commandType_0);

        object ExecuteScalar(string string_0, CommandType commandType_0, IDataParameter[] idataParameter_0);

        object ExecuteScalar(string string_0, CommandType commandType_0);

        object ExecuteScalar(string string_0);

        object ExecuteScalar(string string_0, IDataParameter[] idataParameter_0);

        int ExecuteSp(string string_0, CommandType commandType_0);

        int ExecuteSp(string string_0, CommandType commandType_0, IDataParameter[] idataParameter_0);

        int ExecuteSp(string string_0);

        IDbCommand GeDataProviderCommand();

        DataProviderType GetCurrentDataProviderType();

        IDbConnection GetDataProviderConnection();

        IDbDataAdapter GetDataProviderDataAdapter();

        DbCommand GetDbCommand();

        DbCommandBuilder GetDbCommandBuilder();

        DbConnection GetDbConnection();

        DbDataAdapter GetDbDataAdapter();

        string getFieldValue(string string_0);

        bool IsExistsField(string string_0, string string_1);

        bool IsExistsTable(string string_0, string string_1);

        bool Open();

        void ReOpen(string string_0);

        void RollbackTransaction();
    }
}