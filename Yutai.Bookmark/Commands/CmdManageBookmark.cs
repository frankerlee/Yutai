// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdManageBookmark.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/01  10:59
// 更新时间 :  2017/06/01  10:59

using System;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Bookmark.Views;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Bookmark.Commands
{
    public class CmdManageBookmark : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                if (this._context.FocusMap == null)
                {
                    return false;
                }

                try
                {
                    IEnumSpatialBookmark bookmarks = (this._context.FocusMap as IMapBookmarks).Bookmarks;
                    bookmarks.Reset();
                    ISpatialBookmark spatialBookmark = bookmarks.Next();
                    if (spatialBookmark != null)
                    {
                        return true;
                    }
                }
                catch
                {
                }

                return false;
            }
        }

        public CmdManageBookmark(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmManageBookmark frm = new frmManageBookmark(_context);
            frm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "管理";
            base.m_category = "View";
            base.m_bitmap = Resources.Bookmark;
            base.m_name = "View_Bookmark_ManageBookmark";
            base._key = "View_Bookmark_ManageBookmark";
            base.m_toolTip = "管理书签";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}