using System;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;

namespace Yutai.Commands.Document
{
    public class CmdOpenMapDocument : YutaiCommand
    {
        public CmdOpenMapDocument(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            ISecureContext secureContext = _context as ISecureContext;
            if (secureContext.YutaiProject!=null && !string.IsNullOrEmpty(secureContext.YutaiProject.MapDocumentName))
            {
                if (MessageService.Current.Ask("当前打开的项目有链接的MXD文档，你确认需要进行替换吗?") == false)
                    return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开Mxd文档";
            dialog.Filter = "MXD文档(*.mxd)|*.mxd";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            string fileName = dialog.FileName;
            if (secureContext.YutaiProject == null)
            {
                secureContext.YutaiProject=new XmlProject(secureContext,"");
            }
            if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                _context.MapControl.LoadMxFile(dialog.FileName);
                secureContext.YutaiProject.MapDocumentName = fileName;
            }
            else if (_context.MainView.ControlType == GISControlType.PageLayout)
            {
                _context.MainView.PageLayoutControl.LoadMxFile(dialog.FileName);
                secureContext.YutaiProject.MapDocumentName = fileName;
            }
            else
            {
                _context.MainView.ActivateMap();
                _context.MapControl.LoadMxFile(dialog.FileName);
                secureContext.YutaiProject.MapDocumentName = fileName;
            }




        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打开二维文档";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_project_open;
            base.m_name = "File.Mxd.OpenMXD";
            base._key = "File_Mxd_OpenMXD";
            base.m_toolTip = "打开二维文档";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}