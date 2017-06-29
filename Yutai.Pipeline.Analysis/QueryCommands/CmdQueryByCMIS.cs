using System;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Analysis.QueryForms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryCommands
{
    class CmdQueryByCMIS : YutaiCommand
    {
        private SimpleQueryByCmisUI QueryUI;

        private PipelineAnalysisPlugin _plugin;
        public CmdQueryByCMIS(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            if (this.QueryUI == null || this.QueryUI.IsDisposed)
            {
                this.QueryUI = new SimpleQueryByCmisUI();
                this.QueryUI.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.QueryUI.MinimizeBox = false;
                this.QueryUI.MaximizeBox = false;
                this.QueryUI.TopMost = true;
                this.QueryUI.MapControl = (IMapControl3)_context.MapControl;
                this.QueryUI.pPipeCfg = _plugin.PipeConfig;
                this.QueryUI.m_context = this._context;
                this.QueryUI.Closing += new CancelEventHandler(this.QueryUI_Closing);
                this.QueryUI.Show();
            }
            else if (!this.QueryUI.Visible)
            {
                this.QueryUI.Show();
                if (this.QueryUI.WindowState == FormWindowState.Minimized)
                {
                    this.QueryUI.WindowState = FormWindowState.Normal;
                }
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "按坐标点查询";
            base.m_category = "PipelineQuery";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeQuery_ByCMIS";
            base._key = "PipeQuery_ByCMIS";
            base.m_toolTip = "按坐标点查询";
            base.m_checked = false;
            base.m_message = "按坐标点查询";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

            CommonUtils.AppContext = _context;
        }

        private void QueryUI_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.QueryUI.Hide();
        }
    }
}