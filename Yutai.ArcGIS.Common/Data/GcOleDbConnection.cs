using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Yutai.ArcGIS.Common.Data
{
    internal sealed class GcOleDbConnection : BaseConnection
    {
        private OleDbConnection oleDbConnection_0 = null;

        public override string ADOConnectionString
        {
            get { throw new NotImplementedException(); }
        }

        public override DbCommandBuilder CommandBuilder
        {
            get { return new OleDbCommandBuilder(); }
        }

        public override IDbConnection Connection
        {
            get { return this.oleDbConnection_0; }
        }

        public override IDbDataAdapter DataAdapter
        {
            get { return new OleDbDataAdapter(); }
        }

        public override DataProviderType DataProviderType
        {
            get { return DataProviderType.OleDb; }
        }

        public override char ParameterChar
        {
            get { return '@'; }
        }

        public GcOleDbConnection(string string_0, string string_1, string string_2)
        {
            this.oleDbConnection_0 =
                new OleDbConnection(
                    string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=True",
                        string_0));
        }

        public GcOleDbConnection(string string_0)
        {
            this.oleDbConnection_0 = new OleDbConnection(string_0);
        }

        public override DbParameter CreateParameter(string string_0, object object_0)
        {
            return new OleDbParameter(string_0, object_0);
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
            string str = string.Concat("select * from ", string_0, " where 1<>1");
            try
            {
                try
                {
                    this.ExecuteSqlDataSet(str);
                    flag = true;
                }
                catch
                {
                    flag = false;
                }
            }
            finally
            {
            }
            return flag;
        }
    }
}