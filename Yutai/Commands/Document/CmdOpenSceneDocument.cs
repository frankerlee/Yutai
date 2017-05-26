using System;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Document
{
    public class CmdOpenSceneDocument : YutaiCommand
    {
        public CmdOpenSceneDocument(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开Sxd文档";
            dialog.Filter = "SXD文档(*.sxd)|*.sxd";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            string fileName = dialog.FileName;
            _context.SceneControl.LoadSxFile(fileName);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打开三维文档";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_project_open;
            base.m_name = "File.Sxd.OpenSXD";
            base._key = "File.Sxd.OpenSXD";
            base.m_toolTip = "打开三维文档";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}