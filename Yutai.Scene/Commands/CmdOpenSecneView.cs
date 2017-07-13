using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands
{
    public class CmdOpenSceneView : YutaiCommand
    {
        private IScenePlugin _plugin;
        private bool _enabled;

        public override bool Enabled
        {
            get
            {
                if (_plugin==null) return false;
                if (_plugin.SceneView == null) return false;
                if (_plugin.SceneVisible == true) return false;
                return true;
            }
        }

        public CmdOpenSceneView(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as IScenePlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_secne;
            this.m_caption = "三维窗口";
            this.m_category = "Scene";
            this.m_message = "三维窗口";
            this.m_name = "Scene_SceneView_Open";
            this._key = "Scene_SceneView_Open";
            this.m_toolTip = "三维窗口";
            _context = hook as IAppContext;
            this._itemType = RibbonItemType.Button;
            _needUpdateEvent = true;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
           _plugin.ShowScene();
        }
    }
}
