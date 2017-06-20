using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common.Framework;

namespace Yutai.ArcGIS.Catalog
{
    public class GxShapefileDataset : IGxObject, IGxDataset, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxContextMenuWap
    {
        private IDatasetName idatasetName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
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
            return true;
        }

        public bool CanDelete()
        {
            try
            {
                IDataset dataset = this.Dataset;
                if (dataset != null)
                {
                    bool flag = false;
                    flag = dataset.CanDelete();
                    dataset = null;
                    return flag;
                }
            }
            catch
            {
            }
            return false;
        }

        public bool CanRename()
        {
            try
            {
                IDataset dataset = this.Dataset;
                if (dataset != null)
                {
                    bool flag = false;
                    flag = dataset.CanRename();
                    dataset = null;
                    return flag;
                }
            }
            catch
            {
            }
            return false;
        }

        public void Delete()
        {
            try
            {
                IDataset dataset = this.Dataset;
                if (dataset != null)
                {
                    dataset.Delete();
                    dataset = null;
                    this.igxCatalog_0.ObjectDeleted(this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

        public void EditProperties(int int_0)
        {
            IDataset dataset = this.Dataset;
            if ((dataset != null) && (dataset is IObjectClass))
            {
                new ObjectClassInfoEdit { ObjectClass = dataset as IObjectClass }.ShowDialog();
            }
        }

        public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        public void Init(object object_0)
        {
            this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
            this.ipopuMenuWrap_0.Clear();
            this.ipopuMenuWrap_0.AddItem("Catalog_Copy", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_Delete", true);
            this.ipopuMenuWrap_0.AddItem("Catalog_Rename", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
            this.ipopuMenuWrap_0.UpdateUI();
        }

        public void Refresh()
        {
        }

        public void Rename(string string_1)
        {
            try
            {
                if ((string_1 != null) && (this.Dataset != null))
                {
                    if (System.IO.Path.GetExtension(string_1).ToLower() == ".shp")
                    {
                        string_1 = System.IO.Path.GetFileNameWithoutExtension(string_1);
                    }
                    this.Dataset.Rename(string_1);
                    this.idatasetName_0.Name = string_1 + ".shp";
                    this.string_0 = this.idatasetName_0.WorkspaceName.PathName + @"\" + this.idatasetName_0.Name;
                    this.igxCatalog_0.ObjectChanged(this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
            get
            {
                if (this.idatasetName_0 == null)
                {
                    return "错误Shapefile";
                }
                return "Shapefile";
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
                catch
                {
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
                return null;
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return null;
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
                try
                {
                    if (this.idatasetName_0 is IFeatureClassName)
                    {
                        IFeatureClassName name = this.idatasetName_0 as IFeatureClassName;
                        IFeatureClass class2 = (name as IName).Open() as IFeatureClass;
                        switch (class2.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                            case esriGeometryType.esriGeometryMultipoint:
                                return ImageLib.GetSmallImage(0x17);

                            case esriGeometryType.esriGeometryPolyline:
                            case esriGeometryType.esriGeometryPath:
                            case esriGeometryType.esriGeometryRay:
                                return ImageLib.GetSmallImage(0x18);

                            case esriGeometryType.esriGeometryPolygon:
                                return ImageLib.GetSmallImage(0x19);

                            case esriGeometryType.esriGeometryEnvelope:
                                goto Label_0082;
                        }
                    }
                }
                catch
                {
                }
            Label_0082:
                return ImageLib.GetSmallImage(0x1f);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                try
                {
                    if (this.idatasetName_0 is IFeatureClassName)
                    {
                        IFeatureClassName name = this.idatasetName_0 as IFeatureClassName;
                        IFeatureClass class2 = (name as IName).Open() as IFeatureClass;
                        switch (class2.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                            case esriGeometryType.esriGeometryMultipoint:
                                return ImageLib.GetSmallImage(0x17);

                            case esriGeometryType.esriGeometryPolyline:
                            case esriGeometryType.esriGeometryPath:
                            case esriGeometryType.esriGeometryRay:
                                return ImageLib.GetSmallImage(0x18);

                            case esriGeometryType.esriGeometryPolygon:
                                return ImageLib.GetSmallImage(0x19);

                            case esriGeometryType.esriGeometryEnvelope:
                                goto Label_0082;
                        }
                    }
                }
                catch
                {
                }
            Label_0082:
                return ImageLib.GetSmallImage(0x1f);
            }
        }

        public esriDatasetType Type
        {
            get
            {
                if (this.idatasetName_0 != null)
                {
                    return this.idatasetName_0.Type;
                }
                return esriDatasetType.esriDTAny;
            }
        }
    }
}

