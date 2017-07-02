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
using Yutai.Plugins.Services;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateGallery
    {
        private static string authentication_mode;
        private static string dbclient;
        private ITable _templateTable = null;
        private ITable _classTable = null;
        private ITable _elementTable = null;
        private ITable _paramTable = null;

        private List<MapCartoTemplateLib.MapTemplateClass> _lstTemplates;
        private static string SDEDatabase;
        private static string SDEInstance;
        private static string SDEPassword;
        private static string SDEServer;
        private static string SDEUser;
        private static string SDEVersion;

        private string _connectionString;
       

        static MapTemplateGallery()
        {
            instance();
        }

        public void AddMapTemplateClass(MapCartoTemplateLib.MapTemplateClass mapTemplateClass)
        {
            if (mapTemplateClass != null)
            {
                if (this._lstTemplates == null)
                {
                    this._lstTemplates = new List<MapCartoTemplateLib.MapTemplateClass>();
                }
                if (!this._lstTemplates.Contains(mapTemplateClass))
                {
                    this._lstTemplates.Add(mapTemplateClass);
                }
            }
        }

        public void SetWorkspace(string connectionString)
        {
            if (this.Workspace != null)
                this.Workspace = null;
            ConnectWorkspace(connectionString);
        }

        public void Init()
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                ConnectWorkspace(_connectionString);
                return;
            }
            if (this.Workspace == null)
            {
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

                if (string.IsNullOrEmpty(str))
                {
                    str = ConfigurationManager.AppSettings["GDBConnection"];
                }

                if (string.IsNullOrEmpty(str))
                {
                    FileInfo fileInfo=new FileInfo(System.IO.Path.Combine(Application.StartupPath + "\\plugins\\configs\\MapTemplate.mdb"));
                    if (fileInfo.Exists)
                    {
                        _connectionString = BuildConnectionString(fileInfo.FullName);
                        str = _connectionString;
                    }
                   
                }


                if (string.IsNullOrEmpty(str))
                {

                    MessageService.Current.Warn("系统没有找到关于模板的配置，请手动选择模板数据库!");
                    return;
                }
                ConnectWorkspace(str);
            }
        }

        private string BuildConnectionString(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string ext = fileInfo.Extension.Substring(1);
            return string.Format("dbclient={0};gdbname={1}", ext, fileName);
        }
        private void ConnectWorkspace(string connectionString)
        {
            string str = "";
            IWorkspaceFactory factory;
            foreach (string str2 in connectionString.Split(new char[] {';'}))
            {
                string[] strArray3 = str2.Split(new char[] {'='});
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
                factory = new AccessWorkspaceFactory();
                this.Workspace = factory.OpenFromFile(str, 0);
            }
            else if (dbclient == "gdb")
            {
                if (str[1] != ':')
                {
                    str = Path.Combine(Application.StartupPath, str);
                }
                factory = new FileGDBWorkspaceFactory();
                this.Workspace = factory.OpenFromFile(str, 0);
            }
            else if (dbclient == "sde")
            {
                if (str[1] != ':')
                {
                    str = Path.Combine(Application.StartupPath, str);
                }
                factory = new SdeWorkspaceFactory();
                this.Workspace = factory.OpenFromFile(str, 0);
            }
            else
            {
                IWorkspaceFactory factory2 = new SdeWorkspaceFactory();
                try
                {
                    IPropertySet connectionProperties = this.CreateConnectionProperty();
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

            if (this.Workspace != null)
            {
                this._templateTable = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplate");
                this._classTable = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplateClass");
                this._elementTable = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplateElement");
                this._paramTable = (this.Workspace as IFeatureWorkspace).OpenTable("MapTemplateParam");
                this.ReadClasses();
                this._connectionString = connectionString;
            }
        }

        public bool MapTemplateClassIsExist(string className)
        {
            QueryFilterClass class2 = new QueryFilterClass
            {
                WhereClause = string.Format("Name='{0}'", className)
            };

            IQueryFilter queryFilter = class2;
            return (this.MapTemplateClassTable.RowCount(queryFilter) > 0);
        }

        private IPropertySet CreateConnectionProperty()
        {
            IPropertySet set = new PropertySetClass();
            if (SDEServer== "")
            {
                return null;
            }
            set.SetProperty("DB_CONNECTION_PROPERTIES",SDEServer);
            if (dbclient== "sqlserver")
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
                set.SetProperty("User",SDEUser);
                set.SetProperty("Password",SDEPassword);
            }
            return
                set;
        }

        private void ReadClasses()
        {
            ICursor cursor = this._classTable.Search(null, false);
            IRow row = cursor.NextRow();
            int index = this._classTable.FindField("Name");

            int num2 = this._classTable.FindField("Description");
            while (row!= null)
            {
                string str = row.get_Value(index).ToString();
                string str2 = row.get_Value(num2).ToString();
                MapCartoTemplateLib.MapTemplateClass class3 = new MapCartoTemplateLib.MapTemplateClass(row.OID, this)
                {
                    Name = str,
                    Description = str2
                };
                this.AddMapTemplateClass(class3);
                row = cursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(row);
        }

        private static void instance()
        {
            SDEServer= "";
            SDEInstance= "";
            SDEUser= "";
            SDEPassword= "";
            SDEVersion= "";
            SDEDatabase= "";
            dbclient= "";
            authentication_mode= "";
        }

        public void RemoveAllMapTemplateClass()
        {
            if (this._lstTemplates != null)
            {
                this._lstTemplates.Clear();
            }
        }

        public void RemoveMapTemplateClass(MapCartoTemplateLib.MapTemplateClass templateClass)
        {
            if (((templateClass != null) && (this._lstTemplates != null)) && this._lstTemplates.Contains(templateClass))
            {
                this._lstTemplates.Remove(templateClass);
            }
        }

        public string Connection
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public List<MapCartoTemplateLib.MapTemplateClass> MapTemplateClass
        {
            get
            {
                if (this._lstTemplates == null)
                {
                    this._lstTemplates = new List<MapCartoTemplateLib.MapTemplateClass>();
                }
                return this._lstTemplates;
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
            get { return this._classTable; }
            set { this._classTable = value; }
        }

        public ITable MapTemplateElementTable
        {
            get { return this._elementTable; }
            set { this._elementTable = value; }
        }

        public ITable MapTemplateParamTable
        {
            get { return this._paramTable; }
            set { this._paramTable = value; }
        }

        public ITable MapTemplateTable
        {
            get { return this._templateTable; }
            set { this._templateTable = value; }
        }

        public IWorkspace Workspace { get; set; }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(_connectionString) || this.Workspace == null) return false;
            return true;
        }
    }
}