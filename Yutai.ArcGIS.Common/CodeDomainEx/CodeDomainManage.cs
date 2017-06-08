using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Common.CodeDomainEx
{
    public class CodeDomainManage
    {
        private static SortedList<string, NameValueCollection> FieldDomain;
        private static SortedList<string, CodeDomainEx> FieldDomainMap;
        private static List<CodeDomainEx> m_CodeDomainExs;
        private static ITable m_pDomainMapTable;
        private static ITable m_pDomainTable;

        static CodeDomainManage()
        {
            old_acctor_mc();
        }

        public static CodeDomainEx AddCodeDomain(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, esriFieldType esriFieldType_0)
        {
            if (m_pDomainTable == null)
            {
                m_pDomainTable = AppConfigInfo.OpenTable(CodeDomainTableStruct.TableName);
                if (m_pDomainTable == null)
                {
                    return null;
                }
            }
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = string.Format("{0} = '{1}'", CodeDomainTableStruct.DomainNameFieldName, string_2)
            };
            if (m_pDomainTable.RowCount(queryFilter) > 0)
            {
                MessageBox.Show("存在同名域值定义，请取其他名字!");
                return null;
            }
            string str = Guid.NewGuid().ToString();
            IRow row = m_pDomainTable.CreateRow();
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ConnectionFieldName), string_0);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.TableFieldName), string_1);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DomainNameFieldName), string_2);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ValueFieldName), string_3);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DescriptionFieldName), string_4);
            m_pDomainTable.FindField(CodeDomainTableStruct.DomainIDFieldName);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DomainIDFieldName), str);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.fieldtypeFieldName), (short) esriFieldType_0);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.IDFieldName), string_5);
            row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ParentIDFieldName), string_6);
            row.Store();
            CodeDomainEx item = new CodeDomainEx {
                DomainID = str,
                Name = string_2,
                FieldType = esriFieldType_0,
                ConnectionStr = string_0,
                CodeFieldName = string_3,
                IDFieldName = string_5,
                NameFieldName = string_4,
                ParentIDFieldName = string_6,
                TableFieldName = string_1
            };
            CodeDomainExs.Add(item);
            return item;
        }

        public static void AddFieldCodeDoaminMap(string string_0, string string_1, string string_2)
        {
            if (m_pDomainMapTable == null)
            {
                m_pDomainMapTable = AppConfigInfo.OpenTable(CodeDomainMapTableStruct.TableName);
                if (m_pDomainMapTable == null)
                {
                    return;
                }
            }
            string[] strArray = string_0.Split(new char[] { '.' });
            string_0 = strArray[strArray.Length - 1];
            if (CheckHasDamain(string_1, string_0))
            {
                ModifyCodeDoaminMap(string_0, string_1, string_2);
            }
            else
            {
                IRow row = m_pDomainMapTable.CreateRow();
                row.set_Value(m_pDomainMapTable.FindField(CodeDomainMapTableStruct.FeatureClassName), string_0);
                row.set_Value(m_pDomainMapTable.FindField(CodeDomainMapTableStruct.FieldName), string_1);
                row.set_Value(m_pDomainMapTable.FindField(CodeDomainMapTableStruct.DomainID), string_2);
                row.Store();
            }
        }

        public static bool CheckHasDamain(string string_0, string string_1)
        {
            if (m_pDomainMapTable == null)
            {
                m_pDomainMapTable = AppConfigInfo.OpenTable(CodeDomainMapTableStruct.TableName);
                if (m_pDomainMapTable == null)
                {
                    return false;
                }
            }
            string[] strArray = string_1.Split(new char[] { '.' });
            string str = string.Format("({0}='{1}' and {2}='{3}') or ({0}='{1}' and {2}='')", new object[] { CodeDomainMapTableStruct.FieldName, string_0, CodeDomainMapTableStruct.FeatureClassName, strArray[strArray.Length - 1] });
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = str
            };
            return (m_pDomainMapTable.RowCount(queryFilter) > 0);
        }

        public static void Clear()
        {
            FieldDomain.Clear();
        }

        public static void DeleteCodeDoaminMap(string string_0, string string_1)
        {
            if (m_pDomainMapTable == null)
            {
                m_pDomainMapTable = AppConfigInfo.OpenTable(CodeDomainMapTableStruct.TableName);
                if (m_pDomainMapTable == null)
                {
                    return;
                }
            }
            string[] strArray = string_0.Split(new char[] { '.' });
            string_0 = strArray[strArray.Length - 1];
            string str = string.Format("{0}='{1}' and {2}='{3}'", new object[] { CodeDomainMapTableStruct.FieldName, string_1, CodeDomainMapTableStruct.FeatureClassName, string_0 });
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = str
            };
            ICursor o = m_pDomainMapTable.Search(queryFilter, false);
            IRow row = o.NextRow();
            if (row != null)
            {
                row.Delete();
            }
            ComReleaser.ReleaseCOMObject(o);
        }

        public static void DeleteDomain(string string_0)
        {
            if (m_pDomainTable == null)
            {
                m_pDomainTable = AppConfigInfo.OpenTable(CodeDomainTableStruct.TableName);
                if (m_pDomainTable == null)
                {
                    return;
                }
            }
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = string.Format("{0} = '{1}'", CodeDomainTableStruct.DomainIDFieldName, string_0)
            };
            m_pDomainTable.DeleteSearchedRows(queryFilter);
            if (m_pDomainMapTable == null)
            {
                m_pDomainMapTable = AppConfigInfo.OpenTable(CodeDomainMapTableStruct.TableName);
                if (m_pDomainMapTable == null)
                {
                    return;
                }
            }
            queryFilter.WhereClause = string.Format("{0} = '{1}'", CodeDomainMapTableStruct.DomainID, string_0);
            m_pDomainMapTable.DeleteSearchedRows(queryFilter);
        }

        public static bool EditDomain(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, esriFieldType esriFieldType_0)
        {
            if (m_pDomainTable == null)
            {
                m_pDomainTable = AppConfigInfo.OpenTable(CodeDomainTableStruct.TableName);
                if (m_pDomainTable == null)
                {
                    return false;
                }
            }
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = string.Format("{0} = '{1}' and {2}<>'{3}'", new object[] { CodeDomainTableStruct.DomainNameFieldName, string_3, CodeDomainTableStruct.DomainIDFieldName, string_0 })
            };
            if (m_pDomainTable.RowCount(queryFilter) > 0)
            {
                MessageBox.Show("存在同名域值定义，请取其他名字!");
                return false;
            }
            queryFilter.WhereClause = string.Format("{0} = '{1}'", CodeDomainTableStruct.DomainIDFieldName, string_0);
            ICursor o = m_pDomainTable.Search(queryFilter, false);
            IRow row = o.NextRow();
            if (row != null)
            {
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ConnectionFieldName), string_1);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.TableFieldName), string_2);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DomainNameFieldName), string_3);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ValueFieldName), string_4);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DescriptionFieldName), string_5);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.fieldtypeFieldName), (short) esriFieldType_0);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.IDFieldName), string_6);
                row.set_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ParentIDFieldName), string_7);
                row.Store();
            }
            ComReleaser.ReleaseCOMObject(o);
            return true;
        }

        public static NameValueCollection GetCodeDomain(string string_0, NameValueCollection nameValueCollection_0)
        {
            if (m_pDomainTable == null)
            {
                m_pDomainTable = AppConfigInfo.OpenTable(CodeDomainTableStruct.TableName);
            }
            if (m_pDomainTable != null)
            {
                string str = string.Format("{0}='{1}'", CodeDomainTableStruct.DomainIDFieldName, string_0);
                IQueryFilter queryFilter = new QueryFilter {
                    WhereClause = str
                };
                ICursor o = m_pDomainTable.Search(queryFilter, false);
                IRow row = o.NextRow();
                int index = m_pDomainTable.FindField(CodeDomainTableStruct.ConnectionFieldName);
                int num2 = m_pDomainTable.FindField(CodeDomainTableStruct.TableFieldName);
                int num3 = m_pDomainTable.FindField(CodeDomainTableStruct.DescriptionFieldName);
                int num4 = m_pDomainTable.FindField(CodeDomainTableStruct.ValueFieldName);
                if (row != null)
                {
                    string str2 = row.get_Value(num2).ToString();
                    string str3 = row.get_Value(index).ToString();
                    string str4 = row.get_Value(num3).ToString();
                    string str5 = row.get_Value(num4).ToString();
                    DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(DataProviderType.OleDb, str3);
                    string str6 = string.Format("select {0},{1} from {2}", str4, str5, str2);
                    dataAccessLayer.Open();
                    DataTable table = dataAccessLayer.ExecuteDataTable(str6);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string name = table.Rows[i][str4].ToString().Trim();
                        string str8 = table.Rows[i][str5].ToString().Trim();
                        nameValueCollection_0.Add(name, str8);
                    }
                }
                ComReleaser.ReleaseCOMObject(o);
            }
            return nameValueCollection_0;
        }

        public static NameValueCollection GetCodeDomain(string string_0, string string_1)
        {
            string[] strArray = string_1.Split(new char[] { '.' });
            string key = string.Format("{0},{1}", string_0, strArray[strArray.Length - 1]);
            if (FieldDomain.ContainsKey(key))
            {
                return FieldDomain[key];
            }
            NameValueCollection values2 = new NameValueCollection();
            if (CheckHasDamain(string_0, string_1))
            {
                string str2 = string.Format("({0}='{1}' and {2}='{3}') or ({0}='{1}' and {2}='')", new object[] { CodeDomainMapTableStruct.FieldName, string_0, CodeDomainMapTableStruct.FeatureClassName, strArray[strArray.Length - 1] });
                IQueryFilter queryFilter = new QueryFilter {
                    WhereClause = str2
                };
                ICursor o = m_pDomainMapTable.Search(queryFilter, false);
                IRow row = o.NextRow();
                int index = m_pDomainMapTable.FindField(CodeDomainMapTableStruct.FeatureClassName);
                int num2 = m_pDomainMapTable.FindField(CodeDomainMapTableStruct.DomainID);
                string str3 = "";
                while (row != null)
                {
                    string str4 = row.get_Value(index).ToString().ToLower();
                    if (str4 == "")
                    {
                        str3 = row.get_Value(num2).ToString();
                    }
                    else if (str4 == strArray[strArray.Length - 1].ToLower())
                    {
                        str3 = row.get_Value(num2).ToString();
                        break;
                    }
                    row = o.NextRow();
                }
                ComReleaser.ReleaseCOMObject(o);
                if (str3.Length > 0)
                {
                    GetCodeDomain(str3, values2);
                }
            }
            FieldDomain.Add(key, values2);
            return values2;
        }

        public static CodeDomainEx GetCodeDomainEx(string string_0, string string_1)
        {
            CodeDomainEx ex = null;
            string[] strArray = string_1.Split(new char[] { '.' });
            string key = string.Format("{0},{1}", string_0, strArray[strArray.Length - 1]);
            if (FieldDomainMap.ContainsKey(key))
            {
                return FieldDomainMap[key];
            }
            if (CheckHasDamain(string_0, string_1))
            {
                string str2 = string.Format("({0}='{1}' and {2}='{3}') or ({0}='{1}' and {2}='')", new object[] { CodeDomainMapTableStruct.FieldName, string_0, CodeDomainMapTableStruct.FeatureClassName, strArray[strArray.Length - 1] });
                IQueryFilter queryFilter = new QueryFilter {
                    WhereClause = str2
                };
                ICursor o = m_pDomainMapTable.Search(queryFilter, false);
                IRow row = o.NextRow();
                int index = m_pDomainMapTable.FindField(CodeDomainMapTableStruct.FeatureClassName);
                int num2 = m_pDomainMapTable.FindField(CodeDomainMapTableStruct.DomainID);
                string str3 = "";
                while (row != null)
                {
                    string str4 = row.get_Value(index).ToString().ToLower();
                    if (str4 == "")
                    {
                        str3 = row.get_Value(num2).ToString();
                    }
                    else if (str4 == strArray[strArray.Length - 1].ToLower())
                    {
                        str3 = row.get_Value(num2).ToString();
                        break;
                    }
                    row = o.NextRow();
                }
                ComReleaser.ReleaseCOMObject(o);
                if (str3.Length > 0)
                {
                    string str5;
                    string str6;
                    string str7;
                    string str8;
                    string str9;
                    string str10;
                    string str11;
                    esriFieldType type;
                    GetDomainInfo(str3, out str5, out str6, out str7, out str8, out str9, out str10, out str11, out type);
                    ex = new CodeDomainEx {
                        NameFieldName = str9,
                        ParentIDFieldName = str11.Trim(),
                        ConnectionStr = str5,
                        IDFieldName = str10.Trim(),
                        CodeFieldName = str8.Trim(),
                        TableFieldName = str6,
                        DomainID = str3.Trim(),
                        FieldType = type,
                        Name = str7
                    };
                }
            }
            FieldDomainMap.Add(key, ex);
            return ex;
        }

        public static string GetDamainName(string string_0, string string_1)
        {
            string str = "";
            if (m_pDomainMapTable == null)
            {
                m_pDomainMapTable = AppConfigInfo.OpenTable(CodeDomainMapTableStruct.TableName);
                if (m_pDomainMapTable == null)
                {
                    return str;
                }
            }
            string[] strArray = string_1.Split(new char[] { '.' });
            string str3 = string.Format("{0}='{1}' and {2}='{3}'", new object[] { CodeDomainMapTableStruct.FieldName, string_0, CodeDomainMapTableStruct.FeatureClassName, strArray[strArray.Length - 1] });
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = str3
            };
            ICursor o = m_pDomainMapTable.Search(queryFilter, false);
            IRow row = o.NextRow();
            if (row != null)
            {
                str = row.get_Value(m_pDomainMapTable.FindField(CodeDomainMapTableStruct.DomainID)).ToString();
                row.Store();
            }
            ComReleaser.ReleaseCOMObject(o);
            return str;
        }

        public static bool GetDomainInfo(string string_0, out string string_1, out string string_2, out string string_3, out string string_4, out string string_5, out string string_6, out string string_7, out esriFieldType esriFieldType_0)
        {
            string_1 = "";
            string_2 = "";
            string_3 = "";
            string_4 = "";
            string_5 = "";
            string_6 = "";
            string_7 = "";
            esriFieldType_0 = esriFieldType.esriFieldTypeString;
            if (m_pDomainTable == null)
            {
                m_pDomainTable = AppConfigInfo.OpenTable(CodeDomainTableStruct.TableName);
                if (m_pDomainTable == null)
                {
                    return false;
                }
            }
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = string.Format("{0} = '{1}'", CodeDomainTableStruct.DomainIDFieldName, string_0)
            };
            ICursor o = m_pDomainTable.Search(queryFilter, false);
            IRow row = o.NextRow();
            if (row != null)
            {
                string_1 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ConnectionFieldName)).ToString();
                string_2 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.TableFieldName)).ToString();
                string_3 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DomainNameFieldName)).ToString();
                string_4 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ValueFieldName)).ToString();
                string_5 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.DescriptionFieldName)).ToString();
                string_6 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.IDFieldName)).ToString();
                string_7 = row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.ParentIDFieldName)).ToString();
                esriFieldType_0 = (esriFieldType) Convert.ToInt32(row.get_Value(m_pDomainTable.FindField(CodeDomainTableStruct.fieldtypeFieldName)));
            }
            ComReleaser.ReleaseCOMObject(o);
            return true;
        }

        public static void LoadDomain()
        {
            if (m_pDomainTable == null)
            {
                m_pDomainTable = AppConfigInfo.OpenTable(CodeDomainTableStruct.TableName);
            }
            if (m_pDomainTable != null)
            {
                ICursor o = m_pDomainTable.Search(null, false);
                IRow row = o.NextRow();
                int index = m_pDomainTable.FindField(CodeDomainTableStruct.DomainNameFieldName);
                int num2 = m_pDomainTable.FindField(CodeDomainTableStruct.DomainIDFieldName);
                int num3 = m_pDomainTable.FindField(CodeDomainTableStruct.fieldtypeFieldName);
                int num4 = m_pDomainTable.FindField(CodeDomainTableStruct.DescriptionFieldName);
                int num5 = m_pDomainTable.FindField(CodeDomainTableStruct.ValueFieldName);
                int num6 = m_pDomainTable.FindField(CodeDomainTableStruct.ParentIDFieldName);
                int num7 = m_pDomainTable.FindField(CodeDomainTableStruct.IDFieldName);
                int num8 = m_pDomainTable.FindField(CodeDomainTableStruct.TableFieldName);
                int num9 = m_pDomainTable.FindField(CodeDomainTableStruct.ConnectionFieldName);
                while (row != null)
                {
                    string str = row.get_Value(index).ToString();
                    if (str.Length > 0)
                    {
                        string str2 = row.get_Value(num2).ToString();
                        esriFieldType type = (esriFieldType) Convert.ToInt32(row.get_Value(num3));
                        CodeDomainEx item = new CodeDomainEx {
                            DomainID = str2,
                            Name = str,
                            FieldType = type,
                            TableFieldName = row.get_Value(num8).ToString(),
                            CodeFieldName = row.get_Value(num5).ToString(),
                            IDFieldName = row.get_Value(num7).ToString(),
                            ConnectionStr = row.get_Value(num9).ToString(),
                            ParentIDFieldName = row.get_Value(num6).ToString(),
                            NameFieldName = row.get_Value(num4).ToString()
                        };
                        CodeDomainExs.Add(item);
                    }
                    row = o.NextRow();
                }
                ComReleaser.ReleaseCOMObject(o);
            }
        }

        public static void ModifyCodeDoaminMap(string string_0, string string_1, string string_2)
        {
            if (m_pDomainMapTable == null)
            {
                m_pDomainMapTable = AppConfigInfo.OpenTable(CodeDomainMapTableStruct.TableName);
                if (m_pDomainMapTable == null)
                {
                    return;
                }
            }
            string[] strArray = string_0.Split(new char[] { '.' });
            string_0 = strArray[strArray.Length - 1];
            string str = string.Format("{0}='{1}' and {2}='{3}'", new object[] { CodeDomainMapTableStruct.FieldName, string_1, CodeDomainMapTableStruct.FeatureClassName, string_0 });
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = str
            };
            ICursor o = m_pDomainMapTable.Search(queryFilter, false);
            IRow row = o.NextRow();
            if (row != null)
            {
                row.set_Value(m_pDomainMapTable.FindField(CodeDomainMapTableStruct.DomainID), string_2);
                row.Store();
            }
            ComReleaser.ReleaseCOMObject(o);
        }

        private static void old_acctor_mc()
        {
            m_CodeDomainExs = null;
            m_pDomainMapTable = null;
            m_pDomainTable = null;
            FieldDomain = new SortedList<string, NameValueCollection>();
            FieldDomainMap = new SortedList<string, CodeDomainEx>();
        }

        public static List<CodeDomainEx> CodeDomainExs
        {
            get
            {
                if (m_CodeDomainExs == null)
                {
                    m_CodeDomainExs = new List<CodeDomainEx>();
                    LoadDomain();
                }
                return m_CodeDomainExs;
            }
        }

        public static ITable DomainTable
        {
            get
            {
                return m_pDomainTable;
            }
        }
    }
}

