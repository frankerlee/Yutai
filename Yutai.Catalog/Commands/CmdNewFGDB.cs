using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdNewFGDB : YutaiCommand
    {
        public CmdNewFGDB(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_filegdb;
            this.m_caption = "创建文件型数据库";
            this.m_category = "Catalog";
            this.m_message = "创建文件型数据库";
            this.m_name = "Catalog_NewFGDB";
            this._key = "Catalog_NewFGDB";
            this.m_toolTip = "创建文件型数据库";
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
                path = (path[path.Length - 1] != '\\'
                    ? string.Concat(path, "\\新建文件型数据库")
                    : string.Concat(path, "新建文件型数据库"));
                string str = string.Concat(path, ".gdb");
                int num = 1;
                while (Directory.Exists(str))
                {
                    num++;
                    str = string.Concat(path, " (", num.ToString(), ").gdb");
                }
                IWorkspaceFactory fileGDBWorkspaceFactoryClass = new FileGDBWorkspaceFactory();
                try
                {
                    IWorkspaceName workspaceName = fileGDBWorkspaceFactoryClass.Create(Path.GetDirectoryName(str),
                        Path.GetFileNameWithoutExtension(str), null, 0);
                    IGxObject gxDatabase = new GxDatabase();
                    (gxDatabase as IGxDatabase).WorkspaceName = workspaceName;
                    IGxCatalog catalog = GxCatalogCommon.GetCatalog(firstObject);
                    gxDatabase.Attach(firstObject, catalog);
                    catalog.ObjectAdded(gxDatabase);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}