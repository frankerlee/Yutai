using System;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
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
}