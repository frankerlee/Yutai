using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class ToolSceneDrawStaticLoc : YutaiTool
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        private ISurface isurface_0;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0) return false;

                if ((this._plugin.Scene as IBasicMap).LayerCount == 0)
                {
                    result = false;
                }
                else if (this._plugin.CurrentLayer == null)
                {
                    result = false;
                }
                else
                {
                    this.isurface_0 = SurfaceInfo.GetSurfaceFromLayer(this._plugin.CurrentLayer);
                    result = (this.isurface_0 != null && FlyByUtils.bStillDrawing);
                }
                return result;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public ToolSceneDrawStaticLoc(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_DrawStaticLoc";
            _itemType = RibbonItemType.Tool;
            this.m_bitmap = Properties.Resources.drawstaticloc;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.DrawStaticLoc.cur"));
            FlyByUtils.bDrawStatic = false;
            this.m_caption = "选择固定位置";
            this.m_category = "3D";
            this.m_name = "Scene_DrawStaticLoc";
        }

        public override void OnClick()
        {
            _plugin.CurrentTool = this;
            ToolSceneDrawFlyByPath.m_frm.Application = this._plugin;
            this.isurface_0 = SurfaceInfo.GetSurfaceFromLayer(this._plugin.CurrentLayer);
        }

        

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IPoint point = Utils3D.XYToPoint(this._plugin.SceneGraph, int_2, int_3);
            if (point != null)
            {
                point.Z = this.isurface_0.GetElevation(point);
                FlyByUtils.DeleteFlyByElement(this._plugin.SceneGraph, FlyByUtils.FlyByElementType.FLYBY_STATIC, false);
                FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, point, FlyByUtils.FlyByElementType.FLYBY_STATIC, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
                Utils3D.FlashLocation3D(this._plugin.SceneGraph, point);
                ToolSceneDrawFlyByPath.m_frm.StaticLoc = point;
            }
        }
    }
}