using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Yutai.ArcGIS.Common.Data
{
	public class SqlDataAccessLayer : DataAccessLayerBaseClass
	{
		public override char ParameterChar
		{
			get
			{
				return '@';
			}
		}

		public SqlDataAccessLayer()
		{
		}

		public SqlDataAccessLayer(string string_2)
		{
			base.ConnectionString = string_2;
		}

		public override DbParameter CreateParameter(string string_2, object object_0)
		{
			return new SqlParameter(string_2, object_0);
		}

		public override DbParameter CreateParameter(string string_2)
		{
			return new SqlParameter()
			{
				ParameterName = string_2
			};
		}

		public override IDbCommand GeDataProviderCommand()
		{
			return new SqlCommand();
		}

		public override DataProviderType GetCurrentDataProviderType()
		{
			return DataProviderType.Sql;
		}

		public override IDbConnection GetDataProviderConnection()
		{
			return new SqlConnection();
		}

		public override IDbDataAdapter GetDataProviderDataAdapter()
		{
			return new SqlDataAdapter();
		}

		public override DbCommand GetDbCommand()
		{
			return new SqlCommand();
		}

		public override DbCommandBuilder GetDbCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		public override DbConnection GetDbConnection()
		{
			return new SqlConnection();
		}

		public override DbDataAdapter GetDbDataAdapter()
		{
			return new SqlDataAdapter();
		}

		public override DbDataAdapter GetDbDataAdapter(DbCommand dbCommand_0)
		{
			return new SqlDataAdapter(dbCommand_0 as SqlCommand);
		}

		public override bool IsExistsField(string string_2, string string_3)
		{
			bool flag = false;
			string str = "";
			str = string.Concat("select * from ", string_3, " where 1<>1");
			DataSet dataSet = base.ExecuteDataSet(str);
			if (dataSet != null)
			{
				DataTable item = dataSet.Tables[0];
				int num = 0;
				while (num < item.Columns.Count)
				{
					if (item.Columns[num].ColumnName.ToString().ToUpper() == string_2.ToString().ToUpper())
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

		public override bool IsExistsTable(string string_2, string string_3)
		{
			bool flag = false;
			string str = string.Concat("select * from Sysobjects where Xtype='u'and name='", string_2, "'");
			DataSet dataSet = base.ExecuteDataSet(str);
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