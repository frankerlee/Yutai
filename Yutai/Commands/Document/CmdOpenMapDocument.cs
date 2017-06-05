using System;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Document
{
    public class CmdOpenMapDocument:YutaiCommand
    {
        public CmdOpenMapDocument(IAppContext context)
        {
           OnCreate(context);
        }

        public override void OnClick()
        {
           OpenFileDialog dialog=new OpenFileDialog();
            dialog.Title = "打开Mxd文档";
            dialog.Filter = "MXD文档(*.mxd)|*.mxd";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            string fileName = dialog.FileName;
            _context.MapControl.LoadMxFile(fileName,null,null);
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
            base._key = "File.Mxd.OpenMXD";
            base.m_toolTip = "打开二维文档";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
