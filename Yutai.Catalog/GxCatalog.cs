using ESRI.ArcGIS.esriSystem;

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Yutai.Catalog;

namespace Yutai.Catalog
{
    public class GxCatalog : IGxObject, IGxObjectContainer, IGxCatalog, IGxFile, IGxObjectEdit, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxObjectFactories, IGxPasteTargetHelper, IGxCatalogEvents
    {
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

        private IGxSelection igxSelection_0 = new GxSelection();

        private OnObjectAddedEventHandler onObjectAddedEventHandler_0;

        private OnObjectChangedEventHandler onObjectChangedEventHandler_0;

        private OnObjectDeletedEventHandler onObjectDeletedEventHandler_0;

        private OnObjectRefreshedEventHandler onObjectRefreshedEventHandler_0;

        private OnRefreshAllEventHandler onRefreshAllEventHandler_0;

        private IPopuMenuWrap ipopuMenuWrap_0 = null;

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
                return this.igxObjectArray_0 as IEnumGxObject;
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

        public IGxObjectFactory GxObjectFactory
        {
            get
            {
                return null;
            }
        }

        public UID GxObjectFactoryCLSID
        {
            get
            {
                return null;
            }
        }

        //public IGxObjectFactory GxObjectFactory(int int_0)
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        //public UID GxObjectFactoryCLSID(int int_0)
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

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

        public bool IsEnabled
        {
            get
            {
                return false;
            }
        }
        //public bool IsEnabled(int int_0)
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

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

        public GxCatalog()
        {
            ImageLib.Init();
            this.Open();
            this.igxSelection_0.SetLocation(this, null);
        }

