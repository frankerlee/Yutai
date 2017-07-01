using System;
using System.Configuration;

namespace Yutai.ArcGIS.Common.Data
{
    public sealed class DataAccessLayerFactory
    {
        private DataAccessLayerFactory()
        {
        }

        public static DataAccessLayerBaseClass GetDataAccessLayer()
        {
            DataProviderType dataProviderType;
            if ((ConfigurationManager.AppSettings["DataProviderType"] == null
                ? true
                : ConfigurationManager.AppSettings["ConnectionString"] == null))
            {
                throw new ArgumentNullException(
                    "Please specify a 'DataProviderType' and 'ConnectionString' configuration keys in the application configuration file.");
            }
            try
            {
                dataProviderType =
                    (DataProviderType)
                    Enum.Parse(typeof(DataProviderType), ConfigurationManager.AppSettings["DataProviderType"], true);
            }
            catch
            {
                throw new ArgumentException("Invalid data access layer provider type.");
            }
            return DataAccessLayerFactory.GetDataAccessLayer(dataProviderType,
                ConfigurationManager.AppSettings["ConnectionString"]);
        }

        public static DataAccessLayerBaseClass GetDataAccessLayer(DataProviderType dataProviderType_0)
        {
            return DataAccessLayerFactory.GetDataAccessLayer(dataProviderType_0, null);
        }

        public static DataAccessLayerBaseClass GetDataAccessLayer(DataProviderType dataProviderType_0, string string_0)
        {
            DataAccessLayerBaseClass oleDbDataAccessLayer;
            switch (dataProviderType_0)
            {
                case DataProviderType.Access:
                case DataProviderType.OleDb:
                {
                    oleDbDataAccessLayer = new OleDbDataAccessLayer(string_0);
                    break;
                }
                case DataProviderType.Odbc:
                {
                    oleDbDataAccessLayer = new OdbcDataAccessLayer(string_0);
                    break;
                }
                case DataProviderType.Oracle:
                {
                    oleDbDataAccessLayer = new OracleDataAccessLayer(string_0);
                    break;
                }
                case DataProviderType.Sql:
                {
                    oleDbDataAccessLayer = new SqlDataAccessLayer(string_0);
                    break;
                }
                default:
                {
                    throw new ArgumentException("Invalid data access layer provider type.");
                }
            }
            return oleDbDataAccessLayer;
        }
    }
}