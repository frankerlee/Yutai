using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
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
}