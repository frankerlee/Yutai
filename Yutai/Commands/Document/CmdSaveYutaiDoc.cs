using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Commands.Document
{
    public class CmdSaveYutaiDoc : YutaiCommand
    {
        private IProjectService _projectService;

        public CmdSaveYutaiDoc(IAppContext context)
        {
            OnCreate(context);
            _projectService = context.Project as IProjectService;
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
            base.m_caption = "保存";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.yt_project_save;
            base.m_name = "File.Document.Sub.SaveProject";
            base._key = "File_Document_SaveProject";
            base.m_toolTip = "保存项目";
            base.m_checked = false;
            base.m_enabled = true;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base._itemType = RibbonItemType.Button;
        }
    }
}