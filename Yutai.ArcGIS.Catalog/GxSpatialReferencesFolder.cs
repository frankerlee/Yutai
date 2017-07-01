using System.Drawing;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public class GxSpatialReferencesFolder : IGxObject, IGxObjectContainer, IGxObjectEdit, IGxObjectProperties,
        IGxObjectUI, IGxPasteTarget, IGxSpatialReferencesFolder
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private string string_0 = @"D:\Program Files\ArcGIS\Coordinate Systems";

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            return null;
        }

        public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_1;
            this.igxCatalog_0 = igxCatalog_1;
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

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public void Refresh()
        {
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
            get { return null; }
        }

        public string Category
        {
            get { return null; }
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
            get { return null; }
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
            get { return this.igxObject_0; }
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
    }
}