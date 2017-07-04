using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
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