using System;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdSearchAffixAnalysis : YutaiTool
    {

        private SearchAffixAnalyDlg searchAffixAnalyDlg_0;

        private PipelineAnalysisPlugin _plugin;


        public CmdSearchAffixAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnClick()
        {

            _context.SetCurrentTool(this);
            if (this.searchAffixAnalyDlg_0 == null)
            {
                this.searchAffixAnalyDlg_0 = new SearchAffixAnalyDlg();
                this.searchAffixAnalyDlg_0.m_iApp = _context;
                this.searchAffixAnalyDlg_0.MapControl = _context.MapControl as IMapControl3;
                this.searchAffixAnalyDlg_0.pPipeCfg = _plugin.PipeConfig;
                this.searchAffixAnalyDlg_0.Show();
            }
            else if (!this.searchAffixAnalyDlg_0.Visible)
            {
                this.searchAffixAnalyDlg_0.InitAppearance();
                this.searchAffixAnalyDlg_0.Visible = true;
            }

        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "设施搜索";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_facility;
            base.m_name = "PipeAnalysis_SearchAffixAnalysis";
            base._key = "PipeAnalysis_SearchAffixAnalysis";
            base.m_toolTip = "设施搜索";
            base.m_checked = false;
            base.m_message = "设施搜索";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {

        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            this.searchAffixAnalyDlg_0.OnMouseDown(point.X, point.Y);

        }

        public override void OnMouseMove(int button, int Shift, int x, int y)
        {
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            this.searchAffixAnalyDlg_0.OnMouseMove(point.X, point.Y);

        }
    }
}