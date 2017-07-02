using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Menu;

namespace Yutai.Plugins.Printing.Commands
{
    class CmdShowMapTemplateView : YutaiCommand
    {
        private MapTemplateViewService _dockService;

        public override bool Enabled
        {
            get
            {
                if (_context.MainView.ControlType == GISControlType.PageLayout)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        public CmdShowMapTemplateView(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_layout;
            this.m_caption = "模板管理";
            this.m_category = "Printing";
            this.m_message = "模板管理";
            this.m_name = "Printing_OpenMapTemplateView";
            this._key = "Printing_OpenMapTemplateView";
            this.m_toolTip = "模板管理";
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
                _dockService = _context.Container.GetInstance<MapTemplateViewService>();
            }
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                return;
            }
        }
    }
}