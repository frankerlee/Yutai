using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Data;
using Yutai.ArcGIS.Common.Priviliges;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class BulidMenuXMLByDB
    {
        private DataProviderType dataProviderType_0;
        private int int_0;
        private MenuInfoHelper menuInfoHelper_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private SysGrants sysGrants_0;

        public BulidMenuXMLByDB(string string_3)
        {
            this.string_0 = "";
            this.string_1 = "";
            this.sysGrants_0 = null;
            this.menuInfoHelper_0 = new MenuInfoHelper();
            this.dataProviderType_0 = DataProviderType.Sql;
            this.int_0 = 0;
            this.string_0 = string_3;
            this.sysGrants_0 = new SysGrants();
            this.dataProviderType_0 = DataProviderType.Sql;
            string[] strArray2 = ConfigurationManager.AppSettings["SYSPRIVDB"].Split(new string[] {"||"},
                StringSplitOptions.RemoveEmptyEntries);
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
            this.string_2 = strArray2[1];
        }

        public BulidMenuXMLByDB(string string_3, string string_4)
        {
            this.string_0 = "";
            this.string_1 = "";
            this.sysGrants_0 = null;
            this.menuInfoHelper_0 = new MenuInfoHelper();
            this.dataProviderType_0 = DataProviderType.Sql;
            this.int_0 = 0;
            this.string_0 = string_3;
            this.string_1 = string_4.ToLower();
            this.sysGrants_0 = new SysGrants(string_4);
            this.dataProviderType_0 = DataProviderType.Sql;
            string[] strArray2 = ConfigurationManager.AppSettings["SYSPRIVDB"].Split(new string[] {"||"},
                StringSplitOptions.RemoveEmptyEntries);
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
            this.string_2 = strArray2[1];
        }

        public void CreateXML(string string_3)
        {
            try
            {
                string str2;
                string str3;
                ITable table = AppConfigInfo.OpenTable(this.string_0);
                IQueryFilter filter = new QueryFilter
                {
                    WhereClause = "  ParentIDS = '0'"
                };
                (filter as IQueryFilterDefinition).PostfixClause = " order by menuid ";
                XmlDocument document = new XmlDocument();
                StringBuilder builder = new StringBuilder();
                builder.Append("<?xml version='1.0' encoding='utf-8' ?>");
                builder.Append("<Bars>");
                builder.Append("</Bars>");
                document.LoadXml(builder.ToString());
                XmlNode node = document.ChildNodes[1];
                string str = "";
                ICursor o = table.Search(null, false);
                IRow row = o.NextRow();
                int num = 0;
                SortedList<int, XmlNode> list = new SortedList<int, XmlNode>();
                Label_009C:
                if (row == null)
                {
                    goto Label_0384;
                }
                XmlNode node2 = document.CreateElement("Bar");
                object obj2 = row.get_Value(table.FindField("ItemCol"));
                object obj3 = row.get_Value(table.FindField("OrderBy"));
                object obj4 = row.get_Value(table.FindField("Caption"));
                object obj5 = row.get_Value(table.FindField("Name"));
                object obj6 = row.get_Value(table.FindField("Visible"));
                if (obj4 is DBNull)
                {
                    str2 = "";
                }
                else
                {
                    str2 = obj4.ToString().Trim();
                }
                if (obj5 is DBNull)
                {
                    str3 = "";
                }
                else
                {
                    str3 = obj5.ToString().Trim();
                }
                XmlAttribute attribute = document.CreateAttribute("caption");
                attribute.Value = str2;
                node2.Attributes.Append(attribute);
                attribute = document.CreateAttribute("name");
                attribute.Value = str3;
                node2.Attributes.Append(attribute);
                if (obj3 is DBNull)
                {
                    attribute = document.CreateAttribute("row");
                    attribute.Value = num.ToString();
                    node2.Attributes.Append(attribute);
                }
                else
                {
                    try
                    {
                        int num2 = int.Parse(obj3.ToString());
                        attribute = document.CreateAttribute("row");
                        attribute.Value = num2.ToString();
                        node2.Attributes.Append(attribute);
                    }
                    catch
                    {
                    }
                }
                goto Label_036D;
                Label_0232:
                attribute = document.CreateAttribute("col");
                attribute.Value = "0";
                node2.Attributes.Append(attribute);
                goto Label_029A;
                Label_025C:
                ;
                try
                {
                    int num3 = int.Parse(obj2.ToString());
                    attribute = document.CreateAttribute("col");
                    attribute.Value = num3.ToString();
                    node2.Attributes.Append(attribute);
                }
                catch
                {
                }
                Label_029A:
                attribute = document.CreateAttribute("visible");
                if (obj6 is DBNull)
                {
                    attribute.Value = "True";
                }
                else
                {
                    try
                    {
                        if (short.Parse(obj6.ToString()) == 0)
                        {
                            attribute.Value = "False";
                        }
                        else
                        {
                            attribute.Value = "True";
                        }
                    }
                    catch
                    {
                        attribute.Value = "True";
                    }
                }
                node2.Attributes.Append(attribute);
                int key = Convert.ToInt32(row.get_Value(table.FindField("menuid")));
                list.Add(key, node2);
                if (node2.ChildNodes.Count > 0)
                {
                    node.AppendChild(node2);
                }
                num++;
                row = o.NextRow();
                goto Label_009C;
                Label_036D:
                if (!(obj2 is DBNull))
                {
                    goto Label_025C;
                }
                goto Label_0232;
                Label_0384:
                ComReleaser.ReleaseCOMObject(o);
                foreach (KeyValuePair<int, XmlNode> pair in list)
                {
                    this.method_0(pair.Value, document, pair.Key, table);
                }
                if (str.Length != 0)
                {
                    document.ChildNodes[1].AppendChild(this.method_1(document, "MainMenu", str));
                }
                if (File.Exists(string_3))
                {
                    File.Delete(string_3);
                }
                document.Save(string_3);
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            this.sysGrants_0.CloseTable();
        }

        private void method_0(XmlNode xmlNode_0, XmlDocument xmlDocument_0, int int_1, ITable itable_0)
        {
            SortedList<int, XmlNode> list = new SortedList<int, XmlNode>();
            IQueryFilter filter = new QueryFilter
            {
                WhereClause = "  ParentIDS like '%" + int_1.ToString() + "%' "
            };
            (filter as IQueryFilterDefinition).PostfixClause = " order by menuid ";
            bool flag = false;
            ICursor o = itable_0.Search(null, false);
            IRow row = o.NextRow();
            int num = 0;
            Label_004F:
            if (row == null)
            {
                ComReleaser.ReleaseCOMObject(o);
                foreach (KeyValuePair<int, XmlNode> pair in list)
                {
                    this.method_0(pair.Value, xmlDocument_0, pair.Key, itable_0);
                }
            }
            else
            {
                string[] strArray = row.get_Value(itable_0.FindField("ParentIDS")).ToString().Split(new char[] {','});
                int index = 0;
                index = 0;
                while (index < strArray.Length)
                {
                    if (strArray[index] == int_1.ToString())
                    {
                        flag = true;
                        break;
                    }
                    index++;
                }
                if (flag)
                {
                    string str2;
                    XmlAttribute attribute;
                    XmlNode node = xmlDocument_0.CreateElement("BarItem");
                    flag = false;
                    object obj3 = row.get_Value(itable_0.FindField("Caption"));
                    object obj4 = row.get_Value(itable_0.FindField("Name"));
                    if (obj4 is DBNull)
                    {
                        str2 = "";
                    }
                    else
                    {
                        str2 = obj4.ToString().Trim();
                    }
                    if (str2.Length == 0)
                    {
                        str2 = "Name";
                    }
                    else
                    {
                        attribute = xmlDocument_0.CreateAttribute("name");
                        attribute.Value = str2;
                        node.Attributes.Append(attribute);
                    }
                    if (!(obj3 is DBNull))
                    {
                        string str3 = obj3.ToString().Trim();
                        attribute = xmlDocument_0.CreateAttribute("caption");
                        attribute.Value = str3;
                        node.Attributes.Append(attribute);
                    }
                    object obj5 = row.get_Value(itable_0.FindField("componentdllname"));
                    object obj6 = row.get_Value(itable_0.FindField("classname"));
                    object obj7 = row.get_Value(itable_0.FindField("progid"));
                    if (((obj5 is DBNull) || (obj6 is DBNull)) ? (obj7 is DBNull) : false)
                    {
                        int key = Convert.ToInt32(row.get_Value(itable_0.FindField("menuid")));
                        list.Add(key, node);
                        if (node.ChildNodes.Count > 0)
                        {
                            xmlNode_0.AppendChild(node);
                        }
                    }
                    else
                    {
                        bool staffAndRolesPlugePri = true;
                        int num3 = Convert.ToInt32(row.get_Value(itable_0.FindField("menuid")));
                        if ((this.string_1.Length > 0) && (this.string_1 != "admin"))
                        {
                            staffAndRolesPlugePri = this.sysGrants_0.GetStaffAndRolesPlugePri(1, num3.ToString());
                        }
                        if (staffAndRolesPlugePri)
                        {
                            string str4;
                            bool flag3;
                            if (obj7 is DBNull)
                            {
                                attribute = xmlDocument_0.CreateAttribute("classname");
                                attribute.Value = obj6.ToString().Trim();
                                node.Attributes.Append(attribute);
                                attribute = xmlDocument_0.CreateAttribute("path");
                                attribute.Value = obj5.ToString().Trim();
                                node.Attributes.Append(attribute);
                            }
                            else
                            {
                                attribute = xmlDocument_0.CreateAttribute("progid");
                                attribute.Value = obj7.ToString().Trim();
                                node.Attributes.Append(attribute);
                            }
                            object obj8 = row.get_Value(itable_0.FindField("subtype"));
                            if (!(obj8 is DBNull))
                            {
                                attribute = xmlDocument_0.CreateAttribute("subtype");
                                attribute.Value = obj8.ToString().Trim();
                                node.Attributes.Append(attribute);
                            }
                            object obj9 = row.get_Value(itable_0.FindField("IsPopmenuItem"));
                            if (!(obj9 is DBNull))
                            {
                                str4 = obj9.ToString().Trim();
                                flag3 = false;
                                if ((str4 == "1") && (index == 0))
                                {
                                    flag3 = true;
                                }
                                attribute = xmlDocument_0.CreateAttribute("ispopupmenu");
                                attribute.Value = flag3.ToString();
                                node.Attributes.Append(attribute);
                            }
                            object obj10 = row.get_Value(itable_0.FindField("BeginGroup"));
                            if (!(obj10 is DBNull))
                            {
                                str4 = obj10.ToString().Trim();
                                flag3 = false;
                                if (str4 == "1")
                                {
                                    flag3 = true;
                                }
                                attribute = xmlDocument_0.CreateAttribute("BeginGroup");
                                attribute.Value = flag3.ToString();
                                node.Attributes.Append(attribute);
                            }
                            object obj11 = row.get_Value(itable_0.FindField("shortcut"));
                            if (!(obj11 is DBNull))
                            {
                                attribute = xmlDocument_0.CreateAttribute("shortcut");
                                attribute.Value = obj11.ToString();
                                node.Attributes.Append(attribute);
                            }
                            object obj12 = row.get_Value(itable_0.FindField("visible"));
                            if (!(obj12 is DBNull))
                            {
                                attribute = xmlDocument_0.CreateAttribute("visible");
                                try
                                {
                                    if (short.Parse(obj12.ToString()) == 0)
                                    {
                                        attribute.Value = "False";
                                    }
                                    else
                                    {
                                        attribute.Value = "True";
                                    }
                                }
                                catch
                                {
                                    attribute.Value = "True";
                                }
                                node.Attributes.Append(attribute);
                            }
                            xmlNode_0.AppendChild(node);
                        }
                    }
                }
                num++;
                row = o.NextRow();
                goto Label_004F;
            }
        }

        private XmlNode method_1(XmlDocument xmlDocument_0, string string_3, string string_4)
        {
            XmlNode node = xmlDocument_0.CreateNode(XmlNodeType.Element, "property", "");
            XmlAttribute attribute = xmlDocument_0.CreateAttribute("name");
            attribute.Value = string_3;
            node.Attributes.Append(attribute);
            XmlNode newChild = xmlDocument_0.CreateNode(XmlNodeType.Text, "", "");
            newChild.Value = string_4;
            node.AppendChild(newChild);
            return node;
        }

        private void method_10(XmlNode xmlNode_0, int int_1)
        {
            try
            {
                XmlNode node = null;
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    node = null;
                    XmlNode node2 = xmlNode_0.ChildNodes[i];
                    if (node2.ChildNodes.Count != 0)
                    {
                        string[] strArray = new string[7];
                        for (int j = 0; j < node2.ChildNodes.Count; j++)
                        {
                            XmlNode node3 = node2.ChildNodes[j];
                            if (node3.ChildNodes.Count != 0)
                            {
                                switch (node3.Attributes["name"].Value.ToLower())
                                {
                                    case "itemname":
                                        strArray[0] = node3.ChildNodes[0].Value;
                                        break;

                                    case "caption":
                                        strArray[1] = node3.ChildNodes[0].Value;
                                        break;

                                    case "path":
                                        strArray[2] = node3.ChildNodes[0].Value;
                                        break;

                                    case "classname":
                                        strArray[3] = node3.ChildNodes[0].Value;
                                        break;

                                    case "itemlinks":
                                        node = node3;
                                        break;

                                    case "subtype":
                                        strArray[4] = node3.ChildNodes[0].Value;
                                        break;

                                    case "popupmenu":
                                        strArray[5] = node3.ChildNodes[0].Value;
                                        break;

                                    case "begingroup":
                                        strArray[6] = node3.ChildNodes[0].Value;
                                        break;
                                }
                            }
                        }
                        MenuInfo byName = this.menuInfoHelper_0.GetByName(strArray[0]);
                        int num4 = 0;
                        if (byName == null)
                        {
                            byName = new MenuInfo
                            {
                                PARENTIDS = int_1.ToString(),
                                NAME = strArray[0]
                            };
                            if (strArray[1] != null)
                            {
                                byName.CAPTION = strArray[1];
                            }
                            if (strArray[2] != null)
                            {
                                byName.COMPONENTDLLNAME = strArray[2];
                            }
                            if (strArray[3] != null)
                            {
                                byName.CLASSNAME = strArray[3];
                            }
                            if (strArray[4] != null)
                            {
                                byName.SUBTYPE = new int?(Convert.ToInt32(strArray[4]));
                            }
                            if (strArray[5] != null)
                            {
                                if (strArray[5].ToLower() == "true")
                                {
                                    byName.ISPOPMENUITEM = true;
                                }
                                else
                                {
                                    byName.ISPOPMENUITEM = false;
                                }
                            }
                            if (strArray[6] != null)
                            {
                                if (strArray[6].ToLower() == "true")
                                {
                                    byName.BEGINGROUP = true;
                                }
                                else
                                {
                                    byName.BEGINGROUP = false;
                                }
                            }
                            num4 = this.menuInfoHelper_0.Add(byName);
                        }
                        else
                        {
                            byName.PARENTIDS = byName.PARENTIDS + "," + int_1.ToString();
                            num4 = Convert.ToInt32(byName.MenuID);
                            this.menuInfoHelper_0.Update(byName);
                        }
                        if (node != null)
                        {
                            this.method_10(node, num4);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void method_11(XmlNode xmlNode_0)
        {
            try
            {
                MenuInfo info = new MenuInfo();
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    info.PARENTIDS = "0";
                    XmlNode node2 = null;
                    for (int j = 0; j < node.ChildNodes.Count; j++)
                    {
                        int num3 = 0;
                        XmlNode node3 = node.ChildNodes[j];
                        if ((node3.ChildNodes.Count != 0) && (node3.NodeType != XmlNodeType.Comment))
                        {
                            string str = node3.Attributes["name"].Value;
                            if (str != null)
                            {
                                if (str != "Text")
                                {
                                    if (!(str == "BarName"))
                                    {
                                        if (str == "ItemLinks")
                                        {
                                            node2 = node3;
                                            num3++;
                                        }
                                    }
                                    else
                                    {
                                        info.NAME = node3.ChildNodes[0].Value;
                                        num3++;
                                    }
                                }
                                else
                                {
                                    info.CAPTION = node3.ChildNodes[0].Value;
                                    num3++;
                                }
                            }
                            if (num3 == 3)
                            {
                                break;
                            }
                        }
                    }
                    this.int_0 = this.menuInfoHelper_0.Add(info);
                    if (node2 != null)
                    {
                        this.method_10(node2, this.int_0);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void method_2(XmlNode xmlNode_0, XmlDocument xmlDocument_0, int int_1, int int_2, string string_3,
            string string_4)
        {
            xmlNode_0.AppendChild(this.method_1(xmlDocument_0, "DockRow", int_1.ToString()));
            xmlNode_0.AppendChild(this.method_1(xmlDocument_0, "DockCol", int_2.ToString()));
            xmlNode_0.AppendChild(this.method_1(xmlDocument_0, "Text", string_3));
            xmlNode_0.AppendChild(this.method_1(xmlDocument_0, "BarName", string_4));
            xmlNode_0.AppendChild(this.method_1(xmlDocument_0, "Visible", "true"));
            XmlNode newChild = xmlDocument_0.CreateNode(XmlNodeType.Element, "property", "");
            XmlAttribute node = xmlDocument_0.CreateAttribute("name");
            node.Value = "OptionsBar";
            newChild.Attributes.Append(node);
            newChild.AppendChild(this.method_1(xmlDocument_0, "DisableClose", "false"));
            newChild.AppendChild(this.method_1(xmlDocument_0, "AllowDelete", "false"));
            newChild.AppendChild(this.method_1(xmlDocument_0, "RotateWhenVertical", "true"));
            newChild.AppendChild(this.method_1(xmlDocument_0, "MultiLine", "true"));
            newChild.AppendChild(this.method_1(xmlDocument_0, "DrawDragBorder", "true"));
            xmlNode_0.AppendChild(newChild);
        }

        private void method_3(XmlNode xmlNode_0, int int_1, DataTable dataTable_0)
        {
            string[] strArray = new string[10];
            strArray[3] = "true";
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        strArray[0] = attribute.Value;
                        break;

                    case "caption":
                        strArray[1] = attribute.Value;
                        break;

                    case "begingroup":
                        strArray[2] = attribute.Value;
                        break;

                    case "visible":
                        strArray[3] = attribute.Value;
                        break;

                    case "path":
                        strArray[4] = attribute.Value;
                        break;

                    case "classname":
                        strArray[5] = attribute.Value;
                        break;

                    case "subtype":
                        strArray[6] = attribute.Value;
                        break;

                    case "ispopupmenu":
                        strArray[7] = attribute.Value;
                        break;

                    case "progid":
                        strArray[8] = attribute.Value;
                        break;

                    case "shortcut":
                        strArray[9] = attribute.Value;
                        break;
                }
            }
            DataRow row = null;
            DataRow[] rowArray = dataTable_0.Select("Name = '" + strArray[0] + "'");
            int num3 = 0;
            if (rowArray.Length == 0)
            {
                row = dataTable_0.NewRow();
                row["menuid"] = ++this.int_0;
                num3 = this.int_0;
                row["ParentIDs"] = int_1.ToString();
                row["Name"] = strArray[0];
                if (strArray[1] != null)
                {
                    row["Caption"] = strArray[1];
                }
                if (strArray[4] != null)
                {
                    row["ComponentDllName"] = strArray[4];
                }
                if (strArray[5] != null)
                {
                    row["classname"] = strArray[5];
                }
                if (strArray[6] != null)
                {
                    row["subtype"] = strArray[6];
                }
                if (strArray[7] != null)
                {
                    if (strArray[7].ToLower() == "true")
                    {
                        row["IsPopmenuItem"] = 1;
                    }
                    else
                    {
                        row["IsPopmenuItem"] = 0;
                    }
                }
                if (strArray[2] != null)
                {
                    if (strArray[2].ToLower() == "true")
                    {
                        row["BeginGroup"] = 1;
                    }
                    else
                    {
                        row["BeginGroup"] = 0;
                    }
                }
                if (strArray[3] != null)
                {
                    if (strArray[3].ToLower() == "true")
                    {
                        row["visible"] = 1;
                    }
                    else
                    {
                        row["visible"] = 0;
                    }
                }
                if (strArray[8] != null)
                {
                    row["progid"] = strArray[8];
                }
                if (strArray[9] != null)
                {
                    row["shortcut"] = strArray[9];
                }
                dataTable_0.Rows.Add(row);
            }
            else
            {
                row = rowArray[0];
                row["ParentIDs"] = row["ParentIDs"] + "," + int_1.ToString();
                object obj1 = row["menuid"];
                num3 = int.Parse(row["menuid"].ToString());
            }
            for (int j = 0; j < xmlNode_0.ChildNodes.Count; j++)
            {
                XmlNode node = xmlNode_0.ChildNodes[j];
                if (node.Name.ToLower() == "baritem")
                {
                    this.method_3(node, num3, dataTable_0);
                }
            }
        }

        private void method_4(XmlNode xmlNode_0, DataTable dataTable_0)
        {
            DataRow row = dataTable_0.NewRow();
            row["menuid"] = ++this.int_0;
            row["ParentIDs"] = "0";
            int num2 = this.int_0;
            int num3 = 0;
            Label_0041:
            if (num3 >= xmlNode_0.Attributes.Count)
            {
                dataTable_0.Rows.Add(row);
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "baritem")
                    {
                        this.method_3(node, num2, dataTable_0);
                    }
                }
            }
            else
            {
                XmlAttribute attribute = xmlNode_0.Attributes[num3];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        row["Name"] = attribute.Value;
                        break;

                    case "caption":
                        row["Caption"] = attribute.Value;
                        break;

                    case "col":
                        try
                        {
                            row["ItemCol"] = short.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "row":
                        try
                        {
                            row["OrderBy"] = short.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "ismainmenu":
                        try
                        {
                        }
                        catch
                        {
                        }
                        break;

                    case "visible":
                        try
                        {
                            if (bool.Parse(attribute.Value))
                            {
                                row["visible"] = 1;
                            }
                            else
                            {
                                row["visible"] = 0;
                            }
                        }
                        catch
                        {
                        }
                        break;
                }
                num3++;
                goto Label_0041;
            }
        }

        private void method_5(XmlNode xmlNode_0)
        {
            if (xmlNode_0.ChildNodes.Count > 1)
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "baritem")
                    {
                        this.method_5(node);
                    }
                    else
                    {
                        this.method_6(node);
                    }
                }
            }
            else
            {
                this.menuInfoHelper_0.Add(this.method_9(xmlNode_0));
            }
        }

        private void method_6(XmlNode xmlNode_0)
        {
            try
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "baritem")
                    {
                        this.method_5(node);
                    }
                    else
                    {
                        this.method_6(node);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private int method_7(string string_3, ITable itable_0)
        {
            try
            {
                ITableHistogram tableHistogram = new BasicTableHistogram() as ITableHistogram;
                tableHistogram.Table = itable_0;
                tableHistogram.Field = string_3;
                ITableHistogram histogram = tableHistogram as ITableHistogram;
                if ((histogram as IStatisticsResults).Count == 0)
                {
                    return 0;
                }
                return (int) (histogram as IStatisticsResults).Maximum;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return 1;
        }

        private int method_8(string string_3)
        {
            return 1;
        }

        private MenuInfo method_9(XmlNode xmlNode_0)
        {
            MenuInfo info = new MenuInfo();
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        info.NAME = attribute.Value;
                        break;

                    case "caption":
                        info.CAPTION = attribute.Value;
                        break;

                    case "begingroup":
                        info.BEGINGROUP = new bool?(Convert.ToBoolean(attribute.Value));
                        break;

                    case "visible":
                        info.VISIBLE = new bool?(Convert.ToBoolean(attribute.Value));
                        break;

                    case "path":
                        info.COMPONENTDLLNAME = attribute.Value;
                        break;

                    case "classname":
                        info.CLASSNAME = attribute.Value;
                        break;

                    case "subtype":
                        info.SUBTYPE = new int?(Convert.ToInt32(attribute.Value));
                        break;

                    case "ispopupmenu":
                        info.ISPOPMENUITEM = new bool?(Convert.ToBoolean(attribute.Value));
                        break;

                    case "progid":
                        info.PROGID = attribute.Value;
                        break;

                    case "shortcut":
                        info.SHORTCUT = attribute.Value;
                        break;
                }
            }
            return info;
        }

        public void SaveXMLToDB(string string_3, bool bool_0)
        {
            this.int_0 = 0;
            try
            {
                if (bool_0)
                {
                    this.sysGrants_0.DeleteGrant("gisPluge");
                    this.menuInfoHelper_0.ClearAll();
                }
                XmlDocument document = new XmlDocument();
                document.Load(string_3);
                XmlElement documentElement = document.DocumentElement;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    XmlNode node = documentElement.ChildNodes[i];
                    this.method_6(node);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}