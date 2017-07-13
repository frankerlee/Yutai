using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class ToolScenePan : YutaiTool
    {
        private IAppContext _context;
        private IScenePlugin _plugin;
        private ITool _tool;

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
            ((ICommand)_tool).OnCreate(_plugin.SceneView.SceneControl);
            OnClick();
        }

        public ToolScenePan(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_Pan";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "平移";
            this.m_toolTip = "平移";
            this.m_name = "Scene_Pan";
            this.m_message = "平移";
            this.m_bitmap = Properties.Resources.pan;
            _tool = new ControlsScenePanTool() as ITool;
            
        }

        public override void OnClick()
        {
            _plugin.SetCurrentTool(_tool);
        }
    }
}