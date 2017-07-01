// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdSortDescending.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  09:58
// 更新时间 :  2017/06/13  09:58

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Controls;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands.ContextMenu
{
    public class CmdSortDescending : YutaiCommand
    {
        private CompContextMenuStrip _menuStrip;

        public CmdSortDescending(IAppContext context, CompContextMenuStrip menuStrip)
        {
            _context = context;
            _menuStrip = menuStrip;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "降序排列";
            base.m_category = "TableEditorCMS";
            base.m_bitmap = null;
            base.m_name = "mnuSortDescending";
            base._key = "mnuSortDescending";
            base.m_toolTip = "降序排列";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }

        public override void OnClick()
        {
            _menuStrip.TableView.VirtualGridView.Sort(_menuStrip.ColumnIndex, ListSortDirection.Descending);
        }
    }
}