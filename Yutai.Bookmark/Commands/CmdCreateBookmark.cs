// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdCreateBookmark.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/05/31  18:19
// 更新时间 :  2017/05/31  18:19

using System;
using System.Windows.Forms;
using Yutai.Plugins.Bookmark.Views;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Bookmark.Commands
{
    public class CmdCreateBookmark : YutaiCommand
    {
        public CmdCreateBookmark(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmCreateBookmarkView frm = new frmCreateBookmarkView(_context);
            frm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "创建";
            base.m_category = "View";
            base.m_bitmap = Resources.Bookmark_add;
            base.m_name = "View.Bookmark.CreateBookmark";
            base._key = "View.Bookmark.CreateBookmark";
            base.m_toolTip = "创建书签";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}