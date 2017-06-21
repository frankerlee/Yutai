using System;
using System.Collections;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdStartAreaMeasureAnalysis : YutaiTool
    {

        private AreaMeasureDlg _areaMeasureDlg;

        private IPointCollection _pointCollection = new Polygon();

        private INewPolygonFeedback  _polygonFeedback;
        public CmdStartAreaMeasureAnalysis(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);

            if (this._areaMeasureDlg == null)
            {
                this._areaMeasureDlg = new AreaMeasureDlg();
                this._areaMeasureDlg.m_app = this._context;
                this._areaMeasureDlg.Show();
            }
            else if (!this._areaMeasureDlg.Visible)
            {
                this._areaMeasureDlg.InitMeasureDlg();
                this._areaMeasureDlg.Visible = true;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "面积量算";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_measure_area;
            base.m_name = "PipeAnalysis_AreaMeasure";
            base._key = "PipeAnalysis_AreaMeasure";
            base.m_toolTip = "面积量算";
            base.m_checked = false;
            base.m_message = "面积量算";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

        
            CommonUtils.AppContext = _context;
        }

        public override void OnDblClick()
        {
            if (this._polygonFeedback != null)
            {
                IPolygon polygon = this._polygonFeedback.Stop();
                ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                ISimpleFillSymbol arg_3E_0 = simpleFillSymbol;
                IRgbColor rgbColorClass = ColorManage.CreatColor(255, 0, 0) as IRgbColor;
                arg_3E_0.Color=rgbColorClass;
                if (polygon != null)
                {
                    CommonUtils.NewPolygonElement(_context.FocusMap, polygon);
                }
            }
            int pointCount = this._pointCollection.PointCount;
            this._pointCollection.RemovePoints(0, pointCount);
            this._polygonFeedback = null;
        }

       

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (button == 1)
            {
                if (this._polygonFeedback == null)
                {
                    this._polygonFeedback = new NewPolygonFeedback();
                    this._polygonFeedback.Display = _context.ActiveView.ScreenDisplay;
                    this._polygonFeedback.Start(point);
                }
                else
                {
                    this._polygonFeedback.AddPoint(point);
                }
                object missing = Type.Missing;
                this._pointCollection.AddPoint(point, ref missing, ref missing);
                if (this._pointCollection.PointCount == 1)
                {
                    this._areaMeasureDlg.CurArea = 0.0;
                }
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (!this._areaMeasureDlg.Visible && this._polygonFeedback != null)
            {
                this._polygonFeedback.Stop();
                this._polygonFeedback = null;
                int pointCount = this._pointCollection.PointCount;
                this._pointCollection.RemovePoints(0, pointCount);
                
            }
            else
            {
                IPoint point =_context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                object missing = Type.Missing;
                this._pointCollection.AddPoint(point, ref missing, ref missing);
                if (this._polygonFeedback != null)
                {
                    this._polygonFeedback.MoveTo(point);
                }
                this._pointCollection.AddPoint(this._pointCollection.get_Point(0), ref missing, ref missing);
                IPolygon polygon = (IPolygon)this._pointCollection;
                double length = polygon.Length;
                IArea area = (IArea)polygon;
                double curArea = Math.Abs(area.Area);
                if (this._pointCollection.PointCount > 2)
                {
                    this._areaMeasureDlg.CurArea = curArea;
                    this._areaMeasureDlg.CurPerimeter = length;
                }
                this._pointCollection.RemovePoints(this._pointCollection.PointCount - 1, 1);
                int pointCount2 = this._pointCollection.PointCount;
                this._pointCollection.RemovePoints(pointCount2 - 1, 1);
            }
        }

        


    }
}