using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxCadDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget
    {
        private IDatasetName idatasetName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private string string_0 = "";

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            string strB = igxObject_1.Name.ToUpper();
            int num = 0;
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                IGxObject obj2 = this.igxObjectArray_0.Item(i);
                num = obj2.Name.ToUpper().CompareTo(strB);
                if (num > 0)
                {
                    this.igxObjectArray_0.Insert(i, igxObject_1);
                    return igxObject_1;
                }
                if (num == 0)
                {
                    return obj2;
                }
            }
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            return igxObject_1;
        }

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
            try
            {
                IDataset dataset = this.Dataset;
                if (dataset != null)
                {
                    bool flag = false;
                    flag = dataset.CanCopy();
                    dataset = null;
                    return flag;
                }
            }
            catch
            {
            }
            return false;
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

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
        {
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
                Logger.Current.Error("",exception, "");
            }
        }

        public void DeleteChild(IGxObject igxObject_1)
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
            IDataset dataset = this.Dataset;
            if ((dataset != null) && !(dataset is IObjectClass))
            {
            }
        }

        public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        private void method_0(string string_1)
        {
            try
            {
                IGxObject obj2 = new GxCadDataset();
                IDatasetName name = new FeatureClassNameClass();
                IWorkspaceName name2 = new WorkspaceNameClass {
                    WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                    PathName = this.idatasetName_0.WorkspaceName.PathName
                };
                name.Name = this.idatasetName_0.Name + ":" + string_1;
                name.WorkspaceName = name2;
                (obj2 as IGxDataset).DatasetName = name;
                obj2.Attach(this, this.igxCatalog_0);
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        private void method_1()
        {
            IGxObject obj2 = new GxCadDrawing();
            IDatasetName name2 = new CadDrawingNameClass();
            IWorkspaceName name = new WorkspaceNameClass {
                WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                PathName = Path.GetDirectoryName(this.string_0)
            };
            name2.Name = Path.GetFileName(this.string_0);
            name2.WorkspaceName = name;
            (obj2 as IGxDataset).DatasetName = name2;
            obj2.Attach(this, this.igxCatalog_0);
            this.method_0("Annotation");
            this.method_0("Point");
            this.method_0("Polyline");
            this.method_0("Polygon");
            this.method_0("MultiPatch");
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
            try
            {
                if ((string_1 != null) && (this.Dataset != null))
                {
                    if (Path.GetFileNameWithoutExtension(string_1).Trim().Length == 0)
                    {
                        MessageBox.Show("必须键入文件名!");
                        this.igxCatalog_0.ObjectChanged(this);
                    }
                    else
                    {
                        this.Dataset.Rename(string_1);
                        this.idatasetName_0.Name = string_1;
                        this.string_0 = this.idatasetName_0.WorkspaceName.PathName + @"\" + this.idatasetName_0.Name;
                        this.igxCatalog_0.ObjectChanged(this);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
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
            get
            {
                return false;
            }
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
                if (this.idatasetName_0 == null)
                {
                    return "错误";
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTCadDrawing)
                {
                    return "CAD要素集";
                }
                if ((this.idatasetName_0 as IFeatureClassName).FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                {
                    return "CAD注记要素类";
                }
                string name = this.Name;
                switch (name)
                {
                    case null:
                        break;

                    case "Annotation":
                        return "CAD注记要素类";

                    case "Point":
                        return "CAD点要素类";

                    case "Polyline":
                        return "CAD多义线要素类";

                    default:
                        if (!(name == "Polygon"))
                        {
                            if (name == "MultiPatch")
                            {
                                return "CAD多面要素类";
                            }
                        }
                        else
                        {
                            return "CAD多边形要素类";
                        }
                        break;
                }
                return "CAD";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.HasChildren && (this.igxObjectArray_0.Count == 0))
                {
                    this.method_1();
                }
                return (this.igxObjectArray_0 as IEnumGxObject);
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

        public bool HasChildren
        {
            get
            {
                return ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer) || (this.idatasetName_0.Type == esriDatasetType.esriDTCadDrawing));
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
                try
                {
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTCadDrawing)
                    {
                        return ImageLib.GetSmallImage(40);
                    }
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        if ((this.idatasetName_0 as IFeatureClassName).FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                        {
                            return ImageLib.GetSmallImage(44);
                        }
                        string name = this.Name;
                        switch (name)
                        {
                            case null:
                                goto Label_00EE;

                            case "Annotation":
                                return ImageLib.GetSmallImage(44);

                            case "Point":
                                return ImageLib.GetSmallImage(41);

                            case "Polyline":
                                return ImageLib.GetSmallImage(42);
                        }
                        if (!(name == "Polygon"))
                        {
                            if (name == "MultiPatch")
                            {
                                return ImageLib.GetSmallImage(45);
                            }
                        }
                        else
                        {
                            return ImageLib.GetSmallImage(43);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("",exception, "");
                }
            Label_00EE:
                return ImageLib.GetSmallImage(40);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                try
                {
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTCadDrawing)
                    {
                        return ImageLib.GetSmallImage(40);
                    }
                    if (this.idatasetName_0 is IFeatureClassName)
                    {
                        if ((this.idatasetName_0 as IFeatureClassName).FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                        {
                            return ImageLib.GetSmallImage(44);
                        }
                        string name = this.Name;
                        switch (name)
                        {
                            case null:
                                goto Label_00EE;

                            case "Annotation":
                                return ImageLib.GetSmallImage(44);

                            case "Point":
                                return ImageLib.GetSmallImage(41);

                            case "Polyline":
                                return ImageLib.GetSmallImage(42);
                        }
                        if (!(name == "Polygon"))
                        {
                            if (name == "MultiPatch")
                            {
                                return ImageLib.GetSmallImage(45);
                            }
                        }
                        else
                        {
                            return ImageLib.GetSmallImage(43);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("",exception, "");
                }
            Label_00EE:
                return ImageLib.GetSmallImage(40);
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

