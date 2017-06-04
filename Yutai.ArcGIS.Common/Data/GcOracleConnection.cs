using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Diagnostics;

namespace Yutai.ArcGIS.Common.Data
{
	internal sealed class GcOracleConnection : BaseConnection
	{
		private OracleConnection oracleConnection_0 = null;

		public override string ADOConnectionString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override DbCommandBuilder CommandBuilder
		{
			get
			{
				return new OracleCommandBuilder();
			}
		}

		public override IDbConnection Connection
		{
			get
			{
				return this.oracleConnection_0;
			}
		}

		public override IDbDataAdapter DataAdapter
		{
			get
			{
				return new OracleDataAdapter();
			}
		}

		public override DataProviderType DataProviderType
		{
			get
			{
				return DataProviderType.Oracle;
			}
		}

		public override char ParameterChar
		{
			get
			{
				return ':';
			}
		}

		public GcOracleConnection(string string_0)
		{
			try
			{
				this.oracleConnection_0 = new OracleConnection(string_0);
			}
			catch (Exception exception)
			{
				Debug.Write(exception.Message);
			}
		}

		public GcOracleConnection(string string_0, string string_1, string string_2, string string_3) : this(string.Concat(new string[] { "Data Source=", string_0, ";User ID=", string_2, ";Password=", string_3 }))
		{
		}

		public override DbParameter CreateParameter(string string_0, object object_0)
		{
			return new OracleParameter(string_0, object_0);
		}

		public override bool FieldExist(string string_0, string string_1)
		{
			bool flag = false;
			string str = "";
			str = string.Concat("select * from ", string_1, " where 1<>1");
			DataSet dataSet = this.ExecuteSqlDataSet(str);
			if (dataSet != null)
			{
				DataTable item = dataSet.Tables[0];
				int num = 0;
				while (num < item.Columns.Count)
				{
					if (item.Columns[num].ColumnName.ToString().ToUpper() == string_0.ToString().ToUpper())
					{
						flag = true;
						return flag;
					}
					else
					{
						num++;
					}
				}
				item.Dispose();
				item = null;
			}
			dataSet.Dispose();
			dataSet = null;
			return flag;
		}

		public override bool TableExist(string string_0, string string_1)
		{
			bool flag = false;
			string str = "select * from SYS.ALL_TABLES ";
			str = string.Concat(str, " where UPPER(TRIM(OWNER))='", string_1.Trim().ToUpper(), "' and ");
			str = string.Concat(str, " upper(trim(TABLE_NAME))='", string_0.Trim().ToUpper(), "'");
			DataSet dataSet = this.ExecuteSqlDataSet(str);
			if (dataSet == null)
			{
				flag = false;
			}
			else
			{
				flag = (dataSet.Tables[0].Rows.Count <= 0 ? false : true);
			}
			return flag;
		}
	}
}