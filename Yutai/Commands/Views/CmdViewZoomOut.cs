using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Views
{
    public class CmdViewZoomOut : YutaiTool
    {
        private IPoint _iPoint;
        private INewEnvelopeFeedback _envelopeFeedback;
        private bool _inZoom;
        private Cursor _cursor;
        private Cursor _cursor1;
        private RibbonItemType _itemType;


        public CmdViewZoomOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override RibbonItemType ItemType
        {
            get { return RibbonItemType.Tool; }
        }

        public override void OnClick()
        {
            _inZoom = false;
            _context.SetCurrentTool(this);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "缩小";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_zoom_out;
            _cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.ZoomOut.cur"));
            _cursor1 =
                new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.MoveZoomOut.cur"));
            base.m_name = "View_ZoomOut";
            base._key = "View_ZoomOut";
            base.m_toolTip = "缩小";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (_inZoom && keyCode == 27)
            {
                this._envelopeFeedback = null;
                this._inZoom = false;
                base.m_cursor = this._cursor1;
                this._context.ActiveView.Refresh();
            }
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 2)
            {
                if (this._context.ActiveView is IPageLayout)
                {
                    IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                    IMap map = this._context.ActiveView.HitTestMap(location);
                    if (map == null)
                    {
                        return;
                    }
                    if (map != this._context.FocusMap)
                    {
                        this._context.ActiveView.FocusMap = map;
                        this._context.ActiveView.Refresh();
                    }
                }
                this._iPoint = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                this._inZoom = true;
                this.m_cursor = this._cursor1;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._inZoom)
            {
                IActiveView activeView = (IActiveView)this._context.FocusMap;
                if (this._envelopeFeedback == null)
                {
                    this._envelopeFeedback = new NewEnvelopeFeedback();
                    this._envelopeFeedback.Display = activeView.ScreenDisplay;
                    this._envelopeFeedback.Start(this._iPoint);
                }
                this._envelopeFeedback.MoveTo(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (!this._inZoom) return;
            this._inZoom = false;
            this.m_cursor = this._cursor;
            IActiveView activeView = (IActiveView)this._context.FocusMap;
            IEnvelope envelope;
            if (this._envelopeFeedback == null)
            {
                envelope = activeView.Extent;
                envelope.Expand(2.0, 2.0, true);
                envelope.CenterAt(this._iPoint);
            }
            else
            {
                IEnvelope envelope2 = this._envelopeFeedback.Stop();
                if (envelope2.Width == 0.0 || envelope2.Height == 0.0)
                {
                    envelope = activeView.Extent;
                    envelope.Expand(2.0, 2.0, true);
                    envelope.CenterAt(this._iPoint);
                }
                else
                {
                    double num = activeView.Extent.Width * (activeView.Extent.Width / envelope2.Width);
                    double num2 = activeView.Extent.Height * (activeView.Extent.Height / envelope2.Height);
                    envelope = new EnvelopeClass();
                    envelope.PutCoords(activeView.Extent.XMin - (envelope2.XMin - activeView.Extent.XMin) * (activeView.Extent.Width / envelope2.Width), activeView.Extent.YMin - (envelope2.YMin - activeView.Extent.YMin) * (activeView.Extent.Height / envelope2.Height), activeView.Extent.XMin - (envelope2.XMin - activeView.Extent.XMin) * (activeView.Extent.Width / envelope2.Width) + num, activeView.Extent.YMin - (envelope2.YMin - activeView.Extent.YMin) * (activeView.Extent.Height / envelope2.Height) + num2);
                }
            }
            activeView.Extent = envelope;
            this._envelopeFeedback = null;
            activeView.Refresh();
            
        }
        
    }
}

