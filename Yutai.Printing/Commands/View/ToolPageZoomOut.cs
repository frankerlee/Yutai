using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolPageZoomOut : YutaiTool
    {
        private IPoint _pPoint0;

        private INewEnvelopeFeedback inewEnvelopeFeedback_0;

        private bool bool_0 = false;

        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.PageZoomOut.cur"));
            this.m_name = "PageZoomOut";
            this.m_caption = "缩小";
            this.m_toolTip = "页面缩小";
            this.m_category = "页面操作";
            base.m_bitmap = Properties.Resources.PageZoomOut;
            base.m_name = "Printing_PageZoomOut";
            _key = "Printing_PageZoomOut";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolPageZoomOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button != 2)
            {
                IActiveView activeView = this._context.ActiveView;
                if (activeView is IPageLayout)
                {
                    this.bool_0 = true;
                    this._pPoint0 = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                }
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            IActiveView activeView = this._context.ActiveView;
            if (activeView is IPageLayout)
            {
                this.bool_0 = false;
                IEnvelope envelope;
                if (this.inewEnvelopeFeedback_0 == null)
                {
                    envelope = activeView.Extent;
                    envelope.Expand(2.0, 2.0, true);
                    envelope.CenterAt(this._pPoint0);
                }
                else
                {
                    activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    IEnvelope envelope2 = this.inewEnvelopeFeedback_0.Stop();
                    this.inewEnvelopeFeedback_0 = null;
                    if (envelope2.Width == 0.0 || envelope2.Height == 0.0)
                    {
                        envelope = activeView.Extent;
                        envelope.Expand(2.0, 2.0, true);
                        envelope.CenterAt(this._pPoint0);
                    }
                    else
                    {
                        double num = activeView.Extent.Width*(activeView.Extent.Width/envelope2.Width);
                        double num2 = activeView.Extent.Height*(activeView.Extent.Height/envelope2.Height);
                        envelope = new Envelope() as IEnvelope;
                        envelope.PutCoords(
                            activeView.Extent.XMin -
                            (envelope2.XMin - activeView.Extent.XMin)*(activeView.Extent.Width/envelope2.Width),
                            activeView.Extent.YMin -
                            (envelope2.YMin - activeView.Extent.YMin)*(activeView.Extent.Height/envelope2.Height),
                            activeView.Extent.XMin -
                            (envelope2.XMin - activeView.Extent.XMin)*(activeView.Extent.Width/envelope2.Width) + num,
                            activeView.Extent.YMin -
                            (envelope2.YMin - activeView.Extent.YMin)*(activeView.Extent.Height/envelope2.Height) + num2);
                    }
                }
                activeView.Extent = envelope;
                activeView.Refresh();
            }
        }

        public override void OnKeyDown(int Button, int Shift)
        {
            if (this.bool_0)
            {
                if (Button == 16)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
                }
                else if (Button == 17)
                {
                    this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsAspect;
                }
                if (Button == 27)
                {
                    this.bool_0 = false;
                    this.inewEnvelopeFeedback_0 = null;
                    this._context.ActiveView.Refresh();
                }
            }
        }

        public override void OnKeyUp(int Button, int Shift)
        {
            if (this.bool_0)
            {
                this.inewEnvelopeFeedback_0.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsNone;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (this.bool_0)
            {
                IActiveView activeView = this._context.ActiveView;
                if (activeView is IPageLayout)
                {
                    if (this.inewEnvelopeFeedback_0 == null)
                    {
                        this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback();
                        this.inewEnvelopeFeedback_0.Display = activeView.ScreenDisplay;
                        this.inewEnvelopeFeedback_0.Start(this._pPoint0);
                    }
                    IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    this.inewEnvelopeFeedback_0.MoveTo(point);
                }
            }
        }

        public override void Refresh(int Button)
        {
            if (this.inewEnvelopeFeedback_0 != null)
            {
                this.inewEnvelopeFeedback_0.Refresh(Button);
            }
        }
    }
}