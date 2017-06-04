using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;

namespace Yutai.ArcGIS.Common.Data
{
	public abstract class BaseConnection : IDisposable, IConnection
	{
		private IDbTransaction idbTransaction_0 = null;

		public abstract string ADOConnectionString
		{
			get;
		}

		public IDbCommand Command
		{
			get
			{
				IDbCommand idbTransaction0 = this.Connection.CreateCommand();
				idbTransaction0.Transaction = this.idbTransaction_0;
				return idbTransaction0;
			}
		}

		public abstract DbCommandBuilder CommandBuilder
		{
			get;
		}

		public abstract IDbConnection Connection
		{
			get;
		}

		public virtual string ConnectionString
		{
			get
			{
				string str;
				str = (this.Connection == null ? string.Empty : this.Connection.ConnectionString);
				return str;
			}
		}

		public abstract IDbDataAdapter DataAdapter
		{
			get;
		}

		public abstract DataProviderType DataProviderType
		{
			get;
		}

		public abstract char ParameterChar
		{
			get;
		}

		public IDbTransaction Transaction
		{
			get
			{
				return this.idbTransaction_0;
			}
		}

		protected BaseConnection()
		{
		}

		public IDbTransaction BeginTransaction()
		{
			this.idbTransaction_0 = this.Connection.BeginTransaction();
			return this.idbTransaction_0;
		}

		public IDbTransaction BeginTransaction(IsolationLevel isolationLevel_0)
		{
			this.idbTransaction_0 = this.Connection.BeginTransaction(isolationLevel_0);
			return this.idbTransaction_0;
		}

		public virtual void Close()
		{
			IDbConnection connection = this.Connection;
			if ((connection == null ? false : connection.State == ConnectionState.Open))
			{
				connection.Close();
			}
		}

		public abstract DbParameter CreateParameter(string string_0, object object_0);

		public void Dispose()
		{
			if (this.Connection.State == ConnectionState.Open)
			{
				this.Connection.Close();
				this.Connection.Dispose();
			}
		}

