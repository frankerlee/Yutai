using System.Drawing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class GxNewDatabase : IGxObject, IGxBasicObject, IGxNewDatabase, IGxObjectEdit, IGxObjectProperties,
        IGxObjectUI
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IWorkspaceFactory iworkspaceFactory_0 = null;
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

        public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        public void Refresh()
        {
        }

        public void Rename(string string_1)
        {
        }

        public void SetProperty(string string_1, object object_0)
        {
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public string BaseName
        {
            get { return this.string_0; }
        }

        public string Category
        {
            get { return ""; }
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
            get { return this.string_0; }
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
            get { return ImageLib.GetSmallImage(8); }
        }

        public Bitmap LargeSelectedImage
        {
            get { return ImageLib.GetSmallImage(8); }
        }

        public string Name
        {
            get { return this.string_0; }
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

        public Bitmap SmallImage
        {
            get { return ImageLib.GetSmallImage(8); }
        }

        public Bitmap SmallSelectedImage
        {
            get { return ImageLib.GetSmallImage(8); }
        }

        public IWorkspaceFactory WorkspaceFactory
        {
            set
            {
                this.iworkspaceFactory_0 = value;
                if (this.iworkspaceFactory_0 is IOleDBConnectionInfo)
                {
                    this.string_0 = "添加OLE DB连接";
                }
                else if (this.iworkspaceFactory_0 is ISetDefaultConnectionInfo)
                {
                    this.string_0 = "添加空间数据库连接";
                }
            }
        }
    }
}