using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Menu;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdCloseLayoutDesign : YutaiCommand
    {
        private PrintingPlugin _plugin;
        private MapTemplateViewService _dockService;
        private bool _enabled;

        public override bool Enabled
        {
            get
            {
                if (_plugin.IsDeign) return true;
                return false;
            }
        }

        public CmdCloseLayoutDesign(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as PrintingPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_design_close;
            this.m_caption = "退出模板";
            this.m_category = "Layout";
            this.m_message = "退出模板";
            this.m_name = "Layout_CloseLayoutDesign";
            this._key = "Layout_CloseLayoutDesign";
            this.m_toolTip = "退出模板";
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
            

            if (_dockService == null)
            {
                _dockService = _context.Container.GetInstance<MapTemplateViewService>();
            }
            if (_dockService.Visible == true)
            {
                _dockService.Hide();
                _dockService.ClearEvents();

            }
            _plugin.IsDeign = false;
        }
    }
}