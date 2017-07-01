using System;
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
    class CmdStartDistMeasureAnalysis : YutaiTool
    {
        private DistMeausreDlg m_dmd;

        private GPolyLine gpolyLine_0 = new GPolyLine();

        private GPoint gpoint_0 = new GPoint();

        private GPoint gpoint_1 = new GPoint();

        private INewLineFeedback _lineFeedback;

        public CmdStartDistMeasureAnalysis(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);

            if (this.m_dmd == null)
            {
                this.m_dmd = new DistMeausreDlg();
                this.m_dmd.m_app = this._context;
                this.m_dmd.Show();
            }
            else if (!this.m_dmd.Visible)
            {
                this.m_dmd.InitMeasureDlg();
                this.m_dmd.Visible = true;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "距离量算";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_measure_length;
            base.m_name = "PipeAnalysis_DistMeasure";
            base._key = "PipeAnalysis_DistMeasure";
            base.m_toolTip = "距离量算";
            base.m_checked = false;
            base.m_message = "距离量算";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {
            if (this._lineFeedback != null)
            {
                IPolyline pPolyLine = this._lineFeedback.Stop();
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                ISimpleLineSymbol arg_3E_0 = simpleLineSymbol;
                IRgbColor rgbColorClass = new RgbColor();
                rgbColorClass.Red = (255);
                rgbColorClass.Green = (0);
                rgbColorClass.Blue = (0);
                arg_3E_0.Color = (rgbColorClass);
                simpleLineSymbol.Width = (1.0);
                simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                CommonUtils.NewLineElement(_context.FocusMap, pPolyLine);
            }
            this.gpolyLine_0.Clear();
            this._lineFeedback = null;
            this.m_dmd.CurDist = 0.0;
        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            double x2 = point.X;
            double y2 = point.Y;
            this.gpoint_1.X = x2;
            this.gpoint_1.Y = y2;
            this.gpolyLine_0.PushBack(this.gpoint_1.GetDeepCopy());
            if (this._lineFeedback == null)
            {
                this._lineFeedback = new NewLineFeedback();
                this._lineFeedback.Display = _context.ActiveView.ScreenDisplay;
                this._lineFeedback.Start(point);
            }
            else
            {
                this._lineFeedback.AddPoint(point);
            }
            this.gpoint_0.X = this.gpoint_1.X;
            this.gpoint_0.Y = this.gpoint_1.Y;
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (!this.m_dmd.Visible)
            {
                this._lineFeedback.Stop();
                this._lineFeedback = null;
                this.gpolyLine_0.Clear();
            }
            else
            {
                IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this.gpoint_1.X = point.X;
                this.gpoint_1.Y = point.Y;
                if (this.gpolyLine_0.Size() > 0)
                {
                    double num = this.gpoint_0.DistanceToPt(this.gpoint_1);
                    double totalDist = this.gpolyLine_0.Length + num;
                    this.m_dmd.CurDist = num;
                    this.m_dmd.TotalDist = totalDist;
                }
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.MoveTo(point);
                }
            }
        }
    }
}