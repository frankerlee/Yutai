using System.Drawing;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public class GxShortcut : IGxObject, IGxFile, IGxObjectEdit, IGxObjectProperties, IGxObjectUI, IGxShortcut
    {
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

        public bool CanRename()
        {
            return false;
        }

        public void Close(bool bool_0)
        {
        }

        public void Delete()
        {
        }

        public void Detach()
        {
        }

        public void Edit()
        {
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

        public void New()
        {
        }

        public void Open()
        {
        }

        public void Refresh()
        {
        }

        public void Rename(string string_0)
        {
        }

        public void Save()
        {
        }

        public void SetProperty(string string_0, object object_0)
        {
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public string BaseName
        {
            get { return null; }
        }

        public string Category
        {
            get { return null; }
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
            get { return null; }
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
            get { return null; }
        }

        public Bitmap LargeSelectedImage
        {
            get { return null; }
        }

        public string Name
        {
            get { return null; }
        }

        public UID NewMenu
        {
            get { return null; }
        }

        public IGxObject Parent
        {
            get { return null; }
        }

        public string Path
        {
            get { return null; }
            set { }
        }

        public int PropertyCount
        {
            get { return 0; }
        }

        public Bitmap SmallImage
        {
            get { return null; }
        }

        public Bitmap SmallSelectedImage
        {
            get { return null; }
        }

        public IGxObject Target
        {
            get { return null; }
            set { }
        }

        public string TargetLocation
        {
            get { return null; }
            set { }
        }
    }
}