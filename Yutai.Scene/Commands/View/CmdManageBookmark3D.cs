using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class CmdManageBookmark3D : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                bool result;
                try
                {
                    int bookmarkCount = (this._plugin.Scene as ISceneBookmarks).BookmarkCount;
                    if (bookmarkCount > 0)
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

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdManageBookmark3D(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_ManageBookmark3D";
            _itemType = RibbonItemType.Button;
            this.m_caption = "管理书签";
            this.m_name = "Scene_ManageBookmark3D";
            this.m_toolTip = "管理书签";
            this.m_category = "书签";
            m_bitmap = Properties.Resources.bookmarkman;

        }

     
        public override void OnClick()
        {
            new frmManageBookMarker
            {
                Map = this._plugin.Scene as IBasicMap
            }.ShowDialog();
        }
    }
}