using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public sealed class CmdSceneFullExtent : YutaiCommand
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

        public CmdSceneFullExtent(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_FullExtent";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "全图";
            this.m_toolTip = "全图";
            this.m_name = "Scene_FullExtent";
            this.m_message = "全图";

            this.m_bitmap = Properties.Resources.fullextent;

        }

       

        public override void OnClick()
        {
            ISceneGraph sceneGraph = this._plugin.SceneGraph;
            ICamera camera = this._plugin.Camera;
            camera.SetDefaultsMBB(sceneGraph.Extent);
            sceneGraph.ActiveViewer.Redraw(true);
        }
    }
}