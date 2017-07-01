using System.Data;
using System.Data.Common;

namespace Yutai.ArcGIS.Common.Data
{
    public interface IConnection
    {
        string ADOConnectionString { get; }

        IDbCommand Command { get; }

        DbCommandBuilder CommandBuilder { get; }

        IDbConnection Connection { get; }

        string ConnectionString { get; }

        IDbDataAdapter DataAdapter { get; }

        DataProviderType DataProviderType { get; }

        char ParameterChar { get; }

        IDbTransaction Transaction { get; }

        IDbTransaction BeginTransaction();

        IDbTransaction BeginTransaction(IsolationLevel isolationLevel_0);

        void Close();

        DbParameter CreateParameter(string string_0, object object_0);

        void Dispose();

        void ExecuteScriptFile(string string_0, string string_1);

        DataSet ExecuteSqlDataSet(IDbCommand idbCommand_0);

        DataSet ExecuteSqlDataSet(string string_0);

        DataTable ExecuteSqlDataTable(IDbCommand idbCommand_0);

        DataTable ExecuteSqlDataTable(string string_0, IDbTransaction idbTransaction_0);

        DataTable ExecuteSqlDataTable(string string_0);

        int ExecuteSqlNonQuery(string string_0, IDbTransaction idbTransaction_0);

        int ExecuteSqlNonQuery(string string_0);

        int ExecuteSqlNonQuery(string string_0, params object[] object_0);

        int ExecuteSqlNonQuery(string string_0, IDbTransaction idbTransaction_0, params object[] object_0);

        int ExecuteSqlNonQuery(IDbCommand idbCommand_0);

        object ExecuteSqlValue(string string_0);

        object ExecuteSqlValue(IDbCommand idbCommand_0);

        bool FieldExist(string string_0, string string_1);

        void Open();

        bool TableExist(string string_0, string string_1);
    }
}