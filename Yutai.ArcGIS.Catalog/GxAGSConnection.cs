using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxAGSConnection : IGxObject, IGxAGSConnection, IGxObjectContainer, IGxObjectEdit, IGxObjectProperties,
        IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxRemoteConnection
    {
        private IAGSServerConnection2 iagsserverConnection2_0 = null;
        private IAGSServerConnectionName iagsserverConnectionName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private IPropertySet ipropertySet_0 = null;
        private object object_0 = null;
        private string string_0 = "";

        public GxAGSConnection()
        {
            this.ConnectionMode = -1;
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
        }

        public bool CanCopy()
        {
            return false;
        }

        public bool CanDelete()
        {
            return true;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public bool CanRename()
        {
            return true;
        }

        public void Connect()
        {
            try
            {
                IAGSServerConnectionFactory2 factory = new AGSServerConnectionFactoryClass();
                if (this.ipropertySet_0 == null)
                {
                    this.ipropertySet_0 = factory.ReadConnectionPropertiesFromFile(this.string_0);
                }
                this.iagsserverConnection2_0 = factory.Open(this.ipropertySet_0, 0) as IAGSServerConnection2;
                this.Init(null);
                this.method_1();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
        }

        public void Delete()
        {
            try
            {
                if (this.IsConnected)
                {
                    this.Disconnect();
                }
                File.Delete(this.string_0);
                this.Detach();
            }
            catch
            {
            }
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

        public void Disconnect()
        {
            this.igxObjectArray_0.Empty();
            this.Init(null);
            this.iagsserverConnection2_0 = null;
        }

        public void EditProperties(int int_1)
        {
            if (this.ConnectionMode == 0)
            {
                frmArcGISServerUseProperty property = new frmArcGISServerUseProperty();
                if (this.iagsserverConnectionName_0 != null)
                {
                    property.AGSServerConnectionName = this.iagsserverConnectionName_0;
                }
                else
                {
                    property.ConnectionProperties = this.ipropertySet_0;
                }
                property.ConnectionFile = this.FileName;
                if (property.ShowDialog() == DialogResult.OK)
                {
                }
            }
            else if ((this.ConnectionMode == 1) || (this.ConnectionMode == 2))
            {
                frmArcGISServerManageProperty property2 = new frmArcGISServerManageProperty();
                if (this.iagsserverConnectionName_0 != null)
                {
                    property2.AGSServerConnectionName = this.iagsserverConnectionName_0;
                }
                else
                {
                    property2.ConnectionProperties = this.ipropertySet_0;
                }
                property2.ConnectionFile = this.FileName;
                if (property2.ShowDialog() != DialogResult.OK)
                {
                }
            }
        }

        public void EditServerProperties(int int_1, short short_0)
        {
            new frmAGSProperty {AGSServerConnectionAdmin = this.iagsserverConnection2_0 as IAGSServerConnectionAdmin}
                .ShowDialog();
        }

        public void GetPropByIndex(int int_1, ref string string_1, ref object object_1)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        public void Init(object object_1)
        {
            if (object_1 != null)
            {
                this.ipopuMenuWrap_0 = object_1 as IPopuMenuWrap;
            }
            this.ipopuMenuWrap_0.Clear();
            this.ipopuMenuWrap_0.AddItem("Catalog_Delete", true);
            this.ipopuMenuWrap_0.AddItem("Catalog_Rename", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_Refresh", true);
            this.ipopuMenuWrap_0.AddItem("Catalog_Connection", true);
            this.ipopuMenuWrap_0.AddItem("Catalog_Disconnection", false);
            if (this.IsConnected)
            {
                this.ipopuMenuWrap_0.AddItem("Catalog_ServerProperty", true);
            }
            this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
        }

        public void LoadFromFile(string string_1)
        {
            this.string_0 = string_1;
            if (this.IsConnected)
            {
                this.Disconnect();
            }
            if (File.Exists(string_1))
            {
                try
                {
                    IAGSServerConnectionFactory2 factory = new AGSServerConnectionFactoryClass();
                    this.ipropertySet_0 = factory.ReadConnectionPropertiesFromFile(string_1);
                    try
                    {
                        this.ConnectionMode = Convert.ToInt32(this.ipropertySet_0.GetProperty("CONNECTIONMODE"));
                    }
                    catch
                    {
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
        }

        private bool method_0(string string_1)
        {
            if (this.object_0 == null)
            {
                return true;
            }
            System.Array array = (System.Array) this.object_0;
            for (int i = 0; i < array.Length; i++)
            {
                string str = (string) array.GetValue(i);
                if (str == string_1)
                {
                    return true;
                }
            }
            return false;
        }

        private void method_1()
        {
            try
            {
                object obj2;
                object obj3;
                IGxObject obj4;
                this.ipropertySet_0.GetAllProperties(out obj2, out obj3);
                string[] strArray = (string[]) obj2;
                if ((strArray.Length == 6) && (strArray[1] == "MANAGERURL"))
                {
                    obj4 = new GxAddAGSObject();
                    obj4.Attach(this, this.igxCatalog_0);
                }
                if (this.iagsserverConnection2_0 != null)
                {
                    IEnumBSTR folders = this.iagsserverConnection2_0.GetFolders("");
                    folders.Reset();
                    for (string str = folders.Next(); str != null; str = folders.Next())
                    {
                        IGxServersFolder folder = new GxServersFolder
                        {
                            AGSServerConnection = this.iagsserverConnection2_0,
                            FolderName = str
                        };
                        (folder as IGxObject).Attach(this, this.igxCatalog_0);
                    }
                    IAGSEnumServerObjectName name = this.iagsserverConnection2_0.get_ServerObjectNamesEx("");
                    name.Reset();
                    for (IAGSServerObjectName name2 = name.Next(); name2 != null; name2 = name.Next())
                    {
                        bool flag;
                        if (name2.Type.ToLower() == "mapserver")
                        {
                            if (this.method_0(name2.Name))
                            {
                                obj4 = new GxAGSMap();
                                (obj4 as IGxAGSObject).AGSServerObjectName = name2;
                                flag = true;
                                if ((this.ConnectionMode == 0) && ((obj4 as IGxAGSObject).Status != "Started"))
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    obj4.Attach(this, this.igxCatalog_0);
                                }
                            }
                        }
                        else if (name2.Type.ToLower() == "gpserver")
                        {
                            if (this.method_0(name2.Name))
                            {
                                obj4 = new GxGPServer();
                                (obj4 as IGxAGSObject).AGSServerObjectName = name2;
                                flag = true;
                                if ((this.ConnectionMode == 0) && ((obj4 as IGxAGSObject).Status != "Started"))
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    obj4.Attach(this, this.igxCatalog_0);
                                }
                            }
                        }
                        else if (name2.Type.ToLower() == "featureserver")
                        {
                            if (this.ConnectionMode == 0)
                            {
                                obj4 = new GxFeatureService();
                                (obj4 as IGxAGSObject).AGSServerObjectName = name2;
                                if ((obj4 as IGxAGSObject).Status == "Started")
                                {
                                    obj4.Attach(this, this.igxCatalog_0);
                                }
                            }
                        }
                        else if ((name2.Type.ToLower() == "geometryserver") && (this.ConnectionMode > 0))
                        {
                            obj4 = new GxGeometryServer();
                            (obj4 as IGxAGSObject).AGSServerObjectName = name2;
                            obj4.Attach(this, this.igxCatalog_0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public void Refresh()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxObjectArray_0.Empty();
                this.method_1();
                this.igxCatalog_0.ObjectRefreshed(this);
            }
        }

        public void Rename(string string_1)
        {
            string directoryName = Path.GetDirectoryName(this.string_0);
            File.Delete(this.string_0);
            this.string_0 = directoryName + @"\" + string_1;
            this.SaveToFile(this.string_0);
        }

        public void SaveToFile(string string_1)
        {
            if (this.iagsserverConnectionName_0 != null)
            {
                if (string_1[1] == ':')
                {
                    this.string_0 = string_1;
                }
                else
                {
                    string path = Environment.SystemDirectory.Substring(0, 2) +
                                  @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
                    if (!Directory.Exists(path))
                    {
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch
                        {
                        }
                    }
                    this.string_0 = path + string_1;
                }
                //IStorageHepler hepler = new StorageHeplerClass();
                //string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.string_0);
                //this.string_0 = Path.GetDirectoryName(this.string_0) + @"\" + fileNameWithoutExtension + ".ags";
                //if (this.object_0 == null)
                //{
                //    hepler.StorageSaveAGS(this.string_0, fileNameWithoutExtension, this.iagsserverConnectionName_0);
                //}
                //else
                //{
                //    hepler.SaveAGS(this.string_0, fileNameWithoutExtension, this.object_0, this.iagsserverConnectionName_0);
                //}
            }
        }

        public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
        {
        }

        public void SetProperty(string string_1, object object_1)
        {
        }

        public IAGSServerConnectionName AGSServerConnectionName
        {
            get
            {
                if ((this.iagsserverConnectionName_0 == null) && (this.iagsserverConnection2_0 != null))
                {
                    this.iagsserverConnectionName_0 = this.iagsserverConnection2_0.FullName as IAGSServerConnectionName;
                }
                return this.iagsserverConnectionName_0;
            }
            set { this.iagsserverConnectionName_0 = value; }
        }

        public bool AreChildrenViewable
        {
            get { return true; }
        }

        public string BaseName
        {
            get { return this.string_0; }
        }

        public string Category
        {
            get
            {
                if (this.iagsserverConnectionName_0 == null)
                {
                    return "ArcGIS Server ";
                }
                if (this.iagsserverConnectionName_0.ConnectionType == esriAGSConnectionType.esriAGSConnectionTypeLAN)
                {
                    return ("ArcGIS Server " +
                            this.iagsserverConnectionName_0.ConnectionProperties.GetProperty("MACHINE").ToString());
                }
                return ("ArcGIS Server " +
                        this.iagsserverConnectionName_0.ConnectionProperties.GetProperty("URL").ToString());
            }
        }

        public IEnumGxObject Children
        {
            get { return (this.igxObjectArray_0 as IEnumGxObject); }
        }

        public UID ClassID
        {
            get { return null; }
        }

        public int ConnectionMode { get; set; }

        public UID ContextMenu
        {
            get { return null; }
        }

        public string FileName
        {
            get { return this.string_0; }
        }

        public string FullName
        {
            get { return this.string_0; }
        }

        public bool HasChildren
        {
            get { return true; }
        }

        public IName InternalObjectName
        {
            get { return (this.iagsserverConnectionName_0 as IName); }
        }

        public bool IsConnected
        {
            get { return (this.iagsserverConnection2_0 != null); }
        }

        public bool IsValid
        {
            get { return false; }
        }

        public Bitmap LargeImage
        {
            get
            {
                if (this.IsConnected)
                {
                    return ImageLib.GetSmallImage(14);
                }
                return ImageLib.GetSmallImage(13);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                if (this.IsConnected)
                {
                    return ImageLib.GetSmallImage(14);
                }
                return ImageLib.GetSmallImage(13);
            }
        }

        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(this.string_0); }
        }

        public UID NewMenu
        {
            get { return null; }
        }

        public IGxObject Parent
        {
            get { return this.igxObject_0; }
        }

        public int PropertyCount
        {
            get { return 0; }
        }

        public object SelectedServerObjects
        {
            get { return this.object_0; }
            set { this.object_0 = value; }
        }

        public Bitmap SmallImage
        {
            get
            {
                if (this.IsConnected)
                {
                    return ImageLib.GetSmallImage(14);
                }
                return ImageLib.GetSmallImage(13);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.IsConnected)
                {
                    return ImageLib.GetSmallImage(14);
                }
                return ImageLib.GetSmallImage(13);
            }
        }
    }
}