namespace Yutai.ArcGIS.Common.Data
{
    public class ConnectionFactory
    {
        public ConnectionFactory()
        {
        }

        public static IConnection CreateInstance(DataProviderType dataProviderType_0, string string_0)
        {
            IConnection gcOleDbConnection;
            switch (dataProviderType_0)
            {
                case DataProviderType.OleDb:
                {
                    gcOleDbConnection = new GcOleDbConnection(string_0);
                    break;
                }
                case DataProviderType.Oracle:
                {
                    gcOleDbConnection = new GcOracleConnection(string_0);
                    break;
                }
                case DataProviderType.Sql:
                {
                    gcOleDbConnection = new GcSqlConnection(string_0);
                    break;
                }
                default:
                {
                    gcOleDbConnection = null;
                    break;
                }
            }
            return gcOleDbConnection;
        }
    }
}