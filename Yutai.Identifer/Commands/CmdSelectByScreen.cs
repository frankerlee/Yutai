using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdSelectByScreen : YutaiCommand
    {
        private IdentifierPlugin _plugin;

        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (_context.MapControl.Map == null || _context.MapControl.Map.LayerCount <= 0 ? false : true);
                ;
                return flag;
            }
        }

        public CmdSelectByScreen(IAppContext context, BasePlugin plugin)
        {
            this.m_bitmap = Properties.Resources.icon_select_screen;
            this.m_caption = "全屏选择";
            this.m_category = "Query";
            this.m_message = "全屏选择";
            this.m_name = "Query_SelectionTools_SelectByScreen";
            this._key = "Query_SelectionTools_SelectByScreen";
            this.m_toolTip = "全屏选择";
            _context = context;
            base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _plugin = plugin as IdentifierPlugin;
        }

        public override void OnCreate(object hook)
        {
        }

        public override void OnClick()
        {
            ISelectionEnvironment selectionEnvironmentClass = _plugin.QuerySettings.SelectionEnvironment;
            IMap pMap = _context.MapControl.Map;
            (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            pMap.SelectByShape((pMap as IActiveView).Extent, selectionEnvironmentClass, false);
            (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}