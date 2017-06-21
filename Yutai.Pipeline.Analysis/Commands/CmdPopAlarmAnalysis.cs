using System;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdPopAlarmAnalysis : YutaiCommand
    {



        public CmdPopAlarmAnalysis(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            new PoPointAlarmForm(_context).ShowDialog();
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "爆点预警";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_PopAlarmAnalysis";
            base._key = "PipeAnalysis_PopAlarmAnalysis";
            base.m_toolTip = "爆点预警";
            base.m_checked = false;
            base.m_message = "爆点预警";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

            CommonUtils.AppContext = _context;
        }


    }
}