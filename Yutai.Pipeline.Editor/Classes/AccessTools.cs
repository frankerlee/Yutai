using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOX;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Classes
{
    public class AccessTools
    {
        private string _DbPath;
        private string _strErrorInfo;

        public AccessTools(string local)
        {
            _DbPath = local;
        }

        /// <summary>
        /// ACCESS数据库路径，包含文件名称
        /// </summary>
        public string DbPath
        {
            get { return _DbPath; }
            set { _DbPath = value; }
        }

        /// <summary>
        /// 获取异常信息
        /// </summary>
        /// <returns></returns>
        public string GetStrErrorInfo()
        {
            return _strErrorInfo;
        }

        /// <summary>
        /// 动态创建ACCESS
        /// </summary>
        /// <returns></returns>
        public bool CreateAccess()
        {
            try
            {
                ADOX.Catalog catalog = new ADOX.Catalog();
                catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DbPath + ";Jet OLEDB:Engine Type=5");
                return true;
            }
            catch (Exception ex)
            {
                _strErrorInfo = ex.Message;
                return false;
            }
        }

        public bool CreateTable(IFeatureClass featureClass)
        {
            try
            {
                ADOX.Catalog catalog = new ADOX.Catalog();
                //创建链接
                ADODB.Connection cn = new ADODB.Connection();
                //打开
                cn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DbPath, null, null, -1);
                //激活链接
                catalog.ActiveConnection = cn;
                //创建表
                ADOX.Table table = new ADOX.Table();
                table.Name = featureClass.AliasName;
                //创建列
                ADOX.Column column = new ADOX.Column();
                column.ParentCatalog = catalog;
                //列名称
                column.Name = featureClass.OIDFieldName;
                //列类型
                column.Type = DataTypeEnum.adInteger;
                //默认长度
                column.DefinedSize = 9;
                //自动增长列
                column.Properties["AutoIncrement"].Value = true;
                //将列添加到表中
                table.Columns.Append(column, DataTypeEnum.adInteger, 9);
                //第一列为主键
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    IField field = featureClass.Fields.Field[i];
                    if (field.Type == esriFieldType.esriFieldTypeOID)
                        continue;
                    ADOX.Column newColumn = new ADOX.Column();
                    newColumn.ParentCatalog = catalog;
                    newColumn.Name = field.Name;
                    newColumn.Type = ConvertToDataTypeEnum(field.Type);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry ||
                        field.Type == esriFieldType.esriFieldTypeBlob || field.Type == esriFieldType.esriFieldTypeRaster)
                        newColumn.DefinedSize = 255;
                    else if (field.Type != esriFieldType.esriFieldTypeXML)
                        newColumn.DefinedSize = field.Length;
                    newColumn.Properties[3].Value = true;
                    table.Columns.Append(newColumn, ConvertToDataTypeEnum(field.Type), field.Length);
                }

                catalog.Tables.Append(table);

                cn.Close();

                return true;
            }
            catch (Exception ex)
            {
                _strErrorInfo = ex.Message;
                return false;
            }

        }

        public bool CreateTable(IDictionary<int, IField> fields, string tableName)
        {
            try
            {
                ADOX.Catalog catalog = new ADOX.Catalog();
                //创建链接
                ADODB.Connection cn = new ADODB.Connection();
                //打开
                cn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DbPath, null, null, -1);
                //激活链接
                catalog.ActiveConnection = cn;
                //创建表
                ADOX.Table table = new ADOX.Table();
                table.Name = tableName;
                //创建列
                ADOX.Column column = new ADOX.Column();
                column.ParentCatalog = catalog;
                //列名称
                column.Name = "ID";
                //列类型
                column.Type = DataTypeEnum.adInteger;
                //默认长度
                column.DefinedSize = 9;
                //自动增长列
                column.Properties["AutoIncrement"].Value = true;
                //将列添加到表中
                table.Columns.Append(column, DataTypeEnum.adInteger, 9);
                //第一列为主键
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                foreach (KeyValuePair<int, IField> keyValuePair in fields)
                {
                    IField field = keyValuePair.Value;
                    ADOX.Column newColumn = new ADOX.Column();
                    newColumn.ParentCatalog = catalog;
                    newColumn.Name = field.Name;
                    newColumn.Type = ConvertToDataTypeEnum(field.Type);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry ||
                        field.Type == esriFieldType.esriFieldTypeBlob || field.Type == esriFieldType.esriFieldTypeRaster)
                        newColumn.DefinedSize = 255;
                    else if (field.Type != esriFieldType.esriFieldTypeXML)
                        newColumn.DefinedSize = field.Length;
                    newColumn.Properties[3].Value = true;
                    table.Columns.Append(newColumn, ConvertToDataTypeEnum(field.Type), field.Length);
                }

                catalog.Tables.Append(table);

                cn.Close();

                return true;
            }
            catch (Exception ex)
            {
                _strErrorInfo = ex.Message;
                return false;
            }

        }

        public bool FillTable(DataTable dataTable)
        {
            try
            {
                string select = "select * from " + dataTable.TableName;
                OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DbPath);
                OleDbDataAdapter adapter = new OleDbDataAdapter(select, oleDbConnection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                builder.QuotePrefix = "[";
                builder.QuoteSuffix = "]";
                adapter.Update(dataTable);
                oleDbConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                _strErrorInfo = $"{dataTable.TableName}:{ex.Message}";
                return false;
            }
        }

        public DataTypeEnum ConvertToDataTypeEnum(esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return DataTypeEnum.adSmallInt;
                case esriFieldType.esriFieldTypeInteger:
                    return DataTypeEnum.adInteger;
                case esriFieldType.esriFieldTypeSingle:
                    return DataTypeEnum.adSingle;
                case esriFieldType.esriFieldTypeDouble:
                    return DataTypeEnum.adDouble;
                case esriFieldType.esriFieldTypeString:
                    return DataTypeEnum.adVarWChar;
                case esriFieldType.esriFieldTypeDate:
                    return DataTypeEnum.adDBDate;
                case esriFieldType.esriFieldTypeOID:
                    return DataTypeEnum.adInteger;
                case esriFieldType.esriFieldTypeGeometry:
                    return DataTypeEnum.adVarWChar;
                case esriFieldType.esriFieldTypeBlob:
                    return DataTypeEnum.adVarWChar;
                case esriFieldType.esriFieldTypeRaster:
                    return DataTypeEnum.adVarWChar;
                case esriFieldType.esriFieldTypeGUID:
                    return DataTypeEnum.adVarWChar;
                case esriFieldType.esriFieldTypeGlobalID:
                    return DataTypeEnum.adVarWChar;
                case esriFieldType.esriFieldTypeXML:
                    return DataTypeEnum.adLongVarWChar;
                default:
                    return DataTypeEnum.adVarWChar;
            }
        }
    }
}
