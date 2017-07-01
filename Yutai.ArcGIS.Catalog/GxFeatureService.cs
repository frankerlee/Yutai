using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxFeatureService : IGxObject, IGxAGSMap, IGxObjectEdit, IGxObjectProperties, IGxObjectUI, IGxAGSObject,
        IGxLayerSource
    {
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IAGSServerObjectName iagsserverObjectName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "";

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
                (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.DeleteConfiguration(name,
                    type);
                this.Detach();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Logger.Current.Error("", exception, "");
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

        public void EditProperties(int int_2)
        {
        }

        public void EditServerObjectProperties(int int_2)
        {
            frmServerObjectPropertySheet sheet = new frmServerObjectPropertySheet();
            this.iserverObjectConfiguration_0 =
                (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).get_ServerObjectConfiguration(
                    this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type);
            sheet.AGSConnectionAdmin = this.iagsserverConnection_0 as IAGSServerConnectionAdmin;
            sheet.ServerObjectConfig = this.iserverObjectConfiguration_0;
            sheet.Status = this.Status;
            sheet.ShowDialog();
            this.iserverObjectConfiguration_0 = null;
        }

        public void GetPropByIndex(int int_2, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
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

        public void SetProperty(string string_1, object object_0)
        {
        }

        public IAGSServerObjectName AGSServerObjectName
        {
            get { return this.iagsserverObjectName_0; }
            set
            {
                this.iagsserverObjectName_0 = value;
                this.iagsserverConnection_0 =
                    (this.iagsserverObjectName_0.AGSServerConnectionName as IName).Open() as IAGSServerConnection;
            }
        }

        public string BaseName
        {
            get { return this.Name; }
        }

        public string Category
        {
            get
            {
                if (this.iagsserverObjectName_0 != null)
                {
                    return this.iagsserverObjectName_0.Type;
                }
                return "FeatureServer";
            }
        }

        public UID ClassID
        {
            get { return null; }
        }

        public UID ContextMenu
        {
            get { return null; }
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

        public IName InternalObjectName
        {
            get { return (this.iagsserverObjectName_0 as IName); }
        }

        public bool IsValid
        {
            get { return false; }
        }

        public Bitmap LargeImage
        {
            get { return ImageLib.GetSmallImage(89); }
        }

        public Bitmap LargeSelectedImage
        {
            get { return ImageLib.GetSmallImage(89); }
        }

        public string Name
        {
            get
            {
                if (this.iagsserverObjectName_0 != null)
                {
                    string[] strArray = this.iagsserverObjectName_0.Name.Split(new char[] {'/'});
                    return strArray[strArray.Length - 1];
                }
                return "";
            }
        }

        public UID NewMenu
        {
            get { return null; }
        }

        public int NumInstancesInUse
        {
            get
            {
                return
                    (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(
                        this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type).InstanceInUseCount;
            }
        }

        public int NumInstancesRunning
        {
            get
            {
                return
                    (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(
                        this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type).InstanceCount;
            }
        }

        public IGxObject Parent
        {
            get { return this.igxObject_0; }
        }

        public int PropertyCount
        {
            get { return 0; }
        }

        public Bitmap SmallImage
        {
            get { return ImageLib.GetSmallImage(89); }
        }

        public Bitmap SmallSelectedImage
        {
            get { return ImageLib.GetSmallImage(89); }
        }

        public string Status
        {
            get
            {
                try
                {
                    IServerObjectConfigurationStatus configurationStatus =
                        (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin
                            .GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type);
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