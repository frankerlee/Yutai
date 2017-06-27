using System;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdStartBufferAnalysis : YutaiTool
    {
        private IPoint _pPoint;
        private INewPolygonFeedback _polyFeedback;
        private bool _inLine;
        private Cursor _cursor;
        private Cursor _cursor1;
        private BufferAnalyseDlg _analyseDlg;
        private IPolygon _polygon;


        private PipelineAnalysisPlugin _plugin;


        public CmdStartBufferAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            _inLine = false;
            _context.SetCurrentTool(this);
            if (_analyseDlg == null)
            {
                _analyseDlg=new BufferAnalyseDlg()
                {
                    m_app = _context,
                    m_PipeConfig=_plugin.PipeConfig
                };
                _analyseDlg.Show();
                _analyseDlg.Closing+= AnalyseDlgOnClosing;
            }
            else if (!_analyseDlg.Visible)
            {
                _analyseDlg.Visible = true;
            }

        }

        private void AnalyseDlgOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            cancelEventArgs.Cancel = true;
            this.Deactivate();
            this._analyseDlg.Hide();
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "缓冲区分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_transect;
            _cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Pipeline.Analysis.Resources.cursor.Hand.cur"));
            _cursor1 =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Pipeline.Analysis.Resources.cursor.MoveHand.cur"));
            base.m_name = "PipeAnalysis_BufferAnalysisStart";
            base._key = "PipeAnalysis_BufferAnalysisStart";
            base.m_toolTip = "缓冲区分析";
            base.m_checked = false;
            base.m_message = "缓冲区分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (_inLine && keyCode == 27)
            {
                _polyFeedback = null;
                _inLine = false;
            }
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button == 2) return;
            IActiveView focusMap = (IActiveView)_context.ActiveView;
            _pPoint=focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (!this._analyseDlg.SelectGeometry)
            {
                GetAnalysisEnvelope(x, y);
            }
            else
            {
                IGeometry geometry = _context.MapControl.TrackPolygon();
                _analyseDlg.m_pDrawGeo = geometry;
                _analyseDlg.bDrawRed = true;
            }
            
            _context.ActiveView.PartialRefresh((esriViewDrawPhase) 32, null, null);
        }

        private void GetAnalysisEnvelope(int x, int y)
        {
            IMap map = _context.FocusMap;
            IEnvelope envelopeClass = new Envelope() as IEnvelope;
            IPoint mapPoint = ((IActiveView)map).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IActiveView activeView = (IActiveView)map;
            envelopeClass.PutCoords(mapPoint.X, mapPoint.Y, mapPoint.X, mapPoint.Y);
            double width = activeView.Extent.Width / 200;
            IEnvelope envelope = envelopeClass;
            envelope.XMin=(envelope.XMin - width);
            IEnvelope envelope1 = envelopeClass;
            envelope1.YMin=(envelope1.YMin - width);
            IEnvelope envelope2 = envelopeClass;
            envelope2.YMax=(envelope2.YMax + width);
            IEnvelope envelope3 = envelopeClass;
            envelope3.XMax=(envelope3.XMax + width);
            map.SelectByShape(envelopeClass, new SelectionEnvironment() , true);
            ((IActiveView)map).PartialRefresh((esriViewDrawPhase.esriViewGeoSelection), null, null);
        }

      
      
        
    }
}