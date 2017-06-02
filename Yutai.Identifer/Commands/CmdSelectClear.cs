using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdSelectClear : YutaiCommand
    {
       
       
        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (_context.MapControl.Map == null || _context.MapControl.Map.SelectionCount <= 0 ? false : true);
                return flag;
            }
        }

        public CmdSelectClear(IAppContext context)
        {
            base.m_bitmap = Properties.Resources.icon_select_clear;
            base.m_caption = "清除选择";
            base.m_category = "Query";
            base.m_message = "清除选择";
            base.m_name = "Query.SelectionTools.ClearSelection";
            base._key = "Query.SelectionTools.ClearSelection";
            base.m_toolTip = "清除选择";
            base._itemType = RibbonItemType.NormalItem;
            base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _context = context;
           
        }

        public override void OnCreate(object hook)
        {

        }

        public override void OnClick()
        {
           
            IMap pMap = _context.MapControl.Map;
            (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, false, null);
            pMap.ClearSelection();
            (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, false, null);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}
