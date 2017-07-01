using System;
using System.Drawing;
using System.IO;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Catalog
{
    public class GxRemoteDatabaseFolder : IGxObject, IGxObjectContainer, IGxRemoteDatabaseFolder, IGxObjectEdit,
        IGxObjectProperties, IGxObjectUI, IGxPasteTarget
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

        private string string_0 = (Environment.SystemDirectory.Substring(0, 2) +
                                   @"Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\");

        public GxRemoteDatabaseFolder()
        {
            string str = "";
                //= RegistryTools.GetRegistryKey("HKEY_CURRENT_USER", @"Software\ESRI\Desktop10.2\CoreRuntime\Locator\Settings", "LocatorDirectory");
            if (!string.IsNullOrEmpty(str) &&
                ((str.IndexOf(@"Locators\", StringComparison.OrdinalIgnoreCase) > 0) &&
                 (str.IndexOf("ArcCatalog", StringComparison.OrdinalIgnoreCase) == -1)))
            {
                this.string_0 = str.Replace("Locators", "ArcCatalog");
            }
            if (!Directory.Exists(this.string_0))
            {
                try
                {
                    Directory.CreateDirectory(this.string_0);
                }
                catch
                {
                }
            }
        }

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            return igxObject_1;
        }

        public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_1;
            this.igxCatalog_0 = igxCatalog_1;
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).AddChild(this);
            }
            if (this.igxObjectArray_0.Count == 0)
            {
                this.method_0();
            }
        }

        public bool CanCopy()
        {
            return false;
        }

        public bool CanDelete()
        {
            return false;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public bool CanRename()
        {
            return false;
        }

        public void Delete()
        {
        }

        public void DeleteChild(IGxObject igxObject_1)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_1)
                {
                    this.igxObjectArray_0.Remove(i);
                    break;
                }
            }
        }

        public void Detach()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxCatalog_0.ObjectDeleted(this);
            }
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
            }
            this.igxObject_0 = null;
            this.igxCatalog_0 = null;
        }

        public void EditProperties(int int_0)
        {
        }

        public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        private void method_0()
        {
            this.igxObjectArray_0.Empty();
            IGxObject obj2 = new GxNewDatabase();
            IWorkspaceFactory factory = new OLEDBWorkspaceFactoryClass();
            (obj2 as IGxNewDatabase).WorkspaceFactory = factory;
            obj2.Attach(this, this.igxCatalog_0);
            obj2 = new GxNewDatabase();
            factory = new SdeWorkspaceFactoryClass();
            (obj2 as IGxNewDatabase).WorkspaceFactory = factory;
            obj2.Attach(this, this.igxCatalog_0);
            if (Directory.Exists(this.string_0))
            {
                IWorkspaceName name;
                foreach (string str in Directory.GetFiles(this.string_0, "*.sde"))
                {
                    obj2 = new GxDatabase();
                    name = new WorkspaceNameClass
                    {
                        WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory",
                        PathName = str
                    };
                    (obj2 as IGxDatabase).WorkspaceName = name;
                    obj2.Attach(this, this.igxCatalog_0);
                }
                foreach (string str in Directory.GetFiles(this.string_0, "*.odc"))
                {
                    obj2 = new GxDatabase();
                    name = new WorkspaceNameClass
                    {
                        WorkspaceFactoryProgID = "esriDataSourcesOleDB.OLEDBWorkspaceFactory",
                        PathName = str
                    };
                    (obj2 as IGxDatabase).WorkspaceName = name;
                    obj2.Attach(this, this.igxCatalog_0);
                }
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public void Refresh()
        {
            this.igxObjectArray_0.Empty();
            this.method_0();
        }

        public void Rename(string string_1)
        {
        }

        public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
        {
        }

        public void SetProperty(string string_1, object object_0)
        {
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public bool AreChildrenViewable
        {
            get { return true; }
        }

        public string BaseName
        {
            get { return "数据库连接"; }
        }

        public string Category
        {
            get { return "数据库连接文件夹"; }
        }

        public IEnumGxObject Children
        {
            get { return (this.igxObjectArray_0 as IEnumGxObject); }
        }

        public UID ClassID
        {
            get { return null; }
        }

        public UID ContextMenu
        {
            get { return null; }
        }

        public string FullName
        {
            get { return "数据库连接"; }
        }

        public bool HasChildren
        {
            get { return true; }
        }

        public IName InternalObjectName
        {
            get { return null; }
        }

        public bool IsValid
        {
            get { return false; }
        }

        public Bitmap LargeImage
        {
            get { return ImageLib.GetSmallImage(2); }
        }

        public Bitmap LargeSelectedImage
        {
            get { return ImageLib.GetSmallImage(2); }
        }

        public string Name
        {
            get { return "数据库连接"; }
        }

        public UID NewMenu
        {
            get { return null; }
        }

        public IGxObject Parent
        {
            get { return this.igxObject_0; }
        }

        public string Path
        {
            get { return this.string_0; }
            set { }
        }

        public int PropertyCount
        {
            get { return 0; }
        }

        public Bitmap SmallImage
        {
            get { return ImageLib.GetSmallImage(2); }
        }

        public Bitmap SmallSelectedImage
        {
            get { return ImageLib.GetSmallImage(2); }
        }
    }
}