		public virtual void ExecuteScriptFile(string string_0, string string_1)
		{
			List<string> strs = new List<string>();
			if (File.Exists(string_0))
			{
				StreamReader streamReader = new StreamReader(string_0, Encoding.Default);
				try
				{
					strs.Add(string.Concat("use ", string_1));
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
						((IDisposable)streamReader).Dispose();
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
							string[] message = new string[] { "\r\n[", null, null, null, null, null };
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
								((IDisposable)streamWriter).Dispose();
							}
						}
					}
				}
				if (File.Exists(str1))
				{
					MessageBox.Show(File.ReadAllText(str1), "提示");
				}
			}
		}

		public virtual DataSet ExecuteSqlDataSet(string string_0)
		{
			IDbCommand command = this.Command;
			command.CommandText = string_0;
			return this.ExecuteSqlDataSet(command);
		}

		public virtual DataSet ExecuteSqlDataSet(IDbCommand idbCommand_0)
		{
			bool flag = true;
			if (this.Connection.State == ConnectionState.Closed)
			{
				this.Open();
				flag = false;
			}
			DataSet dataSet = new DataSet();
			IDbDataAdapter dataAdapter = this.DataAdapter;
			dataAdapter.SelectCommand = idbCommand_0;
			dataAdapter.Fill(dataSet);
			if (!flag)
			{
				this.Close();
			}
			return dataSet;
		}

		public virtual DataTable ExecuteSqlDataTable(string string_0)
		{
			IDbCommand command = this.Command;
			command.CommandText = string_0;
			return this.ExecuteSqlDataTable(command);
		}

		public virtual DataTable ExecuteSqlDataTable(string string_0, IDbTransaction idbTransaction_1)
		{
			IDbCommand command = this.Command;
			if (idbTransaction_1 != null)
			{
				command.Transaction = idbTransaction_1;
			}
			command.CommandText = string_0;
			return this.ExecuteSqlDataTable(command);
		}

		public virtual DataTable ExecuteSqlDataTable(IDbCommand idbCommand_0)
		{
			DataTable item;
			DataSet dataSet = this.ExecuteSqlDataSet(idbCommand_0);
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

		public virtual int ExecuteSqlNonQuery(string string_0)
		{
			return this.ExecuteSqlNonQuery(string_0, (IDbTransaction)null);
		}

		public virtual int ExecuteSqlNonQuery(string string_0, IDbTransaction idbTransaction_1)
		{
			IDbCommand command = this.Command;
			if (idbTransaction_1 != null)
			{
				command.Transaction = idbTransaction_1;
			}
			command.CommandText = string_0;
			return this.ExecuteSqlNonQuery(command);
		}

		public virtual int ExecuteSqlNonQuery(string string_0, params object[] object_0)
		{
			return this.ExecuteSqlNonQuery(string_0, null, object_0);
		}

		public virtual int ExecuteSqlNonQuery(string string_0, IDbTransaction idbTransaction_1, params object[] object_0)
		{
			int num;
			IDbCommand command = this.Command;
			command.CommandText = string_0;
			command.Parameters.Clear();
			if (idbTransaction_1 != null)
			{
				command.Transaction = idbTransaction_1;
			}
			for (int i = 0; i < (int)object_0.Length; i++)
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

		public virtual int ExecuteSqlNonQuery(IDbCommand idbCommand_0)
		{
			bool flag = true;
			if (this.Connection.State == ConnectionState.Closed)
			{
				this.Open();
				flag = false;
			}
			int num = idbCommand_0.ExecuteNonQuery();
			if (!flag)
			{
				this.Close();
			}
			return num;
		}

		public virtual object ExecuteSqlValue(string string_0)
		{
			IDbCommand command = this.Command;
			command.CommandText = string_0;
			return this.ExecuteSqlValue(command);
		}

		public virtual object ExecuteSqlValue(IDbCommand idbCommand_0)
		{
			object obj;
			DataTable dataTable = this.ExecuteSqlDataTable(idbCommand_0);
			obj = ((dataTable == null || dataTable.Rows.Count <= 0 ? true : dataTable.Columns.Count <= 0) ? null : dataTable.Rows[0][0]);
			return obj;
		}

		public abstract bool FieldExist(string string_0, string string_1);

		public virtual void Open()
		{
			IDbConnection connection = this.Connection;
			if ((connection == null ? false : connection.State == ConnectionState.Closed))
			{
				connection.Open();
			}
		}

		public static BaseConnection ShowConnectionDialog(DataProviderType dataProviderType_0, string string_0)
		{
			BaseConnection gcOleDbConnection;
			DataConnectionDialog dataConnectionDialog = new DataConnectionDialog();
			dataConnectionDialog.DataSources.Clear();
			switch (dataProviderType_0)
			{
				case DataProviderType.OleDb:
				{
					dataConnectionDialog.DataSources.Add(DataSource.AccessDataSource);
					dataConnectionDialog.SetSelectedDataProvider(DataSource.AccessDataSource,DataProvider.OleDBDataProvider);
					dataConnectionDialog.SelectedDataSource=DataSource.AccessDataSource;
					break;
				}
				case DataProviderType.Oracle:
				{
					dataConnectionDialog.DataSources.Add(DataSource.OracleDataSource);
					dataConnectionDialog.SetSelectedDataProvider(DataSource.OracleDataSource,DataProvider.OracleDataProvider);
					dataConnectionDialog.SelectedDataSource=DataSource.OracleDataSource;
					break;
				}
				case DataProviderType.Sql:
				{
                        dataConnectionDialog.DataSources.Add(DataSource.SqlDataSource);
                        dataConnectionDialog.SetSelectedDataProvider(DataSource.OracleDataSource, DataProvider.SqlDataProvider);
                        dataConnectionDialog.SelectedDataSource = DataSource.SqlDataSource;
                        break;
				}
			}
			try
			{
				dataConnectionDialog.ConnectionString=string_0;
			}
			catch
			{
			}
			if (DataConnectionDialog.Show(dataConnectionDialog) != DialogResult.OK)
			{
				gcOleDbConnection = null;
			}
			else
			{
				switch (dataProviderType_0)
				{
					case DataProviderType.OleDb:
					{
						gcOleDbConnection = new GcOleDbConnection(dataConnectionDialog.ConnectionString);
						break;
					}
					case DataProviderType.Oracle:
					{
						gcOleDbConnection = new GcOracleConnection(dataConnectionDialog.ConnectionString);
						break;
					}
					case DataProviderType.Sql:
					{
						gcOleDbConnection = new GcSqlConnection(dataConnectionDialog.ConnectionString);
						break;
					}
					default:
					{
						gcOleDbConnection = null;
						break;
					}
				}
			}
			return gcOleDbConnection;
		}

		public abstract bool TableExist(string string_0, string string_1);
	}
}