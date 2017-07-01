using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolPageZoomIn : YutaiTool
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
            //this.m_bitmap = new System.Drawing.Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Pluge.Bitmap.PageZoomIn.bmp"));
            this.m_cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.PageZoomIn.cur"));
            this.m_name = "PageZoomIn";
            this.m_caption = "放大";
            this.m_toolTip = "页面放大";
            this.m_category = "页面操作";
            base.m_bitmap = Properties.Resources.PageZoomIn;
            base.m_name = "Printing_PageZoomIn";
            _key = "Printing_PageZoomIn";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolPageZoomIn(IAppContext context)
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
                    envelope.Expand(0.5, 0.5, true);
                    envelope.CenterAt(this._pPoint0);
                }
                else
                {
                    envelope = this.inewEnvelopeFeedback_0.Stop();
                    this.inewEnvelopeFeedback_0 = null;
                    if (envelope.Width == 0.0 || envelope.Height == 0.0)
                    {
                        envelope = activeView.Extent;
                        envelope.Expand(0.5, 0.5, true);
                        envelope.CenterAt(this._pPoint0);
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

        public new void OnKeyUp(int Button, int Shift)
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