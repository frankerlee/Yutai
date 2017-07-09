using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdCloseLayout : YutaiCommand
    {
        private PrintingPlugin _plugin;
        private bool _enabled;

        public override bool Enabled
        {
            get
            {
                if (_plugin.IsLayout) return true;
                return false;
            }
        }

        public CmdCloseLayout(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as PrintingPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_layout_close;
            this.m_caption = "退出制图";
            this.m_category = "Layout";
            this.m_message = "退出制图";
            this.m_name = "Layout_CloseLayout";
            this._key = "Layout_CloseLayout";
            this.m_toolTip = "退出制图";
            _context = hook as IAppContext;
            this._itemType = RibbonItemType.Button;
            ;
            _needUpdateEvent = true;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            _context.MainView.ActivateMap();
            _plugin.IsLayout = false;
        }
    }
}