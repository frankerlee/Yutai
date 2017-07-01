using System;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdNewFolder : YutaiCommand
    {
        public CmdNewFolder(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "创建文件夹";
            this.m_category = "Catalog";
            this.m_message = "创建文件夹";
            this.m_name = "Catalog_NewFolder";
            this._key = "Catalog_NewFolder";
            this.m_toolTip = "创建文件夹";
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
                        flag = ((firstObject is IGxDiskConnection ? false : !(firstObject is IGxFolder)) ? false : true);
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
            IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
            if (firstObject is IGxFile)
            {
                string path = (firstObject as IGxFile).Path;
                path = (path[path.Length - 1] != '\\' ? string.Concat(path, "\\新建文件夹") : string.Concat(path, "新建文件夹"));
                string str = path;
                int num = 1;
                while (Directory.Exists(str))
                {
                    num++;
                    str = string.Concat(path, " (", num.ToString(), ")");
                }
                Directory.CreateDirectory(str);
                IGxObject gxFolder = new GxFolder();
                (gxFolder as IGxFile).Path = str;
                IGxCatalog catalog = GxCatalogCommon.GetCatalog(firstObject);
                gxFolder.Attach(firstObject, catalog);
                catalog.ObjectAdded(gxFolder);
            }
        }
    }


    class CmdStartServer : YutaiCommand
    {
        public CmdStartServer(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "启动";
            this.m_category = "Catalog";
            this.m_message = "启动服务";
            this.m_name = "Catalog_StartServer";
            this._key = "Catalog_StartServer";
            this.m_toolTip = "启动服务";
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
                return _context.GxSelection != null &&
                       (((IGxSelection) _context.GxSelection).FirstObject is IGxAGSObject &&
                        (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).Status != "Started" &&
                        (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).Status != "Starting");
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            ESRI.ArcGIS.GISClient.IAGSServerObjectName aGSServerObjectName =
                (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).AGSServerObjectName;
            ESRI.ArcGIS.GISClient.IAGSServerConnection iAGSServerConnection =
                (aGSServerObjectName.AGSServerConnectionName as IName).Open() as
                    ESRI.ArcGIS.GISClient.IAGSServerConnection;
            (iAGSServerConnection as ESRI.ArcGIS.GISClient.IAGSServerConnectionAdmin).ServerObjectAdmin
                .StartConfiguration(aGSServerObjectName.Name, aGSServerObjectName.Type);
            ((IGxCatalog) _context.GxCatalog).ObjectRefreshed(((IGxSelection) _context.GxSelection).FirstObject);
        }
    }


    class CmdStoptServer : YutaiCommand
    {
        public CmdStoptServer(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "停止";
            this.m_category = "Catalog";
            this.m_message = "停止服务";
            this.m_name = "Catalog_StopServer";
            this._key = "Catalog_StopServer";
            this.m_toolTip = "停止服务";
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
                return _context.GxSelection != null &&
                       (((IGxSelection) _context.GxSelection).FirstObject is IGxAGSObject &&
                        (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).Status != "Stopped" &&
                        (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).Status != "Stopping");
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            ESRI.ArcGIS.GISClient.IAGSServerObjectName aGSServerObjectName =
                (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).AGSServerObjectName;
            ESRI.ArcGIS.GISClient.IAGSServerConnection iAGSServerConnection =
                (aGSServerObjectName.AGSServerConnectionName as IName).Open() as
                    ESRI.ArcGIS.GISClient.IAGSServerConnection;
            (iAGSServerConnection as ESRI.ArcGIS.GISClient.IAGSServerConnectionAdmin).ServerObjectAdmin
                .StopConfiguration(aGSServerObjectName.Name, aGSServerObjectName.Type);
            ((IGxCatalog) _context.GxCatalog).ObjectRefreshed(((IGxSelection) _context.GxSelection).FirstObject);
        }
    }


    class CmdServiceProperty : YutaiCommand
    {
        public CmdServiceProperty(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "服务属性";
            this.m_category = "Catalog";
            this.m_message = "服务属性";
            this.m_name = "Catalog_ServiceProperty";
            this._key = "Catalog_ServiceProperty";
            this.m_toolTip = "服务属性";
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
                return _context.GxSelection != null &&
                       (!(((IGxSelection) _context.GxSelection).FirstObject is IGxAGSObject) || true);
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSObject).EditServerObjectProperties(0);
        }
    }


    class CmdServerProperty : YutaiCommand
    {
        public CmdServerProperty(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_folder;
            this.m_caption = "服务器属性";
            this.m_category = "Catalog";
            this.m_message = "服务器属性";
            this.m_name = "Catalog_ServerProperty";
            this._key = "Catalog_ServerProperty";
            this.m_toolTip = "服务器属性";
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
                return _context.GxSelection != null &&
                       (!(((IGxSelection) _context.GxSelection).FirstObject is IGxAGSConnection) || true);
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            (((IGxSelection) _context.GxSelection).FirstObject as IGxAGSConnection).EditServerProperties(0, 0);
        }
    }
}