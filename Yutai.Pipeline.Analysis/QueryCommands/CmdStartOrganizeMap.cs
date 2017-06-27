using System;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryCommands
{
    class CmdStartOrganizeMap : YutaiCommand
    {
        private PipelineAnalysisPlugin _plugin;
        private frmMapLayerOrganize frmOrganize = null;
        public CmdStartOrganizeMap(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnClick(object sender, EventArgs args)
        {

            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "管线配置";
            base.m_category = "PipelineQuery";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeQuery_Config";
            base._key = "PipeQuery_Config";
            base.m_toolTip = "管线配置";
            base.m_checked = false;
            base.m_message = "管线配置";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            
            CommonUtils.AppContext = _context;
        }

        public void OnClick()
        {
            if(frmOrganize == null)
            {
                frmOrganize = new frmMapLayerOrganize(_context.FocusMap, _plugin.PipeConfig);
            }
            else
            {
                frmOrganize.InitMap(_context.FocusMap);
            }
            frmOrganize.ShowDialog();

        }
    }
}