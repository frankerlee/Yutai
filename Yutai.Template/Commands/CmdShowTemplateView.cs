using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Template.Menu;

namespace Yutai.Plugins.Template.Commands
{
    class CmdShowTemplateView : YutaiCommand
    {
        private TemplateViewService _dockService;

        public override bool Enabled
        {
            get { return true; }
        }

        public CmdShowTemplateView(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_template_manager;
            this.m_caption = "模板库管理";
            this.m_category = "TemplateDatabase";
            this.m_message = "模板库管理";
            this.m_name = "TemplateDB_OpenTemplateView";
            this._key = "TemplateDB_OpenTemplateView";
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
                _dockService = _context.Container.GetInstance<TemplateViewService>();
            }
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                return;
            }
        }
    }
}