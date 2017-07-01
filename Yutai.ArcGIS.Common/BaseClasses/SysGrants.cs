using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Data;
using Yutai.ArcGIS.Common.Priviliges;
using Yutai.Shared;


namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class SysGrants
    {
        private DataProviderType dataProviderType_0 = DataProviderType.Sql;

        private string string_0;

        private string string_1 = "";

        private string string_2 = "";

        private ITable itable_0 = null;

        private bool bool_0 = true;

        public SysGrants()
        {
            this.dataProviderType_0 = DataProviderType.Sql;
            string item = ConfigurationManager.AppSettings["SYSPRIVDB"];
            if (!string.IsNullOrEmpty(item))
            {
                string[] strArrays = new string[] {"||"};
                string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
                if (!(strArrays1[0].ToLower() == "sqlserver" ? false : !(strArrays1[0].ToLower() == "sql")))
                {
                    this.dataProviderType_0 = DataProviderType.Sql;
                }
                else if (strArrays1[0].ToLower() == "oracle")
                {
                    this.dataProviderType_0 = DataProviderType.Oracle;
                }
                else if (strArrays1[0].ToLower() == "oledb")
                {
                    this.dataProviderType_0 = DataProviderType.OleDb;
                }
                else if (strArrays1[0].ToLower() == "odbc")
                {
                    this.dataProviderType_0 = DataProviderType.Odbc;
                }
                else if (strArrays1[0].ToLower() == "access")
                {
                    this.dataProviderType_0 = DataProviderType.Access;
                }
                this.string_0 = strArrays1[1];
            }
            else
            {
                this.dataProviderType_0 = DataProviderType.None;
            }
        }

        public SysGrants(string string_3)
        {
            if (string.IsNullOrEmpty(string_3))
            {
                string_3 = "admin";
            }
            this.string_1 = string_3;
            if (this.string_1 != "admin")
            {
                this.dataProviderType_0 = DataProviderType.Sql;
                string item = ConfigurationManager.AppSettings["SYSPRIVDB"];
                string[] strArrays = new string[] {"||"};
                string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
                if (!(strArrays1[0].ToLower() == "sqlserver" ? false : !(strArrays1[0].ToLower() == "sql")))
                {
                    this.dataProviderType_0 = DataProviderType.Sql;
                }
                else if (strArrays1[0].ToLower() == "oracle")
                {
                    this.dataProviderType_0 = DataProviderType.Oracle;
                }
                else if (strArrays1[0].ToLower() == "oledb")
                {
                    this.dataProviderType_0 = DataProviderType.OleDb;
                }
                else if (strArrays1[0].ToLower() == "odbc")
                {
                    this.dataProviderType_0 = DataProviderType.Odbc;
                }
                else if (strArrays1[0].ToLower() == "access")
                {
                    this.dataProviderType_0 = DataProviderType.Access;
                }
                this.string_0 = strArrays1[1];
                this.string_1 = string_3;
                if (this.string_1 != "admin")
                {
                    try
                    {
                        string[] roles = this.GetRoles(string_3);
                        if ((roles == null ? false : (int) roles.Length > 0))
                        {
                            StringBuilder stringBuilder = new StringBuilder(roles[0]);
                            for (int i = 1; i < (int) roles.Length; i++)
                            {
                                stringBuilder.Append(',');
                                stringBuilder.Append(roles[i]);
                            }
                            this.string_2 = stringBuilder.ToString();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void AddGrant(string string_3, string string_4, string string_5, string string_6, string string_7,
            string string_8)
        {
            object[] string3 = new object[]
                {"SYSGRANTS", string_3, string_4, string_5, string_6, string_7, string_8, null};
            string3[7] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string str =
                string.Format(
                    "insert into {0}(GRANTEE,GRANTEETYPE,GRANTOR,GRANTOBJECT,OBJECTTYPE,PRIVILEGEFLAG,CREATETIME) values('{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    string3);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_0);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }

        public void CloseTable()
        {
            this.itable_0 = null;
        }

        public static void DeleteByRoleID(DataAccessLayerBaseClass dataAccessLayerBaseClass_0, string string_3)
        {
            dataAccessLayerBaseClass_0.ExecuteNonQuery(CommandType.Text,
                string.Format("delete from SYSGRANTS where GRANTEE='{0}' and GRANTEETYPE='Roles'", string_3));
        }

        public static void DeleteByStaffID(DataAccessLayerBaseClass dataAccessLayerBaseClass_0, string string_3)
        {
            dataAccessLayerBaseClass_0.ExecuteNonQuery(CommandType.Text,
                string.Format("delete from SYSGRANTS where GRANTEE='{0}' and GRANTEETYPE='Staff'", string_3));
        }

        public void DeleteGrant(string string_3, string string_4, string string_5, string string_6)
        {
            object[] string3 = new object[] {"SYSGRANTS", string_3, string_4, string_5, string_6};
            string str =
                string.Format(
                    "delete from  {0} where GRANTEE='{1}' and  GRANTEETYPE='{2}' and  GRANTOBJECT='{3}' and OBJECTTYPE='{4}'",
                    string3);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_0);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }

        public void DeleteGrant(string string_3)
        {
            string str = string.Format("delete from  {0} where  OBJECTTYPE='{1}'", "SYSGRANTS", string_3);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_0);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }

        public string[] GetRoles(string string_3)
        {
            string[] strArrays = null;
            try
            {
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from ORGSTAFFROLES where StaffID='",
                        string_3, "'"));
                strArrays = new string[dataTable.Rows.Count];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow item = dataTable.Rows[i];
                    string str = item["ROLEID"].ToString();
                    strArrays[i] = string.Concat("'", str, "'");
                }
                dataAccessLayer.Close();
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            return strArrays;
        }

        public DataTable GetRolesLayerPri(string string_3, int int_0)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                str = string.Concat("Grantee ='", string_3, "' and GRANTEETYPE='Roles' and  ");
                if (int_0 == 2)
                {
                    str = string.Concat(str, "(ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag='2' ");
                }
                else if (int_0 != 1)
                {
                    dataTable = null;
                    return dataTable;
                }
                else
                {
                    str = string.Concat(str, "(ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag>='1'");
                }
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public DataTable GetRolesMapsPri(string string_3, int int_0)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                str = string.Concat("Grantee ='", string_3, "' and GRANTEETYPE='Roles' and  ");
                if (int_0 == 2)
                {
                    str = string.Concat(str, "ObjectType='gisMap' and privilegeFlag='2' ");
                }
                else if (int_0 != 1)
                {
                    dataTable = null;
                    return dataTable;
                }
                else
                {
                    str = string.Concat(str, "ObjectType='gisMap' and privilegeFlag>='1'");
                }
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public DataTable GetRolesMenuPri(string string_3)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                str = string.Concat("Grantee= '", string_3, "'  and ObjectType='gisPluge' and  GRANTEETYPE='Roles'");
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public DataTable GetRolesObjectPri(string string_3, string string_4)
        {
            string str = string.Format("ObjectType='{0}' and Grantee='{1}' and GRANTEETYPE='Roles'", string_4, string_3);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_0);
            dataAccessLayer.Open();
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
            dataAccessLayer.Close();
            return dataTable;
        }

        public string[] GetRolesOld(string string_3)
        {
            ITable table;
            string[] strArrays;
            DataSet dataSet = null;
            string[] strArrays1 = null;
            Table2DataTable table2DataTable = new Table2DataTable();
            int count = 0;
            try
            {
                table = AppConfigInfo.OpenTable("JLK_STAFFROLES");
                if (table != null)
                {
                    dataSet = new DataSet();
                    dataSet = table2DataTable.GetDataSet(table, string.Concat("STAFFID = '", string_3, "'"));
                    count = dataSet.Tables[0].Rows.Count;
                    strArrays1 = new string[count];
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            string str = dataSet.Tables[0].Rows[i]["ROLEID"].ToString();
                            strArrays1[i] = string.Concat("'", str, "'");
                        }
                    }
                }
                else
                {
                    strArrays = null;
                    return strArrays;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            table = null;
            strArrays = strArrays1;
            return strArrays;
        }

        public DataTable GetRolesPlugePri(string string_3, int int_0)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                object[] string3 = new object[]
                {
                    "Grantee ='", string_3, "' and GRANTEETYPE='Staff' and  ObjectType='gisPluge' and privilegeFlag='",
                    int_0, "'"
                };
                str = string.Concat(string3);
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public bool GetStaffAndRolesDatasetPri(string string_3, int int_0, string string_4)
        {
            bool flag;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                string str = "";
                try
                {
                    string[] strArrays = string_4.Split(new char[] {'.'});
                    string_4 = strArrays[(int) strArrays.Length - 1];
                    IQueryFilter queryFilterClass = new QueryFilter()
                    {
                        WhereClause = string.Concat("RealName = '", string_4, "'")
                    };
                    ITable table = AppConfigInfo.OpenTable("DATASETTABLE");
                    ICursor cursor = null;
                    cursor = table.Search(queryFilterClass, false);
                    IRow row = cursor.NextRow();
                    ComReleaser.ReleaseCOMObject(cursor);
                    if (row != null)
                    {
                        int oID = row.OID;
                        if (this.string_2.Length <= 0)
                        {
                            str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                        }
                        else
                        {
                            string[] string2 = new string[]
                            {
                                " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                                "'  and GRANTEETYPE='Staff' ) ) and  "
                            };
                            str = string.Concat(string2);
                        }
                        if (int_0 == 2)
                        {
                            str = string.Concat(str,
                                "  ObjectType='gisDataset' and privilegeFlag='2' and GRANTOBJECT = '", oID.ToString(),
                                "'");
                        }
                        else if (int_0 != 1)
                        {
                            flag = false;
                            return flag;
                        }
                        else
                        {
                            str = string.Concat(str,
                                "  ObjectType='gisDataset' and privilegeFlag>='1' and GRANTOBJECT = '", oID.ToString(),
                                "'");
                        }
                        DataAccessLayerBaseClass dataAccessLayer =
                            DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                        dataAccessLayer.Open();
                        DataTable dataTable =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        bool count = dataTable.Rows.Count > 0;
                        dataTable = null;
                        flag = count;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                    flag = false;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public bool GetStaffAndRolesDatasetPri(string string_3, string string_4, out int int_0)
        {
            bool flag;
            int_0 = 0;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                string str = "";
                try
                {
                    string[] strArrays = string_4.Split(new char[] {'.'});
                    string_4 = strArrays[(int) strArrays.Length - 1];
                    IQueryFilter queryFilterClass = new QueryFilter()
                    {
                        WhereClause = string.Concat("RealName = '", string_4, "'")
                    };
                    ITable table = AppConfigInfo.OpenTable("DATASETTABLE");
                    ICursor cursor = null;
                    cursor = table.Search(queryFilterClass, false);
                    IRow row = cursor.NextRow();
                    ComReleaser.ReleaseCOMObject(cursor);
                    if (row != null)
                    {
                        int oID = row.OID;
                        if (this.string_2.Length <= 0)
                        {
                            str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                        }
                        else
                        {
                            string[] string2 = new string[]
                            {
                                " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                                "' and GRANTEETYPE='Staff') )  and  "
                            };
                            str = string.Concat(string2);
                        }
                        str = string.Concat(str, " ObjectType='gisDataset' and GRANTOBJECT = '", oID.ToString(), "'");
                        DataAccessLayerBaseClass dataAccessLayer =
                            DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                        dataAccessLayer.Open();
                        DataTable dataTable =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        bool flag1 = false;
                        if (dataTable.Rows.Count > 0)
                        {
                            object item = dataTable.Rows[0]["privilegeFlag"];
                            if (!(item is DBNull))
                            {
                                int_0 = Convert.ToInt32(item);
                            }
                            flag1 = true;
                        }
                        dataTable = null;
                        flag = flag1;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch (Exception exception)
                {
                    flag = false;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public DataTable GetStaffAndRolesLayerPri(string string_3, int int_0)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                if (this.string_2.Length <= 0)
                {
                    str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                }
                else
                {
                    string[] string2 = new string[]
                    {
                        " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                        "'  and GRANTEETYPE='Staff' ) ) and  "
                    };
                    str = string.Concat(string2);
                }
                if (int_0 == 2)
                {
                    str = string.Concat(str,
                        " (ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag='2' ");
                }
                else if (int_0 != 1)
                {
                    dataTable = null;
                    return dataTable;
                }
                else
                {
                    str = string.Concat(str, " (ObjectType='gisLayer' or ObjectType='gisDataset') ");
                }
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public bool GetStaffAndRolesLayerPri(string string_3, int int_0, string string_4)
        {
            bool count;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                if (string_3.Length == 0)
                {
                    string_3 = this.string_1;
                }
                if ((string_3 == "admin" ? false : string_3.Length != 0))
                {
                    string str = "";
                    try
                    {
                        string[] strArrays = string_4.Split(new char[] {'.'});
                        string_4 = strArrays[(int) strArrays.Length - 1];
                        IQueryFilter queryFilterClass = new QueryFilter()
                        {
                            WhereClause = string.Concat("FeatureClassName = '", string_4, "'")
                        };
                        ITable table = AppConfigInfo.OpenTable("LayerConfig");
                        ICursor cursor = null;
                        cursor = table.Search(queryFilterClass, false);
                        IRow row = cursor.NextRow();
                        ComReleaser.ReleaseCOMObject(cursor);
                        if (row != null)
                        {
                            int oID = row.OID;
                            if (this.string_2.Length <= 0)
                            {
                                str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                            }
                            else
                            {
                                string[] string2 = new string[]
                                {
                                    " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='",
                                    string_3, "' and GRANTEETYPE='Staff') )  and  "
                                };
                                str = string.Concat(string2);
                            }
                            if (int_0 == 2)
                            {
                                str = string.Concat(str,
                                    " (ObjectType='gisLayer' or ObjectType='gisDataset')  and privilegeFlag='2' and GRANTOBJECT = '",
                                    oID.ToString(), "'");
                            }
                            else if (int_0 != 1)
                            {
                                count = false;
                                return count;
                            }
                            else
                            {
                                str = string.Concat(str,
                                    " (ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag>='1' and GRANTOBJECT = '",
                                    oID.ToString(), "'");
                            }
                            DataAccessLayerBaseClass dataAccessLayer =
                                DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                            dataAccessLayer.Open();
                            DataTable dataTable =
                                dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                            dataAccessLayer.Close();
                            count = dataTable.Rows.Count > 0;
                        }
                        else
                        {
                            count = this.GetStaffAndRolesDatasetPri(string_3, int_0, string_4);
                        }
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        count = false;
                    }
                }
                else
                {
                    count = true;
                }
            }
            else
            {
                count = true;
            }
            return count;
        }

        public bool GetStaffAndRolesLayerPri(string string_3, int int_0, int int_1)
        {
            bool count;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                if (string_3.Length == 0)
                {
                    string_3 = this.string_1;
                }
                if ((string_3 == "admin" ? false : string_3.Length != 0))
                {
                    string str = "";
                    try
                    {
                        int int1 = int_1;
                        if (this.string_2.Length <= 0)
                        {
                            str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                        }
                        else
                        {
                            string[] string2 = new string[]
                            {
                                " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                                "' and GRANTEETYPE='Staff') )  and  "
                            };
                            str = string.Concat(string2);
                        }
                        if (int_0 == 2)
                        {
                            str = string.Concat(str,
                                " (ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag='2' and GRANTOBJECT = '",
                                int1.ToString(), "'");
                        }
                        else if (int_0 != 1)
                        {
                            count = false;
                            return count;
                        }
                        else
                        {
                            str = string.Concat(str,
                                " (ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag>='1' and GRANTOBJECT = '",
                                int1.ToString(), "'");
                        }
                        DataAccessLayerBaseClass dataAccessLayer =
                            DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                        dataAccessLayer.Open();
                        DataTable dataTable =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        count = dataTable.Rows.Count > 0;
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        count = false;
                    }
                }
                else
                {
                    count = true;
                }
            }
            else
            {
                count = true;
            }
            return count;
        }

        public bool GetStaffAndRolesMapPri(string string_3, int int_0, string string_4)
        {
            bool count;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                if (string_3.Length == 0)
                {
                    string_3 = this.string_1;
                }
                if ((string_3 == "admin" ? false : string_3.Length != 0))
                {
                    string str = "";
                    try
                    {
                        string[] strArrays = string_4.Split(new char[] {'.'});
                        string_4 = strArrays[(int) strArrays.Length - 1];
                        IQueryFilter queryFilterClass = new QueryFilter()
                        {
                            WhereClause = string.Concat("Name = '", string_4, "'")
                        };
                        ITable table = AppConfigInfo.OpenTable("MapDefine");
                        ICursor cursor = null;
                        cursor = table.Search(queryFilterClass, false);
                        IRow row = cursor.NextRow();
                        ComReleaser.ReleaseCOMObject(cursor);
                        if (row != null)
                        {
                            int oID = row.OID;
                            if (this.string_2.Length <= 0)
                            {
                                str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                            }
                            else
                            {
                                string[] string2 = new string[]
                                {
                                    " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='",
                                    string_3, "' and GRANTEETYPE='Staff') )  and  "
                                };
                                str = string.Concat(string2);
                            }
                            if (int_0 == 2)
                            {
                                str = string.Concat(str,
                                    " ObjectType='gisMap' and privilegeFlag='2' and GRANTOBJECT = '", oID.ToString(),
                                    "'");
                            }
                            else if (int_0 != 1)
                            {
                                count = false;
                                return count;
                            }
                            else
                            {
                                str = string.Concat(str,
                                    " ObjectType='gisMap' and privilegeFlag>='1' and GRANTOBJECT = '", oID.ToString(),
                                    "'");
                            }
                            DataAccessLayerBaseClass dataAccessLayer =
                                DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                            dataAccessLayer.Open();
                            DataTable dataTable =
                                dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                            dataAccessLayer.Close();
                            count = dataTable.Rows.Count > 0;
                        }
                        else
                        {
                            count = true;
                        }
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        count = false;
                    }
                }
                else
                {
                    count = true;
                }
            }
            else
            {
                count = true;
            }
            return count;
        }

        public DataTable GetStaffAndRolesMapsPri(string string_3, int int_0)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                if (this.string_2.Length <= 0)
                {
                    str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                }
                else
                {
                    string[] string2 = new string[]
                    {
                        " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                        "'  and GRANTEETYPE='Staff' ) ) and  "
                    };
                    str = string.Concat(string2);
                }
                if (int_0 == 2)
                {
                    str = string.Concat(str, " ObjectType='gisMap' and privilegeFlag='2' ");
                }
                else if (int_0 != 1)
                {
                    dataTable = null;
                    return dataTable;
                }
                else
                {
                    str = string.Concat(str, " ObjectType='gisMap' ");
                }
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public bool GetStaffAndRolesMapsPri(string string_3, int int_0, int int_1)
        {
            bool count;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                if (string_3.Length == 0)
                {
                    string_3 = this.string_1;
                }
                if ((string_3 == "admin" ? false : string_3.Length != 0))
                {
                    string str = "";
                    try
                    {
                        int int1 = int_1;
                        if (this.string_2.Length <= 0)
                        {
                            str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                        }
                        else
                        {
                            string[] string2 = new string[]
                            {
                                " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                                "' and GRANTEETYPE='Staff') )  and  "
                            };
                            str = string.Concat(string2);
                        }
                        if (int_0 == 2)
                        {
                            str = string.Concat(str, " ObjectType='gisMap' and privilegeFlag='2' and GRANTOBJECT = '",
                                int1.ToString(), "'");
                        }
                        else if (int_0 != 1)
                        {
                            count = false;
                            return count;
                        }
                        else
                        {
                            str = string.Concat(str, " ObjectType='gisMap' and privilegeFlag>='1' and GRANTOBJECT = '",
                                int1.ToString(), "'");
                        }
                        DataAccessLayerBaseClass dataAccessLayer =
                            DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                        dataAccessLayer.Open();
                        DataTable dataTable =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        count = dataTable.Rows.Count > 0;
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        count = false;
                    }
                }
                else
                {
                    count = true;
                }
            }
            else
            {
                count = true;
            }
            return count;
        }

        public DataTable GetStaffAndRolesPlugePri(string string_3, int int_0)
        {
            object[] string3;
            DataTable dataTable;
            string str = "";
            try
            {
                if (this.string_2.Length <= 0)
                {
                    string3 = new object[]
                    {
                        " Grantee='", string_3,
                        "'  and GRANTEETYPE='Staff' and  ObjectType='gisPluge' and privilegeFlag='", int_0, "'"
                    };
                    str = string.Concat(string3);
                }
                else
                {
                    string3 = new object[]
                    {
                        " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                        "' and GRANTEETYPE='Staff') )  and  ObjectType='gisPluge' and privilegeFlag='", int_0, "'"
                    };
                    str = string.Concat(string3);
                }
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public bool GetStaffAndRolesPlugePri(string string_3, int int_0, string string_4)
        {
            bool count;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                if (string_3.Length == 0)
                {
                    string_3 = this.string_1;
                }
                if ((string_3 == "admin" ? false : string_3.Length != 0))
                {
                    string str = "";
                    try
                    {
                        if (this.string_2.Length <= 0)
                        {
                            str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Staff' and  ");
                        }
                        else
                        {
                            string[] string2 = new string[]
                            {
                                " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='", string_3,
                                "' and GRANTEETYPE='Staff') )  and  "
                            };
                            str = string.Concat(string2);
                        }
                        if (int_0 == 2)
                        {
                            str = string.Concat(str, " ObjectType='gisPluge' and privilegeFlag='2' and GRANTOBJECT = '",
                                string_4, "'");
                        }
                        else if (int_0 != 1)
                        {
                            count = false;
                            return count;
                        }
                        else
                        {
                            str = string.Concat(str, " ObjectType='gisPluge' and GRANTOBJECT = '", string_4, "'");
                        }
                        DataAccessLayerBaseClass dataAccessLayer =
                            DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                        dataAccessLayer.Open();
                        DataTable dataTable =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        count = dataTable.Rows.Count > 0;
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        count = false;
                    }
                }
                else
                {
                    count = true;
                }
            }
            else
            {
                count = true;
            }
            return count;
        }

        public bool GetStaffAndRolesPlugePri(int int_0, string string_3)
        {
            bool count;
            if (this.dataProviderType_0 == DataProviderType.None)
            {
                count = true;
            }
            else if ((this.string_1 == "admin" ? false : this.string_1.Length != 0))
            {
                string str = "";
                try
                {
                    if (this.string_2.Length <= 0)
                    {
                        str = string.Concat(" Grantee='", this.string_1, "'  and GRANTEETYPE='Staff' and  ");
                    }
                    else
                    {
                        string[] string2 = new string[]
                        {
                            " ( (Grantee in (", this.string_2, ") and GRANTEETYPE='Roles') or (Grantee='",
                            this.string_1, "' and GRANTEETYPE='Staff') )  and  "
                        };
                        str = string.Concat(string2);
                    }
                    if (int_0 == 2)
                    {
                        str = string.Concat(str, " ObjectType='gisPluge' and privilegeFlag='2' and GRANTOBJECT = '",
                            string_3, "'");
                    }
                    else if (int_0 != 1)
                    {
                        count = false;
                        return count;
                    }
                    else
                    {
                        str = string.Concat(str, " ObjectType='gisPluge' and GRANTOBJECT = '", string_3, "'");
                    }
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                    dataAccessLayer.Open();
                    DataTable dataTable =
                        dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                    dataAccessLayer.Close();
                    count = dataTable.Rows.Count > 0;
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, str);
                    count = false;
                }
            }
            else
            {
                count = true;
            }
            return count;
        }

        public DataTable GetStaffLayerPri(string string_3, int int_0)
        {
            DataTable dataTable;
            DataAccessLayerBaseClass dataAccessLayer;
            DataTable dataTable1;
            Exception exception;
            bool flag;
            DialogResult dialogResult;
            string str = "";
            if (int_0 != 2)
            {
                if (int_0 == 1)
                {
                    str = string.Concat("Grantee='", string_3,
                        "' and GRANTEETYPE='Staff' and  (ObjectType='gisLayer' or ObjectType='gisDataset') ");
                    try
                    {
                        dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0,
                            this.string_0);
                        flag = dataAccessLayer.Open();
                        dataTable1 =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        dataTable = dataTable1;
                        return dataTable;
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dialogResult = MessageBox.Show(exception.Message);
                        Logger.Current.Error("", exception, "");
                    }
                    dataTable = null;
                    return dataTable;
                }
                dataTable = null;
                return dataTable;
            }
            else
            {
                str = string.Concat("Grantee='", string_3,
                    "' and GRANTEETYPE='Staff' and  (ObjectType='gisLayer' or ObjectType='gisDataset') and privilegeFlag='2' ");
            }
            try
            {
                dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                flag = dataAccessLayer.Open();
                dataTable1 = dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                exception = exception1;
                dialogResult = MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public DataTable GetStaffMapsPri(string string_3, int int_0)
        {
            DataTable dataTable;
            DataAccessLayerBaseClass dataAccessLayer;
            DataTable dataTable1;
            Exception exception;
            bool flag;
            DialogResult dialogResult;
            string str = "";
            if (int_0 != 2)
            {
                if (int_0 == 1)
                {
                    str = string.Concat("Grantee='", string_3, "' and GRANTEETYPE='Staff' and  ObjectType='gisMap' ");
                    try
                    {
                        dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0,
                            this.string_0);
                        flag = dataAccessLayer.Open();
                        dataTable1 =
                            dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                        dataAccessLayer.Close();
                        dataTable = dataTable1;
                        return dataTable;
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dialogResult = MessageBox.Show(exception.Message);
                        Logger.Current.Error("", exception, "");
                    }
                    dataTable = null;
                    return dataTable;
                }
                dataTable = null;
                return dataTable;
            }
            else
            {
                str = string.Concat("Grantee='", string_3,
                    "' and GRANTEETYPE='Staff' and  ObjectType='gisMap' and privilegeFlag='2' ");
            }
            try
            {
                dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                flag = dataAccessLayer.Open();
                dataTable1 = dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                exception = exception1;
                dialogResult = MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public DataTable GetStaffMenuPri(string string_3)
        {
            DataTable dataTable;
            string str = "";
            try
            {
                str = string.Concat("Grantee= '", string_3, "'  and ObjectType='gisPluge' and  GRANTEETYPE='Staff'");
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public DataTable GetStaffObjectPri(string string_3, string string_4)
        {
            string str = string.Format("ObjectType='{0}' and Grantee='{1}' and GRANTEETYPE='Staff'", string_4, string_3);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_0);
            dataAccessLayer.Open();
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
            dataAccessLayer.Close();
            return dataTable;
        }

        public DataTable GetStaffPlugePri(string string_3, int int_0)
        {
            DataTable dataTable;
            string str = "";
            object[] string3 = new object[]
            {
                "Grantee='", string_3, "' and GRANTEETYPE='Staff' and  ObjectType='gisPluge' and privilegeFlag='",
                int_0, "' "
            };
            str = string.Concat(string3);
            try
            {
                DataAccessLayerBaseClass dataAccessLayer =
                    DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                dataAccessLayer.Open();
                DataTable dataTable1 =
                    dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                dataAccessLayer.Close();
                dataTable = dataTable1;
                return dataTable;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
            dataTable = null;
            return dataTable;
        }

        public bool IsRolesHasMapsPri(string string_3, int int_0, int int_1)
        {
            bool flag;
            string str;
            string[] string3;
            if (this.dataProviderType_0 != DataProviderType.None)
            {
                bool count = true;
                string str1 = "";
                try
                {
                    if (int_0 == 2)
                    {
                        str = str1;
                        string3 = new string[]
                        {
                            str, "Grantee='", string_3,
                            "' and GRANTEETYPE='Roles' and ObjectType='gisMap' and privilegeFlag='2' and  GRANTOBJECT = '",
                            int_1.ToString(), "'"
                        };
                        str1 = string.Concat(string3);
                    }
                    else if (int_0 != 1)
                    {
                        flag = false;
                        return flag;
                    }
                    else
                    {
                        str = str1;
                        string3 = new string[]
                        {
                            str, "Grantee='", string_3,
                            "' and GRANTEETYPE='Roles' and ObjectType='gisMap' and privilegeFlag>='1' and  GRANTOBJECT = '",
                            int_1.ToString(), "'"
                        };
                        str1 = string.Concat(string3);
                    }
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                    dataAccessLayer.Open();
                    DataTable dataTable =
                        dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str1));
                    dataAccessLayer.Close();
                    count = dataTable.Rows.Count > 0;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show(exception.Message);
                    Logger.Current.Error("", exception, "");
                }
                flag = count;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public bool IsStaffHasMapsPri(string string_3, int int_0, int int_1)
        {
            bool result;
            if (this.dataProviderType_0 == DataProviderType.None)
            {
                result = true;
            }
            else
            {
                string str;
                if (int_0 == 2)
                {
                    str = string.Concat(new string[]
                    {
                        "Grantee='",
                        string_3,
                        "' and GRANTEETYPE='Staff' and  ObjectType='gisMap' and privilegeFlag='2'  and  GRANTOBJECT = '",
                        int_1.ToString(),
                        "'"
                    });
                }
                else
                {
                    if (int_0 != 1)
                    {
                        result = false;
                        return result;
                    }
                    str = string.Concat(new string[]
                    {
                        "Grantee='",
                        string_3,
                        "' and GRANTEETYPE='Staff' and ObjectType='gisMap'  and  GRANTOBJECT = '",
                        int_1.ToString(),
                        "'"
                    });
                }
                bool flag = true;
                try
                {
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                    dataAccessLayer.Open();
                    DataTable dataTable = dataAccessLayer.ExecuteDataTable("select * from SYSGRANTS where " + str);
                    dataAccessLayer.Close();
                    flag = (dataTable.Rows.Count > 0);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    Logger.Current.Error("", ex, null);
                }
                result = flag;
            }
            return result;
        }


        public static void RegisterLayer(IFeatureClass ifeatureClass_0)
        {
            try
            {
                ITable table = AppConfigInfo.OpenTable("LayerConfig");
                int num = table.FindField("Name");
                int num1 = table.FindField("FeatureClassName");
                string name = (ifeatureClass_0 as IDataset).Name;
                IQueryFilter queryFilterClass = new QueryFilter()
                {
                    WhereClause = string.Concat("FeatureClassName = '", name, "'")
                };
                if (table.RowCount(queryFilterClass) == 0)
                {
                    IRow row = table.CreateRow();
                    row.Value[num] = name;
                    row.Value[num1] = name;
                    row.Store();
                }
            }
            catch
            {
            }
        }

        public bool RolesHasDatasetPri(string string_3, string string_4, out int int_0)
        {
            bool flag;
            int_0 = 0;
            string str = "";
            try
            {
                string[] strArrays = string_4.Split(new char[] {'.'});
                string_4 = strArrays[(int) strArrays.Length - 1];
                IQueryFilter queryFilterClass = new QueryFilter()
                {
                    WhereClause = string.Concat("RealName = '", string_4, "'")
                };
                ITable table = AppConfigInfo.OpenTable("DATASETTABLE");
                ICursor cursor = null;
                cursor = table.Search(queryFilterClass, false);
                IRow row = cursor.NextRow();
                ComReleaser.ReleaseCOMObject(cursor);
                if (row != null)
                {
                    int oID = row.OID;
                    str = string.Concat(" Grantee='", string_3, "'  and GRANTEETYPE='Roles' and  ");
                    str = string.Concat(str, " ObjectType='gisDataset' and GRANTOBJECT = '", oID.ToString(), "'");
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                    dataAccessLayer.Open();
                    DataTable dataTable =
                        dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                    dataAccessLayer.Close();
                    bool flag1 = false;
                    if (dataTable.Rows.Count > 0)
                    {
                        object item = dataTable.Rows[0]["privilegeFlag"];
                        if (!(item is DBNull))
                        {
                            int_0 = Convert.ToInt32(item);
                        }
                        flag1 = true;
                    }
                    dataTable = null;
                    flag = flag1;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                flag = false;
            }
            return flag;
        }

        public bool StaffIsHasMenuPri(MenuInfoHelper menuInfoHelper_0, string string_3, string string_4)
        {
            bool flag;
            string str;
            if (this.string_1 != "admin")
            {
                MenuInfo byClassName = menuInfoHelper_0.GetByClassName(string_3, string_4);
                if (byClassName == null)
                {
                    flag = true;
                }
                else
                {
                    str = (!string.IsNullOrEmpty(this.string_2)
                        ? string.Format(
                            " ((Grantee in ({0}) and GRANTEETYPE='Roles' ) or (Grantee = '{1}' and GRANTEETYPE='Staff' )) and GRANTOBJECT='{2}' and OBJECTTYPE='gisPluge' ",
                            this.string_2, this.string_1, byClassName.MenuID)
                        : string.Format(
                            "  Grantee = '{1}' and GRANTEETYPE='Staff'  and GRANTOBJECT='{2}' and OBJECTTYPE='gisPluge' ",
                            this.string_2, this.string_1, byClassName.MenuID));
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                    dataAccessLayer.Open();
                    DataTable dataTable =
                        dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                    dataAccessLayer.Close();
                    flag = (dataTable.Rows.Count <= 0 ? false : true);
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public bool StaffIsHasMenuPri(MenuInfoHelper menuInfoHelper_0, string string_3)
        {
            bool flag;
            string str;
            if (this.string_1 != "admin")
            {
                MenuInfo byClassName = menuInfoHelper_0.GetByClassName(string_3);
                if (byClassName == null)
                {
                    flag = true;
                }
                else
                {
                    str = (!string.IsNullOrEmpty(this.string_2)
                        ? string.Format(
                            " ((Grantee in ({0}) and GRANTEETYPE='Roles' ) or (Grantee = '{1}' and GRANTEETYPE='Staff' )) and GRANTOBJECT='{2}' and OBJECTTYPE='gisPluge' ",
                            this.string_2, this.string_1, byClassName.MenuID)
                        : string.Format(
                            "  Grantee = '{1}' and GRANTEETYPE='Staff'  and GRANTOBJECT='{2}' and OBJECTTYPE='gisPluge' ",
                            this.string_2, this.string_1, byClassName.MenuID));
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_0);
                    dataAccessLayer.Open();
                    DataTable dataTable =
                        dataAccessLayer.ExecuteDataTable(string.Concat("select * from SYSGRANTS where ", str));
                    dataAccessLayer.Close();
                    flag = (dataTable.Rows.Count <= 0 ? false : true);
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public void UpdateGrant(string string_3, string string_4, string string_5, string string_6, string string_7)
        {
            object[] string7 = new object[] {"SYSGRANTS", string_7, string_3, string_4, string_5, string_6};
            string str =
                string.Format(
                    "update {0} set PRIVILEGEFLAG='{1}' where GRANTEE='{2}' and GRANTEETYPE='{3}' and GRANTOBJECT='{4}' and OBJECTTYPE='{5}' ",
                    string7);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(
                this.dataProviderType_0, this.string_0);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }
    }
}