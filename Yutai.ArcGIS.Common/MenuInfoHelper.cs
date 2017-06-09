using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Common
{
    public class MenuInfoHelper
    {
        private DataProviderType dataProviderType_0;
        private string string_0;
        private string string_1;

        public MenuInfoHelper()
        {
            this.string_0 = "";
            this.dataProviderType_0 = DataProviderType.Sql;
            this.string_1 = "";
            try
            {
                string[] strArray2 = ConfigurationManager.AppSettings["SYSPRIVDB"].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if ((strArray2[0].ToLower() == "sqlserver") || (strArray2[0].ToLower() == "sql"))
                {
                    this.dataProviderType_0 = DataProviderType.Sql;
                }
                else if (strArray2[0].ToLower() == "oracle")
                {
                    this.dataProviderType_0 = DataProviderType.Oracle;
                }
                else if (strArray2[0].ToLower() == "oledb")
                {
                    this.dataProviderType_0 = DataProviderType.OleDb;
                }
                else if (strArray2[0].ToLower() == "odbc")
                {
                    this.dataProviderType_0 = DataProviderType.Odbc;
                }
                else if (strArray2[0].ToLower() == "access")
                {
                    this.dataProviderType_0 = DataProviderType.Access;
                }
                this.string_1 = strArray2[1];
            }
            catch
            {
            }
            this.string_0 = "MAINMENUCONFIG";
        }

        public MenuInfoHelper(string string_2)
        {
            this.string_0 = "";
            this.dataProviderType_0 = DataProviderType.Sql;
            this.string_1 = "";
            try
            {
                string[] strArray2 = ConfigurationManager.AppSettings["SYSPRIVDB"].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if ((strArray2[0].ToLower() == "sqlserver") || (strArray2[0].ToLower() == "sql"))
                {
                    this.dataProviderType_0 = DataProviderType.Sql;
                }
                else if (strArray2[0].ToLower() == "oracle")
                {
                    this.dataProviderType_0 = DataProviderType.Oracle;
                }
                else if (strArray2[0].ToLower() == "oledb")
                {
                    this.dataProviderType_0 = DataProviderType.OleDb;
                }
                else if (strArray2[0].ToLower() == "odbc")
                {
                    this.dataProviderType_0 = DataProviderType.Odbc;
                }
                else if (strArray2[0].ToLower() == "access")
                {
                    this.dataProviderType_0 = DataProviderType.Access;
                }
                this.string_1 = strArray2[1];
            }
            catch
            {
            }
            this.string_0 = string_2;
        }

        public int Add(MenuInfo menuInfo_0)
        {
            if (!this.IsExist(menuInfo_0))
            {
                string str = string.Format("insert into {0}([NAME],PARENTIDS,ORDERBY,[PROGID],[SHORTCUT],SUBTYPE,VISIBLE,ItemCol,ISPOPMENUITEM,COMPONENTDLLNAME,CLASSNAME,BEGINGROUP,CAPTION) values({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", new object[] { this.string_0, string.IsNullOrEmpty(menuInfo_0.NAME) ? "NULL" : ("'" + menuInfo_0.NAME + "'"), string.IsNullOrEmpty(menuInfo_0.PARENTIDS) ? "NULL" : ("'" + menuInfo_0.PARENTIDS + "'"), menuInfo_0.ORDERBY.HasValue ? menuInfo_0.ORDERBY.Value.ToString() : "NULL", string.IsNullOrEmpty(menuInfo_0.PROGID) ? "NULL" : ("'" + menuInfo_0.PROGID + "'"), string.IsNullOrEmpty(menuInfo_0.SHORTCUT) ? "NULL" : ("'" + menuInfo_0.SHORTCUT + "'"), menuInfo_0.SUBTYPE.HasValue ? menuInfo_0.SUBTYPE.Value.ToString() : "NULL", menuInfo_0.VISIBLE.HasValue ? (menuInfo_0.VISIBLE.Value ? "1" : "0") : "NULL", menuInfo_0.ItemCol.HasValue ? menuInfo_0.ItemCol.Value.ToString() : "NULL", menuInfo_0.ISPOPMENUITEM.HasValue ? (menuInfo_0.ISPOPMENUITEM.Value ? "1" : "0") : "NULL", string.IsNullOrEmpty(menuInfo_0.COMPONENTDLLNAME) ? "NULL" : ("'" + menuInfo_0.COMPONENTDLLNAME + "'"), string.IsNullOrEmpty(menuInfo_0.CLASSNAME) ? "NULL" : ("'" + menuInfo_0.CLASSNAME + "'"), menuInfo_0.BEGINGROUP.HasValue ? (menuInfo_0.BEGINGROUP.Value ? "1" : "0") : "NULL", string.IsNullOrEmpty(menuInfo_0.CAPTION) ? "NULL" : ("'" + menuInfo_0.CAPTION + "'") });
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
                dataAccessLayer.Close();
            }
            return 0;
        }

        public void ClearAll()
        {
            try
            {
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                dataAccessLayer.ExecuteNonQuery("delete from " + this.string_0, new object[0]);
                dataAccessLayer.Close();
            }
            catch
            {
            }
        }

        public MenuInfo GetByClassName(string string_2)
        {
            MenuInfo info = null;
            try
            {
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                DataTable table = dataAccessLayer.ExecuteDataTable("select * from " + this.string_0 + " where PROGID='" + string_2 + "'");
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    info = new MenuInfo {
                        MenuID = Convert.ToString(row["MenuID"]),
                        NAME = Convert.ToString(row["NAME"])
                    };
                    object obj1 = row["ORDERBY"];
                    info.ORDERBY = new int?(!(row["ORDERBY"] is DBNull) ? Convert.ToInt32(row["ORDERBY"]) : -1);
                    info.PROGID = Convert.ToString(row["PROGID"]);
                    info.SHORTCUT = Convert.ToString(row["SHORTCUT"]);
                    info.SUBTYPE = new int?(!(row["SUBTYPE"] is DBNull) ? Convert.ToInt32(row["SUBTYPE"]) : -1);
                    info.VISIBLE = new bool?(!(row["VISIBLE"] is DBNull) ? (Convert.ToInt32(row["VISIBLE"]) == 1) : true);
                    info.ItemCol = new int?(!(row["ItemCol"] is DBNull) ? Convert.ToInt32(row["ItemCol"]) : -1);
                    info.ISPOPMENUITEM = new bool?(!(row["ISPOPMENUITEM"] is DBNull) ? (Convert.ToInt32(row["ISPOPMENUITEM"]) == 1) : false);
                    info.COMPONENTDLLNAME = Convert.ToString(row["COMPONENTDLLNAME"]);
                    info.CLASSNAME = Convert.ToString(row["CLASSNAME"]);
                    info.BEGINGROUP = new bool?(!(row["BEGINGROUP"] is DBNull) ? (Convert.ToInt32(row["BEGINGROUP"]) == 1) : false);
                    info.CAPTION = Convert.ToString(row["CAPTION"]);
                    info.PARENTIDS = Convert.ToString(row["PARENTIDS"]);
                }
                dataAccessLayer.Close();
            }
            catch
            {
            }
            return info;
        }

        public MenuInfo GetByClassName(string string_2, string string_3)
        {
            MenuInfo info = null;
            try
            {
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                DataTable table = dataAccessLayer.ExecuteDataTable("select * from " + this.string_0 + " where COMPONENTDLLNAME='" + string_2 + "' and CLASSNAME='" + string_3 + "'");
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    info = new MenuInfo {
                        MenuID = Convert.ToString(row["MenuID"]),
                        NAME = Convert.ToString(row["NAME"])
                    };
                    object obj1 = row["ORDERBY"];
                    info.ORDERBY = new int?(!(row["ORDERBY"] is DBNull) ? Convert.ToInt32(row["ORDERBY"]) : -1);
                    info.PROGID = Convert.ToString(row["PROGID"]);
                    info.SHORTCUT = Convert.ToString(row["SHORTCUT"]);
                    info.SUBTYPE = new int?(!(row["SUBTYPE"] is DBNull) ? Convert.ToInt32(row["SUBTYPE"]) : -1);
                    info.VISIBLE = new bool?(!(row["VISIBLE"] is DBNull) ? (Convert.ToInt32(row["VISIBLE"]) == 1) : true);
                    info.ItemCol = new int?(!(row["ItemCol"] is DBNull) ? Convert.ToInt32(row["ItemCol"]) : -1);
                    info.ISPOPMENUITEM = new bool?(!(row["ISPOPMENUITEM"] is DBNull) ? (Convert.ToInt32(row["ISPOPMENUITEM"]) == 1) : false);
                    info.COMPONENTDLLNAME = Convert.ToString(row["COMPONENTDLLNAME"]);
                    info.CLASSNAME = Convert.ToString(row["CLASSNAME"]);
                    info.BEGINGROUP = new bool?(!(row["BEGINGROUP"] is DBNull) ? (Convert.ToInt32(row["BEGINGROUP"]) == 1) : false);
                    info.CAPTION = Convert.ToString(row["CAPTION"]);
                    info.PARENTIDS = Convert.ToString(row["PARENTIDS"]);
                }
                dataAccessLayer.Close();
            }
            catch
            {
            }
            return info;
        }

        public MenuInfo GetByName(string string_2)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            DataTable table = dataAccessLayer.ExecuteDataTable("select * from " + this.string_0 + " where NAME=" + string_2);
            MenuInfo info = null;
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                info = new MenuInfo {
                    MenuID = Convert.ToString(row["MenuID"])
                };
                object obj1 = row["NAME"];
                info.NAME = Convert.ToString(row["NAME"]);
                info.ORDERBY = new int?(!(row["ORDERBY"] is DBNull) ? Convert.ToInt32(row["ORDERBY"]) : -1);
                info.PROGID = Convert.ToString(row["PROGID"]);
                info.SHORTCUT = Convert.ToString(row["SHORTCUT"]);
                info.SUBTYPE = new int?(!(row["SUBTYPE"] is DBNull) ? Convert.ToInt32(row["SUBTYPE"]) : -1);
                info.VISIBLE = new bool?(!(row["VISIBLE"] is DBNull) ? (Convert.ToInt32(row["VISIBLE"]) == 1) : true);
                info.ItemCol = new int?(!(row["ItemCol"] is DBNull) ? Convert.ToInt32(row["ItemCol"]) : -1);
                info.ISPOPMENUITEM = new bool?(!(row["ISPOPMENUITEM"] is DBNull) ? (Convert.ToInt32(row["ISPOPMENUITEM"]) == 1) : false);
                info.COMPONENTDLLNAME = Convert.ToString(row["COMPONENTDLLNAME"]);
                info.CLASSNAME = Convert.ToString(row["CLASSNAME"]);
                info.BEGINGROUP = new bool?(!(row["BEGINGROUP"] is DBNull) ? (Convert.ToInt32(row["BEGINGROUP"]) == 1) : false);
                info.CAPTION = Convert.ToString(row["CAPTION"]);
                info.PARENTIDS = Convert.ToString(row["PARENTIDS"]);
            }
            dataAccessLayer.Close();
            return info;
        }

        public bool IsExist(MenuInfo menuInfo_0)
        {
            string str = "";
            if (string.IsNullOrEmpty(menuInfo_0.PROGID))
            {
                str = string.Format("select * from {0} where  COMPONENTDLLNAME='{1}' and CLASSNAME='{2}'", this.string_0, menuInfo_0.COMPONENTDLLNAME, menuInfo_0.CLASSNAME);
            }
            else
            {
                str = string.Format("select * from {0} where  PROGID='{1}' ", this.string_0, menuInfo_0.PROGID);
            }
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            DataTable table = dataAccessLayer.ExecuteDataTable(str);
            dataAccessLayer.Close();
            return (table.Rows.Count > 0);
        }

        public List<MenuInfo> Load()
        {
            List<MenuInfo> list = new List<MenuInfo>();
            try
            {
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
                dataAccessLayer.Open();
                DataTable table = dataAccessLayer.ExecuteDataTable("select * from " + this.string_0);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    MenuInfo item = new MenuInfo {
                        MenuID = Convert.ToString(row["MenuID"]),
                        NAME = Convert.ToString(row["NAME"])
                    };
                    object obj1 = row["ORDERBY"];
                    item.ORDERBY = new int?(!(row["ORDERBY"] is DBNull) ? Convert.ToInt32(row["ORDERBY"]) : -1);
                    item.PROGID = Convert.ToString(row["PROGID"]);
                    item.SHORTCUT = Convert.ToString(row["SHORTCUT"]);
                    item.SUBTYPE = new int?(!(row["SUBTYPE"] is DBNull) ? Convert.ToInt32(row["SUBTYPE"]) : -1);
                    item.VISIBLE = new bool?(!(row["VISIBLE"] is DBNull) ? (Convert.ToInt32(row["VISIBLE"]) == 1) : true);
                    item.ItemCol = new int?(!(row["ItemCol"] is DBNull) ? Convert.ToInt32(row["ItemCol"]) : -1);
                    item.ISPOPMENUITEM = new bool?(!(row["ISPOPMENUITEM"] is DBNull) ? (Convert.ToInt32(row["ISPOPMENUITEM"]) == 1) : false);
                    item.COMPONENTDLLNAME = Convert.ToString(row["COMPONENTDLLNAME"]);
                    item.CLASSNAME = Convert.ToString(row["CLASSNAME"]);
                    item.PARENTIDS = Convert.ToString(row["PARENTIDS"]);
                    item.BEGINGROUP = new bool?(!(row["BEGINGROUP"] is DBNull) ? (Convert.ToInt32(row["BEGINGROUP"]) == 1) : false);
                    item.CAPTION = Convert.ToString(row["CAPTION"]);
                    list.Add(item);
                }
                dataAccessLayer.Close();
            }
            catch
            {
            }
            return list;
        }

        public MenuInfo Load(string string_2)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            DataTable table = dataAccessLayer.ExecuteDataTable("select * from " + this.string_0 + " where MenuID=" + string_2);
            MenuInfo info = null;
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                info = new MenuInfo {
                    MenuID = Convert.ToString(row["MenuID"])
                };
                object obj1 = row["NAME"];
                info.NAME = Convert.ToString(row["NAME"]);
                info.ORDERBY = new int?(!(row["ORDERBY"] is DBNull) ? Convert.ToInt32(row["ORDERBY"]) : -1);
                info.PROGID = Convert.ToString(row["PROGID"]);
                info.SHORTCUT = Convert.ToString(row["SHORTCUT"]);
                info.SUBTYPE = new int?(!(row["SUBTYPE"] is DBNull) ? Convert.ToInt32(row["SUBTYPE"]) : -1);
                info.VISIBLE = new bool?(!(row["VISIBLE"] is DBNull) ? (Convert.ToInt32(row["VISIBLE"]) == 1) : true);
                info.ItemCol = new int?(!(row["ItemCol"] is DBNull) ? Convert.ToInt32(row["ItemCol"]) : -1);
                info.ISPOPMENUITEM = new bool?(!(row["ISPOPMENUITEM"] is DBNull) ? (Convert.ToInt32(row["ISPOPMENUITEM"]) == 1) : false);
                info.COMPONENTDLLNAME = Convert.ToString(row["COMPONENTDLLNAME"]);
                info.CLASSNAME = Convert.ToString(row["CLASSNAME"]);
                info.BEGINGROUP = new bool?(!(row["BEGINGROUP"] is DBNull) ? (Convert.ToInt32(row["BEGINGROUP"]) == 1) : false);
                info.CAPTION = Convert.ToString(row["CAPTION"]);
                info.PARENTIDS = Convert.ToString(row["PARENTIDS"]);
            }
            dataAccessLayer.Close();
            return info;
        }

        public void Update(MenuInfo menuInfo_0)
        {
            string str = string.Format("update {0} set PARENTIDS='{1}' where MenuID={2}", this.string_0, string.IsNullOrEmpty(menuInfo_0.PARENTIDS) ? "NULL" : ("'" + menuInfo_0.PARENTIDS + "'"), menuInfo_0.MenuID);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }
    }
}

