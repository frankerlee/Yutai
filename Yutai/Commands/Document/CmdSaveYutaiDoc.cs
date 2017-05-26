using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Document
{
    public class CmdSaveYutaiDoc : YutaiCommand
    {
        public CmdSaveYutaiDoc(IAppContext context)
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
            base.m_caption = "保存项目";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_save;
            base.m_name = "File.Document.SaveProject";
            base._key = "File.Document.SaveProject";
            base.m_toolTip = "保存项目";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}