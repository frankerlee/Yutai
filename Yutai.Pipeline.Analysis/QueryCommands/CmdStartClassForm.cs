using System;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    class CmdStartClassForm : YutaiTool
    {
        private ClassCollectformsUI QueryUI;


        public CmdStartClassForm(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
            if (this.QueryUI == null || this.QueryUI.IsDisposed)
            {
                this.QueryUI = new ClassCollectformsUI();
                this.QueryUI.MapControl = (IMapControl3)_context.MapControl;
                this.QueryUI.pPipeCfg = _context.pPipeCfg;
                this.QueryUI.m_context = this._context;
                this.QueryUI.Closing += new CancelEventHandler(this.QueryUI_Closing);
                this.QueryUI.Show();
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
            base.m_caption = "分类汇总";
            base.m_category = "PipelineQuery";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeQuery_Statics";
            base._key = "PipeQuery_Statics";
            base.m_toolTip = "分类汇总";
            base.m_checked = false;
            base.m_message = "分类汇总";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (this.QueryUI.SelectGeometry && Button == 1)
            {
                IGeometry ipGeo = null;
                ipGeo = _context.MapControl.TrackRectangle();
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