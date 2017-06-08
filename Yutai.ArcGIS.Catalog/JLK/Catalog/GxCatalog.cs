namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using JLK.Framework;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class GxCatalog : IGxObject, IGxObjectContainer, IGxCatalog, IGxFile, IGxObjectEdit, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxObjectFactories, IGxPasteTargetHelper, IGxCatalogEvents
    {
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IGxSelection igxSelection_0 = new GxSelection();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;

        public event OnObjectAddedEventHandler OnObjectAdded;

        public event OnObjectChangedEventHandler OnObjectChanged;

        public event OnObjectDeletedEventHandler OnObjectDeleted;

        public event OnObjectRefreshedEventHandler OnObjectRefreshed;

        public event OnRefreshAllEventHandler OnRefreshAll;

        public GxCatalog()
        {
            ImageLib.Init();
            this.Open();
            this.igxSelection_0.SetLocation(this, null);
        }

        public IGxObject AddChild(IGxObject igxObject_0)
        {
            bool flag = igxObject_0 is IGxDiskConnection;
            string strB = igxObject_0.Name.ToUpper();
            if (flag && (strB[0] == '\\'))
            {
                this.igxObjectArray_0.Insert(-1, igxObject_0);
                return igxObject_0;
            }
            int num = 0;
            if (flag)
            {
                for (int i = 0; i < this.igxObjectArray_0.Count; i++)
                {
                    IGxObject obj3 = this.igxObjectArray_0.Item(i);
                    if (!(obj3 is IGxDiskConnection))
                    {
                        this.igxObjectArray_0.Insert(i, igxObject_0);
                        return igxObject_0;
                    }
                    num = obj3.Name.ToUpper().CompareTo(strB);
                    if (num > 0)
                    {
                        this.igxObjectArray_0.Insert(i, igxObject_0);
                        return igxObject_0;
                    }
                    if (num == 0)
                    {
                        return obj3;
                    }
                }
            }
            this.igxObjectArray_0.Insert(-1, igxObject_0);
            return igxObject_0;
        }

        public void Attach(IGxObject igxObject_0, IGxCatalog igxCatalog_0)
        {
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
            ienumName_0.Reset();
            IName name = ienumName_0.Next();
            while (name != null)
            {
                if ((name is IFileName) && Directory.Exists((name as IFileName).Path))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanRename()
        {
            return false;
        }

        public void Close()
        {
            this.igxObjectArray_0.Empty();
        }

        public IGxFolder ConnectFolder(string string_0)
        {
            IGxObject obj2;
            string str = string_0.ToUpper();
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                obj2 = this.igxObjectArray_0.Item(i);
                if ((obj2 is IGxDiskConnection) && (obj2.Name.ToUpper() == str))
                {
                    return (obj2 as IGxFolder);
                }
            }
            obj2 = new GxDiskConnection();
            (obj2 as IGxFile).Path = string_0;
            obj2.Attach(this, this);
            this.ObjectAdded(obj2);
            XmlDocument document = new XmlDocument();
            string path = Application.StartupPath + @"\CatalogConfig.xml";
            if (File.Exists(path))
            {
                document.Load(path);
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<?xml version='1.0' encoding='utf-8' ?>");
                builder.Append("<Links>");
                builder.Append("</Links>");
                document.LoadXml(builder.ToString());
            }
            XmlElement documentElement = document.DocumentElement;
            XmlNode newChild = document.CreateNode(XmlNodeType.Element, "Link", "");
            documentElement.AppendChild(newChild);
            XmlAttribute node = document.CreateAttribute("Folder");
            node.Value = string_0;
            newChild.Attributes.Append(node);
            document.Save(path);
            return (obj2 as IGxFolder);
        }

        public string ConstructFullName(IGxObject igxObject_0)
        {
            return null;
        }

        public void Delete()
        {
        }

        public void DeleteChild(IGxObject igxObject_0)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_0)
                {
                    this.igxObjectArray_0.Remove(i);
                    break;
                }
            }
        }

        public void Detach()
        {
        }

        public void DisconnectFolder(string string_0)
        {
        }

        public void Edit()
        {
        }

        public void EditProperties(int int_0)
        {
        }

        public void EnableGxObjectFactory(int int_0, bool bool_0)
        {
        }

        ~GxCatalog()
        {
        }

        public object GetObjectFromFullName(string string_0, out int int_0)
        {
            int_0 = 0;
            IGxObject obj2 = this.method_1(string_0, this.igxObjectArray_0 as IEnumGxObject);
            if (obj2 != null)
            {
                int_0 = 1;
            }
            return obj2;
        }

        public void Init(object object_0)
        {
            this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
            this.ipopuMenuWrap_0.Clear();
            this.ipopuMenuWrap_0.AddItem("RefreshItem", false);
        }

        void IGxFile.Close(bool bool_0)
        {
        }

        bool IGxPasteTargetHelper.CanPaste(IName iname_0, IGxObject igxObject_0, out bool bool_0)
        {
            bool_0 = false;
            return false;
        }

        bool IGxPasteTargetHelper.Paste(IName iname_0, IGxObject igxObject_0, out bool bool_0)
        {
            bool_0 = false;
            return false;
        }

        private void method_0(string string_0)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(string_0);
                XmlElement documentElement = document.DocumentElement;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    XmlNode node = documentElement.ChildNodes[i];
                    string str = node.Attributes["Folder"].Value;
                    IGxObject obj2 = new GxDiskConnection();
                    (obj2 as IGxFile).Path = str;
                    obj2.Attach(this, this);
                }
                document = null;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private IGxObject method_1(string string_0, IEnumGxObject ienumGxObject_0)
        {
            ienumGxObject_0.Reset();
            for (IGxObject obj2 = ienumGxObject_0.Next(); obj2 != null; obj2 = ienumGxObject_0.Next())
            {
                if (obj2.FullName == string_0)
                {
                    return obj2;
                }
                if (obj2 is IGxObjectContainer)
                {
                    obj2 = this.method_1(string_0, (obj2 as IGxObjectContainer).Children);
                    if (obj2 != null)
                    {
                        return obj2;
                    }
                }
            }
            return null;
        }

        public void New()
        {
        }

        public void ObjectAdded(IGxObject igxObject_0)
        {
            if (this.onObjectAddedEventHandler_0 != null)
            {
                this.onObjectAddedEventHandler_0(igxObject_0);
            }
        }

        public void ObjectChanged(IGxObject igxObject_0)
        {
            if (this.onObjectChangedEventHandler_0 != null)
            {
                this.onObjectChangedEventHandler_0(igxObject_0);
            }
        }

        public void ObjectDeleted(IGxObject igxObject_0)
        {
            if (this.onObjectDeletedEventHandler_0 != null)
            {
                this.onObjectDeletedEventHandler_0(igxObject_0);
            }
        }

        public void ObjectRefreshed(IGxObject igxObject_0)
        {
            if (this.onObjectRefreshedEventHandler_0 != null)
            {
                this.onObjectRefreshedEventHandler_0(igxObject_0);
            }
        }

        public void Open()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                IGxObject obj2;
                string[] logicalDrives = Directory.GetLogicalDrives();
                int index = 0;
                while (true)
                {
                    if (index >= logicalDrives.Length)
                    {
                        break;
                    }
                    try
                    {
                        if ((logicalDrives[index].ToUpper() != @"A:\") && (logicalDrives[index].ToUpper() != @"B:\"))
                        {
                            obj2 = new GxDiskConnection();
                            (obj2 as IGxFile).Path = logicalDrives[index];
                            obj2.Attach(this, this);
                        }
                    }
                    catch
                    {
                    }
                    index++;
                }
                string path = Application.StartupPath + @"\CatalogConfig.xml";
                if (File.Exists(path))
                {
                    this.method_0(path);
                }
                obj2 = new GxRemoteDatabaseFolder();
                obj2.Attach(this, this);
                obj2 = new GxDatabaseServerFolder();
                obj2.Attach(this, this);
                obj2 = new GxGISServersFolder();
                obj2.Attach(this, this);
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            ienumName_0.Reset();
            IName name = ienumName_0.Next();
            while (name != null)
            {
                if ((name is IFileName) && Directory.Exists((name as IFileName).Path))
                {
                    IGxObject obj2 = new GxDiskConnection();
                    (obj2 as IGxFile).Path = (name as IFileName).Path;
                    obj2.Attach(this, this);
                    this.ObjectAdded(obj2);
                }
            }
            return false;
        }

        public void Refresh()
        {
            this.igxObjectArray_0.Empty();
            this.Open();
            this.ObjectRefreshed(this);
        }

        public void Rename(string string_0)
        {
        }

        public void Save()
        {
        }

        public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_1)
        {
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public bool AreChildrenViewable
        {
            get
            {
                return true;
            }
        }

        public string BaseName
        {
            get
            {
                return "目录";
            }
        }

        public string Category
        {
            get
            {
                return "目录";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                return (this.igxObjectArray_0 as IEnumGxObject);
            }
        }

        public UID ClassID
        {
            get
            {
                return null;
            }
        }

        public UID ContextMenu
        {
            get
            {
                return null;
            }
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public IEnumGxObjectFactory EnabledGxObjectFactories
        {
            get
            {
                return null;
            }
        }

        public IGxFileFilter FileFilter
        {
            get
            {
                return null;
            }
        }

        public string FullName
        {
            get
            {
                return "目录";
            }
        }

        public IGxObjectFactory this[int int_0]
        {
            get
            {
                return null;
            }
        }

        public UID this[int int_0]
        {
            get
            {
                return null;
            }
        }

        public bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public IName InternalObjectName
        {
            get
            {
                return null;
            }
        }

        public bool this[int int_0]
        {
            get
            {
                return false;
            }
        }

        public bool IsValid
        {
            get
            {
                return false;
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return ImageLib.GetSmallImage(0);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0);
            }
        }

        public string Location
        {
            set
            {
            }
        }

        public string Name
        {
            get
            {
                return "目录";
            }
        }

        public UID NewMenu
        {
            get
            {
                return null;
            }
        }

        public IGxObject Parent
        {
            get
            {
                return null;
            }
        }

        public string Path
        {
            get
            {
                return "目录";
            }
            set
            {
            }
        }

        public IGxObject SelectedObject
        {
            get
            {
                return this.igxSelection_0.FirstObject;
            }
        }

        public IGxSelection Selection
        {
            get
            {
                return this.igxSelection_0;
            }
        }

        public Bitmap SmallImage
        {
            get
            {
                return ImageLib.GetSmallImage(0);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0);
            }
        }
    }
}

