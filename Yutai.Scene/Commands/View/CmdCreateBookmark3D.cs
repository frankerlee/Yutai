using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.GlobeCore;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class CmdCreateBookmark3D : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                return this._plugin.Scene != null && this._plugin.SceneVisible && (this._plugin.Scene as IBasicMap).LayerCount > 0;

            }
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdCreateBookmark3D(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_CreateBookmark3D";
            _itemType = RibbonItemType.Button;
            this.m_caption = "创建书签";
            this.m_name = "Scene_CreateBookmark3D";
            this.m_toolTip = "创建书签";
            this.m_category = "书签";
            m_bitmap = Properties.Resources.bookmarkadd;
            _needUpdateEvent = true;

        }

       

        public override void OnClick()
        {
            new frmBookMark
            {
                Map = this._plugin.Scene as IBasicMap
            }.ShowDialog();
        }
    }
}