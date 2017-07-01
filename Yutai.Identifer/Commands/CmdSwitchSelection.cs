using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdSwitchSelection : YutaiCommand
    {
        private BasePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (this._context.MapControl.Map == null || this._context.MapControl.Map.SelectionCount <= 0
                    ? false
                    : true);
                return flag;
            }
        }

        public CmdSwitchSelection(IAppContext context, BasePlugin plugin)
        {
            this.m_bitmap = Properties.Resources.icon_select_switch;
            this.m_caption = "切换选择";
            this.m_category = "Query";
            this.m_message = "切换选择";
            this.m_name = "Query_SelectionTools_SwitchSelection";
            this._key = "Query_SelectionTools_SwitchSelection";
            this.m_toolTip = "切换选择";

            base.DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _context = context;
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
        }

        public override void OnClick()
        {
            ISelectionEnvironment selectionEnvironmentClass;
            try
            {
                if (((IdentifierPlugin) _plugin).QuerySettings.CurrentLayer == null) return;
                IFeatureLayer pFLayer = ((IdentifierPlugin) _plugin).QuerySettings.CurrentLayer;

                int count = (pFLayer as IFeatureSelection).SelectionSet.Count;
                int num = (pFLayer as IFeatureLayer).FeatureClass.FeatureCount(null);
                count = num - count;

                selectionEnvironmentClass = ((IdentifierPlugin) _plugin).QuerySettings.SelectionEnvironment;

                if (count <= (selectionEnvironmentClass as ISelectionEnvironmentThreshold).WarningThreshold ||
                    MessageBox.Show("所选择记录较多，执行将花较长时间，是否继续？", "选择", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    (_context.MapControl.ActiveView as IActiveView).PartialRefresh(
                        esriViewDrawPhase.esriViewGeoSelection, pFLayer, null);
                    (pFLayer as IFeatureSelection).SelectFeatures(null, esriSelectionResultEnum.esriSelectionResultXOR,
                        false);
                    (_context.MapControl.ActiveView as IActiveView).PartialRefresh(
                        esriViewDrawPhase.esriViewGeoSelection, pFLayer, null);
                }
            }
            catch (Exception exception)
            {
                // CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}