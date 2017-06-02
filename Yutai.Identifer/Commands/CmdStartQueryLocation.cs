using System;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Query;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdStartQueryLocation : YutaiCommand
    {
        public CmdStartQueryLocation(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            frmSpatialAndAttributeQuery queryBuilder = new frmSpatialAndAttributeQuery(_context)
            {
                Map = _context.MapControl.Map as IMap
            };
            queryBuilder.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "空间查询";
            base.m_category = "Query";
            base.m_bitmap = Properties.Resources.QueryLocation;
            base.m_name = "Query.Common.StartLocationQuery";
            base._key = "Query.Common.StartLocationQuery";
            base.m_toolTip = "空间查询";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}