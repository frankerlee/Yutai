using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Views;

namespace Yutai.Commands.Settings
{
    public class CmdOpenSetting : YutaiCommand
    {
        public CmdOpenSetting(IAppContext context)
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
            base.m_category = "设置";
            base.m_bitmap = Properties.Resources.icon_open_setting;
            base.m_name = "Setting_OpenSetting";
            base._key = "Setting_OpenSetting";
            base.m_toolTip = "设置";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            DisplayStyleYT = DisplayStyleYT.ImageAndText;
            TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            _context = hook as IAppContext;
        }


        public override void OnClick()
        {
            _context.Config.LoadAllConfigPages = true;
            var model = _context.Container.GetInstance<ConfigViewModel>();
            _context.Container.Run<ConfigPresenter, ConfigViewModel>(model);
        }
    }
}