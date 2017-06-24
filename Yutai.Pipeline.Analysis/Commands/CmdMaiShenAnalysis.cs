using System;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdMaiShenAnalysis : YutaiCommand
    {
        private PipelineAnalysisPlugin _plugin;

        public CmdMaiShenAnalysis(IAppContext context,PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            FormMaiShenAnalysis formMaiShenAnalysis = new FormMaiShenAnalysis(_context, _plugin.PipeConfig);
            formMaiShenAnalysis.Show();
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "埋深分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_MaiShenAnalysis";
            base._key = "PipeAnalysis_MaiShenAnalysis";
            base.m_toolTip = "埋深分析";
            base.m_checked = false;
            base.m_message = "埋深分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

            CommonUtils.AppContext = _context;
        }

       
    }
}