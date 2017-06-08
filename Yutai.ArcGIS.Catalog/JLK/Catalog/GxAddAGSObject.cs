namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;
    using System.Drawing;

    public class GxAddAGSObject : IGxObject, IGxBasicObject, IGxObjectProperties, IGxObjectUI
    {
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

        public void GetPropByIndex(int int_0, ref string string_0, ref object object_0)
        {
        }

        public object GetProperty(string string_0)
        {
            return null;
        }

        public void Refresh()
        {
        }

        public void SetProperty(string string_0, object object_0)
        {
        }

        public string BaseName
        {
            get
            {
                return "添加Server Object";
            }
        }

        public string Category
        {
            get
            {
                return "添加Server Object";
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
                return "添加Server Object";
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
                return ImageLib.GetSmallImage(0x34);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x34);
            }
        }

        public string Name
        {
            get
            {
                return "添加Server Object";
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
                return ImageLib.GetSmallImage(0x34);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x34);
            }
        }
    }
}

