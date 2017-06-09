using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateGallery
    {
        private static string authentication_mode;
        private static string dbclient;
        private ITable itable_0 = null;
        private ITable itable_1 = null;
        private ITable itable_2 = null;
        private ITable itable_3 = null;
        [CompilerGenerated]
        private IWorkspace iworkspace_0;
        private List<MapCartoTemplateLib.MapTemplateClass> list_0;
        private static string SDEDatabase;
        private static string SDEInstance;
        private static string SDEPassword;
        private static string SDEServer;
        private static string SDEUser;
        private static string SDEVersion;

        static MapTemplateGallery()
        {
            old_acctor_mc();
        }

        public void AddMapTemplateClass(MapCartoTemplateLib.MapTemplateClass mapTemplateClass_0)
        {
            if (mapTemplateClass_0 != null)
            {
                if (this.list_0 == null)
                {
                    this.list_0 = new List<MapCartoTemplateLib.MapTemplateClass>();
                }
                if (!this.list_0.Contains(mapTemplateClass_0))
                {
                    this.list_0.Add(mapTemplateClass_0);
                }
            }
        }

        public void Init()
        {
            if (this.Workspace == null)
            {
                IWorkspaceFactory factory;
                string str = "";
                try
                {
                    str = ConfigurationManager.AppSettings["CartoTemplateDB"];
                }
                catch
                {
                }
                if (string.IsNullOrEmpty(str))
                {
                    str = ConfigurationManager.AppSettings["GDBConnection"];
                }
                foreach (string str2 in str.Split(new char[] { ';' }))
                {
                    string[] strArray3 = str2.Split(new char[] { '=' });
                    switch (strArray3[0].ToLower())
                    {
                        case "dbclient":
                            dbclient = strArray3[1].ToLower();
                            break;

                        case "server":
                            SDEServer = strArray3[1];
                            break;

                        case "authentication_mode":
                            authentication_mode = strArray3[1].ToLower();
                            break;

                        case "user":
                            SDEUser = strArray3[1];
                            break;

                        case "password":
                            SDEPassword = strArray3[1];
                            break;

                        case "version":
                            SDEVersion = strArray3[1];
                            break;

                        case "database":
                            SDEDatabase = strArray3[1];
                            break;

                        case "gdbname":
                            str = strArray3[1];
                            break;
                    }
                }
                if (dbclient == "mdb")
                {
                    if (str[1] != ':')
                    {
                        str = Path.Combine(Application.StartupPath, str);
                    }
                    factory = new AccessWorkspaceFactoryClass();
                    this.Workspace = factory.OpenFromFile(str, 0);
                }
                else if (dbclient == "gdb")
                {
                    if (str[1] != ':')
                    {
                        str = Path.Combine(Application.StartupPath, str);
                    }
                    factory = new FileGDBWorkspaceFactoryClass();
                    this.Workspace = factory.OpenFromFile(str, 0);
                }
                else
                {
                    IWorkspaceFactory factory2 = new SdeWorkspaceFactoryClass();
                    try
                    {
                        IPropertySet connectionProperties = this.method_0();
                        if (connectionProperties == null)
                        {
                            return;
                        }
                        this.Workspace = factory2.Open(connectionProperties, 0);
                    }
                    catch (Exception exception)
                    {
                        //CErrorLog.writeErrorLog(null, exception, "");
                    }
                }
            }
            if (this.Workspace != null)
            {
                this.itable_0 = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplate");
                this.itable_1 = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplateClass");
                this.itable_2 = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplateElement");
                this.itable_3 = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplateParam");
                this.method_1();
            }
        }

        public bool MapTemplateClassIsExist(string string_0)
        {
            QueryFilterClass class2 = new QueryFilterClass {
                WhereClause = string.Format("Name='{0}'", string_0)
            };
            IQueryFilter queryFilter = class2;
            return (this.MapTemplateClassTable.RowCount(queryFilter) > 0);
        }

        private IPropertySet method_0()
        {
            IPropertySet set = new PropertySetClass();
            if (SDEServer == "")
            {
                return null;
            }
            set.SetProperty("DB_CONNECTION_PROPERTIES", SDEServer);
            if (dbclient == "sqlserver")
            {
                set.SetProperty("DBCLIENT", "sqlserver");
                set.SetProperty("Database", SDEDatabase);
            }
            else
            {
                set.SetProperty("DBCLIENT", "oracle");
            }
            if (authentication_mode == "osa")
            {
                set.SetProperty("AUTHENTICATION_MODE", "OSA");
            }
            else
            {
                set.SetProperty("AUTHENTICATION_MODE", "DBMS");
                set.SetProperty("User", SDEUser);
                set.SetProperty("Password", SDEPassword);
            }
            return set;
        }

        private void method_1()
        {
            ICursor cursor = this.itable_1.Search(null, false);
            IRow o = cursor.NextRow();
            int index = this.itable_1.FindField("Name");
            int num2 = this.itable_1.FindField("Description");
            while (o != null)
            {
                string str = o.get_Value(index).ToString();
                string str2 = o.get_Value(num2).ToString();
                MapCartoTemplateLib.MapTemplateClass class3 = new MapCartoTemplateLib.MapTemplateClass(o.OID, this) {
                    Name = str,
                    Description = str2
                };
                this.AddMapTemplateClass(class3);
                o = cursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(o);
        }

        private static void old_acctor_mc()
        {
            SDEServer = "";
            SDEInstance = "";
            SDEUser = "";
            SDEPassword = "";
            SDEVersion = "";
            SDEDatabase = "";
            dbclient = "";
            authentication_mode = "";
        }

        public void RemoveAllMapTemplateClass()
        {
            if (this.list_0 != null)
            {
                this.list_0.Clear();
            }
        }

        public void RemoveMapTemplateClass(MapCartoTemplateLib.MapTemplateClass mapTemplateClass_0)
        {
            if (((mapTemplateClass_0 != null) && (this.list_0 != null)) && this.list_0.Contains(mapTemplateClass_0))
            {
                this.list_0.Remove(mapTemplateClass_0);
            }
        }

        public string Connection
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<MapCartoTemplateLib.MapTemplateClass> MapTemplateClass
        {
            get
            {
                if (this.list_0 == null)
                {
                    this.list_0 = new List<MapCartoTemplateLib.MapTemplateClass>();
                }
                return this.list_0;
            }
            set
            {
                this.RemoveAllMapTemplateClass();
                if (value != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplateClass class2 in value)
                    {
                        this.AddMapTemplateClass(class2);
                    }
                }
            }
        }

        public ITable MapTemplateClassTable
        {
            get
            {
                return this.itable_1;
            }
            set
            {
                this.itable_1 = value;
            }
        }

        public ITable MapTemplateElementTable
        {
            get
            {
                return this.itable_2;
            }
            set
            {
                this.itable_2 = value;
            }
        }

        public ITable MapTemplateParamTable
        {
            get
            {
                return this.itable_3;
            }
            set
            {
                this.itable_3 = value;
            }
        }

        public ITable MapTemplateTable
        {
            get
            {
                return this.itable_0;
            }
            set
            {
                this.itable_0 = value;
            }
        }

        public IWorkspace Workspace
        {
            [CompilerGenerated]
            get
            {
                return this.iworkspace_0;
            }
            [CompilerGenerated]
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

