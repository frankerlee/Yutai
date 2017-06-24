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
    class CmdQueryComplex : YutaiTool
    {
        private ComplexQueryUI QueryUI;


        private PipelineAnalysisPlugin _plugin;
        public CmdQueryComplex(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
            if (this.QueryUI == null || this.QueryUI.IsDisposed)
            {
                this.QueryUI = new ComplexQueryUI();
                this.QueryUI.MapControl = (IMapControl3)_context.MapControl;
                this.QueryUI.pPipeCfg =_plugin.PipeConfig;
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
            base.m_caption = "复合查询";
            base.m_category = "PipelineQuery";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeQuery_Complex";
            base._key = "PipeQuery_Complex";
            base.m_toolTip = "复合查询";
            base.m_checked = false;
            base.m_message = "复合查询";
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
                this.QueryUI.m_OriginGeo = ipGeo;
                try
                {
                    if (this.QueryUI.GlacisNum <= 0)
                    {
                        this.QueryUI.m_ipGeo = ipGeo;
                    }
                    else
                    {
                        IGeometry geometry1 = ((ITopologicalOperator)ipGeo).Buffer(this.QueryUI.GlacisNum);
                        this.QueryUI.m_ipGeo = geometry1;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("复杂多边形,创建缓冲区失败!");
                    this.QueryUI.GlacisNum = 0;
                    this.QueryUI.m_ipGeo = ipGeo;
                }
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