using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;
using WorkspaceOperator = Yutai.ArcGIS.Common.BaseClasses.WorkspaceOperator;

namespace Yutai.ArcGIS.Catalog
{
    public class GxDatabase : IGxObject, IGxDatabase, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName,
        IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxRemoteConnection
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private IWorkspace iworkspace_0 = null;
        private IWorkspaceName iworkspaceName_0;
        private string string_0;
        private SysGrants sysGrants_0;

        public GxDatabase()
        {
            if ((AppConfigInfo.UserID.Length > 0) && (AppConfigInfo.UserID != "admin"))
            {
                this.sysGrants_0 = new SysGrants(AppConfigInfo.UserID);
            }
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
            num4 = (num2 + count)/2;
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

        public IGxObject AddChild1(IGxObject igxObject_1)
        {
            if (!(igxObject_1 is IGxDataset))
            {
                return null;
            }
            IDatasetName datasetName = (igxObject_1 as IGxDataset).DatasetName;
            bool flag = false;
            if (datasetName.Type == esriDatasetType.esriDTFeatureDataset)
            {
                flag = true;
            }
            string strB = igxObject_1.Name.ToUpper();
            int num = 0;
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                IGxObject obj2 = this.igxObjectArray_0.Item(i);
                num = obj2.Name.ToUpper().CompareTo(strB);
                if (obj2.Category == igxObject_1.Category)
                {
                    if (num == 0)
                    {
                        return obj2;
                    }
                    if (num > 0)
                    {
                        this.igxObjectArray_0.Insert(i, igxObject_1);
                        return igxObject_1;
                    }
                }
                else if (flag && ((obj2 as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureDataset))
                {
                    this.igxObjectArray_0.Insert(i, igxObject_1);
                    return igxObject_1;
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
            if (this.iworkspaceName_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
            }
        }

        public bool CanCopy()
        {
            return true;
        }

        public bool CanDelete()
        {
            return true;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_2)
        {
            if (this.Category == "OLE DB连接")
            {
                bool_2 = false;
                return false;
            }
            ienumName_0.Reset();
            IName name = ienumName_0.Next();
            bool flag2 = false;
            while (name != null)
            {
                if (name is IFeatureClassName)
                {
                    flag2 = true;
                    if (((name as IDatasetName).WorkspaceName.PathName == this.iworkspaceName_0.PathName) &&
                        ((name as IFeatureClassName).FeatureDatasetName != null))
                    {
                        bool_2 = true;
                    }
                    else
                    {
                        bool_2 = false;
                    }
                }
                else if (name is ITableName)
                {
                    flag2 = (name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace;
                    bool_2 = false;
                }
                else
                {
                    if (!(name is IFeatureDatasetName))
                    {
                        bool_2 = false;
                        return false;
                    }
                    if ((name as IDatasetName).WorkspaceName.PathName == this.iworkspaceName_0.PathName)
                    {
                        flag2 = false;
                    }
                    else
                    {
                        bool_2 = false;
                        flag2 = true;
                    }
                }
                if (!flag2)
                {
                    bool_2 = false;
                    return false;
                }
                name = ienumName_0.Next();
            }
            return true;
        }

        public bool CanRename()
        {
            if (this.iworkspace_0 != null)
            {
                return (this.iworkspace_0 as IDataset).CanRename();
            }
            return true;
        }

        public void Connect()
        {
            try
            {
                object obj2;
                object obj3;
                this.iworkspaceName_0.ConnectionProperties.GetAllProperties(out obj2, out obj3);
                this.iworkspace_0 = (this.iworkspaceName_0 as IName).Open() as IWorkspace;
                this.iworkspace_0.ConnectionProperties.GetAllProperties(out obj2, out obj3);
                if (((this.sysGrants_0 == null) &&
                     (this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)) &&
                    ((AppConfigInfo.UserID.Length > 0) && (AppConfigInfo.UserID != "admin")))
                {
                    this.sysGrants_0 = new SysGrants(AppConfigInfo.UserID);
                }
                this.method_0(this.iworkspace_0);
            }
            catch (Exception exception)
            {
                if (exception is COMException)
                {
                    switch (((uint) (exception as COMException).ErrorCode))
                    {
                        case 2147751273:
                        case 2147751169:
                            MessageBox.Show("连接数据库失败", "连接");
                            return;

                        case 2147751274:
                            MessageBox.Show("连接数据库失败\r\n该服务器上的SDE没有启动", "连接");
                            return;

                        case 2147811737:
                            MessageBox.Show("连接的数据库服务器未启动", "连接");
                            return;

                        case 2147751224:
                            MessageBox.Show("连接的数据库服务器已暂停", "连接");
                            return;

                        case 2147500037:
                        case 2147811652:
                        case 2147751178:
                            MessageBox.Show("连接数据库失败", "连接");
                            return;
                    }
                }
                Logger.Current.Error("", exception, "");
            }
        }

        public void Delete()
        {
            try
            {
                if (this.IsRemoteDatabase)
                {
                    (this.iworkspaceName_0.WorkspaceFactory as IRemoteDatabaseWorkspaceFactory).DeleteConnectionFile(
                        this.string_0);
                }
                else
                {
                    this.igxObjectArray_0.Empty();
                    if (this.iworkspace_0 != null)
                    {
                        Marshal.ReleaseComObject(this.iworkspace_0);
                        this.iworkspace_0 = null;
                        GC.Collect();
                    }
                    string pathName = this.iworkspaceName_0.PathName;
                    if (this.iworkspaceName_0.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                    {
                        Directory.Delete(pathName, true);
                    }
                    else
                    {
                        File.Delete(pathName);
                    }
                }
                this.Detach();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

        public void Disconnect()
        {
            this.igxObjectArray_0.Empty();
            if (this.iworkspace_0 != null)
            {
                Marshal.ReleaseComObject(this.iworkspace_0);
                this.iworkspace_0 = null;
                GC.Collect();
            }
        }

        public void EditProperties(int int_0)
        {
            if (this.iworkspace_0 == null)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                this.Connect();
                if (this.iworkspace_0 == null)
                {
                    return;
                }
                this.igxCatalog_0.ObjectChanged(this);
                this.igxCatalog_0.ObjectRefreshed(this);
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            if (!(this.iworkspace_0 is IOleDBConnectionInfo))
            {
                new frmGDBInfo {Workspace = this.iworkspace_0}.ShowDialog();
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
            bool flag = this.bool_1;
            if (this.IsRemoteDatabase)
            {
                if (!this.bool_0)
                {
                    this.bool_0 = true;
                    if (this.iworkspace_0 == null)
                    {
                        flag = WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspaceName_0);
                    }
                    else
                    {
                        flag = WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspace_0);
                    }
                    this.bool_1 = flag;
                }
            }
            else
            {
                flag = true;
            }
            this.ipopuMenuWrap_0.AddItem("Catalog_Copy", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_Paste", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_Refresh", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_Delete", true);
            this.ipopuMenuWrap_0.AddItem("Catalog_Rename", false);
            this.ipopuMenuWrap_0.ClearSubItem("NewBarSubItem");
            this.ipopuMenuWrap_0.AddSubmenuItem("NewBarSubItem", "新建", "", true);
            if (flag)
            {
                this.ipopuMenuWrap_0.AddItem("Catalog_NewFeatureDataset", "NewBarSubItem", false);
            }
            this.ipopuMenuWrap_0.AddItem("Catalog_NewFeatureClass", "NewBarSubItem", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_NewTable", "NewBarSubItem", false);
            if (flag)
            {
                this.ipopuMenuWrap_0.AddItem("Catalog_NewRelationClass", "NewBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewRasterFolder", "NewBarSubItem", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewRasterDataset", "NewBarSubItem", false);
            }
            this.ipopuMenuWrap_0.ClearSubItem("ImportBarSubItem");
            this.ipopuMenuWrap_0.AddSubmenuItem("ImportBarSubItem", "导入", "", true);
            this.ipopuMenuWrap_0.AddItem("Catalog_ImportSingleFeatureClass", "ImportBarSubItem", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_ImportMultiFeatureClasses", "ImportBarSubItem", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_ImportSingleTable", "ImportBarSubItem", false);
            this.ipopuMenuWrap_0.AddItem("Catalog_ImportMultiTables", "ImportBarSubItem", false);
            if (flag)
            {
                this.ipopuMenuWrap_0.AddItem("Catalog_RasterToGDB", "ImportBarSubItem", false);
            }
            this.ipopuMenuWrap_0.AddItem("Catalog_ImportXY", "ImportBarSubItem", false);
            if (this.IsRemoteDatabase)
            {
                if (!flag)
                {
                    this.ipopuMenuWrap_0.AddItem("Catalog_EnableSDE", true);
                }
                this.ipopuMenuWrap_0.AddItem("Catalog_VersionInfo", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_Connection", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_Disconnection", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_ConnectionProperty", false);
            }
            this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
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
                    if (this.IsEnterpriseGeodatabase)
                    {
                        if ((name2.Type == esriDatasetType.esriDTRasterDataset) ||
                            (name2.Type == esriDatasetType.esriDTRasterCatalog))
                        {
                            obj2 = new GxRasterDataset();
                        }
                        else if ((name2.Type == esriDatasetType.esriDTFeatureClass) ||
                                 (name2.Type == esriDatasetType.esriDTTable))
                        {
                            if (AppConfigInfo.UserID.Length > 0)
                            {
                                if (AppConfigInfo.UserID.ToLower() == "admin")
                                {
                                    obj2 = new GxDataset();
                                }
                                else if (this.sysGrants_0 != null)
                                {
                                    if (this.sysGrants_0.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 1, name2.Name))
                                    {
                                        obj2 = new GxDataset();
                                    }
                                }
                                else
                                {
                                    obj2 = new GxDataset();
                                }
                            }
                            else
                            {
                                obj2 = new GxDataset();
                            }
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
                    }
                    else
                    {
                        if ((name2.Type == esriDatasetType.esriDTRasterDataset) ||
                            (name2.Type == esriDatasetType.esriDTRasterCatalog))
                        {
                            obj2 = new GxRasterDataset();
                        }
                        else if ((name2.Type == esriDatasetType.esriDTFeatureClass) ||
                                 (name2.Type == esriDatasetType.esriDTTable))
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
                    }
                    name2 = name.Next();
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_2)
        {
            if (this.Category == "OLE DB连接")
            {
                bool_2 = false;
                return false;
            }
            bool flag2 = true;
            try
            {
                if (bool_2)
                {
                    ienumName_0.Reset();
                    IName name = ienumName_0.Next();
                    if (((name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace) &&
                        ((name as IDatasetName).WorkspaceName.PathName == this.iworkspaceName_0.PathName))
                    {
                        IDatasetContainer container = (this.iworkspaceName_0 as IName).Open() as IDatasetContainer;
                        while (name != null)
                        {
                            container.AddDataset(name.Open() as IDataset);
                            (name as IFeatureClassName).FeatureDatasetName = null;
                            IGxObject obj2 = new GxDataset();
                            (name as IDatasetName).WorkspaceName = this.iworkspaceName_0;
                            (obj2 as IGxDataset).DatasetName = name as IDatasetName;
                            obj2.Attach(this, this.igxCatalog_0);
                            this.igxCatalog_0.ObjectAdded(obj2);
                            name = ienumName_0.Next();
                        }
                        return true;
                    }
                }
                else
                {
                    IEnumNameMapping mapping;
                    flag2 = false;
                    IGeoDBDataTransfer transfer = new MyGeoDBDataTransfer();
                    transfer.GenerateNameMapping(ienumName_0, this.iworkspaceName_0 as IName, out mapping);
                    frmGeoDBDataTransfer transfer2 = new frmGeoDBDataTransfer
                    {
                        EnumNameMapping = mapping,
                        ToName = this.iworkspaceName_0 as IName,
                        GeoDBTransfer = transfer
                    };
                    if (transfer2.ShowDialog() == DialogResult.OK)
                    {
                        this.Refresh();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
            return flag2;
        }

        public void Refresh()
        {
            this.bool_0 = false;
            if (this.IsConnected)
            {
                this.igxObjectArray_0.Empty();
                this.iworkspace_0 = null;
                this.iworkspace_0 = (this.iworkspaceName_0 as IName).Open() as IWorkspace;
                this.method_0(this.iworkspace_0);
                this.igxCatalog_0.ObjectRefreshed(this);
            }
        }

        public void Rename(string string_1)
        {
            if (string_1 != null)
            {
                string extension = Path.GetExtension(string_1);
                string str2 = Path.GetFileNameWithoutExtension(string_1).Trim();
                if (str2.Length == 0)
                {
                    MessageBox.Show("必须键入文件名!");
                    this.igxCatalog_0.ObjectChanged(this);
                }
                else
                {
                    string str4;
                    Exception exception;
                    if (this.IsRemoteDatabase)
                    {
                        string str3 = ".sde";
                        if (this.iworkspaceName_0.WorkspaceFactory is IOleDBConnectionInfo)
                        {
                            str3 = ".odc";
                        }
                        if (extension.ToLower() != str3)
                        {
                            string_1 = str2 + str3;
                        }
                        try
                        {
                            str4 = Path.Combine(Path.GetDirectoryName(this.string_0), string_1);
                            if (File.Exists(str4))
                            {
                                MessageBox.Show("已存在同名空间数据库连接，请重新指定其他名字");
                            }
                            else
                            {
                                (this.iworkspaceName_0.WorkspaceFactory as IRemoteDatabaseWorkspaceFactory)
                                    .RenameConnectionFile(this.string_0, string_1);
                                this.string_0 = str4;
                                this.iworkspaceName_0.PathName = this.string_0;
                                this.igxCatalog_0.ObjectChanged(this);
                            }
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            MessageBox.Show(exception.Message);
                        }
                    }
                    else
                    {
                        if ((this.Category == "个人空间数据库") && (extension.ToLower() != ".mdb"))
                        {
                            string_1 = string_1 + ".mdb";
                        }
                        IDataset dataset = this.iworkspace_0 as IDataset;
                        try
                        {
                            str4 = Path.Combine(Path.GetDirectoryName(this.string_0), string_1);
                            if (this.Category == "个人空间数据库")
                            {
                                if (File.Exists(str4))
                                {
                                    MessageBox.Show("已存在同名个人数据库，请重新指定其他名字");
                                    return;
                                }
                            }
                            else if (Directory.Exists(str4))
                            {
                                MessageBox.Show("已存在同名文件型数据库，请重新指定其他名字");
                                return;
                            }
                            dataset.Rename(string_1);
                            if (str4.ToLower() != this.string_0.ToLower())
                            {
                                this.string_0 = str4;
                                this.iworkspaceName_0.PathName = this.string_0;
                                this.igxCatalog_0.ObjectChanged(this);
                            }
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                        }
                    }
                }
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
            get { return (this.igxObjectArray_0.Count > 0); }
        }

        public string BaseName
        {
            get { return Path.GetFileNameWithoutExtension(this.string_0); }
        }

        public string Category
        {
            get
            {
                if (this.iworkspaceName_0 == null)
                {
                    return "错误的数据库连接";
                }
                if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    if (this.iworkspaceName_0.WorkspaceFactory is IOleDBConnectionInfo)
                    {
                        return "OLE DB连接";
                    }
                    return "空间数据库连接";
                }
                if (this.iworkspaceName_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    if (this.iworkspaceName_0.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                    {
                        return "文件型空间数据库";
                    }
                    return "个人空间数据库";
                }
                return "";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (!this.IsRemoteDatabase && !this.IsConnected)
                {
                    this.Connect();
                }
                return (this.igxObjectArray_0 as IEnumGxObject);
            }
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
            get
            {
                if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    return (@"Database Connections\" + Path.GetFileName(this.string_0));
                }
                return this.string_0;
            }
        }

        public bool HasChildren
        {
            get { return true; }
        }

        public IName InternalObjectName
        {
            get { return (this.iworkspaceName_0 as IName); }
        }

        public bool IsConnected
        {
            get
            {
                Exception exception;
                if (this.iworkspace_0 == null)
                {
                    return false;
                }
                if (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    return true;
                }
                try
                {
                    if (this.iworkspace_0.WorkspaceFactory is ISetDefaultConnectionInfo)
                    {
                        IWorkspaceFactoryStatus status = new SdeWorkspaceFactoryClass();
                        IWorkspaceStatus wstatus = status.PingWorkspaceStatus(this.iworkspace_0);
                        if (wstatus.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSAvailable)
                        {
                            try
                            {
                                this.iworkspace_0 = null;
                                this.iworkspace_0 = status.OpenAvailableWorkspace(wstatus);
                                return true;
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                Logger.Current.Error("", exception, "");
                                goto Label_010B;
                            }
                        }
                        if (wstatus.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSDown)
                        {
                            try
                            {
                                this.iworkspace_0 = null;
                                this.iworkspace_0 = status.OpenAvailableWorkspace(wstatus);
                                return true;
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                                Logger.Current.Error("", exception, "");
                                goto Label_010B;
                            }
                        }
                        if (wstatus.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSUp)
                        {
                            return true;
                        }
                    }
                    else if (this.iworkspace_0.WorkspaceFactory is IOleDBConnectionInfo)
                    {
                        return true;
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                    Logger.Current.Error("", exception, "");
                }
                Label_010B:
                return false;
            }
        }

        public bool IsEnterpriseGeodatabase
        {
            get
            {
                if (this.iworkspaceName_0 == null)
                {
                    return false;
                }
                return (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace);
            }
        }

        public bool IsGeoDatabase
        {
            get
            {
                bool flag = this.bool_1;
                if (this.IsRemoteDatabase)
                {
                    if (!this.bool_0)
                    {
                        this.bool_0 = true;
                        if (this.iworkspace_0 == null)
                        {
                            flag = WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspaceName_0);
                        }
                        else
                        {
                            flag = WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspace_0);
                        }
                        this.bool_1 = flag;
                    }
                    return flag;
                }
                return true;
            }
        }

        public bool IsRemoteDatabase
        {
            get
            {
                if (this.iworkspaceName_0 == null)
                {
                    return false;
                }
                return (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace);
            }
        }

        public bool IsValid
        {
            get { return (this.iworkspaceName_0 != null); }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get { return (this.iworkspaceName_0 as IName); }
            set { }
        }

        public Bitmap LargeImage
        {
            get
            {
                if (this.iworkspaceName_0 != null)
                {
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (this.IsConnected)
                        {
                            return ImageLib.GetSmallImage(10);
                        }
                        return ImageLib.GetSmallImage(9);
                    }
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        return ImageLib.GetSmallImage(15);
                    }
                }
                return null;
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                if (this.iworkspaceName_0 != null)
                {
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (this.IsConnected)
                        {
                            return ImageLib.GetSmallImage(10);
                        }
                        return ImageLib.GetSmallImage(9);
                    }
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        return ImageLib.GetSmallImage(15);
                    }
                }
                return null;
            }
        }

        public string Name
        {
            get
            {
                Path.GetExtension(this.string_0).ToLower();
                return Path.GetFileName(this.string_0);
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

        public int PropertyCount
        {
            get { return 0; }
        }

        public Bitmap SmallImage
        {
            get
            {
                if (this.iworkspaceName_0 != null)
                {
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (this.IsConnected)
                        {
                            return ImageLib.GetSmallImage(10);
                        }
                        return ImageLib.GetSmallImage(9);
                    }
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        return ImageLib.GetSmallImage(15);
                    }
                }
                return null;
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.iworkspaceName_0 != null)
                {
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (this.IsConnected)
                        {
                            return ImageLib.GetSmallImage(10);
                        }
                        return ImageLib.GetSmallImage(9);
                    }
                    if (this.iworkspaceName_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        return ImageLib.GetSmallImage(15);
                    }
                }
                return null;
            }
        }

        public IWorkspace Workspace
        {
            get { return this.iworkspace_0; }
        }

        public IWorkspaceName WorkspaceName
        {
            get { return this.iworkspaceName_0; }
            set
            {
                this.iworkspaceName_0 = value;
                this.string_0 = this.iworkspaceName_0.PathName;
            }
        }
    }
}