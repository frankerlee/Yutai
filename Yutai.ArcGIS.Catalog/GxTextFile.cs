using System.Drawing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class GxTextFile : IGxObject, IGxDataset, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxFileSetup, IGxTextFile
    {
        private IDatasetName idatasetName_0;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private string string_0 = "";

        public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_1;
            if (igxObject_1 is IGxObjectContainer)
            {
                (igxObject_1 as IGxObjectContainer).AddChild(this);
            }
        }

        public bool CanCopy()
        {
            return true;
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
        }

        public void Edit()
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

        private IName method_0()
        {
            if (this.string_0.Length == 0)
            {
                return null;
            }
            IWorkspaceName name2 = new WorkspaceNameClass {
                WorkspaceFactoryProgID = "esriDataSourcesOleDB.TextFileWorkspaceFactory",
                PathName = System.IO.Path.GetDirectoryName(this.string_0)
            };
            IDatasetName name3 = new TableNameClass {
                Name = System.IO.Path.GetFileName(this.string_0),
                WorkspaceName = name2
            };
            return (name3 as IName);
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

        public void Rename(string string_1)
        {
        }

        public void Save()
        {
        }

        public void SetImages(int int_0, int int_1, int int_2, int int_3)
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
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(this.string_0);
            }
        }

        public string Category
        {
            set
            {
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
                if (this.idatasetName_0 == null)
                {
                    this.idatasetName_0 = this.method_0() as IDatasetName;
                }
                if (this.idatasetName_0 != null)
                {
                    try
                    {
                        return ((this.idatasetName_0 as IName).Open() as IDataset);
                    }
                    catch
                    {
                    }
                }
                return null;
            }
        }

        public IDatasetName DatasetName
        {
            get
            {
                if (this.idatasetName_0 == null)
                {
                    this.idatasetName_0 = this.method_0() as IDatasetName;
                }
                return this.idatasetName_0;
            }
            set
            {
                this.idatasetName_0 = value;
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
                if (this.idatasetName_0 == null)
                {
                    this.idatasetName_0 = this.method_0() as IDatasetName;
                }
                return (this.idatasetName_0 as IName);
            }
        }

        public bool IsValid
        {
            get
            {
                return false;
            }
        }

        string IGxObject.Category
        {
            get
            {
                return "文本文件";
            }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get
            {
                if (this.idatasetName_0 == null)
                {
                    this.idatasetName_0 = this.method_0() as IDatasetName;
                }
                return (this.idatasetName_0 as IName);
            }
            set
            {
                this.idatasetName_0 = value as IDatasetName;
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x1a);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x1a);
            }
        }

        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(this.string_0);
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

        public string Path
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
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
                return ImageLib.GetSmallImage(0x1a);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(0x1a);
            }
        }

        public esriDatasetType Type
        {
            get
            {
                return esriDatasetType.esriDTText;
            }
        }
    }
}

