using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Yutai.ArcGIS.Common.Data
{
	internal sealed class GcSqlConnection : BaseConnection
	{
		private SqlConnection sqlConnection_0 = null;

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
				return new SqlCommandBuilder();
			}
		}

		public override IDbConnection Connection
		{
			get
			{
				return this.sqlConnection_0;
			}
		}

		public override IDbDataAdapter DataAdapter
		{
			get
			{
				return new SqlDataAdapter();
			}
		}

		public override DataProviderType DataProviderType
		{
			get
			{
				return DataProviderType.Sql;
			}
		}

		public override char ParameterChar
		{
			get
			{
				return '@';
			}
		}

		public GcSqlConnection(string string_0)
		{
			try
			{
				this.sqlConnection_0 = new SqlConnection(string_0);
			}
			catch (Exception exception)
			{
				Debug.Write(exception.Message);
			}
		}

		public GcSqlConnection(string string_0, string string_1, string string_2, string string_3) : this(string.Concat(new string[] { "Data Source=", string_0, ";Initial Catalog=", string_1, ";Persist Security Info=True;User ID=", string_2, ";Password=", string_3 }))
		{
		}

		public override DbParameter CreateParameter(string string_0, object object_0)
		{
			return new SqlParameter(string_0, object_0);
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
			string str = string.Concat("select * from Sysobjects where Xtype='u'and name='", string_0, "'");
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