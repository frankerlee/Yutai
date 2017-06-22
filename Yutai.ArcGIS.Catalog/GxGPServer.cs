using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxGPServer : IGxObject, IGxObjectContainer, IGxObjectEdit, IGxObjectProperties, IGxObjectUI, IGxContextMenuWap, IGxAGSObject
    {
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IAGSServerObjectName iagsserverObjectName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private int int_0 = -1;
        private int int_1 = 0;
        private int int_2 = 0;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "";

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

        public bool CanRename()
        {
            return false;
        }

        public void Delete()
        {
            try
            {
                string name = this.iagsserverObjectName_0.Name;
                string type = this.iagsserverObjectName_0.Type;
                (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.DeleteConfiguration(name, type);
                this.Detach();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Logger.Current.Error("",exception, "");
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

        public void EditProperties(int int_3)
        {
        }

        public void EditServerObjectProperties(int int_3)
        {
        }

        public void GetPropByIndex(int int_3, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        public void Init(object object_0)
        {
            IPopuMenuWrap wrap = null;
            wrap = object_0 as IPopuMenuWrap;
            wrap.Clear();
            if (this.int_0 > 0)
            {
                wrap.AddItem("Catalog_StartServer", true);
                wrap.AddItem("Catalog_StopServer", true);
                wrap.AddItem("Catalog_ServiceProperty", true);
            }
            wrap.AddItem("Catalog_GxObjectProperty", true);
        }

        private void method_0()
        {
            if (this.int_0 == 0)
            {
                IGPToolInfos toolInfos = ((this.iagsserverObjectName_0 as IName).Open() as IGPServer).GetToolInfos();
                for (int i = 0; i < toolInfos.Count; i++)
                {
                    IGPToolInfo info = toolInfos.get_Element(i);
                    IGxObject obj2 = new GxGPGPTool();
                    (obj2 as IGxGPGPTool).AGSServerObjectName = this.iagsserverObjectName_0;
                    (obj2 as IGxGPGPTool).AGSServerConnection = this.iagsserverConnection_0;
                    (obj2 as IGxGPGPTool).GPToolInfo = info;
                    obj2.Attach(this, this.igxCatalog_0);
                }
            }
        }

        public void Refresh()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxCatalog_0.ObjectRefreshed(this);
            }
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

        public IAGSServerObjectName AGSServerObjectName
        {
            get
            {
                return this.iagsserverObjectName_0;
            }
            set
            {
                this.iagsserverObjectName_0 = value;
                this.iagsserverConnection_0 = (this.iagsserverObjectName_0.AGSServerConnectionName as IName).Open() as IAGSServerConnection;
                this.int_0 = Convert.ToInt32(this.iagsserverObjectName_0.AGSServerConnectionName.ConnectionProperties.GetProperty("CONNECTIONMODE"));
            }
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
                return this.Name;
            }
        }

        public string Category
        {
            get
            {
                if (this.iagsserverObjectName_0 != null)
                {
                    return this.iagsserverObjectName_0.Type;
                }
                return "GPServer";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.igxObjectArray_0.Count == 0)
                {
                    this.method_0();
                }
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

        public string DefaultMapName
        {
            get
            {
                if ((this.string_0.Length == 0) && (this.iagsserverObjectName_0 != null))
                {
                    IAGSServerObject obj2 = (this.iagsserverObjectName_0 as IName).Open() as IAGSServerObject;
                    IMapServer server = obj2 as IMapServer;
                    if (server != null)
                    {
                        this.string_0 = server.DefaultMapName;
                    }
                }
                return this.string_0;
            }
        }

        public string FullName
        {
            get
            {
                if (this.iagsserverObjectName_0 != null)
                {
                    return this.iagsserverObjectName_0.Name;
                }
                return "";
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
                return (this.iagsserverObjectName_0 as IName);
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
                if (this.Status != "Started")
                {
                    if (this.Status == "Stopped")
                    {
                        return ImageLib.GetSmallImage(84);
                    }
                    if (this.Status == "Paused")
                    {
                        return ImageLib.GetSmallImage(85);
                    }
                }
                return ImageLib.GetSmallImage(83);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                if (this.Status != "Started")
                {
                    if (this.Status == "Stopped")
                    {
                        return ImageLib.GetSmallImage(84);
                    }
                    if (this.Status == "Paused")
                    {
                        return ImageLib.GetSmallImage(85);
                    }
                }
                return ImageLib.GetSmallImage(83);
            }
        }

        public string Name
        {
            get
            {
                if (this.iagsserverObjectName_0 != null)
                {
                    string[] strArray = this.iagsserverObjectName_0.Name.Split(new char[] { '/' });
                    return strArray[strArray.Length - 1];
                }
                return "";
            }
        }

        public UID NewMenu
        {
            get
            {
                return null;
            }
        }

        public int NumInstancesInUse
        {
            get
            {
                return (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type).InstanceInUseCount;
            }
        }

        public int NumInstancesRunning
        {
            get
            {
                return (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type).InstanceCount;
            }
        }

        public IGxObject Parent
        {
            get
            {
                return this.igxObject_0;
            }
        }

        public int PropertyCount
        {
            get
            {
                return 0;
            }
        }

        public Bitmap SmallImage
        {
            get
            {
                if (this.Status != "Started")
                {
                    if (this.Status == "Stopped")
                    {
                        return ImageLib.GetSmallImage(84);
                    }
                    if (this.Status == "Paused")
                    {
                        return ImageLib.GetSmallImage(85);
                    }
                }
                return ImageLib.GetSmallImage(83);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.Status != "Started")
                {
                    if (this.Status == "Stopped")
                    {
                        return ImageLib.GetSmallImage(84);
                    }
                    if (this.Status == "Paused")
                    {
                        return ImageLib.GetSmallImage(85);
                    }
                }
                return ImageLib.GetSmallImage(83);
            }
        }

        public string Status
        {
            get
            {
                try
                {
                    IServerObjectConfigurationStatus configurationStatus = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type);
                    if (configurationStatus.Status == esriConfigurationStatus.esriCSStarted)
                    {
                        return "Started";
                    }
                    if (configurationStatus.Status == esriConfigurationStatus.esriCSStarting)
                    {
                        return "Starting";
                    }
                    if (configurationStatus.Status == esriConfigurationStatus.esriCSStopped)
                    {
                        return "Stopped";
                    }
                    if (configurationStatus.Status == esriConfigurationStatus.esriCSStopping)
                    {
                        return "Stopping";
                    }
                    if (configurationStatus.Status == esriConfigurationStatus.esriCSPaused)
                    {
                        return "Paused";
                    }
                    if (configurationStatus.Status == esriConfigurationStatus.esriCSDeleted)
                    {
                        return "Deleted";
                    }
                }
                catch (Exception)
                {
                }
                return "Started";
            }
        }
    }
}

