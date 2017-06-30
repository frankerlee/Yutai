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
    public class CmdViewPan : YutaiTool
    {
        private IPoint _iPoint;
        private bool _inZoom;
        private Cursor _cursor;
        private Cursor _cursor1;

        public CmdViewPan(IAppContext context)
        {
            OnCreate(context);
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
            base.m_caption = "平移";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_pan;
            _cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.Hand.cur"));
            _cursor1 = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.MoveHand.cur"));
            base.m_name = "View_Pan";
            base._key = "View_Pan";
            base.m_toolTip = "平移";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button == 2) return;

            IPoint point;
            if (this._context.ActiveView is IPageLayout)
            {
                point = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                IMap map = this._context.ActiveView.HitTestMap(point);
                if (map == null)
                {
                    return;
                }
                if (map != this._context.FocusMap)
                {
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
                point = ((IActiveView)this._context.MainView.PageLayoutControl.ActiveView.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                ((IActiveView)this._context.MainView.PageLayoutControl.ActiveView.FocusMap).ScreenDisplay.PanStart(point);
                this.m_cursor = this._cursor1;
                this._inZoom = true;
                return;
            }
            point = ((IActiveView)this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            ((IActiveView)this._context.FocusMap).ScreenDisplay.PanStart(point);
            this.m_cursor = this._cursor1;
            this._inZoom = true;
           
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._inZoom)
            {
                if (this._context.ActiveView is IPageLayout)
                {
                    IPoint point = ((IActiveView)this._context.MainView.PageLayoutControl.ActiveView.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                    ((IActiveView)this._context.MainView.PageLayoutControl.ActiveView.FocusMap).ScreenDisplay.PanMoveTo(point);
                    return;
                }
                IActiveView focusMap = (IActiveView)_context.FocusMap;
                focusMap.ScreenDisplay.PanMoveTo(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            IEnvelope extent;
            if (this._inZoom)
            {
                this.m_cursor = this._cursor;
                this._inZoom = false;
                if (this._context.ActiveView is IPageLayout)
                {
                    IActiveView focusMap2 = this._context.MainView.PageLayoutControl.ActiveView.FocusMap as IActiveView;
                    focusMap2.Extent = focusMap2.ScreenDisplay.PanStop();
                    focusMap2.Refresh();
                    return;
                }

                IActiveView focusMap = (IActiveView)_context.FocusMap;
                focusMap.Extent = focusMap.ScreenDisplay.PanStop();
                focusMap.Refresh();
            }
        }
    }

}

