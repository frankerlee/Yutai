using System;
using System.Drawing;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxGDSGeodatabase : IGxObject, IGxDatabase, IGxObjectContainer, IGxObjectUI, IGxPasteTarget, IGxGeodatabase, IGxRemoteConnection
    {
        internal IDataServerManager dataServerManager = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IWorkspace iworkspace_0 = null;
        private string string_0 = "dbo.Default";
        private string string_1 = "";

        public GxGDSGeodatabase(string string_2, IDataServerManager idataServerManager_0)
        {
            this.string_1 = string_2;
            this.dataServerManager = idataServerManager_0;
        }

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            IGxObject obj3;
            if (!(igxObject_1 is IGxDataset))
            {
                return null;
            }
            if (this.igxObjectArray_0.Count == 0)
            {
                this.igxObjectArray_0.Insert(-1, igxObject_1);
                return igxObject_1;
            }
            IDatasetName datasetName = (igxObject_1 as IGxDataset).DatasetName;
            bool flag = false;
            if (datasetName.Type == esriDatasetType.esriDTFeatureDataset)
            {
                flag = true;
            }
            string strB = igxObject_1.Name.ToUpper();
            int num = 0;
            int num2 = 0;
            int count = this.igxObjectArray_0.Count;
            int num4 = 0;
            goto Label_0115;
        Label_00AC:
            num = obj3.Name.ToUpper().CompareTo(strB);
            if (num == 0)
            {
                return obj3;
            }
            if (num > 0)
            {
                count = num4;
            }
            else
            {
                num2 = num4 + 1;
            }
            if (num2 == count)
            {
                this.igxObjectArray_0.Insert(num2, igxObject_1);
                return igxObject_1;
            }
        Label_0115:
            num4 = (num2 + count) / 2;
            obj3 = this.igxObjectArray_0.Item(num4);
            if (!flag)
            {
                if ((obj3 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    num2 = num4 + 1;
                    if (num2 == count)
                    {
                        this.igxObjectArray_0.Insert(num2, igxObject_1);
                        return igxObject_1;
                    }
                    goto Label_0115;
                }
                goto Label_00AC;
            }
            if ((obj3 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
            {
                goto Label_00AC;
            }
            count = num4;
            if (num2 == count)
            {
                this.igxObjectArray_0.Insert(num2, igxObject_1);
                return igxObject_1;
            }
            goto Label_0115;
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

        public void Backup(string string_2, string string_3, string string_4, out bool bool_0)
        {
            string serverName = this.dataServerManager.ServerName;
            bool_0 = false;
            (this.dataServerManager as IDataServerManagerAdmin).IsSimpleRecoveryModel(this.string_1, ref bool_0);
            (this.dataServerManager as IDataServerManagerAdmin).BackupGeodatabase(this.string_1, string_3, string_2, string_4);
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public void Connect()
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

        public void DetachGeodatabase()
        {
            (this.dataServerManager as IDataServerManagerAdmin).DeleteGeodatabase(this.string_1);
        }

        public void Disconnect()
        {
        }

        public void GeodatabaseName(ref string string_2)
        {
            string_2 = this.string_1;
        }

        public void GetProperties(out string string_2, out object object_0, out int int_0, out string string_3, out object object_1)
        {
            (this.dataServerManager as IDataServerManagerAdmin).GetDBProperties(this.string_1, out string_3, out int_0, out string_2, out object_0, out object_1);
        }

        private void method_0(IWorkspace iworkspace_1)
        {
            try
            {
                IEnumDatasetName name = iworkspace_1.get_DatasetNames(esriDatasetType.esriDTAny);
                name.Reset();
                IDatasetName name2 = name.Next();
                IGxObject obj2 = null;
                while (name2 != null)
                {
                    obj2 = null;
                    if ((name2.Type == esriDatasetType.esriDTRasterDataset) || (name2.Type == esriDatasetType.esriDTRasterCatalog))
                    {
                        obj2 = new GxRasterDataset();
                    }
                    else if ((name2.Type == esriDatasetType.esriDTFeatureClass) || (name2.Type == esriDatasetType.esriDTTable))
                    {
                        obj2 = new GxDataset();
                    }
                    else
                    {
                        obj2 = new GxDataset();
                    }
                    if (obj2 != null)
                    {
                        (obj2 as IGxDataset).DatasetName = name2;
                        obj2.Attach(this, this.igxCatalog_0);
                    }
                    name2 = name.Next();
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_0)
        {
            return false;
        }

        public void Refresh()
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                this.igxObjectArray_0.Item(i).Refresh();
            }
        }

        public void SearchChildren(string string_2, IGxObjectArray igxObjectArray_1)
        {
        }

        public void Upgrade()
        {
            (this.dataServerManager as IDataServerManagerAdmin).UpgradeGeoDatabase(this.string_1);
        }

        public bool AreChildrenViewable
        {
            get
            {
                return true;
            }
        }

        public string BaseName
        {
            get
            {
                return string.Format("{0} ({1})", this.string_1, this.string_0);
            }
        }

        public string Category
        {
            get
            {
                return "空间数据库";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.igxObjectArray_0.Count == 0)
                {
                    IWorkspaceName name = ((IDataServerManagerAdmin) this.dataServerManager).CreateWorkspaceName(this.string_1, "VERSION", "dbo.Default");
                    this.iworkspace_0 = (name as IName).Open() as IWorkspace;
                    this.method_0(this.iworkspace_0);
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

        public object DataServerManager
        {
            get
            {
                return this.dataServerManager;
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} ({1})", this.string_1, this.string_0);
            }
        }

        public bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public IName InternalObjectName
        {
            get
            {
                return null;
            }
        }

        public bool IsConnected
        {
            get
            {
                return false;
            }
        }

        public bool IsEnterpriseGeodatabase
        {
            get
            {
                return false;
            }
        }

        public bool IsRemoteDatabase
        {
            get
            {
                return false;
            }
        }

        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return ImageLib.GetSmallImage(15);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(15);
            }
        }

        public string Name
        {
            get
            {
                return string.Format("{0} ({1})", this.string_1, this.string_0);
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

        public Bitmap SmallImage
        {
            get
            {
                return ImageLib.GetSmallImage(15);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(15);
            }
        }

        public IWorkspace Workspace
        {
            get
            {
                return null;
            }
        }

        public IWorkspaceName WorkspaceName
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}

