using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Yutai.ArcGIS.Common.Data
{
	public class OleDbDataAccessLayer : DataAccessLayerBaseClass
	{
		public override char ParameterChar
		{
			get
			{
				return '@';
			}
		}

		public OleDbDataAccessLayer()
		{
		}

		public OleDbDataAccessLayer(string string_2)
		{
			base.ConnectionString = string_2;
		}

		public override DbParameter CreateParameter(string string_2, object object_0)
		{
			return new OleDbParameter(string_2, object_0);
		}

		public override DbParameter CreateParameter(string string_2)
		{
			return new OleDbParameter()
			{
				ParameterName = string_2
			};
		}

		public override IDbCommand GeDataProviderCommand()
		{
			return new OleDbCommand();
		}

		public override DataProviderType GetCurrentDataProviderType()
		{
			return DataProviderType.OleDb;
		}

		public override IDbConnection GetDataProviderConnection()
		{
			return new OleDbConnection();
		}

		public override IDbDataAdapter GetDataProviderDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		public override DbCommand GetDbCommand()
		{
			return new OleDbCommand();
		}

		public override DbCommandBuilder GetDbCommandBuilder()
		{
			return new OleDbCommandBuilder();
		}

		public override DbConnection GetDbConnection()
		{
			return new OleDbConnection();
		}

		public override DbDataAdapter GetDbDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		public override DbDataAdapter GetDbDataAdapter(DbCommand dbCommand_0)
		{
			return new OleDbDataAdapter(dbCommand_0 as OleDbCommand);
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
			string str = string.Concat("select * from ", string_2, " where 1<>1");
			DataSet dataSet = null;
			try
			{
				try
				{
					dataSet = base.ExecuteDataSet(str);
					flag = ((dataSet == null ? true : dataSet.Tables.Count <= 0) ? false : true);
				}
				catch
				{
					flag = false;
				}
			}
			finally
			{
				dataSet = null;
			}
			return flag;
		}
	}
}