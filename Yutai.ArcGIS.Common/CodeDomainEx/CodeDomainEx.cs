using System.Collections.Specialized;
using System.Data;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Common.CodeDomainEx
{
    public class CodeDomainEx
    {
        public string FindName(string string_8)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                DataProviderType.OleDb, this.ConnectionStr);
            string str = string.Format("select {0},{1} from {2} where {1}='{3}'",
                new object[] {this.NameFieldName, this.CodeFieldName, this.TableFieldName, string_8});
            dataAccessLayer.Open();
            DataTable table = dataAccessLayer.ExecuteDataTable(str);
            if (table.Rows.Count > 0)
            {
                string_8 = table.Rows[0][this.NameFieldName].ToString();
            }
            dataAccessLayer.Close();
            return string_8;
        }

        public string GetCodeByName(string string_8)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                DataProviderType.OleDb, this.ConnectionStr);
            string str = string.Format("select {0},{1} from {2} where {0}='{3}'",
                new object[] {this.NameFieldName, this.CodeFieldName, this.TableFieldName, string_8});
            dataAccessLayer.Open();
            DataTable table = dataAccessLayer.ExecuteDataTable(str);
            if (table.Rows.Count > 0)
            {
                string_8 = table.Rows[0][this.CodeFieldName].ToString();
            }
            dataAccessLayer.Close();
            return string_8;
        }

        public NameValueCollection GetCodeDomain()
        {
            NameValueCollection values = new NameValueCollection();
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                DataProviderType.OleDb, this.ConnectionStr);
            string str = string.Format("select {0},{1} from {2}", this.NameFieldName, this.CodeFieldName,
                this.TableFieldName);
            dataAccessLayer.Open();
            DataTable table = dataAccessLayer.ExecuteDataTable(str);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i][this.NameFieldName].ToString().Trim();
                string str3 = table.Rows[i][this.CodeFieldName].ToString().Trim();
                values.Add(name, str3);
            }
            dataAccessLayer.Close();
            return values;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string CodeFieldName { get; set; }

        public string ConnectionStr { get; set; }

        public string DomainID { get; set; }

        public esriFieldType FieldType { get; set; }

        public string IDFieldName { get; set; }

        public string Name { get; set; }

        public string NameFieldName { get; set; }

        public string ParentIDFieldName { get; set; }

        public string TableFieldName { get; set; }
    }
}