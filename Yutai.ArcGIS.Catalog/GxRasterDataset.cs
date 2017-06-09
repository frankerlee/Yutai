using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxRasterDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap
    {
        private bool bool_0 = false;
        private IDatasetName idatasetName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;

        public IGxObject AddChild(IGxObject igxObject_1)
        {
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
            return true;
        }

        public bool CanDelete()
        {
            IDataset dataset = this.Dataset;
            bool flag = false;
            try
            {
                flag = dataset.CanDelete();
            }
            catch
            {
            }
            dataset = null;
            return flag;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_1)
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
                IDataset o = this.Dataset;
                o.Delete();
                this.Detach();
                Marshal.ReleaseComObject(o);
                o = null;
            }
            catch
            {
            }
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

        public void GetPropByIndex(int int_0, ref string string_0, ref object object_0)
        {
        }

        public object GetProperty(string string_0)
        {
            return null;
        }

        public void Init(object object_0)
        {
            this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
            this.ipopuMenuWrap_0.Clear();
            if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterDataset)
            {
                this.ipopuMenuWrap_0.AddItem("CopyItem", false);
                this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
                this.ipopuMenuWrap_0.AddItem("ReName", false);
                this.ipopuMenuWrap_0.AddItem("RefreshItem", true);
                this.ipopuMenuWrap_0.UpdateUI();
            }
            else if ((this.idatasetName_0.Type != esriDatasetType.esriDTRasterBand) && (this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog))
            {
                this.ipopuMenuWrap_0.AddItem("DeleteObject", false);
            }
        }

        private void method_0()
        {
            this.bool_0 = true;
            this.igxObjectArray_0.Empty();
            if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterDataset)
            {
                try
                {
                    IEnumDatasetName subsetNames = this.idatasetName_0.SubsetNames;
                    subsetNames.Reset();
                    for (IDatasetName name2 = subsetNames.Next(); name2 != null; name2 = subsetNames.Next())
                    {
                        IGxObject obj2 = new GxRasterDataset();
                        (obj2 as IGxDataset).DatasetName = name2;
                        obj2.Attach(this, this.igxCatalog_0);
                    }
                }
                catch
                {
                }
            }
            else if (this.idatasetName_0.Type != esriDatasetType.esriDTRasterCatalog)
            {
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_1)
        {
            return false;
        }

        public void Refresh()
        {
        }

        public void Rename(string string_0)
        {
            try
            {
                if (string_0 != null)
                {
                    IDataset o = this.Dataset;
                    if (o != null)
                    {
                        Path.GetFileNameWithoutExtension(string_0);
                        this.Dataset.Rename(string_0);
                        this.idatasetName_0.Name = string_0;
                        Marshal.ReleaseComObject(o);
                        o = null;
                        this.igxCatalog_0.ObjectChanged(this);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_1)
        {
        }

        public void SetProperty(string string_0, object object_0)
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
                if (this.idatasetName_0 is IRasterBandName)
                {
                    return false;
                }
                return true;
            }
        }

        public string BaseName
        {
            get
            {
                if (this.idatasetName_0 != null)
                {
                    return Path.GetFileNameWithoutExtension(this.idatasetName_0.Name);
                }
                return "";
            }
        }

        public string Category
        {
            get
            {
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterBand)
                {
                    return "删格波段";
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterDataset)
                {
                    return "删格数据集合";
                }
                return "删格目录";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (!this.bool_0)
                {
                    this.method_0();
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
            }
        }

        public string FullName
        {
            get
            {
                if (this.idatasetName_0 != null)
                {
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog)
                    {
                        if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            return (@"数据库连接\" + Path.GetFileName(this.idatasetName_0.WorkspaceName.PathName) + @"\" + this.idatasetName_0.Name);
                        }
                        return (this.idatasetName_0.WorkspaceName.PathName + @"\" + this.idatasetName_0.Name);
                    }
                    if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        return (@"数据库连接\" + Path.GetFileName(this.idatasetName_0.WorkspaceName.PathName) + @"\" + this.idatasetName_0.Name);
                    }
                    return (this.idatasetName_0.WorkspaceName.PathName + @"\" + this.idatasetName_0.Name);
                }
                return "";
            }
        }

        public bool HasChildren
        {
            get
            {
                if (this.idatasetName_0 is IRasterBandName)
                {
                    return false;
                }
                return true;
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
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog)
                {
                    return ImageLib.GetSmallImage(0x2e);
                }
                return ImageLib.GetSmallImage(0x11);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog)
                {
                    return ImageLib.GetSmallImage(0x2e);
                }
                return ImageLib.GetSmallImage(0x11);
            }
        }

        public string Name
        {
            get
            {
                try
                {
                    if (this.idatasetName_0 != null)
                    {
                        return Path.GetFileName(this.idatasetName_0.Name);
                    }
                }
                catch
                {
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
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog)
                {
                    return ImageLib.GetSmallImage(0x2e);
                }
                return ImageLib.GetSmallImage(0x11);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog)
                {
                    return ImageLib.GetSmallImage(0x2e);
                }
                return ImageLib.GetSmallImage(0x11);
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

