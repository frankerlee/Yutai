using System.Data;
using System.Data.Common;
using System.Data.Odbc;

namespace Yutai.ArcGIS.Common.Data
{
    public class OdbcDataAccessLayer : DataAccessLayerBaseClass
    {
        public override char ParameterChar
        {
            get { return '@'; }
        }

        public OdbcDataAccessLayer()
        {
        }

        public OdbcDataAccessLayer(string string_2)
        {
            base.ConnectionString = string_2;
        }

        public override DbParameter CreateParameter(string string_2, object object_0)
        {
            return null;
        }

        public override DbParameter CreateParameter(string string_2)
        {
            return null;
        }

        public override IDbCommand GeDataProviderCommand()
        {
            return new OdbcCommand();
        }

        public override DataProviderType GetCurrentDataProviderType()
        {
            return DataProviderType.Odbc;
        }

        public override IDbConnection GetDataProviderConnection()
        {
            return new OdbcConnection();
        }

        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new OdbcDataAdapter();
        }

        public override DbCommand GetDbCommand()
        {
            return new OdbcCommand();
        }

        public override DbCommandBuilder GetDbCommandBuilder()
        {
            return new OdbcCommandBuilder();
        }

        public override DbConnection GetDbConnection()
        {
            return new OdbcConnection();
        }

        public override DbDataAdapter GetDbDataAdapter()
        {
            return new OdbcDataAdapter();
        }

        public override DbDataAdapter GetDbDataAdapter(DbCommand dbCommand_0)
        {
            return new OdbcDataAdapter(dbCommand_0 as OdbcCommand);
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
            return false;
        }
    }
}