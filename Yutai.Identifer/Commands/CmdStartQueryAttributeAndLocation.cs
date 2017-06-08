using System;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Query;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdStartQueryAttributeAndLocation : YutaiCommand
    {
        public CmdStartQueryAttributeAndLocation(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            frmSelectByLocation queryBuilder = new frmSelectByLocation(_context)
            {
                Map = _context.MapControl.Map as IMap,
                CloseButton = true
            };
            queryBuilder.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "联合查询";
            base.m_category = "Query";
            base.m_bitmap = Properties.Resources.icon_select_attribute_location;
            base.m_name = "Query_StartAttributeAndLocationQuery";
            base._key = "Query_StartAttributeAndLocationQuery";
            base.m_toolTip = "空间属性联合查询";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}