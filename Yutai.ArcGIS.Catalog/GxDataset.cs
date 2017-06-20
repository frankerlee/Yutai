using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private IDatasetName idatasetName_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private string string_0 = null;
        private string string_1 = null;
        private string string_2 = "";
        private SysGrants sysGrants_0 = new SysGrants(AppConfigInfo.UserID);

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
            return true;
        }

        public bool CanDelete()
        {
            bool flag = false;
            try
            {
                flag = this.Dataset.CanDelete();
            }
            catch
            {
            }
            return flag;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_2)
        {
            if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
            {
                ienumName_0.Reset();
                IName name = ienumName_0.Next();
                bool flag = false;
                while (name != null)
                {
                    if (name is IFileName)
                    {
                        return false;
                    }
                    if (name is IFeatureClassName)
                    {
                        if (flag = (name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace)
                        {
                            if ((name as IDatasetName).WorkspaceName.PathName == this.idatasetName_0.WorkspaceName.PathName)
                            {
                                bool_2 = true;
                            }
                            else
                            {
                                bool_2 = false;
                            }
                        }
                    }
                    else if (name is IFeatureDatasetName)
                    {
                        flag = true;
                        if ((name as IDatasetName).WorkspaceName.PathName == this.idatasetName_0.WorkspaceName.PathName)
                        {
                            if ((name as IDatasetName).Name == this.idatasetName_0.Name)
                            {
                                flag = false;
                            }
                            else
                            {
                                bool_2 = true;
                            }
                        }
                        else
                        {
                            bool_2 = false;
                        }
                    }
                    else
                    {
                        if (name is IWorkspaceName)
                        {
                            return false;
                        }
                        if (name is ITableName)
                        {
                            return false;
                        }
                    }
                    if (!flag)
                    {
                        return false;
                    }
                    name = ienumName_0.Next();
                }
                return true;
            }
            bool_2 = false;
            return false;
        }

        public bool CanRename()
        {
            try
            {
                bool flag = this.Dataset.CanRename();
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public void Delete()
        {
            try
            {
                this.Detach();
                IDataset dataset = this.Dataset;
                this.CanDelete();
                dataset.Delete();
            }
            catch (COMException exception)
            {
                if (exception.ErrorCode == -2147220969)
                {
                    MessageBox.Show("不是该对象的所有者，无法删除对象");
                }
            }
            catch (Exception exception2)
            {
                exception2.ToString();
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
                if (this.igxCatalog_0 != null)
                {
                    this.igxCatalog_0.ObjectDeleted(this);
                }
                if (this.igxObject_0 is IGxObjectContainer)
                {
                    (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
                }
                this.igxCatalog_0 = null;
                this.igxObject_0 = null;
            }
        }

        public void EditProperties(int int_0)
        {
            IDataset dataset = this.Dataset;
            if (dataset != null)
            {
                if (dataset is IFeatureClass)
                {
                    try
                    {
                        new frmEditObjectClass { ObjectClass = dataset as IObjectClass }.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (dataset is IObjectClass)
                {
                    new ObjectClassInfoEdit { ObjectClass = dataset as IObjectClass }.ShowDialog();
                }
                else if (dataset is IFeatureDataset)
                {
                    new frmEditFeatureDataset { FeatureDataset = dataset as IFeatureDataset }.ShowDialog();
                }
                else if (dataset.Type == esriDatasetType.esriDTTopology)
                {
                    frmPropertySheet sheet = new frmPropertySheet();
                    IPropertyPage page = new TopologyGeneralPropertyPage();
                    sheet.AddPage(page);
                    page = new TopologyClassesPropertyPage();
                    sheet.AddPage(page);
                    page = new TopologyRulesPropertyPage();
                    sheet.AddPage(page);
                    sheet.EditProperties(dataset);
                }
                else if (dataset.Type == esriDatasetType.esriDTGeometricNetwork)
                {
                    new frmGNPropertySheet { GeometricNetwork = dataset as IGeometricNetwork }.ShowDialog();
                }
                else if (dataset.Type == esriDatasetType.esriDTNetworkDataset)
                {
                    new frmNetworkPropertySheet { NetworkDataset = dataset as INetworkDataset }.ShowDialog();
                }
                dataset = null;
            }
        }

        ~GxDataset()
        {
            if (this.idatasetName_0 != null)
            {
                Marshal.ReleaseComObject(this.idatasetName_0);
            }
            this.idatasetName_0 = null;
            this.sysGrants_0 = null;
        }

        public void GetPropByIndex(int int_0, ref string string_3, ref object object_0)
        {
        }

        public object GetProperty(string string_3)
        {
            return null;
        }

        public void Init(object object_0)
        {
            this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
            this.ipopuMenuWrap_0.Clear();
            if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
            {
                this.ipopuMenuWrap_0.AddItem("Catalog_Copy", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_Paste", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_Delete", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_Rename", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_Refresh", true);
                if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    this.ipopuMenuWrap_0.ClearSubItem("ArchivingSubItem");
                    this.ipopuMenuWrap_0.AddSubmenuItem("ArchivingSubItem", "管理", "", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_RegisterAsVersion", "ArchivingSubItem", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_UnregisterVersion", "ArchivingSubItem", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_EnableArchiving", "ArchivingSubItem", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_DisableArchiving", "ArchivingSubItem", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_CreateVersionView", "ArchivingSubItem", false);
                }
                this.ipopuMenuWrap_0.ClearSubItem("NewBarSubItem");
                this.ipopuMenuWrap_0.AddSubmenuItem("NewBarSubItem", "新建", "", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewFeatureClass", "NewBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewRelationClass", "NewBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewNetworkDataset", "NewBarSubItem", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewTopology", "NewBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_NewNetwork", "NewBarSubItem", false);
                this.ipopuMenuWrap_0.ClearSubItem("ImportBarSubItem");
                this.ipopuMenuWrap_0.AddSubmenuItem("ImportBarSubItem", "导入", "", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_ImportSingleFeatureClass", "ImportBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_ImportMultiFeatureClasses", "ImportBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_ImportXY", "ImportBarSubItem", false);
                this.ipopuMenuWrap_0.ClearSubItem("ExportBarSubItem");
                this.ipopuMenuWrap_0.AddSubmenuItem("ExportBarSubItem", "导出", "", true);
                this.ipopuMenuWrap_0.AddItem("Catalog_ExportMultiFeatureClasses", "ExportBarSubItem", false);
                this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
                this.ipopuMenuWrap_0.UpdateUI();
            }
            else
            {
                IObjectClass dataset;
                if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
                {
                    this.ipopuMenuWrap_0.AddItem("Catalog_Copy", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_Delete", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_Rename", false);
                    this.ipopuMenuWrap_0.ClearSubItem("ArchivingSubItem");
                    this.ipopuMenuWrap_0.AddSubmenuItem("ArchivingSubItem", "管理", "", true);
                    if ((this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && ((this.idatasetName_0 as IFeatureClassName).FeatureDatasetName == null))
                    {
                        this.ipopuMenuWrap_0.AddItem("Catalog_RegisterAsVersion", "ArchivingSubItem", true);
                        this.ipopuMenuWrap_0.AddItem("Catalog_UnregisterVersion", "ArchivingSubItem", false);
                        this.ipopuMenuWrap_0.AddItem("Catalog_EnableArchiving", "ArchivingSubItem", false);
                        this.ipopuMenuWrap_0.AddItem("Catalog_DisableArchiving", "ArchivingSubItem", false);
                        this.ipopuMenuWrap_0.AddItem("Catalog_CreateVersionView", "ArchivingSubItem", false);
                    }
                    this.ipopuMenuWrap_0.AddItem("Catalog_CreateAttachments", "ArchivingSubItem", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_DeleteAttachments", "ArchivingSubItem", false);
                    dataset = this.Dataset as IObjectClass;
                    if (dataset.ObjectClassID == -1)
                    {
                        this.ipopuMenuWrap_0.AddItem("Catalog_RegisterAsObjectClass", "ArchivingSubItem", true);
                    }
                    this.ipopuMenuWrap_0.AddItem("Catalog_DataLoader", true);
                    this.ipopuMenuWrap_0.ClearSubItem("ExportBarSubItem");
                    this.ipopuMenuWrap_0.AddSubmenuItem("ExportBarSubItem", "导出", "", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_ExportSingleToGDB", "ExportBarSubItem", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_ExportShapefile", "ExportBarSubItem", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
                    this.ipopuMenuWrap_0.UpdateUI();
                }
                else if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
                {
                    this.ipopuMenuWrap_0.AddItem("Catalog_Copy", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_Paste", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_Delete", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_Rename", false);
                    this.ipopuMenuWrap_0.ClearSubItem("ArchivingSubItem");
                    this.ipopuMenuWrap_0.AddSubmenuItem("ArchivingSubItem", "管理", "", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_RegisterAsVersion", "ArchivingSubItem", true);
                    if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        this.ipopuMenuWrap_0.AddItem("Catalog_UnregisterVersion", "ArchivingSubItem", false);
                        this.ipopuMenuWrap_0.AddItem("Catalog_EnableArchiving", "ArchivingSubItem", false);
                        this.ipopuMenuWrap_0.AddItem("Catalog_DisableArchiving", "ArchivingSubItem", false);
                        this.ipopuMenuWrap_0.AddItem("Catalog_CreateVersionView", "ArchivingSubItem", false);
                    }
                    this.ipopuMenuWrap_0.AddItem("Catalog_CreateAttachments", "ArchivingSubItem", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_DeleteAttachments", "ArchivingSubItem", false);
                    dataset = this.Dataset as IObjectClass;
                    if (dataset.ObjectClassID == -1)
                    {
                        this.ipopuMenuWrap_0.AddItem("Catalog_RegisterAsObjectClass", "ArchivingSubItem", true);
                    }
                    this.ipopuMenuWrap_0.AddItem("Catalog_DataLoader", true);
                    this.ipopuMenuWrap_0.ClearSubItem("ExportBarSubItem");
                    this.ipopuMenuWrap_0.AddSubmenuItem("ExportBarSubItem", "导出", "", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_ExportSingleToGDB", "ExportBarSubItem", false);
                    this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
                    this.ipopuMenuWrap_0.UpdateUI();
                }
                else if (this.idatasetName_0.Type == esriDatasetType.esriDTTopology)
                {
                    this.ipopuMenuWrap_0.AddItem("Catalog_Delete", false);
                    this.ipopuMenuWrap_0.AddItem("ValidateTopologyCommand", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
                    this.ipopuMenuWrap_0.UpdateUI();
                }
                else if (this.idatasetName_0.Type == esriDatasetType.esriDTNetworkDataset)
                {
                    this.ipopuMenuWrap_0.AddItem("Catalog_Delete", false);
                    this.ipopuMenuWrap_0.AddItem("BuildNetworkCommand", true);
                    this.ipopuMenuWrap_0.AddItem("Catalog_GxObjectProperty", true);
                    this.ipopuMenuWrap_0.UpdateUI();
                }
                else
                {
                    this.ipopuMenuWrap_0.AddItem("Catalog_Delete", false);
                    this.ipopuMenuWrap_0.UpdateUI();
                }
            }
        }

        private string method_0(IDatasetName idatasetName_1)
        {
            if (idatasetName_1 != null)
            {
                bool flag = false;
                if (idatasetName_1.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    flag = true;
                }
                switch (idatasetName_1.Type)
                {
                    case esriDatasetType.esriDTFeatureDataset:
                        if (!flag)
                        {
                            if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                            {
                                return "文件型空间数据库要素集";
                            }
                            return "个人空间数据库要素集";
                        }
                        return "SDE要素集";

                    case esriDatasetType.esriDTFeatureClass:
                        if (!flag)
                        {
                            if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                            {
                                return "文件型空间数据库要素类";
                            }
                            return "个人空间数据库要素类";
                        }
                        return "SDE要素类";

                    case esriDatasetType.esriDTGeometricNetwork:
                        if (!flag)
                        {
                            if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                            {
                                return "文件型空间数据库几何网络";
                            }
                            return "个人空间数据库几何网络";
                        }
                        return "SDE几何网络";

                    case esriDatasetType.esriDTTopology:
                        if (!this.bool_0)
                        {
                            if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                            {
                                return "文件型空间数据库拓扑";
                            }
                            return "个人空间数据库拓扑";
                        }
                        return "SDE拓扑";

                    case esriDatasetType.esriDTTable:
                    {
                        string category = this.DatasetName.Category;
                        return this.Dataset.Category;
                    }
                    case esriDatasetType.esriDTRelationshipClass:
                        if (!flag)
                        {
                            if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                            {
                                return "文件型空间数据库关系类";
                            }
                            return "个人空间数据库关系类";
                        }
                        return "SDE关系类";

                    case esriDatasetType.esriDTRasterDataset:
                        if (!flag)
                        {
                            return "删格数据集";
                        }
                        return "SDE删格数据集";

                    case esriDatasetType.esriDTTin:
                        return "TIN数据集";

                    case esriDatasetType.esriDTNetworkDataset:
                        if (!flag)
                        {
                            if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                            {
                                return "文件型空间数据库网络要素集";
                            }
                            return "个人空间数据库网络要素集";
                        }
                        return "SDE网络要素集";

                    case esriDatasetType.esriDTCadastralFabric:
                        if (flag)
                        {
                            return "SDE宗地结构";
                        }
                        if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID == "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
                        {
                            return "文件型空间数据库宗地结构";
                        }
                        return "个人空间数据库宗地结构";
                }
            }
            return "";
        }

        private void method_1()
        {
            this.string_0 = "";
            this.string_1 = "";
            if (this.idatasetName_0 != null)
            {
                string pathName = this.idatasetName_0.WorkspaceName.PathName;
                if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    pathName = @"数据库连接\" + System.IO.Path.GetFileName(pathName);
                }
                this.string_0 = this.idatasetName_0.Name;
                if (this.idatasetName_0 is IFeatureClassName)
                {
                    IDatasetName featureDatasetName = (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName;
                    if (featureDatasetName != null)
                    {
                        pathName = pathName + @"\" + featureDatasetName.Name;
                    }
                }
                this.string_1 = pathName + @"\" + this.string_0;
            }
        }

        private void method_2()
        {
            try
            {
                IEnumDatasetName subsetNames = this.idatasetName_0.SubsetNames;
                subsetNames.Reset();
                IDatasetName name2 = subsetNames.Next();
                IGxObject obj2 = null;
                while (name2 != null)
                {
                    obj2 = null;
                    if (this.bool_0)
                    {
                        if (AppConfigInfo.UserID == "admin")
                        {
                            obj2 = new GxDataset();
                        }
                        else if (AppConfigInfo.UserID.Length > 0)
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
                    if (obj2 != null)
                    {
                        (obj2 as IGxDataset).DatasetName = name2;
                        obj2.Attach(this, this.igxCatalog_0);
                    }
                    name2 = subsetNames.Next();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误");
            }
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_2)
        {
            try
            {
                if (bool_2)
                {
                    ienumName_0.Reset();
                    IName name = ienumName_0.Next();
                    if (((name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace) && ((name as IDatasetName).WorkspaceName.PathName == this.idatasetName_0.WorkspaceName.PathName))
                    {
                        IDatasetContainer container = (this.idatasetName_0 as IName).Open() as IDatasetContainer;
                        if (container != null)
                        {
                            while (name != null)
                            {
                                IGxObject obj2;
                                if (name is IFeatureDatasetName)
                                {
                                    IEnumDatasetName subsetNames = (name as IDatasetName).SubsetNames;
                                    subsetNames.Reset();
                                    for (IDatasetName name3 = subsetNames.Next(); name3 != null; name3 = subsetNames.Next())
                                    {
                                        container.AddDataset((name3 as IName).Open() as IDataset);
                                        obj2 = new GxDataset();
                                        if (name3 is IFeatureClassName)
                                        {
                                            (name3 as IFeatureClassName).FeatureDatasetName = this.idatasetName_0;
                                        }
                                        else if (name3 is IRelationshipClassName)
                                        {
                                            (name3 as IRelationshipClassName).FeatureDatasetName = this.idatasetName_0;
                                        }
                                        else if (name3 is IGeometricNetworkName)
                                        {
                                            (name3 as IGeometricNetworkName).FeatureDatasetName = this.idatasetName_0;
                                        }
                                        (obj2 as IGxDataset).DatasetName = name3;
                                        obj2.Attach(this, this.igxCatalog_0);
                                        this.igxCatalog_0.ObjectAdded(obj2);
                                    }
                                }
                                else
                                {
                                    container.AddDataset(name.Open() as IDataset);
                                    obj2 = new GxDataset();
                                    if (name is IFeatureClassName)
                                    {
                                        (name as IFeatureClassName).FeatureDatasetName = this.idatasetName_0;
                                    }
                                    else if (name is IRelationshipClassName)
                                    {
                                        (name as IRelationshipClassName).FeatureDatasetName = this.idatasetName_0;
                                    }
                                    else if (name is IGeometricNetworkName)
                                    {
                                        (name as IGeometricNetworkName).FeatureDatasetName = this.idatasetName_0;
                                    }
                                    (obj2 as IGxDataset).DatasetName = name as IDatasetName;
                                    obj2.Attach(this, this.igxCatalog_0);
                                    this.igxCatalog_0.ObjectAdded(obj2);
                                }
                                name = ienumName_0.Next();
                            }
                            return true;
                        }
                    }
                }
                else
                {
                    IEnumNameMapping mapping;
                    ienumName_0.Reset();
                    IGeoDBDataTransfer transfer = new GeoDBDataTransferClass();
                    transfer.GenerateNameMapping(ienumName_0, this.idatasetName_0.WorkspaceName as IName, out mapping);
                    frmGeoDBDataTransfer transfer2 = new frmGeoDBDataTransfer {
                        EnumNameMapping = mapping,
                        ToName = this.idatasetName_0 as IName,
                        GeoDBTransfer = transfer
                    };
                    if (transfer2.ShowDialog() == DialogResult.OK)
                    {
                        this.Refresh();
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
            return false;
        }

        public void Refresh()
        {
            if (this.igxCatalog_0 != null)
            {
                if ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer) || (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset))
                {
                    this.igxObjectArray_0.Empty();
                    GC.Collect();
                    this.method_2();
                }
                this.igxCatalog_0.ObjectRefreshed(this);
            }
        }

        public void Rename(string string_3)
        {
            try
            {
                if (string_3 != null)
                {
                    if (string_3.Trim().Length == 0)
                    {
                        MessageBox.Show("必须键入数据名!");
                        this.igxCatalog_0.ObjectChanged(this);
                    }
                    else
                    {
                        IDataset o = this.Dataset;
                        o.Rename(string_3);
                        Marshal.ReleaseComObject(o);
                        o = null;
                        this.idatasetName_0.Name = string_3;
                        this.Refresh();
                        this.method_1();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误");
            }
        }

        public void SearchChildren(string string_3, IGxObjectArray igxObjectArray_1)
        {
        }

        public void SetProperty(string string_3, object object_0)
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
                return ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer) || (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset));
            }
        }

        public string BaseName
        {
            get
            {
                if (this.idatasetName_0 == null)
                {
                    return "";
                }
                return this.idatasetName_0.Name;
            }
        }

        public string Category
        {
            get
            {
                return this.string_2;
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.HasChildren && (this.igxObjectArray_0.Count == 0))
                {
                    this.method_2();
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
                if (this.idatasetName_0 != null)
                {
                    try
                    {
                        return ((this.idatasetName_0 as IName).Open() as IDataset);
                    }
                    catch (Exception exception)
                    {
                        this.bool_1 = false;
                        if (exception is COMException)
                        {
                            switch (((uint) (exception as COMException).ErrorCode))
                            {
                                case 0x80041352:
                                case 0x80041538:
                                    throw new Exception("打开对象类错误!");
                            }
                            Logger.Current.Error("",exception, "");
                        }
                        else
                        {
                            MessageBox.Show(exception.Message, "错误");
                        }
                    }
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
                if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    this.bool_0 = true;
                }
                this.method_1();
                this.string_2 = this.idatasetName_0.Category;
            }
        }

        public string FullName
        {
            get
            {
                if (this.idatasetName_0 == null)
                {
                    return "";
                }
                string pathName = this.idatasetName_0.WorkspaceName.PathName;
                if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    pathName = @"Database Connections\" + System.IO.Path.GetFileName(pathName);
                }
                if (this.idatasetName_0 is IFeatureClassName)
                {
                    IDatasetName featureDatasetName = (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName;
                    if (featureDatasetName != null)
                    {
                        pathName = pathName + @"\" + featureDatasetName.Name;
                    }
                }
                return (pathName + @"\" + this.idatasetName_0.Name);
            }
        }

        public bool HasChildren
        {
            get
            {
                return ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer) || (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset));
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
                return (this.idatasetName_0 != null);
            }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get
            {
                return null;
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
                if (this.idatasetName_0 == null)
                {
                    return "";
                }
                return this.idatasetName_0.Name;
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
                if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    return ImageLib.GetSmallImage(0x12);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
                {
                    return ImageLib.GetSmallImage(0x13);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRelationshipClass)
                {
                    return ImageLib.GetSmallImage(30);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTTopology)
                {
                    return ImageLib.GetSmallImage(0x1d);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTGeometricNetwork)
                {
                    return ImageLib.GetSmallImage(0x26);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTCadastralFabric)
                {
                    return ImageLib.GetSmallImage(0x4e);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName name = this.idatasetName_0 as IFeatureClassName;
                    if (name.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        return ImageLib.GetSmallImage(0x27);
                    }
                    switch (name.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                        case esriGeometryType.esriGeometryMultipoint:
                            return ImageLib.GetSmallImage(20);

                        case esriGeometryType.esriGeometryPolyline:
                        case esriGeometryType.esriGeometryPath:
                        case esriGeometryType.esriGeometryRay:
                            return ImageLib.GetSmallImage(0x15);

                        case esriGeometryType.esriGeometryPolygon:
                            return ImageLib.GetSmallImage(0x16);

                        case esriGeometryType.esriGeometryAny:
                            return ImageLib.GetSmallImage(0x4f);
                    }
                }
                else
                {
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
                    {
                        return ImageLib.GetSmallImage(20);
                    }
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTTin)
                    {
                        return ImageLib.GetSmallImage(0x2f);
                    }
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTNetworkDataset)
                    {
                        return ImageLib.GetSmallImage(0x49);
                    }
                }
                return ImageLib.GetSmallImage(20);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    return ImageLib.GetSmallImage(0x12);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTCadastralFabric)
                {
                    return ImageLib.GetSmallImage(0x4e);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
                {
                    return ImageLib.GetSmallImage(0x13);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTRelationshipClass)
                {
                    return ImageLib.GetSmallImage(30);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTTopology)
                {
                    return ImageLib.GetSmallImage(0x1d);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTGeometricNetwork)
                {
                    return ImageLib.GetSmallImage(0x26);
                }
                if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName name = this.idatasetName_0 as IFeatureClassName;
                    if (name.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        return ImageLib.GetSmallImage(0x27);
                    }
                    switch (name.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                        case esriGeometryType.esriGeometryMultipoint:
                            return ImageLib.GetSmallImage(20);

                        case esriGeometryType.esriGeometryPolyline:
                        case esriGeometryType.esriGeometryPath:
                        case esriGeometryType.esriGeometryRay:
                            return ImageLib.GetSmallImage(0x15);

                        case esriGeometryType.esriGeometryPolygon:
                            return ImageLib.GetSmallImage(0x16);

                        case esriGeometryType.esriGeometryAny:
                            return ImageLib.GetSmallImage(0x4f);
                    }
                }
                else
                {
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
                    {
                        return ImageLib.GetSmallImage(20);
                    }
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTTin)
                    {
                        return ImageLib.GetSmallImage(0x2f);
                    }
                    if (this.idatasetName_0.Type == esriDatasetType.esriDTNetworkDataset)
                    {
                        return ImageLib.GetSmallImage(0x49);
                    }
                }
                return ImageLib.GetSmallImage(20);
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

