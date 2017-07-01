using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Common.Priviliges
{
    public class MenuInfoHelper
    {
        private string string_0 = "";

        private DataProviderType dataProviderType_0 = DataProviderType.Sql;

        private string string_1 = "";

        public MenuInfoHelper()
        {
            try
            {
                string text = ConfigurationManager.AppSettings["SYSPRIVDB"];
                string[] array = text.Split(new string[]
                {
                    "||"
                }, System.StringSplitOptions.RemoveEmptyEntries);
                if (array[0].ToLower() == "sqlserver" || array[0].ToLower() == "sql")
                {
                    this.dataProviderType_0 = DataProviderType.Sql;
                }
                else if (array[0].ToLower() == "oracle")
                {
                    this.dataProviderType_0 = DataProviderType.Oracle;
                }
                else if (array[0].ToLower() == "oledb")
                {
                    this.dataProviderType_0 = DataProviderType.OleDb;
                }
                else if (array[0].ToLower() == "odbc")
                {
                    this.dataProviderType_0 = DataProviderType.Odbc;
                }
                else if (array[0].ToLower() == "access")
                {
                    this.dataProviderType_0 = DataProviderType.Access;
                }
                this.string_1 = array[1];
            }
            catch
            {
            }
            this.string_0 = "MAINMENUCONFIG";
        }

        public MenuInfoHelper(string string_2)
        {
            try
            {
                string text = ConfigurationManager.AppSettings["SYSPRIVDB"];
                string[] array = text.Split(new string[]
                {
                    "||"
                }, System.StringSplitOptions.RemoveEmptyEntries);
                if (array[0].ToLower() == "sqlserver" || array[0].ToLower() == "sql")
                {
                    this.dataProviderType_0 = DataProviderType.Sql;
                }
                else if (array[0].ToLower() == "oracle")
                {
                    this.dataProviderType_0 = DataProviderType.Oracle;
                }
                else if (array[0].ToLower() == "oledb")
                {
                    this.dataProviderType_0 = DataProviderType.OleDb;
                }
                else if (array[0].ToLower() == "odbc")
                {
                    this.dataProviderType_0 = DataProviderType.Odbc;
                }
                else if (array[0].ToLower() == "access")
                {
                    this.dataProviderType_0 = DataProviderType.Access;
                }
                this.string_1 = array[1];
            }
            catch
            {
            }
            this.string_0 = string_2;
        }

        public System.Collections.Generic.List<MenuInfo> Load()
        {
            System.Collections.Generic.List<MenuInfo> list = new System.Collections.Generic.List<MenuInfo>();
            try
            {
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                DataTable dataTable = dataAccessLayer.ExecuteDataTable("select * from " + this.string_0);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow dataRow = dataTable.Rows[i];
                    MenuInfo menuInfo = new MenuInfo();
                    menuInfo.MenuID = System.Convert.ToString(dataRow["MenuID"]);
                    menuInfo.NAME = System.Convert.ToString(dataRow["NAME"]);
                    object arg_8E_0 = dataRow["ORDERBY"];
                    menuInfo.ORDERBY =
                        new int?((!(dataRow["ORDERBY"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["ORDERBY"])
                            : -1);
                    menuInfo.PROGID = System.Convert.ToString(dataRow["PROGID"]);
                    menuInfo.SHORTCUT = System.Convert.ToString(dataRow["SHORTCUT"]);
                    menuInfo.SUBTYPE =
                        new int?((!(dataRow["SUBTYPE"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["SUBTYPE"])
                            : -1);
                    menuInfo.VISIBLE =
                        new bool?(dataRow["VISIBLE"] is System.DBNull || System.Convert.ToInt32(dataRow["VISIBLE"]) == 1);
                    menuInfo.ItemCol =
                        new int?((!(dataRow["ItemCol"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["ItemCol"])
                            : -1);
                    menuInfo.ISPOPMENUITEM =
                        new bool?(!(dataRow["ISPOPMENUITEM"] is System.DBNull) &&
                                  System.Convert.ToInt32(dataRow["ISPOPMENUITEM"]) == 1);
                    menuInfo.COMPONENTDLLNAME = System.Convert.ToString(dataRow["COMPONENTDLLNAME"]);
                    menuInfo.CLASSNAME = System.Convert.ToString(dataRow["CLASSNAME"]);
                    menuInfo.PARENTIDS = System.Convert.ToString(dataRow["PARENTIDS"]);
                    menuInfo.BEGINGROUP =
                        new bool?(!(dataRow["BEGINGROUP"] is System.DBNull) &&
                                  System.Convert.ToInt32(dataRow["BEGINGROUP"]) == 1);
                    menuInfo.CAPTION = System.Convert.ToString(dataRow["CAPTION"]);
                    list.Add(menuInfo);
                }
                dataAccessLayer.Close();
            }
            catch
            {
            }
            return list;
        }

        public MenuInfo GetByClassName(string string_2, string string_3)
        {
            MenuInfo menuInfo = null;
            try
            {
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(new string[]
                {
                    "select * from ",
                    this.string_0,
                    " where COMPONENTDLLNAME='",
                    string_2,
                    "' and CLASSNAME='",
                    string_3,
                    "'"
                }));
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    menuInfo = new MenuInfo();
                    menuInfo.MenuID = System.Convert.ToString(dataRow["MenuID"]);
                    menuInfo.NAME = System.Convert.ToString(dataRow["NAME"]);
                    object arg_C4_0 = dataRow["ORDERBY"];
                    menuInfo.ORDERBY =
                        new int?((!(dataRow["ORDERBY"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["ORDERBY"])
                            : -1);
                    menuInfo.PROGID = System.Convert.ToString(dataRow["PROGID"]);
                    menuInfo.SHORTCUT = System.Convert.ToString(dataRow["SHORTCUT"]);
                    menuInfo.SUBTYPE =
                        new int?((!(dataRow["SUBTYPE"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["SUBTYPE"])
                            : -1);
                    menuInfo.VISIBLE =
                        new bool?(dataRow["VISIBLE"] is System.DBNull || System.Convert.ToInt32(dataRow["VISIBLE"]) == 1);
                    menuInfo.ItemCol =
                        new int?((!(dataRow["ItemCol"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["ItemCol"])
                            : -1);
                    menuInfo.ISPOPMENUITEM =
                        new bool?(!(dataRow["ISPOPMENUITEM"] is System.DBNull) &&
                                  System.Convert.ToInt32(dataRow["ISPOPMENUITEM"]) == 1);
                    menuInfo.COMPONENTDLLNAME = System.Convert.ToString(dataRow["COMPONENTDLLNAME"]);
                    menuInfo.CLASSNAME = System.Convert.ToString(dataRow["CLASSNAME"]);
                    menuInfo.BEGINGROUP =
                        new bool?(!(dataRow["BEGINGROUP"] is System.DBNull) &&
                                  System.Convert.ToInt32(dataRow["BEGINGROUP"]) == 1);
                    menuInfo.CAPTION = System.Convert.ToString(dataRow["CAPTION"]);
                    menuInfo.PARENTIDS = System.Convert.ToString(dataRow["PARENTIDS"]);
                }
                dataAccessLayer.Close();
            }
            catch
            {
            }
            return menuInfo;
        }

        public MenuInfo GetByClassName(string string_2)
        {
            MenuInfo menuInfo = null;
            try
            {
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(new string[]
                {
                    "select * from ",
                    this.string_0,
                    " where PROGID='",
                    string_2,
                    "'"
                }));
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    menuInfo = new MenuInfo();
                    menuInfo.MenuID = System.Convert.ToString(dataRow["MenuID"]);
                    menuInfo.NAME = System.Convert.ToString(dataRow["NAME"]);
                    object arg_B8_0 = dataRow["ORDERBY"];
                    menuInfo.ORDERBY =
                        new int?((!(dataRow["ORDERBY"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["ORDERBY"])
                            : -1);
                    menuInfo.PROGID = System.Convert.ToString(dataRow["PROGID"]);
                    menuInfo.SHORTCUT = System.Convert.ToString(dataRow["SHORTCUT"]);
                    menuInfo.SUBTYPE =
                        new int?((!(dataRow["SUBTYPE"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["SUBTYPE"])
                            : -1);
                    menuInfo.VISIBLE =
                        new bool?(dataRow["VISIBLE"] is System.DBNull || System.Convert.ToInt32(dataRow["VISIBLE"]) == 1);
                    menuInfo.ItemCol =
                        new int?((!(dataRow["ItemCol"] is System.DBNull))
                            ? System.Convert.ToInt32(dataRow["ItemCol"])
                            : -1);
                    menuInfo.ISPOPMENUITEM =
                        new bool?(!(dataRow["ISPOPMENUITEM"] is System.DBNull) &&
                                  System.Convert.ToInt32(dataRow["ISPOPMENUITEM"]) == 1);
                    menuInfo.COMPONENTDLLNAME = System.Convert.ToString(dataRow["COMPONENTDLLNAME"]);
                    menuInfo.CLASSNAME = System.Convert.ToString(dataRow["CLASSNAME"]);
                    menuInfo.BEGINGROUP =
                        new bool?(!(dataRow["BEGINGROUP"] is System.DBNull) &&
                                  System.Convert.ToInt32(dataRow["BEGINGROUP"]) == 1);
                    menuInfo.CAPTION = System.Convert.ToString(dataRow["CAPTION"]);
                    menuInfo.PARENTIDS = System.Convert.ToString(dataRow["PARENTIDS"]);
                }
                dataAccessLayer.Close();
            }
            catch
            {
            }
            return menuInfo;
        }

        public void ClearAll()
        {
            try
            {
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                dataAccessLayer.ExecuteNonQuery("delete from " + this.string_0, new object[0]);
                dataAccessLayer.Close();
            }
            catch
            {
            }
        }

        public int Add(MenuInfo menuInfo_0)
        {
            int result;
            if (this.IsExist(menuInfo_0))
            {
                result = 0;
            }
            else
            {
                string string_ =
                    string.Format(
                        "insert into {0}([NAME],PARENTIDS,ORDERBY,[PROGID],[SHORTCUT],SUBTYPE,VISIBLE,ItemCol,ISPOPMENUITEM,COMPONENTDLLNAME,CLASSNAME,BEGINGROUP,CAPTION) values({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})",
                        new object[]
                        {
                            this.string_0,
                            string.IsNullOrEmpty(menuInfo_0.NAME) ? "NULL" : ("'" + menuInfo_0.NAME + "'"),
                            string.IsNullOrEmpty(menuInfo_0.PARENTIDS) ? "NULL" : ("'" + menuInfo_0.PARENTIDS + "'"),
                            menuInfo_0.ORDERBY.HasValue ? menuInfo_0.ORDERBY.Value.ToString() : "NULL",
                            string.IsNullOrEmpty(menuInfo_0.PROGID) ? "NULL" : ("'" + menuInfo_0.PROGID + "'"),
                            string.IsNullOrEmpty(menuInfo_0.SHORTCUT) ? "NULL" : ("'" + menuInfo_0.SHORTCUT + "'"),
                            menuInfo_0.SUBTYPE.HasValue ? menuInfo_0.SUBTYPE.Value.ToString() : "NULL",
                            menuInfo_0.VISIBLE.HasValue ? (menuInfo_0.VISIBLE.Value ? "1" : "0") : "NULL",
                            menuInfo_0.ItemCol.HasValue ? menuInfo_0.ItemCol.Value.ToString() : "NULL",
                            menuInfo_0.ISPOPMENUITEM.HasValue ? (menuInfo_0.ISPOPMENUITEM.Value ? "1" : "0") : "NULL",
                            string.IsNullOrEmpty(menuInfo_0.COMPONENTDLLNAME)
                                ? "NULL"
                                : ("'" + menuInfo_0.COMPONENTDLLNAME + "'"),
                            string.IsNullOrEmpty(menuInfo_0.CLASSNAME) ? "NULL" : ("'" + menuInfo_0.CLASSNAME + "'"),
                            menuInfo_0.BEGINGROUP.HasValue ? (menuInfo_0.BEGINGROUP.Value ? "1" : "0") : "NULL",
                            string.IsNullOrEmpty(menuInfo_0.CAPTION) ? "NULL" : ("'" + menuInfo_0.CAPTION + "'")
                        });
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                dataAccessLayer.ExecuteNonQuery(CommandType.Text, string_);
                dataAccessLayer.Close();
                result = 0;
            }
            return result;
        }

        public void Update(MenuInfo menuInfo_0)
        {
            string string_ = string.Format("update {0} set PARENTIDS='{1}' where MenuID={2}", this.string_0,
                string.IsNullOrEmpty(menuInfo_0.PARENTIDS) ? "NULL" : ("'" + menuInfo_0.PARENTIDS + "'"),
                menuInfo_0.MenuID);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, string_);
            dataAccessLayer.Close();
        }

        public bool IsExist(MenuInfo menuInfo_0)
        {
            string string_;
            if (string.IsNullOrEmpty(menuInfo_0.PROGID))
            {
                string_ = string.Format("select * from {0} where  COMPONENTDLLNAME='{1}' and CLASSNAME='{2}'",
                    this.string_0, menuInfo_0.COMPONENTDLLNAME, menuInfo_0.CLASSNAME);
            }
            else
            {
                string_ = string.Format("select * from {0} where  PROGID='{1}' ", this.string_0, menuInfo_0.PROGID);
            }
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string_);
            dataAccessLayer.Close();
            return dataTable.Rows.Count > 0;
        }

        public MenuInfo Load(string string_2)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            DataTable dataTable =
                dataAccessLayer.ExecuteDataTable("select * from " + this.string_0 + " where MenuID=" + string_2);
            MenuInfo menuInfo = null;
            if (dataTable.Rows.Count > 0)
            {
                DataRow dataRow = dataTable.Rows[0];
                menuInfo = new MenuInfo();
                menuInfo.MenuID = System.Convert.ToString(dataRow["MenuID"]);
                object arg_82_0 = dataRow["NAME"];
                menuInfo.NAME = System.Convert.ToString(dataRow["NAME"]);
                menuInfo.ORDERBY =
                    new int?((!(dataRow["ORDERBY"] is System.DBNull)) ? System.Convert.ToInt32(dataRow["ORDERBY"]) : -1);
                menuInfo.PROGID = System.Convert.ToString(dataRow["PROGID"]);
                menuInfo.SHORTCUT = System.Convert.ToString(dataRow["SHORTCUT"]);
                menuInfo.SUBTYPE =
                    new int?((!(dataRow["SUBTYPE"] is System.DBNull)) ? System.Convert.ToInt32(dataRow["SUBTYPE"]) : -1);
                menuInfo.VISIBLE =
                    new bool?(dataRow["VISIBLE"] is System.DBNull || System.Convert.ToInt32(dataRow["VISIBLE"]) == 1);
                menuInfo.ItemCol =
                    new int?((!(dataRow["ItemCol"] is System.DBNull)) ? System.Convert.ToInt32(dataRow["ItemCol"]) : -1);
                menuInfo.ISPOPMENUITEM =
                    new bool?(!(dataRow["ISPOPMENUITEM"] is System.DBNull) &&
                              System.Convert.ToInt32(dataRow["ISPOPMENUITEM"]) == 1);
                menuInfo.COMPONENTDLLNAME = System.Convert.ToString(dataRow["COMPONENTDLLNAME"]);
                menuInfo.CLASSNAME = System.Convert.ToString(dataRow["CLASSNAME"]);
                menuInfo.BEGINGROUP =
                    new bool?(!(dataRow["BEGINGROUP"] is System.DBNull) &&
                              System.Convert.ToInt32(dataRow["BEGINGROUP"]) == 1);
                menuInfo.CAPTION = System.Convert.ToString(dataRow["CAPTION"]);
                menuInfo.PARENTIDS = System.Convert.ToString(dataRow["PARENTIDS"]);
            }
            dataAccessLayer.Close();
            return menuInfo;
        }

        public MenuInfo GetByName(string string_2)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            DataTable dataTable =
                dataAccessLayer.ExecuteDataTable("select * from " + this.string_0 + " where NAME=" + string_2);
            MenuInfo menuInfo = null;
            if (dataTable.Rows.Count > 0)
            {
                DataRow dataRow = dataTable.Rows[0];
                menuInfo = new MenuInfo();
                menuInfo.MenuID = System.Convert.ToString(dataRow["MenuID"]);
                object arg_82_0 = dataRow["NAME"];
                menuInfo.NAME = System.Convert.ToString(dataRow["NAME"]);
                menuInfo.ORDERBY =
                    new int?((!(dataRow["ORDERBY"] is System.DBNull)) ? System.Convert.ToInt32(dataRow["ORDERBY"]) : -1);
                menuInfo.PROGID = System.Convert.ToString(dataRow["PROGID"]);
                menuInfo.SHORTCUT = System.Convert.ToString(dataRow["SHORTCUT"]);
                menuInfo.SUBTYPE =
                    new int?((!(dataRow["SUBTYPE"] is System.DBNull)) ? System.Convert.ToInt32(dataRow["SUBTYPE"]) : -1);
                menuInfo.VISIBLE =
                    new bool?(dataRow["VISIBLE"] is System.DBNull || System.Convert.ToInt32(dataRow["VISIBLE"]) == 1);
                menuInfo.ItemCol =
                    new int?((!(dataRow["ItemCol"] is System.DBNull)) ? System.Convert.ToInt32(dataRow["ItemCol"]) : -1);
                menuInfo.ISPOPMENUITEM =
                    new bool?(!(dataRow["ISPOPMENUITEM"] is System.DBNull) &&
                              System.Convert.ToInt32(dataRow["ISPOPMENUITEM"]) == 1);
                menuInfo.COMPONENTDLLNAME = System.Convert.ToString(dataRow["COMPONENTDLLNAME"]);
                menuInfo.CLASSNAME = System.Convert.ToString(dataRow["CLASSNAME"]);
                menuInfo.BEGINGROUP =
                    new bool?(!(dataRow["BEGINGROUP"] is System.DBNull) &&
                              System.Convert.ToInt32(dataRow["BEGINGROUP"]) == 1);
                menuInfo.CAPTION = System.Convert.ToString(dataRow["CAPTION"]);
                menuInfo.PARENTIDS = System.Convert.ToString(dataRow["PARENTIDS"]);
            }
            dataAccessLayer.Close();
            return menuInfo;
        }
    }
}