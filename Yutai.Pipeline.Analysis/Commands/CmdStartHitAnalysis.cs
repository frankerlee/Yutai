using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdStartHitAnalysis : YutaiTool
    {

        public HitAnalyseDlg m_hitAlsDlg;


        private PipelineAnalysisPlugin _plugin;


        public CmdStartHitAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);

            if (this.m_hitAlsDlg == null)
            {
                this.m_hitAlsDlg = new HitAnalyseDlg(_context, _plugin.PipeConfig);
             
                this.m_hitAlsDlg.Show();
            }
            else if (!this.m_hitAlsDlg.Visible)
            {
                this.m_hitAlsDlg.InitDistAnalyseDlg();
                this.m_hitAlsDlg.Visible = true;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "碰撞分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_HitAnalysis";
            base._key = "PipeAnalysis_HitAnalysis";
            base.m_toolTip = "碰撞分析";
            base.m_checked = false;
            base.m_message = "碰撞分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {
           
        }



        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button == 1 && this.m_hitAlsDlg.Visible && this.m_hitAlsDlg.HitType == HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw)
            {
                this.m_hitAlsDlg.GetDrawLine();
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
           
            if (button == 1)
            {
                if (this.m_hitAlsDlg.HitType == HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect)
                {
                    this.StartAnalysis(x, y);
                    IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                    this.m_hitAlsDlg.GetBaseLine(point);
                    this.m_hitAlsDlg.RefreshBaseLineGrid();
                }
                if (this.m_hitAlsDlg.HitType == HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw)
                {
                    ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                    ISimpleLineSymbol arg_89_0 = simpleLineSymbol;
                    IRgbColor rgbColorClass = new RgbColor();
                    rgbColorClass.Red=(255);
                    rgbColorClass.Green=(0);
                    rgbColorClass.Blue=(0);
                    arg_89_0.Color=(rgbColorClass);
                    simpleLineSymbol.Width=(5.0);
                    if (this.m_hitAlsDlg.m_commonDistAls.m_pBaseLine != null)
                    {
                        this.m_hitAlsDlg.RefreshBaseLineGrid();
                    }
                }
            }
        }

        private void StartAnalysis(int num, int num2)
        {
            IMap map = this._context.FocusMap;
            IEnvelope envelope = new Envelope() as IEnvelope;
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
            IActiveView activeView = (IActiveView)map;
            envelope.PutCoords(point.X, point.Y, point.X, point.Y);
            double num3 = activeView.Extent.Width / 200.0;
            IEnvelope expr_67 = envelope;
            expr_67.XMin=(expr_67.XMin - num3);
            IEnvelope expr_76 = envelope;
            expr_76.YMin=(expr_76.YMin - num3);
            IEnvelope expr_85 = envelope;
            expr_85.YMax=(expr_85.YMax + num3);
            IEnvelope expr_94 = envelope;
            expr_94.XMax=(expr_94.XMax+ num3);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(envelope, selectionEnvironment, true);
            activeView = (IActiveView)map;
            activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
        }


    }
}