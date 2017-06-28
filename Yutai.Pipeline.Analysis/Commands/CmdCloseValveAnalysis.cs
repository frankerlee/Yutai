using System;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdCloseValveAnalysis : YutaiTool
    {

        private frmBurstReport frmBurst;
        private PipelineAnalysisPlugin _plugin;
        private IPipelineConfig _config;

        public CmdCloseValveAnalysis(IAppContext context,PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);

            if (this.frmBurst == null || this.frmBurst.IsDisposed)
            {
                this.frmBurst = new frmBurstReport();
                frmBurst.m_iApp = this._context;
                frmBurst.m_Config = _plugin.PipeConfig;
                this.frmBurst.Show();
            }
            else
            {
                this.frmBurst.Show();
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "关阀分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipeAnalysis_CloseValveAnalysis";
            base._key = "PipeAnalysis_CloseValveAnalysis";
            base.m_toolTip = "关阀分析";
            base.m_checked = false;
            base.m_message = "关阀分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {

        }



        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 4)
            {
                if (this.frmBurst == null || this.frmBurst.IsDisposed)
                {
                    this.frmBurst = new frmBurstReport();
                    frmBurst.m_iApp = this._context;
                    frmBurst.m_Config = _plugin.PipeConfig;
                    this.frmBurst.Show();
                }
                this.frmBurst.SetMousePoint(x, y);
            }
        }

      


    }
}