using System;
using System.Drawing;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxCadDrawing : IGxObject, IGxDataset, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI
    {
        private IDatasetName idatasetName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
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
            return true;
        }

        public void Delete()
        {
            File.Delete(this.string_0);
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
            if ((string_1 != null) && (string_1.Length != 0))
            {
            }
        }

        public void SetProperty(string string_1, object object_0)
        {
        }

        public string BaseName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.string_0);
            }
        }

        public string Category
        {
            get
            {
                return "CAD绘图";
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

        public IDataset Dataset
        {
            get
            {
                try
                {
                    if (this.idatasetName_0 != null)
                    {
                        return ((this.idatasetName_0 as IName).Open() as IDataset);
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("",exception, "");
                }
                return null;
            }
        }

        public IDatasetName DatasetName
        {
            get
            {
                return this.idatasetName_0;
            }
            set
            {
                this.idatasetName_0 = value;
                this.string_0 = this.idatasetName_0.WorkspaceName.PathName + @"\" + this.idatasetName_0.Name;
            }
        }

        public string FullName
        {
            get
            {
                return this.string_0;
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

        IName IGxObjectInternalName.InternalObjectName
        {
            get
            {
                return (this.idatasetName_0 as IName);
            }
            set
            {
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x38);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x38);
            }
        }

        public string Name
        {
            get
            {
                return Path.GetFileName(this.string_0);
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
                return ImageLib.GetSmallImage(0x38);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x38);
            }
        }

        public esriDatasetType Type
        {
            get
            {
                return esriDatasetType.esriDTCadDrawing;
            }
        }
    }
}

