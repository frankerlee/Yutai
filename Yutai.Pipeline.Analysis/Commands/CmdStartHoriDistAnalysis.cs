using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdStartHoriDistAnalysis : YutaiTool
    {
        public HrzDistDlg hrzDistDlg;
        public Form m_pParentFrom;


        private PipelineAnalysisPlugin _plugin;


        public CmdStartHoriDistAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);

            if (this.hrzDistDlg == null)
            {
                this.hrzDistDlg = new HrzDistDlg(_context, _plugin.PipeConfig);

                this.hrzDistDlg.Show();
            }
            else if (!this.hrzDistDlg.Visible)
            {
                this.hrzDistDlg.InitDistAnalyseDlg();
                this.hrzDistDlg.Visible = true;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "水平净距分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_distance_hor;
            base.m_name = "PipeAnalysis_HoriDistAnalysis";
            base._key = "PipeAnalysis_HoriDistAnalysis";
            base.m_toolTip = "水平净距分析";
            base.m_checked = false;
            base.m_message = "水平净距分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {
        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button == 1 && this.hrzDistDlg.Visible)
            {
                this.StartAnalysis(x, y);
                IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.hrzDistDlg.GetBaseLine(point);
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            //if (button == 1)
            //{
            //    if (this.m_hitAlsDlg.HitType == HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect)
            //    {
            //        this.StartAnalysis(x, y);
            //        this.m_hitAlsDlg.GetBaseLine();
            //        this.m_hitAlsDlg.RefreshBaseLineGrid();
            //    }
            //    if (this.m_hitAlsDlg.HitType == HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw)
            //    {
            //        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
            //        ISimpleLineSymbol arg_89_0 = simpleLineSymbol;
            //        IRgbColor rgbColorClass = new RgbColor();
            //        rgbColorClass.Red = (255);
            //        rgbColorClass.Green = (0);
            //        rgbColorClass.Blue = (0);
            //        arg_89_0.Color = (rgbColorClass);
            //        simpleLineSymbol.Width = (5.0);
            //        if (this.m_hitAlsDlg.m_commonDistAls.m_pBaseLine != null)
            //        {
            //            this.m_hitAlsDlg.RefreshBaseLineGrid();
            //        }
            //    }
            //}
        }

        private void StartAnalysis(int num, int num2)
        {
            IMap map = this._context.FocusMap;
            IEnvelope envelope = new Envelope() as IEnvelope;
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
            IActiveView activeView = (IActiveView) map;
            envelope.PutCoords(point.X, point.Y, point.X, point.Y);
            double num3 = activeView.Extent.Width/200.0;
            IEnvelope expr_67 = envelope;
            expr_67.XMin = (expr_67.XMin - num3);
            IEnvelope expr_76 = envelope;
            expr_76.YMin = (expr_76.YMin - num3);
            IEnvelope expr_85 = envelope;
            expr_85.YMax = (expr_85.YMax + num3);
            IEnvelope expr_94 = envelope;
            expr_94.XMax = (expr_94.XMax + num3);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(envelope, selectionEnvironment, true);
            activeView = (IActiveView) map;
            activeView.PartialRefresh((esriViewDrawPhase) 4, null, null);
        }
    }
}