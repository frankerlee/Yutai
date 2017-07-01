using System.Drawing;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public class GxOfficeFile : IGxObject, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties,
        IGxObjectUI, IGxFileSetup, IGxOfficeFile
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private string string_0 = "";
        private string string_1 = "";

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

        public void Close(bool bool_0)
        {
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

        public void Edit()
        {
        }

        public void EditProperties(int int_0)
        {
        }

        public void GetPropByIndex(int int_0, ref string string_2, ref object object_0)
        {
        }

        public object GetProperty(string string_2)
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

        public void Rename(string string_2)
        {
        }

        public void Save()
        {
        }

        public void SetImages(int int_0, int int_1, int int_2, int int_3)
        {
        }

        public void SetProperty(string string_2, object object_0)
        {
        }

        public string BaseName
        {
            get { return this.string_0; }
        }

        public string Category
        {
            get { return this.string_1; }
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
            get
            {
                IFileName name = new FileNameClass
                {
                    Path = this.string_0
                };
                return (name as IName);
            }
        }

        public bool IsValid
        {
            get { return (this.string_0.Trim().Length > 0); }
        }

        string IGxFileSetup.Category
        {
            set { }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get { return null; }
            set { }
        }

        public Bitmap LargeImage
        {
            get
            {
                if (this.string_1 == "WORD")
                {
                    return ImageLib.GetSmallImage(48);
                }
                return ImageLib.GetSmallImage(49);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                if (this.string_1 == "WORD")
                {
                    return ImageLib.GetSmallImage(48);
                }
                return ImageLib.GetSmallImage(49);
            }
        }

        public string Name
        {
            get
            {
                if (this.string_0.Length == 0)
                {
                    return "";
                }
                return System.IO.Path.GetFileName(this.string_0);
            }
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
            get { return this.string_0; }
            set
            {
                this.string_0 = value;
                switch (System.IO.Path.GetExtension(this.string_0).ToLower())
                {
                    case ".doc":
                        this.string_1 = "WORD";
                        break;

                    case ".xls":
                        this.string_1 = "EXCEL";
                        break;
                }
            }
        }

        public int PropertyCount
        {
            get { return 0; }
        }

        public Bitmap SmallImage
        {
            get
            {
                if (this.string_1 == "WORD")
                {
                    return ImageLib.GetSmallImage(48);
                }
                return ImageLib.GetSmallImage(49);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.string_1 == "WORD")
                {
                    return ImageLib.GetSmallImage(48);
                }
                return ImageLib.GetSmallImage(49);
            }
        }
    }
}