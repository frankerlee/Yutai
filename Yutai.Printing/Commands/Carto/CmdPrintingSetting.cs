using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Views;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    class CmdPrintingSetting : YutaiCommand
    {
        public CmdPrintingSetting(IAppContext context)
        {
            OnCreate(context);
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            base.m_caption = "设置";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_clip_print2;
            base.m_name = "Printing_ConfigSetting";
            base._key = "Printing_ConfigSetting";
            base.m_toolTip = "制图设置";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        
            _context = hook as IAppContext;
        }


        public void OnClick()
        {
            _context.Config.LoadAllConfigPages = false;
            _context.Config.CustomConfigPages = "Printing";
            var model = _context.Container.GetInstance<ConfigViewModel>();
            _context.Container.Run<ConfigPresenter, ConfigViewModel>(model);
        }
    }
}