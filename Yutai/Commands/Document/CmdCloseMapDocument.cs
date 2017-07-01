using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Document
{
    public class CmdCloseMapDocument : YutaiCommand
    {
        public CmdCloseMapDocument(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "关闭二维文档";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_project_open;
            base.m_name = "File.Mxd.CloseMXD";
            base._key = "File_Mxd_CloseMXD";
            base.m_toolTip = "关闭二维文档";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}