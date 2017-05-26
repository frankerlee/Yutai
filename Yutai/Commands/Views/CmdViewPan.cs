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
            base.m_name = "View.Common.Pan";
            base._key = "View.Common.Pan";
            base.m_toolTip = "平移";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }


        public override void OnMouseDown(int button, int shift, int x, int y)
        {

            if (button != 2)
            {
                _iPoint = ((IActiveView)_context.MapControl.ActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                ((IActiveView)_context.MapControl.ActiveView).ScreenDisplay.PanStart(_iPoint);
                this.m_cursor = this._cursor1;
                this._inZoom = true;
            }
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this._inZoom)
            {
                IActiveView focusMap = (IActiveView)_context.MapControl.ActiveView;
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
                IActiveView focusMap = (IActiveView)_context.MapControl.ActiveView;
                focusMap.Extent = focusMap.ScreenDisplay.PanStop();
                focusMap.Refresh();
            }
        }
    }

}

