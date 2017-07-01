using System;
using System.Diagnostics;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public class CmdStartMaptemplateManager : YutaiCommand
    {
        public override void OnCreate(object hook)
        {
            this.m_category = "模板管理工具";
            this.m_caption = "管理";
            this.m_message = "启动模板管理工具";
            this.m_toolTip = "启动模板管理工具";
            this.m_name = "StartMaptemplateManager";
            base.m_bitmap = Properties.Resources.icon_map_manager;
            base.m_name = "Printing_StartMaptemplateManager";
            _key = "Printing_StartMaptemplateManager";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdStartMaptemplateManager(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            Process.Start("CartoTemplateApp.exe");
        }
    }
}