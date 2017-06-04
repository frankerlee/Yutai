using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

namespace Yutai.ArcGIS.Common.Data
{
	public class OracleDataAccessLayer : DataAccessLayerBaseClass
	{
		public override char ParameterChar
		{
			get
			{
				return ':';
			}
		}

		public OracleDataAccessLayer()
		{
		}

		public OracleDataAccessLayer(string string_2)
		{
			base.ConnectionString = string_2;
		}

		public override DbParameter CreateParameter(string string_2, object object_0)
		{
			return new OracleParameter(string_2, object_0);
		}

		public override DbParameter CreateParameter(string string_2)
		{
			return new OracleParameter()
			{
				ParameterName = string_2
			};
		}

		public override IDbCommand GeDataProviderCommand()
		{
			return new OracleCommand();
		}

		public override DataProviderType GetCurrentDataProviderType()
		{
			return DataProviderType.Oracle;
		}

		public override IDbConnection GetDataProviderConnection()
		{
			return new OracleConnection();
		}

		public override IDbDataAdapter GetDataProviderDataAdapter()
		{
			return new OracleDataAdapter();
		}

		public override DbCommand GetDbCommand()
		{
			return new OracleCommand();
		}

		public override DbCommandBuilder GetDbCommandBuilder()
		{
			return new OracleCommandBuilder();
		}

		public override DbConnection GetDbConnection()
		{
			return new OracleConnection();
		}

		public override DbDataAdapter GetDbDataAdapter()
		{
			return new OracleDataAdapter();
		}

		public override DbDataAdapter GetDbDataAdapter(DbCommand dbCommand_0)
		{
			return new OracleDataAdapter(dbCommand_0 as OracleCommand);
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
			string str = "select * from SYS.ALL_TABLES ";
			str = string.Concat(str, " where UPPER(TRIM(OWNER))='", string_3.Trim().ToUpper(), "' and ");
			str = string.Concat(str, " upper(trim(TABLE_NAME))='", string_2.Trim().ToUpper(), "'");
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