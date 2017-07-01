using System;
using ESRI.ArcGIS.Carto;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdPreAlarmAnalysis : YutaiCommand
    {
        private PreAlarmDlg preAlarmDlg_0;
        private PipelineAnalysisPlugin _plugin;


        public CmdPreAlarmAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            if (this.preAlarmDlg_0 == null)
            {
                this.preAlarmDlg_0 = new PreAlarmDlg(_context, _plugin.PipeConfig);
                this.preAlarmDlg_0.App = _context;
                this.preAlarmDlg_0.pPipeCfg = _plugin.PipeConfig;
                this.preAlarmDlg_0.Show();
            }
            else if (!this.preAlarmDlg_0.Visible)
            {
                this.preAlarmDlg_0.Visible = true;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "预警分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_prealarm;
            base.m_name = "PipeAnalysis_PreAlarmAnalysis";
            base._key = "PipeAnalysis_PreAlarmAnalysis";
            base.m_toolTip = "预警分析";
            base.m_checked = false;
            base.m_message = "预警分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }
    }
}