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
using Yutai.Plugins.Events;
using Yutai.Plugins.Identifer.Menu;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    public class CmdViewIdentifier : YutaiTool
    {
        private IEnvelope _envelope;
        private IPoint _iPoint;
        private Cursor _cursor;
        private IdentifierPlugin _plugin;
        private INewEnvelopeFeedback _envelopeFeedback;
        private bool _inIdentify;
        private DockPanelService _dockService;

        public CmdViewIdentifier(IAppContext context, IdentifierPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }


        public override void OnClick()
        {
            _inIdentify = false;
            _plugin.FireStartMapIdentify(null);
            _context.SetCurrentTool(this);

            if (_dockService == null)
                _dockService = _context.Container.GetInstance<DockPanelService>();
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                return;
            }
        }

        public override bool Deactivate()
        {
            _plugin.FireUnMapIdentify(null);
            return base.Deactivate();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "查看地图要素";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_identify;
            base.m_cursor =
                new Cursor(
                    base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Identifer.Resources.Identify.cur"));
            base.m_name = "View_Identify";
            base._key = "View_Identify";
            base.m_toolTip = "查看地图要素";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 1) return;
            IPoint mouseDownPoint = _context.MapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            _iPoint = mouseDownPoint;
            _inIdentify = true;
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._inIdentify)
            {
                IActiveView focusMap = (IActiveView) _context.MapControl.ActiveView;
                if (_envelopeFeedback == null)
                {
                    _envelopeFeedback = new NewEnvelopeFeedback()
                    {
                        Display = focusMap.ScreenDisplay
                    };
                    _envelopeFeedback.Start(_iPoint);
                }
                _envelopeFeedback.MoveTo(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            IEnvelope extent;
            if (this._inIdentify)
            {
                this._inIdentify = false;
                IActiveView focusMap = (IActiveView) _context.MapControl.ActiveView;
                if (this._envelopeFeedback != null)
                {
                    extent = this._envelopeFeedback.Stop();
                }
                else
                {
                    IPoint point =
                        _context.MapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y) as IPoint;
                    extent = point.Envelope;
                }

                this._envelopeFeedback = null;
                _plugin.FireMapIdentifying(new MapIdentifyArgs(extent));
            }
        }
    }
}