﻿using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Document
{
    public class CmdSaveSceneDocument : YutaiCommand
    {
        public CmdSaveSceneDocument(IAppContext context)
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
            base.m_caption = "保存三维文档";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_save;
            base.m_name = "File.Sxd.SaveSXD";
            base._key = "File.Sxd.SaveSXD";
            base.m_toolTip = "保存三维文档";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}