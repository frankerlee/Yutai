﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Document
{
    //该命令打开后缀为.myt的文档，该文档应该为与mxd，sxd同名的文件，主要是用来描述二维三维文档的关系，以及里面图层的对应关系，然后用于后面的初始化分析
    public class CmdOpenYutaiDoc:YutaiCommand
    {
        public CmdOpenYutaiDoc(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开项目";
            dialog.Filter = "项目文档(*.yxd)|*.yxd";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            string fileName = dialog.FileName;
            //_context.SceneControl.LoadSxFile(fileName);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打开项目";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_project_open;
            base.m_name = "File.Document.OpenProject";
            base._key = "File.Document.OpenProject";
            base.m_toolTip = "打开项目";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}
