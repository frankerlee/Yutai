using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public sealed class ToolSceneZoomInOut : YutaiTool
    {
        private System.Windows.Forms.Cursor cursor_0;

        private IAppContext _context;
        private IScenePlugin _plugin;

        private long long_0;

        private long long_1;

        private bool bool_0;

        private ControlsSceneZoomInOutTool controlsSceneZoomInOutToolClass_0 = new ControlsSceneZoomInOutTool();

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
      
        public ToolSceneZoomInOut(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_TargetZoomInOut";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "放大/缩小";
            this.m_toolTip = "放大/缩小";
            this.m_name = "Scene_TargetZoomInOut";
            this.m_message = "对场景动态放大和缩小";

            this.m_bitmap = Properties.Resources.zoominout;

            m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.ZOOMINOUT.CUR"));
          
        }

      
       

        public override bool Deactivate()
        {
            return true;
        }

        public override void OnClick()
        {
            this.controlsSceneZoomInOutToolClass_0.OnCreate(_plugin.SceneView.SceneControl);
            _plugin.SetCurrentTool(controlsSceneZoomInOutToolClass_0 as ITool);
        }
    }
}