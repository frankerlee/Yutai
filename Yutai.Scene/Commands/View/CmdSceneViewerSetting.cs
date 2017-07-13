using System;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class CmdSceneViewerSetting : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0) return false;
                return true;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdSceneViewerSetting(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_ViewSetting";
            _itemType = RibbonItemType.Button;
            this.m_name = "Scene_ViewSetting";
            this.m_caption = "视图设置";
            this.m_bitmap = Properties.Resources.viewsetting;
        }

       

        public override void OnClick()
        {
            new frmViewSettings
            {
                MainSceneViewer = this._plugin.ActiveViewer
            }.ShowDialog();
        }
    }
}