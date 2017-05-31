using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Query;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.UI.Docking;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdStartQueryBuilder : YutaiCommand
    {
        public CmdStartQueryBuilder(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
           frmAttributeQueryBuilder queryBuilder=new frmAttributeQueryBuilder(_context)
           {
               Map = _context.MapControl.Map as IBasicMap
           };
            queryBuilder.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "属性查询";
            base.m_category = "Query";
            base.m_bitmap = Properties.Resources.QueryAttribute;
            base.m_name = "Query.Common.StartAttribute";
            base._key = "Query.Common.StartAttribute";
            base.m_toolTip = "属性查询";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }

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
            frmSelectByLocation queryBuilder = new frmSelectByLocation(_context)
            {
                Map = _context.MapControl.Map as IMap,
                CloseButton=true
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
