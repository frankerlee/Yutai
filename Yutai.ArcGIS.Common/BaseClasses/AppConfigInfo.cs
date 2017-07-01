using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Data;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class AppConfigInfo
    {
        public static string UserID;

        protected static string OLEDBConnection;

        public static string GHXBDBConnection;

        public static string OLEDBServer;

        public static string SDEServer;

        public static string SDEInstance;

        public static string SDEUser;

        public static string SDEPassword;

        public static string SDEVersion;

        public static string SDEDatabase;

        public static string LayerConfig;

        private static string m_LayerConfigDB;

        public static bool IsLogin;

        public static int StyleFileType;

        private static IFeatureWorkspace m_pFeatureWorkspace;

        private static bool m_Init;

        private static IWorkspace m_pWorkspace;

        private static bool m_IsCreate;

        public static string AppName { get; protected set; }

        public static string authentication_mode { get; set; }

        public static string dbclient { get; set; }

        public static DbProviderType DbProviderType { get; set; }

        public static string MenuConfig { get; protected set; }

        static AppConfigInfo()
        {
            AppConfigInfo.old_acctor_mc();
        }

        public AppConfigInfo()
        {
        }

        protected static void CreateGDBConfig(XmlDocument xmlDocument_0, XmlNode xmlNode_0)
        {
            XmlNode xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            XmlAttribute xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "server";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "instance";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "User";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "Password";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "Version";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "Database";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "LayerConfigDB";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
            xmlNodes = xmlDocument_0.CreateNode(XmlNodeType.Element, "add", "");
            xmlAttribute = xmlDocument_0.CreateAttribute("key");
            xmlAttribute.Value = "LayerConfigDBType";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument_0.CreateAttribute("vaule");
            xmlAttribute.Value = "SDE";
            xmlNodes.Attributes.Append(xmlAttribute);
            xmlNode_0.AppendChild(xmlNodes);
        }

        public static IPropertySet CreateSdePropertySet()
        {
            IPropertySet propertySetClass = new PropertySet();
            propertySetClass.SetProperty("DB_CONNECTION_PROPERTIES", AppConfigInfo.SDEServer);
            propertySetClass.SetProperty("DBCLIENT", AppConfigInfo.dbclient);
            if (!string.Equals(AppConfigInfo.authentication_mode, "DBMS", StringComparison.OrdinalIgnoreCase))
            {
                propertySetClass.SetProperty("AUTHENTICATION_MODE", "OSA");
            }
            else
            {
                propertySetClass.SetProperty("AUTHENTICATION_MODE", "DBMS");
                propertySetClass.SetProperty("User", AppConfigInfo.SDEUser);
                propertySetClass.SetProperty("Password", AppConfigInfo.SDEPassword);
            }
            if (AppConfigInfo.SDEDatabase.Length > 0)
            {
                propertySetClass.SetProperty("Database", AppConfigInfo.SDEDatabase);
            }
            return propertySetClass;
        }

        public static IWorkspace GetWorkspace()
        {
            IWorkspaceFactory sdeWorkspaceFactoryClass;
            IWorkspace mPWorkspace;
            Exception exception;
            if (!AppConfigInfo.m_IsCreate)
            {
                AppConfigInfo.m_IsCreate = true;
                string upper = AppConfigInfo.dbclient.ToUpper();
                if (!(upper == "SQLSERVER" ? false : !(upper == "ORACLE")))
                {
                    sdeWorkspaceFactoryClass = new SdeWorkspaceFactory();
                    try
                    {
                        IPropertySet propertySet = AppConfigInfo.CreateSdePropertySet();
                        if (propertySet != null)
                        {
                            AppConfigInfo.m_pWorkspace = sdeWorkspaceFactoryClass.Open(propertySet, 0);
                        }
                        else
                        {
                            mPWorkspace = null;
                            return mPWorkspace;
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Logger.Current.Error("", exception, "");
                    }
                }
                else if (upper != "MDB")
                {
                    try
                    {
                        if ((AppConfigInfo.m_LayerConfigDB.Length <= 0
                            ? false
                            : File.Exists(AppConfigInfo.m_LayerConfigDB)))
                        {
                            sdeWorkspaceFactoryClass = new FileGDBWorkspaceFactory();
                            AppConfigInfo.m_pWorkspace =
                                sdeWorkspaceFactoryClass.OpenFromFile(AppConfigInfo.m_LayerConfigDB, 0);
                        }
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        MessageBox.Show(exception.Message);
                        Logger.Current.Error("", exception, "");
                    }
                }
                else
                {
                    try
                    {
                        if ((AppConfigInfo.m_LayerConfigDB.Length <= 0
                            ? false
                            : File.Exists(AppConfigInfo.m_LayerConfigDB)))
                        {
                            sdeWorkspaceFactoryClass = new AccessWorkspaceFactory();
                            AppConfigInfo.m_pWorkspace =
                                sdeWorkspaceFactoryClass.OpenFromFile(AppConfigInfo.m_LayerConfigDB, 0);
                        }
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        MessageBox.Show(exception.Message);
                        Logger.Current.Error("", exception, "");
                    }
                }
            }
            mPWorkspace = AppConfigInfo.m_pWorkspace;
            return mPWorkspace;
        }

        public static void Init()
        {
            try
            {
                AppConfigInfo.MenuConfig = ConfigurationManager.AppSettings["MenuConfig"].ToString();
            }
            catch (Exception exception)
            {
            }
            try
            {
                AppConfigInfo.DbProviderType =
                    (DbProviderType) Convert.ToInt32(ConfigurationManager.AppSettings["DbProviderType"]);
            }
            catch
            {
            }
            try
            {
                AppConfigInfo.AppName = ConfigurationManager.AppSettings["AppName"].ToString();
            }
            catch
            {
            }
            try
            {
                AppConfigInfo.LayerConfig = ConfigurationManager.AppSettings["LayerConfig"].ToString();
            }
            catch
            {
            }
            try
            {
                AppConfigInfo.StyleFileType = int.Parse(ConfigurationManager.AppSettings["StyleFileType"].ToString());
            }
            catch
            {
            }
            try
            {
                if (ConfigurationManager.AppSettings["IsLogin"].ToString() != "1")
                {
                    AppConfigInfo.IsLogin = false;
                }
                else
                {
                    AppConfigInfo.IsLogin = true;
                }
            }
            catch
            {
                AppConfigInfo.IsLogin = false;
            }
            AppConfigInfo.dbclient = "";
            AppConfigInfo.m_LayerConfigDB = "";
            try
            {
                AppConfigInfo.m_LayerConfigDB = ConfigurationManager.AppSettings["LayerConfigDB"].ToString();
                if (AppConfigInfo.m_LayerConfigDB.Length > 2 && AppConfigInfo.m_LayerConfigDB[1] != ':')
                {
                    AppConfigInfo.m_LayerConfigDB = Path.Combine(Application.StartupPath, AppConfigInfo.m_LayerConfigDB);
                }
            }
            catch
            {
            }
            try
            {
                AppConfigInfo.GHXBDBConnection = ConfigurationManager.AppSettings["GHXBConnection"].ToString();
            }
            catch
            {
            }
            try
            {
                string str = ConfigurationManager.AppSettings["GDBConnection"].ToString();
                char[] chrArray = new char[] {';'};
                string[] strArrays = str.Split(chrArray);
                for (int i = 0; i < (int) strArrays.Length; i++)
                {
                    string str1 = strArrays[i];
                    chrArray = new char[] {'='};
                    string[] strArrays1 = str1.Split(chrArray);
                    string lower = strArrays1[0].ToLower();
                    if (lower != null)
                    {
                        switch (lower)
                        {
                            case "server":
                            {
                                AppConfigInfo.SDEServer = strArrays1[1];
                                break;
                            }
                            case "oledb":
                            {
                                AppConfigInfo.OLEDBServer = strArrays1[1];
                                break;
                            }
                            case "dbclient":
                            {
                                AppConfigInfo.dbclient = strArrays1[1];
                                break;
                            }
                            case "authentication_mode":
                            {
                                AppConfigInfo.authentication_mode = strArrays1[1];
                                break;
                            }
                            case "user":
                            {
                                AppConfigInfo.SDEUser = strArrays1[1];
                                break;
                            }
                            case "password":
                            {
                                AppConfigInfo.SDEPassword = strArrays1[1];
                                break;
                            }
                            case "version":
                            {
                                AppConfigInfo.SDEVersion = strArrays1[1];
                                break;
                            }
                            case "database":
                            {
                                AppConfigInfo.SDEDatabase = strArrays1[1];
                                break;
                            }
                            case "gdbname":
                            {
                                if (AppConfigInfo.m_LayerConfigDB.Length != 0)
                                {
                                    break;
                                }
                                AppConfigInfo.m_LayerConfigDB = strArrays1[1];
                                if (AppConfigInfo.m_LayerConfigDB[1] == ':')
                                {
                                    break;
                                }
                                AppConfigInfo.m_LayerConfigDB = Path.Combine(Application.StartupPath,
                                    AppConfigInfo.m_LayerConfigDB);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
            }
            if (string.IsNullOrEmpty(AppConfigInfo.OLEDBServer))
            {
                AppConfigInfo.OLEDBServer = AppConfigInfo.SDEServer;
            }
            try
            {
                AppConfigInfo.OLEDBConnection = ConfigurationManager.AppSettings["OLEDBConnection"].ToString();
            }
            catch
            {
            }
        }

        private static void old_acctor_mc()
        {
            AppConfigInfo.UserID = "";
            AppConfigInfo.OLEDBConnection = "";
            AppConfigInfo.GHXBDBConnection = "";
            AppConfigInfo.OLEDBServer = "";
            AppConfigInfo.SDEServer = "";
            AppConfigInfo.SDEInstance = "";
            AppConfigInfo.SDEUser = "";
            AppConfigInfo.SDEPassword = "";
            AppConfigInfo.SDEVersion = "";
            AppConfigInfo.SDEDatabase = "";
            AppConfigInfo.LayerConfig = "";
            AppConfigInfo.m_LayerConfigDB = "";
            AppConfigInfo.IsLogin = false;
            AppConfigInfo.StyleFileType = 0;
            AppConfigInfo.m_pFeatureWorkspace = null;
            AppConfigInfo.m_Init = false;
            AppConfigInfo.m_pWorkspace = null;
            AppConfigInfo.m_IsCreate = false;
            AppConfigInfo.Init();
        }

        public static ITable OpenTable(string string_0)
        {
            ITable table;
            string string0;
            string str;
            object obj;
            object obj1;
            try
            {
                if (!AppConfigInfo.m_Init)
                {
                    AppConfigInfo.m_pFeatureWorkspace = AppConfigInfo.GetWorkspace() as IFeatureWorkspace;
                    if (AppConfigInfo.m_pFeatureWorkspace != null)
                    {
                        AppConfigInfo.m_Init = true;
                    }
                }
                if (AppConfigInfo.m_pFeatureWorkspace != null)
                {
                    string0 = string_0;
                    if ((AppConfigInfo.m_pFeatureWorkspace as IWorkspace).Type ==
                        esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        str = "";
                        try
                        {
                            object property =
                                (AppConfigInfo.m_pFeatureWorkspace as IWorkspace).ConnectionProperties.GetProperty(
                                    "VERSION");
                            str = property.ToString();
                            string[] strArrays = str.Split(new char[] {'.'});
                            str = ((int) strArrays.Length <= 1 ? "" : strArrays[0]);
                        }
                        catch (Exception exception)
                        {
                        }
                        if (AppConfigInfo.m_pFeatureWorkspace is ISQLSyntax)
                        {
                            string0 =
                                (AppConfigInfo.m_pFeatureWorkspace as ISQLSyntax).QualifyTableName(
                                    AppConfigInfo.SDEDatabase, str, string_0);
                        }
                    }
                    table = AppConfigInfo.m_pFeatureWorkspace.OpenTable(string0);
                    return table;
                }
                else
                {
                    table = null;
                    return table;
                }
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, "");
            }
            try
            {
                string0 = string_0;
                if ((AppConfigInfo.m_pFeatureWorkspace as IWorkspace).Type ==
                    esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    str = "DBO";
                    if (AppConfigInfo.m_pFeatureWorkspace is ISQLSyntax)
                    {
                        string0 =
                            (AppConfigInfo.m_pFeatureWorkspace as ISQLSyntax).QualifyTableName(
                                AppConfigInfo.SDEDatabase, str, string_0);
                    }
                }
                (AppConfigInfo.m_pFeatureWorkspace as IWorkspace).ConnectionProperties.GetAllProperties(out obj,
                    out obj1);
                table = AppConfigInfo.m_pFeatureWorkspace.OpenTable(string0);
                return table;
            }
            catch (Exception exception2)
            {
                Logger.Current.Error("", exception2, "");
            }
            table = null;
            return table;
        }

        protected static void ResetWorkspace()
        {
            AppConfigInfo.m_pWorkspace = null;
            AppConfigInfo.m_IsCreate = false;
        }
    }
}