using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
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
}