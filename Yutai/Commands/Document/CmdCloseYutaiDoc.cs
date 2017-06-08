using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Commands.Document
{
    public class CmdCloseYutaiDoc:YutaiCommand
    {
        public CmdCloseYutaiDoc(IAppContext context)
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
            base.m_caption = "关闭";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.yt_project_close;
            base.m_name = "File.Document.Sub.CloseProject";
            base._key = "File_Common_CloseProject";
            base.m_toolTip = "关闭项目";
            base.m_checked = false;
            base.m_enabled = true;
            base.TextImageRelationYT= TextImageRelationYT.ImageBeforeText;
            
            base._itemType = RibbonItemType.Button;
        }
    }
}
