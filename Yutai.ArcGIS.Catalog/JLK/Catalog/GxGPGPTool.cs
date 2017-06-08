namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geoprocessing;
    using ESRI.ArcGIS.GISClient;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class GxGPGPTool : IGxObject, IGxObjectProperties, IGxObjectUI, IGxGPGPTool
    {
        [CompilerGenerated]
        private IAGSServerConnection iagsserverConnection_0;
        [CompilerGenerated]
        private IAGSServerObjectName iagsserverObjectName_0;
        [CompilerGenerated]
        private IGPToolInfo igptoolInfo_0;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;

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
            return false;
        }

        public bool CanRename()
        {
            return false;
        }

        public void Delete()
        {
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

        public void GetPropByIndex(int int_0, ref string string_0, ref object object_0)
        {
        }

        public object GetProperty(string string_0)
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

        public void Rename(string string_0)
        {
        }

        public void SetProperty(string string_0, object object_0)
        {
        }

        public IAGSServerConnection AGSServerConnection
        {
            [CompilerGenerated]
            get
            {
                return this.iagsserverConnection_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverConnection_0 = value;
            }
        }

        public IAGSServerObjectName AGSServerObjectName
        {
            [CompilerGenerated]
            get
            {
                return this.iagsserverObjectName_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverObjectName_0 = value;
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
                return "工具箱工具";
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

        public string FullName
        {
            get
            {
                return this.Name;
            }
        }

        public IGPToolInfo GPToolInfo
        {
            [CompilerGenerated]
            get
            {
                return this.igptoolInfo_0;
            }
            [CompilerGenerated]
            set
            {
                this.igptoolInfo_0 = value;
            }
        }

        public IName InternalObjectName
        {
            get
            {
                return null;
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
                return ImageLib.GetSmallImage(90);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(90);
            }
        }

        public string Name
        {
            get
            {
                if (this.GPToolInfo != null)
                {
                    return this.GPToolInfo.Name;
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
                return ImageLib.GetSmallImage(90);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(90);
            }
        }
    }
}

