using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
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
}