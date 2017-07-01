using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Catalog.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateFeatureClass : YutaiCommand
    {
        public CmdCreateFeatureClass(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_featureclass;
            this.m_caption = "新建要素类";
            this.m_category = "Catalog";
            this.m_message = "新建要素类";
            this.m_name = "Catalog_NewFeatureClass";
            this._key = "Catalog_NewFeatureClass";
            this.m_toolTip = "新建要素类";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmNewObjectClass frmNewObjectClass = new frmNewObjectClass();
            IObjectClass objectClass = null;
            IGxSelection gxSelection = _context.GxSelection as IGxSelection;
            if (gxSelection.FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(gxSelection.FirstObject as IGxDatabase);
                if ((gxSelection.FirstObject as IGxDatabase).Workspace == null)
                {
                    return;
                }
                frmNewObjectClass.Workspace = (gxSelection.FirstObject as IGxDatabase).Workspace;
                if (frmNewObjectClass.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    objectClass = frmNewObjectClass.ObjectClass;
                }
            }
            else if (gxSelection.FirstObject is IGxDataset &&
                     (gxSelection.FirstObject as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
            {
                try
                {
                    frmNewObjectClass.Workspace = (gxSelection.FirstObject as IGxDataset).Dataset;
                    if (frmNewObjectClass.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        objectClass = frmNewObjectClass.ObjectClass;
                    }
                }
                catch
                {
                    MessageService.Current.Warn("该要素集有问题，不能新建要素类!");
                }
            }
            if (objectClass != null)
            {
                gxSelection.FirstObject.Refresh();
            }
        }
    }


    class CmdCreateFeatureDataset : YutaiCommand
    {
        public CmdCreateFeatureDataset(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_dataset;
            this.m_caption = "创建要素集";
            this.m_category = "Catalog";
            this.m_message = "创建要素集";
            this.m_name = "Catalog_NewFeatureDataset";
            this._key = "Catalog_NewFeatureDataset";
            this.m_toolTip = "创建要素集";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return _context.GxSelection != null; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace != null)
                {
                    frmNewFeatureDataset _frmNewFeatureDataset = new frmNewFeatureDataset()
                    {
                        Workspace = (((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace
                    };
                    try
                    {
                        if (_frmNewFeatureDataset.ShowDialog() == DialogResult.OK)
                        {
                            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }


    class CmdCreateTable : YutaiCommand
    {
        public CmdCreateTable(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "创建表";
            this.m_category = "Catalog";
            this.m_message = "创建表";
            this.m_name = "Catalog_NewTable";
            this._key = "Catalog_NewTable";
            this.m_toolTip = "创建表";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.GxSelection != null)
                {
                    IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
                    if (firstObject != null)
                    {
                        flag = (!(firstObject is IGxDatabase) ? false : true);
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmObjectClass _frmObjectClass = new frmObjectClass()
            {
                UseType = enumUseType.enumUTObjectClass
            };
            IObjectClass objectClass = null;
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace == null)
                {
                    return;
                }
                _frmObjectClass.Dataset = (((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace;
                if (_frmObjectClass.ShowDialog() == DialogResult.OK)
                {
                    objectClass = _frmObjectClass.ObjectClass;
                }
            }
            if (objectClass != null)
            {
                ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
            }
        }
    }


    class CmdCreateRasterFolder : YutaiCommand
    {
        public CmdCreateRasterFolder(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "新建栅格目录";
            this.m_category = "Catalog";
            this.m_message = "新建栅格目录";
            this.m_name = "Catalog_NewRasterFolder";
            this._key = "Catalog_NewRasterFolder";
            this.m_toolTip = "新建栅格目录";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (_context.GxSelection != null ? ((IGxSelection) _context.GxSelection).FirstObject != null : false);
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace != null)
                {
                    frmCreateRasterFolder _frmCreateRasterFolder = new frmCreateRasterFolder()
                    {
                        OutLocation = ((IGxSelection) _context.GxSelection).FirstObject
                    };
                    try
                    {
                        if (_frmCreateRasterFolder.ShowDialog() == DialogResult.OK)
                        {
                            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                        }
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                }
            }
        }
    }


    class CmdCreateRasterDataset : YutaiCommand
    {
        public CmdCreateRasterDataset(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "新建栅格数据集";
            this.m_category = "Catalog";
            this.m_message = "新建栅格数据集";
            this.m_name = "Catalog_NewRasterDataset";
            this._key = "Catalog_NewRasterDataset";
            this.m_toolTip = "新建栅格数据集";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (_context.GxSelection != null ? ((IGxSelection) _context.GxSelection).FirstObject != null : false);
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace != null)
                {
                    frmCreateGDBRasterDataset _frmCreateGDBRasterDataset = new frmCreateGDBRasterDataset()
                    {
                        OutLocation = ((IGxSelection) _context.GxSelection).FirstObject
                    };
                    try
                    {
                        if (_frmCreateGDBRasterDataset.ShowDialog() == DialogResult.OK)
                        {
                            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                        }
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                }
            }
        }
    }


    class CmdImportTable : YutaiCommand
    {
        public CmdImportTable(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "导入单个表";
            this.m_category = "Catalog";
            this.m_message = "导入单个表";
            this.m_name = "Catalog_ImportSingleTable";
            this._key = "Catalog_ImportSingleTable";
            this.m_toolTip = "导入单个表";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmDataConvert _frmDataConvert = new frmDataConvert()
            {
                OutGxObject = ((IGxSelection) _context.GxSelection).FirstObject,
                ImportDatasetType = esriDatasetType.esriDTTable
            };
            _frmDataConvert.ShowDialog();
        }
    }


    class CmdImportTables : YutaiCommand
    {
        public CmdImportTables(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "入多个表";
            this.m_category = "Catalog";
            this.m_message = "入多个表";
            this.m_name = "Catalog_ImportMultiTables";
            this._key = "Catalog_ImportMultiTables";
            this.m_toolTip = "入多个表";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmMultiDataConvert _frmMultiDataConvert = new frmMultiDataConvert();
            _frmMultiDataConvert.Clear();
            _frmMultiDataConvert.Add(new MyGxFilterTables());
            _frmMultiDataConvert.OutGxObject = ((IGxSelection) _context.GxSelection).FirstObject;
            _frmMultiDataConvert.ShowDialog();
        }
    }


    class CmdRasterToGDB : YutaiCommand
    {
        public CmdRasterToGDB(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "栅格";
            this.m_category = "Catalog";
            this.m_message = "导入栅格数据集";
            this.m_name = "Catalog_RasterToGDB";
            this._key = "Catalog_RasterToGDB";
            this.m_toolTip = "导入栅格数据集";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmRasterLoad _frmRasterLoad = new frmRasterLoad()
            {
                OutGxObject = ((IGxSelection) _context.GxSelection).FirstObject
            };
            _frmRasterLoad.ShowDialog();
            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
        }
    }


    class CmdEGDBStart : YutaiCommand
    {
        public CmdEGDBStart(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "启用企业地理数据库";
            this.m_category = "Catalog";
            this.m_message = "启用企业地理数据库";
            this.m_name = "Catalog_EnableSDE";
            this._key = "Catalog_EnableSDE";
            this.m_toolTip = "启用企业地理数据库";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.GxSelection != null)
                {
                    flag = (!(((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase) ||
                            !(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).IsRemoteDatabase
                        ? false
                        : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            try
            {
                frmEnableGeodatabase _frmEnableGeodatabase = new frmEnableGeodatabase();
                if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
                {
                    _frmEnableGeodatabase.GxObject = ((IGxSelection) _context.GxSelection).FirstObject;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }


    class CmdVersionInfo : YutaiCommand
    {
        public CmdVersionInfo(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "版本";
            this.m_category = "Catalog";
            this.m_message = "查看版本信息";
            this.m_name = "Catalog_VersionInfo";
            this._key = "Catalog_VersionInfo";
            this.m_toolTip = "查看版本信息";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.GxSelection == null)
                {
                    flag = false;
                }
                else if (((IGxSelection) _context.GxSelection).FirstObject != null)
                {
                    flag = (!(((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase) ||
                            !(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).IsRemoteDatabase ||
                            !(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).IsConnected
                        ? false
                        : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (this.Enabled)
            {
                frmVersionInfo _frmVersionInfo = new frmVersionInfo()
                {
                    VersionWorkspace =
                        (((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace as
                            IVersionedWorkspace
                };
                _frmVersionInfo.ShowDialog();
            }
        }
    }


    class CmdConnection : YutaiCommand
    {
        public CmdConnection(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "连接";
            this.m_category = "Catalog";
            this.m_message = "连接";
            this.m_name = "Catalog_Connection";
            this._key = "Catalog_Connection";
            this.m_toolTip = "连接";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool isConnected;
                if (_context.GxSelection != null)
                {
                    IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
                    if (firstObject is IGxDatabase)
                    {
                        isConnected = (!(firstObject as IGxDatabase).IsConnected ? true : false);
                    }
                    else if (!(firstObject is IGxAGSConnection))
                    {
                        isConnected = (!(firstObject is IGxGDSConnection)
                            ? false
                            : !(firstObject as IGxGDSConnection).IsConnected);
                    }
                    else
                    {
                        isConnected = !(firstObject as IGxAGSConnection).IsConnected;
                    }
                }
                else
                {
                    isConnected = false;
                }
                return isConnected;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            try
            {
                IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
                Cursor.Current = Cursors.WaitCursor;
                if (firstObject is IGxDatabase)
                {
                    (firstObject as IGxDatabase).Connect();
                }
                else if (firstObject is IGxAGSConnection)
                {
                    (firstObject as IGxAGSConnection).Connect();
                }
                else if (firstObject is IGxGDSConnection)
                {
                    (firstObject as IGxGDSConnection).Connect();
                }
                GxCatalogCommon.GetCatalog(firstObject).ObjectRefreshed(firstObject);
            }
            catch (Exception exception)
            {
            }
            Cursor.Current = Cursors.Default;
        }
    }


    class CmdDisconnection : YutaiCommand
    {
        public CmdDisconnection(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "断开连接";
            this.m_category = "Catalog";
            this.m_message = "断开连接";
            this.m_name = "Catalog_Disconnection";
            this._key = "Catalog_Disconnection";
            this.m_toolTip = "断开连接";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool isConnected;
                if (_context.GxSelection != null)
                {
                    IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
                    if (firstObject is IGxDatabase)
                    {
                        isConnected = (!(firstObject as IGxDatabase).IsConnected ? false : true);
                    }
                    else if (!(firstObject is IGxAGSConnection))
                    {
                        isConnected = (!(firstObject is IGxGDSConnection)
                            ? false
                            : (firstObject as IGxGDSConnection).IsConnected);
                    }
                    else
                    {
                        isConnected = (firstObject as IGxAGSConnection).IsConnected;
                    }
                }
                else
                {
                    isConnected = false;
                }
                return isConnected;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
            if (firstObject is IGxDatabase)
            {
                (firstObject as IGxDatabase).Disconnect();
            }
            else if (firstObject is IGxAGSConnection)
            {
                (firstObject as IGxAGSConnection).Disconnect();
            }
            else if (firstObject is IGxGDSConnection)
            {
                (firstObject as IGxGDSConnection).Disconnect();
            }
            GxCatalogCommon.GetCatalog(firstObject).ObjectChanged(firstObject);
            GxCatalogCommon.GetCatalog(firstObject).ObjectRefreshed(firstObject);
        }
    }


    class CmdConnectionProperty : YutaiCommand
    {
        public CmdConnectionProperty(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_table;
            this.m_caption = "连接属性";
            this.m_category = "Catalog";
            this.m_message = "连接属性";
            this.m_name = "Catalog_ConnectionProperty";
            this._key = "Catalog_ConnectionProperty";
            this.m_toolTip = "连接属性";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.GxSelection != null)
                {
                    flag = (!(((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase) ? false : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
            IRemoteDatabaseWorkspaceFactory workspaceFactory =
                (firstObject as IGxDatabase).WorkspaceName.WorkspaceFactory as IRemoteDatabaseWorkspaceFactory;
            workspaceFactory.EditConnectionFile((firstObject as IGxDatabase).WorkspaceName.PathName, 0);
        }
    }
}