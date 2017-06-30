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
using Yutai.Properties;

namespace Yutai.Commands.Views
{
   
    public class CmdViewZoomIn:YutaiTool
    {
        private IPoint _iPoint;
        private INewEnvelopeFeedback _envelopeFeedback;
        private bool _inZoom;
        private Cursor _cursor;
        private Cursor _cursor1;
        private RibbonItemType _itemType;


        public CmdViewZoomIn(IAppContext context)
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
            base.m_caption = "放大";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_zoom_in;
            _cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.ZoomIn.cur"));
            _cursor1 = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.MoveZoomIn.cur"));
            base.m_name = "View_ZoomIn";
            base._key = "View_ZoomIn";
            base.m_toolTip = "放大";
            base.m_checked = false;
            base.m_message = "左键点击后按住拖动到想显示的范围后释放，图形将刷新";
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
                this.m_cursor = this._cursor1;
                this._inZoom = true;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._inZoom)
            {
                IActiveView activeView = (IActiveView)this._context.FocusMap;
                if (this._envelopeFeedback == null)
                {
                    this._envelopeFeedback = new NewEnvelopeFeedbackClass();
                    this._envelopeFeedback.Display = activeView.ScreenDisplay;
                    this._envelopeFeedback.Start(this._iPoint);
                }
                this._envelopeFeedback.MoveTo(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (!this._inZoom) return;

            this.m_cursor = this._cursor;
            this._inZoom = false;
            IActiveView activeView = (IActiveView)this._context.FocusMap;
            IEnvelope envelope;
            if (this._envelopeFeedback == null)
            {
                envelope = activeView.Extent;
                envelope.Expand(0.5, 0.5, true);
                envelope.CenterAt(this._iPoint);
            }
            else
            {
                envelope = this._envelopeFeedback.Stop();
                if (envelope.Width == 0.0 || envelope.Height == 0.0)
                {
                    envelope = activeView.Extent;
                    envelope.Expand(0.5, 0.5, true);
                    envelope.CenterAt(this._iPoint);
                }
            }
            activeView.Extent = envelope;
            this._envelopeFeedback = null;
            activeView.Refresh();

        }
    }

   
}