        public IGxObject AddChild(IGxObject igxObject_0)
        {
            IGxObject igxObject0;
            bool flag = igxObject_0 is IGxDiskConnection;
            string upper = igxObject_0.Name.ToUpper();
            if (!flag || upper[0] != '\\')
            {
                int num = 0;
                if (flag)
                {
                    int num1 = 0;
                    while (num1 < this.igxObjectArray_0.Count)
                    {
                        IGxObject gxObject = this.igxObjectArray_0.Item(num1);
                        if (!(gxObject is IGxDiskConnection))
                        {
                            this.igxObjectArray_0.Insert(num1, igxObject_0);
                            igxObject0 = igxObject_0;
                            return igxObject0;
                        }
                        else
                        {
                            num = gxObject.Name.ToUpper().CompareTo(upper);
                            if (num > 0)
                            {
                                this.igxObjectArray_0.Insert(num1, igxObject_0);
                                igxObject0 = igxObject_0;
                                return igxObject0;
                            }
                            else if (num == 0)
                            {
                                igxObject0 = gxObject;
                                return igxObject0;
                            }
                            else
                            {
                                num1++;
                            }
                        }
                    }
                }
                this.igxObjectArray_0.Insert(-1, igxObject_0);
                igxObject0 = igxObject_0;
            }
            else
            {
                this.igxObjectArray_0.Insert(-1, igxObject_0);
                igxObject0 = igxObject_0;
            }
            return igxObject0;
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
            bool flag;
            ienumName_0.Reset();
            IName name = ienumName_0.Next();
            while (true)
            {
                if (name == null)
                {
                    flag = false;
                    break;
                }
                else if (name is IFileName && Directory.Exists((name as IFileName).Path))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
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
            IGxObject gxDiskConnection;
            IGxFolder gxFolder;
            string upper = string_0.ToUpper();
            int num = 0;
            while (true)
            {
                if (num < this.igxObjectArray_0.Count)
                {
                    gxDiskConnection = this.igxObjectArray_0.Item(num);
                    if (!(gxDiskConnection is IGxDiskConnection) || !(gxDiskConnection.Name.ToUpper() == upper))
                    {
                        num++;
                    }
                    else
                    {
                        gxFolder = gxDiskConnection as IGxFolder;
                        break;
                    }
                }
                else
                {
                    gxDiskConnection = new GxDiskConnection();
                    (gxDiskConnection as IGxFile).Path = string_0;
                    gxDiskConnection.Attach(this, this);
                    this.ObjectAdded(gxDiskConnection);
                    XmlDocument xmlDocument = new XmlDocument();
                    string str = string.Concat(Application.StartupPath, "\\CatalogConfig.xml");
                    if (!File.Exists(str))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("<?xml version='1.0' encoding='utf-8' ?>");
                        stringBuilder.Append("<Links>");
                        stringBuilder.Append("</Links>");
                        xmlDocument.LoadXml(stringBuilder.ToString());
                    }
                    else
                    {
                        xmlDocument.Load(str);
                    }
                    XmlElement documentElement = xmlDocument.DocumentElement;
                    XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "Link", "");
                    documentElement.AppendChild(xmlNodes);
                    XmlAttribute string0 = xmlDocument.CreateAttribute("Folder");
                    string0.Value = string_0;
                    xmlNodes.Attributes.Append(string0);
                    xmlDocument.Save(str);
                    gxFolder = gxDiskConnection as IGxFolder;
                    break;
                }
            }
            return gxFolder;
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
            int num = 0;
            while (true)
            {
                if (num >= this.igxObjectArray_0.Count)
                {
                    break;
                }
                else if (this.igxObjectArray_0.Item(num) == igxObject_0)
                {
                    this.igxObjectArray_0.Remove(num);
                    break;
                }
                else
                {
                    num++;
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
            IGxObject gxObject = this.method_1(string_0, this.igxObjectArray_0 as IEnumGxObject);
            if (gxObject != null)
            {
                int_0 = 1;
            }
            return gxObject;
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
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(string_0);
                XmlElement documentElement = xmlDocument.DocumentElement;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    XmlNode itemOf = documentElement.ChildNodes[i];
                    string value = itemOf.Attributes["Folder"].Value;
                    IGxObject gxDiskConnection = new GxDiskConnection();
                    (gxDiskConnection as IGxFile).Path = value;
                    gxDiskConnection.Attach(this, this);
                }
                xmlDocument = null;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private IGxObject method_1(string string_0, IEnumGxObject ienumGxObject_0)
        {
            IGxObject gxObject;
            ienumGxObject_0.Reset();
            IGxObject gxObject1 = ienumGxObject_0.Next();
            while (true)
            {
                if (gxObject1 == null)
                {
                    gxObject = null;
                    break;
                }
                else if (gxObject1.FullName == string_0)
                {
                    gxObject = gxObject1;
                    break;
                }
                else
                {
                    if (gxObject1 is IGxObjectContainer)
                    {
                        gxObject1 = this.method_1(string_0, (gxObject1 as IGxObjectContainer).Children);
                        if (gxObject1 != null)
                        {
                            gxObject = gxObject1;
                            break;
                        }
                    }
                    gxObject1 = ienumGxObject_0.Next();
                }
            }
            return gxObject;
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
            IGxObject gxDiskConnection;
           
                string[] logicalDrives = Directory.GetLogicalDrives();
                for (int i = 0; i < (int)logicalDrives.Length; i++)
                {
                    try
                    {
                        if ((logicalDrives[i].ToUpper() == "A:\\" ? false : !(logicalDrives[i].ToUpper() == "B:\\")))
                        {
                            gxDiskConnection = new GxDiskConnection();
                            (gxDiskConnection as IGxFile).Path = logicalDrives[i];
                            gxDiskConnection.Attach(this, this);
                        }
                    }
                    catch
                    {
                    }
                }
                string str = string.Concat(Application.StartupPath, "\\CatalogConfig.xml");
                if (File.Exists(str))
                {
                    this.method_0(str);
                }
                gxDiskConnection = new GxRemoteDatabaseFolder();
                gxDiskConnection.Attach(this, this);
                gxDiskConnection = new GxDatabaseServerFolder();
                gxDiskConnection.Attach(this, this);
                gxDiskConnection = new GxGISServersFolder();
                gxDiskConnection.Attach(this, this);
            }
       

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            ienumName_0.Reset();
            IName name = ienumName_0.Next();
            while (name != null)
            {
                if (!(name is IFileName) || !Directory.Exists((name as IFileName).Path))
                {
                    continue;
                }
                IGxObject gxDiskConnection = new GxDiskConnection();
                (gxDiskConnection as IGxFile).Path = (name as IFileName).Path;
                gxDiskConnection.Attach(this, this);
                this.ObjectAdded(gxDiskConnection);
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

        public event OnObjectAddedEventHandler OnObjectAdded
        {
            add
            {
                OnObjectAddedEventHandler onObjectAddedEventHandler;
                OnObjectAddedEventHandler onObjectAddedEventHandler0 = this.onObjectAddedEventHandler_0;
                do
                {
                    onObjectAddedEventHandler = onObjectAddedEventHandler0;
                    OnObjectAddedEventHandler onObjectAddedEventHandler1 = (OnObjectAddedEventHandler)Delegate.Combine(onObjectAddedEventHandler, value);
                    onObjectAddedEventHandler0 = Interlocked.CompareExchange<OnObjectAddedEventHandler>(ref this.onObjectAddedEventHandler_0, onObjectAddedEventHandler1, onObjectAddedEventHandler);
                }
                while ((object)onObjectAddedEventHandler0 != (object)onObjectAddedEventHandler);
            }
            remove
            {
                OnObjectAddedEventHandler onObjectAddedEventHandler;
                OnObjectAddedEventHandler onObjectAddedEventHandler0 = this.onObjectAddedEventHandler_0;
                do
                {
                    onObjectAddedEventHandler = onObjectAddedEventHandler0;
                    OnObjectAddedEventHandler onObjectAddedEventHandler1 = (OnObjectAddedEventHandler)Delegate.Remove(onObjectAddedEventHandler, value);
                    onObjectAddedEventHandler0 = Interlocked.CompareExchange<OnObjectAddedEventHandler>(ref this.onObjectAddedEventHandler_0, onObjectAddedEventHandler1, onObjectAddedEventHandler);
                }
                while ((object)onObjectAddedEventHandler0 != (object)onObjectAddedEventHandler);
            }
        }

        public event OnObjectChangedEventHandler OnObjectChanged
        {
            add
            {
                OnObjectChangedEventHandler onObjectChangedEventHandler;
                OnObjectChangedEventHandler onObjectChangedEventHandler0 = this.onObjectChangedEventHandler_0;
                do
                {
                    onObjectChangedEventHandler = onObjectChangedEventHandler0;
                    OnObjectChangedEventHandler onObjectChangedEventHandler1 = (OnObjectChangedEventHandler)Delegate.Combine(onObjectChangedEventHandler, value);
                    onObjectChangedEventHandler0 = Interlocked.CompareExchange<OnObjectChangedEventHandler>(ref this.onObjectChangedEventHandler_0, onObjectChangedEventHandler1, onObjectChangedEventHandler);
                }
                while ((object)onObjectChangedEventHandler0 != (object)onObjectChangedEventHandler);
            }
            remove
            {
                OnObjectChangedEventHandler onObjectChangedEventHandler;
                OnObjectChangedEventHandler onObjectChangedEventHandler0 = this.onObjectChangedEventHandler_0;
                do
                {
                    onObjectChangedEventHandler = onObjectChangedEventHandler0;
                    OnObjectChangedEventHandler onObjectChangedEventHandler1 = (OnObjectChangedEventHandler)Delegate.Remove(onObjectChangedEventHandler, value);
                    onObjectChangedEventHandler0 = Interlocked.CompareExchange<OnObjectChangedEventHandler>(ref this.onObjectChangedEventHandler_0, onObjectChangedEventHandler1, onObjectChangedEventHandler);
                }
                while ((object)onObjectChangedEventHandler0 != (object)onObjectChangedEventHandler);
            }
        }

        public event OnObjectDeletedEventHandler OnObjectDeleted
        {
            add
            {
                OnObjectDeletedEventHandler onObjectDeletedEventHandler;
                OnObjectDeletedEventHandler onObjectDeletedEventHandler0 = this.onObjectDeletedEventHandler_0;
                do
                {
                    onObjectDeletedEventHandler = onObjectDeletedEventHandler0;
                    OnObjectDeletedEventHandler onObjectDeletedEventHandler1 = (OnObjectDeletedEventHandler)Delegate.Combine(onObjectDeletedEventHandler, value);
                    onObjectDeletedEventHandler0 = Interlocked.CompareExchange<OnObjectDeletedEventHandler>(ref this.onObjectDeletedEventHandler_0, onObjectDeletedEventHandler1, onObjectDeletedEventHandler);
                }
                while ((object)onObjectDeletedEventHandler0 != (object)onObjectDeletedEventHandler);
            }
            remove
            {
                OnObjectDeletedEventHandler onObjectDeletedEventHandler;
                OnObjectDeletedEventHandler onObjectDeletedEventHandler0 = this.onObjectDeletedEventHandler_0;
                do
                {
                    onObjectDeletedEventHandler = onObjectDeletedEventHandler0;
                    OnObjectDeletedEventHandler onObjectDeletedEventHandler1 = (OnObjectDeletedEventHandler)Delegate.Remove(onObjectDeletedEventHandler, value);
                    onObjectDeletedEventHandler0 = Interlocked.CompareExchange<OnObjectDeletedEventHandler>(ref this.onObjectDeletedEventHandler_0, onObjectDeletedEventHandler1, onObjectDeletedEventHandler);
                }
                while ((object)onObjectDeletedEventHandler0 != (object)onObjectDeletedEventHandler);
            }
        }

        public event OnObjectRefreshedEventHandler OnObjectRefreshed
        {
            add
            {
                OnObjectRefreshedEventHandler onObjectRefreshedEventHandler;
                OnObjectRefreshedEventHandler onObjectRefreshedEventHandler0 = this.onObjectRefreshedEventHandler_0;
                do
                {
                    onObjectRefreshedEventHandler = onObjectRefreshedEventHandler0;
                    OnObjectRefreshedEventHandler onObjectRefreshedEventHandler1 = (OnObjectRefreshedEventHandler)Delegate.Combine(onObjectRefreshedEventHandler, value);
                    onObjectRefreshedEventHandler0 = Interlocked.CompareExchange<OnObjectRefreshedEventHandler>(ref this.onObjectRefreshedEventHandler_0, onObjectRefreshedEventHandler1, onObjectRefreshedEventHandler);
                }
                while ((object)onObjectRefreshedEventHandler0 != (object)onObjectRefreshedEventHandler);
            }
            remove
            {
                OnObjectRefreshedEventHandler onObjectRefreshedEventHandler;
                OnObjectRefreshedEventHandler onObjectRefreshedEventHandler0 = this.onObjectRefreshedEventHandler_0;
                do
                {
                    onObjectRefreshedEventHandler = onObjectRefreshedEventHandler0;
                    OnObjectRefreshedEventHandler onObjectRefreshedEventHandler1 = (OnObjectRefreshedEventHandler)Delegate.Remove(onObjectRefreshedEventHandler, value);
                    onObjectRefreshedEventHandler0 = Interlocked.CompareExchange<OnObjectRefreshedEventHandler>(ref this.onObjectRefreshedEventHandler_0, onObjectRefreshedEventHandler1, onObjectRefreshedEventHandler);
                }
                while ((object)onObjectRefreshedEventHandler0 != (object)onObjectRefreshedEventHandler);
            }
        }

        public event OnRefreshAllEventHandler OnRefreshAll
        {
            add
            {
                OnRefreshAllEventHandler onRefreshAllEventHandler;
                OnRefreshAllEventHandler onRefreshAllEventHandler0 = this.onRefreshAllEventHandler_0;
                do
                {
                    onRefreshAllEventHandler = onRefreshAllEventHandler0;
                    OnRefreshAllEventHandler onRefreshAllEventHandler1 = (OnRefreshAllEventHandler)Delegate.Combine(onRefreshAllEventHandler, value);
                    onRefreshAllEventHandler0 = Interlocked.CompareExchange<OnRefreshAllEventHandler>(ref this.onRefreshAllEventHandler_0, onRefreshAllEventHandler1, onRefreshAllEventHandler);
                }
                while ((object)onRefreshAllEventHandler0 != (object)onRefreshAllEventHandler);
            }
            remove
            {
                OnRefreshAllEventHandler onRefreshAllEventHandler;
                OnRefreshAllEventHandler onRefreshAllEventHandler0 = this.onRefreshAllEventHandler_0;
                do
                {
                    onRefreshAllEventHandler = onRefreshAllEventHandler0;
                    OnRefreshAllEventHandler onRefreshAllEventHandler1 = (OnRefreshAllEventHandler)Delegate.Remove(onRefreshAllEventHandler, value);
                    onRefreshAllEventHandler0 = Interlocked.CompareExchange<OnRefreshAllEventHandler>(ref this.onRefreshAllEventHandler_0, onRefreshAllEventHandler1, onRefreshAllEventHandler);
                }
                while ((object)onRefreshAllEventHandler0 != (object)onRefreshAllEventHandler);
            }
        }
    }
}