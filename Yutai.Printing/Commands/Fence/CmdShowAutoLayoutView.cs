using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Menu;

namespace Yutai.Plugins.Printing.Commands.Fence
{
    class CmdShowAutoLayoutView : YutaiCommand
    {
        private AutoLayoutViewService _dockService;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType == GISControlType.PageLayout || _context.MainView.ControlType == GISControlType.MapControl)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        public CmdShowAutoLayoutView(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_layout;
            this.m_caption = "分幅出图";
            this.m_category = "Printing";
            this.m_message = "分幅出图";
            this.m_name = "Printing_OpenAutoLayoutView";
            this._key = "Printing_OpenAutoLayoutView";
            this.m_toolTip = "分幅出图";
            _context = hook as IAppContext;
            _itemType = RibbonItemType.Button;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (_dockService == null)
            {
                _dockService = _context.Container.GetInstance<AutoLayoutViewService>();
            }
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                return;
            }
        }
    }
}