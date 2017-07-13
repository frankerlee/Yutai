using System;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands
{
    public class CmdOpenSXD : YutaiCommand
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

        public CmdOpenSXD(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as IScenePlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_open_sxd;
            this.m_caption = "打开三维文档";
            this.m_category = "Scene";
            this.m_message = "打开三维文档";
            this.m_name = "Scene_OpenSXD";
            this._key = "Scene_OpenSXD";
            this.m_toolTip = "打开三维文档";
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
            OpenFileDialog fileDialog=new OpenFileDialog();
            fileDialog.Title = "打开三维文档";
            fileDialog.Filter = "三维文档(*.sxd)|*.sxd";
            DialogResult result = fileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            _plugin.SceneView.OpenSXD(fileDialog.FileName);
            
        }
    }
}