using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;

namespace Yutai.Plugins.Scene.Commands
{
    public class CmdOpenProjectSXD : YutaiCommand
    {
        private IScenePlugin _plugin;
       

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                if (_plugin.SceneView == null) return false;
                if (_plugin.SceneVisible == false) return false;
                return true;
            }
        }

        public CmdOpenProjectSXD(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as IScenePlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_open_projectsxd;
            this.m_caption = "项目三维文档";
            this.m_category = "Scene";
            this.m_message = "打开当前项目登记的三维文档";
            this.m_name = "Scene_OpenProjectSXD";
            this._key = "Scene_OpenProjectSXD";
            this.m_toolTip = "打开当前项目登记的三维文档";
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
            ISecureContext secureContext=_context as ISecureContext;
            if (secureContext.YutaiProject != null &&
                string.IsNullOrEmpty(secureContext.YutaiProject.SceneDocumentName) == false)
            {
                _plugin.SceneView.OpenSXD(secureContext.YutaiProject.SceneDocumentName);
            }
        }
    }
}