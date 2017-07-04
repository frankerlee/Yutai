using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Catalog.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
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
}