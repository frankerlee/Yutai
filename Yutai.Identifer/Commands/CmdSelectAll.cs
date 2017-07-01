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
    class CmdSelectAll : YutaiCommand
    {
        private IdentifierPlugin _plugin;

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.MapControl.Map != null)
                {
                    if ((_context.MapControl.Map.LayerCount <= 0
                        ? true
                        : !(_plugin.QuerySettings.CurrentLayer is IFeatureSelection)))
                    {
                        flag = false;
                        return flag;
                    }
                    flag = true;
                    return flag;
                }
                flag = false;
                return flag;
            }
        }

        public CmdSelectAll(IAppContext context, BasePlugin plugin)
        {
            this.m_bitmap = Properties.Resources.icon_select_all;
            this.m_caption = "全部选择";
            this.m_category = "Query";
            this.m_message = "全部选择";
            this.m_name = "Query_SelectionTools_SelectAll";
            this._key = "Query_SelectionTools_SelectAll";
            this.m_toolTip = "全部选择";
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
            if (_plugin.QuerySettings.CurrentLayer == null) return;
            int num = (_plugin.QuerySettings.CurrentLayer as IFeatureLayer).FeatureClass.FeatureCount(null);

            if (num <= (selectionEnvironmentClass as ISelectionEnvironmentThreshold).WarningThreshold ||
                MessageService.Current.Ask("所选择记录较多，执行将花较长时间，是否继续？") != false)
            {
                (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection,
                    _plugin.QuerySettings.CurrentLayer, null);
                (_plugin.QuerySettings.CurrentLayer as IFeatureSelection).SelectFeatures(null,
                    esriSelectionResultEnum.esriSelectionResultAdd, false);
                (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection,
                    _plugin.QuerySettings.CurrentLayer, null);
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}