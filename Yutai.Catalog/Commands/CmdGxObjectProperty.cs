using System;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdGxObjectProperty : YutaiCommand
    {
        public CmdGxObjectProperty(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "属性";
            this.m_category = "Catalog";
            this.m_message = "对象属性";
            this.m_name = "Catalog_GxObjectProperty";
            this._key = "Catalog_GxObjectProperty";
            this.m_toolTip = "对象属性";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Text;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return _context.GxSelection != null && (((IGxSelection) _context.GxSelection).Count != 1 || true); }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            (((IGxSelection) _context.GxSelection).FirstObject as IGxObjectEdit).EditProperties(0);
        }
    }
}