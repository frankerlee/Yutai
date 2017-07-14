using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public  class CmdSceneExpandFOV : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                if( this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount == 0) return false;

                ICamera camera = this._plugin.Camera;
                bool   result = (camera.ProjectionType != esri3DProjectionType.esriOrthoProjection);
                
                return result;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public CmdSceneExpandFOV(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_ExpandFOV";
            _itemType = RibbonItemType.Button;
            this.m_category = "视图";
            this.m_caption = "扩展观察野";
            this.m_toolTip = "扩展观察野";
            this.m_name = "Scene_ExpandFOV";
            this.m_message = "扩展观察野";


            this.m_bitmap = Properties.Resources.expand;

        }

       

        public override void OnClick()
        {
            ICamera camera = this._plugin.Camera;
            double viewFieldAngle = camera.ViewFieldAngle;
            camera.ViewFieldAngle = viewFieldAngle * 1.1;
            ISceneViewer activeViewer = this._plugin.ActiveViewer;
            activeViewer.Redraw(false);
        }
    }
}