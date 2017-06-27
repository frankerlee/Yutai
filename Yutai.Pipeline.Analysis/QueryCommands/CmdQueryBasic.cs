using System;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Analysis.QueryForms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryCommands
{
    class CmdQueryBasic : YutaiTool
    {
        private BaseQueryUI QueryUI;
        private PipelineAnalysisPlugin _plugin;

        public CmdQueryBasic(IAppContext context,PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
            if (this.QueryUI == null || this.QueryUI.IsDisposed)
            {
                this.QueryUI = new BaseQueryUI();
                this.QueryUI.TopMost = true;
                this.QueryUI.MapControl = (IMapControl3)_context.MapControl;
                this.QueryUI.pPipeCfg = _plugin.PipeConfig;
                this.QueryUI.m_context = this._context;
                this.QueryUI.WindowText = "管点查询";
                this.QueryUI.Closing += new CancelEventHandler(this.QueryUI_Closing);
                this.QueryUI.Show();
                this.QueryUI.Plugin = _plugin;
            }
            else if (!this.QueryUI.Visible)
            {
                this.QueryUI.AutoFlash();
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
            base.m_caption = "管点查询";
            base.m_category = "PipelineQuery";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeQuery_Basic";
            base._key = "PipeQuery_Basic";
            base.m_toolTip = "管点查询";
            base.m_checked = false;
            base.m_message = "管点查询";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (this.QueryUI.SelectGeometry && Button == 1)
            {
                IGeometry ipGeo = null;
                if (this.QueryUI.DrawType == 0)
                {
                    ipGeo = _context.MapControl.TrackRectangle();
                }
                if (this.QueryUI.DrawType == 1)
                {
                    ipGeo = _context.MapControl.TrackPolygon();
                }
                if (this.QueryUI.DrawType == 2)
                {
                    ipGeo = _context.MapControl.TrackCircle();
                }
                this.QueryUI.m_ipGeo = ipGeo;
                _context.ActiveView.PartialRefresh((esriViewDrawPhase)32, null, null);
            }
        }
        private void QueryUI_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.QueryUI.Hide();
        }
    }
}