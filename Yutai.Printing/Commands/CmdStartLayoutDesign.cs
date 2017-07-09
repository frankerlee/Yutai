using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Menu;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdStartLayoutDesign : YutaiCommand
    {
        private PrintingPlugin _plugin;
        private MapTemplateViewService _dockService;
        private AutoLayoutViewService _autoLayoutViewService;
        private bool _enabled;


        public override bool Enabled
        {
            get
            {
                if (_plugin.IsDeign) return false;
                if (!_plugin.IsLayout) return true;
                return true;

            }
        }

        public CmdStartLayoutDesign(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as PrintingPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_design_open;
            this.m_caption = "模板设计";
            this.m_category = "Layout";
            this.m_message = "启动模板设计";
            this.m_name = "Layout_StartLayoutDesign";
            this._key = "Layout_StartLayoutDesign";
            this.m_toolTip = "启动模板设计";
            _context = hook as IAppContext;
            this._itemType = RibbonItemType.Button;
            _needUpdateEvent = true;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            if (_context.MainView.ControlType != GISControlType.PageLayout)
            {
                _context.MainView.ActivatePageLayout();
            }

            if (_autoLayoutViewService == null)
            {
                _autoLayoutViewService=_context.Container.GetInstance<AutoLayoutViewService>();
            }
            _autoLayoutViewService.Hide();

            if (_dockService == null)
            {
                _dockService = _context.Container.GetInstance<MapTemplateViewService>();
            }
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                _dockService.InitEvents();
            }

            _plugin.IsDeign = true;
        }
    }
}