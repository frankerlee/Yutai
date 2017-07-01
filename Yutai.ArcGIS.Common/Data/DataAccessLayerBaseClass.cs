using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.Data
{
    public abstract class DataAccessLayerBaseClass : IConnection, IDataAccessLayer, ISQLSelecter, ISQLTable
    {
        private string string_0;

        private IDbConnection idbConnection_0;

        private IDbCommand idbCommand_0;

        private IDbTransaction idbTransaction_0;

        public string ADODBConnectionString = "";

        private string string_1 = "";

        private DataTable dataTable_0 = null;

        public string ADOConnectionString
        {
            get { return this.ADODBConnectionString; }
        }

        public IDbCommand Command
        {
            get { return this.GetDbCommand(); }
        }

        public DbCommandBuilder CommandBuilder
        {
            get { return this.GetDbCommandBuilder(); }
        }

        public IDbConnection Connection
        {
            get { return this.GetDbConnection(); }
        }

        public string ConnectionString
        {
            get
            {
                if ((this.string_0 == string.Empty ? true : this.string_0.Length == 0))
                {
                    throw new ArgumentException("Invalid database connection string.");
                }
                return this.string_0;
            }
            set { this.string_0 = value; }
        }

        public IDbDataAdapter DataAdapter
        {
            get { return this.GetDbDataAdapter(); }
        }

        public DataProviderType DataProviderType
        {
            get { return this.GetCurrentDataProviderType(); }
        }

        public abstract char ParameterChar { get; }

        public string SQL
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public ConnectionState State
        {
            get { return this.idbConnection_0.State; }
        }

        public IDbTransaction Transaction
        {
            get { return this.idbTransaction_0; }
        }

        protected DataAccessLayerBaseClass()
        {
            this.idbConnection_0 = this.GetDataProviderConnection();
        }

        public void BeginTransaction()
        {
            if (this.idbTransaction_0 == null)
            {
                try
                {
                    this.idbConnection_0 = this.GetDataProviderConnection();
                    this.idbConnection_0.ConnectionString = this.ConnectionString;
                    this.idbConnection_0.Open();
                    this.idbTransaction_0 = this.idbConnection_0.BeginTransaction(IsolationLevel.ReadCommitted);
                }
                catch
                {
                    this.idbConnection_0.Close();
                    throw;
                }
            }
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel_0)
        {
            this.BeginTransaction(isolationLevel_0);
            return this.Transaction;
        }

        public void Close()
        {
            if ((this.idbConnection_0 == null ? false : this.idbConnection_0.State != ConnectionState.Closed))
            {
                if (this.idbTransaction_0 != null)
                {
                    throw new Exception("There is a Transaction on Connection, can not close connetion this time!");
                }
                this.idbConnection_0.Close();
            }
        }

        public void CommitTransaction()
        {
            if (this.idbTransaction_0 != null)
            {
                try
                {
                    try
                    {
                        this.idbTransaction_0.Commit();
                    }
                    catch
                    {
                        this.RollbackTransaction();
                        throw;
                    }
                }
                finally
                {
                    this.idbConnection_0.Close();
                    this.idbTransaction_0 = null;
                }
            }
        }

        public abstract DbParameter CreateParameter(string string_2, object object_0);

        public abstract DbParameter CreateParameter(string string_2);

        public void Dispose()
        {
            try
            {
                this.dataTable_0 = null;
            }
            catch
            {
            }
        }

        public virtual void Execute()
        {
            this.dataTable_0 = null;
            DataSet dataSet = this.ExecuteDataSet(this.SQL);
            if (dataSet != null)
            {
                this.dataTable_0 = dataSet.Tables[0];
            }
            dataSet = null;
        }

        public IDataReader ExecuteDataReader(string string_2)
        {
            return this.ExecuteDataReader(string_2, CommandType.Text, null);
        }

        public IDataReader ExecuteDataReader(string string_2, CommandType commandType_0)
        {
            return this.ExecuteDataReader(string_2, commandType_0, null);
        }

        public IDataReader ExecuteDataReader(string string_2, IDataParameter[] idataParameter_0)
        {
            return this.ExecuteDataReader(string_2, CommandType.Text, idataParameter_0);
        }

        public IDataReader ExecuteDataReader(string string_2, CommandType commandType_0,
            IDataParameter[] idataParameter_0)
        {
            IDataReader dataReader;
            IDataReader dataReader1;
            try
            {
                this.method_0(commandType_0, string_2, idataParameter_0);
                dataReader = (this.idbTransaction_0 != null
                    ? this.idbCommand_0.ExecuteReader()
                    : this.idbCommand_0.ExecuteReader(CommandBehavior.CloseConnection));
                dataReader1 = dataReader;
            }
            catch
            {
                if (this.idbTransaction_0 != null)
                {
                    this.RollbackTransaction();
                }
                else
                {
                    this.idbConnection_0.Close();
                    this.idbCommand_0.Dispose();
                }
                throw;
            }
            return dataReader1;
        }

        public DataSet ExecuteDataSet(string string_2)
        {
            return this.ExecuteDataSet(string_2, CommandType.Text, null);
        }

        public DataSet ExecuteDataSet(string string_2, CommandType commandType_0)
        {
            return this.ExecuteDataSet(string_2, commandType_0, null);
        }

        public DataSet ExecuteDataSet(string string_2, IDataParameter[] idataParameter_0)
        {
            return this.ExecuteDataSet(string_2, CommandType.Text, idataParameter_0);
        }

        public DataSet ExecuteDataSet(string string_2, CommandType commandType_0, IDataParameter[] idataParameter_0)
        {
            DataSet dataSet;
            try
            {
                this.method_0(commandType_0, string_2, idataParameter_0);
                IDbDataAdapter dataProviderDataAdapter = this.GetDataProviderDataAdapter();
                dataProviderDataAdapter.SelectCommand = this.idbCommand_0;
                DataSet dataSet1 = new DataSet();
                dataProviderDataAdapter.Fill(dataSet1);
                dataSet = dataSet1;
            }
            catch (Exception exception)
            {
                if (this.idbTransaction_0 != null)
                {
                    this.RollbackTransaction();
                }
                else
                {
                    this.idbConnection_0.Close();
                }
                dataSet = null;
            }
            return dataSet;
        }

        public DataTable ExecuteDataTable(string string_2)
        {
            DataTable item = null;
            DataSet dataSet = this.ExecuteDataSet(string_2);
            if (dataSet != null)
            {
                item = dataSet.Tables[0];
            }
            dataSet = null;
            return item;
        }

        public DataSet ExecuteKDDataSet(string string_2)
        {
            DataSet dataSet = null;
            DbConnection dbConnection = null;
            DbCommand dbCommand = null;
            DbDataAdapter dbDataAdapter = null;
            DbCommandBuilder dbCommandBuilder = null;
            dbDataAdapter = this.GetDbDataAdapter();
            dbConnection = this.GetDbConnection();
            dbConnection.ConnectionString = this.ConnectionString;
            dbCommand = this.GetDbCommand();
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbCommand.Connection = dbConnection;
            dbCommand.CommandText = string_2;
            dbDataAdapter.SelectCommand = dbCommand;
            dbCommandBuilder = this.GetDbCommandBuilder();
            dbCommandBuilder.DataAdapter = dbDataAdapter;
            dataSet = new DataSet();
            dbDataAdapter.FillSchema(dataSet, SchemaType.Source);
            dbDataAdapter.Fill(dataSet);
            if (dbConnection != null)
            {
                dbConnection.Dispose();
            }
            dbConnection = null;
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
            dbCommand = null;
            dbDataAdapter = null;
            if (dbCommandBuilder != null)
            {
                dbCommandBuilder.Dispose();
            }
            dbCommandBuilder = null;
            return dataSet;
        }

        public int ExecuteNonQuery(DbCommand dbCommand_0)
        {
            return this.ExecuteSqlNonQuery(dbCommand_0);
        }

        public int ExecuteNonQuery(string string_2, params object[] object_0)
        {
            return this.ExecuteSqlNonQuery(string_2, object_0);
        }

        public int ExecuteNonQuery(DbTransaction dbTransaction_0, string string_2, params object[] object_0)
        {
            return this.ExecuteSqlNonQuery(string_2, dbTransaction_0, object_0);
        }

        public int ExecuteNonQuery(CommandType commandType_0, string string_2)
        {
            return this.ExecuteQuery(string_2, commandType_0);
        }

        public int ExecuteNonQuery(string string_2, CommandType commandType_0)
        {
            return this.ExecuteQuery(string_2, commandType_0);
        }

        public int ExecuteQuery(string string_2)
        {
            return this.ExecuteQuery(string_2, CommandType.Text, null);
        }

        public int ExecuteQuery(string string_2, CommandType commandType_0)
        {
            return this.ExecuteQuery(string_2, commandType_0, null);
        }

        public int ExecuteQuery(string string_2, IDataParameter[] idataParameter_0)
        {
            return this.ExecuteQuery(string_2, CommandType.Text, idataParameter_0);
        }

        public int ExecuteQuery(string string_2, CommandType commandType_0, IDataParameter[] idataParameter_0)
        {
            int num;
            try
            {
                try
                {
                    this.method_0(commandType_0, string_2, idataParameter_0);
                    num = this.idbCommand_0.ExecuteNonQuery();
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    if (this.idbTransaction_0 != null)
                    {
                        this.RollbackTransaction();
                    }
                    throw exception;
                }
            }
            finally
            {
                if (this.idbTransaction_0 == null)
                {
                    this.idbConnection_0.Close();
                    this.idbCommand_0.Dispose();
                }
            }
            return num;
        }

        public object ExecuteScalar(string string_2)
        {
            return this.ExecuteScalar(string_2, CommandType.Text, null);
        }

        public object ExecuteScalar(string string_2, CommandType commandType_0)
        {
            return this.ExecuteScalar(string_2, commandType_0, null);
        }

        public object ExecuteScalar(string string_2, IDataParameter[] idataParameter_0)
        {
            return this.ExecuteScalar(string_2, CommandType.Text, idataParameter_0);
        }

        public object ExecuteScalar(string string_2, CommandType commandType_0, IDataParameter[] idataParameter_0)
        {
            object obj;
            try
            {
                try
                {
                    this.method_0(commandType_0, string_2, idataParameter_0);
                    object obj1 = this.idbCommand_0.ExecuteScalar();
                    obj = (obj1 == DBNull.Value ? null : obj1);
                }
                catch
                {
                    if (this.idbTransaction_0 != null)
                    {
                        this.RollbackTransaction();
                    }
                    throw;
                }
            }
            finally
            {
                if (this.idbTransaction_0 == null)
                {
                    this.idbConnection_0.Close();
                    this.idbCommand_0.Dispose();
                }
            }
            return obj;
        }

        public void ExecuteScriptFile(string string_2, string string_3)
        {
            List<string> strs = new List<string>();
            if (File.Exists(string_2))
            {
                StreamReader streamReader = new StreamReader(string_2, Encoding.Default);
                try
                {
                    strs.Add(string.Concat("use ", string_3));
                    string empty = string.Empty;
                    while (!streamReader.EndOfStream)
                    {
                        string str = streamReader.ReadLine();
                        if ((str.ToUpper().Trim() != "GO" ? true : empty.Length <= 0))
                        {
                            empty = string.Concat(empty, " ", str);
                        }
                        else
                        {
                            strs.Add(empty);
                            empty = string.Empty;
                        }
                    }
                    streamReader.Close();
                }
                finally
                {
                    if (streamReader != null)
                    {
                        ((IDisposable) streamReader).Dispose();
                    }
                }
                IDbCommand command = this.Command;
                string str1 = string.Concat(Path.GetTempPath(), "\\ErrorLog.log");
                if (File.Exists(str1))
                {
                    File.Delete(str1);
                }
                foreach (string str2 in strs)
                {
                    try
                    {
                        command.CommandText = str2;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Debug.Write(exception.Message);
                        StreamWriter streamWriter = File.AppendText(str1);
                        try
                        {
                            string[] message = new string[] {"\r\n[", null, null, null, null, null};
                            message[1] = DateTime.Now.ToString();
                            message[2] = "]\r\n";
                            message[3] = str2;
                            message[4] = "\r\n";
                            message[5] = exception.Message;
                            streamWriter.Write(string.Concat(message));
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                        finally
                        {
                            if (streamWriter != null)
                            {
                                ((IDisposable) streamWriter).Dispose();
                            }
                        }
                    }
                }
                File.Exists(str1);
            }
        }

        public int ExecuteSp(string string_2)
        {
            return this.ExecuteSp(string_2, CommandType.StoredProcedure, null);
        }

        public int ExecuteSp(string string_2, CommandType commandType_0)
        {
            return this.ExecuteSp(string_2, commandType_0, null);
        }

        public int ExecuteSp(string string_2, CommandType commandType_0, IDataParameter[] idataParameter_0)
        {
            int num;
            try
            {
                try
                {
                    this.method_0(commandType_0, string_2, idataParameter_0);
                    int num1 = this.idbCommand_0.ExecuteNonQuery();
                    if (idataParameter_0 != null)
                    {
                        this.idbCommand_0.Parameters.Clear();
                    }
                    num = num1;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    if (this.idbTransaction_0 != null)
                    {
                        this.RollbackTransaction();
                    }
                    throw exception;
                }
            }
            finally
            {
                if (this.idbTransaction_0 == null)
                {
                    this.idbConnection_0.Close();
                    this.idbCommand_0.Dispose();
                }
            }
            return num;
        }

        public int ExecuteSp(string string_2, params object[] object_0)
        {
            return this.ExecuteSqlNonQuery(string_2, object_0);
        }

        public DataSet ExecuteSqlDataSet(IDbCommand idbCommand_1)
        {
            bool flag = true;
            if (this.idbConnection_0.State == ConnectionState.Closed)
            {
                if (this.idbConnection_0.ConnectionString == "")
                {
                    this.idbConnection_0.ConnectionString = this.ConnectionString;
                }
                this.Open();
                flag = false;
            }
            if (idbCommand_1.Connection == null)
            {
                idbCommand_1.Connection = this.idbConnection_0;
            }
            DataSet dataSet = new DataSet();
            IDbDataAdapter dataAdapter = this.DataAdapter;
            dataAdapter.SelectCommand = idbCommand_1;
            dataAdapter.Fill(dataSet);
            if (!flag)
            {
                this.Close();
            }
            return dataSet;
        }

        public DataSet ExecuteSqlDataSet(string string_2)
        {
            IDbCommand idbCommand0 = this.idbCommand_0;
            idbCommand0.CommandText = string_2;
            return this.ExecuteSqlDataSet(idbCommand0);
        }

        public DataTable ExecuteSqlDataTable(IDbCommand idbCommand_1)
        {
            DataTable item;
            DataSet dataSet = this.ExecuteSqlDataSet(idbCommand_1);
            if (dataSet.Tables.Count <= 0)
            {
                item = null;
            }
            else
            {
                item = dataSet.Tables[0];
            }
            return item;
        }

        public DataTable ExecuteSqlDataTable(string string_2, IDbTransaction idbTransaction_1)
        {
            IDbCommand command = this.Command;
            if (idbTransaction_1 != null)
            {
                command.Transaction = idbTransaction_1;
            }
            command.CommandText = string_2;
            return this.ExecuteSqlDataTable(command);
        }

        public DataTable ExecuteSqlDataTable(string string_2)
        {
            IDbCommand command = this.Command;
            command.CommandText = string_2;
            return this.ExecuteSqlDataTable(command);
        }

        public int ExecuteSqlNonQuery(string string_2, IDbTransaction idbTransaction_1)
        {
            IDbCommand command = this.Command;
            if (idbTransaction_1 != null)
            {
                command.Transaction = idbTransaction_1;
            }
            command.CommandText = string_2;
            return this.ExecuteSqlNonQuery(command);
        }

        public int ExecuteSqlNonQuery(string string_2)
        {
            return this.ExecuteSqlNonQuery(string_2, (IDbTransaction) null);
        }

        public int ExecuteSqlNonQuery(string string_2, params object[] object_0)
        {
            return this.ExecuteSqlNonQuery(string_2, null, object_0);
        }

        public int ExecuteSqlNonQuery(string string_2, IDbTransaction idbTransaction_1, params object[] object_0)
        {
            int num;
            IDbCommand command = this.Command;
            command.CommandText = string_2;
            command.Parameters.Clear();
            if (command.Connection == null)
            {
                command.Connection = this.idbConnection_0;
            }
            if (idbTransaction_1 != null)
            {
                command.Transaction = idbTransaction_1;
            }
            for (int i = 0; i < (int) object_0.Length; i++)
            {
                object object0 = object_0[i];
                if (object0 == null)
                {
                    object0 = string.Empty;
                }
                DbParameter dbParameter = null;
                if (this.ParameterChar == '@')
                {
                    num = i + 1;
                    dbParameter = this.CreateParameter(string.Concat("p", num.ToString()), object0);
                }
                else
                {
                    num = i + 1;
                    dbParameter = this.CreateParameter(string.Concat(this.ParameterChar, "p", num.ToString()), object0);
                }
                command.Parameters.Add(dbParameter);
            }
            return this.ExecuteSqlNonQuery(command);
        }

        public int ExecuteSqlNonQuery(IDbCommand idbCommand_1)
        {
            bool flag = true;
            if (this.idbConnection_0.State == ConnectionState.Closed)
            {
                this.Open();
                flag = false;
            }
            if (idbCommand_1.Connection == null)
            {
                idbCommand_1.Connection = this.idbConnection_0;
            }
            int num = idbCommand_1.ExecuteNonQuery();
            if (!flag)
            {
                this.Close();
            }
            return num;
        }

        public object ExecuteSqlValue(string string_2)
        {
            IDbCommand command = this.Command;
            command.CommandText = string_2;
            if (command.Connection == null)
            {
                command.Connection = this.idbConnection_0;
            }
            return this.ExecuteSqlValue(command);
        }

        public object ExecuteSqlValue(IDbCommand idbCommand_1)
        {
            object obj;
            DataTable dataTable = this.ExecuteSqlDataTable(idbCommand_1);
            obj = ((dataTable == null || dataTable.Rows.Count <= 0 ? true : dataTable.Columns.Count <= 0)
                ? null
                : dataTable.Rows[0][0]);
            return obj;
        }

        public bool FieldExist(string string_2, string string_3)
        {
            return this.IsExistsField(string_2, string_3);
        }

        public abstract IDbCommand GeDataProviderCommand();

        public abstract DataProviderType GetCurrentDataProviderType();

        public abstract IDbConnection GetDataProviderConnection();

        public abstract IDbDataAdapter GetDataProviderDataAdapter();

        public abstract DbCommand GetDbCommand();

        public abstract DbCommandBuilder GetDbCommandBuilder();

        public abstract DbConnection GetDbConnection();

        public abstract DbDataAdapter GetDbDataAdapter();

        public abstract DbDataAdapter GetDbDataAdapter(DbCommand dbCommand_0);

        public string getFieldValue(string string_2)
        {
            string str = "";
            if (string_2 != "")
            {
                try
                {
                    str = (this.dataTable_0 == null ? "" : this.dataTable_0.Rows[0][string_2].ToString());
                }
                catch
                {
                    str = "";
                }
            }
            return str;
        }

        public abstract bool IsExistsField(string string_2, string string_3);

        public abstract bool IsExistsTable(string string_2, string string_3);

        IDbTransaction IConnection.BeginTransaction()
        {
            this.BeginTransaction();
            return this.Transaction;
        }

        void IConnection.Open()
        {
            this.Open();
        }

        private void method_0(CommandType commandType_0, string string_2, IDataParameter[] idataParameter_0)
        {
            if (this.idbConnection_0 == null)
            {
                this.idbConnection_0 = this.GetDataProviderConnection();
                this.idbConnection_0.ConnectionString = this.ConnectionString;
            }
            if (this.idbConnection_0.State != ConnectionState.Open)
            {
                this.idbConnection_0.Open();
            }
            if (this.idbCommand_0 == null)
            {
                this.idbCommand_0 = this.GeDataProviderCommand();
            }
            this.idbCommand_0.Connection = this.idbConnection_0;
            this.idbCommand_0.CommandText = string_2;
            this.idbCommand_0.CommandType = commandType_0;
            if (this.idbTransaction_0 != null)
            {
                this.idbCommand_0.Transaction = this.idbTransaction_0;
            }
            if (idataParameter_0 != null)
            {
                IDataParameter[] idataParameter0 = idataParameter_0;
                for (int i = 0; i < (int) idataParameter0.Length; i++)
                {
                    IDataParameter dataParameter = idataParameter0[i];
                    this.idbCommand_0.Parameters.Add(dataParameter);
                }
            }
        }

        public bool Open()
        {
            bool flag;
            try
            {
                if (this.State != ConnectionState.Closed)
                {
                    this.idbConnection_0.Close();
                }
                this.idbConnection_0.ConnectionString = this.ConnectionString;
                this.idbConnection_0.Open();
                flag = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                flag = false;
            }
            return flag;
        }

        public void ReOpen(string string_2)
        {
            this.string_0 = string_2;
            this.Close();
            this.idbConnection_0.ConnectionString = this.string_0;
            this.idbConnection_0.Open();
        }

        public void RollbackTransaction()
        {
            if (this.idbTransaction_0 != null)
            {
                try
                {
                    try
                    {
                        this.idbTransaction_0.Rollback();
                    }
                    catch
                    {
                    }
                }
                finally
                {
                    this.idbConnection_0.Close();
                    this.idbTransaction_0 = null;
                }
            }
        }

        public bool TableExist(string string_2, string string_3)
        {
            return this.IsExistsTable(string_2, string_3);
        }
    }
}